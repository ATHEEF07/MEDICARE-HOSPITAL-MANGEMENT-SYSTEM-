namespace MEDICARE_HOSPITAL_MANGAMENT
{
    partial class LOGIN
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 10, 0, 10);
            label1.Size = new Size(322, 61);
            label1.TabIndex = 0;
            label1.Text = "MEDICARE HOSPITAL";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 82);
            label2.Name = "label2";
            label2.Size = new Size(97, 20);
            label2.TabIndex = 1;
            label2.Text = "USER NAME :";
            label2.Click += label2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(122, 75);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(187, 27);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 129);
            label3.Name = "label3";
            label3.Size = new Size(90, 20);
            label3.TabIndex = 3;
            label3.Text = "PASSWORD:";
            label3.Click += label3_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(122, 122);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(187, 27);
            textBox2.TabIndex = 4;
            // 
            // LOGIN
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(482, 453);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LOGIN";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MEDICARE HOSPITAL  MANGEMENT SYSTEM  LOGIN ";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
    }
}
