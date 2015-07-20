using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Careers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Careers | Masline";
        if(!IsPostBack)
        {
            PopulateLeftMenu();
            LoadFeaturedNews();
        }
    }

    private void PopulateLeftMenu()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        sb.Append("<div>");
        sb.Append("<div style=\"height:250px;overflow-x:hidden;overflow-y:hidden;\" id=\"_divCatalog\" runat=\"server\">");//overflow:scroll;overflow-x:hidden;overflow-y:scroll;

        sb.Append("<div class=\"panel-group category-products\" id=\"accordian\">");
        sb1.Append("<div class=\"panel-group category-products\" id=\"accordian1\">");
        Category objCat = new Category();
        DataTable dt = objCat.CategoriesList();
        if (dt != null && dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["subCategoryCount"]) > 0)// if subcategories are present
                {
                    sb.Append("<div class=\"panel panel-default\"> <div class=\"panel-heading\"><h4 class=\"panel-title\">");
                    sb1.Append("<div class=\"panel panel-default\"> <div class=\"panel-heading\"><h4 class=\"panel-title\">");

                    sb.Append(" <a data-toggle=\"collapse\" data-parent=\"#accordian\" href=\"#" + Convert.ToString(dt.Rows[i]["shortcode"]) + "\"><span class=\"badge pull-right\"><i class=\"fa fa-plus\"></i></span>");
                    sb1.Append(" <a data-toggle=\"collapse\" data-parent=\"#accordian1\" href=\"#" + Convert.ToString(dt.Rows[i]["shortcode"]) + "1\"><span class=\"badge pull-right\"><i class=\"fa fa-plus\"></i></span>");

                    sb.Append("<a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["CategoryID"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
                    sb1.Append("<a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["CategoryID"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");

                    sb.Append("                </a>            </h4>        </div>");
                    sb1.Append("                </a>            </h4>        </div>");
                    Category objCat2 = new Category(Convert.ToInt32(dt.Rows[i]["categoryID"]));
                    if (objCat2.SubCategories.Count > 0)
                    {
                        sb.Append("<div id=\"" + Convert.ToString(dt.Rows[i]["shortcode"]) + "\" class=\"panel-collapse collapse\"><div class=\"panel-body\"><ul>");
                        sb1.Append("<div id=\"" + Convert.ToString(dt.Rows[i]["shortcode"]) + "1\" class=\"panel-collapse collapse\"><div class=\"panel-body\"><ul>");
                        for (int j = 0; j < objCat2.SubCategories.Count; j++)
                        {
                            sb.Append("<li><a href='" + GenerateURL(objCat2.SubCategories[j].CategoryID.ToString(), objCat2.SubCategories[j].SubCategoryID.ToString()) + "'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
                            sb1.Append("<li><a href='" + GenerateURL(objCat2.SubCategories[j].CategoryID.ToString(), objCat2.SubCategories[j].SubCategoryID.ToString()) + "'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
                        }
                        sb.Append("</ul> </div></div>");
                        sb1.Append("</ul> </div></div>");
                    }
                    sb.Append("</div>"); //added on 29 may
                    sb1.Append("</div>");
                }
                else //No sub categories
                {
                    sb.Append("  <div class=\"panel panel-default\">       <div class=\"panel-heading\">   <h4 class=\"panel-title\">");
                    sb1.Append("  <div class=\"panel panel-default\">       <div class=\"panel-heading\">   <h4 class=\"panel-title\">");

                    sb.Append("<a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["CategoryID"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
                    sb1.Append("<a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["CategoryID"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");

                    sb.Append("</h4> </div> </div>");
                    sb1.Append("</h4> </div> </div>");
                    // sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "") + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
                }
            }
            sb.Append("</div>");
            sb1.Append("</div>");

            sb.Append("</div><br/>"); //over flow div ends
            sb.Append("<a href='#' id=\"ShowCats\" OnClick=\"btnShowCats()\" style=\"padding-left:160px;\"><span>Show More >></span></a>");
            sb.Append("</div>");
            //CatList = sb1.ToString(); 
            //hdnCategoryListing.Value = sb1.ToString();
            dvCataLogList.InnerHtml = sb1.ToString();
            //hdnCategoryListing.Text = sb1.ToString();
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
    private void LoadFeaturedNews()
    {
        FeaturedNews objF = new FeaturedNews();
        DataTable dt = objF.GetList(true);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ltFeaturedNews.Text += GenerateNewsTemplate(Convert.ToString(dt.Rows[i]["ImageName"]), Convert.ToString(dt.Rows[i]["Descrip"]));
            }
        }
    }

    private string GenerateNewsTemplate(string ImgName, string Content)
    {
        string ImagePath = "";
        if (ImgName != "")
            ImagePath = "images/news/" + ImgName;
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class=\"media commnets\" style=\"border-bottom: 1px solid lightgrey;\">");
        sb.Append("<a class=\"pull-left\" href=\"#\">");
        sb.Append("<img src='" + ImagePath + "' alt=\"\" style=\"width:52px; height:52px;\" />");
        sb.Append("</a><div class=\"media-body\">");
        sb.Append("<p>" + Content + "</p></div></div>");

        return sb.ToString();
    }
}