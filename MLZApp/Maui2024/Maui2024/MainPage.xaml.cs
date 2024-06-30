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

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        await _viewModel.Save();
        await DisplayAlert("Save complete", "Your data has been saved.", "OK");
    }

    private void OnAddClicked(object? sender, EventArgs e)
    {
        _viewModel.Add();
    }
}