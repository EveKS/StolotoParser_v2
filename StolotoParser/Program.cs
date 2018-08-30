using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StolotoParser_v2.Presenters;
using StolotoParser_v2.Services;

namespace StolotoParser_v2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();

            IJsonService jsonService = new JsonService();

            IHtmlService htmlService = new HtmlService();

            IHtmlParser htmlParser = new HtmlParser();

            IFileWriteService fileWriteService = new FileWriteService();

            //FileWriteService : IFileWriteService

            var presenter = new MainPresenter(form, jsonService, htmlService, htmlParser, fileWriteService);

            Application.Run(form);
        }
    }
}
