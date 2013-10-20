using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class BasicCsrfRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public BasicCsrfRule(List<FileLoader> loaders, ITemplate template)
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

        public class BasicCsrfAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public BasicCsrfAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string lwrFilename = this.fileLoader.Filename.ToLower();

                //
                // This is an aspx
                //
                if (lwrFilename.EndsWith(".aspx.cs"))
                {
                    //
                    // it contains a verb.
                    //
                    if (Util.ContainsVerb(lwrFilename))
                    {
                        //
                        // Check to see if there is a parameter being consumed.
                        //
                        if (this.fileLoader.Raw.Contains("Request.QueryString") 
                            || this.fileLoader.Raw.Contains("Request.Form")
                            || this.fileLoader.Raw.Contains("Request.Params"))
                        {
                            string lwrRaw = this.fileLoader.Raw.ToLower();

                            //
                            // Are they at least trying to mitigate this?
                            //
                            if (!lwrRaw.Contains("csrf") && !lwrRaw.Contains("token"))
                            {
                                this.vulns.Add(new GenericVulnerability(this.fileLoader.Filename, "Potential CSRF vulnerability", fileLoader.Filename, "The CSRF rule looks for .aspx.cs files where the filename contains a verb. It then tests for basic CSRF protections (eg, is there a 'csrf token'). It also checks for user input (eg Request.QueryString, etc).", Severity.Medium, VulnerabilityType.Csrf));
                            }
                        }
                    }
                }

            }

        }

        public void Evaluate()
        {
            List<BasicCsrfAnalyzer> analyzers = new List<BasicCsrfAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new BasicCsrfAnalyzer(l, this.template));
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
