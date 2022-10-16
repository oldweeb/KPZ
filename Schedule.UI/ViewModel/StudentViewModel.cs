using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Schedule.Model;

namespace Schedule.UI.ViewModel
{
    public class StudentViewModel : LoggedUserViewModelBase
    {
        private GroupViewModel? _group;

        public StudentViewModel(IRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public GroupViewModel Group
        {
            get
            {
                _group ??= GetGroup();
                return _group;
            }
        }

        private GroupViewModel GetGroup()
        {
            Group group = _repository.GetGroups(g => g.Students.Any(s => s.Id == _user.Id))
                .FirstOrDefault(new Group() {Id = -1, Name = "Lost Group"});

            return _mapper.Map<GroupViewModel>(group);
        }

        protected override IEnumerable<EventViewModel> GetEvents()
        {
            return _repository
                .GetEvents(e => e.Group.Id == Group.Id && e.DayOfWeek == SelectedDay)
                .OrderBy(e => e.Order)
                .Select(_mapper.Map<EventViewModel>);
        }

        public UserViewModel Student
        {
            get => _user;
            set
            {
                if (value.Position != Position.Student)
                {
                    throw new ArgumentException();
                }

                _user = value;
                OnPropertyChanged(nameof(Student));
            }
        }
    }
}