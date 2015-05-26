<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/SiteMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript">
        /* $(function () {
            //******Redirect to registration page on click of signup
            document.getElementById("btnRegister").onclick = function () {
                window.location.href = "Registeration.aspx";
            };
        });*/

        function ValidateLogin()
        {           
            if($("#txtUserName").value =="" && $("#txtPassword").value =="" && $("#txtEmailAddress").value =="")
            {
                $(".ErrorMsg").innerHTML = "*";
                return false;
            }
            else if ($("#txtEmailAddress").value == "" && !($("#txtUserName").value != "" && $("#txtPassword").value != ""))
            {
                if($("#txtUserName").value == "")
                    $("#lblErrorUserName").innerHTML = "*";
                else
                    $("#lblErrorPassword").innerHTML = "*";
                return false;
            }
            else
            {
                $(".ErrorMsg").innerHTML = "";
                return false;
            }

            //post req to payment method, specify redirection URL
        }
    </script>
    <div>
        <h3>Login
        </h3>
    </div>
    <div style="width: 100%">
        <div style="text-align: center;">
            <table style="width: 30%;">
                <tr>
                    <td>User Name:</td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="lblErrorUserName" runat="server" CssClass="ErrorMsg"></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 10px;"></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="lblErrorPassword" runat="server" CssClass="ErrorMsg"></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td colspan="3" style="height: 10px; text-align:center;">
                     OR
                    </td>
                </tr>
                <tr  id="tr2" runat="server">
                    <td>
                        Enter email id:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailAddress" runat="server" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblErrorEmail" runat="server" CssClass="ErrorMsg"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <%--<asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-primary" CausesValidation="false" />--%>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" class="btn btn-primary" OnClientClick="return ValidateLogin();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>

