using united_movers_api.Models;

namespace united_movers_api.Repositories.Interfaces
{
    public interface IRiderRepository
    {

        Task<IEnumerable<RiderShort>> GetAllRidersAsync();

        Task<IEnumerable<Vendor>> GetAllVendorsAsync();

        Task<Rider> GetRiderByIdAsync(int riderId);
        Task<bool> AddRiderAttachmentAsync(RiderAttachment riderAttachment);
        Task<IEnumerable<DocumentTypes>> GetRiderDocumentTypesAsnc();
        Task<RiderAttachment> GetAttachmentContentByAttachmentIDAsync(Guid attachmentID);
        Task<IEnumerable<RiderAttachment>> GetRiderAttachmentsByRiderIDAsync(int riderID);
        Task<bool> DeleteRiderAttachmentAsync(Guid attachmentID);
        Task<bool> UpdateRiderBackgroundVerificationDetailsAsync(RiderBackgroundVerification backgroundVerification);
        Task<bool> UpdateRiderContactInformationAsync(RiderContactInformation contactInformation);
        Task<bool> UpdateRiderFinancialDetailsAsync(RiderFinancialDetails financialDetails);
        Task<bool> UpdateRiderPersonalInformation(ValidateAndCreateRiderIDRequest request);
        Task<CreateRiderResponse> ValidateAndCreateRiderIDAsync(ValidateAndCreateRiderIDRequest request);
        Task<bool> ActivateOrDeactivateRiderAsync(ActivateOrDeactivateRiderRequest request);
    }
}
