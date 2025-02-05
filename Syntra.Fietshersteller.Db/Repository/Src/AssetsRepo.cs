using Microsoft.EntityFrameworkCore;
using Syntra.Fietshersteller.Db.Models;
using Syntra.Fietshersteller.Db.Repository.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Fietshersteller.Db.Repository.Src
{
    public class AssetsRepo : IAssetsRepo
    {
        IDbContextFactory<ApplicationDbContext> ContextFactory { get; set; }
        public AssetsRepo(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }
        public async Task<bool> AddPersonAsync(Person person)
        {
            using var context = ContextFactory.CreateDbContext();
            return await AddPersonAsync(context, person,true);
        }
        public async Task<bool> AddPersonAsync(ApplicationDbContext context, Person person,bool doSave=true)
        {            
            if(context.Persons.Where(p => p.Id == person.Id).AsNoTracking().FirstOrDefault() != null)
            {
                return false;
            }
            context.Persons.Add(person);
            return doSave ? await context.SaveChangesAsync() > 0 : true;
        }

        public async Task<bool> DeletePersonAsync(string id)
        {
            using var context = ContextFactory.CreateDbContext();
            var person = context.Persons.Find(id);
            if(person != null)
            {
                context.Persons.Remove(person);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> DeletePersonAsync(Person person)=> await DeletePersonAsync(person.Id);
        public async Task<IEnumerable<Person>> FindPersonsAsync(string search, int skip = 0, int take = 500)
        {
            using var context = ContextFactory.CreateDbContext();
            search = search.ToLower();
            return await context.Persons.Where(p => p.FirstName.ToLower().Contains(search) || p.LastName.ToLower().Contains(search)).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllPersonsAsync(int skip = 0, int take = 500)
        {
            using var context = ContextFactory.CreateDbContext();
            return await context.Persons.Skip(skip).Take(take).ToListAsync();
        }
        public async Task<Person?> GetPersonByIdAsync(string id)
        {
            using var context = ContextFactory.CreateDbContext();
            return await context.Persons.FindAsync(id);
        }
        public async Task<bool> UpdatePersonAsync(Person person) {
            using var context = ContextFactory.CreateDbContext();
            return await UpdatePersonAsync(context, person);
        }
        public async Task<bool> UpdatePersonAsync(ApplicationDbContext context, Person person,bool doSave=true)
        {
            var existing = await context.Persons.FindAsync(person.Id);
            if(existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(person);
                return doSave ? await context.SaveChangesAsync() > 0 : true;
            }
            return false;
        }
        public async Task<bool> UpsertPersonAsync(Person person)
        {
            using var context = ContextFactory.CreateDbContext();
            if(await AddPersonAsync(context,person) == false)
            {
                return await UpdatePersonAsync(context,person);
            }
            return true;
        }
    }
}
