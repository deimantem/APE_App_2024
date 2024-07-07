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

    private void ViewModel_DisplayAlertRequested(object? sender, DisplayAlertEventArgs e)
    {
        async void Action()
        {
            await DisplayAlert(e.Title, e.Message, e.Cancel);
        }

        MainThread.BeginInvokeOnMainThread(Action);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.DisplayAlertRequested -= ViewModel_DisplayAlertRequested;
    }

    private void EditButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is SailplaneModel item)
        {
            if (BindingContext is MainPageViewModel viewModel)
            {
                viewModel.SelectedItem = item;
                viewModel.Name = item.Name;
                viewModel.Matriculation = item.Matriculation;
                viewModel.Price = item.Price;
                viewModel.Description = item.Description;
                viewModel.YearOfConstruction = item.YearOfConstruction;
                viewModel.IsNewSailplane = item.IsNewSailplane ?? false;
            }
        }
    }

    private void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button { BindingContext: SailplaneModel sailplane })
        {
            _viewModel.Delete(sailplane);
        }
    }
}