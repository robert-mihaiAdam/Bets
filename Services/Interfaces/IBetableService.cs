namespace Services.Interfaces
{
    public interface IBetableService<T, UT>
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);

        Task<T> Update(Guid id, UT entity);

        Task<bool> DeleteById(Guid id);
    }
}
