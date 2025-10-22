using Microsoft.Ajax.Utilities;
using SocietyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SocietyApp.Controllers
{
    public class AdminController : Controller
    {
        SocietyEntities db = new SocietyEntities();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else { return View(); }
           
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin mem)
        {
            if (ModelState.IsValid == true)
            {
                var data = db.Admins.Where(m => m.Username == mem.Username && m.Password == mem.Password).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.ErrorMessage = "Login failed";
                    return View();
                }
                else
                {
                    Session["username"] = mem.Username;
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        public ActionResult AdminLogout()
        {
            Session.Abandon();

            return RedirectToAction("Index","Home");
        }
        public ActionResult MembersUI()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = db.Members.ToList();
                return View(data);
            }
           
        }
        public ActionResult CreateMember()
        {
            if (Session["username"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            else { return View(); }
        }

        [HttpPost]
        public ActionResult CreateMember(Models.Member r)
        {

            Models.Member obj = new Models.Member();
            if (ModelState.IsValid)
            {
                var row = db.Members.Where(x => x.MemID == r.MemID).FirstOrDefault();
                if (row != null)
                {
                    ViewBag.ErrorMessage = "MemberId already existing,please try with some other MemberId";
                    return View();
                }
                else
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
                        TempData["InsertMessage"] = "<script>alert('Record inserted')</script>";
                        return RedirectToAction("MembersUI");
                    }
                    else
                    {
                        TempData["InsertMessage"] = "<script>alert('Record not inserted')</script>";
                        return RedirectToAction("MembersUI");
                    }
                }
               
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var row = db.Members.Where(x => x.MemID == id).FirstOrDefault();
                if(row == null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                { return View(row); }
               
            }
           
        }

        [HttpPost]
        public ActionResult Edit(Models.Member r)
        {
            // var row = db.records.Where(x => x.roll == id).FirstOrDefault();
            //return View(row);
            if (ModelState.IsValid == true)
            {
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Record updated')</script>";
                    return RedirectToAction("MembersUI");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Record not updated')</script>";
                    return RedirectToAction("MembersUI");
                }

            }
            return View();
        }
        public ActionResult Delete(string id)
        {
            if (id !=null)
            {
                var row = db.Members.Where(x => x.MemID == id).FirstOrDefault();
                if (row != null)
                {
                    var newRow = db.MemberPremiseDetails.Where(x => x.MemID == row.MemID).FirstOrDefault();
                    if (newRow != null)
                    {
                        //TempData["isDeleted"] = "<script>alert('Record cannot be deleted as it's related data is avalaible')</script>";
                        TempData["DeleteMessage"] = "<script>alert('Record cannot be deleted as the related data is avalaible')</script>";
                        return RedirectToAction("MembersUI");
                    }
                    else
                    {
                        db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record deleted')</script>";
                            return RedirectToAction("MembersUI");
                        }
                        else
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record not deleted')</script>";
                            return RedirectToAction("MembersUI");
                        }
                    }
                  
                }
                else
                {
                    TempData["isDeleted"] = "<script>alert('Record cannot be deleted as the related data is avalaible')</script>";
                    return RedirectToAction("MembersUI");
                    
                }
            }
            else
            {
                 return RedirectToAction("MembersUI");
            }
            return View();
        }

        public ActionResult Details(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var row = db.Members.Where(x => x.MemID == id).FirstOrDefault();
                return View(row);
            }
           
        }
        public ActionResult RentorUI()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = db.Rentors.ToList();
                return View(data);
            }
           
        }
        public ActionResult CreateRentor()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else { return View(); }
        }

        [HttpPost]
        public ActionResult CreateRentor(Models.Rentor r)
        {
            Models.Rentor obj = new Models.Rentor();
            if (ModelState.IsValid)
            {
                var row = db.Rentors.Where(x => x.RentorID == r.RentorID).FirstOrDefault();
                if (row != null)
                {
                    ViewBag.ErrorMessage = "RentorID already existing,please try with some other MemberId";
                    return View();
                }
                else
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
                    TempData["InsertMessage"] = "<script>alert('Record inserted')</script>";
                    return RedirectToAction("RentorUI");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('Record not inserted')</script>";
                    return RedirectToAction("RentorUI");
                }
            }
            }
            return View();
        }
        public ActionResult EditRentor(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var row = db.Rentors.Where(x => x.RentorID == id).FirstOrDefault();
                if (row == null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                { return View(row); }
            }
           
        }

        [HttpPost]
        public ActionResult EditRentor(Models.Rentor r)
        {
            // var row = db.records.Where(x => x.roll == id).FirstOrDefault();
            //return View(row);
            if (ModelState.IsValid == true)
            {
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Record updated')</script>";
                    return RedirectToAction("RentorUI");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Record not updated')</script>";
                    return RedirectToAction("RentorUI");
                }

            }
            return View();
        }
        public ActionResult DeleteRentor(string id)
        {
            if (id != null)
            {
                var row = db.Rentors.Where(x => x.RentorID == id).FirstOrDefault();
                if (row != null)
                {
                    var newRow = db.RentorPremiseDetails.Where(x => x.RentorID == row.RentorID).FirstOrDefault();
                    if (newRow != null)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Record cannot be deleted as the related data is avalaible')</script>";
                        return RedirectToAction("RentorUI");
                    }
                    else
                    {
                        db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["DeleteMessage"] = "<script>alert('Recorddeleted')</script>";
                            return RedirectToAction("RentorUI");
                        }
                        else
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record not deleted')</script>";
                            return RedirectToAction("RentorUI");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("RentorUI");

                }
            }
            else
            {
                return RedirectToAction("RentorUI");

            }
            //return View();
        }

        public ActionResult DetailsRentor(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var row = db.Rentors.Where(x => x.RentorID == id).FirstOrDefault();
                return View(row);
            }
            
        }
        public ActionResult PremisesUI()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = db.Premises.ToList();
                return View(data);
            }
         
        }
        public ActionResult CreatePremise()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else { return View(); }
           
        }

        [HttpPost]
        public ActionResult CreatePremise(Models.Premis r)
        {
            Models.Premis obj = new Models.Premis();
            if (ModelState.IsValid)
            {
                var row = db.Premises.Where(x => x.PremiseID == r.PremiseID).FirstOrDefault();
                if (row != null)
                {
                    ViewBag.ErrorMessage = "PremiseID already existing,please try with some other MemberId";
                    return View();
                }
                else
                {

                    obj.PremiseID = r.PremiseID;
                obj.Floor = r.Floor;
                obj.Type = r.Type;
               

                db.Premises.Add(obj);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "<script>alert('Record inserted')</script>";
                    return RedirectToAction("PremisesUI");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('Record not inserted')</script>";
                    return RedirectToAction("PremisesUI");
                }
            }
        }
            return View();
        }
        public ActionResult EditPremise(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                var row = db.Premises.Where(x => x.PremiseID == id).FirstOrDefault();
                if (row == null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                { return View(row); }
            }
        }

        [HttpPost]
        public ActionResult EditPremise(Models.Premis r)
        {
            // var row = db.records.Where(x => x.roll == id).FirstOrDefault();
            //return View(row);
            if (ModelState.IsValid == true)
            {
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Record updated')</script>";
                    return RedirectToAction("PremisesUI");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Record not updated')</script>";
                    return RedirectToAction("PremisesUI");
                }

            }
            return View();
        }
        public ActionResult DeletePremise(string id)
        {
            if (id != null)
            {
                var row = db.Premises.Where(x => x.PremiseID == id).FirstOrDefault();
                if (row != null)
                {
                    var newRow = db.MemberPremiseDetails.Where(x => x.PremiseID == row.PremiseID).FirstOrDefault();
                    if (newRow != null)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Record cannot be deleted as the related data is avalaible')</script>";
                        return RedirectToAction("PremisesUI");
                    }
                    else {
                        db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record deleted')</script>";
                            return RedirectToAction("PremisesUI");
                        }
                        else
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record not deleted')</script>";
                            return RedirectToAction("PremisesUI");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("PremisesUI");

                }
            }
            else
            {
                return RedirectToAction("PremisesUI");

            }
            //return View();
        }

        public ActionResult DetailsPremise(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                var row = db.Premises.Where(x => x.PremiseID == id).FirstOrDefault();
                return View(row);
            }
        }

        public ActionResult LinkMembertoPremiseUI()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = db.MemberPremiseDetails.ToList();
                return View(data);
            }

        }
        public ActionResult LinkMembertoPremise()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }else
            {
                var recordsNotInTable = from a in db.Premises
                                         where !db.MemberPremiseDetails.Select(b => b.PremiseID).Contains(a.PremiseID)
                                         select a;
                //var premises = db.Premises.ToList();
                ViewBag.prem = new SelectList(recordsNotInTable, "PremiseID", "PremiseID");
                var mem = db.Members.ToList();
                //var recordsNotInTableB = from a in db.Members
                                         //where !db.MemberPremiseDetails.Select(b => b.MemID).Contains(a.MemID)
                                         //select a;
                ViewBag.mems = new SelectList(mem, "MemID", "MemID");
                return View();
            }
        }

        [HttpPost]
        public ActionResult LinkMembertoPremise(Models.MemberPremiseDetail r)
        {
            Models.MemberPremiseDetail obj = new Models.MemberPremiseDetail();
            if (ModelState.IsValid)
            {
                if(r.PremiseID == null)
                {
                    TempData["InsertMessage"] = "<script>alert('Please select atleast one premises')</script>";
                    return RedirectToAction("LinkMembertoPremiseUI");
                }
                else if (r.MemID==null)
                {
                    TempData["InsertMessage"] = "<script>alert('Please select atleast one member')</script>";
                    return RedirectToAction("LinkMembertoPremiseUI");
                }
                else

                {
                    obj.MemID = r.MemID;
                    obj.PremiseID = r.PremiseID;


                    db.MemberPremiseDetails.Add(obj);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["InsertMessage"] = "<script>alert('Record inserted')</script>";
                        return RedirectToAction("LinkMembertoPremiseUI");
                    }
                    else
                    {
                        TempData["InsertMessage"] = "<script>alert('Record not inserted')</script>";
                        return RedirectToAction("LinkMembertoPremiseUI");
                    }
                }
            }
            return View();
        }
        public ActionResult DeleteMembertoPremiseUI(int id)
        {
            if (id >0)
            {
                var row = db.MemberPremiseDetails.Where(x => x.ID == id).FirstOrDefault();
                if (row != null)
                {
                   
                        db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record deleted')</script>";
                            return RedirectToAction("LinkMembertoPremiseUI");
                        }
                        else
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record not deleted')</script>";
                            return RedirectToAction("LinkMembertoPremiseUI");
                        }
                    
                }
                else
                {
                    return RedirectToAction("LinkMembertoPremiseUI");

                }
            }
            else
            {
                return RedirectToAction("LinkMembertoPremiseUI");

            }
            //return View();
        }
        public ActionResult DetailsMembertoPremiseUI(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var row = db.MemberPremiseDetails.Where(x => x.ID == id).FirstOrDefault();            
                return View(row);
            }

        }
        public ActionResult LinkRentortoPremiseUI()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = db.RentorPremiseDetails.ToList();
                return View(data);
            }

        }
        public ActionResult LinkRentortoPremise()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var recordsNotInTable = from a in db.Premises
                                        where !db.RentorPremiseDetails.Select(b => b.PremiseID).Contains(a.PremiseID)
                                        select a;
                //var premises = db.Premises.ToList();
                ViewBag.prem = new SelectList(recordsNotInTable, "PremiseID", "PremiseID");
                var rentor = db.Rentors.ToList();
                //var recordsNotInTableB = from a in db.Members
                //where !db.MemberPremiseDetails.Select(b => b.MemID).Contains(a.MemID)
                //select a;
                ViewBag.rentors = new SelectList(rentor, "RentorID", "RentorID");
                return View();
            }
        }

        [HttpPost]
        public ActionResult LinkRentortoPremise(Models.RentorPremiseDetail r)
        {
            Models.RentorPremiseDetail obj = new Models.RentorPremiseDetail();
            if (ModelState.IsValid)
            {
                obj.RentorID = r.RentorID;
                obj.PremiseID = r.PremiseID;

                if (r.PremiseID == null)
                {
                    TempData["InsertMessage"] = "<script>alert('Please select atleast one premises')</script>";
                    return RedirectToAction("LinkRentortoPremiseUI");
                }
                else if (r.RentorID == null)
                {
                    TempData["InsertMessage"] = "<script>alert('Please select atleast one Rentor')</script>";
                    return RedirectToAction("LinkRentortoPremiseUI");
                }
               else
                {
                    db.RentorPremiseDetails.Add(obj);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["InsertMessage"] = "<script>alert('Record inserted')</script>";
                        return RedirectToAction("LinkRentortoPremiseUI");
                    }
                    else
                    {
                        TempData["InsertMessage"] = "<script>alert('Record not inserted')</script>";
                        return RedirectToAction("LinkRentortoPremiseUI");
                    }
                }
            }
            return View();
        }
        public ActionResult DeleteLinkRentortoPremiseUI(int id)
        {
            if (id > 0)
            {
                var row = db.RentorPremiseDetails.Where(x => x.ID == id).FirstOrDefault();
                if (row != null)
                {

                    db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Record deleted')</script>";
                        return RedirectToAction("LinkRentortoPremiseUI");
                    }
                    else
                    {
                        TempData["DeleteMessage"] = "<script>alert('Record not deleted')</script>";
                        return RedirectToAction("LinkRentortoPremiseUI");
                    }

                }
                else
                {
                    return RedirectToAction("LinkRentortoPremiseUI");

                }
            }
            else
            {
                return RedirectToAction("LinkRentortoPremiseUI");

            }           
        }
        public ActionResult DetailsLinkRentortoPremiseUI(int id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var row = db.RentorPremiseDetails.Where(x => x.ID == id).FirstOrDefault();               
                return View(row);
            }

        }
        public ActionResult GetDuesForMember()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            { return View(); }
        }
        [HttpPost]
        public ActionResult GetDuesForMember(string memId)
        {
            if (ModelState.IsValid == true)
            {
                var data = db.MemDues.Where(m => m.MemID == memId).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.ErrorMessage = "No Data found";
                    return View();
                }
                else
                {                    
                    return View(data);
                }
            }

            return View();
        }
        public ActionResult DuesForMember()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = db.MemDues.ToList();
                return View(data);
            }

        }
        public ActionResult CreateDues()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                //var row = from a in db.MemDues
                                       // where db.Members.Select(b => b.MemID).Contains(a.MemID)
                                        //select a;
                var row = db.Members.ToList();
                ViewBag.mems = new SelectList(row, "MemID", "MemID");
                return View(); }
        }

        [HttpPost]
        public ActionResult CreateDues(MemDue r)
        {
            Models.MemDue obj = new Models.MemDue();
            if (ModelState.IsValid)
            {            
              
                    obj.MemID = r.MemID;
                    obj.PendingAmount = r.PendingAmount;
                    obj.Period = r.Period;
                    obj.LastDate = r.LastDate;
                    obj.LastAmountPaid = r.LastAmountPaid;

                    db.MemDues.Add(obj);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["InsertMessage"] = "<script>alert('Record inserted')</script>";
                        return RedirectToAction("DuesForMember");
                    }
                    else
                    {
                        TempData["InsertMessage"] = "<script>alert('Record not inserted')</script>";
                        return RedirectToAction("DuesForMember");
                    }
                }
            
            return View();
        }
        public ActionResult DeleteDues(int id)
        {
            if (id >0)
            {
                var row = db.MemDues.Where(x => x.ID == id).FirstOrDefault();
                if (row != null)
                {
                  
                        db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record deleted')</script>";
                            return RedirectToAction("DuesForMember");
                        }
                        else
                        {
                            TempData["DeleteMessage"] = "<script>alert('Record not deleted')</script>";
                            return RedirectToAction("DuesForMember");
                        }
                   
                }
                else
                {
                    return RedirectToAction("DuesForMember");

                }
            }
            else
            {
                return RedirectToAction("DuesForMember");

            }
            //return View();
        }
    }
}