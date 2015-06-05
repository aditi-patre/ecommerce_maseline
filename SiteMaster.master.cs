using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //ViewState["lblCartItemCount"] = lblCartItemCount.Text;

        if (!IsPostBack)
            PopulateLeftMenu();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PopUpParentUrl"] = Request.Url.PathAndQuery;
        //if (!IsPostBack)
        //{
        //Button btnSignUp = (Button)FindControl("btnSignUp");
        System.Web.UI.HtmlControls.HtmlAnchor btnLogin = (System.Web.UI.HtmlControls.HtmlAnchor)FindControl("btnLogin");
        //Button btnLogin = (Button)FindControl("btnLogin");
        ImageButton btnLogOut = (ImageButton)FindControl("btnLogOut");
        if (HttpContext.Current.Session["User"] != null)
        {

            //btnSignUp.Visible = false;
            btnLogin.Visible = false;
            lblUser.Style.Add(HtmlTextWriterStyle.Display, "block");
            lblUser.Text = "Welcome " + Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[0];
            btnLogOut.Visible = true;
        }
        else
        {
            //btnSignUp.Visible = true;
            btnLogin.Visible = true;
            lblUser.Style.Add(HtmlTextWriterStyle.Display, "none");
            btnLogOut.Visible = false;
        }
        //lblCartItemCount.Text = ShoppingCart.Instance.Items.Count.ToString();
        //}
    }

    //private void PopulateLeftMenu()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<ul id='menu-v'>");//sb.Append("<div id='cssmenu'>"); menu-v
    //    //sb.Append("<ul>");
    //    sb.Append("<li><a href='#'><span>Home</span></a></li>"); //  sb.Append("<li class='active'><a href='#'><span>Home</span></a></li>");
    //    sb.Append("<li><a href='Catalogue.aspx'><span>Catalog</span></a>"); //sb.Append("<li class='has-sub'><a href='#'><span>Catalog</span></a>");

    //    Category objCat = new Category();
    //    DataTable dt = objCat.CategoriesList();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        sb.Append("<ul class='sub'>");
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            if (Convert.ToInt32(dt.Rows[i]["subCategoryCount"]) > 0)// if subcategories are present
    //            {
    //                sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
    //                Category objCat2 = new Category(Convert.ToInt32(dt.Rows[i]["categoryID"]));
    //                if (objCat2.SubCategories.Count > 0)
    //                {
    //                    sb.Append("<ul class='sub'>");
    //                    for (int j = 0; j < objCat2.SubCategories.Count; j++)
    //                    {
    //                        //if (j == objCat2.SubCategories.Count - 1)
    //                        //    sb.Append("<li class ='last'><a href='#'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
    //                        //else
    //                        sb.Append("<li><a href='" + GenerateURL(objCat2.SubCategories[j].C_ShortCode, objCat2.SubCategories[j].ShortCode) + "'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
    //                    }
    //                    sb.Append("</ul></li>");
    //                }
    //            }
    //            else //No sub categories
    //            {
    //                //if (i == dt.Rows.Count - 1)
    //                //    sb.Append("<li class='last'><a href='#'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
    //                //else
    //                sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
    //            }
    //        }


    //        sb.Append("</ul>");
    //    }
    //    sb.Append("</li>");

    //    sb.Append("<li><a href='#'><span>Services</span></a></li>");//  sb.Append("<li class='last'><a href='#'><span>Services</span></a></li>");
    //    sb.Append("<li><a href='#'><span>About</span></a></li>");

    //    sb.Append("<li><a href='#'><span>News</span></a></li>");
    //    sb.Append("<li><a href='#'><span>LineCard</span></a></li>");
    //    sb.Append("<li><a href='#'><span>Contact</span></a></li>");
    //    sb.Append("<li><a href='#'><span>Videos</span></a></li>");
    //    sb.Append("</ul>");
    //    //sb.Append("</div>";)
    //    //Literal ltList1 = (Literal)this.Master.FindControl("ltList");
    //    ltList.Text = sb.ToString();
    //}

    //private string GenerateURL(string Category, string SubCategory)
    //{
    //    if (!String.IsNullOrEmpty(SubCategory))
    //        return "Catalogue.aspx?Cat=" + Category + "&SubCat=" + SubCategory;
    //    else
    //        return "Catalogue.aspx?Cat=" + Category;
    //}

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        if (ShoppingCart.Instance.Items.Count > 0)
        {
            do
            {
                ShoppingCart.Instance.Items.RemoveAt(ShoppingCart.Instance.Items.Count - 1);
            }
            while (ShoppingCart.Instance.Items.Count > 0);
        }
        Session.Abandon();
        Response.Redirect("Home.aspx");
        //lblCartItemCount.Text = "0";
    }

    private void PopulateLeftMenu()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"panel-group category-products\" id=\"accordian\">");

        Category objCat = new Category();
        DataTable dt = objCat.CategoriesList();
        if (dt != null && dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["subCategoryCount"]) > 0)// if subcategories are present
                {
                    sb.Append("<div class=\"panel panel-default\"> <div class=\"panel-heading\"><h4 class=\"panel-title\">");
                    sb.Append(" <a data-toggle=\"collapse\" data-parent=\"#accordian\" href=\"#" + Convert.ToString(dt.Rows[i]["shortcode"]) + "\"><span class=\"badge pull-right\"><i class=\"fa fa-plus\"></i></span>");
                    sb.Append("<a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["CategoryID"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
                    sb.Append("                </a>            </h4>        </div>");
                    Category objCat2 = new Category(Convert.ToInt32(dt.Rows[i]["categoryID"]));
                    if (objCat2.SubCategories.Count > 0)
                    {
                        sb.Append("<div id=\"" + Convert.ToString(dt.Rows[i]["shortcode"]) + "\" class=\"panel-collapse collapse\"><div class=\"panel-body\"><ul>");

                        for (int j = 0; j < objCat2.SubCategories.Count; j++)
                        {
                            sb.Append("<li><a href='" + GenerateURL(objCat2.SubCategories[j].CategoryID.ToString(), objCat2.SubCategories[j].SubCategoryID.ToString()) + "'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
                        }
                        sb.Append("</ul> </div></div>");
                    }
                    sb.Append("</div>"); //added on 29 may
                }
                else //No sub categories
                {
                    sb.Append("  <div class=\"panel panel-default\">       <div class=\"panel-heading\">   <h4 class=\"panel-title\">");
                    sb.Append("<a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["CategoryID"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
                    sb.Append("</h4> </div> </div>");

                    // sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
                }
            }
            sb.Append("</div>");
        }
        ltList.Text = sb.ToString();
    }

    private string GenerateURL(string Category, string SubCategory)
    {
        if (!String.IsNullOrEmpty(SubCategory))
            return "ProductListing.aspx?Cat=" + Category + "&SubCat=" + SubCategory;
        else
            return "ProductListing.aspx?Cat=" + Category;
    }

}
