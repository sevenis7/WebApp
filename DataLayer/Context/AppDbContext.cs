using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<BlogArticle> BlogArticles { get; set; }

        public DbSet<Service> Services { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                List<User> users = new List<User>
            {
                new User
                {
                    UserId = Guid.NewGuid(),
                    Login = "admin",
                    Email = "testadmin@gmail.com",
                    FirstName = "admin",
                    LastName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Role = Role.Admin
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Login = "user",
                    Email = "testuser@gmail.com",
                    FirstName = "user",
                    LastName = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user"),
                    Role = Role.User
                }
            };

            List<Request> requests = new List<Request>
            {
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = DateTime.Now,
                    RequestId = 1,
                    Text = "testRecieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.InWork,
                    Date = DateTime.Now,
                    RequestId = 2,
                    Text = "testInWork",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Completed,
                    Date = DateTime.Now,
                    RequestId = 3,
                    Text = "testCompleted",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Rejected,
                    Date = DateTime.Now,
                    RequestId = 4,
                    Text = "testRejected",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Canceled,
                    Date = DateTime.Now,
                    RequestId = 5,
                    Text = "testCanceled",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = new DateTime(2023,9,1),
                    RequestId = 6,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = new DateTime(2023,9,1),
                    RequestId = 7,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = new DateTime(2023,8,1),
                    RequestId = 8,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = new DateTime(2023,8,1),
                    RequestId = 9,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x => x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = new DateTime(2023,8,31),
                    RequestId = 10,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Received,
                    Date = new DateTime(2023,8,31),
                    RequestId = 11,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                }
            };

            List<Project> projects = new List<Project>()
            {
                new Project
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    ImageBase64 = Convert.ToBase64String(File.ReadAllBytes("Images\\default-image.jpg"))
                }
            };

            List<BlogArticle> blogArticles = new()
            {
                new()
                {
                    Id = 1,
                    ImageBase64 = Convert.ToBase64String(File.ReadAllBytes("Images\\default-image.jpg")),
                    PublicationDate = new DateTime(2022,10,10),
                    Description = "test short tescription",
                    Title = "test Title",
                    Text = "TestText"
                }
            };

            List<Service> services = new List<Service>
            {
                new Service
                {
                    Id = 1,
                    Title = "OUR MAIN SERVICE",
                    Text = "TextTest",
                }
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Request>().HasData(requests);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<BlogArticle>().HasData(blogArticles);
            modelBuilder.Entity<Service>().HasData(services);
        }
    }
}
