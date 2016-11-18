using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhonebookApp.Models
{
    [Table("contact_person")]
    public class ContactPerson
    {

        public ContactPerson()
        {
            phonenumbers = new List<Phonenumber>();
        }

        public int id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string last_name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string first_name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string address { get; set; }

        public DateTime birthday { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string email { get; set; }

        public virtual List<Phonenumber> phonenumbers { get; set; }

       
    }
}