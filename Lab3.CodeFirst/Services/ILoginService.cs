using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;

namespace Lab3.CodeFirst.Services;

public interface ILoginService : IRightsValidator
{
    Task<Tuple<string, Position>> Login(LoginDto login);
    Task CreateUser(UserDto data);
    Task UpdateUser(UserDto data);
}