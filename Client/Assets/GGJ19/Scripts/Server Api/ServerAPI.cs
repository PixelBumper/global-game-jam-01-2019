using System.Net.Http;
using GeneratedServerAPI;

namespace GGJ19.Scripts.Server_Api
{
    public class ServerApi :Client
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        public static ServerApi Instance { get; } = new ServerApi("http://localhost:8080", HttpClient);

        private ServerApi(string baseUrl, HttpClient httpClient) : base(baseUrl, httpClient)
        {
        }
    }
}
