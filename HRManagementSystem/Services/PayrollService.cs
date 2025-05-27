using System.Data.SqlClient;

namespace HRManagementSystem
{
    public class PayrollService : IService<Payroll>
    {
        private readonly string _connectionString;
        private readonly EmployeeService _employeeService;

        public PayrollService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _employeeService = new EmployeeService(connectionString);
        }

        public List<Payroll> GetAll()
        {
            var list = new List<Payroll>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"SELECT PayrollId, EmployeeId, EmployeeName, PayPeriodStart, PayPeriodEnd,
                                                     BaseSalary, Allowances, Deductions, NetSalary, IsPaid
                                              FROM Payroll", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(MapReaderToPayroll(rd));
                    }
                }
            }
            return list;
        }

        public Payroll GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"SELECT PayrollId, EmployeeId, EmployeeName, PayPeriodStart, PayPeriodEnd,
                                                    BaseSalary, Allowances, Deductions, NetSalary, IsPaid
                                             FROM Payroll
                                             WHERE PayrollId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                        return MapReaderToPayroll(rd);
                }
            }
            return null;
        }

        public List<Payroll> GetPayrollsByEmployee(string employeeId)
        {
            var list = new List<Payroll>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"SELECT PayrollId, EmployeeId, EmployeeName, PayPeriodStart, PayPeriodEnd,
                                                     BaseSalary, Allowances, Deductions, NetSalary, IsPaid
                                              FROM Payroll
                                              WHERE EmployeeId = @EmpId", conn))
            {
                cmd.Parameters.AddWithValue("@EmpId", employeeId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(MapReaderToPayroll(rd));
                }
            }
            return list;
        }

        public List<Payroll> GetPayrollsByMonth(DateTime month)
        {
            var list = new List<Payroll>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"SELECT PayrollId, EmployeeId, EmployeeName, PayPeriodStart, PayPeriodEnd,
                                                     BaseSalary, Allowances, Deductions, NetSalary, IsPaid
                                              FROM Payroll
                                              WHERE YEAR(PayPeriodStart) = @Y AND MONTH(PayPeriodStart) = @M", conn))
            {
                cmd.Parameters.AddWithValue("@Y", month.Year);
                cmd.Parameters.AddWithValue("@M", month.Month);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(MapReaderToPayroll(rd));
                }
            }
            return list;
        }

        public bool Add(Payroll p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        INSERT INTO Payroll
          (PayrollId, EmployeeId, EmployeeName, PayPeriodStart, PayPeriodEnd,
           BaseSalary, Allowances, Deductions, NetSalary, IsPaid)
        VALUES
          (@PayrollId, @EmployeeId, @EmployeeName, @PayPeriodStart, @PayPeriodEnd,
           @BaseSalary, @Allowances, @Deductions, @NetSalary, @IsPaid)", conn))
            {
                // Thêm tất cả các tham số, tên và thứ tự phải khớp với SQL
                cmd.Parameters.AddWithValue("@PayrollId", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@EmployeeId", p.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", p.EmployeeName);
                cmd.Parameters.AddWithValue("@PayPeriodStart", p.PayPeriodStart);
                cmd.Parameters.AddWithValue("@PayPeriodEnd", p.PayPeriodEnd);
                cmd.Parameters.AddWithValue("@BaseSalary", p.BaseSalary);
                cmd.Parameters.AddWithValue("@Allowances", p.Allowances);
                cmd.Parameters.AddWithValue("@Deductions", p.Deductions);
                cmd.Parameters.AddWithValue("@NetSalary", p.NetSalary);
                cmd.Parameters.AddWithValue("@IsPaid", p.IsPaid);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool Update(Payroll p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE Payroll SET
                  EmployeeId    = @EmployeeId,
                  EmployeeName  = @EmployeeName,
                  PayPeriodStart= @Start,
                  PayPeriodEnd  = @End,
                  BaseSalary    = @Base,
                  Allowances    = @Allow,
                  Deductions    = @Deduct,
                  NetSalary     = @Net,
                  IsPaid        = @IsPaid
                WHERE PayrollId = @PayrollId", conn))
            {
                cmd.Parameters.AddWithValue("@PayrollId", p.PayrollId);
                cmd.Parameters.AddWithValue("@EmployeeId", p.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", p.EmployeeName);
                cmd.Parameters.AddWithValue("@Start", p.PayPeriodStart);
                cmd.Parameters.AddWithValue("@End", p.PayPeriodEnd);
                cmd.Parameters.AddWithValue("@Base", p.BaseSalary);
                cmd.Parameters.AddWithValue("@Allow", p.Allowances);
                cmd.Parameters.AddWithValue("@Deduct", p.Deductions);
                cmd.Parameters.AddWithValue("@Net", p.NetSalary);
                cmd.Parameters.AddWithValue("@IsPaid", p.IsPaid);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM Payroll WHERE PayrollId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool RunPayrollForMonth(DateTime month)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE Payroll
                SET IsPaid = 1
                WHERE YEAR(PayPeriodStart) = @Y AND MONTH(PayPeriodStart) = @M AND IsPaid = 0", conn))
            {
                cmd.Parameters.AddWithValue("@Y", month.Year);
                cmd.Parameters.AddWithValue("@M", month.Month);
                conn.Open();
                int affected = cmd.ExecuteNonQuery();
                return affected > 0;
            }
        }

        //--- Các phương pháp tính toán ---
        public decimal CalculateNetSalary(decimal baseSalary, decimal allowances, decimal deductions)
            => baseSalary + allowances - deductions;

        public decimal CalculateTotalPayroll(List<Payroll> payrolls)
        {
            decimal total = 0;
            foreach (var p in payrolls) total += p.NetSalary;
            return total;
        }

        public decimal CalculateAverageSalary(List<Payroll> payrolls)
            => payrolls.Count == 0 ? 0 : CalculateTotalPayroll(payrolls) / payrolls.Count;

        public void GetSalaryRange(List<Payroll> payrolls, out decimal minSalary, out decimal maxSalary)
        {
            if (payrolls.Count == 0)
            {
                minSalary = maxSalary = 0;
                return;
            }

            minSalary = decimal.MaxValue;
            maxSalary = decimal.MinValue;
            foreach (var p in payrolls)
            {
                if (p.NetSalary < minSalary) minSalary = p.NetSalary;
                if (p.NetSalary > maxSalary) maxSalary = p.NetSalary;
            }
        }

        //--- Helper mapping ---
        private Payroll MapReaderToPayroll(SqlDataReader rd)
        {
            return new Payroll
            {
                PayrollId = rd["PayrollId"].ToString(),
                EmployeeId = rd["EmployeeId"].ToString(),
                EmployeeName = rd["EmployeeName"].ToString(),
                PayPeriodStart = Convert.ToDateTime(rd["PayPeriodStart"]),
                PayPeriodEnd = Convert.ToDateTime(rd["PayPeriodEnd"]),
                BaseSalary = Convert.ToDecimal(rd["BaseSalary"]),
                Allowances = Convert.ToDecimal(rd["Allowances"]),
                Deductions = Convert.ToDecimal(rd["Deductions"]),
                NetSalary = Convert.ToDecimal(rd["NetSalary"]),
                IsPaid = Convert.ToBoolean(rd["IsPaid"])
            };
        }
    }
}
