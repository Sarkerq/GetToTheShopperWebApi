using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core
{
    public static class WebClient
    {
        public static HttpClient client = new HttpClient();

        public static int SetHeaders()
        {
            //client.BaseAddress = new Uri("http://projektnet.mini.pw.edu.pl/GetToTheShopper");
            //client.BaseAddress = new Uri("http://localhost:58960/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return 0;
        }


        public static async Task<T> ReadResponse<T>(string path) where T : class
        {
            T result = null;
            HttpResponseMessage response = await client.GetAsync("http://projektnet.mini.pw.edu.pl/GetToTheShopper/api/" + path).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
            }
            return result;
        }
        public static async Task<bool> ReadPostAsync<T>(string path, T DTO) where T : class
        {
            bool result = false;

            HttpResponseMessage response = await client.PostAsJsonAsync<T>("http://projektnet.mini.pw.edu.pl/GetToTheShopper/api/" + path, DTO).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<bool>().ConfigureAwait(false);
            }
            return result;
        }
        public static async Task<bool> PostAsync<T>(string path, T DTO)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync<T>("http://projektnet.mini.pw.edu.pl/GetToTheShopper/api/" + path, DTO).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        public static async Task<bool> PutAsync<T>(string path, T DTO)
        {

            HttpResponseMessage response = await client.PutAsJsonAsync<T>("http://projektnet.mini.pw.edu.pl/GetToTheShopper/api/" + path, DTO).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        public static async Task<bool> DeleteAsync<T>(string path)
        {

            HttpResponseMessage response = await client.DeleteAsync("http://projektnet.mini.pw.edu.pl/GetToTheShopper/api/" + path).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        //temporary solution
        public static T ReadResponseSync<T>(string path) where T : class
        {
            T result = null;
            HttpResponseMessage response = client.GetAsync("http://projektnet.mini.pw.edu.pl/GetToTheShopper/api/" + path).Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<T>().Result;
            }
            return result;
        }
    }
}
