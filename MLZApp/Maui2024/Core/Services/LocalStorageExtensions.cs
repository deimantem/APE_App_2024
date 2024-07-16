namespace Core.Services
{
    public static class LocalStorageExtensions
    {
        /// <summary>
        /// Loads an item of type <typeparamref name="T"/> from the local storage by its identifier.
        /// </summary>
        /// <typeparam name="T">The type of the object to be loaded.</typeparam>
        /// <param name="localStorage">The local storage instance.</param>
        /// <param name="id">The identifier of the object to load.</param>
        /// <returns>The loaded object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the object with the specified id is not found.</exception>
        public static async Task<T> Load<T>(this ILocalStorage<T> localStorage, int id) where T : class, new()
        {
            var item = await localStorage.TryLoad(id);
            if (item != null)
            {
                return item;
            }

            throw new InvalidOperationException($"Could not load object of type [{typeof(T)}] with id [{id}].");
        }
    }
}