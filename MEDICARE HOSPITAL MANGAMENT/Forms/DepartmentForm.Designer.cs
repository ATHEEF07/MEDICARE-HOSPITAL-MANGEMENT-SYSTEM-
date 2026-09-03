using System.Drawing;
using System.Windows.Forms;

namespace MEDICARE_HOSPITAL_MANGAMENT.Forms
{
    partial class DepartmentForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblHeaderSubtitle = new Label();
            lblHeaderTitle = new Label();
            pnlSidebar = new Panel();
            grpDept = new GroupBox();
            btnClear = new Button();
            btnUpdate = new Button();
            btnSave = new Button();
            chkIsActive = new CheckBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtDepartmentName = new TextBox();
            lblDepartmentName = new Label();
            pnlGrid = new Panel();
            dgvDepartments = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            grpDept.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDepartments).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(27, 54, 93);
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 15, 20, 15);
            pnlHeader.Size = new Size(900, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            lblHeaderSubtitle.AutoSize = true;
            lblHeaderSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(200, 220, 240);
            lblHeaderSubtitle.Location = new Point(22, 45);
            lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            lblHeaderSubtitle.Size = new Size(346, 21);
            lblHeaderSubtitle.TabIndex = 1;
            lblHeaderSubtitle.Text = "Maintain hospital clinical and administrative wings";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 12);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(313, 35);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Department Management";
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(248, 249, 250);
            pnlSidebar.Controls.Add(grpDept);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 80);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Padding = new Padding(15);
            pnlSidebar.Size = new Size(360, 470);
            pnlSidebar.TabIndex = 1;
            // 
            // grpDept
            // 
            grpDept.Controls.Add(btnClear);
            grpDept.Controls.Add(btnUpdate);
            grpDept.Controls.Add(btnSave);
            grpDept.Controls.Add(chkIsActive);
            grpDept.Controls.Add(txtDescription);
            grpDept.Controls.Add(lblDescription);
            grpDept.Controls.Add(txtDepartmentName);
            grpDept.Controls.Add(lblDepartmentName);
            grpDept.Dock = DockStyle.Fill;
            grpDept.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpDept.ForeColor = Color.FromArgb(27, 54, 93);
            grpDept.Location = new Point(15, 15);
            grpDept.Name = "grpDept";
            grpDept.Padding = new Padding(15);
            grpDept.Size = new Size(330, 440);
            grpDept.TabIndex = 0;
            grpDept.TabStop = false;
            grpDept.Text = "Department Info";
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(230, 230, 230);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F);
            btnClear.ForeColor = Color.FromArgb(40, 40, 40);
            btnClear.Location = new Point(15, 335);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(295, 36);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear Fields";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(40, 167, 69);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(165, 290);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(145, 38);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(27, 54, 93);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(15, 290);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(145, 38);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 9.5F);
            chkIsActive.ForeColor = Color.FromArgb(40, 40, 40);
            chkIsActive.Location = new Point(18, 245);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(160, 25);
            chkIsActive.TabIndex = 4;
            chkIsActive.Text = "Department Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Font = new Font("Segoe UI", 9.5F);
            txtDescription.Location = new Point(15, 140);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(295, 90);
            txtDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.ForeColor = Color.FromArgb(40, 40, 40);
            lblDescription.Location = new Point(12, 115);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(85, 20);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description";
            // 
            // txtDepartmentName
            // 
            txtDepartmentName.BorderStyle = BorderStyle.FixedSingle;
            txtDepartmentName.Font = new Font("Segoe UI", 9.5F);
            txtDepartmentName.Location = new Point(15, 70);
            txtDepartmentName.Name = "txtDepartmentName";
            txtDepartmentName.Size = new Size(295, 29);
            txtDepartmentName.TabIndex = 1;
            // 
            // lblDepartmentName
            // 
            lblDepartmentName.AutoSize = true;
            lblDepartmentName.Font = new Font("Segoe UI", 9F);
            lblDepartmentName.ForeColor = Color.FromArgb(40, 40, 40);
            lblDepartmentName.Location = new Point(12, 45);
            lblDepartmentName.Name = "lblDepartmentName";
            lblDepartmentName.Size = new Size(133, 20);
            lblDepartmentName.TabIndex = 0;
            lblDepartmentName.Text = "Department Name";
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(dgvDepartments);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(360, 80);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(15);
            pnlGrid.Size = new Size(540, 470);
            pnlGrid.TabIndex = 2;
            // 
            // dgvDepartments
            // 
            dgvDepartments.AllowUserToAddRows = false;
            dgvDepartments.AllowUserToDeleteRows = false;
            dgvDepartments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDepartments.BackgroundColor = Color.White;
            dgvDepartments.BorderStyle = BorderStyle.Fixed3D;
            dgvDepartments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDepartments.Dock = DockStyle.Fill;
            dgvDepartments.Location = new Point(15, 15);
            dgvDepartments.MultiSelect = false;
            dgvDepartments.Name = "dgvDepartments";
            dgvDepartments.ReadOnly = true;
            dgvDepartments.RowHeadersWidth = 30;
            dgvDepartments.RowTemplate.Height = 28;
            dgvDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartments.Size = new Size(510, 440);
            dgvDepartments.TabIndex = 0;
            dgvDepartments.SelectionChanged += dgvDepartments_SelectionChanged;
            // 
            // DepartmentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 550);
            Controls.Add(pnlGrid);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(800, 500);
            Name = "DepartmentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediCare - Departments";
            Load += DepartmentForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            grpDept.ResumeLayout(false);
            grpDept.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDepartments).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Panel pnlSidebar;
        private GroupBox grpDept;
        private Label lblDepartmentName;
        private TextBox txtDepartmentName;
        private Label lblDescription;
        private TextBox txtDescription;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnClear;
        private Panel pnlGrid;
        private DataGridView dgvDepartments;
    }
}
