using MyFirstWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebSite.Controllers.DBReadWrite
{
    public class DbActionsImpl : DbActions
    {

        private UserContext db = new UserContext();
        private string username, password, firstname, lastname, email;

        public DbActionsImpl()//Login
        {

        }

        public void changePassword()
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void Register()
        {
            throw new NotImplementedException();
        }
    }
}