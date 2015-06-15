using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class ParentMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {

    }

    protected void btnShoppingCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewCart.aspx");
    }
    protected void btnSearchProduct_Click(object sender, EventArgs e)
    {
        Product objProduct = new Product();
        objProduct.ProductCode = hdnPCode.Value;
        DataTable dt = objProduct.GetList(hdnPCode.Value);
        if (dt != null && dt.Rows.Count > 0)
        {
            Response.Redirect("ProductDetails.aspx?PCode=" + dt.Rows[0]["ProductID"].ToString());
        }
    }

}
