using System;
using System.Collections.Generic;
using System.Linq;

namespace SWIFT.Entities
{
    public class TextHeader
    {
        private string name;
        private string amount;
        private string operationType;
        private string currency;
        private string account;
        private List<string> textColumns = new List<string>();

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

        public string Name => this.name;
        public string Amount => this.amount;
        public string OperationType => this.operationType;
        public string Currency => this.currency;
        public string Account => this.account;

        public TextHeader(string message)
        {
            textColumns = message.Replace(",", "").Split("\r\n").ToList();
            PopulateTextTagsValues(textColumns);
        }

        public Dictionary<string, string> TextTags => this.textTags;

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
                        if(tag == "23B")
                        {
                            this.operationType = value.Split(":").Last();
                        }

                        if (tag == "32A")
                        {
                            this.currency = value.Substring(6, 3);
                            this.amount = value.Substring(9);
                        }

                        if (tag == "59" || tag == "59A")
                        {
                            this.name = value.Split(":").Last();
                        }
                    }
                }
            }
            ;
        }
    }
}
