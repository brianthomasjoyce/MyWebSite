using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstWebSite.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using MyFirstWebSite.Models;

namespace MyFirstWebSite.Models
{

    //Login
    public class UserLogin : User
    {

        [Required]
        [Display(Name = "User Name")]
        public override string UserName {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "Password")]
        public override string Password { get; set; }

       public virtual User user { get; set; }
    }

    //register
    public class UserRegisterModel : User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Remote("IsUserNameAvailable", "User", ErrorMessage ="Username is already Taken")]
        [Display(Name = "User Name")]
        public override string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "Password")]
        public override string Password { get; set; }
        [Required]
        public override string FirstName { get; set; }
        [Required]
        public override string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [Remote("IsEmailAvailable", "User", ErrorMessage = "Email Address is already Taken")]
        public override string Email { get; set; }
        public virtual User user { get; set; }
    
    }
    //Change PassWord model
    public class UserChangePasswordModel : User
    {
        [Required]
        [Display(Name = "Current Password")]
        public override string Password { get; set; }


        [NotMapped]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [NotMapped]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage ="New and Confirmed Password do not match")]
        
        public string ConfirmPassword { get; set; }

        public virtual User User { get; set; }
    }
    



    public class AccountCreateResponseModel : User
    {

    }

    [MetadataType(typeof(User))]

    public class UserMetadata
    {

    }
}

/////UserSession Tables

public class UserSessionModel : UserLoginSessionRecords
{

    public virtual UserLoginSessionRecords UserLoginSessionRecords { get; set; }
}