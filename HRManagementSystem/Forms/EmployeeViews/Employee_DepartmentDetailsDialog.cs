namespace HRManagementSystem
{
    public class DepartmentDetailsDialog : Form
    {
        private Department _department;
        private List<Employee> _departmentEmployees;

        public DepartmentDetailsDialog(Department department, List<Employee> departmentEmployees)
        {
            _department = department ?? throw new ArgumentNullException(nameof(department));
            _departmentEmployees = departmentEmployees ?? new List<Employee>();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Department Details";
            Size = new Size(600, 500);
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // Main panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };
            Controls.Add(mainPanel);

            // Department header panel with color accent
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(68, 93, 233)
            };
            mainPanel.Controls.Add(headerPanel);

            // Department name
            Label lblDepartmentName = new Label
            {
                Text = _department.Name,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Location = new Point(2, 5),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblDepartmentName);

            // Department ID
            Label lblDepartmentId = new Label
            {
                Text = $"ID: {_department.DepartmentId}",
                ForeColor = Color.FromArgb(220, 220, 220),
                Font = new Font("Segoe UI", 10F),
                Location = new Point(5, 45),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblDepartmentId);

            // Content panel
            TableLayoutPanel contentPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(0, 20, 0, 0)
            };
            contentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            mainPanel.Controls.Add(contentPanel);

            // Manager section
            Panel managerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            contentPanel.Controls.Add(managerPanel, 0, 0);

            Label lblManagerTitle = new Label
            {
                Text = "Manager",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(0, 0),
                AutoSize = true
            };
            managerPanel.Controls.Add(lblManagerTitle);

            Label lblManagerValue = new Label
            {
                Text = $"{_department.ManagerName} ({_department.ManagerId})",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(0, 25),
                AutoSize = true
            };
            managerPanel.Controls.Add(lblManagerValue);

            // Budget section
            Panel budgetPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            contentPanel.Controls.Add(budgetPanel, 0, 1);

            Label lblBudgetTitle = new Label
            {
                Text = "Budget",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(0, 0),
                AutoSize = true
            };
            budgetPanel.Controls.Add(lblBudgetTitle);

            Label lblBudgetValue = new Label
            {
                Text = $"{_department.Budget:C2}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(0, 25),
                AutoSize = true
            };
            budgetPanel.Controls.Add(lblBudgetValue);

            // Employee count section
            Panel employeeCountPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            contentPanel.Controls.Add(employeeCountPanel, 0, 2);

            Label lblEmployeeCountTitle = new Label
            {
                Text = "Employee Count",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(0, 0),
                AutoSize = true
            };
            employeeCountPanel.Controls.Add(lblEmployeeCountTitle);

            Label lblEmployeeCountValue = new Label
            {
                Text = $"{_departmentEmployees.Count} employees",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(0, 25),
                AutoSize = true
            };
            employeeCountPanel.Controls.Add(lblEmployeeCountValue);

            // Description section
            Panel descriptionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 100
            };
            contentPanel.Controls.Add(descriptionPanel, 0, 3);

            Label lblDescriptionTitle = new Label
            {
                Text = "Description",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(0, 0),
                AutoSize = true
            };
            descriptionPanel.Controls.Add(lblDescriptionTitle);

            Label lblDescriptionValue = new Label
            {
                Text = string.IsNullOrEmpty(_department.Description) ? "No description available." : _department.Description,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(0, 25),
                Size = new Size(560, 60),
                AutoEllipsis = true
            };
            descriptionPanel.Controls.Add(lblDescriptionValue);

            // Close button
            Button btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.Cancel,
                Size = new Size(100, 35),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 120, ClientSize.Height - 55),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            Controls.Add(btnClose);
            CancelButton = btnClose;

            // Set accent colors on close button hover
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
        }
    }
}
