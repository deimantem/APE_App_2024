using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Core.Services;

namespace Core;

public partial class MainPageViewModel : ViewModelBase
{
    public event EventHandler<DisplayAlertEventArgs>? DisplayAlertRequested;

    private readonly ILocalStorage<SailplaneModel> _localStorage;
    private string? _name = string.Empty;
    private string? _matriculation = string.Empty;
    private decimal _price;
    private string? _description = string.Empty;
    private DateTime? _yearOfConstruction;
    private SailplaneModel? _selectedItem;
    private bool? _isNewSailplane;

    // Constructor used for detecting binding in XAML
    public MainPageViewModel()
    {
        // throw new InvalidOperationException("This constructor is for detecting binding in XAML and should never be called.");
    }

    public MainPageViewModel(ILocalStorage<SailplaneModel> localStorage)
    {
        _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));

        AddCommand = new AsyncRelayCommand(AddOrUpdate);
        ToggleIsNewSailplaneCommand = new RelayCommand<object>(ToggleIsNewSailplane);
        DeleteCommand = new RelayCommand<SailplaneModel>(Delete);
        CreateDefaultDataCommand = new AsyncRelayCommand(CreateDefaultData);
    }

    public IAsyncRelayCommand AddCommand { get; }
    public RelayCommand<object> ToggleIsNewSailplaneCommand { get; }
    public RelayCommand<SailplaneModel> DeleteCommand { get; }

    public IAsyncRelayCommand CreateDefaultDataCommand { get; }

    private async Task CreateDefaultData()
    {
        // Generate mock data for testing
        var mockData = new ObservableCollection<SailplaneModel>
        {
            new()
            {
                Name = "Mock Sailplane 1",
                Matriculation = "MAT123",
                Price = 50000,
                Description = "This is a mock sailplane for testing purposes.",
                YearOfConstruction = new DateTime(2023, 1, 1),
                IsNewSailplane = true
            },
            new()
            {
                Name = "Mock Sailplane 2",
                Matriculation = "MAT456",
                Price = 75000,
                Description = "Another mock sailplane to simulate data.",
                YearOfConstruction = new DateTime(2022, 6, 15),
                IsNewSailplane = false
            }
        };

        Items.Clear();

        foreach (var item in mockData)
        {
            await _localStorage.Save(item);

            Items.Add(item);
        }

        OnDisplayAlertRequested("Success", "Default data created successfully.", "OK");
    }

    private bool ValidateFields()
    {
        if (string.IsNullOrEmpty(Name))
        {
            OnDisplayAlertRequested("Error", "Name cannot be empty.", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(Matriculation))
        {
            OnDisplayAlertRequested("Error", "Matriculation cannot be empty.", "OK");
            return false;
        }

        if (Price <= 0)
        {
            OnDisplayAlertRequested("Error", "Price must be greater than zero.", "OK");
            return false;
        }

        if (string.IsNullOrEmpty(Description))
        {
            OnDisplayAlertRequested("Error", "Description cannot be empty.", "OK");
            return false;
        }

        if (Description.Length < 10)
        {
            OnDisplayAlertRequested("Error", "Description must be at least 10 characters long.", "OK");
            return false;
        }

        if (YearOfConstruction == null || YearOfConstruction > DateTime.Now)
        {
            OnDisplayAlertRequested("Error", "Year of construction must be a valid past date.", "OK");
            return false;
        }

        return true;
    }

    private async Task AddOrUpdate()
    {
        if (!ValidateFields())
            return;

        if (_selectedItem == null)
        {
            var newSailplane = new SailplaneModel
            {
                Name = Name,
                Matriculation = Matriculation,
                Price = Price,
                Description = Description,
                YearOfConstruction = YearOfConstruction,
                IsNewSailplane = IsNewSailplane
            };

            var isSaved = await _localStorage.Save(newSailplane);

            if (isSaved)
            {
                Items.Add(newSailplane);
                ClearFields();

                OnDisplayAlertRequested("Success", "Item created successfully.", "OK");
            }
        }
        else
        {
            _selectedItem.Name = Name;
            _selectedItem.Matriculation = Matriculation;
            _selectedItem.Price = Price;
            _selectedItem.Description = Description;
            _selectedItem.YearOfConstruction = YearOfConstruction;
            _selectedItem.IsNewSailplane = IsNewSailplane;

            var isUpdated = await _localStorage.Save(_selectedItem);

            if (isUpdated)
            {
                // Refresh the item in the ObservableCollection to notify the UI of changes
                var index = Items.IndexOf(_selectedItem);
                if (index != -1)
                {
                    Items[index] = _selectedItem;
                }

                ClearFields();
                _selectedItem = null;
                OnDisplayAlertRequested("Success", "Item updated successfully.", "OK");
            }
        }
    }

    private void OnDisplayAlertRequested(string title, string message, string cancel)
    {
        DisplayAlertRequested?.Invoke(this, new DisplayAlertEventArgs(title, message, cancel));
    }

    public async void Delete(SailplaneModel? sailplane)
    {
        if (sailplane != null)
        {
            var isDeleted = await _localStorage.Delete(sailplane);

            if (isDeleted)
            {
                Items.Remove(sailplane);
            }
        }
    }

    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string? Matriculation
    {
        get => _matriculation;
        set => SetField(ref _matriculation, value);
    }

    public decimal Price
    {
        get => _price;
        set => SetField(ref _price, value);
    }

    public string? Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }

    public DateTime? YearOfConstruction
    {
        get => _yearOfConstruction;
        set => SetField(ref _yearOfConstruction, value);
    }

    public bool IsNewSailplane
    {
        get => _isNewSailplane ?? false; // Default to false if null
        set => SetField(ref _isNewSailplane, value);
    }

    public SailplaneModel? SelectedItem
    {
        get => _selectedItem;
        set => SetField(ref _selectedItem, value);
    }

    public ObservableCollection<SailplaneModel> Items { get; private set; } = new();

    public bool IsReady => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Matriculation) && Price > 0;

    public void IncrementPrice()
    {
        Price += 1;
    }

    [RelayCommand]
    public async Task EnsureModelLoaded()
    {
        if (Items.Count == 0)
        {
            try
            {
                await _localStorage.Initialize();

                var sailplaneModels = await _localStorage.LoadAll();

                foreach (var sailplaneModel in sailplaneModels)
                {
                    Items.Add(sailplaneModel);
                }

                OnPropertyChanged(nameof(IsReady));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    private void ClearFields()
    {
        Name = string.Empty;
        Matriculation = string.Empty;
        Price = 0;
        Description = string.Empty;
        YearOfConstruction = null;
        IsNewSailplane = false;
    }

    private void ToggleIsNewSailplane(object? obj)
    {
        IsNewSailplane = !IsNewSailplane;
    }
}