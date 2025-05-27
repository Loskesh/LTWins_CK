using System.Drawing.Drawing2D;

namespace HRManagementSystem
{
    // Custom delegate definitions for event handlers
    public delegate void EmployeeSelectionEventHandler(object? sender, EventArgs e);
    public delegate void ButtonClickEventHandler(object? sender, EventArgs e);
    public delegate void PanelPaintEventHandler(object? sender, PaintEventArgs e);

    public class Employee_ProfileView : Form
    {
        // UI Controls
        private Label lblName = null!;
        private Label lblEmail = null!;
        private Label lblPhone = null!;
        private Label lblAddress = null!;
        private Label lblDOB = null!;
        private Label lblAge = null!;
        private Label lblEmployeeID = null!;
        private Label lblPosition = null!;
        private Label lblDepartment = null!;
        private Label lblHireDate = null!;
        private Label lblYearsOfService = null!;
        private Label lblSalary = null!;
        private Panel personalInfoPanel = null!;
        private Panel employmentInfoPanel = null!;
        private Panel headerPanel = null!;
        private PictureBox profilePicture = null!;
        private Label lblUserName = null!;
        private Label lblUserTitle = null!;
        private Panel mainContentPanel = null!;
        private TableLayoutPanel buttonsPanel = null!;
        private ComboBox cmbEmployeeSelector = null!;
        private Label lblEmployeeSelector = null!;
        private Panel selectorPanel = null!;

        // Color scheme
        private readonly Color primaryColor = Color.FromArgb(60, 141, 188);  // Blue
        private readonly Color accentColor = Color.FromArgb(0, 166, 90);     // Green
        private readonly Color lightGrayColor = Color.FromArgb(245, 245, 245);
        private readonly Color darkTextColor = Color.FromArgb(73, 80, 87);
        private readonly Color lightTextColor = Color.FromArgb(108, 117, 125);
        private readonly Color panelBackColor = Color.White;
        private readonly Color editFieldBackColor = Color.FromArgb(248, 249, 250);

        // Data fields
        private Employee currentEmployee = null!;
        private readonly EmployeeService employeeService;
        private readonly List<Employee> allEmployees;

        // Event handler delegates
        private readonly EmployeeSelectionEventHandler employeeSelectionChangedHandler;
        private readonly PanelPaintEventHandler panelPaintHandler;

        // Assuming the logged-in employee ID is passed to the constructor
        public Employee_ProfileView(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                employeeId = EmployeeSession.Instance.EmployeeId;

            employeeService = new EmployeeService(KetNoi.str);
            allEmployees = employeeService.GetAll();

            // Initialize event handler delegates
            employeeSelectionChangedHandler = new EmployeeSelectionEventHandler(CmbEmployeeSelector_SelectedIndexChanged);
            panelPaintHandler = new PanelPaintEventHandler(Panel_Paint);

            InitializeComponent();
            PopulateEmployeeSelector();

            LoadEmployeeData(EmployeeSession.Instance.EmployeeId);
        }


        private void InitializeComponent()
        {
            // Form properties
            Text = "Employee Profile";
            Size = new Size(900, 680);
            BackColor = lightGrayColor;
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10F);
            Padding = new Padding(15);

            // Create main content panel with shadow effect
            mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = panelBackColor,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            // Create header panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = primaryColor,
                Padding = new Padding(20)
            };

            // Profile picture (placeholder)
            profilePicture = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(20, 20),
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Create a circle for the profile picture
            GraphicsPath gp = new();
            gp.AddEllipse(0, 0, profilePicture.Width - 1, profilePicture.Height - 1);
            profilePicture.Region = new Region(gp);

            // User name in header
            lblUserName = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 16),
                ForeColor = Color.White,
                Location = new Point(110, 30)
            };

            // User title in header
            lblUserTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(220, 220, 220),
                Location = new Point(110, 65)
            };

            headerPanel.Controls.Add(profilePicture);
            headerPanel.Controls.Add(lblUserName);
            headerPanel.Controls.Add(lblUserTitle);

            // Create selector panel
            selectorPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            // Create employee selector
            lblEmployeeSelector = new Label
            {
                Text = "View Employee:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(20, 15)
            };

            cmbEmployeeSelector = new ComboBox
            {
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(130, 12),
                Anchor = AnchorStyles.Left
            };
            // Using the explicit delegate for the event
            cmbEmployeeSelector.SelectedIndexChanged += new EventHandler(employeeSelectionChangedHandler);

            selectorPanel.Controls.Add(lblEmployeeSelector);
            selectorPanel.Controls.Add(cmbEmployeeSelector);

            // Content layout
            TableLayoutPanel contentLayout = new()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(20, 20, 20, 15),
                BackColor = panelBackColor
            };

            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Create Personal Information panel
            personalInfoPanel = CreateRoundedPanel();
            personalInfoPanel.Dock = DockStyle.Fill;
            personalInfoPanel.Padding = new Padding(15);
            personalInfoPanel.Margin = new Padding(5, 10, 10, 10);

            Label personalInfoTitle = new()
            {
                Text = "Personal Information",
                Font = new Font("Segoe UI Semibold", 12),
                ForeColor = darkTextColor,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };

            TableLayoutPanel personalInfoTable = new()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(0, 10, 0, 0)
            };

            personalInfoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            personalInfoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));

            for (int i = 0; i < 6; i++)
            {
                personalInfoTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            }

            // Labels
            lblName = CreateStyledLabel("Name:");
            lblEmail = CreateStyledLabel("Email:");
            lblPhone = CreateStyledLabel("Phone:");
            lblAddress = CreateStyledLabel("Address:");
            lblDOB = CreateStyledLabel("Date of Birth:");
            lblAge = CreateStyledLabel("Age:");

            // Create read-only labels instead of textboxes
            Label lblEmailValue = CreateValueLabel();
            Label lblPhoneValue = CreateValueLabel();
            Label lblAddressValue = CreateValueLabel();

            // Assign names to these labels for data loading
            lblEmailValue.Name = "lblEmailValue";
            lblPhoneValue.Name = "lblPhoneValue";
            lblAddressValue.Name = "lblAddressValue";

            // Add field labels
            personalInfoTable.Controls.Add(lblName, 0, 0);
            personalInfoTable.Controls.Add(lblEmail, 0, 1);
            personalInfoTable.Controls.Add(lblPhone, 0, 2);
            personalInfoTable.Controls.Add(lblAddress, 0, 3);
            personalInfoTable.Controls.Add(lblDOB, 0, 4);
            personalInfoTable.Controls.Add(lblAge, 0, 5);

            // Add name value (read-only)
            Label lblNameValue = CreateValueLabel();
            personalInfoTable.Controls.Add(lblNameValue, 1, 0);

            // Add read-only fields
            personalInfoTable.Controls.Add(lblEmailValue, 1, 1);
            personalInfoTable.Controls.Add(lblPhoneValue, 1, 2);
            personalInfoTable.Controls.Add(lblAddressValue, 1, 3);

            Label lblDOBValue = CreateValueLabel();
            Label lblAgeValue = CreateValueLabel();
            personalInfoTable.Controls.Add(lblDOBValue, 1, 4);
            personalInfoTable.Controls.Add(lblAgeValue, 1, 5);

            personalInfoPanel.Controls.Add(personalInfoTable);
            personalInfoPanel.Controls.Add(personalInfoTitle);

            // Create Employment Information panel
            employmentInfoPanel = CreateRoundedPanel();
            employmentInfoPanel.Dock = DockStyle.Fill;
            employmentInfoPanel.Padding = new Padding(15);
            employmentInfoPanel.Margin = new Padding(10, 10, 5, 10);

            Label employmentInfoTitle = new()
            {
                Text = "Employment Information",
                Font = new Font("Segoe UI Semibold", 12),
                ForeColor = darkTextColor,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };

            TableLayoutPanel employmentInfoTable = new()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(0, 10, 0, 0)
            };

            employmentInfoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            employmentInfoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            for (int i = 0; i < 6; i++)
            {
                employmentInfoTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            }

            // Labels
            lblEmployeeID = CreateStyledLabel("Employee ID:");
            lblPosition = CreateStyledLabel("Position:");
            lblDepartment = CreateStyledLabel("Department:");
            lblHireDate = CreateStyledLabel("Hire Date:");
            lblYearsOfService = CreateStyledLabel("Years of Service:");
            lblSalary = CreateStyledLabel("Base Salary:");

            // Values (all read-only)
            Label lblEmployeeIDValue = CreateValueLabel();
            Label lblPositionValue = CreateValueLabel();
            Label lblDepartmentValue = CreateValueLabel();
            Label lblHireDateValue = CreateValueLabel();
            Label lblYearsOfServiceValue = CreateValueLabel();
            Label lblSalaryValue = CreateValueLabel();

            // Add to table
            employmentInfoTable.Controls.Add(lblEmployeeID, 0, 0);
            employmentInfoTable.Controls.Add(lblPosition, 0, 1);
            employmentInfoTable.Controls.Add(lblDepartment, 0, 2);
            employmentInfoTable.Controls.Add(lblHireDate, 0, 3);
            employmentInfoTable.Controls.Add(lblYearsOfService, 0, 4);
            employmentInfoTable.Controls.Add(lblSalary, 0, 5);

            employmentInfoTable.Controls.Add(lblEmployeeIDValue, 1, 0);
            employmentInfoTable.Controls.Add(lblPositionValue, 1, 1);
            employmentInfoTable.Controls.Add(lblDepartmentValue, 1, 2);
            employmentInfoTable.Controls.Add(lblHireDateValue, 1, 3);
            employmentInfoTable.Controls.Add(lblYearsOfServiceValue, 1, 4);
            employmentInfoTable.Controls.Add(lblSalaryValue, 1, 5);

            employmentInfoPanel.Controls.Add(employmentInfoTable);
            employmentInfoPanel.Controls.Add(employmentInfoTitle);

            // Add panels to content layout
            contentLayout.Controls.Add(personalInfoPanel, 0, 0);
            contentLayout.Controls.Add(employmentInfoPanel, 1, 0);

            // Remove the buttons panel or hide it
            buttonsPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = panelBackColor,
                Padding = new Padding(20, 10, 20, 10),
                Visible = false
            };

            // Assemble the form
            mainContentPanel.Controls.Add(contentLayout);
            mainContentPanel.Controls.Add(selectorPanel);
            mainContentPanel.Controls.Add(headerPanel);
            mainContentPanel.Controls.Add(buttonsPanel);

            Controls.Add(mainContentPanel);

            // Store references to value labels for data loading
            lblNameValue.Name = "lblNameValue";
            lblDOBValue.Name = "lblDOBValue";
            lblAgeValue.Name = "lblAgeValue";
            lblEmployeeIDValue.Name = "lblEmployeeIDValue";
            lblPositionValue.Name = "lblPositionValue";
            lblDepartmentValue.Name = "lblDepartmentValue";
            lblHireDateValue.Name = "lblHireDateValue";
            lblYearsOfServiceValue.Name = "lblYearsOfServiceValue";
            lblSalaryValue.Name = "lblSalaryValue";
        }

        private void PopulateEmployeeSelector()
        {
            cmbEmployeeSelector.Items.Clear();
            foreach (Employee employee in allEmployees)
            {
                cmbEmployeeSelector.Items.Add(new EmployeeSelectionItem(
                    employee.EmployeeId,
                    $"{employee.EmployeeId} - {employee.Name}"
                ));
            }
        }

        private void CmbEmployeeSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbEmployeeSelector.SelectedItem != null)
            {
                EmployeeSelectionItem selectedEmployee = (EmployeeSelectionItem)cmbEmployeeSelector.SelectedItem;
                EmployeeSession.Instance.EmployeeId = selectedEmployee.EmployeeId;
                LoadEmployeeData(selectedEmployee.EmployeeId);
            }
        }

        private Panel CreateRoundedPanel()
        {
            Panel panel = new()
            {
                BackColor = panelBackColor,
                BorderStyle = BorderStyle.None
            };

            // Add shadow effect (simulated with border)
            // Using explicit delegate for the Paint event
            panel.Paint += new PaintEventHandler(panelPaintHandler);

            return panel;
        }

        // Panel paint event handler
        private void Panel_Paint(object? sender, PaintEventArgs e)
        {
            if (sender == null) return;

            Panel panel = (Panel)sender;
            Rectangle rect = new(0, 0, panel.Width - 1, panel.Height - 1);
            using (Pen pen = new(Color.FromArgb(230, 230, 230), 1))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private Label CreateStyledLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10),
                ForeColor = lightTextColor,
                Margin = new Padding(0, 0, 10, 0)
            };
        }

        private Label CreateValueLabel()
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10),
                ForeColor = darkTextColor
            };
        }

        private void LoadEmployeeData(string employeeId)
        {
            try
            {
                // Find the employee using EmployeeService instead of reading directly from file
                currentEmployee = allEmployees.FirstOrDefault(e => e.EmployeeId == employeeId) ??
                    throw new Exception("Employee not found in the database");

                // Select the correct item in the dropdown if it exists
                for (int i = 0; i < cmbEmployeeSelector.Items.Count; i++)
                {
                    EmployeeSelectionItem item = (EmployeeSelectionItem)cmbEmployeeSelector.Items[i]!;
                    if (item.EmployeeId == employeeId)
                    {
                        cmbEmployeeSelector.SelectedIndex = i;
                        break;
                    }
                }

                // Header information
                lblUserName.Text = currentEmployee.Name;
                lblUserTitle.Text = currentEmployee.Position;

                // Try to load a profile picture based on employee ID
                string picturePath = Path.Combine(FileManager.projectDirectory, $"ProfilePictures\\{currentEmployee.EmployeeId}.png");
                if (File.Exists(picturePath))
                {
                    if (profilePicture.Image != null)
                    {
                        profilePicture.Image.Dispose();
                    }
                    profilePicture.Image = Image.FromFile(picturePath);
                }
                else
                {
                    // Create an initial-based avatar
                    if (profilePicture.Image != null)
                    {
                        profilePicture.Image.Dispose();
                    }
                    Bitmap avatarBitmap = CreateInitialsAvatar(currentEmployee.Name);
                    profilePicture.Image = avatarBitmap;
                }

                // Populate personal information
                Label? nameValueLabel = personalInfoPanel.Controls.Find("lblNameValue", true).FirstOrDefault() as Label;
                if (nameValueLabel != null)
                {
                    nameValueLabel.Text = currentEmployee.Name;
                }

                // Update the read-only labels instead of textboxes
                Label? emailValueLabel = personalInfoPanel.Controls.Find("lblEmailValue", true).FirstOrDefault() as Label;
                if (emailValueLabel != null)
                {
                    emailValueLabel.Text = currentEmployee.Email;
                }

                Label? phoneValueLabel = personalInfoPanel.Controls.Find("lblPhoneValue", true).FirstOrDefault() as Label;
                if (phoneValueLabel != null)
                {
                    phoneValueLabel.Text = currentEmployee.Phone;
                }

                Label? addressValueLabel = personalInfoPanel.Controls.Find("lblAddressValue", true).FirstOrDefault() as Label;
                if (addressValueLabel != null)
                {
                    addressValueLabel.Text = currentEmployee.Address;
                }

                Label? dobValueLabel = personalInfoPanel.Controls.Find("lblDOBValue", true).FirstOrDefault() as Label;
                if (dobValueLabel != null)
                {
                    dobValueLabel.Text = currentEmployee.DateOfBirth.ToShortDateString();
                }

                Label? ageValueLabel = personalInfoPanel.Controls.Find("lblAgeValue", true).FirstOrDefault() as Label;
                if (ageValueLabel != null)
                {
                    ageValueLabel.Text = currentEmployee.CalculateAge() + " years";
                }

                // Populate employment information
                Label? idValueLabel = employmentInfoPanel.Controls.Find("lblEmployeeIDValue", true).FirstOrDefault() as Label;
                if (idValueLabel != null)
                {
                    idValueLabel.Text = currentEmployee.EmployeeId;
                }

                Label? positionValueLabel = employmentInfoPanel.Controls.Find("lblPositionValue", true).FirstOrDefault() as Label;
                if (positionValueLabel != null)
                {
                    positionValueLabel.Text = currentEmployee.Position;
                }

                Label? departmentValueLabel = employmentInfoPanel.Controls.Find("lblDepartmentValue", true).FirstOrDefault() as Label;
                if (departmentValueLabel != null)
                {
                    departmentValueLabel.Text = currentEmployee.DepartmentId;
                }

                Label? hireDateValueLabel = employmentInfoPanel.Controls.Find("lblHireDateValue", true).FirstOrDefault() as Label;
                if (hireDateValueLabel != null)
                {
                    hireDateValueLabel.Text = currentEmployee.HireDate.ToShortDateString();
                }

                Label? serviceValueLabel = employmentInfoPanel.Controls.Find("lblYearsOfServiceValue", true).FirstOrDefault() as Label;
                if (serviceValueLabel != null)
                {
                    serviceValueLabel.Text = currentEmployee.CalculateYearsOfService() + " years";
                }

                Label? salaryValueLabel = employmentInfoPanel.Controls.Find("lblSalaryValue", true).FirstOrDefault() as Label;
                if (salaryValueLabel != null)
                {
                    salaryValueLabel.Text = "$" + currentEmployee.BaseSalary.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap CreateInitialsAvatar(string name)
        {
            // Create a bitmap for the avatar
            Bitmap avatarBitmap = new(80, 80);

            // Get initials from name
            string[] nameParts = name.Split(' ');
            string initials = "";

            if (nameParts.Length > 0)
                initials += nameParts[0][0];
            if (nameParts.Length > 1)
                initials += nameParts[nameParts.Length - 1][0];

            initials = initials.ToUpper();

            // Create graphics object
            using (Graphics g = Graphics.FromImage(avatarBitmap))
            {
                // Fill background
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (SolidBrush brush = new(Color.FromArgb(60, 60, 60)))
                {
                    g.FillEllipse(brush, 0, 0, 79, 79);
                }

                // Draw initials
                using (Font font = new("Arial", 24, FontStyle.Bold))
                {
                    // Measure string to center it
                    SizeF textSize = g.MeasureString(initials, font);
                    float x = (80 - textSize.Width) / 2;
                    float y = (80 - textSize.Height) / 2;

                    // Draw initials
                    using (SolidBrush textBrush = new(Color.White))
                    {
                        g.DrawString(initials, font, textBrush, x, y);
                    }
                }
            }

            return avatarBitmap;
        }
    }

    // Helper class for employee selection combobox
    public class EmployeeSelectionItem
    {
        public string EmployeeId { get; }
        public string DisplayName { get; }

        public EmployeeSelectionItem(string employeeId, string displayName)
        {
            EmployeeId = employeeId;
            DisplayName = displayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
