using AuctionSite.Database;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace AuctionSite.Tests.Integration
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected readonly WebApplicationFactory<Program> _webFactory;
        private readonly string _authUsername = "authorized";

        public IntegrationTest()
        {
            _webFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                    // app will not ask you to add admin if one does not exist
                    Console.SetIn(new StringReader("no"));
                });
            });

            using (var scope = _webFactory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            _httpClient = _webFactory.CreateClient();
        }

        protected async Task Authenticate()
        {
            var addUserRequest = new RegisterRequest
            {
                Email = "auth@email.com",
                Password = "zaq1@WSX",
                Username = _authUsername,
            };

            await _httpClient.PostAsJsonAsync("api/auth/register", addUserRequest);

            var loginCommand = new LoginRequest
            {
                Password = addUserRequest.Password,
                Email = addUserRequest.Email,
            };

            var token = await (await _httpClient.PostAsJsonAsync("api/auth/login", loginCommand))
                .Content.ReadFromJsonAsync<DataResponseModel<LoginResponse>>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer", token.Data.AuthToken);
        }

        protected ApplicationUser GetAuthenticatedUser()
            => GetFromDatabase<ApplicationUser>().FirstOrDefault(x => x.UserName == _authUsername);

        protected async Task AddToDatabase<T>(List<T> items) where T : class
        {
            var scope = _webFactory.Services.CreateScope();

            using (scope)
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                await dbContext.Set<T>().AddRangeAsync(items);
                await dbContext.SaveChangesAsync();
            }
        }

        protected async Task AddToDatabase<T>(T item) where T : class
        {
            var scope = _webFactory.Services.CreateScope();

            using (scope)
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                await dbContext.Set<T>().AddAsync(item);
                await dbContext.SaveChangesAsync();
            }
        }

        protected IEnumerable<T> GetFromDatabase<T>() where T : class
        {
            var scope = _webFactory.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
            return dbContext.Set<T>().AsEnumerable();
        }

        public void Dispose()
        {
            using (var scope = _webFactory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>();
                db.Database.EnsureDeleted();
            }
        }
    }
}
