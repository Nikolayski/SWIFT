using System;

namespace SWIFT.Models
{
    public class Transaction
    {
        public Transaction()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string ReceiverBic { get; set; }
        public string SenderBic { get; set; }

        public string FileInfoId { get; set; }
        public virtual FileInfo FileInfo { get; set; }
    }
}
