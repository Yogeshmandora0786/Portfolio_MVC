using Dapper;
using Portfolio_Api.DataDTO;
using Portfolio_Api.Interface;
using Portfolio_Api.Models;
using System.Data;

namespace Portfolio_Api.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DBContext _dBContext;
        public AdminRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<CommonResponse> Admin_ChangePassword(AdminModel adminModel)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", adminModel.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@OldPassword", adminModel.OldPassword, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@NewPassword", adminModel.Password, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", adminModel.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Admin_ChangePassword", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CommonResponse> Admin_Login(AdminModel admin)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Email", admin.Email, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", admin.Password, dbType: DbType.String, direction: ParameterDirection.Input);

                    var task = connection.QueryMultiple("Proc_AdminLogin", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
        }

        public async Task<CommonResponse> Admin_Upsert(AdminModel admin)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", admin.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@FirstName", admin.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@LastName", admin.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Email", admin.Email, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@PhoneNo", admin.PhoneNo, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", admin.Password, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", admin.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Admin_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CommonResponse> UserData_ByEmail(string Email)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Email", Email, dbType: DbType.String, direction: ParameterDirection.Input);
                    var user = await connection.QueryFirstOrDefaultAsync<CommonResponse>("Proc_Admin_By_Email", param, commandType: CommandType.StoredProcedure);
                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
            throw new NotImplementedException();
        }

        public async Task<CommonResponse> ChangePassword_ByEmail(AdminModel adminModel)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Email", adminModel.Email, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@NewPassword", adminModel.Password, dbType: DbType.String, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("ChangePassword_Admin_By_Email", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
            throw new NotImplementedException();
        }

        public async Task<AdminModel> UserData_ById(int Userid)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", Userid, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var user = await connection.QueryFirstOrDefaultAsync<AdminModel>("Proc_Admin_ById", param, commandType: CommandType.StoredProcedure);
                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
