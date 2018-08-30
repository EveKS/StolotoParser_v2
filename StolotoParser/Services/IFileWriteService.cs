using StolotoParser_v2.Models;
using System.Collections.Generic;

namespace StolotoParser_v2.Services
{
    public interface IFileWriteService
    {
        void ClearFile(Element element);

        void WriteStolotoResult(List<StolotoParseResult> stolotoParseResults, Element element);
    }
}
