# MediCare Hospital Management System
## Master Development Prompts & AI Implementation Playbook
**Version:** 1.0  
**Target Platform:** C# .NET 10.0 Windows Forms (`net10.0-windows`)  
**Database:** Microsoft SQL Server with ADO.NET  
**Architecture:** Strict 3-Layer Architecture (Presentation $\rightarrow$ Business Logic $\rightarrow$ Data Access $\rightarrow$ Database)  
**Academic Alignment:** CS107.3 – Object Oriented Programming with C# (Group Coursework, 9 Members)  

---

## 📋 Executive Overview & Document Purpose

This document serves as the **Master Implementation Blueprint** for the MediCare Hospital Management System. It synthesizes all requirements from:
1. **System PRD v2.0** (`MediCare_Hospital_Management_System_System_PRD_v2.0.docx`)
2. **Database Schema Specification v1.0** (`MediCare Hospital Management System database schema.docx`)
3. **System Architecture & Coding Guidelines v1.0** (`MediCare Hospital Management System architecture and coding guidelines .docx`)
4. **Workload Division Specification** (`Workload dividation.pdf`)
5. **Team Documentation Usage Guidelines** (`guidelines.docx`)

### How to Use This Playbook
This document contains a sequence of **11 self-contained, copy-paste-ready prompts (Prompts 0 through 10)**. 
- You can feed each prompt sequentially into an AI coding assistant (such as Antigravity, Claude, or ChatGPT) or use it as an explicit task specification for each team member.
- Each prompt specifies the exact files to create, business rules to enforce, database tables accessed, UI controls to build, and test cases to run.
- **Do not skip prompts**: Each module builds cleanly upon the foundation created by prior prompts.

---

## 🏛️ System Architecture & Engineering Standard

Before executing any module prompt, all development must strictly follow these overarching architectural rules:

### 1. The 3-Layer Structure
```
┌─────────────────────────────────────────────────────────┐
│              PRESENTATION LAYER (Forms/)                │
│  - Windows Forms (.cs & .Designer.cs)                   │
│  - User interaction, data binding, UI validation        │
│  - NEVER writes SQL or opens database connections       │
└────────────────────────────┬────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────┐
│             BUSINESS LOGIC LAYER (Services/)            │
│  - Pure business rules, validation, calculations        │
│  - Mediates between Presentation and Repositories       │
│  - Coordinates cross-module workflows & constraints     │
└────────────────────────────┬────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────┐
│              DATA ACCESS LAYER (Repositories/)          │
│  - ADO.NET (SqlConnection, SqlCommand, SqlDataReader)  │
│  - Parameterized SQL queries only (NO string concats)   │
│  - Maps SQL data to C# domain Models (Models/)          │
└────────────────────────────┬────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────┐
│               DATABASE PERSISTENCE (SQL Server)         │
│  - 12 normalized relational tables                      │
│  - PKs, FKs, CHECK constraints, UNIQUE indexes          │
└─────────────────────────────────────────────────────────┘
```

### 2. Standard Project Folder Structure
```
MEDICARE HOSPITAL MANGAMENT/
├── Database/
│   └── MediCare.sql                       # Complete DDL & Seed Script
├── Helpers/
│   ├── DatabaseHelper.cs                  # Centralized Connection & Transaction helper
│   ├── ValidationHelper.cs                # Regex, NIC, Phone, Email & Input Validators
│   ├── PasswordHasher.cs                  # PBKDF2 / Secure SHA-256 password hashing
│   └── SessionManager.cs                  # Active User Session & Role Tracker
├── Models/
│   ├── Role.cs, User.cs, Department.cs, Doctor.cs, Patient.cs
│   ├── Appointment.cs, MedicalRecord.cs, Medicine.cs
│   ├── Prescription.cs, PrescriptionItem.cs, Bill.cs, Payment.cs
│   └── ReportModels.cs (DTOs for Dashboard & Analytics)
├── Repositories/
│   ├── UserRepository.cs, DepartmentRepository.cs, DoctorRepository.cs
│   ├── PatientRepository.cs, AppointmentRepository.cs, MedicalRecordRepository.cs
│   ├── MedicineRepository.cs, PrescriptionRepository.cs, BillRepository.cs
│   ├── PaymentRepository.cs, ReportRepository.cs
├── Services/
│   ├── AuthenticationService.cs, UserService.cs, DepartmentService.cs
│   ├── DoctorService.cs, PatientService.cs, AppointmentService.cs
│   ├── MedicalRecordService.cs, MedicineService.cs, PrescriptionService.cs
│   ├── PharmacyService.cs, BillingService.cs, PaymentService.cs
│   ├── DashboardService.cs, ReportService.cs
└── Forms/
    ├── MainForm.cs                        # Master Navigation Shell (MDI / Modern Panel)
    ├── LoginForm.cs                       # Authentication Entry
    ├── UserManagementForm.cs              # User CRUD & Role Assignment
    ├── DepartmentForm.cs, DoctorForm.cs   # Staff Structure
    ├── PatientListForm.cs, PatientEntryForm.cs
    ├── AppointmentBookingForm.cs, AppointmentListForm.cs
    ├── ConsultationForm.cs, PatientMedicalHistoryForm.cs
    ├── MedicineForm.cs, PrescriptionForm.cs, PharmacyDispenseForm.cs
    ├── BillingForm.cs, PaymentForm.cs, PaymentHistoryForm.cs
    └── DashboardControl.cs, ReportsForm.cs
```

### 3. Naming Conventions & Coding Guidelines
- **C# Classes & Methods:** `PascalCase` (e.g., `PatientRepository`, `CalculateOutstandingBalance()`)
- **Local Variables & Parameters:** `camelCase` (e.g., `patientId`, `appointmentDate`)
- **Private Fields:** `_camelCase` (e.g., `_connectionString`, `_patientService`)
- **WinForms UI Controls:** Explicit Hungarian-style prefixes:
  - TextBoxes: `txtFirstName`, `txtNIC`, `txtStockQuantity`
  - Buttons: `btnSave`, `btnCancel`, `btnDelete`, `btnSearch`
  - ComboBoxes: `cmbDoctor`, `cmbDepartment`, `cmbStatus`
  - DataGridViews: `dgvPatients`, `dgvAppointments`, `dgvPrescriptionItems`
  - DateTimePickers: `dtpAppointmentDate`, `dtpDateOfBirth`
  - Labels: `lblTotalAmount`, `lblStatus`
- **Security Rule:** No raw SQL concatenation (`"WHERE NIC = '" + txtNIC.Text + "'"` is strictly forbidden). Use `cmd.Parameters.AddWithValue("@NIC", nic)` or explicit `SqlParameter` types.
- **Passwords:** Plain-text passwords must never be stored in the database. `PasswordHash` is stored using PBKDF2/SHA-256.

### 4. Role-Based Access Matrix
| Module / Feature | Administrator | Receptionist | Doctor | Pharmacist | Cashier |
|---|:---:|:---:|:---:|:---:|:---:|
| **Users & Roles** | Full CRUD | ❌ No Access | ❌ No Access | ❌ No Access | ❌ No Access |
| **Departments** | Full CRUD | 👁️ View Only | 👁️ View Only | ❌ No Access | ❌ No Access |
| **Doctors** | Full CRUD | 👁️ View Only | 👁️ View Only | ❌ No Access | ❌ No Access |
| **Patients** | Full CRUD | Full CRUD | 👁️ View / History | ❌ No Access | 👁️ Search / View |
| **Appointments** | Full CRUD | Full CRUD | 👁️ Assigned Only | ❌ No Access | ❌ No Access |
| **Medical Records** | 👁️ View Only | ❌ No Access | Full CRUD | ❌ No Access | ❌ No Access |
| **Prescriptions** | 👁️ View Only | ❌ No Access | Create / View | Full Dispense | ❌ No Access |
| **Pharmacy / Medicines** | Full CRUD | ❌ No Access | 👁️ View Stock | Full CRUD | ❌ No Access |
| **Billing & Payments** | Full CRUD | 👁️ View Only | ❌ No Access | ❌ No Access | Full CRUD |
| **Dashboard & Reports** | All Reports | Appointment Reports | Doctor Workload | Stock / Expiry | Revenue / Receipts |

---

## 🗺️ Master Workload Allocation Mapping (9 Members)

| Member | Assigned Responsibility | Primary UI Forms | Core C# Classes | Main Tables |
|:---:|---|---|---|---|
| **Member 1** | Authentication & User Management | `LoginForm`, `UserManagementForm`, `ChangePasswordForm` | `User`, `Role`, `AuthenticationService`, `UserService`, `UserRepository` | `Users`, `Roles` |
| **Member 2** | Patient Management | `PatientListForm`, `PatientEntryForm` | `Patient`, `PatientService`, `PatientRepository` | `Patients` |
| **Member 3** | Doctor & Department Management | `DepartmentForm`, `DoctorForm` | `Department`, `Doctor`, `DoctorService`, `DepartmentService`, `DoctorRepository` | `Departments`, `Doctors`, `Users` |
| **Member 4** | Appointment Scheduling & Conflict Engine | `AppointmentBookingForm`, `AppointmentListForm` | `Appointment`, `AppointmentService`, `AppointmentRepository` | `Appointments`, `Patients`, `Doctors` |
| **Member 5** | Medical Records & Clinical History | `ConsultationForm`, `PatientMedicalHistoryForm` | `MedicalRecord`, `MedicalRecordService`, `MedicalRecordRepository` | `MedicalRecords`, `Patients`, `Doctors`, `Appointments` |
| **Member 6** | Prescription & Pharmacy Stock Control | `MedicineForm`, `PrescriptionForm`, `PharmacyDispenseForm` | `Medicine`, `Prescription`, `PrescriptionItem`, `PharmacyService`, `PrescriptionService`, `MedicineRepository` | `Medicines`, `Prescriptions`, `PrescriptionItems` |
| **Member 7** | Billing, Invoicing & Payment Processing | `BillingForm`, `PaymentForm`, `PaymentHistoryForm` | `Bill`, `Payment`, `BillingService`, `PaymentService`, `BillRepository`, `PaymentRepository` | `Bills`, `Payments`, `Patients` |
| **Member 8** | Dashboard, Executive Reports & Analytics | `DashboardControl`, `ReportsForm` | `ReportDTOs`, `DashboardService`, `ReportService`, `ReportRepository` | Multi-table read-only queries |
| **Member 9** | Infrastructure, DB DDL & System Integration | `MainForm` (Master Shell), Integration Tests | `DatabaseHelper`, `ValidationHelper`, `PasswordHasher`, `SessionManager` | All 12 tables & Seed script |

---

# 🚀 THE MASTER DEVELOPMENT PROMPTS

---

## 📦 Prompt 0: Database Schema & Seed Data Script
**Assigned Responsibility:** Member 9 (Database & Integration)  
**File Output:** `Database/MediCare.sql`  

```markdown
### TASK: Create the Complete Microsoft SQL Server Database Script for MediCare Hospital Management System

You are the Lead Database Architect for the MediCare Hospital Management System.
Generate a complete, robust, and clean SQL Server script named `Database/MediCare.sql` that can be run directly in SQL Server Management Studio (SSMS) or via sqlcmd.

#### 1. Database Creation & Idempotency
- Check if database `MediCareDB` exists; if not, create it.
- Use `USE MediCareDB;` and wrap table creation in `IF OBJECT_ID('dbo.TableName', 'U') IS NULL` checks or clean drop-and-recreate commands for reproducible setup.

#### 2. Create the 12 Core Relational Tables in Exact Dependency Order:
1. `Roles`:
   - `RoleID` INT IDENTITY(1,1) PRIMARY KEY
   - `RoleName` VARCHAR(30) NOT NULL UNIQUE
   - `Description` VARCHAR(150) NULL
2. `Users`:
   - `UserID` INT IDENTITY(1,1) PRIMARY KEY
   - `Username` VARCHAR(50) NOT NULL UNIQUE
   - `PasswordHash` VARCHAR(255) NOT NULL
   - `FullName` VARCHAR(100) NOT NULL
   - `RoleID` INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleID)
   - `IsActive` BIT NOT NULL DEFAULT 1
   - `CreatedAt` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
3. `Departments`:
   - `DepartmentID` INT IDENTITY(1,1) PRIMARY KEY
   - `DepartmentName` VARCHAR(100) NOT NULL UNIQUE
   - `Description` VARCHAR(200) NULL
   - `IsActive` BIT NOT NULL DEFAULT 1
4. `Doctors`:
   - `DoctorID` INT IDENTITY(1,1) PRIMARY KEY
   - `UserID` INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Users(UserID)
   - `DepartmentID` INT NOT NULL FOREIGN KEY REFERENCES Departments(DepartmentID)
   - `DoctorCode` VARCHAR(20) NOT NULL UNIQUE
   - `FirstName` VARCHAR(50) NOT NULL
   - `LastName` VARCHAR(50) NOT NULL
   - `Specialization` VARCHAR(100) NULL
   - `Phone` VARCHAR(20) NULL
   - `IsActive` BIT NOT NULL DEFAULT 1
5. `Patients`:
   - `PatientID` INT IDENTITY(1,1) PRIMARY KEY
   - `PatientCode` VARCHAR(20) NOT NULL UNIQUE
   - `NIC` VARCHAR(20) NULL UNIQUE
   - `FirstName` VARCHAR(50) NOT NULL
   - `LastName` VARCHAR(50) NOT NULL
   - `DateOfBirth` DATE NOT NULL
   - `Gender` VARCHAR(20) NOT NULL
   - `BloodGroup` VARCHAR(5) NULL
   - `Phone` VARCHAR(20) NOT NULL
   - `Email` VARCHAR(100) NULL
   - `Address` VARCHAR(250) NULL
   - `EmergencyContactName` VARCHAR(100) NULL
   - `EmergencyContactPhone` VARCHAR(20) NULL
   - `RegisteredAt` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
   - `IsActive` BIT NOT NULL DEFAULT 1
6. `Appointments`:
   - `AppointmentID` INT IDENTITY(1,1) PRIMARY KEY
   - `PatientID` INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID)
   - `DoctorID` INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID)
   - `AppointmentDate` DATE NOT NULL
   - `StartTime` TIME NOT NULL
   - `EndTime` TIME NOT NULL
   - `Reason` VARCHAR(250) NULL
   - `Status` VARCHAR(20) NOT NULL DEFAULT 'Scheduled' -- 'Scheduled', 'Completed', 'Cancelled', 'NoShow'
   - `Notes` VARCHAR(500) NULL
   - `CreatedByUserID` INT NOT NULL FOREIGN KEY REFERENCES Users(UserID)
   - `CreatedAt` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
7. `MedicalRecords`:
   - `MedicalRecordID` INT IDENTITY(1,1) PRIMARY KEY
   - `PatientID` INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID)
   - `DoctorID` INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID)
   - `AppointmentID` INT NULL FOREIGN KEY REFERENCES Appointments(AppointmentID)
   - `VisitDate` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
   - `Symptoms` VARCHAR(1000) NULL
   - `Diagnosis` VARCHAR(1000) NULL
   - `Treatment` VARCHAR(1500) NULL
   - `Notes` VARCHAR(1500) NULL
8. `Medicines`:
   - `MedicineID` INT IDENTITY(1,1) PRIMARY KEY
   - `MedicineCode` VARCHAR(20) NOT NULL UNIQUE
   - `MedicineName` VARCHAR(100) NOT NULL
   - `Category` VARCHAR(50) NULL
   - `Unit` VARCHAR(20) NOT NULL
   - `UnitPrice` DECIMAL(10,2) NOT NULL CHECK (UnitPrice >= 0)
   - `StockQuantity` INT NOT NULL DEFAULT 0 CHECK (StockQuantity >= 0)
   - `ReorderLevel` INT NOT NULL DEFAULT 10 CHECK (ReorderLevel >= 0)
   - `ExpiryDate` DATE NULL
   - `IsActive` BIT NOT NULL DEFAULT 1
9. `Prescriptions`:
   - `PrescriptionID` INT IDENTITY(1,1) PRIMARY KEY
   - `PatientID` INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID)
   - `DoctorID` INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID)
   - `MedicalRecordID` INT NULL FOREIGN KEY REFERENCES MedicalRecords(MedicalRecordID)
   - `PrescriptionDate` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
   - `Status` VARCHAR(20) NOT NULL DEFAULT 'Active' -- 'Active', 'Dispensed', 'Cancelled'
   - `Notes` VARCHAR(500) NULL
10. `PrescriptionItems`:
    - `PrescriptionItemID` INT IDENTITY(1,1) PRIMARY KEY
    - `PrescriptionID` INT NOT NULL FOREIGN KEY REFERENCES Prescriptions(PrescriptionID) ON DELETE CASCADE
    - `MedicineID` INT NOT NULL FOREIGN KEY REFERENCES Medicines(MedicineID)
    - `Quantity` INT NOT NULL CHECK (Quantity > 0)
    - `Dosage` VARCHAR(100) NOT NULL
    - `Frequency` VARCHAR(100) NOT NULL
    - `DurationDays` INT NOT NULL CHECK (DurationDays > 0)
    - `Instructions` VARCHAR(250) NULL
11. `Bills`:
    - `BillID` INT IDENTITY(1,1) PRIMARY KEY
    - `PatientID` INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID)
    - `AppointmentID` INT NULL FOREIGN KEY REFERENCES Appointments(AppointmentID)
    - `BillDate` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
    - `Description` VARCHAR(500) NULL
    - `Subtotal` DECIMAL(10,2) NOT NULL CHECK (Subtotal >= 0)
    - `Discount` DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (Discount >= 0)
    - `TotalAmount` DECIMAL(10,2) NOT NULL CHECK (TotalAmount >= 0)
    - `Status` VARCHAR(20) NOT NULL DEFAULT 'Pending' -- 'Pending', 'PartiallyPaid', 'Paid', 'Cancelled'
12. `Payments`:
    - `PaymentID` INT IDENTITY(1,1) PRIMARY KEY
    - `BillID` INT NOT NULL FOREIGN KEY REFERENCES Bills(BillID)
    - `PaymentDate` DATETIME2 NOT NULL DEFAULT SYSDATETIME()
    - `Amount` DECIMAL(10,2) NOT NULL CHECK (Amount > 0)
    - `PaymentMethod` VARCHAR(20) NOT NULL -- 'Cash', 'Card', 'BankTransfer'
    - `ReferenceNo` VARCHAR(50) NULL
    - `ReceivedByUserID` INT NOT NULL FOREIGN KEY REFERENCES Users(UserID)

#### 3. Performance Indexes:
Add non-clustered indexes on frequently searched columns:
- `IX_Appointments_Doctor_Date_Time` ON `Appointments(DoctorID, AppointmentDate, StartTime)`
- `IX_Patients_NIC` ON `Patients(NIC)`
- `IX_Patients_PatientCode` ON `Patients(PatientCode)`
- `IX_Medicines_Stock` ON `Medicines(StockQuantity, ReorderLevel)`
- `IX_Bills_PatientID` ON `Bills(PatientID, Status)`
- `IX_Prescriptions_Status` ON `Prescriptions(Status)`

#### 4. Realistic Fictional Seed Data:
Include seed data for:
- 5 Roles: `Administrator`, `Receptionist`, `Doctor`, `Pharmacist`, `Cashier`
- 5 System Users with hashed passwords (using a known test password like `Admin@123`, `Doctor@123`, etc.):
  - `admin` (Role: Administrator)
  - `receptionist1` (Role: Receptionist)
  - `dr_smith` (Role: Doctor)
  - `dr_johnson` (Role: Doctor)
  - `pharmacist1` (Role: Pharmacist)
  - `cashier1` (Role: Cashier)
- 5 Departments: `General Medicine`, `Pediatrics`, `Cardiology`, `Pharmacy`, `Laboratory`
- 2 Doctor profiles linked to `dr_smith` and `dr_johnson`
- 10 Realistic Patients (`PAT001` to `PAT010`)
- 15 Core Medicines with realistic prices, stocks, and reorder levels (`MED001` to `MED015`)
- Sample appointments, medical records, prescriptions, and paid/pending bills to enable instant end-to-end testing.
```

---

## 📦 Prompt 1: Shared Core Infrastructure & Helpers
**Assigned Responsibility:** Member 9 (Infrastructure & Integration)  
**File Outputs:**
- `Helpers/DatabaseHelper.cs`
- `Helpers/PasswordHasher.cs`
- `Helpers/ValidationHelper.cs`
- `Helpers/SessionManager.cs`

```markdown
### TASK: Implement Centralized Infrastructure & Helper Classes

Implement the core cross-cutting utility classes in `MEDICARE HOSPITAL MANGAMENT` under the `Helpers` namespace. All modules in the application will depend on these utilities.

#### 1. `Helpers/DatabaseHelper.cs`
- Centralize connection string management. Default to:
  `Server=localhost;Database=MediCareDB;Trusted_Connection=True;TrustServerCertificate=True;`
  (or configurable via `App.config` / connection string property).
- Method `SqlConnection GetConnection()`: Returns a new `SqlConnection`.
- Method `bool TestConnection(out string errorMessage)`: Opens and closes a connection to verify database reachability.
- Provide helper methods for executing scalar queries, parameterized non-queries, and data tables safely with `using` blocks.

#### 2. `Helpers/PasswordHasher.cs`
- Secure password hashing implementation using PBKDF2 (`Rfc2898DeriveBytes`) with HMACSHA256, 10,000+ iterations, and a cryptographic salt.
- Method `string HashPassword(string password)`: Returns a formatted string containing `salt:hash`.
- Method `bool VerifyPassword(string password, string storedHash)`: Safely compares the entered password against the stored salt & hash.
- Include a fallback verification for any predefined demo seed hashes.

#### 3. `Helpers/ValidationHelper.cs`
- Method `bool IsValidNIC(string nic)`: Validates old Sri Lankan NIC (9 digits + 'V'/'X') and new NIC format (12 digits).
- Method `bool IsValidPhone(string phone)`: Validates 10-digit mobile/landline numbers (e.g., `07XXXXXXXX`).
- Method `bool IsValidEmail(string email)`: Validates standard email address formats using regex.
- Method `bool IsNotEmpty(string input, string fieldName, out string errorMessage)`: Checks for null/whitespace.
- Method `bool IsPositiveDecimal(string input, string fieldName, out decimal value, out string errorMessage)`.
- Method `bool IsValidDateRange(DateTime start, DateTime end, out string errorMessage)`.

#### 4. `Helpers/SessionManager.cs`
- Static class managing the current logged-in user context.
- Properties:
  - `User? CurrentUser { get; set; }`
  - `bool IsLoggedIn { get; }`
  - `int CurrentUserId { get; }`
  - `string CurrentUsername { get; }`
  - `string CurrentUserRole { get; }`
- Helper permission check methods:
  - `bool HasRole(string roleName)`
  - `bool IsAdmin()`
  - `bool IsDoctor()`
  - `bool IsReceptionist()`
  - `bool IsPharmacist()`
  - `bool IsCashier()`
- Method `void Logout()`: Clears session state.

All code must include XML documentation comments, strict null checks, and defensive exception handling.
```

---

## 📦 Prompt 2: Module 1 – Authentication & User Management
**Assigned Responsibility:** Member 1  
**File Outputs:**
- `Models/Role.cs`, `Models/User.cs`
- `Repositories/UserRepository.cs`
- `Services/AuthenticationService.cs`, `Services/UserService.cs`
- `Forms/LoginForm.cs` (and `.Designer.cs`)
- `Forms/UserManagementForm.cs` (and `.Designer.cs`)
- `Forms/ChangePasswordForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Authentication & User Management (Member 1)

Implement the full authentication and user management module following the 3-layer architecture.

#### 1. Models:
- `Models/Role.cs`: `RoleID`, `RoleName`, `Description`.
- `Models/User.cs`: `UserID`, `Username`, `PasswordHash`, `FullName`, `RoleID`, `RoleName` (joined for display), `IsActive`, `CreatedAt`.

#### 2. Data Access Layer (`Repositories/UserRepository.cs`):
- Implement ADO.NET methods using parameterized queries:
  - `User? GetByUsername(string username)`
  - `User? GetById(int userId)`
  - `List<User> GetAllUsers()`
  - `List<Role> GetAllRoles()`
  - `int CreateUser(User user)`
  - `bool UpdateUser(User user)`
  - `bool UpdatePassword(int userId, string newPasswordHash)`
  - `bool ToggleUserActiveStatus(int userId, bool isActive)`
  - `bool IsUsernameTaken(string username, int? excludeUserId = null)`

#### 3. Business Logic Layer:
- `Services/AuthenticationService.cs`:
  - `User? Authenticate(string username, string password, out string errorMessage)`: Verifies username existence, checks `IsActive == true`, verifies password hash via `PasswordHasher`, sets `SessionManager.CurrentUser`, and returns the user.
- `Services/UserService.cs`:
  - Validate user input before persisting (non-empty username, unique username, full name, role selection).
  - Business rules: Cannot deactivate the last remaining active Administrator.

#### 4. Presentation Layer:
- `Forms/LoginForm.cs`:
  - Modern, elegant login dialog with hospital branding.
  - Inputs: `txtUsername`, `txtPassword` (password masked), `btnLogin`, `btnCancel`.
  - On submit: Calls `AuthenticationService.Authenticate()`. If successful, closes login dialog with `DialogResult.OK`. If failed, shows user-friendly error message without revealing whether username or password was incorrect.
- `Forms/UserManagementForm.cs`:
  - Available to Administrators only.
  - Controls: DataGridView `dgvUsers`, TextBoxes `txtUsername`, `txtFullName`, ComboBox `cmbRole`, CheckBox `chkIsActive`, Buttons `btnAddUser`, `btnUpdateUser`, `btnResetPassword`, `btnToggleStatus`.
- `Forms/ChangePasswordForm.cs`:
  - Inputs: `txtCurrentPassword`, `txtNewPassword`, `txtConfirmPassword`.
  - Validates password length (minimum 6 characters) and matching confirmation.
```

---

## 📦 Prompt 3: Module 3 – Doctor & Department Management
**Assigned Responsibility:** Member 3  
**File Outputs:**
- `Models/Department.cs`, `Models/Doctor.cs`
- `Repositories/DepartmentRepository.cs`, `Repositories/DoctorRepository.cs`
- `Services/DepartmentService.cs`, `Services/DoctorService.cs`
- `Forms/DepartmentForm.cs` (and `.Designer.cs`)
- `Forms/DoctorForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Doctor & Department Management (Member 3)

Implement the hospital staffing and department organization module according to the PRD and Database Schema.

#### 1. Models:
- `Models/Department.cs`: `DepartmentID`, `DepartmentName`, `Description`, `IsActive`.
- `Models/Doctor.cs`: `DoctorID`, `UserID`, `DepartmentID`, `DepartmentName` (display), `DoctorCode`, `FirstName`, `LastName`, `FullName` (computed property), `Specialization`, `Phone`, `IsActive`.

#### 2. Data Access Layer:
- `Repositories/DepartmentRepository.cs`:
  - `List<Department> GetAllDepartments(bool activeOnly = false)`
  - `Department? GetDepartmentById(int id)`
  - `int CreateDepartment(Department dept)`
  - `bool UpdateDepartment(Department dept)`
  - `bool IsDepartmentNameTaken(string name, int? excludeId = null)`
- `Repositories/DoctorRepository.cs`:
  - `List<Doctor> GetAllDoctors(bool activeOnly = false)`
  - `Doctor? GetDoctorById(int doctorId)`
  - `Doctor? GetDoctorByUserId(int userId)`
  - `List<Doctor> GetDoctorsByDepartment(int departmentId)`
  - `int CreateDoctor(Doctor doctor)`
  - `bool UpdateDoctor(Doctor doctor)`
  - `string GenerateNextDoctorCode()`: Generates `DOC001`, `DOC002` automatically.
  - `List<User> GetEligibleDoctorUserAccounts()`: Returns active Users with Role 'Doctor' who do not yet have a doctor profile attached.

#### 3. Business Logic Layer:
- `Services/DepartmentService.cs`:
  - Validate department name is required and unique.
- `Services/DoctorService.cs`:
  - Validate required fields (First Name, Last Name, Department selection, valid phone).
  - Ensure `DoctorCode` is unique.
  - Connect doctor profile to a valid `UserID` with role 'Doctor'.

#### 4. Presentation Layer:
- `Forms/DepartmentForm.cs`:
  - Controls: `dgvDepartments`, `txtDepartmentName`, `txtDescription`, `chkIsActive`, `btnSave`, `btnUpdate`, `btnClear`.
  - Provides quick department CRUD.
- `Forms/DoctorForm.cs`:
  - Controls: `dgvDoctors`, `txtDoctorCode` (read-only auto-generated), `cmbUserAccount`, `cmbDepartment`, `txtFirstName`, `txtLastName`, `txtSpecialization`, `txtPhone`, `chkIsActive`, `btnSave`, `btnUpdate`, `btnClear`, `txtSearchDoctor`.
  - Filter doctors dynamically by department.
```

---

## 📦 Prompt 4: Module 2 – Patient Management
**Assigned Responsibility:** Member 2  
**File Outputs:**
- `Models/Patient.cs`
- `Repositories/PatientRepository.cs`
- `Services/PatientService.cs`
- `Forms/PatientListForm.cs` (and `.Designer.cs`)
- `Forms/PatientEntryForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Patient Registration & Management (Member 2)

Implement the patient management module responsible for registration, search, history lookup, and updating patient demographics.

#### 1. Model (`Models/Patient.cs`):
- Properties:
  - `int PatientID`, `string PatientCode`, `string? NIC`, `string FirstName`, `string LastName`
  - `string FullName => $"{FirstName} {LastName}"`
  - `DateTime DateOfBirth`, `string Gender`, `string? BloodGroup`, `string Phone`, `string? Email`
  - `string? Address`, `string? EmergencyContactName`, `string? EmergencyContactPhone`
  - `DateTime RegisteredAt`, `bool IsActive`
  - `int Age => DateTime.Today.Year - DateOfBirth.Year - (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);`

#### 2. Data Access Layer (`Repositories/PatientRepository.cs`):
- Parameterized SQL queries for:
  - `List<Patient> GetAllPatients(bool activeOnly = true)`
  - `Patient? GetPatientById(int patientId)`
  - `Patient? GetPatientByCode(string patientCode)`
  - `List<Patient> SearchPatients(string query)`: Searches across `PatientCode`, `NIC`, `FirstName`, `LastName`, and `Phone`.
  - `int CreatePatient(Patient patient)`
  - `bool UpdatePatient(Patient patient)`
  - `bool DeactivatePatient(int patientId)`: Soft delete (`IsActive = 0`).
  - `string GenerateNextPatientCode()`: Generates next sequential `PAT001`, `PAT002`, etc.
  - `bool IsNICTaken(string nic, int? excludePatientId = null)`

#### 3. Business Logic Layer (`Services/PatientService.cs`):
- Enforce Business Rules BR-01, BR-02, BR-12:
  - Mandatory fields: `FirstName`, `LastName`, `DateOfBirth`, `Gender`, `Phone`.
  - Phone validation via `ValidationHelper.IsValidPhone`.
  - NIC validation via `ValidationHelper.IsValidNIC` (if provided, must be valid and unique).
  - Date of Birth must not be in the future.
  - Email format validation if supplied.

#### 4. Presentation Layer:
- `Forms/PatientListForm.cs`:
  - DataGridView `dgvPatients` showing PatientCode, FullName, NIC, Age, Gender, Phone, RegisteredAt.
  - Quick Search box `txtSearch` with instant key-up filtering or search button.
  - Buttons: `btnRegisterNew`, `btnEditPatient`, `btnDeactivatePatient`, `btnViewHistory`, `btnRefresh`.
- `Forms/PatientEntryForm.cs`:
  - Modal dialog for both Registration and Editing.
  - Controls: `txtPatientCode` (read-only), `txtNIC`, `txtFirstName`, `txtLastName`, `dtpDateOfBirth`, `cmbGender` (Male, Female, Other), `cmbBloodGroup` (A+, A-, B+, B-, AB+, AB-, O+, O-), `txtPhone`, `txtEmail`, `txtAddress`, `txtEmergencyName`, `txtEmergencyPhone`.
  - ErrorProvider component for clean field-level validation feedback.
```

---

## 📦 Prompt 5: Module 4 – Appointment Scheduling & Conflict Engine
**Assigned Responsibility:** Member 4  
**File Outputs:**
- `Models/Appointment.cs`
- `Repositories/AppointmentRepository.cs`
- `Services/AppointmentService.cs`
- `Forms/AppointmentBookingForm.cs` (and `.Designer.cs`)
- `Forms/AppointmentListForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Appointment Booking & Overlap Prevention Engine (Member 4)

Build the Appointment Scheduling module with strict double-booking and time overlap prevention.

#### 1. Model (`Models/Appointment.cs`):
- Properties: `AppointmentID`, `PatientID`, `DoctorID`, `AppointmentDate`, `StartTime`, `EndTime`, `Reason`, `Status` ('Scheduled', 'Completed', 'Cancelled', 'NoShow'), `Notes`, `CreatedByUserID`, `CreatedAt`.
- Joined display properties: `PatientCode`, `PatientName`, `PatientPhone`, `DoctorName`, `DepartmentName`.

#### 2. Data Access Layer (`Repositories/AppointmentRepository.cs`):
- Parameterized ADO.NET methods:
  - `List<Appointment> GetAppointmentsByDate(DateTime date, int? doctorId = null)`
  - `List<Appointment> GetAppointmentsForDoctor(int doctorId, DateTime? date = null)`
  - `List<Appointment> GetAppointmentsForPatient(int patientId)`
  - `Appointment? GetAppointmentById(int appointmentId)`
  - `int CreateAppointment(Appointment appt)`
  - `bool UpdateAppointment(Appointment appt)`
  - `bool UpdateAppointmentStatus(int appointmentId, string status)`
  - `bool HasDoctorOverlap(int doctorId, DateTime date, TimeSpan startTime, TimeSpan endTime, int? excludeAppointmentId = null)`:
    Executes:
    ```sql
    SELECT COUNT(1) FROM Appointments
    WHERE DoctorID = @DoctorID
      AND AppointmentDate = @AppointmentDate
      AND Status = 'Scheduled'
      AND (@ExcludeID IS NULL OR AppointmentID != @ExcludeID)
      AND (StartTime < @EndTime AND EndTime > @StartTime)
    ```

#### 3. Business Logic Layer (`Services/AppointmentService.cs`):
- Enforce Business Rules BR-03, BR-04, BR-11:
  - An appointment must reference valid, active Patient and Doctor records.
  - Appointment date cannot be in the past.
  - `EndTime` must be after `StartTime` (e.g., minimum 15-minute slot).
  - Overlap check: System strictly throws a validation exception if `HasDoctorOverlap` returns true:
    *"Doctor already has an active appointment between {StartTime} and {EndTime} on {Date}."*
  - Status transition validation: Cancelled appointments cannot be directly completed without rescheduling.

#### 4. Presentation Layer:
- `Forms/AppointmentListForm.cs`:
  - Filters: `dtpFilterDate`, `cmbFilterDoctor`, `cmbFilterStatus`.
  - DataGridView `dgvAppointments` displaying Time, Patient, Doctor, Department, Reason, Status.
  - Action buttons: `btnNewAppointment`, `btnReschedule`, `btnCancelAppointment`, `btnMarkCompleted`.
- `Forms/AppointmentBookingForm.cs`:
  - Patient selector dropdown or "Search Patient" lookup dialog.
  - Doctor selector dropdown (grouped/filtered by Department).
  - `dtpDate`, `dtpStartTime` (Time picker), `dtpEndTime` (Time picker or duration selector 15/30/45 min).
  - `txtReason`, `txtNotes`.
  - Real-time check button: `btnCheckAvailability` to confirm slot before saving.
```

---

## 📦 Prompt 6: Module 5 – Clinical Consultations & Medical Records
**Assigned Responsibility:** Member 5  
**File Outputs:**
- `Models/MedicalRecord.cs`
- `Repositories/MedicalRecordRepository.cs`
- `Services/MedicalRecordService.cs`
- `Forms/ConsultationForm.cs` (and `.Designer.cs`)
- `Forms/PatientMedicalHistoryForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Medical Consultation Records & Patient History (Member 5)

Implement the clinical consultation records module where Doctors record symptoms, diagnoses, treatment notes, and view past patient histories.

#### 1. Model (`Models/MedicalRecord.cs`):
- Properties: `MedicalRecordID`, `PatientID`, `DoctorID`, `AppointmentID`, `VisitDate`, `Symptoms`, `Diagnosis`, `Treatment`, `Notes`.
- Joined display properties: `PatientCode`, `PatientName`, `DoctorName`, `DepartmentName`.

#### 2. Data Access Layer (`Repositories/MedicalRecordRepository.cs`):
- Parameterized ADO.NET queries:
  - `int CreateRecord(MedicalRecord record)`
  - `bool UpdateRecord(MedicalRecord record)`
  - `MedicalRecord? GetById(int recordId)`
  - `MedicalRecord? GetByAppointmentId(int appointmentId)`
  - `List<MedicalRecord> GetRecordsByPatientId(int patientId)`: Ordered by `VisitDate DESC` to show timeline.
  - `List<MedicalRecord> GetRecordsByDoctorId(int doctorId)`

#### 3. Business Logic Layer (`Services/MedicalRecordService.cs`):
- Enforce Business Rules BR-05:
  - Only authenticated users with role 'Doctor' (or Administrator) can record consultations.
  - Ensure `PatientID` and `DoctorID` exist.
  - `Symptoms` and `Diagnosis` are mandatory.
  - When saving a consultation linked to an `AppointmentID`, automatically update that appointment's status to 'Completed'.

#### 4. Presentation Layer:
- `Forms/ConsultationForm.cs`:
  - Header: Patient Info summary panel (PatientCode, Name, Age, Gender, BloodGroup).
  - Inputs: `txtSymptoms` (multiline), `txtDiagnosis` (multiline), `txtTreatment` (multiline), `txtNotes` (multiline).
  - Buttons: `btnSaveConsultation`, `btnCreatePrescription` (opens Prescription form with Patient & Doctor pre-filled), `btnViewFullHistory`.
- `Forms/PatientMedicalHistoryForm.cs`:
  - Displays chronological timeline of previous consultations for a selected patient.
  - Split view: Left side lists previous visit dates and diagnosing doctors; Right side displays full details of the selected visit (symptoms, diagnosis, treatments, and linked prescriptions).
```

---

## 📦 Prompt 7: Module 6 – Prescription & Pharmacy Stock Management
**Assigned Responsibility:** Member 6  
**File Outputs:**
- `Models/Medicine.cs`, `Models/Prescription.cs`, `Models/PrescriptionItem.cs`
- `Repositories/MedicineRepository.cs`, `Repositories/PrescriptionRepository.cs`
- `Services/MedicineService.cs`, `Services/PrescriptionService.cs`, `Services/PharmacyService.cs`
- `Forms/MedicineForm.cs` (and `.Designer.cs`)
- `Forms/PrescriptionForm.cs` (and `.Designer.cs`)
- `Forms/PharmacyDispenseForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Prescription & Pharmacy Stock Management (Member 6)

Build the prescription and pharmacy inventory module, supporting multi-item prescriptions, stock tracking, low-stock warnings, and atomic dispensing.

#### 1. Models:
- `Models/Medicine.cs`: `MedicineID`, `MedicineCode`, `MedicineName`, `Category`, `Unit`, `UnitPrice`, `StockQuantity`, `ReorderLevel`, `ExpiryDate`, `IsActive`, `IsLowStock => StockQuantity <= ReorderLevel`.
- `Models/Prescription.cs`: `PrescriptionID`, `PatientID`, `DoctorID`, `MedicalRecordID`, `PrescriptionDate`, `Status` ('Active', 'Dispensed', 'Cancelled'), `Notes`, `PatientName`, `DoctorName`, `List<PrescriptionItem> Items`.
- `Models/PrescriptionItem.cs`: `PrescriptionItemID`, `PrescriptionID`, `MedicineID`, `MedicineName`, `Quantity`, `Dosage`, `Frequency`, `DurationDays`, `Instructions`, `AvailableStock`.

#### 2. Data Access Layer:
- `Repositories/MedicineRepository.cs`:
  - `List<Medicine> GetAllMedicines(bool activeOnly = true)`
  - `List<Medicine> GetLowStockMedicines()`: `WHERE StockQuantity <= ReorderLevel AND IsActive = 1`
  - `Medicine? GetById(int medicineId)`
  - `int CreateMedicine(Medicine med)`
  - `bool UpdateMedicine(Medicine med)`
  - `bool UpdateStock(int medicineId, int quantityChange, SqlTransaction? transaction = null)`
- `Repositories/PrescriptionRepository.cs`:
  - `int CreatePrescriptionWithItems(Prescription prescription)`: Uses an ADO.NET `SqlTransaction` to insert the `Prescription` header, obtain `SCOPE_IDENTITY()`, and insert all `PrescriptionItems`.
  - `Prescription? GetPrescriptionWithItems(int prescriptionId)`
  - `List<Prescription> GetPrescriptionsByStatus(string status)`
  - `List<Prescription> GetPrescriptionsForPatient(int patientId)`
  - `bool DispensePrescription(int prescriptionId, List<PrescriptionItem> items)`: Executes inside a database transaction:
    1. Checks if all items have sufficient stock.
    2. Decrements `Medicines.StockQuantity` for each item.
    3. Updates `Prescriptions.Status = 'Dispensed'`.

#### 3. Business Logic Layer:
- `Services/MedicineService.cs`:
  - Validates positive price and non-negative stock quantity.
  - Unique `MedicineCode`.
- `Services/PrescriptionService.cs`:
  - Validates that prescription has at least 1 item.
  - Validates that prescribed quantity > 0 and duration > 0.
- `Services/PharmacyService.cs`:
  - Dispense validation: If `item.Quantity > medicine.StockQuantity`, throws `InvalidOperationException`:
    *"Cannot dispense prescription: Insufficient stock for medicine '{medicine.MedicineName}'. Required: {item.Quantity}, Available: {medicine.StockQuantity}."*

#### 4. Presentation Layer:
- `Forms/MedicineForm.cs`:
  - Inventory management grid with color-coded low-stock highlighting (yellow/red).
  - Inputs: `txtMedicineCode`, `txtMedicineName`, `txtCategory`, `txtUnit` (Tablets, Syrup, Injection), `txtUnitPrice`, `txtStockQuantity`, `txtReorderLevel`, `dtpExpiryDate`.
- `Forms/PrescriptionForm.cs`:
  - Used by Doctors.
  - Select Patient, link to Consultation.
  - Medicine selection sub-grid: Add multiple medicines with Dosage, Frequency (e.g., "TDS / 3 times a day"), Duration (days), and Quantity.
- `Forms/PharmacyDispenseForm.cs`:
  - Used by Pharmacists.
  - Displays pending active prescriptions. Selecting a prescription displays all items, required quantity vs. currently available stock.
  - `btnDispense`: Validates stock, prompts confirmation, deducts inventory, and marks as Dispensed.
```

---

## 📦 Prompt 8: Module 7 – Billing & Payment Processing
**Assigned Responsibility:** Member 7  
**File Outputs:**
- `Models/Bill.cs`, `Models/Payment.cs`
- `Repositories/BillRepository.cs`, `Repositories/PaymentRepository.cs`
- `Services/BillingService.cs`, `Services/PaymentService.cs`
- `Forms/BillingForm.cs` (and `.Designer.cs`)
- `Forms/PaymentForm.cs` (and `.Designer.cs`)
- `Forms/PaymentHistoryForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Billing, Invoicing & Payment Processing (Member 7)

Build the billing and payment processing module for cashiers to invoice consultations/medicines, apply discounts, collect payments, and track balances.

#### 1. Models:
- `Models/Bill.cs`: `BillID`, `PatientID`, `AppointmentID`, `BillDate`, `Description`, `Subtotal`, `Discount`, `TotalAmount`, `Status` ('Pending', 'PartiallyPaid', 'Paid', 'Cancelled'), `PatientName`, `PatientCode`, `TotalPaid`, `OutstandingBalance => TotalAmount - TotalPaid`.
- `Models/Payment.cs`: `PaymentID`, `BillID`, `PaymentDate`, `Amount`, `PaymentMethod` ('Cash', 'Card', 'BankTransfer'), `ReferenceNo`, `ReceivedByUserID`, `ReceivedByUserName`.

#### 2. Data Access Layer:
- `Repositories/BillRepository.cs`:
  - `int CreateBill(Bill bill)`
  - `Bill? GetBillById(int billId)`
  - `List<Bill> GetBillsByPatient(int patientId)`
  - `List<Bill> GetPendingBills()`: Bills with status 'Pending' or 'PartiallyPaid'.
  - `bool UpdateBillStatus(int billId, string status, SqlTransaction? transaction = null)`
- `Repositories/PaymentRepository.cs`:
  - `int RecordPayment(Payment payment)`: Uses transaction to insert Payment and update `Bill.Status` ('Paid' if balance == 0, 'PartiallyPaid' if balance > 0).
  - `List<Payment> GetPaymentsByBillId(int billId)`
  - `decimal GetTotalPaidForBill(int billId)`

#### 3. Business Logic Layer:
- `Services/BillingService.cs`:
  - Calculate `TotalAmount = Subtotal - Discount`.
  - Validate `Discount >= 0` and `Discount <= Subtotal`.
  - Ensure `Subtotal >= 0`.
- `Services/PaymentService.cs`:
  - Enforce Business Rule BR-10:
    Payment amount must be strictly > 0.
    `payment.Amount` cannot exceed `bill.OutstandingBalance`:
    *"Payment amount (Rs. {Amount}) exceeds outstanding balance (Rs. {OutstandingBalance})."*
  - Record payment, update bill status atomically.

#### 4. Presentation Layer:
- `Forms/BillingForm.cs`:
  - Search patient or select appointment.
  - Calculate charges (Consultation fee, Pharmacy fee, Room/Service charge).
  - Inputs: `txtSubtotal`, `txtDiscount`, `lblTotalAmount` (calculated dynamically).
  - Buttons: `btnGenerateBill`, `btnViewPendingBills`.
- `Forms/PaymentForm.cs`:
  - Select Bill: Displays Bill details, Total Amount, Previous Payments, Outstanding Balance.
  - Inputs: `txtPaymentAmount`, `cmbPaymentMethod` (Cash, Card, Bank Transfer), `txtReferenceNo`.
  - Buttons: `btnProcessPayment`, `btnPrintReceipt` (clean printable layout / preview box).
- `Forms/PaymentHistoryForm.cs`:
  - Displays all payment receipts for a bill or patient with date, method, receiver name, and remaining balance.
```

---

## 📦 Prompt 8+: Module 8 – Operational Dashboard, Reports & Global Analytics
**Assigned Responsibility:** Member 8  
**File Outputs:**
- `Models/ReportModels.cs` (DTOs for KPIs and Reports)
- `Repositories/ReportRepository.cs`
- `Services/DashboardService.cs`, `Services/ReportService.cs`
- `Forms/DashboardControl.cs` (or `Forms/DashboardForm.cs`)
- `Forms/ReportsForm.cs` (and `.Designer.cs`)

```markdown
### TASK: Implement Dashboard KPI Summaries & Multi-Criteria Reporting (Member 8)

Build the executive dashboard and comprehensive reporting engine executing read-only aggregate queries across all 12 tables.

#### 1. DTOs (`Models/ReportModels.cs`):
- `DashboardMetrics`: `TotalPatients`, `TodayAppointments`, `CompletedAppointmentsToday`, `AvailableDoctorsCount`, `LowStockMedicinesCount`, `PendingBillsCount`, `TodayRevenue`.
- `AppointmentReportItem`: `AppointmentID`, `Date`, `Time`, `PatientName`, `DoctorName`, `DepartmentName`, `Status`.
- `StockReportItem`: `MedicineCode`, `MedicineName`, `Category`, `StockQuantity`, `ReorderLevel`, `UnitPrice`, `TotalValue`, `Status`.
- `RevenueReportItem`: `BillID`, `BillDate`, `PatientName`, `TotalAmount`, `PaidAmount`, `Balance`, `Status`.

#### 2. Data Access Layer (`Repositories/ReportRepository.cs`):
- Clean parameterized analytical queries:
  - `DashboardMetrics GetDashboardMetrics()`:
    Executes single optimized query or batch scalar queries to fetch today's KPI counts.
  - `List<AppointmentReportItem> GetAppointmentReport(DateTime fromDate, DateTime toDate, int? doctorId, string? status)`
  - `List<StockReportItem> GetMedicineStockReport(bool lowStockOnly, string? category)`
  - `List<RevenueReportItem> GetRevenueReport(DateTime fromDate, DateTime toDate, string? paymentStatus)`
  - `List<Patient> GetPatientRegistryReport(DateTime fromDate, DateTime toDate)`

#### 3. Business Logic Layer:
- `Services/DashboardService.cs`: Returns `DashboardMetrics`. Formats alerts for low stock items and critical metrics.
- `Services/ReportService.cs`: Validates date ranges (from $\le$ to). Prepares tabular data and summary statistics (e.g., Total Revenue, Total Outstanding).

#### 4. Presentation Layer:
- `Forms/DashboardControl.cs` (or embedded in `MainForm`):
  - Visual KPI Summary Cards:
    - 👥 Total Registered Patients
    - 📅 Today's Appointments (Scheduled vs. Completed)
    - 🩺 Active On-Duty Doctors
    - ⚠️ Low Stock Alerts (clickable badge)
    - 💳 Today's Collected Revenue (Rs.)
  - Quick DataGridView: Today's pending appointments for immediate action.
- `Forms/ReportsForm.cs`:
  - Tabbed or Dropdown Report Selector:
    1. Appointment Summary Report
    2. Doctor Workload Report
    3. Medicine Stock & Reorder Report
    4. Hospital Billing & Revenue Report
    5. Patient Consultation History Report
  - Filter controls: `dtpStartDate`, `dtpEndDate`, `cmbDoctorFilter`, `cmbStatusFilter`, `btnGenerateReport`, `btnPrintExport`.
  - DataGridView `dgvReportResults` formatted with proper currencies, dates, and calculated totals footer.
```

---

## 📦 Prompt 10: Module 9 – Master Navigation Shell, Role Access & System Integration
**Assigned Responsibility:** Member 9 (Integration Coordinator)  
**File Outputs:**
- `Forms/MainForm.cs` (and `.Designer.cs`)
- `Program.cs` (Application Entry Point)
- `App.config` / DB Configuration
- Integration Testing & End-to-End Workflow Verification

```markdown
### TASK: Implement Master Navigation Shell, Role-Based Access Enforcement & System Integration (Member 9)

Integrate all 8 individual modules into one cohesive, single desktop application with modern Windows Forms styling, role-based navigation, and global error handling.

#### 1. Master Application Shell (`Forms/MainForm.cs`):
- Modern hospital layout:
  - Top header banner: Hospital Logo, "MediCare Hospital Management System", Logged-in User Full Name, Role Badge (e.g. `[DOCTOR]`), and `btnLogout`.
  - Left navigation sidebar:
    - 📊 Dashboard
    - 👥 Patients (Receptionist, Admin, Doctor, Cashier)
    - 🩺 Doctors & Departments (Admin, Receptionist)
    - 📅 Appointments (Receptionist, Doctor, Admin)
    - 📝 Consultations (Doctor, Admin)
    - 💊 Prescriptions & Pharmacy (Doctor, Pharmacist, Admin)
    - 💳 Billing & Cashier (Cashier, Admin)
    - 📈 Reports & Analytics (Role-specific)
    - ⚙️ User Administration (Admin only)
  - Main Content Panel: Hosts the active module child form/control seamlessly without messy MDI child windows (or uses managed MDI containers).
- Role-based Menu Access Controller:
  - In `MainForm_Load`, inspects `SessionManager.CurrentUserRole` and sets `.Visible` / `.Enabled` on navigation buttons matching the PRD User Access Matrix.

#### 2. Application Entry Point (`Program.cs`):
- Initialize Windows Forms styles:
  ```csharp
  ApplicationConfiguration.Initialize();
  ```
- Startup lifecycle:
  1. Check database connectivity using `DatabaseHelper.TestConnection()`. If unreachable, show helpful setup prompt:
     *"Cannot connect to SQL Server. Please verify SQL Server is running and connection string in Helpers/DatabaseHelper.cs is configured."*
  2. Launch `LoginForm` as modal dialog:
     ```csharp
     using (var loginForm = new LoginForm())
     {
         if (loginForm.ShowDialog() == DialogResult.OK && SessionManager.IsLoggedIn)
         {
             Application.Run(new MainForm());
         }
     }
     ```
  3. Global Exception Handler: Attach `Application.ThreadException` and `AppDomain.CurrentDomain.UnhandledException` to capture unexpected errors cleanly and log/display them without abrupt crashing.

#### 3. Complete End-to-End Integration Test Suite:
Provide a step-by-step test verification procedure that exercises the entire hospital lifecycle:
1. **Admin logs in** $\rightarrow$ Creates new Doctor user and attaches Doctor profile.
2. **Receptionist logs in** $\rightarrow$ Registers new Patient (`PAT999`) $\rightarrow$ Books appointment with Doctor at 10:00 AM $\rightarrow$ Verifies double-booking at 10:15 AM is rejected.
3. **Doctor logs in** $\rightarrow$ Views appointment on dashboard $\rightarrow$ Opens Consultation $\rightarrow$ Records symptoms & diagnosis $\rightarrow$ Saves consultation (auto-completing appointment) $\rightarrow$ Prescribes 2 medicines.
4. **Pharmacist logs in** $\rightarrow$ Opens Pharmacy Dispense screen $\rightarrow$ Verifies prescribed medicines $\rightarrow$ Clicks Dispense (verifying stock is deducted in database).
5. **Cashier logs in** $\rightarrow$ Opens Billing $\rightarrow$ Generates Bill for consultation & medicines $\rightarrow$ Records payment $\rightarrow$ Verifies outstanding balance reaches 0 and status updates to 'Paid'.
6. **Administrator / Receptionist** $\rightarrow$ Checks Reports & Dashboard $\rightarrow$ Verifies financial and operational metrics match the transactions.
```

---

## 🧪 Verification & Acceptance Checklist

Before submitting or demoing the coursework, run through this final checklist:

- [ ] **All 12 Database Tables Created**: Run `SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'` (must equal 12).
- [ ] **Zero SQL Concatenation**: Codebase search for `+ txt` or `+ cmb` in repository files shows 0 occurrences. All queries use `@parameters`.
- [ ] **Layered Separation Enforced**:
  - No `System.Data.SqlClient` or `Microsoft.Data.SqlClient` imports in any `Forms/*.cs` files.
  - All Form events call `Services`, and `Services` call `Repositories`.
- [ ] **Role Permissions Enforced**:
  - Logging in as `receptionist1` hides User Management and Clinical Consultation editing.
  - Logging in as `cashier1` hides Prescription creation and Doctor Management.
- [ ] **Exception Handling**:
  - Invalid database credentials or disconnected network cable displays a clean error dialog instead of crashing with unhandled exception.
  - Inputting letters into numeric fields (e.g. Payment Amount, Unit Price) triggers validation error providers.
- [ ] **Git Contribution**:
  - Each of the 9 members has committed their assigned classes, forms, and tests to the GitHub repository.

---
*End of Master Development Prompts Document.*
