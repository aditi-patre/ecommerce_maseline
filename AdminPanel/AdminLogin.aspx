<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminPanel_AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ValidateLogin() {
            if (document.getElementById('<%=txtUserName.ClientID %>').value == "" && document.getElementById('<%=txtPassword.ClientID %>').value == "") {
                 document.getElementById('<%=lblErrorUserName.ClientID %>').innerHTML = "*";
                 document.getElementById('<%=lblErrorPassword.ClientID %>').innerHTML = "*";
                 return false;
             }
             else if (!(document.getElementById('<%=txtUserName.ClientID %>').value != "" && document.getElementById('<%=txtPassword.ClientID %>').value != "")) {
                 if (document.getElementById('<%=txtUserName.ClientID %>').value == "")
                    document.getElementById('<%=lblErrorUserName.ClientID %>').innerHTML = "*";
                else
                    document.getElementById('<%=lblErrorPassword.ClientID %>').innerHTML = "*";
                return false;
            }
            else {
                document.getElementById('<%=lblErrorUserName.ClientID %>').innerHTML = "";
                document.getElementById('<%=lblErrorPassword.ClientID %>').innerHTML = "";
            }
             
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="form">
        <!--form-->
        <div class="container">
            <div class="row">
                <div class="col-sm-4 col-sm-offset-1">
                    <div class="login-form">
                        <!--login form-->
                        <h2>Login to your account</h2>
                        <div style="float: left; width: 90%;">
                            <input type="text" placeholder="User Name" id="txtUserName" runat="server" />
                        </div>
                        <div style="float: left;">
                            <asp:Label ID="lblErrorUserName" runat="server" Style="color: red; float: left;"></asp:Label>
                        </div>
                        <div style="float: left; width: 90%;">
                            <input type="password" placeholder="Password" id="txtPassword" runat="server" />
                        </div>
                        <div style="float: left;">
                            <asp:Label ID="lblErrorPassword" runat="server" Style="color: red; float: left;"></asp:Label>
                        </div>
                        <br />
                        <span style="float: left; width: 100%;">
                            <input type="checkbox" class="checkbox" />
                            Keep me signed in
                        </span>
                        <asp:ImageButton ImageUrl="../images/home/login_button.png" ID="btnLogin" runat="server" OnClick="btnLogin_Click" OnClientClick="return ValidateLogin()" Width="150px" />

                    </div>
                    <!--/login form-->
                </div>
            </div>
        </div>
    </section>
</asp:Content>

