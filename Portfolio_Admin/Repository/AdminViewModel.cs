using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using Portfolio_Admin.Helper;
using Portfolio_Admin.Models;
using System.Text;

namespace Portfolio_Admin.Repository
{
    public class AdminViewModel
    {
        private readonly HttpClient client;
        private readonly IClientHelper _clientHelper;
        private readonly IDataProtector _protector;

        public AdminViewModel(IClientHelper clientHelper, IDataProtectionProvider dataProtectionProvider)
        {
            _clientHelper = clientHelper ??
         throw new ArgumentNullException(nameof(clientHelper));
            _protector = dataProtectionProvider.CreateProtector("MyCookieEncryptionPurpose");
        }

        public async Task<CommonResponseModel> AdminLogin(Admin admin)
        {
            try
            {
                CommonResponseModel res = new CommonResponseModel();
                HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

                // Create the request content
                var json = JsonConvert.SerializeObject(admin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make a POST request
                var response = await client.PostAsync("api/Admin/AdminLogin", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error in AdminLogin: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return res;
            }
            catch (HttpRequestException ex) when (ex.InnerException != null)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}, Inner Exception: {ex.InnerException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}");
                throw;
            }
        }

        public async Task<Admin> GetDataFromUserId(int UserId)
        {
            Admin res = new Admin();
            HttpClient client = await _clientHelper.PrepareAuthenticatedClient();
            var response = await client.GetAsync("api/Admin/UserData_ById/" + UserId + "");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Admin>(content);
            }
            return res;
        }

        public async Task<CommonResponseModel> ForgetPasswordByEmail(string forgetemail)
        {
            CommonResponseModel res = new CommonResponseModel();
            HttpClient client = await _clientHelper.PrepareAuthenticatedClient();
            var response = await client.GetAsync("api/Admin/UserData_ByEmail/" + forgetemail + "");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<CommonResponseModel>(content);
            }
            return res;
        }

        public async Task<CommonResponseModel> Admin_Upsert(Admin admin)
        {
            try
            {
                CommonResponseModel res = new CommonResponseModel();
                HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

                // Create the request content
                var json = JsonConvert.SerializeObject(admin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make a POST request
                var response = await client.PutAsync("api/Admin/Admin_Upsert", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error in AdminLogin: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return res;
            }
            catch (HttpRequestException ex) when (ex.InnerException != null)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}, Inner Exception: {ex.InnerException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}");
                throw;
            }
        }

        public async Task<CommonResponseModel> Admin_ChangePassword(Admin admin)
        {
            try
            {
                CommonResponseModel res = new CommonResponseModel();
                HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

                // Create the request content
                var json = JsonConvert.SerializeObject(admin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make a POST request
                var response = await client.PutAsync("api/Admin/Admin_ChangePassword", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error in AdminLogin: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return res;
            }
            catch (HttpRequestException ex) when (ex.InnerException != null)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}, Inner Exception: {ex.InnerException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}");
                throw;
            }
        }

        public async Task<CommonResponseModel> ChangePassword_ByEmail(Admin admin)
        {
            try
            {
                CommonResponseModel res = new CommonResponseModel();
                HttpClient client = await _clientHelper.PrepareAuthenticatedClient();

                // Create the request content
                var json = JsonConvert.SerializeObject(admin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make a POST request
                var response = await client.PutAsync("api/Admin/ChangePassword_ByEmail", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<CommonResponseModel>(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error in AdminLogin: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return res;
            }
            catch (HttpRequestException ex) when (ex.InnerException != null)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}, Inner Exception: {ex.InnerException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AdminLogin: {ex.Message}");
                throw;
            }
        }

    }
}
