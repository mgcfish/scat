using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public class GraphBuilder
    {
        public static void BuildGraph(List<FileLoader> loaders)
        {
            foreach (var l in loaders)
            {
                foreach (var n in l.SyntaxAnalyzer.Nodes)
                {
                    BuildGraph(loaders, n, 1);
                }
            }

        }

        private static void BuildGraph(List<FileLoader> loaders, Node currentNode, int depth)
        {
            if (depth < Configuration.MaxRecursionDepth)
            {
                foreach (var i in currentNode.Invocations)
                {
                    Node adjacentNode = FindNode(loaders, currentNode, i);
                    if (adjacentNode != null)
                    {
                        currentNode.AdjacentNodes.Add(adjacentNode);
                        BuildGraph(loaders, adjacentNode, depth + 1);
                    }
                }
            }
        }

        //
        // this one gets...complex.
        // Case 1: foo.fn(snarf);
        // Case 2: Foo.fn(snarf);
        // case 3: x = new CommandRunner(Request.QueryString[0]); <- this is bad.
        // case 4: x = foo.fn(snarf)?!?!
        //
        private static string FindClassNameForInvocation(List<FileLoader> loaders, Node sourceNode, Invocation i)
        {
            string retval = string.Empty;

            string prefix = i.InvocationCode.Substring(0, i.InvocationCode.IndexOf('('));

            if (prefix.Contains("="))
            {
                prefix = prefix.Substring(1 + prefix.IndexOf("="));
            }

            if (prefix.Contains("new"))
            {
                // get the token after new
                string[] tokens = prefix.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                retval = tokens.Last();
            }
            else
            {
                if (prefix.Contains("."))
                {
                    if (prefix.Contains("this."))
                    {
                        retval = sourceNode.ClassName;
                    }
                    else
                    {
                        string[] subtokens = prefix.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (subtokens.Length > 0)
                        {
                            string variableOrClassName = subtokens[0];

                            //
                            // cycle through class names to see if it's a static call.
                            //
                            foreach (var l in loaders)
                            {
                                foreach (var c in l.SyntaxAnalyzer.Classes)
                                {
                                    if (variableOrClassName.CompareTo(c) == 0)
                                    {
                                        retval = c;
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(retval))
                            {
                                //
                                // it looks like it's a variable.
                                //

                                foreach (var l in loaders)
                                {
                                    foreach (var n in l.SyntaxAnalyzer.Nodes)
                                    {
                                        foreach (var v in n.VariablesInScope)
                                        {
                                            if (v.VariableName.CompareTo(variableOrClassName) == 0)
                                            {
                                                retval = v.VariableType;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    retval = sourceNode.ClassName;
                }
            }
         

            Console.WriteLine(prefix);

            return retval;  
        }

        private static Node FindNode(List<FileLoader> loaders, Node sourceNode, Invocation i)
        {
            Node retval = null;

            string invocationClassName = FindClassNameForInvocation( loaders,  sourceNode,  i);


            foreach (var l in loaders)
            {
                foreach (var n in l.SyntaxAnalyzer.Nodes)
                {
                    if (n.NodeName.CompareTo(i.InvocationName) == 0 && n.ClassName.CompareTo(invocationClassName) == 0)
                    {
                        retval = n;
                    }
                }
            }

            return retval;
        }

    }
}
