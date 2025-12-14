using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using EnterpriseGradeInventoryAPI.DTO.Output;
using EnterpriseGradeInventoryAPI.GraphQL;
using System.Security.Claims;

[ExtendObjectType(typeof(Query))]
public class PurchaseOrderQuery
{
  public IQueryable<AllPurchaseOrdersPayload> GetAllPurchaseOrder([Service] ApplicationDbContext context)
  {
    try
    {
      return context.PurchaseOrders.Select(po => new AllPurchaseOrdersPayload
        {
          Id = po.Id,
          PurchaseOrderNumber = po.PurchaseOrderNumber,
          SupplierName = po.SupplierName,
          OrderDate = po.OrderDate.ToString("MM/dd/yyyy"),
          TotalAmount = po.TotalAmount,
          Status = po.Status
        });
    }
    catch (Exception ex)
    {
      throw new GraphQLException($"Error fetching purchase orders: {ex.Message}");
    }  
  }
  public async Task<PurchaseOrderDetailsPayload?> GetPurchaseOrderById([Service] ApplicationDbContext context, int id, ClaimsPrincipal user)
  {
    try {
      Console.WriteLine($"[DEBUG] GetPurchaseOrderById called with ID: {id}");
      
      var order = await context.PurchaseOrders.Include(po =>po.Items).FirstOrDefaultAsync(po => po.Id == id);
      Console.WriteLine($"[DEBUG] Order fetched: {(order != null ? $"Found - PO#{order.PurchaseOrderNumber}" : "Not found")}");
      
      var items = await context.PurchaseOrderItems.Where(poi => poi.PurchaseOrderId == id).ToListAsync();
      Console.WriteLine($"[DEBUG] Items fetched: {items.Count} items found");
      
      if(order == null)
      {
        Console.WriteLine("[DEBUG] Returning null - order not found");
        return null;
      }

      var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      Console.WriteLine($"[DEBUG] User ID from claims: {userId ?? "null"}");
      
      if(userId == null)
        throw new GraphQLException("User not authenticated");

      if(!int.TryParse(userId, out int userIdInt))
        throw new GraphQLException("Invalid user ID format");

      var userName = await context.Users.Where(u => u.Id == int.Parse(userId)).Select(u => $"{u.FirstName} {u.LastName}").FirstOrDefaultAsync();
      Console.WriteLine($"[DEBUG] User name fetched: {userName ?? "null"}");
      
      var OrderDetails = new PurchaseOrderDetailsPayload
      {
        Id = order.Id,
        OrderDate = order.OrderDate.ToString("dd/MM/yyyy"),
        SupplierName = order.SupplierName,
        StaffResponsible = userName ?? "Unknown",
        DeliveryWarehouse = order.DeliveryWarehouse,
        ExpectedDeliveryDate = order.ExpectedDeliveryDate.ToString("dd/MM/yyyy"),
        Status = order.Status,
        Items = [.. items.Select(i => new PurchaseOrderItemPayload
        {
          Id = i.Id,
          ProductName = i.ProductName,
          Price = i.Price,
          Quantity = i.Quantity
        })]
      };
      
      Console.WriteLine($"[DEBUG] OrderDetails created successfully with {OrderDetails.Items.Count} items");
      return OrderDetails;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"[ERROR] Exception in GetPurchaseOrderById: {ex.Message}");
      Console.WriteLine($"[ERROR] Stack trace: {ex.StackTrace}");
      throw new GraphQLException($"Error fetching purchase order details: {ex.Message}");
    }
  }
}