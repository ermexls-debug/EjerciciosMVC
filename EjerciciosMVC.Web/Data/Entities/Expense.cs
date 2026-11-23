using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public ExpenseCategory? Category { get; set; }
    }
}
