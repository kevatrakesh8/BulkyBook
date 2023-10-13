
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;




namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        //CoverType
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var objCoverTypeList = _db.Categories.ToList();  
            //IEnumerable<CoverType> objCoverTypeList = _db.GetAll();// Strongly Type Connect With Modal
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();// Strongly Type Connect With Modal
            return View(objCoverTypeList);
        }

        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                //_db.Add(obj);
                //_db.Save();

                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created Successfully....!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverTypeFromDb = _db.Find(id);
            //var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var CoverTypeFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            //var getFirstOrDefault = _db.GetFirstOrDefault(u => u.Id == id);
            var getFirstOrDefault = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (getFirstOrDefault == null)
            {
                return NotFound();
            }
            return View(getFirstOrDefault);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
             
            if (ModelState.IsValid)
            {
                //_db.Update(obj);
                //_db.Save();

                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Edited Successfully....!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverType = _db.Categories.Find(id);
            //var CoverType  = _db.GetFirstOrDefault(u => u.Id == id);
            var CoverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverType == null)
            {
                return NotFound();
            }
            return View(CoverType);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj = _db.Categories.Find(id);
            //var obj = _db.GetFirstOrDefault(u => u.Id == id);
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(obj);
            //_db.SaveChanges();

            //_db.Remove(obj);
            //_db.Save();
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Deleted Successfully....!";

            return RedirectToAction("Index");
        }


    }


} 