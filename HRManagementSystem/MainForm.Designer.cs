namespace HRManagementSystem
{
    partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelNavigation;
        private Panel panelContent;
        private Panel panelNavBorder;

        private Button btnLogo;
        private Button btnRoleSwitcher;
        private Button btnDashboard;
        private Button btnEmployees;
        private Button btnDepartments;
        private Button btnAttendance;
        private Button btnPayroll;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.components = new System.ComponentModel.Container();

            Color navBackground = Color.White;
            Color borderColor = Color.FromArgb(230, 230, 230);
            Color primaryColor = Color.FromArgb(25, 118, 210);

            // Navigation Panel
            this.panelNavigation = new Panel();
            this.panelNavigation.BackColor = navBackground;
            this.panelNavigation.Dock = DockStyle.Top;
            this.panelNavigation.Height = 60;
            this.panelNavigation.Padding = new Padding(0);

            // Nav Border Panel - separates nav from content
            this.panelNavBorder = new Panel();
            this.panelNavBorder.BackColor = borderColor;
            this.panelNavBorder.Height = 1;
            this.panelNavBorder.Dock = DockStyle.Top;

            // Logo Button - now with white background and black text
            this.btnLogo = new Button();
            this.btnLogo.Text = "HRMS";
            this.btnLogo.FlatStyle = FlatStyle.Flat;
            this.btnLogo.FlatAppearance.BorderSize = 0;
            this.btnLogo.BackColor = navBackground;  // White background
            this.btnLogo.ForeColor = Color.Black;    // Black text
            this.btnLogo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.btnLogo.Size = new Size(100, 60);
            this.btnLogo.Location = new Point(0, 0);
            this.btnLogo.Cursor = Cursors.Default;
            this.btnLogo.Enabled = false;

            // Role Switcher Button - with proper border
            this.btnRoleSwitcher = new Button();
            this.btnRoleSwitcher.Text = "Admin ▼";
            this.btnRoleSwitcher.FlatStyle = FlatStyle.Standard; // Changed to Standard for proper borders
            this.btnRoleSwitcher.BackColor = navBackground;
            this.btnRoleSwitcher.ForeColor = Color.Black;
            this.btnRoleSwitcher.Font = new Font("Segoe UI", 10);
            this.btnRoleSwitcher.Size = new Size(100, 36);
            this.btnRoleSwitcher.Location = new Point(120, 12); // Moved right without avatar
            this.btnRoleSwitcher.Cursor = Cursors.Hand;
            this.btnRoleSwitcher.Click += new EventHandler(btnRoleSwitcher_Click);

            // Right-align navigation buttons
            int buttonWidth = 110;
            int totalButtonsWidth = buttonWidth * 5; // 5 navigation buttons
            int startX = this.ClientSize.Width - totalButtonsWidth; 
            // Navigation Buttons - right aligned
            this.btnDashboard = CreateNavButton("Dashboard", startX);
            this.btnEmployees = CreateNavButton("Employees", startX + buttonWidth);
            this.btnDepartments = CreateNavButton("Departments", startX + buttonWidth * 2);
            this.btnAttendance = CreateNavButton("Attendance", startX + buttonWidth * 3);
            this.btnPayroll = CreateNavButton("Payroll", startX + buttonWidth * 4);

            this.btnDashboard.Click += new EventHandler(btnDashboard_Click);
            this.btnEmployees.Click += new EventHandler(btnEmployees_Click);
            this.btnDepartments.Click += new EventHandler(btnDepartments_Click);
            this.btnAttendance.Click += new EventHandler(btnAttendance_Click);
            this.btnPayroll.Click += new EventHandler(btnPayroll_Click);

            // Add controls to navigation panel
            this.panelNavigation.Controls.Add(this.btnLogo);
            this.panelNavigation.Controls.Add(this.btnRoleSwitcher);
            this.panelNavigation.Controls.Add(this.btnDashboard);
            this.panelNavigation.Controls.Add(this.btnEmployees);
            this.panelNavigation.Controls.Add(this.btnDepartments);
            this.panelNavigation.Controls.Add(this.btnAttendance);
            this.panelNavigation.Controls.Add(this.btnPayroll);

            // Content Panel
            this.panelContent = new Panel();
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.WhiteSmoke;

            // Add panels to form
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelNavBorder);
            this.Controls.Add(this.panelNavigation);

            // Handle form resize to maintain layout
            this.Resize += new EventHandler(MainForm_Resize);
        }

        // Add this method to handle window resizing
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Recalculate positions when form size changes
            if (btnRoleSwitcher != null)
            {
                // Update role switcher position
                btnRoleSwitcher.Location = new Point(120, 12);

                // Update navigation buttons
                int buttonWidth = 110;
                int totalButtonsWidth = buttonWidth * 5;
                int startX = this.ClientSize.Width - totalButtonsWidth - 20;

                if (btnDashboard != null) btnDashboard.Location = new Point(startX, 10);
                if (btnEmployees != null) btnEmployees.Location = new Point(startX + buttonWidth, 10);
                if (btnDepartments != null) btnDepartments.Location = new Point(startX + buttonWidth * 2, 10);
                if (btnAttendance != null) btnAttendance.Location = new Point(startX + buttonWidth * 3, 10);
                if (btnPayroll != null) btnPayroll.Location = new Point(startX + buttonWidth * 4, 10);
            }
        }

        private Button CreateNavButton(string text, int x)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Segoe UI", 10);
            btn.Size = new Size(110, 40);
            btn.Location = new Point(x, 10);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Cursor = Cursors.Hand;

            return btn;
        }

        // This will be called in the constructor to apply rounded corners to the active tab
        private void ApplyRoundedCorners(Button button)
        {
            int radius = 6;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
            button.Region = new Region(path);
        }
    }
}
