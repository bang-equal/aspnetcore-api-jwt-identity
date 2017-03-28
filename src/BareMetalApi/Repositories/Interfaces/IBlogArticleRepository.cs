using System.Collections.Generic;
using System.Threading.Tasks;
using BareMetalApi.Models;

namespace BareMetalApi.Repositories.Interfaces
{
    public interface IBlogArticleRepository
    {
        //Task<IEnumerable<BlogArticle>> TestGetAll();
        Task<bool> DoesItemExist(int id);

        Task<IList<BlogArticle>> GetAll();

        Task<BlogArticle> GetById(int id);

        Task<int> AddAsync(BlogArticle blogarticle);

        Task UpdateAsync(BlogArticle blogarticle);

        Task DeleteAsync(BlogArticle blogarticle);
    }
}