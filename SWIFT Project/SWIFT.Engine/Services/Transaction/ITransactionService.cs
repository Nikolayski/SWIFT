using System.Threading.Tasks;

namespace SWIFT.Engine.Services.Transaction
{
    public interface ITransactionService
    {
        SWIFT.Models.Transaction Create(string name, string amount, string currency, string operationType, string account, string receiverBic, string senderBic);

        Task AddAsync(SWIFT.Models.Transaction transaction);
    }
}
