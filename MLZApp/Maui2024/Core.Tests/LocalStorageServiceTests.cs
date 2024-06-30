using Core.Services;

namespace Core.Tests
{
    [TestFixture]
    public class LocalStorageServiceTests : TestsBase
    {
        [Test]
        public async Task TestSaveAndLoad()
        {
            var localStorage = await GetLocalStorage();
            var sailplaneModel = CreateSettingsModel();

            Assert.That(sailplaneModel.Id, Is.Null);

            var saved = await localStorage.Save(sailplaneModel);
            Assert.Multiple(() =>
            {
                Assert.That(saved, "Object was not saved");
                Assert.That(sailplaneModel.Id, Is.Not.Zero);
            });

            if (sailplaneModel.Id != null)
            {
                var loadedSettingsModel = await localStorage.Load(sailplaneModel.Id.Value);

                Assert.That(loadedSettingsModel.Id, Is.EqualTo(sailplaneModel.Id));
            }
        }

        [Test]
        public async Task TestSaveAndTryLoad()
        {
            var localStorage = await GetLocalStorage();
            var sailplaneModel = CreateSettingsModel();

            Assert.That(sailplaneModel.Id, Is.Null);

            var saved = await localStorage.Save(sailplaneModel);
            Assert.Multiple(() =>
            {
                Assert.That(saved, "Object was not saved");
                Assert.That(sailplaneModel.Id, Is.Not.Zero);
            });

            if (sailplaneModel.Id != null)
            {
                var loadedSettingsModel = await localStorage.TryLoad(sailplaneModel.Id.Value);

                Assert.Multiple(() =>
                {
                    Assert.That(loadedSettingsModel, Is.Not.Null, $"Could not load item with Id = [{sailplaneModel.Id}]");
                    Assert.That(loadedSettingsModel!.Id, Is.EqualTo(sailplaneModel.Id));
                });
            }
        }

        [Test]
        public async Task TestSaveLoadAndDelete()
        {
            var localStorage = await GetLocalStorage();
            var sailplaneModel = CreateSettingsModel();

            Assert.That(sailplaneModel.Id, Is.Null);

            var saved = await localStorage.Save(sailplaneModel);
            Assert.Multiple(() =>
            {
                Assert.That(saved, "Object was not saved");
                Assert.That(sailplaneModel.Id, Is.Not.Zero);
            });

            if (sailplaneModel.Id != null)
            {
                var loadedSettingsModel = await localStorage.Load(sailplaneModel.Id.Value);

                Assert.That(loadedSettingsModel.Id, Is.EqualTo(sailplaneModel.Id));

                var deleted = await localStorage.Delete(loadedSettingsModel);

                Assert.That(deleted, "Object was not deleted.");
            }

            if (sailplaneModel.Id != null)
            {
                var settingsModelWithId = await localStorage.TryLoad(sailplaneModel.Id.Value);

                Assert.That(settingsModelWithId, Is.Null, "Object should no longer exist.");
            }
        }

        [Test]
        public async Task TestDeleteAndLoadAll()
        {
            var localStorage = await GetLocalStorage();
            var deleted = await localStorage.DeleteAll();

            Assert.That(deleted, "Could not delete all objects.");

            var settingsModels = await localStorage.LoadAll();

            Assert.That(settingsModels.Count, Is.EqualTo(0));

            for (var i = 0; i < 5; i++)
            {
                var saved = await localStorage.Save(CreateSettingsModel());

                Assert.That(saved, "Object was not saved.");
            }

            settingsModels = await localStorage.LoadAll();

            Assert.That(settingsModels.Count, Is.EqualTo(5));
        }

        protected override IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            return base.AddServices(serviceCollection)
                .AddSingleton(new LocalStorageSettings { DatabaseFilename = "Maui2024Tests.db3" })
                .AddSingleton<ILocalStorage<SailplaneModel>, SqliteLocalStorage<SailplaneModel>>();
        }

        private async Task<ILocalStorage<SailplaneModel>> GetLocalStorage()
        {
            var serviceProvider = CreateServiceProvider();
            var localStorage = serviceProvider.GetRequiredService<ILocalStorage<SailplaneModel>>();

            await localStorage.Initialize();
            return localStorage;
        }

        private static SailplaneModel CreateSettingsModel()
        {
            return new SailplaneModel
            {
                Name = "Discus 2b",
                Description = "Best glider ever"
            };
        }
    }
}