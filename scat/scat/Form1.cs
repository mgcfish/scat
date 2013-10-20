using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using mshtml;

namespace scat
{
    public partial class Form1 : Form
    {
        private StaticCodeAnalyzer staticCodeAnalyzer;
        private Thread staticCodeAnalyzerThread;
        private Form myself;

        public Form1()
        {
            InitializeComponent();
            this.treeViewResults.AfterSelect += new TreeViewEventHandler(treeViewResults_AfterSelect);
            // DEBUG
            //Configuration.SourceFolder = @"C:\me\src\Final Air ticket";
            Configuration.SourceFolder = @"C:\me\StaticCodeAnalysis\Oops";
            //Configuration.SourceFolder = @"C:\me\Code\SuperSecureBank-master";

            Configuration.debug = this.Debug;
            this.myself = (Form)this;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void OpenSourceFolder()
        {
            if (dlgFolderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Configuration.SourceFolder = dlgFolderBrowser.SelectedPath;
            }
        }

        private void tsbnOpen_Click(object sender, EventArgs e)
        {
            this.OpenSourceFolder();
        }

        private void openSourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenSourceFolder();
        }

        private void Analyze()
        {
            if (string.IsNullOrEmpty(Configuration.SourceFolder))
            {
                MessageBox.Show("No source folder selected.");
            }
            else if (!Directory.Exists(Configuration.SourceFolder))
            {
                MessageBox.Show("Directory does not exist.");
            }
            else
            {
                //
                // Execute the scan.
                //

                this.lblStatus.Text = "Scanning...";
                this.staticCodeAnalyzer = new StaticCodeAnalyzer(this.Completed);
                this.staticCodeAnalyzerThread = new Thread(this.staticCodeAnalyzer.OnThread);
                this.staticCodeAnalyzerThread.Start();
                this.myself.Enabled = false;
                progressBar.Visible = true;
                Application.UseWaitCursor = true;
            }
        }

        private void startAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Analyze();
        }

        private void tsbnScan_Click(object sender, EventArgs e)
        {
            this.Analyze();
        }

        private void aboutScatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutDialog().ShowDialog();
        }

        private void Debug(string message)
        {
            this.FindForm().Invoke(new Action(() =>
            {
                rtbDebug.Text += message + "\n";
            }));
        }

        private void treeViewResults_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // MessageBox.Show( this.treeViewResults.SelectedNode

            if (this.treeViewResults.SelectedNode.Tag != null)
            {
                Guid vulnId = (Guid)this.treeViewResults.SelectedNode.Tag;

                foreach (var v in this.staticCodeAnalyzer.Vulnerabilities)
                {
                    if (v.GetId() == vulnId)
                    {
                        wbOutput.DocumentText = v.ToHtml();
                    }
                }
            }

        }

        private void Completed(StaticCodeAnalyzer analyzer)
        {
            this.FindForm().Invoke(new Action(() =>
            {
                

                foreach (var v in analyzer.Vulnerabilities)
                {
                    Color color = Util.GetColorForSeverity(v.GetSeverity());
                    string colorName = color.Name;
                    string vulnType = v.GetType().ToString();
                    string severityName = v.GetSeverity().ToString();

                    if (!treeViewResults.Nodes.ContainsKey(severityName))
                    {
                        TreeNode tnSeverityNode = new TreeNode(severityName);
                        tnSeverityNode.ImageIndex = Util.GetImageIndexForSeverity(v.GetSeverity());
                        tnSeverityNode.SelectedImageIndex = Util.GetImageIndexForSeverity(v.GetSeverity());
                        tnSeverityNode.Name = severityName;
                        tnSeverityNode.BackColor = color;
                        treeViewResults.Nodes.Add(tnSeverityNode);
                    }

                    if (!treeViewResults.Nodes[severityName].Nodes.ContainsKey(vulnType))
                    {
                        TreeNode tnTypeNode = new TreeNode(vulnType);
                        tnTypeNode.ImageIndex = treeViewResults.Nodes[severityName].ImageIndex;
                        tnTypeNode.SelectedImageIndex = treeViewResults.Nodes[severityName].SelectedImageIndex;
                        tnTypeNode.Name = vulnType;
                        tnTypeNode.BackColor = color;
                        treeViewResults.Nodes[severityName].Nodes.Add(tnTypeNode);
                    }

                    //
                    // add the actual vuln.
                    //
                    TreeNode vulnNode = new TreeNode(v.GetFilename());
                    vulnNode.BackColor = color;
                    vulnNode.Tag = v.GetId();
                    vulnNode.ImageIndex = treeViewResults.Nodes[severityName].ImageIndex;
                    vulnNode.SelectedImageIndex = treeViewResults.Nodes[severityName].SelectedImageIndex;
                    treeViewResults.Nodes[severityName].Nodes[vulnType].Nodes.Add(vulnNode);

                }

             //   System.Diagnostics.Debugger.Break();
                progressBar.Visible = false;
                this.myself.Enabled = true;
                Application.UseWaitCursor = false;
                this.lblStatus.Text = "Finished.";
            }));
        }

        private void SaveVulnerabilities()
        {

            if (DialogResult.OK == dlgExport.ShowDialog())
            {
                if (File.Exists(dlgExport.FileName))
                {
                    File.Delete(dlgExport.FileName);
                }

                if (dlgExport.FileName.ToLower().EndsWith(".html"))
                {
                    // MessageBox.Show(dlgExportVulnerabilities.FileName);
                    string output = "<html>";
                    output += "<title>Scat Vulnerability Report</title>";
                    output += "<head>";
                    output += "<center><h1>Vulnerability Report</h1></center>";
                    output += string.Format("<center><h3>Scan Date: {0}</h3></center>", DateTime.Now);
                    output += "</head>";
                    output += "<body>";

                    foreach (var v in this.staticCodeAnalyzer.Vulnerabilities)
                    {
                        output += v.ToHtml() + "<br/><br/><hr/><br/><br/>";
                    }

                    output += "</body></html>";



                    File.WriteAllText(dlgExport.FileName, output);
                }
                else if (dlgExport.FileName.ToLower().EndsWith(".xml"))
                {
                    string output = "<vulnerabilities>";

                    foreach (var v in this.staticCodeAnalyzer.Vulnerabilities)
                    {
                        output += v.ToXml();
                    }

                    output += "</vulnerabilities>";

                    File.WriteAllText(dlgExport.FileName, output);
                }
                else if (dlgExport.FileName.ToLower().EndsWith(".csv"))
                {
                    string output = string.Empty;

                    foreach (var v in this.staticCodeAnalyzer.Vulnerabilities)
                    {
                        output += v.ToCsv() + "\n"; ;
                    }

                    File.WriteAllText(dlgExport.FileName, output);
                }

            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveVulnerabilities();
        }

        private void tsbnExport_Click(object sender, EventArgs e)
        {
            this.SaveVulnerabilities();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(GetFileNameForSelectedNode());
            }
            catch (Exception)
            {
            }
        }

        private string GetFileNameForSelectedNode()
        {
            string retval = string.Empty;
            if (this.treeViewResults.SelectedNode.Tag != null)
            {
                Guid vulnId = (Guid)this.treeViewResults.SelectedNode.Tag;

                foreach (var v in this.staticCodeAnalyzer.Vulnerabilities)
                {
                    if (v.GetId() == vulnId)
                    {
                        retval = v.GetFilename();
                    }
                }
            }

            return retval;
        }

        private void openInNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = GetFileNameForSelectedNode();
            if (File.Exists(filename))
            {
                System.Diagnostics.Process.Start("notepad.exe", filename);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // needs a reference: Microsoft.mshtml.dll
            IHTMLDocument2 htmlDocument = wbOutput.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

                if (range != null)
                {
                    string filename = range.text;
                    if (File.Exists(filename))
                    {
                        System.Diagnostics.Process.Start(filename);
                    }
                    // if(File.Exists(
                    // MessageBox.Show(range.text);
                }
            }
        }

        private void ShowOnlyCriticalVulnerabilities()
        {
            foreach (var v in staticCodeAnalyzer.Vulnerabilities)
            {
                if (v.GetSeverity() == Severity.OhShit)
                {
                    Color color = Util.GetColorForSeverity(v.GetSeverity());
                    string colorName = color.Name;
                    string vulnType = v.GetType().ToString();
                    string severityName = v.GetSeverity().ToString();

                    if (!treeViewResults.Nodes.ContainsKey(severityName))
                    {
                        TreeNode tnSeverityNode = new TreeNode(severityName);
                        tnSeverityNode.ImageIndex = Util.GetImageIndexForSeverity(v.GetSeverity());
                        tnSeverityNode.SelectedImageIndex = Util.GetImageIndexForSeverity(v.GetSeverity());
                        tnSeverityNode.Name = severityName;
                        tnSeverityNode.BackColor = color;
                        treeViewResults.Nodes.Add(tnSeverityNode);
                    }

                    if (!treeViewResults.Nodes[severityName].Nodes.ContainsKey(vulnType))
                    {
                        TreeNode tnTypeNode = new TreeNode(vulnType);
                        tnTypeNode.ImageIndex = treeViewResults.Nodes[severityName].ImageIndex;
                        tnTypeNode.SelectedImageIndex = treeViewResults.Nodes[severityName].SelectedImageIndex;
                        tnTypeNode.Name = vulnType;
                        tnTypeNode.BackColor = color;
                        treeViewResults.Nodes[severityName].Nodes.Add(tnTypeNode);
                    }

                    //
                    // add the actual vuln.
                    //
                    TreeNode vulnNode = new TreeNode(v.GetFilename());
                    vulnNode.BackColor = color;
                    vulnNode.Tag = v.GetId();
                    vulnNode.ImageIndex = treeViewResults.Nodes[severityName].ImageIndex;
                    vulnNode.SelectedImageIndex = treeViewResults.Nodes[severityName].SelectedImageIndex;
                    treeViewResults.Nodes[severityName].Nodes[vulnType].Nodes.Add(vulnNode);
                }
            }
        }

        private void showOnlyCriticalVulnerabilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewResults.Nodes.Clear();
            ShowOnlyCriticalVulnerabilities();
        }

        List<Guid> hiddenVulnerabilities = new List<Guid>();

        private void CreateTree()
        {
            treeViewResults.Nodes.Clear();

            foreach (var v in staticCodeAnalyzer.Vulnerabilities)
            {
                if (hiddenVulnerabilities.Contains(v.GetId()))
                {
                    continue;
                }

                if (v.GetSeverity() == Severity.OhShit)
                {
                    Color color = Util.GetColorForSeverity(v.GetSeverity());
                    string colorName = color.Name;
                    string vulnType = v.GetType().ToString();
                    string severityName = v.GetSeverity().ToString();

                    if (!treeViewResults.Nodes.ContainsKey(severityName))
                    {
                        TreeNode tnSeverityNode = new TreeNode(severityName);
                        tnSeverityNode.ImageIndex = Util.GetImageIndexForSeverity(v.GetSeverity());
                        tnSeverityNode.SelectedImageIndex = Util.GetImageIndexForSeverity(v.GetSeverity());
                        tnSeverityNode.Name = severityName;
                        tnSeverityNode.BackColor = color;
                        treeViewResults.Nodes.Add(tnSeverityNode);
                    }

                    if (!treeViewResults.Nodes[severityName].Nodes.ContainsKey(vulnType))
                    {
                        TreeNode tnTypeNode = new TreeNode(vulnType);
                        tnTypeNode.ImageIndex = treeViewResults.Nodes[severityName].ImageIndex;
                        tnTypeNode.SelectedImageIndex = treeViewResults.Nodes[severityName].SelectedImageIndex;
                        tnTypeNode.Name = vulnType;
                        tnTypeNode.BackColor = color;
                        treeViewResults.Nodes[severityName].Nodes.Add(tnTypeNode);
                    }

                    //
                    // add the actual vuln.
                    //
                    TreeNode vulnNode = new TreeNode(v.GetFilename());
                    vulnNode.BackColor = color;
                    vulnNode.Tag = v.GetId();
                    vulnNode.ImageIndex = treeViewResults.Nodes[severityName].ImageIndex;
                    vulnNode.SelectedImageIndex = treeViewResults.Nodes[severityName].SelectedImageIndex;
                    treeViewResults.Nodes[severityName].Nodes[vulnType].Nodes.Add(vulnNode);
                }
            }

        }

        private void hideThisVulnerabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // also implement hide all vulnerabilities for this file.


            if (this.treeViewResults.SelectedNode.Tag != null)
            {
                Guid vulnId = (Guid)this.treeViewResults.SelectedNode.Tag;

                foreach (var v in this.staticCodeAnalyzer.Vulnerabilities)
                {
                    if (v.GetId() == vulnId)
                    {
                        // it's this vulnerability.
                        hiddenVulnerabilities.Add(v.GetId());
                        CreateTree();
                    }
                }
            }

        }

        private void customRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgCustomRules.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Configuration.CustomRulesFilename = dlgCustomRules.FileName;
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationDialog d = new ConfigurationDialog();

            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // we've configured the system.
            }
        }

    }
}
