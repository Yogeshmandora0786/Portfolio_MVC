using Dapper;
using Portfolio_Api.DataDTO;
using Portfolio_Api.Interface;
using Portfolio_Api.Models;
using System.Data;

namespace Portfolio_Api.Repository
{
    public class MyPortfolioRepository : IMyPortfolio
    {
        private readonly DBContext _dBContext;
        public MyPortfolioRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<CommonResponse> Portfolio_Insert(MyPortfolio myPortfolio)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", myPortfolio.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@Portfolio_Image", myPortfolio.Portfolio_Image_String, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Portfolio_Title", myPortfolio.Portfolio_Title, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Short_Description", myPortfolio.Short_Description, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@PublishedDate", myPortfolio.PublishedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@Visiable", myPortfolio.Visiable, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@CreatedBy", myPortfolio.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", myPortfolio.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Portfolio_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CommonResponse> Portfolio_Update(MyPortfolio myPortfolio)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", myPortfolio.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@Portfolio_Image", myPortfolio.Portfolio_Image_String, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Portfolio_Title", myPortfolio.Portfolio_Title, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Short_Description", myPortfolio.Short_Description, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@PublishedDate", myPortfolio.PublishedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@Visiable", myPortfolio.Visiable, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@CreatedBy", myPortfolio.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", myPortfolio.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Portfolio_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CommonResponse> Portfolio_Delete(MyPortfolio myPortfolio)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", myPortfolio.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@DeletedBy", myPortfolio.DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Portfolio_Delete", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<MyPortfolio>> Portfolio_All()
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    var items = await connection.QueryAsync<MyPortfolio>("Proc_Get_Portfolio", commandType: CommandType.StoredProcedure);
                    return items.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<MyPortfolio> Portfolio_ById(int blogid)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", blogid, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var user = await connection.QueryFirstOrDefaultAsync<MyPortfolio>("Proc_Get_Portfolio", param, commandType: CommandType.StoredProcedure);
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
