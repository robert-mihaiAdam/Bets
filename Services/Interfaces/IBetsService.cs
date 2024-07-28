﻿using Domain.Dto.Bets;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBetsService
    {
        Task<BetsDto> CreateAsync(CreateBetsDto entity);

        IQueryable<Bets> GetAllAsync();

        Task<BetsDto> GetByIdAsync(Guid id);

        Task<BetsDto> UpdateById(Guid id, UpdateBetsDto newEntity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}
