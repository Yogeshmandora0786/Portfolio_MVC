using Dapper;
using Portfolio_Api.DataDTO;
using Portfolio_Api.Interface;
using Portfolio_Api.Models;
using System.Data;

namespace Portfolio_Api.Repository
{
    public class MyBlogRepository : IMyBlogs
    {
        private readonly DBContext _dBContext;
        public MyBlogRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<CommonResponse> BlogPost_Insert(MyBlogs blogPosts)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", blogPosts.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@Blog_Image", blogPosts.Blog_Image_String, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Blog_Title", blogPosts.Blog_Title, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Blog_Content", blogPosts.Blog_Content, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Short_Description", blogPosts.Short_Description, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@PublishedDate", blogPosts.PublishedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@Visiable", blogPosts.Visiable, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@CreatedBy", blogPosts.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", blogPosts.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Posts_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CommonResponse> BlogPost_Update(MyBlogs blogPosts)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", blogPosts.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@Blog_Image", blogPosts.Blog_Image, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Blog_Title", blogPosts.Blog_Title, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Blog_Content", blogPosts.Blog_Content, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Short_Description", blogPosts.Short_Description, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@PublishedDate", blogPosts.PublishedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@Visiable", blogPosts.Visiable, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@CreatedBy", blogPosts.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", blogPosts.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Posts_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<CommonResponse> BlogPost_Delete(MyBlogs blogPosts)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", blogPosts.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@DeletedBy", blogPosts.DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var task = connection.QueryMultiple("Proc_Posts_Delete", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<CommonResponse>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<MyBlogs>> BlogPost_All()
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    var items = await connection.QueryAsync<MyBlogs>("Proc_Get_Posts", commandType: CommandType.StoredProcedure);
                    return items.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<MyBlogs> BlogPost_ById(int blogid)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponse commonResponse = new CommonResponse();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", blogid, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var user = await connection.QueryFirstOrDefaultAsync<MyBlogs>("Proc_Get_Posts", param, commandType: CommandType.StoredProcedure);
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
