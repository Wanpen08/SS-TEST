using Microsoft.AspNetCore.Mvc;
using MyWebApp.DEL;
using MyWebApp.Models;
using MyWebApp.Models.DBEntities;

namespace MyWebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly UsersDBContext _context;
        public UserController(UsersDBContext context)
        { 
            this._context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userData = _context.UsersData.ToList();
            List<UsersViewModel> usersList = new List<UsersViewModel>();

            if (userData != null)
            {
                foreach (var data in userData)
                {
                    var UsersViewModel = new UsersViewModel()
                    {
                        ID = data.ID,
                        Name = data.Name,
                        Address = data.Address,
                        Marks = data.Marks
                    };
                    usersList.Add(UsersViewModel);
                }
                return View("~/Views/User/ShowData.cshtml", usersList);

            }

            return View("~/Views/User/ShowData.cshtml");

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UsersViewModel usersData) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var users = new User()
                    {
                        ID = usersData.ID,
                        Name = usersData.Name,
                        Address = usersData.Address,
                        Marks = usersData.Marks,
                        CreateDate = usersData.CreateDate
                    };
                    _context.UsersData.Add(users);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Create successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Data is not valid.";
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            try
            {
                var users = _context.UsersData.SingleOrDefault(u => u.ID == id);
                if (users != null)
                {
                    var usersView = new UsersViewModel()
                    {
                        ID = users.ID,
                        Name = users.Name,
                        Address = users.Address,
                        Marks = users.Marks
                    };
                    return View("~/Views/User/Edit.cshtml", usersView);
                }
                else
                {
                    TempData["errorMessage"] = $"Invalid data or ID : {id}.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UsersViewModel usersEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var users = new User()
                    {
                        ID = usersEdit.ID,
                        Name = usersEdit.Name,
                        Address = usersEdit.Address,
                        Marks = usersEdit.Marks,
                        UpdateDate = usersEdit.UpdateDate
                    };
                    _context.UsersData.Update(users);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Update successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Invalid data";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            try
            {
                var users = _context.UsersData.SingleOrDefault(u => u.ID == id);
                if (users != null)
                {
                    var usersView = new UsersViewModel()
                    {
                        ID = users.ID,
                        Name = users.Name,
                        Address = users.Address,
                        Marks = users.Marks
                    };
                    return View("~/Views/User/View.cshtml", usersView);
                }
                else
                {
                    TempData["errorMessage"] = $"Invalid data or ID : {id}.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult GetDetails(int id)
        {
            var user = _context.UsersData.FirstOrDefault(u => u.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(new { id = user.ID, name = user.Name, address = user.Address, marks = user.Marks });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var users = _context.UsersData.SingleOrDefault(u => u.ID == id);
                if (users != null)
                {
                    _context.UsersData.Remove(users);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Delete successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Invalid data";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
