using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Kings_BYOD_Helpdesk.fonts
{
    public partial class DeviceRegistered : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Page.LoadComplete += (Redirect);
        }
        protected void Redirect(object sender, EventArgs e)
        {
            Response.AppendHeader("Refresh", "5;url=http://kingsbyod/");
        }
    }
}