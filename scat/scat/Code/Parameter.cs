using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public class Parameter
    {
        public string ParameterName
        {
            get;
            set;
        }

        public string ParameterCode
        {
            get;
            set;
        }

        public bool IsTainted
        {
            get;
            set;
        }

        public Parameter(string name, string code)
        {
            this.ParameterName = name;
            this.ParameterCode = code;
            this.IsTainted = false;
        }
    }
}
