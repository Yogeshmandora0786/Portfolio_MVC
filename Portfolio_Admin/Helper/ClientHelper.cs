using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Net.Http.Headers;

namespace Portfolio_Admin.Helper
{
    public class ClientHelper : IClientHelper
    {
        private HttpClient _httpClient;
        //private readonly ITokenAcquisition _tokenAcquisition;

        private readonly string _APIUrl = string.Empty;
        private readonly string _DLPatientCommClientId = string.Empty;
        private readonly string _DLPatientCommClientSecret = string.Empty;
        private readonly string _Instance = string.Empty;
        private readonly string _Tenant = string.Empty;
        private readonly string _Portfolio_APIScope = string.Empty;

        private readonly string _Portfolio_APIBaseAddress = "https://localhost:44313";

        

        public ClientHelper()
        {
           
        }

        public async Task<HttpClient> PrepareAuthenticatedClient()
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => { return true; }
                };

                _httpClient = new HttpClient(handler);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.BaseAddress = new Uri(_Portfolio_APIBaseAddress);
                return _httpClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error preparing HTTP client: {ex.Message}");
                throw;
            }
        }



        public string Authority
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}", "1c42c758-1edb-4a8c-9d0f-8a03eb0a3e95");
            }
        }
        public async Task<HttpClient> PreparePatientCommAuthenticatedClient(string scope)
        {
            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create("97c7ee85-b5f5-425b-a04d-2d7ed5034fc7")
                    .WithClientSecret("gVi8Q~L6RLwUt0furxq9Zj2pGfYQ-WjujUDr_ayj")
                    .WithAuthority(new Uri(Authority))
                    .Build();
            app.AddInMemoryTokenCache();
            string[] scopes = new string[] { 
                scope
            };
            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {

            }
            var httpClient = new HttpClient();
            var defaultRequestHeaders = httpClient.DefaultRequestHeaders;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            return httpClient;
        }
    }
}
