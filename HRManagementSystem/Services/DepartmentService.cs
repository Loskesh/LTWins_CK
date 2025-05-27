using System.Data.SqlClient;

namespace HRManagementSystem
{
    public class DepartmentService : IService<Department>
    {
        private readonly string _connectionString;

        public DepartmentService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT DepartmentId, Name, Description, Budget, ManagerId, ManagerName FROM Department";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DepartmentId = reader["DepartmentId"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Budget = Convert.ToDecimal(reader["Budget"]),
                            ManagerId = reader["ManagerId"].ToString(),
                            ManagerName = reader["ManagerName"].ToString()
                        });
                    }
                }
            }

            return departments;
        }

        public Department GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Department WHERE DepartmentId = @DepartmentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Department
                        {
                            DepartmentId = reader["DepartmentId"].ToString(),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Budget = Convert.ToDecimal(reader["Budget"]),
                            ManagerId = reader["ManagerId"].ToString(),
                            ManagerName = reader["ManagerName"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public bool Add(Department entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Department (DepartmentId, Name, Description, Budget, ManagerId, ManagerName)
                                 VALUES (@DepartmentId, @Name, @Description, @Budget, @ManagerId, @ManagerName)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentId", entity.DepartmentId);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Description", entity.Description ?? "");
                cmd.Parameters.AddWithValue("@Budget", entity.Budget);
                cmd.Parameters.AddWithValue("@ManagerId", entity.ManagerId ?? "");
                cmd.Parameters.AddWithValue("@ManagerName", entity.ManagerName ?? "");

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Department entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.DepartmentId))
                throw new ArgumentNullException(nameof(entity));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Department SET 
                                 Name = @Name,
                                 Description = @Description,
                                 Budget = @Budget,
                                 ManagerId = @ManagerId,
                                 ManagerName = @ManagerName
                                 WHERE DepartmentId = @DepartmentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentId", entity.DepartmentId);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Description", entity.Description ?? "");
                cmd.Parameters.AddWithValue("@Budget", entity.Budget);
                cmd.Parameters.AddWithValue("@ManagerId", entity.ManagerId ?? "");
                cmd.Parameters.AddWithValue("@ManagerName", entity.ManagerName ?? "");

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Department WHERE DepartmentId = @DepartmentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentId", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public string GenerateNewDepartmentId()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT MAX(DepartmentId) FROM Department WHERE DepartmentId LIKE 'DEP%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result == DBNull.Value || result == null)
                    return "DEP001";

                string lastId = result.ToString();
                if (int.TryParse(lastId.Substring(3), out int number))
                {
                    return $"DEP{(number + 1):D3}";
                }

                return "DEP001";
            }
        }
    }
}
