using Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace Database
{
    public class AccountDatabase
    {
        DatabaseEntities db = new DatabaseEntities();
        public string Login(string userName, string pass)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
            {
                var rowFind = db.USERS.Where(x => x.USER_NAME == userName);
                if (rowFind.Count() > 0)
                {
                    var row = db.USERS.FirstOrDefault(x => x.USER_NAME == userName && x.USER_PASSWORD == pass);
                    if (row != null)
                    {
                        HttpContext.Current.Session.Add("User", new
                        {
                            email = row.USER_NAME,
                            isAuthentication = true,
                            role = row.USER_TYPE
                        });
                        HttpContext.Current.Session.Add("Role", row.USER_TYPE);
                        HttpContext.Current.Session.Add("userName", row.USER_FIRST_NAME + " " + row.USER_SURNAME);
                        return "Basarili";
                        
                    }
                    return "Şifre Hatalı";
                }
                return "Kullanıcı Adı Hatalı";
            }
            return null;
        }
        

        public string Register(string userName, string pass, string firstName, int district, string surname, string gsm, string email)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
            {
                var rowFind = db.USERS.Where(x => x.USER_NAME == userName);
                if (rowFind.Count() > 0)
                {
                    return "Gecersiz email";
                }
                db.USERS.Add(new USER
                {
                    USER_NAME = userName,
                    USER_PASSWORD = pass,
                    USER_FIRST_NAME = firstName,
                    USER_DISTRICT = district,
                    USER_SURNAME = surname,
                    USER_GSM = gsm,
                    USER_MAIL = email,
                    USER_TYPE = "U",
                    USER_POS = Int32.Parse("3"),
                    USER_NEG = Int32.Parse("0")
            });
                db.SaveChanges();
                return "Basarili";
            }
            return null;
        }

    }
}
