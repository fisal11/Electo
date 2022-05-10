using AutoMapper;
using Electo.DAL.Context;
using Electo.DAL.Entities;
using Electo.PL.Models;
using Electo.PL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Electo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admins")]
    public class BrandsController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;
        private UnitOfWork _unitOfWork;
        public BrandsController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var getAllBrands = _unitOfWork.BrandRepository.Get();
            return View(_mapper.Map<IEnumerable<BrandVM>>(getAllBrands));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BrandVM  brand)
        {
            if (ModelState.IsValid)
            {
                var MappBrand = _mapper.Map<Brand>(brand);
                _unitOfWork.BrandRepository.Add(MappBrand);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var brand = _unitOfWork.BrandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            var brandModel = _mapper.Map<BrandVM>(brand);
            return View(brandModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BrandVM brand)
        {
            if (ModelState.IsValid)
            {
                var MappBrand = _mapper.Map<Brand>(brand);
                _unitOfWork.BrandRepository.Edit(MappBrand);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(brand);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var brand = _unitOfWork.BrandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            var brandModel = _mapper.Map<BrandVM>(brand);
            return View(brandModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id , BrandVM vM )
        {
            _mapper.Map<Brand>(vM);
            _unitOfWork.BrandRepository.GetById(id);
            _unitOfWork.BrandRepository.Delete(id );
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
