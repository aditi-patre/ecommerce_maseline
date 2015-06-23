<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="AdminPanel_AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="ja">
        function ShowImage1(objImg) {
            var UploaderPopUp;
            document.getElementById('<%=hdnSliderImageID.ClientID%>').value = objImg.id;
            UploaderPopUp = $("#UploaderPopUp").dialog({
                autoOpen: false,
                height: 200,
                width: 550,
                modal: true,
                title: "Slider Image"
            });
            UploaderPopUp.dialog("open");
        }

        function Upload(imgID) {
            var file = document.getElementById('<%=fupBanner.ClientID %>');
            var fd = new FormData();
            fd.append(file.files[0].name, file.files[0]);
            fd.append("ImageID", imgID);

            $.ajax({
                url: "FileUploadHandler.ashx?ImageID=" + imgID,
                type: "POST",
                data: fd,
                contentType: false,
                processData: false,
                success: function (result) { alert(result); },
                error: function (err) {
                    alert(err.statusText)
                }
            });
            return true;
        }

        function ShowImage(objImg) {
            var UploaderPopUp;
            document.getElementById('<%=hdnSliderImageID.ClientID%>').value = objImg.id;
            var Img = objImg.id + "|" + objImg.src;
            UploaderPopUp = $("#UploaderPopUp").dialog({
                autoOpen: false,
                height: 220,
                width: 550,
                modal: true,
                title: "Slider Image",
                buttons: {
                    "Upload": function () {
                        Upload(Img);
                    }
                }
            });
            UploaderPopUp.dialog("open");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="slider">
        <!--slider-->
        <div class="container">
            <div class="row">
                <div class="col-sm-7" style="width:78%;">
                    <div id="slider-carousel" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#slider-carousel" data-slide-to="0" class="active"></li>
                            <li data-target="#slider-carousel" data-slide-to="1"></li>
                            <li data-target="#slider-carousel" data-slide-to="2"></li>
                        </ol>

                        <div class="carousel-inner">
                            <div class="item active">
                                <div class="col-sm-6">
                                    <img src="../images/home/banner1.jpg" class="girl img-responsive" title="Click to Change" alt="Click to Change" onclick="ShowImage(this)" id="SliderImg1" runat="server" />
                                </div>
                            </div>
                            <div class="item ">

                                <div class="col-sm-6">
                                    <img src="../images/home/banner2.png" class="girl img-responsive" title="Click to Change" alt="Click to Change" onclick="ShowImage(this)" id="SliderImg2" runat="server" />
                                </div>
                            </div>

                            <div class="item ">

                                <div class="col-sm-6">
                                    <img src="../images/home/banner3.png" class="girl img-responsive" title="Click to Change" alt="Click to Change" onclick="ShowImage(this)" id="SliderImg3" runat="server" />
                                </div>
                            </div>

                        </div>

                        <a href="#slider-carousel" class="left control-carousel hidden-xs" data-slide="prev">
                            <i class="fa fa-angle-left"></i>
                        </a>
                        <a href="#slider-carousel" class="right control-carousel hidden-xs" data-slide="next">
                            <i class="fa fa-angle-right"></i>
                        </a>
                    </div>

                </div>
            </div>
        </div>
    </section>

    <div id="UploaderPopUp" style="display: none;">
        <table>
            <tr>
                <td>Select Image to Upload:<br />
                    <br />
                </td>
                <td>
                    <asp:FileUpload ID="fupBanner" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnImageID" runat="server" EnableViewState="true" />
                </td>
                <td>
                    <%--<asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Upload" />--%>
                   <%-- <button type="button" id="btnSave" runat="server" class="btn btn-primary" onclick="Upload()">Upload</button>--%>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnSliderImageID" runat="server" />
    <%-- <asp:Button ID="btnSave1" runat="server" CssClass="btn btn-primary" style="display:none;" OnClick="btnSave_Click" Text="Upload" />--%>
    <%-- <button type="button" id="btnSave" runat="server" style="display: block;" onclick="btnSave_Click" ></button>--%>
</asp:Content>

