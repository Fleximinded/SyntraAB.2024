using Syntra.Fietshersteller.Db.Models;
using Syntra.Fietshersteller.Db.Repository.Defines;

namespace Syntra.Fietshersteller.Site.Services
{
    public class PersonService : IPersonService
    {
        IAssetsRepo _repo;
        public PersonService(IAssetsRepo repo) {
            _repo = repo;
        }

        public async Task<Person?> GetAsync(string id) => await _repo.GetPersonByIdAsync(id);

        public async Task<bool> StoreAsync(Person person)
        { 
            return await _repo.UpsertPersonAsync(person);
        }
    }
}
