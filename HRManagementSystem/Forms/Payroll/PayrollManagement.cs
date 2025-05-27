namespace HRManagementSystem
{
    public partial class PayrollManagement : Form
    {
        private readonly PayrollService _payrollService;
        private readonly EmployeeService _employeeService;

        private DateTime _currentMonth;
        private List<Payroll> _currentPayrolls;

        public PayrollManagement()
        {
            InitializeComponent();

            // Initialize both services with default constructors, no dependencies
            _payrollService = new PayrollService(KetNoi.str);
            _employeeService = new EmployeeService(KetNoi.str);

            _currentMonth = DateTime.Now;
            _currentPayrolls = new List<Payroll>();

            SetupDataGridView();
            UpdateMonthDisplay();
            LoadPayrollData();
        }

        // Keep the existing overloaded constructor that accepts a PayrollService
        // but don't create a new EmployeeService with FileManager dependency
        public PayrollManagement(PayrollService payrollService)
        {
            InitializeComponent();
            _payrollService = payrollService ?? throw new ArgumentNullException(nameof(payrollService));

            // Initialize EmployeeService with default constructor
            _employeeService = new EmployeeService(KetNoi.str);

            _currentMonth = DateTime.Now;
            _currentPayrolls = new List<Payroll>();

            SetupDataGridView();
            UpdateMonthDisplay();
            LoadPayrollData();
        }

        private void SetupDataGridView()
        {

            dgvPayrolls.AutoGenerateColumns = false;
            dgvPayrolls.Columns.Clear();

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = "PayrollId",
                HeaderText = "PayrollId",
                DataPropertyName = "PayrollId",
                Width = 100
            };

            DataGridViewTextBoxColumn employeeIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "EmployeeId",
                HeaderText = "EmployeeId",
                DataPropertyName = "EmployeeId",
                Width = 100
            };

            DataGridViewTextBoxColumn employeeNameColumn = new DataGridViewTextBoxColumn
            {
                Name = "EmployeeName",
                HeaderText = "EmployeeName",
                DataPropertyName = "EmployeeName",
                Width = 150
            };

            DataGridViewTextBoxColumn startDateColumn = new DataGridViewTextBoxColumn
            {
                Name = "PayPeriodStart",
                HeaderText = "PayPeriodStart",
                DataPropertyName = "PayPeriodStart",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            };

            DataGridViewTextBoxColumn endDateColumn = new DataGridViewTextBoxColumn
            {
                Name = "PayPeriodEnd",
                HeaderText = "PayPeriodEnd",
                DataPropertyName = "PayPeriodEnd",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            };

            DataGridViewTextBoxColumn baseSalaryColumn = new DataGridViewTextBoxColumn
            {
                Name = "BaseSalary",
                HeaderText = "BaseSalary",
                DataPropertyName = "BaseSalary",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            };

            DataGridViewTextBoxColumn allowancesColumn = new DataGridViewTextBoxColumn
            {
                Name = "Allowances",
                HeaderText = "Allowances",
                DataPropertyName = "Allowances",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            };

            DataGridViewTextBoxColumn deductionsColumn = new DataGridViewTextBoxColumn
            {
                Name = "Deductions",
                HeaderText = "Deductions",
                DataPropertyName = "Deductions",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            };

            DataGridViewTextBoxColumn netSalaryColumn = new DataGridViewTextBoxColumn
            {
                Name = "NetSalary",
                HeaderText = "NetSalary",
                DataPropertyName = "NetSalary",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            };

            DataGridViewCheckBoxColumn isPaidColumn = new DataGridViewCheckBoxColumn
            {
                Name = "IsPaid",
                HeaderText = "IsPaid",
                DataPropertyName = "IsPaid",
                Width = 80
            };


            dgvPayrolls.Columns.Add(idColumn);
            dgvPayrolls.Columns.Add(employeeIdColumn);
            dgvPayrolls.Columns.Add(employeeNameColumn);
            dgvPayrolls.Columns.Add(startDateColumn);
            dgvPayrolls.Columns.Add(endDateColumn);
            dgvPayrolls.Columns.Add(baseSalaryColumn);
            dgvPayrolls.Columns.Add(allowancesColumn);
            dgvPayrolls.Columns.Add(deductionsColumn);
            dgvPayrolls.Columns.Add(netSalaryColumn);
            dgvPayrolls.Columns.Add(isPaidColumn);
        }

        private void UpdateMonthDisplay()
        {
            lblMonth.Text = _currentMonth.ToString("MM/yyyy");
        }
        public class PayrollViewModel
        {
            public string PayrollId { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public DateTime PayPeriodStart { get; set; }
            public DateTime PayPeriodEnd { get; set; }
            public decimal BaseSalary { get; set; }
            public decimal Allowances { get; set; }
            public decimal Deductions { get; set; }
            public decimal NetSalary { get; set; }
            public bool IsPaid { get; set; }
        }
        private void LoadPayrollData()
        {
            try
            {
                _currentPayrolls = _payrollService.GetPayrollsByMonth(_currentMonth);
                List<PayrollViewModel> payrollViewModels = new List<PayrollViewModel>();

                foreach (Payroll payroll in _currentPayrolls)
                {
                    PayrollViewModel viewModel = new PayrollViewModel
                    {
                        PayrollId = payroll.PayrollId,
                        EmployeeId = payroll.EmployeeId,
                        EmployeeName = payroll.EmployeeName ?? "[Không tìm thấy]",
                        PayPeriodStart = payroll.PayPeriodStart,
                        PayPeriodEnd = payroll.PayPeriodEnd,
                        BaseSalary = payroll.BaseSalary,
                        Allowances = payroll.Allowances,
                        Deductions = payroll.Deductions,
                        NetSalary = payroll.NetSalary,
                        IsPaid = payroll.IsPaid
                    };
                    payrollViewModels.Add(viewModel);
                }

                dgvPayrolls.DataSource = null;
                dgvPayrolls.DataSource = payrollViewModels;
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateStatistics()
        {
            if (_currentPayrolls.Count > 0)
            {
                decimal totalSalary = _payrollService.CalculateTotalPayroll(_currentPayrolls);
                decimal avgSalary = _payrollService.CalculateAverageSalary(_currentPayrolls);
                decimal minSalary, maxSalary;
                _payrollService.GetSalaryRange(_currentPayrolls, out minSalary, out maxSalary);

                lblTotalPayroll.Text = $"Total: {totalSalary:N0} $";
                lblAverageSalary.Text = $"Average: {avgSalary:N0} $";
                lblSalaryRange.Text = $"Min-Max: {minSalary:N0} - {maxSalary:N0} $";
            }
            else
            {
                lblTotalPayroll.Text = "Total: 0 $";
                lblAverageSalary.Text = "Average: 0 $";
                lblSalaryRange.Text = "Min-Max: 0 - 0 $";
            }
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(-1);
            UpdateMonthDisplay();
            LoadPayrollData();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(1);
            UpdateMonthDisplay();
            LoadPayrollData();
        }

        private void btnNewPayroll_Click(object sender, EventArgs e)
        {

            PayrollForm payrollForm = new PayrollForm(_payrollService, _employeeService);
            if (payrollForm.ShowDialog() == DialogResult.OK)
            {
                LoadPayrollData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPayrolls.SelectedRows.Count > 0)
            {
                string payrollId = dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value.ToString();
                Payroll selectedPayroll = _payrollService.GetById(payrollId);

                if (selectedPayroll != null)
                {
                    PayrollForm payrollForm = new PayrollForm(_payrollService, _employeeService, selectedPayroll);
                    if (payrollForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadPayrollData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu lương để sửa", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPayrolls.SelectedRows.Count > 0)
            {
                string payrollId = dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa phiếu lương này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _payrollService.Delete(payrollId);
                        LoadPayrollData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa phiếu lương: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu lương để xóa", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvPayrolls.SelectedRows.Count > 0)
            {
                string payrollId = dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value.ToString();
                Payroll selectedPayroll = _payrollService.GetById(payrollId);

                if (selectedPayroll != null)
                {

                    MessageBox.Show("Chức năng in phiếu lương đang được phát triển", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu lương để in", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            PayrollReport reportForm = new PayrollReport(_payrollService);
            reportForm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            PayrollSearch searchForm = new PayrollSearch(_payrollService);
            searchForm.ShowDialog();
        }

        private void PayrollManagement_Load(object sender, EventArgs e)
        {



            LoadPayrollData();

        }

        private void btnRunPayroll_Click(object sender, EventArgs e)
        {
            // Confirm with the user before running payroll
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to mark all payroll records for {_currentMonth.ToString("MMMM yyyy")} as paid?\n\nThis action cannot be undone.",
                "Run Payroll Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool changesApplied = _payrollService.RunPayrollForMonth(_currentMonth);

                    if (changesApplied)
                    {
                        MessageBox.Show(
                            $"Payroll successfully processed for {_currentMonth.ToString("MMMM yyyy")}.",
                            "Payroll Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Refresh the data after running payroll
                        LoadPayrollData();
                    }
                    else
                    {
                        MessageBox.Show(
                            $"No unpaid payroll records found for {_currentMonth.ToString("MMMM yyyy")}.",
                            "No Changes Made",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error processing payroll: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}