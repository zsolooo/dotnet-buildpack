using System;
using System.IO;
using System.Diagnostics;
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
            string stdout;
            RunProcessAndWaitForExit("dotnet", "--version", TimeSpan.FromSeconds(30), out stdout);
            ViewBag.Version = stdout;
            return View();
        } 
        
        // source from dotnet/cli
        private static int RunProcessAndWaitForExit(string fileName, string arguments, TimeSpan timeout, out string stdout)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = Process.Start(startInfo);

            stdout = null;
            if (process.WaitForExit((int)timeout.TotalMilliseconds))
            {
                stdout = process.StandardOutput.ReadToEnd();
            }
            else
            {
                process.Kill();
            }

            return process.ExitCode;
        }
    }
}