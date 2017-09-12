using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class CustomerSupportHandleContactFormVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

    }
}
