﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

            if (!isListing)
                ltCatalogue.Text = GetCategories(Category, SubCategory);
            else
            {
                GetProducts("", "", "", "",false,false);
            }
            PopulateSearchCriteria();
        }
    }

    private string GetCategories(string Cat, string SubCat)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<ul id='menu-v1' style='font-size:20px;'>");
        sb.Append("<li><a href='Catalogue.aspx'><span>Catalog</span></a>");

        Category objCat = new Category();
        DataTable dt = objCat.CategoriesList();
        if (dt != null && dt.Rows.Count > 0)
        {
            if (Cat != "")
                dt = dt.Select("shortcode='" + Cat + "'").CopyToDataTable();

            sb.Append("<ul>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["subCategoryCount"]) > 0)// if subcategories are present
                {
                    sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "", false) + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a>");
                    Category objCat2 = new Category(Convert.ToInt32(dt.Rows[i]["categoryID"]));
                    if (objCat2.SubCategories.Count > 0)
                    {
                        sb.Append("<ul>");
                        for (int j = 0; j < objCat2.SubCategories.Count; j++)
                        {
                            if ((SubCat != "" && objCat2.SubCategories[j].ShortCode == SubCat) || SubCat == "") // Redirect to products
                                sb.Append("<li><a href='" + GenerateURL(objCat2.SubCategories[j].C_ShortCode, objCat2.SubCategories[j].ShortCode, true) + "'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");
                            //sb.Append("<li><a href='#' onclick=\'GetProducts('" + objCat2.SubCategories[j].C_ShortCode + "', '" + objCat2.SubCategories[j].ShortCode + "');'><span>" + objCat2.SubCategories[j].Name + "</span></a></li>");

                        }
                        sb.Append("</ul>");
                        if (sb.ToString().LastIndexOf("<ul></ul>") > -1)
                            sb.Remove(sb.ToString().LastIndexOf("<ul></ul>"), ("<ul></ul>").Length);//If no subcategory then remove the ul tags
                        sb.Append("</li>");
                    }
                }
                else //No sub categories
                {
                    sb.Append("<li><a href='" + GenerateURL(Convert.ToString(dt.Rows[i]["shortcode"]), "", true) + "'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
                    //sb.Append("<li><a href='#' onclick='GetProducts('" + Convert.ToString(dt.Rows[i]["shortcode"]) + "', '" + "" + "');'><span>" + Convert.ToString(dt.Rows[i]["name"]) + "</span></a></li>");
                }
            }


            sb.Append("</ul>");
        }
        sb.Append("</li>");
        sb.Append("</ul>");
        return sb.ToString();
    }

    private string GenerateURL(string Category, string SubCategory, bool productListing)
    {
        string url = "";
        if (!String.IsNullOrEmpty(SubCategory))
            url = "Catalogue.aspx?Cat=" + Category + "&SubCat=" + SubCategory;
        else
            url = "Catalogue.aspx?Cat=" + Category;
        return url = url + "&listing=" + productListing.ToString();
    }

    protected void gvProducts_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblInventory = (Label)e.Row.FindControl("lblInventory");
                Button btnGetPrice = (Button)e.Row.FindControl("btnGetPrice");
                if (lblInventory.Text == "0")
                    lblInventory.Text = "Unavailable";
                if (Convert.ToInt32(lblInventory.Text) <= 0)
                {
                    lblInventory.Text = "Requires Quote";
                    btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else
                    btnGetPrice.Style.Add(HtmlTextWriterStyle.Display, "none");

                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                Button btnCallAvail = (Button)e.Row.FindControl("btnCallAvail");
                if (Convert.ToInt32(lblPrice.Text) <= 0)
                {
                    lblPrice.Text = "Requires Quote";
                    btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else
                    btnCallAvail.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void gvProducts_RowCreated(object sende, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Attributes.Add("onmouseover", "this.className='Row'");
                e.Row.Attributes.Add("onmouseout", "this.className='alt'");
            }
            catch (Exception ex)
            {

            }
        }
    }

    private void GetProducts(string CategoryID, string SubCategoryID, string ManufacturerID, string Attributes, bool IsInStock, bool IsPricingAvailable)
    {
        Product objP = new Product();
        DataTable dt = new DataTable();
        dt = objP.GetList(CategoryID, SubCategoryID, ManufacturerID, Attributes, IsInStock, IsPricingAvailable).Tables[0];
        if (dt != null)
        {
            gvProducts.DataSource = dt;
            gvProducts.DataBind();
        }
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
        chkCategory.DataValueField = "ShortCode";
        chkCategory.DataBind();

        SubCategory objS = new SubCategory();
        dt = objS.GetList();
        chkSubCategory.DataSource = dt;
        chkSubCategory.DataTextField = "Name";
        chkSubCategory.DataValueField = "S_ShortCode";
        chkSubCategory.DataBind();

        string filter = "";
        Attribute objA = new Attribute();
        dt = objA.GetList();
        if (chkCategory.Items.Cast<ListItem>().Count(li => li.Selected) > 0)
        {
            foreach (ListItem itm in chkCategory.Items)
            {
                if (itm.Selected)
                    filter = (filter == "") ? "C_ShortCode ='" + itm.Value + "'" : "or C_ShortCode ='" + itm.Value + "'";
            }
        }

        if (chkSubCategory.Items.Cast<ListItem>().Count(li => li.Selected) > 0)
        {
            foreach (ListItem itm in chkSubCategory.Items)
            {
                if (itm.Selected)
                    filter = (filter == "") ? "S_ShortCode ='" + itm.Value + "'" : "or S_ShortCode ='" + itm.Value + "'";
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
            sb.Append("<tr><td>" + dt.Rows[i]["Name"] + ": </td><td><input type=\"text\" name='" + dt.Rows[i]["AttributeID"] + "' id='" + dt.Rows[i]["AttributeID"] + "' runat=\"server\" style=\" width:80px;\" /><br/></td></tr>");
        }
        sb.Append("</table>");
        ltAttributes.Text = sb.ToString();
    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        ltCatalogue.Text = "";
        GetProducts(hdnCategory.Value, hdnSubCategory.Value, hdnManufacturer.Value, hdnAttributes.Value, Convert.ToBoolean(hdnInStock.Value), Convert.ToBoolean(hdnPricingAvailable.Value));
    }

    //private string GetSelectedManufacturer()
    //{
    //    string Cat = "";
    //    foreach (ListItem item in chkManufacturer.Items) 
    //    {
    //        if (item.Selected)
    //        {
    //            Cat = Cat == "" ? item.Value : Cat + ", " + item.Value;
    //        }
    //    }
    //    return Cat;
    //}

    //private string GetSelectedCategories()
    //{
    //    string Cat = "";
    //    foreach (ListItem item in chkCategory.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            Cat = Cat == "" ? item.Value : Cat + ", " + item.Value;
    //        }
    //    }
    //    return Cat;
    //}

    //private string GetSelectedSubCategories()
    //{
    //    string Cat = "";
    //    foreach (ListItem item in chkSubCategory.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            Cat = Cat == "" ? item.Value : Cat + ", " + item.Value;
    //        }
    //    }
    //    return Cat;
    //}
}