
namespace Syntra.EF.Repository.Contracts
{
    public class MenuItem : ModelBase
    {
        public MenuItem()
        {

        }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public FoodType? FoodType { get; set; } = null;
        public string FoodTypeId { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string? ImageUrl { get; set; }
    }
}
