using Giftify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Giftify.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Company> Companies{ get; set; }
        public AppDbContext() : base()
        {
            
        }


        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>().HasData(
                new Company()
                {
                    Id = 1,
                    Name = "Walmart",
                    City = "Cairo",
                    Country = "Egypt",
                    Phone = "01264543232",
                    PostalCode = "123123",
                    StreetAddress = "21 Imaginary street"
                },
                   new Company()
                   {
                       Id = 2,
                       Name = "Karfour",
                       City = "Cairo",
                       Country = "Egypt",
                       Phone = "012645213232",
                       PostalCode = "125123",
                       StreetAddress = "26 Imaginary street"
                   }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "PUBG" },
                new Category { Id = 2, Name = "Roblox" },
                new Category { Id = 3, Name = "Binance" },
                new Category { Id = 4, Name = "TikTok" },
                new Category { Id = 5, Name = "FIFA" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "TikTok 10$",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"\r\n\r\n",
                    Price = 400,
                    Price10 = 380,
                    Price20 = 370,
                    CategoryId = 4,
                    ImageURL = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "TikTok 20$",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"\r\n\r\n",
                    Price = 800,
                    Price10 = 780,
                    Price20 = 770,
                    CategoryId = 4,
                    ImageURL=""
                },
                new Product
                {
                    Id = 3,
                    Title = "PUBG 10$",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"\r\n\r\n",
                    Price = 350,
                    Price10 = 340,
                    Price20 = 330,
                    CategoryId = 1,
                    ImageURL=""
                }
                );
                base.OnModelCreating(modelBuilder);
            }
        }
    }

