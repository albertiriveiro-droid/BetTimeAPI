using BetTime.Models;

namespace BetTime.Data;

public interface ITransactionRepository
{

void AddTransaction(Transaction transaction);
IEnumerable<Transaction> GetAllTransactions();
Transaction GetTransactionById (int transactionId);
IEnumerable<Transaction> GetTransactionsByUser(int userID);
void SaveChanges();
}