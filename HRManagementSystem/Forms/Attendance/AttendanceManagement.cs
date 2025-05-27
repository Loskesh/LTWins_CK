using System.Data;

namespace HRManagementSystem
{
    public class AttendanceManagement : Form
    {
        private TabControl tabControl;
        private TabPage attendanceTab;
        private TabPage leaveRequestsTab;
        private DataGridView attendanceGridView;
        private DataGridView leaveRequestsGridView;
        private DateTimePicker datePicker;
        private TextBox searchBox;
        private Label lblWorkingHours;

        private RadioButton rbtnMonthly;
        private RadioButton rbtnDaily;
        private bool isMonthlyView = true;

        // Add services for attendance and leave management
        private AttendanceService attendanceService;
        private LeaveService leaveService;
        private EmployeeService employeeService;

        // For tracking current view month/year
        private int currentMonth;
        private int currentYear;

        // Company working hours
        private TimeSpan workStartTime = new TimeSpan(9, 0, 0); // 9:00 AM
        private TimeSpan workEndTime = new TimeSpan(17, 30, 0); // 5:30 PM

        // Lists to store the loaded data
        private List<Attendance> attendances;
        private List<LeaveRequest> leaveRequests;

        public AttendanceManagement()
        {
            InitializeComponent();

            // Initialize services using singleton pattern
            attendanceService = new AttendanceService();
            leaveService = new LeaveService(KetNoi.str);
            employeeService = new EmployeeService(KetNoi.str);

            // Set current month and year
            currentMonth = DateTime.Now.Month;
            currentYear = DateTime.Now.Year;

            // Set date picker to current month
            datePicker.Value = new DateTime(currentYear, currentMonth, 1);

            // Load data from services
            LoadAttendanceData();
            LoadLeaveData();
        }

        private void InitializeComponent()
        {
            BackColor = Color.WhiteSmoke;
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
                Text = "Attendance Management",
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

            // Top control panel (with labels, search box, month picker)
            Panel controlPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 80 // Increased height to fit view options
            };
            directoryLayout.Controls.Add(controlPanel, 0, 0);

            // Records section label
            Label lblRecords = new Label
            {
                Text = "Attendance & Leave Records",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(0, 0)
            };
            controlPanel.Controls.Add(lblRecords);

            // Working hours label
            lblWorkingHours = new Label
            {
                Text = $"Official Working Hours: {workStartTime.ToString(@"hh\:mm")} - {workEndTime.ToString(@"hh\:mm")}",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(0, 30)
            };
            controlPanel.Controls.Add(lblWorkingHours);

            // View options
            Panel viewOptionsPanel = new Panel
            {
                Location = new Point(0, 55),
                Size = new Size(250, 25),
                BackColor = Color.Transparent
            };
            controlPanel.Controls.Add(viewOptionsPanel);

            // Monthly radio button
            rbtnMonthly = new RadioButton
            {
                Text = "Monthly View",
                Checked = true,
                Location = new Point(0, 0),
                AutoSize = true
            };
            rbtnMonthly.CheckedChanged += ViewTypeRadioButton_CheckedChanged;
            viewOptionsPanel.Controls.Add(rbtnMonthly);

            // Daily radio button
            rbtnDaily = new RadioButton
            {
                Text = "Daily View",
                Checked = false,
                Location = new Point(120, 0),
                AutoSize = true
            };
            rbtnDaily.CheckedChanged += ViewTypeRadioButton_CheckedChanged;
            viewOptionsPanel.Controls.Add(rbtnDaily);

            // Date picker - will now be used for both month and day selection
            datePicker = new DateTimePicker
            {
                Location = new Point(controlPanel.Width - 170, 0),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MMMM yyyy", // Default to month view
                ShowUpDown = true,
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            datePicker.ValueChanged += DatePicker_ValueChanged;
            controlPanel.Controls.Add(datePicker);

            // Search and filter panel
            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 50
            };
            directoryLayout.Controls.Add(searchPanel, 0, 1);

            // Search box
            searchBox = new TextBox
            {
                Size = new Size(500, 35),
                Location = new Point(0, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                PlaceholderText = "🔍 Search records..."
            };
            searchBox.TextChanged += SearchBox_TextChanged;
            searchPanel.Controls.Add(searchBox);

            // Add a button to view individual employee records
            Button btnViewEmployee = new Button
            {
                Text = "View Employee Records",
                Size = new Size(180, 35),
                Location = new Point(500, 10),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnViewEmployee.FlatAppearance.BorderSize = 0;
            btnViewEmployee.Click += BtnViewEmployee_Click;
            controlPanel.Controls.Add(btnViewEmployee);

            // Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            directoryLayout.Controls.Add(tabControl, 0, 2);

            // Attendance Tab
            attendanceTab = new TabPage
            {
                Text = "Attendance",
                BackColor = Color.White
            };
            tabControl.Controls.Add(attendanceTab);

            // Leave Requests Tab
            leaveRequestsTab = new TabPage
            {
                Text = "Leave Requests",
                BackColor = Color.White
            };
            tabControl.Controls.Add(leaveRequestsTab);

            // Attendance Grid
            attendanceGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DefaultCellStyle = { SelectionBackColor = Color.LightSkyBlue, SelectionForeColor = Color.Black }
            };
            attendanceGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            attendanceGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            attendanceGridView.CellClick += AttendanceGridView_CellClick;

            // Set up columns for attendance
            attendanceGridView.Columns.Add("AttendanceId", "Attendance ID");
            attendanceGridView.Columns.Add("EmployeeId", "Employee ID");
            attendanceGridView.Columns.Add("Name", "Name");
            attendanceGridView.Columns.Add("Date", "Date");
            attendanceGridView.Columns.Add("TimeIn", "Time In");
            attendanceGridView.Columns.Add("TimeOut", "Time Out");
            attendanceGridView.Columns.Add("Status", "Status");

            // Add Actions column with buttons
            DataGridViewButtonColumn actionsColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Name = "Actions",
                Text = "⚙",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            attendanceGridView.Columns.Add(actionsColumn);
            attendanceTab.Controls.Add(attendanceGridView);

            // Leave Requests Grid
            leaveRequestsGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DefaultCellStyle = { SelectionBackColor = Color.LightSkyBlue, SelectionForeColor = Color.Black }
            };
            leaveRequestsGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            leaveRequestsGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            leaveRequestsGridView.CellClick += LeaveRequestsGridView_CellClick;

            // Set up columns for leave requests
            leaveRequestsGridView.Columns.Add("RequestId", "Request ID");
            leaveRequestsGridView.Columns.Add("EmployeeId", "Employee ID");
            leaveRequestsGridView.Columns.Add("Name", "Name");
            leaveRequestsGridView.Columns.Add("Type", "Leave Type");
            leaveRequestsGridView.Columns.Add("StartDate", "Start Date");
            leaveRequestsGridView.Columns.Add("EndDate", "End Date");
            leaveRequestsGridView.Columns.Add("Status", "Status");
            leaveRequestsGridView.Columns.Add("Remarks", "Remarks");

            // Add Actions column with buttons for leave requests
            DataGridViewButtonColumn leaveActionsColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Actions",
                Name = "Actions",
                Text = "⚙",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            leaveRequestsGridView.Columns.Add(leaveActionsColumn);
            leaveRequestsTab.Controls.Add(leaveRequestsGridView);
        }

        private void BtnViewEmployee_Click(object sender, EventArgs e)
        {
            EmployeeAttendanceViewer employeeViewer = new EmployeeAttendanceViewer();
            employeeViewer.ShowDialog();
        }

        private void ViewTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                isMonthlyView = (radioButton == rbtnMonthly);

                // Update DateTimePicker format based on selected view
                if (isMonthlyView)
                {
                    datePicker.Format = DateTimePickerFormat.Custom;
                    datePicker.CustomFormat = "MMMM yyyy";
                    datePicker.ShowUpDown = true;
                }
                else
                {
                    datePicker.Format = DateTimePickerFormat.Short;
                    datePicker.ShowUpDown = false;
                }

                // Reload data based on the new view type
                LoadAttendanceData();
                LoadLeaveData();
            }
        }

        private void LoadAttendanceData()
        {
            try
            {
                if (isMonthlyView)
                {
                    // Get attendances for the selected month and year
                    attendances = attendanceService.GetMonthlyAttendance(currentYear, currentMonth);
                }
                else
                {
                    // Get attendances for the selected day
                    DateTime selectedDate = datePicker.Value.Date;
                    attendances = attendanceService.GetDailyAttendance(selectedDate);

                    // If in daily view, also identify and add absent employees
                    if (selectedDate <= DateTime.Today)  // Only check for past or current days
                    {
                        // Identify absent employees and add them to the attendance list
                        PopulateAbsentEmployees(selectedDate);
                    }
                }

                DisplayAttendanceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading attendance data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateAbsentEmployees(DateTime selectedDate)
        {
            try
            {
                // First, get all active employees
                List<Employee> allEmployees = employeeService.GetAll()
                    .Where(e => e.Status == EmployeeStatus.Active)
                    .ToList();

                // Create a set of employee IDs who were present
                HashSet<string> presentEmployeeIds = new HashSet<string>(
                    attendances.Select(a => a.EmployeeId)
                );

                // Get employees who were on approved leave on the selected date
                List<LeaveRequest> approvedLeaves = leaveService.GetDailyLeaves(selectedDate)
                    .Where(l => l.Status == LeaveStatus.Approved)
                    .ToList();
                HashSet<string> onLeaveEmployeeIds = new HashSet<string>(
                    approvedLeaves.Select(l => l.EmployeeId)
                );

                // Add absent employees to the attendance list
                foreach (Employee employee in allEmployees)
                {
                    // Skip if employee was present or on approved leave
                    if (presentEmployeeIds.Contains(employee.EmployeeId) ||
                        onLeaveEmployeeIds.Contains(employee.EmployeeId))
                    {
                        continue;
                    }

                    // Create a virtual attendance entry for the absent employee
                    Attendance absentAttendance = new Attendance
                    {
                        AttendanceId = $"ABSENT-{employee.EmployeeId}-{selectedDate.ToString("yyyyMMdd")}",
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.Name,
                        Date = selectedDate,
                        ClockInTime = DateTime.MinValue,
                        ClockOutTime = DateTime.MinValue,
                        Status = AttendanceStatus.Present, // We'll use a different mechanism to identify absences

                        IsAbsentRecord = true // New property to explicitly mark as absent
                    };

                    // Add to the attendance list for display
                    attendances.Add(absentAttendance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error populating absent employees: {ex.Message}");
                // Don't throw the exception - continue with whatever data we have
            }
        }

        private void DisplayAttendanceData()
        {
            attendanceGridView.Rows.Clear();

            foreach (Attendance attendance in attendances)
            {
                // Use the EmployeeName directly from the attendance record
                string employeeName = attendance.EmployeeName ?? "Unknown";

                // Determine if this is an absent employee record
                bool isAbsent = attendance.IsAbsentRecord ||
                                attendance.AttendanceId.StartsWith("ABSENT") ||
                                (!isMonthlyView && attendance.ClockInTime == DateTime.MinValue &&
                                 attendance.ClockOutTime == DateTime.MinValue);

                // Always display "Absent" for absent employees
                string status = isAbsent ? "Absent" : GetAttendanceStatusString(attendance.Status);

                string clockIn = attendance.ClockInTime != DateTime.MinValue
                    ? attendance.ClockInTime.ToString("HH:mm")
                    : "--";

                string clockOut = attendance.ClockOutTime != DateTime.MinValue
                    ? attendance.ClockOutTime.ToString("HH:mm")
                    : "--";

                int rowIndex = attendanceGridView.Rows.Add(
                    attendance.AttendanceId,
                    attendance.EmployeeId,
                    employeeName,
                    attendance.Date.ToString("yyyy-MM-dd"),
                    clockIn,
                    clockOut,
                    status
                );

                // Store the attendance object in the row's Tag for later retrieval
                attendanceGridView.Rows[rowIndex].Tag = attendance;

                // Apply color to status cells
                DataGridViewCell statusCell = attendanceGridView.Rows[rowIndex].Cells["Status"];
                if (isAbsent || attendance.AttendanceId.StartsWith("ABSENT"))
                {
                    // Absent styling
                    statusCell.Style.ForeColor = Color.White;
                    statusCell.Style.BackColor = Color.FromArgb(220, 53, 69); // Red background

                    // Color the entire row with light red background
                    foreach (DataGridViewCell cell in attendanceGridView.Rows[rowIndex].Cells)
                    {
                        if (cell.ColumnIndex != statusCell.ColumnIndex)
                        {
                            cell.Style.BackColor = Color.FromArgb(255, 240, 240); // Light red
                        }
                    }
                }
                else
                {
                    switch (attendance.Status)
                    {
                        case AttendanceStatus.Present:
                            statusCell.Style.ForeColor = Color.Green;
                            statusCell.Style.BackColor = Color.FromArgb(230, 255, 230);
                            break;
                        case AttendanceStatus.HalfDay:
                            statusCell.Style.ForeColor = Color.Orange;
                            statusCell.Style.BackColor = Color.FromArgb(255, 250, 230);
                            break;
                        case AttendanceStatus.WorkFromHome:
                            statusCell.Style.ForeColor = Color.DarkOrange;
                            statusCell.Style.BackColor = Color.FromArgb(255, 240, 230);
                            break;
                        case AttendanceStatus.Late:
                            statusCell.Style.ForeColor = Color.DarkOrange;
                            statusCell.Style.BackColor = Color.FromArgb(255, 235, 200);
                            break;
                    }
                }
            }
        }

        private string GetAttendanceStatusString(AttendanceStatus status)
        {
            return status switch
            {
                AttendanceStatus.Present => "Present",
                AttendanceStatus.HalfDay => "Half Day",
                AttendanceStatus.WorkFromHome => "Work From Home",
                AttendanceStatus.Late => "Late",
                _ => "Unknown"
            };
        }

        private void LoadLeaveData()
        {
            try
            {
                // Load leave requests
                if (isMonthlyView)
                {
                    // Get leaves for selected month and year
                    leaveRequests = leaveService.GetMonthlyLeaves(currentYear, currentMonth);
                }
                else
                {
                    // Get leaves for selected day
                    DateTime selectedDate = datePicker.Value.Date;
                    leaveRequests = leaveService.GetDailyLeaves(selectedDate);
                }

                if (leaveRequests == null || !leaveRequests.Any())
                {
                    string timeFrame = isMonthlyView ? "month" : "day";
                    leaveRequests = new List<LeaveRequest>();
                }

                DisplayLeaveRequestsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leave data: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                leaveRequests = new List<LeaveRequest>();
                DisplayLeaveRequestsData();
            }
        }

        private void DisplayLeaveRequestsData()
        {
            leaveRequestsGridView.Rows.Clear();

            if (leaveRequests == null || leaveRequests.Count == 0)
            {
                return; // Nothing to display
            }

            // Debug output
            foreach (LeaveRequest request in leaveRequests)
            {
                try
                {
                    if (request == null) continue;

                    // Get enum strings safely
                    string leaveTypeStr = "Unknown";
                    string statusStr = "Unknown";

                    // Map numeric values to enum strings
                    try
                    {
                        leaveTypeStr = request.Type.ToString();
                    }
                    catch (Exception)
                    {
                        // Handle numeric values directly if enum conversion fails
                        int typeValue = (int)request.Type;
                        leaveTypeStr = typeValue switch
                        {
                            0 => "Annual",
                            1 => "Sick",
                            2 => "Personal",
                            3 => "Maternity",
                            4 => "Training",
                            5 => "Compensatory",
                            _ => $"Type-{typeValue}"
                        };
                    }

                    try
                    {
                        statusStr = request.Status.ToString();
                    }
                    catch (Exception)
                    {
                        // Handle numeric values directly if enum conversion fails
                        int statusValue = (int)request.Status;
                        statusStr = statusValue switch
                        {
                            0 => "Pending",
                            1 => "Approved",
                            2 => "Rejected",
                            3 => "Cancelled",
                            _ => $"Status-{statusValue}"
                        };
                    }

                    string startDateStr = request.StartDate.ToString("yyyy-MM-dd");
                    string endDateStr = request.EndDate.ToString("yyyy-MM-dd");

                    int rowIndex = leaveRequestsGridView.Rows.Add(
                        request.RequestId ?? "Unknown",
                        request.EmployeeId ?? "Unknown",
                        request.EmployeeName ?? "Unknown",
                        leaveTypeStr,
                        startDateStr,
                        endDateStr,
                        statusStr,
                        request.Remarks ?? ""
                    );

                    // Store the leave request object in the row's Tag
                    leaveRequestsGridView.Rows[rowIndex].Tag = request;

                    // Apply color to status cells
                    DataGridViewCell statusCell = leaveRequestsGridView.Rows[rowIndex].Cells["Status"];
                    try
                    {
                        switch (request.Status)
                        {
                            case LeaveStatus.Approved:
                                statusCell.Style.ForeColor = Color.Green;
                                statusCell.Style.BackColor = Color.FromArgb(230, 255, 230);
                                break;
                            case LeaveStatus.Rejected:
                                statusCell.Style.ForeColor = Color.Red;
                                statusCell.Style.BackColor = Color.FromArgb(255, 230, 230);
                                break;
                            case LeaveStatus.Pending:
                                statusCell.Style.ForeColor = Color.Orange;
                                statusCell.Style.BackColor = Color.FromArgb(255, 250, 230);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        // Fallback for status coloring based on the string
                        if (statusStr == "Approved")
                        {
                            statusCell.Style.ForeColor = Color.Green;
                            statusCell.Style.BackColor = Color.FromArgb(230, 255, 230);
                        }
                        else if (statusStr == "Rejected")
                        {
                            statusCell.Style.ForeColor = Color.Red;
                            statusCell.Style.BackColor = Color.FromArgb(255, 230, 230);
                        }
                        else if (statusStr == "Pending")
                        {
                            statusCell.Style.ForeColor = Color.Orange;
                            statusCell.Style.BackColor = Color.FromArgb(255, 250, 230);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error displaying leave request: {ex.Message}");
                }
            }
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (isMonthlyView)
            {
                // Update current month and year
                currentMonth = datePicker.Value.Month;
                currentYear = datePicker.Value.Year;
            }

            // Reload data for either monthly or daily view
            LoadAttendanceData();
            LoadLeaveData();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();

            if (tabControl.SelectedTab == attendanceTab)
            {
                foreach (DataGridViewRow row in attendanceGridView.Rows)
                {
                    bool visible = false;

                    // Check if any cell in the row contains the search text
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                        {
                            visible = true;
                            break;
                        }
                    }

                    row.Visible = visible;
                }
            }
            else if (tabControl.SelectedTab == leaveRequestsTab)
            {
                foreach (DataGridViewRow row in leaveRequestsGridView.Rows)
                {
                    bool visible = false;

                    // Check if any cell in the row contains the search text
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                        {
                            visible = true;
                            break;
                        }
                    }

                    row.Visible = visible;
                }
            }
        }

        private void AttendanceGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the Actions column
            if (e.ColumnIndex == attendanceGridView.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                // Get the attendance record from the row's Tag
                Attendance attendance = attendanceGridView.Rows[e.RowIndex].Tag as Attendance;
                if (attendance == null) return;

                // Get employee name directly from attendance record
                string employeeName = attendance.EmployeeName ?? "Unknown";

                // Skip action menu for auto-generated absence records
                if (attendance.AttendanceId.StartsWith("ABSENT"))
                {
                    MessageBox.Show(
                        $"Employee Information\n\n" +
                        $"Employee: {employeeName} ({attendance.EmployeeId})\n" +
                        $"Date: {attendance.Date.ToShortDateString()}\n" +
                        $"Status: Absent (No clock-in record for this day)",
                        "Employee Absence",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Show context menu with options
                ContextMenuStrip actionMenu = new ContextMenuStrip();

                // If the employee hasn't clocked out yet, add a clock out option
                if (attendance.ClockInTime != DateTime.MinValue && attendance.ClockOutTime == DateTime.MinValue)
                {
                    ToolStripMenuItem clockOutItem = new ToolStripMenuItem("Record Clock Out");
                    clockOutItem.Tag = attendance.AttendanceId;
                    clockOutItem.Click += RecordClockOut_Click;
                    actionMenu.Items.Add(clockOutItem);
                }

                ToolStripMenuItem viewDetailsItem = new ToolStripMenuItem("View Details");
                viewDetailsItem.Tag = attendance;
                viewDetailsItem.Click += ViewAttendanceDetails_Click;
                actionMenu.Items.Add(viewDetailsItem);

                actionMenu.Show(Cursor.Position);
            }
        }

        private void RecordClockOut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                string attendanceId = menuItem.Tag.ToString();
                try
                {
                    attendanceService.UpdateClockOut(attendanceId);
                    LoadAttendanceData();
                    MessageBox.Show("Clock out recorded successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error recording clock out: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ViewAttendanceDetails_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                Attendance attendance = menuItem.Tag as Attendance;
                if (attendance == null) return;

                // Check if the employee clocked in late
                string lateStatus = "";
                if (attendance.Status == AttendanceStatus.Late)
                {
                    TimeSpan clockInTime = attendance.ClockInTime.TimeOfDay;
                    TimeSpan minutesLate = clockInTime - workStartTime;
                    lateStatus = $"\nLate by: {minutesLate.TotalMinutes:0} minutes";
                }

                MessageBox.Show(
                    $"Attendance Details\n\n" +
                    $"Employee: {attendance.EmployeeName} ({attendance.EmployeeId})\n" +
                    $"Date: {attendance.Date.ToShortDateString()}\n" +
                    $"Clock In: {(attendance.ClockInTime != DateTime.MinValue ? attendance.ClockInTime.ToString("HH:mm") : "Not recorded")}\n" +
                    $"Clock Out: {(attendance.ClockOutTime != DateTime.MinValue ? attendance.ClockOutTime.ToString("HH:mm") : "Not recorded")}\n" +
                    $"Status: {GetAttendanceStatusString(attendance.Status)}{lateStatus}",
                    "Attendance Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void LeaveRequestsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the Actions column
            if (e.ColumnIndex == leaveRequestsGridView.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                // Get the leave request from the row's Tag
                LeaveRequest request = leaveRequestsGridView.Rows[e.RowIndex].Tag as LeaveRequest;
                if (request == null) return;

                // Get employee name directly from leave request
                string employeeName = request.EmployeeName ?? "Unknown";

                // Show context menu with approve/reject options
                ContextMenuStrip actionMenu = new ContextMenuStrip();

                if (request.Status == LeaveStatus.Pending)
                {
                    ToolStripMenuItem approveItem = new ToolStripMenuItem("Approve");
                    approveItem.Tag = request.RequestId;
                    approveItem.Click += ApproveLeaveRequest_Click;
                    actionMenu.Items.Add(approveItem);

                    ToolStripMenuItem rejectItem = new ToolStripMenuItem("Reject");
                    rejectItem.Tag = request;
                    rejectItem.Click += RejectLeaveRequest_Click;
                    actionMenu.Items.Add(rejectItem);
                }

                ToolStripMenuItem viewDetailsItem = new ToolStripMenuItem("View Details");
                viewDetailsItem.Tag = request;
                viewDetailsItem.Click += ViewLeaveDetails_Click;
                actionMenu.Items.Add(viewDetailsItem);

                actionMenu.Show(Cursor.Position);
            }
        }

        private void ApproveLeaveRequest_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                string requestId = menuItem.Tag.ToString();
                try
                {
                    // Use an admin ID for approval (in a real system, this would be the logged-in user's ID)
                    string approverId = "ADMIN001";
                    leaveService.ApproveLeaveRequest(requestId, approverId);
                    LoadLeaveData(); // Refresh the data
                    MessageBox.Show("Leave request approved!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error approving leave request: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RejectLeaveRequest_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                LeaveRequest request = menuItem.Tag as LeaveRequest;
                if (request == null) return;

                // Create a simple form to get rejection reason
                Form rejectionForm = new Form
                {
                    Text = "Reject Leave Request",
                    Size = new Size(400, 200),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblReason = new Label
                {
                    Text = "Rejection Reason:",
                    Location = new Point(20, 20),
                    AutoSize = true
                };
                rejectionForm.Controls.Add(lblReason);

                TextBox txtReason = new TextBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(340, 80),
                    Multiline = true
                };
                rejectionForm.Controls.Add(txtReason);

                Button btnConfirm = new Button
                {
                    Text = "Confirm Rejection",
                    Location = new Point(240, 140),
                    Size = new Size(120, 30),
                    BackColor = Color.FromArgb(220, 53, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnConfirm.FlatAppearance.BorderSize = 0;
                btnConfirm.Tag = new object[] { request, txtReason, rejectionForm };
                btnConfirm.Click += ConfirmRejectLeaveRequest_Click;
                rejectionForm.Controls.Add(btnConfirm);

                rejectionForm.ShowDialog();
            }
        }

        private void ConfirmRejectLeaveRequest_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Tag != null)
            {
                object[] parameters = button.Tag as object[];
                if (parameters != null && parameters.Length == 3)
                {
                    LeaveRequest request = parameters[0] as LeaveRequest;
                    TextBox txtReason = parameters[1] as TextBox;
                    Form rejectionForm = parameters[2] as Form;

                    if (request != null && txtReason != null && rejectionForm != null)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(txtReason.Text))
                            {
                                MessageBox.Show("Please provide a reason for rejection.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Use an admin ID for rejection (in a real system, this would be the logged-in user's ID)
                            string approverId = "ADMIN001";
                            leaveService.RejectLeaveRequest(request.RequestId, approverId, txtReason.Text);
                            LoadLeaveData(); // Refresh the data

                            rejectionForm.Close();
                            MessageBox.Show("Leave request rejected!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error rejecting leave request: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ViewLeaveDetails_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                LeaveRequest request = menuItem.Tag as LeaveRequest;
                if (request == null) return;

                MessageBox.Show(
                    $"Leave Request Details\n\n" +
                    $"Employee: {request.EmployeeName} ({request.EmployeeId})\n" +
                    $"Request Date: {request.RequestDate.ToShortDateString()}\n" +
                    $"Leave Type: {request.Type}\n" +
                    $"Period: {request.StartDate.ToShortDateString()} to {request.EndDate.ToShortDateString()}\n" +
                    $"Status: {request.Status}\n" +
                    $"Remarks: {request.Remarks}",
                    "Leave Request Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
