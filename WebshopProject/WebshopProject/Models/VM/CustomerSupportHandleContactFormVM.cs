using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{

    public class CustomerSupportHandleContactFormVM
    {
        [Required(ErrorMessage = "Detta fält måste fyllas i!")]
        [Display(Name = "Förnamn:")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Detta fält måste fyllas i!")]
        [Display(Name = "Efternamn:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Detta fält måste fyllas i!")]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Detta fält måste fyllas i!")]
        [Display(Name = "Meddelande:")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

    }
}
