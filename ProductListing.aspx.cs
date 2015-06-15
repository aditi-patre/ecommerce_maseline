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
    int gCurrentPage, gTotalRecords;
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

            //if (!isListing)
            //    ltCatalogue.Text = GetCategories(Category, SubCategory);
            //else
            //if (isListing)
            //{
            //            GetProducts("", "", "", "", false, false);
            PopulateSearchCriteria();

            if (Request.QueryString.ToString() == "")
                GetProducts("", "", "", "", false, false, "", 1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
            else
                GetProducts(Category, SubCategory, "", "", false, false, "", 1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
            //}

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
        dt = objM.GetList();
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
            sb.Append("<tr><td style=\"padding-bottom:15px;\">" + dt.Rows[i]["Name"] + ": </td><td style=\"padding-bottom:15px; padding-left:15px;\"><input type=\"text\" name='" + dt.Rows[i]["AttributeID"] + "' id='" + dt.Rows[i]["AttributeID"] + "' runat=\"server\" style=\" width:80px;\" /><br/></td></tr>");
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
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
                gCurrentPage = CurrentPageNo;
                gTotalRecords = TotalRecords;
                //BindPagingList();
                PopulatePager(TotalRecords, gCurrentPage, PageSize);
            }
        }
        catch (Exception ex)
        { }
    }
    protected void gvProducts_DataBound(object sender, GridViewRowEventArgs e)
    {
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
                        lblPrice.Text = "Requires Quote";
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
                    //lblInventory.Style.Add(HtmlTextWriterStyle.Display, "none");
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
    protected void gvProducts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                //e.Row.Attributes.Add("onmouseover", "this.className='Row'");
                //e.Row.Attributes.Add("onmouseout", "this.className='alt'");
            }
            catch (Exception ex)
            {

            }
        }
        //else if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    foreach (TableCell tc in e.Row.Cells)
        //    {
        //        if (tc.HasControls())
        //        {
        //            Table lnk = (Table)tc.Controls[0];
        //            //System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
        //            System.Web.UI.WebControls.Button btn = new System.Web.UI.WebControls.Button();
        //            btn.Text = "[+]";
        //            tc.Controls.Add(btn);
        //        }
        //    }
        //}
    }

    protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int ProductID = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "ShowProductDetails")
        {
            Response.Redirect("ProductDetails.aspx?PCode=" + ProductID);
        }
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

    protected void btnExpand_Click(object sender, EventArgs e)
    {
        try
        {
            Label lblHeader = new Label();
            Image img = (Image)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[1];
            Label lbl = new Label();
            string lblID = "";
            if (gvProducts.Rows[0].FindControl("lblDescrip") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 6)
            {
                lblID = "lblDescrip";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Description";
            }
            else if (gvProducts.Rows[0].FindControl("lblTechnology") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 7)
            {
                lblID = "lblTechnology";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Technology";
            }
            else if (gvProducts.Rows[0].FindControl("lblHarmonizedCode") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 8)
            {
                lblID = "lblHarmonizedCode";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Harmonized Code";
            }
            else if (gvProducts.Rows[0].FindControl("lblCategory") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 9)
            {
                lblID = "lblCategory";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Category";
            }
            else if (gvProducts.Rows[0].FindControl("lblSubCategory") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 10)
            {
                lblID = "lblSubCategory";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Sub-Category";
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
            }
        }
        catch (Exception ex)

        { }
    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        //int CurrentPageNo = 1;
        ////ltCatalogue.Text = "";
        //string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
        //string SortField ="Product";
        //if(ddlSortBy.SelectedValue.Contains("Prod"))
        //{
        //    SortField ="Product";
        //}
        //else if(ddlSortBy.SelectedValue.Contains("Mcft"))
        //{
        //    SortField="Manufacturer";
        //}
        //else if(ddlSortBy.SelectedValue.Contains("Price"))
        //{
        //    SortField ="Price";
        //}
        //GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), Convert.ToString(hdnPriceRange.Value), CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue)+1,SortField,SortOrder);
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
            int CurrentPageNo = 1;
            string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
            string SortField = "Product";
            if (ddlSortBy.SelectedValue.Contains("Prod"))
            {
                SortField = "Product";
            }
            else if (ddlSortBy.SelectedValue.Contains("Mcft"))
            {
                SortField = "Manufacturer";
            }
            else if (ddlSortBy.SelectedValue.Contains("Price"))
            {
                SortField = "Price";
            }
            hdnInStock.Value = hdnInStock.Value == "" ? "false" : hdnInStock.Value;
            hdnPricingAvailable.Value = hdnPricingAvailable.Value == "" ? "false" : hdnPricingAvailable.Value;
            GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), Convert.ToString(hdnPriceRange.Value), CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        }
        catch (Exception ex)
        { }
    }

    #region ForPaging
    /*
    #region Properties
    public string btnNextClientID;
    public string lblErrorClientID;

    //Following properties are for page indexing purpose

    private int CurrentPage
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
    private int fistIndex
    {
        get
        {

            int _FirstIndex = 0;
            if (ViewState["_FirstIndex"] == null)
            {
                _FirstIndex = 0;
            }
            else
            {
                _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
            }
            return _FirstIndex;
        }
        set { ViewState["_FirstIndex"] = value; }
    }
    private int lastIndex
    {
        get
        {

            int _LastIndex = 0;
            if (ViewState["_LastIndex"] == null)
            {
                _LastIndex = 0;
            }
            else
            {
                _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
            }
            return _LastIndex;
        }
        set { ViewState["_LastIndex"] = value; }
    }
    public int TotalPages
    {
        get
        {
            if (ViewState["TotalPages"] == null)
                return 0;
            else
                return Convert.ToInt32(ViewState["TotalPages"].ToString());
        }

        set
        {
            if (ViewState["TotalPages"] == null)
                ViewState.Add("TotalPages", value);
            else
                ViewState["TotalPages"] = value;
        }
    }
    #endregion
    PagedDataSource _PageDataSource = new PagedDataSource();

    public void BindPagingList()
    {
        try
        {
            if (!IsPostBack)
            {
                if (gCurrentPage > 0)
                {
                    CurrentPage = gCurrentPage - 1;
                    int Page1 = (CurrentPage / 10) * 10;
                    fistIndex = Page1;
                }
                else
                {
                    CurrentPage = 0;
                    gCurrentPage = 1;
                }
            }
            _PageDataSource.AllowPaging = true;
            _PageDataSource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _PageDataSource.PageCount;


            //this.lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
            //this.lbtnNext.Enabled = !_PageDataSource.IsLastPage;
            //this.lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
            //this.lbtnLast.Enabled = !_PageDataSource.IsLastPage;
            double d = Convert.ToDouble(gTotalRecords / 10.00);
            gCurrentPage = this.CurrentPage + 1;
            this.TotalPages = Convert.ToInt32(Math.Ceiling(d));
            this.doPaging();
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void lbtnPrevious3_Click(object sender, EventArgs e)
    {
        int Page = (CurrentPage / 3) * 3 - 10;
        if (Page >= 0)
        {
            CurrentPage = Page;
            fistIndex = Page;
            lastIndex = Page + 10;
        }
        else
        {
            CurrentPage = fistIndex = 0;
            lastIndex = 10;
        }

        gCurrentPage = CurrentPage + 1;
        GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), gCurrentPage);

    }

    protected void lbtnNext3_Click(object sender, EventArgs e)
    {
        int Page1 = (CurrentPage / 3) * 3 + 3;
        fistIndex = Page1;
        lastIndex = Page1 + 3;
        CurrentPage = Page1;
        gCurrentPage = CurrentPage + 1;
        GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), gCurrentPage);
    }

    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");

        if (CurrentPage >= 10)
        {
            lastIndex = fistIndex + 10;
        }
        else
        {
            fistIndex = 0;
            lastIndex = 10;
        }
        if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            fistIndex = lastIndex - 10;
        }

        if (fistIndex < 0)
        {
            fistIndex = 0;
        }

        for (int i = fistIndex; i < lastIndex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }
        double d = Convert.ToDouble(gTotalRecords / 10.00);
        int TotalPage =Convert.ToInt32(Math.Ceiling(d));
        this.dlPaging.DataSource = dt;
        this.dlPaging.DataBind();
        //if (dt.Rows.Count < 1 || Convert.ToInt32(ViewState["TotalPages"]) < 2)
        //    tblPaging.Visible = false;
        //else
        //    tblPaging.Visible = true;


        if (this.CurrentPage >= TotalPage - 1)
        {
            //lbtnLast.Enabled = false;
            //lbtnNext.Enabled = false;
            LinkButton1.Enabled = false;
        }
        else
        {
            //lbtnLast.Enabled = true;
            //lbtnNext.Enabled = true;
        }
        if (this.CurrentPage >= ((TotalPage / 3) * 3))
        {
            LinkButton1.Enabled = false;
        }
        else
        {
            LinkButton1.Enabled = true;
        }
        if (this.CurrentPage < 3)
        {
            LinkButton2.Enabled = false;
        }
        else
        {
            LinkButton2.Enabled = true;
        }
    }

    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            gCurrentPage = CurrentPage + 1;
            hdnInStock.Value = hdnInStock.Value == "" ? "false" : hdnInStock.Value;
            hdnPricingAvailable.Value = hdnPricingAvailable.Value == "" ? "false" : hdnPricingAvailable.Value;
            GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), (gCurrentPage));
        }
    }

    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            //lnkbtnPage.Style.Add("fone-size", "14px");
            lnkbtnPage.CssClass = "active";
            lnkbtnPage.Font.Bold = true;
        }
    }
    */
    #endregion

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
        string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
        string SortField = "Product";
        if (ddlSortBy.SelectedValue.Contains("Prod"))
        {
            SortField = "Product";
        }
        else if (ddlSortBy.SelectedValue.Contains("Mcft"))
        {
            SortField = "Manufacturer";
        }
        else if (ddlSortBy.SelectedValue.Contains("Price"))
        {
            SortField = "Price";
        }

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        hdnInStock.Value = hdnInStock.Value == "" ? "false" : hdnInStock.Value;
        hdnPricingAvailable.Value = hdnPricingAvailable.Value == "" ? "false" : hdnPricingAvailable.Value;
        GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value), Convert.ToString(hdnPriceRange.Value), pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
    }
    #endregion

}