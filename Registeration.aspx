<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Registeration.aspx.cs" Inherits="Registeration" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript">
        $(function () {
            var obj = {};
            $("#btnRegister").button().on("click", function () {
                obj.UserName = $("#txtUserName").val();
                obj.Password = $("#txtPassword").val();
                if (obj.UserName != "" && obj.Password != "") {
                    $.ajax({
                        type: "POST",
                        url: "QueryPage.aspx/Register",
                        contentType: "application/json; charset:utf-8",
                        data: JSON.stringify(obj),
                        dataType: "json",
                        success: AjaxSucceeded,
                        error: AjaxFailed
                    });
                }
                else
                {
                    alert("Please enter the username & password");
                }
            });//btnRegister
            function AjaxSucceeded(result) {
                var output = JSON.parse(result.d);
                alert(output.Message+" please login to continue");
                window.location.replace("Home.aspx");
            }
            function AjaxFailed(result) {
                alert('Failed to register');
            }
        });//function
    </script>
    <div>
        <h3>Registration Form
        </h3>
    </div>
    <div style="width: 100%">
        <div style="text-align: center;">
            <table style="width: 20%;">
                <tr>
                    <td>User Name:</td>
                    <td>
                        <input type="text" id="txtUserName" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 10px;"></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td>
                        <input type="password" id="txtPassword"/></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 10px;"></td>
                </tr>
                <%--<tr>
            <td>Full Name:</td>
            <td><input type="text" id="txtFullName"/></td>
        </tr>
        <tr>
            <td>Email Id:</td>
            <td><input type="text" id="txtEmailId"/></td>
        </tr>--%>
                <tr>
                    <td colspan="2">
                        <input type="button" id="btnRegister" value="Register" class="btn btn-info" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
