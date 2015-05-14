﻿using System;
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
    #endregion
    public Manufacturer()
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
            dt = SqlHelper.ExecuteDataSet("ManufacturerGetList", CommandType.StoredProcedure, sqlParams).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
}