using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedAcademy_task_2.Areas.Identity.Data;
using RedAcademy_task_2.Data;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Controllers.HumanResources
{
    [Authorize]
    public class AssessmentController : Controller
    {
        private readonly AppIdentityContext _context;
        public AssessmentController(AppIdentityContext context)
        {
            _context = context;
        }

        //[ClaimsAuthorize("Assessment", "List")]
        [HttpGet]
        public IActionResult Index([FromQuery] string filter = null, [FromQuery] string query = null)
        {

            //PopulateDatabase();

            // var assessmentsList = _context.Assessments; //Receives Assessment list

            // return View(assessmentsList.ToList().OrderBy(c => c.Id)); //returns list ordered by id


            var assessments = GetAssessment(filter, query);

            ViewBag.Search = query;
            ViewBag.Filter = filter;

            // if (!string.IsNullOrEmpty(query))
              //  Alert("The search for '" + query + "' by the '" + filter + "' was successful!", TypeAlert.info);

            return View(assessments);

        }

        //[ClaimsAuthorize("Assessment", "Add")]
        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Assessment assessment)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(assessment);
            }

            // Add          
            _context.Assessments.Add(assessment);
            _context.SaveChanges();

            TempData["success"] = "The Assessment " + assessment.Title + " with ID " + assessment.Id + " was successfuly added!";
            return RedirectToAction("Index");

        }

        //[ClaimsAuthorize("Assessment", "Detail")]
        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail(Guid id)
        {
            var assessment = _context.Assessments.FirstOrDefault(f => f.Id == id);

            if (GetAssessmentById(id) == null)
                return RedirectToAction("Index");

            return View(assessment);
        }

        //[ClaimsAuthorize("Assessment", "Edit")]
        [Route("edit-assessment/{id:guid}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var assessment = _context.Assessments.FirstOrDefault(f => f.Id == id);

            if (GetAssessmentById(id) == null)
                return RedirectToAction("Index");

            return View(assessment);
        }

        //[Route("edit-assessment/{id:guid}")]
        [HttpPost]
        public IActionResult Edit(Guid id, Assessment assessment)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(assessment);
            }

            // EDIT           
            _context.Assessments.Update(assessment);
            _context.SaveChanges();

            TempData["success"] = "The Assessment " + assessment.Title + " with ID " + assessment.Id + " was successfuly edited!";
            return RedirectToAction("Index");
        }

        [Route("delete-assessment/{id:guid}")]
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var assessment = _context.Assessments.FirstOrDefault(f => f.Id == id);

            if (GetAssessmentById(id) == null)
                return RedirectToAction("Index");

            return View(assessment);
        }

        //[ClaimsAuthorize("Assessment", "Delete")]
        [Route("delete-assessment/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Assessment assessment)
        {
            if (assessment.Id == null)
            {
                TempData["error"] = "There was an error with the Delete opertation";
                return View(assessment);
            }

            // DELETE           
            _context.Assessments.Remove(assessment);
            _context.SaveChanges();

            TempData["success"] = "The Assessment " + assessment.Title + " with ID " + assessment.Id + " was successfuly deleted!";
            return RedirectToAction("Index");
        }

        private IEnumerable<Assessment> GetAssessment(string filter = null, string query = null) {
        
            List<Assessment> assessments = null;

            if (!string.IsNullOrEmpty(query))
            {
                switch (filter)
                {
                    case "Title":
                        assessments = _context.Assessments.Where(f => f.Title.Contains(query)).ToList();
                        break;
                    case "Employee":
                        assessments = _context.Assessments.Where(f => f.Employee.Contains(query)).ToList();
                        break;
                    default:
                        assessments = _context.Assessments.Where(f => f.Title.Contains(query)).ToList();
                        break;
                }
            }
            else
                assessments = _context.Assessments.ToList();

            return assessments;

        }
        private Assessment GetAssessmentById(Guid id)
        {
            var assessment = _context.Assessments.FirstOrDefault(f => f.Id == id);

            if (assessment == null)
                TempData["error"] = "There was an error with the ID provided";

            return assessment;
        }

        private void PopulateDatabase()
        {
          var assessments = new List<Assessment>(GetAssessments());
           foreach (Assessment assessment in assessments)
            {
               Add(assessment);
            }
        }

       // private List<Assessment> GetAssessment(string filter = null, string query = null)
        
        private List<Assessment> GetAssessments()
        {
            var assessments = new List<Assessment>()
            {
                    new Assessment
                    {
                        Title = "Avaliação Anaul 2022",
                        Employee = "Idalina Freitas",
                        Description = "Uma avaliação anaul do ano 2022",
                        StartDate = new DateTime(2022, 1, 1), // example start date
                        EndDate = new DateTime(2022, 12, 31), // example end date
                        FinalEvaluation = 4,
                        Active = true
                    },

                    new Assessment
                    {
                        Title = "Avaliação Anaul 2021",
                        Employee = "Joao Freitas",
                        Description = "Uma avaliação anaul do ano 2022",
                        StartDate = new DateTime(2022, 4, 1), // example start date
                        EndDate = new DateTime(2022, 12, 31), // example end date
                        Active = true
                    },

            };
            return assessments;
        }
    }
}

