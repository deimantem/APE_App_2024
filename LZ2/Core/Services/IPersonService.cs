using Core.Models;

namespace Core.Services
{
    public interface IPersonService
    {
        Task<bool> Save(Person person);
    }
}