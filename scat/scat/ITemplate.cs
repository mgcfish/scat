using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public interface ITemplate
    {
        string GetRuleName(); // Command Injection Rule
        List<string> GetMethods(); // Process.Start
        BaseVulnerability GetVulnerability(string filename, string location, string code);
        BaseVulnerability GetVulnerability(string filename, string location, string code, string callStackHtml, string codeStackHtml);
    }
}
