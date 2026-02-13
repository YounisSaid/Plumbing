# 🚰 Plumbers911

**Plumbers911** is a professional, enterprise-grade Content Management System (CMS) built with **ASP.NET Core MVC** 💎. It features a robust **N-Tier Architecture** 🧱, **NLog** for structured logging 📝, and a fully dynamic database-driven frontend 🌐.

---

## 📋 Table of Contents

* [✨ Key Features](https://www.google.com/search?q=%23-key-features)
* [📖 Project Overview](https://www.google.com/search?q=%23-project-overview)
* [🏗️ Architecture Overview](https://www.google.com/search?q=%23%25EF%25B8%258F-architecture-overview)
* [🛡️ Security & Concurrency](https://www.google.com/search?q=%23%25EF%25B8%258F-security--concurrency)
* [🛠️ Technologies & Tools](https://www.google.com/search?q=%23%25EF%25B8%258F-technologies--tools)
* [📂 Project Structure](https://www.google.com/search?q=%23-project-structure)
* [📝 Logging & Validation](https://www.google.com/search?q=%23-logging--validation)
* [⚙️ Getting Started](https://www.google.com/search?q=%23%25EF%25B8%258F-getting-started)
* [📜 License](https://www.google.com/search?q=%23-license)

---

## ✨ Key Features

* **🌐 Fully Dynamic CMS:** Every section (About Us, Services, Portfolio, Team) is database-driven and editable via the Admin Dashboard.
* **👥 Dynamic Role Management:** Admins can grant limited access to users via a UI-based "Extend" feature.
* **📝 Advanced Logging:** Integrated **NLog** for high-performance, structured error reporting and file-based logging.
* **🧱 N-Tier Architecture:** Strict separation of concerns ensuring scalability, maintainability, and clean code principles.
* **🔄 Repository Pattern:** Implements Generic Repository and **Unit of Work** patterns to ensure data consistency.
* **✅ Robust Validation:** Server-side validation using **FluentValidation**.
* **🔔 Interactive UI:** Real-time user feedback using **NToastNotify**.

---

## 📖 Project Overview

While **Plumbers911** is demonstrated as a Plumbing Service platform, it is engineered as a **Universal Business CMS**. 🌍

The application is fully dynamic—meaning no content is hardcoded in the HTML. From the service icons to the team member bios, everything is managed via the database. This architecture allows you to apply this exact codebase to **any other business** (e.g., a Law Firm or Consulting Agency) simply by updating the content in the Admin Panel. 🎨

---

## 🏗️ Architecture Overview

The system is built using a professional **N-Tier Architecture**, separating the application into distinct logical layers. 🎯

1. **Core Layer (Domain) 🌳**
The root of the dependency chain. Contains base contracts (`IBaseEntity`) ensuring no circular dependencies.
2. **Entity Layer (Data Domain) 💾**
Houses database entities (`Service`, `Team`) and ViewModels (DTOs).
3. **Repository Layer (Persistence) 🗄️**
Handles Data Access Logic (DAL) using **Entity Framework Core** and the **Unit of Work** pattern.
4. **Service Layer (Business Logic) ⚙️**
The heart of the application. It orchestrates data flow, manages complex business rules, and strictly separates concerns.
#### 🧩 Identity Customizations


The project overrides default ASP.NET Core Identity behaviors to provide a tailored user experience:
* **`CustomIdentityErrorDescriber`**: Replaces standard English error messages (e.g., "Password requires a digit") with custom, user-friendly messages suitable for the target audience.
* **`CustomPasswordValidator` & `CustomUserValidator**`: Implements specific logic for password complexity and username rules beyond the framework's defaults.


#### 🛡️ Filters & Validation


* **Action Filters (`ValidationFilterAttribute`)**: A smart custom filter that ensures data integrity and Singleton logic.
* **Singleton Page Protection:** Prevents duplicate content for single-instance pages like **About Us**, **Contact**, or **Home**.
* **Logic:** If a record already exists for these sections, the "Add" button is hidden in the UI. Crucially, if a user attempts to bypass the UI by manually typing the URL (e.g., `/About/Add`), the filter intercepts the request and **redirects them back to the list**, preventing duplicate entries.
* **Model Validation:** Automatically checks `ModelState.IsValid` before controller actions execute.


* **FluentValidation**: All validation logic is decoupled from ViewModels. Instead of cluttering classes with `[Required]` attributes, rules are defined in separate validator classes (e.g., `TeamValidator`), ensuring separation of concerns and testability.


#### 🛠️ Helpers & Utilities


* **`EmailHelper`**: Encapsulates logic for sending "Forgot Password" and system notification emails via SMTP.
* **`ImageHelper`**: Manages secure file uploads, image resizing, and deletion of old files when records are updated.
* **`NotificationMessages`**: A static central repository for all system feedback strings (e.g., "Saved Successfully", "Error Occurred"). This eliminates "magic strings" and makes localization easy.


**Detailed Structure:**
```text
ServiceLayer/
├── 📂 Services/
│   ├── 📄 AboutService.cs                # Manages 'About Us' content
│   ├── 📄 AuthenticationUserListService.cs # Handles User List & Role Extension
│   ├── 📄 DashBoardService.cs            # Aggregates Admin Dashboard stats
│   ├── 📄 PortfolioService.cs            # Manages Project Portfolio & Images
│   ├── 📄 ServiceService.cs              # Manages Service Offerings
│   ├── 📄 TeamService.cs                 # Manages Team Members & Social Links
│   └── 📄 TestimonialService.cs          # Manages Client Reviews
├── 📂 AutoMapper/
│   ├── 📄 AboutMapper.cs
│   ├── 📄 PortfolioMapper.cs
│   ├── 📄 ServiceMapper.cs
│   ├── 📄 TeamMapper.cs
│   └── 📄 TestimonialMapper.cs
├── 📂 FluentValidation/
│   ├── 📄 AboutValidator.cs
│   ├── 📄 PortfolioValidator.cs
│   ├── 📄 ServiceValidator.cs
│   ├── 📄 TeamValidator.cs
│   └── 📄 TestimonialValidator.cs
├── 📂 Helpers/
│   ├── 📄 EmailHelper.cs                 # Email sending logic (SMTP)
│   └── 📄 ImageHelper.cs                 # File upload & deletion logic
├── 📂 IdentityCustomizations/
│   ├── 📄 CustomIdentityErrorDescriber.cs # Overridden Identity error messages
│   ├── 📄 CustomPasswordValidator.cs      # Custom password rules
│   └── 📄 CustomUserValidator.cs          # Custom username rules
├── 📂 Filters/
│   └── 📄 ValidationFilterAttribute.cs    # Singleton Logic & Model Validation
├── 📂 Messages/
│   └── 📄 NotificationMessages.cs         # Static class for global system messages
└── 📂 Exceptions/
    └── 📄 ClientSideException.cs

```


5. **Presentation Layer (MVC) 🌐**
The entry point. Contains Controllers, Razor Views, and Middleware configuration.

---

## 🛡️ Security & Concurrency

This project implements enterprise-grade security patterns, enforcing **Least Privilege** and protecting against data conflicts.

### 🔐 1. Dynamic "AdminObserver" Assignment

Unlike simple systems where roles are hardcoded in `Program.cs`, **Plumbers911** utilizes a dynamic assignment system via the **User List Dashboard**.

* **The "Extend" Button:** A SuperAdmin can view the User List and click the **"Extend"** button next to a standard user.
* **Behind the Scenes:** This action dynamically injects the `AdminObserver` claim/role into the user's security stamp in the database.
* **Immediate Effect:** The user immediately gains access to the Dashboard without requiring a server restart or code deployment.

### 🚫 2. Granular Permissions & Restrictions

The `AdminObserver` policy is strictly limited to prevent abuse and data loss. It enforces a hierarchy of power:

* **✅ Can:** View the Dashboard, Edit Content (Services, Portfolio, etc.), and Update Texts.
* **❌ Cannot Delete:** The `AdminObserver` **cannot delete** any entity (Services, Team Members, etc.). The delete button is programmatically disabled/protected.
* **❌ Cannot Grant Roles:** The `AdminObserver` **cannot** see or use the "Extend" button. They cannot promote themselves or others to SuperAdmin.

### ⚔️ 3. Concurrency Control (`RowVersion`)

To prevent the "Lost Update" problem where two admins edit the same record simultaneously:

* **Implementation:** Every table inherits from `BaseEntity` which includes a `RowVersion` (timestamp).
* **Conflict Handling:** If User A saves a record that User B has already modified, the system detects the `RowVersion` mismatch.
* **Result:** The save is aborted, and a **Toast Notification** warns the user to refresh the data, preventing accidental overwrites.

### 🕵️ 4. Security Stamp Middleware

A custom **Security Middleware** validates every HTTP request.

* **Function:** It checks the user's **Security Stamp** against the database on every request.
* **Impact:** If a SuperAdmin revokes a user's role or bans them, their existing cookie is **immediately invalidated**, forcing a logout on their very next click.

---

## 🛠️ Technologies & Tools

* **🚀 Framework:** ASP.NET Core 8.0 (MVC)
* **💾 ORM:** Entity Framework Core (SQL Server)
* **📝 Logging:** NLog
* **✅ Validation:** FluentValidation
* **🔄 Mapping:** AutoMapper
* **🛡️ Security:** ASP.NET Core Identity
* **🎨 UI:** Bootstrap 5 & jQuery

---

## 📂 Project Structure

```text
├── CoreLayer                    # 🧩 Base Interfaces & Common Contracts
├── EntityLayer                  # 📦 Database Entities & ViewModels (DTOs)
├── RepositoryLayer              # 🗄️ DbContext, Migrations & Generic Repositories
├── ServiceLayer                 # ⚙️ Business Logic, Validation Rules & Mapping Profiles
└── Plumbing.MVC                 # 🌐 Controllers, Views, NLog.config & wwwroot

```

---

## 📝 Logging & Validation

This project prioritizes reliability and debugging speed through a sophisticated pipeline:

* **🔍 NLog Integration:** A custom `nlog.config` captures runtime data. Errors are written to structured log files, allowing developers to trace issues in production without attaching a debugger.
* **🛡️ FluentValidation:** Validation logic is decoupled into the Service Layer. This ensures that business rules are consistent regardless of where the data comes from (API, Web, or Console).

---

## ⚙️ Getting Started

### 📋 Prerequisites

* **🖥️ .NET 8.0 SDK**
* **🗃️ SQL Server**
* **💻 Visual Studio 2022**

### 🛠️ Setup

1. **Clone the repository:**
```bash
git clone https://github.com/YounisSaid/Plumbing.git
cd Plumbing

```


2. **Configure appsettings.json 📝:**
Update the `ConnectionStrings` in `Plumbing.MVC/appsettings.json` to point to your local SQL Server instance.
3. **Run Migrations 🏗️:**
```bash
dotnet ef database update --project RepositoryLayer --startup-project Plumbing.MVC

```


4. **Run the Application ▶️:**
```bash
dotnet run --project Plumbing.MVC

```


## 📜 License

Distributed under the MIT License. See [LICENSE](https://www.google.com/search?q=LICENSE) for more information.
