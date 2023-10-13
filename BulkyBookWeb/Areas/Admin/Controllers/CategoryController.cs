
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;




namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //Category
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var objCategoryList = _db.Categories.ToList();  
            //IEnumerable<Category> objCategoryList = _db.GetAll();// Strongly Type Connect With Modal
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();// Strongly Type Connect With Modal
            return View(objCategoryList);
        }

        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                //_db.Add(obj);
                //_db.Save();

                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully....!";
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
            //var categoryFromDb = _db.Find(id);
            //var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            //var getFirstOrDefault = _db.GetFirstOrDefault(u => u.Id == id);
            var getFirstOrDefault = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (getFirstOrDefault == null)
            {
                return NotFound();
            }
            return View(getFirstOrDefault);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                //_db.Update(obj);
                //_db.Save();

                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully....!";
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
            //var category = _db.Categories.Find(id);
            //var category  = _db.GetFirstOrDefault(u => u.Id == id);
            var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj = _db.Categories.Find(id);
            //var obj = _db.GetFirstOrDefault(u => u.Id == id);
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(obj);
            //_db.SaveChanges();

            //_db.Remove(obj);
            //_db.Save();
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully....!";

            return RedirectToAction("Index");
        }


    }


}



#region ##Operation1


//#########################################################################################################################################
//                                                                  ##Program.cs
//#########################################################################################################################################

//builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();





//#########################################################################################################################################
//                                                                  ##ModelRepository
//#########################################################################################################################################


//##IRepository
//namespace BulkyBook.DataAccess.Repository.IRepository
//{
//    public interface IRepository<T> where T : class
//    {
//        //T - Category
//        //T GetFirstOrDefault(Expression<Func<T, bool>> filter);
//        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
//        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
//        void Add(T entity);
//        void Remove(T entity);
//        void RemoveRange(IEnumerable<T> entity);
//    }
//}

//##Repository
//namespace BulkyBook.DataAccess.Repository
//{
//    public class Repository<T> : IRepository<T> where T : class
//    {
//        private readonly ApplicationDbContext _db;
//        internal DbSet<T> dbSet;
//        public Repository(ApplicationDbContext db)
//        {
//            _db = db;
//            this.dbSet = _db.Set<T>();

//        }
//        public void Add(T entity)
//        {
//            // _db.Categories.Add(obj);
//            dbSet.Add(entity);
//        }

//        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
//        {  //var objCategoryList = _db.Categories.ToList();  
//            IQueryable<T> query = dbSet;
//            return query.ToList();
//        }

//        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
//        {

//            //var categoryFromDb = _db.Categories.Find(id);
//            ////var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
//            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
//            IQueryable<T> query = dbSet;
//            query = query.Where(filter);
//            return query.FirstOrDefault();
//        }

//        public void Remove(T entity)
//        {
//            //_db.Categories.Remove(obj);
//            dbSet.Remove(entity);
//        }

//        public void RemoveRange(IEnumerable<T> entity)
//        {
//            dbSet.RemoveRange(entity);

//        }
//    }
//}

//##ICategoryRepository
//namespace BulkyBook.DataAccess.Repository.IRepository
//{
//    public interface ICategoryRepository : IRepository<Category>
//    {
//        void Update(Category obj);

//        void Save();
//    } 

//}


//##CategoryRepository
//namespace BulkyBook.DataAccess.Repository
//{
//    public class CategoryRepository : Repository<Category>, ICategoryRepository
//    {
//        private ApplicationDbContext _db;
//        public CategoryRepository(ApplicationDbContext db) : base(db)
//        {
//            _db = db;
//        }

//        public void Save()
//        {
//            _db.SaveChanges();
//        }

//        public void Update(Category obj)
//        {
//            _db.Categories.Update(obj);
//        }
//    }
//}
//#########################################################################################################################################
//                                                                  ##Controller
//#########################################################################################################################################

//namespace BulkyBookWeb.Controllers
//{
//    public class CategoryController : Controller
//    {
//        //Category
//        private readonly ICategoryRepository _db;
//        public CategoryController(ICategoryRepository db)
//        {
//            _db = db;
//        }

//        public IActionResult Index()
//        {
//            //var objCategoryList = _db.Categories.ToList();  
//            IEnumerable<Category> objCategoryList = _db.GetAll();// Strongly Type Connect With Modal
//            return View(objCategoryList);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        //POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Category obj)
//        {
//            if (ModelState.IsValid)
//            {
//                _db.Add(obj);
//                _db.Save();
//                TempData["success"] = "Category Created Successfully....!";
//                return RedirectToAction("Index");
//            }
//            return View(obj);
//        }

//        //GET
//        public IActionResult Edit(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound();
//            }
//            //var categoryFromDb = _db.Find(id);
//            //var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
//            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
//            var getFirstOrDefault = _db.GetFirstOrDefault(u => u.Id == id);

//            if (getFirstOrDefault == null)
//            {
//                return NotFound();
//            }
//            return View(getFirstOrDefault);
//        }


//        //POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Category obj)
//        {
//            if (obj.Name == obj.DisplayOrder.ToString())
//            {
//                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
//            }
//            if (ModelState.IsValid)
//            {
//                _db.Update(obj);
//                _db.Save();
//                TempData["success"] = "Category Edited Successfully....!";
//                return RedirectToAction("Index");
//            }
//            return View(obj);
//        }


//        public IActionResult Delete(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound();
//            }
//            //var category = _db.Categories.Find(id);
//            var category = _db.GetFirstOrDefault(u => u.Id == id);
//            if (category == null)
//            {
//                return NotFound();
//            }
//            return View(category);
//        }


//        //POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeletePost(int? id)
//        {
//            //var obj = _db.Categories.Find(id);
//            var obj = _db.GetFirstOrDefault(u => u.Id == id);
//            if (obj == null)
//            {
//                return NotFound();
//            }
//            //_db.Categories.Remove(obj);
//            //_db.SaveChanges();

//            _db.Remove(obj);
//            _db.Save();
//            TempData["success"] = "Category Deleted Successfully....!";

//            return RedirectToAction("Index");
//        }
//    }
//}
#endregion

#region Opearation2 Add UnitOfWorks
//public class CategoryController : Controller
//{
//    //Category
//    private readonly IUnitOfWork _unitOfWork;
//    public CategoryController(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//    }

//    public IActionResult Index()
//    {
//        //var objCategoryList = _db.Categories.ToList();  
//        //IEnumerable<Category> objCategoryList = _db.GetAll();// Strongly Type Connect With Modal
//        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();// Strongly Type Connect With Modal
//        return View(objCategoryList);
//    }

//    public ActionResult Create()
//    {
//        return View();
//    }

//    //POST
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Create(Category obj)
//    {
//        if (ModelState.IsValid)
//        {
//            //_db.Add(obj);
//            //_db.Save();

//            _unitOfWork.Category.Add(obj);
//            _unitOfWork.Save();
//            TempData["success"] = "Category Created Successfully....!";
//            return RedirectToAction("Index");
//        }
//        return View(obj);
//    }

//    //GET
//    public IActionResult Edit(int? id)
//    {
//        if (id == null || id == 0)
//        {
//            return NotFound();
//        }
//        //var categoryFromDb = _db.Find(id);
//        //var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
//        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
//        //var getFirstOrDefault = _db.GetFirstOrDefault(u => u.Id == id);
//        var getFirstOrDefault = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

//        if (getFirstOrDefault == null)
//        {
//            return NotFound();
//        }
//        return View(getFirstOrDefault);
//    }


//    //POST
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Edit(Category obj)
//    {
//        if (obj.Name == obj.DisplayOrder.ToString())
//        {
//            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
//        }
//        if (ModelState.IsValid)
//        {
//            //_db.Update(obj);
//            //_db.Save();

//            _unitOfWork.Category.Update(obj);
//            _unitOfWork.Save();
//            TempData["success"] = "Category Edited Successfully....!";
//            return RedirectToAction("Index");
//        }
//        return View(obj);
//    }


//    public IActionResult Delete(int? id)
//    {
//        if (id == null || id == 0)
//        {
//            return NotFound();
//        }
//        //var category = _db.Categories.Find(id);
//        //var category  = _db.GetFirstOrDefault(u => u.Id == id);
//        var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
//        if (category == null)
//        {
//            return NotFound();
//        }
//        return View(category);
//    }


//    //POST
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult DeletePost(int? id)
//    {
//        //var obj = _db.Categories.Find(id);
//        //var obj = _db.GetFirstOrDefault(u => u.Id == id);
//        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
//        if (obj == null)
//        {
//            return NotFound();
//        }
//        //_db.Categories.Remove(obj);
//        //_db.SaveChanges();

//        //_db.Remove(obj);
//        //_db.Save();
//        _unitOfWork.Category.Remove(obj);
//        _unitOfWork.Save();
//        TempData["success"] = "Category Deleted Successfully....!";

//        return RedirectToAction("Index");
//    }


//}

#endregion
