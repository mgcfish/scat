using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class InlineAspxRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public InlineAspxRule(List<FileLoader> loaders, ITemplate template)
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

        public class InlineAspxAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public InlineAspxAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string raw = this.fileLoader.Raw;

                IEnumerable<string> s = Util.FindAllInlineCodes(raw);


                foreach (var c in s)
                {
                    if (!c.Contains("@ Page"))
                    {
                        string rawCode = c.Replace("<%", string.Empty).Replace("%>", string.Empty);

                        string[] lines = rawCode.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        // Util.ContainsScaryInput

                        foreach (string line in lines)
                        {
                            if (Util.ContainsScaryInput(line) && Util.ContainsScaryMethod(line))
                            {
                                if (Util.ScaryMethodBeforeEvilInput(line))
                                {
                                    this.vulns.Add(new GenericVulnerability(this.fileLoader.Filename, "Unsafe use of API", this.fileLoader.Filename, line, Severity.High, VulnerabilityType.UnspecifiedBadThing));
                                }
                            }
                        }

                        List<Variable> taintedVariables = Util.EnumerateTaintedVariables(rawCode);

                        foreach (var taintedVariable in taintedVariables)
                        {
                         //   Configuration.debug("TAINTED VARIABLE: " + taintedVariable.VariableName);
                            foreach (var line in lines)
                            {
                                if (Util.ContainsScaryMethod(line) && line.Contains(taintedVariable.VariableName))
                                {
                                    this.vulns.Add(new GenericVulnerability(this.fileLoader.Filename, "Something Bad", ".", taintedVariable.VariableCode + "<--->" + line, Severity.High, VulnerabilityType.UnspecifiedBadThing));
                                }
                            }
                        }
                
                    }
                    
                }
            }

        }

        public void Evaluate()
        {
            List<InlineAspxAnalyzer> analyzers = new List<InlineAspxAnalyzer>();

            foreach (var l in this.Loaders)
            {
                if (l.Filename.ToLower().EndsWith(".aspx"))
                {
                    analyzers.Add(new InlineAspxAnalyzer(l, this.template));
                }
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
