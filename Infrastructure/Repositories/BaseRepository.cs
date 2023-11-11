using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class BaseRepository<T>: IBaseRepository<T> where T: class
{
    private readonly LikeButtonContext _context;

    public BaseRepository(LikeButtonContext context)
    {
        _context = context;
    }

    public void Add(T item)
    {
        _context.Set<T>()
            .Add(item);
        
        _context.SaveChanges();
    }

    public void Delete(T item)
    {
        _context.Set<T>()
            .Remove(item);
        
        _context.SaveChanges();
    }

    public void Update(T item)
    {
        _context.Set<T>()
            .Update(item);
        
        _context.SaveChanges();
    }
}
