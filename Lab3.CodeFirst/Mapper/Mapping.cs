using AutoMapper;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;

namespace Lab3.CodeFirst.Mapper;

public class Mapping
{
    public static IMapper Create()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EventDto, Event>();
            cfg.CreateMap<GroupDto, Group>();
            cfg.CreateMap<UserDto, User>();
        });

        return config.CreateMapper();
    }
}