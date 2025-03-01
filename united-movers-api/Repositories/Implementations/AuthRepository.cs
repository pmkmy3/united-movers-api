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

        public async Task<IDataReader> AuthenticateAsync(LoginRequest request)
        {
            try
            {
                using (var command = _dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[ValidateLogin]";
                    command.Parameters.Add(Utils.AddParameter(command, "@UserName", request.UserName, DbType.String));
                    command.Parameters.Add(Utils.AddParameter(command, "@Password", request.Password, DbType.String));
                    
                    _dbConnection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return reader;
                    }
                    return null;
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
