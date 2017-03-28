using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BareMetalApi.Models;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi.Repositories
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogArticleRepository(ApplicationDbContext context)
        {
            _context = context;    
        }

        public async Task<bool> DoesItemExist(int id)
        {
            var dbEntity = await _context.BlogArticles.AnyAsync(blogarticle => blogarticle.Id == id);
            return dbEntity;
        }
        public async Task <IList<BlogArticle>> GetAll()
        {
            var dbEntity = await _context.BlogArticles.ToListAsync();
            return dbEntity;
        }

        public async Task <BlogArticle> GetById(int id)
        {
            var dbEntity = await _context.BlogArticles.FirstOrDefaultAsync();
            return dbEntity;
            
        }

        public async Task <int> AddAsync(BlogArticle blogarticle)
        {
            _context.BlogArticles.Add(new BlogArticle { ArticleTitle = blogarticle.ArticleTitle, ArticleContent = blogarticle.ArticleContent});
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogArticle blogarticle)
        {
            _context.Attach(blogarticle);
            var entry = _context.Entry(blogarticle);
            entry.Property(e => e.ArticleTitle).IsModified = true;
            entry.Property(e => e.ArticleContent).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlogArticle blogarticle)
        {
            _context.BlogArticles.Remove(blogarticle);
            await _context.SaveChangesAsync();
        }
    }
}