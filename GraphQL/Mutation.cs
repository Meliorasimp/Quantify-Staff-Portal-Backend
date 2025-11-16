using HotChocolate.Types;
using EnterpriseGradeInventoryAPI.GraphQL.Mutations;

namespace EnterpriseGradeInventoryAPI.GraphQL
{
  public class Mutation : ObjectType
  {
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
      descriptor.Name("Mutation");

      descriptor.Field<UserMutation>(t => t.registerUser(default!, default!, default!, default!, default!))
        .Name("registerUser")
        .Description("Register a new User");
      descriptor.Field<LoginMutation>(t => t.loginUser(default!, default!, default!))
        .Name("loginUser")
        .Description("Login an existing User");
      descriptor.Field<InventoryMutation>(t => t.addInventory(default!, default!, default!))
        .Name("addInventory")
        .Description("Add a new Inventory Item");
      // WarehouseMutation is now handled via [ExtendObjectType] attribute
      
    }
  }
}