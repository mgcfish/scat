using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
public partial class BasicCommandInjection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cmd = Request.QueryString["cmd"];

        if (!string.IsNullOrEmpty(cmd))
        {
            Process.Start(cmd, "/test");
        }
    }
}