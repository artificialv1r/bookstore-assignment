using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApplication.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    
    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration,IMapper mapper)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task RegisterAsync(RegistrationDto data)
    {
        var user = _mapper.Map<ApplicationUser>(data);
        var result = await _userManager.CreateAsync(user, data.Password);
        if (!result.Succeeded)
        {
            string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errorMessage);
        }
    }
    
    public async Task<string> Login(LoginDto data)
    {
        var user = await _userManager.FindByNameAsync(data.Username);
        if (user == null)
        {
            throw new BadRequestException("Invalid credentials.");
        }

        var passwordMatch = await _userManager.CheckPasswordAsync(user, data.Password);
        if (!passwordMatch)
        {
            throw new BadRequestException("Invalid credentials.");
        }
        var token = await GenerateJwt(user);
        return token;
    }

    private async Task<string> GenerateJwt(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),  // 'sub' atribut
            new Claim("username", user.UserName),  // 'username' atribut
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // jedinstveni identifikator tokena
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal)
    {
        var username = userPrincipal.FindFirstValue("username");

        if (username == null)
        {
            throw new BadRequestException("Token is invalid");
        }
  
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            throw new NotFoundException(int.Parse(user.Id));
        }

        return _mapper.Map<ProfileDto>(user);
    }

}