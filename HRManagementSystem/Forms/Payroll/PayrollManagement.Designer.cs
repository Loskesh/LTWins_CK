namespace HRManagementSystem
{
    partial class PayrollManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            btnNextMonth = new Button();
            lblMonth = new Label();
            btnPreviousMonth = new Button();
            lblTitle = new Label();
            panelButtons = new Panel();
            btnRunPayroll = new Button();
            btnSearch = new Button();
            btnReport = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnNewPayroll = new Button();
            dgvPayrolls = new DataGridView();
            panelBottom = new Panel();
            lblSalaryRange = new Label();
            lblAverageSalary = new Label();
            lblTotalPayroll = new Label();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayrolls).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(btnNextMonth);
            panelTop.Controls.Add(lblMonth);
            panelTop.Controls.Add(btnPreviousMonth);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 75);
            panelTop.TabIndex = 0;
            // 
            // btnNextMonth
            // 
            btnNextMonth.FlatStyle = FlatStyle.Popup;
            btnNextMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnNextMonth.Location = new Point(600, 19);
            btnNextMonth.Margin = new Padding(3, 4, 3, 4);
            btnNextMonth.Name = "btnNextMonth";
            btnNextMonth.Size = new Size(40, 38);
            btnNextMonth.TabIndex = 3;
            btnNextMonth.Text = ">";
            btnNextMonth.UseVisualStyleBackColor = true;
            btnNextMonth.Click += btnNextMonth_Click;
            // 
            // lblMonth
            // 
            lblMonth.AutoSize = true;
            lblMonth.Font = new Font("Microsoft Sans Serif", 10F);
            lblMonth.Location = new Point(522, 28);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(68, 20);
            lblMonth.TabIndex = 2;
            lblMonth.Text = "03/2025";
            lblMonth.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPreviousMonth
            // 
            btnPreviousMonth.FlatStyle = FlatStyle.Popup;
            btnPreviousMonth.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnPreviousMonth.Location = new Point(470, 19);
            btnPreviousMonth.Margin = new Padding(3, 4, 3, 4);
            btnPreviousMonth.Name = "btnPreviousMonth";
            btnPreviousMonth.Size = new Size(40, 38);
            btnPreviousMonth.TabIndex = 1;
            btnPreviousMonth.Text = "<";
            btnPreviousMonth.UseVisualStyleBackColor = true;
            btnPreviousMonth.Click += btnPreviousMonth_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 19);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(358, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "PAYROLL MANAGEMENT";
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(btnRunPayroll);
            panelButtons.Controls.Add(btnSearch);
            panelButtons.Controls.Add(btnReport);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnEdit);
            panelButtons.Controls.Add(btnNewPayroll);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(0, 75);
            panelButtons.Margin = new Padding(3, 4, 3, 4);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(800, 62);
            panelButtons.TabIndex = 1;
            // 
            // btnRunPayroll
            // 
            btnRunPayroll.FlatStyle = FlatStyle.Popup;
            btnRunPayroll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRunPayroll.Location = new Point(800, 12);
            btnRunPayroll.Margin = new Padding(3, 4, 3, 4);
            btnRunPayroll.Name = "btnRunPayroll";
            btnRunPayroll.Size = new Size(120, 38);
            btnRunPayroll.TabIndex = 6;
            btnRunPayroll.Text = "RUN PAYROLL";
            btnRunPayroll.UseVisualStyleBackColor = true;
            btnRunPayroll.Click += btnRunPayroll_Click;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(651, 12);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 38);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "REPORT";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnReport
            // 
            btnReport.FlatStyle = FlatStyle.Popup;
            btnReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReport.Location = new Point(495, 12);
            btnReport.Margin = new Padding(3, 4, 3, 4);
            btnReport.Name = "btnReport";
            btnReport.Size = new Size(100, 38);
            btnReport.TabIndex = 4;
            btnReport.Text = "SEARCH";
            btnReport.UseVisualStyleBackColor = true;
            btnReport.Click += btnReport_Click;
            // 
            // btnDelete
            // 
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(329, 12);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 38);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.Location = new Point(170, 12);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 38);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "UPDATE";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnNewPayroll
            // 
            btnNewPayroll.FlatStyle = FlatStyle.Popup;
            btnNewPayroll.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNewPayroll.Location = new Point(20, 12);
            btnNewPayroll.Margin = new Padding(3, 4, 3, 4);
            btnNewPayroll.Name = "btnNewPayroll";
            btnNewPayroll.Size = new Size(110, 38);
            btnNewPayroll.TabIndex = 0;
            btnNewPayroll.Text = "ADD";
            btnNewPayroll.UseVisualStyleBackColor = true;
            btnNewPayroll.Click += btnNewPayroll_Click;
            // 
            // dgvPayrolls
            // 
            dgvPayrolls.AllowUserToAddRows = false;
            dgvPayrolls.AllowUserToDeleteRows = false;
            dgvPayrolls.BackgroundColor = Color.White;
            dgvPayrolls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayrolls.Dock = DockStyle.Fill;
            dgvPayrolls.GridColor = Color.White;
            dgvPayrolls.Location = new Point(0, 137);
            dgvPayrolls.Margin = new Padding(3, 4, 3, 4);
            dgvPayrolls.MultiSelect = false;
            dgvPayrolls.Name = "dgvPayrolls";
            dgvPayrolls.ReadOnly = true;
            dgvPayrolls.RowHeadersWidth = 51;
            dgvPayrolls.RowTemplate.Height = 24;
            dgvPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayrolls.Size = new Size(800, 363);
            dgvPayrolls.TabIndex = 2;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(lblSalaryRange);
            panelBottom.Controls.Add(lblAverageSalary);
            panelBottom.Controls.Add(lblTotalPayroll);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 500);
            panelBottom.Margin = new Padding(3, 4, 3, 4);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(800, 62);
            panelBottom.TabIndex = 3;
            // 
            // lblSalaryRange
            // 
            lblSalaryRange.AutoSize = true;
            lblSalaryRange.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSalaryRange.Location = new Point(548, 19);
            lblSalaryRange.Name = "lblSalaryRange";
            lblSalaryRange.Size = new Size(203, 28);
            lblSalaryRange.TabIndex = 2;
            lblSalaryRange.Text = "Min-Max: 0 - 0 VND";
            // 
            // lblAverageSalary
            // 
            lblAverageSalary.AutoSize = true;
            lblAverageSalary.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAverageSalary.Location = new Point(279, 19);
            lblAverageSalary.Name = "lblAverageSalary";
            lblAverageSalary.Size = new Size(174, 28);
            lblAverageSalary.TabIndex = 1;
            lblAverageSalary.Text = "AVERAGE: 0 VND";
            // 
            // lblTotalPayroll
            // 
            lblTotalPayroll.AutoSize = true;
            lblTotalPayroll.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalPayroll.Location = new Point(20, 19);
            lblTotalPayroll.Name = "lblTotalPayroll";
            lblTotalPayroll.Size = new Size(145, 28);
            lblTotalPayroll.TabIndex = 0;
            lblTotalPayroll.Text = "TOTAL: 0 VND";
            // 
            // PayrollManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 562);
            Controls.Add(dgvPayrolls);
            Controls.Add(panelBottom);
            Controls.Add(panelButtons);
            Controls.Add(panelTop);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PayrollManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PAYROLL MANAGEMENT";
            Load += PayrollManagement_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayrolls).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNewPayroll;
        private System.Windows.Forms.DataGridView dgvPayrolls;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblSalaryRange;
        private System.Windows.Forms.Label lblAverageSalary;
        private System.Windows.Forms.Label lblTotalPayroll;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Button btnPreviousMonth;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRunPayroll;
    }
}