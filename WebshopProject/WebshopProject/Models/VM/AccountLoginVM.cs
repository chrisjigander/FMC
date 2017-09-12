using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Vänligen ange ett användarnamn.")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vänligen ange ett lösenord.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string PassWord { get; set; }

    }
}
