using FinalProjectEntityFramework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectEntityFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext<ChoreUser>
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chore> Chores { get; set; }
        public virtual DbSet<ChoreUser> ChoreUsers { get; set; }
        public virtual DbSet<ChoreMonths> ChoreMonths { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // DATA SEEDING

            builder.Entity<Category>()
                .HasData(
                    new Category()
                    {
                        Id = 1,
                        Name = "Cleaning"
                    },
                    new Category()
                    {
                        Id = 2,
                        Name = "Shopping"
                    }, new Category()
                    {
                        Id = 3,
                        Name = "Finances"
                    },
                    new Category()
                    {
                        Id = 4,
                        Name = "Yardwork"
                    }
                );

            var user1 = new ChoreUser()
            {
                Id = "1a",
                FirstName = "Jaeden",
                LastName = "Fuller",
                Email = "email@gmail.com",
                EmailConfirmed = true,
                UserName = "email@gmail.com",
                NormalizedUserName = "EMAIL@GMAIL.COM"
            };

            var user2 = new ChoreUser()
            {
                Id = "2a",
                FirstName = "John",
                LastName = "Doe",
                Email = "unoriginal@email.com",
                EmailConfirmed = true,
                UserName = "unoriginal@email.com",
                NormalizedUserName = "UNORIGINAL@EMAIL.COM"
            };

            var user3 = new ChoreUser()
            {
                Id = "3a",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "moreunoriginal@email.com",
                EmailConfirmed = true,
                UserName = "moreunoriginal@email.com",
                NormalizedUserName = "MOREUNORIGINAL@EMAIL.COM"
            };

            PasswordHasher<ChoreUser> ph = new PasswordHasher<ChoreUser>();
            user1.PasswordHash = ph.HashPassword(user1, "1234*Pass");
            user2.PasswordHash = ph.HashPassword(user2, "$Pass5678");
            user3.PasswordHash = ph.HashPassword(user3, "Pass%1010");

            builder.Entity<ChoreUser>().HasData(user1, user2, user3);

            builder.Entity<Chore>()
                .HasData(
                    new Chore()
                    {
                        Id = 1,
                        Name = "Do dishes",
                        ChoreType = ChoreType.Weekly,
                        IsComplete = false,
                        ChoreUserId = "1a",
                        CategoryId = 1,
                    },
                    
                    new Chore()
                    {
                        Id = 2,
                        Name = "Do taxes",
                        ChoreType = ChoreType.SemiMonthly,
                        IsComplete = false,
                        ChoreUserId = "2a",
                        CategoryId = 3,
                    },

                    new Chore()
                    {
                        Id = 3,
                        Name = "Pay ticket",
                        ChoreType = ChoreType.Once,
                        IsComplete = false,
                        ChoreUserId = null,
                        CategoryId = null,
                    },

                    new Chore()
                    {
                        Id = 4,
                        Name = "Tidy the house",
                        ChoreType = ChoreType.Daily,
                        IsComplete = true,
                        ChoreUserId = "2a",
                        CategoryId = 1,
                    },

                    new Chore()
                    {
                        Id = 5,
                        Name = "Vacuum house",
                        ChoreType = ChoreType.Monthly,
                        IsComplete = false,
                        ChoreUserId = "3a",
                        CategoryId = 1,
                    },

                    new Chore()
                    {
                        Id = 6,
                        Name = "Stock up for the year",
                        ChoreType = ChoreType.Annual,
                        IsComplete = false,
                        ChoreUserId = "1a",
                        CategoryId = 2,
                    },

                    new Chore()
                    {
                        Id = 7,
                        Name = "Eat dinner",
                        ChoreType = ChoreType.Daily,
                        IsComplete = false,
                        ChoreUserId = null,
                        CategoryId = null,
                    },

                    new Chore()
                    {
                        Id = 8,
                        Name = "Buy groceries",
                        ChoreType = ChoreType.Weekly,
                        IsComplete = true,
                        ChoreUserId = "1a",
                        CategoryId = 2,
                    },

                    new Chore()
                    {
                        Id = 9,
                        Name = "Shovel",
                        ChoreType = ChoreType.SemiMonthly,
                        IsComplete = false,
                        ChoreUserId = "3a",
                        CategoryId = 4,
                    },

                    new Chore()
                    {
                        Id = 10,
                        Name = "Pay car insurance",
                        ChoreType = ChoreType.Monthly,
                        IsComplete = false,
                        ChoreUserId = "1a",
                        CategoryId = 3,
                    }

                );

            builder.Entity<ChoreMonths>()
                .HasData(
                    new ChoreMonths()
                    {
                        Id = 1,
                        Month = Month.January,
                        ChoreId = 2
                    },
                    new ChoreMonths()
                    {
                        Id = 2,
                        Month = Month.July,
                        ChoreId = 2
                    },
                    new ChoreMonths()
                    {
                        Id = 3,
                        Month = Month.September,
                        ChoreId = 3
                    },
                    new ChoreMonths()
                    {
                        Id = 4,
                        Month = Month.January,
                        ChoreId = 6
                    },
                    new ChoreMonths()
                    {
                        Id = 5,
                        Month = Month.January,
                        ChoreId = 9
                    },
                    new ChoreMonths()
                    {
                        Id = 6,
                        Month = Month.February,
                        ChoreId = 9
                    },
                    new ChoreMonths()
                    {
                        Id = 7,
                        Month = Month.March,
                        ChoreId = 9
                    },
                    new ChoreMonths()
                    {
                        Id = 8,
                        Month = Month.November,
                        ChoreId = 9
                    },
                    new ChoreMonths()
                    {
                        Id = 9,
                        Month = Month.December,
                        ChoreId = 9
                    }
                );
        }
    }
}