using System.Data.SqlClient;

namespace HRManagementSystem
{
    public class EmployeeService : IService<Employee>
    {
        private readonly string _connectionString;

        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT Id, EmployeeId, Name, Email, Phone, DateOfBirth, Address, 
                            HireDate, Position, BaseSalary, DepartmentId, Status, EmployeeType,
                            (SELECT Name FROM Department WHERE DepartmentId = Employee.DepartmentId) as DepartmentName
                            FROM Employee ORDER BY EmployeeId";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee emp = new Employee
                            {
                                Id = reader["Id"].ToString(),
                                EmployeeId = reader["EmployeeId"].ToString(),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Address = reader["Address"].ToString(),
                                HireDate = Convert.ToDateTime(reader["HireDate"]), // ✅ Đảm bảo convert đúng
                                Position = reader["Position"].ToString(),
                                BaseSalary = Convert.ToDecimal(reader["BaseSalary"]),
                                DepartmentId = reader["DepartmentId"].ToString(),
                                Status = (EmployeeStatus)Convert.ToInt32(reader["Status"]), // ✅ Convert từ int
                                EmployeeType = reader["EmployeeType"].ToString(),
                                DepartmentName = reader["DepartmentName"]?.ToString()
                            };

                            // ✅ DEBUG: Log employee được load từ DB
                            System.Diagnostics.Debug.WriteLine($"[DB] Loaded employee {emp.EmployeeId}:");
                            System.Diagnostics.Debug.WriteLine($"  - HireDate from DB: {emp.HireDate}");
                            System.Diagnostics.Debug.WriteLine($"  - Status from DB: {emp.Status} (value: {Convert.ToInt32(reader["Status"])})");

                            employees.Add(emp);
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine($"[DB] Total employees loaded: {employees.Count}");
                return employees;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DB ERROR] {ex.Message}");
                throw new HRSystemException($"Failed to retrieve employees: {ex.Message}");
            }
        }

        public Employee GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT e.Id,e.EmployeeId, e.EmployeeType,e.Position,  e.Name, e.Email, e.Phone, e.Address, e.DepartmentId, d.Name AS DepartmentName
                                 FROM Employee e
                                 LEFT JOIN Department d ON e.DepartmentId = d.DepartmentId
                                 WHERE e.Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                Id = reader["Id"].ToString(),
                                EmployeeId = reader["EmployeeId"].ToString(),
                                EmployeeType = reader["EmployeeType"].ToString(),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Position = reader["Position"].ToString(),
                                Address = reader["Address"].ToString(),
                                DepartmentId = reader["DepartmentId"].ToString(),
                                DepartmentName = reader["DepartmentName"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
        public Employee GetByEmployeeId(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId)) throw new ArgumentNullException(nameof(employeeId));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT e.Id, e.EmployeeId, e.EmployeeType, e.Position, e.Name, e.Email, e.Phone, e.Address, e.DepartmentId, d.Name AS DepartmentName
                         FROM Employee e
                         LEFT JOIN Department d ON e.DepartmentId = d.DepartmentId
                         WHERE e.EmployeeId = @EmployeeId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                Id = reader["Id"].ToString(),
                                EmployeeId = reader["EmployeeId"].ToString(),
                                EmployeeType = reader["EmployeeType"].ToString(),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Position = reader["Position"].ToString(),
                                Address = reader["Address"].ToString(),
                                DepartmentId = reader["DepartmentId"].ToString(),
                                DepartmentName = reader["DepartmentName"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool Add(Employee entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // ✅ SỬA: Validate dates trước khi insert
            if (entity.DateOfBirth < new DateTime(1753, 1, 1) || entity.DateOfBirth > DateTime.Today)
            {
                throw new ValidationException("Date of Birth must be between 1753 and today.");
            }

            if (entity.HireDate < new DateTime(1753, 1, 1) || entity.HireDate > DateTime.Today.AddYears(1))
            {
                throw new ValidationException("Hire Date must be between 1753 and 1 year from today.");
            }

            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Employee 
                         (Id, EmployeeId, Name, Email, Phone, DateOfBirth, Address, HireDate, Position, BaseSalary, DepartmentId, Status, EmployeeType, AnnualBonus)
                         VALUES 
                         (@Id, @EmployeeId, @Name, @Email, @Phone, @DateOfBirth, @Address, @HireDate, @Position, @BaseSalary, @DepartmentId, @Status, @EmployeeType, @AnnualBonus)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        System.Diagnostics.Debug.WriteLine($"[SERVICE ADD] Inserting employee:");
                        System.Diagnostics.Debug.WriteLine($"  - DOB: {entity.DateOfBirth}");
                        System.Diagnostics.Debug.WriteLine($"  - HireDate: {entity.HireDate}");

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId);
                        cmd.Parameters.AddWithValue("@Name", entity.Name);
                        cmd.Parameters.AddWithValue("@Email", entity.Email);
                        cmd.Parameters.AddWithValue("@Phone", entity.Phone);
                        cmd.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Address", entity.Address);
                        cmd.Parameters.AddWithValue("@HireDate", entity.HireDate);
                        cmd.Parameters.AddWithValue("@Position", entity.Position);
                        cmd.Parameters.AddWithValue("@BaseSalary", entity.BaseSalary);
                        cmd.Parameters.AddWithValue("@DepartmentId", entity.DepartmentId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", (int)entity.Status);
                        cmd.Parameters.AddWithValue("@EmployeeType", entity.EmployeeType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@AnnualBonus", 0);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SERVICE ADD ERROR] {ex.Message}");
                throw new HRSystemException($"Failed to add employee: {ex.Message}");
            }
        }

        public bool Update(Employee entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Id))
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // ✅ DEBUG: Log trước khi update
                System.Diagnostics.Debug.WriteLine($"[SERVICE] Updating employee:");
                System.Diagnostics.Debug.WriteLine($"  - ID: {entity.Id}");
                System.Diagnostics.Debug.WriteLine($"  - Name: '{entity.Name}'");
                System.Diagnostics.Debug.WriteLine($"  - Status: {(int)entity.Status} ({entity.Status})");
                System.Diagnostics.Debug.WriteLine($"  - HireDate: {entity.HireDate}");

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"UPDATE Employee SET 
                            EmployeeId = @EmployeeId,
                            Name = @Name,
                            Email = @Email,
                            Phone = @Phone,
                            DateOfBirth = @DateOfBirth,
                            Address = @Address,
                            HireDate = @HireDate,
                            Position = @Position,
                            BaseSalary = @BaseSalary,
                            DepartmentId = @DepartmentId,
                            Status = @Status,
                            EmployeeType = @EmployeeType
                            WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Name", entity.Name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", entity.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@HireDate", entity.HireDate);
                        cmd.Parameters.AddWithValue("@Position", entity.Position ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BaseSalary", entity.BaseSalary);
                        cmd.Parameters.AddWithValue("@DepartmentId", entity.DepartmentId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", (int)entity.Status); // ✅ Cast to int
                        cmd.Parameters.AddWithValue("@EmployeeType", entity.EmployeeType ?? (object)DBNull.Value);

                        // ✅ DEBUG: Log parameters
                        System.Diagnostics.Debug.WriteLine($"[SERVICE] SQL Parameters:");
                        foreach (SqlParameter param in cmd.Parameters)
                        {
                            System.Diagnostics.Debug.WriteLine($"  {param.ParameterName} = {param.Value}");
                        }

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        System.Diagnostics.Debug.WriteLine($"[SERVICE] Update result: {rowsAffected} rows affected");

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SERVICE ERROR] {ex.Message}");
                throw new HRSystemException($"Failed to update employee: {ex.Message}");
            }
        }
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Employee WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
