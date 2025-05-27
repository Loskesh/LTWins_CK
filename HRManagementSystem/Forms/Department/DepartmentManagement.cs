namespace HRManagementSystem
{
    public class DepartmentManagement : Form
    {
        private DataGridView dgvDepartments;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnRefresh;

        private DepartmentService _departmentService;
        private EmployeeService _employeeService;
        private List<Department> _departments;
        private List<Employee> _employees;
        private FileManager _fileManager;

        public DepartmentManagement()
        {
            InitializeComponent();
            // Initialize FileManager with JsonFileStorage
            _fileManager = new FileManager(new JsonFileStorage());

            // Initialize services with FileManager to ensure data persistence
            _departmentService = new DepartmentService(KetNoi.str);
            _employeeService = new EmployeeService(KetNoi.str);

            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Department Management";
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

            // Header panel with title and add button
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            mainLayout.Controls.Add(headerPanel, 0, 0);

            // Title label
            Label lblTitle = new Label
            {
                Text = "Department Management",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            headerPanel.Controls.Add(lblTitle);

            // Add New Department button
            btnAdd = new Button
            {
                Text = "+ Add New Department",
                BackColor = Color.FromArgb(68, 93, 233),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 40),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                Location = new Point(headerPanel.Width - 220, 10),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;
            headerPanel.Controls.Add(btnAdd);

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
            btnSearch.Click += BtnSearch_Click;
            searchPanel.Controls.Add(btnSearch);

            // Refresh button
            btnRefresh = new Button
            {
                Text = "Refresh",
                Location = new Point(btnSearch.Right + 10, 10),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            btnRefresh.Click += BtnRefresh_Click;
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

            // Define columns - no description, will be shown in edit form
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

            // Add action buttons column
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Name = "Edit",
                Text = "✏️",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvDepartments.Columns.Add(editColumn);

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Name = "Delete",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvDepartments.Columns.Add(deleteColumn);

            dgvDepartments.CellClick += DgvDepartments_CellClick;
        }

        private void LoadData()
        {
            try
            {
                // Load departments using the department service
                _departments = _departmentService.GetAll();

                // Load employees to populate manager dropdown
                _employees = _employeeService.GetAll();

                // Instead of using DataSource binding, we'll populate the grid manually
                PopulateDepartmentsGrid(_departments);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int employeeCount = CountEmployeesInDepartment(dept.DepartmentId);

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

        // Helper method to count employees in a department
        private int CountEmployeesInDepartment(string departmentId)
        {
            int count = 0;
            foreach (Employee employee in _employees)
            {
                if (employee.DepartmentId == departmentId)
                {
                    count++;
                }
            }
            return count;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                PopulateDepartmentsGrid(_departments);
                return;
            }

            string searchTerm = txtSearch.Text.ToLower();
            List<Department> filteredList = FilterDepartmentsBySearchTerm(searchTerm);
            PopulateDepartmentsGrid(filteredList);
        }

        // Helper method to filter departments by search term
        private List<Department> FilterDepartmentsBySearchTerm(string searchTerm)
        {
            List<Department> result = new List<Department>();

            foreach (Department d in _departments)
            {
                if (d.DepartmentId.ToLower().Contains(searchTerm) ||
                    d.Name.ToLower().Contains(searchTerm) ||
                    (d.ManagerId != null && d.ManagerId.ToLower().Contains(searchTerm)) ||
                    (d.ManagerName != null && d.ManagerName.ToLower().Contains(searchTerm)))
                {
                    result.Add(d);
                }
            }

            return result;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadData();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Create a new department with a generated ID
            Department newDepartment = new Department
            {
                DepartmentId = _departmentService.GenerateNewDepartmentId(),
                Name = "",
                Description = "",
                Budget = 0,
                ManagerId = "",
                ManagerName = ""
            };

            // Open edit dialog for the new department
            using (DepartmentEditDialog dialog = new DepartmentEditDialog(newDepartment, _employees, true))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        bool success = _departmentService.Add(dialog.Department);
                        if (success)
                        {
                            MessageBox.Show("Department added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add department.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header clicks
            if (e.RowIndex < 0) return;

            // Get the department object from the row's Tag
            Department department = dgvDepartments.Rows[e.RowIndex].Tag as Department;
            if (department == null) return;

            // Handle Edit button click
            if (dgvDepartments.Columns[e.ColumnIndex].Name == "Edit")
            {
                using (DepartmentEditDialog dialog = new DepartmentEditDialog(department, _employees, false))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            bool success = _departmentService.Update(dialog.Department);
                            if (success)
                            {
                                MessageBox.Show("Department updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update department.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            // Handle Delete button click
            else if (dgvDepartments.Columns[e.ColumnIndex].Name == "Delete")
            {
                // Check if department has employees assigned to it
                if (DepartmentHasEmployees(department.DepartmentId))
                {
                    MessageBox.Show(
                        $"Cannot delete department '{department.Name}' because it has employees assigned to it. " +
                        "Please reassign or remove the employees first.",
                        "Delete Restricted",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete department {department.Name}?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool success = _departmentService.Delete(department.DepartmentId);
                        if (success)
                        {
                            MessageBox.Show("Department deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete department.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Add new method to check if a department has employees
        private bool DepartmentHasEmployees(string departmentId)
        {
            if (string.IsNullOrEmpty(departmentId))
                return false;

            // Check if any employees are assigned to this department
            foreach (Employee employee in _employees)
            {
                if (employee.DepartmentId == departmentId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
