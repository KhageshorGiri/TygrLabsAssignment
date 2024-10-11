using TygrLabs.Domain.Entity;

namespace TygrLabs.Application.IServices;

public interface IUserService
{
    List<string> GetColumns();                             // Get the list of column names (fields).
    List<User> GetUsers();                          // Get all users.
    User GetUserById(string id);                       // Get a specific user by ID.
    void AddColumn(string columnName);                     // Add a new column to the user data.
    void AddUser(Dictionary<string, object> user);         // Add a new user with a dictionary of dynamic fields.
    void EditUser(string id, Dictionary<string, object> user);// Edit an existing user by ID.
    void DeleteUser(string id);
}
