using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<MainContent> MainContent { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ContactLink> ContactLinks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
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
                    Status = RequestStatus.Receieved,
                    DateTime = DateTime.Now,
                    RequestId = 1,
                    Text = "testRecieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.InWork,
                    DateTime = DateTime.Now,
                    RequestId = 2,
                    Text = "testInWork",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Completed,
                    DateTime = DateTime.Now,
                    RequestId = 3,
                    Text = "testCompleted",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Rejected,
                    DateTime = DateTime.Now,
                    RequestId = 4,
                    Text = "testRejected",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Canceled,
                    DateTime = DateTime.Now,
                    RequestId = 5,
                    Text = "testCanceled",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Receieved,
                    DateTime = new DateTime(2023,9,1),
                    RequestId = 6,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Receieved,
                    DateTime = new DateTime(2023,9,1),
                    RequestId = 7,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Receieved,
                    DateTime = new DateTime(2023,8,1),
                    RequestId = 8,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Receieved,
                    DateTime = new DateTime(2023,8,1),
                    RequestId = 9,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x => x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Receieved,
                    DateTime = new DateTime(2023,8,31),
                    RequestId = 10,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                },
                new Request
                {
                    Status = RequestStatus.Receieved,
                    DateTime = new DateTime(2023,8,31),
                    RequestId = 11,
                    Text = "testReceieved",
                    UserId = users.FirstOrDefault(x=>x.Login == "user")!.UserId
                }
            };

            MainContent mainContent = new MainContent
            {
                MainContentId = 1,
                Title = "Консалтинг без регистрации и смс"
            };

            List<Project> projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Title = "Оработка кредитных заявок в режиме онлайн",
                    ImagePath = @"C:\\Users\\callm\\Desktop\\sber.jpg",
                    Description = "Описание проектаОписание проектаОписание проекта"
                }
            };

            List<Article> articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "ArticleTitle",
                    Description = "ArticleDescription"
                }
            };

            List<ContactLink> links = new List<ContactLink>
            {
                new ContactLink
                {
                    Id = 1,
                    Url = "www.google.com"
                }
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Request>().HasData(requests);
            modelBuilder.Entity<MainContent>().HasData(mainContent);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<Article>().HasData(articles);
            modelBuilder.Entity<ContactLink>().HasData(links);
        }
    }
}
