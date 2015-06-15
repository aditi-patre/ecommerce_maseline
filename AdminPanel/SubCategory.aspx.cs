using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_SubCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int RoleID = 0;
        if (HttpContext.Current.Session["User"] != null)
        {
           RoleID = Convert.ToInt32(Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[2]);
        }

        if (RoleID == 0)
            Response.Redirect("~/AdminPanel/AdminLogin.aspx", true);
        else
        {
        }
    }
}