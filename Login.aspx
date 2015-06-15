<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/SiteMaster.master" ValidateRequest="false" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function ValidateLogin() {
            if (document.getElementById('<%=txtUserName.ClientID %>').value == "" && document.getElementById('<%=txtPassword.ClientID %>').value == "" && document.getElementById('<%=txtEmailAddress.ClientID %>').value == "") {
                document.getElementById('<%=lblErrorUserName.ClientID %>').innerHTML = "*";
                document.getElementById('<%=lblErrorPassword.ClientID %>').innerHTML = "*";
                document.getElementById('<%=lblErrorEmail.ClientID %>').innerHTML = "*";
                return false;
            }
            else if (document.getElementById('<%=txtEmailAddress.ClientID %>').value == "" && !(document.getElementById('<%=txtUserName.ClientID %>').value != "" && document.getElementById('<%=txtPassword.ClientID %>').value != "")) {
                if (document.getElementById('<%=txtUserName.ClientID %>').value == "")
                    document.getElementById('<%=lblErrorUserName.ClientID %>').innerHTML = "*";
                else
                    document.getElementById('<%=lblErrorPassword.ClientID %>').innerHTML = "*";
                return false;
            }
            else if (document.getElementById('<%=txtEmailAddress.ClientID %>').value != "") {
                var EmailPattern = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
                if (EmailPattern.test(document.getElementById('<%=txtEmailAddress.ClientID %>').value) == false) {
                    document.getElementById('<%=lblErrorEmail.ClientID %>').innerHTML = "Invalid";
                }
                return false;
            }
            else {
                document.getElementById('<%=lblErrorUserName.ClientID %>').innerHTML = "";
                document.getElementById('<%=lblErrorPassword.ClientID %>').innerHTML = "";
                document.getElementById('<%=lblErrorEmail.ClientID %>').innerHTML = "";
                
              }
              //post req to payment method, specify redirection URL
  }
    </script>

    <section id="form">
        <!--form-->
        <div class="container">
            <div class="row">
                <div class="col-sm-4 col-sm-offset-1">
                    <div class="login-form">
                        <!--login form-->
                        <h2>Login to your account</h2>
                        <%--<form action="#">
							<input type="text" placeholder="Name" />
							<input type="email" placeholder="Email Address" />
							<span>
								<input type="checkbox" class="checkbox"> 
								Keep me signed in
							</span>
							<button type="submit" class="btn btn-default">Login</button>
						</form>--%>
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
                        <asp:ImageButton ImageUrl="images/home/login_button.png" ID="btnLogin" runat="server" OnClick="btnLogin_Click" OnClientClick="return ValidateLogin()" Width="150px" />
                        <%--  <asp:Button runat="server" ID="btnValidatelogin" style="display:none;" OnClick="btnLogin_Click"/>
                        <asp:Image ID="btnLogin" onclick="javascript:ValidateLogin();" runat="server" ImageUrl="images/home/login_button.png" ToolTip="Login" ImageAlign="AbsMiddle" style="width:150px" />--%>
                        <%--<asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" OnClientClick="return ValidateLogin();" Width="100px" />--%>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnLogin_Click" CssClass="btn btn-primary" OnClientClick="ValidateLogin()" Width="100px" />
                    </div>
                    <!--/login form-->
                </div>
                <div id="dvEmailAddress" runat="server">
                    <div class="col-sm-1">
                        <h2 class="or">OR</h2>
                    </div>
                    <div class="col-sm-4">
                        <div class="signup-form">
                            <!--sign up form-->

                            <h2>Enter your email address!</h2>
                            <input type="text" placeholder="Email Address" id="txtEmailAddress" runat="server" /><asp:Label ID="lblErrorEmail" runat="server" Style="color: red; float: left;"></asp:Label>
                            <%-- <button type="submit" class="btn btn-default">Submit</button>--%>
                        </div>
                        <!--/sign up form-->
                    </div>
                </div>

            </div>
        </div>
    </section>
    <!--/form-->
</asp:Content>



