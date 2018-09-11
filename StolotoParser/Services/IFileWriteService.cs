using StolotoParser_v2.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StolotoParser_v2.Services
{
    public interface IFileWriteService
    {
        void AppendResults(List<StolotoParseResult> stolotoParseResults);

        void InitialOldData();

        void Finalize();
    }
}
