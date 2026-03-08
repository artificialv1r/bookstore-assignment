using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        // Kreiranje prvog urednika
        var editor1 = await userManager.FindByNameAsync("pera");
        if (editor1 == null)
        {
            editor1 = new ApplicationUser
            {
                UserName = "pera",
                Email = "pera.peric@example.com",
                Name = "Pera",
                Surname = "Peric",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(editor1, "Test123!");
        }

        if (!await userManager.IsInRoleAsync(editor1, "Editor"))
        {
            await userManager.AddToRoleAsync(editor1, "Editor");
        }
        
        // Kreiranje drugog urednika
        var editor2 = await userManager.FindByNameAsync("sava");
        if (editor2 == null)
        {
            editor2 = new ApplicationUser
            {
                UserName = "sava",
                Email = "sava.savic@example.com",
                Name = "Sava",
                Surname = "Savic",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(editor2, "Test123!");
        }

        if (!await userManager.IsInRoleAsync(editor2, "Editor"))
        {
            await userManager.AddToRoleAsync(editor2, "Editor");
        }
        
        // Kreiranje treceg urednika
        var editor3 = await userManager.FindByNameAsync("marko");
        if (editor3 == null)
        {
            editor3 = new ApplicationUser
            {
                UserName = "marko",
                Email = "marko.markovic@example.com",
                Name = "Marko",
                Surname = "Markovic",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(editor3, "Test123!");
        }

        if (!await userManager.IsInRoleAsync(editor3, "Editor"))
        {
            await userManager.AddToRoleAsync(editor3, "Editor");
        }
    }

}