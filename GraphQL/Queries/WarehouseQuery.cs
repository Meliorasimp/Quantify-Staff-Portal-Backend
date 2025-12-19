using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using EnterpriseGradeInventoryAPI.DTO.Input;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using EnterpriseGradeInventoryAPI.DTO.Output;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnterpriseGradeInventoryAPI.GraphQL.Queries
{
  [ExtendObjectType(typeof(Query))]
  public class WarehouseQuery
  {

    public IQueryable<Warehouse> GetAllWarehouses([Service] ApplicationDbContext context) 
        => context.Warehouses;
    public async Task<SelectedWarehousePayload> GetWarehouse(
    [Service] ApplicationDbContext context,
    int id) 
      {
          int warehouseId = id;

          var totalProducts = await context.Inventories
              .Include(i => i.StorageLocation)
              .Where(i => i.StorageLocation != null && i.StorageLocation.WarehouseId == warehouseId)
              .CountAsync();

          var availableSectors = await context.StorageLocations
              .Where(sl => sl.WarehouseId == warehouseId && sl.OccupiedCapacity < sl.MaxCapacity)
              .CountAsync();

          var maxCapacity = await context.StorageLocations
              .Where(sl => sl.WarehouseId == warehouseId)
              .SumAsync(sl => sl.MaxCapacity);

          var occupiedCapacity = await context.StorageLocations
              .Where(sl => sl.WarehouseId == warehouseId)
              .SumAsync(sl => sl.OccupiedCapacity);

          int capacityUtilization = maxCapacity == 0 
              ? 0 
              : occupiedCapacity * 100 / maxCapacity;

          var warehouse = await context.Warehouses
              .Where(w => w.Id == warehouseId)
              .Select(w => new 
              {
                  w.WarehouseName,
                  w.WarehouseCode,
                  w.Address,
                  w.Manager,
                  w.Region,
                  w.Status,
                  w.ContactEmail
              })
              .FirstOrDefaultAsync();

          if (warehouse == null)
          {
              throw new GraphQLException($"Warehouse with ID {warehouseId} not found.");
          }

          return new SelectedWarehousePayload
          {
              WarehouseId = warehouseId,
              WarehouseName = warehouse.WarehouseName,
              WarehouseCode = warehouse.WarehouseCode,
              Address = warehouse.Address,
              Manager = warehouse.Manager,
              Region = warehouse.Region,
              Status = warehouse.Status,
              TotalProducts = totalProducts,
              AvailableSectors = availableSectors,
              CapacityUtilization = capacityUtilization,
              ContactEmail = warehouse.ContactEmail
          };
      }
    
    public async Task<DeletedWarehousePayload> DeleteWarehouse(
        [Service] ApplicationDbContext context,
        [Service] AuditLogService auditService,
        ClaimsPrincipal user,
        int id
    )
        {
        if (user == null)
          throw new GraphQLException(new Error("User must be authenticated", "UNAUTHORIZED"));
        int userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
          ?? throw new GraphQLException(new Error("User ID not found in token", "INVALID_TOKEN")));
    
            var warehouse = await context.Warehouses.FindAsync(id);
            if (warehouse == null)
                throw new GraphQLException(new Error("Warehouse not found", "WAREHOUSE_NOT_FOUND"));
            context.Warehouses.Remove(warehouse);

            await auditService.CreateAuditLog("Delete", userId, "Warehouse", id, null, null, warehouse.WarehouseName);
            await context.SaveChangesAsync();
            return new DeletedWarehousePayload
            {
                WarehouseId = warehouse.Id,
                WarehouseName = warehouse.WarehouseName
            };
        }
  }
}