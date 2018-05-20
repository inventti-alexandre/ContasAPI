using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contas.Infrastructure.CrossCutting.Identity.Models.AccountViewModels;
using Contas.Tests.Api.Integration.DTO;
using Newtonsoft.Json;
using Xunit;

namespace Contas.Tests.Api.Integration
{
    public class UsuariosControllerIntegrationTests
    {
        public UsuariosControllerIntegrationTests()
        {
            Environment.CriarServidor();
        }

        [Fact]
        public async Task UsuariosController_RegistrarUsuario_RetornarSucesso()
        {
            var usuario = new RegisterViewModel()
            {
                Nome = "Aurelion",
                Sobrenome = "Sol",
                CPF = "01234567890",
                Email = "aurelion@email.com",
                DataNascimento = DateTime.Now,
                Senha = "P@ssw0rd",
                ConfirmSenha = "P@ssw0rd"
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

            var response = await Environment.Client.PostAsync("/api/v1/usuario/registrar", postContent);
            var result = JsonConvert.DeserializeObject<UsuarioDTO>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            var token = result.data.result.access_token;
            Assert.NotEmpty(token);
        }
    }
}