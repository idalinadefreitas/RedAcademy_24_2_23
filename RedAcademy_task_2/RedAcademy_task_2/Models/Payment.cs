using System.ComponentModel.DataAnnotations;

namespace RedAcademy_task_2.Models
{
    public class Payment
    {
        public Payment()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title field is mandatory!")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters for Genre field!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Fill in the Employee field")]
        [StringLength(1000, ErrorMessage = "Maximum 100 characters for Genre field!")]
        public string Description { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }

        public int PaymentTypeId { get; set; } // Id do tipo de pagamento (correspondente ao enum)
        public string CostCenter { get; set; } // Centro de custo

        [Required(ErrorMessage = "Fill in the Start Date field.")]
        [DataType(DataType.DateTime, ErrorMessage = "The Start Date is in the wrong format!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Payment  Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Fill in the field")]
        public decimal BaseSalaryValue { get; set; } // Valor do salário base
        
        [Required(ErrorMessage = "Fill in the field")]
        public decimal InsuranceValue { get; set; } // Valor do seguro

        [Required(ErrorMessage = "Fill in the field")]
        public decimal MealValue { get; set; } // Valor do vale-refeição

        [Required(ErrorMessage = "Fill in the field")]
        public decimal DisplacementValue { get; set; } // Valor do vale-transporte

        [Required(ErrorMessage = "Fill in the field")]
        public decimal TotalLiquid { get; set; } // Valor total líquido do pagamento
       
        
        public decimal MonthlyIRSValue { get; set; } // Valor mensal do Imposto de Renda retido na fonte
        
        public decimal SocialSecurityValue { get; set; } // Valor da contribuição ao INSS

        [Required(ErrorMessage = "Fill in the field")]
        public decimal Total { get; set; } // Valor total do pagamento (soma de todos os valores)

        [Required(ErrorMessage = "Fill in the field")]
        public bool Active { get; set; }
    }

    public enum PaymentTypeEnum
    {
        Multibanco,
        MBWay,
        Money,
        CreditCard,
        DebitCard
    }
}
