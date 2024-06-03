using LZ1.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LZ1.Core.Tests;

[TestFixture]
public class CounterStateTests : TestsBase
{
    private ICounterState GetCounterState()
    {
        var provider = CreateProvider();
        return provider.GetRequiredService<ICounterState>();
    }

    [Test]
    public void TestIncrement()
    {
        var counterState = GetCounterState();

        Assert.That(counterState.Count, Is.EqualTo(0));

        counterState.Increment();

        Assert.That(counterState.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestDecrement()
    {
        var counterState = GetCounterState();

        counterState.Increment();

        var initialCount = counterState.Count;

        counterState.Decrement();

        Assert.That(counterState.Count, Is.EqualTo(initialCount - 1), "Counter should be decremented by 1");
    }
}