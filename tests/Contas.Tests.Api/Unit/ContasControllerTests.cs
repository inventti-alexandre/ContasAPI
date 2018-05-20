using AutoMapper;
using Contas.Domain.Contas.Commands;
using Contas.Domain.Contas.Repository;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Contas.Services.Api.Controllers;
using Contas.Services.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Contas.Tests.Api.Unit
{
    public class ContasControllerTests
    {
        public ContasController contasController;
        public Mock<DomainNotificationHandler> mockNotification;
        public Mock<IUser> mockUser;
        public Mock<IContaRepository> mockContasRepository;
        public Mock<IMapper> mockMapper;
        public Mock<IMediatorHandler> mockMediator;

        public ContasControllerTests()
        {
            mockNotification = new Mock<DomainNotificationHandler>();
            mockUser = new Mock<IUser>();
            mockContasRepository = new Mock<IContaRepository>();
            mockMapper = new Mock<IMapper>();
            mockMediator = new Mock<IMediatorHandler>();
            contasController = new ContasController(mockNotification.Object, mockUser.Object, mockContasRepository.Object, mockMapper.Object, mockMediator.Object);
        }

        [Fact]
        public void ContasController_RegistrarConta_RetornarSucesso()
        {
            var contaViewModel = new ContaViewModel();
            var contaCommand = new RegistrarContaCommand("Foo", DateTime.Now, 100, false, 0, "", Guid.NewGuid(), Guid.NewGuid());
            mockMapper.Setup(m => m.Map<RegistrarContaCommand>(contaViewModel)).Returns(contaCommand);
            mockNotification.Setup(m => m.GetNotifications()).Returns(new List<DomainNotification>());

            var result = contasController.Post(contaViewModel);

            mockMediator.Verify(m => m.EnviarComando(contaCommand), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ContasController_RegistrarConta_RetornarErrosNaModelState()
        {
            var notificationList = new List<DomainNotification> {new DomainNotification("Error", "Model error")};
            mockNotification.Setup(m => m.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(m => m.HasNotifications()).Returns(true);
            contasController.ModelState.AddModelError("Error", "Model error");

            var result = contasController.Post(new ContaViewModel());

            mockMediator.Verify(m => m.EnviarComando(It.IsAny<RegistrarContaCommand>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ContasController_RegistrarConta_RetornarErrosDeDominio()
        {
            var contaViewModel = new ContaViewModel();
            var contaCommand = new RegistrarContaCommand("Foo", DateTime.Now, 100, false, 0, "", Guid.NewGuid(), Guid.NewGuid());
            var notificationList = new List<DomainNotification> { new DomainNotification("Error", "Domain error") };
            mockMapper.Setup(m => m.Map<RegistrarContaCommand>(contaViewModel)).Returns(contaCommand);
            mockNotification.Setup(m => m.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(m => m.HasNotifications()).Returns(true);

            var result = contasController.Post(contaViewModel);

            mockMediator.Verify(m => m.EnviarComando(contaCommand), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}