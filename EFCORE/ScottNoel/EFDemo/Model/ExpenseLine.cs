using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class ExpenseLine
    {
        public int ExpenseLineId { get; set; }
        public string Description { get; set; }
        [Range(1, 10, ErrorMessage = "{0} must be between 1 and 10.")]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        [Range(0.01,100.00,ErrorMessage ="Unit cost must be between 0.01 and 100.")]
        public decimal UnitCost { get; set; }
        [NotMapped]
        public  string Secret { get; set; }
        public int ExpenseHeaderId { get; set; }
        public ExpenseHeader ExpenseHeader { get; set; }

    }
}
