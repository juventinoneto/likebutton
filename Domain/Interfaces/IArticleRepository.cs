using System.Linq;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IArticleRepository: IBaseRepository<Article>
    {
        Article FindById(long id);
        
        IQueryable<Article> List();
    }
}