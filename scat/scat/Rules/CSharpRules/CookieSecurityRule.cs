using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class CookieSecurityRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;


        public CookieSecurityRule(List<FileLoader> loaders, ITemplate template)
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

        public class CookieSecurityAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public CookieSecurityAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
     
                string raw = this.fileLoader.Raw;
                string[] lines = this.fileLoader.Lines;

                if (raw.Contains("HttpCookie"))
                {

                    for (int x = 0; x < lines.Length; x++)
                    {
                        string currentLine = lines[x];

                        //
                        // This case is: HttpCookie xxx = new HttpCookie("foo", "bar")
                        //               xxx.IsSecure = true;
                        //               xxx.HttpOnly = true; <- looking for these
                        //
                        if (currentLine.Contains("HttpCookie") && currentLine.Contains("=") && currentLine.Contains("new"))
                        {

                            string[] tokens = currentLine.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++)
                            {
                                if (tokens[tokenIndex].CompareTo("HttpCookie") == 0)
                                {
                                    string cookieVariableName = tokens[tokenIndex + 1];

                                    if (!raw.Contains(cookieVariableName + ".Secure") && !raw.Contains(cookieVariableName + ".HttpOnly"))
                                    {
                                        string message = string.Format("There appears to be an insecurely configured cookie: <b>{0}</b> which does not have .Secure or .HttpOnly configured.<br>{1}</br>", cookieVariableName, currentLine);
                                        this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, this.template.GetRuleName(), message));

                                    }
                                }
                            }



                        }
                        else if (currentLine.Contains("HttpCookie(") && currentLine.Contains("new") && !currentLine.Contains("="))
                        {
                            //
                            // This is the case of Response.Cookies.Add( new HttpCookie("foo", "bar"))
                            //

                            if (!currentLine.Contains("true")) // <- it does not appear to have a true flag, so setting to secure is unlikely.
                            {
                                string message = string.Format("There appears to be an insecurely configured cookie which does not have .Secure or .HttpOnly configured.<br>{0}</br>", currentLine);
                                this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, this.template.GetRuleName(), message));

                            }
                        }
                    }
                }

               
            }

        }

        public void Evaluate()
        {
            List<CookieSecurityAnalyzer> analyzers = new List<CookieSecurityAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new CookieSecurityAnalyzer(l, this.template));
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
