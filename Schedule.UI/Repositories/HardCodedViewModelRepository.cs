using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Schedule.Model;
using Schedule.UI.ViewModel;

namespace Schedule.UI.Repositories;

public class HardCodedViewModelRepository : IViewModelRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;

    public HardCodedViewModelRepository(IRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public DataViewModel GetData()
    {
        return _mapper.Map<DataViewModel>(_repository.GetData());
    }

    public IEnumerable<EventViewModel> GetEvents()
    {
        return _repository.GetEvents().Select(e => _mapper.Map<EventViewModel>(e));
    }

    public IEnumerable<GroupViewModel> GetGroups()
    {
        return _repository.GetEvents().Select(g => _mapper.Map<GroupViewModel>(g));
    }

    public IEnumerable<UserViewModel> GetUsers()
    {
        return _repository.GetUsers().Select(u => _mapper.Map<UserViewModel>(u));
    }
}