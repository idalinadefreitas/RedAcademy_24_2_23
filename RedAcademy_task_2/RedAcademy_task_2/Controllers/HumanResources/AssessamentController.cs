using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using RedAcademy_task_2.Data;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Controllers.HumanResources
{
    public class AssessamentController : Controller
    {
        private readonly MyDbContext _context;
        public AssessamentController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

          //  PopulateDatabase();

            var assessamentsList = _context.Assessaments; //Receives consultant list

            return View(assessamentsList.ToList().OrderBy(c => c.Id)); //returns list ordered by id
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Assessament assessament)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(assessament);
            }

            // Add          
            _context.Assessaments.Add(assessament);
            _context.SaveChanges();

            TempData["success"] = "The assessament " + assessament.Title + " with ID " + assessament.Id + " was successfuly added!";
            return RedirectToAction("Index");


        }

        [HttpGet]
        [Route("Detail")]
        public IActionResult Details(Guid id)
        {
            var assessament = _context.Assessaments.FirstOrDefault(f => f.Id == id);

            if (GetAssessamentById(id) == null)
                return RedirectToAction("Index");

            return View(assessament);
        }


        [Route("edit-assessment/{id:guid}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var assessament = _context.Assessaments.FirstOrDefault(f => f.Id == id);

            if (GetAssessamentById(id) == null)
                return RedirectToAction("Index");

            return View(assessament);
        }

        [Route("edit-assessment/{id:guid}")]
        [HttpPost]
        public IActionResult Edit(Guid id, Assessament assessament)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "There was an error with the Add opertation";
                return View(assessament);
            }

            // EDIT           
            _context.Assessaments.Update(assessament);
            _context.SaveChanges();

            TempData["success"] = "The assessament " + assessament.Title + " with ID " + assessament.Id + " was successfuly edited!";
            return RedirectToAction("Index");
        }

        [Route("delete-assessment/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Assessament assessament)
        {
            if (assessament.Id == null)
            {
                TempData["error"] = "There was an error with the Delete opertation";
                return View(assessament);
            }

            // DELETE           
            _context.Assessaments.Remove(assessament);
            _context.SaveChanges();

            TempData["success"] = "The assessament " + assessament.Title + " with ID " + assessament.Id + " was successfuly deleted!";
            return RedirectToAction("Index");
        }

        private Assessament GetAssessamentById(Guid id)
        {
            var assessament = _context.Assessaments.FirstOrDefault(f => f.Id == id);

            if (assessament == null)
                TempData["error"] = "There was an error with the ID provided";

            return assessament;
        }

        private void PopulateDatabase()
        {
            var assessaments = new List<Assessament>(GetAssessaments());
            foreach (Assessament assessament in assessaments)
            {
                Add(assessament);
            }
        }

        private List<Assessament> GetAssessaments()
        {
            var assessaments = new List<Assessament>()
            {
                    new Assessament
                    {
                        Title = "Avaliação Anaul 2022",
                        Employee = "Idalina Freitas",
                        Description = "Uma avaliação anaul do ano 2022",
                        StartDate = new DateTime(2022, 1, 1), // example start date
                        EndDate = new DateTime(2022, 12, 31), // example end date
                        FinalEvaluation = 4,
                        Active = true
                    },

                    new Assessament
                    {
                        Title = "Avaliação Anaul 2021",
                        Employee = "Joao Freitas",
                        Description = "Uma avaliação anaul do ano 2022",
                        StartDate = new DateTime(2022, 4, 1), // example start date
                        EndDate = new DateTime(2022, 12, 31), // example end date
                        Active = true
                    },

            };
            return assessaments;
        }
    }
}

