using BetTime.Models;
using BetTime.Data;
using Microsoft.EntityFrameworkCore;
namespace BetTime.Business;

public class UserService : IUserService
{
    
private readonly IUserRepository _repository;

public UserService(IUserRepository repository)
    {
    _repository=repository;
    }

public User RegisterUser(UserCreateDTO userCreateDTO)
    {
         if (_repository.EmailExists(userCreateDTO.Email))
        throw new InvalidOperationException("Email already in use.");

    if (_repository.UsernameExists(userCreateDTO.Username))
        throw new InvalidOperationException("Username already in use.");


        var user = new User(userCreateDTO.Username, userCreateDTO.Email, userCreateDTO.Password);
        _repository.AddUser(user);

        return user;
    }

public IEnumerable<User> GetAllUsers()
    {
     return _repository.GetAllUsers();  
    }
public User GetUserByEmail(string email){
    try{
     return _repository.GetUserByEmail(email);
    }
     catch(Exception e){
           
     throw new Exception("An error has ocurred getting the user", e);
    }
    }

public User GetUserById(int userId)
    {
     var user= _repository.GetUserById(userId);
        if (user == null)
        {
        throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }   
        return user;
    }

public void DeleteUser(int userId)
    {
    var user= _repository.GetUserById(userId);
    _repository.DeleteUser(user);

    }
    
public void UpdateUser(int id, UserUpdateDTO userUpdateDTO)
{
    var user = _repository.GetUserById(id)
        ?? throw new KeyNotFoundException($"User with ID {id} not found.");

    
    if (!string.IsNullOrWhiteSpace(userUpdateDTO.Username) &&
        !userUpdateDTO.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase))
    {
        if (_repository.UsernameExists(userUpdateDTO.Username))
            throw new InvalidOperationException("Username is already taken.");

        user.Username = userUpdateDTO.Username.Trim();
    }

  
    if (!string.IsNullOrWhiteSpace(userUpdateDTO.Email) &&
        !userUpdateDTO.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
    {
        if (_repository.EmailExists(userUpdateDTO.Email))
            throw new InvalidOperationException("Email is already taken.");

        user.Email = userUpdateDTO.Email.Trim().ToLower();
    }

  
    if (!string.IsNullOrWhiteSpace(userUpdateDTO.Password))
    {
        user.Password = userUpdateDTO.Password;
    }

    try
    {
        _repository.UpdateUser(user);
    }
    catch (DbUpdateException)
    {
       
        throw new InvalidOperationException("Email or username already exists.");
    }
}




public User loginCheck(string email, string password)
{
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        throw new ArgumentException("Email and password are obligatory.");
    }

    foreach (var userToLog in _repository.GetAllUsers())
    {
        if (userToLog.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            userToLog.Password.Equals(password))
        {
            return userToLog;
        }
    }
    return null;
}  
}