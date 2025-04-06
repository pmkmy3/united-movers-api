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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Riders = await _riderService.GetAllRidersAsync();
            if (Riders == null)
            {
                return BadRequest("Failed to fetch all RIders");
            }
            return Ok(Riders);
        }

        // GET: api/Rider/Vendor
        [HttpGet("Vendor")]
        public async Task<IActionResult> GetAllVendors()
        {
            var vendors = await _riderService.GetAllVendorsAsync();
            if (vendors == null)
            {
                return BadRequest("Failed to fetch all vendors");
            }
            return Ok(vendors);
        }

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


        [HttpGet("GetAttachmentContent/{attachmentID}")]
        public async Task<IActionResult> GetAttachmentContentByAttachmentID(Guid attachmentID)
        {
            var AttchmentWithContent = await _riderService.GetAttachmentContentByAttachmentID(attachmentID);
            if (AttchmentWithContent == null)
            {
                return BadRequest("Failed to fetch Attachment details with content by id");
            }
            return Ok(AttchmentWithContent);
        }

        [HttpGet("GetRiderDocumentTypes")]
        public async Task<IActionResult> GetRiderDocumentTypes()
        {
            var riderDocumentTypes = await _riderService.GetRiderDocumentTypesAsync();
            if (riderDocumentTypes == null)
            {
                return NotFound();
            }

            return  Ok(riderDocumentTypes);
        }

        // PUT: api/Rider/AddAttachment
        [HttpPut("AddAttachment")]
        public async Task<IActionResult> AddRiderAttachment([FromBody] RiderAttachment request)
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

            return Ok(result);
        }

        [HttpDelete("DeleteAttachment/{attachmentID}")]
        public async Task<IActionResult> DeleteRiderAttachment(Guid attachmentID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _riderService.DeleteRiderAttachmentAsync(attachmentID);

            if (!result)
            {
                return BadRequest("Failed to add attachment");
            }

            return Ok(result);
        }

        [HttpGet("GetAttachments/{riderID}")]
        public async Task<IActionResult> GetRiderAttachmentsByRiderID(int riderID)
        {
            var riderAttachments = await _riderService.GetRiderAttachmentsByRiderID(riderID);
            if (riderAttachments == null)
            {
                return BadRequest("Failed to fetch all employee attachments");
            }

            return Ok(riderAttachments);
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

            return Ok(result);
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

            return Ok(result);
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

            return Ok(result);
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
                return Ok(result);
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

            return Ok(result);
        }

    }
}




