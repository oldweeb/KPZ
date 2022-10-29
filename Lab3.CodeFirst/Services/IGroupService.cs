using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;

namespace Lab3.CodeFirst.Services;

public interface IGroupService : IRightsValidator
{
    Task<Group?> GetGroupById(Guid id);
    IQueryable<Group> GetGroups(PageInfo? pageInfo = null);
    Task AddGroup(GroupDto group);
    Task UpdateGroup(GroupDto group);
    Task<bool> DeleteGroup(Guid id);
}