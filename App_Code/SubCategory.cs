using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubCategory
/// </summary>
public class SubCategory
{

    #region Properties
    public int SubCategoryID
    {
        get;
        set;
    }

    public string ShortCode
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }

    public int CategoryID
    {
        get;
        set;
    }
    public string C_ShortCode
    {
        get;
        set;
    }

    public string Descrip
    {
        get;
        set;
    }


    #endregion

    #region Constructors
    public SubCategory()
    {
        this.SubCategoryID = -1;
        this.ShortCode = "";
        this.CategoryID = -1;
        this.C_ShortCode = "";
        this.Descrip = "";
        this.Name = "";

    }

    public SubCategory(int SubCategoryID)
    {
        DataSet ds = new DataSet();
        DataTable dtSubCategory = new DataTable();
        //SqlParameter[] sqlParams = new SqlParameter[1];
        //sqlParams[0] = new SqlParameter("@CategoryID", CategoryID);
        string Query = "select SubCategoryID, S_ShortCode, C_ShortCode, s.CategoryID, isnull(s.Name,'') as Name, s.Descrip, isnull(s.IsActive,1) as IsActive, c.Name as Cat_Name from SubCategory s left outer join Category c ";
        Query += " on s.CategoryID = c.CategoryID where SubCategoryID =" + SubCategoryID.ToString();
        dtSubCategory = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        if (dtSubCategory.Rows.Count > 0)
        {
            this.SubCategoryID = SubCategoryID;
            this.Name = Convert.ToString(dtSubCategory.Rows[0]["Name"]);
            this.ShortCode = Convert.ToString(dtSubCategory.Rows[0]["S_ShortCode"]);
            this.CategoryID = Convert.ToInt32(dtSubCategory.Rows[0]["CategoryID"]);
            this.C_ShortCode = Convert.ToString(dtSubCategory.Rows[0]["C_ShortCode"]);
            this.Descrip = Convert.ToString(dtSubCategory.Rows[0]["Descrip"]);
        }
    }

    public DataTable GetList()
    {
        DataTable dt = null;
        try
        {
            string Query = "select SubCategoryID, S_ShortCode, Name, CategoryID, C_ShortCode, Descrip from SubCategory where ISNULL(IsActive,1) = 1";
            if (this.Name != "")
                Query = Query + " and Name like '%" + this.Name + "%'";
            Query += " order by Name";
            dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public DataTable GetList(int CurrentPgNo, int PageSize, string SortField, string SortOrder, out int TotalRecords)
    {
        DataTable dt = null;
        if (CurrentPgNo == 0)
            CurrentPgNo = 1;
        int StrIndex = 1;
        TotalRecords = 0;
        if (SortField == "")
            SortField = "s.Name";
        if (SortOrder == "")
            SortOrder = "ASC";

        try
        {
            string Query = "select s.SubCategoryID, s.S_ShortCode, s.Name, s.CategoryID, c.ShortCode, s.Descrip, isnull(c.Name,'') as CatName ";
            Query += ", ROW_NUMBER() OVER (ORDER BY " + SortField + " " + SortOrder.ToUpper() + " ) AS RowNum";
            Query += " from SubCategory s left outer join Category c on s.CategoryID = c.CategoryID";
            Query += " where ISNULL(s.IsActive,1) = 1";
            if (this.Name != "")
                Query = Query + " and s.Name like '%" + this.Name + "%'";
            //Query += " order by Name";
            dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
            TotalRecords = dt.Rows.Count;

            if (PageSize <= 100) //Not all records are to be fetched
            {
                if (CurrentPgNo == 0)
                    CurrentPgNo = 1;
                StrIndex = PageSize * (CurrentPgNo - 1);
                Query = "select * from (" + Query + ")t where RowNum >" + StrIndex + " and RowNum<=" + Convert.ToInt32(StrIndex + PageSize);
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
        if (this.SubCategoryID <= 0)
            return this.Insert();
        else
            return this.Update();
    }


    private bool Insert()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@SubCategoryID", SubCategoryID);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@SubCategoryID";

            string Query = "insert into SubCategory(S_ShortCode, CategoryID, C_ShortCode, Name, Descrip) values('" + this.ShortCode + "'," + this.CategoryID.ToString() + ", '" + this.C_ShortCode + "', '" + this.Name + "','" + this.Descrip + "'); set @SubCategoryID= @@identity";
            SqlHelper.ExecuteScalar(Query, CommandType.Text, sqlParams);
            this.SubCategoryID = Convert.ToInt32(sqlParams[0].Value);
        }
        catch (Exception ex)
        {
            return false;
        }

        return this.SubCategoryID > 0;
    }

    private bool Update()
    {
        try
        {
            string Query = "update SubCategory set S_ShortCode='" + this.ShortCode + "', Name='" + this.Name + "', Descrip='" + this.Descrip + "', CategoryID=" + this.CategoryID.ToString() + ", C_ShortCode='" + this.C_ShortCode + "' where SubCategoryID=" + this.SubCategoryID;
            SqlHelper.ExecuteNonQuery(Query, CommandType.Text, null);
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
            string Query = " update SubCategory set IsActive = 0 where SubCategoryID =" + this.SubCategoryID;
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
        string Query = "select SubCategoryID from SubCategory where S_ShortCode = '" + this.ShortCode + "' and ISNULL(IsActive,1) = 1 and SubCategoryID !=" + ID;
        dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;

    }
    #endregion
}