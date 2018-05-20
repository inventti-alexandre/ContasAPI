using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contas.Services.Api.ViewModels;
using Contas.Tests.Api.Integration.DTO;
using Newtonsoft.Json;
using Xunit;

namespace Contas.Tests.Api.Integration
{
    public class ContasControllerIntegrationTests
    {
        public ContasControllerIntegrationTests()
        {
            Environment.CriarServidor();
        }

        [Fact]
        public async Task ContasController_RegistrarConta_RetornarSucesso()
        {
            var login = await UserUtils.RealizarLogin(Environment.Client);
            var contaViewModel = new ContaViewModel()
            {
                Data = DateTime.Now,
                Nome = "Foo",
                NumeroParcelas = 12,
                Observacao = "",
                Valor = 1200,
                Parcelado = true,
                IdCategoria = new Guid("05CACC06-0BCD-4CCD-9D0E-10FEBA8BFEEB"),
                IdUsuario = new Guid(login.user.id)
            };

            var response = await Environment.Server.CreateRequest("api/v1/contas/nova").AddHeader("Authorization", "Bearer " + login.access_token)
                .And(request => request.Content = new StringContent(JsonConvert.SerializeObject(contaViewModel), Encoding.UTF8, "application/json")).PostAsync();

            var result = JsonConvert.DeserializeObject<ContaDTO>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            Assert.IsType<ContaData>(result.data);
        }
    }
}