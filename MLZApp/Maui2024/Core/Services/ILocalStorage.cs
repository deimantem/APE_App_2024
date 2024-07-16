namespace Core.Services
{
    /// <summary>
    /// Interface for local storage operations.
    /// </summary>
    /// <typeparam name="T">The type of the object to be stored.</typeparam>
    public interface ILocalStorage<T> where T : class, new()
    {
        /// <summary>
        /// Tries to load an object from storage by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the loaded object, or null if not found.</returns>
        Task<T?> TryLoad(int id);

        /// <summary>
        /// Saves an object to storage.
        /// </summary>
        /// <param name="item">The object to be saved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the save operation succeeded.</returns>
        Task<bool> Save(T item);

        /// <summary>
        /// Deletes an object from storage.
        /// </summary>
        /// <param name="item">The object to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the delete operation succeeded.</returns>
        Task<bool> Delete(T item);

        /// <summary>
        /// Loads all objects from storage.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all loaded objects.</returns>
        Task<List<T>> LoadAll();

        /// <summary>
        /// Deletes all objects from storage.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the delete all operation succeeded.</returns>
        Task<bool> DeleteAll();

        /// <summary>
        /// Initializes the storage system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Initialize();
    }
}