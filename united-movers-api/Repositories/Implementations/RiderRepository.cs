using Azure.Core;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using united_movers_api.Common;
using united_movers_api.Models;
using united_movers_api.Repositories.Interfaces;

namespace united_movers_api.Repositories.Implementations
{
    public class RiderRepository : IRiderRepository
    {
        private readonly IDbConnection _dbConnection;

        public RiderRepository(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }


      
       public async  Task<bool> AddRiderAttachmentAsync(AddRiderAttachment riderAttachment)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[AddRiderAttachment]";


                    command.Parameters.Add(new SqlParameter("@RiderID", riderAttachment.RiderID));
                    command.Parameters.Add(new SqlParameter("@AttachmentTypeID", riderAttachment.AttachmentTypeID));
                    command.Parameters.Add(new SqlParameter("@ReportTypeID", riderAttachment.ReportTypeID));
                    command.Parameters.Add(new SqlParameter("@NumberOfKB", riderAttachment.NumberOfKB));
                    command.Parameters.Add(new SqlParameter("@Resource", riderAttachment.Resource));
                    command.Parameters.Add(new SqlParameter("@Tags", riderAttachment.Tags));
                    command.Parameters.Add(new SqlParameter("@ContentType", riderAttachment.ContentType));
                    command.Parameters.Add(new SqlParameter("@LoggedInUserID", -1));
                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to add the Attachment ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<IEnumerable<Rider>> GetAllActiveRidersAsync()
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[spGetRiderById]";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@RiderID";
                    parameter.Value = -1;
                    parameter.DbType = DbType.Int32;
                    command.Parameters.Add(parameter);

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                            List<Rider> riders = new List<Rider>();
                            do
                            {
                                riders.Add(new Rider
                                {
                                    RiderID = reader.GetInt32(reader.GetOrdinal("RiderID")),
                                    VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                                    ReferenceName = reader.IsDBNull(reader.GetOrdinal("ReferenceName")) ? null : reader.GetString(reader.GetOrdinal("ReferenceName")),
                                    FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString(reader.GetOrdinal("LastName")),
                                    Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    AadharCardNumber = reader.IsDBNull(reader.GetOrdinal("AadharCardNumber")) ? null : reader.GetString(reader.GetOrdinal("AadharCardNumber")),
                                    PANNumber = reader.IsDBNull(reader.GetOrdinal("PANNumber")) ? null : reader.GetString(reader.GetOrdinal("PANNumber")),
                                    BloodGroup = reader.IsDBNull(reader.GetOrdinal("BloodGroup")) ? null : reader.GetString(reader.GetOrdinal("BloodGroup")),
                                    ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? null : reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    EmailID = reader.IsDBNull(reader.GetOrdinal("EmailID")) ? null : reader.GetString(reader.GetOrdinal("EmailID")),
                                    AlternativeContactNumber = reader.IsDBNull(reader.GetOrdinal("AlternativeContactNumber")) ? null : reader.GetString(reader.GetOrdinal("AlternativeContactNumber")),
                                    AlternativeEmail = reader.IsDBNull(reader.GetOrdinal("AlternativeEmail")) ? null : reader.GetString(reader.GetOrdinal("AlternativeEmail")),
                                    EmergencyContactName = reader.IsDBNull(reader.GetOrdinal("EmergencyContactName")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactName")),
                                    EmergencyContactRelation = reader.IsDBNull(reader.GetOrdinal("EmergencyContactRelation")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactRelation")),
                                    EmergencyContactPersonID = reader.IsDBNull(reader.GetOrdinal("EmergencyContactPersonID")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactPersonID")),
                                    EmergencyContactNumber = reader.IsDBNull(reader.GetOrdinal("EmergencyContactNumber")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactNumber")),
                                    AddressLine1 = reader.IsDBNull(reader.GetOrdinal("AddressLine1")) ? null : reader.GetString(reader.GetOrdinal("AddressLine1")),
                                    AddressLine2 = reader.IsDBNull(reader.GetOrdinal("AddressLine2")) ? null : reader.GetString(reader.GetOrdinal("AddressLine2")),
                                    State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                                    City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                                    Zip = reader.IsDBNull(reader.GetOrdinal("Zip")) ? null : reader.GetString(reader.GetOrdinal("Zip")),
                                    Landmark = reader.IsDBNull(reader.GetOrdinal("Landmark")) ? null : reader.GetString(reader.GetOrdinal("Landmark")),
                                    HighestDegreeEarned = reader.IsDBNull(reader.GetOrdinal("HighestDegreeEarned")) ? null : reader.GetString(reader.GetOrdinal("HighestDegreeEarned")),
                                    PreviousOrgName = reader.IsDBNull(reader.GetOrdinal("PreviousOrgName")) ? null : reader.GetString(reader.GetOrdinal("PreviousOrgName")),
                                    AccountNumber = reader.IsDBNull(reader.GetOrdinal("AccountNumber")) ? null : reader.GetString(reader.GetOrdinal("AccountNumber")),
                                    BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : reader.GetString(reader.GetOrdinal("BankName")),
                                    IFSCCode = reader.IsDBNull(reader.GetOrdinal("IFSCCode")) ? null : reader.GetString(reader.GetOrdinal("IFSCCode")),
                                    UANNumber = reader.IsDBNull(reader.GetOrdinal("UANNumber")) ? null : reader.GetString(reader.GetOrdinal("UANNumber")),
                                    InsurancePolicyNumber = reader.IsDBNull(reader.GetOrdinal("InsurancePolicyNumber")) ? null : reader.GetString(reader.GetOrdinal("InsurancePolicyNumber")),
                                    InsurerName = reader.IsDBNull(reader.GetOrdinal("InsurerName")) ? null : reader.GetString(reader.GetOrdinal("InsurerName")),
                                    InsuranceStartDate = reader.IsDBNull(reader.GetOrdinal("InsuranceStartDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("InsuranceStartDate")),
                                    InsuranceEndDate = reader.IsDBNull(reader.GetOrdinal("InsuranceEndDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("InsuranceEndDate")),
                                    FamilyMemberName = reader.IsDBNull(reader.GetOrdinal("FamilyMemberName")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberName")),
                                    FamilyMemberRelation = reader.IsDBNull(reader.GetOrdinal("FamilyMemberRelation")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberRelation")),
                                    FamilyMemberIDType = reader.IsDBNull(reader.GetOrdinal("FamilyMemberIDType")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberIDType")),
                                    FamilyMemberID = reader.IsDBNull(reader.GetOrdinal("FamilyMemberID")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberID")),
                                    FamilyMemberContact = reader.IsDBNull(reader.GetOrdinal("FamilyMemberContact")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberContact")),
                                    IsBackgroundVerificationCompleted = reader.GetBoolean(reader.GetOrdinal("IsBackgroundVerificationCompleted")),
                                    IsPhysicalVerificationCompleted = reader.GetBoolean(reader.GetOrdinal("IsPhysicalVerificationCompleted")),
                                    BackgroundVerificationAgencyName = reader.IsDBNull(reader.GetOrdinal("BackgroundVerificationAgencyName")) ? null : reader.GetString(reader.GetOrdinal("BackgroundVerificationAgencyName")),
                                    IsAadhaarVerified = reader.GetBoolean(reader.GetOrdinal("IsAadhaarVerified")),
                                    IsContactNumberVerified = reader.GetBoolean(reader.GetOrdinal("IsContactNumberVerified")),
                                    AdditionalNotes = reader.IsDBNull(reader.GetOrdinal("AdditionalNotes")) ? null : reader.GetString(reader.GetOrdinal("AdditionalNotes")),
                                    CreatedByID = reader.GetInt32(reader.GetOrdinal("CreatedByID")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                                });
                            }
                            while (reader.Read());
                            return riders;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to get all the active riders", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<Rider> GetRiderByIdAsync(int riderId)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[spGetRiderById]";
                    command.Parameters.Add(Utils.AddParameter(command, "@RiderID", riderId, DbType.Int32));

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
#pragma warning disable CS8601 // Possible null reference assignment.
                            return new Rider
                            {
                                RiderID = reader.GetInt32(reader.GetOrdinal("RiderID")),
                                VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                                ReferenceName = reader.IsDBNull(reader.GetOrdinal("ReferenceName")) ? null : reader.GetString(reader.GetOrdinal("ReferenceName")),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString(reader.GetOrdinal("LastName")),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                AadharCardNumber = reader.IsDBNull(reader.GetOrdinal("AadharCardNumber")) ? null : reader.GetString(reader.GetOrdinal("AadharCardNumber")),
                                PANNumber = reader.IsDBNull(reader.GetOrdinal("PANNumber")) ? null : reader.GetString(reader.GetOrdinal("PANNumber")),
                                BloodGroup = reader.IsDBNull(reader.GetOrdinal("BloodGroup")) ? null : reader.GetString(reader.GetOrdinal("BloodGroup")),
                                ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? null : reader.GetString(reader.GetOrdinal("ContactNumber")),
                                EmailID = reader.IsDBNull(reader.GetOrdinal("EmailID")) ? null : reader.GetString(reader.GetOrdinal("EmailID")),
                                AlternativeContactNumber = reader.IsDBNull(reader.GetOrdinal("AlternativeContactNumber")) ? null : reader.GetString(reader.GetOrdinal("AlternativeContactNumber")),
                                AlternativeEmail = reader.IsDBNull(reader.GetOrdinal("AlternativeEmail")) ? null : reader.GetString(reader.GetOrdinal("AlternativeEmail")),
                                EmergencyContactName = reader.IsDBNull(reader.GetOrdinal("EmergencyContactName")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactName")),
                                EmergencyContactRelation = reader.IsDBNull(reader.GetOrdinal("EmergencyContactRelation")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactRelation")),
                                EmergencyContactPersonID = reader.IsDBNull(reader.GetOrdinal("EmergencyContactPersonID")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactPersonID")),
                                EmergencyContactNumber = reader.IsDBNull(reader.GetOrdinal("EmergencyContactNumber")) ? null : reader.GetString(reader.GetOrdinal("EmergencyContactNumber")),
                                AddressLine1 = reader.IsDBNull(reader.GetOrdinal("AddressLine1")) ? null : reader.GetString(reader.GetOrdinal("AddressLine1")),
                                AddressLine2 = reader.IsDBNull(reader.GetOrdinal("AddressLine2")) ? null : reader.GetString(reader.GetOrdinal("AddressLine2")),
                                State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                                Zip = reader.IsDBNull(reader.GetOrdinal("Zip")) ? null : reader.GetString(reader.GetOrdinal("Zip")),
                                Landmark = reader.IsDBNull(reader.GetOrdinal("Landmark")) ? null : reader.GetString(reader.GetOrdinal("Landmark")),
                                HighestDegreeEarned = reader.IsDBNull(reader.GetOrdinal("HighestDegreeEarned")) ? null : reader.GetString(reader.GetOrdinal("HighestDegreeEarned")),
                                PreviousOrgName = reader.IsDBNull(reader.GetOrdinal("PreviousOrgName")) ? null : reader.GetString(reader.GetOrdinal("PreviousOrgName")),
                                AccountNumber = reader.IsDBNull(reader.GetOrdinal("AccountNumber")) ? null : reader.GetString(reader.GetOrdinal("AccountNumber")),
                                BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : reader.GetString(reader.GetOrdinal("BankName")),
                                IFSCCode = reader.IsDBNull(reader.GetOrdinal("IFSCCode")) ? null : reader.GetString(reader.GetOrdinal("IFSCCode")),
                                UANNumber = reader.IsDBNull(reader.GetOrdinal("UANNumber")) ? null : reader.GetString(reader.GetOrdinal("UANNumber")),
                                InsurancePolicyNumber = reader.IsDBNull(reader.GetOrdinal("InsurancePolicyNumber")) ? null : reader.GetString(reader.GetOrdinal("InsurancePolicyNumber")),
                                InsurerName = reader.IsDBNull(reader.GetOrdinal("InsurerName")) ? null : reader.GetString(reader.GetOrdinal("InsurerName")),
                                InsuranceStartDate = reader.IsDBNull(reader.GetOrdinal("InsuranceStartDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("InsuranceStartDate")),
                                InsuranceEndDate = reader.IsDBNull(reader.GetOrdinal("InsuranceEndDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("InsuranceEndDate")),
                                FamilyMemberName = reader.IsDBNull(reader.GetOrdinal("FamilyMemberName")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberName")),
                                FamilyMemberRelation = reader.IsDBNull(reader.GetOrdinal("FamilyMemberRelation")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberRelation")),
                                FamilyMemberIDType = reader.IsDBNull(reader.GetOrdinal("FamilyMemberIDType")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberIDType")),
                                FamilyMemberID = reader.IsDBNull(reader.GetOrdinal("FamilyMemberID")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberID")),
                                FamilyMemberContact = reader.IsDBNull(reader.GetOrdinal("FamilyMemberContact")) ? null : reader.GetString(reader.GetOrdinal("FamilyMemberContact")),
                                IsBackgroundVerificationCompleted = reader.GetBoolean(reader.GetOrdinal("IsBackgroundVerificationCompleted")),
                                IsPhysicalVerificationCompleted = reader.GetBoolean(reader.GetOrdinal("IsPhysicalVerificationCompleted")),
                                BackgroundVerificationAgencyName = reader.IsDBNull(reader.GetOrdinal("BackgroundVerificationAgencyName")) ? null : reader.GetString(reader.GetOrdinal("BackgroundVerificationAgencyName")),
                                IsAadhaarVerified = reader.GetBoolean(reader.GetOrdinal("IsAadhaarVerified")),
                                IsContactNumberVerified = reader.GetBoolean(reader.GetOrdinal("IsContactNumberVerified")),
                                AdditionalNotes = reader.IsDBNull(reader.GetOrdinal("AdditionalNotes")) ? null : reader.GetString(reader.GetOrdinal("AdditionalNotes")),
                                CreatedByID = reader.GetInt32(reader.GetOrdinal("CreatedByID")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                            };
#pragma warning restore CS8601 // Possible null reference assignment.
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to get the employee by ID", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<bool> ActivateOrDeactivateRiderAsync(ActivateOrDeactivateRiderRequest request)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ActivateOrDeActivateRider]";


                    command.Parameters.Add(new SqlParameter("@RiderID", request.RiderID));
                    command.Parameters.Add(new SqlParameter("@ActivateEmployee", request.ActivateRider));
                    command.Parameters.Add(new SqlParameter("@LoggedInUser", request.LoggedInUser));
                    command.Parameters.Add(new SqlParameter("@Comments", request.Comments));

                    _dbConnection.Open();

                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update Employee Attributes ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }


        public async Task<bool> UpdateRiderBackgroundVerificationDetailsAsync(RiderBackgroundVerification request)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateRiderBackgroundVerificationDetails]";


                    command.Parameters.Add(new SqlParameter("@RiderID", request.RiderID));
                    command.Parameters.Add(new SqlParameter("@IsBackgroundVerificationCompleted", request.IsBackgroundVerificationCompleted));
                    command.Parameters.Add(new SqlParameter("@IsPhysicalVerificationCompleted", request.IsPhysicalVerificationCompleted));
                    command.Parameters.Add(new SqlParameter("@IsAadhaarVerified", request.IsAadhaarVerified));
                    command.Parameters.Add(new SqlParameter("@IsContactNumberVerified", request.IsContactNumberVerified));
                    command.Parameters.Add(new SqlParameter("@FamilyMemberName", request.FamilyMemberName ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@FamilyMemberRelation", request.FamilyMemberRelation ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@FamilyMemberID", request.FamilyMemberID ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@FamilyMemberIDType", request.FamilyMemberIDType ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@FamilyMemberContact", request.FamilyMemberContact ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@BackgroundVerificationAgencyName", request.BackgroundVerificationAgencyName ?? (object)DBNull.Value));

                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update the Back ground Information ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<bool> UpdateRiderContactInformationAsync(RiderContactInformation contactInformation)
        {


            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateRiderContactInformation]";


                    command.Parameters.Add(new SqlParameter("@EmployeeID", contactInformation.RiderID));
                    command.Parameters.Add(new SqlParameter("@AddressLine1", contactInformation.AddressLine1 ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@AddressLine2", contactInformation.AddressLine2 ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@AlternativeContactNumber", contactInformation.AlternativeContactNumber ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@AlternativeEmail", contactInformation.AlternativeEmail ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@City", contactInformation.City ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EmergencyContactName", contactInformation.EmergencyContactName ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EmergencyContactNumber", contactInformation.EmergencyContactNumber ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EmergencyContactPersonID", contactInformation.EmergencyContactPersonID ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EmergencyContactRelation", contactInformation.EmergencyContactRelation ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@HighestDegreeEarned", contactInformation.HighestDegreeEarned ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Landmark", contactInformation.Landmark ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@PreviousOrgName", contactInformation.PreviousOrgName ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@State", contactInformation.State ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Zip", contactInformation.Zip ?? (object)DBNull.Value));


                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update the contact Information ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<bool> UpdateRiderFinancialDetailsAsync(RiderFinancialDetails financialDetails)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateRiderFinancialDetails]";


                    command.Parameters.Add(new SqlParameter("@EmployeeID", financialDetails.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@BankAccountNumber", financialDetails.BankAccountNumber ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@BankName", financialDetails.BankName ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@BankIFSCCode", financialDetails.BankIFSCCode ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@InsuranceEndDate", financialDetails.InsuranceEndDate ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@InsurancePolicyNumber", financialDetails.InsurancePolicyNumber ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@InsuranceStartDate", financialDetails.InsuranceStartDate ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@InsurerName", financialDetails.InsurerName ?? (object)DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UANNumber", financialDetails.UANNumber ?? (object)DBNull.Value));

                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update the Financial Information ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }

        }

        public async Task<bool> UpdateRiderPersonalInformation(ValidateAndCreateRiderIDRequest request)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateRiderPersonalInformation]";
                    command.Parameters.Add(new SqlParameter("@EmployeeID", request.RiderID));
                    command.Parameters.Add(new SqlParameter("@FirstName", request.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", request.LastName));
                    command.Parameters.Add(new SqlParameter("@Gender", request.Gender));
                    command.Parameters.Add(new SqlParameter("@DateOfBirth", request.DateOfBirth));
                    command.Parameters.Add(new SqlParameter("@AadhaarNumber", request.AadhaarNumber));
                    command.Parameters.Add(new SqlParameter("@PAN", request.PAN));
                    command.Parameters.Add(new SqlParameter("@ContactNumber", request.ContactNumber));
                    command.Parameters.Add(new SqlParameter("@BloodGroup", request.BloodGroup));
                    command.Parameters.Add(new SqlParameter("@PersonalEmailID", request.PersonalEmailID));
                    command.Parameters.Add(new SqlParameter("@LoggedInUserID", request.LoggedInUserID));


                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;


                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update the Personal Information ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }


        public async Task<CreateRiderResponse> ValidateAndCreateRiderIDAsync(ValidateAndCreateRiderIDRequest request)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ValidateAndCreateRiderID]";

                    command.Parameters.Add(new SqlParameter("@FirstName", request.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", request.LastName));
                    command.Parameters.Add(new SqlParameter("@Gender", request.Gender));
                    command.Parameters.Add(new SqlParameter("@DateOfBirth", request.DateOfBirth));
                    command.Parameters.Add(new SqlParameter("@AadhaarNumber", request.AadhaarNumber));
                    command.Parameters.Add(new SqlParameter("@PAN", request.PAN));
                    command.Parameters.Add(new SqlParameter("@ContactNumber", request.ContactNumber));
                    command.Parameters.Add(new SqlParameter("@BloodGroup", request.BloodGroup));
                    command.Parameters.Add(new SqlParameter("@PersonalEmailID", request.PersonalEmailID));
                    command.Parameters.Add(new SqlParameter("@LoggedInUserID", request.LoggedInUserID));

                    _dbConnection.Open();
                    //await Task.Run(() => command.ExecuteReader());
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                            var response = new CreateRiderResponse
                            {
                                Message = reader["Message"].ToString(),
                                Proceedfurther = Convert.ToBoolean(reader["ProceedFurther"]),
                                RiderID = Convert.ToInt32(reader["RiderID"])
                            };
                            return response;
                        }
                        else
                        {
                            return null;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update the Financial Information ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }

        }

    }
}
