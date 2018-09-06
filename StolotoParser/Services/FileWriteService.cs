using StolotoParser_v2.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System;

namespace StolotoParser_v2.Services
{
    public class FileWriteService : IFileWriteService
    {
        void IFileWriteService.WriteStolotoResult(List<StolotoParseResult> stolotoParseResults, Element element, CancellationToken tocken, Action<int> setProgres, Action<string> uppText, int fistProgresValue)
        {
            var format = "{0} _ {1}";

            var filePath = Path.Combine(Application.StartupPath, element.FileName);

            this.WriteInfo(stolotoParseResults, format, filePath, tocken, setProgres, uppText, fistProgresValue);
        }

        private void WriteInfo(List<StolotoParseResult> stolotoParseResults, string format, string path, CancellationToken tocken, Action<int> setProgres, Action<string> uppText, int fistProgresValue)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
            {
                foreach (var stolotoParseResult in stolotoParseResults)
                {
                    if (tocken.IsCancellationRequested) return;

                    fistProgresValue++;

                    setProgres(fistProgresValue);

                    if (stolotoParseResult.Numbers == null || stolotoParseResult.Numbers.Count == 0) continue;

                    var info = string.Format(format, stolotoParseResult.Draw, string.Join(" ", stolotoParseResult.Numbers.Select(val => val.ToString("d2"))));

                    uppText(info);

                    streamWriter.WriteLine(info);
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
