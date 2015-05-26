<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCart.aspx.cs" Inherits="ViewCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--CART ITEMS --%>

            <asp:GridView runat="server" ID="gvShoppingCart" AutoGenerateColumns="false" EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" CellPadding="5"
                ShowFooter="true" DataKeyNames="ProductId" OnRowDataBound="gvShoppingCart_RowDataBound" OnRowCommand="gvShoppingCart_RowCommand">
                <HeaderStyle HorizontalAlign="Left" BackColor="#3D7169" ForeColor="#FFFFFF" />
                <FooterStyle HorizontalAlign="Right" BackColor="#6C6B66" ForeColor="#FFFFFF" />
                <AlternatingRowStyle BackColor="#F8F8F8" />
                <Columns>
                    <asp:BoundField DataField="Code" HeaderText="Product Code" />
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtQuantity" Columns="5" Text='<%# Eval("Quantity") %>'></asp:TextBox><br />
                            <%--<asp:LinkButton runat="server" ID="btnRemove" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ProductId") %>' Style="font-size: 12px;"></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" Text='<%# Eval("Price") %>' runat="server" DataFormatString="{0:C}"></asp:Label>
                            <%-- <asp:Label ID="lblPricing" Text='<%# Eval("Pricing") %>' runat="server" style="display:none;"></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:BoundField DataField="UnitPrice" HeaderText="Price" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />--%>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPrice" Text='<%# Eval("TotalPrice") %>' runat="server" DataFormatString="{0:C}"></asp:Label>
                          <%--  <asp:ImageButton ID="lbtnRemove" runat="server"  OnClick="lbtnRemove_Click" 
                                OnClientClick="if (confirm('Are you sure you want to remove this item from cart?') == false) return false;"
                                ImageUrl="~/images/delete_icon.png"/>--%>
                            <asp:ImageButton ID="lbtnRemove" runat="server"  CommandName="Remove" CommandArgument='<%# Eval("ProductId") %>' 
                                OnClientClick="if (confirm('Are you sure you want to remove this item from cart?') == false) return false;"
                                ImageUrl="~/images/delete_icon.png"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="TotalPrice" HeaderText="Total" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:C}" />--%>
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
            <asp:Button runat="server" ID="btnCheckoutCart" Text="Checkout" class="btn btn-primary"  OnClick="btnCheckoutCart_Click"/>
            <%--            <asp:Button runat="server" ID="btnCheckoutCart" Text="Checkout" class="btn btn-primary"
                OnClientClick='<%# String.Format("javascript:return btnCheckoutCart_Client()")%>' />--%>
        </div>

    </form>
</body>
</html>
