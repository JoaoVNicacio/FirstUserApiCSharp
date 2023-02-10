using ApiCSharp.Models;

namespace ApiCSharp.Repository
{
  public interface IUserRepository
  {
    Task<IEnumerable<User>> FindUsers();
    Task<User> FindUserById(int id);
    void AddUser(User user);
    void EditUser(User user);
    void DeleteUser(User user);

    Task<bool> SaveChangesAsync();
  }
}