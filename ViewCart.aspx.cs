using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

//public delegate void dBindCartHandler(); 
public partial class ViewCart : System.Web.UI.Page
{
    //public event dBindCartHandler eBindCart;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindCart();
    }
    public void BindCart()
    {
        try
        {
            ShoppingCart objCart = ShoppingCart.Instance;
            gvShoppingCart.DataSource = objCart.Items;
            gvShoppingCart.DataBind();
            if (objCart.Items.Count == 0)
                btnCheckoutCart.Visible = false;
            else
                btnCheckoutCart.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvShoppingCart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtQty = e.Row.FindControl("txtQuantity") as TextBox;
            if (txtQty != null)
            {
                string id = txtQty.ClientID+"|"+e.Row.RowIndex;
                txtQty.Attributes.Add("onKeyUp", "ChangeQty('" + id + "')");
            }            
            //txtQty.Attributes.Add("onkeyup", "javascript:ChangeQty("++")");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "test("+a+")", true);
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalCartAmt = e.Row.FindControl("lblTotalCartAmt") as Label;
            lblTotalCartAmt.Text = ShoppingCart.Instance.GetSubTotal().ToString();
        }
    }

    protected void gvShoppingCart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int ProductID = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "Remove")
        {
            ShoppingCart.Instance.RemoveItem(ProductID);
            BindCart();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Item removed from cart');", true);
        }
    }

    protected void btnCheckoutCart_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["User"] == null)
        {
            Response.Redirect("Login.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please login to continue');", true);
            
        }
        else
        {
           
        }
    }
}