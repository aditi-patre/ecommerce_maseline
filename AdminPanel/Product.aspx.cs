using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Product : System.Web.UI.Page
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
        int RoleID = 0;
        if (HttpContext.Current.Session["User"] != null)
        {
            RoleID = Convert.ToInt32(Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[2]);
        }

        if (RoleID == 0)
            Response.Redirect("~/AdminPanel/AdminLogin.aspx", true);
        else
        {
            if (!IsPostBack)
            {
                PopulateSearchCriteria();
                if (gCurrentPage <= 1)
                    gCurrentPage = 1;

                if (ViewState["SortDirection"] == null)
                    ViewState["SortDirection"] = "ASC";

                if (ViewState["SortField"] == null)
                    ViewState["SortField"] = "Product";

                BindProduct(gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, ViewState["SortField"].ToString(), ViewState["SortDirection"].ToString());

                if (hdnHideCols.Value == "" || hdnHideCols.Value == null)
                    hdnHideCols.Value = "true";
            }
        }
    }

    #region GridEvents
    protected void gvProducts_DataBound(object sender, GridViewRowEventArgs e)
    {
        //** For gray borders to gridview, for ff & ie
        foreach (TableCell tc in e.Row.Cells)
        {
            tc.Attributes["style"] = "border:1px solid #CCCCCC";
            //if(tc.Controls[0].GetType().Name == "ImageButton")
            //{
            //    tc.Attributes["style"] = "width:30px; height:30px";
            //}
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvProducts.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvProducts.RowStyle.CssClass = "ListingRowStyle";
            try
            {
                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblSubCategoryID = (Label)e.Row.FindControl("lblSubCategoryID");

                //ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                //btnEdit.Attributes.Add("OnClick", "return ProductEdit('" + gvProducts.DataKeys[e.Row.RowIndex].Value + "','" + e.Row.RowIndex + "','" + lblCategoryID.Text + "','" + lblSubCategoryID.Text + "')");

                /* Map the path of product image*/
                Image img = (Image)e.Row.FindControl("imgProduct");
                Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");
                string ProdImagePath = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "//" + lblImagePath.Text;
                if (File.Exists(Server.MapPath(ProdImagePath)))
                    img.ImageUrl = ProdImagePath;
                else
                    img.ImageUrl = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//Not_available.jpg";//img.Style.Add(HtmlTextWriterStyle.Display, "none");
                //****

                ImageButton ibtnDel = (ImageButton)e.Row.FindControl("ibtnDelete");
                if (ibtnDel.ImageUrl.IndexOf("delete_icon") != -1)
                    ibtnDel.Width = Unit.Pixel(30);
                ibtnDel.Height = Unit.Pixel(30);


                //**Clicking on image, product name redirects to product description page
                img.Attributes.Add("onclick", "location='ProductDetails.aspx?PCode=" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "'");
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                lblProductCode.Attributes.Add("onclick", "location='ProductDetails.aspx?PCode=" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "'");
                //****

                Literal ltQty = (Literal)e.Row.FindControl("ltQty");
                Literal ltPricing = (Literal)e.Row.FindControl("ltPricing");

                //when pricing as per qty NA                
                Label lblPrice = (Label)e.Row.FindControl("lblPrice"); //The price of product
                Label lblPricing = (Label)e.Row.FindControl("lblPricing"); //1|9|6.25,10|24|5.25
                //[-]
                //System.Web.UI.HtmlControls.HtmlButton btnAddToCart = (System.Web.UI.HtmlControls.HtmlButton)e.Row.FindControl("btnAddToCart");
                //ImageButton btnAddToCart = (ImageButton)e.Row.FindControl("btnAddToCart");
                //if (Convert.ToInt32(lblInventory.Text) <= 0)
                //    btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "block");
                //else
                //{
                //    btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "none");
                //    IsAvail = true;
                //}
                //if (lblInventory.Text == "0")
                //    lblInventory.Text = "Unavailable";

                if (lblPricing.Text == "") //Multiple prices not defined
                {
                    lblPrice.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ltPricing.Visible = false;
                    if (Convert.ToDouble(lblPrice.Text) <= 0)
                    {
                        ltQty.Text = "-";
                        lblPrice.Text = "";//"Requires Quote";
                        //btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "block");
                    }
                    else
                    {

                        ltQty.Text = "1 and Up";
                        //btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                }
                else //display pricing as per quantity table
                {
                    //btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "none");
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
                                    Qty = Qty == "" ? arrQtyPrice[j] : Qty + " and Up";
                            }
                        }
                        sbQty.Append("</tr>"); sbPrice.Append("</tr>");
                    }
                    sbQty.Append("</table>"); sbPrice.Append("</table>");
                    ltQty.Text = sbQty.ToString();
                    ltPricing.Text = sbPrice.ToString();

                }

            }
            catch (Exception ex)
            {
            }
        }
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
                                        img.ImageUrl = "..//images//" + (ViewState["SortDirection"].ToString() == "ASC" ? "up_arrow" : "down_arrow") + ".png";
                                        tc.Controls.Add(new LiteralControl(" "));
                                        tc.Controls.Add(img);
                                    }
                                    //else
                                    //{
                                    //    img.ImageUrl = "..//images//Sort.png";
                                    //    tc.Controls.Add(new LiteralControl(" "));
                                    //    tc.Controls.Add(img);
                                    //}
                                }
                            }
                        }
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
            BindProduct(gCurrentPage, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);

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
        if (e.CommandName == "DeleteM")
        {
            Product objP = new Product(Convert.ToInt32(e.CommandArgument));
            objP.Delete();

            ApplySort();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Product has been deleted successfully.');", true);
        }
        else if (e.CommandName == "EditM")
        {
            Response.Redirect("ProductDetails.aspx?PID=" + e.CommandArgument.ToString());
        }
    }

    #endregion
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
    private void ApplySort()
    {
        try
        {
            int CurrentPageNo = 1;
            string SortOrder = "";
            string SortField = "";
            SortOrder = ViewState["SortDirection"].ToString();
            SortField = ViewState["SortField"].ToString();
            BindProduct(CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key", "ToggleAll_1();", true);
        }
        catch (Exception ex)
        { }
    }
    private void BindProduct(int Pgindex, int PageSize, string SortField, string SortOrder)
    {
        int TR;
        Product obj = new Product();
        obj.Name = txtSearchProduct.Value;
        DataTable dt = obj.GetList("", "", "", "", false, false, "", Pgindex, PageSize, SortField, SortOrder, out TR).Tables[0];
        if (dt != null)
        {
            for (int j = 0; j < dt.Rows.Count; j++) //For every row
            {
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
        }
        gvProducts.DataSource = dt;
        gvProducts.DataBind();
        gCurrentPage = Pgindex;
        gTotalRecords = TR;
        PopulatePager(TR, Pgindex, PageSize);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindProduct(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
    }

    #region ExpandCollapse
    protected void btnExpand_Click(object sender, EventArgs e)
    {
        try
        {
            Label lblHeader = new Label();
            Image img = (Image)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[1];
            Label lbl = new Label();
            string lblID = "";
            if (gvProducts.Rows[0].FindControl("lblDescrip") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 9) //7
            {
                lblID = "lblDescrip";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Description";
            }
            else if (gvProducts.Rows[0].FindControl("lblTechnology") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 10) //8
            {
                lblID = "lblTechnology";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Technology";
            }
            else if (gvProducts.Rows[0].FindControl("lblHarmonizedCode") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 11)
            {
                lblID = "lblHarmonizedCode";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Harmonized Code";
            }
            else if (gvProducts.Rows[0].FindControl("lblCategory") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 12)
            {
                lblID = "lblCategory";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Category";
            }
            else if (gvProducts.Rows[0].FindControl("lblSubCategory") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 13)
            {
                lblID = "lblSubCategory";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Sub-Category";
            }
            else if (gvProducts.Rows[0].FindControl("lblCapacitance") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 14)
            {
                lblID = "lblCapacitance";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Capacitance";
            }
            else if (gvProducts.Rows[0].FindControl("lblVoltage") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 15)
            {
                lblID = "lblVoltage";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Voltage";
            }
            else if (gvProducts.Rows[0].FindControl("lblMaterial") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 16)
            {
                lblID = "lblMaterial";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Material";
            }
            else if (gvProducts.Rows[0].FindControl("lblStyle") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 17)
            {
                lblID = "lblStyle";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Style";
            }
            else if (gvProducts.Rows[0].FindControl("lblTolerance") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 18) //8
            {
                lblID = "lblTolerance";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Tolerance";
            }
            else if (gvProducts.Rows[0].FindControl("lblTemperature") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 19)
            {
                lblID = "lblTemperature";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Temperature";
            }
            else if (gvProducts.Rows[0].FindControl("lblConstruction") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 20)
            {
                lblID = "lblConstruction";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Construction";
            }
            else if (gvProducts.Rows[0].FindControl("lblFeatures") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 21)
            {
                lblID = "lblFeatures";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Features";
            }
            else if (gvProducts.Rows[0].FindControl("lblWattage") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 22)
            {
                lblID = "lblWattage";
                lblHeader = (Label)gvProducts.HeaderRow.Cells[Convert.ToInt32(hdnColNoImg.Value.Split('|')[0])].Controls[3];
                lblHeader.Text = "Wattage";
            }
            else if (gvProducts.Rows[0].FindControl("lblResistance") != null && Convert.ToInt32(hdnColNoImg.Value.Split('|')[0]) == 23)
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
            string lblID = "";

            lblID = "lblDescrip";
            ShowHide(9, lblID);

            lblID = "lblTechnology";
            ShowHide(10, lblID);

            lblID = "lblHarmonizedCode";
            ShowHide(11, lblID);

            lblID = "lblCategory";
            ShowHide(12, lblID);

            lblID = "lblSubCategory";
            ShowHide(13, lblID);

            lblID = "lblCapacitance";
            ShowHide(14, lblID);

            lblID = "lblVoltage";
            ShowHide(15, lblID);

            lblID = "lblMaterial";
            ShowHide(16, lblID);

            lblID = "lblStyle";
            ShowHide(17, lblID);

            lblID = "lblTolerance";
            ShowHide(18, lblID);

            lblID = "lblTemperature";
            ShowHide(19, lblID);

            lblID = "lblConstruction";
            ShowHide(20, lblID);

            lblID = "lblFeatures";
            ShowHide(21, lblID);

            lblID = "lblWattage";
            ShowHide(22, lblID);

            lblID = "lblResistance";
            ShowHide(23, lblID);


            string Toggle_All = false.ToString();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key", "SetToggle_All('" + Toggle_All + "');", true);

            if (hdnHideCols.Value.ToLower() == "true")
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key1", "HideCols();", true);

            Image ibtnShowAttributes = (Image)gvProducts.HeaderRow.Cells[8].Controls[1];
            if (ibtnShowAttributes.ImageUrl.IndexOf("show_attributes") != -1)//expanded
                ibtnShowAttributes.ImageUrl = "../images/hide_attributes.png";
            else
                ibtnShowAttributes.ImageUrl = "../images/show_attributes.png";
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowHide(int Index, string lblID)
    {
        Label lbl;
        if (!hdnColImg.Value.Contains("plus"))
        {
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
        }
        else
        {
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
        }
    }
    #endregion

    #region Search
    private void PopulateSearchCriteria()
    {
        DataTable dt = new DataTable();
        
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int CurrentPageNo = 1;
            Product objM = new Product();
            objM.Name = txtSearchProduct.Value;
            int TR;

            string SortOrder = "";
            string SortField = "";
            SortOrder = Convert.ToString(ViewState["SortDirection"]) == "" ? "ASC" : Convert.ToString(ViewState["SortDirection"]);
            SortField = Convert.ToString(ViewState["SortField"]) == "" ? "Product" : Convert.ToString(ViewState["SortField"]);

            DataTable dt = new DataTable();

            dt = objM.GetList("", "", "", "", false, false, "", CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder, out TR).Tables[0];
            if (dt != null)
            {
                for (int j = 0; j < dt.Rows.Count; j++) //For every row
                {
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
                gCurrentPage = CurrentPageNo;
                gTotalRecords = TR;
            }
            PopulatePager(TR, 1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearchProduct.Value = "";
        ddlPageSize.SelectedValue = "9";
        ViewState["SortDirection"] = "ASC";
        ViewState["SortField"] = "Product";
        ApplySort();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductDetails.aspx");
    }
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

        rptPagerBottom.DataSource = pages;
        rptPagerBottom.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        if (ViewState["SortDirection"] == null)
            ViewState["SortDirection"] = "ASC";

        if (ViewState["SortField"] == null)
            ViewState["SortField"] = "Product";
        string SortOrder = ViewState["SortDirection"].ToString();
        string SortField = ViewState["SortField"].ToString();

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gCurrentPage = pageIndex;
        BindProduct(pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "key", "ToggleAll_1();", true);

    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplySort();
    }
    #endregion


}