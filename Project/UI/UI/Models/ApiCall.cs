using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ApiCall
    {
        public static async Task<DataTable> PostInfo(UserVM requestObj, string extention)
        {
            // Initialization.  
            DataTable responseObj = new DataTable();

            // Posting.  
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44362/api/";
                // Setting Base address.  
                client.BaseAddress = new Uri(url);

                // Setting content type.                   
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP POST  
                //response = await client.PostAsJsonAsync(extention, requestObj).ConfigureAwait(false);
                //HttpClient c = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
                //response = await client.PostAsJsonAsync(extention + "?emailOrMobile=" + requestObj.emailOrMobile + "?password=" + requestObj.password, null).ConfigureAwait(false); // returns 200

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<DataTable>(result);
                }
            }


            //HttpClient c = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
            //var x = c.PostAsync("https://localhost:44362/api/values?emailOrMobile=" + "test", sc).Result; // returns 200


            return responseObj;
        }
    }
}
