using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public class Invocation
    {
        public string InvocationName
        {
            get;
            set;
        }

        public string InvocationCode
        {
            get;
            set;
        }

        public string[] InvocationParameters
        {
            get;
            set;
        }

        public string InvocationParameterList
        {
            get;
            set;
        }

        public bool IsVariableInitializer
        {
            get;
            set;
        }

        public Invocation(string name, string code, string[] parameters)
        {
            this.InvocationName = name;
            this.InvocationCode = code;
            this.InvocationParameters = parameters;
            this.InvocationParameterList = GetParameterListForInvocation(this.InvocationCode);
            this.IsVariableInitializer = false;
        }

        public Invocation(string name, string code, string [] parameters, bool isVariableInitializer)
        {
            this.InvocationName = name;
            this.InvocationCode = code;
            this.InvocationParameters = parameters;
            this.InvocationParameterList = GetParameterListForInvocation(this.InvocationCode);
            this.IsVariableInitializer = isVariableInitializer;
        }

        private string GetParameterListForInvocation(string invocationCode)
        {
            string retval = invocationCode.Substring(invocationCode.IndexOf('('), invocationCode.LastIndexOf(')') - invocationCode.IndexOf('(')) + ")";

            return retval;
        }
    }
}
