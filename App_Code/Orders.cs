using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Orders
/// </summary>
public class Orders
{
    #region Properties

    public int OrderID
    { get; set; }

    public int UserID
    { get; set; }

    public string UserEmail
    { get; set; }

    public string OrderGUID
    { get; set; }

    public string Address
    {
        get;
        set;
    }

    public DateTime DateOfBooking
    {
        get;
        set;
    }

    public DateTime DateOfTransact
    {
        get;
        set;
    }
    #endregion

    #region Constructors
    public Orders()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Orders(int OrderID)
    {
        DataSet ds = new DataSet();
        DataTable dtOrder = new DataTable();
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@OrderID", OrderID);
        ds = SqlHelper.ExecuteDataSet("OrderGetDetails", CommandType.StoredProcedure, sqlParams);
        if (ds != null)
            dtOrder = ds.Tables[0];
        if (dtOrder.Rows.Count > 0)
        {
            this.OrderID = OrderID;
            this.UserID = Convert.ToInt32(Convert.ToString(dtOrder.Rows[0]["UserID"]));
            this.UserEmail = Convert.ToString(dtOrder.Rows[0]["UserID"]);
            this.OrderGUID = Convert.ToString(dtOrder.Rows[0]["OrderGUID"]);
            this.Address = Convert.ToString(dtOrder.Rows[0]["Address"]);
            this.DateOfBooking = (dtOrder.Rows[0]["DateOfBooking"]) != System.DBNull.Value ? Convert.ToDateTime(dtOrder.Rows[0]["DateOfBooking"]) : Convert.ToDateTime(null);
            this.DateOfTransact = (dtOrder.Rows[0]["DateOfTransact"]) != System.DBNull.Value ? Convert.ToDateTime(dtOrder.Rows[0]["DateOfTransact"]) : Convert.ToDateTime(null);
        }
    }
    #endregion

    #region Methods

    public DataSet GetList()
    {
        DataSet dt = null;
        //SqlParameter[] sqlParams = new SqlParameter[0];
        try
        {
            dt = SqlHelper.ExecuteDataSet("OrderGetList", CommandType.StoredProcedure, null);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public bool Save()
    {
        if (this.OrderID <= 0)
        {
            return this.Insert();
        }
        //else
        //{
        //    return this.Update();
        //}
        return false;
    }

    private bool Insert()
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@OrderID", OrderID);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@OrderID";
            sqlParams[1] = this.UserID > 0 ? new SqlParameter("@UserID", this.UserID) : new SqlParameter("@UserID", DBNull.Value);
            sqlParams[2] = this.UserEmail != null ? new SqlParameter("@UserEmail", this.UserEmail) : new SqlParameter("@UserEmail", DBNull.Value);
            sqlParams[3] = this.OrderGUID != null ? new SqlParameter("@OrderGUID", this.OrderGUID) : new SqlParameter("@OrderGUID", DBNull.Value);
            sqlParams[4] = this.Address != null ? new SqlParameter("@Address", this.Address) : new SqlParameter("@Address", DBNull.Value);
            sqlParams[5] = new SqlParameter("@TransactionID", DBNull.Value);
            sqlParams[6] = new SqlParameter("@DateOfBooking", DateTime.Now);
            sqlParams[7] = new SqlParameter("@DateOfTransact", System.DBNull.Value);
            SqlHelper.ExecuteScalar("OrdersInsert", CommandType.StoredProcedure, sqlParams);
            this.OrderID = Convert.ToInt32(sqlParams[0].Value);
        }
        catch (Exception ex)
        {
            // To Do: Handle Exception
            return false;
        }

        return this.OrderID > 0;
    }

    #endregion
}