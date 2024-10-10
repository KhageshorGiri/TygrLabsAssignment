using TygrLabs.Application.IServices;
using TygrLabs.Domain.Entity;
using TygrLabs.Domain.Repositories;

namespace TygrLabs.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<string> GetColumns() => _userRepository.GetColumns();

    public List<User> GetUsers() => _userRepository.GetUsers();

    public User GetUserById(int id) => _userRepository.GetUserById(id);

    public void AddColumn(string columnName)
    {
        if (string.IsNullOrWhiteSpace(columnName))
        {
            throw new ArgumentException("Column name cannot be empty or whitespace.");
        }

        _userRepository.AddColumn(columnName);
    }

    public void AddUser(Dictionary<string, object> user)
    {
        var columns = _userRepository.GetColumns();
        foreach (var column in columns)
        {
            if (!user.ContainsKey(column))
            {
                throw new ArgumentException($"Missing field: {column}");
            }
        }

        var dynamicUser = new User
        {
            Fields = user
        };

        _userRepository.AddUser(dynamicUser);
    }

    public void EditUser(int id, Dictionary<string, object> updatedFields)
    {
        var existingUser = _userRepository.GetUserById(id);
        if (existingUser == null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        var updatedUser = new User
        {
            Fields = updatedFields
        };

        _userRepository.EditUser(id, updatedUser);
    }

    public void DeleteUser(int id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        _userRepository.DeleteUser(id);
    }
}
