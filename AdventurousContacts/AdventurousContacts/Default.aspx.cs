using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdventurousContacts.Model;

namespace AdventurousContacts
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Contact> ContactListView_GetData()
        {
            try
            {
                return Service.GetContacts();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgifter skulle hämtas.");
                return null;
            }
        }

        public void ContactListView_InsertItem(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveContact(contact);
                }
                catch (Exception ex)
                {
                    //var validationResults = ex.Data["ValidationResults"] as IEnumerable<ValidationResult>;
                    //if (validationResults != null && validationResults.Any())
                    //{
                    //    foreach (var validationResult in validationResults)
                    //    {
                    //        foreach (var memberName in validationResult.MemberNames)
                    //        {
                    //            ModelState.AddModelError(memberName, validationResult.ErrorMessage);
                    //        }
                    //    }
                    //}
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle läggas till.");
                }
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_UpdateItem(int contactId)
        {
            try
            {
                var contact = Service.GetContact(contactId);
                if (contact == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty,
                        String.Format("Kontakten med kontaktnummer {0} hittades inte.", contactId));
                    return;
                }

                if (TryUpdateModel(contact))
                {
                    Service.SaveContact(contact);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle uppdateras.");
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_DeleteItem(int contactId)
        {
            try
            {
                Service.DeleteContact(contactId);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle tas bort.");
            }
        }
    }
}