using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace scat
{
    public enum VulnerabilityType
    {
        LdapInjection,
        Csrf,
        Custom,
        Misconfiguration,
        UnspecifiedBadThing,
        MiscellaneousFileSystemOperation,
        ResourceLeak,
        WeakCrypto,
        HardCodedPassword,
        InsecureCookies,
        WeakPermissions,
        XSS,
        SqlInjection,
        CommandInjection,
        RemoteFileInclude,
        LocalFileInclude,
        OpenRedirect,
        ArbitraryFileAccess,
        XPathInjection,
        MiscFileOperation
    }

    public enum Severity
    {
        OhShit,
        High,
        Medium,
        Low,
        Informational
    }


    public static class Util
    {

        public static void AddVulnerability(List<BaseVulnerability> l, BaseVulnerability v)
        {
            string s = v.ToHtml();

            bool found = false;

            foreach (var x in l)
            {
                if (x.ToHtml().CompareTo(v.ToHtml()) == 0)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                l.Add(v);
            }
        }

        public static void TaintVariable(Node n, string variableName)
        {
            foreach (var v in n.VariablesInScope)
            {
                if (v.VariableName.CompareTo(variableName) == 0)
                {
                    v.IsTainted = true;
                }
            }
        }

        public static bool ContainsVerb(string s)
        {
            bool retval = false;
            string lwr = s.ToLower();

            foreach (var v in Verbs)
            {
                if (lwr.Contains(v))
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }

        public static string FindVerb(string s)
        {
            string retval = string.Empty;

            string lwr = s.ToLower();

            foreach (var v in Verbs)
            {
                if (lwr.Contains(v))
                {
                    retval = v;
                    break;
                }
            }

            return retval;
        }

        public static string[] Verbs = 
        {
            "accept",
            "add",
            "allow",
            "approve",
            "break",
            "buy",
            "call",
            "change",
            "charge",
            "claim",
            "clean",
            "clear",
            "commit",
            "confirm",
            "delete",
            "describe",
            "destroy",
            "drop",
            "execute",
            "force",
            "kick",
            "kill",
            "like",
            "publish",
            "reboot",
            "remember",
            "remove",
            "rename",
            "reply",
            "run",
            "save",
            "sell",
            "send",
            "shutdown",
            "start",
            "stop",
            "transfer",
            "write"
        };

        public static string CodeStackToHtml(Stack<string> codeStack)
        {
            string retval = "<table  style=\"border:1px solid #000;\">";

            string[] codeArray = codeStack.ToArray();

            foreach (var codeStackFrame in codeArray)
            {
                retval += string.Format("  <tr>   <td><hr/>{0}</td>   </tr>  ",  System.Net.WebUtility.HtmlEncode( codeStackFrame) );
            }

            retval += "</table>";

            return retval;
        }

        public static int GetImageIndexForSeverity(Severity severity)
        {
            int retval = 0;

            switch (severity)
            {
                case Severity.OhShit:
                    retval = 0;
                    break;
                case Severity.High:
                    retval = 1;
                    break;
                case Severity.Medium:
                    retval = 2;
                    break;
                case Severity.Low:
                    retval = 3;
                    break;
                case Severity.Informational:
                    retval = 4;
                    break;
                default:
                    break;
            }

            return retval;
        }

        public static List<Variable> EnumerateTaintedVariables(string code)
        {
            List<Variable> retval = new List<Variable>();

            string[] lines = code.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string lwr = line.ToLower().Trim();
                if (lwr.StartsWith("string"))
                {
                    if (line.Contains("=") && Util.ContainsScaryInput(line) && !line.Contains("!=") && !line.Contains("=="))
                    {
                        
                        string lh = line.Substring(0, line.IndexOf('='));
                        string rh = line.Substring(line.IndexOf('='));

                        if (lh.ToLower().Contains("string") && Util.ContainsScaryInput(rh))
                        {
                            string[] subTokens = lh.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            retval.Add(new Variable(subTokens.Last(), line));


                        }
                    }
                }
            }

            return retval;
        }

        public static bool ScaryMethodBeforeEvilInput(string code)
        {
            bool retval = false;

            foreach (var scaryMethod in ScaryMethodNames)
            {
                if (code.Contains(scaryMethod))
                {
                    foreach (var scaryInput in ScaryUserInput)
                    {
                        if (code.Contains(scaryInput))
                        {
                            if (ABeforeB(code, scaryMethod, scaryInput))
                            {
                                retval = true;
                            }
                        }
                    }
                }
            }

            return retval;
        }

        public static bool ABeforeB(string s, string a, string b)
        {
            bool retval = false;

            if (s.IndexOf(a) > 0 && s.IndexOf(b) > 0)
            {
                if (s.IndexOf(a) < s.IndexOf(b))
                {
                    retval = true;
                }
            }

            return retval;
        }

        public static string[] ScaryMethodNames = { 
            "File.AppendAllLines",
            "File.AppendAllText",
            "File.AppendText",
            "File.Copy",
            "File.Exists",
            "File.Create",
            "File.Decrypt",
            "File.Delete",
            "File.Encrypt",
            "File.Move",
            "File.ReadAllBytes",
            "File.ReadAllLines",
            "File.ReadAllText",
            "File.ReadLines",
            "File.SetAccessControl",
            "File.SetCreationTime",
            "File.WriteAllBytes",
            "File.WriteAllLines",
            "File.WriteAllText",
            "SqlCommand",
            "Server.Execute",
            "Server.Transfer",
            "Directory.CreateDirectory",
            "Directory.Delete",
            "Directory.Move",
            "Response.Write",
            "Response.TransmitFile",
            "Response.WriteFile",
            "Response.BinaryWrite",
            "Response.Redirect",
            "Process.Start"
                               };

        public static string FindFirstScaryMethod(string code)
        {
            string retval = string.Empty;

            foreach (var sm in ScaryMethodNames)
            {
                if (code.Contains(sm))
                {
                    retval = sm;
                    break;
                }
            }

            return retval;
        }

        public static bool ContainsScaryMethod(string code)
        {
            bool retval = false;

            foreach (var sm in ScaryMethodNames)
            {
                if (code.Contains(sm))
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }

        private static string VariablesInScopeToHtml(List<Variable> variableList)
        {
            string retval = "<table>";

            foreach (var v in variableList)
            {
                if (v.IsTainted)
                {
                    retval += string.Format("<tr>  <td>{0}</td> <td>{1}</td>  </tr>", v.VariableName, v.IsTainted ? "Tainted" : "Clean");
                }
            }

            retval += "</table>";

            return retval;
        }

        public static string CallStackToHtml(Stack<Node> s)
        {
            string retval = "<table  style=\"border:1px solid #000;\">";

            Node[] ar = s.ToArray();

            foreach (Node n in ar)
            {
                string row = string.Format("  <tr>   <td> <hr/>{0}</td>      </tr> ", n.Filename + "::" + n.ClassName + "." + n.NodeName);
                retval += row;
            }

            retval += "</table>";

            return retval;
        }
        public static Color GetColorForSeverity(Severity s)
        {
            Color retval = Color.LightGreen;

            switch (s)
            {
                case Severity.OhShit:
                    retval = Color.Red;
                    break;
                case Severity.High:
                    retval = Color.Orange;
                    break;
                case Severity.Medium:
                    retval = Color.Yellow;
                    break;
                case Severity.Low:
                    retval = Color.LightBlue;
                    break;
                default:
                    retval = Color.LightGreen;
                    break;
            }

            return retval;
        }

        public static string[] RedeemingWords = { "clean", "parse", "encode", "sanitiz", "convert.toint" };

        public static bool IsRedeemed(string code)
        {
            bool retval = false;
            string lwr = code.ToLower();

            foreach (var rw in RedeemingWords)
            {
                if (lwr.Contains(rw))
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }

        public static string[] ScaryUserInput = { "Request.", "Request[", ".Text" };

        public static bool ContainsScaryInput(string code)
        {
            bool retval = false;

            foreach (string s in ScaryUserInput)
            {
                if (code.Contains(s))
                {
                    retval = true;
                }
            }

            return retval;
        }

        public static string ParseMethodNameFromInvocation(string invocationCode)
        {
            string retval = string.Empty;
            string sub = invocationCode.Trim().Substring(0, invocationCode.IndexOf('('));
            string[] tokens = sub.Split(". ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                retval = token;
            }

            return retval.Trim();
        }

        //
        // finds all instances of <% ... %>
        //
        public static IEnumerable<string> FindAllInlineCodes(string raw)
        {
            List<string> retval = new List<string>();

            if (raw.Contains("<%") && raw.Contains("%>"))
            {
                char[] pch = raw.ToCharArray();

                bool open = false;
                StringBuilder sb = new StringBuilder();

                for (int x = 0; x < pch.Length; x++)
                {
                    if (open)
                    {
                        sb.Append(pch[x]);
                    }

                    if (pch[x] == '<' && pch[x + 1] == '%')
                    {
                        open = true;
                    }
                    else if (pch[x] == '%' && pch[x + 1] == '>')
                    {
                        open = false;
                        retval.Add("<" + sb.ToString() + ">");
                        sb = new StringBuilder();
                    }

                }
            }


            return retval;
        }

        public static IEnumerable<string> FindAllTags(string raw)
        {
            List<string> retval = new List<string>();

            char[] pch = raw.ToCharArray();

            for (int x = 0; x < pch.Length; x++)
            {
                if (pch[x] == '<')
                {
                    StringBuilder sb = new StringBuilder();
                    for (; x < pch.Length; x++)
                    {
                        sb.Append(pch[x]);
                        if (pch[x] == '>')
                        {
                            retval.Add(sb.ToString());
                            break;
                        }
                    }
                }
            }

            return retval;
        }

        public static IEnumerable<string> FindAllInstancesOf(string raw, string method)
        {
            List<string> retval = new List<string>();

            string tmp = raw;

            while (tmp.Contains(method))
            {
                string code = FindFirstInstanceOf(tmp, method);
                retval.Add(code);

                int startIndex = tmp.IndexOf(method);
                if ((startIndex + method.Length) < tmp.Length)
                {
                    tmp = tmp.Substring(startIndex + method.Length);
                }
                else
                {
                    break;
                }
            }


            return retval.AsEnumerable();
        }

        public static string FindFirstInstanceOf(string raw, string method)
        {
            string retval = string.Empty;

            int startIndex = raw.IndexOf(method);
            if (startIndex > 0)
            {
                string code = raw.Substring(startIndex); // raw, from response.write to the end
                char[] pchCode = code.ToCharArray();

                StringBuilder stringBuilder = new StringBuilder();

                int open = 0;
                int close = 0;

                string xxx = string.Empty;

                for (int x = 0; x < pchCode.Length; x++)
                {
                    stringBuilder.Append(pchCode[x]);

                    if (pchCode[x] == '(')
                    {
                        open++;
                    }

                    if (pchCode[x] == ')')
                    {
                        close++;
                    }

                    if (open > 0)
                    {
                        if (open == close)
                        {
                            break;
                        }
                    }
                }
                retval = stringBuilder.ToString();
            }
            return retval;
        }

        public static List<string> FindAllComments(string raw)
        {
            List<string> retval = new List<string>();

            char[] pch = raw.ToCharArray();
            for (int x = 0; x < pch.Length; x++)
            {
                if (pch[x] == '<' && pch[x + 1] == '!' && pch[x + 2] == '-' && pch[x + 3] == '-')
                {
                    StringBuilder sb = new StringBuilder();

                    for (; x < pch.Length; x++)
                    {
                        sb.Append(pch[x]);
                        if (pch[x] == '-' && pch[x + 1] == '-' && pch[x + 2] == '>')
                        {
                            retval.Add(sb.ToString().Trim());
                            break;
                        }
                    }
                }
            }

            return retval;
        }
    }
}
