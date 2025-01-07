using Bogus;
using Catedra_3_Backend.src.models;
using Microsoft.AspNetCore.Identity;

namespace Catedra_3_Backend.src.data
{
    public class Seeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                if (!context.Users.Any())
                {
                    var userFaker = new Faker<User>()
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.UserName, (f, u) => u.Email);
                    var users = userFaker.Generate(100);
                    foreach (var user in users)
                    {
                        var userResult = await userManager.CreateAsync(user, "P4ssw0rd");
                        if (userResult.Succeeded)
                        {
                            Console.WriteLine($"User {user.Email} created");
                            await userManager.AddToRoleAsync(user, "User");
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
           
        }
    }
}