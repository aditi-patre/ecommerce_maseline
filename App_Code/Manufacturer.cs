using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Manufacturer
/// </summary>
public class Manufacturer
{
    #region Properties
    public int ManufacturerID
    { get;set;}

    public string Name
    { get; set; }

    public string ManufacturerCode
    { get; set; }

    public bool IsActive
    { get; set; }
    #endregion

    #region Constructors
    public Manufacturer()
	{
        ManufacturerID = -1;
        ManufacturerCode = "";
        Name = "";
        IsActive = true;
	}

    public Manufacturer(int ManufacturerID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@ManufacturerID", ManufacturerID);
        ds = SqlHelper.ExecuteDataSet("ManufacturerGetDetails", CommandType.StoredProcedure, sqlParams);
         if (ds != null)
            dt = ds.Tables[0];
        if(dt.Rows.Count > 0)
        {
            this.ManufacturerID = ManufacturerID;
            this.ManufacturerCode = Convert.ToString(dt.Rows[0]["ManufacturerCode"]);
            this.Name = Convert.ToString(dt.Rows[0]["Name"]);
            this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
        }
    }
    #endregion

    #region Methods
    public DataTable GetList()
    {
        DataTable dt = null;
        //SqlParameter[] sqlParams = new SqlParameter[0];
        try
        {
            string Query = "select ManufacturerID, Name, ISNULL(ManufacturerCode,'') as ManufacturerCode, ISNULL(IsActive,1) as IsActive from Manufacturer where ISNULL(IsActive,1) = 1";
            if(this.Name != "")
                Query = Query +" and Name like '%"+this.Name+"%'";
            Query += " order by Name";
            dt = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public bool Save()
    {
        if (this.ManufacturerID == 0)
            return this.Insert();
        else
            return this.Update();
    }


    private bool Insert()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@ManufacturerID", this.ManufacturerID);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@ManufacturerID";
            sqlParams[1] = new SqlParameter("@Name", this.Name);
            sqlParams[2] = new SqlParameter("@ManufacturerCode", this.ManufacturerCode);
            SqlHelper.ExecuteScalar("ManufacturerInsert", CommandType.StoredProcedure, sqlParams);
            this.ManufacturerID = Convert.ToInt32(sqlParams[0].Value);
        }
        catch (Exception ex)
        {
            return false;
        }

        return this.ManufacturerID > 0;
    }

    private bool Update()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@ManufacturerID", this.ManufacturerID);
            sqlParams[1] = new SqlParameter("@Name", this.Name);
            sqlParams[2] = new SqlParameter("@ManufacturerCode", this.ManufacturerCode);
            SqlHelper.ExecuteNonQuery("ManufacturerUpdate", CommandType.StoredProcedure, sqlParams);
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
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@ManufacturerID", this.ManufacturerID);
            SqlHelper.ExecuteNonQuery("ManufacturerDelete", CommandType.StoredProcedure, sqlParams);
        }
        catch(Exception ex)
        {
            return false;
        }
        return true;
    }

    

    #endregion

}