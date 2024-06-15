using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MauiApp1.Model;
using MauiApp1.Repository;

namespace MauiApp1.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ISailplaneRepository _sailplaneRepository;
        private ObservableCollection<Sailplane> _sailplanes;
        private Sailplane _selectedSailplane;

        public ObservableCollection<Sailplane> Sailplanes
        {
            get => _sailplanes;
            private set
            {
                _sailplanes = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(ISailplaneRepository sailplaneRepository)
        {
            _sailplaneRepository = sailplaneRepository;
            Sailplanes = new ObservableCollection<Sailplane>();

            Initialize();
        }

        private async void Initialize()
        {
            await LoadSailplanesAsync();
        }

        private async Task LoadSailplanesAsync()
        {
            var sailplanes = _sailplaneRepository.GetAllSailplanesAsync();

            Sailplanes.Clear();

            foreach (var sailplane in sailplanes)
            {
                Sailplanes.Add(sailplane);
            }
        }

        public async Task AddSailplaneAsync(Sailplane sailplane)
        {
            await _sailplaneRepository.AddSailplaneAsync(sailplane);
            await LoadSailplanesAsync(); // Reload all sailplanes after adding
        }

        public async Task UpdateSailplaneAsync(Sailplane sailplane)
        {
            await _sailplaneRepository.UpdateSailplaneAsync(sailplane);
            await LoadSailplanesAsync(); // Reload all sailplanes after update
        }

        public async Task DeleteSailplaneAsync(Sailplane sailplane)
        {
            await _sailplaneRepository.DeleteSailplaneAsync(sailplane.Id);
            Sailplanes.Remove(sailplane);
        }

        public Sailplane SelectedSailplane
        {
            get => _selectedSailplane;
            set
            {
                _selectedSailplane = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}