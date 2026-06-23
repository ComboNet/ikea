namespace LoadExperimentFile1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            cbPortName = new ToolStripComboBox();
            cmdConnect = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            cmdRead = new ToolStripButton();
            dataGridView1 = new DataGridView();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, cbPortName, cmdConnect, toolStripSeparator1, cmdRead });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(944, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(70, 22);
            toolStripLabel1.Text = "Port Name: ";
            // 
            // cbPortName
            // 
            cbPortName.Items.AddRange(new object[] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7" });
            cbPortName.Name = "cbPortName";
            cbPortName.Size = new Size(121, 25);
            cbPortName.Text = "COM1";
            // 
            // cmdConnect
            // 
            cmdConnect.DisplayStyle = ToolStripItemDisplayStyle.Text;
            cmdConnect.Image = (Image)resources.GetObject("cmdConnect.Image");
            cmdConnect.ImageTransparentColor = Color.Magenta;
            cmdConnect.Name = "cmdConnect";
            cmdConnect.Size = new Size(56, 22);
            cmdConnect.Text = "Connect";
            cmdConnect.Click += cmdConnect_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // cmdRead
            // 
            cmdRead.DisplayStyle = ToolStripItemDisplayStyle.Text;
            cmdRead.Image = (Image)resources.GetObject("cmdRead.Image");
            cmdRead.ImageTransparentColor = Color.Magenta;
            cmdRead.Name = "cmdRead";
            cmdRead.Size = new Size(37, 22);
            cmdRead.Text = "Read";
            cmdRead.Click += cmdRead_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 25);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(944, 576);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 601);
            Controls.Add(dataGridView1);
            Controls.Add(toolStrip1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Load Experiment File";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox cbPortName;
        private ToolStripButton cmdConnect;
        private DataGridView dataGridView1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton cmdRead;
    }
}
