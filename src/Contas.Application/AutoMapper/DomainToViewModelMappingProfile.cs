using AutoMapper;
using Contas.Application.ViewModels;
using Contas.Domain.Contas;
using Contas.Domain.Usuarios;

namespace Contas.Application.AutoMapper
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