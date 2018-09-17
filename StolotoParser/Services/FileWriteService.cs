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

        private readonly Action<int, int> _setProgres;

        private readonly Action<string> _uppText;

        private readonly Action<Exception> _errorLogged;

        private readonly string _fileFormat;

        private readonly string _filePath;

        private readonly string _fileAllPath;

        private readonly ParsingSettings _parsingSettings;

        private readonly int _stopDrowAll;

        private readonly int _stopDrowCurrent;

        private List<StolotoParseResult> _oldDraws;

        private List<StolotoParseResult> _oldDrawsAll;

        private List<StolotoParseResult> _newDraws;

        private List<StolotoParseResult> _newDrawsAll;
      

        public FileWriteService(ParsingSettings parsingSettings, Element element, CancellationToken tocken,
            Action<int, int> setProgres, Action<string> uppText, int stopDrowCurrent, int stopDrowAll, Action<Exception> errorLogged)
        {
            this._parsingSettings = parsingSettings;

            this._tocken = tocken;

            this._element = element;

            this._setProgres = setProgres;

            this._uppText = uppText;

            this._stopDrowAll = stopDrowAll;

            this._stopDrowCurrent = stopDrowCurrent;

            this._errorLogged = errorLogged;

            this._fileFormat = "{0} _ {1}";

            this._filePath = Path.Combine(Application.StartupPath, element.FileName);

            this._fileAllPath = Path.Combine(Application.StartupPath, element.FileAllName);

            this._newDraws = new List<StolotoParseResult>();

            this._newDrawsAll = new List<StolotoParseResult>();
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

        private void Update(bool writeToAllFile)
        {
            try
            {

                if (!File.Exists(writeToAllFile ? this._fileAllPath : this._filePath)) return;

                using (StreamReader sr = new StreamReader(writeToAllFile ? this._fileAllPath : this._filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string firstRow = sr.ReadLine();

                        var datas = firstRow.Split('_');

                        var draw = Convert.ToInt32(datas[0].Trim());

                        var numbers = datas[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => {
                            var tmp = n.TrimStart('0');

                            return Convert.ToInt32(string.IsNullOrEmpty(tmp) ? "0" : tmp);
                        });

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
            }
            catch (Exception ex)
            {
                this._errorLogged(ex);
            }
        }

        void IFileWriteService.AppendResults(List<StolotoParseResult> stolotoParseResults)
        {
            if (this._parsingSettings.AddToAll)
            {
                this._newDrawsAll.AddRange(stolotoParseResults);
            }

            if (this._parsingSettings.AddToCurrent && stolotoParseResults.Any(d => d.Draw <= this._stopDrowCurrent))
            {
                stolotoParseResults = stolotoParseResults.Where(d => d.Draw > this._stopDrowCurrent).ToList();
            }

            this._newDraws.AddRange(stolotoParseResults);
        }

        void IFileWriteService.Finalize()
        {
            if (this._parsingSettings.AddToAll && !this._tocken.IsCancellationRequested)
            {
                var newAllData = this._oldDrawsAll != null ? this._oldDrawsAll.Concat(this._newDrawsAll) : this._newDrawsAll;

                this.WriteInfo(newAllData, 0, this._parsingSettings.AddToAll, true);
            }

            if (!this._tocken.IsCancellationRequested)
            {
                var newData = this._oldDraws != null ? this._oldDraws.Concat(this._newDraws) : this._newDraws;

                if (!this._parsingSettings.AddToCurrent)
                {
                    newData = newData.Take(this._stopDrowCurrent);
                }

                this.WriteInfo(newData, 0, false, this._parsingSettings.AddToCurrent);
            }
        }

        private void WriteInfo(IEnumerable<StolotoParseResult> stolotoParseResults, int fistProgresValue, bool toAllFile = false, bool setStopDraw = false)
        {
            try
            {
                var path = toAllFile ? this._fileAllPath : this._filePath;

                var stopDraw = setStopDraw ? toAllFile ? this._stopDrowAll : this._stopDrowCurrent : -1;

                var collection = stolotoParseResults.OrderBy(v => -v.Draw).Distinct(new BoxEqualityComparer());

                var maximum = collection.Count();

                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
                {
                    foreach (var stolotoParseResult in collection)
                    {
                        if (this._tocken.IsCancellationRequested) return;

                        if (!toAllFile)
                        {
                            fistProgresValue++;

                            this._setProgres(fistProgresValue, maximum);
                        }

                        if (stolotoParseResult.Numbers == null || stolotoParseResult.Numbers.Count == 0)
                        {
                            this._uppText(string.Format("{0} тираж ожидается", stolotoParseResult.Draw));

                            continue;
                        }

                        var info = string.Format(this._fileFormat, stolotoParseResult.Draw, string.Join(" ", stolotoParseResult.Numbers.Select(val => val.ToString("d2"))));

                        if (!toAllFile && this._stopDrowCurrent < stolotoParseResult.Draw)
                        {
                            this._uppText(info);
                        }

                        streamWriter.WriteLine(info);
                    }
                }
            }
            catch (Exception ex)
            {
                this._errorLogged(ex);
            }
        }
    }

    class BoxEqualityComparer : IEqualityComparer<StolotoParseResult>
    {
        public bool Equals(StolotoParseResult a, StolotoParseResult b)
        {
            return a.Draw == b.Draw;
        }

        public int GetHashCode(StolotoParseResult stolotoParseResult)
        {
            int hCode = stolotoParseResult.Draw;

            for (int index = 0; index < stolotoParseResult.Numbers.Count; index++)
            {
                hCode ^= stolotoParseResult.Numbers[index];
            }

            return hCode.GetHashCode();
        }
    }
}
