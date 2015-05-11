<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Catalogue.aspx.cs" Inherits="Catalogue" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript">
        function GetProducts(Category, SubCategory) {
            alert("in get products");
            window.location("Catalogue.aspx?a=x");
        }
    </script>
    <table>
        <tr>
            <td style="width: 20%;">
                <%--Search Filter--%>
                <div>
                    <div>Narror Your Choices</div>
                    <div>
                        Manufacturer
                       <asp:CheckBoxList ID="chkManufacturer" runat="server">
                       </asp:CheckBoxList>
                       <br />
                        Category
                        <asp:CheckBoxList ID="chkCategory" runat="server">

                        </asp:CheckBoxList>
                        <br />
                        SubCategory
                        <asp:CheckBoxList ID="chkSubCategory" runat="server">

                        </asp:CheckBoxList>
                        <br />
                    </div>
                </div>
            </td>
            <td>
                <asp:Literal ID="ltCatalogue" runat="server" Text="">

                </asp:Literal>
                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductID"
                    ShowFooter="false" CssClass="mSearchGrid" Width="800px" Style="font-size: 15px"
                    PagerStyle-CssClass="pgr" PagerSettings-Position="Bottom" AlternatingRowStyle-CssClass="alt"
                    RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true"
                    FooterStyle-Wrap="true" GridLines="None" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                    ShowHeader="true" OnRowDataBound="gvProducts_DataBound" OnRowCreated="gvProducts_RowCreated">
                    <HeaderStyle CssClass="mSearchGrid"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"
                            >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Image ID="imgProduct" runat="server" ToolTip="ProductImage" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Product" DataField="ProductCode"></asp:BoundField>
                        <asp:BoundField HeaderText="Manufacturer" DataField="Manufacturer"></asp:BoundField>
                        <asp:TemplateField HeaderText="Price" ItemStyle-Width="25%">
                            <ItemTemplate>
                                <span>
                                    <span style="float: left;">
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                    </span>
                                    <span style="float: right;">
                                        <asp:Button ID="btnGetPrice" runat="server" Text="Request Quote" /></span>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inventory" ItemStyle-Width="35%">
                            <ItemTemplate>
                                <asp:Label ID="lblInventory" runat="server" Text='<%# Eval("Inventory") %>'></asp:Label>
                                <asp:Button ID="btnCallAvail" runat="server" Text="Call for Availability" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>

    </table>

</asp:Content>
