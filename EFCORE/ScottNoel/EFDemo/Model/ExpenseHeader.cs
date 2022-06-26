using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExpenseHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Description { get; set; }
        public DateTime? ExpenseDate { get; set; }
        [ForeignKey("Requester")]
        public int RequesterId { get; set; }
        [InverseProperty("RequesterExpenseHeaders")]
        public User Requester { get; set; }
        [ForeignKey("Approver")] 
        public int ApproverId { get; set; }
        [InverseProperty("ApprovalExpenseHeaders")]
        public User Approver { get; set; }

        public List<ExpenseLine> ExpenseLines { get; set; }
    }
}
