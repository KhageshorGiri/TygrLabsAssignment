using TygrLabs.Domain.Entity;

namespace TygrLabs.Domain.Repositories;

public interface IUserRepository
{
    List<string> GetColumns();                 // Get the list of column names (fields).
    List<User> GetUsers();              // Get all users.
    User GetUserById(string id);           // Get a specific user by ID.
    void AddColumn(string columnName);         // Add a new column to the user data.
    void AddUser(User user);            // Add a new user.
    void EditUser(string id, User user);   // Edit an existing user by ID.
    void DeleteUser(string id);
}
