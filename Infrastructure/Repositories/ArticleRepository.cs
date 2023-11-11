using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    private readonly LikeButtonContext _context;
        
    public ArticleRepository(LikeButtonContext context) : base(context)
    {
        _context = context;
    }

    public Article FindById(long id)
    {
        return _context
            .Articles
            .Include(x => x.Likes)
            .FirstOrDefault(x => x.Id == id);
    }
    
    public IQueryable<Article> List()
    {
        return _context
            .Articles
            .Include(x => x.Likes);
    }
}
