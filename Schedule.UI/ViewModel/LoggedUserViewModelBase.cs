using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using Schedule.Model;
using Schedule.UI.Commands;

namespace Schedule.UI.ViewModel;

public abstract class LoggedUserViewModelBase : LogoutViewModelBase
{
    protected UserViewModel _user;
    protected IRepository _repository;
    protected IMapper _mapper;
    private DayOfWeek _selectedDay = DayOfWeek.Monday;
    private ObservableCollection<EventViewModel>? _eventsByDay;
    private ICommand? _setNextDay;
    private ICommand? _setPreviousDay;

    protected LoggedUserViewModelBase(IRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public ObservableCollection<EventViewModel> Events
    {
        get
        {
            _eventsByDay ??= new ObservableCollection<EventViewModel>(GetEvents());
            return _eventsByDay;
        }
        set
        {
            _eventsByDay = value;
            OnPropertyChanged(nameof(Events));
        }
    }

    public ICommand SetNextDay
    {
        get
        {
            _setNextDay ??= new RelayCommand(_ => HandleSetNextDay(), _ => SelectedDay < DayOfWeek.Saturday);
            return _setNextDay;
        }
    }

    public ICommand SetPreviousDay
    {
        get
        {
            _setPreviousDay ??= new RelayCommand(_ => HandleSetPreviousDay(), _ => SelectedDay > DayOfWeek.Monday);
            return _setPreviousDay;
        }
    }

    public DayOfWeek SelectedDay
    {
        get => _selectedDay;
        set
        {
            _selectedDay = value;
            OnPropertyChanged(nameof(SelectedDay));
        }
    }

    protected abstract IEnumerable<EventViewModel> GetEvents();

    private void HandleSetNextDay()
    {
        if (SelectedDay == DayOfWeek.Saturday)
        {
            throw new InvalidOperationException();
        }

        SelectedDay++;
        Events = new ObservableCollection<EventViewModel>(GetEvents());
    }

    private void HandleSetPreviousDay()
    {
        if (SelectedDay == DayOfWeek.Monday)
        {
            throw new InvalidOperationException();
        }

        SelectedDay--;
        Events = new ObservableCollection<EventViewModel>(GetEvents());
    }
}