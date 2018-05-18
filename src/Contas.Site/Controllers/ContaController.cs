using Contas.Application.Contracts;
using Contas.Application.ViewModels;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Contas.Site.Controllers
{
    public class ContaController : BaseController
    {
        private readonly IContaAppService _contaAppService;

        public ContaController(IContaAppService contaAppService, IDomainNotificationHandler<DomainNotification> notification, IUser user) : base (notification, user)
        {
            _contaAppService = contaAppService;
        }

        [Route("")]
        [Route("contas")]
        public IActionResult Index()
        {
            return View(_contaAppService.ObterTodos());
        }

        [Authorize(Policy = "PodeVisualizar")]
        [Route("contas/minhas-contas")]
        public IActionResult MinhasContas()
        {
            return View(_contaAppService.ObterContasPorUsuario(IdUsuario));
        }

        [Route("contas/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();
            var contaViewModel = _contaAppService.ObterPorId(id.Value);
            if (contaViewModel == null) return NotFound();
            return View(contaViewModel);
        }

        [Authorize(Policy = "PodeAlterar")]
        [Route("contas/registrar")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "PodeAlterar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contas/registrar")]
        public IActionResult Create(ContaViewModel contaViewModel)
        {
            if (!ModelState.IsValid) return View(contaViewModel);

            contaViewModel.IdUsuario = IdUsuario;
            _contaAppService.Registrar(contaViewModel);

            ViewBag.RetornoPost = OperacaoValida()
                ? "success,Conta cadastrada com sucesso!"
                : "error,Ocorreu um erro ao registrar! Verifique as mensagens.";

            return View(contaViewModel);
        }

        [Authorize(Policy = "PodeAlterar")]
        [Route("contas/atualizar/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var contaViewModel = _contaAppService.ObterPorId(id.Value);

            if (!VerificarSeContaPertenceAoUsuarioLogado(contaViewModel)) return RedirectToAction("Index");

            if (contaViewModel == null) return NotFound();
            return View(contaViewModel);
        }

        [Authorize(Policy = "PodeAlterar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contas/atualizar/{id:guid}")]
        public IActionResult Edit(ContaViewModel contaViewModel)
        {
            if (!VerificarSeContaPertenceAoUsuarioLogado(contaViewModel)) return RedirectToAction("Index");

            if (!ModelState.IsValid) return View(contaViewModel);
            _contaAppService.Atualizar(contaViewModel);
            ViewBag.RetornoPost = OperacaoValida()
                ? "success,Conta atualizada com sucesso!"
                : "error,Ocorreu um erro ao atualizar! Verifique as mensagens.";
            return View(contaViewModel);
        }

        [Authorize(Policy = "PodeAlterar")]
        [Route("contas/deletar/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var contaViewModel = _contaAppService.ObterPorId(id.Value);
            if (!VerificarSeContaPertenceAoUsuarioLogado(contaViewModel)) return RedirectToAction("Index");
            if (contaViewModel == null) return NotFound();
            return View(contaViewModel);
        }

        [Authorize(Policy = "PodeAlterar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("contas/deletar/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (!VerificarSeContaPertenceAoUsuarioLogado(_contaAppService.ObterPorId(id))) return RedirectToAction("Index");

            _contaAppService.Desativar(id);
            return RedirectToAction("Index");
        }

        private bool VerificarSeContaPertenceAoUsuarioLogado(ContaViewModel contaViewModel)
        {
            return contaViewModel.IdUsuario == IdUsuario;
        }
    }
}
