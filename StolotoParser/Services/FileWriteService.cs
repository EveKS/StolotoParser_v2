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
        private readonly CancellationToken _tocken;

        private readonly Element _element;

        private readonly Action<int> _setProgres;

        private readonly Action<string> _uppText;

        private readonly string _fileFormat;

        private readonly string _filePath;

        private readonly string _fileAllPath;

        private readonly ParsingSettings _parsingSettings;

        private readonly int _stopDrowAll;

        private readonly int _stopDrowCurrent;

        private List<StolotoParseResult> _oldDraws;

        private List<StolotoParseResult> _oldDrawsAll;

        public FileWriteService(ParsingSettings parsingSettings, Element element, CancellationToken tocken, Action<int> setProgres, Action<string> uppText, int stopDrowCurrent, int stopDrowAll)
        {
            this._parsingSettings = parsingSettings;

            this._tocken = tocken;

            this._element = element;

            this._setProgres = setProgres;

            this._uppText = uppText;

            this._stopDrowAll = stopDrowAll;

            this._stopDrowCurrent = stopDrowCurrent;

            this._fileFormat = "{0} _ {1}";

            this._filePath = Path.Combine(Application.StartupPath, element.FileName);

            this._fileAllPath = Path.Combine(Application.StartupPath, element.FileAllName);
        }

        public void InitialOldData()
        {
            if (this._parsingSettings.AddToCurrent)
            {
                this._oldDraws = new List<StolotoParseResult>();

                this.Update(false);
            }
            if (this._parsingSettings.AddToAll)
            {
                this._oldDrawsAll = new List<StolotoParseResult>();

                this.Update(true);
            }
        }

        public void WritOldData()
        {
            if (this._parsingSettings.AddToAll)
            {
                if (this._oldDrawsAll.Count > 0) this.WriteInfo(this._oldDrawsAll, 0, this._parsingSettings.AddToAll, false);
            }

            if (this._parsingSettings.AddToCurrent)
            {
                if (this._oldDraws.Count > 0) this.WriteInfo(this._oldDraws, 0, false, false);
            }
        }

        private void Update(bool writeToAllFile)
        {
            using (StreamReader sr = new StreamReader(writeToAllFile ? this._fileAllPath : this._filePath))
            {
                while (!sr.EndOfStream)
                {
                    string firstRow = sr.ReadLine();

                    var datas = firstRow.Split('_');

                    var draw = Convert.ToInt32(datas[0].Trim());

                    var numbers = datas[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n.TrimStart('0')));

                    if (writeToAllFile)
                    {
                        this._oldDrawsAll.Add(new StolotoParseResult() { Draw = draw, Numbers = numbers.ToList() });
                    }
                    else
                    {
                        this._oldDraws.Add(new StolotoParseResult() { Draw = draw, Numbers = numbers.ToList() });
                    }
                }
            }

            this.ClearFile(writeToAllFile);
        }

        void IFileWriteService.WriteStolotoResult(List<StolotoParseResult> stolotoParseResults, int fistProgresValue)
        {
            if (this._parsingSettings.AddToAll)
            {
                this.WriteInfo(stolotoParseResults, fistProgresValue, this._parsingSettings.AddToAll);
            }

            this.WriteInfo(stolotoParseResults, fistProgresValue);
        }

        private void WriteInfo(List<StolotoParseResult> stolotoParseResults, int fistProgresValue, bool toAllFile = false, bool setStopDraw = true)
        {
            var path = toAllFile ? this._fileAllPath : this._filePath;

            var stopDraw = setStopDraw ? toAllFile ? this._stopDrowAll : this._stopDrowCurrent : -1;

            using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
            {
                foreach (var stolotoParseResult in stolotoParseResults)
                {
                    if (this._tocken.IsCancellationRequested || stolotoParseResult.Draw <= stopDraw) return;

                    if (!toAllFile)
                    {
                        fistProgresValue++;

                        this._setProgres(fistProgresValue);
                    }

                    if (stolotoParseResult.Numbers == null || stolotoParseResult.Numbers.Count == 0) continue;

                    var info = string.Format(this._fileFormat, stolotoParseResult.Draw, string.Join(" ", stolotoParseResult.Numbers.Select(val => val.ToString("d2"))));

                    if (!toAllFile)
                    {
                        this._uppText(info);
                    }

                    streamWriter.WriteLine(info);
                }
            }
        }

        public void ClearFile(bool fileAll)
        {
            using (FileStream fileStream = new FileStream(fileAll ? this._fileAllPath : this._filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) { }
        }
    }
}
