﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Publisher> objList = _db.Publisher.ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Publisher obj = new Publisher();
            if (id == null)
                return View(obj);

            obj = _db.Publisher.FirstOrDefault(u => u.Publisher_Id == id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Publisher_Id == 0)
                {
                    //this is create
                    _db.Publisher.Add(obj);
                }
                else
                {
                    //this is an update
                    _db.Publisher.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            var objFormDb = _db.Publisher.FirstOrDefault(u => u.Publisher_Id == id);
            _db.Publisher.Remove(objFormDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
