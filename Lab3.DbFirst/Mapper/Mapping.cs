using AutoMapper;
using Lab3.DbFirst.DTOs;
using Lab3.DbFirst.Models;

namespace Lab3.DbFirst.Mapper;

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