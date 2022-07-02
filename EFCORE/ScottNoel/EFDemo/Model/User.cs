using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }
        public List<ExpenseHeader> RequesterExpenseHeaders { get; set; }
        public List<ExpenseHeader> ApprovalExpenseHeaders { get; set; }

    }
}
