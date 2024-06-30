using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Core.Services;

namespace Core
{
    public partial class MainPageViewModel : ViewModelBase
    {
        public event EventHandler<DisplayAlertEventArgs>? DisplayAlertRequested;

        private readonly ILocalStorage<SailplaneModel> _localStorage;
        private string? _name = string.Empty;
        private string? _matriculation = string.Empty;
        private decimal _price;
        private string? _description = string.Empty;

        // Constructor used for detecting binding in XAML
        public MainPageViewModel()
        {
            // throw new InvalidOperationException("This constructor is for detecting binding in XAML and should never be called.");
        }

        public MainPageViewModel(ILocalStorage<SailplaneModel> localStorage)
        {
            _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));

            AddCommand = new AsyncRelayCommand(Add);
        }

        public IAsyncRelayCommand AddCommand { get; }

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

            return true;
        }

        private async Task Add()
        {
            if (!ValidateFields())
                return;

            var newSailplane = new SailplaneModel
            {
                Name = Name,
                Matriculation = Matriculation,
                Price = Price,
                Description = Description
            };

            var isSaved = await _localStorage.Save(newSailplane);

            if (isSaved)
            {
                Items.Add(newSailplane);
                ClearFields();

                OnDisplayAlertRequested("Success", "Item created successfully.", "OK");
            }
        }

        private void OnDisplayAlertRequested(string title, string message, string cancel)
        {
            DisplayAlertRequested?.Invoke(this, new DisplayAlertEventArgs(title, message, cancel));
        }

        // private void Edit(SailplaneModel sailplane)
        // {
        //     // Implement edit logic
        // }

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
        }
    }
}