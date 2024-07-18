using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPlacedBetsService<T, UT>
    {
        Task<T> Create(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<T> Update(Guid id, UT entity);
    }
}
