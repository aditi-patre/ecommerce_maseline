<%@ WebHandler Language="C#" Class="FileUploadHandler" %>
 
using System;
using System.Web;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string[] arr = context.Request.QueryString["ImageID"].Split('|');

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    //Save as existing
                    string fileName = context.Server.MapPath("..//"+arr[1].Substring(arr[1].IndexOf("images")));
                    //string fname = context.Server.MapPath("~/uploads/" + file.FileName);
                    file.SaveAs(fileName);
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully!");
                
            }
        }
        catch (Exception ex)
        { }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}