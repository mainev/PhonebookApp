
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PhonebookApp.App_Start;
using PhonebookApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhonebookApp.Controllers.WebAPIController
{
    [RoutePrefix("api/ContactPerson")]
    public class ContactPersonController : ApiController
    {
        PhonebookDB db = new PhonebookDB();

        [Route("all")]
        public List<ContactPersonViewModel> getUsers()
        {
            var result = (from cp in db.ContactPersons
                          select new ContactPersonViewModel
                          {
                              id = cp.id,
                              last_name = cp.last_name,
                              first_name = cp.first_name,
                              address = cp.address,
                              birthday = cp.birthday,
                              email = cp.email
                          }).OrderBy(cp => cp.id).ToList<ContactPersonViewModel>();
            return result;

        }

        [Route("save")]
        public async Task<IHttpActionResult> saveContactPerson(ContactPersonViewModel contactPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var newContactPerson = new ContactPerson
            {
                last_name = contactPerson.last_name,
                first_name = contactPerson.first_name,
                address = contactPerson.address,
                birthday = contactPerson.birthday,
                email = contactPerson.email
            };
            foreach (var p in contactPerson.phonenumbers)
            {

                var newPhoneNo = new Phonenumber
                {
                    phonenumber = p.phonenumber
                };
                newContactPerson.phonenumbers.Add(newPhoneNo);
            }
            db.ContactPersons.Add(newContactPerson);
            await db.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("delete")]
        public async Task<IHttpActionResult> deleteContactPerson(ContactPersonViewModel contactPerson)
        {

            var dbContactPerson = db.ContactPersons.Find(contactPerson.id);
            dbContactPerson.phonenumbers.ToList().ForEach(p => db.Entry(p).State = EntityState.Deleted);
            db.Entry(dbContactPerson).State = EntityState.Deleted;

            await db.SaveChangesAsync();
            return Ok();

        }

        [Route("details")]
        public ContactPersonViewModel getContactPersonDetails(int id)
        {

            var cp = db.ContactPersons.Where(c => c.id == id).First();


            if (cp != null)
            {
                var newCp = new ContactPersonViewModel
                {
                    id = cp.id,
                    last_name = cp.last_name,
                    first_name = cp.first_name,
                    address = cp.address,
                    birthday = cp.birthday,
                    email = cp.email
                };
                foreach (var phoneno in cp.phonenumbers)
                {


                    var newPhoneNo = new PhonenumberViewModel()
                    {
                        id = phoneno.id,
                        phonenumber = phoneno.phonenumber

                    };
                    newCp.phonenumbers.Add(newPhoneNo);

                }
                return newCp;

            }

            return null;

        }

        [Route("update")]
        public async Task<IHttpActionResult> updateContactPerson(ContactPersonViewModel updatedContactPerson)
        {

            List<Phonenumber> updatedPhoneNumbers = new List<Phonenumber>();
            foreach (PhonenumberViewModel pn in updatedContactPerson.phonenumbers)
            {
                var newPn = new Phonenumber
                {
                    phonenumber = pn.phonenumber,
                    contact_person = db.ContactPersons.Find(updatedContactPerson.id)
                };
                updatedPhoneNumbers.Add(newPn);
            }

            var oldPhoneNumbers = db.Phonenumbers.Where(p => p.contact_person.id == updatedContactPerson.id).ToList<Phonenumber>();

            var addedPhoneNumbers = updatedPhoneNumbers.ExceptBy(oldPhoneNumbers, p => p.id);
            var deletedPhoneNumbers = oldPhoneNumbers.ExceptBy(updatedPhoneNumbers, p => p.id);
            var modifiedPhoneNumbers = updatedPhoneNumbers.ExceptBy(addedPhoneNumbers, p => p.id);

            addedPhoneNumbers.ToList<Phonenumber>().ForEach(p => db.Phonenumbers.Add(p));
            deletedPhoneNumbers.ToList<Phonenumber>().ForEach(p => db.Entry(p).State = EntityState.Deleted);
            foreach (Phonenumber phoneno in modifiedPhoneNumbers)
            {

                var existingPhoneNo = db.Phonenumbers.Find(phoneno.id);
                if (existingPhoneNo != null)
                {
                    var phoneNoEntry = db.Entry(existingPhoneNo);
                    phoneNoEntry.CurrentValues.SetValues(phoneno);
                }
            }

            var existingContactPerson = db.ContactPersons.Find(updatedContactPerson.id);
            if (existingContactPerson != null)
            {
                var contactPersonEntry = db.Entry(existingContactPerson);
                contactPersonEntry.CurrentValues.SetValues(updatedContactPerson);
            }



            await db.SaveChangesAsync();

            return Ok();
        }

        [Route("generate_report")]
        [HttpPost]
        public HttpResponseMessage generateReport()
        {

            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/reports/contacts.rpt"));
            System.IO.Stream stream = new System.IO.MemoryStream();

            var contactsDataSet = new contacts();
            DataTable phoneNoDT = contactsDataSet.Tables["phonebookDb"];

            var contactPersonList = db.ContactPersons.ToList();

            foreach (var cp in contactPersonList)
            {
                DataRow dr = phoneNoDT.NewRow();
                dr["id"] = cp.id;
                dr["last_name"] = cp.last_name;
                dr["first_name"] = cp.first_name;
                dr["address"] = cp.address;
                dr["email"] = cp.email;
                dr["birthday"] = cp.birthday;
                foreach (var pns in cp.phonenumbers)
                {
                    dr["phonenumbers"] = pns.phonenumber;
                }
                phoneNoDT.Rows.Add(dr);
            }

            cryRpt.SetDataSource(phoneNoDT);
            stream = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }

}
