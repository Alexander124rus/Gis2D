using GeoMVC7.Domain.Entities.GeoEntitie;
using GeoMVC7.Domain.Repos.Interfaces;
using GeoMVC7.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GeoMVC7.Areas.Admin.Controllers
{
    public class DropDownItemModel : Object
    {
        public DropDownItemModel() { }
        public string Command { get; set; }
        public string cssClass { get; set; }
        public string IconCss { get; set; }
        public string ListImage { get; set; }
        public string SubCommand { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string arialabel { get; set; }
    }
    [Area("Admin"), Authorize]
    public class PageController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public readonly IMyGeometryRepo _myGeometryRepo;
        //public readonly IAuthorizationService _authorizationService;
        public readonly GeoService _geoService;

        public PageController(IWebHostEnvironment hostingEnvironment, IMyGeometryRepo myGeometryRepo, /*IAuthorizationService authorizationService,*/ GeoService geoService)
        {
            _hostingEnvironment = hostingEnvironment;
            _myGeometryRepo = myGeometryRepo;
            //_authorizationService = authorizationService;
            _geoService = geoService;
        }

        // GET: PageController
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            var listMyGeometry = _myGeometryRepo.GetAll();
            //ViewBag.DataSource = _myGeometryRepo.GetAll();
            return View(listMyGeometry);
        }

        // GET: PageController/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.insertImageSettings = new
            {
                maxWidth = "600"
            };

            //Стили таблицы
            List<DropDownItemModel> StyleItems = new List<DropDownItemModel>()
            {
                new DropDownItemModel() { text = "Dashed Borders", cssClass = "e-dashed-borders", value = "e-dropdown-btn-item_21"},
            };
            ViewBag.styles = StyleItems;


            ViewBag.ajaxSettings = new FileManagerService().FileManagerGet(Request);

            var tools = new
            {
                tooltipText = "Insert Horizontal Rule",
                template = "<a class='e-tbar-btn e-btn' tabindex='-1' id='custom_tbar'  style='width:100%'><div class='e-tbar-btn-text rtecustomtool' style='font-weight: 500;'> &#937;</div></a>"
            };
            ViewBag.items = new object[] { "Bold", "Italic", "Underline", "CreateTable",
            "Formats", "Alignments", "-", "OrderedList", "UnorderedList", "FileManager", "CreateLink", "SourceCode", "Outdent", "Indent", "InsertCode", tools};
            var myGeometry = _myGeometryRepo.Find(id);
            //var authResult = _authorizationService.AuthorizeAsync(User, myGeometry, "CanManager");
            return View(myGeometry);
        }

        // POST: PageController/Edit/5
        [HttpPost]
        public IActionResult Edit(MyGeometry myGeometry)
        {
            ViewBag.insertImageSettings = new
            {
                maxWidth = "600"
            };
            myGeometry.MyPage.MyGeometryId = myGeometry.Id;
            myGeometry = new PointLayersService().MyGeometry(myGeometry);
            if (ModelState.IsValid)
            {
                _myGeometryRepo.Update(myGeometry);
                _geoService.Geo();
            }
            return RedirectToAction("Edit");
        }

        // GET: PageController/Create
        [HttpGet]
        public IActionResult Create()
        {
            var tools = new
            {
                tooltipText = "Insert Horizontal Rule",
                template = "<a class='e-tbar-btn e-btn' tabindex='-1' id='custom_tbar'  style='width:100%'><div class='e-tbar-btn-text rtecustomtool' style='font-weight: 500;'> &#937;</div></a>"
            };

            var hostUrl = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
            ViewBag.ajaxSettings = new FileManagerService().FileManagerGet(Request);
            ViewBag.items = new object[] { "Bold", "Italic", "Underline", "CreateTable",
            "Formats", "Alignments", "-", "OrderedList", "UnorderedList", "FileManager", "CreateLink", "SourceCode", "Outdent", "Indent", tools};

            return View();
        }

        [HttpPost]
        public IActionResult Create(MyGeometry myGeometry)
        {
            myGeometry = new PointLayersService().MyGeometry(myGeometry);

            if (ModelState.IsValid)
            {
                _myGeometryRepo.Add(myGeometry);
                _geoService.Geo();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Create");
        }

        public IActionResult Delete(int id)
        {
            var myGeometry = _myGeometryRepo.Find(id);
            return View(myGeometry);
        }

        [HttpPost]
        public IActionResult Delete(MyGeometry myGeometry)
        {
            _myGeometryRepo.Delete(myGeometry);
            _geoService.Geo();
            return RedirectToAction("Index");
        }
    }
}
