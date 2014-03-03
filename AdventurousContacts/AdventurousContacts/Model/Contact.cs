using System.ComponentModel.DataAnnotations;

namespace AdventurousContacts.Model
{
    public class Contact
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage = "Ett förnamn måste anges.")]
        [StringLength(50, ErrorMessage = "Förnamnet kan bestå av som mest 50 tecken.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ett efternamn måste anges.")]
        [StringLength(50, ErrorMessage = "Efternamnet kan bestå av som mest 50 tecken.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "En mailaddress måste anges.")]
        [StringLength(50, ErrorMessage = "Mailaddressen kan bestå av som mest 50 tecken.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Mailaddressen verkar inte vara korrekt.")]
        public string EmailAddress { get; set; }

    }
}