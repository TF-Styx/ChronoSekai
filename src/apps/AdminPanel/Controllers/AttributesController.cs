using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AttributesController : Controller
    {
        public IActionResult Artist() => View();
        public IActionResult Author() => View();
        public IActionResult Genre() => View();
        public IActionResult Publisher() => View();
        public IActionResult Tag() => View();
    }
}
