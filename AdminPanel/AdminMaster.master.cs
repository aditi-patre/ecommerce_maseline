using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlAnchor btnLogin = (System.Web.UI.HtmlControls.HtmlAnchor)FindControl("btnLogin");
        Button btnLogOut = (Button)FindControl("btnLogOut");
        if (HttpContext.Current.Session["User"] != null)
        {
            btnLogin.Visible = false;
            lblUser.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblUser.Text = "Welcome " + Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[0];
            btnLogOut.Visible = true;
        }
        else
        {
            btnLogin.Visible = true;
            lblUser.Style.Add(HtmlTextWriterStyle.Display, "none");
            btnLogOut.Visible = false;
        }
        
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
            HttpContext.Current.Session["User"] = null;
            Session.Abandon();
            Response.Redirect("AdminHome.aspx", true);
       
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    if(hdnSearchCriteria.Value.IndexOf("Manufacturer") != -1)
    //    {
    //        //Manufacturer objM = new Manufacturer();
    //        //objM.Name = hdnSearch.Value;
    //        //int TR;
    //        //DataTable dt = objM.GetList(1,10,"","", out TR);
    //        //if (dt != null && dt.Rows.Count > 0)
    //        //{
    //            Response.Redirect("Manufacturer.aspx?Name=" + hdnSearch.Value);
    //        //}
    //    }
    //    else if(hdnSearchCriteria.Value.IndexOf("SubCategory") != -1)
    //    {
    //        //SubCategory objS = new SubCategory();
    //        //objS.Name = hdnSearch.Value;
    //        //DataTable dt = objS.GetList();
    //        //if (dt != null && dt.Rows.Count > 0)
    //        //{
    //        Response.Redirect("SubCategory.aspx?Name=" + hdnSearch.Value);
    //        //}
    //    }
    //    else if (hdnSearchCriteria.Value.IndexOf("Category") != -1)
    //    {
    //        //Category objC = new Category();
    //        //objC.Name = hdnSearch.Value;
    //        //DataTable dt = objC.CategoriesList();
    //        //if (dt != null && dt.Rows.Count > 0)
    //        //{
    //        Response.Redirect("Category.aspx?Name=" + hdnSearch.Value);
    //        //}
    //    }
    //    else if (hdnSearchCriteria.Value.IndexOf("Product") != -1)
    //    {
    //        Product objProduct = new Product();
    //        objProduct.ProductCode = hdnSearch.Value;
    //        DataTable dt = objProduct.GetList(hdnSearch.Value);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            Response.Redirect("Product.aspx?PCode=" + dt.Rows[0]["ProductID"].ToString());
    //        }
    //    }
      
    //}
}
