using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public interface IRule
    {
        string GetRuleName();
        IEnumerable<BaseVulnerability> GetVulnerabilities();
        void Evaluate();
    }
}
