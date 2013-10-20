namespace scat
{
    partial class ConfigurationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationDialog));
            this.bnOK = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.csharp = new System.Windows.Forms.TabPage();
            this.php = new System.Windows.Forms.TabPage();
            this.cbPhpCommandInjection = new System.Windows.Forms.CheckBox();
            this.cbBasicPhpFileInclusion = new System.Windows.Forms.CheckBox();
            this.cbBasicPhpSqlInjection = new System.Windows.Forms.CheckBox();
            this.cbBasicSqlInjection = new System.Windows.Forms.CheckBox();
            this.cbAdvancedSqlInjection = new System.Windows.Forms.CheckBox();
            this.cbMildSqlInjection = new System.Windows.Forms.CheckBox();
            this.cbBasicLdapInjection = new System.Windows.Forms.CheckBox();
            this.cbBasicCsrf = new System.Windows.Forms.CheckBox();
            this.cbIisConfig = new System.Windows.Forms.CheckBox();
            this.cbSqlConnectionString = new System.Windows.Forms.CheckBox();
            this.cbInlineAspxRule = new System.Windows.Forms.CheckBox();
            this.cbBasicReflectedXss = new System.Windows.Forms.CheckBox();
            this.cbAdvancedReflectedXss = new System.Windows.Forms.CheckBox();
            this.cbBasicOpenRedirect = new System.Windows.Forms.CheckBox();
            this.cbAdvancedOpenRedirect = new System.Windows.Forms.CheckBox();
            this.cbBasicArbitraryFileOperations = new System.Windows.Forms.CheckBox();
            this.cbAdvancedArbitraryFileOperations = new System.Windows.Forms.CheckBox();
            this.cbBasicCommandInjection = new System.Windows.Forms.CheckBox();
            this.cbAdvancedCommandInjection = new System.Windows.Forms.CheckBox();
            this.cbBasicArbitraryFileAccess = new System.Windows.Forms.CheckBox();
            this.cbAdvancedArbitraryFileAccess = new System.Windows.Forms.CheckBox();
            this.cbBasicXPathInjection = new System.Windows.Forms.CheckBox();
            this.cbAdvancedXPathInjection = new System.Windows.Forms.CheckBox();
            this.cbWeakPermissions = new System.Windows.Forms.CheckBox();
            this.cbCookieSecurity = new System.Windows.Forms.CheckBox();
            this.cbHardCodedPassword = new System.Windows.Forms.CheckBox();
            this.cbWeakCryptoRule = new System.Windows.Forms.CheckBox();
            this.cbResourceLeak = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.csharp.SuspendLayout();
            this.php.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnOK
            // 
            this.bnOK.Location = new System.Drawing.Point(174, 419);
            this.bnOK.Name = "bnOK";
            this.bnOK.Size = new System.Drawing.Size(75, 23);
            this.bnOK.TabIndex = 0;
            this.bnOK.Text = "OK";
            this.bnOK.UseVisualStyleBackColor = true;
            this.bnOK.Click += new System.EventHandler(this.bnOK_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Location = new System.Drawing.Point(266, 419);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 23);
            this.bnCancel.TabIndex = 1;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.csharp);
            this.tabControl1.Controls.Add(this.php);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 401);
            this.tabControl1.TabIndex = 2;
            // 
            // csharp
            // 
            this.csharp.Controls.Add(this.cbResourceLeak);
            this.csharp.Controls.Add(this.cbWeakCryptoRule);
            this.csharp.Controls.Add(this.cbHardCodedPassword);
            this.csharp.Controls.Add(this.cbCookieSecurity);
            this.csharp.Controls.Add(this.cbWeakPermissions);
            this.csharp.Controls.Add(this.cbAdvancedXPathInjection);
            this.csharp.Controls.Add(this.cbBasicXPathInjection);
            this.csharp.Controls.Add(this.cbAdvancedArbitraryFileAccess);
            this.csharp.Controls.Add(this.cbBasicArbitraryFileAccess);
            this.csharp.Controls.Add(this.cbAdvancedCommandInjection);
            this.csharp.Controls.Add(this.cbBasicCommandInjection);
            this.csharp.Controls.Add(this.cbAdvancedArbitraryFileOperations);
            this.csharp.Controls.Add(this.cbBasicArbitraryFileOperations);
            this.csharp.Controls.Add(this.cbAdvancedOpenRedirect);
            this.csharp.Controls.Add(this.cbBasicOpenRedirect);
            this.csharp.Controls.Add(this.cbAdvancedReflectedXss);
            this.csharp.Controls.Add(this.cbBasicReflectedXss);
            this.csharp.Controls.Add(this.cbInlineAspxRule);
            this.csharp.Controls.Add(this.cbSqlConnectionString);
            this.csharp.Controls.Add(this.cbIisConfig);
            this.csharp.Controls.Add(this.cbBasicCsrf);
            this.csharp.Controls.Add(this.cbBasicLdapInjection);
            this.csharp.Controls.Add(this.cbMildSqlInjection);
            this.csharp.Controls.Add(this.cbAdvancedSqlInjection);
            this.csharp.Controls.Add(this.cbBasicSqlInjection);
            this.csharp.Location = new System.Drawing.Point(4, 22);
            this.csharp.Name = "csharp";
            this.csharp.Padding = new System.Windows.Forms.Padding(3);
            this.csharp.Size = new System.Drawing.Size(510, 375);
            this.csharp.TabIndex = 0;
            this.csharp.Text = "ASP.NET/C#";
            this.csharp.UseVisualStyleBackColor = true;
            // 
            // php
            // 
            this.php.Controls.Add(this.cbBasicPhpSqlInjection);
            this.php.Controls.Add(this.cbBasicPhpFileInclusion);
            this.php.Controls.Add(this.cbPhpCommandInjection);
            this.php.Location = new System.Drawing.Point(4, 22);
            this.php.Name = "php";
            this.php.Padding = new System.Windows.Forms.Padding(3);
            this.php.Size = new System.Drawing.Size(729, 375);
            this.php.TabIndex = 1;
            this.php.Text = "Php";
            this.php.UseVisualStyleBackColor = true;
            // 
            // cbPhpCommandInjection
            // 
            this.cbPhpCommandInjection.AutoSize = true;
            this.cbPhpCommandInjection.Location = new System.Drawing.Point(19, 24);
            this.cbPhpCommandInjection.Name = "cbPhpCommandInjection";
            this.cbPhpCommandInjection.Size = new System.Drawing.Size(167, 17);
            this.cbPhpCommandInjection.TabIndex = 1;
            this.cbPhpCommandInjection.Text = "Basic Php Command Injection";
            this.cbPhpCommandInjection.UseVisualStyleBackColor = true;
            // 
            // cbBasicPhpFileInclusion
            // 
            this.cbBasicPhpFileInclusion.AutoSize = true;
            this.cbBasicPhpFileInclusion.Location = new System.Drawing.Point(19, 48);
            this.cbBasicPhpFileInclusion.Name = "cbBasicPhpFileInclusion";
            this.cbBasicPhpFileInclusion.Size = new System.Drawing.Size(138, 17);
            this.cbBasicPhpFileInclusion.TabIndex = 2;
            this.cbBasicPhpFileInclusion.Text = "Basic Php File Inclusion";
            this.cbBasicPhpFileInclusion.UseVisualStyleBackColor = true;
            // 
            // cbBasicPhpSqlInjection
            // 
            this.cbBasicPhpSqlInjection.AutoSize = true;
            this.cbBasicPhpSqlInjection.Location = new System.Drawing.Point(19, 82);
            this.cbBasicPhpSqlInjection.Name = "cbBasicPhpSqlInjection";
            this.cbBasicPhpSqlInjection.Size = new System.Drawing.Size(113, 17);
            this.cbBasicPhpSqlInjection.TabIndex = 3;
            this.cbBasicPhpSqlInjection.Text = "Basic Sql Injection";
            this.cbBasicPhpSqlInjection.UseVisualStyleBackColor = true;
            // 
            // cbBasicSqlInjection
            // 
            this.cbBasicSqlInjection.AutoSize = true;
            this.cbBasicSqlInjection.Location = new System.Drawing.Point(9, 19);
            this.cbBasicSqlInjection.Name = "cbBasicSqlInjection";
            this.cbBasicSqlInjection.Size = new System.Drawing.Size(116, 17);
            this.cbBasicSqlInjection.TabIndex = 0;
            this.cbBasicSqlInjection.Text = "Basic Sql  Injection";
            this.cbBasicSqlInjection.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedSqlInjection
            // 
            this.cbAdvancedSqlInjection.AutoSize = true;
            this.cbAdvancedSqlInjection.Location = new System.Drawing.Point(9, 43);
            this.cbAdvancedSqlInjection.Name = "cbAdvancedSqlInjection";
            this.cbAdvancedSqlInjection.Size = new System.Drawing.Size(136, 17);
            this.cbAdvancedSqlInjection.TabIndex = 1;
            this.cbAdvancedSqlInjection.Text = "Advanced Sql Injection";
            this.cbAdvancedSqlInjection.UseVisualStyleBackColor = true;
            // 
            // cbMildSqlInjection
            // 
            this.cbMildSqlInjection.AutoSize = true;
            this.cbMildSqlInjection.Location = new System.Drawing.Point(9, 67);
            this.cbMildSqlInjection.Name = "cbMildSqlInjection";
            this.cbMildSqlInjection.Size = new System.Drawing.Size(106, 17);
            this.cbMildSqlInjection.TabIndex = 2;
            this.cbMildSqlInjection.Text = "Mild Sql Injection";
            this.cbMildSqlInjection.UseVisualStyleBackColor = true;
            // 
            // cbBasicLdapInjection
            // 
            this.cbBasicLdapInjection.AutoSize = true;
            this.cbBasicLdapInjection.Location = new System.Drawing.Point(9, 91);
            this.cbBasicLdapInjection.Name = "cbBasicLdapInjection";
            this.cbBasicLdapInjection.Size = new System.Drawing.Size(122, 17);
            this.cbBasicLdapInjection.TabIndex = 3;
            this.cbBasicLdapInjection.Text = "Basic Ldap Injection";
            this.cbBasicLdapInjection.UseVisualStyleBackColor = true;
            // 
            // cbBasicCsrf
            // 
            this.cbBasicCsrf.AutoSize = true;
            this.cbBasicCsrf.Location = new System.Drawing.Point(9, 115);
            this.cbBasicCsrf.Name = "cbBasicCsrf";
            this.cbBasicCsrf.Size = new System.Drawing.Size(73, 17);
            this.cbBasicCsrf.TabIndex = 4;
            this.cbBasicCsrf.Text = "Basic Csrf";
            this.cbBasicCsrf.UseVisualStyleBackColor = true;
            // 
            // cbIisConfig
            // 
            this.cbIisConfig.AutoSize = true;
            this.cbIisConfig.Location = new System.Drawing.Point(9, 139);
            this.cbIisConfig.Name = "cbIisConfig";
            this.cbIisConfig.Size = new System.Drawing.Size(104, 17);
            this.cbIisConfig.TabIndex = 5;
            this.cbIisConfig.Text = "IIS Configuration";
            this.cbIisConfig.UseVisualStyleBackColor = true;
            // 
            // cbSqlConnectionString
            // 
            this.cbSqlConnectionString.AutoSize = true;
            this.cbSqlConnectionString.Location = new System.Drawing.Point(9, 163);
            this.cbSqlConnectionString.Name = "cbSqlConnectionString";
            this.cbSqlConnectionString.Size = new System.Drawing.Size(128, 17);
            this.cbSqlConnectionString.TabIndex = 6;
            this.cbSqlConnectionString.Text = "Sql Connection String";
            this.cbSqlConnectionString.UseVisualStyleBackColor = true;
            // 
            // cbInlineAspxRule
            // 
            this.cbInlineAspxRule.AutoSize = true;
            this.cbInlineAspxRule.Location = new System.Drawing.Point(9, 187);
            this.cbInlineAspxRule.Name = "cbInlineAspxRule";
            this.cbInlineAspxRule.Size = new System.Drawing.Size(77, 17);
            this.cbInlineAspxRule.TabIndex = 7;
            this.cbInlineAspxRule.Text = "Inline Aspx";
            this.cbInlineAspxRule.UseVisualStyleBackColor = true;
            // 
            // cbBasicReflectedXss
            // 
            this.cbBasicReflectedXss.AutoSize = true;
            this.cbBasicReflectedXss.Location = new System.Drawing.Point(9, 211);
            this.cbBasicReflectedXss.Name = "cbBasicReflectedXss";
            this.cbBasicReflectedXss.Size = new System.Drawing.Size(121, 17);
            this.cbBasicReflectedXss.TabIndex = 8;
            this.cbBasicReflectedXss.Text = "Basic Reflected Xss";
            this.cbBasicReflectedXss.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedReflectedXss
            // 
            this.cbAdvancedReflectedXss.AutoSize = true;
            this.cbAdvancedReflectedXss.Location = new System.Drawing.Point(9, 235);
            this.cbAdvancedReflectedXss.Name = "cbAdvancedReflectedXss";
            this.cbAdvancedReflectedXss.Size = new System.Drawing.Size(144, 17);
            this.cbAdvancedReflectedXss.TabIndex = 9;
            this.cbAdvancedReflectedXss.Text = "Advanced Reflected Xss";
            this.cbAdvancedReflectedXss.UseVisualStyleBackColor = true;
            // 
            // cbBasicOpenRedirect
            // 
            this.cbBasicOpenRedirect.AutoSize = true;
            this.cbBasicOpenRedirect.Location = new System.Drawing.Point(9, 259);
            this.cbBasicOpenRedirect.Name = "cbBasicOpenRedirect";
            this.cbBasicOpenRedirect.Size = new System.Drawing.Size(124, 17);
            this.cbBasicOpenRedirect.TabIndex = 10;
            this.cbBasicOpenRedirect.Text = "Basic Open Redirect";
            this.cbBasicOpenRedirect.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedOpenRedirect
            // 
            this.cbAdvancedOpenRedirect.AutoSize = true;
            this.cbAdvancedOpenRedirect.Location = new System.Drawing.Point(9, 283);
            this.cbAdvancedOpenRedirect.Name = "cbAdvancedOpenRedirect";
            this.cbAdvancedOpenRedirect.Size = new System.Drawing.Size(147, 17);
            this.cbAdvancedOpenRedirect.TabIndex = 11;
            this.cbAdvancedOpenRedirect.Text = "Advanced Open Redirect";
            this.cbAdvancedOpenRedirect.UseVisualStyleBackColor = true;
            // 
            // cbBasicArbitraryFileOperations
            // 
            this.cbBasicArbitraryFileOperations.AutoSize = true;
            this.cbBasicArbitraryFileOperations.Location = new System.Drawing.Point(9, 307);
            this.cbBasicArbitraryFileOperations.Name = "cbBasicArbitraryFileOperations";
            this.cbBasicArbitraryFileOperations.Size = new System.Drawing.Size(166, 17);
            this.cbBasicArbitraryFileOperations.TabIndex = 12;
            this.cbBasicArbitraryFileOperations.Text = "Basic Arbitrary File Operations";
            this.cbBasicArbitraryFileOperations.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedArbitraryFileOperations
            // 
            this.cbAdvancedArbitraryFileOperations.AutoSize = true;
            this.cbAdvancedArbitraryFileOperations.Location = new System.Drawing.Point(9, 331);
            this.cbAdvancedArbitraryFileOperations.Name = "cbAdvancedArbitraryFileOperations";
            this.cbAdvancedArbitraryFileOperations.Size = new System.Drawing.Size(189, 17);
            this.cbAdvancedArbitraryFileOperations.TabIndex = 13;
            this.cbAdvancedArbitraryFileOperations.Text = "Advanced Arbitrary File Operations";
            this.cbAdvancedArbitraryFileOperations.UseVisualStyleBackColor = true;
            // 
            // cbBasicCommandInjection
            // 
            this.cbBasicCommandInjection.AutoSize = true;
            this.cbBasicCommandInjection.Location = new System.Drawing.Point(273, 19);
            this.cbBasicCommandInjection.Name = "cbBasicCommandInjection";
            this.cbBasicCommandInjection.Size = new System.Drawing.Size(145, 17);
            this.cbBasicCommandInjection.TabIndex = 14;
            this.cbBasicCommandInjection.Text = "Basic Command Injection";
            this.cbBasicCommandInjection.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedCommandInjection
            // 
            this.cbAdvancedCommandInjection.AutoSize = true;
            this.cbAdvancedCommandInjection.Location = new System.Drawing.Point(273, 43);
            this.cbAdvancedCommandInjection.Name = "cbAdvancedCommandInjection";
            this.cbAdvancedCommandInjection.Size = new System.Drawing.Size(168, 17);
            this.cbAdvancedCommandInjection.TabIndex = 15;
            this.cbAdvancedCommandInjection.Text = "Advanced Command Injection";
            this.cbAdvancedCommandInjection.UseVisualStyleBackColor = true;
            // 
            // cbBasicArbitraryFileAccess
            // 
            this.cbBasicArbitraryFileAccess.AutoSize = true;
            this.cbBasicArbitraryFileAccess.Location = new System.Drawing.Point(273, 67);
            this.cbBasicArbitraryFileAccess.Name = "cbBasicArbitraryFileAccess";
            this.cbBasicArbitraryFileAccess.Size = new System.Drawing.Size(150, 17);
            this.cbBasicArbitraryFileAccess.TabIndex = 16;
            this.cbBasicArbitraryFileAccess.Text = "Basic Arbitrary File Access";
            this.cbBasicArbitraryFileAccess.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedArbitraryFileAccess
            // 
            this.cbAdvancedArbitraryFileAccess.AutoSize = true;
            this.cbAdvancedArbitraryFileAccess.Location = new System.Drawing.Point(273, 91);
            this.cbAdvancedArbitraryFileAccess.Name = "cbAdvancedArbitraryFileAccess";
            this.cbAdvancedArbitraryFileAccess.Size = new System.Drawing.Size(173, 17);
            this.cbAdvancedArbitraryFileAccess.TabIndex = 17;
            this.cbAdvancedArbitraryFileAccess.Text = "Advanced Arbitrary File Access";
            this.cbAdvancedArbitraryFileAccess.UseVisualStyleBackColor = true;
            // 
            // cbBasicXPathInjection
            // 
            this.cbBasicXPathInjection.AutoSize = true;
            this.cbBasicXPathInjection.Location = new System.Drawing.Point(273, 115);
            this.cbBasicXPathInjection.Name = "cbBasicXPathInjection";
            this.cbBasicXPathInjection.Size = new System.Drawing.Size(127, 17);
            this.cbBasicXPathInjection.TabIndex = 18;
            this.cbBasicXPathInjection.Text = "Basic XPath Injection";
            this.cbBasicXPathInjection.UseVisualStyleBackColor = true;
            // 
            // cbAdvancedXPathInjection
            // 
            this.cbAdvancedXPathInjection.AutoSize = true;
            this.cbAdvancedXPathInjection.Location = new System.Drawing.Point(273, 139);
            this.cbAdvancedXPathInjection.Name = "cbAdvancedXPathInjection";
            this.cbAdvancedXPathInjection.Size = new System.Drawing.Size(150, 17);
            this.cbAdvancedXPathInjection.TabIndex = 19;
            this.cbAdvancedXPathInjection.Text = "Advanced XPath Injection";
            this.cbAdvancedXPathInjection.UseVisualStyleBackColor = true;
            // 
            // cbWeakPermissions
            // 
            this.cbWeakPermissions.AutoSize = true;
            this.cbWeakPermissions.Location = new System.Drawing.Point(273, 163);
            this.cbWeakPermissions.Name = "cbWeakPermissions";
            this.cbWeakPermissions.Size = new System.Drawing.Size(113, 17);
            this.cbWeakPermissions.TabIndex = 20;
            this.cbWeakPermissions.Text = "Weak Permissions";
            this.cbWeakPermissions.UseVisualStyleBackColor = true;
            // 
            // cbCookieSecurity
            // 
            this.cbCookieSecurity.AutoSize = true;
            this.cbCookieSecurity.Location = new System.Drawing.Point(273, 187);
            this.cbCookieSecurity.Name = "cbCookieSecurity";
            this.cbCookieSecurity.Size = new System.Drawing.Size(108, 17);
            this.cbCookieSecurity.TabIndex = 21;
            this.cbCookieSecurity.Text = "Insecure Cookies";
            this.cbCookieSecurity.UseVisualStyleBackColor = true;
            // 
            // cbHardCodedPassword
            // 
            this.cbHardCodedPassword.AutoSize = true;
            this.cbHardCodedPassword.Location = new System.Drawing.Point(273, 211);
            this.cbHardCodedPassword.Name = "cbHardCodedPassword";
            this.cbHardCodedPassword.Size = new System.Drawing.Size(136, 17);
            this.cbHardCodedPassword.TabIndex = 22;
            this.cbHardCodedPassword.Text = "Hard-coded Passwords";
            this.cbHardCodedPassword.UseVisualStyleBackColor = true;
            // 
            // cbWeakCryptoRule
            // 
            this.cbWeakCryptoRule.AutoSize = true;
            this.cbWeakCryptoRule.Location = new System.Drawing.Point(273, 235);
            this.cbWeakCryptoRule.Name = "cbWeakCryptoRule";
            this.cbWeakCryptoRule.Size = new System.Drawing.Size(88, 17);
            this.cbWeakCryptoRule.TabIndex = 23;
            this.cbWeakCryptoRule.Text = "Weak Crypto";
            this.cbWeakCryptoRule.UseVisualStyleBackColor = true;
            // 
            // cbResourceLeak
            // 
            this.cbResourceLeak.AutoSize = true;
            this.cbResourceLeak.Location = new System.Drawing.Point(273, 259);
            this.cbResourceLeak.Name = "cbResourceLeak";
            this.cbResourceLeak.Size = new System.Drawing.Size(99, 17);
            this.cbResourceLeak.TabIndex = 24;
            this.cbResourceLeak.Text = "Resource Leak";
            this.cbResourceLeak.UseVisualStyleBackColor = true;
            // 
            // ConfigurationDialog
            // 
            this.AcceptButton = this.bnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.ClientSize = new System.Drawing.Size(518, 461);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigurationDialog";
            this.Text = "ConfigurationDialog";
            this.tabControl1.ResumeLayout(false);
            this.csharp.ResumeLayout(false);
            this.csharp.PerformLayout();
            this.php.ResumeLayout(false);
            this.php.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnOK;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage csharp;
        private System.Windows.Forms.TabPage php;
        private System.Windows.Forms.CheckBox cbPhpCommandInjection;
        private System.Windows.Forms.CheckBox cbBasicPhpFileInclusion;
        private System.Windows.Forms.CheckBox cbBasicPhpSqlInjection;
        private System.Windows.Forms.CheckBox cbBasicSqlInjection;
        private System.Windows.Forms.CheckBox cbResourceLeak;
        private System.Windows.Forms.CheckBox cbWeakCryptoRule;
        private System.Windows.Forms.CheckBox cbHardCodedPassword;
        private System.Windows.Forms.CheckBox cbCookieSecurity;
        private System.Windows.Forms.CheckBox cbWeakPermissions;
        private System.Windows.Forms.CheckBox cbAdvancedXPathInjection;
        private System.Windows.Forms.CheckBox cbBasicXPathInjection;
        private System.Windows.Forms.CheckBox cbAdvancedArbitraryFileAccess;
        private System.Windows.Forms.CheckBox cbBasicArbitraryFileAccess;
        private System.Windows.Forms.CheckBox cbAdvancedCommandInjection;
        private System.Windows.Forms.CheckBox cbBasicCommandInjection;
        private System.Windows.Forms.CheckBox cbAdvancedArbitraryFileOperations;
        private System.Windows.Forms.CheckBox cbBasicArbitraryFileOperations;
        private System.Windows.Forms.CheckBox cbAdvancedOpenRedirect;
        private System.Windows.Forms.CheckBox cbBasicOpenRedirect;
        private System.Windows.Forms.CheckBox cbAdvancedReflectedXss;
        private System.Windows.Forms.CheckBox cbBasicReflectedXss;
        private System.Windows.Forms.CheckBox cbInlineAspxRule;
        private System.Windows.Forms.CheckBox cbSqlConnectionString;
        private System.Windows.Forms.CheckBox cbIisConfig;
        private System.Windows.Forms.CheckBox cbBasicCsrf;
        private System.Windows.Forms.CheckBox cbBasicLdapInjection;
        private System.Windows.Forms.CheckBox cbMildSqlInjection;
        private System.Windows.Forms.CheckBox cbAdvancedSqlInjection;
    }
}