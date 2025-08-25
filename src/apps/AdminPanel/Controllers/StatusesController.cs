using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class StatusesController : Controller
    {
        public IActionResult StatusTitle() => View();
        public IActionResult StatusTranslate() => View();
        public IActionResult TypeTitle() => View();
    }
}
