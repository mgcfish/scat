using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class IISConfigRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public IISConfigRule(List<FileLoader> loaders, ITemplate template)
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

        public class IISConfigAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public IISConfigAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            private GenericVulnerability GetVulnerability(string message)
            {
                return new GenericVulnerability(this.fileLoader.Filename, message, string.Empty, string.Empty, Severity.Low, VulnerabilityType.Misconfiguration);
            }

            public void Analyze()
            {
           
                IEnumerable<string> tags = Util.FindAllTags(this.fileLoader.Raw);

                foreach (string tag in tags)
                {
                    string lwrt = tag.ToLower();



                    if (lwrt.Contains("<pages"))
                    {
                        if (!lwrt.ToLower().Contains("enableviewstatemac=\"true\""))
                        {
                            string message = "Viewstate hashing does not appear to be enabled. Ensure enableViewStateMAC is set to true.";
                            this.vulns.Add(GetVulnerability( message));

                        }
                    }

                    if (lwrt.Contains("<pages"))
                    {
                        if (lwrt.ToLower().Contains("validaterequest=\"false\""))
                        {
                            string message = "pages validateRequest is set to false.  This is a vulnerabiity configuration..";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }

                    if (lwrt.Contains("<forms"))
                    {
                        if (lwrt.ToLower().Contains("slidingexpiration=\"true\""))
                        {
                            string message = "forms sliding expiration should be disabled.";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }

                    if (lwrt.Contains("<forms"))
                    {
                        if (lwrt.ToLower().Contains("requiressl=\"false\""))
                        {
                            string message = "Forms do NOT appear to require SSL.  That's not good.";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }


                    if (lwrt.Contains("<sessionstate"))
                    {
                        if (lwrt.ToLower().Contains("cookieless=\"useuri\""))
                        {
                            string message = "Avoid using cookieless session state. This could lead to session fixation.";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }

                    if (lwrt.Contains("<httpcookies"))
                    {
                        if (lwrt.ToLower().Contains("httponlycookies=\"false\""))
                        {
                            string message = "Cookies should all have .HttpOnly";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }


                    if (tag.Contains("<compilation"))
                    {
                        if (tag.ToLower().Contains("debug=\"true\""))
                        {
                            string message = "debug should be set to false";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }

                    if (tag.Contains("<customErrors"))
                    {
                        if (tag.ToLower().Contains("mode=\"off\""))
                        {
                            string message = "customErrors should be set to RemoteOnly";
                            this.vulns.Add(GetVulnerability(message));
                        }
                    }


                    if (tag.Contains("<trace"))
                    {
                        if (tag.ToLower().Contains("enabled=\"true\""))
                        {
                            if (tag.ToLower().Contains("localonly=\"false\""))
                            {
                                string message = "trace should not be enabled. If an hits trace.axd, bad things will happen.";
                                this.vulns.Add(GetVulnerability(message));
                            }
                        }
                    }

                }


            }

        }

        public void Evaluate()
        {
            List<IISConfigAnalyzer> analyzers = new List<IISConfigAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new IISConfigAnalyzer(l, this.template));
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
