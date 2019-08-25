using App.Extensions;
using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository,
                                   ISupplierService supplierService,
                                   INotificator notificator,
                                   IMapper mapper) : base(notificator)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        // GET: Suppliers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync()));
        }

        // GET: Suppliers/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressAndProducts(id));

            if (supplierViewModel == null)
                return NotFound();

            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        [ClaimsAuthorize("Suppliers", "Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ClaimsAuthorize("Suppliers", "Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierService.AddAsync(supplier);

            if(!OperationIsValid())
                return View(supplierViewModel);

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Suppliers", "Edit")]
        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressAndProducts(id));

            if (supplierViewModel == null)
                return NotFound();

            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ClaimsAuthorize("Suppliers", "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierService.UpdateAsync(supplier);

            if (!OperationIsValid())
                return View(supplierViewModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Delete/5
        [ClaimsAuthorize("Suppliers", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressAndProducts(id));

            if (supplierViewModel == null)
                return NotFound();

            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [ClaimsAuthorize("Suppliers", "Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await _supplierRepository.GetByIdAsync(id);

            if (supplierViewModel == null)
                return NotFound();

            await _supplierService.RemoveAsync(id);

            if (!OperationIsValid())
                return View(supplierViewModel);

            ViewData["Success"] = $"Supplier {supplierViewModel.Name} successfully excluded.";

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Suppliers", "Edit")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressAndProducts(id));

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateAddress", new SupplierViewModel { Address = supplierViewModel.Address });
        }


        [AllowAnonymous]

        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddress(id));

            if (supplier == null)
                return NotFound();

            return PartialView("_DetailsAddress", supplier);
        }

        [ClaimsAuthorize("Suppliers", "Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("DocumentNumber");

            if (!ModelState.IsValid)
                return PartialView("_updateAddress", supplierViewModel);

            await _supplierService.UpdateAddressAsync(_mapper.Map<Address>(supplierViewModel.Address));

            if (!OperationIsValid())
                return View(supplierViewModel);

            var url = Url.Action("GetAddress", "Suppliers", new { id = supplierViewModel.Address.SupplierId });
            return Json(new { success = true, url });
        }
    }
}
