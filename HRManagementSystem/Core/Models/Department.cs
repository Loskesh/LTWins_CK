using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class Department
    {
        private string departmentId;
        private string name;
        private string description;
        private decimal budget;
        private string managerId;
        private string managerName;

        public Department()
        {
            // Default constructor required for JSON serialization
            Employees = new List<Employee>();
        }

        public Department(
            string departmentId,
            string name,
            string description,
            decimal budget,
            string managerId,
            string managerName)
        {
            this.departmentId = departmentId ?? throw new ArgumentNullException(nameof(departmentId));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.description = description;
            this.budget = budget;
            this.managerId = managerId ?? throw new ArgumentNullException(nameof(managerId));
            this.managerName = managerName ?? throw new ArgumentNullException(nameof(managerName));
            Employees = new List<Employee>();
        }

        [JsonPropertyName("departmentId")]
        public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get { return name; }
            set { name = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [JsonPropertyName("budget")]
        public decimal Budget
        {
            get { return budget; }
            set { budget = value; }
        }

        [JsonPropertyName("managerId")]
        public string ManagerId
        {
            get { return managerId; }
            set { managerId = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("managerName")]
        public string ManagerName
        {
            get { return managerName; }
            set { managerName = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("employees")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Employee> Employees { get; set; }

        [JsonPropertyName("manager")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Employee Manager { get; set; }

        public int GetEmployeeCount()
        {
            if (Employees == null)
            {
                return 0;
            }

            return Employees.Count;
        }

        public decimal GetBudgetUtilization()
        {
            if (Employees == null || Employees.Count == 0)
            {
                return 0;
            }

            decimal totalSalary = 0;
            for (int i = 0; i < Employees.Count; i++)
            {
                totalSalary += Employees[i].CalculateSalary();
            }

            if (budget == 0)
            {
                return 0;
            }

            return totalSalary / budget * 100;
        }
    }
}