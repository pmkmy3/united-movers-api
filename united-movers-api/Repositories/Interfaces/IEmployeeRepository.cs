using united_movers_api.Models;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeShort>> GetAllActiveEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        //Task<int> InsertEmployeeAsync(Employee employee);

        //Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<bool> AddEmployeeAttachmentAsync(AddEmployeeAttachment employeeAttachment );
        Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync(EmployeeBackgroundVerification backgroundVerification);
        Task<bool> UpdateEmployeeContactInformationAsync(EmployeeContactInformation contactInformation);
        Task<bool> UpdateEmployeeFinancialDetailsAsync(EmployeeFinancialDetails financialDetails);
        Task<bool> UpdateEmployeePersonalInformation(ValidateAndCreateEmployeeIDRequest request);


        Task<CreateEmployeeResponse> ValidateAndCreateEmployeeIDAsync(ValidateAndCreateEmployeeIDRequest request);
        Task<bool> ActivateOrDeactivateEmployeeAsync(ActivateOrDeactivateEmployeeRequest request);
    }
}
