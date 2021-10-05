using FacultyManagement.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacultyManagement.Models.ViewModels
{
    public class FacultyViewModel
    {
        //Faculty ID
        [Display(Name ="Faculty Id")]
        [Key]

        public int FacultyId { get; set; }

        //Faculty Name
        [Display(Name = "Faculty Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Faculty Name Required")]
        public string FacultyName { get; set; }

        //Email
        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        //Course Id
        [Display(Name = "Course")]
        [Required(ErrorMessage = "Select Course Please")]
        public int CourseId { get; set; }

        //Course Name
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        //Image Name
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        
        //Image Url
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public string PageTitle { get; set; }

        //Date
        [Required(ErrorMessage = "Join Date Required")]
        [Display(Name = "Join Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [JoinDateValidation]
        public DateTime JoinDate { get; set; }
    }
}