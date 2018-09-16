using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO.Compression;
using System.IO;

namespace StolotoParser_v2.Services
{
    public class HtmlService : IHtmlService
    {
        private void SetHeaders(WebClient client)
        {
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        }

        string IHtmlService.GetStringContent(string url)
        {
            try
            {

                using (WebClient client = new WebClient())
                {
                    this.SetHeaders(client);

                    return client.UploadString(url, "");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
