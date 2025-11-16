namespace EnterpriseGradeInventoryAPI.Models
{
  public class Inventory
  {
    public int Id { get; set; }
    public string ItemSKU { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string WarehouseLocation { get; set; } = null!;
    public string RackLocation { get; set; } = null!;
    public int QuantityInStock { get; set; }
    public int ReorderLevel { get; set; }
    public string UnitOfMeasure { get; set; } = null!;
    public int CostPerUnit { get; set; }
    public int TotalValue { get; set; }
    public DateTime LastRestocked { get; set; }
    //Foreign Key to StorageLocation Model
    public int? StorageLocationId { get; set; }
    public StorageLocation? StorageLocation { get; set; }
    //Foreign Key to User Model
    public int UserId { get; set; }
    //Navigation Property of User Model
    public User User { get; set; } = null!;
  }
}