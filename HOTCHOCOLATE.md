# 🔍 HotChocolate GraphQL Function Name Rules

The **function name** of the Query or Mutation must match the corresponding **C# method name** in the backend.  
⚠️ Note: HotChocolate removes certain prefixes automatically — this list helps you avoid mismatches and confusion.

---

## From Query/Mutation name ➡️ Backend C# name

### 🧩 Prefixes HotChocolate Removes

#### Get Prefix (Most Common)

| Query/Mutation     | Backend C# Function |
| ------------------ | ------------------- |
| `GetUser()`        | `user`              |
| `GetUsers()`       | `users`             |
| `GetInventory()`   | `inventory`         |
| `GetUserById()`    | `userById`          |
| `GetProductList()` | `productList`       |

#### Is Prefix (Boolean Methods)

| Example             | Result          |
| ------------------- | --------------- |
| `IsActive()`        | `active`        |
| `IsValid()`         | `valid`         |
| `IsAuthenticated()` | `authenticated` |

#### Has Prefix (Boolean Methods)

| Example           | Result       |
| ----------------- | ------------ |
| `HasPermission()` | `permission` |
| `HasAccess()`     | `access`     |
| `HasInventory()`  | `inventory`  |

#### Can Prefix (Boolean Methods)

| Example       | Result   |
| ------------- | -------- |
| `CanEdit()`   | `edit`   |
| `CanDelete()` | `delete` |
| `CanAccess()` | `access` |

---

## 🚫 Prefixes HotChocolate **Keeps**

✅ Action verbs (for Mutations):
| Example | Result |
|----------|---------|
| `CreateUser()` | `createUser` |
| `UpdateProduct()` | `updateProduct` |
| `DeleteItem()` | `deleteItem` |
| `AddInventory()` | `addInventory` |

---

## 🧠 Other Verbs

| Example            | Result           |
| ------------------ | ---------------- |
| `FindUser()`       | `findUser`       |
| `SearchProducts()` | `searchProducts` |
| `CalculateTotal()` | `calculateTotal` |
