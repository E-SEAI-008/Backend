public class Repository<T> where T : Entity
{
    private readonly List<T> _items = new();

    public void Save(T entity)
    {
        _items.Add(entity);
        Console.WriteLine($"Saved {typeof(T).Name} with id {entity.Id}");
    }

    public IEnumerable<T> GetAll() => _items;
}