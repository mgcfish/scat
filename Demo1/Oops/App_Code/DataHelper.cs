using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


public static class DataHelper
{
    public static bool LookupUser(string username)
    {
        bool retval = false;

        string fullyQualifiedUsername = "EVIL-" + username;
        SqlCommand lookupQuery = new SqlCommand("SELECT * from users where username = '" + fullyQualifiedUsername + "'");


        return retval;
    }
}