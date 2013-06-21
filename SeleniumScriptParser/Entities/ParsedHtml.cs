using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumScriptParser.Entities
{
    /// <summary>
    /// A parsed HTML Result.
    /// </summary>
    public class ParsedHtml
    {
        /// <summary>
        /// The commands that where parsed.
        /// </summary>
        public List<BaseCommand> Commands { get; set; }

        /// <summary>
        /// The base url of the test.
        /// </summary>
        public string SeleniumBase { get; set; }
    }
}
