using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISupplierService : IDisposable
    {
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task RemoveAsync(Guid id);

        Task UpdateAddressAsync(Address address);
    }
}
