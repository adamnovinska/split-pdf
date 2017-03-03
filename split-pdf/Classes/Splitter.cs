using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace split_pdf.Classes
{
    public static class Splitter
    {
        public static void SplitFiles(SplitOptions options)
        {
            var files = new List<string>();
            var verb = options.SplitVerb;

            files = !string.IsNullOrEmpty(verb.InputDirectory) ? Directory.GetFiles(verb.InputDirectory, "*.pdf").ToList() : new List<string>() {verb.InputFile};

            foreach (var file in files)
            {
                Console.Write($"Splitting file {file}...");
                SplitFile(file, verb.OutputDirectory, verb.PageSkip);
                Console.WriteLine($"done{Environment.NewLine}");
            }
        }

        private static void SplitFile(string fileName, string outputDirectory, int pageSkip)
        {
            using (var reader = new PdfReader(fileName))
            {
                var fileIndex = 1;

                for (var pageIndex = 1; pageIndex <= reader.NumberOfPages; pageIndex += pageSkip)
                {
                    var outputFileName = Path.Combine(outputDirectory, $"{Path.GetFileNameWithoutExtension(fileName)}_{fileIndex}.pdf");

                    if (!Directory.Exists(Path.GetDirectoryName(outputFileName)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
                    }

                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    var copyProvider = new PdfCopy(document, new FileStream(outputFileName, FileMode.OpenOrCreate));

                    document.Open();

                    for (var i = pageIndex; i < pageIndex + pageSkip; i++)
                    {
                        var page = copyProvider.GetImportedPage(reader, i);
                        copyProvider.AddPage(page);
                    }

                    document.Close();

                    fileIndex++;
                }
            }
        }
    }
}
