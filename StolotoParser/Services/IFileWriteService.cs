using StolotoParser_v2.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StolotoParser_v2.Services
{
    public interface IFileWriteService
    {
        void ClearFile(Element element);

        void WriteStolotoResult(List<StolotoParseResult> stolotoParseResults, Element element, CancellationToken tocken, Action<int> setProgres, Action<string> uppText, int fistProgresValue);
    }
}
