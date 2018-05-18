using AutoMapper;
using Contas.Domain.Contas;
using Contas.Domain.Usuarios;
using Contas.Services.Api.ViewModels;

namespace Contas.Services.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Conta, ContaViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}