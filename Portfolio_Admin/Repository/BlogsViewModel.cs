using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using Portfolio_Api.Models;
using System.Net.Http.Headers;
using System.Text;

namespace Portfolio_Admin.Repository
{
    public class BlogsViewModel
    {
        private readonly HttpClient client;
        private readonly IClientHelper _clientHelper;
        private readonly IDataProtector _protector;

        public BlogsViewModel(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper ??
         throw new ArgumentNullException(nameof(clientHelper));
            _protector = dataProtectionProvider.CreateProtector("MyCookieEncryptionPurpose");
        }

        public async Task<string> UploadFileAsync(IFormFile file, string uploadPath)
        {
            try
            {
                var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
                if (!File.Exists(tempFilePath))
                {
                    // Save the IFormFile to the temporary file path
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                var client = new HttpClient(handler);
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44313/api/FileUpload/upload?filepath=" + uploadPath);
                request.Headers.Add("accept", "*/*");
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(tempFilePath)), "file", file.FileName);
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                return (responseContent);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error uploading file: {ex.Message}");
                return ("");
            }
        }

        public async Task<CommonResponseModel> AddBlog(Blogs blogs)
        {
            try
            {

                var postsModelDTO = new BlogsDTO
                {
                    Id = blogs.Id,
                    Blog_Image_String = blogs.Blog_Image_String,
                    Blog_Title = blogs.Blog_Title,
                    Blog_Content = blogs.Blog_Content,
                    Short_Description = blogs.Short_Description,
                    PublishedDate = blogs.PublishedDate,
                    Visiable = blogs.Visiable,
                    CreatedBy = blogs.CreatedBy,
                    UpdatedBy = blogs.UpdatedBy,
                };

                CommonResponseModel res = new CommonResponseModel();
                HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

                // Create the request content
                var serializedItemToCreate = JsonConvert.SerializeObject(postsModelDTO);

                // Make a POST request
                var response = await client.PostAsync("api/Blogs/BlogPost_Insert", new StringContent(serializedItemToCreate,
                                            System.Text.Encoding.Unicode,
                                            "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error in BlogPost: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return res;
            }
            catch (HttpRequestException ex) when (ex.InnerException != null)
            {
                Console.WriteLine($"Error in BlogPost: {ex.Message}, Inner Exception: {ex.InnerException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BlogPost: {ex.Message}");
                throw;
            }
        }

        public async Task<CommonResponseModel> DeleteBlog(Blogs blogs)
        {
            CommonResponseModel res = new CommonResponseModel();
            HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

            // Assuming blogs.Id and blogs.DeletedBy are the identifiers you want to pass
            var deleteUrl = $"api/Blogs/BlogPost_Delete?id={blogs.Id}&deletedBy={blogs.DeletedBy}";

            var response = await client.DeleteAsync(deleteUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<CommonResponseModel>(content);
            }
            return res;
        }

        public async Task<List<BlogsDTO>> GetAllBlog()
        {
            List<BlogsDTO> res = new List<BlogsDTO>();

            HttpClient client = await _clientHelper.PrepareAuthenticatedClient();
            var response = await client.GetAsync("api/Blogs/BlogPost_All/");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<List<BlogsDTO>>(content);
            }

            return res;
        }

        public async Task<BlogsDTO> GetByIdBlog(int id)
        {
            try
            {
                BlogsDTO res = null;


                HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

                var response = await client.GetAsync("api/Blogs/BlogPost_ById/" + id + "");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<BlogsDTO>(content);
                }

                return res;
            }
            catch (Exception ex) 
            {

                throw;
            }
        }



    }
}
