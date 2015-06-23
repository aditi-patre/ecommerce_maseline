using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        ShoppingCart objCart = ShoppingCart.Instance;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtQty = e.Row.FindControl("txtQuantity") as TextBox;
            if (txtQty != null)
            {
                string id = txtQty.ClientID + "|" + e.Row.RowIndex;
                txtQty.Attributes.Add("onKeyUp", "ChangeQty('" + id + "')");
            }

            /* Map the path of product image*/
            Image img = (Image)e.Row.FindControl("imgProduct");
            //Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");
            string ProdImagePath = System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "//" + gvShoppingCart.DataKeys[e.Row.RowIndex].Value.ToString() + "//" + objCart.Items[e.Row.RowIndex].Prod.ImageName;
            if (File.Exists(Server.MapPath(ProdImagePath)))
                img.ImageUrl = ProdImagePath;
            else
                img.Style.Add(HtmlTextWriterStyle.Display, "none");
            //***

            LinkButton lbtnProdName = (LinkButton)e.Row.FindControl("lbtnProdName");
            lbtnProdName.Text = objCart.Items[e.Row.RowIndex].Prod.Name;

            //ImageButton lbtnRemove = e.Row.FindControl("lbtnRemove") as ImageButton;
            //if (lbtnRemove != null)
            //{
            //    lbtnRemove.Attributes.Add("OnClientClick", "RemoveCartItem('" + gvShoppingCart.DataKeys[e.Row.RowIndex].Value + "')");
            //}

            //txtQty.Attributes.Add("onkeyup", "javascript:ChangeQty("++")");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "test("+a+")", true);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
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
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Item removed from cart');", true);
            Response.Redirect(Convert.ToString(Session["PopUpParentUrl"]));
        }
    }
    protected void gvShoppingCart_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                e.Row.Attributes.Add("onmouseover", "this.className='Row'");
                e.Row.Attributes.Add("onmouseout", "this.className='alt'");
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void btnCheckoutCart_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["User"] == null)
        {
            Response.Redirect("Login.aspx?Checkout=Y");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Please login to continue');", true);

        }
        else
        {
            SaveOrder();
        }
    }

    private void SaveOrder()
    {
        if (HttpContext.Current.Session["ShoppingCart"] != null)
        {
            Orders order = new Orders();
            order.OrderID = 0;
            order.UserID = Convert.ToInt32(Convert.ToString(HttpContext.Current.Session["User"]).Split('|')[1]);
            order.UserEmail = "";
            order.Save();

            DataTable dtCart = new DataTable();
            dtCart.Columns.Add("OrderID", typeof(int));
            dtCart.Columns.Add("ProductID", typeof(int));
            dtCart.Columns.Add("Qty", typeof(int));
            dtCart.Columns.Add("Price", typeof(decimal));           

            foreach (CartItem objItem in ShoppingCart.Instance.Items)
            {
                dtCart.Rows.Add(order.OrderID, objItem.ProductId, objItem.Quantity, objItem.UnitPrice);
            }

            OrderDetails OrderDtls = new OrderDetails();
            OrderDtls.Insert(dtCart);
        }
    }

    protected void lbtnRemove_Click(object sender, EventArgs e)
    {
        ImageButton lbtnRemove = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnRemove.NamingContainer;

        int ProductId = Convert.ToInt32(gvShoppingCart.DataKeys[gvRow.RowIndex].Value);
        ShoppingCart.Instance.RemoveItem(ProductId);
        BindCart();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Item removed from cart');", true);
        Response.Redirect("ViewCart.aspx");
    }
}