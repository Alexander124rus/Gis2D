using GeoMVC7.Domain.Repos.Interfaces;
using GeoMVC7.Models;
using GeoMVC7.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeoMVC7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IMyGeometryRepo _myGeometryRepo;

        public HomeController(ILogger<HomeController> logger, IMyGeometryRepo myGeometryRepo)
        {
            _logger = logger;
            _myGeometryRepo = myGeometryRepo;
        }

        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Mark(int id)
        {
            var myGeometry = _myGeometryRepo.Find(id);
            return View(myGeometry);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}