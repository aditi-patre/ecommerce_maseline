using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SqlHelper
/// </summary>
public class SqlHelper
{
    private static SqlConnection Connection()
    {
        string conn_string = SqlHelper.GetConnectionString();
        if (conn_string == null)
        {
            throw new Exception("No 'connection string' found in AppSettings.");
        }

        SqlConnection output = null;
        try
        {
            output = new SqlConnection(conn_string);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return output;
    }



    public static object ExecuteScalar(string sql)
    {
        //if no command type, make it text.
        object result;
        result = SqlHelper.ExecuteScalar(sql, CommandType.Text);

        return result;
    }

    public static object ExecuteScalar(string sql, CommandType type)
    {
        //execute with null parameters.
        return SqlHelper.ExecuteScalar(sql, type, null);
    }

    public static object ExecuteScalar(string sql, CommandType type, SqlParameter[] parameters)
    {
        object output = null;
        //fetch a connection.
        using (SqlConnection conn = SqlHelper.Connection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;
            //try to add parameters to command object, and execute it
            int i = 0;

            try
            {
                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        if (p.Value != null)
                        {
                            if ((p.Value.GetType() == typeof(DateTime)) && (((DateTime)p.Value) < new DateTime(1753, 1, 1)))
                            {
                                if (p.Value != null)
                                    System.Diagnostics.Debug.WriteLine(string.Format("Parameter: {0} - {2} is {1}", i, p.Value, p.ParameterName));
                                else
                                    System.Diagnostics.Debug.WriteLine(string.Format("Parameter: {0} - {2} is {1}", i, "null", p.ParameterName));
                            }
                        }

                        cmd.Parameters.Add(p);
                        i++;

                        if (p.Value != null)
                            System.Diagnostics.Debug.WriteLine(string.Format("Parameter: {0} - {2} is {1}", i, p.Value, p.ParameterName));
                        else
                            System.Diagnostics.Debug.WriteLine(string.Format("Parameter: {0} - {2} is {1}", i, "null", p.ParameterName));
                    }


                }
                conn.Open();

                try
                {
                    output = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //SaveExceptionLogger s = new SaveExceptionLogger(ex, cmd);
                    //s.Run();

                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        return output;

    }

    public static DataTable ExecuteDataTable(string sql)
    {
        //if no command type, make it text.
        return SqlHelper.ExecuteDataTable(sql, CommandType.Text);
    }

    public static DataTable ExecuteDataTable(string sql, CommandType type)
    {
        //execute with null parameters.
        return SqlHelper.ExecuteDataTable(sql, type, null);
    }

    public static DataTable ExecuteDataTable(string sql, CommandType type, SqlParameter[] parameters)
    {
        DataTable output = new DataTable();
        //fetch a connection.
        using (SqlConnection conn = SqlHelper.Connection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;
            //try to add parameters to command object, and execute it
            try
            {
                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();

                try
                {
                    da.Fill(output);
                }
                catch (Exception ex)
                {
                    //SaveExceptionLogger s = new SaveExceptionLogger(ex, cmd);
                    //s.Run();

                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        return output;

    }

    public static DataSet ExecuteDataSet(string sql)
    {
        //if no command type, make it text.
        return SqlHelper.ExecuteDataSet(sql, CommandType.Text);
    }
    public static DataSet ExecuteDataSet(string sql, CommandType type)
    {
        //execute with null parameters.
        return SqlHelper.ExecuteDataSet(sql, type, null);
    }


    public static DataSet ExecuteDataSet(string sql, CommandType type, SqlParameter[] parameters)
    {
        DataSet output = new DataSet();
        using (SqlConnection conn = SqlHelper.Connection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;
            try
            {
                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();

                try
                {
                    da.Fill(output);
                }
                catch (Exception ex)
                {
                    //SaveExceptionLogger s = new SaveExceptionLogger(ex, cmd);
                    //s.Run();

                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        return output;

    }



    public static int ExecuteNonQuery(string sql)
    {
        //if no command type, make it text.
        return SqlHelper.ExecuteNonQuery(sql, CommandType.Text);
    }

    public static int ExecuteNonQuery(string sql, CommandType type)
    {
        //execute with null parameters.
        return SqlHelper.ExecuteNonQuery(sql, type, null);
    }

    public static int ExecuteNonQuery(string sql, CommandType type, SqlParameter[] parameters)
    {
        //fetch a connection.
        using (SqlConnection conn = SqlHelper.Connection())
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;
            //try to add parameters to command object, and execute it
            try
            {
                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                conn.Open();

                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //SaveExceptionLogger s = new SaveExceptionLogger(ex, cmd);
                    //s.Run();

                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return i;
        }

    }


    public static DataTable SelectDistinct(DataTable SourceTable, params string[] FieldNames)
    {
        object[] lastValues;
        DataTable newTable;
        DataRow[] orderedRows;

        if (FieldNames == null || FieldNames.Length == 0)
            throw new ArgumentNullException("FieldNames");

        lastValues = new object[FieldNames.Length];
        newTable = new DataTable();

        //			foreach (string fieldName in FieldNames)
        //				newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

        //get all of the columns in the sourceTable
        foreach (DataColumn _column in SourceTable.Columns)
        {
            newTable.Columns.Add(_column.ColumnName, _column.DataType);
        }

        orderedRows = SourceTable.Select("", string.Join(", ", FieldNames));

        foreach (DataRow row in orderedRows)
        {
            if (!fieldValuesAreEqual(lastValues, row, FieldNames))
            {
                newTable.Rows.Add(createRowClone(row, newTable.NewRow()));

                setLastValues(lastValues, row, FieldNames);
            }
        }

        return newTable;
    }


    private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
    {
        bool areEqual = true;

        for (int i = 0; i < fieldNames.Length; i++)
        {
            if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
            {
                areEqual = false;
                break;
            }
        }

        return areEqual;
    }


    private static DataRow createRowClone(DataRow sourceRow, DataRow newRow)
    {
        for (int i = 0; i < sourceRow.ItemArray.Length; i++)
        {
            newRow[i] = sourceRow[i];
        }

        return newRow;
    }


    private static void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
    {
        for (int i = 0; i < fieldNames.Length; i++)
            lastValues[i] = sourceRow[fieldNames[i]];
    }


    public static DataTable CreateTable(DataView obDataView)
    {
        if (null == obDataView)
        {
            throw new ArgumentNullException
                ("DataView", "Invalid DataView object specified");
        }

        DataTable obNewDt = obDataView.Table.Clone();
        int idx = 0;
        string[] strColNames = new string[obNewDt.Columns.Count];
        foreach (DataColumn col in obNewDt.Columns)
        {
            strColNames[idx++] = col.ColumnName;
        }

        IEnumerator viewEnumerator = obDataView.GetEnumerator();
        while (viewEnumerator.MoveNext())
        {
            DataRowView drv = (DataRowView)viewEnumerator.Current;
            DataRow dr = obNewDt.NewRow();
            try
            {
                foreach (string strName in strColNames)
                {
                    dr[strName] = drv[strName];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            obNewDt.Rows.Add(dr);
        }

        return obNewDt;
    }

    private static string GetConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    }

}