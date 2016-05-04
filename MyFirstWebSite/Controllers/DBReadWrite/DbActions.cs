using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebSite.Controllers.DBReadWrite
{
    public interface DbActions
    {

        void Login();
        void Register();
        void Logout();
        void changePassword();
    }
}
