using Core.Services;

namespace Core.Tests
{
    [TestFixture]
    public class MainPageViewModelTests : TestsBase
    {
        [Test]
        public async Task TestSettingName()
        {
            var viewModel = await GetMainPageViewModel();

            Assert.That(viewModel.Name, Is.EqualTo("Glider1"));

            viewModel.Name = "Glider2";

            Assert.That(viewModel.Name, Is.EqualTo("Glider2"));
        }

        [Test]
        public async Task TestSettingMatriculation()
        {
            var viewModel = await GetMainPageViewModel();

            Assert.That(viewModel.Matriculation, Is.EqualTo("M-001"));

            viewModel.Matriculation = "M-002";

            Assert.That(viewModel.Matriculation, Is.EqualTo("M-002"));
        }

        [Test]
        public async Task TestIncrementPriceTriggersChange()
        {
            var viewModel = await GetMainPageViewModel();

            var notifications = new List<string?>();

            viewModel.PropertyChanged += (_, args) => notifications.Add(args.PropertyName);

            Assert.That(viewModel.Price, Is.EqualTo(100));

            viewModel.IncrementPrice();

            Assert.That(viewModel.Price, Is.EqualTo(101));
            Assert.That(notifications, Is.EquivalentTo(new[] { "Price" }));
        }

        // [Test]
        // public async Task TestSave()
        // {
        //     var serviceProvider = CreateServiceProvider();
        //     var localStorage = serviceProvider.GetRequiredService<ILocalStorage<SailplaneModel>>();
        //
        //     await localStorage.DeleteAll();
        //
        //     var viewModel = await GetMainPageViewModel(serviceProvider);
        //
        //     await viewModel.Save();
        //
        //     var settingsModels = await localStorage.LoadAll();
        //
        //     Assert.That(settingsModels, Has.Count.EqualTo(1));
        //     Assert.That(settingsModels[0].Name, Is.EqualTo("Glider1"));
        //     Assert.That(settingsModels[0].Matriculation, Is.EqualTo("M-001"));
        //     Assert.That(settingsModels[0].Price, Is.EqualTo(100));
        // }

        // [Test]
        // public async Task TestEnsureModelLoaded()
        // {
        //     var serviceProvider = CreateServiceProvider();
        //     var viewModel = serviceProvider.GetRequiredService<MainPageViewModel>();
        //     var localStorage = serviceProvider.GetRequiredService<ILocalStorage<SailplaneModel>>();
        //
        //     await localStorage.DeleteAll();
        //
        //     var notifications = new List<string?>();
        //
        //     viewModel.PropertyChanged += (_, args) => notifications.Add(args.PropertyName);
        //
        //     await viewModel.EnsureModelLoaded();
        //
        //     Assert.That(notifications, Is.EquivalentTo(new[] { "SelectedItem", "Name", "FullName", "Matriculation", "FullName", "Price", "IsReady" }));
        // }

        private async Task<MainPageViewModel> GetMainPageViewModel()
        {
            var serviceProvider = CreateServiceProvider();

            return await GetMainPageViewModel(serviceProvider);
        }

        private static async Task<MainPageViewModel> GetMainPageViewModel(IServiceProvider serviceProvider)
        {
            var result = serviceProvider.GetRequiredService<MainPageViewModel>();

            await result.EnsureModelLoaded();

            return result;
        }

        protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            return base.AddServices(serviceCollection)
                .AddSingleton(new LocalStorageSettings { DatabaseFilename = "Maui2024Tests.db3" })
                .AddSingleton<ILocalStorage<SailplaneModel>, MockLocalStorage<SailplaneModel>>() // Use MockLocalStorage for testing
                .AddSingleton<MainPageViewModel>();
        }

        private class MockLocalStorage<T> : ILocalStorage<T> where T : SailplaneModel, new()
        {
            private readonly List<T> _mockData = new()
            {
                new T { Id = 1, Name = "Glider1", Matriculation = "M-001", Price = 100 },
                new T { Id = 2, Name = "Glider2", Matriculation = "M-002", Price = 150 },
                new T { Id = 3, Name = "Glider3", Matriculation = "M-003", Price = 200 }
            };

            public Task Initialize()
            {
                return Task.CompletedTask;
            }

            public Task<bool> Save(T item)
            {
                var existingItem = _mockData.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.Matriculation = item.Matriculation;
                    existingItem.Price = item.Price;
                }
                else
                {
                    item.Id = _mockData.Max(i => i.Id ?? 0) + 1;
                    _mockData.Add(item);
                }
                return Task.FromResult(true);
            }

            public Task<bool> Delete(T item)
            {
                var existingItem = _mockData.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    _mockData.Remove(existingItem);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }

            public Task<T?> TryLoad(int id)
            {
                return Task.FromResult(_mockData.FirstOrDefault(i => i.Id == id));
            }

            public Task<List<T>> LoadAll()
            {
                return Task.FromResult(_mockData);
            }

            public Task<bool> DeleteAll()
            {
                _mockData.Clear();

                return Task.FromResult(true);
            }
        }
    }
}