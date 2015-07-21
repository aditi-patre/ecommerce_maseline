<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="VideoGallery.aspx.cs" Inherits="AdminPanel_VideoGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
        <div style="padding: 1.5% 2.5% 2.5% 2.5%;">
            <div class="listingtbl">
                <div class="col-sm-9" style="padding: 10px 10px 10px 12px; width: 100%;">
                    <h2>PRODUCT DETAILS</h2>
                    <br />
                </div>
                <div style="width: 128%;">
                    <div class="col-sm-15 pull-left" style="padding: 0px 5px 5px 12px;">
                        <asp:FileUpload ID="fupldImage" runat="server" /><asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Upload" OnClick="btnAdd_Click" />

                        <div class="floating">
                            <asp:DataList ID="rptImages" runat="server" OnItemCommand="rptImages_ItemCommand" RepeatDirection="Horizontal" RepeatColumns="2">
                                <ItemTemplate>
                                    <%-- <div style="background-image: url('<%#GetImageFromByte(DataBinder.Eval(Container.DataItem,"VideoName")) %>'); text-align: right; height: 300px; width: 300px; border: 1px solid gray; background-repeat: no-repeat; background-size: cover;">
                                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/images/delete_icon.png" Width="10px" Height="10px" CommandName="DeleteI" CommandArgument='<%# Eval("VideoName") %>' OnClientClick="if (confirm('Are you sure you want to delete the Image?') == false) return false" />
                                    </div>--%>

                                    <div style="position: absolute; z-index: 1; height: 300px; width: 300px; border: 1px solid gray;" runat="server">
                                        <video width="300px" height="300px">
                                            <source src="../images/VideoGallery/test2.mp4" type="video/mp4">
                                        </video>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

