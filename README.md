# HR Management System

## Overview
The HR Management System is a comprehensive WinForms-based application designed to manage employee records, departments, attendance, leave requests, payroll, and role-based access within an organization. The system follows a layered architecture and utilizes object-oriented programming principles with design patterns implementation.

## Links
- **Sample UI**: [Live Demo](https://v0-hr-management-system-iota.vercel.app/)
- **GitHub Repository**: [HRMS Repo](https://github.com/tadyuh76/HRMS/)
- **Class Diagram**: [View Class Diagram](https://drive.google.com/file/d/1CUEI8IbjBwlp0hYt2TSFOpkpvLEALc8i/view?usp=sharing)

## Features
### 1. Employee Management
- **Employee Profile Management**: Complete CRUD operations for employee records
- **Multi-type Employee Support**: Regular, Full-time, and Contract employees with different calculation methods
- **Employee Factory Pattern**: Dynamic employee creation based on type
- **Status Tracking**: Active, On Leave, Terminated, Suspended status management
- **Department Assignment**: Employee-department relationship management
- **Advanced Search & Filtering**: Search by name, ID, position, or department

### 2. Department Management
- **Department Structure**: Complete department hierarchy management
- **Budget Tracking**: Department budget allocation and utilization monitoring
- **Manager Assignment**: Department manager assignment and management
- **Employee Count Tracking**: Real-time employee count per department
- **Department Statistics**: Comprehensive reporting and analytics

### 3. Attendance & Leave Management
- **Clock-in/Clock-out System**: Real-time attendance tracking with automatic status detection
- **Leave Request Workflow**: Complete leave request submission and approval process
- **Multiple Leave Types**: Annual, Sick, Maternity, Paternity, Unpaid, Bereavement leave support
- **Attendance Analytics**: Monthly and daily attendance reports
- **Late Arrival Detection**: Automatic late arrival detection and reporting
- **Absence Management**: Automatic absent employee tracking for HR oversight

### 4. Payroll Management
- **Multi-type Salary Calculation**: Different calculation methods for Regular, Full-time, and Contract employees
- **Payroll Generation**: Automated payroll processing with allowances and deductions
- **Payslip Creation**: Professional payslip generation and management
- **Payroll Reports**: Comprehensive payroll analytics and reporting
- **Payment Status Tracking**: Track paid/unpaid payroll records
- **Salary Range Analytics**: Min, max, and average salary calculations

### 5. Role-Based Access Control
- **Admin Role**: Full system access with all management capabilities
- **Employee Role**: Self-service portal with limited access to personal information
- **Dynamic Role Switching**: Real-time role switching for testing and demonstration
- **Permission Management**: Feature-based access control system

### 6. Data Management & Storage
- **Dual Storage System**: SQL Server database with JSON file backup
- **Data Serialization**: Custom JSON serialization for employee types
- **File Management**: Centralized file management system
- **Data Integrity**: Validation and exception handling throughout the system

## Technical Architecture

### Project Structure
```
HRManagementSystem/
├── Program.cs                      # Application entry point
├── MainForm.cs                     # Main navigation interface
├── EmployeeSession.cs              # Session management
├── KetNoi.cs                       # Database connection
│
├── Core/                           # Core Domain Layer
│   ├── Models/                     # Domain models
│   │   ├── Person.cs               # Base person class
│   │   ├── Employee.cs             # Base employee class
│   │   ├── FullTimeEmployee.cs    # Full-time employee implementation
│   │   ├── ContractEmployee.cs    # Contract employee implementation
│   │   ├── Department.cs          # Department model
│   │   ├── Attendance.cs          # Attendance tracking model
│   │   ├── LeaveRequest.cs        # Leave request model
│   │   └── Payroll.cs             # Payroll calculation model
│   ├── Factory/
│   │   └── EmployeeFactory.cs     # Factory pattern for employee creation
│   ├── Exceptions/                # Custom exception handling
│   │   ├── HRSystemException.cs
│   │   ├── EntityNotFoundException.cs
│   │   └── ValidationException.cs
│   ├── ColorPalette.cs            # UI color scheme
│   ├── Enums.cs                   # System enumerations
│   ├── EventArgs.cs               # Custom event arguments
│   └── Interfaces.cs              # Core interfaces
│
├── Forms/                          # Presentation Layer
│   ├── Dashboard/
│   │   └── DashboardOverview.cs   # Main dashboard with statistics
│   ├── Employee/
│   │   ├── EmployeeManagement.cs  # Employee CRUD operations
│   │   └── EditEmployeeForm.cs    # Employee editing interface
│   ├── Department/
│   │   ├── DepartmentManagement.cs # Department CRUD operations
│   │   └── DepartmentEdit.cs      # Department editing interface
│   ├── Attendance/
│   │   ├── AttendanceManagement.cs # Attendance tracking interface
│   │   └── EmployeeAttendanceViewer.cs # Individual attendance viewer
│   ├── Payroll/
│   │   ├── PayrollManagement.cs   # Payroll management interface
│   │   ├── AddEdit/PayrollForm.cs # Payroll creation/editing
│   │   ├── PayrollReport/         # Payroll reporting
│   │   └── Search/                # Payroll search functionality
│   └── EmployeeViews/             # Employee self-service portal
│       ├── Employee_ProfileView.cs
│       ├── Employee_AttendanceManagement.cs
│       ├── Employee_DepartmentView.cs
│       └── Employee_PayrollView.cs
│
├── Services/                       # Business Logic Layer
│   ├── EmployeeService.cs         # Employee business operations
│   ├── DepartmentService.cs       # Department business operations
│   ├── AttendanceService.cs       # Attendance business operations
│   ├── LeaveService.cs            # Leave management operations
│   ├── PayrollService.cs          # Payroll calculations
│   └── RoleSelectionService.cs    # Role management
│
├── Storage/                        # Data Access Layer
│   ├── JSONFileStorage.cs         # JSON serialization implementation
│   └── FileManager.cs             # File management utilities
│
└── Data/                           # Data Storage
    ├── Employees.json             # Employee data backup
    ├── Departments.json           # Department data backup
    ├── Attendances.json           # Attendance records backup
    ├── LeaveRequests.json         # Leave request data backup
    └── Payrolls.json              # Payroll data backup
```

### Design Patterns Implemented
- **Factory Pattern**: `EmployeeFactory` for dynamic employee type creation
- **Repository Pattern**: Service layer with data access abstraction
- **Observer Pattern**: Event-driven architecture for UI updates
- **Strategy Pattern**: Different salary calculation strategies for employee types
- **Singleton Pattern**: `EmployeeSession` for session management

### Key Technical Features
- **Polymorphism**: Employee inheritance hierarchy with virtual salary calculations
- **Custom Serialization**: Specialized JSON handling for complex object hierarchies
- **Event-Driven Architecture**: Delegates and events for loose coupling
- **Exception Handling**: Comprehensive custom exception system
- **Data Validation**: Input validation throughout the application
- **Role-Based UI**: Dynamic interface adaptation based on user role

## 🔧 Key Developers & Responsibilities

| Developer | Primary Responsibilities | Key Contributions |
|-----------|-------------------------|-------------------|
| **Minh Hoàng** | **System Architecture & Employee Management** | • Core system foundation and architecture design<br>• Employee management module (CRUD operations)<br>• Employee factory pattern implementation<br>• Multi-type employee system (Regular, Full-time, Contract)<br>• Employee editing forms and validation<br>• Database integration and data models |
| **Thanh Vy** | **Department & Attendance Management** | • Department management system<br>• Department-employee relationship management<br>• Attendance tracking and clock-in/out system<br>• Leave request management and approval workflow<br>• Attendance analytics and reporting<br>• Department budget tracking and statistics |
| **Đàm Hiếu** | **Payroll & Employee Portal** | • Payroll calculation engine for all employee types<br>• Payroll management interface and reporting<br>• Employee self-service portal development<br>• Role-based access control implementation<br>• PDF payslip generation<br>• Employee dashboard and profile management |

## Prerequisites & Installation
### System Requirements
- **.NET 8.0 Windows Forms** application
- **SQL Server** for primary data storage
- **Windows 7+** operating system
- **Visual Studio 2022** or later for development

### Dependencies
- `System.Data.SqlClient` (4.9.0) - Database connectivity
- `iTextSharp` (5.5.13.4) - PDF generation for payslips
- Built-in .NET JSON serialization for data backup

### Installation Steps
1. **Clone the repository:**
   ```bash
   git clone https://github.com/tadyuh76/OOP---HRMS.git
   ```

2. **Open in Visual Studio:**
   - Open `hrms.sln` in Visual Studio 2022 or later

3. **Configure Database:**
   - Update connection string in `KetNoi.cs`
   - Ensure SQL Server is running and accessible

4. **Restore NuGet Packages:**
   ```bash
   dotnet restore
   ```

5. **Build and Run:**
   ```bash
   dotnet build
   dotnet run
   ```

## Usage Guidelines
### Admin Features
- Complete access to all modules
- Employee, department, attendance, and payroll management
- System reports and analytics
- User role management

### Employee Features
- Personal profile management
- Attendance clock-in/out
- Leave request submission
- Personal payslip viewing
- Department information access

### Default Login
- **Admin Role**: Full system access
- **Employee Role**: Limited self-service access
- Use the role switcher in the top navigation for testing different access levels

## Data Storage Strategy
The system implements a **dual storage approach**:
- **Primary Storage**: SQL Server database for real-time operations
- **Backup Storage**: JSON files for data persistence and portability
- **Automatic Sync**: Changes are reflected in both storage systems

## Contributing Guidelines
### Code Standards
- Follow C# naming conventions and coding standards
- Implement proper exception handling
- Add XML documentation for public methods
- Maintain separation of concerns across layers

### Development Workflow
1. Create feature branches from main
2. Implement features with proper testing
3. Submit pull requests with detailed descriptions
4. Code review and approval process
5. Merge to main branch

## Future Enhancements
- **Web-based Interface**: Migration to ASP.NET Core
- **Advanced Reporting**: Enhanced analytics and dashboard features
- **Mobile Application**: Employee self-service mobile app
- **Integration APIs**: Third-party system integration capabilities
- **Advanced Security**: Multi-factor authentication and encryption

## License
This project is developed for educational purposes as part of an Object-Oriented Programming course. All rights reserved to the development team.

## Support & Contact
For questions, issues, or contributions, please contact the development team or create an issue in the GitHub repository.

---
**HR Management System** - Efficient, Scalable, User-Friendly HR Solution
