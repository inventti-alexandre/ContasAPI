using Contas.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contas.Site.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public SummaryViewComponent(IDomainNotificationHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notification = await Task.FromResult(_notifications.GetNotifications());
            notification.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Value));
            return View();
        }
    }
}