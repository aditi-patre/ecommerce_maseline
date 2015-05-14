using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Attribute
/// </summary>
public class Attribute
{
    #region Properties

    public int AttributeID
    { get; set; }

    public string ShortCode
    { get; set; }

    public string Name
    { get; set; }

    public int CategoryID
    { get; set; }

    public int SubCategoryID
    { get; set; }

    public string C_ShortCode
    { get; set; }

    public string S_ShortCode
    { get; set; }
    #endregion

    public Attribute()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetList()
    {
        DataTable dt = null;
        SqlParameter[] sqlParams = new SqlParameter[0];
        try
        {
            dt = SqlHelper.ExecuteDataSet("AttributeGetList", CommandType.StoredProcedure, sqlParams).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
}