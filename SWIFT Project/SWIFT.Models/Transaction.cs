namespace SWIFT.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Bic { get; set; }
    }
}
