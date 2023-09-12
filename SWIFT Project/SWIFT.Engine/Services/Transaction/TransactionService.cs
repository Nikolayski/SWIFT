using System.Threading.Tasks;

namespace SWIFT.Engine.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private ApplicationDbContext db;
        public TransactionService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Models.Transaction transaction)
        {
            await this.db.Transaction.AddAsync(transaction);
            await this.db.SaveChangesAsync();
        }

        public Models.Transaction Create(string name, string amount, string currency, string operationType, string account, string receiverBic, string senderBic)
        {
            return new Models.Transaction { 
                                            Name = name, 
                                            Amount = amount, 
                                            Currency = currency, 
                                            Reason = operationType, 
                                            Account = account, 
                                            ReceiverBic = receiverBic, 
                                            SenderBic = senderBic };
                                          }
    }
}
