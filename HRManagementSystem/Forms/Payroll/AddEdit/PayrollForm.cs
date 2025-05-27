namespace HRManagementSystem
{
    public partial class PayrollForm : Form
    {
        private readonly PayrollService _payrollService;
        private readonly EmployeeService _employeeService;
        private Payroll _payroll;
        private bool _isEditMode = false;
        private Employee _selectedEmployee;
        private bool _isEmployeeTypeLoaded = false;
        private Label lblEmployeeType;
        private Label lblEmployeeTypeValue;
        private Label lblSalaryBreakdown;
        private TextBox txtSalaryBreakdown;

        public PayrollForm(PayrollService payrollService, EmployeeService employeeService)
        {
            InitializeComponent();
            _payrollService = payrollService;
            _employeeService = employeeService;
            _payroll = new Payroll();

            SetupForm();
            InitializeNewLabels();
        }

        public PayrollForm(PayrollService payrollService, EmployeeService employeeService, Payroll payroll)
        {
            InitializeComponent();
            _payrollService = payrollService;
            _employeeService = employeeService;
            _payroll = payroll;
            _isEditMode = true;

            SetupForm();
            InitializeNewLabels();
            LoadPayrollData();
        }

        private void InitializeNewLabels()
        {
            // Resize the form to accommodate the new controls
            this.Height += 200;

            // Add employee type labels
            lblEmployeeType = new Label();
            lblEmployeeType.Name = "lblEmployeeType";
            lblEmployeeType.Text = "Employee Type:";
            lblEmployeeType.Location = new Point(groupBox3.Location.X + 22, groupBox3.Location.Y + groupBox3.Height + 20);
            lblEmployeeType.AutoSize = true;
            Controls.Add(lblEmployeeType);

            lblEmployeeTypeValue = new Label();
            lblEmployeeTypeValue.Name = "lblEmployeeTypeValue";
            lblEmployeeTypeValue.Text = "N/A";
            lblEmployeeTypeValue.Location = new Point(groupBox3.Location.X + 155, groupBox3.Location.Y + groupBox3.Height + 20);
            lblEmployeeTypeValue.AutoSize = true;
            lblEmployeeTypeValue.Font = new Font(lblEmployeeTypeValue.Font, FontStyle.Bold);
            lblEmployeeTypeValue.ForeColor = Color.Blue;
            Controls.Add(lblEmployeeTypeValue);

            // Add salary breakdown section
            lblSalaryBreakdown = new Label();
            lblSalaryBreakdown.Name = "lblSalaryBreakdown";
            lblSalaryBreakdown.Text = "Salary Calculation:";
            lblSalaryBreakdown.Location = new Point(groupBox3.Location.X + 22, groupBox3.Location.Y + groupBox3.Height + 50);
            lblSalaryBreakdown.AutoSize = true;
            Controls.Add(lblSalaryBreakdown);

            txtSalaryBreakdown = new TextBox();
            txtSalaryBreakdown.Name = "txtSalaryBreakdown";
            txtSalaryBreakdown.Multiline = true;
            txtSalaryBreakdown.ReadOnly = true;
            txtSalaryBreakdown.BackColor = Color.WhiteSmoke;
            txtSalaryBreakdown.BorderStyle = BorderStyle.FixedSingle;
            txtSalaryBreakdown.Location = new Point(groupBox3.Location.X + 22, groupBox3.Location.Y + groupBox3.Height + 75);
            txtSalaryBreakdown.Width = groupBox3.Width - 44;
            txtSalaryBreakdown.Height = 100;
            txtSalaryBreakdown.Font = new Font("Consolas", 9F, FontStyle.Regular);
            Controls.Add(txtSalaryBreakdown);

            // Move the buttons down to accommodate the new controls
            btnSave.Location = new Point(btnSave.Location.X, txtSalaryBreakdown.Location.Y + txtSalaryBreakdown.Height + 20);
            btnCancel.Location = new Point(btnCancel.Location.X, txtSalaryBreakdown.Location.Y + txtSalaryBreakdown.Height + 20);
        }

        private void LoadEmployeeList()
        {
            try
            {
                List<Employee> employees = _employeeService.GetAll();

                List<object> employeeList = new List<object>();
                foreach (Employee e in employees)
                {
                    employeeList.Add(new
                    {
                        Id = e.Id,
                        EmployeeId = e.EmployeeId,
                        Name = $"{e.Name}"
                    });
                }

                employeeList.Insert(0, new { Id = "", EmployeeId = "", Name = "-- Choose Employee --" });

                cboEmployee.DisplayMember = "Name";
                cboEmployee.ValueMember = "Id";
                cboEmployee.DataSource = employeeList;

                cboEmployee.SelectedIndexChanged += CboEmployee_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmployee.SelectedIndex > 0)
            {
                try
                {
                    dynamic selectedEmployee = cboEmployee.SelectedItem;
                    string id = selectedEmployee.Id;

                    _selectedEmployee = _employeeService.GetById(id);

                    if (_selectedEmployee != null)
                    {
                        SetupEmployeeTypeFields(_selectedEmployee);
                        txtBaseSalary.Text = _selectedEmployee.BaseSalary.ToString("N0");

                        // Update employee type display with visual emphasis
                        lblEmployeeTypeValue.Text = _selectedEmployee.EmployeeType;

                        // Set different colors based on employee type for better visibility
                        switch (_selectedEmployee.EmployeeType)
                        {
                            case "FullTime":
                                lblEmployeeTypeValue.ForeColor = Color.DarkGreen;
                                break;
                            case "Contract":
                                lblEmployeeTypeValue.ForeColor = Color.DarkBlue;
                                break;
                            default:
                                lblEmployeeTypeValue.ForeColor = Color.DarkOrange;
                                break;
                        }

                        // Calculate default allowance based on employee type
                        if (_selectedEmployee is FullTimeEmployee fullTimeEmp && !_isEditMode)
                        {
                            // For new payrolls, set initial allowance to include monthly bonus
                            decimal monthlyBonus = fullTimeEmp.AnnualBonus / 12;
                            txtAllowances.Text = monthlyBonus.ToString("N0");
                        }

                        CalculateNetSalary();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading employee data: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Reset when no employee is selected
                lblEmployeeTypeValue.Text = "N/A";
                lblEmployeeTypeValue.ForeColor = Color.Black;
                txtSalaryBreakdown.Text = "";
            }
        }

        private void SetupEmployeeTypeFields(Employee employee)
        {
            _isEmployeeTypeLoaded = false;

            if (employee is FullTimeEmployee fullTimeEmployee)
            {
                if (!Controls.ContainsKey("lblAnnualBonus"))
                {
                    Label lblAnnualBonus = new Label();
                    lblAnnualBonus.Name = "lblAnnualBonus";
                    lblAnnualBonus.Text = "Annual Bonus:";
                    lblAnnualBonus.Location = new Point(txtAllowances.Location.X, txtAllowances.Location.Y + 40);
                    Controls.Add(lblAnnualBonus);

                    TextBox txtAnnualBonus = new TextBox();
                    txtAnnualBonus.Name = "txtAnnualBonus";
                    txtAnnualBonus.Location = new Point(txtAllowances.Location.X + 120, txtAllowances.Location.Y + 40);
                    txtAnnualBonus.Width = txtAllowances.Width;
                    txtAnnualBonus.TextChanged += SalaryField_TextChanged;
                    Controls.Add(txtAnnualBonus);

                    Label lblMonthlyBonus = new Label();
                    lblMonthlyBonus.Name = "lblMonthlyBonus";
                    lblMonthlyBonus.Text = "Monthly Bonus:";
                    lblMonthlyBonus.Location = new Point(txtAllowances.Location.X + 350, txtAllowances.Location.Y + 40);
                    lblMonthlyBonus.AutoSize = true;
                    Controls.Add(lblMonthlyBonus);

                    Label lblMonthlyBonusValue = new Label();
                    lblMonthlyBonusValue.Name = "lblMonthlyBonusValue";
                    lblMonthlyBonusValue.Text = "0";
                    lblMonthlyBonusValue.Location = new Point(txtAllowances.Location.X + 450, txtAllowances.Location.Y + 40);
                    lblMonthlyBonusValue.Font = new Font(lblMonthlyBonusValue.Font, FontStyle.Bold);
                    lblMonthlyBonusValue.ForeColor = Color.Green;
                    lblMonthlyBonusValue.AutoSize = true;
                    Controls.Add(lblMonthlyBonusValue);
                }

                ((TextBox)Controls["txtAnnualBonus"]).Text = fullTimeEmployee.AnnualBonus.ToString("N0");
                ((Label)Controls["lblMonthlyBonusValue"]).Text = (fullTimeEmployee.AnnualBonus / 12).ToString("N0");

                if (Controls.ContainsKey("txtHourlyRate"))
                {
                    Controls["lblHourlyRate"].Visible = false;
                    Controls["txtHourlyRate"].Visible = false;
                    Controls["lblHoursWorked"].Visible = false;
                    Controls["txtHoursWorked"].Visible = false;
                }

                Controls["lblAnnualBonus"].Visible = true;
                Controls["txtAnnualBonus"].Visible = true;
                Controls["lblMonthlyBonus"].Visible = true;
                Controls["lblMonthlyBonusValue"].Visible = true;
            }
            else if (employee is ContractEmployee contractEmployee)
            {
                if (!Controls.ContainsKey("lblHourlyRate"))
                {
                    Label lblHourlyRate = new Label();
                    lblHourlyRate.Name = "lblHourlyRate";
                    lblHourlyRate.Text = "Hourly Rate:";
                    lblHourlyRate.Location = new Point(txtAllowances.Location.X, txtAllowances.Location.Y + 40);
                    Controls.Add(lblHourlyRate);

                    TextBox txtHourlyRate = new TextBox();
                    txtHourlyRate.Name = "txtHourlyRate";
                    txtHourlyRate.Location = new Point(txtAllowances.Location.X + 120, txtAllowances.Location.Y + 40);
                    txtHourlyRate.Width = txtAllowances.Width;
                    txtHourlyRate.TextChanged += SalaryField_TextChanged;
                    Controls.Add(txtHourlyRate);

                    Label lblHoursWorked = new Label();
                    lblHoursWorked.Name = "lblHoursWorked";
                    lblHoursWorked.Text = "Hours Worked:";
                    lblHoursWorked.Location = new Point(txtAllowances.Location.X, txtAllowances.Location.Y + 80);
                    Controls.Add(lblHoursWorked);

                    TextBox txtHoursWorked = new TextBox();
                    txtHoursWorked.Name = "txtHoursWorked";
                    txtHoursWorked.Location = new Point(txtAllowances.Location.X + 120, txtAllowances.Location.Y + 80);
                    txtHoursWorked.Width = txtAllowances.Width;
                    txtHoursWorked.TextChanged += SalaryField_TextChanged;
                    Controls.Add(txtHoursWorked);
                }

                ((TextBox)Controls["txtHourlyRate"]).Text = contractEmployee.HourlyRate.ToString("N0");
                ((TextBox)Controls["txtHoursWorked"]).Text = contractEmployee.HoursWorked.ToString();

                if (Controls.ContainsKey("txtAnnualBonus"))
                {
                    Controls["lblAnnualBonus"].Visible = false;
                    Controls["txtAnnualBonus"].Visible = false;
                    Controls["lblMonthlyBonus"].Visible = false;
                    Controls["lblMonthlyBonusValue"].Visible = false;
                }

                Controls["lblHourlyRate"].Visible = true;
                Controls["txtHourlyRate"].Visible = true;
                Controls["lblHoursWorked"].Visible = true;
                Controls["txtHoursWorked"].Visible = true;
            }
            else
            {
                if (Controls.ContainsKey("txtAnnualBonus"))
                {
                    Controls["lblAnnualBonus"].Visible = false;
                    Controls["txtAnnualBonus"].Visible = false;
                    Controls["lblMonthlyBonus"].Visible = false;
                    Controls["lblMonthlyBonusValue"].Visible = false;
                }

                if (Controls.ContainsKey("txtHourlyRate"))
                {
                    Controls["lblHourlyRate"].Visible = false;
                    Controls["txtHourlyRate"].Visible = false;
                    Controls["lblHoursWorked"].Visible = false;
                    Controls["txtHoursWorked"].Visible = false;
                }
            }

            _isEmployeeTypeLoaded = true;
        }

        private void SetupForm()
        {
            Text = _isEditMode ? "Update Payslip" : "Add Payslip";

            LoadEmployeeList();

            dtpPayPeriodStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPayPeriodEnd.Value = dtpPayPeriodStart.Value.AddMonths(1).AddDays(-1);

            txtBaseSalary.TextChanged += SalaryField_TextChanged;
            txtAllowances.TextChanged += SalaryField_TextChanged;
            txtDeductions.TextChanged += SalaryField_TextChanged;
        }

        private void LoadPayrollData()
        {
            for (int i = 0; i < cboEmployee.Items.Count; i++)
            {
                dynamic item = cboEmployee.Items[i];
                if (item.EmployeeId == _payroll.EmployeeId)
                {
                    cboEmployee.SelectedIndex = i;
                    break;
                }
            }

            cboEmployee.Enabled = false;

            dtpPayPeriodStart.Value = _payroll.PayPeriodStart;
            dtpPayPeriodEnd.Value = _payroll.PayPeriodEnd;
            txtBaseSalary.Text = _payroll.BaseSalary.ToString("N0");
            txtAllowances.Text = _payroll.Allowances.ToString("N0");
            txtDeductions.Text = _payroll.Deductions.ToString("N0");
            txtNetSalary.Text = _payroll.NetSalary.ToString("N0");
            chkIsPaid.Checked = _payroll.IsPaid;

            // Update employee type if employee is selected
            if (_selectedEmployee != null)
            {
                lblEmployeeTypeValue.Text = _selectedEmployee.EmployeeType;

                // Set different colors based on employee type
                switch (_selectedEmployee.EmployeeType)
                {
                    case "FullTime":
                        lblEmployeeTypeValue.ForeColor = Color.DarkGreen;
                        break;
                    case "Contract":
                        lblEmployeeTypeValue.ForeColor = Color.DarkBlue;
                        break;
                    default:
                        lblEmployeeTypeValue.ForeColor = Color.DarkOrange;
                        break;
                }

                CalculateNetSalary(); // This will update the salary breakdown
            }
        }

        private void SalaryField_TextChanged(object sender, EventArgs e)
        {
            CalculateNetSalary();

            // If the annual bonus field is changed, update the monthly bonus display
            if (sender is TextBox && ((TextBox)sender).Name == "txtAnnualBonus" && Controls.ContainsKey("lblMonthlyBonusValue"))
            {
                try
                {
                    decimal annualBonus = ParseCurrency(((TextBox)Controls["txtAnnualBonus"]).Text);
                    decimal monthlyBonus = annualBonus / 12;
                    ((Label)Controls["lblMonthlyBonusValue"]).Text = monthlyBonus.ToString("N0");
                }
                catch
                {
                    ((Label)Controls["lblMonthlyBonusValue"]).Text = "0";
                }
            }
        }

        private void CalculateNetSalary()
        {
            try
            {
                decimal baseSalary = ParseCurrency(txtBaseSalary.Text);
                decimal allowances = ParseCurrency(txtAllowances.Text);
                decimal deductions = ParseCurrency(txtDeductions.Text);
                decimal calculatedSalary = baseSalary;
                decimal monthlyBonus = 0;

                if (_selectedEmployee != null && _isEmployeeTypeLoaded)
                {
                    if (_selectedEmployee is FullTimeEmployee fullTimeEmployee && Controls.ContainsKey("txtAnnualBonus"))
                    {
                        decimal annualBonus = ParseCurrency(((TextBox)Controls["txtAnnualBonus"]).Text);
                        fullTimeEmployee.AnnualBonus = annualBonus;

                        monthlyBonus = annualBonus / 12;
                        ((Label)Controls["lblMonthlyBonusValue"]).Text = monthlyBonus.ToString("N0");

                        calculatedSalary = fullTimeEmployee.CalculateSalary();

                        txtSalaryBreakdown.Text = $"Full-Time Employee Salary:\n" +
                                                  $"- Base Salary: {baseSalary:N0}\n" +
                                                  $"- Monthly Bonus: {monthlyBonus:N0}\n" +
                                                  $"- Allowances: {allowances:N0}\n" +
                                                  $"- Deductions: {deductions:N0}";
                    }
                    else if (_selectedEmployee is ContractEmployee contractEmployee &&
                             Controls.ContainsKey("txtHourlyRate") &&
                             Controls.ContainsKey("txtHoursWorked"))
                    {
                        decimal hourlyRate = ParseCurrency(((TextBox)Controls["txtHourlyRate"]).Text);
                        int hoursWorked = int.TryParse(((TextBox)Controls["txtHoursWorked"]).Text, out int hw) ? hw : 0;

                        contractEmployee.HourlyRate = hourlyRate;
                        contractEmployee.HoursWorked = hoursWorked;

                        calculatedSalary = contractEmployee.CalculateSalary();

                        txtSalaryBreakdown.Text = $"Contract Employee Salary:\n" +
                                                  $"- Hourly Rate: {hourlyRate:N0}\n" +
                                                  $"- Hours Worked: {hoursWorked}\n" +
                                                  $"- Total: {calculatedSalary:N0}\n" +
                                                  $"- Allowances: {allowances:N0}\n" +
                                                  $"- Deductions: {deductions:N0}";
                    }
                    else
                    {
                        calculatedSalary = _selectedEmployee.CalculateSalary();

                        txtSalaryBreakdown.Text = $"Regular Employee Salary:\n" +
                                                  $"- Base Salary: {baseSalary:N0}\n" +
                                                  $"- Allowances: {allowances:N0}\n" +
                                                  $"- Deductions: {deductions:N0}";
                    }
                }
                else
                {
                    txtSalaryBreakdown.Text = "No employee selected.";
                }

                decimal netSalary = calculatedSalary + allowances - deductions;
                txtNetSalary.Text = netSalary.ToString("N0");
            }
            catch (Exception ex)
            {
                txtNetSalary.Text = "0";
                txtSalaryBreakdown.Text = $"Error: {ex.Message}";
            }
        }

        private decimal ParseCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            string numericValue = "";
            foreach (char c in value)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    numericValue += c;
                }
            }

            if (decimal.TryParse(numericValue, out decimal result))
                return result;

            return 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    dynamic selectedEmployee = cboEmployee.SelectedItem;

                    _payroll.EmployeeId = selectedEmployee.EmployeeId;
                    _payroll.EmployeeName = selectedEmployee.Name;
                    _payroll.PayPeriodStart = dtpPayPeriodStart.Value;
                    _payroll.PayPeriodEnd = dtpPayPeriodEnd.Value;
                    _payroll.BaseSalary = ParseCurrency(txtBaseSalary.Text);
                    _payroll.Allowances = ParseCurrency(txtAllowances.Text);
                    _payroll.Deductions = ParseCurrency(txtDeductions.Text);

                    if (_selectedEmployee != null)
                    {
                        if (_selectedEmployee is FullTimeEmployee fullTimeEmployee && Controls.ContainsKey("txtAnnualBonus"))
                        {
                            fullTimeEmployee.AnnualBonus = ParseCurrency(((TextBox)Controls["txtAnnualBonus"]).Text);
                        }
                        else if (_selectedEmployee is ContractEmployee contractEmployee &&
                                Controls.ContainsKey("txtHourlyRate") && Controls.ContainsKey("txtHoursWorked"))
                        {
                            contractEmployee.HourlyRate = ParseCurrency(((TextBox)Controls["txtHourlyRate"]).Text);
                            contractEmployee.HoursWorked = int.Parse(((TextBox)Controls["txtHoursWorked"]).Text);
                        }

                        _payroll.NetSalary = _selectedEmployee.CalculateSalary() + _payroll.Allowances - _payroll.Deductions;
                    }
                    else
                    {
                        _payroll.NetSalary = ParseCurrency(txtNetSalary.Text);
                    }

                    _payroll.IsPaid = chkIsPaid.Checked;

                    if (_isEditMode)
                    {
                        _payrollService.Update(_payroll);
                    }
                    else
                    {
                        _payrollService.Add(_payroll);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu phiếu lương: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (cboEmployee.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboEmployee.Focus();
                return false;
            }

            if (dtpPayPeriodEnd.Value < dtpPayPeriodStart.Value)
            {
                MessageBox.Show("Ngày kết thúc kỳ lương phải sau ngày bắt đầu", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpPayPeriodEnd.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBaseSalary.Text) || ParseCurrency(txtBaseSalary.Text) <= 0)
            {
                MessageBox.Show("Vui lòng nhập lương cơ bản hợp lệ", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBaseSalary.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dtpPayPeriodStart_ValueChanged(object sender, EventArgs e)
        {
            dtpPayPeriodEnd.Value = new DateTime(
                dtpPayPeriodStart.Value.Year,
                dtpPayPeriodStart.Value.Month,
                DateTime.DaysInMonth(dtpPayPeriodStart.Value.Year, dtpPayPeriodStart.Value.Month)
            );
        }
    }
}