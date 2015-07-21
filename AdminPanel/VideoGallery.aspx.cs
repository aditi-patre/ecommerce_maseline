using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_VideoGallery : System.Web.UI.Page
{
    string FolderPath = "..//" + System.Configuration.ConfigurationManager.AppSettings["VideoGalleryPath"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindVideos();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(fupldImage.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(fupldImage.FileName).ToLower();
            String[] allowedExtensions = { ".mp4", ".mpg", ".ogv", ".3gp", ".3g2" }; // ".m4v", ".mov", ".wmv",".avi",
            if (Array.IndexOf(allowedExtensions, fileExtension) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please upload a video file.');", true);
            }
            else
            {
                fupldImage.SaveAs(Server.MapPath(FolderPath) + "//" + fupldImage.FileName);
                BindVideos();
            }
        }
    }


    protected void rptImages_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "DeleteI")
        {
            string FileName = Convert.ToString(e.CommandArgument);
            if (File.Exists(Server.MapPath(FolderPath + "//" + FileName)))
                File.Delete(Server.MapPath(FolderPath + "//" + FileName));

            BindVideos();
        }
    }

    public string GetImageFromByte(object VN)
    {
        try
        {
            //string VideoPath = (string)VP;
            string VideoName = (string)VN;

            if (!Directory.Exists(Server.MapPath(FolderPath)))
                return (FolderPath + "//" + VideoName);
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void BindVideos()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("VideoName", typeof(string));
        dt.Columns.Add("VideoPath", typeof(string));

        if (Directory.Exists(Server.MapPath(FolderPath)))
        {
            string[] files = Directory.GetFiles(Server.MapPath(FolderPath));
            foreach (string fp in files)
            {
                DataRow dr = dt.NewRow();
                dr["VideoName"] = fp.Substring(fp.LastIndexOf("\\") + 1);
                dr["VideoPath"] = FolderPath.Replace("/", "//") + fp.Substring(fp.LastIndexOf("\\")).Replace("\\", "//");
                dt.Rows.Add(dr);
            }
        }

        rptImages.DataSource = dt;
        rptImages.DataBind();
    }
}