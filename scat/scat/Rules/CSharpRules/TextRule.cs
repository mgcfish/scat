using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class TextRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        private string[] ContainsAny
        {
            get;
            set;
        }

        public TextRule(List<FileLoader> loaders, ITemplate template, string [] containsAny)
        {
            this.Loaders = loaders;
            this.Vulnerabilities = new List<BaseVulnerability>();
            this.template = template;
            this.ContainsAny = containsAny;
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

        public class TextAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;
            private string[] containsAny;

            public TextAnalyzer(FileLoader l, ITemplate template, string [] containsAny)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
                this.containsAny = containsAny;
            }

            public void Analyze()
            {
                foreach (string e in this.containsAny)
                {
                    foreach (string line in this.fileLoader.Lines)
                    {
                        if (line.Contains(e))
                        {
                            this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, this.template.GetRuleName(), line + "<=>" + e));
                        }
                    }

                }

               
            }

        }

        public void Evaluate()
        {
            List<TextAnalyzer> analyzers = new List<TextAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new TextAnalyzer(l, this.template, this.ContainsAny));
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
