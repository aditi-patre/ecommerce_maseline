<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="SubCategory.aspx.cs" Inherits="AdminPanel_SubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _Title = "";
        var SubCategoryPopUp;
        var CatInfo;
        function f1() {
            var _SubCategory = {};
            _SubCategory.ID = document.getElementById('<%=hdnSubCategoryId.ClientID%>').value;
            _SubCategory.Name = document.getElementById('<%=txtSubCategoryName.ClientID%>').value;
            _SubCategory.Code = document.getElementById('<%=txtSubCategoryCode.ClientID%>').value;
            _SubCategory.Descrip = document.getElementById('<%=txtSubCategoryDescrip.ClientID%>').value;

            var _ddl = document.getElementById('<%=ddlCategoryID.ClientID %>');
            if (_ddl.options[_ddl.selectedIndex].innerHTML == "Select") {
                alert("Please select a category");
                return false;
            }
            else if (document.getElementById('<%=hdnCatInfo.ClientID%>').value == "0" || _ddl.options[_ddl.selectedIndex].value != "0")
                CatInfo = _ddl.options[_ddl.selectedIndex].value.split("|");
            else
                CatInfo = document.getElementById('<%=hdnCatInfo.ClientID%>').value.split("|");

        _SubCategory.CategoryID = CatInfo[0];
        _SubCategory.CategoryShortCode = CatInfo[1];

        $.ajax({
            type: "POST",
            url: "../QueryPage.aspx/SubCategorySave",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(_SubCategory),
            dataType: "json",
            success: function (result) {
                SubCategoryPopUp.dialog("close");
                alert(JSON.parse(result.d).Message);
                if (JSON.parse(result.d).IsSuccess == true)
                    document.getElementById('<%=btnRefresh.ClientID%>').click();
                },
                error: function (e) {
                    alert("SubCategory could not be saved!!");
                }
            });
        }

        function setSelectedValue(selectObj, valueToSet) {
            for (var i = 0; i < selectObj.options.length; i++) {
                if (selectObj.options[i].text == valueToSet) {
                    selectObj.options[i].selected = true;
                    return;
                }
            }
        }

        function SubCategoryEdit(SubCatID, rowIndex, CatInfo) {
            document.getElementById('<%=hdnSubCategoryId.ClientID%>').value = SubCatID;
            document.getElementById('<%=hdnCatInfo.ClientID%>').value = CatInfo;
            var _ddl = document.getElementById('<%=ddlCategoryID.ClientID %>');
            if (rowIndex != -1) //Edit key
            {
                _Title = "Update SubCategory";
                var grd = document.getElementById('<%= gvSubCategory.ClientID %>');

                document.getElementById('<%=txtSubCategoryCode.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['0'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");
                document.getElementById('<%=txtSubCategoryName.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['1'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");;
                document.getElementById('<%=txtSubCategoryDescrip.ClientID%>').value = grd.rows[parseInt(rowIndex) + 1].cells['2'].innerHTML.replace(new RegExp("&nbsp;", 'g'), "");

                if (grd.rows[parseInt(rowIndex) + 1].cells['3'].innerHTML != "") {
                    if (_ddl.innerHTML.indexOf(grd.rows[parseInt(rowIndex) + 1].cells['3'].innerHTML) > -1) {
                        setSelectedValue(_ddl, grd.rows[parseInt(rowIndex) + 1].cells['3'].innerHTML);
                    }
                    else
                        alert("Please select a value for category as the specified category is unavailable");
                }
                else
                    setSelectedValue(_ddl, "Select");
            }
            else {
                _Title = "Add SubCategory";
                document.getElementById('<%=txtSubCategoryCode.ClientID%>').value = "";
                document.getElementById('<%=txtSubCategoryName.ClientID%>').value = "";
                document.getElementById('<%=txtSubCategoryDescrip.ClientID%>').value = "";
                _ddl.selectedIndex = 0;
            }

            SubCategoryPopUp = $("#SubCategoryPopUp").dialog({
                autoOpen: false,
                height: 440,
                width: 400,
                modal: true,
                title: _Title,
                buttons: {
                    Done: function () {
                        document.getElementById('<%=hdnSubCategoryId.ClientID%>').value = SubCatID;
                        document.getElementById('<%=btnSave1.ClientID%>').click();
                    }
                }
            });
            SubCategoryPopUp.dialog("open");
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
        <div style="padding: 1.5% 2.5% 2.5% 2.5%;">
            <asp:UpdatePanel ID="up_gvProducts" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional" class="listingtbl">
                <ContentTemplate>
                    <div class="col-sm-9" style="padding: 10px 10px 10px 12px; width: 100%;">
                        <h2>SUB-CATEGORY LISTING</h2>
                        <div style="padding-top: 15px; padding-bottom: 15px;">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    <div class="input-group" style="width: 300px;">
                                        <input type="text" class="form-control" placeholder="Sub-Category Name" name="srch-term" id="txtSearchSubCategory" runat="server" onkeypress="HandleKeyPress(event)" />
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="submit" onclick="Search()"><i class="glyphicon glyphicon-search"></i></button>
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Style="display: none;" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4" style="margin-left: -6%;">
                                    Sort by
                        <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="dropdownSearch" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Sub-Category Name: A to Z" Value="SubCatA_Z"></asp:ListItem>
                            <asp:ListItem Text="Sub-Category Name: Z to A" Value="SubCatZ_A"></asp:ListItem>
                            <asp:ListItem Text="Sub-Category Code: A to Z" Value="CatCodeA_Z"></asp:ListItem>
                            <asp:ListItem Text="Sub-Category Code: Z to A" Value="CatCodeZ_A"></asp:ListItem>
                            <asp:ListItem Text="Category Name: A to Z" Value="CatA_Z"></asp:ListItem>
                            <asp:ListItem Text="Category Name: Z to A" Value="CatZ_A"></asp:ListItem>
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2" style="margin-left: -14%;">
                                    Show
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdownSearch" Style="width: 80px;" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-1" style="margin-left: -4%;">
                                    <asp:Button ID="btnClearSearch" runat="server" CssClass="btn btn-primary pull-right" Style="margin-top: 0px !important;" OnClick="btnClearSearch_Click" Text="Clear" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;                                    
                                </div>
                                <div class="col-sm-1" style="margin-left: 2%;">
                                    <button type="button" class="btn btn-primary pull-right" style="margin-top: 0px !important;" onclick="return SubCategoryEdit(0,-1,0|0);" id="btnAdd">Add</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 115%;">
                        <div class="col-sm-15 pull-left" style="padding: 0px 5px 5px 12px;">
                            <div class="features_items">
                                <div class="listingMcft">
                                    <div class="table-responsive cart_info">
                                        <asp:GridView ID="gvSubCategory" runat="server" AutoGenerateColumns="false" DataKeyNames="SubCategoryID"
                                            ShowFooter="false" CssClass="table table-condensed" Style="font-size: 15px" AlternatingRowStyle-CssClass="alt"
                                            RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true" EmptyDataText="No records found!!"
                                            FooterStyle-Wrap="true" GridLines="None" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                                            ShowHeader="true" OnRowDataBound="gvSubCategory_RowDataBound" OnRowCommand="gvSubCategory_RowCommand">
                                            <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundField HeaderText="Sub-Category Code" DataField="S_ShortCode" ControlStyle-Width="27%" ItemStyle-Width="27%" HeaderStyle-Width="27%" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description" />
                                                <asp:BoundField HeaderText="Sub-Category" DataField="Name" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description"></asp:BoundField>
                                                <asp:BoundField HeaderText="Description" DataField="Descrip" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description"></asp:BoundField>
                                                <asp:BoundField HeaderText="Category" DataField="CatName" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCat_Code" runat="server" Style="display: none;" Text='<%# Eval("ShortCode") %>'></asp:Label>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("CategoryID") %>' CommandName="EditM"
                                                            ImageUrl="~/images/edit-icon.png" Width="30px" Height="30px" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%# Eval("SubCategoryID") %>' CommandName="DeleteM" ImageUrl="~/images/delete_icon.png"
                                                            Width="30px" Height="30px" OnClientClick="if (confirm('Are you sure you want to delete the SubCategory?') == false) return false" />
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
    <div id="SubCategoryPopUp" style="display: none;">
        <table>
            <tr>
                <td>SubCategory Code:<br />
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtSubCategoryCode" runat="server" EnableViewState="true"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>SubCategory Name:<br />
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtSubCategoryName" runat="server" EnableViewState="true"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>Description:<br />
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtSubCategoryDescrip" runat="server" TextMode="MultiLine" Rows="4" EnableViewState="true" CssClass="MultiLineText"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" EnableViewState="true" />
                    <asp:HiddenField ID="hdnCatInfo" runat="server" EnableViewState="true" />
                </td>
            </tr>
            <tr>
                <td>Category:<br />
                    <br />
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoryID" runat="server" EnableViewState="true"></asp:DropDownList>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <button type="button" id="btnSave1" runat="server" style="display: none;" onclick="return f1();"></button>
    <asp:Button ID="btnRefresh" runat="server" Style="display: none;" OnClick="btnRefresh_Click" />
    </div>
</asp:Content>

