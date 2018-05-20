using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contas.Infrastructure.CrossCutting.Identity.Models.AccountViewModels;
using Contas.Tests.Api.Integration.DTO;
using Newtonsoft.Json;

namespace Contas.Tests.Api.Integration
{
    public class UserUtils
    {
        public static async Task<Result> RealizarLogin(HttpClient client)
        {
            var user = new LoginViewModel
            {
                Email = "aurelion@email.com",
                Senha = "P@ssw0rd"
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/v1/usuario/login", postContent);

            var postResult = await response.Content.ReadAsStringAsync();
            var userResult = JsonConvert.DeserializeObject<UsuarioDTO>(postResult);
            return userResult.data.result;
        }
    }
}