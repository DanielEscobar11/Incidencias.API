using Incidencias.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Incidencias.API.Data.Seed
{
    public static class DataSeeder
    {
        public async static Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var role in Roles.Todos)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = role
                    });
        
            }

            var superUsuario = "admin@incidencias.com";

            if(await userManager.FindByEmailAsync(superUsuario) == null) 
            {
                //que el usuario no existe 
                var usuario = new ApplicationUser() { UserName = superUsuario, Email = superUsuario, EmailConfirmed = true };
                var result = await userManager.CreateAsync(usuario, "SuperContraseña123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(usuario, Roles.Todos);
                }
            }
        }
    
    }

    public static class Roles 
    {
        private static string Admin = "Admin";
        private static string Tecnic = "Tecnico";
        private static string Usuario = "Usuario";

        public static List<string> Todos = new List<string>() { Admin, Tecnic, Usuario };

    }
}
