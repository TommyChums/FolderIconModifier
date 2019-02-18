using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
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
        long sizeOfUpdate = 0;

        private void UpdateApplication()
        {
            if (ApplicationDeployment.IsNetworkDeployed) {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                ad.CheckForUpdateCompleted += new CheckForUpdateCompletedEventHandler(ad_CheckForUpdateCompleted);
                ad.CheckForUpdateProgressChanged += new DeploymentProgressChangedEventHandler(ad_CheckForUpdateProgressChanged);

                ad.CheckForUpdateAsync();
            }
        }

        void ad_CheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            downloadStatus.Text = String.Format("Downloading: {0}. {1:D}K of {2:D}K downloaded.", GetProgressString(e.State), e.BytesCompleted / 1024, e.BytesTotal / 1024);
        }

        string GetProgressString(DeploymentProgressState state)
        {
            if (state == DeploymentProgressState.DownloadingApplicationFiles)
            {
                return "application files";
            }
            else if (state == DeploymentProgressState.DownloadingApplicationInformation)
            {
                return "application manifest";
            }
            else
            {
                return "deployment manifest";
            }
        }

        void ad_CheckForUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("ERROR: Could not retrieve new version of the application. Reason: \n" + e.Error.Message + "\nPlease report this error to the system administrator.");
                return;
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show("The update was cancelled.");
            }

            // Ask the user if they would like to update the application now.
            if (e.UpdateAvailable)
            {
                sizeOfUpdate = e.UpdateSizeBytes;

                if (!e.IsUpdateRequired)
                {
                    DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?\n\nEstimated Download Time: ", "Update Available", MessageBoxButtons.OKCancel);
                    if (DialogResult.OK == dr)
                    {
                        BeginUpdate();
                    }
                }
                else
                {
                    MessageBox.Show("A mandatory update is available for your application. We will install the update now, after which we will save all of your in-progress data and restart your application.");
                    BeginUpdate();
                }
            }
            else {
	            MessageBox.Show("No Update Available");
            }
        }

        private void BeginUpdate()
        {
            ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
            ad.UpdateCompleted += new AsyncCompletedEventHandler(ad_UpdateCompleted);

            // Indicate progress in the application's status bar.
            ad.UpdateProgressChanged += new DeploymentProgressChangedEventHandler(ad_UpdateProgressChanged);
            ad.UpdateAsync();
        }

        void ad_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            String progressText = String.Format("{0:D}K out of {1:D}K downloaded - {2:D}% complete", e.BytesCompleted / 1024, e.BytesTotal / 1024, e.ProgressPercentage);
            downloadStatus.Text = progressText;
        }

        void ad_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("The update of the application's latest version was cancelled.");
                return;
            }
            else if (e.Error != null)
            {
                MessageBox.Show("ERROR: Could not install the latest version of the application. Reason: \n" + e.Error.Message + "\nPlease report this error to the system administrator.");
                return;
            }

            DialogResult dr = MessageBox.Show("The application has been updated. Restart? (If you do not restart now, the new version will not take effect until after you quit and launch the application again.)", "Restart Application", MessageBoxButtons.OKCancel);
            if (DialogResult.OK == dr)
            {
                Application.Restart();
            }
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
		                    Size = new Size(70, 65),
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

        private void checkForUpdate(object sender, LinkLabelLinkClickedEventArgs e) {
	        var link = (LinkLabel) sender;
			UpdateApplication();
        }
    }
}
