namespace scat
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutScatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbnOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbnExport = new System.Windows.Forms.ToolStripButton();
            this.tsbnScan = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewResults = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInNotepadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyCriticalVulnerabilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideThisVulnerabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.wbOutput = new System.Windows.Forms.WebBrowser();
            this.contextMenuBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgExport = new System.Windows.Forms.SaveFileDialog();
            this.dlgCustomRules = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scanToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(655, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSourceFolderToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSourceFolderToolStripMenuItem
            // 
            this.openSourceFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openSourceFolderToolStripMenuItem.Image")));
            this.openSourceFolderToolStripMenuItem.Name = "openSourceFolderToolStripMenuItem";
            this.openSourceFolderToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openSourceFolderToolStripMenuItem.Text = "Open Source Folder...";
            this.openSourceFolderToolStripMenuItem.Click += new System.EventHandler(this.openSourceFolderToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripMenuItem.Image")));
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startAnalysisToolStripMenuItem,
            this.customRulesToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.scanToolStripMenuItem.Text = "Analysis";
            // 
            // startAnalysisToolStripMenuItem
            // 
            this.startAnalysisToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startAnalysisToolStripMenuItem.Image")));
            this.startAnalysisToolStripMenuItem.Name = "startAnalysisToolStripMenuItem";
            this.startAnalysisToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.startAnalysisToolStripMenuItem.Text = "Start Analysis";
            this.startAnalysisToolStripMenuItem.Click += new System.EventHandler(this.startAnalysisToolStripMenuItem_Click);
            // 
            // customRulesToolStripMenuItem
            // 
            this.customRulesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("customRulesToolStripMenuItem.Image")));
            this.customRulesToolStripMenuItem.Name = "customRulesToolStripMenuItem";
            this.customRulesToolStripMenuItem.ShowShortcutKeys = false;
            this.customRulesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.customRulesToolStripMenuItem.Text = "Custom Rules...";
            this.customRulesToolStripMenuItem.Click += new System.EventHandler(this.customRulesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutScatToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutScatToolStripMenuItem
            // 
            this.aboutScatToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutScatToolStripMenuItem.Image")));
            this.aboutScatToolStripMenuItem.Name = "aboutScatToolStripMenuItem";
            this.aboutScatToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aboutScatToolStripMenuItem.Text = "About Scat...";
            this.aboutScatToolStripMenuItem.Click += new System.EventHandler(this.aboutScatToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbnOpen,
            this.tsbnExport,
            this.tsbnScan});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(655, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbnOpen
            // 
            this.tsbnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbnOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbnOpen.Image")));
            this.tsbnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbnOpen.Name = "tsbnOpen";
            this.tsbnOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbnOpen.Text = "Select Code Folder...";
            this.tsbnOpen.Click += new System.EventHandler(this.tsbnOpen_Click);
            // 
            // tsbnExport
            // 
            this.tsbnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbnExport.Image")));
            this.tsbnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbnExport.Name = "tsbnExport";
            this.tsbnExport.Size = new System.Drawing.Size(23, 22);
            this.tsbnExport.Text = "Save Results...";
            this.tsbnExport.Click += new System.EventHandler(this.tsbnExport_Click);
            // 
            // tsbnScan
            // 
            this.tsbnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbnScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbnScan.Image")));
            this.tsbnScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbnScan.Name = "tsbnScan";
            this.tsbnScan.Size = new System.Drawing.Size(23, 22);
            this.tsbnScan.Text = "Run Analyzer";
            this.tsbnScan.Click += new System.EventHandler(this.tsbnScan_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 396);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(655, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.Text = "Ready...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewResults);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(655, 347);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeViewResults
            // 
            this.treeViewResults.ContextMenuStrip = this.contextMenuStrip1;
            this.treeViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewResults.ImageIndex = 0;
            this.treeViewResults.ImageList = this.imageListTreeView;
            this.treeViewResults.Location = new System.Drawing.Point(0, 0);
            this.treeViewResults.Name = "treeViewResults";
            this.treeViewResults.SelectedImageIndex = 0;
            this.treeViewResults.Size = new System.Drawing.Size(214, 343);
            this.treeViewResults.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.openInNotepadToolStripMenuItem,
            this.showOnlyCriticalVulnerabilitiesToolStripMenuItem,
            this.hideThisVulnerabilityToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(247, 92);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.editToolStripMenuItem.Text = "Open...";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // openInNotepadToolStripMenuItem
            // 
            this.openInNotepadToolStripMenuItem.Name = "openInNotepadToolStripMenuItem";
            this.openInNotepadToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.openInNotepadToolStripMenuItem.Text = "Open in Notepad";
            this.openInNotepadToolStripMenuItem.Click += new System.EventHandler(this.openInNotepadToolStripMenuItem_Click);
            // 
            // showOnlyCriticalVulnerabilitiesToolStripMenuItem
            // 
            this.showOnlyCriticalVulnerabilitiesToolStripMenuItem.Name = "showOnlyCriticalVulnerabilitiesToolStripMenuItem";
            this.showOnlyCriticalVulnerabilitiesToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.showOnlyCriticalVulnerabilitiesToolStripMenuItem.Text = "Show only Critical Vulnerabilities";
            this.showOnlyCriticalVulnerabilitiesToolStripMenuItem.Click += new System.EventHandler(this.showOnlyCriticalVulnerabilitiesToolStripMenuItem_Click);
            // 
            // hideThisVulnerabilityToolStripMenuItem
            // 
            this.hideThisVulnerabilityToolStripMenuItem.Name = "hideThisVulnerabilityToolStripMenuItem";
            this.hideThisVulnerabilityToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.hideThisVulnerabilityToolStripMenuItem.Text = "Hide this vulnerability";
            this.hideThisVulnerabilityToolStripMenuItem.Click += new System.EventHandler(this.hideThisVulnerabilityToolStripMenuItem_Click);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "critical.png");
            this.imageListTreeView.Images.SetKeyName(1, "high.png");
            this.imageListTreeView.Images.SetKeyName(2, "medium.png");
            this.imageListTreeView.Images.SetKeyName(3, "low.png");
            this.imageListTreeView.Images.SetKeyName(4, "informational.jpg");
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.wbOutput);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rtbDebug);
            this.splitContainer2.Size = new System.Drawing.Size(433, 347);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 0;
            // 
            // wbOutput
            // 
            this.wbOutput.ContextMenuStrip = this.contextMenuBrowser;
            this.wbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbOutput.IsWebBrowserContextMenuEnabled = false;
            this.wbOutput.Location = new System.Drawing.Point(0, 0);
            this.wbOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbOutput.Name = "wbOutput";
            this.wbOutput.Size = new System.Drawing.Size(429, 196);
            this.wbOutput.TabIndex = 0;
            // 
            // contextMenuBrowser
            // 
            this.contextMenuBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem});
            this.contextMenuBrowser.Name = "contextMenuBrowser";
            this.contextMenuBrowser.Size = new System.Drawing.Size(132, 26);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.openFileToolStripMenuItem.Text = "Open file...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // rtbDebug
            // 
            this.rtbDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebug.Location = new System.Drawing.Point(0, 0);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.Size = new System.Drawing.Size(429, 139);
            this.rtbDebug.TabIndex = 0;
            this.rtbDebug.Text = "";
            // 
            // dlgFolderBrowser
            // 
            this.dlgFolderBrowser.SelectedPath = "C:\\me\\Code\\SuperSecureBank-master";
            // 
            // dlgExport
            // 
            this.dlgExport.FileName = "Results";
            this.dlgExport.Filter = "HTML files|*.html|CSV  files|*.csv|XML files|*.xml";
            // 
            // dlgCustomRules
            // 
            this.dlgCustomRules.FileName = "CustomRules.csv";
            this.dlgCustomRules.Filter = "CSV|*.csv";
            this.dlgCustomRules.InitialDirectory = "C:\\me\\StaticCodeAnalysis\\scat\\CustomRules";
            this.dlgCustomRules.Title = "Select Custom Rules File";
            // 
            // progressBar
            // 
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Value = 20;
            this.progressBar.Visible = false;
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.configurationToolStripMenuItem.Text = "Configuration...";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 418);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Static Code Analysis Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutScatToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbnOpen;
        private System.Windows.Forms.ToolStripButton tsbnScan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewResults;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.WebBrowser wbOutput;
        private System.Windows.Forms.RichTextBox rtbDebug;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbnExport;
        private System.Windows.Forms.SaveFileDialog dlgExport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInNotepadToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuBrowser;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlyCriticalVulnerabilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideThisVulnerabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customRulesToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgCustomRules;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
    }
}

