using AutoMapper;
using Electo.DAL.Context;
using Electo.DAL.Entities;
using Electo.PL.Models;
using Electo.PL.Repositories;
using Electo.Web.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Electo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admins")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;
        private UnitOfWork _unitOfWork;
        public ProductsController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
        }
        public IActionResult Index( )
        {
            var getData = _context.Product.Select(a => new ProductVM
            {
                Id = a.Id,
                Name = a.Name,
                Salary = a.Salary,
                Image = a.Image,
                Descrption = a.Descrption,
                BrandName = a.Brand.BrandName,
                CategoryName = a.Category.CategoryName
            }).ToList();
            return View(getData);
        }
        public IActionResult Create()
        {
            var BrandName = _unitOfWork.BrandRepository.Get();
            var CategoryName = _unitOfWork.CategoryRepository.Get();

            ViewBag.BrandList = new SelectList(BrandName, "Id", "BrandName");
            ViewBag.CategoryList = new SelectList(CategoryName, "Id", "CategoryName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM product)
        {
            try
            {
                var MappProduct = _mapper.Map<Product>(product);
                MappProduct.Image = UploadFile.SavaFile(product.PhotoUrl, "Photos");

                var BrandName = _unitOfWork.BrandRepository.Get();
                var CategoryName = _unitOfWork.CategoryRepository.Get();

                ViewBag.BrandList = new SelectList(BrandName, "Id", "BrandName");
                ViewBag.CategoryList = new SelectList(CategoryName, "Id", "CategoryName");

                _unitOfWork.ProductRepository.Add(MappProduct);
                _unitOfWork.Save();

                return RedirectToAction("Index");


            }
            catch (Exception e)
            {

                return View(e);
            }
            
        }
        public IActionResult Edit(int? id)
        {
           
            var product = _unitOfWork.ProductRepository.GetById(id);
            var BrandName = _unitOfWork.BrandRepository.Get();
            var CategoryName = _unitOfWork.CategoryRepository.Get();
            ViewBag.BrandList = new SelectList(BrandName,
                                "Id", "BrandName", product);
            ViewBag.CategoryList = new SelectList(CategoryName,
                                "Id", "CategoryName", product);
            var productModel = _mapper.Map<ProductVM>(product);
            return View(productModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM product)
        {
            try
            {
                
                var MappProduct = _mapper.Map<Product>(product);
                MappProduct.Image = UploadFile.SavaFile(product.PhotoUrl, "Photos");
               
                var BrandName = _unitOfWork.BrandRepository.Get();
                var CategoryName = _unitOfWork.CategoryRepository.Get();
                ViewBag.BrandList = new SelectList(BrandName,
                        "Id", "BrandName", product.BrandId);
                ViewBag.CategoryList = new SelectList(CategoryName,
                                    "Id", "CategoryName", product.CategoryId);

                _unitOfWork.ProductRepository.Edit(MappProduct);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception E)
            {

                return View(E);
            }

        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = _unitOfWork.ProductRepository.GetItmeWithQyry((int)id);
            if (product == null)
            {
                return NotFound();
            }
            var productModel = _mapper.Map<ProductVM>(product);
            return View(productModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id , ProductVM vM )
        {

            var  data = _mapper.Map<Product>(vM);
            UploadFile.DeleteFile("Photos/", data.Image);
            _unitOfWork.ProductRepository.GetItmeWithQyry(id);
            _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
           
        }
        public IActionResult Details(int id)
        {

           var data = _unitOfWork.ProductRepository.GetItmeWithQyry(id);
            return View(data);
        }
    }
}
