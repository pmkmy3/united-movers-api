using System.Net.Mail;
using united_movers_api.Models;
using united_movers_api.Models.Common;

namespace united_movers_api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeShort>> GetAllEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        //Task<int> InsertEmployeeAsync(Employee employee);

        //Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<IEnumerable<Roles>> GetEmployeeRolesAsync();
        Task<IEnumerable<DocumentTypes>> GetEmployeeDocumentTypesAsync();
        Task<EmployeeAttachment> GetAttachmentContentByAttachmentID(Guid attachmentID);
        Task<bool> AddEmployeeAttachmentAsync(EmployeeAttachment employeeAttachment);
        Task<IEnumerable<EmployeeAttachment>> GetEmployeeAttachmentsByEmplIDasync(int emplID);
        Task<bool> DeleteEmployeeAttachmentAsync(Guid attachmentID);
        Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync(EmployeeBackgroundVerification backgroundVerification);
        Task<bool> UpdateEmployeeContactInformationAsync(EmployeeContactInformation contactInformation);
        Task<bool> UpdateEmployeeFinancialDetailsAsync(EmployeeFinancialDetails financialDetails);
        Task<bool> UpdateEmployeePersonalInformation(ValidateAndCreateEmployeeIDRequest request);

        Task<CreateEmployeeResponse> ValidateAndCreateEmployeeIDAsync(ValidateAndCreateEmployeeIDRequest request);
        Task<bool> ActivateOrDeactivateEmployeeAsync(ActivateOrDeactivateEmployeeRequest request);


        Task<bool> DeleteEmployeeRoleAsync(int roleMappingID);
        Task<bool> AddEmployeeRoleAsync(EmployeeRoleMapping employeeRoleMapping);
        Task<IEnumerable<EmployeeRoleMapping>> GetEmployeeAssignedRolesByEmplID(int emplID);

    }
}





/*


using System.Collections.Generic;
using System.Threading.Tasks;
using united_movers_api.Models;

namespace united_movers_api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<int> InsertEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> AddEmployeeAttachmentAsync(int empId, int attachmentTypeId, int reportTypeId, float numberOfKB, byte[] resource, string tags, string contentType, int loggedInUserId);
        Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync(int employeeId, bool isBackgroundVerificationCompleted, bool isPhysicalVerificationCompleted, string backgroundVerificationAgencyName);
        Task<bool> UpdateEmployeeContactInformationAsync(ContactInformation contactInformation);
        Task<bool> UpdateEmployeeFinancialDetailsAsync(FinancialDetails financialDetails);
    }
}
*/