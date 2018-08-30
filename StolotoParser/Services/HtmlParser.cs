using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery;
using System.Text.RegularExpressions;
using StolotoParser_v2.Models;

namespace StolotoParser_v2.Services
{
    public class HtmlParser : IHtmlParser
    {
        List<StolotoParseResult> IHtmlParser.ParseHtml(string content)
        {
            try
            {
                CQ cq = CQ.Create(content);

                var container = cq[".month .elem"];

                var result = container.Select(el =>
                {
                    var html = el.InnerHTML;

                    CQ cq2 = CQ.Create(html);

                    //var prize = cq2[".main .prize.with_jackpot"]; // .FirstChild(super);
                    var draw = Regex.Replace(cq2[".main .draw"].Text(), @"[\W\D]+", string.Empty).Trim();
                    var numbers = cq2[".main .numbers .container.cleared:not(.sorted):not(.sub) b:not(.extra)"];
                    var intNumbers = numbers.Select(num => Regex.Replace(num.InnerText, @"[\W\D]+", string.Empty).Trim()).Select(int.Parse);

                    return new StolotoParseResult()
                    {
                        Draw = Convert.ToInt32(draw),
                        Numbers = intNumbers.ToList()
                    };
                });

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка в процессе парсинга данных", ex);
            }
        }

    }
}
