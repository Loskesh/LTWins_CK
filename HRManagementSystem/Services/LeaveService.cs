using System.Data.SqlClient;

namespace HRManagementSystem
{
    public class LeaveService
    {

        private List<LeaveRequest> leaveRequests;

        private readonly string connectionString;

        public LeaveService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public LeaveRequest SubmitLeaveRequest(string employeeId, DateTime startDate, DateTime endDate, LeaveType leaveType, string remarks)
        {
            if (startDate > endDate)
                throw new ValidationException("Start date must be before or equal to end date.");

            EmployeeService employeeService = new EmployeeService(connectionString);
            Employee employee = employeeService.GetByEmployeeId(employeeId) ?? throw new EntityNotFoundException("Employee not found.");

            string requestId = GenerateLeaveRequestId();

            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"INSERT INTO LeaveRequest (RequestId, EmployeeId, EmployeeName, RequestDate, StartDate, EndDate, Type, Status, Remarks, ApproverId)
                             VALUES (@RequestId, @EmployeeId, @EmployeeName, @RequestDate, @StartDate, @EndDate, @Type, @Status, @Remarks, NULL)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@RequestId", requestId);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
            cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@Type", (int)leaveType);
            cmd.Parameters.AddWithValue("@Status", (int)LeaveStatus.Pending);
            cmd.Parameters.AddWithValue("@Remarks", remarks ?? string.Empty);

            conn.Open();
            cmd.ExecuteNonQuery();

            return new LeaveRequest
            {
                RequestId = requestId,
                EmployeeId = employeeId,
                EmployeeName = employee.Name,
                RequestDate = DateTime.Now,
                StartDate = startDate,
                EndDate = endDate,
                Type = leaveType,
                Status = LeaveStatus.Pending,
                Remarks = remarks,
                ApproverId = null,
                Employee = employee
            };
        }

        // Helper method to generate consistent leave request IDs
        private string GenerateLeaveRequestId()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT MAX(RequestId) FROM LeaveRequest WHERE RequestId LIKE 'LVE%'";

            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value && result != null)
            {
                string maxId = result.ToString();
                if (int.TryParse(maxId.Substring(3), out int number))
                {
                    return $"LVE{(number + 1):D3}";
                }
            }

            return "LVE001";
        }



        public bool ApproveLeaveRequest(string requestId, string approverId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"UPDATE LeaveRequest SET Status = @Status, ApproverId = @ApproverId WHERE RequestId = @RequestId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", (int)LeaveStatus.Approved);
            cmd.Parameters.AddWithValue("@ApproverId", approverId);
            cmd.Parameters.AddWithValue("@RequestId", requestId);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }



        public bool RejectLeaveRequest(string requestId, string approverId, string rejectionReason)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"UPDATE LeaveRequest SET Status = @Status, ApproverId = @ApproverId, Remarks = @Remarks WHERE RequestId = @RequestId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", (int)LeaveStatus.Rejected);
            cmd.Parameters.AddWithValue("@ApproverId", approverId);
            cmd.Parameters.AddWithValue("@Remarks", rejectionReason ?? "");
            cmd.Parameters.AddWithValue("@RequestId", requestId);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
        private LeaveRequest MapLeaveRequest(SqlDataReader reader)
        {
            // Lấy ordinal để performance tốt hơn
            int ordReq = reader.GetOrdinal("RequestDate");
            int ordStart = reader.GetOrdinal("StartDate");
            int ordEnd = reader.GetOrdinal("EndDate");

            return new LeaveRequest
            {
                RequestId = reader["RequestId"].ToString(),
                EmployeeId = reader["EmployeeId"].ToString(),
                EmployeeName = reader["EmployeeName"].ToString(),

                // Đọc datetimeoffset rồi chuyển sang DateTime
                RequestDate = reader.GetFieldValue<DateTimeOffset>(ordReq).DateTime,

                // Cột kiểu date trả về DateTime, có thể dùng Convert hoặc GetDateTime
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),

                Type = (LeaveType)Convert.ToInt32(reader["Type"]),
                Status = (LeaveStatus)Convert.ToInt32(reader["Status"]),
                Remarks = reader["Remarks"]?.ToString(),
                ApproverId = reader["ApproverId"]?.ToString()
            };
        }


        public List<LeaveRequest> GetEmployeeLeaves(string employeeId)
        {
            List<LeaveRequest> result = new List<LeaveRequest>();

            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM LeaveRequest WHERE EmployeeId = @EmployeeId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(MapLeaveRequest(reader));
            }

            return result;
        }





        public List<LeaveRequest> GetMonthlyLeaves(int year, int month)
        {
            List<LeaveRequest> result = new List<LeaveRequest>();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"
        SELECT * FROM LeaveRequest
        WHERE 
            (YEAR(StartDate) = @Year AND MONTH(StartDate) = @Month)
            OR (YEAR(EndDate) = @Year AND MONTH(EndDate) = @Month)
            OR (StartDate < @FirstOfMonth AND EndDate >= @FirstOfMonth)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@FirstOfMonth", new DateTime(year, month, 1));

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(MapLeaveRequest(reader));
            }

            return result;
        }


        public List<LeaveRequest> GetDailyLeaves(DateTime date)
        {
            List<LeaveRequest> result = new List<LeaveRequest>();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"
        SELECT * FROM LeaveRequest
        WHERE @Date BETWEEN StartDate AND EndDate";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Date", date.Date);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(MapLeaveRequest(reader));
            }

            return result;
        }



        public List<LeaveRequest> GetEmployeeMonthlyLeaves(string employeeId, int year, int month)
        {
            List<LeaveRequest> result = new List<LeaveRequest>();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"
        SELECT * FROM LeaveRequest
        WHERE EmployeeId = @EmployeeId AND (
            (YEAR(StartDate) = @Year AND MONTH(StartDate) = @Month)
            OR (YEAR(EndDate) = @Year AND MONTH(EndDate) = @Month)
            OR (StartDate < @FirstOfMonth AND EndDate >= @FirstOfMonth)
        )";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@FirstOfMonth", new DateTime(year, month, 1));

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(MapLeaveRequest(reader));
            }

            return result;
        }


        public List<LeaveRequest> GetEmployeeDailyLeaves(string employeeId, DateTime date)
        {
            List<LeaveRequest> result = new List<LeaveRequest>();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"
        SELECT * FROM LeaveRequest
        WHERE EmployeeId = @EmployeeId AND @Date BETWEEN StartDate AND EndDate";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@Date", date.Date);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(MapLeaveRequest(reader));
            }

            return result;
        }




    }
}