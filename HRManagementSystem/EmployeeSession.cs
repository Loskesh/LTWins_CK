namespace HRManagementSystem
{
    public class EmployeeSession
    {
        private static EmployeeSession instance;
        private static readonly object lockObj = new object();

        public string EmployeeId { get; set; }

        // Constructor private để không cho phép khởi tạo từ bên ngoài
        private EmployeeSession()
        {
            EmployeeId = "EMP001"; // Giá trị mặc định
        }

        // Property Instance để truy cập singleton
        public static EmployeeSession Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeSession();
                    }
                    return instance;
                }
            }
        }
    }
}
