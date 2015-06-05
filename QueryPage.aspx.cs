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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Collections;

public partial class QueryPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*[WebMethod]
    public static string ApplyFilter(string Manufacturer, string Category, string SubCategory, bool IsInStock, bool IsPricingAvail, string Attributes)
    {
        try
        {
            int TR;
            DataSet ds = null;
            Product objProduct = new Product();
            ds = objProduct.GetList(Category, SubCategory, Manufacturer, Attributes, IsInStock, IsPricingAvail,1,out TR);
            return ds.GetXml();

        }
        catch (Exception ex)
        {
            return null;
        }
    }*/


    [WebMethod]
    public static string ExpandSearch(string CriteriaToExpand)
    {
        try
        {
            if (CriteriaToExpand.Contains("Category"))
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
            else if (CriteriaToExpand.Contains("Manufacturer"))
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
            else if (CriteriaToExpand.Contains("Sub-Category"))
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
                    objMsg.Message = "User registered successfully.";
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

    [WebMethod]
    public static string AddToCart(string ProductID)
    {
        int CartItemCount = ShoppingCart.Instance.AddItem(Convert.ToInt32(ProductID));
        JavaScriptSerializer ser = new JavaScriptSerializer();
        return ser.Serialize(CartItemCount.ToString());
    }

    [WebMethod]
    public static string ComputePrice(string Qty, string Index)
    {
        //List<CartItem> obj = ShoppingCart.Instance.Items;
        Output objMsg = new Output();
        if (Convert.ToInt32(Qty) > ShoppingCart.Instance.Items[Convert.ToInt32(Index)].Inventory)
        {
            ShoppingCart.Instance.Items[Convert.ToInt32(Index)].Quantity = 0;
            objMsg.IsSuccess = false;
            objMsg.Message = "The specified quantity is unavailable";
        }
        else
        {
            ShoppingCart.Instance.Items[Convert.ToInt32(Index)].Quantity = Convert.ToInt32(Qty);
            objMsg.IsSuccess = true;
            objMsg.Message = ShoppingCart.Instance.Items[Convert.ToInt32(Index)].Price.ToString();
        }
        objMsg.TotalAmt = ShoppingCart.Instance.GetSubTotal().ToString();
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(objMsg);
    }

    [WebMethod]
    public string Checkout(string CartHasItems)
    {
        Output objMsg = new Output();
        if (HttpContext.Current.Session["User"] == null)
        {
            objMsg.IsSuccess = false;
            objMsg.Message = "Please login to continue";
        }
        else
        {
            objMsg.IsSuccess = true;
            objMsg.Message = Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[0];
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(objMsg);
    }

    [WebMethod]
    public static void RequestEntity(string ProductID, string RequestEntity, string Name, string Email, string ContactNo)
    {
        Output objMsg = new Output();
        string to = System.Configuration.ConfigurationManager.AppSettings["ReceiverEmail"].ToString();
        string from = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
        string subject = "Request " + RequestEntity;
        try
        {
            Hashtable hashtable = new Hashtable();
            Product ci = new Product(Convert.ToInt32(ProductID));
            hashtable.Add("[ProductInfo]", "Product Code: " + ci.ProductCode + "<br/>" + "Name: " + ci.Name + "<br/>Harmonized Code: " + ci.HarmonizedCode);
            hashtable.Add("[RequestEntity]", RequestEntity);
            hashtable.Add("[CustName]", Name);
            hashtable.Add("[Email]", Email);
            hashtable.Add("[ContactNo]", ContactNo);

            using (MailMessage mm = new MailMessage(from, to))
            {
                string path = HttpContext.Current.Request.MapPath("EmailTemplates/RequestQuote.html");
                mm.Subject = subject;
                mm.Body = GenerateEmailTemplate(path, hashtable);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FromEmail"]), Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Password"]));
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                objMsg.IsSuccess = true;
                objMsg.Message = "A mail requesting the " + RequestEntity.ToLower() + " has been sent.";
            }
        }
        catch (Exception ex)
        {
            objMsg.IsSuccess = false;
            objMsg.Message = "Mail could not be sent.";
        }
    }



    [WebMethod]
    public static string ManufacturerSave(string ID, string Name, string Code)
    {
        Output objMsg = new Output();
        Manufacturer objM = new Manufacturer(Convert.ToInt32(ID));
        objM.Name = Name;
        objM.ManufacturerCode = Code;
        objM.Save();
        objMsg.IsSuccess = true;
        objMsg.Message = "Manufacturer details have been " + (Convert.ToInt32(ID) > 0 ? "update" : "saved") + " successfully";
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(objMsg);
    }
    public static string GenerateEmailTemplate(string path, Hashtable hashTable)
    {
        StreamReader streamreader = new StreamReader(path);
        string text = "", stream;
        try
        {
            text = "";
            stream = streamreader.ReadLine();
            while (stream != null)
            {
                stream = streamreader.ReadLine();
                IDictionaryEnumerator en = hashTable.GetEnumerator();
                while (en.MoveNext())
                {
                    if (stream.Contains(en.Key.ToString()))
                    {
                        string str = en.Value.ToString();
                        stream = stream.Replace(en.Key.ToString(), en.Value.ToString());
                        hashTable.Remove(en.Key);
                        break;
                    }
                }
                text = text + stream;
            }
            streamreader.Close();
        }
        catch (Exception ex)
        {
            streamreader.Close();
        }
        return text;
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

    public string TotalAmt { get; set; }
}