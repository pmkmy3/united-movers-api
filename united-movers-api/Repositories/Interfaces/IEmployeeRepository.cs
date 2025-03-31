using united_movers_api.Models;
using united_movers_api.Models.Common;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeShort>> GetAllActiveEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        //Task<int> InsertEmployeeAsync(Employee employee);

        //Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<IEnumerable<Roles>> GetEmployeeRolesAsync();
        Task<IEnumerable<DocumentTypes>> GetEmployeeDocumentTypesAsync();
        Task<EmployeeAttachment> GetAttachmentContentByAttachmentIDAsync(Guid attachmentID);
        Task<bool> AddEmployeeAttachmentAsync(EmployeeAttachment employeeAttachment);

        Task<bool> DeleteEmployeeAttachmentAsync(Guid attachmentID);

        Task<bool> DeleteEmployeeRoleAsync(int roleMappingID);
        Task<bool> AddEmployeeRoleAsync(EmployeeRoleMapping employeeRoleMapping);
        Task<IEnumerable<EmployeeRoleMapping>> GetEmployeeAssignedRolesByEmplID(int emplID);
        Task<IEnumerable<EmployeeAttachment>> GetEmployeeAttachmentsByEmplID(int emplID);
        Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync(EmployeeBackgroundVerification backgroundVerification);
        Task<bool> UpdateEmployeeContactInformationAsync(EmployeeContactInformation contactInformation);
        Task<bool> UpdateEmployeeFinancialDetailsAsync(EmployeeFinancialDetails financialDetails);
        Task<bool> UpdateEmployeePersonalInformation(ValidateAndCreateEmployeeIDRequest request);


        Task<CreateEmployeeResponse> ValidateAndCreateEmployeeIDAsync(ValidateAndCreateEmployeeIDRequest request);
        Task<bool> ActivateOrDeactivateEmployeeAsync(ActivateOrDeactivateEmployeeRequest request);
    }
}
