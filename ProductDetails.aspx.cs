using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
public partial class ProductDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int PCode = 0;
        if (!IsPostBack)
            PopulateLeftMenu();
        if (Request.QueryString["PCode"] != null)
        {
            Int32.TryParse(Request.QueryString["PCode"], out PCode);
            if (PCode > 0)
            {
                LoadProductDetails(PCode);
                hdnProductID.Value = PCode.ToString();
                LoadFeaturedNews();
            }
        }
        else
        {
            productinfo.Style.Add(HtmlTextWriterStyle.Display, "none");
            recommendedItems.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }
    /*private void PopulateLeftMenu()
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
    }*/

    private void PopulateLeftMenu()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        sb.Append("<div>");
        sb.Append("<div style=\"height:715px;overflow-x:hidden;overflow-y:hidden;\">");//overflow:scroll;overflow-x:hidden;overflow-y:scroll;

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
            //hdnCategoryListing.Value = sb1.ToString();
            dvCataLogList.InnerHtml = sb1.ToString();
        }
        ltList.Text = sb.ToString();
    }


    private void LoadProductDetails(int PCode)
    {
        try
        {
            Product objP = new Product(PCode);
            if (objP.Inventory <= 0)
            {
                hdnQty.Value = "Unavailable";
            }
            else
                hdnQty.Value = "";
            if (objP.Price <= 0)
            {
                hdnPrice.Value = "Unavailable";
            }
            else
                hdnPrice.Value = "";
            string FolderPath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + PCode.ToString();
            string ProdImagePath = FolderPath + "//" + objP.ImageName;
            if (File.Exists(Server.MapPath(ProdImagePath)))
                imgMain.Src = ProdImagePath;
            else
                imgMain.Src = System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + "Not_available.jpg";
            //ltSimilarImages
            if (Directory.Exists(Server.MapPath(FolderPath)))
            {
                StringBuilder sb = new StringBuilder();
                string[] files = Directory.GetFiles(Server.MapPath(FolderPath));
                for (int i = 0; i < files.Length; i++)
                {
                    if (i % 3 != 0 || i == 0)
                    {
                        if (i == 0)
                        {
                            sb.Append("<div class=\"item active\" style=\"left:2.8%;\">");
                        }
                        string path = "ProductImages/" + PCode.ToString() + "/" + files[i].Substring(files[i].LastIndexOf("\\") + 1);
                        sb.Append("<a href=\"\"><img src=\"ProductImages/" + PCode.ToString() + "/" + files[i].Substring(files[i].LastIndexOf("\\") + 1) + "\" onclick=\"return Enlarge('" + path + "')\" alt=\"\"></a>");
                    }
                    else
                    {
                        sb.Append("</div>");
                        if (i < files.Length - 1)
                        {
                            sb.Append("<div class=\"item\">");
                            string path = "ProductImages/" + PCode.ToString() + "/" + files[i].Substring(files[i].LastIndexOf("\\") + 1);
                            sb.Append("<a href=\"\"><img src=\"ProductImages/" + PCode.ToString() + "/" + files[i].Substring(files[i].LastIndexOf("\\") + 1) + "\" onclick=\"return Enlarge('" + path + "')\"  alt=\"\"></a>");
                        }
                    }
                }
                if (!sb.ToString().EndsWith("</div>"))
                    sb.Append("</div>");
                if (sb.ToString() != "")
                    ltSimilarImages.Text = sb.ToString();
            }//if directory....


            //****Product description
            lblProductCode.Text = objP.ProductCode;
            lblDescription.Text = objP.Descrip;
            Manufacturer objM = new Manufacturer(objP.ManufacturerID);
            lblManufacturer.Text = objM.Name;
            StringBuilder sbPricing = new StringBuilder();
            if (objP.Pricing != null)
            {
                if (objP.Pricing.Count > 1)
                {
                    sbPricing.Append("<table border=\"1\" cellpadding=\"10\">");
                    foreach (KeyValuePair<string, decimal> p in objP.Pricing)
                    {
                        if (Convert.ToString(p.Key).Split('-').Length > 1)
                            sbPricing.Append("<tr><td>" + Convert.ToString(p.Key) + "</td><td>$" + Convert.ToString(p.Value) + "</td></tr>");
                        else
                            sbPricing.Append("<tr><td>" + Convert.ToString(p.Key) + "- UP" + "</td><td>$" + Convert.ToString(p.Value) + "</td></tr>");
                    }
                    sbPricing.Append("</table");
                }
            }

            ltPricing.Text = sbPricing.ToString() != "" ? sbPricing.ToString() : "$" + objP.Price.ToString();
            lblInventory.Text = objP.Inventory > 0 ? objP.Inventory.ToString() : "Unavailable";
            LoadRecommendations(objP.CategoryID, objP.SubCategoryID, PCode);
        }
        catch (Exception ex)
        {

        }

    }
    private string GenerateURL(string Category, string SubCategory)
    {
        if (!String.IsNullOrEmpty(SubCategory))
            return "ProductListing.aspx?Cat=" + Category + "&SubCat=" + SubCategory;
        else
            return "ProductListing.aspx?Cat=" + Category;
    }

    private void LoadRecommendations(int CatID, int SubCatID, int PCode)
    {

        Product objP = new Product();
        int TotalRecords = 0;
        DataSet ds = objP.GetList(CatID.ToString(), SubCatID.ToString(), "", "", false, false, "", 1, 0, "", "", out TotalRecords);

        StringBuilder sb = new StringBuilder();
        DataView dv = ds.Tables[0].DefaultView;
        dv.RowFilter = "ProductID NOT IN(" + PCode.ToString() + ")";
        DataTable dt = dv.ToTable();
        if (TotalRecords > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 3 != 0 || i == 0)
                {
                    if (i == 0)
                    {
                        sb.Append("<div class=\"item active\">");
                    }
                    sb.Append("<div class=\"col-sm-44\"><div class=\"product-image-wrapper\"><div class=\"single-products\"> <div class=\"productinfo text-center\">");

                    if (dt.Rows[i]["ImageName"] != "")
                        sb.Append(" <img src=\"ProductImages/" + dt.Rows[i]["ProductID"] + "/" + dt.Rows[i]["ImageName"] + "\" alt=\"\" style=\"width:200px; height:160px\" />");
                    else
                    {
                        if (Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + dt.Rows[i]["ProductID"]))
                        {
                            string[] files = Directory.GetFiles(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + dt.Rows[i]["ProductID"]));
                            if (files.Length > 0)
                                sb.Append(" <img src=\"ProductImages/" + dt.Rows[i]["ProductID"] + "/" + files[i].Substring(files[0].LastIndexOf("\\") + 1) + "\" alt=\"\" style=\"width:200px; height:160px\"/>");
                        }
                        else
                        {
                            sb.Append(" <img src=\"ProductImages/Not_available.jpg" + "\" alt=\"\" style=\"width:200px; height:160px\"/>");
                        }
                    }
                    sb.Append("<h2>" + (Convert.ToDecimal(dt.Rows[i]["Price"]) == 0 ? "" : "$" + dt.Rows[i]["Price"]) + "</h2>");
                    sb.Append("<p>" + dt.Rows[i]["ProductCode"] + (dt.Rows[i]["Name"] != "" ? " - " + dt.Rows[i]["Name"] : "") + "</p>");
                    sb.Append("<button type=\"button\" class=\"btn btn-default add-to-cart\" style=\"background-color:#FE980F;\" onclick=\"return btnAddToCart_Client2(" + dt.Rows[i]["ProductID"] + ") \"><i class=\"fa fa-shopping-cart\"></i>Add to cart</button>");
                    sb.Append("</div></div></div></div>");

                    //OnClientClick='<%# string.Format("javascript:return btnAddToCart_Client(\"{0}\")", Eval("ProductID")) %>'
                }
                else
                {
                    sb.Append("</div>");
                    //if more items
                    if (i < dt.Rows.Count - 1)
                    {
                        sb.Append("<div class=\"item\">");

                        sb.Append("<div class=\"col-sm-44\"><div class=\"product-image-wrapper\"><div class=\"single-products\"> <div class=\"productinfo text-center\">");

                        if (dt.Rows[i]["ImageName"] != "")
                            sb.Append(" <img src=\"ProductImages/" + dt.Rows[i]["ProductID"] + "/" + dt.Rows[i]["ImageName"] + "\" alt=\"\" style=\"width:200px; height:160px\" />");
                        else
                        {
                            if (Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + dt.Rows[i]["ProductID"]))
                            {
                                string[] files = Directory.GetFiles(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + dt.Rows[i]["ProductID"]));
                                if (files.Length > 0)
                                    sb.Append(" <img src=\"ProductImages/" + dt.Rows[i]["ProductID"] + "/" + files[i].Substring(files[0].LastIndexOf("\\") + 1) + "\" alt=\"\" style=\"width:200px; height:160px\"/>");
                            }
                            else
                            {
                                sb.Append(" <img src=\"ProductImages/Not_available.jpg" + "\" alt=\"\" style=\"width:200px; height:160px\"/>");
                            }
                        }
                        sb.Append("<h2>" + (Convert.ToDecimal(dt.Rows[i]["Price"]) == 0 ? "" : "$" + dt.Rows[i]["Price"]) + "</h2>");
                        sb.Append("<p>" + dt.Rows[i]["ProductCode"] + (dt.Rows[i]["Name"] != "" ? " - " + dt.Rows[i]["Name"] : "") + "</p>");
                        sb.Append("<button type=\"button\" class=\"btn btn-default add-to-cart\" style=\"background-color:#FE980F;\" onclick=\"return btnAddToCart_Client2(" + dt.Rows[i]["ProductID"] + ") \"><i class=\"fa fa-shopping-cart\"></i>Add to cart</button>");
                        sb.Append("</div></div></div></div>");
                    }
                }
            }
            // sb.Append("</div>");



            if (dt.Rows.Count >= 1 && dt.Rows.Count <= 3)
                sb.Append("</div>");
            else if (dt.Rows.Count > 3)
                sb.Append("</div></div>");
        }
        if (sb.ToString() != "")
            ltRecommendations.Text = sb.ToString();
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