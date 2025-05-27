namespace HRManagementSystem
{
    public class DashboardOverview : Form
    {
        // Event for module button clicks that MainForm can subscribe to
        public event EventHandler<ModuleAccessEventArgs> ModuleAccessRequested;

        // Dashboard panels
        private Panel pnlDashboard = null!;
        private Panel pnlCompanyInfo = null!;
        private Panel pnlStatistics = null!;
        private Panel pnlModules = null!;

        // Reference to MainForm can be optional depending on your approach
        private MainForm mainForm;

        // References to services
        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;

        // Statistics data
        private int _employeeCount;
        private int _departmentCount;

        public DashboardOverview(MainForm mainForm = null)
        {
            this.mainForm = mainForm;

            // Initialize services
            _employeeService = new EmployeeService(KetNoi.str);
            _departmentService = new DepartmentService(KetNoi.str);

            // Load initial statistics
            LoadStatistics();

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Text = "Human Resource Management System";
            BackColor = Color.FromArgb(240, 240, 240);

            // Create dashboard panel
            CreateDashboardPanel();

            Resize += Form_Resize; // Add resize event handler
        }

        // Method to load statistics from services
        private void LoadStatistics()
        {
            try
            {
                List<Employee> employees = _employeeService.GetAll();
                _employeeCount = employees?.Count ?? 0;

                List<Department> departments = _departmentService.GetAll();
                _departmentCount = departments?.Count ?? 0;
            }
            catch (Exception ex)
            {
                // Fallback to zeros if there's an error
                _employeeCount = 0;
                _departmentCount = 0;
                MessageBox.Show($"Error loading statistics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to update statistics cards with real data
        public void UpdateStatistics()
        {
            LoadStatistics();

            // Find and update the employee and department statistic cards
            Panel employeeCard = FindCardByTag("stat-card-1");
            Panel departmentCard = FindCardByTag("stat-card-2");

            if (employeeCard != null)
            {
                foreach (Control c in employeeCard.Controls)
                {
                    if (c is Label lbl && lbl.Tag?.ToString() == "StatValue")
                    {
                        lbl.Text = $"{_employeeCount}";
                    }
                }
            }

            if (departmentCard != null)
            {
                foreach (Control c in departmentCard.Controls)
                {
                    if (c is Label lbl && lbl.Tag?.ToString() == "StatValue")
                    {
                        lbl.Text = $"{_departmentCount}";
                    }
                }
            }
        }

        private void CreateDashboardPanel()
        {
            pnlDashboard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                AutoScroll = true // Enable scrolling
            };
            Controls.Add(pnlDashboard);

            // Create top section panel to hold company info and statistics in a two-column layout
            Panel pnlTopSection = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(ClientSize.Width - 60, 250),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlDashboard.Controls.Add(pnlTopSection);

            // Create company info section (left column)
            pnlCompanyInfo = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(pnlTopSection.Width / 2 - 20, 250),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };
            pnlTopSection.Controls.Add(pnlCompanyInfo);

            // Create statistics section (right column)
            pnlStatistics = new Panel
            {
                Location = new Point(pnlTopSection.Width / 2, 0),
                Size = new Size(pnlTopSection.Width / 2, 250),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };
            pnlTopSection.Controls.Add(pnlStatistics);

            // Create modules panel (bottom)
            pnlModules = new Panel
            {
                Location = new Point(0, 290), // Set location to match top section (20px padding)
                Size = new Size(ClientSize.Width - 60, 300), // Match width calculation used for top section
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoScroll = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlDashboard.Controls.Add(pnlModules);

            Label lblModulesTitle = new Label
            {
                Text = "Access Modules",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(12, 12)
            };
            pnlModules.Controls.Add(lblModulesTitle);

            // Populate the sections
            PopulateCompanyInfo();
            PopulateStatistics();
            PopulateModules();
        }

        private void PopulateCompanyInfo()
        {
            // Company Name (larger and bolder)
            Label lblCompanyName = new Label
            {
                Text = "HVH Organization ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(-4, 0),
                ForeColor = Color.Black
            };
            pnlCompanyInfo.Controls.Add(lblCompanyName);

            // Company Tagline (medium size, muted color)
            Label lblTagline = new Label
            {
                Text = "Đổi mới mỗi ngày, vững bước tương lai",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(0, 50),
                ForeColor = Color.Gray
            };
            pnlCompanyInfo.Controls.Add(lblTagline);

            // Contact info with icons
            CreateContactItem("map-pin", "279 Nguyen Tri Phuong Street, Ward 5, District 10, Ho Chi Minh City", 0, 80);
            CreateContactItem("phone", "(555) 123-4567", 0, 110);
            CreateContactItem("mail", "lenguyenminhhoang18@gmail.com", 0, 140);
           
        }

        private void CreateContactItem(string iconType, string text, int x, int y)
        {
            // Panel to contain the icon and text
            Panel pnlContact = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(pnlCompanyInfo.Width - 20, 30),
                BackColor = Color.Transparent
            };
            pnlCompanyInfo.Controls.Add(pnlContact);

            // Create icon label (using Unicode as a simple substitute for SVG icons)
            Label lblIcon = new Label
            {
                AutoSize = true,
                Location = new Point(0, 0),
                Size = new Size(24, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI Symbol", 12)
            };

            // Set appropriate Unicode symbol based on icon type
            switch (iconType)
            {
                case "map-pin":
                    lblIcon.Text = "📍";
                    break;
                case "phone":
                    lblIcon.Text = "📞";
                    break;
                case "mail":
                    lblIcon.Text = "✉️";
                    break;
                case "globe":
                    lblIcon.Text = "🌐";
                    break;
            }

            pnlContact.Controls.Add(lblIcon);

            // Text label
            Label lblText = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(pnlContact.Width - 30, 24),
                Location = new Point(30, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlContact.Controls.Add(lblText);
        }

        private void PopulateStatistics()
        {
            // Calculate grid layout dimensions
            int cardWidth = (pnlStatistics.Width / 2) - 10; // 10px gap between cards
            int cardHeight = 110;

            // Create statistics cards in a 2x2 grid with real data
            CreateStatCard("Total Employees", _employeeCount.ToString(), 0, 0, cardWidth, cardHeight, "stat-card-1");
            CreateStatCard("Departments", _departmentCount.ToString(), cardWidth + 10, 0, cardWidth, cardHeight, "stat-card-2");
            CreateStatCard("Retention Rate", "94%", 0, cardHeight + 10, cardWidth, cardHeight, "stat-card-3");
            CreateStatCard("Employee Satisfaction", "4.5/5", cardWidth + 10, cardHeight + 10, cardWidth, cardHeight, "stat-card-4");
        }

        private void CreateStatCard(string title, string value, int x, int y, int width, int height, string tag)
        {
            // Create card panel with border and slight shadow effect
            Panel pnlCard = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Tag = tag
            };

            // Add border and shadow effect using the Paint event
            pnlCard.Paint += PaintStatCard;

            pnlStatistics.Controls.Add(pnlCard);

            // Title label (smaller, muted color)
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(width - 20, 20),
                Location = new Point(15, 15),
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = "StatTitle"
            };
            pnlCard.Controls.Add(lblTitle);

            // Value label (larger, bold)
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(width - 20, 30),
                Location = new Point(15, 50),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.Black,
                Tag = "StatValue"
            };
            pnlCard.Controls.Add(lblValue);
        }

        // Separate method for the Paint event to make it easier to maintain
        private void PaintStatCard(object sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
            {
                // Draw a complete rectangle border around the entire panel
                Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
        }

        private void PopulateModules()
        {
            // Clear any existing module cards first to prevent duplications
            ClearModuleCards();

            // Module definitions
            string[] moduleNames = new string[] {
                "Employees Management",
                "Departments Management",
                "Leave & Attendance",
                "Payroll",
            };

            string[] descriptions = new string[] {
                "Add, edit, and manage employee records, including salary adjustments and promotions",
                "Create and manage departments, assign managers and employees",
                "Track attendance and manage leave requests",
                "Manage employee compensation and process payroll",
            };

            // Create module cards with initial position (will be repositioned in RecalculateModuleCardLayout)
            for (int i = 0; i < moduleNames.Length; i++)
            {
                CreateModuleCard(moduleNames[i], descriptions[i], 0, 0);
            }

            // Position the cards correctly
            RecalculateModuleCardLayout();
        }

        private void ClearModuleCards()
        {
            // Find and remove all existing module cards
            List<Control> controlsToRemove = new List<Control>();

            foreach (Control control in pnlModules.Controls)
            {
                // Identify module cards (but not the title label)
                if (control is Panel && control.Tag?.ToString()?.StartsWith("ModuleCard") == true)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Remove the identified controls
            foreach (Control control in controlsToRemove)
            {
                pnlModules.Controls.Remove(control);
                control.Dispose(); // Properly dispose of the control
            }
        }

        private void CreateModuleCard(string title, string description, int x, int y)
        {
            // Calculate card width to fit 4 cards in a single row with proper spacing
            int cardWidth = (pnlModules.Width - 45) / 4; // Adjusted for 4 cards with 5 padding spaces

            Panel pnlModule = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(cardWidth, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Tag = "ModuleCard" + title.Replace(" ", "") // Unique tag for each card
            };

            // Add border and shadow effect using the Paint event, same as stat cards
            pnlModule.Paint += PaintStatCard;

            pnlModules.Controls.Add(pnlModule);

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 15),
                Tag = "ModuleTitle"
            };
            pnlModule.Controls.Add(lblTitle);

            Label lblDescription = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Size = new Size(cardWidth - 30, 60),
                Location = new Point(15, 45),
                Tag = "ModuleDescription"
            };
            pnlModule.Controls.Add(lblDescription);

            Button btnAccess = new Button
            {
                Text = "Access Module",
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                Location = new Point(15, 105),
                Cursor = Cursors.Hand,
                Tag = title
            };
            btnAccess.FlatAppearance.BorderSize = 0;

            // Connect button click event to handler
            btnAccess.Click += ModuleButton_Click;

            pnlModule.Controls.Add(btnAccess);
        }

        // Handler for module button clicks
        private void ModuleButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                string moduleName = button.Tag.ToString();

                // Fire the event with module information
                ModuleAccessRequested?.Invoke(this, new ModuleAccessEventArgs(moduleName));
            }
        }

        // Handle form resizing to adjust dashboard panel and its contents
        private void Form_Resize(object sender, EventArgs e)
        {
            if (pnlDashboard != null)
            {
                // Find the top section panel
                Panel pnlTopSection = null;
                foreach (Control c in pnlDashboard.Controls)
                {
                    if (c is Panel && c.Location.Y == 20 && c != pnlModules)
                    {
                        pnlTopSection = c as Panel;
                        break;
                    }
                }

                if (pnlTopSection != null)
                {
                    // Adjust top section panel size
                    pnlTopSection.Width = ClientSize.Width - 60; // Consistent padding (20px on each side + 20px from parent panel)

                    // Recalculate company info and stats panel widths
                    if (pnlCompanyInfo != null)
                    {
                        pnlCompanyInfo.Width = pnlTopSection.Width / 2 - 20;

                        // Adjust contact items width
                        foreach (Control c in pnlCompanyInfo.Controls)
                        {
                            if (c is Panel contactPanel)
                            {
                                contactPanel.Width = pnlCompanyInfo.Width - 20;

                                // Adjust text label width
                                foreach (Control child in contactPanel.Controls)
                                {
                                    if (child is Label lbl && child.Location.X > 0)
                                    {
                                        lbl.Width = contactPanel.Width - 30;
                                    }
                                }
                            }
                        }
                    }

                    if (pnlStatistics != null)
                    {
                        pnlStatistics.Location = new Point(pnlTopSection.Width / 2, 0);
                        pnlStatistics.Width = pnlTopSection.Width / 2;

                        // Fixed: Recalculate statistics card layout
                        RecalculateStatisticsLayout();
                    }
                }

                // Adjust modules panel
                if (pnlModules != null)
                {
                    pnlModules.Width = ClientSize.Width - 30; // Match top section width calculation
                    RecalculateModuleCardLayout();

                    // Force redraw of all module cards
                    foreach (Control c in pnlModules.Controls)
                    {
                        if (c is Panel panel && panel.Tag != null && panel.Tag.ToString().StartsWith("ModuleCard"))
                        {
                            panel.Invalidate();
                        }
                    }
                }
            }
        }

        private void RecalculateStatisticsLayout()
        {
            // Fixed calculation of the stat cards layout
            int cardWidth = (pnlStatistics.Width / 2) - 10;
            int cardHeight = 110;

            // Get each card by tag to ensure correct positioning
            Panel topLeft = FindCardByTag("stat-card-1");
            Panel topRight = FindCardByTag("stat-card-2");
            Panel bottomLeft = FindCardByTag("stat-card-3");
            Panel bottomRight = FindCardByTag("stat-card-4");

            if (topLeft != null && topRight != null && bottomLeft != null && bottomRight != null)
            {
                // Position and resize cards
                topLeft.Location = new Point(0, 0);
                topLeft.Width = cardWidth;

                topRight.Location = new Point(cardWidth + 10, 0);
                topRight.Width = cardWidth;

                bottomLeft.Location = new Point(0, cardHeight + 10);
                bottomLeft.Width = cardWidth;

                bottomRight.Location = new Point(cardWidth + 10, cardHeight + 10);
                bottomRight.Width = cardWidth;

                // Update labels inside each card
                UpdateCardLabels(topLeft, cardWidth);
                UpdateCardLabels(topRight, cardWidth);
                UpdateCardLabels(bottomLeft, cardWidth);
                UpdateCardLabels(bottomRight, cardWidth);

                // Force redraw of all stat cards to update borders
                topLeft.Invalidate();
                topRight.Invalidate();
                bottomLeft.Invalidate();
                bottomRight.Invalidate();
            }
        }

        private Panel FindCardByTag(string tag)
        {
            foreach (Control c in pnlStatistics.Controls)
            {
                if (c is Panel panel && panel.Tag != null && panel.Tag.ToString() == tag)
                {
                    return panel;
                }
            }
            return null;
        }

        private void UpdateCardLabels(Panel card, int cardWidth)
        {
            if (card != null)
            {
                foreach (Control c in card.Controls)
                {
                    if (c is Label lbl)
                    {
                        if (lbl.Tag?.ToString() == "StatTitle" || lbl.Tag?.ToString() == "StatValue")
                        {
                            lbl.Width = cardWidth - 30;
                        }
                    }
                }
            }
        }

        private void RecalculateModuleCardLayout()
        {
            int padding = 15;

            // Calculate total width properly to match the top section's width
            int totalWidth = pnlModules.Width;
            int availableWidth = totalWidth - (padding * 5); // 5 padding spaces for 4 cards
            int cardWidth = availableWidth / 4; // Divide by 4 for four cards

            // Find all module cards
            List<Panel> moduleCards = new List<Panel>();
            foreach (Control control in pnlModules.Controls)
            {
                if (control is Panel pnl && pnl.Tag != null && pnl.Tag?.ToString()?.StartsWith("ModuleCard") == true)
                {
                    moduleCards.Add(pnl);
                }
            }

            // Position cards in a single row layout
            for (int i = 0; i < moduleCards.Count; i++)
            {
                Panel pnl = moduleCards[i];
                pnl.Location = new Point(padding + (i * (cardWidth + padding)), 60);
                pnl.Size = new Size(cardWidth, 150); // Set both width and height to ensure proper sizing

                // Force redraw to update borders properly
                pnl.Invalidate();

                // Resize description label and ensure access button stays in position
                foreach (Control c in pnl.Controls)
                {
                    if (c is Label lbl && lbl.Tag != null && lbl.Tag.ToString() == "ModuleDescription")
                    {
                        lbl.Width = cardWidth - 30;
                    }
                    else if (c is Button btn)
                    {
                        btn.Location = new Point(15, 105);
                    }
                }
            }
        }
    }
}