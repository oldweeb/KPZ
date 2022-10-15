using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Schedule.Model;

namespace Schedule.UI.ViewModel
{
    public class ProfessorViewModel : LoggedUserViewModelBase
    {
        public UserViewModel Professor
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(Professor));
            }
        }

        public ProfessorViewModel(IRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override IEnumerable<EventViewModel> GetEvents()
        {
            return _repository
                .GetEvents(e => e.Professor.Id == Professor.Id && e.DayOfWeek == SelectedDay)
                .OrderBy(e => e.Order)
                .Select(e => _mapper.Map<EventViewModel>(e));
        }
    }
}
