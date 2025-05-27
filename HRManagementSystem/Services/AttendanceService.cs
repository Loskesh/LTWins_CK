using System.Data.SqlClient;

namespace HRManagementSystem
{
    public class AttendanceService
    {
        private readonly string connectionString = KetNoi.str;

        public Attendance RecordAttendance(string employeeId, string employeeName, AttendanceStatus status)
        {
            if (HasAttendanceToday(employeeId))
            {
                throw new HRSystemException("Attendance already recorded for today.");
            }

            DateTime currentTime = DateTime.Now;
            if (status == AttendanceStatus.Present && currentTime.TimeOfDay > GetWorkStartTime())
            {
                status = AttendanceStatus.Late;
            }

            string newId = GenerateNewAttendanceId();

            Attendance attendance = new Attendance
            {
                AttendanceId = newId,
                EmployeeId = employeeId,
                EmployeeName = employeeName,
                Date = DateTime.Today,
                ClockInTime = currentTime,
                ClockOutTime = DateTime.MinValue, // chưa checkout
                Status = status,
                IsAbsentRecord = false
            };

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Attendance 
                        (AttendanceId, EmployeeId, EmployeeName, Date, ClockInTime, ClockOutTime, Status, IsAbsentRecord)
                         VALUES 
                        (@AttendanceId, @EmployeeId, @EmployeeName, @Date, @ClockInTime, @ClockOutTime, @Status, @IsAbsentRecord)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttendanceId", attendance.AttendanceId);
                cmd.Parameters.AddWithValue("@EmployeeId", attendance.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", attendance.EmployeeName);
                cmd.Parameters.AddWithValue("@Date", attendance.Date);
                cmd.Parameters.AddWithValue("@ClockInTime", attendance.ClockInTime);

                // 👇 Xử lý ClockOutTime nếu là DateTime.MinValue
                if (attendance.ClockOutTime < new DateTime(1753, 1, 1))
                    cmd.Parameters.AddWithValue("@ClockOutTime", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ClockOutTime", attendance.ClockOutTime);

                cmd.Parameters.AddWithValue("@Status", attendance.Status.ToString());
                cmd.Parameters.AddWithValue("@IsAbsentRecord", attendance.IsAbsentRecord);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return attendance;
        }


        public void UpdateClockOut(string attendanceId)
        {
            DateTime clockOut = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Attendance SET ClockOutTime = @ClockOutTime WHERE AttendanceId = @AttendanceId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ClockOutTime", clockOut);
                cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Attendance attendance = GetAttendanceById(attendanceId.ToString());
            UpdateContractEmployeeHours(attendance);
        }


        private Attendance GetAttendanceById(string attendanceId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Attendance WHERE AttendanceId = @AttendanceId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Attendance
                    {
                        AttendanceId = reader["AttendanceId"].ToString(),
                        EmployeeId = reader["EmployeeId"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        ClockInTime = Convert.ToDateTime(reader["ClockInTime"]),
                        ClockOutTime = Convert.ToDateTime(reader["ClockOutTime"]),
                        Status = Enum.Parse<AttendanceStatus>(reader["Status"].ToString()),
                        IsAbsentRecord = Convert.ToBoolean(reader["IsAbsentRecord"])
                    };
                }
            }

            throw new EntityNotFoundException("Attendance record not found.");
        }

        private bool HasAttendanceToday(string employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Attendance WHERE EmployeeId = @EmployeeId AND CAST(Date AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private string GenerateNewAttendanceId()
        {
            int maxId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(CAST(SUBSTRING(AttendanceId, 4, LEN(AttendanceId) - 3) AS INT)) FROM Attendance WHERE AttendanceId LIKE 'ATT%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    maxId = Convert.ToInt32(result);
                }
            }
            return $"ATT{(maxId + 1):D3}";
        }

        private void UpdateContractEmployeeHours(Attendance attendance)
        {
            if (attendance.ClockInTime == DateTime.MinValue || attendance.ClockOutTime == DateTime.MinValue)
                return;

            EmployeeService employeeService = new EmployeeService(KetNoi.str);
            Employee? employee = employeeService.GetByEmployeeId(attendance.EmployeeId);

            if (employee == null || employee.EmployeeType != "Contract") return;

            TimeSpan workedTime = attendance.ClockOutTime - attendance.ClockInTime;
            decimal hours = (decimal)workedTime.TotalHours;

            if (employee is ContractEmployee ce)
            {
                ce.HoursWorked += hours;
                employeeService.Update(ce);
            }
        }

        public List<Attendance> GetMonthlyAttendance(int year, int month)
        {
            List<Attendance> result = new List<Attendance>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Attendance WHERE YEAR(Date) = @Year AND MONTH(Date) = @Month";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Month", month);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Attendance
                    {
                        AttendanceId = reader["AttendanceId"].ToString(),
                        EmployeeId = reader["EmployeeId"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        ClockInTime = reader["ClockInTime"] != DBNull.Value
    ? Convert.ToDateTime(reader["ClockInTime"])
    : DateTime.MinValue, // hoặc throw nếu ClockInTime không bao giờ null

                        ClockOutTime = reader["ClockOutTime"] != DBNull.Value
    ? Convert.ToDateTime(reader["ClockOutTime"])
    : DateTime.MinValue
,
                        Status = Enum.Parse<AttendanceStatus>(reader["Status"].ToString()),
                        IsAbsentRecord = Convert.ToBoolean(reader["IsAbsentRecord"])
                    });
                }
            }

            return result;
        }

        public Dictionary<string, int> GetAttendanceSummary(int year, int month)
        {
            var summary = new Dictionary<string, int>();
            var records = GetMonthlyAttendance(year, month);

            foreach (var item in records)
            {
                string key = item.Status.ToString();
                if (!summary.ContainsKey(key)) summary[key] = 0;
                summary[key]++;
            }

            return summary;
        }

        public List<Attendance> GetEmployeeAttendance(string employeeId, int year, int month)
        {
            return GetMonthlyAttendance(year, month).FindAll(a => a.EmployeeId == employeeId);
        }

        public List<Attendance> GetDailyAttendance(DateTime date)
        {
            List<Attendance> result = new List<Attendance>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Attendance WHERE CAST(Date AS DATE) = @Date";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Date", date.Date);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Attendance
                    {
                        AttendanceId = reader["AttendanceId"].ToString(),
                        EmployeeId = reader["EmployeeId"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        ClockInTime = reader["ClockInTime"] != DBNull.Value
    ? Convert.ToDateTime(reader["ClockInTime"])
    : DateTime.MinValue, // hoặc throw nếu ClockInTime không bao giờ null

                        ClockOutTime = reader["ClockOutTime"] != DBNull.Value
                ? Convert.ToDateTime(reader["ClockOutTime"])
                : DateTime.MinValue,

                        Status = Enum.Parse<AttendanceStatus>(reader["Status"].ToString()),
                        IsAbsentRecord = Convert.ToBoolean(reader["IsAbsentRecord"])
                    });
                }
            }

            return result;
        }

        public List<Attendance> GetEmployeeDailyAttendance(string employeeId, DateTime date)
        {
            return GetDailyAttendance(date).FindAll(a => a.EmployeeId == employeeId);
        }

        public static TimeSpan GetWorkStartTime() => new TimeSpan(9, 0, 0);
        public static TimeSpan GetWorkEndTime() => new TimeSpan(17, 30, 0);
    }
}
