using EnterpriseGradeInventoryAPI.Models;

public class PurchaseOrder
{
  public int Id { get; set; }
  public string SupplierName { get; set; } = null!;
  public string DeliveryWarehouse { get; set; } = null!;
  public DateTime ExpectedDeliveryDate { get; set; }
  public int PurchaseOrderNumber { get; set; }
  public int SupplierId { get; set; }
  public string Notes { get; set; } = null!;
  public DateTime OrderDate { get; set; } = DateTime.UtcNow;
  public int TotalAmount { get; set; }
  public string Status { get; set; } = "Pending";
  public User User { get; set; } = null!;
  public int UserId { get; set; }
  public ICollection<PurchaseOrderItems> Items { get; set; } = new List<PurchaseOrderItems>();
}