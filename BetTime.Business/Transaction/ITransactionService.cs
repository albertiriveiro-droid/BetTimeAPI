using BetTime.Models;

namespace BetTime.Business;


public interface ITransactionService
{
    
    Transaction CreateTransaction(TransactionCreateDTO dto); 
    IEnumerable<Transaction> GetTransactionsByUser(int userId);
    Transaction GetTransactionById(int transactionId);
    IEnumerable<Transaction> GetAllTransactions();
}