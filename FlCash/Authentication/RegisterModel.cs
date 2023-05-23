using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Authentication
{
    public class RegisterModel
    {
        

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Face Book Id is required")]
        public string FaceId { get; set; }
    }
}
