``` csharp
//用法示例
CommandArgs commandArg = CommandLine.Parse(args);
List<string> lparams = commandArg.Params;

Dictionary<string, string> argPairs = commandArg.ArgPairs;
```

``` csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    //---------------------------------------------------------------------
    /// <summary>
    /// Contains the parsed command line arguments. This consists of two
    /// lists, one of argument pairs, and one of stand-alone arguments.
    /// </summary>
    public class CommandArgs
    {
        //---------------------------------------------------------------------
        /// <summary>
        /// Returns the dictionary of argument/value pairs.
        /// </summary>
        public Dictionary<string, string> ArgPairs
        {
            get { return mArgPairs; }
        }

        private Dictionary<string, string> mArgPairs = new Dictionary<string, string>();

        //---------------------------------------------------------------------
        /// <summary>
        /// Returns the list of stand-alone parameters.
        /// </summary>
        public List<string> Params
        {
            get { return mParams; }
        }

        private List<string> mParams = new List<string>();
    }

    //---------------------------------------------------------------------
    /// <summary>
    /// Implements command line parsing
    /// </summary>
    public class CommandLine
    {
        //---------------------------------------------------------------------
        /// <summary>
        /// Parses the passed command line arguments and returns the result
        /// in a CommandArgs object.
        /// </summary>
        /// The command line is assumed to be in the format:
        ///
        /// CMD [param] [[-|--|\]&lt;arg&gt;[[=]&lt;value&gt;]] [param]
        ///
        /// Basically, stand-alone parameters can appear anywhere on the command line.
        /// Arguments are defined as key/value pairs. The argument key must begin
        /// with a '-', '--', or '\'. Between the argument and the value must be at
        /// least one space or a single '='. Extra spaces are ignored. Arguments MAY
        /// be followed by a value or, if no value supplied, the string 'true' is used.
        /// You must enclose argument values in quotes if they contain a space, otherwise
        /// they will not parse correctly.
        ///
        /// Example command lines are:
        ///
        /// cmd first -o outfile.txt --compile second \errors=errors.txt third fourth --test = "the value" fifth
        ///
        /// <param name="args">array of command line arguments</param>
        /// <returns>CommandArgs object containing the parsed command line</returns>
        public static CommandArgs Parse(string[] args)
        {
            char[] kEqual = new char[] { '=' };
            char[] kArgStart = new char[] { '-', '\\' };
            CommandArgs ca = new CommandArgs();
            int ii = -1;
            string token = NextToken(args, ref ii);
            while (token != null)
            {
                if (IsArg(token))
                {
                    string arg = token.TrimStart(kArgStart).TrimEnd(kEqual);
                    string value = null;
                    if (arg.Contains("="))
                    {
                        // arg was specified with an '=' sign, so we need
                        // to split the string into the arg and value, but only
                        // if there is no space between the '=' and the arg and value.
                        string[] r = arg.Split(kEqual, 2);
                        if (r.Length == 2 && r[1] != string.Empty)
                        {
                            arg = r[0];
                            value = r[1];
                        }
                    }
                    while (value == null)
                    {
                        string next = NextToken(args, ref ii);
                        if (next != null)
                        {
                            if (IsArg(next))
                            {
                                // push the token back onto the stack so
                                // it gets picked up on next pass as an Arg
                                ii--;
                                value = "true";
                            }
                            else if (next != "=")
                            {
                                // save the value (trimming any '=' from the start)
                                value = next.TrimStart(kEqual);
                            }
                        }
                    }
                    // save the pair
                    ca.ArgPairs.Add(arg, value);
                }
                else if (token != string.Empty)
                {
                    // this is a stand-alone parameter.
                    ca.Params.Add(token);
                }
                token = NextToken(args, ref ii);
            }
            return ca;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Returns True if the passed string is an argument (starts with
        /// '-', '--'.)
        /// </summary>
        /// <param name="arg">the string token to test</param>
        /// <returns>true if the passed string is an argument, else false if a parameter</returns>
        private static bool IsArg(string arg)
        {
            return (arg.StartsWith("-"));
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Returns the next string token in the argument list
        /// </summary>
        /// <param name="args">list of string tokens</param>
        /// <param name="ii">index of the current token in the array</param>
        /// <returns>the next string token, or null if no more tokens in array</returns>
        private static string NextToken(string[] args, ref int ii)
        {
            ii++; // move to next token
            while (ii < args.Length)
            {
                string cur = args[ii].Trim();
                if (cur != string.Empty)
                {
                    // found valid token
                    return cur;
                }
                ii++;
            }
            // failed to get another token
            return null;
        }
    }
}
```
