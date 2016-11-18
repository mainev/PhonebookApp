namespace PhonebookApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonebookDB : DbContext
    {
        // Your context has been configured to use a 'PhonebookDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PhonebookApp.Models.PhonebookDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PhonebookDB' 
        // connection string in the application configuration file.
        public PhonebookDB()
            : base("name=PhonebookDB")
        {
        }

        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Phonenumber> Phonenumbers { get; set; }
    }

  
}