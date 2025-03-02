using Elfie.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
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

        public async Task<IEnumerable<Employee>> GetAllActiveEmployeesAsync()
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
                            List<Employee> employees = new List<Employee>();
                            do
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                employees.Add(new Employee
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
                                    AccountNumber = reader["AccountNumber"]?.ToString(),
                                    BankName = reader["BankName"]?.ToString(),
                                    IFSCCode = reader["IFSCCode"]?.ToString(),
                                    DateOfBirth = reader["DateOfBirth"] != null ? Convert.ToDateTime(reader["DateOfBirth"]).ToCustomFormattedDate() : null,
                                    CreatedDate = reader["CreatedDate"] != null ? Convert.ToDateTime(reader["CreatedDate"]).ToCustomFormattedDate() : null,
                                    ModifiedDate = reader["ModifiedDate"] != null ? Convert.ToDateTime(reader["ModifiedDate"]).ToCustomFormattedDate() : null,
                                    CreatedByID = reader["CreatedByID"] != null ? Convert.ToInt32(reader["CreatedByID"]) : -1,
                                    ModifiedByID = reader["ModifiedByID"] != null ? Convert.ToInt32(reader["ModifiedByID"]) : -1,
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
                                    InsuranceStartDate = reader["InsuranceStartDate"] != null ? Convert.ToDateTime(reader["InsuranceStartDate"]).ToCustomFormattedDate() : null,
                                    InsuranceEndDate = reader["InsuranceEndDate"] != null ? Convert.ToDateTime(reader["InsuranceEndDate"]).ToCustomFormattedDate() : null,
                                    IsBackgroundVerificationCompleted = reader["IsBackgroundVerficationCompleted"] != null ? Convert.ToBoolean(reader["IsBackgroundVerficationCompleted"]) : false,
                                    IsPhysicalVerificationCompleted = reader["IsPhysicalVerificationCompleted"] != null ? Convert.ToBoolean(reader["IsPhysicalVerificationCompleted"]) : false,
                                    BackgroundVerificationAgencyName = reader["BackgroundVerificationAgencyName"]?.ToString()
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
                                AccountNumber = reader["AccountNumber"]?.ToString(),
                                BankName = reader["BankName"]?.ToString(),
                                IFSCCode = reader["IFSCCode"]?.ToString(),
                                DateOfBirth = reader["DateOfBirth"] != null ? Convert.ToDateTime(reader["DateOfBirth"]).ToCustomFormattedDate() : null,
                                CreatedDate = reader["CreatedDate"] != null ? Convert.ToDateTime(reader["CreatedDate"]).ToCustomFormattedDate() : null,
                                ModifiedDate = reader["ModifiedDate"] != null ? Convert.ToDateTime(reader["ModifiedDate"]).ToCustomFormattedDate() : null,
                                CreatedByID = reader["CreatedByID"] != null ? Convert.ToInt32(reader["CreatedByID"]) : -1,
                                ModifiedByID = reader["ModifiedByID"] != null ? Convert.ToInt32(reader["ModifiedByID"]) : -1,
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
                                InsuranceStartDate = reader["InsuranceStartDate"] != null ? Convert.ToDateTime(reader["InsuranceStartDate"]).ToCustomFormattedDate() : null,
                                InsuranceEndDate = reader["InsuranceEndDate"] != null ? Convert.ToDateTime(reader["InsuranceEndDate"]).ToCustomFormattedDate() : null,
                                IsBackgroundVerificationCompleted = reader["IsBackgroundVerficationCompleted"] != null ? Convert.ToBoolean(reader["IsBackgroundVerficationCompleted"]) : false,
                                IsPhysicalVerificationCompleted = reader["IsPhysicalVerificationCompleted"] != null ? Convert.ToBoolean(reader["IsPhysicalVerificationCompleted"]) : false,
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

        public async Task<int> InsertEmployeeAsync(Employee employee)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[SaveEmployeeInformation]";
                    command.Parameters.Add(Utils.AddParameter(command, "@FirstName", employee.FirstName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@FirstName", employee.FirstName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@LastName", employee.LastName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@BloodGroup", employee.BloodGroup, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Gender", employee.Gender, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@PersonalEmailID", employee.PersonalEmailID, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@ContactNumber", employee.ContactNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AlternativeContactNumber", employee.AlternativeContactNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactNumber", employee.EmergencyContactNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AadhaarNumber", employee.AadhaarNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@PanNumber", employee.PanNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AccountNumber", employee.AccountNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@BankName", employee.BankName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@IFSCCode", employee.IFSCCode, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@DateOfBirth", employee.DateOfBirth != null ? employee.DateOfBirth.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@CreatedDate", employee.CreatedDate != null ? employee.CreatedDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@ModifiedDate", employee.ModifiedDate != null ? employee.ModifiedDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@CreatedByID", employee.CreatedByID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@ModifiedByID", employee.ModifiedByID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@AlternativeEmail", employee.AlternativeEmail, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactName", employee.EmergencyContactName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactRelation", employee.EmergencyContactRelation, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactPersonID", employee.EmergencyContactPersonID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@AddressLine1", employee.AddressLine1, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AddressLine2", employee.AddressLine2, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@State", employee.State, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@City", employee.City, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Zip", employee.Zip, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Landmark", employee.Landmark, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@HighestDegreeEarned", employee.HighestDegreeEarned, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@PreviousOrgName", employee.PreviousOrgName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@UANNumber", employee.UANNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsurancePolicyNumber", employee.InsurancePolicyNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsurerName", employee.InsurerName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsuranceStartDate", employee.InsuranceStartDate != null ? employee.InsuranceStartDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsuranceEndDate", employee.InsuranceEndDate != null ? employee.InsuranceEndDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@IsBackgroundVerificationCompleted", employee.IsBackgroundVerificationCompleted, DbType.Boolean));
                    command.Parameters.Add(Utils.AddParameter(command, "@IsPhysicalVerificationCompleted", employee.IsPhysicalVerificationCompleted, DbType.Boolean));
                    command.Parameters.Add(Utils.AddParameter(command, "@BackgroundVerificationAgencyName", employee.BackgroundVerificationAgencyName, DbType.String));

                    _dbConnection.Open();
                    var result = await Task.Run(() => command.ExecuteScalar());
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to create the employee", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[UpdateEmployee]";

                    command.Parameters.Add(Utils.AddParameter(command, "@EmployeeID", employee.EmployeeID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@FirstName", employee.FirstName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@LastName", employee.LastName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@BloodGroup", employee.BloodGroup, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Gender", employee.Gender, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@PersonalEmailID", employee.PersonalEmailID, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@ContactNumber", employee.ContactNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AlternativeContactNumber", employee.AlternativeContactNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactNumber", employee.EmergencyContactNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AadhaarNumber", employee.AadhaarNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@PanNumber", employee.PanNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AccountNumber", employee.AccountNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@BankName", employee.BankName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@IFSCCode", employee.IFSCCode, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@DateOfBirth", employee.DateOfBirth != null ? employee.DateOfBirth.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@CreatedDate", employee.CreatedDate != null ? employee.CreatedDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@ModifiedDate", employee.ModifiedDate != null ? employee.ModifiedDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@CreatedByID", employee.CreatedByID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@ModifiedByID", employee.ModifiedByID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@AlternativeEmail", employee.AlternativeEmail, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactName", employee.EmergencyContactName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactRelation", employee.EmergencyContactRelation, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@EmergencyContactPersonID", employee.EmergencyContactPersonID, DbType.Int32));
                    command.Parameters.Add(Utils.AddParameter(command, "@AddressLine1", employee.AddressLine1, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@AddressLine2", employee.AddressLine2, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@State", employee.State, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@City", employee.City, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Zip", employee.Zip, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Landmark", employee.Landmark, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@HighestDegreeEarned", employee.HighestDegreeEarned, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@PreviousOrgName", employee.PreviousOrgName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@UANNumber", employee.UANNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsurancePolicyNumber", employee.InsurancePolicyNumber, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsurerName", employee.InsurerName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsuranceStartDate", employee.InsuranceStartDate != null ? employee.InsuranceStartDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@InsuranceEndDate", employee.InsuranceEndDate != null ? employee.InsuranceEndDate.Value : DBNull.Value, DbType.DateTime));
                    command.Parameters.Add(Utils.AddParameter(command, "@IsBackgroundVerificationCompleted", employee.IsBackgroundVerificationCompleted, DbType.Boolean));
                    command.Parameters.Add(Utils.AddParameter(command, "@IsPhysicalVerificationCompleted", employee.IsPhysicalVerificationCompleted, DbType.Boolean));
                    command.Parameters.Add(Utils.AddParameter(command, "@BackgroundVerificationAgencyName", employee.BackgroundVerificationAgencyName, DbType.String));

                    _dbConnection.Open();
                    await Task.Run(() => command.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to update the employee", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }


        public async Task<bool> AddEmployeeAttachmentAsync(AddEmployeeAttachment employeeAttachment)
        {
            try
            {
                using (IDbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[AddEmployeeAttachment]";


                    command.Parameters.Add(new SqlParameter("@EmpID", employeeAttachment.EmpID));
                    command.Parameters.Add(new SqlParameter("@AttachmentTypeID", employeeAttachment.AttachmentTypeID));
                    command.Parameters.Add(new SqlParameter("@ReportTypeID", employeeAttachment.ReportTypeID));
                    command.Parameters.Add(new SqlParameter("@NumberOfKB", employeeAttachment.NumberOfKB));
                    command.Parameters.Add(new SqlParameter("@Resource", employeeAttachment.Resource));
                    command.Parameters.Add(new SqlParameter("@Tags", employeeAttachment.Tags));
                    command.Parameters.Add(new SqlParameter("@ContentType", employeeAttachment.ContentType));
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

        public async Task<bool> UpdateEmployeeBackgroundVerificationDetailsAsync( BackgroundVerification    backgroundVerification)
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

        public async Task<bool> UpdateEmployeeContactInformationAsync(ContactInformation contactInformation)
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
 

        public async Task<bool> UpdateEmployeeFinancialDetailsAsync(FinancialDetails financialDetails)
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
 


    }
}
