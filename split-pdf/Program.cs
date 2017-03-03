using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using split_pdf.Classes;

namespace split_pdf
{
    class Program
    {
        static void Main(string[] args)
        {
            var invokedVerb = string.Empty;
            var invokedVerbInstance = new object();

            var options = new SplitOptions();

            if (!CommandLine.Parser.Default.ParseArguments(args, options, (verb, subOptions) =>
            {
                invokedVerb = verb;
                invokedVerbInstance = subOptions;
            }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            switch (invokedVerb)
            {
                case "split":
                    var splitOptions = (SplitOptions) invokedVerbInstance;
                    Splitter.SplitFiles(options);
                    break;
            }
        }
    }
}
