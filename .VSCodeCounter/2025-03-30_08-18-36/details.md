# Details

Date : 2025-03-30 08:18:36

Directory c:\\Users\\tadyuh\\Coding Projects\\HRMS\\HRManagementSystem

Total : 67 files,  12058 codes, 1008 comments, 1542 blanks, all 14608 lines

[Summary](results.md) / Details / [Diff Summary](diff.md) / [Diff Details](diff-details.md)

## Files
| filename | language | code | comment | blank | total |
| :--- | :--- | ---: | ---: | ---: | ---: |
| [HRManagementSystem/Core/ColorPalette.cs](/HRManagementSystem/Core/ColorPalette.cs) | C# | 11 | 1 | 1 | 13 |
| [HRManagementSystem/Core/Enums.cs](/HRManagementSystem/Core/Enums.cs) | C# | 44 | 1 | 5 | 50 |
| [HRManagementSystem/Core/EventArgs.cs](/HRManagementSystem/Core/EventArgs.cs) | C# | 19 | 1 | 4 | 24 |
| [HRManagementSystem/Core/Exceptions/EntityNotFoundException.cs](/HRManagementSystem/Core/Exceptions/EntityNotFoundException.cs) | C# | 23 | 0 | 4 | 27 |
| [HRManagementSystem/Core/Exceptions/HRSystemException.cs](/HRManagementSystem/Core/Exceptions/HRSystemException.cs) | C# | 23 | 0 | 5 | 28 |
| [HRManagementSystem/Core/Exceptions/ValidationException.cs](/HRManagementSystem/Core/Exceptions/ValidationException.cs) | C# | 23 | 0 | 5 | 28 |
| [HRManagementSystem/Core/Factory/EmployeeFactory.cs](/HRManagementSystem/Core/Factory/EmployeeFactory.cs) | C# | 127 | 3 | 10 | 140 |
| [HRManagementSystem/Core/Interfaces.cs](/HRManagementSystem/Core/Interfaces.cs) | C# | 16 | 0 | 2 | 18 |
| [HRManagementSystem/Core/Models/Attendance.cs](/HRManagementSystem/Core/Models/Attendance.cs) | C# | 25 | 0 | 9 | 34 |
| [HRManagementSystem/Core/Models/ContractEmployee.cs](/HRManagementSystem/Core/Models/ContractEmployee.cs) | C# | 32 | 0 | 6 | 38 |
| [HRManagementSystem/Core/Models/Department.cs](/HRManagementSystem/Core/Models/Department.cs) | C# | 100 | 1 | 17 | 118 |
| [HRManagementSystem/Core/Models/Employee.cs](/HRManagementSystem/Core/Models/Employee.cs) | C# | 100 | 3 | 16 | 119 |
| [HRManagementSystem/Core/Models/FullTimeEmployee.cs](/HRManagementSystem/Core/Models/FullTimeEmployee.cs) | C# | 40 | 3 | 7 | 50 |
| [HRManagementSystem/Core/Models/LeaveRequest.cs](/HRManagementSystem/Core/Models/LeaveRequest.cs) | C# | 128 | 1 | 19 | 148 |
| [HRManagementSystem/Core/Models/Payroll.cs](/HRManagementSystem/Core/Models/Payroll.cs) | C# | 114 | 3 | 16 | 133 |
| [HRManagementSystem/Core/Models/Person.cs](/HRManagementSystem/Core/Models/Person.cs) | C# | 76 | 2 | 12 | 90 |
| [HRManagementSystem/Data/Attendances.json](/HRManagementSystem/Data/Attendances.json) | JSON | 191 | 0 | 0 | 191 |
| [HRManagementSystem/Data/Departments.json](/HRManagementSystem/Data/Departments.json) | JSON | 47 | 0 | 0 | 47 |
| [HRManagementSystem/Data/Employees.json](/HRManagementSystem/Data/Employees.json) | JSON | 257 | 0 | 0 | 257 |
| [HRManagementSystem/Data/LeaveRequests.json](/HRManagementSystem/Data/LeaveRequests.json) | JSON | 122 | 0 | 0 | 122 |
| [HRManagementSystem/Data/Payrolls.json](/HRManagementSystem/Data/Payrolls.json) | JSON | 314 | 0 | 0 | 314 |
| [HRManagementSystem/Forms/Attendance/AttendanceManagement.cs](/HRManagementSystem/Forms/Attendance/AttendanceManagement.cs) | C# | 842 | 82 | 114 | 1,038 |
| [HRManagementSystem/Forms/Attendance/EmployeeAttendanceViewer.cs](/HRManagementSystem/Forms/Attendance/EmployeeAttendanceViewer.cs) | C# | 595 | 52 | 90 | 737 |
| [HRManagementSystem/Forms/Dashboard/DashboardOverview.cs](/HRManagementSystem/Forms/Dashboard/DashboardOverview.cs) | C# | 493 | 63 | 83 | 639 |
| [HRManagementSystem/Forms/Department/DepartmentEdit.cs](/HRManagementSystem/Forms/Department/DepartmentEdit.cs) | C# | 238 | 22 | 38 | 298 |
| [HRManagementSystem/Forms/Department/DepartmentManagement.cs](/HRManagementSystem/Forms/Department/DepartmentManagement.cs) | C# | 419 | 36 | 54 | 509 |
| [HRManagementSystem/Forms/EmployeeViews/Employee\_AttendanceManagement.cs](/HRManagementSystem/Forms/EmployeeViews/Employee_AttendanceManagement.cs) | C# | 736 | 46 | 109 | 891 |
| [HRManagementSystem/Forms/EmployeeViews/Employee\_DepartmentDetailsDialog.cs](/HRManagementSystem/Forms/EmployeeViews/Employee_DepartmentDetailsDialog.cs) | C# | 180 | 11 | 23 | 214 |
| [HRManagementSystem/Forms/EmployeeViews/Employee\_DepartmentView.cs](/HRManagementSystem/Forms/EmployeeViews/Employee_DepartmentView.cs) | C# | 309 | 35 | 50 | 394 |
| [HRManagementSystem/Forms/EmployeeViews/Employee\_PayrollView.cs](/HRManagementSystem/Forms/EmployeeViews/Employee_PayrollView.cs) | C# | 535 | 72 | 66 | 673 |
| [HRManagementSystem/Forms/EmployeeViews/Employee\_ProfileView.cs](/HRManagementSystem/Forms/EmployeeViews/Employee_ProfileView.cs) | C# | 478 | 52 | 87 | 617 |
| [HRManagementSystem/Forms/Employee/EditEmployeeForm.cs](/HRManagementSystem/Forms/Employee/EditEmployeeForm.cs) | C# | 584 | 48 | 83 | 715 |
| [HRManagementSystem/Forms/Employee/EmployeeManagement.cs](/HRManagementSystem/Forms/Employee/EmployeeManagement.cs) | C# | 531 | 59 | 80 | 670 |
| [HRManagementSystem/Forms/Payroll/AddEdit/PayrollForm.Designer.cs](/HRManagementSystem/Forms/Payroll/AddEdit/PayrollForm.Designer.cs) | C# | 250 | 74 | 6 | 330 |
| [HRManagementSystem/Forms/Payroll/AddEdit/PayrollForm.cs](/HRManagementSystem/Forms/Payroll/AddEdit/PayrollForm.cs) | C# | 486 | 12 | 78 | 576 |
| [HRManagementSystem/Forms/Payroll/PayrollManagement.Designer.cs](/HRManagementSystem/Forms/Payroll/PayrollManagement.Designer.cs) | C# | 244 | 65 | 5 | 314 |
| [HRManagementSystem/Forms/Payroll/PayrollManagement.cs](/HRManagementSystem/Forms/Payroll/PayrollManagement.cs) | C# | 335 | 6 | 52 | 393 |
| [HRManagementSystem/Forms/Payroll/PayrollReport/PayrollReport.Designer.cs](/HRManagementSystem/Forms/Payroll/PayrollReport/PayrollReport.Designer.cs) | C# | 294 | 86 | 6 | 386 |
| [HRManagementSystem/Forms/Payroll/PayrollReport/PayrollReport.cs](/HRManagementSystem/Forms/Payroll/PayrollReport/PayrollReport.cs) | C# | 253 | 0 | 65 | 318 |
| [HRManagementSystem/Forms/Payroll/Search/PayrollSearch.Designer.cs](/HRManagementSystem/Forms/Payroll/Search/PayrollSearch.Designer.cs) | C# | 163 | 44 | 6 | 213 |
| [HRManagementSystem/Forms/Payroll/Search/PayrollSearch.cs](/HRManagementSystem/Forms/Payroll/Search/PayrollSearch.cs) | C# | 198 | 0 | 39 | 237 |
| [HRManagementSystem/HRManagementSystem.csproj](/HRManagementSystem/HRManagementSystem.csproj) | XML | 12 | 0 | 3 | 15 |
| [HRManagementSystem/MainForm.Designer.cs](/HRManagementSystem/MainForm.Designer.cs) | C# | 130 | 15 | 22 | 167 |
| [HRManagementSystem/MainForm.cs](/HRManagementSystem/MainForm.cs) | C# | 184 | 16 | 32 | 232 |
| [HRManagementSystem/Program.cs](/HRManagementSystem/Program.cs) | C# | 26 | 3 | 1 | 30 |
| [HRManagementSystem/Services/AttendanceService.cs](/HRManagementSystem/Services/AttendanceService.cs) | C# | 245 | 26 | 38 | 309 |
| [HRManagementSystem/Services/DepartmentService.cs](/HRManagementSystem/Services/DepartmentService.cs) | C# | 128 | 6 | 24 | 158 |
| [HRManagementSystem/Services/EnployeeService.cs](/HRManagementSystem/Services/EnployeeService.cs) | C# | 127 | 9 | 24 | 160 |
| [HRManagementSystem/Services/LeaveService.cs](/HRManagementSystem/Services/LeaveService.cs) | C# | 285 | 13 | 43 | 341 |
| [HRManagementSystem/Services/PayrollService.cs](/HRManagementSystem/Services/PayrollService.cs) | C# | 203 | 5 | 30 | 238 |
| [HRManagementSystem/Services/RoleSelectionService.cs](/HRManagementSystem/Services/RoleSelectionService.cs) | C# | 90 | 6 | 15 | 111 |
| [HRManagementSystem/Storage/FileManager.cs](/HRManagementSystem/Storage/FileManager.cs) | C# | 70 | 3 | 10 | 83 |
| [HRManagementSystem/Storage/JSONFileStorage.cs](/HRManagementSystem/Storage/JSONFileStorage.cs) | C# | 126 | 11 | 20 | 157 |
| [HRManagementSystem/bin/Debug/net8.0-windows/Data/Departments.json](/HRManagementSystem/bin/Debug/net8.0-windows/Data/Departments.json) | JSON | 11 | 0 | 0 | 11 |
| [HRManagementSystem/bin/Debug/net8.0-windows/HRManagementSystem.deps.json](/HRManagementSystem/bin/Debug/net8.0-windows/HRManagementSystem.deps.json) | JSON | 59 | 0 | 0 | 59 |
| [HRManagementSystem/bin/Debug/net8.0-windows/HRManagementSystem.runtimeconfig.json](/HRManagementSystem/bin/Debug/net8.0-windows/HRManagementSystem.runtimeconfig.json) | JSON | 19 | 0 | 0 | 19 |
| [HRManagementSystem/obj/Debug/net8.0-windows/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs](/HRManagementSystem/obj/Debug/net8.0-windows/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs) | C# | 3 | 1 | 1 | 5 |
| [HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.AssemblyInfo.cs](/HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.AssemblyInfo.cs) | C# | 11 | 9 | 5 | 25 |
| [HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.GeneratedMSBuildEditorConfig.editorconfig](/HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.GeneratedMSBuildEditorConfig.editorconfig) | Properties | 22 | 0 | 1 | 23 |
| [HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.GlobalUsings.g.cs](/HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.GlobalUsings.g.cs) | C# | 9 | 1 | 1 | 11 |
| [HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.designer.deps.json](/HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.designer.deps.json) | JSON | 11 | 0 | 0 | 11 |
| [HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.designer.runtimeconfig.json](/HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.designer.runtimeconfig.json) | JSON | 24 | 0 | 0 | 24 |
| [HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.sourcelink.json](/HRManagementSystem/obj/Debug/net8.0-windows/HRManagementSystem.sourcelink.json) | JSON | 1 | 0 | 0 | 1 |
| [HRManagementSystem/obj/HRManagementSystem.csproj.nuget.dgspec.json](/HRManagementSystem/obj/HRManagementSystem.csproj.nuget.dgspec.json) | JSON | 83 | 0 | 0 | 83 |
| [HRManagementSystem/obj/HRManagementSystem.csproj.nuget.g.props](/HRManagementSystem/obj/HRManagementSystem.csproj.nuget.g.props) | XML | 16 | 0 | 0 | 16 |
| [HRManagementSystem/obj/HRManagementSystem.csproj.nuget.g.targets](/HRManagementSystem/obj/HRManagementSystem.csproj.nuget.g.targets) | XML | 2 | 0 | 0 | 2 |
| [HRManagementSystem/obj/project.assets.json](/HRManagementSystem/obj/project.assets.json) | JSON | 166 | 0 | 0 | 166 |

[Summary](results.md) / Details / [Diff Summary](diff.md) / [Diff Details](diff-details.md)