using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    bool RedirctToPaymntGateway = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Checkout"] != null)
        {
            if (Request.QueryString["Checkout"].ToLower() == "y")
            {
                //tr1.Style.Add(HtmlTextWriterStyle.Display, "block");
                //tr2.Style.Add(HtmlTextWriterStyle.Display, "block");
                dvEmailAddress.Style.Add(HtmlTextWriterStyle.Display, "block");
                btnLogin.Style.Add(HtmlTextWriterStyle.Display, "none");
                btnSubmit.Style.Add(HtmlTextWriterStyle.Display, "block");
                RedirctToPaymntGateway = true;
            }
        }
        else
        {
            dvEmailAddress.Style.Add(HtmlTextWriterStyle.Display, "none");
            btnLogin.Style.Add(HtmlTextWriterStyle.Display, "block");
            btnSubmit.Style.Add(HtmlTextWriterStyle.Display, "none");
            //tr1.Style.Add(HtmlTextWriterStyle.Display, "none");
            //tr2.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (true)
        {
            if (txtUserName.Value != "" && txtPassword.Value != "")
            {
                //Button btnSignUp = (Button)this.Master.FindControl("btnSignUp");
                //Button btnLogin = (Button)this.Master.FindControl("btnLogin");
                //System.Web.UI.HtmlControls.HtmlAnchor btnLogin = (System.Web.UI.HtmlControls.HtmlAnchor)this.Master.FindControl("btnLogin");
                //Label lblUser = (Label)this.Master.FindControl("lblUser");
                //ImageButton btnLogOut = (ImageButton)this.Master.FindControl("btnLogOut");

                int i = UserInfo.GetUserIdByUsernameAndPassword(txtUserName.Value, txtPassword.Value);
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "alert('Invalid username/password');", true);
          
                    //lblUser.Style.Add(HtmlTextWriterStyle.Display, "none");
                    //btnLogOut.Visible = false;
                    btnLogin.Visible = true;
                }
                else
                {
                    if (HttpContext.Current.Session["User"] != null)
                    {
                        HttpContext.Current.Session["User"] = txtUserName.Value + "|" + i.ToString();
                        
                        //lblUser.Style.Add(HtmlTextWriterStyle.Display, "block");
                        //lblUser.Text = "Welcome " + txtUserName.Value;
                        btnLogin.Visible = false;
                        //btnLogOut.Visible = true;
                       
                    }
                    //On logging in check the url, if user came via checkout then save order else just redirect user to home page
                    //Save Order in DB
                    if (RedirctToPaymntGateway)
                        SaveOrder();
                    else
                        Response.Redirect("Home.aspx");
                }
            }
            else if(txtEmailAddress.Value != "" && RedirctToPaymntGateway)
            {
                SaveOrder();
            }
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
}