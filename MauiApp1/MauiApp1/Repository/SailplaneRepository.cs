using MauiApp1.Model;

namespace MauiApp1.Repository
{
    public class SailplaneRepository : ISailplaneRepository
    {
        private readonly List<Sailplane> _sailplanes;

        public SailplaneRepository()
        {
            _sailplanes = new List<Sailplane>();
            InitializeData();
        }

        private void InitializeData()
        {
            _sailplanes.Add(new Sailplane { Id = 1, Name = "Glider 1", Matriculation = "GL123", Price = 5000.00m, Description = "High-performance glider" });
            _sailplanes.Add(new Sailplane { Id = 2, Name = "Glider 2", Matriculation = "GL456", Price = 8000.00m, Description = "Beginner's glider" });
        }

        public List<Sailplane> GetAllSailplanesAsync()
        {
            return _sailplanes.ToList();
        }

        public async Task AddSailplaneAsync(Sailplane sailplane)
        {
            await Task.Delay(100); // Simulate asynchronous operation

            sailplane.Id = _sailplanes.Count + 1;
            _sailplanes.Add(sailplane);
        }

        public async Task UpdateSailplaneAsync(Sailplane sailplane)
        {
            await Task.Delay(100); // Simulate asynchronous operation

            var existingSailplane = _sailplanes.FirstOrDefault(s => s.Id == sailplane.Id);
            if (existingSailplane != null)
            {
                existingSailplane.Name = sailplane.Name;
                existingSailplane.Matriculation = sailplane.Matriculation;
                existingSailplane.Price = sailplane.Price;
                existingSailplane.Description = sailplane.Description;
            }
        }

        public async Task DeleteSailplaneAsync(int sailplaneId)
        {
            await Task.Delay(100); // Simulate asynchronous operation

            var sailplaneToRemove = _sailplanes.FirstOrDefault(s => s.Id == sailplaneId);
            if (sailplaneToRemove != null)
                _sailplanes.Remove(sailplaneToRemove);
        }
    }
}