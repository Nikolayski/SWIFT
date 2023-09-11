using System;
using System.Collections.Generic;

namespace SWIFT.Models
{
    public class FileInfo
    {
        private string name;
        private string hour;
        public FileInfo(string name, string hour)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transactions = new HashSet<Transaction>();
            this.hour = hour;
            this.name = name;
        }
        public string Id { get; set; }

        public string Name => this.name;

        public string Hour => this.hour;

        public ICollection<Transaction> Transactions{ get; set; }
    }
}
