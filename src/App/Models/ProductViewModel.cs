﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} needs to have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(1000, ErrorMessage = "{0} needs to have between {2} and {1} characters")]
        public string Description { get; set; }

        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegistryDate { get; set; }

        [DisplayName("Is Active?")]
        public bool Active { get; set; }

        public SupplierViewModel Supplier { get; set; }
    }
}
