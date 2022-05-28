using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacebookClone.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "First Name must be more than 2 characters")]
        [MaxLength(30, ErrorMessage = "First Name must be less than 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Last Name must be more than 2 characters.")]
        [MaxLength(30, ErrorMessage = "Last Name must be less than 30 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImageURL { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Country name length must be more than 1 char.")]
        [MaxLength(60, ErrorMessage = "Country name length must be less than 60 chars.")]
        public string Country { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(10, ErrorMessage = "Minimum password must be more than nine chars.")]
        public string Password { get; set; }
        [Phone]
        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(30, ErrorMessage = "Phone number mustn't exceed 30 characters.")]
        public string PhoneNumber { get; set; }
        public List<Post> Posts { get; set; }
    }
}