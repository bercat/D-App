using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {   //attributt for å overstyre databaseskjema-regelen som gjør at et datafelt kan være tomt.
        [Required]//Angir at det kreves en datafeltverdi
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}