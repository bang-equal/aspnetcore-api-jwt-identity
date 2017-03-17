using Microsoft.EntityFrameworkCore.Migrations;

namespace BareMetalApi.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                //Table name must be same as DbSet
                name: "BlogArticles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        //upgrading from Npgsql.EntityFrameworkCore.PostgreSQL 1.0.0
                        //caused error on following line
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    ArticleTitle = table.Column<string>(nullable: true),
                    ArticleContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogArticles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogArticles");
        }
    }
}
