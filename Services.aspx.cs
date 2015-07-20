using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Services : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsPostBack)
        //{
            if(Request.QueryString["p"] != null && Request.QueryString["p"] != "")
            {
                string type = Request.QueryString["p"];
                ClientScript.RegisterStartupScript(GetType(), "LoadService", "LoadService('"+type.Replace('_',' ')+"')", true);
            }
        //}
    }
}