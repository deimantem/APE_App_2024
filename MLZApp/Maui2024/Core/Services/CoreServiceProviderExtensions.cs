namespace Core.Services;

public static class CoreServiceProviderExtensions
{
    public static IServiceCollection CreateDefaultServiceCollection()
    {
        return new ServiceCollection().AddDefaultServices();
    }

    public static IServiceCollection AddDefaultServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton(typeof(ILocalStorage<>), typeof(SqliteLocalStorage<>))
            .AddSingleton<LocalStorageSettings>()
            .AddSingleton<SailplaneModel>()
            .AddTransient<MainPageViewModel>();
    }

    public static IServiceProvider CreateDefaultServiceProvider()
    {
        return CreateDefaultServiceCollection().BuildServiceProvider();
    }
}