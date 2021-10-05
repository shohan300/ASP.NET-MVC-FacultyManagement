using FacultyManagement.Models;
using FacultyManagement.Models.ViewModels;
using FacultyManagement.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacultyManagement.Controllers
{
    public class FacultysController : Controller
    {
        // GET: Facultys
        FacultyRepository repo = new FacultyRepository();
        [HttpGet]
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<FacultyViewModel> list = repo.GetAllFacutys();
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(f => f.FacultyName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(f => f.FacultyName).ToList();
                    break;
                default:
                    list = list.OrderBy(f => f.FacultyName).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ViewResult Create()
        {
            List<Course> list = repo.GetCourses();
            ViewBag.Courses = list;
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            List<Course> list = repo.GetCourses();
            ViewBag.Courses = list;
            Faculty faculty = repo.GetFaculty(id);
            FacultyViewModel obj = new FacultyViewModel();
            obj.FacultyId = faculty.FacultyId;
            obj.FacultyName = faculty.FacultyName;
            obj.Email = faculty.Email;
            obj.ImageName = faculty.ImageName;
            obj.ImageUrl = faculty.ImageUrl;
            obj.CourseId = faculty.CourseId;
            obj.JoinDate = faculty.JoinDate;
            return View(obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(FacultyViewModel obj)
        {
            var result = false;
            Faculty obj1;
            if (obj.FacultyId==0)
            {
                if (ModelState.IsValid)
                {
                    obj1 = new Faculty();
                    obj1.FacultyName = obj.FacultyName;
                    obj1.Email = obj.Email;
                    obj1.JoinDate = obj.JoinDate;
                    obj1.CourseId = obj.CourseId;
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj1.ImageName = fileName + extension;
                    obj1.ImageUrl = "~/Images/" + obj1.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    repo.SaveFaculty(obj1);
                    result = true;
                }

            }
            else
            {
                obj1 = repo.GetFaculty(obj.FacultyId);
                obj1.FacultyName = obj.FacultyName;
                obj1.Email = obj.Email;
                obj1.JoinDate = obj.JoinDate;
                obj1.CourseId = obj.CourseId;
                if (obj.ImageFile!=null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj1.ImageName = fileName + extension;
                    obj1.ImageUrl = "~/Images/" + obj1.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                }
                else
                {
                    obj.ImageName = obj1.ImageName;
                    obj.ImageName = obj1.ImageUrl;
                }
                repo.UpdateFaculty(obj1);
                result = true;
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Course> list = repo.GetCourses();
                ViewBag.Courses = list;
                return View();       
            }
        }
        [HttpGet]
        public ViewResult Delete(int id)
        {
            Faculty faculty = repo.GetFaculty(id);
            FacultyViewModel obj = new FacultyViewModel();
            obj.FacultyId = faculty.FacultyId;
            obj.FacultyName = faculty.FacultyName;
            obj.Email = faculty.Email;
            obj.ImageName = faculty.ImageName;
            obj.ImageUrl = faculty.ImageUrl;
            obj.CourseId = faculty.CourseId;
            obj.CourseName = faculty.Course.CourseName;
            obj.JoinDate = faculty.JoinDate;
            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(FacultyViewModel viewobj)
        {
            Faculty obj = repo.GetFaculty(viewobj.FacultyId);
            repo.DeleteFaculty(obj.FacultyId);
            return RedirectToAction("Index");
        }
        public PartialViewResult Details(int id)
        {
            List<Course> list = repo.GetCourses();
            ViewBag.Courses = list;
            Faculty faculty = repo.GetFaculty(id);
            FacultyViewModel obj = new FacultyViewModel();
            obj.FacultyId = faculty.FacultyId;
            obj.FacultyName = faculty.FacultyName;
            obj.Email = faculty.Email;
            obj.ImageName = faculty.ImageName;
            obj.ImageUrl = faculty.ImageUrl;
            obj.CourseId = faculty.CourseId;
            obj.JoinDate = faculty.JoinDate;
            ViewBag.Details = "Show";
            return PartialView("_DetailsRecord", obj);
        }
    }
}