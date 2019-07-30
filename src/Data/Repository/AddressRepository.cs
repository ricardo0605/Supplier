using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(SupplierContext context)
            : base(context)
        {
        }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await Context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == supplierId);
        }
    }
}
