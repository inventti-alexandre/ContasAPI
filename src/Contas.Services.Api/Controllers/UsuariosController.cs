using AutoMapper;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Contas.Domain.Usuarios.Commands;
using Contas.Domain.Usuarios.Repository;
using Contas.Infrastructure.CrossCutting.Identity.Authorization;
using Contas.Infrastructure.CrossCutting.Identity.Models;
using Contas.Infrastructure.CrossCutting.Identity.Models.AccountViewModels;
using Contas.Services.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contas.Services.Api.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMediatorHandler _mediator;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IMapper _mapper;

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public UsuariosController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory, TokenDescriptor tokenDescriptor, INotificationHandler<DomainNotification> notifications, IUser user, IUsuarioRepository usuarioRepository, IMediatorHandler mediator, IMapper mapper) : base(notifications, user, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger<UsuariosController>();
            _tokenDescriptor = tokenDescriptor;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("usuario/registrar")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, int version)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Contas", "Visualizar"));
                await _userManager.AddClaimAsync(user, new Claim("Contas", "Alterar"));

                var registroCommand = new RegistrarUsuarioCommand(Guid.Parse(user.Id), model.Nome, model.Sobrenome, model.CPF, user.Email, model.DataNascimento);
                await _mediator.EnviarComando(registroCommand);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");
                var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Senha = model.Senha });
                return Response(response);
            }
            AdicionarErrosIdentity(result);
            return Response(model);
        }

        [HttpGet]
        [Authorize(Policy = "PodeVisualizar")]
        [Route("usuario/perfil")]
        public UsuarioViewModel ObterPerfil()
        {
            return _mapper.Map<UsuarioViewModel>(_usuarioRepository.ObterPorId(IdUsuario));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("usuario/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                var response = GerarTokenUsuario(model);
                return Response(response);
            }

            NotificarErro(result.ToString(), "Falha ao realizar o login");
            return Response(model);
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);
            var orgUser = _usuarioRepository.ObterPorId(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    nome = orgUser.Nome,
                    email = orgUser.Email,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }
    }
}