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

        // Privat fält
        private ContactDAL _contactDAL;

        // Egenskap som skapar en ContactDAL-klass om det inte redan finns någon.
        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        // Tar bort vald kontakt ur databasen.
        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        // Sparar en kontakts kontaktuppgifter i databasen.
        public void SaveContact(Contact contact)
        {
            // Uppfyller inte objektet affärsreglerna...
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults))
            {
                // Klarar inte objektet valideringen så kastas ett undantag, samt en referens till valideringssamlingen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            // Sparar contact-objektet. Är ContactID 0 skapas en ny kontakt...
            if (contact.ContactID == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else //...annars uppdateras en befintlig.
            {
                ContactDAL.UpdateContact(contact);
            }
        }

        // Hämtar en kontakt med ett specifikt id från databasen.
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        // Hämtar alla kontakter ur databasen.
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        // Hämtar alla kontakter och visar givet antal per sida.
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        #endregion
    }
}