using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfo
/// </summary>
public class UserInfo
{
    #region Properties
    public int UserId
    { get; set; }

    public string UserName
    { get; set; }

    public string Password
    { get; set; }

    public string UserGuid
    { get; set; }

    public string UserEmail
    { get; set; }

    public string UserAddress
    {
        get;
        set;
    }

    public int RoleID
    { get; set; }
    #endregion

    #region Constructors
    public UserInfo()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    public UserInfo(int UserID)
    {
        DataTable dtUserInfo = new DataTable();
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@UserID", UserID);
        DataSet ds = SqlHelper.ExecuteDataSet("UserGetDetails", CommandType.StoredProcedure, sqlParams);
        if (ds != null)
            dtUserInfo = ds.Tables[0];
        if (dtUserInfo.Rows.Count > 0)
        {
            this.UserId = UserID;
            this.UserName = Convert.ToString(dtUserInfo.Rows[0]["UserName"]);
            this.Password = Convert.ToString(dtUserInfo.Rows[0]["Password"]);
            this.RoleID = Convert.ToInt32(dtUserInfo.Rows[0]["RoleID"]);
        }
    }
    #endregion


    #region Methods
    public static bool AddUser(string username, string password)
    {
        Guid UserGuid = System.Guid.NewGuid();
        string hashedPassword = Security.HashSHA1(password + UserGuid.ToString());

        SqlParameter[] sqlParams = new SqlParameter[4];
        sqlParams[0] = new SqlParameter("@UserID", 0);
        sqlParams[0].Direction = ParameterDirection.Output;
        sqlParams[1] = new SqlParameter("@UserName", username);
        sqlParams[2] = new SqlParameter("@Password", hashedPassword);
        sqlParams[3] = new SqlParameter("@UserGuid", UserGuid);
        
        int i = SqlHelper.ExecuteNonQuery("UserInsert", CommandType.StoredProcedure, sqlParams);
        return true;
    }

   
    public static int GetUserIdByUsernameAndPassword(string username, string password, out int Role_ID)
    {
        int userId = 0;
        Role_ID = 0;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        using (SqlCommand cmd = new SqlCommand("SELECT UserID, Password, UserGuid, isnull(RoleID,0) as RoleID FROM [Users] WHERE UserName=@username", con))
        {
            cmd.Parameters.AddWithValue("@username", username);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int dbUserId = Convert.ToInt32(dr["UserID"]);
                string dbPassword = Convert.ToString(dr["Password"]);
                string dbUserGuid = Convert.ToString(dr["UserGuid"]);
                Role_ID = Convert.ToInt32(dr["RoleID"]);
                HttpContext.Current.Session["User"] = username+"|"+dbUserId.ToString();
                string hashedPassword = Security.HashSHA1(password + dbUserGuid);
                if (dbPassword == hashedPassword)
                {
                    userId = dbUserId;
                }
            }
            con.Close();
        }
        return userId;
    }
   
    public static bool UserExists(string username)
    {
        int userId = 0;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        using (SqlCommand cmd = new SqlCommand("SELECT count(UserID) as cnt FROM [Users] WHERE UserName=@username", con))
        {
            cmd.Parameters.AddWithValue("@username", username);
            con.Open();           

            SqlDataReader dr = cmd.ExecuteReader();            
            while (dr.Read())
            {
                userId = Convert.ToInt32(dr["cnt"]);
            }
            con.Close();
        }
        return (userId > 0);
    }
    #endregion
}