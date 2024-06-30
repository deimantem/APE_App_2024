using SQLite;

namespace Core.Services
{
    public class SqliteLocalStorage<T> : ILocalStorage<T> where T : class, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public SqliteLocalStorage(LocalStorageSettings settings)
        {
            var options = new SQLiteConnectionString(settings.DatabasePath);
            _connection = new SQLiteAsyncConnection(options);
        }

        public async Task Initialize()
        {
            if (_connection.TableMappings.All(x =>
                    !x.TableName.Equals(typeof(T).Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                await _connection.CreateTableAsync<T>();
            }
        }

        public async Task<bool> Delete(T item)
        {
            var primaryKeyProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Any());

            if (primaryKeyProperty == null)
            {
                throw new InvalidOperationException("No primary key defined on the entity.");
            }

            var primaryKeyValue = primaryKeyProperty.GetValue(item);
            return await _connection.DeleteAsync<T>(primaryKeyValue) == 1;
        }

        public Task<List<T>> LoadAll()
        {
            return _connection.Table<T>().ToListAsync();
        }

        public async Task<bool> DeleteAll()
        {
            return await _connection.DeleteAllAsync<T>() >= 0;
        }

        public async Task<bool> Save(T item)
        {
            return await _connection.InsertOrReplaceAsync(item) == 1;
        }

        public async Task<T?> TryLoad(int id)
        {
            return await _connection.FindAsync<T>(id);
        }
    }
}