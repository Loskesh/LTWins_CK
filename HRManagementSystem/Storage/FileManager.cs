namespace HRManagementSystem
{
    public class FileManager
    {
        private readonly IFileStorage _fileStorage;
        public static string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
        public static string dataDirectory = Path.Combine(projectDirectory, "Data");
        public static string employeeDataPath = Path.Combine(dataDirectory, "Employees.json");
        public static string departmentDataPath = Path.Combine(dataDirectory, "Departments.json");
        public static string attendanceDataPath = Path.Combine(dataDirectory, "Attendances.json");
        public static string leaveDataPath = Path.Combine(dataDirectory, "LeaveRequests.json");
        public static string payrollDataPath = Path.Combine(dataDirectory, "Payrolls.json");

        public FileManager(IFileStorage storage)
        {
            _fileStorage = storage ?? throw new ArgumentNullException(nameof(storage));
            // Ensure Data directory exists
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }

        public List<Employee> LoadEmployees()
        {
            return _fileStorage.LoadData<List<Employee>>(employeeDataPath) ?? new List<Employee>();
        }

        public bool SaveEmployees(List<Employee> employees)
        {
            if (employees == null)
            {
                throw new ArgumentNullException(nameof(employees));
            }
            return _fileStorage.SaveData(employeeDataPath, employees);
        }

        public List<Department> LoadDepartments()
        {
            return _fileStorage.LoadData<List<Department>>(departmentDataPath) ?? new List<Department>();
        }

        public bool SaveDepartments(List<Department> departments)
        {
            if (departments == null)
            {
                throw new ArgumentNullException(nameof(departments));
            }
            return _fileStorage.SaveData(departmentDataPath, departments);
        }

        public List<Attendance> LoadAttendances()
        {
            return _fileStorage.LoadData<List<Attendance>>(attendanceDataPath) ?? new List<Attendance>();
        }

        public bool SaveAttendances(List<Attendance> attendances)
        {
            if (attendances == null)
            {
                throw new ArgumentNullException(nameof(attendances));
            }
            return _fileStorage.SaveData(attendanceDataPath, attendances);
        }

        public List<LeaveRequest> LoadLeaveRequests()
        {
            // Load leave requests only from the leaveDataPath
            return _fileStorage.LoadData<List<LeaveRequest>>(leaveDataPath) ?? new List<LeaveRequest>();
        }

        public bool SaveLeaveRequests(List<LeaveRequest> leaveRequests)
        {
            if (leaveRequests == null)
            {
                throw new ArgumentNullException(nameof(leaveRequests));
            }

            // Save only to the leaveDataPath
            return _fileStorage.SaveData(leaveDataPath, leaveRequests);
        }
    }
}