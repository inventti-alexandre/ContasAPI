using System.Net.Http;
using Contas.Services.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Contas.Tests.Api.Integration
{
    public class Environment
    {
        public static TestServer Server;
        public static HttpClient Client;

        public static void CriarServidor()
        {
            Server = new TestServer(new WebHostBuilder().UseEnvironment("Testing").UseUrls("http://localhost:64998").UseStartup<StartupTests>());
            Client = Server.CreateClient();
        }
    }
}