﻿using HP.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HP.Shared
{
    // GOing to be used for the Identity service in the future.
    public class User
    {
        [Required]
        [StringLength(15, ErrorMessage = "User name is too long.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The password is required.")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ObservableCollection<Todo> TodoItems { get; set; }

    }


}