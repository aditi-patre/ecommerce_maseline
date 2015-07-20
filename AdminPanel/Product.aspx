<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="AdminPanel_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var _Title = "";
        var ProductPopUp;

        <%-- function f1() {
            var _Product = {};
            _Product.ID = document.getElementById('<%=hdnSubCategoryId.ClientID%>').value;
            _Product.Name = document.getElementById('<%=txtProductName.ClientID%>').value;
            _Product.Code = document.getElementById('<%=txtProductCode.ClientID%>').value;
            _Product.Descrip = document.getElementById('<%=txtProductDescrip.ClientID%>').value;
            _Product.Inventory = document.getElementById('<%=txtInventory.ClientID %>').value;
            _Product.Price = document.getElementById('<%=txtPrice.ClientID %>').value;

            var _ddlC = document.getElementById('<%=ddlCategoryID.ClientID %>');
            var _ddlS = document.getElementById('<%=ddlSubCategoryID.ClientID %>');
            var _ddlM = document.getElementById('<%=ddlManufacturer.ClientID %>');

            if (_ddlC.options[_ddlC.selectedIndex].innerHTML == "Select") {
                alert("Please select a category");
                return false;
            }
            _Product.CategoryID = _ddlC.options[_ddlC.selectedIndex].value;
            _Product.SubCategoryID = _ddlC.options[_ddlS.selectedIndex].value;
            _Product.ManufacturerID = _ddlC.options[_ddlM.selectedIndex].value;

            $.ajax({
                type: "POST",
                url: "../QueryPage.aspx/ProductSave",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(_SubCategory),
                dataType: "json",
                success: function (result) {
                    ProductPopUp.dialog("close");
                    alert(JSON.parse(result.d).Message);
                    if (JSON.parse(result.d).IsSuccess == true)
                        document.getElementById('<%=btnRefresh.ClientID%>').click();
                },
                error: function (e) {
                    alert("Product could not be saved!!");
                }
            });
        }
        --%>

        function setSelectedValue(selectObj, valueToSet) {
            for (var i = 0; i < selectObj.options.length; i++) {
                if (selectObj.options[i].text == valueToSet) {
                    selectObj.options[i].selected = true;
                    return;
                }
            }
        }



        function Search() {
            document.getElementById('<%=btnSearch.ClientID %>').click();
        }

        function HandleKeyPress(e) {
            if (e.keyCode === 13) {
                Search();
            }
        }

        //**To expand/collapse columns
        var Toggle_All = false;
        var tbl = null;
        var UpperBound = 0;
        var LowerBound = 1;
        var CollapseImage = '../images/minus.png';
        var ExpandImage = '../images/plus.png';
        var n = 1;
        var TimeSpan = 100;
        var Rows = null;
        var Cols = null;
        var img = "";
        var IsImageExpanded = false;

        function HideCols() {
            tbl = document.getElementById('<%= this.gvProducts.ClientID %>');
            HideColumns = document.getElementById('<%=hdnHideCols.ClientID %>').value;
            var ibtn = document.getElementById('<%=btnShowAttributes.ClientID %>');
            for (j = 0; j < tbl.rows.length; j++) {
                for (i = 9; i <= 23; i++) {
                    if (HideColumns == "true") {
                        tbl.rows[j].cells[i].style.display = "none";
                        ibtn.src = "../images/show_attributes.png";
                    }
                    else {
                        tbl.rows[j].cells[i].style.display = "block";
                        ibtn.src = "../images/hide_attributes.png";
                    }
                }
            }
        }


        window.onload = function () {
            tbl = document.getElementById('<%= this.gvProducts.ClientID %>');
             UpperBound = parseInt('<%= this.gvProducts.Rows.Count %>');
             Rows = tbl.getElementsByTagName('tr');
             Cols = tbl.getElementsByTagName('td');
             HideCols();
         }

         function Toggle(Index) {
             ToggleImage(Index);
             ToggleColumns(Index);
         }

         function ToggleImage(Image, Index) {
             if (document.getElementById('<%=hdnHideCols.ClientID %>').value == true) {
            n = UpperBound;
            img = ExpandImage;
            IsImageExpanded = false;
        }
        else {
            n = LowerBound;
            img = CollapseImage;
            IsImageExpanded = true;
        }
        document.getElementById('<%=hdnColImg.ClientID %>').value = img;

        if (Toggle_All == 'false' || Toggle_All == 'False')
            document.getElementById('<%=hdnColNoImg.ClientID %>').value = Index + "|" + img;
}

function ToggleColumns(Index) {
    var HideColumns = document.getElementById('<%=hdnHideCols.ClientID %>').value;

    if (Toggle_All == 'false' || Toggle_All == 'False')
        document.getElementById('<%=hdnColNoImg.ClientID %>').value = Index + "|" + img;
    if (n < LowerBound || n > UpperBound) return;
    if (tbl.rows.length > 0) {
        if (UpperBound > tbl.rows.length - 1)
            UpperBound = tbl.rows.length - 1;
        var tbl_row = tbl.rows[parseInt(n)];
        var tbl_Cell = tbl_row.cells[Index];
        if (parseInt(tbl_Cell.style.width.replace("px", "")) >= parseInt("100")) {
            tbl_Cell.style.width = "3px";
            if (HideColumns == "true")
                tbl_Cell.style.display = "none";
            else
                tbl_Cell.style.display = "block";
        }
        else {
            tbl_Cell.style.width = "100px";
            if (HideColumns == "true")
                tbl_Cell.style.display = "none";
            else
                tbl_Cell.style.display = "block";
        }

        if (IsImageExpanded) n++; else n--;
        setTimeout(function () { ToggleColumns(Index); }, TimeSpan);
    }
    if (Toggle_All == 'false' || Toggle_All == 'False')
        document.getElementById('<%=btn1.ClientID %>').click();
        }//Toggle Columns

        function ToggleAll(ibtn) {
            if (document.getElementById('<%=hdnHideCols.ClientID %>').value == "true") {
        document.getElementById('<%=hdnHideCols.ClientID %>').value = "false";
        ibtn.src = "../images/hide_attributes.png";
    }
    else {
        document.getElementById('<%=hdnHideCols.ClientID %>').value = "true";
        ibtn.src = "../images/show_attributes.png";
    }
    ToggleAll_1();
}

//will be invoked On page index changed: call to expand/collapse cols based on prev selection
function ToggleAll_1() {
    Toggle_All = true;
    var gv = document.getElementById('<%=gvProducts.ClientID %>');
    for (var i = 9; i <= 23; i++) {
        Toggle(i);
    }
    HideCols();
    document.getElementById('<%=btnShowAttributes.ClientID %>').click();
        }

        function SetToggle_All(val) {
            Toggle_All = val;
        }
        //**
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
        <div style="padding: 1.5% 2.5% 2.5% 2.5%;">
            <asp:UpdatePanel ID="up_gvProducts" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional" class="listingtbl">
                <ContentTemplate>
                    <div class="col-sm-9" style="padding: 10px 10px 10px 12px; width: 100%;">
                        <h2>PRODUCT LISTING</h2>
                        <div style="padding-top: 15px; padding-bottom: 15px;">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    <div class="input-group" style="width: 300px;">
                                        <input type="text" class="form-control" placeholder="Product Name" name="srch-term" id="txtSearchProduct" runat="server" onkeypress="HandleKeyPress(event)" />
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="submit" onclick="Search()"><i class="glyphicon glyphicon-search"></i></button>
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Style="display: none;" />
                                        </div>
                                    </div>
                                </div>
                                <%--                            <div class="col-sm-4" style="margin-left: -6%;">
                                Sort by
                        <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="dropdownSearch" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Product Name: A to Z" Value="ProdA_Z"></asp:ListItem>
                            <asp:ListItem Text="Product Name: Z to A" Value="ProdZ_A"></asp:ListItem>
                            <asp:ListItem Text="Manufacturer: A to Z" Value="McftA_Z"></asp:ListItem>
                            <asp:ListItem Text="Manufacturer: Z to A" Value="McftZ_A"></asp:ListItem>
                            <asp:ListItem Text="Price: Low to High" Value="PriceL_H"></asp:ListItem>
                            <asp:ListItem Text="Price: High to Low" Value="PriceH_L"></asp:ListItem>
                        </asp:DropDownList>
                            </div>--%>
                                <div class="col-sm-2" style="margin-left: -6%;">
                                    Show
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdownSearch" Style="width: 80px;" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-1" style="margin-left: -4%;">
                                    <asp:Button ID="btnClearSearch" runat="server" CssClass="btn btn-primary pull-right" Style="margin-top: 0px !important;" OnClick="btnClearSearch_Click" Text="Clear" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;                                    
                                </div>
                                <div class="col-sm-1" style="margin-left: 2%;">
                                    <%--<button type="button" class="btn btn-primary pull-right" style="margin-top: 0px !important;" onclick="btnAdd_Click" id="btnAdd">Add</button>--%>
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-primary pull-right" Style="margin-top: 0px !important;" OnClick="btnAdd_Click" Text="Add" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 131%;">
                        <div class="col-sm-15 pull-left" style="padding: 0px 5px 5px 12px;">
                           <%-- <div class="listingtbl">--%>
                                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductID"
                                    ShowFooter="false" CssClass="table table-condensed" Width="" Style="font-size: 15px; border: 1px solid lightgray !important;"
                                    PagerStyle-CssClass="pgr" PagerSettings-Position="Bottom" BorderColor="#CCCCCC" AllowSorting="true"
                                    RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true" OnSorting="gvProducts_OnSorting"
                                    FooterStyle-Wrap="true" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                                    ShowHeader="true" OnRowDataBound="gvProducts_DataBound" OnRowCreated="gvProducts_RowCreated" AllowPaging="true"
                                    OnRowCommand="gvProducts_RowCommand">
                                    <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="14%">
                                            <HeaderStyle CssClass="image" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgProduct" runat="server" ToolTip="Click to view details" Style="width: 100px; height: 75px; cursor: pointer;" />
                                                <asp:Label ID="lblImagePath" runat="server" Style="display: none;" Text='<%# Eval("ImageName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name/Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="24%" HeaderStyle-Width="24%" SortExpression="Product">
                                            <HeaderStyle CssClass="description" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="center" VerticalAlign="Top" CssClass="cart_description" />
                                            <ControlStyle CssClass="cart_description" />
                                            <ItemTemplate>
                                                <%--<h4>
                                                <asp:LinkButton ID="lbtnProdName" ToolTip="Click to view details" runat="server" Text='<%# Eval("Name") %>' CommandName="ShowProductDetails" CommandArgument='<%# Eval("ProductID") %>' Style="cursor: pointer;" title="Click to view details"></asp:LinkButton>
                                            </h4>--%>
                                                <h4>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("ProductCode") %>'></asp:Label>
                                                </h4>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Manufacturer" DataField="Manufacturer" ItemStyle-CssClass="cart_description" ItemStyle-Width="15%" SortExpression="Manufacturer"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <HeaderStyle CssClass="cart_total" />
                                            <ItemStyle CssClass="cart_total" />
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lblPricing" runat="server" Text='<%# Eval("Pricing") %>' Style="display: none;"></asp:Label>
                                                    <asp:Literal ID="ltQty" runat="server"></asp:Literal>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="17%" SortExpression="Price">
                                            <HeaderStyle CssClass="cart_total" />
                                            <ItemStyle CssClass="cart_total" />
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                    <asp:Literal ID="ltPricing" runat="server"></asp:Literal>
                                                    <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CategoryID") %>' Style="display: none;"></asp:Label>
                                                    <asp:Label ID="lblSubCategoryID" runat="server" Text='<%# Eval("SubCategoryID") %>' Style="display: none;"></asp:Label>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Inventory" DataField="Inventory" HeaderStyle-CssClass="description" ControlStyle-CssClass="cart_description" ItemStyle-CssClass="cart_description"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="EditM"
                                                    ImageUrl="~/images/edit-icon.png" Width="30px" Height="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="DeleteM" ImageUrl="~/images/delete_icon.png"
                                                    Width="30px" Height="30px" OnClientClick="if (confirm('Are you sure you want to delete the Product?') == false) return false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" ItemStyle-Width="5%" ControlStyle-Width="5%">
                                            <HeaderTemplate>
                                                <asp:Image ID="ibtnShowAttributes" onclick="javascript:ToggleAll(this);" runat="server" ImageUrl="~/images/show_attributes.png" ToolTip="Show/Hide Attributes" ImageAlign="AbsMiddle" />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="Shorter" />
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Descrip">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescrip" runat="server" Text='<%# Eval("Descrip") %>' Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="Shorter" />
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Technology" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Technology">

                                            <ItemTemplate>
                                                <asp:Label ID="lblTechnology" runat="server" Text='<%# Eval("Technology") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Harmonized Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="HarmonizedCode">

                                            <ItemTemplate>
                                                <asp:Label ID="lblHarmonizedCode" runat="server" Text='<%# Eval("HarmonizedCode") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Category">

                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubCategory" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="SubCategory">

                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("SubCategory") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Capacitance" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Capacitance">

                                            <ItemTemplate>
                                                <asp:Label ID="lblCapacitance" runat="server" Text='<%# Eval("Capacitance") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voltage" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Voltage">

                                            <ItemTemplate>
                                                <asp:Label ID="lblVoltage" runat="server" Text='<%# Eval("Voltage") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Material">

                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" runat="server" Text='<%# Eval("Material") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Style">

                                            <ItemTemplate>
                                                <asp:Label ID="lblStyle" runat="server" Text='<%# Eval("Style") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tolerance" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Tolerance">

                                            <ItemTemplate>
                                                <asp:Label ID="lblTolerance" runat="server" Text='<%# Eval("Tolerance") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Temperature" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Temperature">

                                            <ItemTemplate>
                                                <asp:Label ID="lblTemperature" runat="server" Text='<%# Eval("Temperature") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Construction" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Construction">

                                            <ItemTemplate>
                                                <asp:Label ID="lblConstruction" runat="server" Text='<%# Eval("Construction") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Features" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Features">

                                            <ItemTemplate>
                                                <asp:Label ID="lblFeatures" runat="server" Text='<%# Eval("Features") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wattage" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Wattage">

                                            <ItemTemplate>
                                                <asp:Label ID="lblWattage" runat="server" Text='<%# Eval("Wattage") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resistance" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px" SortExpression="Resistance">

                                            <ItemTemplate>
                                                <asp:Label ID="lblResistance" runat="server" Text='<%# Eval("Resistance") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnColNoImg" runat="server" />
                                <asp:Button ID="btn1" runat="server" Style="display: none;" OnClick="btnExpand_Click" />
                                <asp:HiddenField ID="hdnColImg" runat="server" />
                                <asp:HiddenField ID="hdnHideCols" runat="server" />
                                <asp:Button ID="btnShowAttributes" OnClick="btnShowAttributes_Click" runat="server" Style="display: none;" />
                            <%--</div>--%>
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <%--<asp:PostBackTrigger ControlID="ddlSortBy" />--%>
                    <asp:PostBackTrigger ControlID="ddlPageSize" />
                    <asp:PostBackTrigger ControlID="btnAdd" />
                    <%--<asp:PostBackTrigger ControlID="btnClearSearch"/>--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    <%--        <div id="ProductPopUp" style="display: block;">
            <table>
                <tr>
                    <td>Name:<br />
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductName" runat="server" EnableViewState="true"></asp:TextBox><br />
                        <br />
                    </td>
                    <td>Product Code:<br />
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductCode" runat="server" EnableViewState="true"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>Description:<br />
                        <br />
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtProductDescrip" runat="server" TextMode="MultiLine" Rows="4" EnableViewState="true" CssClass="MultiLineText"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>Manufacturer:<br />
                        <br />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlManufacturer" runat="server" EnableViewState="true"></asp:DropDownList>
                        <br />
                    </td>
                    <td>Inventory:<br />
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtInventory" runat="server"></asp:TextBox><br />
                        <br />
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
                    <td>SubCategory:<br />
                        <br />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubCategoryID" runat="server" EnableViewState="true"></asp:DropDownList>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>Price:<br />
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox><br />
                        <br />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsPricing" runat="server" Text="Is Multiple Pricing Available?" />
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>Min Qty:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRngeFrom1" runat="server"></asp:TextBox>
                                </td>
                                <td>Max Qty:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRngeTo1" runat="server"></asp:TextBox>
                                </td>
                                <td>Price:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRngePrice" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnRngeAdd" runat="server" CssClass="btn btn-primary" OnClick="btnRngeAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <asp:GridView ID="gvPricing" runat="server" DataKeyNames="RangeID" OnRowCommand="gvPricing_RowCommand" OnRowDataBound="gvPricing_RowDataBound"
                                        OnRowCancelingEdit="gvPricing_RowCancelingEdit" OnRowEditing="gvPricing_RowEditing" OnRowUpdating="gvPricing_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qty Range">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQtyRange" runat="server" Text='<%# Eval("QtyRange")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtQtyRange1" Text='<%# Eval("QtyRange")%>' />
                                                    <asp:TextBox runat="server" ID="txtQtyRange2" Text='<%# Eval("QtyRange")%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Price" DataField="Price" ItemStyle-CssClass="cart_description" />
                                            <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnPricingDelete" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteP" ImageUrl="~/images/delete_icon.png"
                                                        Width="20px" Height="20px" OnClientClick="if (confirm('Are you sure you want to delete the Range?') == false) return false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>Images:<br />
                        <br />
                    </td>
                    <td colspan="3">
                        <asp:FileUpload ID="fupldImage" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Repeater ID="rptImages" runat="server"></asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="hdnSubCategoryId" runat="server" EnableViewState="true" />
                        <asp:HiddenField ID="hdnCatInfo" runat="server" EnableViewState="true" />
                    </td>
                </tr>
            </table>
        </div>
        <button type="button" id="btnSave1" runat="server" style="display: none;" onclick="return f1();"></button>--%>
    <asp:Button ID="btnRefresh" runat="server" Style="display: none;" OnClick="btnRefresh_Click" />
    </div>
</asp:Content>

