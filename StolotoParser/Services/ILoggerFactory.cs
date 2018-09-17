using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StolotoParser_v2.Services
{
    public interface ILoggerFactory
    {
        void CloseProgramLogged();

        void ErrorLogged(Exception ex);

        void RunProgramLogged();
    }
}
