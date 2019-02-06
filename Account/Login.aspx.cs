using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Kings_BYOD_Helpdesk.Models;
using System.Web.Security;

namespace Kings_BYOD_Helpdesk.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string[] partsOfUserName = UserName.Text.Split("@".ToCharArray());
            string domainName = partsOfUserName[1];
            MembershipProvider domainProvider;
            switch (domainName)
            {
                case "kings-school.co.uk":
                    domainProvider = Membership.Providers["TestDomain1ADMembershipProvider"];
                    break;
                //case "TestDomain2.test.com":
                    //domainProvider = Membership.Providers["TestDomain2ADMembershipProvider"];
                    //break;
                //case "TestDomain3.test.com":
                    //domainProvider = Membership.Providers["TestDomain3ADMembershipProvider"];
                    //break;
                default:
                    throw (new Exception("This domain is not supported"));
            }

            // Validate the user with the membership system.
            if (Membership.ValidateUser(UserName.Text, Password.Text))
            {
                Session["userName"] = partsOfUserName[0];
                Session["password"] = Password.Text;
                // If there is a RequestUrl query string attribute, the user has
                // been redirected to the login page by forms authentication after
                // requesting another page while not authenticated.
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    // RedirectFromLoginPage sets the authorization cookie and then
                    // redirects to the page the user originally requested.
                    // Set second parameter to false so cookie is not persistent
                    // across sessions.
                    FormsAuthentication.RedirectFromLoginPage(
                        UserName.Text, false);
                }
                else
                {
                    // If there is no RequestUrl query string attribute, just set
                    // the authentication cookie. Provide navigation on the login page
                    // to pages that require authentication, or user can use browser
                    // to navigate to protected pages. 
                    // Set second parameter to false so cookie is not persistent
                    // across sessions.
                    FormsAuthentication.SetAuthCookie(UserName.Text, false);
                    Response.Redirect("/RegisterDevice");
                }
            }
            else
            {
                Response.Write("Invalid UserID and Password");
            }
            
            
            //if (IsValid)
            //{
                // Validate the user password
                //var manager = new UserManager();
                //ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                //if (user != null)
                //{
                    //IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                //}
                //else
                //{
                    //FailureText.Text = "Invalid username or password.";
                    //ErrorMessage.Visible = true;
                //}
            //}
        }
    }
}