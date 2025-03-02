using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using united_movers_api.Models;
using united_movers_api.Repositories.Interfaces;

namespace united_movers_api.Repositories.Implementations
{
    public class RiderRepository : IRiderRepository
    {
        Task<bool> IRiderRepository.ActivateOrDeactivateRiderAsync(ActivateOrDeactivateRiderRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRiderRepository.AddRiderAttachmentAsync(AddRiderAttachment riderAttachment)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Rider>> IRiderRepository.GetAllActiveRidersAsync()
        {
            throw new NotImplementedException();
        }

        Task<Rider> IRiderRepository.GetRiderByIdAsync(int riderId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRiderRepository.UpdateRiderBackgroundVerificationDetailsAsync(RiderBackgroundVerification backgroundVerification)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRiderRepository.UpdateRiderContactInformationAsync(RiderContactInformation contactInformation)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRiderRepository.UpdateRiderFinancialDetailsAsync(RiderFinancialDetails financialDetails)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRiderRepository.UpdateRiderPersonalInformation(ValidateAndCreateRiderIDRequest request)
        {
            throw new NotImplementedException();
        }

        Task<CreateRiderResponse> IRiderRepository.ValidateAndCreateRiderIDAsync(ValidateAndCreateRiderIDRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
