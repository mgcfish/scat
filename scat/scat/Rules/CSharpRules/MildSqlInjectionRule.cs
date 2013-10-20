using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class MildSqlInjectionRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public MildSqlInjectionRule(List<FileLoader> loaders, ITemplate template)
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

        public class MildSqlInjectionAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public MildSqlInjectionAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                //     this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, this.template.GetRuleName(), line + "<=>" + e));

                foreach (var n in this.fileLoader.SyntaxAnalyzer.Nodes)
                {
                    foreach (var v in n.VariablesInScope)
                    {
                        if (v.VariableCode.Contains("SqlCommand"))
                        {
                        //    Configuration.debug(v.VariableCode);

                            if (v.VariableCode.Contains("SqlCommand") && v.VariableCode.Contains("new"))
                            {
                                if (v.VariableCode.Contains("\"") && v.VariableCode.Contains("+"))
                                {
                                    GenericVulnerability gv = new GenericVulnerability(this.fileLoader.Filename, "Potential SqlInjection", "Avoid using dynamic sql. Use parameterized queries wherever possible.", v.VariableCode, Severity.Informational, VulnerabilityType.SqlInjection);
                                    this.vulns.Add(gv);

                                }
                            }

                        }
                    }
                }
               
            }

        }

        public void Evaluate()
        {
            List<MildSqlInjectionAnalyzer> analyzers = new List<MildSqlInjectionAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new MildSqlInjectionAnalyzer(l, this.template));
            }

            Parallel.ForEach(analyzers, a =>
            {
                a.Analyze();
            });

            //
            // aggregate the vulnerabilities.
            //
            foreach(var a in analyzers)
            {
                if(a.vulns.Count > 0)
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
