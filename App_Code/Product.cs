using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
public class Product
{
    #region Properties
    public int ProductID
    { get; set; }

    public string Name
    { get; set; }

    public string ProductCode
    { get; set; }

    public int CategoryID
    { get; set; }
    public int SubCategoryID
    { get; set; }

    public string Descrip
    { get; set; }

    public int ManufacturerID
    { get; set; }

    public string Technology
    { get; set; }

    public string HarmonizedCode
    { get; set; }

    public decimal Price
    { get; set; }

    public List<KeyValuePair<int, string>> Attributes
    {
        get;
        set;
    }

    public List<KeyValuePair<string, decimal>> Pricing
    {
        get;
        set;
    }

    public int Inventory
    { get; set; }
    #endregion

    #region Constructors
    public Product()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Product(int ProductID)
    {
        DataSet ds = new DataSet();
        DataTable dtProduct = new DataTable();
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@ProductID", ProductID);
        ds = SqlHelper.ExecuteDataSet("ProductGetDetails", CommandType.StoredProcedure, sqlParams);
        if (ds != null)
            dtProduct = ds.Tables[0];
        if (dtProduct.Rows.Count > 0)
        {
            this.ProductID = ProductID;
            this.Name = Convert.ToString(dtProduct.Rows[0]["Name"]);
            this.CategoryID = Convert.ToInt32(dtProduct.Rows[0]["CategoryID"]);
            this.SubCategoryID = Convert.ToInt32(dtProduct.Rows[0]["SubCategoryID"]);
            this.Descrip = Convert.ToString(dtProduct.Rows[0]["Descrip"]);
            this.ManufacturerID = Convert.ToInt32(dtProduct.Rows[0]["ManufacturerID"]);
            this.Technology = Convert.ToString(dtProduct.Rows[0]["Technology"]);
            this.HarmonizedCode = Convert.ToString(dtProduct.Rows[0]["HarmonizedCode"]);
            this.Price = Convert.ToDecimal(dtProduct.Rows[0]["Price"]);
            this.ProductCode = Convert.ToString(dtProduct.Rows[0]["ProductCode"]);
            if (Convert.ToString(dtProduct.Rows[0]["Attributes"]) != "")
            {
                this.Attributes = new List<KeyValuePair<int, string>>();
                string[] arrAttributes = Convert.ToString(dtProduct.Rows[0]["Attributes"]).Split(',');
                for (int i = 0; i < arrAttributes.Length; i++)
                {
                    this.Attributes.Add(new KeyValuePair<int, string>(Convert.ToInt32(arrAttributes[i].Split('|')[0]), Convert.ToString(arrAttributes[i].Split('|')[1])));
                }
            }

            if (Convert.ToString(dtProduct.Rows[0]["Pricing"]) != "")
            {
                this.Pricing = new List<KeyValuePair<string, decimal>>();
                string[] arrAttributes = Convert.ToString(dtProduct.Rows[0]["Pricing"]).Split(',');
                for (int i = 0; i < arrAttributes.Length; i++)
                {
                    if (Convert.ToString(arrAttributes[i].Split('|')[1]) != "0")
                        this.Pricing.Add(new KeyValuePair<string, decimal>(Convert.ToString(arrAttributes[i].Split('|')[0]) + "-" + Convert.ToString(arrAttributes[i].Split('|')[1]), Convert.ToDecimal(arrAttributes[i].Split('|')[2])));
                    else
                        this.Pricing.Add(new KeyValuePair<string, decimal>(Convert.ToString(arrAttributes[i].Split('|')[0]), Convert.ToDecimal(arrAttributes[i].Split('|')[2])));
                }
            }
            this.Inventory = Convert.ToInt32(dtProduct.Rows[0]["Inventory"]);
        }
    }
    #endregion

    #region Methods
    public DataSet GetList(string CategoryID, string SubCategoryID, string ManufacturerID, string Attributes,bool IsInStock, bool IsPricingAvail)
    {
        DataSet dt = null;
        SqlParameter[] sqlParams = new SqlParameter[6];
        try
        {
            sqlParams[0] = CategoryID != "" ? new SqlParameter("@CategoryID", CategoryID) : new SqlParameter("@CategoryID", DBNull.Value);
            sqlParams[1] = SubCategoryID != "" ? new SqlParameter("@SubCategoryID", SubCategoryID) : new SqlParameter("@SubCategoryID", DBNull.Value);
            sqlParams[2] = ManufacturerID != "" ? new SqlParameter("@ManufacturerID", ManufacturerID) : new SqlParameter("@ManufacturerID", DBNull.Value);
            sqlParams[3] = Attributes != "" ? new SqlParameter("@Attributes", Attributes) : new SqlParameter("@Attributes", DBNull.Value);
            sqlParams[4] = IsInStock == true ? new SqlParameter("@InStock", IsInStock) : new SqlParameter("@InStock", DBNull.Value);
            sqlParams[5] = IsPricingAvail == true ? new SqlParameter("@PricingAvailable", IsPricingAvail) : new SqlParameter("@PricingAvailable", DBNull.Value);
            dt = SqlHelper.ExecuteDataSet("ProductGetList", CommandType.StoredProcedure, sqlParams);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    #endregion
}