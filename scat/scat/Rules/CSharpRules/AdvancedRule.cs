using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class AdvancedRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        ITemplate template;
        public AdvancedRule(List<FileLoader> loaders, ITemplate template)
        {
            this.template = template;
            this.Loaders = loaders;
            this.Vulnerabilities = new List<BaseVulnerability>();
        }

        private List<BaseVulnerability> Vulnerabilities;

        public IEnumerable<BaseVulnerability> GetVulnerabilities()
        {
            return this.Vulnerabilities;
        }

        public string GetRuleName()
        {
            return this.template.GetRuleName();
        }

        private Invocation FindInvocationForAdjacentNode(Node n, Node a)
        {
            Invocation retval = null;

            foreach (var i in n.Invocations)
            {
                if (a.NodeName.CompareTo(i.InvocationName) == 0)
                {
                    retval = i;
                    break;
                }
            }

            return retval;
        }

        private Node FindAdjacentNodeForInvocation(Node n, Invocation i)
        {
            Node retval = null;

            foreach (var a in n.AdjacentNodes)
            {
                if (a.NodeName.CompareTo(i.InvocationName) == 0)
                {
                    retval = a;
                    break;
                }
            }

            return retval;
        }


        private void DeepEvaluate()
        {
            foreach (var l in this.Loaders)
            {
                if (l.Filename.ToLower().EndsWith(".aspx.cs"))
                {

                    foreach (var n in l.SyntaxAnalyzer.Nodes)
                    {
                        foreach (var i in n.Invocations)
                        {

                            Node adjacnetNode = FindAdjacentNodeForInvocation(n, i);
                            if (adjacnetNode != null)
                            {
                                Stack<Node> s = new Stack<Node>();
                                s.Push(n);

                                DeepEvaluate_r(adjacnetNode, s, 1);

                                s.Pop();
                            }
                        }
                    }
                }
            }
        }

 

        private void DeepEvaluate_r(Node n, Stack<Node> s, int depth)
        {
            if (depth < Configuration.MaxRecursionDepth)
            {
                s.Push(n);

                foreach (var i in n.Invocations)
                {
                    if (Util.ContainsScaryMethod(i.InvocationCode))
                    {
      
                        foreach (var v in n.VariablesInScope)
                        {
                            if (v.IsTainted)
                            {
                                if (i.InvocationParameterList.Contains(v.VariableName))
                                {
                                    string stack = Util.CallStackToHtml(s);
                                    BaseVulnerability baseVulnerability = new GenericVulnerability(n.Filename, "Total Protonic Reversal: " + Util.FindFirstScaryMethod(i.InvocationCode), n.ClassName + "." + n.NodeName, "", Util.CallStackToHtml(s), "Tainted variable: " + v.VariableName, Severity.Medium, VulnerabilityType.Custom);
                                    Util.AddVulnerability(this.Vulnerabilities, baseVulnerability);
                                    break;
                                }
                            }
                        }
                    }
                }

                foreach (var adjacnetNode in n.AdjacentNodes)
                {
                    DeepEvaluate_r(adjacnetNode, s, depth + 1);
                }

                s.Pop();
            }
        }

        public void Evaluate()
        {
            lock (Configuration.globalLock)
            {
                foreach (var l in this.Loaders)
                {
                    foreach (var n in l.SyntaxAnalyzer.Nodes)
                    {
                        foreach (var i in n.Invocations)
                        {
                            //
                            // Case: fn(Request.QueryString[0])
                            //

                            if (Util.ContainsScaryInput(i.InvocationCode) && !Util.IsRedeemed(i.InvocationCode))
                            {

                                Stack<Node> stack = new Stack<Node>();
                                stack.Push(n);


                                Node adjacnetNode = FindAdjacentNodeForInvocation(n, i);
                                if (adjacnetNode != null)
                                {
                                    if (adjacnetNode.ParameterListContainsAString())
                                    {

                                        for (int x = 0; x < i.InvocationParameters.Count(); x++)
                                        {
                                            if (Util.ContainsScaryInput(i.InvocationParameters[x]))
                                            {
                                                if (x < adjacnetNode.Parameters.Count)
                                                {
                                                    adjacnetNode.Parameters[x].IsTainted = true;
                                                }
                                            }
                                        }

                                        Stack<string> codeStack = new Stack<string>();
                                        codeStack.Push(n.NodeCode);
                                        codeStack.Push(i.InvocationCode);
                                        Trace(adjacnetNode, stack, codeStack, 1);
                                        codeStack.Pop();
                                        codeStack.Pop();
                                    }

                                }

                            }
                            else
                            {
                                foreach (var v in n.VariablesInScope)
                                {
                                    if (Util.ContainsScaryInput(v.VariableCode) && !Util.IsRedeemed(v.VariableCode))
                                    {
                                        if (i.InvocationCode.Contains(v.VariableName))
                                        {
                                            //
                                            // Case
                                            // string s = Request.QueryString[0];
                                            // fn(s);
                                            //

                                            try
                                            {

                                                Stack<Node> stack = new Stack<Node>();
                                                stack.Push(n);

                                                Node adjacnetNode = FindAdjacentNodeForInvocation(n, i);
                                                if (adjacnetNode != null && adjacnetNode.ParameterListContainsAString())
                                                {
                                                    for (int x = 0; x < i.InvocationParameters.Length; x++)
                                                    {
                                                        if (i.InvocationParameters[x].Contains(v.VariableName))
                                                        {
                                                            adjacnetNode.Parameters[x].IsTainted = true;
                                                        }
                                                    }

                                                    Stack<string> codeStack = new Stack<string>();
                                                    codeStack.Push(n.NodeCode);
                                                    codeStack.Push(i.InvocationCode);
                                                    Trace(adjacnetNode, stack, codeStack, 1);
                                                    codeStack.Pop();
                                                    codeStack.Pop();


                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }


            DeepEvaluate();


        }

        private void Trace(Node n, Stack<Node> stack, Stack<string> codeStack, int depth)
        {
            if (depth < Configuration.MaxRecursionDepth)
            {

                //
                // we might be assigning to a member variable who is tainted and used elsewhere.
                //
                foreach (var v in n.VariablesInScope)
                {
                    foreach (var p in n.Parameters)
                    {
                        if (p.IsTainted)
                        {
                            if (v.VariableCode.Contains(p.ParameterName))
                            {
                                // v.IsTainted = true;
                                Util.TaintVariable(n, v.VariableName);
                            }
                        }
                    }
                }


                foreach (var i in n.Invocations)
                {
                    foreach (var p in n.Parameters)
                    {
                        if (p.IsTainted)
                        {

                            if (i.InvocationCode.Contains(p.ParameterName))
                            {
                                //
                                // we are passing in a tainted parameter.
                                //

                                foreach (var method in this.template.GetMethods())
                                {

                                    if (i.InvocationCode.Contains(method))
                                    {
                                        stack.Push(n);
                                        codeStack.Push(i.InvocationCode);
                                        this.Vulnerabilities.Add(template.GetVulnerability(n.Filename, n.ClassName + "." + n.NodeName, i.InvocationCode, Util.CallStackToHtml(stack), Util.CodeStackToHtml(codeStack)));
                                        codeStack.Pop();
                                        stack.Pop();
                                    }
                                }

                                Node adjacnetNode = FindAdjacentNodeForInvocation(n, i);
                                if (adjacnetNode != null && adjacnetNode.ParameterListContainsAString())
                                {
                                    for (int x = 0; x < i.InvocationParameters.Length; x++)
                                    {
                                        if (i.InvocationParameters[x].Contains(p.ParameterName))
                                        {
                                            adjacnetNode.Parameters[x].IsTainted = true;
                                        }
                                    }

                                    codeStack.Push(i.InvocationCode);
                                    stack.Push(n);
                                    Trace(adjacnetNode, stack, codeStack, depth + 1);
                                    stack.Pop();
                                    codeStack.Pop();

                                }


                            }
                        }
                    }

                    //
                    // we need to test to see if tainted paramters are assigned to variables in scope.
                    // if so, we need to mark the variables astainted.
                    //

                    foreach (var currentNodeParameter in n.Parameters)
                    {
                        if (currentNodeParameter.IsTainted)
                        {
                            foreach (var currentInScopeVariable in n.VariablesInScope)
                            {
                                if (currentInScopeVariable.VariableCode.Contains(currentNodeParameter.ParameterName))
                                {
                                    // currentInScopeVariable.IsTainted = true;
                                    Util.TaintVariable(n, currentInScopeVariable.VariableName);
                                }
                            }
                        }
                    }


                    foreach (var v in n.VariablesInScope)
                    {

                        if (v.IsTainted)
                        {
                            if (i.InvocationCode.Contains(v.VariableName))
                            {
                                //
                                // we are passing in a tainted variable. 
                                //

                                foreach (var method in this.template.GetMethods())
                                {
                                    if (i.InvocationCode.Contains(method))
                                    {
                                        stack.Push(n);
                                        codeStack.Push(i.InvocationCode);
                                        this.Vulnerabilities.Add(template.GetVulnerability(n.Filename, n.ClassName + "." + n.NodeName, i.InvocationCode, Util.CallStackToHtml(stack), Util.CodeStackToHtml(codeStack)));
                                        codeStack.Pop();
                                        stack.Pop();
                                    }
                                }

                                Node adjacnetNode = FindAdjacentNodeForInvocation(n, i);
                                if (adjacnetNode != null && adjacnetNode.ParameterListContainsAString())
                                {
                                    for (int x = 0; x < i.InvocationParameters.Length; x++)
                                    {

                                        if (i.InvocationParameters[x].Contains(v.VariableName))
                                        {
                                            adjacnetNode.Parameters[x].IsTainted = true;
                                        }
                                    }

                                    codeStack.Push(i.InvocationCode);
                                    stack.Push(n);
                                    Trace(adjacnetNode, stack, codeStack, depth + 1);
                                    stack.Pop();
                                    codeStack.Pop();

                                }


                            }
                        }
                    }

                }

            }
            else
            {
                Configuration.debug("Our tracer has recursed too deep. Bailing out.");
            }
        }

    }
}
