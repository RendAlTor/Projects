using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ECP.Models;

namespace ECP.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                // Записываем ключи и модуль
                using (site_dbDataContext contex = new site_dbDataContext()) //Создан экземпляр класса БД               
                {
                    RSA.RSA_Sign A_RSA_Sign = new RSA.RSA_Sign(); //конструктор
                    //String userId = (from x in new site_dbDataContext().AspNetUsers where x.Email == Context.User.Identity.Name select x.Id).First();
                    //Создаем объекты таблиц
                    PublicKeys PublicKey = new PublicKeys();
                    //PublicKey
                    PublicKey.PublicKey = A_RSA_Sign.E;
                    PublicKey.Mod = A_RSA_Sign.N; //Mod
                    PublicKey.UserLogin = Email.Text;
                    contex.PublicKeys.InsertOnSubmit(PublicKey);
                    contex.SubmitChanges();
                    Sisurity PrivatKey = new Sisurity();
                    //PrivatKey
                    PrivatKey.PrivatKey = A_RSA_Sign.D;
                    PrivatKey.UserLogin = Email.Text;
                    contex.Sisurity.InsertOnSubmit(PrivatKey);
                    contex.SubmitChanges();
                }
                ///
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }

        }

    }
}