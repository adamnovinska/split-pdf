using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace split_pdf.Classes
{
    public abstract class Options
    {
        [Option('i', Required = false, HelpText = "Input .pdf file name")]
        public string InputFile { get; set; }

        [Option('d', Required = false, HelpText = "Directory to process")]
        public string InputDirectory { get; set; }

        [Option('o', Required = true, HelpText = "Output directory")]
        public string OutputDirectory { get; set; }

        [Option('v', Required = false, HelpText = "Print details during operation")]
        public bool Verbose { get; set; }

        [VerbOption("split", HelpText = "Split .pdf file into one or more files")]
        public SplitOptions SplitVerb { get; set; }
    }

    public class SplitOptions : Options
    {
        [Option('p', Required = false, HelpText = "Split every P pages into individual output files")]
        public int PageSkip { get; set; }
    }
}
