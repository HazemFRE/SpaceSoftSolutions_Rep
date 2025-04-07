using Microsoft.AspNetCore.Mvc;
using SpaceSoftSolutions.Models;
using SpaceSoftSolutions.Service;

namespace SpaceSoftSolutions.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDbContext _context;

        public LoginController(MyDbContext obj)
        {
            _context = obj;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheackLogin(string Role, string Password, string Email)
        {
            //var userObj = _context.Hrs.SingleOrDefault(u => u.Email == Email);
            //if (userObj != null)
            //{
            //    bool IsMatchedPass = PasswordHasher.VerifyPassword(Password, userObj.PasswordHash);
            //    if (!IsMatchedPass)
            //    {
            //        ViewBag.ErrorLogin = "Invalid Username or Password";
            //        return View("Login");
            //    }
            //}

            if (Role == "HR")
            {

                var user = _context.Hrs.SingleOrDefault(u => u.Email == Email && u.PasswordHash == Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserType", "HR");
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    return RedirectToAction("Departments", "HR");

                }
                else
                {
                    ViewBag.ErrorLogin="Invalid Password , Email , Department Please Cheack";
                    return View("Login");
                }

            }

            else if (Role == "Manager")
            {
                var user = _context.Managers.SingleOrDefault(u => u.Email == Email && u.PasswordHash == Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserType", "Manager");
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    return RedirectToAction("Index", "Managers");

                }
                else
                {
                    ViewBag.ErrorLogin = "Invalid Password , Email , Department Please Cheack";
                    return View("Login");
                }

            }
            else if (Role == "Employee")
            {
                var user = _context.Employees.SingleOrDefault(u => u.Email == Email && u.PasswordHash == Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserType", "Employee");
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.ErrorLogin = "Invalid Password , Email , Department Please Cheack";
                    return View("Login");
                }

            }
            else
            {
                ViewBag.ErrorLogin = "Invalid Password , Email , Department Please Cheack";
                return View("Login");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View(nameof(Login));
        }

        public IActionResult forgetPassword()
        {
            return View();
        }
    }
}
