using Domain.Dto.BetableEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBetableEntityFacade
    {
        Task<BetableEntityDto> CreateBetEntityAsync(CreateBetableEntityDto newEntity);

        Task<BetableEntityDto> GetBetEntityByIdAsync(Guid id);

        Task<IEnumerable<BetableEntityDto>> GetAllBetEntitiesAsync();

        Task<BetableEntityDto> EditBetEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity);

        Task<bool> DeleteBetEntityByIdAsync(Guid id);
    }
}
