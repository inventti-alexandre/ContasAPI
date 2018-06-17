using AutoMapper;
using Contas.Domain.Contas.Commands;
using Contas.Domain.Contas.Repository;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Contas.Services.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contas.Services.Api.Controllers
{
    public class ContasController : BaseController
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ContasController(INotificationHandler<DomainNotification> notifications, IUser user, IContaRepository contaRepository, IMapper mapper, IMediatorHandler mediator) : base(notifications, user, mediator)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("contas")]
        [Authorize(Policy = "PodeVisualizar")]
        public IEnumerable<ContaViewModel> Get(DateTime? data, string key)
        {
            return _mapper.Map<IEnumerable<ContaViewModel>>(_contaRepository.ObterContasPorUsuario(IdUsuario, data, key));
        }

        [HttpGet]
        [Route("contas/{id:guid}")]
        [Authorize(Policy = "PodeVisualizar")]
        public ContaViewModel Get(Guid id)
        {
            return _mapper.Map<ContaViewModel>(_contaRepository.ObterPorId(id));
        }

        [HttpGet]
        [Route("contas/categorias")]
        [Authorize(Policy = "PodeVisualizar")]
        public IEnumerable<CategoriaViewModel> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(_contaRepository.ObterCategorias());
        }

        [HttpGet]
        [Route("contas/data-mais-antiga")]
        [Authorize(Policy = "PodeVisualizar")]
        public DateTime? ObterDataMaisAntiga()
        {
            return _contaRepository.ObterDataMaisAntiga();
        }

        [HttpPost]
        [Route("contas")]
        [Authorize(Policy = "PodeAlterar")]
        public IActionResult Post([FromBody]ContaViewModel contaViewModel)
        {
            if (!IsModelStateValid()) return Response();
            var contaCommand = _mapper.Map<RegistrarContaCommand>(contaViewModel);
            _mediator.EnviarComando(contaCommand);
            return Response(contaCommand);
        }

        [HttpPut]
        [Route("contas")]
        [Authorize(Policy = "PodeAlterar")]
        public IActionResult Put(Guid id, [FromBody] ContaViewModel contaViewModel)
        {
            if (!IsModelStateValid()) return Response();
            var contaCommand = _mapper.Map<AtualizarContaCommand>(contaViewModel);
            _mediator.EnviarComando(contaCommand);
            return Response(contaCommand);
        }

        [HttpDelete]
        [Route("contas/{id:guid}")]
        [Authorize(Policy = "PodeAlterar")]
        public IActionResult Delete(Guid id)
        {
            var contaViewModel = new ContaViewModel { Id = id };
            var contaCommand = _mapper.Map<ExcluirContaCommand>(contaViewModel);
            _mediator.EnviarComando(contaCommand);
            return Response(contaCommand);
        }
    }
}