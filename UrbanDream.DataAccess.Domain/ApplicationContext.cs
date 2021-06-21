using BazarJok.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BazarJok.DataAccess.Models.Pins;
using BazarJok.DataAccess.Models.System;
using BazarJok.DataAccess.Models.Users;

namespace BazarJok.DataAccess.Domain
{
    public sealed class ApplicationContext : DbContext
    {
         public ApplicationContext(DbContextOptions options) : base(options)
        {
            try
            {
                // It should throw exception when migrations are not available,
                // for example in a tests
                // Database.Migrate();
                Database.EnsureCreated();
            }
            catch (InvalidOperationException)
            {
                Database.EnsureCreated();
            }
                
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<ProblemPin> ProblemPins { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var superAdmin = new Admin
            {
                CreationDate = DateTime.Now,
                Login = "Admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123123"),
                Role = AdminRole.Developer
            };
            modelBuilder.Entity<Admin>().HasData(superAdmin);

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Login)
                .IsUnique();

            modelBuilder.Entity<Image>()
            .HasOne(p => p.ProblemPin)
            .WithMany(b => b.Images);

            modelBuilder.Entity<Tag>()
              .HasMany(s => s.Pins)
              .WithMany(c => c.Tags);

        }
    }
}
