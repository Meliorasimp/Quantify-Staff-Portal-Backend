using EnterpriseGradeInventoryAPI.Models;

namespace EnterpriseGradeInventoryAPI.Models
{
  public class Warehouse
  {
    public int Id { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string WarehouseCode { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Manager { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedByLastName { get; set; } = null!;
    //Foreign Key to User Model
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
  }
}