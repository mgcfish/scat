using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class BasicRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public BasicRule(List<FileLoader> loaders, ITemplate template)
        {
            this.Loaders = loaders;
            this.Vulnerabilities = new List<BaseVulnerability>();
            this.template = template;
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

        public class BasicAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public BasicAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                foreach (var n in this.fileLoader.SyntaxAnalyzer.Nodes)
                {

        

                    //
                    // Process.Start( Request.QueryString[0]);
                    //
                    foreach (var i in n.Invocations)
                    {

                        if (i.InvocationCode.Contains("SqlCommand"))
                        {
                            Configuration.debug("*" + n.ClassName + "." + n.NodeName + " -> " + i.InvocationCode);
                        }

                        if (Util.ContainsScaryInput(i.InvocationCode))  // does it contain Request.QueryString
                        {
                            foreach (var method in this.template.GetMethods())
                            {
                                if (i.InvocationCode.Contains(method)) // does it contain a scary method.
                                {
                                    if (!Util.IsRedeemed(i.InvocationCode))
                                    {
                                        this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, n.ClassName + "." + n.NodeName, i.InvocationCode));
                                    }
                                }
                            }
                        }

                    }

                    //
                    // string s = Request.QueryString[0];
                    // Process.Start(s);
                    //
                    //

                    foreach (var i in n.Invocations)
                    {
                        foreach (var v in n.VariablesInScope)
                        {
                            if (Util.ContainsScaryInput(v.VariableCode) && !Util.IsRedeemed(v.VariableCode))
                            {
                                foreach (var method in this.template.GetMethods())
                                {
                                    if (i.InvocationParameterList.Contains(v.VariableName) && i.InvocationCode.Contains(method))
                                    {
                                        this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, n.ClassName + "." + n.NodeName + " Var: " + v.VariableName, i.InvocationCode + "<===>" + v.VariableCode));
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public void Evaluate()
        {
            List<BasicAnalyzer> analyzers = new List<BasicAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new BasicAnalyzer(l, this.template));
            }

            Parallel.ForEach(analyzers, a =>
            {
                a.Analyze();
            });

            //
            // aggregate the vulnerabilities.
            //
            foreach (var a in analyzers)
            {
                if (a.vulns.Count > 0)
                {
                    foreach (var v in a.vulns)
                    {
                        this.Vulnerabilities.Add(v);
                    }
                }
            }

        }
    }
}
