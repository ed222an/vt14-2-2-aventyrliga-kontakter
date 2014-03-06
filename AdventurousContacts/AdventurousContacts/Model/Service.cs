using System;
using System.Collections.Generic;
using AdventurousContacts.Model.DAL;
using System.ComponentModel.DataAnnotations;
using AdventurousContacts.App_Infrastructure;

namespace AdventurousContacts.Model
{
    public class Service
    {
        #region Contact

        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            // Ett ContactDAL-objekt skapas först då det behövs för första gången
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        // Tar bort specifierad kontakt ur databasen.
        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        // Spara en kontakts kontaktuppgifter i databasen.
        public void SaveContact(Contact contact)
        {
            // Uppfyller inte objektet affärsreglerna...
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults)) // Använder "extension method" för valideringen!
            {
                // ...kastas ett undantag med ett allmänt felmeddelande samt en referens till samlingen med resultat av valideringen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            // Contact-objektet sparas antingen genom att en ny post skapas eller genom att en befintlig post uppdateras.
            if (contact.ContactID == 0) // Ny post om ContactID är 0!
            {
                ContactDAL.InsertContact(contact);
            }
            else // Annars är det en uppdatering som ska göras.
            {
                ContactDAL.UpdateContact(contact);
            }
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

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        #endregion
    }
}