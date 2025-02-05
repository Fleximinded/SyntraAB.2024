using Syntra.Fietshersteller.Db.Models;

namespace Syntra.Fietshersteller.Site.Services
{
    public interface IPersonService
    {
        Task<Person?> GetAsync(string id);
        Task<bool> StoreAsync(Person person);
    }
}
