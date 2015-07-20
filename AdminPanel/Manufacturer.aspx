<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="Manufacturer.aspx.cs" Inherits="AdminPanel_Manufacturer" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _Title = "";
        var ManufacturerPopUp;
        function f1() {
            var _Manufacturer = {};
            _Manufacturer.ID = document.getElementById('<%=hdnManufacturerId.ClientID%>').value;
            _Manufacturer.Name = document.getElementById('<%=txtManufacturerName.ClientID%>').value;
            _Manufacturer.Code = document.getElementById('<%=txtManufacturerCode.ClientID%>').value;
            $.ajax({
                type: "POST",
                url: "../QueryPage.aspx/ManufacturerSave",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(_Manufacturer),
                dataType: "json",
                success: function (result) {
                    ManufacturerPopUp.dialog("close");
                    alert(JSON.parse(result.d).Message);
                    if (JSON.parse(result.d).IsSuccess == true)
                        document.getElementById('<%=btnRefresh.ClientID%>').click();
                },
                error: function (e) {
                    alert("Manufacturer could not be saved!!");
                }
            });
        }

        function ManufacturerEdit(ManfctrID, rowIndex) {
            document.getElementById('<%=hdnManufacturerId.ClientID%>').value = ManfctrID;
            if (rowIndex != -1) //Edit key
            {
                _Title = "Update Manufacturer";
                var grd = document.getElementById('<%= gvManufacturer.ClientID %>');

                document.getElementById('<%=txtManufacturerCode.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['0'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");
                document.getElementById('<%=txtManufacturerName.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['1'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");
            }
            else {
                _Title = "Add Manufacturer";
                document.getElementById('<%=txtManufacturerCode.ClientID%>').value = "";
                document.getElementById('<%=txtManufacturerName.ClientID%>').value = "";
            }

            ManufacturerPopUp = $("#ManufacturerPopUp").dialog({
                autoOpen: false,
                height: 400,
                width: 400,
                modal: true,
                title: _Title,
                buttons: {
                    Done: function () {
                        document.getElementById('<%=hdnManufacturerId.ClientID%>').value = ManfctrID;
                        document.getElementById('<%=btnSave1.ClientID%>').click();
                    }
                }
            });
            ManufacturerPopUp.dialog("open");
            return false;
        }

        function Search() {
            document.getElementById('<%=btnSearch.ClientID %>').click();
        }
        function HandleKeyPress(e) {
            if (e.keyCode === 13) {
                Search();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--   <div class="search_box pull-left" style="margin-top: -7%; margin-left:20%">
        <input type="text" placeholder="Search Manufacturer" onclick="btnSearch_Click" id="txtSearch" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>--%>
        <div style="padding:1.5% 2.5% 2.5% 2.5%;">
            <asp:UpdatePanel ID="up_gvProducts" runat="server" ChildrenAsTriggers="true" UpdateMode="Always" class="listingtbl">
                <ContentTemplate>
                    <div class="col-sm-9" style="padding: 10px 10px 10px 12px; width: 100%;">
                        <h2>MANUFACTURER LISTING</h2>
                        <div>
                            <div style="padding-top: 15px; padding-bottom: 15px;">
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <div class="input-group" style="width: 300px;">
                                            <input type="text" class="form-control" placeholder="Manufacturer Name" name="srch-term" id="txtSearchManufacturer" onkeypress="HandleKeyPress(event)" runat="server" tabindex="1" />
                                            <div class="input-group-btn">
                                                <button class="btn btn-default" autofocus="autofocus" id="ibtnSearch" runat="server" type="submit" onclick="Search()" tabindex="0"><i class="glyphicon glyphicon-search"></i></button>
                                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Style="display: none;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4" style="margin-left: -6%;">
                                        Sort by
                        <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="dropdownSearch" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Manufacturer: A to Z" Value="McftA_Z"></asp:ListItem>
                            <asp:ListItem Text="Manufacturer: Z to A" Value="McftZ_A"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        
                                    </div>
                                    <div class="col-sm-2" style="margin-left: -14%;">
                                        Show
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdownSearch" Style="width: 80px;" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                                    <div class="col-sm-1" style="margin-left: -4%;">
                                        <asp:Button ID="btnClearSearch" runat="server" CssClass="btn btn-primary pull-right" Style="margin-top: 0px !important;" OnClick="btnClearSearch_Click" Text="Clear" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;                                    
                                    </div>
                                    <div class="col-sm-1" style="margin-left: 2%;">
                                        <button type="button" class="btn btn-primary pull-right" style="margin-top: 0px !important;" onclick="return ManufacturerEdit(0,-1);" id="btnAdd">Add</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 115%;">
                        <div class="col-sm-15 pull-left" style="padding: 0px 5px 5px 12px;">
                            <div class="features_items">
                                <div class="listingMcft">
                                    <div class="table-responsive cart_info">
                                        <asp:GridView ID="gvManufacturer" runat="server" AutoGenerateColumns="false" DataKeyNames="ManufacturerID"
                                            ShowFooter="false" CssClass="table table-condensed" Style="font-size: 15px"
                                            AlternatingRowStyle-CssClass="alt"
                                            RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true"
                                            FooterStyle-Wrap="true" GridLines="None" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                                            ShowHeader="true" OnRowDataBound="gvManufacturer_RowDataBound" OnRowCommand="gvManufacturer_RowCommand">
                                            <%--OnPageIndexChanging="gvManufacturer_PageIndexChanging"--%>
                                            <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundField HeaderText="Manufacturer Code" DataField="ManufacturerCode" ControlStyle-Width="27%" ItemStyle-Width="27%" HeaderStyle-Width="27%" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description" />
                                                <asp:BoundField HeaderText="Manufacturer" DataField="Name" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ManufacturerID") %>' CommandName="EditM"
                                                            ImageUrl="~/images/edit-icon.png" Width="30px" Height="30px" />
                                                        <%--OnClientClick='<%# string.Format("javascript:return ManufacturerEdit(\"{0}\",\"{1}\")", Eval("ManufacturerID"),this) %>'  />     --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%# Eval("ManufacturerID") %>' CommandName="DeleteM" ImageUrl="~/images/delete_icon.png"
                                                            Width="30px" Height="30px" OnClientClick="if (confirm('Are you sure you want to delete the manufacturer?') == false) return false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <ul class="pagination">
                                        <asp:Repeater ID="rptPagerBottom" runat="server">
                                            <ItemTemplate>
                                                <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'>
                                                    </asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <%--    <div class="col-sm-15 pull-left">
        <div class="features_items">

        </div>
    </div>--%>

        <div id="ManufacturerPopUp" style="display: none;">
            <table>
                <tr>
                    <td>Manufacturer Code:<br />
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtManufacturerCode" runat="server" EnableViewState="true"></asp:TextBox><br />
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
                        <asp:HiddenField ID="hdnManufacturerId" runat="server" EnableViewState="true" />

                    </td>
                </tr>
            </table>
        </div>
        <button type="button" id="btnSave1" runat="server" style="display: none;" onclick="return f1();"></button>
        <%--<button type="button" id="btnRefresh" runat="server" style="display: none;" onclick="btnRefresh_Click"></button>--%>
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" OnClick="btnRefresh_Click" />
    </div>
</asp:Content>

