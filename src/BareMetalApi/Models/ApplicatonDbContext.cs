using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BareMetalApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base()
        { }

        public ApplicationDbContext(DbContextOptions<DbContext> options) : base(options)
        { }

        //object name must be same as table name defined in Migrations
        public DbSet<BlogArticle> BlogArticles{ get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "localhost";
            string dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            string dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password";
            string pgConStr = string.Format("Host={0};Port=5432;Database=Blog;User ID={1};Password={2}", dbName, dbUser, dbPassword);
            builder.UseNpgsql(pgConStr);
            base.OnConfiguring(builder);
        }

    }
}
