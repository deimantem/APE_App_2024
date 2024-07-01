using Core.Models;

namespace Core.Services
{
    public class PersonService(ILocalStorage localStorage) : IPersonService
    {
        public async Task<bool> Save(Person person)
        {
            return await localStorage.Save(person);
        }

        public async Task<List<Person>> Load()
        {
            await localStorage.Initialize();

            var people = await localStorage.LoadAll();

            if (people.Count == 0)
            {
                people.Add(new Person());
            }

            return people;
        }
    }
}