using System.Collections.Generic;
using AdventurousContacts.Model.DAL;
using System.ComponentModel.DataAnnotations;

namespace AdventurousContacts.Model
{
    // Klassen tillhandahåller metoder presentationslogiklagret
    // anropar för att hantera data. Främst innehåller klassen
    // metoder som använder sig av klasser i dataåtkomstlagret.
    public class Service
    {
        #region Contact

        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            // Ett ContactDAL-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        // Tar bort specifierad kontakt ur databasen.
        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        // Hämtar en kontakt med ett specifikt kontaktnummer från databasen.
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        // Hämtar alla kontakter som finns lagrade i databasen.
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        // Spara en kontakts kontaktuppgifter i databasen.
        public void SaveContact(Contact contact)
        {
            //var validationContext = new ValidationContext(contact);
            //var validationResults = new List<ValidationResult>();
            //if (!Validator.TryValidateObject(contact, validationContext, validationResults, true))
            //{
            //    // Uppfyller inte objektet affärsreglerna kastas ett undantag med
            //    // ett allmänt felmeddelande samt en referens till samlingen med
            //    // resultat av valideringen.
            //    var ex = new ValidationException("Objektet klarade inte valideringen.");
            //    ex.Data.Add("ValidationResults", validationResults);
            //    throw ex;
            //}

            // Uppfyller inte objektet affärsreglerna...
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults)) // Använder "extension method" för valideringen!
            {                                              // Klassen finns under App_Infrastructure.
                // ...kastas ett undantag med ett allmänt felmeddelande samt en referens 
                // till samlingen med resultat av valideringen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            // Contact-objektet sparas antingen genom att en ny post 
            // skapas eller genom att en befintlig post uppdateras.
            if (contact.ContactID == 0) // Ny post om ContactID är 0!
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }

        #endregion
    }
}