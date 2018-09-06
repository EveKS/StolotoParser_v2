using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StolotoParser_v2.Models;

namespace StolotoParser_v2.Services
{
    public interface IHtmlParser
    {
        List<StolotoParseResult> ParseHtml(string content, bool getExtra);
    }
}
