using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Contas.Site.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IUser _user;

        public Guid IdUsuario { get; set; }

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications, IUser user)
        {
            _notifications = notifications;
            _user = user;

            if (_user.IsAuthenticated())
            {
                IdUsuario = user.GetUserId();
            }
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }
    }
}