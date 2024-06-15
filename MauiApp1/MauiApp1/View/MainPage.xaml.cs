using MauiApp1.Model;
using MauiApp1.Repository;
using MauiApp1.ViewModel;

namespace MauiApp1.View;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        
        BindingContext = new MainViewModel(new Sailplane(), new SailplaneRepository());
    }

    private void SaveButton_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void EditButton_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}