using SocietyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocietyApp.Controllers
{
    public class SocietyController : Controller
    {
        SocietyEntities db = new SocietyEntities();

        // GET: Society
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MemberRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberRegister(Member r)
        {
            Member obj = new Member();
            if (ModelState.IsValid)
            {
                obj.MemID = r.MemID;
                obj.Name = r.Name;
                obj.Address = r.Address;
                obj.Contact = r.Contact;
                obj.Password = r.Password;

                db.Members.Add(obj);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "<script>alert('inserted')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('not inserted')</script>";
                    return RedirectToAction("Index");
                }
            }
            return View();

        }
        public ActionResult MemberLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberLogin(Member mem)
        {
            if (ModelState.IsValid == true)
            {
                var data = db.Members.Where(m => m.MemID == mem.MemID && m.Password == mem.Password).FirstOrDefault();
            if(data == null)
                {
                    ViewBag.ErrorMessage = "Login failed";
                    return View();
                }
                else
                {
                    Session["username"] = mem.MemID;
                    return RedirectToAction("MemberPortal");
                }
            }

            return View();
        }
        public ActionResult MemberPortal()
        {
            return View();
        }

        public ActionResult MemberLogout()
        {
            Session.Abandon();

            return RedirectToAction("Index");
        }

        public ActionResult RentorRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RentorRegister(Rentor r)
        {
            Rentor obj = new Rentor();
            if (ModelState.IsValid)
            {
                obj.RentorID = r.RentorID;
                obj.Name = r.Name;
                obj.Address = r.Address;
                obj.Contact = r.Contact;
                obj.Password = r.Password;

                db.Rentors.Add(obj);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "<script>alert('inserted')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('not inserted')</script>";
                    return RedirectToAction("Index");
                }
            }
            return View();

        }
        public ActionResult RentorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RentorLogin(Rentor mem)
        {
            if (ModelState.IsValid == true)
            {
                var data = db.Rentors.Where(m => m.RentorID == mem.RentorID && m.Password == mem.Password).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.ErrorMessage = "Login failed";
                    return View();
                }
                else
                {
                    Session["username"] = mem.RentorID;
                    return RedirectToAction("RentorPortal");
                }
            }

            return View();
        }
        public ActionResult RentorPortal()
        {
            return View(); 
        }

        public ActionResult RentorLogout()
        {
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}