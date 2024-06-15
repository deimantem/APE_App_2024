using MauiApp1.Model;
using MauiApp1.Repository;
using MauiApp1.ViewModel;

namespace MauiApp1.View;

public partial class MainPage
{
    private readonly MainViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        
        _viewModel = new MainViewModel(new SailplaneRepository());
    }

    private async void SaveButton_Clicked(object? sender, EventArgs e)
    {
        var newSailplane = new Sailplane
        {
            Name = NameEntry.Text,
            Matriculation = MatriculationEntry.Text,
            Price = decimal.Parse(PriceEntry.Text),
            Description = DescriptionEntry.Text
        };

        await _viewModel.AddSailplaneAsync(newSailplane);
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