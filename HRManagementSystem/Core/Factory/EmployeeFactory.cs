namespace HRManagementSystem
{
    public class EmployeeFactory
    {
        public Employee CreateEmployee(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            switch (type.ToLower())
            {
                case "fulltime":
                    return CreateFullTimeEmployee();
                case "contract":
                    return CreateContractEmployee();
                case "regular": // Add support for "Regular" type
                    return new Employee
                    {
                        Status = EmployeeStatus.Active
                    };
                default:
                    throw new ArgumentException("Invalid employee type", nameof(type));
            }
        }

        public FullTimeEmployee CreateFullTimeEmployee()
        {
            return new FullTimeEmployee
            {
                Status = EmployeeStatus.Active
            };
        }

        public ContractEmployee CreateContractEmployee()
        {
            return new ContractEmployee
            {
                Status = EmployeeStatus.Active
            };
        }

        // Add conversion methods between employee types
        public Employee ConvertToType(Employee sourceEmployee, string targetType)
        {
            if (string.IsNullOrEmpty(targetType))
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            // If employee is already the target type, just return it
            if (sourceEmployee.EmployeeType.Equals(targetType, StringComparison.OrdinalIgnoreCase))
            {
                return sourceEmployee;
            }

            switch (targetType.ToLower())
            {
                case "fulltime":
                    return ConvertToFullTime(sourceEmployee);
                case "contract":
                    return ConvertToContract(sourceEmployee);
                case "regular":
                    return ConvertToRegular(sourceEmployee);
                default:
                    throw new ArgumentException("Invalid employee type", nameof(targetType));
            }
        }

        private FullTimeEmployee ConvertToFullTime(Employee source)
        {
            FullTimeEmployee target = new FullTimeEmployee
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                Phone = source.Phone,
                DateOfBirth = source.DateOfBirth,
                Address = source.Address,
                EmployeeId = source.EmployeeId,
                HireDate = source.HireDate,
                Position = source.Position,
                BaseSalary = source.BaseSalary,
                DepartmentId = source.DepartmentId,
                Status = source.Status,
                AnnualBonus = 0  // Default annual bonus
            };
            return target;
        }

        private ContractEmployee ConvertToContract(Employee source)
        {
            ContractEmployee target = new ContractEmployee
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                Phone = source.Phone,
                DateOfBirth = source.DateOfBirth,
                Address = source.Address,
                EmployeeId = source.EmployeeId,
                HireDate = source.HireDate,
                Position = source.Position,
                BaseSalary = source.BaseSalary,
                DepartmentId = source.DepartmentId,
                Status = source.Status,
                HourlyRate = source.BaseSalary / 160,  // Assuming 160 hours per month
                HoursWorked = 160  // Default hours worked
            };
            return target;
        }

        private Employee ConvertToRegular(Employee source)
        {
            // If it's already a base Employee, return it
            if (source.GetType() == typeof(Employee))
            {
                return source;
            }

            Employee target = new Employee
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                Phone = source.Phone,
                DateOfBirth = source.DateOfBirth,
                Address = source.Address,
                EmployeeId = source.EmployeeId,
                HireDate = source.HireDate,
                Position = source.Position,
                BaseSalary = source.BaseSalary,
                DepartmentId = source.DepartmentId,
                Status = source.Status
            };
            return target;
        }
    }
}