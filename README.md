# 🥞 Mini Munch — Pancake Ordering Web App

A full-stack **ASP.NET Core 8 MVC** pancake ordering platform with role-based access, JWT authentication stored in HttpOnly cookies, and a clean Tailwind CSS UI. Customers can browse the menu, manage a cart, and place orders — admins get a dedicated dashboard to manage products and orders.

> 🔗 **GitHub:** [github.com/gitamrit1443/Mini-Munch](https://github.com/gitamrit1443/Mini-Munch)

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | ASP.NET Core 8 MVC |
| **Language** | C# 12 |
| **Database** | Microsoft SQL Server 2022 |
| **ORM** | Entity Framework Core 8 |
| **View Engine** | ASP.NET Core Razor Views |
| **Auth** | JWT Bearer — stored in HttpOnly cookie |
| **Password Hashing** | BCrypt.Net |
| **Styling** | Tailwind CSS (CDN) |

---

## ✨ Features

### Customer
- Register, Login, Logout with JWT + HttpOnly cookie auth
- Browse menu with product detail pages
- Add to cart, update quantities, remove items
- Checkout and place orders
- View full order history with status tracking

### Admin
- Dedicated admin dashboard
- Full product CRUD (create, edit, delete, list)
- Order management — view all orders and update their status
- Role-based authorization (`[Authorize(Roles = "Admin")]`)

---

## 📸 Screenshots

### 🏠 Landing Page
![Landing Page](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20121703.png)

### 🍽️ Menu
![Menu Page](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20121746.png)

### 🔐 Login
![Login](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20121829.png)

### 🛒 Menu (Logged In)
![Menu Logged In](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20121921.png)

### 🥞 Product Cards
![Product Cards](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20122022.png)

### 📦 Order History
![Order History](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20122140.png)

### 🛒 Cart
![Cart](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20122305.png)

### 📊 Admin Dashboard
![Admin Dashboard](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20133551.png)

### 📊 Admin Dashboard (Full)
![Admin Dashboard Full](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20133617.png)

### 🗂️ Admin — Menu View
![Admin Menu View](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20133631.png)

### ✏️ Admin — Profile
![Admin Profile](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20133643.png)

### 📝 Register
![Register](MiniMunch.Api/wwwroot/Screenshots/Screenshot%202026-07-15%20133700.png)

---

## 🗂️ Project Structure

```
Mini-Munch/
└── MiniMunch.Api/
    ├── Controllers/
    │   ├── HomeController.cs         # Landing, menu, product detail
    │   ├── AccountController.cs      # Register, login, logout
    │   ├── CartController.cs         # Cart CRUD
    │   ├── OrderController.cs        # Checkout, order history
    │   └── Admin/
    │       ├── AdminDashboardController.cs
    │       ├── AdminProductController.cs
    │       └── AdminOrderController.cs
    ├── Models/
    │   ├── Entities/                 # EF Core entity classes
    │   └── ViewModels/               # View-specific DTOs
    ├── Views/
    │   ├── Home/
    │   ├── Account/
    │   ├── Cart/
    │   ├── Order/
    │   ├── Admin/
    │   └── Shared/
    │       ├── _Layout.cshtml
    │       └── _AdminLayout.cshtml
    ├── Data/
    │   ├── AppDbContext.cs
    │   └── DbSeeder.cs               # Seeds pancake menu + test users
    ├── Services/
    │   ├── AuthService.cs            # JWT generation + validation
    │   └── CartService.cs
    ├── Middleware/
    │   └── JwtCookieMiddleware.cs    # Reads JWT from HttpOnly cookie
    ├── wwwroot/
    │   └── Screenshots/              # App screenshots
    ├── appsettings.json
    └── Program.cs
```

---

## 🔑 Default Accounts

| Role | Email | Password |
|---|---|---|
| Admin | admin@minimunch.local | Admin@123 |
| Customer | customer@minimunch.local | Customer@123 |

---

## 🚀 Run Locally

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server 2022 (or SQL Server Express)

### Steps

```bash
# 1. Clone the repo
git clone https://github.com/gitamrit1443/Mini-Munch.git
cd Mini-Munch

# 2. Update connection string if needed
# Edit: MiniMunch.Api/appsettings.json

# 3. Restore and run
dotnet restore MiniMunch.Api/MiniMunch.Api.csproj
dotnet run --project MiniMunch.Api/MiniMunch.Api.csproj
```

Open the localhost URL shown in terminal. Database and seed data are created automatically via `EnsureCreatedAsync()`.

---

## 🔐 Auth Flow

```
User Login
    │
    ▼
AccountController.Login()
    │  Validates credentials with BCrypt
    │  Generates JWT (claims: userId, email, role)
    ▼
JWT stored in HttpOnly cookie (not localStorage)
    │
    ▼
JwtCookieMiddleware (runs on every request)
    │  Reads cookie → validates token → sets HttpContext.User
    ▼
Controllers use [Authorize] and [Authorize(Roles = "Admin")]
```

---

## ⚠️ Production Notes

- Replace JWT secret in `appsettings.json` with a strong environment variable
- Set `CookieSecurePolicy.Always` and configure HTTPS
- Switch from `EnsureCreatedAsync()` to proper EF Core migrations
- Replace Tailwind CDN with a compiled production build

---

## 👤 Author

**Amrit Pal Singh**
Full Stack Developer · B.Tech CSE (2022–2026)
🌐 [amritcode.com](https://amritcode.com) · 💻 [github.com/gitamrit1443](https://github.com/gitamrit1443)
