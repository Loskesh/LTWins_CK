using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class FullTimeEmployee : Employee
    {
        private decimal annualBonus;

        public FullTimeEmployee() : base()
        {
            // Default constructor required for JSON serialization
        }

        public FullTimeEmployee(
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
            EmployeeStatus status,
            decimal annualBonus) : base(id, name, email, phone, dateOfBirth, address, employeeId, hireDate, position, baseSalary, departmentId, status)
        {
            this.annualBonus = annualBonus;
        }

        [JsonPropertyName("annualBonus")]
        public decimal AnnualBonus
        {
            get { return annualBonus; }
            set { annualBonus = value; }
        }

        // Override the employee type
        [JsonPropertyName("employeeType")]
        public override string EmployeeType { get; set; } = "FullTime";

        public override decimal CalculateSalary()
        {
            // Monthly salary calculation (base salary + annualBonus/12)
            return BaseSalary + (annualBonus / 12);
        }
    }

}