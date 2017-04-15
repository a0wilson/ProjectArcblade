using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models
{
    public class UserDetail
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name ="Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }
                 
        public ICollection<ClubUser> ClubUsers { get; set; }
    }
}
