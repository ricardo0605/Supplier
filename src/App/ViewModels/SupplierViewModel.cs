using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Document number")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(14, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string DocumentNumber { get; set; }

        [DisplayName("Type")]
        [Required(ErrorMessage = "{0} is required")]
        public int SupplierType { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Is Active?")]
        public bool IsActive { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
