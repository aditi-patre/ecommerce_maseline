using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
[Serializable()]
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
        this.ProductID = -1;
        this.Name = "";
        this.CategoryID = -1;
        this.SubCategoryID = -1;
        this.Descrip = "";
        this.ManufacturerID = -1;
        this.Technology = "";
        this.HarmonizedCode = "";
        this.Price = 0.00M;
        this.ProductCode = ""; ;
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
        SqlParameter[] sqlParams = new SqlParameter[14];
        try
        {
            string _Name = this.Name;
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
            sqlParams[12] = _Name != "" ? new SqlParameter("@Name", _Name) : new SqlParameter("@Name", DBNull.Value);
            sqlParams[13] = new SqlParameter("@TotalRecords", TotalRecords);
            sqlParams[13].Direction = ParameterDirection.Output;
            sqlParams[13].SqlDbType = SqlDbType.Int;
            sqlParams[13].ParameterName = "@TotalRecords";
            dt = SqlHelper.ExecuteDataSet("ProductGetList1", CommandType.StoredProcedure, sqlParams);
            TotalRecords = Convert.ToInt32(sqlParams[13].Value);
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


    public bool Save()
    {
        if (this.ProductID == 0)
            return this.Insert();
        else
            return this.Update();
    }

    private bool Insert()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ProductID", 0);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@ProductID";

            string Query = "";
            if (SubCategoryID <= 0)
            {
                Query = "insert into Product(Name, ProductCode, CategoryID, Descrip, Technology, HarmonizedCode, Price, ManufacturerID, Inventory, ImageName) values('";
                Query += this.Name + "','" + this.ProductCode + "'," + this.CategoryID + ", '" + this.Descrip + "','" + this.Technology + "','" + this.HarmonizedCode + "', " + this.Price + ", " + this.ManufacturerID + ", " + this.Inventory + ", '" + this.ImageName + "');";
                Query += " set @ProductID= @@identity";
            }

            if (SubCategoryID > 0)
            {
                Query = "insert into Product(Name, ProductCode, CategoryID, SubCategoryID, Descrip, Technology, HarmonizedCode, Price, ManufacturerID, Inventory, ImageName) values('";
                Query += this.Name + "','" + this.ProductCode + "'," + this.CategoryID + "," + this.SubCategoryID + ", '" + this.Descrip + "','" + this.Technology + "','" + this.HarmonizedCode + "'," + this.Price + ", " + this.ManufacturerID + ", " + this.Inventory + ", '" + this.ImageName + "');";
                Query += " set @ProductID= @@identity";
            }
            SqlHelper.ExecuteScalar(Query, CommandType.Text, sqlParams);
            this.ProductID = Convert.ToInt32(sqlParams[0].Value);

            //Add Pricing if applicable
            if (this.Pricing != null)
            {
                for (int i = 0; i < this.Pricing.Count; i++)
                {
                    if (this.Pricing[i].Key.IndexOf('-') >= 0)
                    {
                        string[] arrRange = this.Pricing[i].Key.Split('-');
                        if (!arrRange[1].ToLower().Contains("up"))
                            Query = "insert into Pricing(ProductID, MinQty, MaxQty,Price) values(" + this.ProductID + ", " + arrRange[0] + ", " + arrRange[1] + ", " + this.Pricing[i].Value + ");";
                        else
                            Query = "insert into Pricing(ProductID, MinQty, Price) values(" + this.ProductID + ", " + arrRange[0] + ", " + this.Pricing[i].Value + ");";
                    }
                    else
                        Query = "insert into Pricing(ProductID, MinQty, Price) values(" + this.ProductID + ", " + this.Pricing[i].Key + ", " + this.Pricing[i].Value + ");";

                    SqlHelper.ExecuteScalar(Query, CommandType.Text, null);
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }

        return this.ProductID > 0;
    }

    private bool Update()
    {
        try
        {
            string Query = " update Product set Name='" + this.Name + "', ProductCode='" + this.ProductCode + "', CategoryID=" + this.CategoryID;
            if (SubCategoryID > 0)
                Query += ", SubCategoryID=" + this.SubCategoryID.ToString();
            Query += ", Descrip='" + this.Descrip + "', Technology='" + this.Technology + "', HarmonizedCode='" + this.HarmonizedCode + "', Price=" + this.Price.ToString() + ", ManufacturerID=" + this.ManufacturerID + ", Inventory=" + this.Inventory + ", ImageName='" + this.ImageName + "'";
            Query += " where ProductID=" + this.ProductID;
            SqlHelper.ExecuteNonQuery(Query, CommandType.Text, null);

            Product objP = new Product(this.ProductID);
            int cnt = 0;
            if (objP.Pricing != null)
                cnt = objP.Pricing.Count;

            if (this.Pricing != null)
            {
                for (int i = 0; i < this.Pricing.Count; i++)
                {
                    if (i < cnt)//Update existing pricing record
                    {
                        if (this.Pricing[i].Key.IndexOf('-') > -1)
                        {
                            string[] arrRange = this.Pricing[i].Key.Split('-');
                            if (!arrRange[1].ToLower().Contains("up"))
                                Query = "update Pricing set MinQty=" + arrRange[0] + ", MaxQty=" + arrRange[1] + ", Price=" + this.Pricing[i].Value + " where PricingID in (select top(" + Convert.ToInt32(i + 1) + ") PricingID from Pricing except (select top(" + i + ") PricingID from Pricing where ProductID=" + this.ProductID + "))";
                            else
                                Query = "update Pricing set MinQty=" + arrRange[0] + ", Price=" + this.Pricing[i].Value + " where PricingID in (select top(" + Convert.ToInt32(i + 1) + ") PricingID from Pricing except (select top(" + i + ") PricingID from Pricing where ProductID=" + this.ProductID + "))";
                        }
                        else
                            Query = "update Pricing set MinQty=" + this.Pricing[i].Key + ", Price=" + this.Pricing[i].Value + " where PricingID in (select top(" + Convert.ToInt32(i + 1) + ") PricingID from Pricing except (select top(" + i + ") PricingID from Pricing where ProductID=" + this.ProductID + "))";

                        SqlHelper.ExecuteScalar(Query, CommandType.Text, null);
                    }
                    else //add new
                    {
                        if (this.Pricing[i].Key.IndexOf('-') > -1)
                        {
                            string[] arrRange = this.Pricing[i].Key.Split('-');
                            if (!arrRange[1].ToLower().Contains("up"))
                                Query = "insert into Pricing(ProductID, MinQty,MaxQty, Price) values(" + this.ProductID + ", " + arrRange[0] + ", " + arrRange[1] + ", " + this.Pricing[i].Value + ")";
                            else
                                Query = "insert into Pricing(ProductID, MinQty,Price) values(" + this.ProductID + ", " + arrRange[0] + ", " + this.Pricing[i].Value + ")";
                        }
                        else
                            Query = "insert into Pricing(ProductID, MinQty,Price) values(" + this.ProductID + ", " + this.Pricing[i].Key + ", " + this.Pricing[i].Value + ")";

                        SqlHelper.ExecuteScalar(Query, CommandType.Text, null);
                    }
                }
                if (this.Pricing.Count < cnt) //Delete pricing
                {
                    Query = "delete from Pricing where ProductID=" + this.ProductID + " and PricingID in (select PricingID from Pricing except (select top(" + this.Pricing.Count + ") PricingID from Pricing where ProductID=" + this.ProductID + "))";
                    SqlHelper.ExecuteScalar(Query, CommandType.Text, null);
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public void AddPricing()
    {
        string Query = "";
        int i = this.Pricing.Count - 1;
        if (this.Pricing[i].Key.IndexOf('-') > -1)
        {
            string[] arrRange = this.Pricing[i].Key.Split('-');
            Query = "insert into Pricing(ProductID, MinQty,MaxQty, Price) values(" + this.ProductID + ", " + arrRange[0] + ", " + arrRange[1] + ", " + this.Pricing[i].Value + ")";
        }
        else
            Query = "insert into Pricing(ProductID, MinQty,Price) values(" + this.ProductID + ", " + this.Pricing[i].Key + ", " + this.Pricing[i].Value + ")";

        SqlHelper.ExecuteScalar(Query, CommandType.Text, null);
    }

    public static DataTable GetSearchableAttributes()
    {
        DataTable dt = null;
        SqlParameter[] sqlParams = new SqlParameter[0];
        dt = SqlHelper.ExecuteDataSet("select distinct a.Name, isnull(a.IsSrchCriteria,1)as IsSrchCriteria, STUFF((SELECT distinct ',' + CONVERT(varchar(10),isnull(a1.[CategoryID],0)) FROM Attribute a1 WHERE a.Name = a1.Name FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'') Categories from Attribute a where isnull(IsSrchCriteria,1)=1", CommandType.Text, sqlParams).Tables[0];

        return dt;
    }
    public bool CodeExists(string ID)
    {
        DataTable dt = null;
        string Query = "select ProductID from Product where ProductCode = '" + this.ProductCode + "' and ISNULL(IsActive,1) = 1 and ProductID !=" + ID;
        dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;

    }

    public bool Delete()
    {
        try
        {
            string Query = " update Product set IsActive = 0 where ProductID =" + this.ProductID;
            SqlHelper.ExecuteNonQuery(Query, CommandType.Text, null);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    #endregion
}