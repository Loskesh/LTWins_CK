namespace HRManagementSystem
{
    partial class PayrollSearch
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
            lblTitle = new Label();
            panelCenter = new Panel();
            dgvPayrolls = new DataGridView();
            panelBottom = new Panel();
            btnExport = new Button();
            btnClose = new Button();
            lblAverageSalary = new Label();
            lblTotalAmount = new Label();
            lblTotalRecords = new Label();
            panelTop.SuspendLayout();
            panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayrolls).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 5, 4, 5);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1176, 92);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(400, 26);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(340, 39);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "PAYROLL REPORT";
            // 
            // panelCenter
            // 
            panelCenter.Controls.Add(dgvPayrolls);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(0, 92);
            panelCenter.Margin = new Padding(4, 5, 4, 5);
            panelCenter.Name = "panelCenter";
            panelCenter.Padding = new Padding(13, 15, 13, 15);
            panelCenter.Size = new Size(1176, 532);
            panelCenter.TabIndex = 1;
            // 
            // dgvPayrolls
            // 
            dgvPayrolls.AllowUserToAddRows = false;
            dgvPayrolls.AllowUserToDeleteRows = false;
            dgvPayrolls.BackgroundColor = Color.White;
            dgvPayrolls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayrolls.Dock = DockStyle.Fill;
            dgvPayrolls.GridColor = Color.White;
            dgvPayrolls.Location = new Point(13, 15);
            dgvPayrolls.Margin = new Padding(4, 5, 4, 5);
            dgvPayrolls.Name = "dgvPayrolls";
            dgvPayrolls.ReadOnly = true;
            dgvPayrolls.RowHeadersWidth = 51;
            dgvPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayrolls.Size = new Size(1150, 502);
            dgvPayrolls.TabIndex = 0;
            dgvPayrolls.CellDoubleClick += dgvPayrolls_CellDoubleClick;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.White;
            panelBottom.Controls.Add(btnExport);
            panelBottom.Controls.Add(btnClose);
            panelBottom.Controls.Add(lblAverageSalary);
            panelBottom.Controls.Add(lblTotalAmount);
            panelBottom.Controls.Add(lblTotalRecords);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 624);
            panelBottom.Margin = new Padding(4, 5, 4, 5);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1176, 77);
            panelBottom.TabIndex = 2;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(896, 15);
            btnExport.Margin = new Padding(4, 5, 4, 5);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(133, 46);
            btnExport.TabIndex = 4;
            btnExport.Text = "PRINT";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.Location = new Point(1037, 15);
            btnClose.Margin = new Padding(4, 5, 4, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(133, 46);
            btnClose.TabIndex = 3;
            btnClose.Text = "CLOSED";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // lblAverageSalary
            // 
            lblAverageSalary.AutoSize = true;
            lblAverageSalary.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblAverageSalary.Location = new Point(594, 23);
            lblAverageSalary.Margin = new Padding(4, 0, 4, 0);
            lblAverageSalary.Name = "lblAverageSalary";
            lblAverageSalary.Size = new Size(146, 31);
            lblAverageSalary.TabIndex = 2;
            lblAverageSalary.Text = "Average: 0 đ";
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTotalAmount.Location = new Point(316, 23);
            lblTotalAmount.Margin = new Padding(4, 0, 4, 0);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(113, 31);
            lblTotalAmount.TabIndex = 1;
            lblTotalAmount.Text = "Total: 0 đ";
            // 
            // lblTotalRecords
            // 
            lblTotalRecords.AutoSize = true;
            lblTotalRecords.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblTotalRecords.Location = new Point(43, 23);
            lblTotalRecords.Margin = new Padding(4, 0, 4, 0);
            lblTotalRecords.Name = "lblTotalRecords";
            lblTotalRecords.Size = new Size(231, 31);
            lblTotalRecords.TabIndex = 0;
            lblTotalRecords.Text = "Number of record: 0";
            // 
            // PayrollSearch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnClose;
            ClientSize = new Size(1176, 701);
            Controls.Add(panelCenter);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1194, 748);
            Name = "PayrollSearch";
            StartPosition = FormStartPosition.CenterParent;
            Text = "REPORT";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayrolls).EndInit();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvPayrolls;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblAverageSalary;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExport;
    }
}
