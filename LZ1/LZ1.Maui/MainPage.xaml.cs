using LZ1.Core.Services;

namespace LZ1.App
{
    public partial class MainPage : ContentPage
    {
        private readonly ICounterService _counterService;

        public MainPage(ICounterService counterService)
        {
            InitializeComponent();
            _counterService = counterService ?? throw new ArgumentNullException(nameof(counterService));
            UpdateCounterLabel();
        }

        private void UpdateCounterLabel()
        {
            CounterLabel.Text = _counterService.GetLabel();
        }

        private async void OnIncrementClicked(object sender, EventArgs e)
        {
            var result = await _counterService.TryIncrement();
            if (result)
            {
                UpdateCounterLabel();
            }
        }

        private async void OnDecrementClicked(object sender, EventArgs e)
        {
            var result = await _counterService.TryDecrement();
            if (result)
            {
                UpdateCounterLabel();
            }
        }
    }
}