using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Prod : System.Web.UI.Page
{
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
                BindProduct(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
            }
        }
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
                string ProdImagePath = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "//" + lblImagePath.Text;
                if (File.Exists(Server.MapPath(ProdImagePath)))
                    img.ImageUrl = ProdImagePath;
                else
                    img.ImageUrl = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//Not_available.jpg";//img.Style.Add(HtmlTextWriterStyle.Display, "none");
                //****

                //**Clicking on image, product name redirects to product description page
                //img.Attributes.Add("onclick", "location='ProductDetails.aspx?PCode=" + gvProducts.DataKeys[e.Row.RowIndex].Value.ToString() + "'");
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                lblProductCode.Attributes.Add("onclick", "return ProductEdit('" + gvProducts.DataKeys[e.Row.RowIndex][0].ToString() + "','" + e.Row.RowIndex + "','" + gvProducts.DataKeys[e.Row.RowIndex][1].ToString() + "','" + gvProducts.DataKeys[e.Row.RowIndex][2].ToString() + "')");
                img.Attributes.Add("OnClick", "return ProductEdit('" + gvProducts.DataKeys[e.Row.RowIndex][0].ToString() + "','" + e.Row.RowIndex + "','" + gvProducts.DataKeys[e.Row.RowIndex][1].ToString() + "','" + gvProducts.DataKeys[e.Row.RowIndex][2].ToString() + "')");

                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.Attributes.Add("OnClick", "return ProductEdit('" + gvProducts.DataKeys[e.Row.RowIndex][0].ToString() + "','" + e.Row.RowIndex + "','" + gvProducts.DataKeys[e.Row.RowIndex][1].ToString() + "','" + gvProducts.DataKeys[e.Row.RowIndex][2].ToString() + "')");
                //****


                bool IsPriceAvail = false;
                Literal ltQty = (Literal)e.Row.FindControl("ltQty");
                Literal ltPricing = (Literal)e.Row.FindControl("ltPricing");

                //when pricing as per qty NA                
                Label lblPrice = (Label)e.Row.FindControl("lblPrice"); //The price of product
                Label lblPricing = (Label)e.Row.FindControl("lblPricing"); //1|9|6.25,10|24|5.25
                //[-]



                if (lblPricing.Text == "") //Multiple prices not defined
                {
                    lblPrice.Style.Add(HtmlTextWriterStyle.Display, "block");
                    ltPricing.Visible = false;
                    if (Convert.ToDouble(lblPrice.Text) <= 0)
                    {
                        ltQty.Text = "-";
                        lblPrice.Text = "-";
                    }
                    else
                    {
                        IsPriceAvail = true;
                        ltQty.Text = "1 and Up";
                    }
                }
                else //display pricing as per quantity table
                {
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
            }
            catch (Exception ex)
            {
            }
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
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindProduct(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
    }
    private void PopulateSearchCriteria()
    {
        DataTable dt = new DataTable();
        Manufacturer objM = new Manufacturer();
        int TR;
        dt = objM.GetList(1, 101, "", "", out TR);
        ddlManufacturer.DataSource = dt;
        ddlManufacturer.DataTextField = "Name";
        ddlManufacturer.DataValueField = "ManufacturerID";
        ddlManufacturer.DataBind();

        Category objC = new Category();
        dt = objC.CategoriesList();
        ddlCategoryID.DataSource = dt;
        ddlCategoryID.DataTextField = "Name";
        ddlCategoryID.DataValueField = "CategoryID";//"ShortCode";
        ddlCategoryID.DataBind();

        SubCategory objS = new SubCategory();
        dt = objS.GetList();
        ddlSubCategoryID.DataSource = dt;
        ddlSubCategoryID.DataTextField = "Name";
        ddlSubCategoryID.DataValueField = "SubCategoryID"; //"S_ShortCode";
        ddlSubCategoryID.DataBind();


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
    private void BindProduct(int Pgindex, int PageSize, string SortField, string SortOrder)
    {
        int TR;
        Product obj = new Product();
        obj.Name = txtSearchProduct.Value;
        DataTable dtM = obj.GetList("", "", "", "", false, false, "", Pgindex, PageSize, SortField, SortOrder, out TR).Tables[0];
        gvProducts.DataSource = dtM;
        gvProducts.DataBind();
        PopulatePager(TR, Pgindex, PageSize);
    }
    private void ApplySort()
    {
        try
        {
            int CurrentPageNo = 1;
            string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
            string SortField = "Product";
            if (ddlSortBy.SelectedValue.Contains("Name"))
                SortField = "Product";
            else if (ddlSortBy.SelectedValue.Contains("Mcft"))
                SortField = "Manufacturer";
            else if (ddlSortBy.SelectedValue.Contains("Price"))
                SortField = "Price";

            BindProduct(CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        }
        catch (Exception ex)
        { }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int CurrentPageNo = 1;
            Product objM = new Product();
            objM.Name = txtSearchProduct.Value;
            int TR;

            string SortField = "Name";
            if (ddlSortBy.SelectedValue.Contains("Name"))
                SortField = "Product";
            else if (ddlSortBy.SelectedValue.Contains("Mcft"))
                SortField = "Manufacturer";
            else if (ddlSortBy.SelectedValue.Contains("Price"))
                SortField = "Price";

            DataTable dt = new DataTable();

            dt = objM.GetList("", "", "", "", false, false, "", CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, ddlSortBy.SelectedValue.Contains("A_Z") ? "ASC" : "DESC", out TR).Tables[0];
            if (dt != null)
            {
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
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
        ddlSortBy.SelectedValue = "ProdA_Z";
        ddlPageSize.SelectedValue = "9";
        ApplySort();
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

        rptPagerBottom.DataSource = pages;
        rptPagerBottom.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
        string SortField = "Product";
        if (ddlSortBy.SelectedValue.Contains("Name"))
            SortField = "Product";
        else if (ddlSortBy.SelectedValue.Contains("Mcft"))
            SortField = "Manufacturer";
        else if (ddlSortBy.SelectedValue.Contains("Price"))
            SortField = "Price";

        BindProduct(pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
    }

    protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplySort();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplySort();
    }
    #endregion

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


    #region Pricing
    protected void btnRngeAdd_Click(object sender, EventArgs e)
    {
        string[] arrRnge = null;
        int RangeID = 0;
        DataTable dtPricing = new DataTable();
        dtPricing.Columns.Add("RangeID", typeof(int));
        dtPricing.Columns.Add("QtyRange", typeof(string));
        dtPricing.Columns.Add("Price", typeof(string));

        if (gvPricing.Rows.Count > 0)
        {
            dtPricing = (DataTable)gvPricing.DataSource;
            arrRnge = dtPricing.Rows[dtPricing.Rows.Count - 1]["QtyRange"].ToString().Split('-');
            RangeID = Convert.ToInt32(dtPricing.Rows[dtPricing.Rows.Count - 1]["RangeID"]);
        }

        if (arrRnge != null && (arrRnge[1].Trim() == "Up" || Convert.ToInt32(txtRngeFrom1.Text) <= Convert.ToInt32(arrRnge[1].Trim())))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('The current range should be greater than the previously defined range.');", true);
        }
        else if (txtRngeTo1.Text != "" && txtRngeTo1.Text != "Up")
        {
            if (Convert.ToInt32(txtRngeFrom1.Text.Trim()) > Convert.ToInt32(txtRngeTo1.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Invalid range');", true);
            }
        }
        else
        {
            DataRow dr = dtPricing.NewRow();
            dr["RangeID"] = RangeID;
            dr["QtyRange"] = txtRngeFrom1.Text + " - " + txtRngeTo1.Text == "" ? "Up" : txtRngeTo1.Text;
            dr["Price"] = txtPrice.Text;
            dtPricing.Rows.Add(dr);

            gvPricing.DataSource = dtPricing;
            gvPricing.DataBind();
            txtRngeFrom1.Text = "";
            txtRngeTo1.Text = "";
            txtRngePrice.Text = "";
        }
    }

    protected void gvPricing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteP")
        {
            DataTable dtPricing = new DataTable();
            dtPricing.Columns.Add("Range", typeof(int));
            dtPricing.Columns.Add("QtyRange", typeof(string));
            dtPricing.Columns.Add("Price", typeof(string));

            dtPricing = (DataTable)gvPricing.DataSource;
            var id = Convert.ToInt32(e.CommandArgument);
            gvPricing.DeleteRow(id);
        }
    }

    protected void gvPricing_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPricing.EditIndex = e.NewEditIndex;
    }

    protected void gvPricing_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        gvPricing.EditIndex = -1;
    }

    protected void gvPricing_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvPricing.Rows[e.RowIndex];
        TextBox txtQtyRange1 = (TextBox)row.FindControl("txtQtyRange1");
        TextBox txtQtyRange2 = (TextBox)row.FindControl("txtQtyRange2");
        Label lblQtyRange = (Label)row.FindControl("lblQtyRange");
        lblQtyRange.Text = txtQtyRange1.Text + " - " + txtQtyRange2.Text;
    }

    protected void gvPricing_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        TextBox txtQtyRange1 = (TextBox)e.Row.FindControl("txtQtyRange1");
        TextBox txtQtyRange2 = (TextBox)e.Row.FindControl("txtQtyRange2");

        Label lblQtyRange = (Label)e.Row.FindControl("lblQtyRange");
        string[] arrRange = lblQtyRange.Text.Split('-');
        txtQtyRange1.Text = arrRange[0];
        txtQtyRange2.Text = arrRange[1];
    }
    #endregion
}