namespace _123ClickGUI
{
    partial class GUI
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
            this.components = new System.ComponentModel.Container();
            this.tbName = new System.Windows.Forms.TextBox();
            this.countDownTimer = new System.Windows.Forms.Timer(this.components);
            this.rbLog = new System.Windows.Forms.RichTextBox();
            this.lvClickLocations = new System.Windows.Forms.ListView();
            this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCountDown = new System.Windows.Forms.Button();
            this.lvOnline = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblConnected = new System.Windows.Forms.Label();
            this.btnRewind = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnChangeChannel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(15, 9);
            this.tbName.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(196, 33);
            this.tbName.TabIndex = 5;
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // countDownTimer
            // 
            this.countDownTimer.Interval = 1000;
            this.countDownTimer.Tick += new System.EventHandler(this.countDownTimer_Tick);
            // 
            // rbLog
            // 
            this.rbLog.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLog.ForeColor = System.Drawing.Color.Red;
            this.rbLog.Location = new System.Drawing.Point(15, 526);
            this.rbLog.Name = "rbLog";
            this.rbLog.ReadOnly = true;
            this.rbLog.Size = new System.Drawing.Size(662, 99);
            this.rbLog.TabIndex = 7;
            this.rbLog.Text = "";
            // 
            // lvClickLocations
            // 
            this.lvClickLocations.BackColor = System.Drawing.SystemColors.Window;
            this.lvClickLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTitle,
            this.chX,
            this.chY});
            this.lvClickLocations.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lvClickLocations.FullRowSelect = true;
            this.lvClickLocations.GridLines = true;
            this.lvClickLocations.HideSelection = false;
            this.lvClickLocations.Location = new System.Drawing.Point(217, 9);
            this.lvClickLocations.MultiSelect = false;
            this.lvClickLocations.Name = "lvClickLocations";
            this.lvClickLocations.Size = new System.Drawing.Size(460, 472);
            this.lvClickLocations.TabIndex = 4;
            this.lvClickLocations.UseCompatibleStateImageBehavior = false;
            this.lvClickLocations.View = System.Windows.Forms.View.Details;
            this.lvClickLocations.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // chTitle
            // 
            this.chTitle.Text = "Title";
            this.chTitle.Width = 206;
            // 
            // chX
            // 
            this.chX.Text = "X";
            this.chX.Width = 125;
            // 
            // chY
            // 
            this.chY.Text = "Y";
            this.chY.Width = 125;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(214, 487);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(150, 32);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add new";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(527, 488);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(150, 32);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(371, 488);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(150, 32);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCountDown
            // 
            this.btnCountDown.Enabled = false;
            this.btnCountDown.Location = new System.Drawing.Point(81, 486);
            this.btnCountDown.Name = "btnCountDown";
            this.btnCountDown.Size = new System.Drawing.Size(63, 33);
            this.btnCountDown.TabIndex = 0;
            this.btnCountDown.Tag = "Hotkey: (CapsLock + S)";
            this.btnCountDown.Text = "▶";
            this.btnCountDown.UseVisualStyleBackColor = true;
            this.btnCountDown.Click += new System.EventHandler(this.btnCountDown_Click);
            this.btnCountDown.MouseHover += new System.EventHandler(this.MouseHover_ToolTip);
            // 
            // lvOnline
            // 
            this.lvOnline.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvOnline.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvOnline.GridLines = true;
            this.lvOnline.HideSelection = false;
            this.lvOnline.Location = new System.Drawing.Point(12, 86);
            this.lvOnline.Name = "lvOnline";
            this.lvOnline.Size = new System.Drawing.Size(199, 356);
            this.lvOnline.TabIndex = 6;
            this.lvOnline.UseCompatibleStateImageBehavior = false;
            this.lvOnline.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Online users";
            this.columnHeader1.Width = 195;
            // 
            // lblConnected
            // 
            this.lblConnected.ForeColor = System.Drawing.Color.Red;
            this.lblConnected.Location = new System.Drawing.Point(12, 47);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConnected.Size = new System.Drawing.Size(199, 33);
            this.lblConnected.TabIndex = 9;
            this.lblConnected.Text = "Disconnected";
            this.lblConnected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRewind
            // 
            this.btnRewind.Enabled = false;
            this.btnRewind.Location = new System.Drawing.Point(12, 486);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(63, 33);
            this.btnRewind.TabIndex = 10;
            this.btnRewind.Tag = "Hotkey: (CapsLock + R)";
            this.btnRewind.Text = "«";
            this.btnRewind.UseVisualStyleBackColor = true;
            this.btnRewind.Click += new System.EventHandler(this.btnRewind_Click);
            this.btnRewind.MouseHover += new System.EventHandler(this.MouseHover_ToolTip);
            // 
            // btnForward
            // 
            this.btnForward.Enabled = false;
            this.btnForward.Location = new System.Drawing.Point(148, 486);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(63, 33);
            this.btnForward.TabIndex = 11;
            this.btnForward.Tag = "Hotkey: (CapsLock+ F)";
            this.btnForward.Text = "»";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            this.btnForward.MouseHover += new System.EventHandler(this.MouseHover_ToolTip);
            // 
            // btnChangeChannel
            // 
            this.btnChangeChannel.Location = new System.Drawing.Point(12, 448);
            this.btnChangeChannel.Name = "btnChangeChannel";
            this.btnChangeChannel.Size = new System.Drawing.Size(199, 33);
            this.btnChangeChannel.TabIndex = 12;
            this.btnChangeChannel.Text = "Change channel";
            this.btnChangeChannel.UseVisualStyleBackColor = true;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 633);
            this.Controls.Add(this.btnChangeChannel);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnRewind);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.lvOnline);
            this.Controls.Add(this.btnCountDown);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lvClickLocations);
            this.Controls.Add(this.rbLog);
            this.Controls.Add(this.tbName);
            this.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.Text = "123 Click";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
            this.Shown += new System.EventHandler(this.GUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Timer countDownTimer;
        private System.Windows.Forms.RichTextBox rbLog;
        private System.Windows.Forms.ListView lvClickLocations;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chX;
        private System.Windows.Forms.ColumnHeader chY;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCountDown;
        private System.Windows.Forms.ListView lvOnline;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Button btnRewind;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnChangeChannel;
    }
}

