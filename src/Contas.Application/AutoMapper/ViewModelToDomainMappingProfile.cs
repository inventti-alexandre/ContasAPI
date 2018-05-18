using AutoMapper;
using Contas.Application.ViewModels;
using Contas.Domain.Contas.Commands;
using Contas.Domain.Usuarios.Commands;

namespace Contas.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ContaViewModel, RegistrarContaCommand>().ConstructUsing(c => new RegistrarContaCommand(c.Nome, c.Data, c.Valor, c.Parcelado, c.NumeroParcelas,c.Observacao, c.IdCategoria, c.IdUsuario));
            CreateMap<ContaViewModel, AtualizarContaCommand>().ConstructUsing(c => new AtualizarContaCommand(c.Id, c.Nome, c.Data, c.Valor, c.Parcelado, c.NumeroParcelas, c.Observacao, c.IdCategoria, c.IdUsuario));
            CreateMap<ContaViewModel, DesativarContaCommand>().ConstructUsing(c => new DesativarContaCommand(c.Id));

            CreateMap<UsuarioViewModel, RegistrarUsuarioCommand>().ConstructUsing(u => new RegistrarUsuarioCommand(u.Id, u.Nome, u.CPF, u.Email));
        }
    }
}