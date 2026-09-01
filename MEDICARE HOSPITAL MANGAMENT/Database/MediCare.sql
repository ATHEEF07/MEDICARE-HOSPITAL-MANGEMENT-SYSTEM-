-- ============================================================================
-- MediCare Hospital Management System
-- Master Database DDL & Seed Data Script
-- Version: 1.0
-- Target: Microsoft SQL Server 2019 / 2022 / Express / LocalDB
-- ============================================================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MediCareDB')
BEGIN
    CREATE DATABASE MediCareDB;
    PRINT 'Database MediCareDB created successfully.';
END
GO

USE MediCareDB;
GO

-- ============================================================================
-- 1. DROP EXISTING TABLES (Reverse Dependency Order for clean re-run)
-- ============================================================================
IF OBJECT_ID('dbo.Payments', 'U') IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID('dbo.Bills', 'U') IS NOT NULL DROP TABLE dbo.Bills;
IF OBJECT_ID('dbo.PrescriptionItems', 'U') IS NOT NULL DROP TABLE dbo.PrescriptionItems;
IF OBJECT_ID('dbo.Prescriptions', 'U') IS NOT NULL DROP TABLE dbo.Prescriptions;
IF OBJECT_ID('dbo.Medicines', 'U') IS NOT NULL DROP TABLE dbo.Medicines;
IF OBJECT_ID('dbo.MedicalRecords', 'U') IS NOT NULL DROP TABLE dbo.MedicalRecords;
IF OBJECT_ID('dbo.Appointments', 'U') IS NOT NULL DROP TABLE dbo.Appointments;
IF OBJECT_ID('dbo.Patients', 'U') IS NOT NULL DROP TABLE dbo.Patients;
IF OBJECT_ID('dbo.Doctors', 'U') IS NOT NULL DROP TABLE dbo.Doctors;
IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL DROP TABLE dbo.Departments;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;
GO

-- ============================================================================
-- 2. CREATE 12 CORE RELATIONAL TABLES
-- ============================================================================

-- Table 1: Roles
CREATE TABLE dbo.Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(30) NOT NULL UNIQUE,
    Description VARCHAR(150) NULL
);
GO

-- Table 2: Users
CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FullName VARCHAR(100) NOT NULL,
    RoleID INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES dbo.Roles(RoleID)
);
GO

-- Table 3: Departments
CREATE TABLE dbo.Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(200) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- Table 4: Doctors
CREATE TABLE dbo.Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL UNIQUE,
    DepartmentID INT NOT NULL,
    DoctorCode VARCHAR(20) NOT NULL UNIQUE,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Specialization VARCHAR(100) NULL,
    Phone VARCHAR(20) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Doctors_Users FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_Doctors_Departments FOREIGN KEY (DepartmentID) REFERENCES dbo.Departments(DepartmentID)
);
GO

-- Table 5: Patients
CREATE TABLE dbo.Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    PatientCode VARCHAR(20) NOT NULL UNIQUE,
    NIC VARCHAR(20) NULL UNIQUE,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(20) NOT NULL,
    BloodGroup VARCHAR(5) NULL,
    Phone VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NULL,
    Address VARCHAR(250) NULL,
    EmergencyContactName VARCHAR(100) NULL,
    EmergencyContactPhone VARCHAR(20) NULL,
    RegisteredAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- Table 6: Appointments
CREATE TABLE dbo.Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    Reason VARCHAR(250) NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Scheduled', -- 'Scheduled', 'Completed', 'Cancelled', 'NoShow'
    Notes VARCHAR(500) NULL,
    CreatedByUserID INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID),
    CONSTRAINT FK_Appointments_CreatedByUser FOREIGN KEY (CreatedByUserID) REFERENCES dbo.Users(UserID),
    CONSTRAINT CHK_Appointments_TimeOrder CHECK (EndTime > StartTime)
);
GO

-- Table 7: MedicalRecords
CREATE TABLE dbo.MedicalRecords (
    MedicalRecordID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentID INT NULL,
    VisitDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Symptoms VARCHAR(1000) NULL,
    Diagnosis VARCHAR(1000) NULL,
    Treatment VARCHAR(1500) NULL,
    Notes VARCHAR(1500) NULL,
    CONSTRAINT FK_MedicalRecords_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_MedicalRecords_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID),
    CONSTRAINT FK_MedicalRecords_Appointments FOREIGN KEY (AppointmentID) REFERENCES dbo.Appointments(AppointmentID)
);
GO

-- Table 8: Medicines
CREATE TABLE dbo.Medicines (
    MedicineID INT IDENTITY(1,1) PRIMARY KEY,
    MedicineCode VARCHAR(20) NOT NULL UNIQUE,
    MedicineName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NULL,
    Unit VARCHAR(20) NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    ReorderLevel INT NOT NULL DEFAULT 10,
    ExpiryDate DATE NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT CHK_Medicines_UnitPrice CHECK (UnitPrice >= 0),
    CONSTRAINT CHK_Medicines_StockQuantity CHECK (StockQuantity >= 0),
    CONSTRAINT CHK_Medicines_ReorderLevel CHECK (ReorderLevel >= 0)
);
GO

-- Table 9: Prescriptions
CREATE TABLE dbo.Prescriptions (
    PrescriptionID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    MedicalRecordID INT NULL,
    PrescriptionDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Status VARCHAR(20) NOT NULL DEFAULT 'Active', -- 'Active', 'Dispensed', 'Cancelled'
    Notes VARCHAR(500) NULL,
    CONSTRAINT FK_Prescriptions_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_Prescriptions_Doctors FOREIGN KEY (DoctorID) REFERENCES dbo.Doctors(DoctorID),
    CONSTRAINT FK_Prescriptions_MedicalRecords FOREIGN KEY (MedicalRecordID) REFERENCES dbo.MedicalRecords(MedicalRecordID)
);
GO

-- Table 10: PrescriptionItems
CREATE TABLE dbo.PrescriptionItems (
    PrescriptionItemID INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionID INT NOT NULL,
    MedicineID INT NOT NULL,
    Quantity INT NOT NULL,
    Dosage VARCHAR(100) NOT NULL,
    Frequency VARCHAR(100) NOT NULL,
    DurationDays INT NOT NULL,
    Instructions VARCHAR(250) NULL,
    CONSTRAINT FK_PrescriptionItems_Prescriptions FOREIGN KEY (PrescriptionID) REFERENCES dbo.Prescriptions(PrescriptionID) ON DELETE CASCADE,
    CONSTRAINT FK_PrescriptionItems_Medicines FOREIGN KEY (MedicineID) REFERENCES dbo.Medicines(MedicineID),
    CONSTRAINT CHK_PrescriptionItems_Quantity CHECK (Quantity > 0),
    CONSTRAINT CHK_PrescriptionItems_Duration CHECK (DurationDays > 0)
);
GO

-- Table 11: Bills
CREATE TABLE dbo.Bills (
    BillID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    AppointmentID INT NULL,
    BillDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Description VARCHAR(500) NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    Discount DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(10,2) NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending', -- 'Pending', 'PartiallyPaid', 'Paid', 'Cancelled'
    CONSTRAINT FK_Bills_Patients FOREIGN KEY (PatientID) REFERENCES dbo.Patients(PatientID),
    CONSTRAINT FK_Bills_Appointments FOREIGN KEY (AppointmentID) REFERENCES dbo.Appointments(AppointmentID),
    CONSTRAINT CHK_Bills_Subtotal CHECK (Subtotal >= 0),
    CONSTRAINT CHK_Bills_Discount CHECK (Discount >= 0),
    CONSTRAINT CHK_Bills_TotalAmount CHECK (TotalAmount >= 0)
);
GO

-- Table 12: Payments
CREATE TABLE dbo.Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    BillID INT NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(20) NOT NULL, -- 'Cash', 'Card', 'BankTransfer'
    ReferenceNo VARCHAR(50) NULL,
    ReceivedByUserID INT NOT NULL,
    CONSTRAINT FK_Payments_Bills FOREIGN KEY (BillID) REFERENCES dbo.Bills(BillID),
    CONSTRAINT FK_Payments_ReceivedByUser FOREIGN KEY (ReceivedByUserID) REFERENCES dbo.Users(UserID),
    CONSTRAINT CHK_Payments_Amount CHECK (Amount > 0)
);
GO

-- ============================================================================
-- 3. CREATE PERFORMANCE NON-CLUSTERED INDEXES
-- ============================================================================
CREATE NONCLUSTERED INDEX IX_Appointments_Doctor_Date_Time ON dbo.Appointments (DoctorID, AppointmentDate, StartTime);
CREATE NONCLUSTERED INDEX IX_Appointments_Patient ON dbo.Appointments (PatientID, AppointmentDate);
CREATE NONCLUSTERED INDEX IX_Patients_NIC ON dbo.Patients (NIC);
CREATE NONCLUSTERED INDEX IX_Patients_PatientCode ON dbo.Patients (PatientCode);
CREATE NONCLUSTERED INDEX IX_Medicines_Stock ON dbo.Medicines (StockQuantity, ReorderLevel);
CREATE NONCLUSTERED INDEX IX_Bills_Patient_Status ON dbo.Bills (PatientID, Status);
CREATE NONCLUSTERED INDEX IX_Prescriptions_Status ON dbo.Prescriptions (Status);
GO

-- ============================================================================
-- 4. REALISTIC DEVELOPMENT SEED DATA
-- ============================================================================

-- Roles
INSERT INTO dbo.Roles (RoleName, Description) VALUES
('Administrator', 'Full access to administration, staff, and reporting'),
('Receptionist', 'Patient registration, lookup, and appointment management'),
('Doctor', 'Patient consultation, clinical records, and prescription creation'),
('Pharmacist', 'Medicine stock management and prescription dispensing'),
('Cashier', 'Billing generation and payment processing');
GO

-- Users
-- Standard password for demo accounts:
-- admin -> Admin@123
-- dr_smith, dr_johnson -> Doctor@123
-- receptionist1 -> Recep@123
-- pharmacist1 -> Pharma@123
-- cashier1 -> Cashier@123
INSERT INTO dbo.Users (Username, PasswordHash, FullName, RoleID, IsActive) VALUES
('admin', 'WslyEDwEX4pZ1uIcxXeX+w==:30bLGzjhU7oWxM0MGp/S7TvS3MIj944TkXSf3B2XhNo=', 'System Administrator', 1, 1),
('receptionist1', 'joCojhUKRf/fruJEj0OITA==:90Q9VWiwLNmEBr/WhsAGgUVXwLyfDKBQNQBzvefhRXk=', 'Sarah Jenkins', 2, 1),
('dr_smith', 'kIHzXV0difMikRByLyOTIw==:EU2OG3nFpVL54HSZXktzZozPeb0SjccJjKxZn5min0E=', 'Dr. Arthur Smith', 3, 1),
('dr_johnson', 'Vow4am5cSIq918cxoaDSoQ==:CZnhlKk6xMKV7VBBdA4sbt7ro7eF4MZT1SzJ4gxKfL8=', 'Dr. Emily Johnson', 3, 1),
('pharmacist1', 'kyzs7ECLdaurty+tXowKYg==:AzLDdqE5eE2jTDR45oa/5/LK7X1DOs3BvBjd98kSuCM=', 'David Miller', 4, 1),
('cashier1', 'FkgketLcU/lST0dup3cJcw==:dICLlvHDq5Pqj0zb5EZyczWh62uFOVzdKuvZ2VgmCIE=', 'Jessica Taylor', 5, 1);
GO

-- Departments
INSERT INTO dbo.Departments (DepartmentName, Description, IsActive) VALUES
('General Medicine', 'Primary consultation and routine healthcare', 1),
('Pediatrics', 'Specialized care for infants and children', 1),
('Cardiology', 'Heart and cardiovascular care', 1),
('Pharmacy', 'Hospital dispensary and pharmaceutical supplies', 1),
('Laboratory', 'Diagnostic testing and pathology', 1);
GO

-- Doctors
INSERT INTO dbo.Doctors (UserID, DepartmentID, DoctorCode, FirstName, LastName, Specialization, Phone, IsActive) VALUES
(3, 1, 'DOC001', 'Arthur', 'Smith', 'General Physician', '0771234567', 1),
(4, 2, 'DOC002', 'Emily', 'Johnson', 'Pediatrician', '0772345678', 1);
GO

-- Patients (10 realistic fictional patients)
INSERT INTO dbo.Patients (PatientCode, NIC, FirstName, LastName, DateOfBirth, Gender, BloodGroup, Phone, Email, Address, EmergencyContactName, EmergencyContactPhone) VALUES
('PAT001', '199012345678', 'Kamal', 'Perera', '1990-05-14', 'Male', 'O+', '0773456789', 'kamal.perera@email.com', '12 Galle Road, Colombo 03', 'Sunil Perera', '0773333333'),
('PAT002', '198565432109', 'Nimali', 'Fernando', '1985-08-22', 'Female', 'A+', '0714567890', 'nimali.f@email.com', '45 Kandy Road, Kiribathgoda', 'Gamini Fernando', '0714444444'),
('PAT003', '199578901234', 'Mohamed', 'Rizwan', '1995-11-03', 'Male', 'B+', '0765678901', 'm.rizwan@email.com', '78 Main Street, Negombo', 'Fathima Rizwan', '0765555555'),
('PAT004', '200112340001', 'Sanduni', 'Silva', '2001-02-18', 'Female', 'AB+', '0756789012', 'sanduni.silva@email.com', '34 Flower Road, Colombo 07', 'Chitra Silva', '0756666666'),
('PAT005', '197823459876', 'Rohan', 'De Silva', '1978-09-30', 'Male', 'O-', '0787890123', 'rohan.desilva@email.com', '89 High Level Road, Nugegoda', 'Malkanthi De Silva', '0787777777'),
('PAT006', '199289012345', 'Anoma', 'Wickramasinghe', '1992-12-10', 'Female', 'A-', '0708901234', 'anoma.w@email.com', '56 Peradeniya Road, Kandy', 'Sarath Wickramasinghe', '0708888888'),
('PAT007', '198834561234', 'Dinesh', 'Gunawardena', '1988-04-05', 'Male', 'B-', '0729012345', 'dinesh.g@email.com', '101 Station Road, Dehiwala', 'Priyantha Gunawardena', '0729999999'),
('PAT008', '200345678901', 'Kasun', 'Bandara', '2003-07-25', 'Male', 'O+', '0740123456', 'kasun.b@email.com', '23 Temple Road, Kurunegala', 'Wasantha Bandara', '0740000000'),
('PAT009', '199856789012', 'Tharushi', 'Jayawardena', '1998-03-12', 'Female', 'AB-', '0771122334', 'tharushi.j@email.com', '67 Lake Road, Rajagiriya', 'Manel Jayawardena', '0771111111'),
('PAT010', '197567890123', 'Sunil', 'Ranasinghe', '1975-10-08', 'Male', 'A+', '0712233445', 'sunil.r@email.com', '15 Baseline Road, Dematagoda', 'Kusuma Ranasinghe', '0712222222');
GO

-- Medicines (15 realistic core medicines)
INSERT INTO dbo.Medicines (MedicineCode, MedicineName, Category, Unit, UnitPrice, StockQuantity, ReorderLevel, ExpiryDate) VALUES
('MED001', 'Paracetamol 500mg', 'Pain Relief', 'Tablet', 5.00, 1500, 200, '2027-12-31'),
('MED002', 'Amoxicillin 500mg', 'Antibiotic', 'Capsule', 25.00, 800, 100, '2027-06-30'),
('MED003', 'Cetirizine 10mg', 'Antihistamine', 'Tablet', 10.00, 600, 100, '2028-01-15'),
('MED004', 'Metformin 500mg', 'Antidiabetic', 'Tablet', 8.50, 950, 150, '2027-09-30'),
('MED005', 'Omeprazole 20mg', 'Antacid / PPI', 'Capsule', 18.00, 700, 100, '2027-11-20'),
('MED006', 'Atorvastatin 20mg', 'Cholesterol', 'Tablet', 30.00, 450, 80, '2027-08-15'),
('MED007', 'Ibuprofen 400mg', 'NSAID / Anti-inflammatory', 'Tablet', 12.00, 500, 100, '2028-03-10'),
('MED008', 'Salbutamol Inhaler 100mcg', 'Respiratory', 'Inhaler', 650.00, 85, 20, '2027-05-30'),
('MED009', 'Azithromycin 500mg', 'Antibiotic', 'Tablet', 75.00, 300, 50, '2027-10-15'),
('MED010', 'Ciprofloxacin 500mg', 'Antibiotic', 'Tablet', 35.00, 250, 50, '2027-07-20'),
('MED011', 'Losartan Potassium 50mg', 'Antihypertensive', 'Tablet', 22.00, 650, 100, '2028-02-28'),
('MED012', 'Vitamin C 500mg', 'Supplement', 'Tablet', 6.00, 1200, 150, '2028-06-30'),
('MED013', 'Oral Rehydration Salts (ORS)', 'Electrolytes', 'Sachet', 45.00, 400, 60, '2028-04-15'),
('MED014', 'Insulin Glargine 100IU/ml', 'Antidiabetic', 'Vial', 1850.00, 15, 10, '2026-12-31'), -- Low stock sample
('MED015', 'Pantoprazole 40mg', 'Antacid / PPI', 'Tablet', 20.00, 8, 20, '2027-04-30'); -- Low stock sample (below reorder)
GO

-- Sample Appointments
INSERT INTO dbo.Appointments (PatientID, DoctorID, AppointmentDate, StartTime, EndTime, Reason, Status, Notes, CreatedByUserID) VALUES
(1, 1, CAST(GETDATE() AS DATE), '09:00:00', '09:30:00', 'Persistent cough and mild fever', 'Completed', 'Patient arrived on time', 2),
(2, 1, CAST(GETDATE() AS DATE), '09:30:00', '10:00:00', 'Routine blood pressure checkup', 'Scheduled', 'First follow-up', 2),
(3, 2, CAST(GETDATE() AS DATE), '10:00:00', '10:30:00', 'Child seasonal allergy consultation', 'Scheduled', 'Accompanied by mother', 2),
(4, 1, DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), '11:00:00', '11:30:00', 'General body weakness', 'Scheduled', 'Requested morning slot', 2);
GO

-- Sample Medical Record
INSERT INTO dbo.MedicalRecords (PatientID, DoctorID, AppointmentID, VisitDate, Symptoms, Diagnosis, Treatment, Notes) VALUES
(1, 1, 1, SYSDATETIME(), 'Fever 38.5C, dry cough for 3 days, body aches', 'Upper Respiratory Tract Infection', 'Prescribed paracetamol and hydration rest', 'Advised to return if symptoms persist after 5 days');
GO

-- Sample Prescription
INSERT INTO dbo.Prescriptions (PatientID, DoctorID, MedicalRecordID, PrescriptionDate, Status, Notes) VALUES
(1, 1, 1, SYSDATETIME(), 'Active', 'Take medications after meals');
GO

-- Sample Prescription Items
INSERT INTO dbo.PrescriptionItems (PrescriptionID, MedicineID, Quantity, Dosage, Frequency, DurationDays, Instructions) VALUES
(1, 1, 15, '500mg (1 tablet)', 'TDS (3 times daily)', 5, 'Take after meals for fever'),
(1, 3, 5, '10mg (1 tablet)', 'Nocte (At night)', 5, 'Take before bed for cough/allergy');
GO

-- Sample Bill
INSERT INTO dbo.Bills (PatientID, AppointmentID, BillDate, Description, Subtotal, Discount, TotalAmount, Status) VALUES
(1, 1, SYSDATETIME(), 'Doctor Consultation & Basic Registration Fee', 2500.00, 200.00, 2300.00, 'PartiallyPaid');
GO

-- Sample Payment
INSERT INTO dbo.Payments (BillID, PaymentDate, Amount, PaymentMethod, ReferenceNo, ReceivedByUserID) VALUES
(1, SYSDATETIME(), 1500.00, 'Cash', 'RCP-10001', 6);
GO

PRINT '============================================================================';
PRINT 'MediCareDB setup completed successfully with all 12 tables and seed data!';
PRINT '============================================================================';
GO
