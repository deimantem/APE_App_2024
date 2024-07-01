using Core;

namespace Maui2024;

public partial class MainPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = viewModel;

        _viewModel.DisplayAlertRequested += ViewModel_DisplayAlertRequested;

    }

    private void ViewModel_DisplayAlertRequested(object sender, DisplayAlertEventArgs e)
    {
        Device.BeginInvokeOnMainThread(async () =>
        {
            await DisplayAlert(e.Title, e.Message, e.Cancel);
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.DisplayAlertRequested -= ViewModel_DisplayAlertRequested;
    }

    private void EditButton_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button { BindingContext: SailplaneModel sailplane })
        {
            _viewModel.Delete(sailplane);
        }
    }
}