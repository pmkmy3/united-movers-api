using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using united_movers_api.Models;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            if (employees == null)
            {
                return BadRequest("Failed to fetch all employees");
            }
            return Ok(employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }



        [HttpGet("GetAttachments/{emplID}")]
        public async Task<IActionResult> GetEmployeeAttachmentsByEmplID(int emplID)
        {
            var employeeAttachments = await _employeeService.GetEmployeeAttachmentsByEmplIDasync(emplID);
            if (employeeAttachments == null)
            {
                return BadRequest("Failed to fetch all employee attachments");
            }

            return Ok(employeeAttachments);
        }

        [HttpGet("GetAttachmentContent/{attachmentID}")]
        public async Task<IActionResult> GetAttachmentContentByAttachmentID(Guid attachmentID)
        {
            var AttchmentWithContent = await _employeeService.GetAttachmentContentByAttachmentID(attachmentID);
            if(AttchmentWithContent == null)
            {
                return BadRequest("Failed to fetch Attachment details with content by id");
            }
            return Ok(AttchmentWithContent);
        }

        //async Task<IEnumerable<EmployeeRoles>> GetEmployeeRolesAsync()
        [HttpGet("GetEmployeeRoles")]
        public async Task<IActionResult> GetEmployeeRoles()
        {
            var employeeRoles = await _employeeService.GetEmployeeRolesAsync();
            if (employeeRoles == null)
            {
                return BadRequest("Failed to fetch all employee Roles");
            }
            return Ok(employeeRoles);
        }


        [HttpGet("GetEmployeeDocumentTypes")]
        public async Task<IActionResult> GetEmployeeDocumentTypes()
        {
            var employeeDocumentTypes = await _employeeService.GetEmployeeDocumentTypesAsync();
            if (employeeDocumentTypes == null)
            {
                return BadRequest("Failed to fetch all employee document types");
            }
            return Ok(employeeDocumentTypes);
        }

        // PUT: api/Employee/AddAttachment
        [HttpPut("AddAttachment")]
        public async Task<IActionResult> AddEmployeeAttachment([FromBody] EmployeeAttachment request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.AddEmployeeAttachmentAsync(request);

            if (!result)
            {
                return BadRequest("Failed to add attachment");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteAttachment/{attachmentID}")]
        public async Task<IActionResult> DeleteEmployeeAttachment(Guid attachmentID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.DeleteEmployeeAttachmentAsync(attachmentID);

            if (!result)
            {
                return BadRequest("Failed to add attachment");
            }

            return Ok(result);
        }




        // PUT: api/Employee/UpdateBackgroundVerification
        [HttpPut("UpdateBackgroundVerification")]
        public async Task<IActionResult> UpdateEmployeeBackgroundVerificationDetails([FromBody] EmployeeBackgroundVerification request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateEmployeeBackgroundVerificationDetailsAsync(request);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Employee/UpdateContactInformation
        [HttpPut("UpdateContactInformation")]
        public async Task<IActionResult> UpdateEmployeeContactInformation([FromBody] EmployeeContactInformation contactInformation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _employeeService.UpdateEmployeeContactInformationAsync(contactInformation);

                if (!result)
                {
                    return BadRequest("Failed to update employee Contact Information details.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                // Clean up code
            }
        }

        // PUT: api/Employee/UpdateFinancialDetails
        [HttpPut("UpdateFinancialDetails")]
        public async Task<IActionResult> UpdateEmployeeFinancialDetails([FromBody] EmployeeFinancialDetails financialDetails)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _employeeService.UpdateEmployeeFinancialDetailsAsync(financialDetails);

                if (!result)
                {
                    return BadRequest("Failed to update employee Contact Information details.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                // Clean up code
            }
        }

        [HttpPost("ValidateAndCreateEmployeeID")]
        public async Task<IActionResult> ValidateAndCreateEmployeeID([FromBody] ValidateAndCreateEmployeeIDRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (request.EmployeeID > 0)
                {
                    var result = await _employeeService.UpdateEmployeePersonalInformation(request);
                    if (!result)
                    {
                        return BadRequest("Failed to update EmployeeID");
                    }
                    return Ok(result);
                }
                else
                {
                    var response = await _employeeService.ValidateAndCreateEmployeeIDAsync(request);

                    if (response == null)
                    {
                        return BadRequest("Failed to validate and create EmployeeID");
                    }

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                // Clean up code
            }
        }

        [HttpPut("ActivateOrDeactivate")]
        public async Task<IActionResult> ActivateOrDeactivateEmployee([FromBody] ActivateOrDeactivateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.ActivateOrDeactivateEmployeeAsync(request);

            if (!result)
            {
                return BadRequest("Failed to activate or deactivate employee");
            }

            return Ok(result);
        }


        [HttpGet("GetEmployeeAssignedRolesByEmplID/{id}")]
        public async Task<IActionResult> GetEmployeeAssignedRolesByEmplID(int id)
        {
            var employeeRoles = await _employeeService.GetEmployeeAssignedRolesByEmplID(id);
            if (employeeRoles == null)
            {
                return BadRequest("Failed to fetch all employee roles");
            }

            return Ok(employeeRoles);
        }

        // POST: api/Employee/AddAttachment
        [HttpPut("AddEmployeeRole")]
        public async Task<IActionResult> AddEmployeeRole([FromBody] EmployeeRoleMapping request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.AddEmployeeRoleAsync(request);

            if (!result)
            {
                return BadRequest("Failed to add role");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteEmployeeRole/{mappingID}")]
        public async Task<IActionResult> DeleteEmployeeRole(int mappingID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.DeleteEmployeeRoleAsync(mappingID);

            if (!result)
            {
                return BadRequest("Failed to delete role");
            }

            return Ok(result);
        }



    }
}




