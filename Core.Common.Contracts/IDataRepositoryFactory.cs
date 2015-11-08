namespace Core.Common.Contracts
{
    // Get a data repository
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IDataRepository;
    }
}