using Portfolio_Api.DataDTO;
using Portfolio_Api.Models;

namespace Portfolio_Api.Interface
{
    public interface IAdminRepository
    {
        public Task<CommonResponse> Admin_Login(AdminModel admin);
        public Task<AdminModel> UserData_ById(int Userid);
        public Task<CommonResponse> Admin_Upsert(AdminModel admin);
        public Task<CommonResponse> Admin_ChangePassword(AdminModel admin);
    }
}
