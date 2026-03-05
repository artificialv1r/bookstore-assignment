using System.Security.Claims;
using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegistrationDto data);
    Task<string>  Login(LoginDto data);
    Task<ProfileDto> GetProfile(ClaimsPrincipal userPrincipal);
}