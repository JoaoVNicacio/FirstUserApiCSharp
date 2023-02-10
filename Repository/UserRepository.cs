using ApiCSharp.Data;
using ApiCSharp.Models;
using Microsoft.EntityFrameworkCore;
// Classe que extende a interface

namespace ApiCSharp.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
      _context = context;
    }

    // Get users:
    public async Task<IEnumerable<User>> FindUsers()
    {
      return await _context.Users.ToListAsync();
    }

    // Get users by ID:
    public async Task<User> FindUserById(int id)
    {
      return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    // Add new user:
    public void AddUser(User user)
    {
      _context.Add(user);
    }

    // Edit User:
    public void EditUser(User user)
    {
      _context.Update(user);
    }

    // Delete user:
    public void DeleteUser(User user)
    {
      _context.Remove(user);
    }

    // Save changes:
    public async Task<bool> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

  }
}