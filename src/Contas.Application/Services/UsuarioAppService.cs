using AutoMapper;
using Contas.Application.Contracts;
using Contas.Application.ViewModels;
using Contas.Domain.Core.Bus;
using Contas.Domain.Usuarios.Commands;
using Contas.Domain.Usuarios.Repository;

namespace Contas.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(IMapper mapper, IBus bus, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _usuarioRepository = usuarioRepository;
        }

        public void Registrar(UsuarioViewModel usuarioViewModel)
        {
            var registroCommand = _mapper.Map<RegistrarUsuarioCommand>(usuarioViewModel);
            _bus.SendCommand(registroCommand);
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }
    }
}