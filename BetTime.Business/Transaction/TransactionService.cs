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

public Transaction CreateTransaction(TransactionCreateDTO dto)
{
    var user = _userRepository.GetUserById(dto.UserId)
        ?? throw new KeyNotFoundException($"User with ID {dto.UserId} not found");

    if (dto.Amount <= 0)
        throw new ArgumentException("Amount must be greater than 0.");

    if (dto.Type != "DEPOSIT" && dto.Type != "WITHDRAW")
        throw new ArgumentException("Transaction type must be DEPOSIT or WITHDRAW.");

    if (dto.Type == "DEPOSIT" && string.IsNullOrWhiteSpace(dto.PaymentMethod))
        throw new ArgumentException("Payment method is required for deposits.");

   
    if (dto.Type == "DEPOSIT")
        user.Balance += dto.Amount;
    else if (dto.Type == "WITHDRAW")
    {
        if (user.Balance < dto.Amount)
            throw new InvalidOperationException("Insufficient balance.");
        user.Balance -= dto.Amount;
    }

    _userRepository.UpdateUser(user);

  
    string? paymentMethod = dto.Type == "DEPOSIT" ? dto.PaymentMethod : null;
    var transaction = new Transaction(user.Id, dto.Amount, dto.Type, paymentMethod);

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