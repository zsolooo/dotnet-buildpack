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
            ViewBag.Version = GetCliVersion();
            return View();
        } 
        
        [HttpGet("/badge")]
        [ResponseCache(NoStore = true)]
        public ContentResult Badge()
        {
            var str = @"<svg xmlns='http://www.w3.org/2000/svg' width='{1}' height='20'>
    <mask id='a'>
        <rect width='{1}' height='20' rx='0' fill='#fff' />
    </mask>
    <g mask='url(#a)'>
        <path fill='#555' d='M0 0h136v20H0z' />
        <path fill='#007ec6' d='M136 0h{2}v20H136z' />
    </g>
    <g fill='#fff' font-family='DejaVu Sans,Verdana,Geneva,sans-serif' font-size='11'>
        <text x='5' y='15' fill='#010101' fill-opacity='.3'>compatible cli version</text>
        <text x='5' y='14'>compatible cli version</text>
        <text x='140' y='15' fill='#010101' fill-opacity='.3'>{0}</text>
        <text x='140' y='14'>{0}</text>
    </g>
</svg>";
            var version = GetCliVersion();
            var length = version.Length;
            var svgLength = 7*length+146;
            var content = string.Format(str, version, svgLength, svgLength - 136);
            return new ContentResult{Content = content, ContentType="image/svg+xml; charset=utf-8; api-version=2.2"};
        }
        
        private string GetCliVersion()
        {
            string stdout = string.Empty;
            RunProcessAndWaitForExit("dotnet", "--version", TimeSpan.FromSeconds(30), out stdout);
            return stdout.Trim();
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