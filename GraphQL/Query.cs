using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using HotChocolate;
using System.Linq;

namespace EnterpriseGradeInventoryAPI.GraphQL
{
  public class Query
  {
    public IQueryable<Inventory> GetInventoryItems([Service] ApplicationDbContext context) => context.Inventories;
  }
}