using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.CSharp;
using System.IO;

namespace scat
{
    public class SyntaxAnalyzer
    {
        public SyntaxTree SyntaxTree
        {
            get;
            private set;
        }

        public string Filename
        {
            get;
            private set;
        }

        public List<string> Classes
        {
            get;
            set;
        }

        public List<Variable> GlobalVariables
        {
            get;
            set;
        }

        public List<Node> Nodes
        {
            get;
            set;
        }

        public SyntaxAnalyzer(string filename)
        {
            this.Filename = filename;
            this.Classes = new List<string>();
            this.Nodes = new List<Node>();
            this.GlobalVariables = new List<Variable>();
        }

        public void Load()
        {
            if (this.Filename.EndsWith(".cs"))
            {
                string raw = File.ReadAllText(this.Filename);
                this.SyntaxTree = SyntaxTree.Parse(raw);
                Analyze(this.SyntaxTree.Children);


                //
                // add in the global variables, I guess.
                //
                foreach (var n in this.Nodes)
                {
                    foreach (var g in this.GlobalVariables)
                    {
                        n.VariablesInScope.Add(g);
                    }
                }
            }
        }

        private string GetFieldTypeForFieldDeclaration(string code)
        {
            string retval = string.Empty;

            string[] lines = code.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (!line.Trim().StartsWith("//") && !line.Trim().StartsWith("["))
                {
                    string t = line.Trim().Replace("public", string.Empty).Replace("private", string.Empty).Replace("protected", string.Empty).Replace("global::", string.Empty);
                    string[] tokens = t.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length > 1)
                    {
                        retval = tokens[0];
                        break;
                    }
                }
            }

            return retval;
        }

        private string FindNameForVariableInitializer(string code)
        {
            string retval = string.Empty;

            if (!code.Contains(" ") && !code.Contains("("))
            {
                retval = code.Split(" \r\t\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            }
            else
            {
                string[] tokens = code.Replace("(", " ( ").Split(". \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < tokens.Length; x++)
                {
                    if (tokens[x].CompareTo("(") == 0)
                    {
                        if ((x - 1) >= 0)
                        {
                            retval = tokens[x - 1];
                        }
                    }
                }
            }

            return retval;
        }

        private string[] ParseInvocationParameters(string code)
        {
            List<string> retval = new List<string>();
            string subCode = code.Replace(".ToString ()", string.Empty);
            int open = subCode.IndexOf('(') + 1;
            int close = subCode.LastIndexOf(')');

            if (open < close)
            {
                string paramList = subCode.Substring(open, close - open);
                char[] pch = paramList.ToCharArray();
                string currentParameter = string.Empty;

                for (int x = 0; x < pch.Length; x++)
                {
                    if (pch[x] == '"')
                    {
                        currentParameter += pch[x];
                        x++;

                        while (x < pch.Length)
                        {

                            if (pch[x] == '"')
                            {
                                if (pch[x - 1] != '\\')
                                {
                                    currentParameter += pch[x];
                                    break;
                                }
                                else
                                {
                                    x++;
                                }
                            }
                            else
                            {
                                currentParameter += pch[x];
                                x++;
                            }


                        }
                    }
                    else if (pch[x] == '(')
                    {
                        currentParameter += pch[x];
                        x++;
                        for (; x < pch.Length; x++)
                        {
                            currentParameter += pch[x];
                            if (pch[x] == ')')
                            {
                                break;
                            }
                        }
                    }
                    else if (pch[x] == ',')
                    {
                        if (!string.IsNullOrEmpty(currentParameter.Trim()))
                        {
                            retval.Add(currentParameter.Trim());
                            currentParameter = string.Empty;
                        }
                    }
                    else
                    {
                        currentParameter += pch[x];
                    }
                }
                if (!string.IsNullOrEmpty(currentParameter.Trim()))
                {
                    retval.Add(currentParameter.Trim());
                }

            }

            return retval.ToArray();
        }

        private void Analyze(IEnumerable<AstNode> nodes)
        {
            foreach (AstNode node in nodes)
            {
                string code = node.ToString();
                string typeName = node.GetType().Name;

#if SNARF
                if (code.Contains("SqlCommand"))
                {
                    Configuration.debug("********");
                    Configuration.debug("T: " + typeName);
                    Configuration.debug("   ---------  ");
                    Configuration.debug(code);
                    Configuration.debug("********");
                }
#endif

                if (typeName.CompareTo("TypeDeclaration") == 0)
                {
                    string className = FindNext(node.Children, "Identifier");
                    if (!string.IsNullOrEmpty(className))
                    {
                        if (!this.Classes.Contains(className))
                        {
                            this.Classes.Add(className);
                        }
                    }
                }
                else if (typeName.CompareTo("FieldDeclaration") == 0)
                {
                    string name = FindNext(node.Children, "Identifier");

                    if (!string.IsNullOrEmpty(name))
                    {
                        Variable v = new Variable(name, code, GetFieldTypeForFieldDeclaration(code));
                        this.GlobalVariables.Add(v);
                    }


                }
                else if (typeName.CompareTo("MethodDeclaration") == 0 || typeName.CompareTo("ConstructorDeclaration") == 0)
                {
                    string name = FindNext(node.Children, "Identifier");
                    string className = this.Classes.Count > 0 ? this.Classes.Last() : "GLOBAL";
                    Node n = new Node(this.Filename, className, name, code);
                    this.Nodes.Add(n);
                }

                else if (typeName.CompareTo("VariableDeclarationStatement") == 0)
                {
                    try
                    {
                        string name = FindNext(node.Children, "Identifier");

                        //
                        // extract variable type.
                        //

                        string[] tokens = code.Trim().Split(" \r\t\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        Node n = this.Nodes.Last();

                        if (tokens.Length > 0)
                        {
                            string variableType = tokens[0];
                            Variable v = new Variable(name, code, variableType);
                            n.VariablesInScope.Add(v);

                        }
                        else
                        {
                            this.Nodes.Last().VariablesInScope.Add(new Variable(name, code));
                        }
                    }
                    catch (Exception)
                    {
                    }

                }
                else if (typeName.CompareTo("VariableInitializer") == 0)
                {
                    try
                    {

                        //string name = FindNext(node.Children, "Identifier");
                        string name = FindNameForVariableInitializer(code);
                        string[] parameters = ParseInvocationParameters(code);

                        this.Nodes.Last().Invocations.Add(new Invocation(name, code, parameters, true));
                    }
                    catch (Exception)
                    {
                        string name = FindNext(node.Children, "Identifier");
                        this.GlobalVariables.Add(new Variable(name, code));
                    }
                }
                else if (typeName.CompareTo("AssignmentExpression") == 0)
                {
                    try
                    {
                        if (code.Contains("(") && code.Contains(")") && code.Contains("new"))
                        {
                            string name = Util.ParseMethodNameFromInvocation(code);
                            string[] parameters = ParseInvocationParameters(code);
                            Invocation i = new Invocation(name, code, parameters);
                            this.Nodes.Last().Invocations.Add(i);
                        }
                        else
                        {
                            string name = FindNext(node.Children, "IdentifierExpression");
                            this.Nodes.Last().VariablesInScope.Add(new Variable(name, code));
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (typeName.CompareTo("InvocationExpression") == 0)
                {
                    try
                    {
                        string name = Util.ParseMethodNameFromInvocation(code);
                        string[] parameters = ParseInvocationParameters(code);
                        this.Nodes.Last().Invocations.Add(new Invocation(name, code, parameters));
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (typeName.CompareTo("ParameterDeclaration") == 0)
                {
                    try
                    {
                        string name = FindNext(node.Children, "Identifier");
                        this.Nodes.Last().Parameters.Add(new Parameter(name, code));
                    }
                    catch (Exception)
                    {
                    }
                }

                if (node.Children.Count() > 0)
                {
                    Analyze(node.Children);
                }
            }
        }

        private string FindNext(IEnumerable<AstNode> nodes, string type)
        {
            string retval = string.Empty;

            foreach (AstNode node in nodes)
            {
                string typeName = node.GetType().Name;

                if (typeName.CompareTo(type) == 0)
                {
                    retval = node.ToString();
                    break;
                }

                if (node.Children.Count() > 0)
                {
                    retval = FindNext(node.Children, type);
                }
            }

            return retval;
        }
    }
}
