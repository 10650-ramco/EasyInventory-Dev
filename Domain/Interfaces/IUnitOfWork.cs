namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IEmployeeRepository Employees { get; }
        IProductRepository Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

//namespace Domain.Interfaces
//{
//    public interface IUnitOfWork : IDisposable
//    {
//        Task<int> SaveChangesAsync(
//            CancellationToken cancellationToken = default);
//    }
//}