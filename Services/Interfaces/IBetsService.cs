﻿using Domain.Dto.Bets;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IBetsService
    {
        Task<BetsDto> CreateAsync(CreateBetsDto entity);

        IQueryable<Bets> GetAll();

        Task<BetsDto> GetByIdAsync(Guid id);

        Task<BetsDto> UpdateById(Guid id, UpdateBetsDto newEntity);

        Task DeleteByIdAsync(Guid id);
    }
}
