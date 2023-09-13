using System.Collections.Generic;
using System.Linq;

namespace SWIFT.Entities
{
    public class TextHeader
    {
        private string name;
        private string amount;
        private string operationType;
        private string senderReference;
        private string currency;
        private string account;
        private string detailsOfCharges;
        private string exchangeRate;
        private string orderingCustomer;
        private string orderingInstitution;
        private string intermediaryInstitution;
        private string remittanceInformation;
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
        public string SenderReference => this.senderReference;
        public string DetailsOfCharges => this.detailsOfCharges;
        public string ExchangeRate => this.exchangeRate;
        public string OrderingCustomer => this.orderingCustomer;
        public string OrderingInstitution => this.orderingInstitution;
        public string IntermediaryInstitution => this.intermediaryInstitution;
        public string RemittanceInformation => this.remittanceInformation;

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
                        switch (tag)
                        {
                            case "20":
                                this.senderReference = value.Split(":").LastOrDefault();
                                break;
                            case "23B":
                                this.operationType = value.Split(":").LastOrDefault();
                                break;
                            case "32A":
                                this.currency = value.Substring(6, 3);
                                this.amount = value.Substring(9);
                                break;
                            case "36":
                                this.exchangeRate = value.Split(":").LastOrDefault();
                                break;
                            case "50K":
                                this.orderingInstitution = value.Split(":").LastOrDefault();
                                break;
                            case "52A":
                                this.orderingCustomer = value.Split(":").LastOrDefault();
                                break;
                            case "56A":
                                this.intermediaryInstitution = value.Split(":").LastOrDefault();
                                break;
                            case "57A":
                                this.account = value.Split(":").LastOrDefault();
                                break;
                            case "59":
                                this.name = value.Split(":").LastOrDefault();
                                break;
                            case "59A":
                                this.name = value.Split(":").LastOrDefault();
                                break;
                            case "70":
                                this.remittanceInformation = value.Split(":").LastOrDefault();
                                break;
                            case "71A":
                                this.detailsOfCharges = value.Split(":").LastOrDefault();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
