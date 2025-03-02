using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var employees = await _employeeService.GetActiveEmployeesAsync();
            if (employees == null)
            {
                return NotFound();
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




        // POST: api/Employee
        //[HttpPost]
        //public async Task<IActionResult> InsertEmployee([FromBody] Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var employeeId = await _employeeService.InsertEmployeeAsync(employee);
        //    return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employee);
        //}


        //// PUT: api/Employee/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != employee.EmployeeID)
        //    {
        //        return BadRequest("Employee ID mismatch");
        //    }

        //    var result = await _employeeService.UpdateEmployeeAsync(employee);
        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        // POST: api/Employee/AddAttachment
        [HttpPost("AddAttachment")]
        public async Task<IActionResult> AddEmployeeAttachment([FromBody] AddEmployeeAttachment request)
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

            return Ok("Attachment added successfully");
        }


        // PUT: api/Employee/UpdateBackgroundVerification
        [HttpPut("UpdateBackgroundVerification")]
        public async Task<IActionResult> UpdateEmployeeBackgroundVerificationDetails([FromBody] BackgroundVerification request)
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
        public async Task<IActionResult> UpdateEmployeeContactInformation([FromBody] ContactInformation contactInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateEmployeeContactInformationAsync(contactInformation);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Employee/UpdateFinancialDetails
        [HttpPut("UpdateFinancialDetails")]
        public async Task<IActionResult> UpdateEmployeeFinancialDetails([FromBody] FinancialDetails financialDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateEmployeeFinancialDetailsAsync(financialDetails);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("ValidateAndCreateEmployeeID")]
        public async Task<IActionResult> ValidateAndCreateEmployeeID([FromBody] ValidateAndCreateEmployeeIDRequest request)
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
                return Ok("EmployeeID updated successfully");
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

            return Ok("Employee activation status updated successfully");
        }

    }
}




