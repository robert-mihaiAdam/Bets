namespace Services.Interfaces
{
    public interface IBetsService<T, UT>
    {
        Task<T> Create(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);
    }
}
