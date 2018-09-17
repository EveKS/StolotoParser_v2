using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StolotoParser_v2.Services
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly string _path;

        public LoggerFactory()
        {
            this._path = Application.StartupPath + "\\logs.txt";
        }

        #region ILoggerFactory
        void ILoggerFactory.RunProgramLogged()
        {
            var appendInfo = string.Format("{0:dd.MM.yy hh:mm:ss}\tprogram run", DateTime.Now);

            this.WriteInfo(appendInfo);
        }

        void ILoggerFactory.CloseProgramLogged()
        {
            var appendInfo = string.Format("{0:dd.MM.yy hh:mm:ss}\tprogram close", DateTime.Now);

            this.WriteInfo(appendInfo);
        }

        void ILoggerFactory.ErrorLogged(Exception ex)
        {
            var appendInfo = string.Format("{0:dd.MM.yy hh:mm:ss}\texception", DateTime.Now);

            string[] exDetail =
            {
                string.Format("Member name:\t{0}", ex.TargetSite),

                string.Format("Class defining member:\t{0}", ex.TargetSite.DeclaringType),

                string.Format("Member Type:\t{0}", ex.TargetSite.MemberType),

                string.Format("Message:\t{0}", ex.Message),

                string.Format("Source:\t{0}", ex.Source),

                string.Format("Help Link:\t{0}", ex.HelpLink),

                string.Format("Stack:\t{0}", ex.StackTrace)
            };

            WriteInfo(appendInfo, exDetail);
        }
        #endregion

        private void WriteInfo(string info)
        {
            WriteInfo(info, null);
        }

        private Task WriteInfo(string info, params string[] details)
        {
            return Task.Factory.StartNew(() =>
            {
                if (!string.IsNullOrEmpty(info) && !string.IsNullOrEmpty(this._path))
                {
                    using (FileStream fileStream = new FileStream(this._path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
                    {
                        streamWriter.WriteLine(info);

                        if (details != null)
                        {
                            for (int i = 0; i < details.Length; i++)
                            {
                                streamWriter.WriteLine(details[i]);
                            }
                        }
                    }
                }
            });
        }
    }
}
