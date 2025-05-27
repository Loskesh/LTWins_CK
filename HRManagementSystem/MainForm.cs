namespace HRManagementSystem
{
    public partial class MainForm : Form
    {
        private Form activePanel = null!;
        private RoleSelectionService roleService;

        public MainForm()
        {
            InitializeComponent();
            CustomizeDesign();

            // Use the singleton instance instead of creating a new one
            roleService = RoleSelectionService.Instance;
            roleService.RoleChanged += RoleService_RoleChanged;

            // Update the role switcher button text
            UpdateRoleSwitcherText();

            SetActiveTab(btnDashboard); // Set Dashboard as default active tab
            OpenChildForm(CreateDashboard()); // Create and open initial dashboard
        }

        // Handle role changes
        private void RoleService_RoleChanged(object sender, RoleChangedEventArgs e)
        {
            UpdateRoleSwitcherText();
            UpdateTabNamesForRole();

            // Update tab visibility
            btnDashboard.Visible = roleService.CurrentRole == UserRole.Admin;

            // Always reset to the default view for each role
            if (roleService.CurrentRole == UserRole.Employee)
            {
                // For employee role, set Employee tab as default
                SetActiveTab(btnEmployees);
                OpenChildForm(CreateEmployeeManagement());
            }
            else // Admin role
            {
                // For admin role, always set Dashboard as default
                SetActiveTab(btnDashboard);
                OpenChildForm(CreateDashboard());
            }
        }

        private void UpdateTabNamesForRole()
        {
            if (roleService.CurrentRole == UserRole.Admin)
            {
                btnEmployees.Text = "Employees";
                btnDepartments.Text = "Departments";
            }
            else
            {
                btnEmployees.Text = "Profile";
                btnDepartments.Text = "Department";
            }
        }

        private void UpdateRoleSwitcherText()
        {
            btnRoleSwitcher.Text = roleService.CurrentRole.ToString() + " ▼";
        }

        // Create a dashboard with event subscription properly attached
        private DashboardOverview CreateDashboard()
        {
            DashboardOverview dashboard = new DashboardOverview();
            dashboard.ModuleAccessRequested += Dashboard_ModuleAccessRequested;
            return dashboard;
        }

        // Factory methods to create the appropriate form based on current role
        private Form CreateEmployeeManagement()
        {
            return roleService.CurrentRole == UserRole.Admin ?
                new EmployeeManagement() :
                new Employee_ProfileView(EmployeeSession.Instance.EmployeeId);
        }

        private Form CreateDepartmentManagement()
        {
            return roleService.CurrentRole == UserRole.Admin ?
                new DepartmentManagement() :
                new Employee_DepartmentView();
        }

        private Form CreateAttendanceManagement()
        {
            return roleService.CurrentRole == UserRole.Admin ?
                new AttendanceManagement() :
                new Employee_AttendanceView(EmployeeSession.Instance.EmployeeId);
        }

        private Form CreatePayrollManagement()
        {
            return roleService.CurrentRole == UserRole.Admin ?
                new PayrollManagement() :
                new Employee_PayrollView();
        }

        // Event handler for dashboard module access requests
        private void Dashboard_ModuleAccessRequested(object? sender, ModuleAccessEventArgs e)
        {
            // Open the appropriate form based on module name and current role
            switch (e.ModuleName)
            {
                case "Employees Management":
                    SetActiveTab(btnEmployees);
                    OpenChildForm(CreateEmployeeManagement());
                    break;
                case "Departments Management":
                    SetActiveTab(btnDepartments);
                    OpenChildForm(CreateDepartmentManagement());
                    break;
                case "Leave & Attendance":
                    SetActiveTab(btnAttendance);
                    OpenChildForm(CreateAttendanceManagement());
                    break;
                case "Payroll":
                    SetActiveTab(btnPayroll);
                    OpenChildForm(CreatePayrollManagement());
                    break;
            }
        }

        private void CustomizeDesign()
        {
            BackColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = true;
            Text = "Human Resource Management System";
            Size = new Size(1200, 800);
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Unsubscribe from events when removing a form
        public void OpenChildForm(Form childForm)
        {
            if (activePanel != null)
            {
                // Unsubscribe from events if the active panel is a dashboard
                if (activePanel is DashboardOverview dashboard)
                {
                    dashboard.ModuleAccessRequested -= Dashboard_ModuleAccessRequested;
                }

                panelContent.Controls.Remove(activePanel);
                activePanel.Dispose(); // Important: properly dispose the form
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            activePanel = childForm;
        }


        private void SetActiveTab(Button selectedButton)
        {
            // Reset all buttons
            foreach (Control ctrl in panelNavigation.Controls)
            {
                if (ctrl is Button && ctrl != btnLogo && ctrl != btnRoleSwitcher)
                {
                    Button btn = (Button)ctrl;
                    btn.BackColor = ColorPalette.navBackground;
                    btn.ForeColor = Color.Black;
                    btn.FlatAppearance.BorderSize = 0;
                }
            }

            // Highlight selected button
            selectedButton.BackColor = ColorPalette.primaryColor;
            selectedButton.ForeColor = Color.White;
            selectedButton.FlatAppearance.BorderSize = 0;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveTab((Button)sender);
            OpenChildForm(CreateDashboard());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            SetActiveTab((Button)sender);
            OpenChildForm(CreateEmployeeManagement());
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            SetActiveTab((Button)sender);
            OpenChildForm(CreateDepartmentManagement());
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            SetActiveTab((Button)sender);
            OpenChildForm(CreateAttendanceManagement());
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            SetActiveTab((Button)sender);
            OpenChildForm(CreatePayrollManagement());
        }

        private void btnRoleSwitcher_Click(object sender, EventArgs e)
        {
            ContextMenuStrip roleMenu = new ContextMenuStrip();
            roleMenu.Items.Add("Admin", null, (s, args) => SwitchRole(UserRole.Admin));
            roleMenu.Items.Add("Employee", null, (s, args) => SwitchRole(UserRole.Employee));

            roleMenu.Show(btnRoleSwitcher, new Point(0, btnRoleSwitcher.Height));
        }

        private void SwitchRole(UserRole role)
        {
            roleService.SwitchRole(role);
            // The UI update will be handled by the RoleService_RoleChanged event handler
        }
    }
}
