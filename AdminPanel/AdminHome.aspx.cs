using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class AdminPanel_AdminHome : System.Web.UI.Page
{
    public string SelectedImage;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Master.FindControl("HeaderSearch").Visible = false;
        //this.Master.FindControl("iSearch").Visible = false;
        
        int RoleID = 0;
        if (HttpContext.Current.Session["User"] != null)
        {
            RoleID = Convert.ToInt32(Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[2]);
        }

        if (RoleID == 0)
            Response.Redirect("~/AdminPanel/AdminLogin.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(hdnSliderImageID.Value!="")
        {
            ContentPlaceHolder c = (ContentPlaceHolder)((System.Web.UI.HtmlControls.HtmlForm)this.Controls[0].Controls[3]).FindControl("ContentPlaceHolder1");
            System.Web.UI.HtmlControls.HtmlImage img = (System.Web.UI.HtmlControls.HtmlImage)c.FindControl(hdnSliderImageID.Value.Split('_')[1]);
            if (File.Exists(Server.MapPath(img.Src)))
            {
                fupBanner.PostedFile.SaveAs(img.Src);
            }
        }
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
      
    }

}