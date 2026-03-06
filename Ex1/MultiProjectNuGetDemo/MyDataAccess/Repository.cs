namespace MyDataAccess
{
    public class Repository<T>
    {
        private readonly List<T> _items = new List<T>();

        public void Add(T entity)
        {
            _items.Add(entity);
        }

        public T GetById(int id)
        {
            return _items[id];
        }
    }
}
