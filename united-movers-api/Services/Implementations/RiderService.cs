using united_movers_api.Models;
using united_movers_api.Repositories.Interfaces;
using united_movers_api.Services.Interfaces;

namespace united_movers_api.Services.Implementations
{
    public class RiderService : IRiderService
    {
        private readonly IRiderRepository _RiderRepository;

        public RiderService(IRiderRepository RiderRepository)
        {
            _RiderRepository = RiderRepository;
        }

        public async Task<IEnumerable<RiderShort>> GetAllRidersAsync()
        {
            return await _RiderRepository.GetAllRidersAsync();
        }

        public Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return _RiderRepository.GetAllVendorsAsync();
        }

       public Task<RiderAttachment> GetAttachmentContentByAttachmentID(Guid attachmentID)
        {
            return _RiderRepository.GetAttachmentContentByAttachmentIDAsync(attachmentID);
        }

        public async Task<Rider> GetRiderByIdAsync(int RiderId)
        {
            return await _RiderRepository.GetRiderByIdAsync(RiderId);
        }

        public async Task<IEnumerable<DocumentTypes>> GetRiderDocumentTypesAsync()
        {
            return await _RiderRepository.GetRiderDocumentTypesAsnc();
        }

        public async Task<bool> AddRiderAttachmentAsync(RiderAttachment RiderAttachment)
        {
            return await _RiderRepository.AddRiderAttachmentAsync(RiderAttachment);
        }
        public async Task<bool> DeleteRiderAttachmentAsync(RiderAttachment RiderAttachment)
        {
            return await _RiderRepository.DeleteRiderAttachmentAsync(RiderAttachment);
        }

        public async Task<IEnumerable<RiderAttachment>> GetRiderAttachmentsByRiderID(int RiderID)
        {
            return await _RiderRepository.GetRiderAttachmentsByRiderIDAsync(RiderID);
        }
        public async Task<bool> UpdateRiderBackgroundVerificationDetailsAsync(RiderBackgroundVerification backgroundVerification)
        {
            return await _RiderRepository.UpdateRiderBackgroundVerificationDetailsAsync(backgroundVerification);

        }

        public async Task<bool> UpdateRiderContactInformationAsync(RiderContactInformation contactInformation)
        {
            return await _RiderRepository.UpdateRiderContactInformationAsync(contactInformation);
        }

        public async Task<bool> UpdateRiderFinancialDetailsAsync(RiderFinancialDetails financialDetails)
        {
            return await _RiderRepository.UpdateRiderFinancialDetailsAsync(financialDetails);
        }

        public async Task<CreateRiderResponse> ValidateAndCreateRiderIDAsync(ValidateAndCreateRiderIDRequest request)
        {
            return await _RiderRepository.ValidateAndCreateRiderIDAsync(request);
        }

        public async Task<bool> UpdateRiderPersonalInformation(ValidateAndCreateRiderIDRequest request)
        {
            return await _RiderRepository.UpdateRiderPersonalInformation(request);
        }

        public async Task<bool> ActivateOrDeactivateRiderAsync(ActivateOrDeactivateRiderRequest request)
        {
            return await _RiderRepository.ActivateOrDeactivateRiderAsync(request);
        }
         
    }
}
