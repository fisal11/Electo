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
    public class CategorysController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;
        private UnitOfWork _unitOfWork;
        public CategorysController(IMapper mapper , ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var getAllCategory = _unitOfWork.CategoryRepository.Get();
            return View(_mapper.Map<IEnumerable<CategoryVM>>(getAllCategory));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryVM category)
        {
            if (ModelState.IsValid) 
            {
                var MappCategory = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepository.Add(MappCategory);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryModel = _mapper.Map<CategoryVM>(category);
            return View(categoryModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryVM category)
        {
            if (ModelState.IsValid) 
            {
                var MappCategory = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepository.Edit(MappCategory);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryModel = _mapper.Map<CategoryVM>(category);
            return View(categoryModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id ,CategoryVM vM )
        {
            _mapper.Map<Category>(vM);
            _unitOfWork.CategoryRepository.GetById(id);
            _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
