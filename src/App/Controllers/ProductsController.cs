using App.ViewModels;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository,
                                  ISupplierRepository supplierRepository,
                                  IProductService productService,
                                  INotificator notificator,
                                  IMapper mapper) : base(notificator)
        {
            _productRepository = repository;
            _supplierRepository = supplierRepository;
            _productService = productService;
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

            var imageNamePrefix = $"{Guid.NewGuid()}_";

            if (!await UploadFile(productViewModel.ImageUpload, imageNamePrefix))
                return View(productViewModel);

            productViewModel.Image = imageNamePrefix + productViewModel.ImageUpload.FileName;

            await _productService.AddAsync(_mapper.Map<Product>(productViewModel));

            if (!OperationIsValid())
            {
                return View(productViewModel);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetProduct(id));

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

            var updatedProduct = await GetProductById(productViewModel.Id);
            productViewModel.Supplier = updatedProduct.Supplier;
            productViewModel.Image = updatedProduct.Image;

            if (!ModelState.IsValid)
                return View(productViewModel);

            if (productViewModel.ImageUpload != null)
            {
                var imageNamePrefix = $"{Guid.NewGuid()}_";

                if (!await UploadFile(productViewModel.ImageUpload, imageNamePrefix))
                    return View(productViewModel);

                updatedProduct.Image = imageNamePrefix + productViewModel.ImageUpload.FileName;
            }

            updatedProduct.Name = productViewModel.Name;
            updatedProduct.Description = productViewModel.Description;
            updatedProduct.Value = productViewModel.Value;
            updatedProduct.Active = productViewModel.Active;

            await _productService.UpdateAsync(_mapper.Map<Product>(updatedProduct));

            if (!OperationIsValid())
            {
                return View(productViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));

            if (productViewModel == null)
                return NotFound();

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            await _productService.RemoveAsync(id);

            if (!OperationIsValid())
            {
                return View(product);
            }

            ViewData["Success"] = $"Product {product.Name} successfully excluded.";

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

        private async Task<bool> UploadFile(IFormFile file, string imageNamePrefix)
        {
            if (file.Length <= 0)
                return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageNamePrefix + file.FileName);

            if (System.IO.File.Exists(path))
                ModelState.AddModelError(string.Empty, "File already exists.");

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);

            return true;
        }
    }
}
