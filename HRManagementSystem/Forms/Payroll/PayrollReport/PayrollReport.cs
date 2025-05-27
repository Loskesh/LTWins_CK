using System.Data;

namespace HRManagementSystem
{
    public partial class PayrollReport : Form
    {
        private readonly PayrollService _payrollService;

        public PayrollReport(PayrollService payrollService)
        {
            InitializeComponent();
            _payrollService = payrollService ?? throw new ArgumentNullException(nameof(payrollService));

            InitializeControls();
        }

        private class EmployeeComparer : IComparer<object>
        {
            public int Compare(object x, object y)
            {
                string nameX = ((dynamic)x).Name?.ToString() ?? string.Empty;
                string nameY = ((dynamic)y).Name?.ToString() ?? string.Empty;
                return string.Compare(nameX, nameY, StringComparison.CurrentCulture);
            }
        }

        private void InitializeControls()
        {
            try
            {

                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpToDate.Value = DateTime.Now;

                cboPaymentStatus.Items.Clear();
                cboPaymentStatus.Items.Add("All");
                cboPaymentStatus.Items.Add("IsPaid");
                cboPaymentStatus.Items.Add("UnPaid");
                cboPaymentStatus.SelectedIndex = 0;


                ConfigureDataGridView();


                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo điều khiển: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {

                dgvPayroll.Columns.Clear();


                dgvPayroll.AutoGenerateColumns = false;


                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "EmployeeId",
                    DataPropertyName = "EmployeeId",
                    HeaderText = "EmployeeId",
                    Width = 80
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "EmployeeName",
                    DataPropertyName = "EmployeeName",
                    HeaderText = "EmployeeName",
                    Width = 150
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "PayPeriodStart",
                    DataPropertyName = "PayPeriodStart",
                    HeaderText = "PayPeriodStart",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "PayPeriodEnd",
                    DataPropertyName = "PayPeriodEnd",
                    HeaderText = "PayPeriodEnd",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "BaseSalary",
                    DataPropertyName = "BaseSalary",
                    HeaderText = "BaseSalary",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Allowances",
                    DataPropertyName = "Allowances",
                    HeaderText = "Allowances",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Deductions",
                    DataPropertyName = "Deductions",
                    HeaderText = "Deductions",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dgvPayroll.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NetSalary",
                    DataPropertyName = "NetSalary",
                    HeaderText = "NetSalary",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
                });

                dgvPayroll.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "IsPaid",
                    DataPropertyName = "IsPaid",
                    HeaderText = "IsPaid",
                    Width = 100
                });


                dgvPayroll.AllowUserToAddRows = false;
                dgvPayroll.AllowUserToDeleteRows = false;
                dgvPayroll.ReadOnly = true;
                dgvPayroll.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPayroll.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cấu hình DataGridView: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                List<Payroll> payrolls = _payrollService.GetAll();

                List<object> allEmployees = new List<object>();
                allEmployees.Add(new { Id = "", Name = "All Employee" });

                if (payrolls != null && payrolls.Count > 0)
                {
                    Dictionary<string, string> employeeMap = new Dictionary<string, string>();
                    foreach (Payroll payroll in payrolls)
                    {
                        if (!employeeMap.ContainsKey(payroll.EmployeeId))
                        {
                            employeeMap.Add(payroll.EmployeeId, payroll.EmployeeName ?? payroll.EmployeeId);
                        }
                    }

                    List<object> employees = new List<object>();
                    foreach (KeyValuePair<string, string> entry in employeeMap)
                    {
                        employees.Add(new { Id = entry.Key, Name = entry.Value });
                    }

                    employees.Sort(new EmployeeComparer());
                    allEmployees.AddRange(employees);
                }

                cboEmployee.DisplayMember = "Name";
                cboEmployee.ValueMember = "Id";
                cboEmployee.DataSource = allEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                List<object> defaultList = new List<object>();
                defaultList.Add(new { Id = "", Name = "All Employee" });

                cboEmployee.DisplayMember = "Name";
                cboEmployee.ValueMember = "Id";
                cboEmployee.DataSource = defaultList;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPayrollData();
        }

        private void LoadPayrollData()
        {
            try
            {

                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);
                string employeeId = cboEmployee.SelectedValue?.ToString() ?? "";
                bool? isPaid = null;

                if (cboPaymentStatus.SelectedIndex == 1)
                    isPaid = true;
                else if (cboPaymentStatus.SelectedIndex == 2)
                    isPaid = false;


                List<Payroll> allPayrolls = _payrollService.GetAll();

                if (allPayrolls == null || !allPayrolls.Any())
                {
                    MessageBox.Show("Không có dữ liệu phiếu lương nào trong hệ thống.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                    dgvPayroll.DataSource = null;


                    CalculateSummary(new List<Payroll>());

                    return;
                }


                List<Payroll> payrolls = allPayrolls
                    .Where(p => p.PayPeriodStart.Date >= fromDate && p.PayPeriodEnd.Date <= toDate)
                    .Where(p => string.IsNullOrEmpty(employeeId) || p.EmployeeId == employeeId)
                    .Where(p => isPaid == null || p.IsPaid == isPaid)
                    .ToList();


                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = payrolls;
                dgvPayroll.DataSource = bindingSource;


                CalculateSummary(payrolls);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu phiếu lương: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateSummary(IEnumerable<Payroll> payrolls)
        {
            try
            {
                if (payrolls == null || !payrolls.Any())
                {
                    lblTotalCount.Text = "0";
                    lblTotalBaseSalary.Text = "0";
                    lblTotalAllowances.Text = "0";
                    lblTotalDeductions.Text = "0";
                    lblTotalNetSalary.Text = "0";
                    return;
                }


                int totalCount = payrolls.Count();
                decimal totalBaseSalary = payrolls.Sum(p => p.BaseSalary);
                decimal totalAllowances = payrolls.Sum(p => p.Allowances);
                decimal totalDeductions = payrolls.Sum(p => p.Deductions);
                decimal totalNetSalary = payrolls.Sum(p => p.NetSalary);


                lblTotalCount.Text = totalCount.ToString();
                lblTotalBaseSalary.Text = totalBaseSalary.ToString("N0");
                lblTotalAllowances.Text = totalAllowances.ToString("N0");
                lblTotalDeductions.Text = totalDeductions.ToString("N0");
                lblTotalNetSalary.Text = totalNetSalary.ToString("N0");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Lỗi khi tính tổng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


                lblTotalCount.Text = "0";
                lblTotalBaseSalary.Text = "0";
                lblTotalAllowances.Text = "0";
                lblTotalDeductions.Text = "0";
                lblTotalNetSalary.Text = "0";
            }
        }

        private void dgvPayroll_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.Value == null) return;

            if (dgvPayroll.Columns[e.ColumnIndex].Name == "IsPaid" && dgvPayroll.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
            {
                if (e.Value is bool isPaid)
                {
                    e.Value = isPaid ? "IsPaid" : "UnPaid";
                    e.FormattingApplied = true;
                }
            }
        }

        private void PayrollReport_Load(object sender, EventArgs e)
        {

            LoadPayrollData();
        }
    }
}