namespace HRManagementSystem
{
    public partial class EditEmployeeForm : Form
    {
        private Employee _employee;
        private List<Department> _departments;
        private bool _isNewEmployee;
        private EmployeeFactory _employeeFactory = new EmployeeFactory();
        private TextBox txtEmployeeId;
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private DateTimePicker dtpDob;
        private TextBox txtAddress;  // ← THÊM DÒNG NÀY
        private ComboBox cmbDepartment;
        private TextBox txtPosition;
        private DateTimePicker dtpHireDate;
        private ComboBox cmbStatus;
        private NumericUpDown nudSalary;
        private ComboBox cmbEmployeeType;
        private NumericUpDown nudAnnualBonus;
        private NumericUpDown nudHourlyRate;
        private NumericUpDown nudHoursWorked;
        // Property to access the updated employee after the form closes
        public Employee UpdatedEmployee => _employee;

        public EditEmployeeForm(Employee employee, List<Department> departments, bool isNewEmployee = false)
        {
            _employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _departments = departments ?? throw new ArgumentNullException(nameof(departments));
            _isNewEmployee = isNewEmployee;

            if (_isNewEmployee)
            {
                if (_employee.DateOfBirth == DateTime.MinValue)
                    _employee.DateOfBirth = DateTime.Today.AddYears(-25); // Default: 25 tuổi

                if (_employee.HireDate == DateTime.MinValue)
                    _employee.HireDate = DateTime.Today; // Default: hôm nay
            } 
            InitializeComponent();
            LoadEmployeeData();
        }

        private void InitializeComponent()
        {
            Text = _isNewEmployee ? "Add New Employee" : "Edit Employee";
            Size = new Size(600, 700);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Padding = new Padding(20);

            // Main container - Tab Control
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10),
                Margin = new Padding(10)
            };

            // Personal Information Tab
            TabPage tabPersonal = new TabPage
            {
                Text = "Personal Information",
                Padding = new Padding(10)
            };

            // Employment Tab
            TabPage tabEmployment = new TabPage
            {
                Text = "Employment Details",
                Padding = new Padding(10)
            };

            tabControl.TabPages.Add(tabPersonal);
            tabControl.TabPages.Add(tabEmployment);
            Controls.Add(tabControl);

            // Personal Information Panel
            TableLayoutPanel personalPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7,
                Padding = new Padding(5),
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Set row styles for personal panel
            for (int i = 0; i < 7; i++)
            {
                personalPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            tabPersonal.Controls.Add(personalPanel);

            // Employee ID field
            Label lblEmployeeId = new Label
            {
                Text = "Employee ID:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            personalPanel.Controls.Add(lblEmployeeId, 0, 0);

            TextBox txtEmployeeId = new TextBox
            {
                Name = "txtEmployeeId",
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10),
                ReadOnly = true
            };
            personalPanel.Controls.Add(txtEmployeeId, 1, 0);

            // Name field
            Label lblName = new Label
            {
                Text = "Full Name:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            personalPanel.Controls.Add(lblName, 0, 1);

            TextBox txtName = new TextBox
            {
                Name = "txtName",
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };
            personalPanel.Controls.Add(txtName, 1, 1);

            // Email field
            Label lblEmail = new Label
            {
                Text = "Email:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            personalPanel.Controls.Add(lblEmail, 0, 2);

            TextBox txtEmail = new TextBox
            {
                Name = "txtEmail",
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };
            personalPanel.Controls.Add(txtEmail, 1, 2);

            // Phone field
            Label lblPhone = new Label
            {
                Text = "Phone:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            personalPanel.Controls.Add(lblPhone, 0, 3);

            TextBox txtPhone = new TextBox
            {
                Name = "txtPhone",
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };
            personalPanel.Controls.Add(txtPhone, 1, 3);

            // Date of Birth field
            Label lblDob = new Label
            {
                Text = "Date of Birth:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            personalPanel.Controls.Add(lblDob, 0, 4);

            DateTimePicker dtpDob = new DateTimePicker
            {
                Name = "dtpDob",
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Short,
                Margin = new Padding(0, 10, 0, 10)
            };
            personalPanel.Controls.Add(dtpDob, 1, 4);

            // Address field
            Label lblAddress = new Label
            {
                Text = "Address:",
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            personalPanel.Controls.Add(lblAddress, 0, 5);

            txtAddress = new TextBox
            {
                Name = "txtAddress",
                Dock = DockStyle.Fill,
                Multiline = true,
                Height = 80,
                Margin = new Padding(0, 10, 0, 10)
            };
            personalPanel.Controls.Add(txtAddress, 1, 5);

            // Employment Panel
            TableLayoutPanel employmentPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 9, // Increased row count for employee type fields
                Padding = new Padding(5),
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Set row styles for employment panel
            for (int i = 0; i < 9; i++) // Increased loop count
            {
                employmentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            tabEmployment.Controls.Add(employmentPanel);

            // Department field
            Label lblDepartment = new Label
            {
                Text = "Department:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            employmentPanel.Controls.Add(lblDepartment, 0, 0);

            ComboBox cmbDepartment = new ComboBox
            {
                Name = "cmbDepartment",
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 10, 0, 10)
            };
            employmentPanel.Controls.Add(cmbDepartment, 1, 0);

            // Position field
            Label lblPosition = new Label
            {
                Text = "Position:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            employmentPanel.Controls.Add(lblPosition, 0, 1);

            TextBox txtPosition = new TextBox
            {
                Name = "txtPosition",
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };
            employmentPanel.Controls.Add(txtPosition, 1, 1);

            // Hire Date field
            Label lblHireDate = new Label
            {
                Text = "Hire Date:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            employmentPanel.Controls.Add(lblHireDate, 0, 2);

            DateTimePicker dtpHireDate = new DateTimePicker
            {
                Name = "dtpHireDate",
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Short,
                Margin = new Padding(0, 10, 0, 10)
            };
            employmentPanel.Controls.Add(dtpHireDate, 1, 2);

            // Status field
            Label lblStatus = new Label
            {
                Text = "Status:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            employmentPanel.Controls.Add(lblStatus, 0, 3);

            ComboBox cmbStatus = new ComboBox
            {
                Name = "cmbStatus",
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 10, 0, 10)
            };
            // Add status options
            cmbStatus.Items.AddRange(Enum.GetNames(typeof(EmployeeStatus)));
            employmentPanel.Controls.Add(cmbStatus, 1, 3);

            // Base Salary field
            Label lblSalary = new Label
            {
                Text = "Base Salary:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            employmentPanel.Controls.Add(lblSalary, 0, 4);

            NumericUpDown nudSalary = new NumericUpDown
            {
                Name = "nudSalary",
                Dock = DockStyle.Fill,
                Maximum = 1000000,
                Increment = 500,
                ThousandsSeparator = true,
                DecimalPlaces = 2,
                Margin = new Padding(0, 10, 0, 10)
            };
            employmentPanel.Controls.Add(nudSalary, 1, 4);

            // Employee Type field
            Label lblEmployeeType = new Label
            {
                Text = "Employee Type:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            employmentPanel.Controls.Add(lblEmployeeType, 0, 5);

            ComboBox cmbEmployeeType = new ComboBox
            {
                Name = "cmbEmployeeType",
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 10, 0, 10)
            };
            cmbEmployeeType.Items.AddRange(new object[] { "Regular", "FullTime", "Contract" });
            cmbEmployeeType.SelectedIndexChanged += CmbEmployeeType_SelectedIndexChanged;
            employmentPanel.Controls.Add(cmbEmployeeType, 1, 5);

            // Annual Bonus field (for FullTime employees)
            Label lblAnnualBonus = new Label
            {
                Name = "lblAnnualBonus",
                Text = "Annual Bonus:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            employmentPanel.Controls.Add(lblAnnualBonus, 0, 6);

            NumericUpDown nudAnnualBonus = new NumericUpDown
            {
                Name = "nudAnnualBonus",
                Dock = DockStyle.Fill,
                Maximum = 100000,
                Increment = 1000,
                ThousandsSeparator = true,
                DecimalPlaces = 2,
                Margin = new Padding(0, 10, 0, 10),
                Visible = false
            };
            employmentPanel.Controls.Add(nudAnnualBonus, 1, 6);

            // Hourly Rate field (for Contract employees)
            Label lblHourlyRate = new Label
            {
                Name = "lblHourlyRate",
                Text = "Hourly Rate:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            employmentPanel.Controls.Add(lblHourlyRate, 0, 7);

            NumericUpDown nudHourlyRate = new NumericUpDown
            {
                Name = "nudHourlyRate",
                Dock = DockStyle.Fill,
                Maximum = 1000,
                Increment = 5,
                ThousandsSeparator = true,
                DecimalPlaces = 2,
                Margin = new Padding(0, 10, 0, 10),
                Visible = false
            };
            employmentPanel.Controls.Add(nudHourlyRate, 1, 7);

            // Hours Worked field (for Contract employees)
            Label lblHoursWorked = new Label
            {
                Name = "lblHoursWorked",
                Text = "Hours Worked:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            employmentPanel.Controls.Add(lblHoursWorked, 0, 8);

            NumericUpDown nudHoursWorked = new NumericUpDown
            {
                Name = "nudHoursWorked",
                Dock = DockStyle.Fill,
                Maximum = 500,
                Increment = 1,
                ThousandsSeparator = true,
                Margin = new Padding(0, 10, 0, 10),
                Visible = false
            };
            employmentPanel.Controls.Add(nudHoursWorked, 1, 8);

            // Bottom button panel
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10)
            };
            Controls.Add(buttonPanel);

            // Save button
            Button btnSave = new Button
            {
                Text = "Save",
                Size = new Size(100, 40),
                Location = new Point(buttonPanel.Width - 230, 10),
                BackColor = Color.FromArgb(68, 93, 233),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            buttonPanel.Controls.Add(btnSave);

            // Cancel button
            Button btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(100, 40),
                Location = new Point(buttonPanel.Width - 120, 10),
                BackColor = Color.FromArgb(160, 160, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            buttonPanel.Controls.Add(btnCancel);

            // Fill department dropdown
            foreach (Department department in _departments)
            {
                cmbDepartment.Items.Add(new DepartmentItem(department));
            }

            // Set Anchor for buttons so they stay in position when form resizes
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Adjust tab control to make room for buttons
            tabControl.Height = ClientSize.Height - buttonPanel.Height;
        }

        private void CmbEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbEmployeeType = sender as ComboBox;
            if (cmbEmployeeType == null) return;

            string selectedType = cmbEmployeeType.SelectedItem.ToString();

            // Get references to type-specific controls
            Label lblAnnualBonus = Controls.Find("lblAnnualBonus", true).FirstOrDefault() as Label;
            NumericUpDown nudAnnualBonus = Controls.Find("nudAnnualBonus", true).FirstOrDefault() as NumericUpDown;
            Label lblHourlyRate = Controls.Find("lblHourlyRate", true).FirstOrDefault() as Label;
            NumericUpDown nudHourlyRate = Controls.Find("nudHourlyRate", true).FirstOrDefault() as NumericUpDown;
            Label lblHoursWorked = Controls.Find("lblHoursWorked", true).FirstOrDefault() as Label;
            NumericUpDown nudHoursWorked = Controls.Find("nudHoursWorked", true).FirstOrDefault() as NumericUpDown;

            // Hide all type-specific fields first
            if (lblAnnualBonus != null) lblAnnualBonus.Visible = false;
            if (nudAnnualBonus != null) nudAnnualBonus.Visible = false;
            if (lblHourlyRate != null) lblHourlyRate.Visible = false;
            if (nudHourlyRate != null) nudHourlyRate.Visible = false;
            if (lblHoursWorked != null) lblHoursWorked.Visible = false;
            if (nudHoursWorked != null) nudHoursWorked.Visible = false;

            // Show only relevant fields for the selected type
            switch (selectedType)
            {
                case "FullTime":
                    if (lblAnnualBonus != null) lblAnnualBonus.Visible = true;
                    if (nudAnnualBonus != null) nudAnnualBonus.Visible = true;
                    break;
                case "Contract":
                    if (lblHourlyRate != null) lblHourlyRate.Visible = true;
                    if (nudHourlyRate != null) nudHourlyRate.Visible = true;
                    if (lblHoursWorked != null) lblHoursWorked.Visible = true;
                    if (nudHoursWorked != null) nudHoursWorked.Visible = true;
                    break;
                default: // Regular
                    // No additional fields needed
                    break;
            }
        }

        private void LoadEmployeeData()
        {
            // Load data into form controls
            if (_employee != null)
            {
                // ✅ DEBUG: Kiểm tra dữ liệu employee trước khi load
                System.Diagnostics.Debug.WriteLine($"[LOAD] Loading employee data:");
                System.Diagnostics.Debug.WriteLine($"  - ID: {_employee?.Id}");
                System.Diagnostics.Debug.WriteLine($"  - Name: {_employee?.Name}");
                System.Diagnostics.Debug.WriteLine($"  - Status: {_employee?.Status}");
                System.Diagnostics.Debug.WriteLine($"  - HireDate: {_employee?.HireDate}");
                System.Diagnostics.Debug.WriteLine($"  - Department: {_employee?.DepartmentName}"); 

                TextBox txtEmployeeId = Controls.Find("txtEmployeeId", true).FirstOrDefault() as TextBox;
                TextBox txtName = Controls.Find("txtName", true).FirstOrDefault() as TextBox;
                TextBox txtEmail = Controls.Find("txtEmail", true).FirstOrDefault() as TextBox;
                TextBox txtPhone = Controls.Find("txtPhone", true).FirstOrDefault() as TextBox;
                DateTimePicker dtpDob = Controls.Find("dtpDob", true).FirstOrDefault() as DateTimePicker;
                TextBox tdress = Controls.Find("txtAddress", true).FirstOrDefault() as TextBox;
                ComboBox cmbDepartment = Controls.Find("cmbDepartment", true).FirstOrDefault() as ComboBox;
                TextBox txtPosition = Controls.Find("txtPosition", true).FirstOrDefault() as TextBox;
                DateTimePicker dtpHireDate = Controls.Find("dtpHireDate", true).FirstOrDefault() as DateTimePicker;
                ComboBox cmbStatus = Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;
                NumericUpDown nudSalary = Controls.Find("nudSalary", true).FirstOrDefault() as NumericUpDown;

               // ✅ KIỂM TRA: Controls có được tìm thấy không
        if (txtName == null) System.Diagnostics.Debug.WriteLine("[LOAD ERROR] txtName not found!");
        if (dtpHireDate == null) System.Diagnostics.Debug.WriteLine("[LOAD ERROR] dtpHireDate not found!");
        if (cmbStatus == null) System.Diagnostics.Debug.WriteLine("[LOAD ERROR] cmbStatus not found!");

        // Load basic info
        if (txtEmployeeId != null) txtEmployeeId.Text = _employee.EmployeeId ?? "";
        if (txtName != null) txtName.Text = _employee.Name ?? "";
        if (txtEmail != null) txtEmail.Text = _employee.Email ?? "";
        if (txtPhone != null) txtPhone.Text = _employee.Phone ?? "";
        if (txtAddress != null) txtAddress.Text = _employee.Address ?? "";
        if (txtPosition != null) txtPosition.Text = _employee.Position ?? "";
        if (nudSalary != null) nudSalary.Value = _employee.BaseSalary;

                // ✅ SỬA: Load Date of Birth với validation
                if (dtpDob != null)
                {
                    DateTime dobToSet;
                    if (_employee.DateOfBirth == DateTime.MinValue ||
                        _employee.DateOfBirth < new DateTime(1753, 1, 1) ||
                        _employee.DateOfBirth > DateTime.Today)
                    {
                        dobToSet = DateTime.Today.AddYears(-25); // Default 25 tuổi
                        System.Diagnostics.Debug.WriteLine($"[LOAD] DOB invalid, set to default: {dobToSet}");
                    }
                    else
                    {
                        dobToSet = _employee.DateOfBirth;
                        System.Diagnostics.Debug.WriteLine($"[LOAD] DOB set to: {dobToSet}");
                    }
                    dtpDob.Value = dobToSet;
                }


                if (dtpHireDate != null)
                {
                    DateTime hireDateToSet;
                    if (_employee.HireDate == DateTime.MinValue ||
                        _employee.HireDate < new DateTime(1753, 1, 1) ||
                        _employee.HireDate > DateTime.Today.AddYears(10)) // Không quá 10 năm trong tương lai
                    {
                        hireDateToSet = DateTime.Today; // Default hôm nay
                        System.Diagnostics.Debug.WriteLine($"[LOAD] HireDate invalid, set to default: {hireDateToSet}");
                    }
                    else
                    {
                        hireDateToSet = _employee.HireDate;
                        System.Diagnostics.Debug.WriteLine($"[LOAD] HireDate set to: {hireDateToSet}");
                    }
                    dtpHireDate.Value = hireDateToSet;
                }

                // ✅ SỬA: Load Status
                if (cmbStatus != null)
                {
                    // Clear và populate lại status options
                    cmbStatus.Items.Clear();
                    cmbStatus.Items.AddRange(Enum.GetNames(typeof(EmployeeStatus)));

                    // Set selected status
                    string statusName = _employee.Status.ToString();
                    int statusIndex = cmbStatus.Items.IndexOf(statusName);
                    if (statusIndex >= 0)
                    {
                        cmbStatus.SelectedIndex = statusIndex;
                        System.Diagnostics.Debug.WriteLine($"[LOAD] Status set to: {statusName} (index {statusIndex})");
                    }
                    else
                    {
                        cmbStatus.SelectedIndex = 0; // Default to first status
                        System.Diagnostics.Debug.WriteLine($"[LOAD] Status set to default: {cmbStatus.Items[0]}");
                    }
                }

                // ✅ SỬA: Load Department
                if (cmbDepartment != null && !string.IsNullOrEmpty(_employee.DepartmentId))
                {
                    for (int i = 0; i < cmbDepartment.Items.Count; i++)
                    {
                        if (cmbDepartment.Items[i] is DepartmentItem item &&
                            item.Department.DepartmentId == _employee.DepartmentId)
                        {
                            cmbDepartment.SelectedIndex = i;
                            System.Diagnostics.Debug.WriteLine($"[LOAD] Department set to: {item.Department.Name}");
                            break;
                        }
                    }
                }


                // Handle employee type
                ComboBox cmbEmployeeType = Controls.Find("cmbEmployeeType", true).FirstOrDefault() as ComboBox;
                NumericUpDown nudAnnualBonus = Controls.Find("nudAnnualBonus", true).FirstOrDefault() as NumericUpDown;
                NumericUpDown nudHourlyRate = Controls.Find("nudHourlyRate", true).FirstOrDefault() as NumericUpDown;
                NumericUpDown nudHoursWorked = Controls.Find("nudHoursWorked", true).FirstOrDefault() as NumericUpDown;

                if (cmbEmployeeType != null)
                {
                    cmbEmployeeType.SelectedItem = _employee.EmployeeType;
                }

                // Load type-specific data
                if (_employee is FullTimeEmployee fullTimeEmployee && nudAnnualBonus != null)
                {
                    nudAnnualBonus.Value = Math.Min(fullTimeEmployee.AnnualBonus, nudAnnualBonus.Maximum);
                }
                else if (_employee is ContractEmployee contractEmployee)
                {
                    if (nudHourlyRate != null)
                    {
                        nudHourlyRate.Value = Math.Min(contractEmployee.HourlyRate, nudHourlyRate.Maximum);
                    }
                    if (nudHoursWorked != null)
                    {
                        nudHoursWorked.Value = Math.Min(contractEmployee.HoursWorked, nudHoursWorked.Maximum);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                {
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"[SAVE] Starting save process for Employee ID: {_employee?.Id}");

                // Get values from form controls
                TextBox txtName = Controls.Find("txtName", true).FirstOrDefault() as TextBox;
                TextBox txtEmail = Controls.Find("txtEmail", true).FirstOrDefault() as TextBox;
                TextBox txtPhone = Controls.Find("txtPhone", true).FirstOrDefault() as TextBox;

                // ✅ DEBUG: Kiểm tra giá trị từ form
                System.Diagnostics.Debug.WriteLine($"[SAVE] Form values:");
                System.Diagnostics.Debug.WriteLine($"  - Name from form: '{txtName?.Text}'");

                DateTimePicker dtpDob = Controls.Find("dtpDob", true).FirstOrDefault() as DateTimePicker;
                TextBox txtAddress = Controls.Find("txtAddress", true).FirstOrDefault() as TextBox;
                ComboBox cmbDepartment = Controls.Find("cmbDepartment", true).FirstOrDefault() as ComboBox;
                TextBox txtPosition = Controls.Find("txtPosition", true).FirstOrDefault() as TextBox;
                NumericUpDown nudSalary = Controls.Find("nudSalary", true).FirstOrDefault() as NumericUpDown;
                ComboBox cmbStatus = Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;
              
                DateTimePicker dtpHireDate = Controls.Find("dtpHireDate", true).FirstOrDefault() as DateTimePicker;
            

             

                ComboBox cmbEmployeeType = Controls.Find("cmbEmployeeType", true).FirstOrDefault() as ComboBox;
                string selectedType = cmbEmployeeType?.SelectedItem?.ToString() ?? "Regular";

                // Create a new employee using the factory
                Employee updatedEmployee = _employeeFactory.CreateEmployee(selectedType);
                updatedEmployee.Id = _employee.Id; // Retain the same ID for updates
                updatedEmployee.EmployeeId = _employee.EmployeeId; // Retain the same EmployeeId for updates

                updatedEmployee.Name = txtName?.Text ?? "";
                System.Diagnostics.Debug.WriteLine($"[SAVE] Name updated to: '{updatedEmployee.Name}'");

                updatedEmployee.Email = txtEmail?.Text ?? "";
                updatedEmployee.Phone = txtPhone?.Text ?? "";
                updatedEmployee.Address = txtAddress?.Text ?? "";
                updatedEmployee.Position = txtPosition?.Text ?? "";
                updatedEmployee.BaseSalary = nudSalary?.Value ?? 0;
                if (cmbStatus?.SelectedItem != null && Enum.TryParse(cmbStatus.SelectedItem.ToString(), out EmployeeStatus status))
                {
                    updatedEmployee.Status = status;
                }
                if (cmbDepartment?.SelectedItem is DepartmentItem selectedDept)
                {
                    updatedEmployee.DepartmentId = selectedDept.Department.DepartmentId;
                    updatedEmployee.DepartmentName = selectedDept.Department.Name;
                }
                // ✅ SỬA: Date handling với validation
                if (dtpDob != null)
                {
                    DateTime dobValue = dtpDob.Value;

                    // Kiểm tra DOB hợp lệ
                    if (dobValue < new DateTime(1900, 1, 1))
                    {
                        MessageBox.Show("Date of Birth cannot be before year 1900.", "Invalid Date",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dobValue > DateTime.Today)
                    {
                        MessageBox.Show("Date of Birth cannot be in the future.", "Invalid Date",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    updatedEmployee.DateOfBirth = dobValue;
                    System.Diagnostics.Debug.WriteLine($"[SAVE] DateOfBirth validated and set to: {dobValue}");
                }

               

                    updatedEmployee.HireDate = dtpHireDate?.Value ?? DateTime.Now;
                   
                // Handle type-specific properties
                if (updatedEmployee is FullTimeEmployee fullTimeEmployee)
                {
                    NumericUpDown nudAnnualBonus = Controls.Find("nudAnnualBonus", true).FirstOrDefault() as NumericUpDown;
                    if (nudAnnualBonus != null)
                    {
                        fullTimeEmployee.AnnualBonus = nudAnnualBonus.Value;
                        System.Diagnostics.Debug.WriteLine($"[SAVE] AnnualBonus updated to: {nudAnnualBonus.Value}");
                    }
                }
                else if (updatedEmployee is ContractEmployee contractEmployee)
                {
                    NumericUpDown nudHourlyRate = Controls.Find("nudHourlyRate", true).FirstOrDefault() as NumericUpDown;
                    NumericUpDown nudHoursWorked = Controls.Find("nudHoursWorked", true).FirstOrDefault() as NumericUpDown;

                    if (nudHourlyRate != null)
                    {
                        contractEmployee.HourlyRate = nudHourlyRate.Value;
                        System.Diagnostics.Debug.WriteLine($"[SAVE] HourlyRate updated to: {nudHourlyRate.Value}");
                    }

                    if (nudHoursWorked != null)
                    {
                        contractEmployee.HoursWorked = (int)nudHoursWorked.Value;
                        System.Diagnostics.Debug.WriteLine($"[SAVE] HoursWorked updated to: {nudHoursWorked.Value}");
                    }
                }

                // ✅ QUAN TRỌNG: Cập nhật _employee reference
                _employee = updatedEmployee;

                // ✅ DEBUG: Kiểm tra giá trị cuối cùng
                System.Diagnostics.Debug.WriteLine($"[SAVE] Final employee data:");
                System.Diagnostics.Debug.WriteLine($"  - ID: {_employee.Id}");
                System.Diagnostics.Debug.WriteLine($"  - Name: '{_employee.Name}'");
                System.Diagnostics.Debug.WriteLine($"  - Status: {_employee.Status}");
                System.Diagnostics.Debug.WriteLine($"  - HireDate: {_employee.HireDate}");
                System.Diagnostics.Debug.WriteLine($"  - Department: '{_employee.DepartmentName}'");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SAVE ERROR] {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[SAVE ERROR] Stack: {ex.StackTrace}");
                MessageBox.Show($"Error saving employee data:\n{ex.Message}",
                                "Save Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        private bool ValidateForm()
        {
            TextBox txtName = Controls.Find("txtName", true).FirstOrDefault() as TextBox;
            TextBox txtEmail = Controls.Find("txtEmail", true).FirstOrDefault() as TextBox;
            ComboBox cmbDepartment = Controls.Find("cmbDepartment", true).FirstOrDefault() as ComboBox;
            TextBox txtPosition = Controls.Find("txtPosition", true).FirstOrDefault() as TextBox;
            ComboBox cmbStatus = Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox;

            // Basic validation
            if (string.IsNullOrWhiteSpace(txtName?.Text))
            {
                MessageBox.Show("Employee name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName?.Focus();
                return false;
            }

            // Email validation (basic format check)
            if (!string.IsNullOrWhiteSpace(txtEmail?.Text) &&
                !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail?.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPosition?.Text))
            {
                MessageBox.Show("Position is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPosition?.Focus();
                return false;
            }

            if (cmbDepartment?.SelectedItem == null)
            {
                MessageBox.Show("Please select a department.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartment?.Focus();
                return false;
            }

            if (cmbStatus?.SelectedItem == null)
            {
                MessageBox.Show("Please select an employee status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus?.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Helper class for department dropdown
        private class DepartmentItem
        {
            public Department Department { get; }

            public DepartmentItem(Department department)
            {
                Department = department;
            }

            public override string ToString()
            {
                return $"{Department.DepartmentId} - {Department.Name}";
            }
        }
    }
}
