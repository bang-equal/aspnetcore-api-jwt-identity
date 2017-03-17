using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Data.Entities;

namespace BareMetalApi.Data
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        public async Task<bool> DoesItemExist(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var dbEntity = await context.BlogArticles.AnyAsync(blogarticle => blogarticle.Id == id);
                return dbEntity;
            }
        }
        public async Task <IList<BlogArticle>> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                var dbEntity = await context.BlogArticles.ToListAsync();
                return dbEntity;
            }
        }

        public async Task <BlogArticle> GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var dbEntity = await context.BlogArticles.FirstOrDefaultAsync();
                return dbEntity;
            }
        }

        public async Task <int> AddAsync(BlogArticle blogarticle)
        {
            using (var context = new ApplicationDbContext())
            {
                context.BlogArticles.Add(new BlogArticle { ArticleTitle = blogarticle.ArticleTitle, ArticleContent = blogarticle.ArticleContent});
                return await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(BlogArticle blogarticle)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Attach(blogarticle);
                var entry = context.Entry(blogarticle);
                entry.Property(e => e.ArticleTitle).IsModified = true;
                entry.Property(e => e.ArticleContent).IsModified = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(BlogArticle blogarticle)
        {
            using (var context = new ApplicationDbContext())
            {
                context.BlogArticles.Remove(blogarticle);
                await context.SaveChangesAsync();
            }
        }
    }
}