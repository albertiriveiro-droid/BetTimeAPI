
using BetTime.Models;
using Microsoft.EntityFrameworkCore;


namespace BetTime.Data;

public class TransactionEFRepository: ITransactionRepository
{
   private readonly BetTimeContext _context;

   public TransactionEFRepository(BetTimeContext context)
    {
    _context=context;
    } 

public void AddTransaction(Transaction transaction)
    {
    _context.Transactions.Add(transaction);
    SaveChanges();
        
    }

public IEnumerable<Transaction> GetAllTransactions()
    {
    return _context.Transactions
                .Include(t => t.User)
                .ToList();
    }


    public Transaction GetTransactionById(int transactionId)
        {
            return _context.Transactions
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == transactionId);
        }

     public IEnumerable<Transaction> GetTransactionsByUser(int userId)
        {
            return _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .ToList();
        }
   

public void SaveChanges()
    {
        
    _context.SaveChanges();
    }


}