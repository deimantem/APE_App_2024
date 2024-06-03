namespace LZ1.Core.Services;

public interface ICounterService
{
    string GetLabel();

    void Increment();

    void Decrement();

    Task<bool> TryIncrement();

    Task<bool> TryDecrement();
}
