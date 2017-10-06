using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    /// <summary>
    /// Demo client to connect to rest services
    /// </summary>
    class Program
    {
        /// <summary>
        /// add your username / password here
        /// please contact office@hgkv.at for mor information
        /// </summary>
        const string userName = "";
        const string password = "";

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static void Main(string[] args)
        {

            MainAsync().GetAwaiter().GetResult();
        }

        // async main 
        static async Task MainAsync()
        {
            var p = new Program();

            // result = "pong"
            var result = await p.TestLogin();
            // write result
            Console.Write(result);
            // other methods and info can be fount here:  https://db.hgkv.at/doku or https://db.hgkv.at/apiDocumentation/index
            Console.Write("Press Enter to exit!");
            Console.ReadLine();
        }

        /// <summary>
        /// create a simple rest client 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private HttpClient CreateClient(string uri = "https://db.hgkv.at/api/service/400/")
        {
            // create the client and add the login credentials
            var credentials = new NetworkCredential(userName, password);
            var _client = new HttpClient(new HttpClientHandler() { Credentials = credentials, ClientCertificateOptions = ClientCertificateOption.Automatic })
            {

                Timeout = new TimeSpan(0, 10, 0),
                BaseAddress = new Uri(uri)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        /// <summary>
        /// Call a methor on the server
        /// </summary>
        /// <returns></returns>
        private async Task<string> TestLogin()
        {
            var test = CreateClient().GetAsync("ping").Result;
            return await test.Content.ReadAsStringAsync();
        }

       
    }
}
