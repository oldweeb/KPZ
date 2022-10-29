using AutoMapper;
using Schedule.Model;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Mapper;

public class Mapping
{
    public static IMapper Create()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DataModel, DataViewModel>();
            cfg.CreateMap<DataViewModel, DataModel>();

            cfg.CreateMap<Event, EventViewModel>();
            cfg.CreateMap<EventViewModel, Event>();

            cfg.CreateMap<Group, GroupViewModel>();
            cfg.CreateMap<GroupViewModel, Group>();

            cfg.CreateMap<User, UserViewModel>();
            cfg.CreateMap<UserViewModel, User>();
        });

        return config.CreateMapper();
    }
}