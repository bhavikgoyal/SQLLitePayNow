using Demo_projects.AppContectDb;
using Demo_projects.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Demo_projects.Controllers
{
    public class BillingController : Controller
    {

        public IActionResult Index()
        {
            List<Billing> billingList = new List<Billing>();
            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();

                billingList = dbContext.Billings.ToList();

            }
            return View(model: billingList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Billing billing)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();

                dbContext.Billings.Add(billing);
                dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Edit(int Id)
        {
            Billing billing = new Billing();
            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();

                billing = dbContext.Billings.Where(x => x.BillNo == Id).FirstOrDefault();

            }

            return View(billing);
        }
        [HttpPost]
        public IActionResult Edit(Billing billing)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Database.EnsureCreated();

                Billing billingGet = dbContext.Billings.Where(x => x.BillNo == billing.BillNo).FirstOrDefault();
                billingGet.CustomertNo = billing.CustomertNo;
                billingGet.Date = billing.Date;
                billingGet.Amount = billing.Amount;
                billingGet.IsPaid = billing.IsPaid;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

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
    }
}
