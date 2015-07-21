<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="VideoGallery.aspx.cs" Inherits="VideoGallery" %>

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
        window.onload = function () {
            
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
                <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9" style="padding-left: 2%; padding-bottom: 2%;">
                    <div class="col-sm-9">
                        <h2>VIDEO GALLERY</h2>
                    </div>
                    <div style="width: 96%;">
                        <div class="col-md-15 pull-left" style="margin-top: 3%;">
                            <asp:Literal ID="ltVideoGallery" runat="server"></asp:Literal>
                            <p>
                                <a href="https://www.youtube.com/channel/UCsPQTAe2Qrau1e2TumlaATQ?sub_confirmation=1" target="youtube">SUBSCRIBE NOW to Masline Electronics YouTube Channel</a>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%--Popup for category listing--%>
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

