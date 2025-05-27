namespace HRManagementSystem
{
    partial class PayrollReport
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
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            btnSearch = new Button();
            cboPaymentStatus = new ComboBox();
            label4 = new Label();
            cboEmployee = new ComboBox();
            label3 = new Label();
            dtpToDate = new DateTimePicker();
            label2 = new Label();
            dtpFromDate = new DateTimePicker();
            label1 = new Label();
            panel2 = new Panel();
            dgvPayroll = new DataGridView();
            panel3 = new Panel();
            lblTotalNetSalary = new Label();
            label9 = new Label();
            lblTotalDeductions = new Label();
            label8 = new Label();
            lblTotalAllowances = new Label();
            label7 = new Label();
            lblTotalBaseSalary = new Label();
            label6 = new Label();
            lblTotalCount = new Label();
            label5 = new Label();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayroll).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(984, 150);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(cboPaymentStatus);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cboEmployee);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(dtpToDate);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dtpFromDate);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 15);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(960, 125);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "SEARCH";
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Location = new Point(849, 79);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 35);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cboPaymentStatus
            // 
            cboPaymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaymentStatus.FormattingEnabled = true;
            cboPaymentStatus.Location = new Point(617, 81);
            cboPaymentStatus.Margin = new Padding(3, 4, 3, 4);
            cboPaymentStatus.Name = "cboPaymentStatus";
            cboPaymentStatus.Size = new Size(200, 28);
            cboPaymentStatus.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(517, 85);
            label4.Name = "label4";
            label4.Size = new Size(53, 20);
            label4.TabIndex = 6;
            label4.Text = "Status";
            // 
            // cboEmployee
            // 
            cboEmployee.BackColor = Color.White;
            cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployee.FormattingEnabled = true;
            cboEmployee.Location = new Point(617, 36);
            cboEmployee.Margin = new Padding(3, 4, 3, 4);
            cboEmployee.Name = "cboEmployee";
            cboEmployee.Size = new Size(332, 28);
            cboEmployee.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(517, 40);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 4;
            label3.Text = "Employee";
            // 
            // dtpToDate
            // 
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.Location = new Point(120, 81);
            dtpToDate.Margin = new Padding(3, 4, 3, 4);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(200, 27);
            dtpToDate.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 85);
            label2.Name = "label2";
            label2.Size = new Size(26, 20);
            label2.TabIndex = 2;
            label2.Text = "To";
            // 
            // dtpFromDate
            // 
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.Location = new Point(120, 36);
            dtpFromDate.Margin = new Padding(3, 4, 3, 4);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(200, 27);
            dtpFromDate.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 40);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 0;
            label1.Text = "From";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(dgvPayroll);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 150);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(12, 0, 12, 0);
            panel2.Size = new Size(984, 551);
            panel2.TabIndex = 1;
            // 
            // dgvPayroll
            // 
            dgvPayroll.AllowUserToAddRows = false;
            dgvPayroll.AllowUserToDeleteRows = false;
            dgvPayroll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayroll.BackgroundColor = Color.White;
            dgvPayroll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayroll.Dock = DockStyle.Fill;
            dgvPayroll.Location = new Point(12, 0);
            dgvPayroll.Margin = new Padding(3, 4, 3, 4);
            dgvPayroll.Name = "dgvPayroll";
            dgvPayroll.ReadOnly = true;
            dgvPayroll.RowHeadersWidth = 51;
            dgvPayroll.RowTemplate.Height = 24;
            dgvPayroll.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayroll.Size = new Size(960, 551);
            dgvPayroll.TabIndex = 0;
            dgvPayroll.CellFormatting += dgvPayroll_CellFormatting;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(lblTotalNetSalary);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(lblTotalDeductions);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(lblTotalAllowances);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(lblTotalBaseSalary);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(lblTotalCount);
            panel3.Controls.Add(label5);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 576);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(984, 125);
            panel3.TabIndex = 2;
            // 
            // lblTotalNetSalary
            // 
            lblTotalNetSalary.AutoSize = true;
            lblTotalNetSalary.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalNetSalary.Location = new Point(658, 84);
            lblTotalNetSalary.Name = "lblTotalNetSalary";
            lblTotalNetSalary.Size = new Size(15, 16);
            lblTotalNetSalary.TabIndex = 9;
            lblTotalNetSalary.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.Location = new Point(517, 81);
            label9.Name = "label9";
            label9.Size = new Size(121, 20);
            label9.TabIndex = 8;
            label9.Text = "Total NetSalary:";
            // 
            // lblTotalDeductions
            // 
            lblTotalDeductions.AutoSize = true;
            lblTotalDeductions.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalDeductions.Location = new Point(658, 52);
            lblTotalDeductions.Name = "lblTotalDeductions";
            lblTotalDeductions.Size = new Size(15, 16);
            lblTotalDeductions.TabIndex = 7;
            lblTotalDeductions.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.Location = new Point(517, 48);
            label8.Name = "label8";
            label8.Size = new Size(131, 20);
            label8.TabIndex = 6;
            label8.Text = "Total Deductions:";
            // 
            // lblTotalAllowances
            // 
            lblTotalAllowances.AutoSize = true;
            lblTotalAllowances.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAllowances.Location = new Point(658, 18);
            lblTotalAllowances.Name = "lblTotalAllowances";
            lblTotalAllowances.Size = new Size(15, 16);
            lblTotalAllowances.TabIndex = 5;
            lblTotalAllowances.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(517, 14);
            label7.Name = "label7";
            label7.Size = new Size(135, 20);
            label7.TabIndex = 4;
            label7.Text = "Total Allowances: ";
            // 
            // lblTotalBaseSalary
            // 
            lblTotalBaseSalary.AutoSize = true;
            lblTotalBaseSalary.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalBaseSalary.Location = new Point(154, 51);
            lblTotalBaseSalary.Name = "lblTotalBaseSalary";
            lblTotalBaseSalary.Size = new Size(15, 16);
            lblTotalBaseSalary.TabIndex = 3;
            lblTotalBaseSalary.Text = "0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(20, 48);
            label6.Name = "label6";
            label6.Size = new Size(128, 20);
            label6.TabIndex = 2;
            label6.Text = "Total BaseSalary:";
            // 
            // lblTotalCount
            // 
            lblTotalCount.AutoSize = true;
            lblTotalCount.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalCount.Location = new Point(142, 17);
            lblTotalCount.Name = "lblTotalCount";
            lblTotalCount.Size = new Size(15, 16);
            lblTotalCount.TabIndex = 1;
            lblTotalCount.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(20, 14);
            label5.Name = "label5";
            label5.Size = new Size(116, 20);
            label5.TabIndex = 0;
            label5.Text = "Total of record:";
            // 
            // PayrollReport
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 701);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1000, 738);
            Name = "PayrollReport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SEARCH";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayroll).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboPaymentStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvPayroll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalNetSalary;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalDeductions;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalAllowances;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalBaseSalary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label label5;
    }
}
