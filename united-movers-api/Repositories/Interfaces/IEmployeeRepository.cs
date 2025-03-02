using united_movers_api.Models;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllActiveEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<int> InsertEmployeeAsync(Employee employee);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<bool> AddEmployeeAttachmentAsync(AddEmployeeAttachment employeeAttachment );
        Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync(BackgroundVerification backgroundVerification);
        Task<bool> UpdateEmployeeContactInformationAsync(ContactInformation contactInformation);
        Task<bool> UpdateEmployeeFinancialDetailsAsync(FinancialDetails financialDetails);

        Task<CreateEmployeeResponse> ValidateAndCreateEmployeeIDAsync(ValidateAndCreateEmployeeIDRequest request);
    }
}
