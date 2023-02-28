using Microsoft.AspNetCore.Mvc;
using RedAcademy_task_2.Areas.Identity.Data;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Controllers
{
    public class MarketingController : Controller
    {
        private readonly AppIdentityContext _context;
        public MarketingController(AppIdentityContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Marketing marketing)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(marketing);
            }

            // Add          
            _context.Marketings.Add(marketing);
            _context.SaveChanges();

            TempData["success"] = "The Marketing " + marketing.Title + " with ID " + marketing.Id + " was successfuly added!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail(Guid id)
        {
            var marketing = _context.Marketings.FirstOrDefault(f => f.Id == id);

            if (GetMarketingById(id) == null)
                return RedirectToAction("Index");

            return View(marketing);
        }

        [Route("edit-marketing/{id:guid}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var marketing = _context.Marketings.FirstOrDefault(f => f.Id == id);

            if (GetMarketingById(id) == null)
                return RedirectToAction("Index");

            return View(marketing);
        }

        [Route("edit-marketing/{id:guid}")]
        [HttpPost]
        public IActionResult Edit(Guid id, Marketing marketing)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(marketing);
            }

            // EDIT           
            _context.Marketings.Update(marketing);
            _context.SaveChanges();

            TempData["success"] = "The Marketing " + marketing.Title + " with ID " + marketing.Id + " was successfuly edited!";
            return RedirectToAction("Index");
        }

        [Route("delete-assessment/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Marketing marketing)
        {
            if (marketing.Id == null)
            {
                TempData["error"] = "There was an error with the Delete opertation";
                return View(marketing);
            }

            // DELETE           
            _context.Marketings.Remove(marketing);
            _context.SaveChanges();

            TempData["success"] = "The Assessment " + marketing.Title + " with ID " + marketing.Id + " was successfuly deleted!";
            return RedirectToAction("Index");
        }

        private Marketing GetMarketingById(Guid id)
        {
            var marketing = _context.Marketings.FirstOrDefault(f => f.Id == id);

            if (marketing == null)
                TempData["error"] = "There was an error with the ID provided";

            return marketing;
        }


    }
}
