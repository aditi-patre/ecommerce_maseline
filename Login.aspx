<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/SiteMaster.master" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">  
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
      <script type="text/javascript">
          /* $(function () {
              //******Redirect to registration page on click of signup
              document.getElementById("btnRegister").onclick = function () {
                  window.location.href = "Registeration.aspx";
              };
          });*/

          function ValidateLogin() {
              if (document.getElementById("MainContentPlaceHolder_txtUserName").value == "" && document.getElementById("MainContentPlaceHolder_txtPassword").value == "" && document.getElementById("MainContentPlaceHolder_txtEmailAddress").value == "") {
                  document.getElementById("MainContentPlaceHolder_lblErrorUserName").innerHTML = "*";
                  document.getElementById("MainContentPlaceHolder_lblErrorPassword").innerHTML = "*";
                  document.getElementById("MainContentPlaceHolder_lblErrorEmail").innerHTML = "*";
                  return false;
              }
              else if (document.getElementById("MainContentPlaceHolder_txtEmailAddress").value == "" && !(document.getElementById("MainContentPlaceHolder_txtUserName").value != "" && document.getElementById("MainContentPlaceHolder_txtPassword").value != "")) {
                  if (document.getElementById("MainContentPlaceHolder_txtUserName").value == "")
                      document.getElementById("MainContentPlaceHolder_lblErrorUserName").innerHTML = "*";
                  else
                      document.getElementById("MainContentPlaceHolder_lblErrorPassword").innerHTML = "*";
                  return false;
              }
              else if (document.getElementById("MainContentPlaceHolder_txtEmailAddress").value != "") {
                  var EmailPattern = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
                  if (EmailPattern.test(document.getElementById("MainContentPlaceHolder_txtEmailAddress").value) == false) {
                      document.getElementById("MainContentPlaceHolder_lblErrorEmail").innerHTML = "Invalid";
                  }
              }
              else {
                  document.getElementById("MainContentPlaceHolder_lblErrorUserName").innerHTML = "";
                  document.getElementById("MainContentPlaceHolder_lblErrorPassword").innerHTML = "";
                  document.getElementById("MainContentPlaceHolder_lblErrorEmail").innerHTML = "";
                  return true;
              }
              //post req to payment method, specify redirection URL
          }
    </script>

    <section id="form"><!--form-->
		<div class="container">
			<div class="row">
				<div class="col-sm-4 col-sm-offset-1">
					<div class="login-form"><!--login form-->
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
                        <asp:ImageButton ImageUrl="images/home/login_button.png" ID="btnLogin" runat="server" OnClick="btnLogin_Click" OnClientClick="return ValidateLogin();" Width="150px"/>
                        <%--<asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" OnClientClick="return ValidateLogin();" Width="100px" />--%>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnLogin_Click" CssClass="btn btn-primary" OnClientClick="return ValidateLogin();" Width="100px" />
					</div><!--/login form-->
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
	</section><!--/form-->
</asp:Content>



