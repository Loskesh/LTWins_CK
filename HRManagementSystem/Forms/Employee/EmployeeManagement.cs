using System.Data;

namespace HRManagementSystem
{
    public partial class EmployeeManagement : Form
    {
        private List<Employee> employees;
        private List<Department> departments;
        private EmployeeService employeeService;
        private DepartmentService departmentService;

        public EmployeeManagement()
        {
            InitializeComponent();
            employeeService = new EmployeeService(KetNoi.str);
            departmentService = new DepartmentService(KetNoi.str);
            LoadData();
            PopulateEmployeeDataGridView();
        }

        private void InitializeComponent()
        {
            Text = "Employee Management";
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
                Text = "Employee Management",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            headerPanel.Controls.Add(lblTitle);

            // Add New Employee button
            Button btnAddEmployee = new Button
            {
                Text = "+ Add New Employee",
                BackColor = Color.FromArgb(68, 93, 233), // Blue color from the image
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 40),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                Location = new Point(headerPanel.Width - 220, 10),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            btnAddEmployee.FlatAppearance.BorderSize = 0;
            btnAddEmployee.Click += BtnAddEmployee_Click;
            headerPanel.Controls.Add(btnAddEmployee);

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
                Text = "Employee Directory",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            };
            directoryLayout.Controls.Add(lblDirectory, 0, 0);

            // Search and filter panel
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 60
            };
            directoryLayout.Controls.Add(searchPanel, 0, 1);

            // Search box
            TextBox txtSearch = new TextBox
            {
                Size = new Size(500, 30),
                Location = new Point(0, 10),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Name = "txtSearch" // Add name for the search box so we can find it later
            };
            txtSearch.SetBounds(txtSearch.Left, txtSearch.Top, txtSearch.Width, 35);
            txtSearch.TextChanged += TxtSearch_TextChanged; // Add event handler for text changed
            searchPanel.Controls.Add(txtSearch);

            // Search icon (can be improved with an actual icon)
            Label lblSearchIcon = new Label
            {
                Text = "🔍",
                AutoSize = true,
                Location = new Point(10, 17)
            };
            txtSearch.Controls.Add(lblSearchIcon);
            txtSearch.Padding = new Padding(25, 5, 5, 5);

            // Filter buttons panel
            Panel filterPanel = new Panel
            {
                Location = new Point(txtSearch.Right + 20, 10),
                Height = 35,
                Width = 450, // Increased width to accommodate more buttons
                BorderStyle = BorderStyle.FixedSingle,
                Name = "filterPanel"
            };
            searchPanel.Controls.Add(filterPanel);

            // Filter buttons - updated to include all status types
            string[] filterOptions = { "All", "Active", "On Leave", "Terminated", "Suspended" };
            int buttonWidth = filterPanel.Width / filterOptions.Length;

            for (int i = 0; i < filterOptions.Length; i++)
            {
                Button btnFilter = new Button
                {
                    Text = filterOptions[i],
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(buttonWidth, filterPanel.Height - 2),
                    Location = new Point(i * buttonWidth, 0),
                    BackColor = i == 0 ? Color.White : Color.FromArgb(245, 245, 245),
                    ForeColor = i == 0 ? Color.FromArgb(68, 93, 233) : Color.FromArgb(100, 100, 100),
                    Tag = filterOptions[i]
                };
                btnFilter.FlatAppearance.BorderSize = 0;
                btnFilter.Click += BtnFilter_Click;
                filterPanel.Controls.Add(btnFilter);
            }

            // DataGridView for employees
            DataGridView dgvEmployees = new DataGridView
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
                Name = "dgvEmployees"
            };

            // DataGridView style
            dgvEmployees.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 100, 100);
            dgvEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dgvEmployees.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgvEmployees.ColumnHeadersHeight = 50;
            dgvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEmployees.RowTemplate.Height = 50;
            dgvEmployees.DefaultCellStyle.Padding = new Padding(10);
            dgvEmployees.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dgvEmployees.DefaultCellStyle.SelectionForeColor = Color.Black;

            directoryLayout.Controls.Add(dgvEmployees, 0, 2);

            // Set up columns for the DataGridView - remove DataPropertyName to handle data binding manually
            DataGridViewTextBoxColumn[] columns = new[]
            {
                new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Name", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Department", HeaderText = "Department", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Position", HeaderText = "Position", Width = 180 },
                new DataGridViewTextBoxColumn { Name = "DateHired", HeaderText = "Date Hired", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 100 }
            };

            foreach (DataGridViewTextBoxColumn? column in columns)
            {
                dgvEmployees.Columns.Add(column);
            }

            // Add action buttons column
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Name = "Edit",
                Text = "✏️",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 50
            };
            dgvEmployees.Columns.Add(editColumn);

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Name = "Delete",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 50
            };
            dgvEmployees.Columns.Add(deleteColumn);

            dgvEmployees.CellClick += DgvEmployees_CellClick;
            dgvEmployees.CellFormatting += DgvEmployees_CellFormatting;
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Reset all buttons
            foreach (Control control in clickedButton.Parent.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(245, 245, 245);
                    button.ForeColor = Color.FromArgb(100, 100, 100);
                }
            }

            // Highlight selected button
            clickedButton.BackColor = Color.White;
            clickedButton.ForeColor = Color.FromArgb(68, 93, 233);

            // Apply filter with current search text
            string filter = (string)clickedButton.Tag;
            TextBox txtSearch = Controls.Find("txtSearch", true).FirstOrDefault() as TextBox;
            string searchText = txtSearch?.Text?.ToLower() ?? "";

            ApplySearchAndFilter(searchText, filter);
        }

        // Add search text changed event handler
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();
            ApplySearchAndFilter(searchText, GetCurrentFilter());
        }

        // Get the current filter from the selected filter button
        private string GetCurrentFilter()
        {
            Panel filterPanel = Controls.Find("filterPanel", true).FirstOrDefault() as Panel;
            if (filterPanel == null) return "All";

            foreach (Control control in filterPanel.Controls)
            {
                if (control is Button button && button.BackColor == Color.White)
                {
                    return (string)button.Tag;
                }
            }

            return "All";
        }

        // Combined method to handle both search and filter
        private void ApplySearchAndFilter(string searchText, string filter)
        {
            DataGridView dgvEmployees = (DataGridView)Controls.Find("dgvEmployees", true)[0];

            // First apply status filter
            List<Employee> filteredEmployees = filter switch
            {
                "Active" => employees.Where(e => e.Status == EmployeeStatus.Active).ToList(),
                "On Leave" => employees.Where(e => e.Status == EmployeeStatus.OnLeave).ToList(),
                "Terminated" => employees.Where(e => e.Status == EmployeeStatus.Terminated).ToList(),
                "Suspended" => employees.Where(e => e.Status == EmployeeStatus.Suspended).ToList(),
                _ => employees.ToList(),
            };

            // Then apply search text filter if it's not empty
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredEmployees = filteredEmployees.Where(e =>
                    e.Name.ToLower().Contains(searchText) ||
                    e.EmployeeId.ToLower().Contains(searchText) ||
                    e.Position.ToLower().Contains(searchText) ||
                    GetDepartmentName(e.DepartmentId).ToLower().Contains(searchText)
                ).ToList();
            }

            // Apply the filtered data to the grid using our own method instead of binding
            PopulateEmployeeDataGridWithFilteredData(dgvEmployees, filteredEmployees);
        }

        // Method to manually populate DataGridView to keep column order consistent
        private void PopulateEmployeeDataGridWithFilteredData(DataGridView dgv, List<Employee> filteredEmployees)
        {
            try
            {
                dgv.Rows.Clear();

                System.Diagnostics.Debug.WriteLine($"[POPULATE] Populating grid with {filteredEmployees.Count} employees");

                foreach (Employee employee in filteredEmployees)
                {
                    // ✅ DEBUG: Log dữ liệu của từng employee
                    System.Diagnostics.Debug.WriteLine($"[POPULATE] Employee {employee.EmployeeId}:");
                    System.Diagnostics.Debug.WriteLine($"  - Name: {employee.Name}");
                    System.Diagnostics.Debug.WriteLine($"  - HireDate: {employee.HireDate}");
                    System.Diagnostics.Debug.WriteLine($"  - Status: {employee.Status}");

                    // Set department name if missing
                    if (string.IsNullOrEmpty(employee.DepartmentName))
                    {
                        Department dept = departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId);
                        employee.DepartmentName = dept?.Name ?? "Unknown";
                    }

                    // ✅ SỬA: Format date đúng cách
                    string hireDateDisplay;
                    if (employee.HireDate == DateTime.MinValue || employee.HireDate < new DateTime(1900, 1, 1))
                    {
                        hireDateDisplay = "Not Set";
                        System.Diagnostics.Debug.WriteLine($"  - HireDate display: {hireDateDisplay} (invalid date)");
                    }
                    else
                    {
                        hireDateDisplay = employee.HireDate.ToString("yyyy-MM-dd");
                        System.Diagnostics.Debug.WriteLine($"  - HireDate display: {hireDateDisplay}");
                    }

                    int rowIndex = dgv.Rows.Add(
                        employee.EmployeeId,
                        employee.Name,
                        employee.DepartmentName ?? "Unknown",
                        employee.Position,
                        hireDateDisplay, // ✅ Sử dụng formatted date
                        employee.Status.ToString()
                    );

                    // ✅ QUAN TRỌNG: Gán employee object vào Tag để edit có thể truy cập
                    dgv.Rows[rowIndex].Tag = employee;

                    System.Diagnostics.Debug.WriteLine($"  - Row {rowIndex} added to grid");
                }

                System.Diagnostics.Debug.WriteLine($"[POPULATE] Grid population completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[POPULATE ERROR] {ex.Message}");
                MessageBox.Show($"Error populating employee grid: {ex.Message}", "Error");
            }
        }

        private void LoadData()
        {
            // Load departments from the service
            departments = departmentService.GetAll();

            // If no departments were loaded, create some sample data (fallback)
            if (departments == null || departments.Count == 0)
            {
                departments = new List<Department>
                {
                    new Department { DepartmentId = "DEP001", Name = "Engineering", Description = "Engineering Department" },
                    new Department { DepartmentId = "DEP002", Name = "Marketing", Description = "Marketing Department" },
                    new Department { DepartmentId = "DEP003", Name = "Finance", Description = "Finance Department" },
                    new Department { DepartmentId = "DEP004", Name = "Human Resources", Description = "HR Department" }
                };
            }

            // Load employees from the service
            employees = employeeService.GetAll();

            // If no employees were loaded, create some sample data (fallback)
            if (employees == null || employees.Count == 0)
            {
                employees = new List<Employee> { };
            }
            else
            {
                // Associate department names with employees
                foreach (Employee employee in employees)
                {
                    employee.DepartmentName = GetDepartmentName(employee.DepartmentId);
                }
            }
        }

        private string GetDepartmentName(string departmentId)
        {
            return departments.FirstOrDefault(d => d.DepartmentId == departmentId)?.Name ?? "Unknown";
        }

        private void PopulateEmployeeDataGridView()
        {
            DataGridView dgvEmployees = (DataGridView)Controls.Find("dgvEmployees", true)[0];

            // Use the new method to populate the grid manually
            PopulateEmployeeDataGridWithFilteredData(dgvEmployees, employees);

            // Apply formatting
            FormatStatusColumn(dgvEmployees);
        }

        private void FormatStatusColumn(DataGridView dgvEmployees)
        {
            // Ensure we have the Status column
            if (dgvEmployees.Columns.Contains("Status"))
            {
                // Handle status cell formatting through CellFormatting event
                dgvEmployees.CellFormatting += (sender, e) =>
                {
                    if (e.ColumnIndex == dgvEmployees.Columns["Status"].Index && e.RowIndex >= 0)
                    {
                        if (e.Value != null)
                        {
                            string status = e.Value.ToString();

                            switch (status)
                            {
                                case "Active":
                                    e.CellStyle.ForeColor = Color.Green;
                                    e.CellStyle.BackColor = Color.FromArgb(230, 255, 230);
                                    break;
                                case "OnLeave":
                                    e.CellStyle.ForeColor = Color.Orange;
                                    e.CellStyle.BackColor = Color.FromArgb(255, 250, 230);
                                    e.Value = "On Leave";
                                    break;
                                case "Terminated":
                                    e.CellStyle.ForeColor = Color.Red;
                                    e.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
                                    break;
                                case "Suspended":
                                    e.CellStyle.ForeColor = Color.DarkOrange;
                                    e.CellStyle.BackColor = Color.FromArgb(255, 240, 230);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                };
            }
        }

        private void DgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            // Format Status column
            if (dgv.Columns[e.ColumnIndex].Name == "Status" && e.RowIndex >= 0)
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();

                    switch (status)
                    {
                        case "Active":
                            e.CellStyle.ForeColor = Color.Green;
                            e.CellStyle.BackColor = Color.FromArgb(230, 255, 230);
                            break;
                        case "OnLeave":
                            e.CellStyle.ForeColor = Color.Orange;
                            e.CellStyle.BackColor = Color.FromArgb(255, 250, 230);
                            e.Value = "On Leave";
                            break;
                        case "Terminated":
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            break;
                        case "Suspended":
                            e.CellStyle.ForeColor = Color.DarkOrange;
                            e.CellStyle.BackColor = Color.FromArgb(255, 240, 230);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void DgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            // Check if click is on a button column
            if (e.RowIndex >= 0)
            {
                // Use the Employee object attached to the row's Tag
                Employee selectedEmployee = dgv.Rows[e.RowIndex].Tag as Employee;
                if (selectedEmployee == null) return;

                if (dgv.Columns[e.ColumnIndex].Name == "Edit")
                {
                    try
                    {
                        // ✅ THÊM debug
                        System.Diagnostics.Debug.WriteLine($"[EDIT] Current: HireDate={selectedEmployee.HireDate}, Status={selectedEmployee.Status}");

                        System.Diagnostics.Debug.WriteLine($"[EDIT] Opening edit form for: {selectedEmployee.Name} (ID: {selectedEmployee.Id})");
                  
                        EditEmployeeForm editForm = new EditEmployeeForm(selectedEmployee, departments, false);
                        DialogResult result = editForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            Employee updatedEmployee = editForm.UpdatedEmployee;
                            if (updatedEmployee == null)
                            {
                                MessageBox.Show("Error: Updated employee data is null.", "Error");
                                return;
                            }

                            // ✅ THÊM debug
                            System.Diagnostics.Debug.WriteLine($"[EDIT] Updated: HireDate={updatedEmployee.HireDate}, Status={updatedEmployee.Status}");

                            bool success = employeeService.Update(updatedEmployee);

                            if (success)
                            {
                                System.Diagnostics.Debug.WriteLine($"[EDIT] Database update successful");

                                try
                                {
                                   
                                    List<Employee> updatedEmployees = employeeService.GetAll();
                                   
                                    System.Diagnostics.Debug.WriteLine($"[EDIT] JSON backup saved");
                                }
                                catch (Exception jsonEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"[EDIT] JSON save failed: {jsonEx.Message}");
                                }

                                // ✅ THÊM force refresh
                                LoadData();
                                PopulateEmployeeDataGridView();

                                employees = employeeService.GetAll();

                                TextBox txtSearch = Controls.Find("txtSearch", true).FirstOrDefault() as TextBox;
                                string searchText = txtSearch?.Text?.ToLower() ?? "";
                                ApplySearchAndFilter(searchText, GetCurrentFilter());

                                MessageBox.Show("Employee updated successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"[EDIT] Database update failed");
                                MessageBox.Show("Failed to update employee in database.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"[EDIT] User cancelled edit operation");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[EDIT ERROR] {ex.Message}");
                        MessageBox.Show($"Error updating employee: {ex.Message}", "Error");
                    }
                }
                else if (dgv.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show(
                        $"Are you sure you want to delete employee with ID: {selectedEmployee.EmployeeId}?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            System.Diagnostics.Debug.WriteLine($"[DELETE] Deleting employee: {selectedEmployee.Name} (ID: {selectedEmployee.Id})");

                            // Delete the employee from the service
                            bool success = employeeService.Delete(selectedEmployee.Id);

                            if (success)
                            {
                                System.Diagnostics.Debug.WriteLine($"[DELETE] Database delete successful");

                                // ✅ OPTIONAL: Force save to JSON
                                try
                                {
                                 
                                    List<Employee> updatedEmployees = employeeService.GetAll();
                                 
                                    System.Diagnostics.Debug.WriteLine($"[DELETE] JSON backup updated");
                                }
                                catch (Exception jsonEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"[DELETE] JSON save failed: {jsonEx.Message}");
                                }

                                // Refresh the employee list and grid
                                employees = employeeService.GetAll();

                                // Reapply current search and filter
                                TextBox txtSearch = Controls.Find("txtSearch", true).FirstOrDefault() as TextBox;
                                string searchText = txtSearch?.Text?.ToLower() ?? "";
                                ApplySearchAndFilter(searchText, GetCurrentFilter());

                                MessageBox.Show($"Employee with ID: {selectedEmployee.EmployeeId} deleted!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"[DELETE] Database delete failed");
                                MessageBox.Show("Failed to delete employee from database.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"[DELETE ERROR] {ex.Message}");
                            MessageBox.Show($"Error deleting employee: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeFactory employeeFactory = new EmployeeFactory();
                Employee newEmployee = employeeFactory.CreateEmployee("Regular");

                // ✅ SỬA: Set các giá trị mặc định hợp lệ
                newEmployee.Id = Guid.NewGuid().ToString();
                newEmployee.EmployeeId = GenerateNewEmployeeId();
                newEmployee.Name = "New Employee";
                newEmployee.DateOfBirth = DateTime.Today.AddYears(-25); // 25 tuổi
                newEmployee.HireDate = DateTime.Today; // Hôm nay
                newEmployee.Position = "Unassigned";
                newEmployee.BaseSalary = 0;
                newEmployee.Status = EmployeeStatus.Active;
                newEmployee.DepartmentId = departments.FirstOrDefault()?.DepartmentId ?? string.Empty;

                System.Diagnostics.Debug.WriteLine($"[ADD] Creating new employee with:");
                System.Diagnostics.Debug.WriteLine($"  - DOB: {newEmployee.DateOfBirth}");
                System.Diagnostics.Debug.WriteLine($"  - HireDate: {newEmployee.HireDate}");

                EditEmployeeForm addForm = new EditEmployeeForm(newEmployee, departments, true);
                DialogResult result = addForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Employee employeeToAdd = addForm.UpdatedEmployee;

                    // ✅ DOUBLE CHECK: Validate dates trước khi add
                    if (employeeToAdd.DateOfBirth < new DateTime(1753, 1, 1))
                    {
                        MessageBox.Show("Invalid Date of Birth. Please check and try again.", "Error");
                        return;
                    }

                    if (employeeToAdd.HireDate < new DateTime(1753, 1, 1))
                    {
                        MessageBox.Show("Invalid Hire Date. Please check and try again.", "Error");
                        return;
                    }

                    // Set department name if missing
                    if (string.IsNullOrEmpty(employeeToAdd.DepartmentName))
                    {
                        Department dept = departments.FirstOrDefault(d => d.DepartmentId == employeeToAdd.DepartmentId);
                        if (dept != null)
                            employeeToAdd.DepartmentName = dept.Name;
                    }

                    bool success = employeeService.Add(employeeToAdd);

                    if (success)
                    {
                        MessageBox.Show("New employee added successfully!", "Success");
                        LoadData();
                        PopulateEmployeeDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add employee.", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ADD ERROR] {ex.Message}");
                MessageBox.Show($"Error adding employee: {ex.Message}", "Error");
            }
        }
        private string GenerateNewEmployeeId()
        {
            // Find the highest employee ID number and increment by 1
            int highestNumber = 0;

            foreach (Employee employee in employees)
            {
                if (employee.EmployeeId != null && employee.EmployeeId.StartsWith("EMP"))
                {
                    if (int.TryParse(employee.EmployeeId.Substring(3), out int idNumber))
                    {
                        highestNumber = Math.Max(highestNumber, idNumber);
                    }
                }
            }

            // Format the new ID with leading zeros (EMP001, EMP002, etc.)
            return $"EMP{(highestNumber + 1):D3}";
        }
    }

}