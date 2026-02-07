//using Application.Common;
//using Application.Interfaces;
//using Application.Mapping;
//using Domain.Interfaces;

//namespace Application.Services
//{
//    public sealed class EmployeeService : IEmployeeService
//    {
//        private readonly IEmployeeRepository _employeeRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public EmployeeService(
//            IEmployeeRepository employeeRepository,
//            IUnitOfWork unitOfWork)
//        {
//            _employeeRepository = employeeRepository;
//            _unitOfWork = unitOfWork;
//        }

//        // -------------------------
//        // GET ALL
//        // -------------------------
//        public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync(
//            CancellationToken cancellationToken = default)
//        {
//            var employees = await _employeeRepository
//                .GetAllAsync(cancellationToken);

//            return employees
//                .Select(e => e.ToDto())
//                .ToList();
//        }

//        // -------------------------
//        // GET BY ID
//        // -------------------------
//        public async Task<EmployeeDto?> GetByIdAsync(
//            int id,
//            CancellationToken cancellationToken = default)
//        {
//            var employee = await _employeeRepository
//                .GetByIdAsync(id, cancellationToken);

//            return employee?.ToDto();
//        }

//        // -------------------------
//        // CREATE
//        // -------------------------
//        public async Task<Result> CreateAsync(
//            EmployeeDto dto,
//            CancellationToken cancellationToken = default)
//        {
//            var employee = dto.ToDomain();

//            await _employeeRepository.AddAsync(employee, cancellationToken);
//            await _unitOfWork.SaveChangesAsync(cancellationToken);

//            return Result.Success();
//        }

//        // -------------------------
//        // UPDATE
//        // -------------------------
//        public async Task<Result> UpdateAsync(
//            EmployeeDto dto,
//            CancellationToken cancellationToken = default)
//        {
//            if (!await _employeeRepository.ExistsAsync(dto.Id, cancellationToken))
//            {
//                return Result.Failure("Employee not found");
//            }

//            var employee = dto.ToDomain(dto.Id);

//            await _employeeRepository.UpdateAsync(employee, cancellationToken);
//            await _unitOfWork.SaveChangesAsync(cancellationToken);

//            return Result.Success();
//        }

//        // -------------------------
//        // DELETE
//        // -------------------------
//        public async Task<Result> DeleteAsync(
//            int id,
//            CancellationToken cancellationToken = default)
//        {
//            if (!await _employeeRepository.ExistsAsync(id, cancellationToken))
//            {
//                return Result.Failure("Employee not found");
//            }

//            await _employeeRepository.DeleteAsync(id, cancellationToken);
//            await _unitOfWork.SaveChangesAsync(cancellationToken);

//            return Result.Success();
//        }
//    }
//}
