using BetTime.Data;
using BetTime.Models;

namespace BetTime.Business;

public class TransactionService : ITransactionService
{
private readonly ITransactionRepository _repository;
private readonly IUserRepository _userRepository;

public TransactionService(ITransactionRepository repository, IUserRepository userRepository)
    {
    _repository = repository;
    _userRepository = userRepository;
    }

public Transaction CreateTransaction(TransactionCreateDTO transactionCreateDTO)
    {
        var user = _userRepository.GetUserById(transactionCreateDTO.UserId)
            ?? throw new KeyNotFoundException($"User with ID {transactionCreateDTO.UserId} not found");

        if (transactionCreateDTO.Amount <= 0)
            throw new ArgumentException("Amount must be greater than 0.");

        if (transactionCreateDTO.Type != "DEPOSIT" && transactionCreateDTO.Type != "WITHDRAW")
            throw new ArgumentException("Transaction type must be DEPOSIT or WITHDRAW.");

        
        if (transactionCreateDTO.Type == "DEPOSIT")
            user.Balance += transactionCreateDTO.Amount;
        else if (transactionCreateDTO.Type == "WITHDRAW")
        {
            if (user.Balance < transactionCreateDTO.Amount)
                throw new InvalidOperationException("Insufficient balance.");

            user.Balance -= transactionCreateDTO.Amount;
        }

        _userRepository.UpdateUser(user);

        var transaction = new Transaction(user.Id, transactionCreateDTO.Amount, transactionCreateDTO.Type, transactionCreateDTO.PaymentMethod);
        _repository.AddTransaction(transaction);

        return transaction;
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
        return _repository.GetAllTransactions();
    }

    public Transaction GetTransactionById( int transactionId)
    {
        return _repository.GetTransactionById(transactionId);
    }

    public IEnumerable<Transaction> GetTransactionsByUser(int userId)
    {
       return _repository.GetTransactionsByUser(userId); 
    }




}