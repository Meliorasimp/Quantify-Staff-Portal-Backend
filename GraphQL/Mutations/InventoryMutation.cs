using EnterpriseGradeInventoryAPI.Data;
using EnterpriseGradeInventoryAPI.Models;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseGradeInventoryAPI.GraphQL.Mutations
{
  //Input type for GraphQL mutations
  public class InventoryInput
  {
    public string ItemSKU { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string WarehouseLocation { get; set; } = string.Empty;
    public string RackLocation { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public int ReorderLevel { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public int CostPerUnit { get; set; }
    public int TotalValue { get; set; }
  }

  public class InventoryMutation
  {
    //Add Inventory to the Database
    public async Task<InventoryPayload> addInventory([Service] ApplicationDbContext context, string userId, List<InventoryInput> inventory)
    {
      try
      {
        // Validate userId format
        if (!int.TryParse(userId, out int userIdInt))
        {
          throw new GraphQLException("Invalid user ID format");
        }

        foreach (var item in inventory)
        {
          // Input validation
          if (string.IsNullOrWhiteSpace(item.ItemSKU) || string.IsNullOrWhiteSpace(item.ProductName))
          {
            throw new GraphQLException("ItemSKU and ProductName are required");
          }

          if (item.QuantityInStock < 0 || item.CostPerUnit < 0)
          {
            throw new GraphQLException("Quantity and Cost must be non-negative");
          }

          // Check for duplicate SKU
          var existingItem = await context.Inventories.FirstOrDefaultAsync(i => i.ItemSKU == item.ItemSKU && i.UserId == userIdInt);
          if (existingItem != null)
          {
            throw new GraphQLException($"Item with SKU '{item.ItemSKU}' already exists");
          }

          var newInventory = new Inventory
          {
            ItemSKU = item.ItemSKU,
            ProductName = item.ProductName,
            Category = item.Category,
            WarehouseLocation = item.WarehouseLocation,
            RackLocation = item.RackLocation,
            QuantityInStock = item.QuantityInStock,
            ReorderLevel = item.ReorderLevel,
            UnitOfMeasure = item.UnitOfMeasure,
            CostPerUnit = item.CostPerUnit,
            TotalValue = item.QuantityInStock * item.CostPerUnit,
            UserId = userIdInt,
            LastRestocked = DateTime.UtcNow // Use UTC for consistency
          };

          // Handle storage location association with validation
          var storageLocation = await context.StorageLocations
            .FirstOrDefaultAsync(sl => sl.SectionName == item.RackLocation && sl.UserId == userIdInt);
          
          if (storageLocation != null)
          {
            // Check capacity constraints
            if (storageLocation.OccupiedCapacity + item.QuantityInStock > storageLocation.MaxCapacity)
            {
              throw new GraphQLException($"Adding {item.QuantityInStock} items would exceed storage capacity for location '{item.RackLocation}'");
            }

            newInventory.StorageLocationId = storageLocation.Id;
            storageLocation.OccupiedCapacity += item.QuantityInStock;
          }
          
          context.Inventories.Add(newInventory);
        }
        
        //Save changes to MySQL Database
        await context.SaveChangesAsync();
        
        // Get the last added inventory for response
        var lastInventory = await context.Inventories
          .Where(i => i.UserId == userIdInt)
          .OrderByDescending(i => i.Id)
          .FirstAsync();
        
        return new InventoryPayload
        {
          Id = lastInventory.Id,
          ItemName = lastInventory.ProductName,
          Quantity = lastInventory.QuantityInStock,
          UserId = userIdInt
        };
      }
      catch (Exception ex)
      {
        // Handle exceptions
        throw new GraphQLException("Error adding inventory", ex);
      }
    }

    //Return type for GraphQL response OwO
    public class InventoryPayload
    {
      [GraphQLName("id")]
      public int Id { get; set; }
      [GraphQLName("itemName")]
      public string ItemName { get; set; } = string.Empty;
      [GraphQLName("quantity")]
      public int Quantity { get; set; }
      [GraphQLName("userId")]
      public int UserId { get; set; }

    }
  }
}