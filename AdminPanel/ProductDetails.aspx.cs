using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_ProductDetails : System.Web.UI.Page
{
    int _ProductID = 0;
    public Product _objProduct = null;
    public Images _Images = new Images();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropdownLists();
            if (Request.QueryString != null)
            {
                _ProductID = Convert.ToInt32(Request.QueryString["PID"]);
                LoadProductDetails(_ProductID);
            }
        }
        //}
    }

    private void PopulateDropdownLists()
    {
        DataTable dt = new DataTable();
        //ddlManufacturer.Items.Clear();
        Manufacturer objM = new Manufacturer();
        int TR;
        dt = objM.GetList(1, 101, "", "", out TR);
        DataRow dr = dt.NewRow();
        dr["Name"] = "Select";
        dr["ManufacturerID"] = "0";
        dt.Rows.Add(dr);
        ddlManufacturer.DataSource = dt;
        ddlManufacturer.DataTextField = "Name";
        ddlManufacturer.DataValueField = "ManufacturerID";
        ddlManufacturer.DataBind();
        ddlManufacturer.SelectedValue = "0";

        //ddlCategoryID.Items.Clear();
        Category objC = new Category();
        dt = objC.CategoriesList();
        DataRow dr2 = dt.NewRow();
        dr2["Name"] = "Select";
        dr2["CategoryID"] = "0";
        dt.Rows.Add(dr2);
        ddlCategoryID.DataSource = dt;
        ddlCategoryID.DataTextField = "Name";
        ddlCategoryID.DataValueField = "CategoryID";//"ShortCode";
        ddlCategoryID.DataBind();
        ddlCategoryID.SelectedValue = "0";

        //ddlSubCategoryID.Items.Clear();
        SubCategory objS = new SubCategory();
        dt = objS.GetList();
        DataRow dr3 = dt.NewRow();
        dr3["Name"] = "Select";
        dr3["SubCategoryID"] = "0";
        dt.Rows.Add(dr3);
        ddlSubCategoryID.DataSource = dt;
        ddlSubCategoryID.DataTextField = "Name";
        ddlSubCategoryID.DataValueField = "SubCategoryID"; //"S_ShortCode";
        ddlSubCategoryID.DataBind();
        ddlSubCategoryID.SelectedValue = "0";

    }
    private void LoadProductDetails(int PID)
    {
        Product objProduct = new Product(PID);
        txtProductName.Text = objProduct.Name;
        txtProductCode.Text = objProduct.ProductCode;
        txtProductDescrip.Text = objProduct.Descrip;
        ddlManufacturer.SelectedValue = objProduct.ManufacturerID > 0 ? objProduct.ManufacturerID.ToString() : "0";
        txtInventory.Text = Convert.ToString(objProduct.Inventory);

        ddlCategoryID.SelectedValue = objProduct.CategoryID > 0 ? objProduct.CategoryID.ToString() : "0";
        ddlSubCategoryID.SelectedValue = objProduct.SubCategoryID > 0 ? objProduct.SubCategoryID.ToString() : "0";

        txtHarmonizedCode.Text = objProduct.HarmonizedCode;
        txtTechnology.Text = objProduct.Technology;

        if (objProduct.Pricing == null)
        {
            chkIsPricing.Checked = false;
            txtPrice.Text = String.Format("{0:0.00}", objProduct.Price);//Convert.ToString(objProduct.Price)
            //tblPricing.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
        else
        {
            chkIsPricing.Checked = true;
            //tblPricing.Style.Add(HtmlTextWriterStyle.Display, "block");
            DataTable dtPricing = new DataTable();
            dtPricing.Columns.Add("RangeID", typeof(Int32));
            dtPricing.Columns.Add("QtyRange", typeof(string));
            dtPricing.Columns.Add("Price", typeof(string));
            dtPricing.Columns.Add("Range1", typeof(string));
            dtPricing.Columns.Add("Range2", typeof(string));
            for (int i = 0; i < objProduct.Pricing.Count; i++)
            {
                DataRow dr = dtPricing.NewRow();
                dr["RangeID"] = i;
                dr["QtyRange"] = Convert.ToString(objProduct.Pricing[i].Key);
                dr["Price"] = Convert.ToString(objProduct.Pricing[i].Value);
                dtPricing.Rows.Add(dr);

                if (objProduct.Pricing[i].Key.Contains('-'))
                {
                    string[] arrRange = objProduct.Pricing[i].Key.Split('-');
                    dr["Range1"] = arrRange[0].ToString();
                    dr["Range2"] = arrRange[1].ToString();
                }
                else
                {
                    dr["Range1"] = objProduct.Pricing[i].Key;
                    dr["Range2"] = "";
                    dr["QtyRange"] = dr["QtyRange"] + " and Up";
                }
            }
            gvPricing.DataSource = dtPricing;
            gvPricing.DataBind();
        }

        string FolderPath = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + objProduct.ProductID.ToString();
        if (Directory.Exists(Server.MapPath(FolderPath)))
        {
            ddlImageList.Enabled = true;
            string[] ImageFiles = Directory.GetFiles(Server.MapPath(FolderPath));
            string[] dtImage = new string[ImageFiles.Length];

            for (int i = 0; i < ImageFiles.Length; i++)
            {
                if (ImageFiles[i].IndexOf(objProduct.ImageName) != -1)
                    _Images.imageName = objProduct.ImageName;

                if (_Images.prodImage == null)
                    _Images.prodImage = new List<KeyValuePair<System.Drawing.Image, string>>();

                dtImage[i] = ImageFiles[i].Substring(ImageFiles[i].LastIndexOf('\\') + 1);
                _Images.prodImage.Add(new KeyValuePair<System.Drawing.Image, string>(System.Drawing.Image.FromFile(ImageFiles[i]), dtImage[i]));

            }

            ddlImageList.DataSource = dtImage;
            ddlImageList.DataBind();
            ViewState["Images"] = _Images;
        }
        else
            ddlImageList.Enabled = false;
        //ViewState["Images"] = 
        _objProduct = objProduct;
        ViewState["Product"] = objProduct;
        BindImagesLocal();
        if (PID > 0)
        {
            ////ddlImageList.Style.Add(HtmlTextWriterStyle.MarginTop, "10%");
            idivMainImage.Style.Add(HtmlTextWriterStyle.Display, "block");
            LoadMainImage();
        }
        else
        {
            //ddlImageList.Style.Add(HtmlTextWriterStyle.MarginTop, "0%");
            idivMainImage.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx", true);
    }

    private void LoadMainImage()
    {
        string FolderPath = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + ((Product)ViewState["Product"]).ProductID.ToString();

        if (((Product)ViewState["Product"]).ImageName != "" && ddlImageList.SelectedItem.Text != "")
        {
            ddlImageList.SelectedItem.Text = ((Product)ViewState["Product"]).ImageName;
            string FolderPathTmp = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//Temp";
            //if (Convert.ToInt32(((Product)ViewState["Product"]).ProductID) > 0)
            //    idivMainImage.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url('" + FolderPath + "//" + ((Product)ViewState["Product"]).ImageName + "')");
            //else
            idivMainImage.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url('" + FolderPathTmp + "//" + ((Product)ViewState["Product"]).ImageName + "')");
            idivMainImage.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
    }

    #region Images
    protected void btnAddImage_Click(object sender, EventArgs e)
    {
        Product objP = (Product)ViewState["Product"];
        if (rptImages.Items.Count == 0)
        {
            objP.ImageName = fupldImage.FileName;
        }
        if (fupldImage.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(fupldImage.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            if (Array.IndexOf(allowedExtensions, fileExtension) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please upload an image file.');", true);
            }
            else
            {
                System.Drawing.Image postedImage = System.Drawing.Image.FromStream(fupldImage.FileContent);

                if (ViewState["Images"] != null)
                    _Images = (Images)ViewState["Images"];

                _Images.imageName = fupldImage.FileName;
                if (_Images.prodImage == null)
                    _Images.prodImage = new List<KeyValuePair<System.Drawing.Image, string>>();
                _Images.prodImage.Add(new KeyValuePair<System.Drawing.Image, string>(postedImage, fupldImage.FileName));
                //fupldImage.SaveAs(Server.MapPath("~/ProductImages/Temp/" + fupldImage.FileName));
                ViewState["Images"] = _Images;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please select an image to upload.');", true);
        }
        BindImagesLocal();
        LoadMainImage();
    }

    private void BindImagesLocal()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ImageName", typeof(string));
        dt.Columns.Add("ImagePath", typeof(System.Drawing.Image));

        _Images = (Images)ViewState["Images"];
        if (_Images != null)
        {
            if (_Images.prodImage != null)
            {
                for (int i = 0; i < _Images.prodImage.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["ImageName"] = _Images.prodImage[i].Value;
                    dr["ImagePath"] = (System.Drawing.Image)_Images.prodImage[i].Key;
                    dt.Rows.Add(dr);
                }
            }
        }
        ddlImageList.DataSource = dt;
        ddlImageList.DataTextField = "ImageName";
        ddlImageList.DataValueField = "ImageName";
        ddlImageList.DataBind();
        if (dt.Rows.Count > 0)
        {
            ddlImageList.Enabled = true;
            idivMainImage.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            ddlImageList.Enabled = false;
            idivMainImage.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        rptImages.DataSource = dt;
        rptImages.DataBind();
    }

    public string GetImageFromByte(object IP, object IN)
    {
        try
        {
            System.Drawing.Image ImagePath = (System.Drawing.Image)IP;
            string ImageName = (string)IN;
            string FolderPath = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//Temp";
            if (!Directory.Exists(Server.MapPath(FolderPath)))
                Directory.CreateDirectory(Server.MapPath(FolderPath));
            ImagePath.Save(Server.MapPath(FolderPath) + "//" + ImageName);

            return (FolderPath + "//" + ImageName);
            //byte[] byts = Convert.ToByte(byt);
            //return byteArrayToImage(byts);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    protected void rptImages_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteI")
        {
            //ViewState["Images"] = (Images)            
            _Images = (Images)ViewState["Images"];
            if (_Images.imageName == _Images.prodImage[Convert.ToInt32(e.Item.ItemIndex)].Value.ToString()) //Main image removed
            {
                if (Convert.ToInt32(e.Item.ItemIndex) == _Images.prodImage.Count - 1) //last image was the main image
                {
                    if (e.Item.ItemIndex > 0)
                        _Images.imageName = _Images.prodImage[Convert.ToInt32(e.Item.ItemIndex) - 1].Value.ToString();
                    else//Only 1 image
                    {
                        _Images.imageName = "";
                    }
                }
                else
                    _Images.imageName = _Images.prodImage[Convert.ToInt32(e.Item.ItemIndex) + 1].Value.ToString();
            }


            _Images.prodImage.RemoveAt(Convert.ToInt32(e.Item.ItemIndex));

            ViewState["Images"] = _Images;
            Product p = (Product)ViewState["Product"];
            p.ImageName = _Images.imageName;
            ViewState["Product"] = p;
            BindImagesLocal();
            LoadMainImage();

        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Product objProduct = new Product(((Product)ViewState["Product"]).ProductID);
            objProduct.Name = txtProductName.Text;
            objProduct.ProductCode = txtProductCode.Text;
            if (!objProduct.CodeExists(objProduct.ProductID.ToString()))
            {
                objProduct.Descrip = txtProductDescrip.Text;
                objProduct.ManufacturerID = Convert.ToInt32(ddlManufacturer.SelectedItem.Value);
                objProduct.CategoryID = Convert.ToInt32(ddlCategoryID.SelectedItem.Value);
                objProduct.SubCategoryID = Convert.ToInt32(ddlSubCategoryID.SelectedItem.Value);
                objProduct.HarmonizedCode = Convert.ToString(txtHarmonizedCode.Text);
                objProduct.Technology = Convert.ToString(txtTechnology.Text);
                objProduct.ImageName = Convert.ToString(ddlImageList.SelectedItem.Text);

                if (ViewState["Product"] != null)
                {
                    if (((Product)ViewState["Product"]).Pricing != null)
                    {
                        _objProduct = ((Product)ViewState["Product"]);
                        objProduct.Pricing = _objProduct.Pricing;
                    }
                }
                objProduct.Price = chkIsPricing.Checked == false ? Convert.ToDecimal(txtPrice.Text) : objProduct.Pricing[0].Value;
                bool Success = objProduct.Save();
                if (Success)
                {
                    _objProduct = objProduct;
                    ViewState["Product"] = objProduct;

                    Images rptImg = (Images)ViewState["Images"];

                    //Save Images
                    string FolderPath = "..//" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + objProduct.ProductID.ToString();
                    if (!Directory.Exists(Server.MapPath(FolderPath)))
                        Directory.CreateDirectory(Server.MapPath(FolderPath));
                    else
                    {
                        bool IsFound = true;
                        //Clear existing images
                        string[] files = Directory.GetFiles(Server.MapPath(FolderPath));
                        foreach (string filepath in files)
                        {
                            for (int i = 0; i < rptImg.prodImage.Count; i++)
                            {
                                if (filepath.IndexOf(rptImg.prodImage[i].Value.ToString()) != -1)
                                {
                                    IsFound = true;
                                    break;
                                }
                                else
                                    IsFound = false;
                            }
                            if (!IsFound)
                                File.Delete(filepath);
                        }
                    }

                    for (int i = 0; i < rptImg.prodImage.Count; i++)
                    {
                        System.Drawing.Image img = (System.Drawing.Image)rptImg.prodImage[i].Key;
                        //if (File.Exists(Server.MapPath(FolderPath) + "//" + rptImg.prodImage[i].Value.ToString()))
                        //{
                        //    GC.Collect();
                        //    GC.WaitForPendingFinalizers();
                        //    File.Delete(Server.MapPath(FolderPath) + "//" + rptImg.prodImage[i].Value.ToString());
                        //}
                        if (!File.Exists(Server.MapPath(FolderPath) + "//" + rptImg.prodImage[i].Value.ToString()))
                            img.Save(Server.MapPath(FolderPath) + "//" + rptImg.prodImage[i].Value.ToString());

                    }
                    if (objProduct.Pricing != null)
                    {
                        if (objProduct.Pricing.Count > 0)
                            BindPricing();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Product details saved successfully');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('There was an error in saving the product');", true);

            }
            else
            {
                if (ViewState["Product"] != null)
                {
                    if (((Product)ViewState["Product"]).Pricing != null)
                        _objProduct.Pricing = ((Product)ViewState["Product"]).Pricing; //(List<KeyValuePair<string, decimal>>)ViewState["Pricing"];
                }
                BindPricingLocal();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Enter a unique product code');", true);
            }
        }
        catch (Exception ex)
        {
            //Error
        }
    }

    #region PricingEditEvents

    protected void BindPricing()
    {
        Product objProduct = new Product(((Product)ViewState["Product"]).ProductID);
        DataTable dtPricing = new DataTable();
        dtPricing.Columns.Add("RangeID", typeof(Int32));
        dtPricing.Columns.Add("QtyRange", typeof(string));
        dtPricing.Columns.Add("Price", typeof(string));
        dtPricing.Columns.Add("Range1", typeof(string));
        dtPricing.Columns.Add("Range2", typeof(string));
        for (int i = 0; i < objProduct.Pricing.Count; i++)
        {
            DataRow dr = dtPricing.NewRow();
            dr["RangeID"] = i;
            dr["QtyRange"] = Convert.ToString(objProduct.Pricing[i].Key);
            dr["Price"] = Convert.ToString(objProduct.Pricing[i].Value);
            dtPricing.Rows.Add(dr);

            if (objProduct.Pricing[i].Key.Contains('-'))
            {
                string[] arrRange = objProduct.Pricing[i].Key.Split('-');
                dr["Range1"] = arrRange[0].ToString();
                dr["Range2"] = arrRange[1].ToString();
            }
            else
            {
                dr["Range1"] = objProduct.Pricing[i].Key;
                dr["Range2"] = "";
                dr["QtyRange"] = dr["QtyRange"] + " and Up";
            }
        }
        gvPricing.DataSource = dtPricing;
        gvPricing.DataBind();
    }
    protected void BindPricingLocal()
    {
        DataTable dtPricing = new DataTable();
        dtPricing.Columns.Add("RangeID", typeof(Int32));
        dtPricing.Columns.Add("QtyRange", typeof(string));
        dtPricing.Columns.Add("Price", typeof(string));
        dtPricing.Columns.Add("Range1", typeof(string));
        dtPricing.Columns.Add("Range2", typeof(string));

        _objProduct = ((Product)ViewState["Product"]);// (List<KeyValuePair<string, decimal>>)ViewState["Pricing"];
        for (int i = 0; i < _objProduct.Pricing.Count; i++)
        {
            DataRow dr = dtPricing.NewRow();
            dr["RangeID"] = i;
            dr["QtyRange"] = Convert.ToString(_objProduct.Pricing[i].Key);
            dr["Price"] = Convert.ToString(_objProduct.Pricing[i].Value);
            dtPricing.Rows.Add(dr);

            if (_objProduct.Pricing[i].Key.Contains('-'))
            {
                string[] arrRange = _objProduct.Pricing[i].Key.Split('-');
                dr["Range1"] = arrRange[0].ToString();
                dr["Range2"] = arrRange[1].ToString();
            }
            else
            {
                dr["Range1"] = _objProduct.Pricing[i].Key;
                dr["Range2"] = "";
                dr["QtyRange"] = dr["QtyRange"] + " and Up";
            }
        }
        gvPricing.DataSource = dtPricing;
        gvPricing.DataBind();
    }
    protected void gvPricing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteP")
        {
            if (_objProduct == null)
                _objProduct = ((Product)ViewState["Product"]); //_objProduct.Pricing = ((Product)ViewState["Product"]).Pricing; //(List<KeyValuePair<string, decimal>>)ViewState["Pricing"];
            _objProduct.Pricing.RemoveAt(Convert.ToInt32(e.CommandArgument));

            Product pp = ((Product)ViewState["Product"]);
            pp.Pricing = _objProduct.Pricing;
            ViewState["Product"] = pp;
            BindPricingLocal();
        }
    }

    protected void gvPricing_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPricing.EditIndex = e.NewEditIndex;
        BindPricingLocal();
    }

    protected void gvPricing_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        gvPricing.EditIndex = -1;
        BindPricingLocal();
    }

    protected void gvPricing_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvPricing.Rows[e.RowIndex];
        TextBox txtQtyRange1 = (TextBox)row.FindControl("txtQtyRange1");
        TextBox txtQtyRange2 = (TextBox)row.FindControl("txtQtyRange2");
        string Rnge = txtQtyRange1.Text + " - " + txtQtyRange2.Text;

        decimal dprice;
        bool IsValid = Decimal.TryParse(((TextBox)gvPricing.Rows[e.RowIndex].Cells[1].Controls[0]).Text, out dprice);

        if (((Product)ViewState["Product"]).Pricing != null) //Add new
        {
            _objProduct = (Product)ViewState["Product"];
            _objProduct.Pricing[e.RowIndex] = new KeyValuePair<string, decimal>(Rnge, Convert.ToDecimal(((TextBox)gvPricing.Rows[e.RowIndex].Cells[1].Controls[0]).Text));
        }
        else
        {
            if (!IsValid)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please enter a valid price');", true);
            else
            {
                Product objProduct = new Product(((Product)ViewState["Product"]).ProductID);
                objProduct.Pricing = new List<KeyValuePair<string, decimal>>();
                for (int r = 0; r < gvPricing.Rows.Count; r++)
                {
                    if (r == 0)
                    {
                        if (gvPricing.Rows.Count == 1) //the first row is being updated
                            objProduct.Price = Convert.ToDecimal(((TextBox)gvPricing.Rows[0].Cells[1].Controls[0]).Text);
                        else
                            objProduct.Price = Convert.ToDecimal(gvPricing.Rows[0].Cells[1]);
                    }
                    objProduct.Pricing.Add(new KeyValuePair<string, decimal>(Rnge, Convert.ToDecimal(((TextBox)gvPricing.Rows[e.RowIndex].Cells[1].Controls[0]).Text)));

                }
                if (_objProduct == null)
                    _objProduct = (Product)ViewState["Product"];
                _objProduct.Pricing = objProduct.Pricing;
            }
        }
        if (IsValid)
        {
            Product pp = ((Product)ViewState["Product"]);
            pp.Pricing = _objProduct.Pricing;
            ViewState["Product"] = pp;
        }
        gvPricing.EditIndex = -1;
        BindPricingLocal();
    }

    protected void gvPricing_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            gvPricing.AlternatingRowStyle.CssClass = "ListingAltRowStyle";
            gvPricing.RowStyle.CssClass = "ListingRowStyle";
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            gvPricing.HeaderStyle.CssClass = "ListingAltRowStyle";
        }
        //TextBox txtQtyRange1 = (TextBox)e.Row.FindControl("txtQtyRange1");
        //TextBox txtQtyRange2 = (TextBox)e.Row.FindControl("txtQtyRange2");

        //Label lblQtyRange = (Label)e.Row.FindControl("lblQtyRange");
        //if (lblQtyRange.Text.IndexOf('-') >= 0)
        //{
        //    string[] arrRange = lblQtyRange.Text.Split('-');
        //    txtQtyRange1.Text = arrRange[0];
        //    txtQtyRange2.Text = arrRange[1];
        //}
        //else
        //{
        //    txtQtyRange1.Text = lblQtyRange.Text;
        //    txtQtyRange2.Text = "";
        //}
    }

    protected void btnRngeAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (true)
            {
                bool IsValid = true;
                string[] arrRnge = null;
                //int RangeID = 0;
                DataTable dtPricing = new DataTable();
                dtPricing.Columns.Add("RangeID", typeof(int));
                dtPricing.Columns.Add("QtyRange", typeof(string));
                dtPricing.Columns.Add("Price", typeof(string));

                if (gvPricing.Rows.Count > 0)
                {
                    arrRnge = ((Label)gvPricing.Rows[gvPricing.Rows.Count - 1].FindControl("lblQtyRange")).Text.Split('-'); //Lastrow range
                    //RangeID = Convert.ToInt32((gvPricing.Rows[gvPricing.Rows.Count - 1].Cells[1]).Text); //Last row id
                }

                if (arrRnge != null && (arrRnge[1].Trim() == "Up" || arrRnge[1].Trim() == "" || Convert.ToInt32(txtRngeFrom1.Text) <= Convert.ToInt32(arrRnge[1].Trim())))
                {
                    IsValid = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('The current range should be greater than the previously defined range.');", true);
                }
                else if (txtRngeTo1.Text != "" && txtRngeTo1.Text != "Up") //check From > than to
                {
                    if (Convert.ToInt32(txtRngeFrom1.Text.Trim()) > Convert.ToInt32(txtRngeTo1.Text.Trim()))
                    {
                        IsValid = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Invalid range');", true);
                    }
                }
                if (IsValid)
                {
                    //Product objP = new Product(_ProductID);
                    //objP.Pricing.Add(new KeyValuePair<string, decimal>(txtRngeFrom1.Text + " - " + txtRngeTo1.Text == "" ? "Up" : txtRngeTo1.Text, Convert.ToDecimal(txtRngePrice.Text)));
                    //objP.AddPricing();
                    if (((Product)ViewState["Product"]).Pricing == null || ((Product)ViewState["Product"]).Pricing.Count == 0)  //(_objProduct.Pricing == null)
                    {
                        Product p1 = ((Product)ViewState["Product"]);
                        p1.Pricing = new List<KeyValuePair<string, decimal>>();
                        ViewState["Product"] = p1;
                        if (_objProduct == null)
                            _objProduct = (Product)ViewState["Product"];
                        _objProduct.Pricing = p1.Pricing;//new List<KeyValuePair<string, decimal>>();
                    }
                    else if (ViewState["Product"] != null)
                    {
                        _objProduct = (Product)ViewState["Product"];
                        //if (((Product)ViewState["Product"]).Pricing != null)
                        //    _objProduct.Pricing = (List<KeyValuePair<string, decimal>>)((Product)ViewState["Product"]).Pricing;
                    }
                    _objProduct.Pricing.Add(new KeyValuePair<string, decimal>((txtRngeFrom1.Text + " - " + (txtRngeTo1.Text == "" ? "Up" : txtRngeTo1.Text)), Convert.ToDecimal(txtRngePrice.Text)));

                    Product pp = ((Product)ViewState["Product"]);
                    pp.Pricing = _objProduct.Pricing;
                    ViewState["Product"] = pp;

                    BindPricingLocal();
                    txtRngeFrom1.Text = "";
                    txtRngeTo1.Text = "";
                    txtRngePrice.Text = "";
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    protected void ddlImageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlImageList.Enabled == true)
        {
            Product objP = ((Product)ViewState["Product"]);
            objP.ImageName = ddlImageList.SelectedItem.Text;
            ViewState["Product"] = objP;

            Images img = (Images)ViewState["Images"];
            img.imageName = ddlImageList.SelectedItem.Text;
            ViewState["Images"] = img;
            LoadMainImage();
        }
    }
}

[Serializable]
public class Images
{
    public string imageName
    {
        get;
        set;
    }


    public List<KeyValuePair<System.Drawing.Image, string>> prodImage
    {
        get;
        set;
    }
}