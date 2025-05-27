using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class Employee : Person
    {
        private string employeeId;
        private DateTime hireDate;
        private string position;
        private decimal baseSalary;
        private string departmentId;
        private EmployeeStatus status;

        public Employee() : base()
        {
            // Default constructor required for JSON serialization
            employeeId = string.Empty;
            hireDate = DateTime.MinValue;
            position = string.Empty;
            baseSalary = 0;
            departmentId = string.Empty;
            status = EmployeeStatus.Active;
        }

        public Employee(
            string id,
            string name,
            string email,
            string phone,
            DateTime dateOfBirth,
            string address,
            string employeeId,
            DateTime hireDate,
            string position,
            decimal baseSalary,
            string departmentId,
            EmployeeStatus status) : base(id, name, email, phone, dateOfBirth, address)
        {
            this.employeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId));
            this.hireDate = hireDate;
            this.position = position ?? throw new ArgumentNullException(nameof(position));
            this.baseSalary = baseSalary;
            this.departmentId = departmentId ?? throw new ArgumentNullException(nameof(departmentId));
            this.status = status;
        }

        [JsonPropertyName("employeeId")]
        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("hireDate")]
        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        [JsonPropertyName("position")]
        public string Position
        {
            get { return position; }
            set { position = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("baseSalary")]
        public decimal BaseSalary
        {
            get { return baseSalary; }
            set { baseSalary = value; }
        }

        [JsonPropertyName("departmentId")]
        public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("status")]
        public EmployeeStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        [JsonPropertyName("department")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Department Department { get; set; }

        // Add a new property for department name that isn't serialized
        [JsonIgnore]
        public string DepartmentName { get; set; }

        // Add employee type property for type discrimination
        [JsonPropertyName("employeeType")]
        public virtual string EmployeeType { get; set; } = "Regular";

        public int CalculateYearsOfService()
        {
            DateTime today = DateTime.Today;
            int years = today.Year - hireDate.Year;

            if (hireDate.Date > today.AddYears(-years))
            {
                years--;
            }

            return years;
        }

        public virtual decimal CalculateSalary()
        {
            return baseSalary;
        }
    }
}