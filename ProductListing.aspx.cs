using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductListing : System.Web.UI.Page
{
    int gTotalRecords;
    string[] _SrchAttributes;
    private int gCurrentPage
    {
        get
        {
            object objPage = ViewState["_CurrentPage"];
            int _CurrentPage = 0;
            if (objPage == null)
            {
                _CurrentPage = 0;
            }
            else
            {
                _CurrentPage = (int)objPage;
            }
            return _CurrentPage;
        }
        set { ViewState["_CurrentPage"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Products | Masline";

        if (!IsPostBack)
        {
            string Category = "", SubCategory = "";
            bool isListing = false;
            if (Request.QueryString["Cat"] != null) // show categories
                Category = Convert.ToString(Request.QueryString["Cat"]);

            if (Request.QueryString["SubCat"] != null) //show subcategories under a specific category
                SubCategory = Convert.ToString(Request.QueryString["SubCat"]);

            if (Request.QueryString["listing"] != null)
                isListing = Convert.ToBoolean(Request.QueryString["listing"]);

            PopulateSearchCriteria();

            if (gCurrentPage <= 1)
                gCurrentPage = 1;

            if (ViewState["SortDirection"] == null)
                ViewState["SortDirection"] = "ASC";

            if (ViewState["SortField"] == null)
                ViewState["SortField"] = "Product";


            if (Request.QueryString.ToString() == "")
                GetProducts("", "", "", "", false, false, "", gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, ViewState["SortField"].ToString(), ViewState["SortDirection"].ToString());
            else
                GetProducts(Category, SubCategory, "", "", false, false, "", gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, ViewState["SortField"].ToString(), ViewState["SortDirection"].ToString());
            //}
            if (hdnHideCols.Value == "" || hdnHideCols.Value == null)
                hdnHideCols.Value = "true";
        }
        else if (hdnAttributes.Value != "")
        {
            string[] arr = hdnAttributes.Value.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                string[] arr1 = arr[i].Split('|');
                ltAttributes.Text = ltAttributes.Text.Replace("id='" + arr1[0] + "'", "id='" + arr1[0] + "' value='" + arr1[1] + "'");
            }
        }

    }

    //private string GetCategories(string Cat, string SubCat)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<ul id='menu-v1' style='font-size:20px;'>");
    //    sb.Append("<li><a href='Catalogue.aspx'><span>Catalog</span></a>");

    //    Category objCat = new Category();
    //    DataTable dt = objCat.CategoriesList();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        if (Cat != "")
    //            dt = dt.Select("shortcode='" + Cat + "'").CopyToDataTable();

    //        sb.Append("<ul>");
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            if (Convert.ToInt32(dt.Rows[i]["subCategoryCount"]) > 0)// if subcategories are present
    //            {
    //                sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "", false) + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
    //                Category objCat2 = new Category(Convert.ToInt32(dt.Rows[i]["categoryID"]));
    //                if (objCat2.SubCategories.Count > 0)
    //                {
    //                    sb.Append("<ul>");
    //                    for (int j = 0; j < objCat2.SubCategories.Count; j++)
    //                    {
    //                        if ((SubCat != "" && objCat2.SubCategories[j].ShortCode == SubCat) || SubCat == "") // Redirect to products
    //                            sb.Append("<li><a href='" + GenerateURL(objCat2.SubCategories[j].C_ShortCode, objCat2.SubCategories[j].ShortCode, true) + "'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
    //                        //sb.Append("<li><a href='#' onclick=\'GetProducts('" + objCat2.SubCategories[j].C_ShortCode + "', '" + objCat2.SubCategories[j].ShortCode + "');'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");

    //                    }
    //                    sb.Append("</ul>");
    //                    if (sb.ToString().LastIndexOf("<ul></ul>") > -1)
    //                        sb.Remove(sb.ToString().LastIndexOf("<ul></ul>"), ("<ul></ul>").Length);//If no subcategory then remove the ul tags
    //                    sb.Append("</li>");
    //                }
    //            }
    //            else //No sub categories
    //            {
    //                sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "", true) + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
    //                //sb.Append("<li><a href='#' onclick='GetProducts('" + Convert.ToString(dt.Rows[i]["shortcode"]) + "', '" + "" + "');'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
    //            }
    //        }


    //        sb.Append("</ul>");
    //    }
    //    sb.Append("</li>");
    //    sb.Append("</ul>");
    //    return sb.ToString();
    //}

    private string GenerateURL(string Category, string SubCategory, bool productListing)
    {
        string url = "";
        if (!String.IsNullOrEmpty(SubCategory))
            url = "ProductListing.aspx?Cat=" + Category + "&SubCat=" + SubCategory;
        else
            url = "ProductListing.aspx?Cat=" + Category;
        return url = url + "&listing=" + productListing.ToString();
    }
    private void PopulateSearchCriteria()
    {
        DataTable dt = new DataTable();
        Manufacturer objM = new Manufacturer();
        int TR;
        dt = objM.GetList(1, 101, "", "", out TR);
        chkManufacturer.DataSource = dt;
        chkManufacturer.DataTextField = "Name";
        chkManufacturer.DataValueField = "ManufacturerID";
        chkManufacturer.DataBind();

        Category objC = new Category();
        dt = objC.CategoriesList();
        chkCategory.DataSource = dt;
        chkCategory.DataTextField = "Name";
        chkCategory.DataValueField = "CategoryID";//"ShortCode";
        chkCategory.DataBind();

        SubCategory objS = new SubCategory();
        dt = objS.GetList();
        chkSubCategory.DataSource = dt;
        chkSubCategory.DataTextField = "Name";
        chkSubCategory.DataValueField = "SubCategoryID"; //"S_ShortCode";
        chkSubCategory.DataBind();

        string filter = "";
        Attribute objA = new Attribute();
        dt = objA.GetList();
        if (chkCategory.Items.Cast<ListItem>().Count(li => li.Selected) > 0)
        {
            foreach (ListItem itm in chkCategory.Items)
            {
                if (itm.Selected)
                    filter = (filter == "") ? "CategoryID ='" + itm.Value + "'" : "or CategoryID ='" + itm.Value + "'";//filter = (filter == "") ? "C_ShortCode ='" + itm.Value + "'" : "or C_ShortCode ='" + itm.Value + "'";
            }
        }

        if (chkSubCategory.Items.Cast<ListItem>().Count(li => li.Selected) > 0)
        {
            foreach (ListItem itm in chkSubCategory.Items)
            {
                if (itm.Selected)
                    filter = (filter == "") ? "SubCategoryID ='" + itm.Value + "'" : "or SubCategoryID ='" + itm.Value + "'";//filter = (filter == "") ? "S_ShortCode ='" + itm.Value + "'" : "or S_ShortCode ='" + itm.Value + "'";
            }
        }
        if (filter != "")
        {
            if (dt.Select(filter).Length > 0)
            {
                dt = dt.Select(filter).CopyToDataTable();
            }
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<table>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr><td style=\"padding-bottom:15px;\">" + dt.Rows[i]["Name"] + ": </td><td style=\"padding-bottom:15px; padding-left:15px;\"><input type=\"text\" name='" + dt.Rows[i]["AttributeID"] + "' id='" + dt.Rows[i]["AttributeID"] + "' runat=\"server\" style=\" width:80px;\" onblur=\"ApplyFilter()\" EnableViewState=\"true\" /><br/></td></tr>");
        }
        sb.Append("</table>");
        ltAttributes.Text = sb.ToString();

        dt = new DataTable();
        dt.Columns.Add("Index", typeof(int));
        dt.Columns.Add("PageSize", typeof(string));
        for (int i = 0; i < 100; i++)
        {
            dt.Rows.Add(i, (i + 1).ToString());
        }
        dt.Rows.Add(100, "All");
        ddlPageSize.DataSource = dt;
        ddlPageSize.DataTextField = "PageSize";
        ddlPageSize.DataValueField = "Index";
        ddlPageSize.DataBind();
        ddlPageSize.Items.FindByValue("9").Selected = true;
    }
    private void GetProducts(string CategoryID, string SubCategoryID, string ManufacturerID, string Attributes, bool IsInStock, bool IsPricingAvailable, string PriceRange, int CurrentPageNo, int PageSize, string SortField, string SortOrder)
    {
        try
        {
            Product objP = new Product();
            DataTable dt = new DataTable();
            int TotalRecords;
            dt = objP.GetList(CategoryID, SubCategoryID, ManufacturerID, Attributes, IsInStock, IsPricingAvailable, PriceRange, CurrentPageNo, PageSize, SortField, SortOrder, out TotalRecords).Tables[0];
            if (dt != null)
            {
                //DataTable dtAttributes = Product.GetSearchableAttributes();
                //if (dtAttributes != null && dtAttributes.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtAttributes.Rows.Count; i++)
                //        dt.Columns.Add(dtAttributes.Rows[i]["Name"].ToString(), typeof(string));
                //}
                for (int j = 0; j < dt.Rows.Count; j++) //For every row
                {
                    //Append attribute values 
                    //if (Convert.ToString(dt.Rows[j]["AttributeInfo"]) != "")
                    //{
                    //    string[] arr = Convert.ToString(dt.Rows[j]["AttributeInfo"]).Split(',');
                    //    for (int i = 0; i < arr.Length; i++)
                    //    {
                    //        string[] _arr = arr[i].Split('|');
                    //        for (int k = 0; k < dtAttributes.Rows.Count; k++) //For every col of row
                    //        {
                    //            if (Array.IndexOf(_arr, dtAttributes.Rows[k]["Name"].ToString()) > -1)
                    //            {
                    //                dt.Rows[j][dtAttributes.Rows[k]["Name"].ToString()] = _arr[1].ToString();
                    //                break;
                    //            }
                    //        }
                    //        //}
                    //    }
                    //}

                    //If the attribute is not applicable for the category to which the product belongs then NA
                    DataTable dtAttributes = Product.GetSearchableAttributes();
                    string[] attributes = dtAttributes
                                             .AsEnumerable()
                                             .Select(row => row.Field<string>("Name"))
                                             .ToArray();
                    foreach (DataColumn dc in dt.Columns)//for (int col = 19; col < dt.Columns.Count; col++)
                    {
                        if (Array.IndexOf(attributes, dc.ColumnName) > -1)
                        {
                            for (int attRow = 0; attRow < dtAttributes.Rows.Count; attRow++)
                            {
                                if (dc.ColumnName == dtAttributes.Rows[attRow]["Name"].ToString())
                                {
                                    string[] arrAttr = dtAttributes.Rows[attRow]["Categories"].ToString().Split(',');//Comma separate list of categories to which the attribute is applicable
                                    if (Array.IndexOf(arrAttr, dt.Rows[j]["CategoryID"].ToString()) == -1) //If the categoryID of product is not found in list of categories to which attribute is applicable
                                    {
                                        string colname = dtAttributes.Rows[attRow]["Name"].ToString();
                                        dt.Rows[j][colname] = "NA";
                                        break;
                                    }
                                    else
                                        break;
                                }
                            }
                        }
                    }
                    //**
                }
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
                //gvProduct1.DataSource = dt;
                //gvProduct1.DataBind();
                gCurrentPage = CurrentPageNo;
                gTotalRecords = TotalRecords;
                //BindPagingList();
                PopulatePager(TotalRecords, gCurrentPage, PageSize);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gvProducts_DataBound(object sender, GridViewRowEventArgs e)
    {
        //** For gray borders to gridview, for ff & ie
        foreach (TableCell tc in e.Row.Cells)
            tc.Attributes["style"] = "border:1px solid #CCCCCC";
            
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvProducts.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvProducts.RowStyle.CssClass = "ListingRowStyle";
                       
            try
            {
                /* Map the path of product image*/
                Image img = (Image)e.Row.FindControl("imgProduct");
                Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");
                string ProdImagePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "//" + lblImagePath.Text;
                if (File.Exists(Server.MapPath(ProdImagePath)))
                    img.ImageUrl = ProdImagePath;
                else
                    img.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//Not_available.jpg";//img.Style.Add(HtmlTextWriterStyle.Display, "none");
                //****

                //**Clicking on image, product name redirects to product description page
                img.Attributes.Add("onclick", "location='ProductDetails.aspx?PCode=" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "'");
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                lblProductCode.Attributes.Add("onclick", "location='ProductDetails.aspx?PCode=" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "'");
                //****


                bool IsPriceAvail = false, IsAvail = false;
                Literal ltQty = (Literal)e.Row.FindControl("ltQty");
                Literal ltPricing = (Literal)e.Row.FindControl("ltPricing");
                Label lblInventory = (Label)e.Row.FindControl("lblInventory");
                lblInventory.Style.Add(HtmlTextWriterStyle.Display, "block");
                LinkButton btnCallAvail = (LinkButton)e.Row.FindControl("btnCallAvail");

                //when pricing as per qty NA                
                LinkButton btnGetPrice = (LinkButton)e.Row.FindControl("btnGetPrice");
                Label lblPrice = (Label)e.Row.FindControl("lblPrice"); //The price of product
                Label lblPricing = (Label)e.Row.FindControl("lblPricing"); //1|9|6.25,10|24|5.25
                //[-]
                //System.Web.UI.HtmlControls.HtmlButton btnAddToCart = (System.Web.UI.HtmlControls.HtmlButton)e.Row.FindControl("btnAddToCart");
                ImageButton btnAddToCart = (ImageButton)e.Row.FindControl("btnAddToCart");
                if (Convert.ToInt32(lblInventory.Text) <= 0)
                    btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "block");
                else
                {
                    btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "none");
                    IsAvail = true;
                }
                if (lblInventory.Text == "0")
                    lblInventory.Text = "Unavailable";

                if (lblPricing.Text == "") //Multiple prices not defined
                {
                    lblPrice.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ltPricing.Visible = false;
                    if (Convert.ToDouble(lblPrice.Text) <= 0)
                    {
                        ltQty.Text = "-";
                        lblPrice.Text = "";//"Requires Quote";
                        btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "block");
                    }
                    else
                    {
                        IsPriceAvail = true;
                        ltQty.Text = "1 and Up";
                        btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                }
                else //display pricing as per quantity table
                {
                    btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "none");
                    lblPrice.Style.Add(HtmlTextWriterStyle.Display, "none");

                    StringBuilder sbQty = new StringBuilder();
                    sbQty.Append("<table class=\"table table-striped table-hover\">");

                    StringBuilder sbPrice = new StringBuilder();
                    sbPrice.Append("<table class=\"table table-striped table-hover\">");

                    //sb.Append("<tr><td>Quantity</td><td>Price Per Unit</td></tr>");
                    string[] arrPricing = lblPricing.Text.Split(',');
                    string Qty = "";
                    for (int i = 0; i < arrPricing.Length; i++)
                    {
                        sbQty.Append("<tr>"); sbPrice.Append("<tr>");
                        string[] arrQtyPrice = arrPricing[i].Split('|');
                        for (int j = 0; j < arrQtyPrice.Length; j++)
                        {
                            if (j > 0 && j % 2 == 0)
                            {
                                sbQty.Append("<td>" + Qty + "</td>");
                                sbPrice.Append("<td>" + arrQtyPrice[j] + "</td>");
                                Qty = "";
                            }
                            else
                            {
                                if (Convert.ToInt32(arrQtyPrice[j]) > 0)
                                    Qty = Qty == "" ? arrQtyPrice[j] : Qty + " - " + arrQtyPrice[j];
                                else
                                    Qty = Qty == "" ? arrQtyPrice[j] : Qty + "and Up";
                            }
                        }
                        sbQty.Append("</tr>"); sbPrice.Append("</tr>");
                    }
                    sbQty.Append("</table>"); sbPrice.Append("</table>");
                    ltQty.Text = sbQty.ToString();
                    ltPricing.Text = sbPrice.ToString();
                    IsPriceAvail = true;
                }
                if (IsPriceAvail && IsAvail)
                    btnAddToCart.Style.Add(HtmlTextWriterStyle.Display, "block");
                else
                    btnAddToCart.Style.Add(HtmlTextWriterStyle.Display, "none");
                                
                //Display none, as currently such a situation may not arise
                //btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "none");
                //btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            catch (Exception ex)
            {
            }
        }
    }

    private string GetSortDirection(string column)
    {
        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortField"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortField"] = column;
        return sortDirection;
    }

    private void GetSearchAttributes()
    {
        DataTable dtAttributes = Product.GetSearchableAttributes();
        DataRow dr = dtAttributes.NewRow();
        dr["Name"] = "Product";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "Manufacturer";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "Price";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "Descrip";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "Technology";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "Harmonized Code";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "Category";
        dtAttributes.Rows.Add(dr);
        dr = dtAttributes.NewRow();
        dr["Name"] = "SubCategory";
        dtAttributes.Rows.Add(dr);
        _SrchAttributes = dtAttributes
                                         .AsEnumerable()
                                         .Select(row => row.Field<string>("Name"))
                                         .ToArray();
    }
    protected void gvProducts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GetSearchAttributes();
                foreach (TableCell tc in e.Row.Cells)
                {
                    if (tc.HasControls())
                    {
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        if (ViewState["SortDirection"] == null)
                        {
                            ViewState["SortDirection"] = "ASC";
                        }
                        img.Height = Unit.Pixel(16);
                        img.Width = Unit.Pixel(20);
                        if (tc.Controls[0].GetType().Name == "DataControlLinkButton")
                        {
                            LinkButton lnk = (LinkButton)tc.Controls[0];
                            //foreach (DataControlField field in gvProducts.Columns)
                            //{
                            //    if(field.SortExpression == ViewState["SortField"].ToString())
                            //}
                            if (ViewState["SortField"] == null)
                            {
                                if (lnk.Text == "Product Name")
                                {
                                    tc.Controls.Add(new LiteralControl(" "));
                                    tc.Controls.Add(img);
                                }
                            }
                            else
                            {
                                if (Array.IndexOf(_SrchAttributes, lnk.CommandArgument) >= 0)
                                {
                                    if (lnk.CommandArgument == ViewState["SortField"].ToString())
                                    {
                                        img.ImageUrl = "~/images/" + (ViewState["SortDirection"].ToString() == "ASC" ? "up_arrow" : "down_arrow") + ".png";
                                        tc.Controls.Add(new LiteralControl(" "));
                                        tc.Controls.Add(img);
                                    }
                                    //else
                                    //{
                                    //    img.ImageUrl = "~/images/Sort.png";
                                    //    tc.Controls.Add(new LiteralControl(" "));
                                    //    tc.Controls.Add(img);
                                    //}
                                }
                            }
                        }
                        //else if (tc.Controls[3].GetType().Name == "LinkButton")
                        //{
                        //    string HeaderText = ((System.Web.UI.WebControls.DataControlFieldCell)(((LinkButton)tc.Controls[3]).Parent)).ContainingField.ToString();//((System.Web.UI.WebControls.DataControlFieldCell)(((Label)tc.Controls[3]).Parent)).ContainingField.ToString();
                        //    HeaderText = (HeaderText == "Description") ? "Descrip" : HeaderText;
                        //    if (Array.IndexOf(_SrchAttributes, HeaderText) >= 0)
                        //    {
                        //        if (HeaderText == ViewState["SortField"].ToString())
                        //        {
                        //            tc.Controls.Add(new LiteralControl(" "));
                        //            tc.Controls.Add(img);
                        //        }
                        //    }

                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvProducts_OnSorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            string SortField = e.SortExpression;
            string SortOrder = GetSortDirection(e.SortExpression);
            ViewState["SortField"] = SortField;
            ViewState["SortDirection"] = SortOrder;

            gCurrentPage = 1;
            GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, false, false, Convert.ToString(hdnPriceRange.Value), gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
            if (hdnHideCols.Value.ToLower() == "true")
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key1", "HideCols();", true);
            else // Inorder to populate data in hidden columns
            {
                btnShowAttributes_Click(sender, e);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Sort")
        {
            int ProductID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "ShowProductDetails")
            {
                Response.Redirect("ProductDetails.aspx?PCode=" + ProductID);
            }
        }
        //if (e.CommandName == "HeaderClick")
        //{
        //    switch (e.CommandArgument.ToString())
        //    {
        //        case "Descrip":
        //        case "Technology":
        //        case "HarmonizedCode":
        //        case "Category":
        //        case "SubCategory":
        //        case "Capacitance":
        //        case "Voltage":
        //        case "Material":
        //        case "Style":
        //        case "Tolerance":
        //        case "Temperature":
        //        case "Construction":
        //        case "Features":
        //        case "Wattage":
        //        case "Resistance":
        //            string SortField = e.CommandArgument.ToString();
        //            string SortOrder = GetSortDirection(e.CommandArgument.ToString());
        //            ViewState["SortField"] = SortField;
        //            ViewState["SortDirection"] = SortOrder;
        //            gCurrentPage = 1;
        //            GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, false, false, Convert.ToString(hdnPriceRange.Value), gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        //            if (hdnHideCols.Value.ToLower() == "true")
        //                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key1", "HideCols();", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if(e.CommandName =="CollapseExpand")
        //{
        //    ImageButton img = (ImageButton)sender;
        //    if (img.ImageUrl.Contains("minus.png"))
        //    {
        //        img.ImageUrl = img.ImageUrl.Replace("minus", "plus");
        //        gvProducts.Columns[Convert.ToInt32(e.CommandArgument)].ItemStyle.CssClass = "";
        //        gvProducts.Columns[Convert.ToInt32(e.CommandArgument)].ItemStyle.Width = Unit.Pixel(100);
        //    }
        //    else
        //    {
        //        img.ImageUrl = img.ImageUrl.Replace("plus", "minus");
        //        gvProducts.Columns[Convert.ToInt32(e.CommandArgument)].ItemStyle.CssClass = "Shorter";
        //    }
        //}
    }
    //protected void lbtnHeader_Click(object sender, EventArgs e)
    //{
    //    LinkButton lbtn = (LinkButton)sender;
    //    GridViewRow row = (GridViewRow)lbtn.NamingContainer;

    //    switch (lbtn.CommandArgument.ToString())
    //    {
    //        case "Descrip":
    //        case "Technology":
    //        case "HarmonizedCode":
    //        case "Category":
    //        case "SubCategory":
    //        case "Capacitance":
    //        case "Voltage":
    //        case "Material":
    //        case "Style":
    //        case "Tolerance":
    //        case "Temperature":
    //        case "Construction":
    //        case "Features":
    //        case "Wattage":
    //        case "Resistance":
    //            string SortField = lbtn.CommandArgument.ToString();
    //            string SortOrder = GetSortDirection(lbtn.CommandArgument.ToString());
    //            ViewState["SortField"] = SortField;
    //            ViewState["SortDirection"] = SortOrder;
    //            gCurrentPage = 1;
    //            GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, false, false, Convert.ToString(hdnPriceRange.Value), gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
    //            if (hdnHideCols.Value.ToLower() == "true")
    //                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key1", "HideCols();", true);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //protected void lbtnHeader_PreRender(object sender, EventArgs e)
    //{
    //    LinkButton btn = sender as LinkButton;
    //    ScriptManager sc = ScriptManager.GetCurrent(this.Page);
    //    sc.RegisterPostBackControl(btn);
    //}
    protected void btnExpand_Click(object sender, EventArgs e)
    {
        try
        {
            Label lblHeader = new Label();
            Image img = (Image)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[1];
            Label lbl = new Label();
            string lblID = "";
            if (gvProducts.Rows[0].FindControl("lblDescrip") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 7) //7
            {
                lblID = "lblDescrip";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Description";
            }
            else if (gvProducts.Rows[0].FindControl("lblTechnology") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 8) //8
            {
                lblID = "lblTechnology";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Technology";
            }
            else if (gvProducts.Rows[0].FindControl("lblHarmonizedCode") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 9)
            {
                lblID = "lblHarmonizedCode";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Harmonized Code";
            }
            else if (gvProducts.Rows[0].FindControl("lblCategory") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 10)
            {
                lblID = "lblCategory";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Category";
            }
            else if (gvProducts.Rows[0].FindControl("lblSubCategory") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 11)
            {
                lblID = "lblSubCategory";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Sub-Category";
            }
            else if (gvProducts.Rows[0].FindControl("lblCapacitance") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 12)
            {
                lblID = "lblCapacitance";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Capacitance";
            }
            else if (gvProducts.Rows[0].FindControl("lblVoltage") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 13)
            {
                lblID = "lblVoltage";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Voltage";
            }
            else if (gvProducts.Rows[0].FindControl("lblMaterial") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 14)
            {
                lblID = "lblMaterial";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Material";
            }
            else if (gvProducts.Rows[0].FindControl("lblStyle") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 15)
            {
                lblID = "lblStyle";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Style";
            }
            else if (gvProducts.Rows[0].FindControl("lblTolerance") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 16) //8
            {
                lblID = "lblTolerance";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Tolerance";
            }
            else if (gvProducts.Rows[0].FindControl("lblTemperature") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 17)
            {
                lblID = "lblTemperature";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Temperature";
            }
            else if (gvProducts.Rows[0].FindControl("lblConstruction") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 18)
            {
                lblID = "lblConstruction";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Construction";
            }
            else if (gvProducts.Rows[0].FindControl("lblFeatures") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 19)
            {
                lblID = "lblFeatures";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Features";
            }
            else if (gvProducts.Rows[0].FindControl("lblWattage") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 20)
            {
                lblID = "lblWattage";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Wattage";
            }
            else if (gvProducts.Rows[0].FindControl("lblResistance") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 21)
            {
                lblID = "lblResistance";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Resistance";
            }


            if (!hdnColNoImg.Value.Contains("plus"))
                lblHeader.Text = "";
            if (hdnColNoImg.Value.Contains("plus"))
            {
                img.ImageUrl = "~/images/minus.png";
                img.ToolTip = "Collapse";
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].ItemStyle.CssClass = "";
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].ItemStyle.Width = Unit.Pixel(150);
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].HeaderStyle.Width = Unit.Pixel(150);
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].ControlStyle.Width = Unit.Pixel(150);

                for (int i = 0; i < gvProducts.Rows.Count; i++)
                {
                    lbl = (Label)gvProducts.Rows[i].FindControl(lblID);
                    lbl.Style.Add(HtmlTextWriterStyle.Width, "auto");
                    lbl.Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
                    lbl.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                }
                lblHeader.Style.Add(HtmlTextWriterStyle.Width, "auto");
                lblHeader.Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
                lblHeader.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            }
            else
            {
                //Image img = (Image)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].FindControl("ibtnExpand");
                img.ImageUrl = "~/images/plus.png";
                img.ToolTip = "Expand";
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].ItemStyle.CssClass = "Shorter";
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].ItemStyle.Width = Unit.Pixel(0);
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].HeaderStyle.Width = Unit.Pixel(0);
                gvProducts.Columns[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].ControlStyle.Width = Unit.Pixel(0);

                for (int i = 0; i < gvProducts.Rows.Count; i++)
                {
                    lbl = (Label)gvProducts.Rows[i].FindControl(lblID);
                    lbl.Style.Add(HtmlTextWriterStyle.Width, "0px");
                    lbl.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
                    lbl.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                }
                lblHeader.Style.Add(HtmlTextWriterStyle.Width, "0px");
                lblHeader.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
                if (hdnHideCols.Value.ToLower() == "true")
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key1", "HideCols();", true);
            }
        }
        catch (Exception ex)
        { }
    }

    protected void btnShowAttributes_Click(object sender, EventArgs e)
    {
        try
        {
            //LinkButton lblHeader = new LinkButton();
            string lblID = "";

            lblID = "lblDescrip";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[7].Controls[3];
            //lblHeader.Text = "Description";
            ShowHide(7, lblID);//ShowHide(lblHeader, 7, lblID);

            lblID = "lblTechnology";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[8].Controls[3];
            //lblHeader.Text = "Technology";
            ShowHide(8, lblID);//ShowHide(lblHeader, 8, lblID);

            lblID = "lblHarmonizedCode";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[9].Controls[3];
            //lblHeader.Text = "Harmonized Code";
            ShowHide(9, lblID);//ShowHide(lblHeader, 9, lblID);

            lblID = "lblCategory";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[10].Controls[3];
            //lblHeader.Text = "Category";
            ShowHide(10, lblID);//ShowHide(lblHeader, 10, lblID);

            lblID = "lblSubCategory";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[11].Controls[3];
            //lblHeader.Text = "Sub-Category";
            ShowHide(11, lblID);//ShowHide(lblHeader, 11, lblID);

            lblID = "lblCapacitance";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[12].Controls[3];
            //lblHeader.Text = "Capacitance";
            ShowHide(12, lblID);//ShowHide(lblHeader, 12, lblID);

            lblID = "lblVoltage";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[13].Controls[3];
            //lblHeader.Text = "Voltage";
            ShowHide(13, lblID);//ShowHide(lblHeader, 13, lblID);

            lblID = "lblMaterial";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[14].Controls[3];
            //lblHeader.Text = "Material";
            ShowHide(14, lblID);//ShowHide(lblHeader, 14, lblID);

            lblID = "lblStyle";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[15].Controls[3];
            //lblHeader.Text = "Style";
            ShowHide(15, lblID);//ShowHide(lblHeader, 15, lblID);

            lblID = "lblTolerance";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[16].Controls[3];
            //lblHeader.Text = "Tolerance";
            ShowHide(16, lblID);//ShowHide(lblHeader, 16, lblID);

            lblID = "lblTemperature";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[17].Controls[3];
            //lblHeader.Text = "Temperature";
            ShowHide(17, lblID);//ShowHide(lblHeader, 17, lblID);

            lblID = "lblConstruction";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[18].Controls[3];
            //lblHeader.Text = "Construction";
            ShowHide(18, lblID);//ShowHide(lblHeader, 18, lblID);

            lblID = "lblFeatures";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[19].Controls[3];
            //lblHeader.Text = "Features";
            ShowHide(19, lblID);//ShowHide(lblHeader, 19, lblID);

            lblID = "lblWattage";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[20].Controls[3];
            //lblHeader.Text = "Wattage";
            ShowHide(20, lblID);//ShowHide(lblHeader, 20, lblID);

            lblID = "lblResistance";
            //lblHeader = (LinkButton)gvProducts.HeaderRow.Cells[21].Controls[3];
            //lblHeader.Text = "Resistance";
            ShowHide(21, lblID);//ShowHide(lblHeader, 21, lblID);


            string Toggle_All = false.ToString();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key", "SetToggle_All('" + Toggle_All + "');", true);

            if (hdnHideCols.Value.ToLower() == "true")
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key1", "HideCols();", true);

            //for (int j =11; j <= 7; j--) //Cols
            //{
            //    for (int i = 0; i < gvProducts.Rows.Count; i++)
            //    {
            //        if (hdnHideCols.Value.ToLower() ==  "false")
            //        {
            //            gvProducts.Rows[i].Cells[j].Style.Add(HtmlTextWriterStyle.Display, "block");                        
            //        }
            //        else
            //        {
            //            gvProducts.Rows[i].Cells[j].Style.Add(HtmlTextWriterStyle.Display, "none");
            //        }
            //    }
            //    if(hdnHideCols.Value.ToLower() == "false")
            //        gvProducts.HeaderRow.Cells[j].Style.Add(HtmlTextWriterStyle.Display, "block");
            //    else
            //        gvProducts.HeaderRow.Cells[j].Style.Add(HtmlTextWriterStyle.Display, "none");
            //}
            Image ibtnShowAttributes = (Image)gvProducts.HeaderRow.Cells[6].Controls[1];
            if (ibtnShowAttributes.ImageUrl.IndexOf("show_attributes") != -1)//expanded
                ibtnShowAttributes.ImageUrl = "~/images/hide_attributes.png";
            else
                ibtnShowAttributes.ImageUrl = "~/images/show_attributes.png";
        }
        catch (Exception ex)
        {
        }
    }

    //private void ShowHide(LinkButton lblHeader, int Index, string lblID)
    //{
    //    Label lbl;
    //    Image img = (Image)gvProducts.HeaderRow.Cells[Index].Controls[1];
    //    if (!hdnColImg.Value.Contains("plus"))
    //        lblHeader.Text = "";
    //    if (hdnColImg.Value.Contains("plus"))
    //    {
    //        img.ImageUrl = "~/images/minus.png";
    //        img.ToolTip = "Collapse";
    //        gvProducts.Columns[Index].ItemStyle.CssClass = "";
    //        gvProducts.Columns[Index].ItemStyle.Width = Unit.Pixel(150);
    //        gvProducts.Columns[Index].HeaderStyle.Width = Unit.Pixel(150);
    //        gvProducts.Columns[Index].ControlStyle.Width = Unit.Pixel(150);

    //        for (int i = 0; i < gvProducts.Rows.Count; i++)
    //        {
    //            lbl = (Label)gvProducts.Rows[i].FindControl(lblID);
    //            lbl.Style.Add(HtmlTextWriterStyle.Width, "auto");
    //            lbl.Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
    //            lbl.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
    //        }
    //        lblHeader.Style.Add(HtmlTextWriterStyle.Width, "auto");
    //        lblHeader.Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
    //        lblHeader.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
    //    }
    //    else
    //    {
    //        img.ImageUrl = "~/images/plus.png";
    //        img.ToolTip = "Expand";
    //        gvProducts.Columns[Index].ItemStyle.CssClass = "Shorter";
    //        gvProducts.Columns[Index].ItemStyle.Width = Unit.Pixel(0);
    //        gvProducts.Columns[Index].HeaderStyle.Width = Unit.Pixel(0);
    //        gvProducts.Columns[Index].ControlStyle.Width = Unit.Pixel(0);

    //        for (int i = 0; i < gvProducts.Rows.Count; i++)
    //        {
    //            lbl = (Label)gvProducts.Rows[i].FindControl(lblID);
    //            lbl.Style.Add(HtmlTextWriterStyle.Width, "0px");
    //            lbl.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
    //            lbl.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
    //        }
    //        lblHeader.Style.Add(HtmlTextWriterStyle.Width, "0px");
    //        lblHeader.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
    //    }
    //}

    private void ShowHide(int Index, string lblID)
    {
        Label lbl;
        //if (!hdnColImg.Value.Contains("plus"))
        //    lblHeader.Text = "";
        if (!hdnColImg.Value.Contains("plus"))
        {
            //img.ImageUrl = "~/images/minus.png";
            //img.ToolTip = "Collapse";
            gvProducts.Columns[Index].ItemStyle.CssClass = "";
            gvProducts.Columns[Index].ItemStyle.Width = Unit.Pixel(150);
            gvProducts.Columns[Index].HeaderStyle.Width = Unit.Pixel(150);
            gvProducts.Columns[Index].ControlStyle.Width = Unit.Pixel(150);

            for (int i = 0; i < gvProducts.Rows.Count; i++)
            {
                lbl = (Label)gvProducts.Rows[i].FindControl(lblID);
                lbl.Style.Add(HtmlTextWriterStyle.Width, "auto");
                lbl.Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
                lbl.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            }
            gvProducts.HeaderRow.Cells[Index].Style.Add(HtmlTextWriterStyle.Width, "auto");
            gvProducts.HeaderRow.Cells[Index].Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
            gvProducts.HeaderRow.Cells[Index].Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            //lblHeader.Style.Add(HtmlTextWriterStyle.Width, "auto");
            //lblHeader.Style.Add(HtmlTextWriterStyle.OverflowX, "visible");
            //lblHeader.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
        }
        else
        {
            //img.ImageUrl = "~/images/plus.png";
            //img.ToolTip = "Expand";
            gvProducts.Columns[Index].ItemStyle.CssClass = "Shorter";
            gvProducts.Columns[Index].ItemStyle.Width = Unit.Pixel(0);
            gvProducts.Columns[Index].HeaderStyle.Width = Unit.Pixel(0);
            gvProducts.Columns[Index].ControlStyle.Width = Unit.Pixel(0);

            for (int i = 0; i < gvProducts.Rows.Count; i++)
            {
                lbl = (Label)gvProducts.Rows[i].FindControl(lblID);
                lbl.Style.Add(HtmlTextWriterStyle.Width, "0px");
                lbl.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
                lbl.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            }
            gvProducts.HeaderRow.Cells[Index].Style.Add(HtmlTextWriterStyle.Width, "0px");
            gvProducts.HeaderRow.Cells[Index].Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
            //lblHeader.Style.Add(HtmlTextWriterStyle.Width, "0px");
            //lblHeader.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
        }
    }
    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        ApplySort();
    }

    protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplySort();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplySort();
    }
    private void ApplySort()
    {
        try
        {
            gCurrentPage = 1;
            string SortOrder = "";
            string SortField = "";
            SortOrder = ViewState["SortDirection"].ToString();
            SortField = ViewState["SortField"].ToString();
            //string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
            //string SortField = "Product";
            //if (ddlSortBy.SelectedValue.Contains("Prod"))
            //{
            //    SortField = "Product";
            //}
            //else if (ddlSortBy.SelectedValue.Contains("Mcft"))
            //{
            //    SortField = "Manufacturer";
            //}
            //else if (ddlSortBy.SelectedValue.Contains("Price"))
            //{
            //    SortField = "Price";
            //}
            hdnInStock.Value = hdnInStock.Value == "" ? "false" : hdnInStock.Value;
            hdnPricingAvailable.Value = hdnPricingAvailable.Value == "" ? "false" : hdnPricingAvailable.Value;
            GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), Convert.ToString(hdnPriceRange.Value), gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key", "ToggleAll_1();", true);
        }
        catch (Exception ex)
        { }
    }

    #region TopPaging
    private void PopulatePager(int recordCount, int currentPage, int size)
    {
        int PageSize = size;
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }
        //rptPager.DataSource = pages;
        //rptPager.DataBind();
        rptPagerBottom.DataSource = pages;
        rptPagerBottom.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        //string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
        //string SortField = "Product";
        //if (ddlSortBy.SelectedValue.Contains("Prod"))
        //{
        //    SortField = "Product";
        //}
        //else if (ddlSortBy.SelectedValue.Contains("Mcft"))
        //{
        //    SortField = "Manufacturer";
        //}
        //else if (ddlSortBy.SelectedValue.Contains("Price"))
        //{
        //    SortField = "Price";
        //}
        if (ViewState["SortDirection"] == null)
            ViewState["SortDirection"] = "ASC";

        if (ViewState["SortField"] == null)
            ViewState["SortField"] = "Product";
        string SortOrder = ViewState["SortDirection"].ToString();
        string SortField = ViewState["SortField"].ToString();

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gCurrentPage = pageIndex;
        hdnInStock.Value = hdnInStock.Value == "" ? "false" : hdnInStock.Value;
        hdnPricingAvailable.Value = hdnPricingAvailable.Value == "" ? "false" : hdnPricingAvailable.Value;
        GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), Convert.ToString(hdnPriceRange.Value), pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key", "ToggleAll_1();", true);
    }
    #endregion

}

class GridColumn : ITemplate
{
    private DataControlRowType _type;
    private string _colName;

    public GridColumn(DataControlRowType type, string colname)
    {
        _type = type;
        _colName = colname;
    }
    public void InstantiateIn(System.Web.UI.Control container)
    {

        switch (_type)
        {
            case DataControlRowType.Header:
                // Create the controls to put in the header section and set their properties.
                //Literal lc = new Literal();
                //lc.Text = "<B>" + _colName + "</B>";
                //Image img = new Image();
                //img.ID = "ibtnExpand";
                //img.

                Label lbl = new Label();
                lbl.ID = "label1";
                lbl.Text = _colName;
                lbl.Attributes.Add("style", "width: 0px; overflow-x: hidden; white-space: nowrap;");

                // Add the controls to the Controls collection of the container.
                container.Controls.Add(lbl);
                break;
            case DataControlRowType.DataRow:
                // Create the controls to put in a data row section and set their properties.
                Label lbl1 = new Label();
                lbl1.Attributes.Add("style", "width: 0px; overflow: hidden; white-space: nowrap");
                lbl1.DataBinding += new EventHandler(this.lbl1_DataBinding);
                container.Controls.Add(lbl1);

                //// To support data binding, register the event-handling methods
                //// to perform the data binding. Each control needs its own event handler.
                //firstName.DataBinding += new EventHandler(this.FirstName_DataBinding);
                //lastName.DataBinding += new EventHandler(this.LastName_DataBinding);

                //// Add the controls to the Controls collection of the container.
                //container.Controls.Add(firstName);
                //container.Controls.Add(spacer);
                //container.Controls.Add(lastName);
                break;

            // Insert cases to create the content for the other row types, if desired.

            default:
                // Insert code to handle unexpected values.
                break;

        }

    }
    private void lbl1_DataBinding(Object sender, EventArgs e)
    {
        // Get the Label control to bind the value. The Label control
        // is contained in the object that raised the DataBinding event (the sender parameter).
        Label l = (Label)sender;

        // Get the GridViewRow object that contains the Label control. 
        GridViewRow row = (GridViewRow)l.NamingContainer;

        // Get the field value from the GridViewRow object and assign it to the Text property of the Label control.

        //if (DataBinder.Eval(row.DataItem, "AttributeInfo").ToString() != "")
        //{
        string[] arr = DataBinder.Eval(row.DataItem, "AttributeInfo").ToString().Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            string[] _arr = arr[i].Split('|');
            if (_arr.Length > 0)
            {
                if (Array.IndexOf(_arr, _colName) > -1)
                {
                    l.Text = _arr[1].ToString();
                    break;
                }
                else
                    l.Text = "NA";
            }
        }
        //}
    }
}