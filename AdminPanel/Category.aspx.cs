using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Category : System.Web.UI.Page
{
    int PageSize = 10;
    public int ManufactureID;
    string Search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //((System.Web.UI.HtmlControls.HtmlInputText)(this.Master.FindControl("HeaderSearch"))).Value = "Enter Manufacturer";
        //}

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
                BindCategory(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
            }
        }
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
    private void BindCategory(int Pgindex, int PageSize, string SortField, string SortOrder)
    {
        int TR;
        Category obj = new Category();
        obj.Name = Search;
        DataTable dtM = obj.CategoriesList(Pgindex, PageSize, SortField, SortOrder, out TR);
        gvCategory.DataSource = dtM;
        gvCategory.DataBind();
        PopulatePager(TR, Pgindex, PageSize);
    }

    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvCategory.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvCategory.RowStyle.CssClass = "ListingRowStyle";

            ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
            btnEdit.Attributes.Add("OnClick", "return CategoryEdit('" + gvCategory.DataKeys[e.Row.RowIndex].Value + "','" + e.Row.RowIndex + "')");
        }
    }

    protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteM")
        {
            Category objC = new Category(Convert.ToInt32(e.CommandArgument));
            objC.Delete();

            ApplySort();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Category has been deleted successfully.');", true);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindCategory(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    Category objM = new Category();
    //    //objM.Name = txtSearch.Value;
    //    gvCategory.DataSource = objM.GetList(1,10,)
    //    gvCategory.DataBind();
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
        if (ddlSortBy.SelectedValue.Contains("CatCode"))
        {
            SortField = "ShortCode";
        }
        else if (ddlSortBy.SelectedValue.Contains("Cat"))
        {
            SortField = "Name";
        }
        BindCategory(pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
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
            if (ddlSortBy.SelectedValue.Contains("CatCode"))
            {
                SortField = "ShortCode";
            }
            else if (ddlSortBy.SelectedValue.Contains("Cat"))
            {
                SortField = "Name";
            }
            BindCategory(CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        }
        catch (Exception ex)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Category objM = new Category();
            objM.Name = txtSearchCategory.Value;
            int TR;
            string SortField = "Name";
            if (ddlSortBy.SelectedValue.Contains("CatCode"))
                SortField = "ShortCode";
            else if (ddlSortBy.SelectedValue.Contains("Cat"))
                SortField = "Name";

            DataTable dt = objM.CategoriesList(1, 10, SortField, ddlSortBy.SelectedValue.Contains("A_Z") ? "ASC" : "DESC", out TR);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvCategory.DataSource = dt;
                gvCategory.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
}