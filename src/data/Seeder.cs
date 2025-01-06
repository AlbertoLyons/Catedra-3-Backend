using Bogus;
using Catedra_3_Backend.src.models;

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
                if (!context.Users.Any())
                {
                    var userFaker = new Faker<User>()
                    .RuleFor(u => u.Email, f => f.Internet.Email());
                    var users = userFaker.Generate(100);
                    foreach (var user in users)
                    {
                        
                    }
                    await context.SaveChangesAsync();
                }
            }
           
        }
    }
}