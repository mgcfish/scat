using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SqlInjectable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = Request.QueryString["user"];
        if (DataHelper.LookupUser(username))
        {
            Response.Write("<h1>User Exists: " + username + "</h1>");
        }
        else
        {
            Response.Write("<h1>User Failed to exist: " + username + "</h1>");
        }
    }
}