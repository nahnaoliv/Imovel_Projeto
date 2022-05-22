using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImovelWEB.Services
{
    public class HttpWebServices
    {
        private string _apiUrl { get; } //atributo de consulta.

        public HttpWebServices(string url)
        {
            _apiUrl = url;
        }

        //GET
        public async Task<string> GetAsync(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(_apiUrl + endPoint);

            request.ContentType = "application/json";
            request.Method = "GET";

            var response = await request.GetResponseAsync() as HttpWebResponse; //resposta

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }

            return null;
        }

        public async Task<string> PostAsync(string endPoint, string jsonData = "", string methodType = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_apiUrl + endPoint);

            request.ContentType = "application/json";
            request.Method = methodType;
            request.ContentLength = dataBytes.Length;

            using (Stream requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            var response = request.GetResponse() as HttpWebResponse;

            if (response != null && (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent))
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }

            return null;
        }
    }
}
