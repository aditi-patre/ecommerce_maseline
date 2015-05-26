using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderDetails
/// </summary>
public class OrderDetails
{
    #region Properties
    public int OrderDetailID
    { get; set; }

    public int OrderID
    { get; set; }

    public int ProductID
    { get; set; }
    public int Quantity
    {
        get;
        set;
    }
    public decimal Price
    {
        get;
        set;
    }

    #endregion

    #region Constructors
    public OrderDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Methods

    public DataSet GetList(int OrderID)
    {
        DataSet dt = null;
        SqlParameter[] sqlParams = new SqlParameter[1];
        try
        {
            sqlParams[0] = OrderID > 0 ? new SqlParameter("@OrderID", OrderID) : new SqlParameter("@OrderID", DBNull.Value);
            dt = SqlHelper.ExecuteDataSet("OrderDetailsGetList", CommandType.StoredProcedure, sqlParams);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public bool Save()
    {
        if (this.OrderDetailID <= 0)
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
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@OrderDetailID", OrderID);
            sqlParams[0].Direction = ParameterDirection.Output;
            sqlParams[0].SqlDbType = SqlDbType.Int;
            sqlParams[0].ParameterName = "@OrderDetailID";
            sqlParams[1] = new SqlParameter("@OrderID", OrderID);
            sqlParams[2] = new SqlParameter("@ProductID", ProductID);
            sqlParams[3] = new SqlParameter("@Qty", Quantity);
            sqlParams[4] = new SqlParameter("@Price", Price);

            SqlHelper.ExecuteScalar("OrderDetailsInsert", CommandType.StoredProcedure, sqlParams);
            this.OrderDetailID = Convert.ToInt32(sqlParams[0].Value);
        }
        catch (Exception ex)
        {
            // To Do: Handle Exception
            return false;
        }

        return this.OrderID > 0;
    }

    public bool Insert(DataTable dtOrderDetails) //
    {
        bool ret = true;
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "OrderDetailsInsert";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@OrderDtls", dtOrderDetails);
                sqlParams[0].SqlDbType = SqlDbType.Structured;
                sqlParams[0].ParameterName = "@OrderDtls";

                conn.Open();
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                //return false;
                ret = false;
            }
            finally
            {
                conn.Close();
            }
        }
        return ret;
    }
    #endregion
}