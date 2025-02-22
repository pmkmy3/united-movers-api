using System.Data;
using united_movers_api.Common;
using united_movers_api.Models;

namespace united_movers_api.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _dbConnection;

        public AuthRepository(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public LoginResponse Authenticate(LoginRequest request)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ValidateLogin]";
                    command.Parameters.Add(Utils.AddParameter(command, "@UserName", request.UserName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Password", request.Password, DbType.String));
                    var response = new LoginResponse() { EmployeeId = 0, IsAuthenticated = false, IsTempPassword = false, Message = "User name or Password might be incorrect" };
                    _dbConnection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        response.Message = "";
                        response.IsAuthenticated = true;
                        response.IsTempPassword = reader["IsTempPassword"] != null ? Convert.ToBoolean(reader["IsTempPassword"]) : true;
                        response.EmployeeId = reader["EmployeeId"] != null ? Convert.ToInt32(reader["EmployeeId"]) : -1;
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to authenticate user", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public bool ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ChangePassword]";
                    command.Parameters.Add(Utils.AddParameter(command, "@Username", request.UserName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@OldPassword", request.OldPassword, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@NewPassword", request.NewPassword, DbType.String));
                    _dbConnection.Open();
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to change password", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                }
            }
        }

        public bool ForgotPassword(ChangePasswordRequest request)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ForgotPassword]";
                    command.Parameters.Add(Utils.AddParameter(command, "@UserName", request.UserName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@TempPassword", request.NewPassword, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@SecretPin", request.SecurityQuestion, DbType.String));
                    _dbConnection.Open();
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to reset password", ex);
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
