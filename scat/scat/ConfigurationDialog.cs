using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scat
{
    public partial class ConfigurationDialog : Form
    {
        public ConfigurationDialog()
        {
            InitializeComponent();
            cbPhpCommandInjection.Checked = Configuration.bBasicPhpCommandInjection;
            cbBasicPhpFileInclusion.Checked = Configuration.bBasicPhpFileInclusion;
            cbBasicPhpSqlInjection.Checked = Configuration.bBasicPhpSqlInjection;
            cbBasicSqlInjection.Checked = Configuration.bBasicSqlInjection;
            cbAdvancedSqlInjection.Checked = Configuration.bAdvancedSqlInjection;
            cbMildSqlInjection.Checked = Configuration.bMildSqlInjection;
            cbBasicLdapInjection.Checked = Configuration.bBasicLdapInjection;
            cbBasicCsrf.Checked = Configuration.bBasicCsrf;
            cbIisConfig.Checked = Configuration.bIisConfig;
            cbSqlConnectionString.Checked = Configuration.bSqlConnectionString;
            cbInlineAspxRule.Checked = Configuration.bInlineAspxRule;
            cbBasicReflectedXss.Checked = Configuration.bBasicReflectedXss;
            cbAdvancedReflectedXss.Checked = Configuration.bAdvancedReflectedXss;
            cbBasicOpenRedirect.Checked = Configuration.bBasicOpenRedirect;
            cbAdvancedOpenRedirect.Checked = Configuration.bAdvancedOpenRedirect;
            cbBasicArbitraryFileOperations.Checked = Configuration.bBasicArbitraryFileOperations;
            cbAdvancedArbitraryFileOperations.Checked = Configuration.bAdvancedArbitraryFileOperations;
            cbBasicCommandInjection.Checked = Configuration.bBasicCommandInjection;
            cbAdvancedCommandInjection.Checked = Configuration.bAdvancedCommandInjection;
            cbBasicArbitraryFileAccess.Checked = Configuration.bBasicArbitraryFileAccess;
            cbAdvancedArbitraryFileAccess.Checked = Configuration.bAdvancedArbitraryFileAccess;
            cbBasicXPathInjection.Checked = Configuration.bBasicXPathInjection;
            cbAdvancedXPathInjection.Checked = Configuration.bAdvancedXPathInjection;
            cbWeakPermissions.Checked = Configuration.bWeakPermissions;
            cbCookieSecurity.Checked = Configuration.bCookieSecurity;
            cbHardCodedPassword.Checked = Configuration.bHardCodedPassword;
            cbWeakCryptoRule.Checked = Configuration.bWeakCryptoRule;
            cbResourceLeak.Checked = Configuration.bResourceLeak;
        }

        private void bnOK_Click(object sender, EventArgs e)
        {
            Configuration.bBasicPhpCommandInjection = cbPhpCommandInjection.Checked;
            Configuration.bBasicPhpFileInclusion = cbBasicPhpFileInclusion.Checked;
            Configuration.bBasicPhpSqlInjection = cbBasicPhpSqlInjection.Checked;
            Configuration.bBasicSqlInjection = cbBasicSqlInjection.Checked;
            Configuration.bAdvancedSqlInjection = cbAdvancedSqlInjection.Checked;
            Configuration.bMildSqlInjection = cbMildSqlInjection.Checked;
            Configuration.bBasicLdapInjection = cbBasicLdapInjection.Checked;
            Configuration.bBasicCsrf = cbBasicCsrf.Checked;
            Configuration.bIisConfig = cbIisConfig.Checked;
            Configuration.bSqlConnectionString = cbSqlConnectionString.Checked;
            Configuration.bInlineAspxRule = cbInlineAspxRule.Checked;
            Configuration.bBasicReflectedXss = cbBasicReflectedXss.Checked;
            Configuration.bAdvancedReflectedXss = cbAdvancedReflectedXss.Checked;
            Configuration.bBasicOpenRedirect = cbBasicOpenRedirect.Checked;
            Configuration.bAdvancedOpenRedirect = cbAdvancedOpenRedirect.Checked;
            Configuration.bBasicArbitraryFileOperations = cbBasicArbitraryFileOperations.Checked;
            Configuration.bAdvancedArbitraryFileOperations = cbAdvancedArbitraryFileOperations.Checked;
            Configuration.bBasicCommandInjection = cbBasicCommandInjection.Checked;
            Configuration.bAdvancedCommandInjection = cbAdvancedCommandInjection.Checked;
            Configuration.bBasicArbitraryFileAccess = cbBasicArbitraryFileAccess.Checked;
            Configuration.bAdvancedArbitraryFileAccess = cbAdvancedArbitraryFileAccess.Checked;
            Configuration.bBasicXPathInjection = cbBasicXPathInjection.Checked;
            Configuration.bAdvancedXPathInjection = cbAdvancedXPathInjection.Checked;
            Configuration.bWeakPermissions = cbWeakPermissions.Checked;
            Configuration.bCookieSecurity = cbCookieSecurity.Checked;
            Configuration.bHardCodedPassword = cbHardCodedPassword.Checked;
            Configuration.bWeakCryptoRule = cbWeakCryptoRule.Checked;
            Configuration.bResourceLeak = cbResourceLeak.Checked;

            this.DialogResult = DialogResult.OK;
        }
    }
}
