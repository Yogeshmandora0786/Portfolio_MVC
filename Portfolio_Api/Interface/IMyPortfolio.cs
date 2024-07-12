using Portfolio_Api.DataDTO;
using Portfolio_Api.Models;

namespace Portfolio_Api.Interface
{
    public interface IMyPortfolio
    {
        public Task<CommonResponse> Portfolio_Insert(MyPortfolio myPortfolio);
        public Task<CommonResponse> Portfolio_Update(MyPortfolio myPortfolio);
        public Task<CommonResponse> Portfolio_Delete(MyPortfolio myPortfolio);
        public Task<List<MyPortfolio>> Portfolio_All();
        public Task<MyPortfolio> Portfolio_ById(int Portfolio);
    }
}
