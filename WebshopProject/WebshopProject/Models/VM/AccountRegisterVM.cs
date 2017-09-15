using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class AccountRegisterVM
    {
        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [DataType(DataType.Password)]
        [Display(Name = "Upprepa lösenord")]
        [Compare("PassWord", ErrorMessage ="Lösenorden måste stämma överens")]
        public string RepeatPassWord { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [EmailAddress(ErrorMessage = "Ogiltig e-mail!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Adress")]
        public string AddressLine { get; set; }

        //[Range(4, 6)]//Todo Rimligt??? bör vara 5!
        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Stad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält!")]
        [Display(Name = "Telefonnummer mobil")]
        [Phone(ErrorMessage = "Ogiltigt telefonnummer!")]
        public string PhoneNumber { get; set; }


    }
}
