using System.Text;

namespace HRManagementSystem
{
    public class EmployeeAttendanceViewer : Form
    {
        private ComboBox cmbEmployees;
        private DateTimePicker datePicker;
        private RadioButton rbtnMonthly;
        private RadioButton rbtnDaily;
        private TabControl tabControl;
        private DataGridView attendanceGridView;
        private DataGridView leaveGridView;
        private Label lblEmployeeInfo;
        private Label lblWorkingHours;
        private Button btnClose;
        private Button btnViewAbsences;

        private TimeSpan workStartTime;
        private TimeSpan workEndTime;

        private bool isMonthlyView = true;
        private string selectedEmployeeId;
        private List<Employee> employees;

        // Services
        private EmployeeService employeeService;
        private AttendanceService attendanceService;
        private LeaveService leaveService;

        public EmployeeAttendanceViewer()
        {
            employeeService = new EmployeeService(KetNoi.str);
            attendanceService = new AttendanceService();
            leaveService = new LeaveService(KetNoi.str);

            // Get working hours
            workStartTime = AttendanceService.GetWorkStartTime();
            workEndTime = AttendanceService.GetWorkEndTime();

            // Load all employees
            employees = employeeService.GetAll();

            InitializeComponent();
            PopulateEmployeeComboBox();
        }

        private void InitializeComponent()
        {
            Text = "Employee Attendance Viewer";
            Size = new Size(1100, 700);
            BackColor = Color.WhiteSmoke;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(mainLayout);

            // Top panel for filters and controls
            Panel controlPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 100
            };
            mainLayout.Controls.Add(controlPanel, 0, 0);

            // Select employee combo box
            Label lblEmployee = new Label
            {
                Text = "Select Employee:",
                Location = new Point(0, 15),
                AutoSize = true
            };
            controlPanel.Controls.Add(lblEmployee);

            cmbEmployees = new ComboBox
            {
                Location = new Point(120, 12),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbEmployees.SelectedIndexChanged += CmbEmployees_SelectedIndexChanged;
            controlPanel.Controls.Add(cmbEmployees);

            // Employee Info display
            lblEmployeeInfo = new Label
            {
                Location = new Point(0, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            controlPanel.Controls.Add(lblEmployeeInfo);

            // Working hours label
            lblWorkingHours = new Label
            {
                Text = $"Official Working Hours: {workStartTime.ToString(@"hh\:mm")} - {workEndTime.ToString(@"hh\:mm")}",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(0, 75)
            };
            controlPanel.Controls.Add(lblWorkingHours);

            // Date picker and view options
            Panel dateFilterPanel = new Panel
            {
                Location = new Point(600, 12),
                Size = new Size(300, 100),  // Increased height from 80 to 100
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            controlPanel.Controls.Add(dateFilterPanel);

            // Monthly radio button
            rbtnMonthly = new RadioButton
            {
                Text = "Monthly View",
                Checked = true,
                Location = new Point(0, 0),
                AutoSize = true
            };
            rbtnMonthly.CheckedChanged += ViewTypeRadioButton_CheckedChanged;
            dateFilterPanel.Controls.Add(rbtnMonthly);

            // Daily radio button
            rbtnDaily = new RadioButton
            {
                Text = "Daily View",
                Checked = false,
                Location = new Point(120, 0),
                AutoSize = true
            };
            rbtnDaily.CheckedChanged += ViewTypeRadioButton_CheckedChanged;
            dateFilterPanel.Controls.Add(rbtnDaily);

            // Date picker
            datePicker = new DateTimePicker
            {
                Location = new Point(0, 30),
                Size = new Size(200, 25),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MMMM yyyy",
                ShowUpDown = true
            };
            datePicker.ValueChanged += DatePicker_ValueChanged;
            dateFilterPanel.Controls.Add(datePicker);

            // Button to view absences
            btnViewAbsences = new Button
            {
                Text = "View Absences",
                Location = new Point(0, 65),  // Adjusted position from 60 to 65
                Size = new Size(150, 25),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnViewAbsences.FlatAppearance.BorderSize = 0;
            btnViewAbsences.Click += BtnViewAbsences_Click;
            dateFilterPanel.Controls.Add(btnViewAbsences);

            // Tab control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            mainLayout.Controls.Add(tabControl, 0, 1);

            // Attendance tab
            TabPage attendanceTab = new TabPage
            {
                Text = "Attendance",
                BackColor = Color.White
            };
            tabControl.Controls.Add(attendanceTab);

            // Leave tab
            TabPage leaveTab = new TabPage
            {
                Text = "Leave",
                BackColor = Color.White
            };
            tabControl.Controls.Add(leaveTab);

            // Attendance grid
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

            // Set up columns for attendance
            attendanceGridView.Columns.Add("AttendanceId", "ID");
            attendanceGridView.Columns.Add("Date", "Date");
            attendanceGridView.Columns.Add("TimeIn", "Time In");
            attendanceGridView.Columns.Add("TimeOut", "Time Out");
            attendanceGridView.Columns.Add("Status", "Status");
            attendanceTab.Controls.Add(attendanceGridView);

            // Leave grid
            leaveGridView = new DataGridView
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
            leaveGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            leaveGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // Set up columns for leave requests
            leaveGridView.Columns.Add("RequestId", "Request ID");
            leaveGridView.Columns.Add("Type", "Leave Type");
            leaveGridView.Columns.Add("StartDate", "Start Date");
            leaveGridView.Columns.Add("EndDate", "End Date");
            leaveGridView.Columns.Add("Status", "Status");
            leaveGridView.Columns.Add("Remarks", "Remarks");
            leaveTab.Controls.Add(leaveGridView);

            // Bottom panel for buttons
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 50
            };
            mainLayout.Controls.Add(buttonPanel, 0, 2);

            // Close button
            btnClose = new Button
            {
                Text = "Close",
                Size = new Size(100, 35),
                Location = new Point(buttonPanel.Width - 100, 7),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += BtnClose_Click;
            buttonPanel.Controls.Add(btnClose);
        }

        private void PopulateEmployeeComboBox()
        {
            cmbEmployees.Items.Clear();

            // Add a default "Select employee" item
            cmbEmployees.Items.Add(new EmployeeComboItem { DisplayText = "-- Select Employee --", EmployeeId = null });

            // Add all employees
            foreach (Employee? emp in employees.OrderBy(e => e.Name))
            {
                cmbEmployees.Items.Add(new EmployeeComboItem
                {
                    DisplayText = $"{emp.Name} ({emp.EmployeeId})",
                    EmployeeId = emp.EmployeeId
                });
            }

            cmbEmployees.SelectedIndex = 0;
        }

        private void CmbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployees.SelectedItem is EmployeeComboItem selectedItem && selectedItem.EmployeeId != null)
            {
                selectedEmployeeId = selectedItem.EmployeeId;

                // Find the selected employee
                Employee? employee = employees.FirstOrDefault(e => e.EmployeeId == selectedEmployeeId);
                if (employee != null)
                {
                    lblEmployeeInfo.Text = $"Position: {employee.Position} | Department: {employee.DepartmentName ?? "Unknown"} | Status: {employee.Status}";
                    LoadEmployeeData();
                }
            }
            else
            {
                selectedEmployeeId = null;
                lblEmployeeInfo.Text = "";
                ClearGrids();
            }
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

                // Reload data if an employee is selected
                if (!string.IsNullOrEmpty(selectedEmployeeId))
                {
                    LoadEmployeeData();
                }
            }
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            // Reload data if an employee is selected
            if (!string.IsNullOrEmpty(selectedEmployeeId))
            {
                LoadEmployeeData();
            }
        }

        private void LoadEmployeeData()
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
                return;

            try
            {
                // Load attendance data
                LoadAttendanceData();

                // Load leave data
                LoadLeaveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAttendanceData()
        {
            attendanceGridView.Rows.Clear();

            List<Attendance> attendances;
            DateTime selectedDate = datePicker.Value;

            if (isMonthlyView)
            {
                // Get month's attendance
                attendances = attendanceService.GetEmployeeAttendance(
                    selectedEmployeeId, selectedDate.Year, selectedDate.Month);
            }
            else
            {
                // Get day's attendance
                attendances = attendanceService.GetEmployeeDailyAttendance(
                    selectedEmployeeId, selectedDate);

                // For daily view, add absence record if no attendance and no leave
                if (attendances.Count == 0 && selectedDate.Date <= DateTime.Today)
                {
                    // Check if employee was on approved leave
                    List<LeaveRequest> leavesOnDate = leaveService.GetEmployeeDailyLeaves(selectedEmployeeId, selectedDate)
                        .Where(l => l.Status == LeaveStatus.Approved).ToList();

                    if (leavesOnDate.Count == 0)
                    {
                        // Get employee details
                        Employee? employee = employees.FirstOrDefault(e => e.EmployeeId == selectedEmployeeId);

                        if (employee != null)
                        {
                            // Add a virtual attendance record showing absence
                            Attendance absentAttendance = new Attendance
                            {
                                AttendanceId = $"ABSENT-{selectedEmployeeId}-{selectedDate.ToString("yyyyMMdd")}",
                                EmployeeId = selectedEmployeeId,
                                EmployeeName = employee.Name,
                                Date = selectedDate,
                                ClockInTime = DateTime.MinValue,
                                ClockOutTime = DateTime.MinValue,
                                Status = AttendanceStatus.Present, // This doesn't matter, we'll override in display
                                IsAbsentRecord = true // Mark as absent
                            };
                            attendances.Add(absentAttendance);
                        }
                    }
                }
            }

            foreach (Attendance attendance in attendances)
            {
                // Determine if this is an absent record
                bool isAbsent = attendance.IsAbsentRecord ||
                               attendance.AttendanceId.StartsWith("ABSENT") ||
                               (!isMonthlyView && attendance.ClockInTime == DateTime.MinValue &&
                                attendance.ClockOutTime == DateTime.MinValue);

                string status = isAbsent ? "Absent" : GetAttendanceStatusString(attendance.Status);

                string clockIn = attendance.ClockInTime != DateTime.MinValue
                    ? attendance.ClockInTime.ToString("HH:mm")
                    : "--";

                string clockOut = attendance.ClockOutTime != DateTime.MinValue
                    ? attendance.ClockOutTime.ToString("HH:mm")
                    : "--";

                int rowIndex = attendanceGridView.Rows.Add(
                    attendance.AttendanceId,
                    attendance.Date.ToString("yyyy-MM-dd"),
                    clockIn,
                    clockOut,
                    status
                );

                attendanceGridView.Rows[rowIndex].Tag = attendance;

                DataGridViewCell statusCell = attendanceGridView.Rows[rowIndex].Cells["Status"];
                if (isAbsent)
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

        private void LoadLeaveData()
        {
            leaveGridView.Rows.Clear();

            List<LeaveRequest> leaveRequests;
            DateTime selectedDate = datePicker.Value;

            if (isMonthlyView)
            {
                // Get leave requests for the selected month
                leaveRequests = leaveService.GetEmployeeMonthlyLeaves(
                    selectedEmployeeId, selectedDate.Year, selectedDate.Month);
            }
            else
            {
                // Get leave requests for the selected day
                leaveRequests = leaveService.GetEmployeeDailyLeaves(
                    selectedEmployeeId, selectedDate);
            }

            foreach (LeaveRequest request in leaveRequests)
            {
                if (request == null) continue;

                string leaveTypeStr = request.Type.ToString();
                string statusStr = request.Status.ToString();

                int rowIndex = leaveGridView.Rows.Add(
                    request.RequestId,
                    leaveTypeStr,
                    request.StartDate.ToString("yyyy-MM-dd"),
                    request.EndDate.ToString("yyyy-MM-dd"),
                    statusStr,
                    request.Remarks
                );

                leaveGridView.Rows[rowIndex].Tag = request;

                DataGridViewCell statusCell = leaveGridView.Rows[rowIndex].Cells["Status"];
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
                    case LeaveStatus.Cancelled:
                        statusCell.Style.ForeColor = Color.Gray;
                        statusCell.Style.BackColor = Color.FromArgb(240, 240, 240);
                        break;
                }
            }
        }

        private void ClearGrids()
        {
            attendanceGridView.Rows.Clear();
            leaveGridView.Rows.Clear();
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnViewAbsences_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedEmployeeId))
            {
                MessageBox.Show("Please select an employee first.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime startDate, endDate;
            string dateRangeText;

            if (isMonthlyView)
            {
                int year = datePicker.Value.Year;
                int month = datePicker.Value.Month;
                startDate = new DateTime(year, month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
                dateRangeText = $"{startDate:MMMM yyyy}";
            }
            else
            {
                startDate = datePicker.Value.Date;
                endDate = startDate;
                dateRangeText = $"{startDate:yyyy-MM-dd}";
            }

            // Make sure we only check dates up to today
            endDate = DateTime.Today < endDate ? DateTime.Today : endDate;

            // Get all attendance for this employee in the date range
            List<Attendance> allAttendance = attendanceService.GetEmployeeAttendance(
                selectedEmployeeId, startDate.Year, startDate.Month);

            // Create a list to track all the dates employee was present
            HashSet<DateTime> datesWithAttendance = new HashSet<DateTime>();
            foreach (Attendance attendance in allAttendance)
            {
                datesWithAttendance.Add(attendance.Date.Date);
            }

            // Get approved leaves
            List<LeaveRequest> approvedLeaves = GetApprovedLeaves(selectedEmployeeId);

            // List to store absence dates
            List<DateTime> absenceDates = new List<DateTime>();

            // Check each day in the range
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Skip weekends
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                // If no attendance record for this date
                if (!datesWithAttendance.Contains(date.Date))
                {
                    // Check if the employee was on approved leave
                    bool onApprovedLeave = IsEmployeeOnLeave(approvedLeaves, date);

                    // If not on leave, they were absent
                    if (!onApprovedLeave)
                    {
                        absenceDates.Add(date);
                    }
                }
            }

            // Display message with absences
            if (absenceDates.Count > 0)
            {
                string absencesText = GetFormattedAbsenceDates(absenceDates);
                Employee selectedEmployee = GetEmployeeById(selectedEmployeeId);
                string employeeName = selectedEmployee != null ? selectedEmployee.Name : "Unknown";

                MessageBox.Show(
                    $"Absences for {employeeName} during {dateRangeText}:\n\n{absencesText}",
                    "Absence Report",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Employee selectedEmployee = GetEmployeeById(selectedEmployeeId);
                string employeeName = selectedEmployee != null ? selectedEmployee.Name : "Unknown";

                MessageBox.Show(
                    $"No absences found for {employeeName} during {dateRangeText}.",
                    "Absence Report",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private Employee GetEmployeeById(string employeeId)
        {
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeId == employeeId)
                {
                    return employee;
                }
            }
            return null;
        }

        private string GetFormattedAbsenceDates(List<DateTime> absenceDates)
        {
            StringBuilder result = new StringBuilder();
            foreach (DateTime date in absenceDates)
            {
                if (result.Length > 0)
                {
                    result.AppendLine();
                }
                result.Append(date.ToString("yyyy-MM-dd (dddd)"));
            }
            return result.ToString();
        }

        private bool IsEmployeeOnLeave(List<LeaveRequest> approvedLeaves, DateTime date)
        {
            foreach (LeaveRequest leave in approvedLeaves)
            {
                if (date.Date >= leave.StartDate.Date && date.Date <= leave.EndDate.Date)
                {
                    return true;
                }
            }
            return false;
        }

        private List<LeaveRequest> GetApprovedLeaves(string employeeId)
        {
            List<LeaveRequest> result = new List<LeaveRequest>();
            List<LeaveRequest> allLeaves = leaveService.GetEmployeeLeaves(employeeId);

            foreach (LeaveRequest leave in allLeaves)
            {
                if (leave.Status == LeaveStatus.Approved)
                {
                    result.Add(leave);
                }
            }

            return result;
        }

        // Class to hold employee data for combo box
        private class EmployeeComboItem
        {
            public string DisplayText { get; set; }
            public string EmployeeId { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }
    }
}
