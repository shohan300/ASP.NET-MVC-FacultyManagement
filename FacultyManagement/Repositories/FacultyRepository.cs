using FacultyManagement.Models;
using FacultyManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FacultyManagement.Repositories
{
    public class FacultyRepository
    {
        FMDBEntities dbObj = new FMDBEntities();
        public IEnumerable<FacultyViewModel>GetAllFacutys()
        {
            List<FacultyViewModel> facultyList = dbObj.Faculties.Select(f => new FacultyViewModel
            {
                FacultyId = f.FacultyId,
                FacultyName = f.FacultyName,
                JoinDate = f.JoinDate,
                Email = f.Email,
                ImageName = f.ImageName,
                ImageUrl = f.ImageUrl,
                CourseName = f.Course.CourseName,
                CourseId = f.CourseId,
                PageTitle="Faculty List"
            }).ToList();
            return facultyList;
        }
        public Faculty GetFaculty(int id)
        {
            Faculty obj = dbObj.Faculties.SingleOrDefault(f => f.FacultyId == id);
            return obj;
        }
        public void SaveFaculty(Faculty obj)
        {
            dbObj.Faculties.Add(obj);
            dbObj.SaveChanges();
        }
        public void UpdateFaculty(Faculty upObj)
        {
            dbObj.Entry(upObj).State = EntityState.Modified;
            dbObj.SaveChanges();
        }
        public void DeleteFaculty(int id)
        {
            Faculty deleteFaculty = GetFaculty(id);
            dbObj.Faculties.Remove(deleteFaculty);
            dbObj.SaveChanges();
        }
        public List<Course>GetCourses()
        {
            List<Course> list = dbObj.Courses.ToList();
            return list;
        }
    }
}