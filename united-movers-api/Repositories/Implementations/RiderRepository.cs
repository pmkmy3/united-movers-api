using Microsoft.Data.SqlClient;
using System.Data;
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

        public async Task<IEnumerable<RiderShort>> GetAllRidersAsync()
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
                            List<RiderShort> riders = new List<RiderShort>();
                            do
                            {
                                riders.Add(new RiderShort
                                {
                                    RiderID = reader.GetInt32(reader.GetOrdinal("RiderID")),
                                    FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? "" : reader.GetString(reader.GetOrdinal("FullName")),
                                    AadharCardNumber = reader.IsDBNull(reader.GetOrdinal("AadharCardNumber")) ? "" : reader.GetString(reader.GetOrdinal("AadharCardNumber")),
                                    PANNumber = reader.IsDBNull(reader.GetOrdinal("PANNumber")) ? "" : reader.GetString(reader.GetOrdinal("PANNumber")),
                                    ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    EmailID = reader.IsDBNull(reader.GetOrdinal("EmailID")) ? "" : reader.GetString(reader.GetOrdinal("EmailID")),
                                    VendorName = reader.IsDBNull(reader.GetOrdinal("VendorName")) ? "" : reader.GetString(reader.GetOrdinal("VendorName"))
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

        public async Task<IEnumerable<DocumentTypes>> GetRiderDocumentTypesAsnc()
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[sp_GetRiderDocumentTypes]";

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                            List<DocumentTypes> documentTypes = new List<DocumentTypes>();
                            do
                            {
                                documentTypes.Add(new DocumentTypes
                                {
                                    DocumentTypeID = reader.GetInt32(reader.GetOrdinal("DocumentTypeID")),
                                    DocumentType = reader.GetString(reader.GetOrdinal("DocumentTypeName"))
                                });
                            }
                            while (reader.Read());
                            return documentTypes;
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
                throw new Exception("An error occurred while trying to get the rider document types", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }


        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetRegistredVendors]";

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                            List<Vendor> vendors = new List<Vendor>();
                            do
                            {
                                vendors.Add(new Vendor
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("EmailID")) ? "" : reader.GetString(reader.GetOrdinal("EmailID")),
                                    ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("ContactNumber")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                });
                            }
                            while (reader.Read());
                            return vendors;
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
                throw new Exception("An error occurred while trying to get all the active vendors", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<RiderAttachment> GetAttachmentContentByAttachmentIDAsync(Guid attachmentID)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[sp_GetAttachmentContentByAttachmentID]";
                    command.Parameters.Add(new SqlParameter("@AttachmentID", attachmentID));

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                            return new RiderAttachment
                            {
                                AttachmentID = attachmentID,
                                NumberOfKB =  reader["NumberOfKB"].ToString(),
                                ContentType =  reader["ContentType"].ToString(),
                                Content =  reader["Content"].ToString()
                            };
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
                throw new Exception("An error occurred while trying to get the attachment content by ID", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }


      
        public async Task<bool> DeleteRiderAttachmentAsync(RiderAttachment riderAttachment)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[Sp_DeleteRiderAttachments]";

                    command.Parameters.Add(new SqlParameter("@RiderID", riderAttachment.RiderID));
                    command.Parameters.Add(new SqlParameter("@AttachmentID", riderAttachment.AttachmentID)); 
                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to delete the Attachment ", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<bool> AddRiderAttachmentAsync(RiderAttachment riderAttachment)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[Sp_SaveRiderAttachments]";

                    command.Parameters.Add(new SqlParameter("@RiderID", riderAttachment.RiderID));
                    command.Parameters.Add(new SqlParameter("@AttachmentName", riderAttachment.AttachmentName));
                    command.Parameters.Add(new SqlParameter("@DocumentTypeID", riderAttachment.AttachmentTypeID));
                    command.Parameters.Add(new SqlParameter("@NumberOfKB", riderAttachment.NumberOfKB));
                    command.Parameters.Add(new SqlParameter("@ContentType", riderAttachment.ContentType));
                    command.Parameters.Add(new SqlParameter("@Resource", riderAttachment.Content));
                    command.Parameters.Add(new SqlParameter("@LoggedInUserID", riderAttachment.LoggedInUserID));
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

        public async Task<IEnumerable<RiderAttachment>> GetRiderAttachmentsByRiderIDAsync(int riderID)
        {

            try
            {
                List<RiderAttachment> attachments = new List<RiderAttachment>();
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[sp_GetRiderAttachmentsByID]";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@RiderID";
                    parameter.Value = riderID;
                    parameter.DbType = DbType.Int32;
                    command.Parameters.Add(parameter);

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                           
                            do
                            {
                                attachments.Add(new RiderAttachment
                                {
                                    //A.ContentType,lkp.LookupName AS DocumentType,AttachmentId ,AttachmentName
                                    ContentType = reader["ContentType"]?.ToString(),
                                    AttachmentType = reader["DocumentType"]?.ToString(),
                                    AttachmentID = Guid.Parse(reader["AttachmentID"].ToString()),
                                    AttachmentName = reader["AttachmentName"]?.ToString(),
                                    RiderID = riderID
                                });

                            }
                            while (reader.Read());
                            return attachments;
                        }
                        else
                        {
                            return attachments;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to get the attachments for the employee", ex);
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
                            return new Rider //(!reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                            {
                                RiderID = reader["RiderID"] != null ? reader.GetInt32(reader.GetOrdinal("RiderID")) : 0,
                                VendorID = reader["VendorID"] != null ? reader.GetInt32(reader.GetOrdinal("VendorID")) : 0,
                                ReferenceName = reader.IsDBNull(reader.GetOrdinal("ReferenceName")) ? "" : reader.GetString(reader.GetOrdinal("ReferenceName")),
                                FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? "" : reader.GetString(reader.GetOrdinal("FullName")),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? "" : reader.GetString(reader.GetOrdinal("Gender")),
                                DateOfBirth = (!reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))) ? Convert.ToDateTime(reader["DateOfBirth"]).ToCustomFormattedDate() : null,
                                AadharCardNumber = reader.IsDBNull(reader.GetOrdinal("AadharCardNumber")) ? "" : reader.GetString(reader.GetOrdinal("AadharCardNumber")),
                                PANNumber = reader.IsDBNull(reader.GetOrdinal("PANNumber")) ? "" : reader.GetString(reader.GetOrdinal("PANNumber")),
                                BloodGroup = reader.IsDBNull(reader.GetOrdinal("BloodGroup")) ? "" : reader.GetString(reader.GetOrdinal("BloodGroup")),
                                ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("ContactNumber")),
                                EmailID = reader.IsDBNull(reader.GetOrdinal("EmailID")) ? "" : reader.GetString(reader.GetOrdinal("EmailID")),
                                AlternativeContactNumber = reader.IsDBNull(reader.GetOrdinal("AlternativeContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("AlternativeContactNumber")),
                                AlternativeEmail = reader.IsDBNull(reader.GetOrdinal("AlternativeEmail")) ? "" : reader.GetString(reader.GetOrdinal("AlternativeEmail")),
                                EmergencyContactName = reader.IsDBNull(reader.GetOrdinal("EmergencyContactName")) ? "" : reader.GetString(reader.GetOrdinal("EmergencyContactName")),
                                EmergencyContactRelation = reader.IsDBNull(reader.GetOrdinal("EmergencyContactRelation")) ? "" : reader.GetString(reader.GetOrdinal("EmergencyContactRelation")),
                                EmergencyContactPersonID = reader.IsDBNull(reader.GetOrdinal("EmergencyContactPersonID")) ? "" : reader.GetString(reader.GetOrdinal("EmergencyContactPersonID")),
                                EmergencyContactNumber = reader.IsDBNull(reader.GetOrdinal("EmergencyContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("EmergencyContactNumber")),
                                AddressLine1 = reader.IsDBNull(reader.GetOrdinal("AddressLine1")) ? "" : reader.GetString(reader.GetOrdinal("AddressLine1")),
                                AddressLine2 = reader.IsDBNull(reader.GetOrdinal("AddressLine2")) ? "" : reader.GetString(reader.GetOrdinal("AddressLine2")),
                                State = reader.IsDBNull(reader.GetOrdinal("State")) ? "" : reader.GetString(reader.GetOrdinal("State")),
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? "" : reader.GetString(reader.GetOrdinal("City")),
                                Zip = reader.IsDBNull(reader.GetOrdinal("Zip")) ? "" : reader.GetString(reader.GetOrdinal("Zip")),
                                Landmark = reader.IsDBNull(reader.GetOrdinal("Landmark")) ? "" : reader.GetString(reader.GetOrdinal("Landmark")),
                                HighestDegreeEarned = reader.IsDBNull(reader.GetOrdinal("HighestDegreeEarned")) ? "" : reader.GetString(reader.GetOrdinal("HighestDegreeEarned")),
                                PreviousOrgName = reader.IsDBNull(reader.GetOrdinal("PreviousOrgName")) ? "" : reader.GetString(reader.GetOrdinal("PreviousOrgName")),
                                AccountNumber = reader.IsDBNull(reader.GetOrdinal("AccountNumber")) ? "" : reader.GetString(reader.GetOrdinal("AccountNumber")),
                                BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? "" : reader.GetString(reader.GetOrdinal("BankName")),
                                IFSCCode = reader.IsDBNull(reader.GetOrdinal("IFSCCode")) ? "" : reader.GetString(reader.GetOrdinal("IFSCCode")),
                                UANNumber = reader.IsDBNull(reader.GetOrdinal("UANNumber")) ? "" : reader.GetString(reader.GetOrdinal("UANNumber")),
                                InsurancePolicyNumber = reader.IsDBNull(reader.GetOrdinal("InsurancePolicyNumber")) ? "" : reader.GetString(reader.GetOrdinal("InsurancePolicyNumber")),
                                InsurerName = reader.IsDBNull(reader.GetOrdinal("InsurerName")) ? "" : reader.GetString(reader.GetOrdinal("InsurerName")),
                                InsuranceStartDate = (!reader.IsDBNull(reader.GetOrdinal("InsuranceStartDate"))) ? Convert.ToDateTime(reader["InsuranceStartDate"]).ToCustomFormattedDate() : null,
                                InsuranceEndDate = (!reader.IsDBNull(reader.GetOrdinal("InsuranceEndDate"))) ? Convert.ToDateTime(reader["InsuranceEndDate"]).ToCustomFormattedDate() : null,
                                FamilyMemberName = reader.IsDBNull(reader.GetOrdinal("FamilyMemberName")) ? "" : reader.GetString(reader.GetOrdinal("FamilyMemberName")),
                                FamilyMemberRelation = reader.IsDBNull(reader.GetOrdinal("FamilyMemberRelation")) ? "" : reader.GetString(reader.GetOrdinal("FamilyMemberRelation")),
                                FamilyMemberIDType = reader.IsDBNull(reader.GetOrdinal("FamilyMemberIDType")) ? "" : reader.GetString(reader.GetOrdinal("FamilyMemberIDType")),
                                FamilyMemberID = reader.IsDBNull(reader.GetOrdinal("FamilyMemberID")) ? "" : reader.GetString(reader.GetOrdinal("FamilyMemberID")),
                                FamilyMemberContact = reader.IsDBNull(reader.GetOrdinal("FamilyMemberContact")) ? "" : reader.GetString(reader.GetOrdinal("FamilyMemberContact")),
                                IsBackgroundVerificationCompleted = reader.GetBoolean(reader.GetOrdinal("IsBackgroundVerificationCompleted")),
                                IsPhysicalVerificationCompleted = reader.GetBoolean(reader.GetOrdinal("IsPhysicalVerificationCompleted")),
                                BackgroundVerificationAgencyName = reader.IsDBNull(reader.GetOrdinal("BackgroundVerificationAgencyName")) ? "" : reader.GetString(reader.GetOrdinal("BackgroundVerificationAgencyName")),
                                IsAadhaarVerified = reader.GetBoolean(reader.GetOrdinal("IsAadhaarVerified")),
                                IsContactNumberVerified = reader.GetBoolean(reader.GetOrdinal("IsContactNumberVerified")),
                                AdditionalNotes = reader.IsDBNull(reader.GetOrdinal("AdditionalNotes")) ? null : reader.GetString(reader.GetOrdinal("AdditionalNotes")),
                                CreatedByID = reader["CreatedByID"] != null ? reader.GetInt32(reader.GetOrdinal("CreatedByID")) : 0,
                                CreatedDate = (!reader.IsDBNull(reader.GetOrdinal("Createddate"))) ? Convert.ToDateTime(reader["Createddate"]).ToCustomFormattedDate() : null
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


                    command.Parameters.Add(new SqlParameter("@RiderID", contactInformation.RiderID));
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


                    command.Parameters.Add(new SqlParameter("@RiderID", financialDetails.RiderID));
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
                    command.Parameters.Add(new SqlParameter("@FirstName", request.FullName));
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
                    command.Parameters.Add(new SqlParameter("@VendorID", request.VendorID));
                    command.Parameters.Add(new SqlParameter("@FullName", request.FullName));
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
