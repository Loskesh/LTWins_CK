namespace HRManagementSystem
{
    // New dialog class for department editing
    public class DepartmentEditDialog : Form
    {
        private Department _department;
        private List<Employee> _employees;
        private bool _isNew;

        private TextBox txtDepartmentId;
        private TextBox txtName;
        private TextBox txtDescription;
        private NumericUpDown nudBudget;
        private ComboBox cmbManager;
        private TextBox txtManagerName;
        private Button btnSave;
        private Button btnCancel;

        public Department Department => _department;

        public DepartmentEditDialog(Department department, List<Employee> employees, bool isNew)
        {
            _department = department ?? throw new ArgumentNullException(nameof(department));
            _employees = employees ?? throw new ArgumentNullException(nameof(employees));
            _isNew = isNew;

            InitializeComponent();
            LoadDepartmentData();
        }

        private void InitializeComponent()
        {
            Text = _isNew ? "Add New Department" : "Edit Department";
            Size = new Size(500, 500); // Increased height to accommodate layout
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Padding = new Padding(20);

            // Main container panel
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7, // Added row for manager name
                Padding = new Padding(10),
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 30),
                    new ColumnStyle(SizeType.Percent, 70)
                }
            };

            // Add row styles
            for (int i = 0; i < 6; i++) // Increased to 6 rows
            {
                mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
            // Last row for buttons
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            Controls.Add(mainPanel);

            // ID field
            Label lblId = new Label
            {
                Text = "Department ID:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblId, 0, 0);

            txtDepartmentId = new TextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Margin = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(txtDepartmentId, 1, 0);

            // Name field
            Label lblName = new Label
            {
                Text = "Name:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblName, 0, 1);

            txtName = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(txtName, 1, 1);

            // Description field
            Label lblDescription = new Label
            {
                Text = "Description:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblDescription, 0, 2);

            txtDescription = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Height = 80,
                Margin = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(txtDescription, 1, 2);

            // Budget field
            Label lblBudget = new Label
            {
                Text = "Budget:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblBudget, 0, 3);

            nudBudget = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Maximum = 10000000,
                Increment = 10000,
                ThousandsSeparator = true,
                DecimalPlaces = 2,
                Margin = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(nudBudget, 1, 3);

            // Manager ID field - now separate from manager name
            Label lblManagerId = new Label
            {
                Text = "Manager ID:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblManagerId, 0, 4);

            cmbManager = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(cmbManager, 1, 4);

            // Manager Name field (read-only) - now in a separate row
            Label lblManagerName = new Label
            {
                Text = "Manager Name:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            mainPanel.Controls.Add(lblManagerName, 0, 5);

            txtManagerName = new TextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Enabled = false, // Explicitly disabled
                BackColor = Color.FromArgb(240, 240, 240),
                Margin = new Padding(0, 10, 0, 10)
            };
            mainPanel.Controls.Add(txtManagerName, 1, 5);

            // Button panel
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(buttonPanel, 1, 6);

            // Save button
            btnSave = new Button
            {
                Text = "Save",
                Size = new Size(100, 35),
                Location = new Point(buttonPanel.Width - 220, 10),
                BackColor = Color.FromArgb(68, 93, 233),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            buttonPanel.Controls.Add(btnSave);

            // Cancel button
            btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(100, 35),
                Location = new Point(buttonPanel.Width - 110, 10),
                BackColor = Color.FromArgb(160, 160, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            buttonPanel.Controls.Add(btnCancel);

            // Populate manager dropdown
            PopulateManagerDropdown();

            // Add manager selection change event
            cmbManager.SelectedIndexChanged += CmbManager_SelectedIndexChanged;
        }

        private void LoadDepartmentData()
        {
            txtDepartmentId.Text = _department.DepartmentId;
            txtName.Text = _department.Name;
            txtDescription.Text = _department.Description;
            nudBudget.Value = _department.Budget;

            // Set manager if it exists
            if (!string.IsNullOrEmpty(_department.ManagerId))
            {
                cmbManager.Text = _department.ManagerId;
                txtManagerName.Text = _department.ManagerName;
            }
        }

        private void PopulateManagerDropdown()
        {
            cmbManager.Items.Clear();

            // Add a blank item
            cmbManager.Items.Add("");

            // Add employee IDs
            foreach (Employee employee in _employees)
            {
                cmbManager.Items.Add(employee.EmployeeId);
            }
        }

        private void CmbManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            string managerId = cmbManager.Text;

            if (string.IsNullOrEmpty(managerId))
            {
                txtManagerName.Text = "";
                return;
            }

            // Find the employee by ID and set the manager name
            Employee manager = FindEmployeeById(managerId);
            txtManagerName.Text = manager != null ? manager.Name : "";
        }

        // New helper method to replace lambda function
        private Employee FindEmployeeById(string employeeId)
        {
            foreach (Employee emp in _employees)
            {
                if (emp.EmployeeId == employeeId)
                {
                    return emp;
                }
            }
            return null;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Department name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update department object
            _department.Name = txtName.Text;
            _department.Description = txtDescription.Text;
            _department.Budget = nudBudget.Value;
            _department.ManagerId = cmbManager.Text;
            _department.ManagerName = txtManagerName.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}