using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StolotoParser_v2.Models
{
    public class ParsingSettings
    {
        public bool AddToCurrent { get; set; }

        public bool AddToAll { get; set; }

        public bool ParsingExtraNumbers { get; set; }
    }
}
