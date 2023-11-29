namespace Library.Repositories;

public interface Repository<T>
{
    public void Insert(T item);

    public void Update(T item);
    
    public void Delete(T item);
    
    public List<Dictionary<string, string>> FindAll();
}