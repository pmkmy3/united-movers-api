using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using united_movers_api.Models;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RiderController : ControllerBase
    {

        private readonly IRiderService _riderService;

        public RiderController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var Riders = await _riderService.GetActiveRidersAsync();
        //    if (Riders == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(Riders);
        //}

        // GET: api/Rider/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRiderById(int id)
        {
            var Rider = await _riderService.GetRiderByIdAsync(id);
            if (Rider == null)
            {
                return NotFound();
            }
            return Ok(Rider);
        }




       
        // POST: api/Rider/AddAttachment
        [HttpPost("AddAttachment")]
        public async Task<IActionResult> AddRiderAttachment([FromBody] AddRiderAttachment request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _riderService.AddRiderAttachmentAsync(request);

            if (!result)
            {
                return BadRequest("Failed to add attachment");
            }

            return Ok("Attachment added successfully");
        }


        // PUT: api/Rider/UpdateBackgroundVerification
        [HttpPut("UpdateBackgroundVerification")]
        public async Task<IActionResult> UpdateRiderBackgroundVerificationDetails([FromBody] RiderBackgroundVerification request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _riderService.UpdateRiderBackgroundVerificationDetailsAsync(request);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Rider/UpdateContactInformation
        [HttpPut("UpdateContactInformation")]
        public async Task<IActionResult> UpdateRiderContactInformation([FromBody] RiderContactInformation contactInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _riderService.UpdateRiderContactInformationAsync(contactInformation);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Rider/UpdateFinancialDetails
        [HttpPut("UpdateFinancialDetails")]
        public async Task<IActionResult> UpdateRiderFinancialDetails([FromBody] RiderFinancialDetails financialDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _riderService.UpdateRiderFinancialDetailsAsync(financialDetails);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("ValidateAndCreateRiderID")]
        public async Task<IActionResult> ValidateAndCreateRiderID([FromBody] ValidateAndCreateRiderIDRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (request.RiderID > 0)
            {
                var result = await _riderService.UpdateRiderPersonalInformation(request);
                if (!result)
                {
                    return BadRequest("Failed to update RiderID");
                }
                return Ok("RiderID updated successfully");
            }
            else
            {
                var response = await _riderService.ValidateAndCreateRiderIDAsync(request);

                if (response == null)
                {
                    return BadRequest("Failed to validate and create RiderID");
                }

                return Ok(response);
            }
        }

        [HttpPut("ActivateOrDeactivate")]
        public async Task<IActionResult> ActivateOrDeactivateRider([FromBody] ActivateOrDeactivateRiderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _riderService.ActivateOrDeactivateRiderAsync(request);

            if (!result)
            {
                return BadRequest("Failed to activate or deactivate Rider");
            }

            return Ok("Rider activation status updated successfully");
        }

    }
}




