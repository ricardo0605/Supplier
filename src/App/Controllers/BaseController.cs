using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool OperationIsValid()
        {
            return !_notificator.HasNotifications();
        }
    }
}