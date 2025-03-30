using Elfie.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.Net.Mail;
using System.Xml.Linq;
using united_movers_api.Common;
using united_movers_api.Models;
using united_movers_api.Repositories.Interfaces;

namespace united_movers_api.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

          public async Task<IEnumerable<EmployeeShort>> GetAllActiveEmployeesAsync()
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetEmployeeByID]";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@EmployeeID";
                    parameter.Value = -1;
                    parameter.DbType = DbType.Int32;
                    command.Parameters.Add(parameter);

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
                            List<EmployeeShort> employees = new List<EmployeeShort>();
                            do
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                employees.Add(new EmployeeShort
                                {
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    FirstName = reader["FirstName"]?.ToString(),
                                    LastName = reader["LastName"]?.ToString(),
                                    PersonalEmailID = reader["PersonalEmailID"]?.ToString(),
                                    ContactNumber = reader["ContactNumber"]?.ToString(),
                                    AadhaarNumber = reader["AadhaarNumber"]?.ToString(),
                                    PanNumber = reader["PanNumber"]?.ToString()
                                });
#pragma warning restore CS8601 // Possible null reference assignment.
                            }
                            while (reader.Read());
                            return employees;
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
                throw new Exception("An error occurred while trying to get all the active employees", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetEmployeeByID]";
                    command.Parameters.Add(Utils.AddParameter(command, "@EmployeeID", employeeId, DbType.Int32));

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                        if (reader.Read())
                        {
#pragma warning disable CS8601 // Possible null reference assignment.
                            return new Employee
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"]?.ToString(),
                                LastName = reader["LastName"]?.ToString(),
                                BloodGroup = reader["BloodGroup"]?.ToString(),
                                Gender = reader["Gender"]?.ToString(),
                                PersonalEmailID = reader["PersonalEmailID"]?.ToString(),
                                ContactNumber = reader["ContactNumber"]?.ToString(),
                                AlternativeContactNumber = reader["AlternativeContactNumber"]?.ToString(),
                                EmergencyContactNumber = reader["EmergencyContactNumber"]?.ToString(),
                                AadhaarNumber = reader["AadhaarNumber"]?.ToString(),
                                PanNumber = reader["PanNumber"]?.ToString(),
                                BankAccountNumber = reader["AccountNumber"]?.ToString(),
                                BankName = reader["BankName"]?.ToString(),
                                BankIFSCCode = reader["IFSCCode"]?.ToString(),
                                DateOfBirth = (!reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))) ? Convert.ToDateTime(reader["DateOfBirth"]).ToCustomFormattedDate() : null,
                                CreatedDate = (!reader.IsDBNull(reader.GetOrdinal("CreatedDate"))) ? Convert.ToDateTime(reader["CreatedDate"]).ToCustomFormattedDate() : null,
                                ModifiedDate = (!reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))) ? Convert.ToDateTime(reader["ModifiedDate"]).ToCustomFormattedDate() : null,
                                CreatedByID = (!reader.IsDBNull(reader.GetOrdinal("CreatedByID"))) ? Convert.ToInt32(reader["CreatedByID"]) : -1,
                                ModifiedByID = (!reader.IsDBNull(reader.GetOrdinal("ModifiedByID"))) ? Convert.ToInt32(reader["ModifiedByID"]) : -1,
                                AlternativeEmail = reader["AlternativeEmail"]?.ToString(),
                                EmergencyContactName = reader["EmergencyContactName"]?.ToString(),
                                EmergencyContactRelation = reader["EmergencyContactRelation"]?.ToString(),
                                EmergencyContactPersonID = reader["EmergencyContactPersonID"]?.ToString(),
                                AddressLine1 = reader["AddressLine1"]?.ToString(),
                                AddressLine2 = reader["AddressLine2"]?.ToString(),
                                State = reader["State"]?.ToString(),
                                City = reader["City"]?.ToString(),
                                Zip = reader["Zip"]?.ToString(),
                                Landmark = reader["Landmark"]?.ToString(),
                                HighestDegreeEarned = reader["HighestDegreeEarned"]?.ToString(),
                                PreviousOrgName = reader["PreviousOrgName"]?.ToString(),
                                UANNumber = reader["UANNumber"]?.ToString(),
                                InsurancePolicyNumber = reader["InsurancePolicyNumber"]?.ToString(),
                                InsurerName = reader["InsurerName"]?.ToString(),
                                InsuranceStartDate = (!reader.IsDBNull(reader.GetOrdinal("InsuranceStartDate"))) ? Convert.ToDateTime(reader["InsuranceStartDate"]).ToCustomFormattedDate() : null,
                                InsuranceEndDate = (!reader.IsDBNull(reader.GetOrdinal("InsuranceEndDate"))) ? Convert.ToDateTime(reader["InsuranceEndDate"]).ToCustomFormattedDate() : null,
                                IsBackgroundVerificationCompleted = (!reader.IsDBNull(reader.GetOrdinal("IsBackgroundVerficationCompleted"))) ? Convert.ToBoolean(reader["IsBackgroundVerficationCompleted"]) : false,
                                IsPhysicalVerificationCompleted = (!reader.IsDBNull(reader.GetOrdinal("IsPhysicalVerificationCompleted"))) ? Convert.ToBoolean(reader["IsPhysicalVerificationCompleted"]) : false,
                                BackgroundVerificationAgencyName = reader["BackgroundVerificationAgencyName"]?.ToString()
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

        public async Task<bool> DeleteEmployeeAttachmentAsync(EmployeeAttachment employeeAttachment)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[Sp_DeleteEmployeeAttachments]";

                    command.Parameters.Add(new SqlParameter("@EmplID", employeeAttachment.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@AttachmentID", employeeAttachment.AttachmentID));
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
        public async Task<bool> AddEmployeeAttachmentAsync(EmployeeAttachment employeeAttachment)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[Sp_SaveEmployeeAttachments]";

                    command.Parameters.Add(new SqlParameter("@EmpID", employeeAttachment.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@AttachmentName", employeeAttachment.AttachmentName));
                    command.Parameters.Add(new SqlParameter("@DocumentTypeID", employeeAttachment.AttachmentTypeID));
                    command.Parameters.Add(new SqlParameter("@NumberOfKB", employeeAttachment.NumberOfKB));
                    command.Parameters.Add(new SqlParameter("@ContentType", employeeAttachment.ContentType));
                    command.Parameters.Add(new SqlParameter("@Resource", employeeAttachment.Content));
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
        public async Task<IEnumerable<EmployeeAttachment>> GetEmployeeAttachmentsByEmplID(int emplID)
        {

            try
            {
                List<EmployeeAttachment> attachments = new List<EmployeeAttachment>();
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[sp_GetEmployeeAttachmentsByID]";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@EmpID";
                    parameter.Value = emplID;
                    parameter.DbType = DbType.Int32;
                    command.Parameters.Add(parameter);

                    _dbConnection.Open();
                    using (IDataReader reader = await Task.Run(() => command.ExecuteReader()))
                    {
                       
                        if (reader.Read())
                        {   
                            do
                            {
                                attachments.Add(new EmployeeAttachment
                                {
                                    //A.ContentType,lkp.LookupName AS DocumentType,AttachmentId ,AttachmentName
                                    ContentType = reader["ContentType"]?.ToString(),
                                    AttachmentType = reader["DocumentType"]?.ToString(),
                                    AttachmentID = Guid.Parse(reader["AttachmentID"].ToString()),
                                    AttachmentName = reader["AttachmentName"]?.ToString(),
                                    EmployeeID = emplID
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

        public async Task<EmployeeAttachment> GetAttachmentContentByAttachmentIDAsync(Guid attachmentID)
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
                            return new EmployeeAttachment
                            {
                                AttachmentID = attachmentID,
                                NumberOfKB =  reader["NumberOfKB"].ToString(),
                                ContentType = reader["ContentType"].ToString(),
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


        public async Task<IEnumerable<DocumentTypes>> GetEmployeeDocumentTypesAsync()
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[sp_GetEmployeeDocumentTypes]";

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
                                    DocumentTypeID =  Convert.ToInt32( reader["DocumentTypeID"]),
                                    DocumentType =  reader["DocumentTypeName"].ToString()
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
        public async Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync(EmployeeBackgroundVerification backgroundVerification)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateEmployeeBackgroundVerificationDetails]";


                    command.Parameters.Add(new SqlParameter("@EmployeeID", backgroundVerification.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@IsBackgroundVerificationCompleted", backgroundVerification.IsBackgroundVerificationCompleted));
                    command.Parameters.Add(new SqlParameter("@IsPhysicalVerificationCompleted", backgroundVerification.IsPhysicalVerificationCompleted));
                    command.Parameters.Add(new SqlParameter("@BackgroundVerificationAgencyName", backgroundVerification.BackgroundVerificationAgencyName));

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

        public async Task<bool> UpdateEmployeeContactInformationAsync(EmployeeContactInformation contactInformation)
        {




            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateEmployeeContactInformation]";


                    command.Parameters.Add(new SqlParameter("@EmployeeID", contactInformation.EmployeeID));
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


        public async Task<bool> UpdateEmployeeFinancialDetailsAsync(EmployeeFinancialDetails financialDetails)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateEmployeeFinancialDetails]";


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

        public async Task<CreateEmployeeResponse> ValidateAndCreateEmployeeIDAsync(ValidateAndCreateEmployeeIDRequest request)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ValidateAndCreateEmployeeID]";

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
                            var response = new CreateEmployeeResponse
                            {
                                Message = reader["Message"].ToString(),
                                Proceedfurther = Convert.ToBoolean(reader["ProceedFurther"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"])
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

        public async Task<bool> UpdateEmployeePersonalInformation(ValidateAndCreateEmployeeIDRequest request)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateEmployeePersonalInformation]";
                    command.Parameters.Add(new SqlParameter("@EmployeeID", request.EmployeeID));
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



        public async Task<bool> ActivateOrDeactivateEmployeeAsync(ActivateOrDeactivateEmployeeRequest request)
        {

            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ActivateOrDeActivateEmployee]";


                    command.Parameters.Add(new SqlParameter("@EmployeeID", request.EmployeeID));
                    command.Parameters.Add(new SqlParameter("@ActivateEmployee", request.ActivateEmployee));
                    command.Parameters.Add(new SqlParameter("@LoggedInUser", request.LoggedInUser));
                    command.Parameters.Add(new SqlParameter("@Comments", request.Comments));
                    command.Parameters.Add(new SqlParameter("@Password", request.Password));
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
    }
}
