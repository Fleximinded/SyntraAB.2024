using Microsoft.AspNetCore.Identity;
using Syntra.Fietshersteller.Db.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntra.Fietshersteller.Db.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public Person? PersonalInfo { get; set; }
        public string? PersonId { get; set; }
    }

}
