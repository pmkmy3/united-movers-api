using united_movers_api.Models;


namespace united_movers_api.Services.Interfaces
{
    public interface IRiderService
    {

        Task<IEnumerable<RiderShort>> GetAllRidersAsync();

        Task<IEnumerable<Vendor>> GetAllVendorsAsync();

        Task<Rider> GetRiderByIdAsync(int riderId);
        Task<bool> AddRiderAttachmentAsync(RiderAttachment riderAttachment);

        Task<bool> DeleteRiderAttachmentAsync(RiderAttachment riderAttachment);
        Task<bool> UpdateRiderBackgroundVerificationDetailsAsync(RiderBackgroundVerification backgroundVerification);
        Task<bool> UpdateRiderContactInformationAsync(RiderContactInformation contactInformation);
        Task<bool> UpdateRiderFinancialDetailsAsync(RiderFinancialDetails financialDetails);
        Task<bool> UpdateRiderPersonalInformation(ValidateAndCreateRiderIDRequest request);
        Task<CreateRiderResponse> ValidateAndCreateRiderIDAsync(ValidateAndCreateRiderIDRequest request);
        Task<bool> ActivateOrDeactivateRiderAsync(ActivateOrDeactivateRiderRequest request);
    }
}
