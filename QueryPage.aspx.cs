using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QueryPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string ApplyFilter(string Manufacturer, string Category, string SubCategory, bool IsInStock, bool IsPricingAvail, string Attributes)
    {
        try
        {
            DataSet ds = null;
            Product objProduct = new Product();
            ds = objProduct.GetList(Category, SubCategory, Manufacturer, Attributes, IsInStock, IsPricingAvail);
            return ds.GetXml();

        }
        catch (Exception ex)
        {
            return null;
        }
    }


    [WebMethod]
    public static string ExpandSearch(string CriteriaToExpand)
    {
        try
        {
            if (CriteriaToExpand == "Category")
            {
                Category objM = new Category();
                DataTable dtM = objM.CategoriesList();
                for (int i = dtM.Columns.Count - 1; i >= 0; i--)
                {
                    if (!(dtM.Columns[i].ColumnName == "categoryID" || dtM.Columns[i].ColumnName == "Name"))
                        dtM.Columns.Remove(dtM.Columns[i].ColumnName);
                }
                dtM.Columns["categoryID"].ColumnName = "Value";
                dtM.AcceptChanges();
                List<CategoryItem> chkM = new List<CategoryItem>();
                chkM = ConvertDataTable<CategoryItem>(dtM);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                return ser.Serialize(chkM);
            }
            else if (CriteriaToExpand == "Manufacturer")
            {
                Manufacturer objM = new Manufacturer();
                DataTable dtM = objM.GetList();
                dtM.Columns["ManufacturerID"].ColumnName = "Value";
                dtM.AcceptChanges();
                List<ManufacturerItem> chkM = new List<ManufacturerItem>();
                chkM = ConvertDataTable<ManufacturerItem>(dtM);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                return ser.Serialize(chkM);
            }
            else if (CriteriaToExpand == "Sub-Category")
            {
                SubCategory objM = new SubCategory();
                DataTable dtM = objM.GetList();
                for (int i = dtM.Columns.Count - 1; i >= 0; i--)
                {
                    if (!(dtM.Columns[i].ColumnName == "SubCategoryID" || dtM.Columns[i].ColumnName == "Name"))
                        dtM.Columns.Remove(dtM.Columns[i].ColumnName);
                }
                dtM.Columns["SubCategoryID"].ColumnName = "Value";
                dtM.AcceptChanges();
                List<SubCategoryItem> chkM = new List<SubCategoryItem>();
                chkM = ConvertDataTable<SubCategoryItem>(dtM);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                return ser.Serialize(chkM);
            }
            return "";
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [WebMethod]
    public static string Register(string UserName, string Password)
    {
        bool RetVal = false;
        Output objMsg = new Output();
        if (UserName != "" && Password != "")
        {
            if (UserInfo.UserExists(UserName))
            {
                RetVal = false;
                objMsg.Message = "UserName in use, please select another user.";
            }
            else
            {
                if (UserInfo.AddUser(UserName, Password))
                {
                    RetVal = true;
                    objMsg.Message = "User added successfully.";
                }
            }
        }
        objMsg.IsSuccess = RetVal;
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(objMsg);
    }

    private static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }
}
public class ManufacturerItem
{
    public int Value { get; set; }
    public string Name { get; set; }
}

public class SubCategoryItem
{
    public int Value { get; set; }
    public string Name { get; set; }
}

public class CategoryItem
{
    public int Value { get; set; }
    public string Name { get; set; }
}

public class Output
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}