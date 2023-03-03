using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedAcademy_task_2.Areas.Identity.Data;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Controllers.HumanResources
{
    [Authorize]
    public class GoalController : Controller
    {
        private readonly AppIdentityContext _context;
        public GoalController(AppIdentityContext context)
        {
            _context = context;
        }

        [ClaimsAuthorize("Goal", "List")]
        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, [FromQuery] string filter = null, [FromQuery] string query = null)
        {

            //PopulateDatabase();
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //return View(goals.ToPagedList(pageNumber, pageSize));

            var goalsList = _context.Goals; //Receives Goal list

            //return View(goalsList.ToList().OrderBy(c => c.Id)); //returns list ordered by id


            var goals = GetGoal(filter, query).ToList();

            ViewBag.Search = query;
            ViewBag.Filter = filter;

            // var paginated = PaginatedList<Goal>.CreateAsync(goalsList, pageNumber, pageSize);

            // if (!string.IsNullOrEmpty(query))
            //  Alert("The search for '" + query + "' by the '" + filter + "' was successful!", TypeAlert.info);

            return View(goals);

        }

        [ClaimsAuthorize("Goal", "Add")]
        [HttpGet]
        [Route("Add-Goal")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add-Goal")]
        public IActionResult Add(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(goal);
            }

            // Add          
            _context.Goals.Add(goal);
            _context.SaveChanges();

            TempData["success"] = "The Goal " + goal.Title + " with ID " + goal.Id + " was successfuly added!";
            return RedirectToAction("Index");

        }

        [ClaimsAuthorize("Goal", "Detail")]
        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail(Guid id)
        {
            var goal = _context.Goals.FirstOrDefault(f => f.Id == id);

            if (GetGoalById(id) == null)
                return RedirectToAction("Index");

            return View(goal);
        }

        [ClaimsAuthorize("Goal", "Edit")]
        [Route("edit-goal/{id:guid}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var goal = _context.Goals.FirstOrDefault(f => f.Id == id);

            if (GetGoalById(id) == null)
                return RedirectToAction("Index");

            return View(goal);
        }

        [Route("edit-goal/{id:guid}")]
        [HttpPost]
        public IActionResult Edit(Guid id, Goal goal)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(goal);
            }

            // EDIT           
            _context.Goals.Update(goal);
            _context.SaveChanges();

            TempData["success"] = "The Goal " + goal.Title + " with ID " + goal.Id + " was successfuly edited!";
            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Goal", "Delete")]
        [Route("delete-goal/{id:guid}")]
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var goal = _context.Goals.FirstOrDefault(f => f.Id == id);

            if (GetGoalById(id) == null)
                return RedirectToAction("Index");

            return View(goal);
        }

        [Route("delete-goal/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Goal goal)
        {
            if (goal.Id == null)
            {
                TempData["error"] = "There was an error with the Delete opertation";
                return View(goal);
            }

            // DELETE           
            _context.Goals.Remove(goal);
            _context.SaveChanges();

            TempData["success"] = "The Goal " + goal.Title + " with ID " + goal.Id + " was successfuly deleted!";
            return RedirectToAction("Index");
        }

        private IEnumerable<Goal> GetGoal(string filter = null, string query = null)
        {

            List<Goal> goals = null;

            if (!string.IsNullOrEmpty(query))
            {
                switch (filter)
                {
                    case "Title":
                        goals = _context.Goals.Where(f => f.Title.Contains(query)).ToList();
                        break;
                    case "Employee":
                        goals = _context.Goals.Where(f => f.Employee.Contains(query)).ToList();
                        break;
                    default:
                        goals = _context.Goals.Where(f => f.Title.Contains(query)).ToList();
                        break;
                }
            }
            else
                goals = _context.Goals.ToList();

            return goals;

        }
        private Goal GetGoalById(Guid id)
        {
            var goal = _context.Goals.FirstOrDefault(f => f.Id == id);

            if (goal == null)
                TempData["error"] = "There was an error with the ID provided";

            return goal;
        }

        private void PopulateDatabase()
        {
            var goals = new List<Goal>(GetGoals());
            foreach (Goal goal in goals)
            {
                Add(goal);
            }
        }

        // private List<Goal> GetGoal(string filter = null, string query = null)

        private List<Goal> GetGoals()
        {
            var goals = new List<Goal>()
            {
                    new Goal
                    {
                        Title = "Avaliação Anaul 2022",
                        Employee = "Idalina Freitas",
                        Description = "Uma avaliação anaul do ano 2022",
                        StartDate = new DateTime(2022, 1, 1), // example start date
                        EndDate = new DateTime(2022, 12, 31), // example end date
                        FinalEvaluation = 4,
                        Active = true
                    },

                    new Goal
                    {
                        Title = "Avaliação Anaul 2021",
                        Employee = "Joao Freitas",
                        Description = "Uma avaliação anaul do ano 2022",
                        StartDate = new DateTime(2022, 4, 1), // example start date
                        EndDate = new DateTime(2022, 12, 31), // example end date
                        Active = true
                    },

            };
            return goals;
        }
    }
}
