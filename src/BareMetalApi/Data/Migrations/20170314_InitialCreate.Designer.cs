using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BareMetalApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170314_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("BareMetalApi.Data.Entities.BlogArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleTitle");

                    b.Property<string>("ArticleContent");

                    b.HasKey("Id");
                    //Table name must be same as DbSet
                    b.ToTable("BlogArticles");
                });
        }
    }
}