using StolotoParser_v2.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StolotoParser_v2.Services
{
    public interface IFileWriteService
    {
        void ClearFile(bool fileAll = false);

        void WriteStolotoResult(List<StolotoParseResult> stolotoParseResults, int fistProgresValue);

        void InitialOldData();

        void WritOldData();
    }
}
