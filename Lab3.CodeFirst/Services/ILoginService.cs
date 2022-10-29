using Lab3.CodeFirst.DTOs;

namespace Lab3.CodeFirst.Services;

public interface ILoginService : IRightsValidator
{
    Task<string> Login(LoginDto login);
    Task CreateUser(UserDto data);
    Task UpdateUser(UserDto data);
}