using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void Pre_Init(object sender, EventArgs e)
    //{
    //    this.MasterPageFile = "SiteMaster.master";
    //}
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtUserName.Text != "" && txtPassword.Text != "")
            {
                Button btnSignUp = (Button)this.Master.FindControl("btnSignUp");
                Button btnLogin = (Button)this.Master.FindControl("btnLogin");
                Label lblUser = (Label)this.Master.FindControl("lblUser");
                Button btnLogOut = (Button)this.Master.FindControl("btnLogOut");

                int i = UserInfo.GetUserIdByUsernameAndPassword(txtUserName.Text, txtPassword.Text);
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Invalid username/password');", true);
                    btnSignUp.Visible = true;
                    lblUser.Style.Add(HtmlTextWriterStyle.Display, "none");
                    btnLogOut.Visible = false;
                }
                else
                {
                    if (HttpContext.Current.Session["User"] != null)
                    {
                        HttpContext.Current.Session["User"] = txtUserName.Text;
                        btnSignUp.Visible = false;
                        lblUser.Style.Add(HtmlTextWriterStyle.Display, "block");
                        lblUser.Text = "Welcome " + txtUserName.Text;
                        btnLogOut.Visible = true;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please enter user name & password');", true);
            }
        }
    }
}