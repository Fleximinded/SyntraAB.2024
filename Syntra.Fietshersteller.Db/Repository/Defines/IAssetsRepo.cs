using Syntra.Fietshersteller.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Fietshersteller.Db.Repository.Defines
{
    public interface IAssetsRepo
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync(int skip=0,int take=500);
        Task<IEnumerable<Person>> FindPersonsAsync(string search, int skip = 0, int take = 500);
        Task<Person?> GetPersonByIdAsync(string id);
        Task<bool> AddPersonAsync(Person person);
        Task<bool> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(string id);
        Task<bool> DeletePersonAsync(Person person);
        Task<bool> UpsertPersonAsync(Person person);
    }
}
