using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Master.FindControl("HeaderSearch").Visible = false;
        //this.Master.FindControl("iSearch").Visible = false;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (true)
        {
            if (txtUserName.Value != "" && txtPassword.Value != "")
            {
                int RoleID;
                int i = UserInfo.GetUserIdByUsernameAndPassword(txtUserName.Value, txtPassword.Value, out RoleID);
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Invalid username/password');", true);
                    btnLogin.Visible = true;
                }
                else if (RoleID == 1)
                {
                    if (HttpContext.Current.Session["User"] != null)
                    {
                        HttpContext.Current.Session["User"] = txtUserName.Value + "|" + i.ToString() + "|" + RoleID.ToString();
                        btnLogin.Visible = false;
                        Response.Redirect("Manufacturer.aspx");//("AdminHome.aspx");
                    }
                }
            }
        }
    }
}