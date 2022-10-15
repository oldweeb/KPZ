using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using AutoMapper;
using Newtonsoft.Json;
using Schedule.Model;
using Schedule.UI.Commands;

namespace Schedule.UI.ViewModel
{
    public class SystemAdministratorViewModel : LogoutViewModelBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private UserViewModel _user;
        private ObservableCollection<EventViewModel>? _events;
        private ObservableCollection<UserViewModel>? _professors;
        private ObservableCollection<GroupViewModel>? _groups;
        private ObservableCollection<UserViewModel>? _users;
        private ObservableCollection<UserViewModel>? _students;
        private UserViewModel? _selectedStudent;
        private GroupViewModel? _selectedGroup;
        private ICommand? _save;
        private ICommand? _addStudentToGroup;
        private ICommand? _removeStudentFromGroup;

        public UserViewModel SystemAdministrator
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(SystemAdministrator));
            }
        }

        public ObservableCollection<EventViewModel> Events
        {
            get
            {
                _events ??= new ObservableCollection<EventViewModel>(_repository.GetEvents().OrderBy(e => e.Id).Select(_mapper.Map<EventViewModel>));
                return _events;
            }
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        public ObservableCollection<UserViewModel> Professors
        {
            get
            {
                _professors ??= new ObservableCollection<UserViewModel>(_repository
                    .GetUsers(u => u.Position is Position.Assistant or Position.Professor)
                    .Select(_mapper.Map<UserViewModel>));
                return _professors;
            }
            set
            {
                _professors = value;
                OnPropertyChanged(nameof(Professors));
            }
        }

        public ObservableCollection<GroupViewModel> Groups
        {
            get
            {
                _groups ??= new ObservableCollection<GroupViewModel>(_repository.GetGroups()
                    .Select(_mapper.Map<GroupViewModel>));
                return _groups;
            }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public ObservableCollection<UserViewModel> Users
        {
            get
            {
                _users ??= new ObservableCollection<UserViewModel>(_repository.GetUsers()
                    .Select(_mapper.Map<UserViewModel>));
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<UserViewModel> Students
        {
            get
            {
                _students ??= new ObservableCollection<UserViewModel>(_repository
                    .GetUsers(u => u.Position == Position.Student).Select(_mapper.Map<UserViewModel>));
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public UserViewModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }

        public GroupViewModel? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        public ICommand Save
        {
            get
            {
                _save ??= new RelayCommand(_ => HandleSave());
                return _save;
            }
        }

        public ICommand AddStudentToGroup
        {
            get
            {
                _addStudentToGroup ??= new RelayCommand(_ => HandleAdd(), _ => CanAddStudentToGroup());
                return _addStudentToGroup;
            }
        }

        public ICommand RemoveStudentFromGroup
        {
            get
            {
                _removeStudentFromGroup ??= new RelayCommand(_ => HandleRemove(), _ => CanRemoveStudent());
                return _removeStudentFromGroup;
            }
        }

        private void HandleSave()
        {
            IEnumerable<User> users = Users.Select(_mapper.Map<User>);
            IEnumerable<Group> groups = Groups.Select(_mapper.Map<Group>);
            IEnumerable<Event> events = Events.Select(_mapper.Map<Event>);
            var data = new
            {
                users = users,
                groups = groups,
                events = events
            };

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(GlobalConfiguration.DataPath, json);
        }

        private void HandleAdd()
        {
            _selectedGroup.Add(_selectedStudent);
        }

        private void HandleRemove()
        {
            _selectedGroup.Remove(_selectedStudent);
        }

        private bool CanAddStudentToGroup()
        {
            return _selectedGroup != null 
                   && _selectedStudent != null 
                   && !Groups.Any(g => g.Students.Contains(_selectedStudent));
        }

        private bool CanRemoveStudent()
        {
            return _selectedStudent != null
                   && _selectedGroup != null
                   && _selectedGroup.Students.Contains(_selectedStudent);
        }

        public SystemAdministratorViewModel(IRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
