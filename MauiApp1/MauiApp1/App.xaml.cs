using System.Collections.ObjectModel;
using MauiApp1.Model;

namespace MauiApp1;

public partial class App : Application
{
    public ObservableCollection<Sailplane> Sailplanes { get; set; }
    private Sailplane selectedSailplane;

    public App()
    {
        InitializeComponent();
        
        
        // Initialize ObservableCollection if needed
        Sailplanes = new ObservableCollection<Sailplane>();

        // Example: Adding dummy data for demonstration
        Sailplanes.Add(new Sailplane { Name = "Glider 1", Matriculation = "GL123", Price = 5000.00m, Description = "High-performance glider" });
        Sailplanes.Add(new Sailplane { Name = "Glider 2", Matriculation = "GL456", Price = 8000.00m, Description = "Beginner's glider" });

        // Set the BindingContext to this page so that Sailplanes can be accessed in XAML
        BindingContext = this;

        MainPage = new AppShell();
    }
}