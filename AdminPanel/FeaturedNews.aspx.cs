using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_FeaturedNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindFeaturedNews();
    }

    protected void gvFeaturedNews_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvFeaturedNews.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvFeaturedNews.RowStyle.CssClass = "ListingRowStyle";

            ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
            btnEdit.Attributes.Add("OnClick", "return FeaturedNewsEdit('" + gvFeaturedNews.DataKeys[e.Row.RowIndex].Value + "','" + e.Row.RowIndex + "')");

            Image img = (Image)e.Row.FindControl("imgLogo");
            Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");
            if (lblImagePath.Text != "")
            {
                img.ImageUrl = "..//" + System.Configuration.ConfigurationManager.AppSettings["NewsImagePath"].ToString() + "//" + lblImagePath.Text;
            }
        }
    }
    protected void gvFeaturedNews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteF")
        {
            FeaturedNews obj = new FeaturedNews(Convert.ToInt32(e.CommandArgument));
            obj.IsActive = false;
            obj.Save();

            BindFeaturedNews();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('The Featured News has been deleted successfully.');", true);
        }
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (hdntxtDescrip.Value != "")
        {
            FeaturedNews obj = new FeaturedNews(Convert.ToInt32(hdnFeaturedNewsId.Value));
            obj.Descrip = hdntxtDescrip.Value;//txtDescrip.Text;
            if (fupldImageName.HasFile)
                obj.ImageName = fupldImageName.FileName;
            
            obj.IsActive = true;
            if (obj.Save() && fupldImageName.HasFile)
            {
                string ImagePath = Server.MapPath("..//" + System.Configuration.ConfigurationManager.AppSettings["NewsImagePath"].ToString()) + "//" + fupldImageName.FileName;
                if (File.Exists(ImagePath))
                {
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    File.Delete(ImagePath);
                }
                fupldImageName.SaveAs(ImagePath);
            }
            BindFeaturedNews();
        }
        else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please enter the Featured News');", true);
    }

    private void BindFeaturedNews()
    {
        FeaturedNews obj = new FeaturedNews();
        DataTable dt = obj.GetList(true);
        gvFeaturedNews.DataSource = dt;
        gvFeaturedNews.DataBind();
    }
}