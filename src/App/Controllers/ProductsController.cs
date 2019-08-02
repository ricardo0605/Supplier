using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository,
                                  ISupplierRepository supplierRepository,
                                  IMapper mapper)
        {
            _productRepository = repository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsAndTheirSuppliers()));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetProduct(id));

            if (productViewModel == null)
                return NotFound();

            return View(productViewModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var product = await FullfillProductSuppliers(new ProductViewModel());

            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await FullfillProductSuppliers(productViewModel);

            if (!ModelState.IsValid)
                return View(productViewModel);

            await _productRepository.AddAsync(_mapper.Map<Product>(productViewModel));

            return View(productViewModel);

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await _productRepository.GetProduct(id);

            if (productViewModel == null)
                return NotFound();

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            await _productRepository.UpdateAsync(_mapper.Map<Product>(productViewModel));

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            await _productRepository.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> GetProductById(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProduct(id));
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync());

            return product;
        }

        private async Task<ProductViewModel> FullfillProductSuppliers(ProductViewModel product)
        {
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAllAsync());

            return product;
        }
    }
}
