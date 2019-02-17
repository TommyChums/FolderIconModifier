using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderIconChanger {
    public partial class IconModification : Form {
	    private ArrayList pictureBoxes = new ArrayList();
	    private ArrayList FilePaths = new ArrayList();
	    private bool IncludeSubFolders = false;
	    ToolTip tt = new ToolTip();

        private string selectedIcon;

        public IconModification() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private static void ApplyFolderIcon(string targetFolderPath, string iconFilePath) {
	        var iniPath = Path.Combine(targetFolderPath, "desktop.ini");
	        if (iconFilePath == null) iconFilePath = @"%SystemRoot%\system32\imageres.dll,-112";

            if (File.Exists(iniPath)) {
		        //remove hidden and system attributes to make ini file writable
		        File.SetAttributes(
			        iniPath,
			        File.GetAttributes(iniPath) &
			        ~(FileAttributes.Hidden | FileAttributes.System));
	        }

	        //create new ini file with the required contents
	        var iniContents = new StringBuilder()
		        .AppendLine("[.ShellClassInfo]")
		        .AppendLine($"IconResource={iconFilePath},0")
		        .AppendLine($"IconFile={iconFilePath}")
		        .AppendLine("IconIndex=0")
		        .ToString();
	        File.WriteAllText(iniPath, iniContents);

	        //hide the ini file and set it as system
	        File.SetAttributes(
		        iniPath,
		        File.GetAttributes(iniPath) | FileAttributes.Hidden | FileAttributes.System);
	        //set the folder as system
	        File.SetAttributes(
		        targetFolderPath,
		        File.GetAttributes(targetFolderPath) | FileAttributes.System);
        }

	    private void UpdateFolderDisplay() {
		    if (FilePaths.Count < 1) {
                folderDisplay.Clear();
				return;
		    }

		    var lines = FilePaths.ToArray(typeof(string));

		    folderDisplay.Lines = (string[]) lines;
	    }

        private void addFolderPath(object sender, EventArgs e) {
	        var browserDialog = new FolderBrowserDialog {Description = "Add folder you wish to change icon for", ShowNewFolderButton = true};

	        if (browserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
		        if (FilePaths.Contains(browserDialog.SelectedPath)) {
			        MessageBox.Show("Folder already added");
			        return;
		        }

                if (IncludeSubFolders) {
	                FilePaths.Add(browserDialog.SelectedPath);
                    var directories = Directory.GetDirectories(browserDialog.SelectedPath, "*", SearchOption.AllDirectories);
                    foreach (var d in directories) {
                        FilePaths.Add(d);
                    }
                }
                else FilePaths.Add(browserDialog.SelectedPath);

				UpdateFolderDisplay();
	        }
        }

        private void addIconPath(object sender, EventArgs e) {
	        var browserDialog = new FolderBrowserDialog { Description = "Select Folder Containing Icons to Add", ShowNewFolderButton = true };
	        var x = new Point(2, 115);
	        if (browserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
		        
		        foreach (PictureBox p in pictureBoxes) {
					iconDisplay.Controls.Remove(p);
		        }

		        try {
                    var files = Directory.GetFiles(browserDialog.SelectedPath, "*.ico", SearchOption.TopDirectoryOnly);

                    foreach (var f in files) {

	                    var picBox = new PictureBox {
		                    ImageLocation = f,
		                    Size = new Size(70, 100),
		                    SizeMode = PictureBoxSizeMode.StretchImage

	                    };
						
	                    iconDisplay.Controls.Add(picBox);
	                    picBox.Click += clickPic;
	                    picBox.MouseHover += hoverPic;
	                    picBox.Cursor = Cursors.Hand;
                        pictureBoxes.Add(picBox);
                    }
                }
		        catch (UnauthorizedAccessException exception) {
                    MessageBox.Show("Unable to access " + browserDialog.SelectedPath);
                    
		        }
			}
        }

        private void hoverPic(object sender, EventArgs e) {
	        var picBox = (PictureBox) sender;
			tt.SetToolTip(picBox, Path.GetFileName(picBox.ImageLocation));
        }

        private void clickPic(object sender, EventArgs e) {
	        var icon = (PictureBox) sender;
	        selectedIcon = icon.ImageLocation;
	        MessageBox.Show("Icon Selected: " + Path.GetFileName(selectedIcon));
        }

        private void includeSubfolders(object sender, EventArgs e) {
	        var checkBox = (CheckBox) sender;
	        IncludeSubFolders = checkBox.CheckState == CheckState.Checked;
        }

        private void clearLastFolder(object sender, EventArgs e) {
	        if (FilePaths.Count < 1) return;
            FilePaths.RemoveAt(FilePaths.Count - 1);
			UpdateFolderDisplay();
        }

        private void clearAllFolders(object sender, EventArgs e) {
			FilePaths.Clear();
			UpdateFolderDisplay();
        }

        private void restoreDefaults(object sender, EventArgs e) {
	        var i = 0;
	        if (FilePaths.Count < 1) {
		        MessageBox.Show("No folders to modify");
		        return;
	        }

	        foreach (string s in FilePaths) {
		        try {
			        ApplyFolderIcon(s, null);
		        }
		        catch (Exception ex) {
			        i++;
			        MessageBox.Show(ex.Message);
			        continue;
		        }
	        }

	        MessageBox.Show("Completed with " + i + " errors.");
        }

        private void beginChange(object sender, EventArgs e) {
	        var i = 0;
	        if(FilePaths.Count < 1) {
		        MessageBox.Show("No folders to modify");
		        return;
	        }

	        if (selectedIcon == null) {
		        MessageBox.Show("Please Select an Icon");
		        return;
	        }

	        foreach (string s in FilePaths) {
		        try {
			        ApplyFolderIcon(s, selectedIcon);
		        }
		        catch (Exception ex) {
			        i++;
			        MessageBox.Show(ex.Message);
			        continue;
		        }
	        }

	        MessageBox.Show("Completed with " + i + " errors.");
        }
    }
}
