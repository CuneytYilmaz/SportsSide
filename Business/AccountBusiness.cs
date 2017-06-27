using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public class AccountBusiness
    {
        public string Login(string userName, string pass)
        {
            var result = new Database.AccountDatabase().Login(userName, pass);

            return result;
        }
        public string Register(string userName, string pass, string firstName, int district, string surname, string gsm, string email)
        {
            var result = new Database.AccountDatabase().Register(userName, pass, firstName, district, surname, gsm, email);

            return result;
        }

        public Models.User Member()
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                return HttpContext.Current.Session["User"] as Models.User;
            }
            return new Models.User();
        }
        public string currentMember()
        {
            if (HttpContext.Current.Session["Role"] != null)
            {
                return HttpContext.Current.Session["Role"].ToString();
            }
            return "";
        }
        public void Logout()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
