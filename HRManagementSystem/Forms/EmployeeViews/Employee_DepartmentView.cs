namespace HRManagementSystem
{
    // Define custom delegates for button events
    public delegate void SearchButtonClickDelegate(object? sender, EventArgs e);
    public delegate void RefreshButtonClickDelegate(object? sender, EventArgs e);
    public delegate void DataGridCellClickDelegate(object? sender, DataGridViewCellEventArgs e);

    public class Employee_DepartmentView : Form
    {
        private DataGridView dgvDepartments = null!;
        private TextBox txtSearch = null!;
        private Button btnSearch = null!;
        private Button btnRefresh = null!;

        // Declare events with the custom delegates
        private event SearchButtonClickDelegate SearchButtonClicked;
        private event RefreshButtonClickDelegate RefreshButtonClicked;
        private event DataGridCellClickDelegate DepartmentCellClicked;

        private DepartmentService _departmentService;
        private EmployeeService _employeeService;
        private List<Department> _departments = new List<Department>();
        private List<Employee> _employees = new List<Employee>();
        private string _currentEmployeeId;

        public Employee_DepartmentView(string employeeId = "EMP001")
        {
            _currentEmployeeId = employeeId;
            InitializeComponent();
            _departmentService = new DepartmentService(KetNoi.str);
            _employeeService = new EmployeeService(KetNoi.str);

            // Subscribe to the custom events
            SearchButtonClicked += OnSearchButtonClicked;
            RefreshButtonClicked += OnRefreshButtonClicked;
            DepartmentCellClicked += OnDepartmentCellClicked;

            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Department Information";
            Size = new Size(1200, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // Main layout panel
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 2,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Controls.Add(mainLayout);

            // Header panel with title
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            mainLayout.Controls.Add(headerPanel, 0, 0);

            // Title label
            Label lblTitle = new Label
            {
                Text = "Department Information",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            headerPanel.Controls.Add(lblTitle);

            // Directory panel (second row)
            Panel directoryPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            mainLayout.Controls.Add(directoryPanel, 0, 1);

            // Directory layout
            TableLayoutPanel directoryLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 3,
                ColumnCount = 1
            };
            directoryLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            directoryLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            directoryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            directoryPanel.Controls.Add(directoryLayout);

            // Directory header
            Label lblDirectory = new Label
            {
                Text = "Department Directory",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            };
            directoryLayout.Controls.Add(lblDirectory, 0, 0);

            // Search and action panel
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            directoryLayout.Controls.Add(searchPanel, 0, 1);

            // Search box
            txtSearch = new TextBox
            {
                Size = new Size(500, 30),
                Location = new Point(0, 10),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                PlaceholderText = "Search departments..."
            };
            txtSearch.SetBounds(txtSearch.Left, txtSearch.Top, txtSearch.Width, 35);
            searchPanel.Controls.Add(txtSearch);

            // Search icon
            Label lblSearchIcon = new Label
            {
                Text = "🔍",
                AutoSize = true,
                Location = new Point(10, 17)
            };
            txtSearch.Controls.Add(lblSearchIcon);
            txtSearch.Padding = new Padding(25, 5, 5, 5);

            // Search button
            btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(txtSearch.Right + 10, 10),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            btnSearch.Click += new EventHandler(SearchButton_Click);
            searchPanel.Controls.Add(btnSearch);

            // Refresh button
            btnRefresh = new Button
            {
                Text = "Refresh",
                Location = new Point(btnSearch.Right + 10, 10),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            btnRefresh.Click += new EventHandler(RefreshButton_Click);
            searchPanel.Controls.Add(btnRefresh);

            // DataGridView for departments
            dgvDepartments = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(230, 230, 230),
                ReadOnly = true
            };

            // DataGridView style
            dgvDepartments.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvDepartments.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 100, 100);
            dgvDepartments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvDepartments.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgvDepartments.ColumnHeadersHeight = 50;
            dgvDepartments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDepartments.RowTemplate.Height = 50;
            dgvDepartments.DefaultCellStyle.Padding = new Padding(10);
            dgvDepartments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dgvDepartments.DefaultCellStyle.SelectionForeColor = Color.Black;

            directoryLayout.Controls.Add(dgvDepartments, 0, 2);

            // Define columns - similar to management view but without action columns
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            {
                Name = "DepartmentId",
                HeaderText = "ID",
                DataPropertyName = "DepartmentId"
            };
            dgvDepartments.Columns.Add(colId);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Name",
                DataPropertyName = "Name"
            };
            dgvDepartments.Columns.Add(colName);

            DataGridViewTextBoxColumn colBudget = new DataGridViewTextBoxColumn
            {
                Name = "Budget",
                HeaderText = "Budget",
                DataPropertyName = "Budget",
                DefaultCellStyle = { Format = "C2" }
            };
            dgvDepartments.Columns.Add(colBudget);

            DataGridViewTextBoxColumn colEmployeeCount = new DataGridViewTextBoxColumn
            {
                Name = "EmployeeCount",
                HeaderText = "Employee Count",
                DataPropertyName = "EmployeeCount"
            };
            dgvDepartments.Columns.Add(colEmployeeCount);

            DataGridViewTextBoxColumn colManagerId = new DataGridViewTextBoxColumn
            {
                Name = "ManagerId",
                HeaderText = "Manager ID",
                DataPropertyName = "ManagerId"
            };
            dgvDepartments.Columns.Add(colManagerId);

            DataGridViewTextBoxColumn colManagerName = new DataGridViewTextBoxColumn
            {
                Name = "ManagerName",
                HeaderText = "Manager",
                DataPropertyName = "ManagerName"
            };
            dgvDepartments.Columns.Add(colManagerName);

            // Add view details button column
            DataGridViewButtonColumn viewDetailsColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Details",
                Name = "ViewDetails",
                Text = "👁️",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvDepartments.Columns.Add(viewDetailsColumn);

            dgvDepartments.CellClick += new DataGridViewCellEventHandler(DgvDepartments_CellClick);
        }

        private void LoadData()
        {
            try
            {
                // Load departments using the department service
                _departments = _departmentService.GetAll();

                // Load employees to get counts
                _employees = _employeeService.GetAll();

                // Populate the grid manually
                PopulateDepartmentsGrid(_departments);

                // If we have a current employee, highlight their department
                if (!string.IsNullOrEmpty(_currentEmployeeId))
                {
                    HighlightEmployeeDepartment();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightEmployeeDepartment()
        {
            // Find the current employee's department
            Employee? currentEmployee = _employees.FirstOrDefault(e => e.EmployeeId == _currentEmployeeId);
            if (currentEmployee != null && !string.IsNullOrEmpty(currentEmployee.DepartmentId))
            {
                // Find and highlight the row with this department
                foreach (DataGridViewRow row in dgvDepartments.Rows)
                {
                    if (row.Cells["DepartmentId"].Value.ToString() == currentEmployee.DepartmentId)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // Light blue highlight
                        row.DefaultCellStyle.Font = new Font(dgvDepartments.Font, FontStyle.Bold);
                        break;
                    }
                }
            }
        }

        private void PopulateDepartmentsGrid(List<Department> departments)
        {
            // Clear existing rows without affecting columns
            dgvDepartments.Rows.Clear();

            // Add each department to the grid in the order we want
            foreach (Department dept in departments)
            {
                // Calculate employee count for this department
                int employeeCount = _employees.Count(e => e.DepartmentId == dept.DepartmentId);

                int rowIndex = dgvDepartments.Rows.Add(
                    dept.DepartmentId,
                    dept.Name,
                    dept.Budget,
                    employeeCount,
                    dept.ManagerId,
                    dept.ManagerName
                );

                // Store the department object for later use
                dgvDepartments.Rows[rowIndex].Tag = dept;
            }
        }

        // Event handler implementations
        private void OnSearchButtonClicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                PopulateDepartmentsGrid(_departments);
                return;
            }

            string searchTerm = txtSearch.Text.ToLower();
            List<Department> filteredList = _departments.Where(d =>
                d.DepartmentId.ToLower().Contains(searchTerm) ||
                d.Name.ToLower().Contains(searchTerm) ||
                (d.ManagerId?.ToLower()?.Contains(searchTerm) ?? false) ||
                (d.ManagerName?.ToLower()?.Contains(searchTerm) ?? false)
            ).ToList();

            PopulateDepartmentsGrid(filteredList);
        }

        private void OnRefreshButtonClicked(object? sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadData();
        }

        private void OnDepartmentCellClicked(object? sender, DataGridViewCellEventArgs e)
        {
            // Ignore header clicks
            if (e.RowIndex < 0) return;

            // Get the department object from the row's Tag
            Department? department = dgvDepartments.Rows[e.RowIndex].Tag as Department;
            if (department == null) return;

            // Handle View Details button click
            if (dgvDepartments.Columns[e.ColumnIndex].Name == "ViewDetails")
            {
                // Get employees in this department
                List<Employee> departmentEmployees = _employees
                    .Where(emp => emp.DepartmentId == department.DepartmentId)
                    .ToList();

                // Show the department details in a proper dialog
                using (DepartmentDetailsDialog detailsDialog = new DepartmentDetailsDialog(department, departmentEmployees))
                {
                    detailsDialog.ShowDialog(this);
                }
            }
        }

        // New regular event handlers to replace lambdas
        private void SearchButton_Click(object? sender, EventArgs e)
        {
            SearchButtonClicked?.Invoke(sender, e);
        }

        private void RefreshButton_Click(object? sender, EventArgs e)
        {
            RefreshButtonClicked?.Invoke(sender, e);
        }

        private void DgvDepartments_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            DepartmentCellClicked?.Invoke(sender, e);
        }
    }
}
