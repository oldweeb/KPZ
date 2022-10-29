using AutoMapper;
using Lab3.CodeFirst.DB;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Helpers;
using Lab3.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.CodeFirst.Services;

public class LoginService : ILoginService
{
    private readonly DbSet<User> _users;
    private readonly DbSet<Group> _groups;
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;
    private readonly JwtTokenHelper _tokenHelper;

    public LoginService(ScheduleDbContext context, IMapper mapper, JwtTokenHelper helper)
    {
        _users = context.Set<User>();
        _groups = context.Set<Group>();
        _context = context;
        _mapper = mapper;
        _tokenHelper = helper;
    }

    public async Task<string> Login(LoginDto login)
    {
        login.Email = login.Email.Trim().ToLower();
        User? user =
            await _users.FirstOrDefaultAsync(u => u.Email.ToLower() == login.Email && u.Password == login.Password);

        if (user is null)
        {
            throw new ArgumentException("Invalid credentials.");
        }

        return _tokenHelper.GenerateToken(user);
    }

    public async Task CreateUser(UserDto data)
    {
        await RequestUser(data, RequestType.Add);
    }

    public async Task UpdateUser(UserDto data)
    {
        if (data.Id is null)
        {
            throw new ArgumentException("User Id is missing.");
        }

        if (!await _users.AnyAsync(e => e.Id == data.Id))
        {
            throw new ArgumentException($"Event with id {data.Id} does not exist.");
        }
        await RequestUser(data, RequestType.Update);
    }

    private async Task RequestUser(UserDto data, RequestType type)
    {
        var user = _mapper.Map<User>(data);
        user.Email = user.Email.Trim().ToLower();

        if (await _users.AnyAsync(u => u.Email.ToLower() == user.Email))
        {
            throw new InvalidOperationException($"User with email {user.Email} already exists.");
        }

        if (data.Position == Position.Student && data.GroupId is null)
        {
            throw new ArgumentException($"Missing GroupId for user of type Student.");
        }

        if (data.Position == Position.Student)
        {
            user.Group = await _groups.FirstOrDefaultAsync(g => g.Id == data.GroupId) ??
                         throw new KeyNotFoundException($"Group with id {data.GroupId} does not exist");
        }

        _context.Entry(user).State = type == RequestType.Add ? EntityState.Added : EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public bool ValidateReadRights(Position position)
    {
        return position == Position.SystemAdministrator;
    }

    public bool ValidateWriteRights(Position position)
    {
        return position == Position.SystemAdministrator;
    }
}