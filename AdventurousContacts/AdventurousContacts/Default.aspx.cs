using System;
using System.Web.UI;
using System.Collections.Generic;
using AdventurousContacts.Model;

namespace AdventurousContacts
{
    public partial class Default : System.Web.UI.Page
    {
        // Sessionsvariabel för meddelandesträng
        private string SuccessMessage
        {
            get
            {
                string successMessage = Session["SuccessMessage"] as string;
                Session.Remove("SuccessMessage");
                return successMessage;
            }
            set { Session["SuccessMessage"] = value; }
        }

        // Kontrollerar ifall sessionsvariabeln är null.
        public bool ExistingMessage
        {
            get
            {
                return Session["SuccessMessage"] != null;
            }
        }

        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Returnerar ExistingMessage true sätts texten i Succestill variabelns sträng och meddelandet visas.
            if (ExistingMessage)
            {
                SuccessPanel.Visible = true;
                SuccessLabel.Text = SuccessMessage;
            }
        }

        // Visar listan med kontakter med givet antal kontakter per sida.
        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        // Lägget rill ny kontakt.
        public void ContactListView_InsertItem(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveContact(contact);
                    SuccessMessage = String.Format("Skapandet av den nya kontakten lyckades!");
                    Response.Redirect(Request.Path);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle läggas till.");
                }
            }
        }

        // Uppdaterar en befintlig kontakt.
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
                    SuccessMessage = String.Format("Uppdateringen av kontakten lyckades!");
                    Response.Redirect(Request.Path);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle uppdateras.");
            }
        }

        // Tar bort vald kontakt.
        public void ContactListView_DeleteItem(int contactId)
        {
            try
            {
                // Kallar på javascript för att visa en confirm-box.
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Service.DeleteContact(contactId);
                    SuccessMessage = String.Format("Borttagandet av kontakten lyckades!");
                    Response.Redirect(Request.Path);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle tas bort.");
            }
        }

        // Klickar användaren på kryss-bilden så "stängs" meddelandepanelen.
        protected void CloseImageButton_Click(object sender, ImageClickEventArgs e)
        {
            SuccessPanel.Visible = false;
        }
    }
}