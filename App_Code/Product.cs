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

    public string ImageName
    { get; set; }
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
            this.Price = Convert.ToDecimal(Convert.ToString(dtProduct.Rows[0]["Price"]));
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
            this.ImageName = Convert.ToString(dtProduct.Rows[0]["ImageName"]);
        }
    }
    #endregion

    #region Methods
    public DataSet GetList(string CategoryID, string SubCategoryID, string ManufacturerID, string Attributes, bool IsInStock, bool IsPricingAvail, string PriceRange, int CurrentPgNo, int PageSize, string SortField, string SortOrder, out int TotalRecords)
    {
        TotalRecords = 0;
        DataSet dt = null;
        string[] arrPR = new string[2];
        if (PriceRange.Contains(':'))
        {
            arrPR = PriceRange.Split(':');
        }
        else
        {
            arrPR[0] = "0";
            arrPR[1] = "0";
        }
        SqlParameter[] sqlParams = new SqlParameter[13];
        try
        {
            sqlParams[0] = CategoryID != "" ? new SqlParameter("@CategoryID", CategoryID) : new SqlParameter("@CategoryID", DBNull.Value);
            sqlParams[1] = SubCategoryID != "" ? new SqlParameter("@SubCategoryID", SubCategoryID) : new SqlParameter("@SubCategoryID", DBNull.Value);
            sqlParams[2] = ManufacturerID != "" ? new SqlParameter("@ManufacturerID", ManufacturerID) : new SqlParameter("@ManufacturerID", DBNull.Value);
            sqlParams[3] = Attributes != "" ? new SqlParameter("@Attributes", Attributes) : new SqlParameter("@Attributes", DBNull.Value);
            sqlParams[4] = IsInStock == true ? new SqlParameter("@InStock", IsInStock) : new SqlParameter("@InStock", DBNull.Value);
            sqlParams[5] = IsPricingAvail == true ? new SqlParameter("@PricingAvailable", IsPricingAvail) : new SqlParameter("@PricingAvailable", DBNull.Value);
            sqlParams[6] = Convert.ToInt32(arrPR[0].Trim()) > 0 ? new SqlParameter("@PriceRange1", Convert.ToInt32(arrPR[0])) : new SqlParameter("@PriceRange1", DBNull.Value);
            sqlParams[7] = Convert.ToInt32(arrPR[1].Trim()) > 0 ? new SqlParameter("@PriceRange2", Convert.ToInt32(arrPR[1])) : new SqlParameter("@PriceRange2", DBNull.Value);
            sqlParams[8] = new SqlParameter("@CurrentPgNo", CurrentPgNo);
            sqlParams[9] = PageSize > 100 ? new SqlParameter("@PageSize", DBNull.Value) : new SqlParameter("@PageSize", PageSize);
            sqlParams[10] = new SqlParameter("@SortField", SortField);
            sqlParams[11] = new SqlParameter("@SortOrder", SortOrder);

            sqlParams[12] = new SqlParameter("@TotalRecords", TotalRecords);
            sqlParams[12].Direction = ParameterDirection.Output;
            sqlParams[12].SqlDbType = SqlDbType.Int;
            sqlParams[12].ParameterName = "@TotalRecords";
            dt = SqlHelper.ExecuteDataSet("ProductGetList1", CommandType.StoredProcedure, sqlParams);
            TotalRecords = Convert.ToInt32(sqlParams[12].Value);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public DataTable GetList(string ProductCode)
    {
        DataTable dt = null;
        SqlParameter[] sqlParams = new SqlParameter[0];
        dt = SqlHelper.ExecuteDataSet("SELECT * FROM Product WHERE ProductCode='" + ProductCode + "'", CommandType.Text, sqlParams).Tables[0];

        return dt;
    }
    #endregion
}