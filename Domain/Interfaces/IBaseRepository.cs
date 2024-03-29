namespace Domain.Interfaces;

public interface IBaseRepository<T> where T : class 
{
    void Add(T item);
    void Delete(T item);
    void Update(T item);
}
