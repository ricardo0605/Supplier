﻿using System.Collections.Generic;

namespace Business.Models
{
    public class Supplier : Entity
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public SupplierType SupplierType { get; set; }
        public Address Address { get; set; }
        public bool IsActive { get; set; }

        /*EF Relations */
        public IEnumerable<Product> Products { get; set; }
    }
}
