using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public static class PhpUtil
    {
        public static string[] PhpUserInput =
        {
            "$_COOKIE",
            "$_GET",
            "$_POST",
            "$_REQUEST",
            "$_SERVER"
        };

        public static List<Tuple<string,string>> EnumerateTaintedVariables(string[] lines)
        {
            List<Tuple<string,string>> retval = new List<Tuple<string,string>>();

            foreach (var line in lines)
            {
                if (PhpUtil.ContainsUserInput(line) && line.Contains("=") && !line.Contains("==") && !line.Contains("!="))
                {
                    string[] tokens = line.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length == 2)
                    {
                        string lh = tokens[0];
                        string rh = tokens[1];

                        if (PhpUtil.ContainsUserInput(rh))
                        {
                            string tlh = lh.Trim();
                            if (tlh.StartsWith("$"))
                            {
                                string taintedVariableName = tlh;
                                retval.Add(new Tuple<string, string>(taintedVariableName, line));

                            }
                        }
                    }
                }
            }

            return retval;
        }

        public static bool ContainsUserInput(string code)
        {
            bool retval = false;

            foreach(var p in PhpUserInput)
            {
                if(code.Contains(p))
                {
                    retval = true;
                }
            }

            return retval;
        }
    }
}
