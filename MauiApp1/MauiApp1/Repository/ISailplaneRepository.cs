﻿using MauiApp1.Model;

namespace MauiApp1.Repository;

public interface ISailplaneRepository
{
        Task<List<Sailplane>> GetAllSailplanesAsync();
        Task AddSailplaneAsync(Sailplane sailplane);
        Task UpdateSailplaneAsync(Sailplane sailplane);
        Task DeleteSailplaneAsync(int sailplaneId);
}