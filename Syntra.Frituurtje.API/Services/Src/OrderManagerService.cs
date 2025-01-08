using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Database.Defines;

namespace Syntra.Frituurtje.API.Services
{
    public class OrderManagerService : IOrderManagerService
    {
        IOrderRepository _orderRepository = default!;
        public OrderManagerService(IOrderRepository repository) {
            _orderRepository = repository;
        }
        public async Task<bool> CreateAsync(FoodOrder order) => await _orderRepository.AddAsync(order);

        public async Task<bool> DeleteAsync(string id)=> await _orderRepository.DeleteAsync(id);

        public async Task<IEnumerable<FoodOrder>> FindClientAsync(string client, decimal? avgPrice = null)
        {
           return await _orderRepository.FindAsync(client: client,price: avgPrice);
        }

        public async Task<IEnumerable<FoodOrder>> FilterAsync(string? id = null, decimal? price = null, string? client = null, string? item = null, DateOnly? orderDate = null) { 
            return await _orderRepository.FindAsync(id,price,client,item,orderDate);
        }

        public async Task<IEnumerable<FoodOrder>> GetAllAsync() => await _orderRepository.GetAllAsync();

        public async Task<FoodOrder?> GetByIdAsync(string id) => await _orderRepository.GetByIdAsync(id);

        public async Task<bool> UpdateAsync(FoodOrder order)=> await _orderRepository.UpdateAsync(order);

        public async Task<bool> UpsertAsync(FoodOrder order) => await _orderRepository.UpsertAsync(order);
    }
}
