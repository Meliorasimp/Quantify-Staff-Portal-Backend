namespace EnterpriseGradeInventoryAPI.DTO.Output
{
  public class InventoryPayload
  {
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int UserId { get; set; }
  }

  public class DeletedInventoryPayload
  {
    public int Id { get; set; }
    public string ItemSKU { get; set; } = string.Empty;
  }

  public class UpdatedInventoryPayload
  {
    public int Id { get; set; }
    public string ItemSKU { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public int ReorderLevel { get; set; }
    public string Category { get; set; } = string.Empty;
  }
}