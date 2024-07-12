using Portfolio_Api.DataDTO;
using Portfolio_Api.Models;

namespace Portfolio_Api.Interface
{
    public interface IMyBlogs
    {
        public Task<CommonResponse> BlogPost_Insert(MyBlogs myBlogs);
        public Task<CommonResponse> BlogPost_Update(MyBlogs myBlogs);
        public Task<CommonResponse> BlogPost_Delete(MyBlogs blogPosts);
        public Task<List<MyBlogs>> BlogPost_All();
        public Task<MyBlogs> BlogPost_ById(int blogid);
    }
}
