using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedAcademy_task_2.Areas.Identity.Data;
using RedAcademy_task_2.Data;
using RedAcademy_task_2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace RedAcademy_task_2.Controllers.Finantial

{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly AppIdentityContext _context;
        public PaymentController(AppIdentityContext context)
        {
            _context = context;
        }

        [ClaimsAuthorize("Payment", "List")]
        [HttpGet]
        public IActionResult Index([FromQuery] int ps = 5, [FromQuery] int page = 1, [FromQuery] string filter = null, [FromQuery] string query = null)
        {

            //PopulateDatabase();

            // var assessmentsList = _context.Payments; //Receives Payment list

            // return View(assessmentsList.ToList().OrderBy(c => c.Id)); //returns list ordered by id


            var payments = GetPayments(ps, page, filter, query);

            ViewBag.Search = query;
            ViewBag.Filter = filter;


            return View(payments);

        }

        [ClaimsAuthorize("Payment", "Add")]
        [HttpGet]
        [Route("Add-Payment")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add-Payment")]
        public IActionResult Add(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(payment);
            }

            // Add          
            _context.Payments.Add(payment);
            _context.SaveChanges();

            TempData["success"] = "The Payment " + payment.Title + " with ID " + payment.Id + " was successfuly added!";
            return RedirectToAction("Index");

        }

        [ClaimsAuthorize("Payment", "Detail")]
        [HttpGet]
        [Route("Detail-Payment")]
        public IActionResult Detail(Guid id)
        {
            var payment = _context.Payments.FirstOrDefault(f => f.Id == id);

            if (GetPaymentById(id) == null)
                return RedirectToAction("Index");

            return View(payment);
        }

        [ClaimsAuthorize("Payment", "Edit")]
        [Route("edit-payment/{id:guid}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var payment = _context.Payments.FirstOrDefault(f => f.Id == id);

            if (GetPaymentById(id) == null)
                return RedirectToAction("Index");

            return View(payment);
        }

        [Route("edit-payment/{id:guid}")]
        [HttpPost]
        public IActionResult Edit(Guid id, Payment payment)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(payment);
            }

            // EDIT           
            _context.Payments.Update(payment);
            _context.SaveChanges();

            TempData["success"] = "The Payment " + payment.Title + " with ID " + payment.Id + " was successfuly edited!";
            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Payment", "Delete")]
        [Route("delete-payment/{id:guid}")]
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var payment = _context.Payments.FirstOrDefault(f => f.Id == id);

            if (GetPaymentById(id) == null)
                return RedirectToAction("Index");

            return View(payment);
        }

        [Route("delete-payment/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Payment payment)
        {
            if (payment.Id == null)
            {
                TempData["error"] = "There was an error with the Delete opertation";
                return View(payment);
            }

            // DELETE           
            _context.Payments.Remove(payment);
            _context.SaveChanges();

            TempData["success"] = "The Payment " + payment.Title + " with ID " + payment.Id + " was successfuly deleted!";
            return RedirectToAction("Index");
        }

        private IEnumerable<Payment> GetPayment(string filter = null, string query = null) {
        
            List<Payment> payments = null;

            if (!string.IsNullOrEmpty(query))
            {
                switch (filter)
                {
                    case "Title":
                        payments = _context.Payments.Where(f => f.Title.Contains(query)).ToList();
                        break;
                    default:
                        payments = _context.Payments.Where(f => f.Title.Contains(query)).ToList();
                        break;
                }
            }
            else
                payments = _context.Payments.ToList();

            return payments;

        }
        private Payment GetPaymentById(Guid id)
        {
            var payment = _context.Payments.FirstOrDefault(f => f.Id == id);

            if (payment == null)
                TempData["error"] = "There was an error with the ID provided";

            return payment;
        }

        //private void PopulateDatabase()
        //{
        //  var payments = new List<Payment>(GetAssessments());
        //   foreach (Payment payment in payments)
        //    {
        //       Add(payment);
        //    }
        //}
        private PagedViewModel<Payment> GetPayments(int pageSize, int pageIndex, string filter = null, string query = null)
        {
            var sql = @$"SELECT * FROM Payments
                      WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%') 
                      ORDER BY [Title] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Payments 
                      WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%')"; var multi = _context.Database.GetDbConnection()
            .QueryMultiple(sql, new { Title = query }); var payments = multi.Read<Payment>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<Payment>()

            {
                ReferenceAction = "Index",
                List = payments,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Filter = filter,
                Query = query
            };
        }
    }
}

