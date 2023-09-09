using System;
using System.Collections.Generic;
using System.Linq;

namespace SWIFT.Entities
{
    public class TextHeader
    {
        private Dictionary<string, string> textTags = new Dictionary<string, string>
        {
            { "20", string.Empty },
            { "23B", string.Empty },
            { "32A", string.Empty },
            { "33B", string.Empty },
            { "36", string.Empty },
            { "50K", string.Empty },
            { "52a", string.Empty },
            { "56A", string.Empty },
            { "57A", string.Empty },
            { "59", string.Empty },
            { "70", string.Empty },
            { "71A", string.Empty },
        };

        private List<string> textColumns = new List<string>();

        public TextHeader(string message)
        {
            textColumns = message.Replace(",", "").Split("\r\n").ToList();
            PopulateTextTagsValues(textColumns);
        }

        private void PopulateTextTagsValues(List<string> textColumns)
        {
            foreach (var textColumn in textColumns)
            {
                if (textColumn.Contains(":"))
                {
                    var tag = textColumn.Split(":")[1].Split(":")[0];
                    var value = textColumn.Split(":").Last();
                    if (textTags.ContainsKey(tag) && textTags[tag] == "")
                    {
                        textTags[tag] = value;
                    }
                }
            }
            ;
        }
    }
}
