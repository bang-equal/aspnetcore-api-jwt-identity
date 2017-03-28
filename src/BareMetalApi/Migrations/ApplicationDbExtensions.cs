using System.Linq;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;

namespace BareMetalApi.Migrations
{
    public static class ApplicationDbExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.BlogArticles.Any())
                {
                    context.BlogArticles.AddRange(
                        new BlogArticle { ArticleTitle = "How to Dab", ArticleContent = "First tuck you head down" },
                        new BlogArticle { ArticleTitle = "How to Whip", ArticleContent = "Rock back and forth"  },
                        new BlogArticle { ArticleTitle = "How to Nae Nae", ArticleContent = "Add a connecting move"  },
                        new BlogArticle { ArticleTitle = "How to Dougie", ArticleContent = "Pass your hand through"  },
                        new BlogArticle { ArticleTitle = "How to Wop", ArticleContent = "Worm your upper body"  });

                    context.SaveChanges();
                }
            }
        }
    }
}