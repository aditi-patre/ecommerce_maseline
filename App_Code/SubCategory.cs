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
        
    }

    public SubCategory(int SubCategoryID)
    {


    }

    public DataTable GetList()
    {
        //DataTable dt = null;
        //SqlParameter[] sqlParams = new SqlParameter[0];
        //try
        //{
        //    dt = SqlHelper.ExecuteDataSet("SubCategoryGetList", CommandType.StoredProcedure, sqlParams).Tables[0];
        //}
        //catch (Exception ex)
        //{

        //}
        //return dt;

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
    #endregion
}