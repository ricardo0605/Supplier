using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository,
                               IAddressRepository addressRepository,
                               INotificator notificator) : base(notificator)
        {
            this._supplierRepository = supplierRepository;
            this._addressRepository = addressRepository;
        }

        public async Task AddAsync(Supplier supplier)
        {
            if ((!ExecuteValidation(new SupplierValidation(), supplier)) 
                || (!ExecuteValidation(new AddressValidation(), supplier.Address)))
            {
                return;
            }

            if (_supplierRepository.FindAsync(f => f.DocumentNumber == supplier.DocumentNumber).Result.Any())
            {
                Notificate("There is already a supplier registred with this document number.");
                return;
            }

            await _supplierRepository.AddAsync(supplier);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }

        public async Task RemoveAsync(Guid id)
        {
            if (_supplierRepository.GetSupplierAddressAndProducts(id).Result.Products.Any())
            {
                Notificate("This supplier has products!");
                return;
            }

            var address = await _addressRepository.GetAddressBySupplier(id);

            if (address != null)
            {
                await _addressRepository.RemoveAsync(address.Id);
            }

            await _supplierRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier))
                return;

            if (_supplierRepository.FindAsync(f => f.DocumentNumber == supplier.DocumentNumber && f.Id != supplier.Id).Result.Any())
            {
                Notificate("There is already an supplier registred with this document number.");
                return;
            }

            await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task UpdateAddressAsync(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
                return;

            await _addressRepository.UpdateAsync(address);
        }
    }
}
