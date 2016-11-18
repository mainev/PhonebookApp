using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhonebookApp.Models
{
   
    public class ContactPersonViewModel
    {

        public ContactPersonViewModel()
        {
            phonenumbers = new List<PhonenumberViewModel>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string last_name { get; set; }
        
        [StringLength(100)]
        public string first_name { get; set; }
        
        [StringLength(100)]
        public string address { get; set; }

        public DateTime birthday { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        public List<PhonenumberViewModel> phonenumbers { get; set; }

       
    }
}