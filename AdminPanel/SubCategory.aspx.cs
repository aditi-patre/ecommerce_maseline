using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class AdminPanel_SubCategory : System.Web.UI.Page
{
    int PageSize = 10;
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
                BindSubCategory(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
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

        dt = new DataTable();
        Category objC = new Category();
        dt = objC.CategoriesList();
        DataRow dr = dt.NewRow();
        dr["Name"] = "Select";
        dr["CategoryID"] = "0";
        dr["ShortCode"] = "";
        dt.Rows.InsertAt(dr, 0);

        var dtQuery = from cat in dt.AsEnumerable()
                      select new
                      {
                          Name = cat.Field<string>("Name"),
                          Category = Convert.ToString(cat.Field<int>("CategoryID")) + "|" + cat.Field<string>("ShortCode")
                      };



        ddlCategoryID.DataSource = dtQuery;
        ddlCategoryID.DataTextField = "Name";
        ddlCategoryID.DataValueField = "Category";
        ddlCategoryID.DataBind();
        ddlCategoryID.Items.FindByText("Select").Selected = true;
    }
    private void BindSubCategory(int Pgindex, int PageSize, string SortField, string SortOrder)
    {
        int TR;
        SubCategory obj = new SubCategory();
        obj.Name = txtSearchSubCategory.Value;
        DataTable dtM = obj.GetList(Pgindex, PageSize, SortField, SortOrder, out TR);
        gvSubCategory.DataSource = dtM;
        gvSubCategory.DataBind();
        PopulatePager(TR, Pgindex, PageSize);
    }

    protected void gvSubCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvSubCategory.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvSubCategory.RowStyle.CssClass = "ListingRowStyle";

            Label lblCat_Code = (Label)e.Row.FindControl("lblCat_Code");

            ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
            btnEdit.Attributes.Add("OnClick", "return SubCategoryEdit('" + gvSubCategory.DataKeys[e.Row.RowIndex].Value + "','" + e.Row.RowIndex + "','" + btnEdit.CommandArgument + "|" + lblCat_Code.Text + "')");
        }
    }

    protected void gvSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteM")
        {
            SubCategory objC = new SubCategory(Convert.ToInt32(e.CommandArgument));
            objC.Delete();

            ApplySort();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Sub-Category has been deleted successfully.');", true);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindSubCategory(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, "", "");
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
        string SortField = "Name";
        if (ddlSortBy.SelectedValue.Contains("SubCat"))
        {
            SortField = "s.Name";
        }
        else if (ddlSortBy.SelectedValue.Contains("CatCode"))
        {
            SortField = "S_ShortCode";
        }
        else if (ddlSortBy.SelectedValue.Contains("Cat"))
        {
            SortField = "c.Name";
        }
        BindSubCategory(pageIndex, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
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
            if (ddlSortBy.SelectedValue.Contains("SubCat"))
            {
                SortField = "s.Name";
            }
            else if (ddlSortBy.SelectedValue.Contains("CatCode"))
            {
                SortField = "S_ShortCode";
            }
            else if (ddlSortBy.SelectedValue.Contains("Cat"))
            {
                SortField = "c.Name";
            }
            BindSubCategory(CurrentPageNo, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, SortOrder);
        }
        catch (Exception ex)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SubCategory objM = new SubCategory();
            objM.Name = txtSearchSubCategory.Value;
            int TR;

            string SortField = "Name";
            if (ddlSortBy.SelectedValue.Contains("SubCat"))
                SortField = "s.Name";
            else if (ddlSortBy.SelectedValue.Contains("CatCode"))
                SortField = "S_ShortCode";
            else if (ddlSortBy.SelectedValue.Contains("Cat"))
                SortField = "c.Name";

            DataTable dt = objM.GetList(1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1, SortField, ddlSortBy.SelectedValue.Contains("A_Z") ? "ASC" : "DESC", out TR);
            if (dt != null)
            {
                gvSubCategory.DataSource = dt;
                gvSubCategory.DataBind();
            }
            PopulatePager(TR, 1, Convert.ToInt32(ddlPageSize.SelectedValue) + 1);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearchSubCategory.Value = "";
        ddlSortBy.SelectedValue = "SubCatA_Z";
        ddlPageSize.SelectedValue = "9";
        ApplySort();
    }
}