<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="FeaturedNews.aspx.cs" Inherits="AdminPanel_FeaturedNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var _Title = "";
        var FeaturedNewsPopUp;
        var ht = 400;

        function FeaturedNewsEdit(FeaturedNewsID, rowIndex) {
            debugger;
            document.getElementById('<%=hdnFeaturedNewsId.ClientID%>').value = FeaturedNewsID;
            if (rowIndex != -1) //Edit key
            {
                _Title = "Update Featured News";
                var grd = document.getElementById('<%= gvFeaturedNews.ClientID %>');

                document.getElementById('<%=txtDescrip.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['0'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");
                document.getElementById('<%=imgImageLogo.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['1'].childNodes['3'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");
                document.getElementById('<%=imgImageLogo.ClientID%>').style.display = "block";
                document.getElementById('<%=imgImageLogo.ClientID%>').src = grd.rows[parseInt(rowIndex) + 1].cells['1'].childNodes[1].src;
                ht = 650;
            }
            else {
                _Title = "Add Featured News";
                document.getElementById('<%=txtDescrip.ClientID%>').value = "";
                document.getElementById('<%=imgImageLogo.ClientID%>').value = "";
                document.getElementById('<%=imgImageLogo.ClientID%>').style.display = "none";
                document.getElementById('<%=fupldImageName.ClientID%>').value = "";
                ht = 400;
            }


            FeaturedNewsPopUp = $("#FeaturedNewsPopUp").dialog({
                appendTo: "form",
                autoOpen: false,
                height: ht,
                width: 500,
                modal: true,
                title: _Title,
                buttons: {
                    Done: function () {
                        document.getElementById('<%=hdnFeaturedNewsId.ClientID%>').value = FeaturedNewsID;
                        document.getElementById('<%=hdntxtDescrip.ClientID%>').value = document.getElementById('<%=txtDescrip.ClientID%>').value;
                        document.getElementById('<%=btnSave1.ClientID%>').click();
                    }
                }
            });
            FeaturedNewsPopUp.dialog("open");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
        <div style="padding: 1.5% 2.5% 2.5% 2.5%;">
            <div class="listingtbl">
                <div class="col-sm-9" style="padding: 10px 10px 10px 12px; width: 100%;">
                    <h2>FEATURED NEWS</h2>
                    <div class="col-sm-9" style="padding: 10px 10px 10px 20px; width: 96%;">
                        <button type="button" class="btn btn-primary pull-right" style="margin-top: 0px !important;" onclick="return FeaturedNewsEdit(0,-1);" id="btnAdd">Add</button>
                    </div>
                </div>
                <div style="width: 128%;">
                    <div class="col-sm-15 pull-left" style="padding: 0px 5px 5px 12px;">
                        <div class="features_items">
                            <div class="table-responsive cart_info">
                                <asp:GridView ID="gvFeaturedNews" runat="server" AutoGenerateColumns="false" DataKeyNames="FeaturedNewsID"
                                    ShowFooter="false" CssClass="table table-condensed" Style="font-size: 15px"
                                    AlternatingRowStyle-CssClass="alt" EmptyDataText="No Featured News"
                                    RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true"
                                    FooterStyle-Wrap="true" GridLines="None" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                                    ShowHeader="true" OnRowDataBound="gvFeaturedNews_RowDataBound" OnRowCommand="gvFeaturedNews_RowCommand">
                                    <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Descrip" DataField="Descrip" ControlStyle-Width="67%" ItemStyle-Width="67%" HeaderStyle-Width="67%" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description" />
                                        <%--<asp:BoundField HeaderText="ImageName" DataField="ImageName" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Logo">
                                            <ItemTemplate>
                                                <asp:Image ID="imgLogo" runat="server" Style="width: 100px; height: 75px; cursor: pointer;" />
                                                <asp:Label ID="lblImagePath" runat="server" Style="display: none;" Text='<%# Eval("ImageName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("FeaturedNewsID") %>' CommandName="EditM"
                                                    ImageUrl="~/images/edit-icon.png" Width="30px" Height="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%# Eval("FeaturedNewsID") %>' CommandName="DeleteF" ImageUrl="~/images/delete_icon.png"
                                                    Width="30px" Height="30px" OnClientClick="if (confirm('Are you sure you want to delete the News?') == false) return false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <%--<ul class="pagination">
                            <asp:Repeater ID="rptPagerBottom" runat="server">
                                <ItemTemplate>
                                    <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                            OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="FeaturedNewsPopUp" style="display: none;">
        <table style="width: 98%;">
            <tr>
                <td style="width: 33%">Featured News:<br />
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtDescrip" runat="server" TextMode="MultiLine" Width="350px" Height="173px" EnableViewState="true"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>Image:<br />
                    <br />
                </td>
                <td>
                    <%--<asp:TextBox ID="txtImageName" runat="server" EnableViewState="true" Style="display: none;"></asp:TextBox>--%>
                    <asp:Image ID="imgImageLogo" runat="server" />
                    <br />
                    <asp:FileUpload ID="fupldImageName" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnFeaturedNewsId" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hdntxtDescrip" runat="server" EnableViewState="true" />
    <%--<button type="button" id="btnSave1" runat="server" style="display: none;" onclick="btnSave1_Click"></button>--%>
    <asp:Button ID="btnSave1" runat="server" Style="display: none;" OnClick="btnSave1_Click" />
</asp:Content>

