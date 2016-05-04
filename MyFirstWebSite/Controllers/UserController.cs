using MyFirstWebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Text;
using MyFirstWebSite.Controllers.DBReadWrite;
using SimpleCrypto;
using MyFirstWebSite.Migrations.GUID;
using System.Threading.Tasks;
using Postal;

namespace MyFirstWebSite.Controllers
{
    public class UserController : Controller
    {
        private UserContext db = new UserContext();


        [Authorize]
        public ActionResult PreviousLogins()
        {

            
            using (UserContext context = new UserContext())
            {
                var LoginSession = context.loginSession.SqlQuery("Select * from [dbo].[UserSession] where Id = 3");
                return View(LoginSession.ToList());
            }
      
    


        }
                                                                                                                                                                                                                                                                                           
        public ActionResult Details(int id)
        {      
            User user = db.User.Single(user1 => user1.Id == id);
            return View();
        }
        //LOGIN

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.UserLogin user)
        {
            if (IsValid(user.UserName, user.Password))
            {
                var userWithID = db.User.FirstOrDefault(u => u.UserName == user.UserName);
                
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                addUserSessionAndTime(userWithID.Id);
                return RedirectToAction("CV", "Index");
            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("CV", "index");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(user);
        }
        //REGISTER

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.UserRegisterModel userr)
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                RedirectToAction("CV", "Index");
            }
            else
            {
                using (UserContext context = new UserContext())
                {                   
                    var newUser = new User();
                    var crypt = md5.GetMD5(userr.Password);
                    newUser.Id = 1;
                    newUser.UserName = userr.UserName;
                    newUser.Password = crypt.ToString();
                    newUser.FirstName = userr.FirstName;
                    newUser.Surname = userr.Surname;
                    newUser.Email = userr.Email;
                    newUser.EmailConfirmed = false;
                    newUser.EmailConfirmationToken = CreateToken();
                    //generate token and then generate email helper
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                context.User.Add(newUser); //add the user to memory
                                context.SaveChanges();// then save all changes
                                bool sent = SendEmailConfirmation(newUser.UserName, newUser.Email, newUser.EmailConfirmationToken);                               
                            }
                            catch (DbUpdateConcurrencyException ex) //check concorrency issues
                            {
                                ex.Entries.Single().Reload();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "This is not Correct");
                        }

                    }//try

                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }//catch
                }//using
            }

            return View();

        }


        //LOGOUT

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage", "Index");
        }

      //  [HttpPost]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult EmailVerified(string token)
        {
          
            var confirmToken = db.User.FirstOrDefault(u => u.EmailConfirmationToken == token);

            if(confirmToken == null)
            {
                ViewBag.Message = "Something went Wrong with that token.  You will need to try again";
                
            }
            else
            {
                confirmToken.EmailConfirmed = true;
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }

            return View();
        }


        private bool IsValid(string username, string password)
        {

            bool IsValid = false;
            using (var db = new MyFirstWebSite.Models.UserContext())
            {
                var user = db.User.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        string pass = md5.GetMD5(password);
                        if (pass.Equals(user.Password))
                        {
                            IsValid = true;
                        }
                    }
                    
                }
            }

            return IsValid;
        }


        private static bool HasUserConfirmedEmail()
        {
            return false;
        }



        //create token for Email Confirmation
        private static string CreateToken()
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[16];
                provider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }


        private bool SendEmailConfirmation(String username, string emailAddress, string token)
        {
            bool isEmailSent = false;

            var urllink = Url.Action("EmailVerified", "User", new { token = token });
            dynamic email = new Email("EmailReg");
            email.To = emailAddress;
            email.Subject = "Reg Email";
            email.Message = " ";
            email.Date = DateTime.UtcNow;
            email.token = token;
            email.link = "Localhost:53083" + urllink;

            try
            {
                email.Send();
                isEmailSent = true;
            }
            catch
            {
                isEmailSent = false;
            }
            return isEmailSent;
        }

        private void addUserSessionAndTime(int id)
        {
            var loginSession = new UserLoginSessionRecords();
            DateTime now = DateTime.Now;
            loginSession.Id = id;
            loginSession.UserLoginTimeAndDate = now;
            db.loginSession.Add(loginSession);
       //     try
       //     {
                db.SaveChanges();
        //    }
        //    catch
         //   {

          //  }
        }

    
        //check if usernname is there ajax request
        public JsonResult IsUserNameAvailable(string UserName)
        {
            return Json(!db.User.Any(us => us.UserName == UserName), JsonRequestBehavior.AllowGet);

        }
        
      //check if email address is there
        public JsonResult IsEmailAvailable(string Email)
        {
            return Json(!db.User.Any(us => us.Email == Email), JsonRequestBehavior.AllowGet);
        }

    }
}