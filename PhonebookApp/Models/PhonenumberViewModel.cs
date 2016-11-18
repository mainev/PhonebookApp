using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhonebookApp.Models
{

    public class PhonenumberViewModel
    {
        public int id { get; set; }

      
        [StringLength(100)]
        public string phonenumber { get; set; }

        public ContactPersonViewModel contact_person { get; set; }

       public virtual int contact_person_id { get; set; }

    }
}