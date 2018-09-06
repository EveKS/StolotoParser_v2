using StolotoParser_v2.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StolotoParser_v2.Services
{
    public class FileWriteService : IFileWriteService
    {
        void IFileWriteService.WriteStolotoResult(List<StolotoParseResult> stolotoParseResults, Element element)
        {
            var format = "{0} _ {1}";

            var filePath = Path.Combine(Application.StartupPath, element.FileName);

            this.WriteInfo(stolotoParseResults, format, filePath);
        }

        private void WriteInfo(List<StolotoParseResult> stolotoParseResults, string format, string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
            {
                foreach (var stolotoParseResult in stolotoParseResults)
                {
                    if (stolotoParseResult.Numbers == null || stolotoParseResult.Numbers.Count == 0) continue;

                    streamWriter.WriteLine(string.Format(format, stolotoParseResult.Draw, string.Join(" ", stolotoParseResult.Numbers.Select(val => val.ToString("d2")))));
                }
            }
        }

        void IFileWriteService.ClearFile(Element element)
        {
            var filePath = Path.Combine(Application.StartupPath, element.FileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) { }
        }
    }
}
