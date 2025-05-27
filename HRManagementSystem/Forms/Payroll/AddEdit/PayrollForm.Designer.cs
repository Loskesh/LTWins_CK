namespace HRManagementSystem
{
    partial class PayrollForm
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
            groupBox1 = new GroupBox();
            cboEmployee = new ComboBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dtpPayPeriodEnd = new DateTimePicker();
            label3 = new Label();
            dtpPayPeriodStart = new DateTimePicker();
            label2 = new Label();
            groupBox3 = new GroupBox();
            chkIsPaid = new CheckBox();
            txtNetSalary = new TextBox();
            label8 = new Label();
            txtDeductions = new TextBox();
            label7 = new Label();
            txtAllowances = new TextBox();
            label6 = new Label();
            txtBaseSalary = new TextBox();
            label5 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cboEmployee);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(14, 15);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(630, 88);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Employee Information";
            // 
            // cboEmployee
            // 
            cboEmployee.BackColor = Color.White;
            cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployee.FormattingEnabled = true;
            cboEmployee.Location = new Point(155, 38);
            cboEmployee.Margin = new Padding(3, 4, 3, 4);
            cboEmployee.Name = "cboEmployee";
            cboEmployee.Size = new Size(450, 28);
            cboEmployee.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 41);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "Employee";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dtpPayPeriodEnd);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(dtpPayPeriodStart);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(14, 110);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(630, 112);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "PAYROLL PERIOD";
            // 
            // dtpPayPeriodEnd
            // 
            dtpPayPeriodEnd.CustomFormat = "dd/MM/yyyy";
            dtpPayPeriodEnd.Format = DateTimePickerFormat.Custom;
            dtpPayPeriodEnd.Location = new Point(155, 74);
            dtpPayPeriodEnd.Margin = new Padding(3, 4, 3, 4);
            dtpPayPeriodEnd.Name = "dtpPayPeriodEnd";
            dtpPayPeriodEnd.Size = new Size(224, 27);
            dtpPayPeriodEnd.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 74);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 2;
            label3.Text = "End Date";
            // 
            // dtpPayPeriodStart
            // 
            dtpPayPeriodStart.CustomFormat = "dd/MM/yyyy";
            dtpPayPeriodStart.Format = DateTimePickerFormat.Custom;
            dtpPayPeriodStart.Location = new Point(155, 34);
            dtpPayPeriodStart.Margin = new Padding(3, 4, 3, 4);
            dtpPayPeriodStart.Name = "dtpPayPeriodStart";
            dtpPayPeriodStart.Size = new Size(224, 27);
            dtpPayPeriodStart.TabIndex = 1;
            dtpPayPeriodStart.ValueChanged += dtpPayPeriodStart_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 39);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 0;
            label2.Text = "Start Date";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(chkIsPaid);
            groupBox3.Controls.Add(txtNetSalary);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(txtDeductions);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(txtAllowances);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(txtBaseSalary);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new Point(14, 230);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(630, 225);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "SALARY INFORMATION";
            // 
            // chkIsPaid
            // 
            chkIsPaid.AutoSize = true;
            chkIsPaid.Location = new Point(155, 193);
            chkIsPaid.Margin = new Padding(3, 4, 3, 4);
            chkIsPaid.Name = "chkIsPaid";
            chkIsPaid.Size = new Size(73, 24);
            chkIsPaid.TabIndex = 8;
            chkIsPaid.Text = "Ispaid";
            chkIsPaid.UseVisualStyleBackColor = true;
            // 
            // txtNetSalary
            // 
            txtNetSalary.Location = new Point(155, 145);
            txtNetSalary.Margin = new Padding(3, 4, 3, 4);
            txtNetSalary.Name = "txtNetSalary";
            txtNetSalary.ReadOnly = true;
            txtNetSalary.Size = new Size(224, 27);
            txtNetSalary.TabIndex = 7;
            txtNetSalary.Text = "0";
            txtNetSalary.TextAlign = HorizontalAlignment.Right;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(22, 148);
            label8.Name = "label8";
            label8.Size = new Size(82, 20);
            label8.TabIndex = 6;
            label8.Text = "Net Salary";
            // 
            // txtDeductions
            // 
            txtDeductions.Location = new Point(155, 107);
            txtDeductions.Margin = new Padding(3, 4, 3, 4);
            txtDeductions.Name = "txtDeductions";
            txtDeductions.Size = new Size(224, 27);
            txtDeductions.TabIndex = 5;
            txtDeductions.Text = "0";
            txtDeductions.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(22, 110);
            label7.Name = "label7";
            label7.Size = new Size(88, 20);
            label7.TabIndex = 4;
            label7.Text = "Deductions";
            // 
            // txtAllowances
            // 
            txtAllowances.Location = new Point(155, 72);
            txtAllowances.Margin = new Padding(3, 4, 3, 4);
            txtAllowances.Name = "txtAllowances";
            txtAllowances.Size = new Size(224, 27);
            txtAllowances.TabIndex = 3;
            txtAllowances.Text = "0";
            txtAllowances.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 72);
            label6.Name = "label6";
            label6.Size = new Size(92, 20);
            label6.TabIndex = 2;
            label6.Text = "Allowances ";
            // 
            // txtBaseSalary
            // 
            txtBaseSalary.Location = new Point(155, 32);
            txtBaseSalary.Margin = new Padding(3, 4, 3, 4);
            txtBaseSalary.Name = "txtBaseSalary";
            txtBaseSalary.Size = new Size(224, 27);
            txtBaseSalary.TabIndex = 1;
            txtBaseSalary.Text = "0";
            txtBaseSalary.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 35);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 0;
            label5.Text = "Base Salary";
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Location = new Point(424, 462);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(101, 38);
            btnSave.TabIndex = 3;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Location = new Point(542, 462);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(101, 38);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "CANCEL";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PayrollForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnCancel;
            ClientSize = new Size(657, 700); // Increased height to fit new controls
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PayrollForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ADD PAYSLIP";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpPayPeriodEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpPayPeriodStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkIsPaid;
        private System.Windows.Forms.TextBox txtNetSalary;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDeductions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAllowances;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBaseSalary;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
