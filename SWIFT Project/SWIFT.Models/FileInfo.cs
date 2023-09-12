using System;
using System.Collections.Generic;

namespace SWIFT.Models
{
    public class FileInfo
    {
        public FileInfo()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transactions = new HashSet<Transaction>();

        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Hour { get; set; }

        public ICollection<Transaction> Transactions{ get; set; }
    }
}
