using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Areas.Admin.Models
{
    public class OTPViewModel
    {

        [Required]
        public string OTP { get; set; }
        
    }
}
