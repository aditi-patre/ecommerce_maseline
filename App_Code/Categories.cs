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
        if(dtCategory.Rows.Count > 0)
        {
            this.SubCategories = new List<SubCategory>();
            this.CategoryID = CategoryID;
            this.Descrip = Convert.ToString(dtCategory.Rows[0]["c_descrip"]);
            this.Name = Convert.ToString(dtCategory.Rows[0]["c_name"]);
            this.ShortCode = Convert.ToString(dtCategory.Rows[0]["shortcode"]);
            //this.SubCategories.Add
            for(int i=0; i< dtCategory.Rows.Count; i++)
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
        SqlParameter[] sqlParams = new SqlParameter[0];
        try
        {
            dt = SqlHelper.ExecuteDataSet("CategoryGetList", CommandType.StoredProcedure, sqlParams).Tables[0];
        }
        catch (Exception ex)
        {
           
        }
        return dt;
    }
    #endregion
}