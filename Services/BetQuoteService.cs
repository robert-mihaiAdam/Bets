﻿using Services.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Dto.BetQuote;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Services
{
    public sealed class BetQuoteService : IBetQuoteService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public BetQuoteService(DBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<BetQuoteDto> CreateAsync(CreateBetQuotesDto entity, Guid betId)
        {
            BetQuotes newBetQuote = _mapper.Map<BetQuotes>(entity);
            newBetQuote.BetId = betId;
            _dbContext.BetQuotes.Add(newBetQuote);
            await _dbContext.SaveChangesAsync();
            BetQuoteDto newBetQuoteDto = _mapper.Map<BetQuoteDto>(newBetQuote);
            return newBetQuoteDto;
        }

        public IQueryable<BetQuotes> GetAll()
        {
            return _dbContext.BetQuotes.AsQueryable();
        }

        public async Task<BetQuoteDto>GetByBetIdAsync(Guid betId)
        {
            BetQuotes entity =  await _dbContext.BetQuotes.FirstOrDefaultAsync(bq => bq.BetId == betId);
            BetQuoteDto entityDto = _mapper.Map<BetQuoteDto>(entity);
            return entityDto;
        }

        public async Task<BetQuoteDto> GetByIdAsync(Guid id)
        {
            BetQuotes entity = await _dbContext.BetQuotes.FindAsync(id);
            BetQuoteDto entityDto = _mapper.Map<BetQuoteDto>(entity);
            return entityDto;
        }

        private BetQuotes GetNewQuotesByType(BetOptions type,  BetQuotes currentBetQuote)
        {
            if(type == BetOptions.A)
            {
                currentBetQuote.QuoteA += 0.01m;
                currentBetQuote.QuoteX -= 0.01m;
                currentBetQuote.QuoteB -= 0.01m;
            }

            if (type == BetOptions.B)
            {
                currentBetQuote.QuoteA -= 0.01m;
                currentBetQuote.QuoteX -= 0.01m;
                currentBetQuote.QuoteB += 0.01m;
            }

            if (type == BetOptions.X)
            {
                currentBetQuote.QuoteA -= 0.01m;
                currentBetQuote.QuoteX -= 0.01m;
                currentBetQuote.QuoteB += 0.01m;
            }

            return currentBetQuote;
        }

        public async Task<bool> UpdateBetQuotes(BetOptions type, Guid betQuoteId)
        {
            BetQuotes currentBetQuote = await _dbContext.BetQuotes.FindAsync(betQuoteId);
            if(currentBetQuote == null)
            {
                return false;
            }

            currentBetQuote = GetNewQuotesByType(type, currentBetQuote);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<BetQuoteDto> UpdateById(Guid id, UpdateBetQuotesDto newEntity)
        {
            BetQuotes currentEntity = await _dbContext.BetQuotes.FindAsync(id);
            if (currentEntity == null)
            {
                return null;
            }

            _mapper.Map(newEntity, currentEntity);
            await _dbContext.SaveChangesAsync();
            BetQuoteDto entityDto = _mapper.Map<BetQuoteDto>(currentEntity);
            return entityDto;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {          
            BetQuotes betQuote = await _dbContext.BetQuotes.FindAsync(id);
            if (betQuote == null)
            {
                return false;
            }

            _dbContext.BetQuotes.Remove(betQuote);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
