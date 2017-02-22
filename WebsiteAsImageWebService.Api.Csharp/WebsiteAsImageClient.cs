using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;

namespace WebsiteAsImageWebService.Api
{
    public class WebsiteAsImageClient
    {
        private string Authority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authority"></param>
        /// <remarks>
        /// Currently no support for https
        /// </remarks>
        public WebsiteAsImageClient(string authority)
        {

            if (authority.Contains("http://") || authority.Contains("https://"))
                throw new ArgumentException("Authority doesn't contain a protocol ( or scheme in .net terminalogy), eg. www.example.com or example.com:64640");

            this.Authority = authority;
        }


        public async Task<System.IO.Stream> GetScreenshot(string UrlToGrab)
        {
            var compressClient = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            using (var client = new HttpClient(compressClient))
            {

                client.DefaultRequestHeaders.ExpectContinue = false;

                string encodedUrlParameter = Uri.EscapeDataString((UrlToGrab));
                string encodedFileName = encodedUrlParameter + ".png";

                var requestMessage = new HttpRequestMessage() {
                    RequestUri = new Uri(string.Format("http://{0}/thumb/{1}", Authority, encodedUrlParameter)),
                     Method = HttpMethod.Get
                };

                HttpResponseMessage response = response = await client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;

                    var dicHeaders = new Dictionary<string, string>();
                    foreach (var header in response.Headers)
                    {
                        dicHeaders.Add(header.Key, header.Value.ToString());
                    }

                    return await content.ReadAsStreamAsync();
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                        "Http Status= {0} - {1}", (int)response.StatusCode,
                       response.StatusCode.ToString()));
                }
            }

        }
    }
}