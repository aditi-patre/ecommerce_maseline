<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCart.aspx.cs" Inherits="ViewCart" MasterPageFile="~/ParentMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <script type="text/javascript">
        function ChangeQty(param) {       
            var c1 = param.split('|')[0];
            var txtQty = document.getElementById(c1);
            if (isNaN(parseInt(txtQty.value)))
                document.getElementById(c1.replace("txtQuantity", "lblTotalPrice")).innerHTML = 0.00;
            else {
                var txtPrice = {};
                txtPrice.Qty = txtQty.value;
                txtPrice.Index = param.split('|')[1];
                $.ajax({
                    type: "POST",
                    url: "QueryPage.aspx/ComputePrice",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(txtPrice),
                    dataType: "json",
                    success: function (result) {
                        var Success = JSON.parse(result.d).IsSuccess;
                        if (Success == true) {
                            var Price = JSON.parse(result.d).Message;
                            document.getElementById(c1.replace("txtQuantity", "lblUnitPrice")).innerHTML = Price;
                            document.getElementById(c1.replace("txtQuantity", "lblTotalPrice")).innerHTML = Price * txtQty.value;
                        } else {
                            document.getElementById(c1.replace("txtQuantity", "lblTotalPrice")).innerHTML = 0;
                            alert(JSON.parse(result.d).Message);
                        }
                        document.getElementById(c1.replace("txtQuantity", "lblTotalCartAmt").replace("_0", "")).innerHTML = JSON.parse(result.d).TotalAmt;
                    },
                    error: function () {
                        alert("Some error!!");
                    }
                });
            }
        }
    </script>

    <section>
        <div class="container">
            <div>
                <h2>SHOPPING CART</h2>
            </div>
            <div class="breadcrumbs">
                <ol class="breadcrumb">
                    <li><a href="#">Home</a></li>
                    <li class="active">Shopping Cart</li>
                </ol>
            </div>
            <%--CART ITEMS --%>

            <asp:GridView runat="server" ID="gvShoppingCart" AutoGenerateColumns="false" EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" CellPadding="5"
                CssClass="table table-condensed" OnRowCreated="gvShoppingCart_RowCreated"
                ShowFooter="true" DataKeyNames="ProductId" OnRowDataBound="gvShoppingCart_RowDataBound" OnRowCommand="gvShoppingCart_RowCommand">
                <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                <FooterStyle CssClass="cart_menu"></FooterStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" ControlStyle-BorderColor="White">
                        <HeaderStyle CssClass="image" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="cart_product" BorderColor="White" />
                        <ItemTemplate>
                            <asp:Image ID="imgProduct" runat="server" ToolTip="ProductImage" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ControlStyle-BorderColor="White" HeaderStyle-Width="30%" >
                        <HeaderStyle CssClass="description" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" CssClass="cart_description" BorderColor="White"  Width="30%"/>
                        <ControlStyle CssClass="cart_description" BorderColor="White" />
                        <ItemTemplate>
                            <h4>
                                <asp:LinkButton ID="lbtnProdName" runat="server" Text='<%# Eval("Prod.Name") %>' CommandName="ShowProductDetails" CommandArgument='<%# Eval("ProductID") %>'></asp:LinkButton>
                            </h4>
                            <p id="lblProductCode" runat="server"><%# string.Format("{0}{1}","$", Eval("Prod.ProductCode")) %></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" ControlStyle-BorderColor="White">
                        <ItemStyle BorderColor="White" VerticalAlign="Middle" HorizontalAlign="Left"  />
                        <ControlStyle BorderColor="White"  />
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" Text='<%# Eval("Price") %>' runat="server" DataFormatString="{0:C}"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" ControlStyle-BorderColor="White" HeaderStyle-Width="20%">
                        <ItemStyle BorderColor="White" VerticalAlign="Middle"  />
                        <ControlStyle BorderColor="White"  />
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtQuantity" Columns="5" Text='<%# Eval("Quantity") %>'></asp:TextBox><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" ControlStyle-BorderColor="White" HeaderStyle-Width="20%" >
                        <ItemStyle BorderColor="White" VerticalAlign="Middle" />
                        <ControlStyle BorderColor="White" />
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPrice" Text='<%# Eval("TotalPrice") %>' runat="server" DataFormatString="{0:C}"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="lbtnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ProductId") %>'
                                OnClientClick="if (confirm('Are you sure you want to remove this item from cart?') == false) return false;"
                                ImageUrl="~/images/delete_icon.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <span>Total Amount: 
                                <asp:Label ID="lblTotalCartAmt" runat="server" Text='<%# Eval("TotalPrice") %>' DataFormatString="{0:C}"></asp:Label>
                            </span>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <input class="btn btn-primary" type="button" value="Back" onclick="history.go(-1);" />
            <asp:Button runat="server" ID="btnCheckoutCart" Text="Checkout" class="btn btn-primary" OnClick="btnCheckoutCart_Click" />
        </div>
    </section>
</asp:Content>
