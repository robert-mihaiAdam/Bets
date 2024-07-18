namespace Services.Interfaces
{
    public interface IBetableEntityService<T, UT>
    {
        Task<T> Create(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);
    }
}
