﻿using Microsoft.AspNetCore.Mvc;
using SalesWeb.Services;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;

namespace SalesWeb.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;

        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() { // Get
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel {  Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevenir ataques CSRF
        public IActionResult Create(Seller seller) { // Post

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Isso melhora a manutenção do sistema

        }

        public IActionResult Delete(int? id) { // Isso é como se fosse um "get" delete.
            if (id == null) {
                return NotFound(); // é nulo, cannot delete
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }

            return View(obj);
        }

    }
}
