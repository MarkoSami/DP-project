using Giftify.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Giftify.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configuring auto-increment for Id (optional, Entity Framework does it automatically)
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id); // Ensure Id is set as primary key
            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();


            // Configure one-to-one relationship between Cart and ApplicationUser
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)  // Cart has one User
                .WithOne(u => u.Cart) // ApplicationUser has one Cart
                .HasForeignKey<Cart>(c => c.UserId) // Define the foreign key in Cart
                .IsRequired(); // Cart must have a User (optional if not needed)




            #region Seeding

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction", Description = "Books that contain content derived from the imagination." },
                new Category { Id = 2, Name = "Non-Fiction", Description = "Books that are based on real facts and information." },
                new Category { Id = 3, Name = "Science", Description = "Books covering topics in science and technology." },
                new Category { Id = 4, Name = "History", Description = "Books that explore historical events and figures." },
                new Category { Id = 5, Name = "Mystery", Description = "Books that explore mystery events and figures." }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Price = 9.99,
                    Stock = 10,
                    Description = "The story primarily concerns the young and mysterious millionaire Jay Gatsby and his quixotic passion and obsession with the beautiful former debutante Daisy Buchanan.",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I",
                    CategoryId = 1 // Matches 'Fiction'
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Price = 7.99,
                    Stock = 10,
                    Description = "A novel about the serious issues of rape and racial inequality, observed through the eyes of a young girl.",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I",
                    CategoryId = 5 // Matches 'Mystery'
                }
            );


            #endregion
        }
    }
}

