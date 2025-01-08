using Syntra.Frituurtje.Contracts.Models;

namespace Syntra.Frituurtje.API.Services
{
    public interface IOrderManagerService
    {
        Task<bool> CreateAsync(FoodOrder order);
        Task<bool> UpdateAsync(FoodOrder order);
        Task<bool> UpsertAsync(FoodOrder order);
        Task<bool> DeleteAsync(string id);
        Task<FoodOrder?> GetByIdAsync(string id);
        Task<IEnumerable<FoodOrder>> GetAllAsync();
        Task<IEnumerable<FoodOrder>> FindClientAsync(string client,decimal? avgPrice=null);
        Task<IEnumerable<FoodOrder>> FilterAsync(string? id = null, decimal? price = null, string? client = null, string? item = null, DateOnly? orderDate = null);
    }
}
