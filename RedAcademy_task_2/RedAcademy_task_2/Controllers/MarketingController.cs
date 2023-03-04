using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedAcademy_task_2.Areas.Identity.Data;
using RedAcademy_task_2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RedAcademy_task_2.Controllers
{
    public class MarketingController : Controller
    {
        private readonly AppIdentityContext _context;
        public MarketingController(AppIdentityContext context)
        {
            _context = context;
        }
        public IActionResult Index([FromQuery] int ps = 5, [FromQuery] int page = 1, [FromQuery] string filter = null, [FromQuery] string query = null, [FromQuery] string showold = null)
        {

            var marketings = GetMarketing(showold);

            ViewBag.Search = query;
            ViewBag.Filter = filter;
            ViewBag.showold = showold;



            return View(marketings);

        }

        [ClaimsAuthorize("Marketing", "Add")]
        [HttpGet]
        [Route("Add-marketing")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add-marketing")]
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

        [ClaimsAuthorize("Marketing", "Detail")]
        [HttpGet]
        [Route("Detail-marketing")]
        public IActionResult Detail(Guid id)
        {
            var marketing = _context.Marketings.FirstOrDefault(f => f.Id == id);

            if (GetMarketingById(id) == null)
                return RedirectToAction("Index");

            return View(marketing);
        }

        [ClaimsAuthorize("Marketing", "Edit")]
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


        [ClaimsAuthorize("Marketing", "Delete")]
        [Route("delete-marketing/{id:guid}")]
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var marketing = _context.Marketings.FirstOrDefault(f => f.Id == id);

            if (GetMarketingById(id) == null)
                return RedirectToAction("Index");

            return View(marketing);
        }

        [Route("delete-marketing/{id:guid}")]
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

            TempData["success"] = "The Marketing " + marketing.Title + " with ID " + marketing.Id + " was successfuly deleted!";
            return RedirectToAction("Index");
        }

        private Marketing GetMarketingById(Guid id)
        {
            var marketing = _context.Marketings.FirstOrDefault(f => f.Id == id);

            if (marketing == null)
                TempData["error"] = "There was an error with the ID provided";

            return marketing;
        }


        private IEnumerable<Marketing> GetMarketing(string showold = null)
        {

            List<Marketing> marketings = null;

            if (showold == "on")
            {
                marketings = _context.Marketings.Where(x => x.FinishDate > DateTime.Now).ToList();
            }
            else
            {
                marketings = _context.Marketings.ToList();
            }

            return marketings;

        }
        private PagedViewModel<Marketing> GetMarketings(int pageSize, int pageIndex, string filter = null, string query = null)
        {
            var sql = @$"SELECT * FROM Marketings
                      WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%') 
                      ORDER BY [Title] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Marketings 
                      WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%')"; var multi = _context.Database.GetDbConnection()
            .QueryMultiple(sql, new { Title = query }); var marketings = multi.Read<Marketing>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<Marketing>()

            {
                ReferenceAction = "Index",
                List = marketings,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Filter = filter,
                Query = query
            };
        }

        //private IEnumerable<Marketing> GetMarketing(string filter = null, string query = null)
        //        {

        //            List<Marketing> marketings = null;

        //            if (!string.IsNullOrEmpty(query))
        //            {
        //                switch (filter)
        //                {
        //                    case "Title":
        //                        marketings = _context.Marketings.Where(f => f.Title.Contains(query)).ToList();
        //                        break;
        //                    default:
        //                        marketings = _context.Marketings.Where(f => f.Title.Contains(query)).ToList();
        //                        break;
        //                }
        //            }
        //            else
        //                marketings = _context.Marketings.ToList();

        //            return marketings;

        //        }


    }
}
