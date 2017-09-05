using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Addressline { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
    }
}
