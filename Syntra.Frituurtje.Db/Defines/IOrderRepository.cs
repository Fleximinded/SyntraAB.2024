using Syntra.Frituurtje.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntra.Frituurtje.Db.Defines
{
    public interface IOrderRepository
    {
        Task<IEnumerable<FoodOrder>> GetAllAsync();    
        Task<FoodOrder?> GetByIdAsync(string id);
        Task<bool> AddAsync(FoodOrder order);
        Task<bool> UpdateAsync(FoodOrder order);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpsertAsync(FoodOrder order);
        Task<IEnumerable<FoodOrder>> FindAsync(string? id=null,decimal? price=null,string? client=null,string? item=null,DateOnly? orderDate=null);
    }
}
