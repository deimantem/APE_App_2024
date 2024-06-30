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

    // private async void OnSaveClicked(object? sender, EventArgs e)
    // {
    //     var newSailplane = new SailplaneModel
    //     {
    //         Name = NameEntry.Text,
    //         Matriculation = MatriculationEntry.Text,
    //         Price = decimal.Parse(PriceEntry.Text),
    //         Description = DescriptionEntry.Text
    //     };
    //
    //     _viewModel.Add(newSailplane);
    //
    //     await DisplayAlert("Save complete", "Your data has been saved.", "OK");
    //
    //     ClearFields();
    // }

    private void ClearFields()
    {
        NameEntry.Text = string.Empty;
        MatriculationEntry.Text = string.Empty;
        PriceEntry.Text = string.Empty;
        DescriptionEntry.Text = string.Empty;
    }

    private void OnEditClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnDeleteClicked(object? sender, EventArgs e)
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