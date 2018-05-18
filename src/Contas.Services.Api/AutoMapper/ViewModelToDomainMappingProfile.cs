using AutoMapper;
using Contas.Domain.Contas.Commands;
using Contas.Domain.Usuarios.Commands;
using Contas.Services.Api.ViewModels;

namespace Contas.Services.Api.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ContaViewModel, RegistrarContaCommand>().ConstructUsing(c => new RegistrarContaCommand(c.Nome, c.Data, c.Valor, c.Parcelado, c.NumeroParcelas, c.Observacao, c.IdCategoria, c.IdUsuario));
            CreateMap<ContaViewModel, AtualizarContaCommand>().ConstructUsing(c => new AtualizarContaCommand(c.Id, c.Nome, c.Data, c.Valor, c.Parcelado, c.NumeroParcelas, c.Observacao, c.IdCategoria, c.IdUsuario));
            CreateMap<ContaViewModel, ExcluirContaCommand>().ConstructUsing(c => new ExcluirContaCommand(c.Id));

            CreateMap<UsuarioViewModel, RegistrarUsuarioCommand>().ConstructUsing(u => new RegistrarUsuarioCommand(u.Id, u.Nome, u.Sobrenome, u.CPF, u.Email, u.DataNascimento));
        }
    }
}