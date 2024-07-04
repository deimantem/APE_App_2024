using SQLite;

namespace Core
{
    /// <summary>
    /// Represents a model for a sailplane.
    /// </summary>
    public class SailplaneModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sailplane.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        [NotNull]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the sailplane.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the matriculation number of the sailplane.
        /// </summary>
        public string? Matriculation { get; set; }

        /// <summary>
        /// Gets or sets the price of the sailplane.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the sailplane.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the year of construction of the sailplane.
        /// </summary>
        public DateTime? YearOfConstruction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sailplane is new.
        /// </summary>
        public bool? IsNewSailplane { get; set; }
    }
}