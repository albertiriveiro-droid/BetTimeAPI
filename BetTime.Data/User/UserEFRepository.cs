using BetTime.Models;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Data;


public class UserEFRepository : IUserRepository
{

private readonly BetTimeContext _context;

public UserEFRepository(BetTimeContext context)
    {
     _context =context; 
    }

public void AddUser(User user)
    {
     _context.Users.Add(user);
    SaveChanges();   
    }

public User GetUserById(int userId)
{
    return _context.Users
        .Include(u => u.Transactions)
        .Include(u => u.Bets)
        .FirstOrDefault(u => u.Id == userId);
}

public IEnumerable<User> GetAllUsers()
{
    return _context.Users
        .Include(u => u.Transactions)
        .Include(u => u.Bets)
        .ToList();
}
        
 public User GetUserByEmail(string UserEmail)
    {
       return _context.Users
        .Include(u => u.Transactions)
        .Include(u => u.Bets)
        .FirstOrDefault(u => u.Email == UserEmail);
}

public void DeleteUser(User userDelete)
    {
    var user= GetUserById(userDelete.Id);
     _context.Users.Remove(user);
     SaveChanges();   
    }
 public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            SaveChanges();
        }
    
public bool EmailExists(string email)
{
    return _context.Users.Any(u => u.Email == email);
}

public bool UsernameExists(string username)
{
    return _context.Users.Any(u => u.Username == username);
}
public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }





 