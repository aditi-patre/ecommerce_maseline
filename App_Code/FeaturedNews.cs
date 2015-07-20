using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for FeaturedNews
/// </summary>
public class FeaturedNews
{

    #region Properties

    public int FeaturedNewsID
    {
        get;
        set;
    }

    public string Descrip
    {
        get;
        set;
    }

    public string ImageName
    {
        get;
        set;
    }

    public bool IsActive
    {
        get;
        set;
    }

    #endregion

    #region Constructors
    public FeaturedNews()
    {

    }

    public FeaturedNews(int ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string Query = "select FeaturedNewsID, isnull(Descrip,'') as Descrip, isnull(ImageName,'') as ImageName, isnull(IsActive,1) as IsActive from FeaturedNews where FeaturedNewsID=" + ID.ToString();
        ds = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null);
        if (ds != null)
            dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.FeaturedNewsID = ID;
            this.Descrip = Convert.ToString(dt.Rows[0]["Descrip"]);
            this.ImageName = Convert.ToString(dt.Rows[0]["ImageName"]);
            this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
        }
    }
    #endregion

    public DataTable GetList(bool IsActive) //False indicates get all
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string Query = "select FeaturedNewsID, isnull(Descrip,'') as Descrip, isnull(ImageName,'') as ImageName, isnull(IsActive,1) as IsActive from FeaturedNews ";
        if (IsActive == true)
            Query += " where isnull(IsActive,1)=1";
        ds = SqlHelper.ExecuteDataSet(Query, CommandType.Text, null);
        if (ds != null)
            dt = ds.Tables[0];

        return dt;
    }

    public bool Save()
    {
        if (this.FeaturedNewsID == 0)
            return this.Insert();
        else
            return this.Update();
    }


    private bool Insert()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@FeaturedNewsID", 0);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@FeaturedNewsID";

            string Query = "insert into FeaturedNews(Descrip, ImageName, IsActive) values('" + this.Descrip + "','" + this.ImageName + "'," + (this.IsActive == true ? 1.ToString(): 0.ToString()) + ");";
            Query += " set @FeaturedNewsID= @@identity";
            SqlHelper.ExecuteScalar(Query, CommandType.Text, sqlParams);
            this.FeaturedNewsID = Convert.ToInt32(sqlParams[0].Value);
        }
        catch (Exception ex)
        {
            return false;
        }

        return this.FeaturedNewsID > 0;
    }

    private bool Update()
    {
        try
        {
            string Query = " update FeaturedNews set Descrip='" + this.Descrip + "', ImageName='" + this.ImageName + "', IsActive=" + (this.IsActive == true ? 1.ToString() : 0.ToString());
            Query += " where FeaturedNewsID=" + this.FeaturedNewsID;
            SqlHelper.ExecuteNonQuery(Query, CommandType.Text, null);

        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
}
