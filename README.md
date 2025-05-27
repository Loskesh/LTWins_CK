# HR Management System

## Overview
The HR Management System is a WinForms-based application designed to manage employee records, departments, attendance, leave, payroll, and roles within an organization. It follows a layered architecture and utilizes object-oriented programming principles.

## Links
- **Sample UI**: [Live Demo](https://v0-hr-management-system-iota.vercel.app/)
- **GitHub Repository**: [HRMS Repo](https://github.com/tadyuh76/HRMS/)
- **Class Diagram**: [View Class Diagram](https://drive.google.com/file/d/1CUEI8IbjBwlp0hYt2TSFOpkpvLEALc8i/view?usp=sharing)

## Features
### 1. Project Foundation & System Architecture
- Base application architecture
- Role-based navigation and authentication
- Design patterns implementation (Factory, Event-Delegate, Repository)
- Serialization and File I/O utilities

### 2. Employee Management
- Employee profile management (personal details, employment details, status tracking)
- Position & salary management
- Employee-department relationships
- Role-based access control

### 3. Department Management
- Department structure and operations
- Budget tracking and employee capacity
- Department statistics and reporting

### 4. Attendance & Leave Management
- Clock-in/out system with status tracking
- Leave request and approval workflow
- Attendance and leave reports

### 5. Payroll Management
- Salary calculation engine
- Payslip generation
- Department-wise payroll reports

### 6. Employee Role Module
- Self-service features (profile, attendance, leave, payslips)
- Role-based permissions and access control

## Project Structure
```
HRManagementSystem/
├── Program.cs                      # Entry point
├── MainForm.cs                     # Main application window
├── HRManagementSystem.csproj       # Project file
│
├── Forms/                          # Presentation Layer
│   ├── EmployeeForms/              # Employee management forms
│   ├── DepartmentForms/            # Department management forms
│   ├── AttendanceForms/            # Attendance management forms
│   ├── LeaveForms/                 # Leave management forms
│   ├── PayrollForms/               # Payroll management forms
│   └── Common/                     # Shared UI components
│
├── Core/                           # Core Domain Layer
│   ├── Models/                     # Domain models
│   ├── Factory/                    # Factory Pattern Implementation
│   ├── Events/                     # Events and Delegates
│   ├── Exceptions/                 # Custom Exceptions
│   ├── ColorPalette.cs
│   ├── Enums.cs
│   ├── EventArgs.cs
│   └── Interfaces.cs
│
├── Services/                       # Service Layer (Business Logic)
│
├── Storage/                        # Storage (Repository Pattern)
│
├── Utils/                          # Utilities
│
└── Data/                           # Data storage (JSON files)
```

## Key Developers
| Developer | Responsibilities |
|-----------|-----------------|
| **Thanh Vy** | Employee & Department Management |
| **Mai Anh** | Attendance, Leave & Employee Role Modules |
| **Tu Trinh** | Payroll & Employee Role Modules |
| **Dat Huy** | Foundation & Architecture |

## General Requirements
- WinForms UI with a simple menu
- File-based data storage using serialization
- Object-Oriented Programming (Encapsulation, Abstraction, Inheritance, Polymorphism)
- No LINQ or Lambda expressions
- Minimum of 10 object classes
- Clear task assignments with all members contributing

## How to Run
1. Clone the repository:
   ```sh
   git clone https://github.com/tadyuh76/OOP---HRMS.git
   ```
2. Open the project in Visual Studio.
3. Build and run the solution.

## License
This project is for educational purposes and does not require a license.
# LTWins_CK
