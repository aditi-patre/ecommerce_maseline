<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function btnShowCats() {
            ShowPopUp();
            $('#dvCataLogList').append(document.getElementById('<%=hdnCategoryListing.ClientID%>').value);
        }
        function ShowPopUp() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";
            document.getElementById("dialog-form").style.display = "block";
        }

        function HidePopUp() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";
            document.getElementById("dialog-form").style.display = "none";
        }

        

        function isNumber(evt) {

            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
                
    </script>
    <section>
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                    <div class="left-sidebar" style="background-color: #CFD4D0">
                        <h2>CATALOG</h2>
                        <asp:Literal runat="server" ID="ltList"></asp:Literal><asp:HiddenField ID="hdnCategoryListing" runat="server" />
                        <div style="padding-top: 20px;">
                            <div class="shipping text-center" style="border: 1px solid lightgrey;">
                                <h2>Featured News</h2>
                                <asp:Literal ID="ltFeaturedNews" runat="server"></asp:Literal>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9" style="padding: 0% 1% 1% 1%;">
                    <div class="listingtbl">
                        <div class="col-sm-9" style="padding: 0.5% 0% 0% 1.5%;">
                            <h2>CONTACT US</h2>
                        </div>
                        <div style="width: 115%">
                            <div class="col-sm-15 pull-left" style="font-size: 15px !important;">
                                <br />
                                <div style="font-family: Arial; font-size: 14px; font-weight: bold; color: #35437D; padding: 0.5% 0% 0% 1.5%;">Subscribe to our mailing list!</div>
                                <br />
                                <div class="form-group col-md-3" style="text-align: right; padding-right: 5%;">
                                    <label>Name</label>
                                </div>
                                <div class="form-group col-md-5">
                                    <input type="text" id="txtName" class="form-control" required="required" placeholder="Name" runat="server" />
                                </div>
                                <div class="form-group col-md-1" style="padding-left: 2%; padding-top: 0.5%;">
                                    <label id="lblErrorName" runat="server" class="ui-state-error-text"></label>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3" style="text-align: right; padding-right: 5%;">
                                    <label>Company</label>
                                </div>
                                <div class="form-group col-md-5">
                                    <input type="text" id="txtCompany" class="form-control" required="required" placeholder="Company" runat="server" />
                                </div>
                                <div class="form-group col-md-1" style="padding-left: 2%; padding-top: 0.5%;">
                                    <label id="lblErrorCompany" runat="server" class="ui-state-error-text"></label>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3" style="text-align: right; padding-right: 5%;">
                                    <label>Phone</label>
                                </div>
                                <div class="form-group col-md-5">
                                    <input type="text" id="txtPhone" class="form-control" required="required" onkeypress="return isNumber(event)" placeholder="Phone" runat="server" />
                                </div>
                                <div class="form-group col-md-1" style="padding-left: 2%; padding-top: 0.5%;">
                                    <label id="lblErrorPhone" runat="server" class="ui-state-error-text"></label>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3" style="text-align: right; padding-right: 5%;">
                                    <label>Email</label>
                                </div>
                                <div class="form-group col-md-5">
                                    <input type="text" id="txtEmail" class="form-control" required="required" placeholder="Email" runat="server" />
                                </div>
                                <div class="form-group col-md-1" style="padding-left: 2%; padding-top: 0.5%;">
                                    <label id="lblErrorEmail" runat="server" class="ui-state-error-text"></label>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3">
                                </div>
                                <div class="form-group col-md-5">
                                    <div style="width: 111%;">Reason for contacting including part numbers if applicable* </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3">
                                </div>
                                <div class="form-group col-md-5">
                                    <textarea id="txtEnquiry" class="form-control" required="required" rows="3" cols="4" runat="server"></textarea>
                                </div>
                                <div class="form-group col-md-1">
                                    <label id="lblErrorEnquiry" runat="server" class="ui-state-error-text" style="padding-left: 2%; padding-top: 0.5%;"></label>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3">
                                </div>
                                <div class="form-group col-md-5">
                                    <div style="width: 111%;">Letters are not case-sensitive </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3">
                                </div>
                                <div class="form-group col-md-4">
                                    <div>What Code is in the image?</div>
                                    <input type="text" id="txtimgcode" class="form-control" required="required" placeholder="Captcha" runat="server" />
                                    <br />
                                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/Captcha.aspx" />
                                </div>
                                <div class="form-group col-md-1">
                                    <label id="lblErrorCaptcha" runat="server" class="ui-state-error-text" style="vertical-align: bottom;"></label>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group col-md-3">
                                </div>
                                <div class="form-group col-md-5">
                                    <asp:ImageButton ImageUrl="~/images/Submit.PNG" ID="btnLogin" runat="server" OnClick="btnSubmit_Click" Width="150px" />&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ImageUrl="~/images/Reset.PNG" ID="btnReset" runat="server" OnClick="btnReset_Click" Width="150px" formnovalidate />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div id="divBackground" style="position: fixed; z-index: 998; height: 100%; width: 100%; top: 0; left: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; display: none">
    </div>
    <div id="dialog-form" class="dvdialog-form" style="background-color: #CFD4D0; height: 800px; z-index: 999; position: absolute; color: #000000; filter: alpha(opacity=60); opacity: 1.0; -moz-opacity: 1.0; left: 35%; top: 12%; display: none;">
        <div style="background-color: #CFD4D0; padding-bottom: 15px; padding-left: 15px; padding-right: 15px; padding-top: 15px; width: 100%; height: 90%; overflow: auto; overflow-x: hidden;">
            <div>
                <div style="height: 30px; text-align: center;">CATALOG</div>
                <div style="float: right; margin-top: -25px;">
                    <a href="#">
                        <img src="images/delete_icon.png" onclick="HidePopUp()" alt="" /></a>
                </div>
            </div>
            <br />
            <div id="dvCataLogList" style="height: 100%;" runat="server"></div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>

