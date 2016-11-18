using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhonebookApp.Models
{
    [Table("phonenumber")]
    public class Phonenumber
    {
        public int id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string phonenumber { get; set; }


        public virtual ContactPerson contact_person { get; set; }

       



    }
}