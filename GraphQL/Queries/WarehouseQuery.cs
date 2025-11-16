using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;

namespace EnterpriseGradeInventoryAPI.GraphQL.Queries
{
  [ExtendObjectType(typeof(Query))]
  public class WarehouseQuery
  {

    public IQueryable<Warehouse> GetAllWarehouses([Service] ApplicationDbContext context) 
        => context.Warehouses;
    public IQueryable<Warehouse> GetWarehouse([Service] ApplicationDbContext context, string warehouseName) 
        => context.Warehouses.Where(w => w.WarehouseName.ToLower() == warehouseName.ToLower());
  }
}