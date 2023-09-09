using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitche3.Models
{
    public class User
    {

        public string Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "First name should be between 4 and 30 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Last name should be between 4 and 30 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Email address not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Username should be between 4 and 15 characters")]
        public string Username { get; set; }

        public string Bio { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed")]
        public String Photo { get; set; }

    }
}