using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class HardCodedPasswordRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;


        public HardCodedPasswordRule(List<FileLoader> loaders, ITemplate template)
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

        public class HardCodedPasswordAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public HardCodedPasswordAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
  
                string [] keywords = {"secret", "password", "passwd", "magic", "backdoor"};

                foreach (var n in this.fileLoader.SyntaxAnalyzer.Nodes)
                {
                    foreach (var v in n.VariablesInScope)
                    {
                        foreach(var k in keywords)
                        {
                            if (v.VariableName.ToLower().Contains(k))
                            {
                             
                                if (v.VariableCode.Contains("=") && v.VariableCode.Contains("\"") && !v.VariableCode.Contains("[") && !v.VariableCode.Contains("\"\"") && !v.VariableCode.Contains(".Empty"))
                                {
                                    string message = "<table>";
                                    message += string.Format("  <tr>  <td>Variable Name</td> <td>{0}</td>   </tr>  ", v.VariableName);
                                    message += string.Format("  <tr>  <td>Variable Code</td> <td>{0}</td>   </tr>  ", v.VariableCode);
                                    message += string.Format("  <tr>  <td>Keyword</td> <td>{0}</td>   </tr>  ", k);
                                    message += "</table>";
                                    this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, this.template.GetRuleName(), message));

                                }
                            }
                        }
                    }
                }

            }

        }

        public void Evaluate()
        {
            List<HardCodedPasswordAnalyzer> analyzers = new List<HardCodedPasswordAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new HardCodedPasswordAnalyzer(l, this.template));
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
