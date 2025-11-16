using Microsoft.AspNetCore.Mvc;
using EnterpriseGradeInventoryAPI.Data;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public UsersController(ApplicationDbContext context)
  {
    _context = context;
  }
}