using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio_Admin.Helper
{
    public interface IClientHelper
    {
        Task<HttpClient> PrepareAuthenticatedClient();
        Task<HttpClient> PreparePatientCommAuthenticatedClient(string scope);
    }
}
