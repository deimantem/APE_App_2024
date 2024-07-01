using Core.Models;

namespace Core.Services
{
    /// <summary>
    /// Interface for defining operations related to handling Person objects
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Saves the specified Person object asynchronously
        /// </summary>
        /// <param name="person">The Person object to save</param>
        /// <returns>A task representing the asynchronous save operation, returning true if successful, false otherwise</returns>
        Task<bool> Save(Person person);

        /// <summary>
        /// Loads all stored Person objects asynchronously
        /// </summary>
        /// <returns>A task representing the asynchronous load operation, returning a list of Person object</returns>
        Task<List<Person>> Load();
    }
}