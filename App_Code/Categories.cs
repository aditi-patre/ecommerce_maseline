using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Category
/// </summary>
public class Category
{
    #region Properties

    public int CategoryID
    {
        get;
        set;
    }

    public string ShortCode
    { get; set; }

    public string Name
    {
        get;
        set;
    }
    public string Descrip
    {
        get;
        set;
    }

    public List<SubCategory> SubCategories
    {
        get;
        set;
    }
    #endregion

    #region Constructor
    public Category()
    {
        this.CategoryID = -1;
        this.Descrip = "";
        this.Name = "";
        this.ShortCode = "";
    }

    public Category(int CategoryID)
    {
        DataSet ds = new DataSet();
        DataTable dtCategory = new DataTable();
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@CategoryID", CategoryID);
        ds = SqlHelper.ExecuteDataSet("CategoryGetDetails", CommandType.StoredProcedure, sqlParams);
        if (ds != null)
            dtCategory = ds.Tables[0];
        if (dtCategory.Rows.Count > 0)
        {
            this.SubCategories = new List<SubCategory>();
            this.CategoryID = CategoryID;
            this.Descrip = Convert.ToString(dtCategory.Rows[0]["c_descrip"]);
            this.Name = Convert.ToString(dtCategory.Rows[0]["c_name"]);
            this.ShortCode = Convert.ToString(dtCategory.Rows[0]["shortcode"]);
            //this.SubCategories.Add
            for (int i = 0; i < dtCategory.Rows.Count; i++)
            {
                if (Convert.ToString(dtCategory.Rows[0]["SubCategoryID"]) != "")
                {
                    SubCategory sb = new SubCategory();
                    sb.C_ShortCode = Convert.ToString(dtCategory.Rows[i]["shortcode"]);
                    sb.CategoryID = CategoryID;
                    sb.Descrip = Convert.ToString(dtCategory.Rows[i]["s_descrip"]);
                    sb.Name = Convert.ToString(dtCategory.Rows[i]["sc_name"]);
                    sb.ShortCode = Convert.ToString(dtCategory.Rows[i]["S_ShortCode"]);
                    sb.SubCategoryID = Convert.ToInt32(dtCategory.Rows[i]["SubCategoryID"]);
                    this.SubCategories.Add(sb);
                }
            }
        }
    }
    #endregion

    #region Methods

    public DataTable CategoriesList()
    {
        DataTable dt = null;
        try
        {
            string Query = " select c.categoryID, c.shortcode, c.Name, c.descrip, COUNT(sc.categoryID) as subCategoryCount from category c left outer join SubCategory sc on c.ShortCode = sc.C_ShortCode where ISNULL(c.IsActive,1) = 1 ";

            //string Query = "select CategoryID, ShortCode, Name, Descrip from Category where ISNULL(IsActive,1) = 1";
            if (this.Name != "")
                Query = Query + " and Name like '%" + this.Name + "%'";
            Query += " group by c.CategoryID, c.shortcode, c.Name, c.descrip, sc.categoryID order by c.Name";
            dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public DataTable CategoriesList(int CurrentPgNo, int PageSize, string SortField, string SortOrder, out int TotalRecords)
    {
        DataTable dt = null;
        if (CurrentPgNo == 0)
            CurrentPgNo = 1;
        int StrIndex = 1;
        TotalRecords = 0;
        if (SortField == "")
            SortField = "Name";
        if (SortOrder == "")
            SortOrder = "ASC";
        try
        {
            string Query = " select c.categoryID, c.shortcode, c.Name, c.descrip, COUNT(sc.categoryID) as subCategoryCount ";
            Query += ", ROW_NUMBER() OVER (ORDER BY c." + SortField + " " + SortOrder.ToUpper() + " ) AS RowNum";
            Query += " from category c left outer join SubCategory sc on c.ShortCode = sc.C_ShortCode ";
            Query += "where ISNULL(c.IsActive,1) = 1";

            if (this.Name != "")
                Query = Query + " and c.Name like '%" + this.Name + "%'";
            Query += " group by c.CategoryID, c.shortcode, c.Name, c.descrip, sc.categoryID";
            dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
            TotalRecords = dt.Rows.Count;

            if (PageSize <= 100) //Not all records are to be fetched
            {
                if (CurrentPgNo == 0)
                    CurrentPgNo = 1;
                StrIndex = PageSize * (CurrentPgNo - 1);
                Query = "select * from (" + Query + ")t where RowNum >=" + StrIndex + " and RowNum<=" + Convert.ToInt32(StrIndex + PageSize);
            }
            dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public bool Save()
    {
        if (this.CategoryID == 0)
            return this.Insert();
        else
            return this.Update();
    }

    private bool Insert()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CategoryID", CategoryID);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@CategoryID";

            string Query = "insert into Category(Name, ShortCode, Descrip) values('" + this.Name + "','" + this.ShortCode + "','" + this.Descrip + "'); set @CategoryID= @@identity";
            SqlHelper.ExecuteScalar(Query, CommandType.Text, sqlParams);
            this.CategoryID = Convert.ToInt32(sqlParams[0].Value);
        }
        catch (Exception ex)
        {
            return false;
        }

        return this.CategoryID > 0;
    }

    private bool Update()
    {
        try
        {
            string Query1 = "select ShortCode from Category where CategoryID=" + this.CategoryID;
            DataTable dt = SqlHelper.ExecuteDataSet(Query1, CommandType.Text, null).Tables[0];
            string OldShortCode = dt.Rows[0][0].ToString();

            string Query = " update Category set ShortCode='" + this.ShortCode + "', Name='" + this.Name + "', Descrip='" + this.Descrip + "' where CategoryID=" + this.CategoryID;
            SqlHelper.ExecuteNonQuery(Query, CommandType.Text, null);

            if (OldShortCode != this.ShortCode)
            {
                string Query2 = "update SubCategory set C_ShortCode = '" + this.ShortCode + "' where C_ShortCode='" + OldShortCode + "'";
                SqlHelper.ExecuteNonQuery(Query2, CommandType.Text, null);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public bool Delete()
    {
        try
        {
            //SqlParameter[] sqlParams = new SqlParameter[1];
            //sqlParams[0] = new SqlParameter("@ManufacturerID", this.ManufacturerID);
            //SqlHelper.ExecuteNonQuery("ManufacturerDelete", CommandType.StoredProcedure, sqlParams);
            string Query = " update Category set IsActive = 0 where CategoryID =" + this.CategoryID;
            SqlHelper.ExecuteNonQuery(Query, CommandType.Text, null);
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public bool CodeExists(string ID)
    {
        DataTable dt = null;
        string Query = "select CategoryID from Category where ShortCode = '" + this.ShortCode + "' and ISNULL(IsActive,1) = 1 and CategoryID !=" + ID;
        dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;

    }
    #endregion
}