using System;

namespace SWIFT.Models
{
    public class Transaction
    {
        private string name;
        private string amount;
        private string reason;
        private string currency;
        private string account;
        private string receiverBic;
        private string senderBic;
        public Transaction(string name, string amount, string reason, string currency, string account, string receiverBic, string senderBic)
        {
            this.Id = Guid.NewGuid().ToString();
            this.name = name;
            this.account = account;
            this.amount = amount;
            this.reason = reason;
            this.currency = currency;
            this.receiverBic = receiverBic;
            this.senderBic = senderBic;
        }
        public string Id { get; set; }
        public string Amount => this.amount;
        public string Reason => this.reason;
        public string Currency => this.currency;
        public string Name => this.name;
        public string Account => this.account;
        public string ReceiverBic => this.receiverBic;
        public string SenderBic => this.senderBic;

        public string FileInfoId { get; set; }
        public virtual FileInfo FileInfo { get; set; }
    }
}
