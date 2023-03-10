using System.ComponentModel.DataAnnotations;

namespace RedAcademy_task_2.Models
{
    public class Goal
    {

            public Goal()
            {
                Id = Guid.NewGuid();
            }
            [Key]
            public Guid Id { get; set; }

            [Required(ErrorMessage = "The Title field is mandatory!")]
            [StringLength(100, ErrorMessage = "Maximum 100 characters for Genre field!")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Fill in the Employee field")]
            [StringLength(100, ErrorMessage = "Maximum 50 characters for Genre field!")]
            public string Employee { get; set; }

            [Required(ErrorMessage = "Fill in the Description field")]
            [StringLength(250, ErrorMessage = "Maximum 250 characters for Genre field!")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Fill in the Start Date field.")]
            [DataType(DataType.DateTime, ErrorMessage = "The Start Date is in the wrong format!")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            [Display(Name = "Start Date")]
            public DateTime StartDate { get; set; }

            [Required(ErrorMessage = "Fill in the End Date field.")]
            [DataType(DataType.DateTime, ErrorMessage = "The Start Date is in the wrong format!")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            [Display(Name = "End Date")]
            public DateTime EndDate { get; set; }

            [Required(ErrorMessage = "Fill in the Evaluation field")]
            [Range(1.00, 10.00, ErrorMessage = "The Evaluation field must be between 1 and 10")]
            [Display(Name = "Final Evaluation")]
            public float FinalEvaluation { get; set; }

            public StatusGoal Status { get; set; }

        public bool Active { get; set; }
    }

    public enum StatusGoal
    {
        Start,
        InPreocess,
        Stopped,
        Concluede,
        Canceled
    }
}
