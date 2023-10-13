
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;




namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //Product
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            //var objProductList = _db.Categories.ToList();  
            //IEnumerable<Product> objProductList = _db.GetAll();// Strongly Type Connect With Modal
            //IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();// Strongly Type Connect With Modal
            //return View(objProductList);    
            return View();
        }


        //GET
        //public IActionResult Create|(Edit|Update)(int? id)
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                ),

                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                )

            };

            if (id == null || id == 0)
            {
                //Create Product
                return View(productVM);
            }
            else
            {
                //Update Product
            }
            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;

                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
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
            //var Product = _db.Categories.Find(id);
            //var Product  = _db.GetFirstOrDefault(u => u.Id == id);
            var Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj = _db.Categories.Find(id);
            //var obj = _db.GetFirstOrDefault(u => u.Id == id);
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(obj);
            //_db.SaveChanges();

            //_db.Remove(obj);
            //_db.Save();
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully....!";

            return RedirectToAction("Index");
        }


         
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }
    }


}


