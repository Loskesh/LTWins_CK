using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class ContractEmployee : Employee
    {
        private decimal hourlyRate;
        private decimal hoursWorked;

        public ContractEmployee() : base()
        {
            hourlyRate = 0;
            hoursWorked = 0;
            EmployeeType = "Contract";
        }

        [JsonPropertyName("hourlyRate")]
        public decimal HourlyRate
        {
            get { return hourlyRate; }
            set { hourlyRate = value; }
        }

        [JsonPropertyName("hoursWorked")]
        public decimal HoursWorked
        {
            get { return hoursWorked; }
            set { hoursWorked = value; }
        }

        public override string EmployeeType => "Contract";

        public override decimal CalculateSalary()
        {
            return hourlyRate * hoursWorked;
        }
    }
}