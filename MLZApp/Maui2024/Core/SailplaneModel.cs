using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Core
{
    /// <summary>
    /// Represents a model class for a sailplane, implementing INotifyPropertyChanged for data binding.
    /// </summary>
    public class SailplaneModel : INotifyPropertyChanged
    {
        private string? _name; // Backing field for the Name property
        private string? _matriculation; // Backing field for the Matriculation property
        private decimal _price; // Backing field for the Price property
        private string? _description; // Backing field for the Description property
        private DateTime? _yearOfConstruction; // Backing field for the YearOfConstruction property
        private bool? _isNewSailplane; // Backing field for the IsNewSailplane property

        /// <summary>
        /// Primary key for the database table, auto-incremented and not nullable.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        [NotNull]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the sailplane.
        /// </summary>
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Gets or sets the matriculation number of the sailplane.
        /// </summary>
        public string? Matriculation
        {
            get => _matriculation;
            set => SetProperty(ref _matriculation, value);
        }

        /// <summary>
        /// Gets or sets the price of the sailplane.
        /// </summary>
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        /// <summary>
        /// Gets or sets the description of the sailplane.
        /// </summary>
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        /// <summary>
        /// Gets or sets the year of construction of the sailplane.
        /// </summary>
        public DateTime? YearOfConstruction
        {
            get => _yearOfConstruction;
            set => SetProperty(ref _yearOfConstruction, value);
        }

        /// <summary>
        /// Gets or sets whether the sailplane is new.
        /// </summary>
        public bool? IsNewSailplane
        {
            get => _isNewSailplane;
            set => SetProperty(ref _isNewSailplane, value);
        }

        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes the PropertyChanged event to notify subscribers of a property change.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the value of a property and notifies listeners if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingStore">Reference to the backing field of the property.</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        private void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null!)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return;
            }

            backingStore = value;
            OnPropertyChanged(propertyName);
        }
    }
}