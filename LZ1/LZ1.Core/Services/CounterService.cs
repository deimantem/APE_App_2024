namespace LZ1.Core.Services;

internal class CounterService : ICounterService
{
    private const string IncrementConfirmationMessage = "Are you sure you want to increment?";
    private const string DecrementConfirmationMessage = "Are you sure you want to decrement?";
    private readonly IDialogService _dialogService;

    private readonly ICounterState _state;

    public CounterService(ICounterState state, IDialogService dialogService)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
    }

    /// <inheritdoc />
    public void Increment()
    {
        _state.Increment();
    }

    /// <inheritdoc />
    public async Task<bool> TryIncrement()
    {
        var result = await _dialogService.AskAsync(IncrementConfirmationMessage);

        if (result)
        {
            Increment();
        }

        return result;
    }

    /// <inheritdoc />
    public string GetLabel()
    {
        var suffix = _state.Count == 1 ? string.Empty : "s";

        return $"Clicked {_state.Count} time{suffix}";
    }

    /// <inheritdoc />
    public void Decrement()
    {
        _state.Decrement();
    }

    /// <inheritdoc />
    public async Task<bool> TryDecrement()
    {
        var result = await _dialogService.AskAsync(DecrementConfirmationMessage);

        if (result)
        {
            Decrement();
        }

        return result;
    }
}