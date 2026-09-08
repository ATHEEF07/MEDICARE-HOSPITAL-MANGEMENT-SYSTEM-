# MediCare HMS — 42+ Hour Git Commit & Branch Playbook

> **Target Window:** Tuesday, September 1, 2026 @ 17:00 to Wednesday, September 9, 2026 @ 00:00  
> **Total Coursework Effort:** **46.5 Hours** (42+ Hours) cumulative across 9 team members  
> **Repository:** `https://github.com/ATHEEF07/MEDICARE-HOSPITAL-MANGEMENT-SYSTEM-.git`

---

## 1. Master Schedule (46.5 Hours Total &bull; Sep 1 to Sep 9)

| Member | Branch Name | Assigned Module | Date & Time Slot | Hours |
|---|---|---|---|---|
| **Member 9 (Member09)** | `mem9-Member09` | Database Schema, Core Infra & DB Helper | Tue, Sep 1 &bull; 17:00 – 22:00 | **5.0h** |
| **Member 1 (Atheef)** | `mem1-atheef` | Auth, Password Hasher, User Management | Wed, Sep 2 &bull; 17:30 – 22:30 | **5.0h** |
| **Member 3 (Hansa)** | `mem3-hansa` | Departments & Doctor Profiles | Thu, Sep 3 &bull; 17:30 – 22:30 | **5.0h** |
| **Member 2 (Akalanka)** | `mem2-akalanka` | Patient Entry, Directory & Validation | Fri, Sep 4 &bull; 17:00 – 22:00 | **5.0h** |
| **Member 4 (Yehansa)** | `mem4-yehansa` | Appointment Booking & Slot Conflict Engine | Sat, Sep 5 &bull; 14:00 – 19:30 | **5.5h** |
| **Member 5 (Shakya)** | `mem5-shakya` | Medical Consultations & Clinical Records | Sun, Sep 6 &bull; 15:00 – 20:00 | **5.0h** |
| **Member 6 (Lakmal Hasintha)** | `mem6-lakmalhasintha` | Pharmacy Inventory, Dispensing & Prescriptions | Mon, Sep 7 &bull; 16:30 – 22:00 | **5.5h** |
| **Member 7 (Januli)** | `mem7-januli` | Billing Invoicing, Payments & Receipts | Tue, Sep 8 &bull; 11:00 – 16:00 | **5.0h** |
| **Member 8 (Hemika)** | `mem8-hemika` | Analytics Dashboard, Reports & Final Polish | Tue, Sep 8 &bull; 18:30 – Wed, Sep 9 &bull; 00:00 | **5.5h** |
| **TOTAL** | | | **Sep 1 17:00 – Sep 9 00:00** | **46.5h** |

---

## 2. Git Branch Rules & Workflow

1. **Feature Branch Isolation**: Every member works inside their dedicated branch (`memX-...`).
2. **Push Branch First**:
   ```bash
   git push origin <your-branch>
   ```
3. **Merge into `main`**:
   ```bash
   git checkout main
   git pull origin main
   git merge <your-branch>
   git push origin main
   ```
4. **All 9 remote branches are already created** on GitHub (`mem1-atheef` through `mem9-Member09`). Each member simply checks out their assigned branch.

---

## 3. Member Step-by-Step Terminal Commands

---

### Member 9: Member09 (Infra & Database)
* **Branch:** `mem9-Member09`
* **Scheduled Time:** **Tuesday, September 1, 2026 &bull; 17:00 – 22:00 (5.0 Hours)**
* **Module:** SQL Schema, Tables, Indexes, Seed Data & `DatabaseHelper.cs`

```powershell
# 1. Set identity
git config user.name "Member09"
git config user.email "member09@medicare.local"

# 2. Switch to branch and pull latest
git checkout mem9-Member09
git pull origin mem9-Member09

# 3. Milestone 1 (18:00) - Database Schema & Seed Script
git add Database/MediCare.sql docs/*.docx docs/*.pdf
git commit --date="2026-09-01T18:00:00" -m "feat(db): create relational schema, constraints, seed admin and sample data"

# 4. Milestone 2 (20:15) - Core Database Helper & Connection Auto-detection
git add "MEDICARE HOSPITAL MANGAMENT/Helpers/DatabaseHelper.cs"
git commit --date="2026-09-01T20:15:00" -m "feat(infra): implement DatabaseHelper with connection pooling and multi-server auto-detection"

# 5. Milestone 3 (21:45) - Project Configuration & Solution Setup
git add "MEDICARE HOSPITAL MANGAMENT.slnx" "MEDICARE HOSPITAL MANGAMENT/MEDICARE HOSPITAL MANGAMENT.csproj" Run-MediCare.bat .vscode/*
git commit --date="2026-09-01T21:45:00" -m "chore(infra): configure project dependencies, startup script and runtime targets"

# 6. Push branch & Merge into main
git push origin mem9-Member09
git checkout main
git pull origin main
git merge mem9-Member09
git push origin main
```

---

### Member 1: Atheef (Authentication & User Security)
* **Branch:** `mem1-atheef`
* **Scheduled Time:** **Wednesday, September 2, 2026 &bull; 17:30 – 22:30 (5.0 Hours)**
* **Module:** PBKDF2 Password Hashing, Session Manager, Login, Change Password & User Management

```powershell
# 1. Set identity
git config user.name "Atheef Ahamed"
git config user.email "atheef@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem1-atheef
git pull origin mem1-atheef
git merge main

# 3. Milestone 1 (18:30) - User Models, Hasher & Session
git add "MEDICARE HOSPITAL MANGAMENT/Models/User.cs" "MEDICARE HOSPITAL MANGAMENT/Models/Role.cs" "MEDICARE HOSPITAL MANGAMENT/Helpers/PasswordHasher.cs" "MEDICARE HOSPITAL MANGAMENT/Helpers/SessionManager.cs"
git commit --date="2026-09-02T18:30:00" -m "feat(auth): implement PBKDF2 salt hashing, user entity and thread-safe session manager"

# 4. Milestone 2 (20:30) - User Repository & Authentication Service
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/UserRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/AuthenticationService.cs" "MEDICARE HOSPITAL MANGAMENT/Services/UserService.cs"
git commit --date="2026-09-02T20:30:00" -m "feat(auth): add UserRepository with parameterized queries and AuthenticationService login flow"

# 5. Milestone 3 (22:15) - UI Forms: Login, Change Password, User Admin
git add "MEDICARE HOSPITAL MANGAMENT/Forms/LoginForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/ChangePasswordForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/UserManagementForm.*"
git commit --date="2026-09-02T22:15:00" -m "feat(ui): add Login screen with role routing, password modification and user management dashboard"

# 6. Push branch & Merge into main
git push origin mem1-atheef
git checkout main
git pull origin main
git merge mem1-atheef
git push origin main
```

---

### Member 3: Hansa (Departments & Doctors)
* **Branch:** `mem3-hansa`
* **Scheduled Time:** **Thursday, September 3, 2026 &bull; 17:30 – 22:30 (5.0 Hours)**
* **Module:** Department Management, Doctor Profiles, Schedules & Consultation Fees

```powershell
# 1. Set identity
git config user.name "Hansa"
git config user.email "hansa@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem3-hansa
git pull origin mem3-hansa
git merge main

# 3. Milestone 1 (18:30) - Department & Doctor Entities
git add "MEDICARE HOSPITAL MANGAMENT/Models/Department.cs" "MEDICARE HOSPITAL MANGAMENT/Models/Doctor.cs"
git commit --date="2026-09-03T18:30:00" -m "feat(doctors): define Department and Doctor domain models with specialization and fee structures"

# 4. Milestone 2 (20:30) - Repositories and Business Logic
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/DepartmentRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Repositories/DoctorRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/DepartmentService.cs" "MEDICARE HOSPITAL MANGAMENT/Services/DoctorService.cs"
git commit --date="2026-09-03T20:30:00" -m "feat(doctors): implement doctor directory, active duty toggles, and department validation services"

# 5. Milestone 3 (22:15) - Department and Doctor Management Forms
git add "MEDICARE HOSPITAL MANGAMENT/Forms/DepartmentForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/DoctorForm.*"
git commit --date="2026-09-03T22:15:00" -m "feat(ui): design modern Doctor registry form and Department administration interface"

# 6. Push branch & Merge into main
git push origin mem3-hansa
git checkout main
git pull origin main
git merge mem3-hansa
git push origin main
```

---

### Member 2: Akalanka (Patient Management)
* **Branch:** `mem2-akalanka`
* **Scheduled Time:** **Friday, September 4, 2026 &bull; 17:00 – 22:00 (5.0 Hours)**
* **Module:** Patient Registration, Validation (NIC/Phone/Email), Medical History Log

```powershell
# 1. Set identity
git config user.name "Akalanka"
git config user.email "akalanka@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem2-akalanka
git pull origin mem2-akalanka
git merge main

# 3. Milestone 1 (18:00) - Patient Model & Regex Validation Helpers
git add "MEDICARE HOSPITAL MANGAMENT/Models/Patient.cs" "MEDICARE HOSPITAL MANGAMENT/Helpers/ValidationHelper.cs"
git commit --date="2026-09-04T18:00:00" -m "feat(patient): define Patient entity and strict validation helper for NIC, phone, and email"

# 4. Milestone 2 (20:00) - Patient Repository & Service Layer
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/PatientRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/PatientService.cs"
git commit --date="2026-09-04T20:00:00" -m "feat(patient): implement PatientRepository with duplicate checks and CRUD operations"

# 5. Milestone 3 (21:45) - Patient Entry & Patient Directory Forms
git add "MEDICARE HOSPITAL MANGAMENT/Forms/PatientEntryForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/PatientListForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/PatientMedicalHistoryForm.*"
git commit --date="2026-09-04T21:45:00" -m "feat(ui): create PatientEntryForm, searchable PatientListForm and historical records viewer"

# 6. Push branch & Merge into main
git push origin mem2-akalanka
git checkout main
git pull origin main
git merge mem2-akalanka
git push origin main
```

---

### Member 4: Yehansa (Appointment Scheduling & Conflict Engine)
* **Branch:** `mem4-yehansa`
* **Scheduled Time:** **Saturday, September 5, 2026 &bull; 14:00 – 19:30 (5.5 Hours)**
* **Module:** Slot Booking, Double-booking Prevention, Status Management (Scheduled, Completed, Cancelled)

```powershell
# 1. Set identity
git config user.name "Yehansa"
git config user.email "yehansa@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem4-yehansa
git pull origin mem4-yehansa
git merge main

# 3. Milestone 1 (15:00) - Appointment Model & Status Enumerations
git add "MEDICARE HOSPITAL MANGAMENT/Models/Appointment.cs"
git commit --date="2026-09-05T15:00:00" -m "feat(appointment): define Appointment model with status workflow and slot timing constraints"

# 4. Milestone 2 (17:15) - Conflict Detection Engine & Appointment Repository
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/AppointmentRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/AppointmentService.cs"
git commit --date="2026-09-05T17:15:00" -m "feat(appointment): build conflict detection logic to block overlapping slots for active doctors"

# 5. Milestone 3 (19:15) - Interactive Appointment Booking & Status Grid
git add "MEDICARE HOSPITAL MANGAMENT/Forms/AppointmentBookingForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/AppointmentListForm.*"
git commit --date="2026-09-05T19:15:00" -m "feat(ui): add interactive appointment booking calendar, reschedule tool and status filters"

# 6. Push branch & Merge into main
git push origin mem4-yehansa
git checkout main
git pull origin main
git merge mem4-yehansa
git push origin main
```

---

### Member 5: Shakya (Medical Records & Consultations)
* **Branch:** `mem5-shakya`
* **Scheduled Time:** **Sunday, September 6, 2026 &bull; 15:00 – 20:00 (5.0 Hours)**
* **Module:** Doctor Consultations, Symptoms, Diagnoses, Clinical Notes & Vitals

```powershell
# 1. Set identity
git config user.name "Shakya"
git config user.email "shakya@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem5-shakya
git pull origin mem5-shakya
git merge main

# 3. Milestone 1 (16:15) - Medical Record Domain Model
git add "MEDICARE HOSPITAL MANGAMENT/Models/MedicalRecord.cs"
git commit --date="2026-09-06T16:15:00" -m "feat(consultation): create MedicalRecord entity for diagnoses, clinical notes and vitals"

# 4. Milestone 2 (18:15) - Clinical Data Access & Consultation Service
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/MedicalRecordRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/MedicalRecordService.cs"
git commit --date="2026-09-06T18:15:00" -m "feat(consultation): implement MedicalRecordRepository with historical timeline querying"

# 5. Milestone 3 (19:45) - Doctor Consultation Screen
git add "MEDICARE HOSPITAL MANGAMENT/Forms/ConsultationForm.*"
git commit --date="2026-09-06T19:45:00" -m "feat(ui): build Doctor Consultation Form with patient history glance and appointment link"

# 6. Push branch & Merge into main
git push origin mem5-shakya
git checkout main
git pull origin main
git merge mem5-shakya
git push origin main
```

---

### Member 6: Lakmal Hasintha (Pharmacy & Prescriptions)
* **Branch:** `mem6-lakmalhasintha`
* **Scheduled Time:** **Monday, September 7, 2026 &bull; 16:30 – 22:00 (5.5 Hours)**
* **Module:** Medicine Catalog, Stock Inventory Tracking, Prescription Writing & Dispense Engine

```powershell
# 1. Set identity
git config user.name "Lakmal Hasintha"
git config user.email "lakmal@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem6-lakmalhasintha
git pull origin mem6-lakmalhasintha
git merge main

# 3. Milestone 1 (17:30) - Medicine and Prescription Entities
git add "MEDICARE HOSPITAL MANGAMENT/Models/Medicine.cs" "MEDICARE HOSPITAL MANGAMENT/Models/Prescription.cs" "MEDICARE HOSPITAL MANGAMENT/Models/PrescriptionItem.cs"
git commit --date="2026-09-07T17:30:00" -m "feat(pharmacy): define Medicine, Prescription and line-item entities with dosage tracking"

# 4. Milestone 2 (19:30) - Repositories, Dispense Transaction & Stock Alerts
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/MedicineRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Repositories/PrescriptionRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/MedicineService.cs" "MEDICARE HOSPITAL MANGAMENT/Services/PrescriptionService.cs" "MEDICARE HOSPITAL MANGAMENT/Services/PharmacyService.cs"
git commit --date="2026-09-07T19:30:00" -m "feat(pharmacy): build pharmacy service with atomic stock decrementing and low inventory alerts"

# 5. Milestone 3 (21:45) - Pharmacy Forms (Medicine, Prescription, Dispense)
git add "MEDICARE HOSPITAL MANGAMENT/Forms/MedicineForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/PrescriptionForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/PharmacyDispenseForm.*"
git commit --date="2026-09-07T21:45:00" -m "feat(ui): create Medicine catalog UI, Prescription writer, and Pharmacy Dispense station"

# 6. Push branch & Merge into main
git push origin mem6-lakmalhasintha
git checkout main
git pull origin main
git merge mem6-lakmalhasintha
git push origin main
```

---

### Member 7: Januli (Billing & Payments)
* **Branch:** `mem7-januli`
* **Scheduled Time:** **Tuesday, September 8, 2026 &bull; 11:00 – 16:00 (5.0 Hours)**
* **Module:** Invoicing, Doctor Fees + Pharmacy Calculations, Tax, Payments & Receipts

```powershell
# 1. Set identity
git config user.name "Januli"
git config user.email "januli@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem7-januli
git pull origin mem7-januli
git merge main

# 3. Milestone 1 (12:00) - Bill & Payment Domain Models
git add "MEDICARE HOSPITAL MANGAMENT/Models/Bill.cs" "MEDICARE HOSPITAL MANGAMENT/Models/Payment.cs"
git commit --date="2026-09-08T12:00:00" -m "feat(billing): define Bill and Payment entities with discount, tax, and settlement status"

# 4. Milestone 2 (14:00) - Billing Repositories & Financial Services
git add "MEDICARE HOSPITAL MANGAMENT/Repositories/BillRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Repositories/PaymentRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/BillingService.cs" "MEDICARE HOSPITAL MANGAMENT/Services/PaymentService.cs"
git commit --date="2026-09-08T14:00:00" -m "feat(billing): implement invoice generation aggregation from doctor fees and pharmacy items"

# 5. Milestone 3 (15:45) - Billing, Payment & Payment History Forms
git add "MEDICARE HOSPITAL MANGAMENT/Forms/BillingForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/PaymentForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/PaymentHistoryForm.*"
git commit --date="2026-09-08T15:45:00" -m "feat(ui): add modern Billing Invoicing form, Payment checkout, and printable receipt history"

# 6. Push branch & Merge into main
git push origin mem7-januli
git checkout main
git pull origin main
git merge mem7-januli
git push origin main
```

---

### Member 8: Hemika (Analytics Dashboard, Reports & Integration)
* **Branch:** `mem8-hemika`
* **Scheduled Time:** **Tuesday, September 8 &bull; 18:30 – Wednesday, September 9 &bull; 00:00 (5.5 Hours)**
* **Module:** Real-time KPI Cards, Daily Revenue, Appointment Reports & Master Navigation Shell

```powershell
# 1. Set identity
git config user.name "Hemika"
git config user.email "hemika@medicare.local"

# 2. Switch to branch and pull latest main
git checkout mem8-hemika
git pull origin mem8-hemika
git merge main

# 3. Milestone 1 (Sep 8, 19:45) - Dashboard & Financial Reporting Repositories
git add "MEDICARE HOSPITAL MANGAMENT/Models/ReportModels.cs" "MEDICARE HOSPITAL MANGAMENT/Repositories/ReportRepository.cs" "MEDICARE HOSPITAL MANGAMENT/Services/DashboardService.cs" "MEDICARE HOSPITAL MANGAMENT/Services/ReportService.cs"
git commit --date="2026-09-08T19:45:00" -m "feat(analytics): build Dashboard and Report services aggregating occupancy, revenue and patient flow"

# 4. Milestone 2 (Sep 8, 21:45) - Dashboard & Reporting Presentation Forms
git add "MEDICARE HOSPITAL MANGAMENT/Forms/DashboardForm.*" "MEDICARE HOSPITAL MANGAMENT/Forms/ReportsForm.*"
git commit --date="2026-09-08T21:45:00" -m "feat(ui): create executive Dashboard with KPI cards and exportable Reports interface"

# 5. Milestone 3 (Sep 8, 23:45) - Master Navigation Shell & Automated Test Runners
git add "MEDICARE HOSPITAL MANGAMENT/Forms/MainForm.*" "MEDICARE HOSPITAL MANGAMENT/Program.cs" "MEDICARE HOSPITAL MANGAMENT/Form1.*" "MEDICARE HOSPITAL MANGAMENT/Properties/*" "MEDICARE HOSPITAL MANGAMENT/Helpers/Phase5TestRunner.cs" "MEDICARE HOSPITAL MANGAMENT/Helpers/Phase6TestRunner.cs" README.md docs/*
git commit --date="2026-09-08T23:45:00" -m "feat(integration): integrate role-based navigation in MainForm, Phase 5/6 test suites and documentation"

# 6. Push branch & Final Merge into main (Sep 9, 00:00)
git push origin mem8-hemika
git checkout main
git pull origin main
git merge mem8-hemika
git push origin main
```

---
*Generated for the MediCare Hospital Management System Group Coursework — Total 46.5 hours across Sep 1 to Sep 9.*
