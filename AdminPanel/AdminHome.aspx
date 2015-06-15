<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="AdminPanel_AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function ShowImage(objImg) {
            var UploaderPopUp;
            UploaderPopUp = $("#UploaderPopUp").dialog({
                autoOpen: false,
                height: 400,
                width: 400,
                modal: true,
                title: "Slider Image",
                buttons: {
                    Done: function () {
                        document.getElementById('<%=hdnSliderImageID.ClientID%>').value = objImg.id;
                        document.getElementById('<%=btnSave.ClientID%>').click();
                    }
                }
            });
            UploaderPopUp.dialog("open");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <section id="slider">
        <!--slider-->
        <div class="container">
            <div class="row">
                <div class="col-sm-7">
                    <div id="slider-carousel" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#slider-carousel" data-slide-to="0" class="active"></li>
                            <li data-target="#slider-carousel" data-slide-to="1"></li>
                            <li data-target="#slider-carousel" data-slide-to="2"></li>
                        </ol>

                        <div class="carousel-inner">
                            <div class="item active">
                                <div class="col-sm-6">
                                    <img src="../images/home/banner1.jpg" class="girl img-responsive" alt="Click to Change" onclick="ShowImage(this)" id="SliderImg1" runat="server" />
                                </div>
                            </div>
                            <div class="item ">

                                <div class="col-sm-6">
                                    <img src="../images/home/banner2.jpg" class="girl img-responsive" alt="Click to Change" onclick="ShowImage(this)" id="SliderImg2" runat="server"/>
                                </div>
                            </div>

                            <div class="item ">

                                <div class="col-sm-6">
                                    <img src="../images/home/banner3.jpg" class="girl img-responsive" alt="Click to Change" onclick="ShowImage(this)" id="SliderImg3" runat="server"/>
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
    <asp:HiddenField ID="hdnSliderImageID" runat="server"/>
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
                <td>Manufacturer Name:<br />
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtManufacturerName" runat="server" EnableViewState="true"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HiddenField ID="hdnImageID" runat="server" EnableViewState="true" />
                </td>
            </tr>
        </table>
    </div>
    <button type="button" id="btnSave" runat="server" style="display: none;" onclick="btnSave_Click"></button>

</asp:Content>

