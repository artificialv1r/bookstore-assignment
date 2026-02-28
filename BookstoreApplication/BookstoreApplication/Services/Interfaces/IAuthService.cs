using BookstoreApplication.Models;
using BookstoreApplication.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegistrationDto data);
    Task Login(LoginDto data);
}