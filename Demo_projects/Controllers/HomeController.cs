using Demo_projects.AppContectDb;
using Demo_projects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Diagnostics;

namespace Demo_projects.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        SQLiteConnection connection = new SQLiteConnection("Data Source=billingNew.db");
        public IActionResult Index()
        {
            var data = Cls.DisplayBillingTable(connection);
            return View(data);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Billing billing)
        {

            Cls.InsertBillingRecord(connection, billing);
            return RedirectToAction("Index");


            return View();
        }



        public IActionResult Edit(int Id)
        {
            var data = Cls.EditBillingRecordGetById(connection, Id);

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Billing billing)
        {
            Cls.EditBillingRecord(connection, billing);
            return RedirectToAction("Index");
            return View(billing);
        }

        public ActionResult delete(int Id)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();

                Billing billingGet = dbContext.Billings.Where(x => x.BillNo == Id).FirstOrDefault();
                dbContext.Remove(billingGet);
                dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}