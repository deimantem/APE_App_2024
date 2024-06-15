using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiApp1.Model;
using MauiApp1.Repository;

namespace MauiApp1.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ISailplaneRepository _sailplaneRepository;
    private bool _isEditPopupOpen;
    private Sailplane _selectedSailplane;

    public MainViewModel(Sailplane selectedSailplane, ISailplaneRepository sailplaneRepository)
    {
        _selectedSailplane = selectedSailplane;
        _sailplaneRepository = new SailplaneRepository();
        Sailplanes = new ObservableCollection<Sailplane>();

        LoadSailplanes();

        Sailplanes.Add(new Sailplane { Name = "Glider 1", Matriculation = "GL123", Price = 5000.00m, Description = "High-performance glider" });
        Sailplanes.Add(new Sailplane { Name = "Glider 2", Matriculation = "GL456", Price = 8000.00m, Description = "Beginner's glider" });
    }

    public MainViewModel(ObservableCollection<Sailplane> sailplanes, ISailplaneRepository sailplaneRepository, Sailplane selectedSailplane)
    {
        Sailplanes = sailplanes;
        _sailplaneRepository = sailplaneRepository;
        _selectedSailplane = selectedSailplane;
    }

    private ObservableCollection<Sailplane> Sailplanes { get; }

    public Sailplane SelectedSailplane
    {
        get => _selectedSailplane;
        set
        {
            _selectedSailplane = value;
            OnPropertyChanged();
        }
    }

    public bool IsEditPopupOpen
    {
        get => _isEditPopupOpen;
        set
        {
            _isEditPopupOpen = value;
            OnPropertyChanged();
        }
    }

    private async void LoadSailplanes()
    {
        var sailplanes = await _sailplaneRepository.GetAllSailplanesAsync();
        
        Sailplanes.Clear();
        
        foreach (var sailplane in sailplanes) Sailplanes.Add(sailplane);
    }

    public async Task AddSailplaneAsync(Sailplane sailplane)
    {
        await _sailplaneRepository.AddSailplaneAsync(sailplane);
        Sailplanes.Add(sailplane);
    }

    public async Task UpdateSailplaneAsync(Sailplane sailplane)
    {
        await _sailplaneRepository.UpdateSailplaneAsync(sailplane);

        LoadSailplanes();
    }

    public async Task DeleteSailplaneAsync(Sailplane sailplane)
    {
        await _sailplaneRepository.DeleteSailplaneAsync(sailplane.Id);
        
        Sailplanes.Remove(sailplane);
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}