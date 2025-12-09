
using BetTime.Models;

namespace BetTime.Business;

public interface IUserService
{
    
User RegisterUser(UserCreateDTO userCreateDTO);
IEnumerable<User> GetAllUsers();
User GetUserByEmail(string email);
User GetUserById( int userId);
void UpdateUser(int id, UserUpdateDTO userUpdateDTO);
void DeleteUser(int userId);
bool IsEmailTaken(string email);
User loginCheck(string email, string password);

}