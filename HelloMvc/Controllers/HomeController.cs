using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace HelloMvc
{
    public class HomeController : Controller
    {
        
        [HttpGet("/")]
        public IActionResult Index([FromServices] IHostingEnvironment host) 
        {
            ViewBag.ContentRoot = host.ContentRootPath;
            ViewBag.WebRoot = host.WebRootPath;
            ViewBag.Dir = Directory.GetCurrentDirectory();
            return View();
        } 
    }
}