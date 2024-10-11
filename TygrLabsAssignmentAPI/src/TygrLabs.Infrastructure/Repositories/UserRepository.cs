using TygrLabs.Domain.Entity;
using TygrLabs.Domain.Repositories;
using TygrLabs.Shared.Helpers;

namespace TygrLabs.Infrastructure.Repositories;


public class UserRepository : IUserRepository
{
    private readonly string _filePath = "users.json";
    private readonly List<string> _columns = new List<string> { "ID", "FirstName", "LastName" };  // Default fields

    public UserRepository()
    {
        // Ensure the file exists at startup
        if (!File.Exists(_filePath))
        {
            JsonFileHelper.WriteToFile(_filePath, new List<User>());
        }
    }

    // Get columns (fields)
    public List<string> GetColumns() => _columns;

    // Get all users
    public List<User> GetUsers()
    {
        return JsonFileHelper.ReadFromFile<List<User>>(_filePath) ?? new List<User>();
    }

    // Get user by ID
    public User GetUserById(string id)
    {
        var users = GetUsers();
        return users.FirstOrDefault(u => Convert.ToString(u.Fields["ID"]) == id);
    }

    // Add a new column
    public void AddColumn(string columnName)
    {
        if (!_columns.Contains(columnName))
        {
            _columns.Add(columnName);
        }
    }

    // Add a new user
    public void AddUser(User user)
    {
        var users = GetUsers();
        users.Add(user);
        JsonFileHelper.WriteToFile(_filePath, users);
    }

    // Edit an existing user
    public void EditUser(string id, User updatedUser)
    {
        var users = GetUsers();
        var userIndex = users.FindIndex(u => Convert.ToString(u.Fields["ID"]) == id);

        if (userIndex != -1)
        {
            users[userIndex] = updatedUser;
            JsonFileHelper.WriteToFile(_filePath, users);
        }
    }

    // Delete a user
    public void DeleteUser(string id)
    {
        var users = GetUsers();
        var userToDelete = users.FirstOrDefault(u => Convert.ToString(u.Fields["ID"]) == id);

        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            JsonFileHelper.WriteToFile(_filePath, users);
        }
    }
}
