using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class DropdownVM
    {
        public string DropdownMenuTitle { get; set; }
        public DropDownLink[] DropDownLinks { get; set; }
    }
}
