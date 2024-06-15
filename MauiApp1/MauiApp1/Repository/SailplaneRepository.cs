using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MauiApp1.Model;

namespace MauiApp1.Repository
{
    public class SailplaneRepository : ISailplaneRepository
    {
        private readonly List<Sailplane> _sailplanes = new List<Sailplane>();

        public SailplaneRepository()
        {
            InitializeData();
        }

        public async Task<List<Sailplane>> GetAllSailplanesAsync()
        {
            // Simulated asynchronous operation (replace with actual data retrieval logic)
            await Task.Delay(100); // Simulate delay
            return _sailplanes.ToList();
        }

        public async Task AddSailplaneAsync(Sailplane sailplane)
        {
            // Simulated asynchronous operation (replace with actual data insertion logic)
            await Task.Delay(100); // Simulate delay
            sailplane.Id = _sailplanes.Count + 1;
            _sailplanes.Add(sailplane);
        }

        public async Task UpdateSailplaneAsync(Sailplane sailplane)
        {
            // Simulated asynchronous operation (replace with actual data update logic)
            await Task.Delay(100); // Simulate delay
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
            // Simulated asynchronous operation (replace with actual data deletion logic)
            await Task.Delay(100); // Simulate delay
            var sailplaneToRemove = _sailplanes.FirstOrDefault(s => s.Id == sailplaneId);
            if (sailplaneToRemove != null)
                _sailplanes.Remove(sailplaneToRemove);
        }

        private void InitializeData()
        {
            // Add default sailplanes here
            _sailplanes.Add(new Sailplane { Name = "Glider 1", Matriculation = "GL123", Price = 5000.00m, Description = "High-performance glider" });
            _sailplanes.Add(new Sailplane { Name = "Glider 2", Matriculation = "GL456", Price = 8000.00m, Description = "Beginner's glider" });
        }
    }
}