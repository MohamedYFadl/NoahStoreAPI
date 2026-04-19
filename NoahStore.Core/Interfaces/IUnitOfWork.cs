namespace NoahStore.Core.Interfaces
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        public void Save();
    }
}
