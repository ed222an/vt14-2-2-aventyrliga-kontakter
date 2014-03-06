using System;
using System.Web.UI;
using System.Collections.Generic;
using AdventurousContacts.Model;

namespace AdventurousContacts
{
    public partial class Default : System.Web.UI.Page
    {
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

        public bool ExistingMessage
        {
            get
            {
                return Session["SuccessMessage"] != null;
            }
        }

        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ExistingMessage)
            {
                SuccessPanel.Visible = true;
                SuccesLabel.Text = SuccessMessage;
            }
        }

        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

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
                    SuccessMessage = String.Format("Uppdateringen av kontakten lyckades!");
                    Response.Redirect(Request.Path);
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

        protected void CloseImageButton_Click(object sender, ImageClickEventArgs e)
        {
            SuccessPanel.Visible = false;
        }
    }
}