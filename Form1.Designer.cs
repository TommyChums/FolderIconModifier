namespace FolderIconChanger
{
    partial class IconModification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconModification));
            this.folderSelector = new System.Windows.Forms.Button();
            this.iconSelector = new System.Windows.Forms.Button();
            this.iconDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.folderDisplay = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.changeIcon = new System.Windows.Forms.Button();
            this.clearAll = new System.Windows.Forms.Button();
            this.clearLast = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.downloadStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // folderSelector
            // 
            this.folderSelector.Location = new System.Drawing.Point(712, 76);
            this.folderSelector.Name = "folderSelector";
            this.folderSelector.Size = new System.Drawing.Size(75, 23);
            this.folderSelector.TabIndex = 2;
            this.folderSelector.Text = "Folders";
            this.folderSelector.UseVisualStyleBackColor = true;
            this.folderSelector.Click += new System.EventHandler(this.addFolderPath);
            // 
            // iconSelector
            // 
            this.iconSelector.Location = new System.Drawing.Point(12, 76);
            this.iconSelector.Name = "iconSelector";
            this.iconSelector.Size = new System.Drawing.Size(75, 23);
            this.iconSelector.TabIndex = 3;
            this.iconSelector.Text = "Icons";
            this.iconSelector.UseVisualStyleBackColor = true;
            this.iconSelector.Click += new System.EventHandler(this.addIconPath);
            // 
            // iconDisplay
            // 
            this.iconDisplay.AutoScroll = true;
            this.iconDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iconDisplay.Location = new System.Drawing.Point(12, 106);
            this.iconDisplay.Name = "iconDisplay";
            this.iconDisplay.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.iconDisplay.Size = new System.Drawing.Size(333, 332);
            this.iconDisplay.TabIndex = 4;
            // 
            // folderDisplay
            // 
            this.folderDisplay.Location = new System.Drawing.Point(448, 106);
            this.folderDisplay.Multiline = true;
            this.folderDisplay.Name = "folderDisplay";
            this.folderDisplay.ReadOnly = true;
            this.folderDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.folderDisplay.Size = new System.Drawing.Size(339, 332);
            this.folderDisplay.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoEllipsis = true;
            this.checkBox1.Location = new System.Drawing.Point(588, 78);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(118, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Include Subfolders";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.includeSubfolders);
            // 
            // changeIcon
            // 
            this.changeIcon.Location = new System.Drawing.Point(359, 104);
            this.changeIcon.Name = "changeIcon";
            this.changeIcon.Size = new System.Drawing.Size(75, 23);
            this.changeIcon.TabIndex = 7;
            this.changeIcon.Text = "Begin";
            this.changeIcon.UseVisualStyleBackColor = true;
            this.changeIcon.Click += new System.EventHandler(this.beginChange);
            // 
            // clearAll
            // 
            this.clearAll.Location = new System.Drawing.Point(448, 78);
            this.clearAll.Name = "clearAll";
            this.clearAll.Size = new System.Drawing.Size(54, 20);
            this.clearAll.TabIndex = 8;
            this.clearAll.Text = "Clear All";
            this.clearAll.UseVisualStyleBackColor = true;
            this.clearAll.Click += new System.EventHandler(this.clearAllFolders);
            // 
            // clearLast
            // 
            this.clearLast.Location = new System.Drawing.Point(508, 78);
            this.clearLast.Name = "clearLast";
            this.clearLast.Size = new System.Drawing.Size(63, 20);
            this.clearLast.TabIndex = 9;
            this.clearLast.Text = "Clear Last";
            this.clearLast.UseVisualStyleBackColor = true;
            this.clearLast.Click += new System.EventHandler(this.clearLastFolder);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 10;
            this.button1.Text = "Restore Defaults";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.restoreDefaults);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(655, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select each folder you wish to update the icon for. Check the <Include Subfolders" +
    "> option to add all subfolders in the folder you\'re adding.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(593, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select the folder containing your .ico files. Select the icon you wish to use the" +
    "n press Begin to modify the icons of the folders.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(445, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "If you wish to restore the default windows icon for a folder. Press the Restore D" +
    "efaults button.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // downloadStatus
            // 
            this.downloadStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.downloadStatus.Location = new System.Drawing.Point(12, 454);
            this.downloadStatus.Name = "downloadStatus";
            this.downloadStatus.ReadOnly = true;
            this.downloadStatus.Size = new System.Drawing.Size(775, 13);
            this.downloadStatus.TabIndex = 14;
            // 
            // IconModification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 478);
            this.Controls.Add(this.downloadStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.clearLast);
            this.Controls.Add(this.clearAll);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.changeIcon);
            this.Controls.Add(this.folderDisplay);
            this.Controls.Add(this.iconDisplay);
            this.Controls.Add(this.iconSelector);
            this.Controls.Add(this.folderSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(816, 517);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "IconModification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Allows for the modification of folder icons";
            this.Text = "Icon Modifier";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button folderSelector;
        private System.Windows.Forms.Button iconSelector;
        private System.Windows.Forms.FlowLayoutPanel iconDisplay;
        private System.Windows.Forms.TextBox folderDisplay;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button changeIcon;
        private System.Windows.Forms.Button clearAll;
        private System.Windows.Forms.Button clearLast;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox downloadStatus;
    }
}

