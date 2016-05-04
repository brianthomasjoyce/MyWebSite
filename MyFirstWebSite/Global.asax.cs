using Microsoft.ApplicationInsights.Extensibility.Implementation;
using MyFirstWebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MyFirstWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Database.SetInitializer<MyFirstWebSite.Models.UserContext>(null); // asks not to create a new database
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
          //  Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Models.UserContext>());
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if(FormsAuthentication.CookiesSupported==true)
            {
                if(Request.Cookies[FormsAuthentication.FormsCookieName]!=null)
                    try
                    {
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        using (var db = new MyFirstWebSite.Models.UserContext())
                        {
                            var user = db.User.SingleOrDefault(u => u.UserName == username);
                            roles = user.UserName;
                        }

                        //setting principal with specific details

                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }//try
                    catch(Exception)
                    {
                        //this is where it catches something wrong
                    }//cathch
            }
        }
    }
}
