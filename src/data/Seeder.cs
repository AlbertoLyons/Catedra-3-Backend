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
                Random random = new Random();
                if (!context.Users.Any())
                {
                    var userFaker = new Faker<User>()
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.SecurityStamp, f => Guid.NewGuid().ToString())
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
                if (!context.Posts.Any())
                {
                    var users = context.Users.ToList();
                    if (users.Count > 0)
                    {
                        var postFaker = new Faker<Post>()
                        .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                        .RuleFor(p => p.PostDate, f => f.Date.Past())
                        .RuleFor(p => p.Url, f => f.Image.PicsumUrl())
                        .RuleFor(p => p.UserId, f => users[random.Next(users.Count - 1)].Id);
                        var posts = postFaker.Generate(100);
                        context.Posts.AddRange(posts);
                        await context.SaveChangesAsync();
                    }

                }
            }
           
        }
    }
}