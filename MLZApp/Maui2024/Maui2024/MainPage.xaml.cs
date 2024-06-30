using Core;

namespace Maui2024;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = viewModel;
    }

    private void EditButton_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is SailplaneModel sailplane)
        {
            _viewModel.Delete(sailplane);
        }
    }
}