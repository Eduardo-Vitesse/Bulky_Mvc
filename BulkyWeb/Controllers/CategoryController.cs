﻿using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Category> objCategoryList = _context.Categories.ToList();

        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString())
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

        if (ModelState.IsValid)
        {
            _context.Categories.Add(obj);
            _context.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        Category? categoryFromDb = _context.Categories.Find(id);
        //Category? categoryFromDb = _context.Categories.FirstOrDefault(c => c.Id == id);
        //Category? categoryFromDb = _context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (categoryFromDb == null)
            return NotFound();

        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Update(obj);
            _context.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        Category? categoryFromDb = _context.Categories.Find(id);

        if (categoryFromDb == null)
            return NotFound();

        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Category? obj = _context.Categories.Find(id);

        if (obj == null)
            return NotFound();

        _context.Categories.Remove(obj);
        _context.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
