using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WizLib_Model.Models;

namespace WizLib_Model.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}
