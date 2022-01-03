using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "*required"), MaxLength(200)]
        public string Street { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(100)]
        public string City { get; set; }

        [MaxLength(60)]
        public string State { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(90)]
        public string Country { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(18)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(100)]
        public string CCHolder { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(16)]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Invalid card number.")]
        public string CCNumber { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(3)]
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Invalid cvv.")]
        public string CCCvv { get; set; }

        [Required(ErrorMessage = "*required"), MaxLength(5)]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Invalid expiration date.")]
        public string CCExpiration { get; set; }
    }
}
