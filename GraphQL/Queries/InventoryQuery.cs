using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using StackExchange.Redis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseGradeInventoryAPI.GraphQL.Queries
{
  
  [ExtendObjectType(typeof(Query))]
  public class InventoryQuery
  {
    public IQueryable<Inventory> GetAllInventoryItems([Service] ApplicationDbContext context) =>
    context.Inventories;
    
    public async Task<int> GetTotalInventoryValue(
    [Service] ApplicationDbContext context,
    [Service] IConnectionMultiplexer redis)
    {
        var db = redis.GetDatabase();

        // Try to read from Redis cache
        var cached = await db.StringGetAsync("TotalInventoryValue");

        // If found in cache, return it directly(faster than querying database)
        if (!cached.IsNullOrEmpty)
        {
            return int.Parse(cached!);
        }

        // If not in cache, query the database
        int total = await context.Inventories.SumAsync(i => i.TotalValue);
        // Cache for 10 seconds
        await db.StringSetAsync("TotalInventoryValue", total, TimeSpan.FromSeconds(10));

        return total;
    }

    public async Task<int> GetInStockItems([Service] ApplicationDbContext context, [Service] IConnectionMultiplexer redis)
    {
      var db = redis.GetDatabase();

      var cached = await db.StringGetAsync("InStockItems");

      if(!cached.IsNullOrEmpty)
      {
        return (int)cached;
      }
      
      int count = context.Inventories.Count(i => i.QuantityInStock > 0);

      await db.StringSetAsync("InStockItems", count, TimeSpan.FromSeconds(10));
      return count;
    }

    public async Task<int> GetLowStockItems([Service] ApplicationDbContext context, [Service] IConnectionMultiplexer redis)
    {
      var db = redis.GetDatabase();

      var cached = await db.StringGetAsync("LowStockItems");

      if(!cached.IsNullOrEmpty)
      {
        return (int)cached;
      }
      
      int count = context.Inventories.Count(i => i.QuantityInStock <= i.ReorderLevel);

      await db.StringSetAsync("LowStockItems", count, TimeSpan.FromSeconds(10));
      return count;
    }

    public async Task<int> GetOutOfStockItems([Service] ApplicationDbContext context, [Service] IConnectionMultiplexer redis)
    {
      var db = redis.GetDatabase();

      var cached = await db.StringGetAsync("OutOfStockItems");

      if(!cached.IsNullOrEmpty)
      {
        return (int)cached;
      }
      
      int count = await context.Inventories.CountAsync(i => i.QuantityInStock == 0);
      await db.StringSetAsync("OutOfStockItems", count, TimeSpan.FromSeconds(10));
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