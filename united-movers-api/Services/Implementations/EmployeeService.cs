using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using united_movers_api.Models;
using united_movers_api.Models.Common;
using united_movers_api.Repositories.Implementations;
using united_movers_api.Repositories.Interfaces;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<IEnumerable<EmployeeShort>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllActiveEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(employeeId);
        }

        public async Task<EmployeeAttachment> GetAttachmentContentByAttachmentID(Guid attachmentID)
        {
            return await _employeeRepository.GetAttachmentContentByAttachmentIDAsync(attachmentID);
        }

        public async Task<IEnumerable<Roles>> GetEmployeeRolesAsync()
        {
            return await _employeeRepository.GetEmployeeRolesAsync();
        }

        public async Task<IEnumerable<DocumentTypes>> GetEmployeeDocumentTypesAsync()
        {
            return await _employeeRepository.GetEmployeeDocumentTypesAsync();
        }


        public async Task<bool> AddEmployeeRoleAsync(EmployeeRoleMapping employeeRoleMapping)
        {
            return await _employeeRepository.AddEmployeeRoleAsync(employeeRoleMapping);
        }
        public async Task<bool> DeleteEmployeeRoleAsync(int roleMappingID)
        {
            return await _employeeRepository.DeleteEmployeeRoleAsync(roleMappingID);
        }

        public async Task<IEnumerable<EmployeeRoleMapping>> GetEmployeeAssignedRolesByEmplID(int emplID)
        {
            return await _employeeRepository.GetEmployeeAssignedRolesByEmplID(emplID);
        }

        public async Task<bool> AddEmployeeAttachmentAsync( EmployeeAttachment employeeAttachment)
        {
            return await _employeeRepository.AddEmployeeAttachmentAsync(employeeAttachment);
        }
        public async Task<bool> DeleteEmployeeAttachmentAsync(Guid attachmentID)
        {
            return await _employeeRepository.DeleteEmployeeAttachmentAsync(attachmentID);
        }

        public async Task<IEnumerable<EmployeeAttachment>> GetEmployeeAttachmentsByEmplIDasync(int emplID)
        {
            return await _employeeRepository.GetEmployeeAttachmentsByEmplID(emplID);
        }
        public async Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync( EmployeeBackgroundVerification backgroundVerification)
        {
            return await _employeeRepository.UpdateEmployeeBackgroundVerificationDetailsAsync(backgroundVerification);    
        
        }

        public async Task<bool> UpdateEmployeeContactInformationAsync(EmployeeContactInformation contactInformation)
        {
            return await _employeeRepository.UpdateEmployeeContactInformationAsync(contactInformation);
        }

        public async Task<bool> UpdateEmployeeFinancialDetailsAsync( EmployeeFinancialDetails financialDetails)
        {
           return await _employeeRepository.UpdateEmployeeFinancialDetailsAsync (financialDetails);
        }

        public async Task<CreateEmployeeResponse> ValidateAndCreateEmployeeIDAsync(ValidateAndCreateEmployeeIDRequest request)
        {
        return await _employeeRepository.ValidateAndCreateEmployeeIDAsync (request);
        }

        public async Task<bool> UpdateEmployeePersonalInformation(ValidateAndCreateEmployeeIDRequest request)
        {
            return await _employeeRepository.UpdateEmployeePersonalInformation(request);
        }

        public async Task<bool> ActivateOrDeactivateEmployeeAsync(ActivateOrDeactivateEmployeeRequest request)
        {
            return await _employeeRepository.ActivateOrDeactivateEmployeeAsync(request);
        }

    }
}
