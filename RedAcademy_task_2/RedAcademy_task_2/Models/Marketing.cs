using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RedAcademy_task_2.Models
{
    public class Marketing
    {
        public Marketing()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title field is mandatory!")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters for Genre field!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Fill in the Description field")]
        [StringLength(1000, ErrorMessage = "Maximum 100 characters for Genre field!")]
        public string Description { get; set; }

        // [Required(ErrorMessage = "Fill in the Description field")]
        // [StringLength(200, ErrorMessage = "Maximum 200 characters for Genre field!")]
        public string Image { get; set; }

        //[Required(ErrorMessage = "Fill in the Description field")]
        //[StringLength(100, ErrorMessage = "Maximum 100 characters for Genre field!")]
        public RedAcademy Columnist { get; set; }

        //[Required(ErrorMessage = "Fill in the Description field")]
        //[StringLength(100, ErrorMessage = "Maximum 100 characters for Genre field!")]
        public CategoryNews Category { get; set; }

        [Required(ErrorMessage = "Fill in the Start Date field.")]
        [DataType(DataType.DateTime, ErrorMessage = "The Start Date is in the wrong format!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Fill in the End Date field.")]
        [DataType(DataType.DateTime, ErrorMessage = "The Start Date is in the wrong format!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime FinishDate { get; set; }

        [Required(ErrorMessage = "Fill in the Time field")]
        [Range(1.00, 60.00, ErrorMessage = "The Time field must be between 1 and 10")]
        [Display(Name = "Time")]
        public float TimetoRead { get; set; }

        public bool Active { get; set; }
    }

    public enum CategoryNews
    {
        Development,
        Marketing,
        HumanResources,
        Finantials
    }

    public enum RedAcademy
    {
        RafaelPalma,
        LuisQueiros,
        MiguelRosa,
        Idalina
    }
}
