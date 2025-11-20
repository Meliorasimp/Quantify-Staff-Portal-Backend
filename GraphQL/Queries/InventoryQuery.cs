using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseGradeInventoryAPI.GraphQL.Queries
{
  
  [ExtendObjectType(typeof(Query))]
  public class InventoryQuery
  {
    public IQueryable<Inventory> GetAllInventoryItems([Service] ApplicationDbContext context) =>
    context.Inventories;
    
    public async Task<int> GetTotalInventoryValue([Service] ApplicationDbContext context)
    {
        int total = await context.Inventories.SumAsync(i => i.TotalValue);
        return total;
    }

    public async Task<int> GetInStockItems([Service] ApplicationDbContext context)
    {
      int count = await context.Inventories.CountAsync(i => i.QuantityInStock > 0);
      return count;
    }

    public async Task<int> GetLowStockItems([Service] ApplicationDbContext context)
    {
      int count = await context.Inventories.CountAsync(i => i.QuantityInStock <= i.ReorderLevel);
      return count;
    }

    public async Task<int> GetOutOfStockItems([Service] ApplicationDbContext context)
    {
      int count = await context.Inventories.CountAsync(i => i.QuantityInStock == 0);
      return count;
    }

    public IQueryable<Inventory> GetItemBySearchTerm([Service] ApplicationDbContext context, string searchTerm)
    {
      var keyword = searchTerm.ToLower();
      return context.Inventories.Where(i =>
        i.ProductName.ToLower().Contains(keyword) ||
        i.Category.ToLower().Contains(keyword) ||
        i.WarehouseLocation.ToLower().Contains(keyword) ||
        i.ItemSKU.ToLower().Contains(keyword));
    }

    public IQueryable<Inventory> GetItemByCategory([Service] ApplicationDbContext context, string category)
    {
      var keyword = category.ToLower();
      return context.Inventories.Where(i => i.Category.ToLower() == keyword);
    }
    
    public IQueryable<Inventory> GetItemByWarehouseLocation([Service] ApplicationDbContext context, string warehouseName) 
    { 
      var keyword = warehouseName.ToLower();
      return context.Inventories.Where(i => i.WarehouseLocation.ToLower() == keyword);
    }
  }
}