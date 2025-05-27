using System.Reflection;

namespace HRManagementSystem
{
    public class Employee_PayrollView : Form
    {
        // Define delegate types
        public delegate void SearchEventHandler(object? sender, EventArgs e);
        public delegate void EmployeeSelectionChangedEventHandler(object? sender, EventArgs e);

        // Define events using the delegate types
        private event SearchEventHandler SearchRequested;
        private event EmployeeSelectionChangedEventHandler EmployeeSelectionChanged;

        private readonly PayrollService _payrollService;
        private readonly EmployeeService _employeeService;
        private string _selectedEmployeeName;
        private List<Payroll> _currentPayrolls;

        private Panel panelTop = null!;
        private Label lblTitle = null!;
        private Panel panelButtons = null!;
        private DataGridView dgvPayrolls = null!;
        private Panel panelBottom = null!;
        private Label lblSalaryRange = null!;
        private Label lblAverageSalary = null!;
        private Label lblTotalPayroll = null!;
        private Button btnSearch = null!;
        private Label label1 = null!;
        private ComboBox cboEmployee = null!;

        public Employee_PayrollView()
        {
            InitializeComponent();

            _payrollService = new PayrollService(KetNoi.str);
            _employeeService = new EmployeeService(KetNoi.str);
            _selectedEmployeeName = string.Empty;
            _currentPayrolls = new List<Payroll>();

            // Initialize and subscribe to events directly in constructor
            SearchEventHandler searchHandler = new SearchEventHandler(BtnSearch_Click);
            EmployeeSelectionChangedEventHandler selectionChangedHandler = new EmployeeSelectionChangedEventHandler(CboEmployee_SelectedIndexChanged);

            // Subscribe to our custom events
            SearchRequested += searchHandler;
            EmployeeSelectionChanged += selectionChangedHandler;

            // Wire up the UI controls to our event handlers
            btnSearch.Click += new EventHandler(BtnSearch_Click);
            cboEmployee.SelectedIndexChanged += new EventHandler(CboEmployee_SelectedIndexChanged);

            SetupDataGridView();
            LoadEmployeeNames();
        }

        // public Employee_PayrollView(PayrollService payrollService)
        // {
        //     InitializeComponent();
        //     _payrollService = payrollService ?? new PayrollService();
        //     _employeeService = new EmployeeService();
        //     _selectedEmployeeName = string.Empty;
        //     _currentPayrolls = new List<Payroll>();

        //     // Initialize and subscribe to events directly in constructor
        //     SearchEventHandler searchHandler = new SearchEventHandler(BtnSearch_Click);
        //     EmployeeSelectionChangedEventHandler selectionChangedHandler = new EmployeeSelectionChangedEventHandler(CboEmployee_SelectedIndexChanged);

        //     // Subscribe to our custom events
        //     SearchRequested += searchHandler;
        //     EmployeeSelectionChanged += selectionChangedHandler;

        //     // Wire up the UI controls to our event handlers
        //     btnSearch.Click += new EventHandler(SearchRequested);
        //     cboEmployee.SelectedIndexChanged += new EventHandler(EmployeeSelectionChanged);

        //     SetupDataGridView();
        //     LoadEmployeeNames();
        // }

        // Method to unsubscribe from events (call this in Dispose or when closing the form)

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblTitle = new Label();
            label1 = new Label();
            cboEmployee = new ComboBox();
            panelButtons = new Panel();
            btnSearch = new Button();
            dgvPayrolls = new DataGridView();
            panelBottom = new Panel();
            lblSalaryRange = new Label();
            lblAverageSalary = new Label();
            lblTotalPayroll = new Label();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayrolls).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 6, 4, 6);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1075, 105);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(472, 29);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(192, 39);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "My Payslip";
            lblTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(46, 26);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(104, 28);
            label1.TabIndex = 2;
            label1.Text = "Employee";
            // 
            // cboEmployee
            // 
            cboEmployee.FormattingEnabled = true;
            cboEmployee.Location = new Point(241, 26);
            cboEmployee.Margin = new Padding(4);
            cboEmployee.Name = "cboEmployee";
            cboEmployee.Size = new Size(265, 36);
            cboEmployee.TabIndex = 1;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(label1);
            panelButtons.Controls.Add(btnSearch);
            panelButtons.Controls.Add(cboEmployee);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(0, 105);
            panelButtons.Margin = new Padding(4, 6, 4, 6);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1075, 87);
            panelButtons.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(874, 23);
            btnSearch.Margin = new Padding(4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(129, 41);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += new EventHandler(BtnSearch_Click);
            // 
            // dgvPayrolls
            // 
            dgvPayrolls.AllowUserToAddRows = false;
            dgvPayrolls.AllowUserToDeleteRows = false;
            dgvPayrolls.BackgroundColor = Color.White;
            dgvPayrolls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayrolls.Dock = DockStyle.Fill;
            dgvPayrolls.Location = new Point(0, 192);
            dgvPayrolls.Margin = new Padding(4, 6, 4, 6);
            dgvPayrolls.MultiSelect = false;
            dgvPayrolls.Name = "dgvPayrolls";
            dgvPayrolls.ReadOnly = true;
            dgvPayrolls.RowHeadersWidth = 51;
            dgvPayrolls.RowTemplate.Height = 24;
            dgvPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayrolls.Size = new Size(1075, 439);
            dgvPayrolls.TabIndex = 2;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(lblSalaryRange);
            panelBottom.Controls.Add(lblAverageSalary);
            panelBottom.Controls.Add(lblTotalPayroll);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 631);
            panelBottom.Margin = new Padding(4, 6, 4, 6);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1075, 87);
            panelBottom.TabIndex = 3;
            // 
            // lblSalaryRange
            // 
            lblSalaryRange.AutoSize = true;
            lblSalaryRange.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSalaryRange.Location = new Point(785, 27);
            lblSalaryRange.Margin = new Padding(4, 0, 4, 0);
            lblSalaryRange.Name = "lblSalaryRange";
            lblSalaryRange.Size = new Size(171, 28);
            lblSalaryRange.TabIndex = 2;
            lblSalaryRange.Text = "Min-Max: 0 - 0 $";
            // 
            // lblAverageSalary
            // 
            lblAverageSalary.AutoSize = true;
            lblAverageSalary.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAverageSalary.ForeColor = Color.Black;
            lblAverageSalary.Location = new Point(427, 27);
            lblAverageSalary.Margin = new Padding(4, 0, 4, 0);
            lblAverageSalary.Name = "lblAverageSalary";
            lblAverageSalary.Size = new Size(131, 28);
            lblAverageSalary.TabIndex = 1;
            lblAverageSalary.Text = "Average: 0 $";
            // 
            // lblTotalPayroll
            // 
            lblTotalPayroll.AutoSize = true;
            lblTotalPayroll.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalPayroll.Location = new Point(61, 27);
            lblTotalPayroll.Margin = new Padding(4, 0, 4, 0);
            lblTotalPayroll.Name = "lblTotalPayroll";
            lblTotalPayroll.Size = new Size(100, 28);
            lblTotalPayroll.TabIndex = 0;
            lblTotalPayroll.Text = "Total: 0 $";
            // 
            // Employee_PayrollView
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1075, 718);
            Controls.Add(dgvPayrolls);
            Controls.Add(panelBottom);
            Controls.Add(panelButtons);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 6, 4, 6);
            Name = "Employee_PayrollView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Payslip";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayrolls).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        private void UnsubscribeEvents()
        {
            // Remove event handlers from controls
            btnSearch.Click -= new EventHandler(SearchRequested);
            cboEmployee.SelectedIndexChanged -= new EventHandler(EmployeeSelectionChanged);

            // Clear all event subscriptions
            SearchRequested -= SearchRequested;
            EmployeeSelectionChanged -= EmployeeSelectionChanged;
        }

        private void LoadEmployeeNames()
        {
            try
            {
                // Get payroll data
                List<Payroll> allPayrolls = _payrollService.GetAll();
                if (allPayrolls == null)
                {
                    allPayrolls = new List<Payroll>();
                }

                // Get employee data
                List<Employee> employees = _employeeService.GetAll();
                if (employees == null)
                {
                    employees = new List<Employee>();
                }

                // Create a list of unique employee names from both payrolls and employee list
                List<string> employeeNamesFromPayrolls = new List<string>();
                foreach (Payroll payroll in allPayrolls)
                {
                    if (!string.IsNullOrEmpty(payroll.EmployeeName) &&
                        !employeeNamesFromPayrolls.Contains(payroll.EmployeeName))
                    {
                        employeeNamesFromPayrolls.Add(payroll.EmployeeName);
                    }
                }

                List<string> employeeNamesFromEmployees = new List<string>();
                foreach (Employee employee in employees)
                {
                    if (!string.IsNullOrEmpty(employee.Name) &&
                        !employeeNamesFromEmployees.Contains(employee.Name))
                    {
                        employeeNamesFromEmployees.Add(employee.Name);
                    }
                }

                // Combine both lists and remove duplicates
                List<string> employeeNames = new List<string>();
                foreach (string name in employeeNamesFromPayrolls)
                {
                    if (!employeeNames.Contains(name))
                    {
                        employeeNames.Add(name);
                    }
                }

                foreach (string name in employeeNamesFromEmployees)
                {
                    if (!employeeNames.Contains(name))
                    {
                        employeeNames.Add(name);
                    }
                }

                // Sort the list
                employeeNames.Sort();

                var bs = new BindingSource();
                bs.DataSource = employees;
                // Chèn placeholder vào đầu BindingSource
                bs.Insert(0, new Employee { EmployeeId = "-- Choose Employee --", Name = "-- Choose Employee --" });

                cboEmployee.DisplayMember = "EmployeeId";
                cboEmployee.ValueMember = "EmployeeId";
                cboEmployee.DataSource = bs;
                cboEmployee.SelectedIndex = 0;



                // Register event for ComboBox
                if (!IsEventHandlerRegistered(cboEmployee, "SelectedIndexChanged", new EventHandler(CboEmployee_SelectedIndexChanged)))
                {
                    cboEmployee.SelectedIndexChanged += new EventHandler(CboEmployee_SelectedIndexChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to check if event handler is already registered
        private bool IsEventHandlerRegistered(Control control, string eventName, Delegate eventHandler)
        {
            if (control == null || string.IsNullOrEmpty(eventName) || eventHandler == null)
                return false;

            FieldInfo? field = typeof(Control).GetField(eventName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                return false;

            object? eventField = field.GetValue(control);
            if (eventField == null)
                return false;

            Delegate[] delegates = ((Delegate)eventField).GetInvocationList();
            return delegates.Contains(eventHandler);
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

        private void LoadPayrollData()
        {
            try
            {
                if (cboEmployee.Text.Equals("-- Choose Employee --"))
                {
                    dgvPayrolls.DataSource = null;
                    lblTotalPayroll.Text = "Total: 0 $";
                    lblAverageSalary.Text = "Average: 0 $";
                    lblSalaryRange.Text = "Min-Max: 0 - 0 $";
                    MessageBox.Show("Vui lòng chọn nhân viên để xem phiếu lương", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _currentPayrolls = _payrollService.GetPayrollsByEmployee(id);
                if (_currentPayrolls == null)
                {
                    _currentPayrolls = new List<Payroll>();
                }

                if (_currentPayrolls.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy phiếu lương cho nhân viên: {_selectedEmployeeName}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvPayrolls.DataSource = null;
                    lblTotalPayroll.Text = "Total: 0 $";
                    lblAverageSalary.Text = "Average: 0 $";
                    lblSalaryRange.Text = "Min-Max: 0 - 0 $";
                    return;
                }

                // Use the payroll objects directly instead of creating view models
                dgvPayrolls.DataSource = null;
                dgvPayrolls.DataSource = _currentPayrolls;
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
        private string id;
        private void CboEmployee_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboEmployee.SelectedIndex > 0 && cboEmployee.SelectedItem != null)
            {
                _selectedEmployeeName = cboEmployee.SelectedItem.ToString() ?? string.Empty;
                id = cboEmployee.SelectedValue.ToString();
            }
            else
            {
                _selectedEmployeeName = string.Empty;
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            LoadPayrollData();
        }

        private void btnNewPayroll_Click(object? sender, EventArgs e)
        {
            PayrollForm payrollForm = new PayrollForm(_payrollService, _employeeService);
            if (payrollForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeNames();
                LoadPayrollData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPayrolls.SelectedRows.Count > 0 && dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value != null)
            {
                string payrollId = dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(payrollId))
                {
                    MessageBox.Show("Invalid payroll ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
            if (dgvPayrolls.SelectedRows.Count > 0 && dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value != null)
            {
                string payrollId = dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(payrollId))
                {
                    MessageBox.Show("Invalid payroll ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
            if (dgvPayrolls.SelectedRows.Count > 0 && dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value != null)
            {
                string payrollId = dgvPayrolls.SelectedRows[0].Cells["PayrollId"].Value.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(payrollId))
                {
                    MessageBox.Show("Invalid payroll ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

        private void PayrollManagement_Load(object sender, EventArgs e)
        {
            LoadEmployeeNames();
        }
    }
}