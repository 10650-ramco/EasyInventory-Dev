using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(
            AppDbContext context,
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository,
            IProductRepository productRepository)
        {
            _context = context;
            Users = userRepository;
            Employees = employeeRepository;
            Products = productRepository;
        }

        public IUserRepository Users { get; }
        public IEmployeeRepository Employees { get; }
        public IProductRepository Products { get; }


        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

//using Domain.Interfaces;

//namespace Infrastructure.Data
//{
//    public sealed class UnitOfWork : IUnitOfWork
//    {
//        private readonly AppDbContext _context;

//        public UnitOfWork(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<int> SaveChangesAsync(
//            CancellationToken cancellationToken = default)
//        {
//            return await _context.SaveChangesAsync(cancellationToken);
//        }

//        public void Dispose()
//        {
//            _context.Dispose();
//        }
//    }
//}
