<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PaymentSuccess.aspx.cs" Inherits="PaymentSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function f1() {
        }
    </script>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Your payment of "></asp:Label>
                &nbsp;<asp:Label ID="lblPaymentAmt" runat="server"></asp:Label>&nbsp;<asp:Label ID="Label5"
                    runat="server" Text=" was successful."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Your transaction ID is: "></asp:Label>
                <asp:Label ID="lblTranCode" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

