using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Manufacturer : System.Web.UI.Page
{
    int PageSize = 10;
    public int ManufactureID;
    //string Search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //((System.Web.UI.HtmlControls.HtmlInputText)(this.Master.FindControl("HeaderSearch"))).Value = "Enter Manufacturer";
        }

        int RoleID = 0;
        if (HttpContext.Current.Session["User"] != null)
        {
            RoleID = Convert.ToInt32(Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[2]);
        }

        if (RoleID == 0)
            Response.Redirect("~/AdminPanel/AdminLogin.aspx", true);
        else
        {
            //if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
            //    Search = Convert.ToString(Request.QueryString["Name"]);
            if (!IsPostBack)
            {
                PopulateSearchCriteria();
                BindManufacturer(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
            }
        }
        ibtnSearch.Focus();
    }

    private void PopulateSearchCriteria()
    {
        DataTable dt = new DataTable();
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
    private void BindManufacturer(int Pgindex, int PageSize, string SortField, string SortOrder)
    {
        int TR;
        Manufacturer obj = new Manufacturer();
        obj.Name = txtSearchManufacturer.Value;
        DataTable dtM = obj.GetList(Pgindex, PageSize, SortField, SortOrder, out TR);
        gvManufacturer.DataSource = dtM;
        gvManufacturer.DataBind();
        PopulatePager(TR, Pgindex, PageSize);
    }

    protected void gvManufacturer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvManufacturer.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvManufacturer.RowStyle.CssClass = "ListingRowStyle";

            ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
            btnEdit.Attributes.Add("OnClick", "return ManufacturerEdit('" + gvManufacturer.DataKeys[e.Row.RowIndex].Value + "','" + e.Row.RowIndex + "')");
        }
    }

    protected void gvManufacturer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteM")
        {
            Manufacturer objM = new Manufacturer(Convert.ToInt32(e.CommandArgument));
            objM.Delete();

            ApplySort();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Manufacturer has been deleted successfully.');", true);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindManufacturer(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    Manufacturer objM = new Manufacturer();
    //    //objM.Name = txtSearch.Value;
    //    gvManufacturer.DataSource = objM.GetList(1,10,)
    //    gvManufacturer.DataBind();
    //}
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
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
        string SortField = "Name";
        //if (ddlSortBy.SelectedValue.Contains("McftCode"))
        //{
        //    SortField = "Code";
        //}else
        if (ddlSortBy.SelectedValue.Contains("Mcft"))
        {
            SortField = "Name";
        }
        BindManufacturer(pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
    }
    #endregion

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
            string SortField = "Name";
            //if (ddlSortBy.SelectedValue.Contains("McftCode"))
            //{
            //    SortField = "Code";
            //}else
            if (ddlSortBy.SelectedValue.Contains("Mcft"))
            {
                SortField = "Name";
            }
            BindManufacturer(CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        }
        catch (Exception ex)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Manufacturer objM = new Manufacturer();
            objM.Name = txtSearchManufacturer.Value;
            int TR;
            string SortOrder = ddlSortBy.SelectedValue.Contains("Z_A") ? "DESC" : "ASC";
            string SortField = "Name";
            //if (ddlSortBy.SelectedValue.Contains("McftCode"))
            //{
            //    SortField = "Code";
            //}else
            if (ddlSortBy.SelectedValue.Contains("Mcft"))
            {
                SortField = "Name";
            }
            DataTable dt = objM.GetList(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder, out TR);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvManufacturer.DataSource = dt;
                gvManufacturer.DataBind();                
            }
            PopulatePager(TR, 1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1);
        }
        catch(Exception ex)
        {

        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearchManufacturer.Value = "";
        ddlSortBy.SelectedValue = "McftA_Z";
        ddlPageSize.SelectedValue = "9";
        ApplySort();
    }
    //protected void btnSave1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string Msg = "The manufacturer details have been ";
    //        Manufacturer objManufacturer = new Manufacturer(Convert.ToInt32(hdnManufacturerId.Value));
    //        objManufacturer.ManufacturerCode = txtManufacturerCode.Text;
    //        objManufacturer.Name = txtManufacturerName.Text;
    //        objManufacturer.Save();
    //        BindManufacturer();
    //        if (Convert.ToInt32(hdnManufacturerId.Value) > 0)
    //            Msg += "updated successfully";
    //        else
    //            Msg += " saved successfully";
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('" + Msg + "');", true);
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
}