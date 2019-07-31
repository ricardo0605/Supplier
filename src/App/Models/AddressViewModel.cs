using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Number { get; set; }

        [StringLength(250, ErrorMessage = "{0} can't have more than {1} characters", MinimumLength = 2)]
        public string AditionalInfo { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(8, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 8)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string State { get; set; }

        [HiddenInput]
        public Guid SupplierId { get; set; }
    }
}
