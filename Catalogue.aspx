<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Catalogue.aspx.cs" Inherits="Catalogue" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%--<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>--%>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <link rel="Stylesheet" href="css/StyleSheet.css" />


    <script type="text/javascript">
        function GetProducts(Category, SubCategory) {
            window.location("Catalogue.aspx?a=x");
        }

        /*
        $(document).ready(function () {
            $("#btnApplyFilter1").click(function () {
                $.support.cors = true;
                $.ajax({
                    type: "POST",
                    url: "QueryPage.aspx/ApplyFilter",
                    data: "{'Manufacturer':'" + selManufacturer + "', 'Category':'" + selCategory + "', 'SubCategory':'" + selSubCategory + "', 'IsInStock':'" + InStock + "', 'IsPricingAvail':'" + IsPricingAvail + "', 'Attributes':'" + Attributes + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var xmlDoc = $.parseXML(response.d);
                        var xml = $(xmlDoc);
                        var customers = xml.find("Table");
                        var row = $("[id*=gvProducts] tr:last-child").clone(true);

                        $.each(customers, function () {
                            var customer = $(this);
                            $("td", row).eq(1).html($(this).find("ProductCode").text());
                            $("td", row).eq(2).html($(this).find("Manufacturer").text());
                            $("td", row).eq(3).html($(this).find("Price").text());
                            $("td", row).eq(4).html($(this).find("Inventory").text());
                            $("[id*=gvProducts]").append(row);
                            row = $("[id*=gvProducts] tr:last-child").clone(true);
                        });
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            });
        });
        */

        /* Set search parameters in hidden fields*/
        function SetSearchFields(x) {
            var selManufacturer = "", selCategory = "", selSubCategory = "";
            $("[id*=chkManufacturer] input:checked").each(function (index, item) {
                if (selManufacturer == "")
                    selManufacturer += $(item).val();
                else
                    selManufacturer += ", " + $(item).val();
            });
            document.getElementById('<%=hdnManufacturer.ClientID%>').value = selManufacturer;

            $("[id*=chkCategory] input:checked").each(function (index, item) {
                if (selCategory == "")
                    selCategory += $(item).val();
                else
                    selCategory += ", " + $(item).val();
            });
            document.getElementById('<%=hdnCategory.ClientID%>').value = selCategory;

            $("[id*=chkSubCategory] input:checked").each(function (index, item) {
                if (selSubCategory == "")
                    selSubCategory += $(item).val();
                else
                    selSubCategory += ", " + $(item).val();
            });
            document.getElementById('<%=hdnSubCategory.ClientID%>').value = selSubCategory;

            document.getElementById('<%=hdnInStock.ClientID%>').value = $('#chkInStock').is(":checked");
            document.getElementById('<%=hdnPricingAvailable.ClientID%>').value = $('#chkPricingAvail').is(":checked");

            var Attributes = "";
            $("#SearchAttributes input[type=text]").each(function () {
                if (this.value != "") {
                    if (Attributes == "")
                        Attributes = this.id + "|" + this.value;
                    else
                        Attributes = Attributes + "," + this.id + "|" + this.value;
                }
            });
            document.getElementById('<%=hdnAttributes.ClientID%>').value = Attributes;
            if (x == 1)
                return true;
            else
                return false;
        }
        /* Set the additional search fields on click of Apply Filter after Show more */
        function SetSearchFields2() {
            if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value == "Manufacturer") {
                $("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
                    if (document.getElementById('<%=hdnManufacturer.ClientID%>').value == "")
                        document.getElementById('<%=hdnManufacturer.ClientID%>').value += $(item).val();
                    else {
                        var IsFound = false;
                        var a = document.getElementById('<%=hdnManufacturer.ClientID%>').value.split(", ");
                        for (var i = 0; i < a.length; i++) {
                            if (parseInt(a[i]) == parseInt($(item).val())) {
                                IsFound = true;
                                break;
                            }
                        }

                        if (!IsFound)
                            document.getElementById('<%=hdnManufacturer.ClientID%>').value += ", " + $(item).val();
                    }
                });
            }
            else if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value == "Category") {
                $("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
                    if (document.getElementById('<%=hdnCategory.ClientID%>').value == "")
                        document.getElementById('<%=hdnCategory.ClientID%>').value += $(item).val();
                    else {
                        var IsFound = false;
                        var a = document.getElementById('<%=hdnCategory.ClientID%>').value.split(", ");
                        for (var i = 0; i < a.length; i++) {
                            if (parseInt(a[i]) == parseInt($(item).val())) {
                                IsFound = true;
                                break;
                            }
                        }
                        if (!IsFound)
                            document.getElementById('<%=hdnCategory.ClientID%>').value += ", " + $(item).val();
                    }
                });
            }
            else if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value == "Sub-Category") {
                $("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
                    if (document.getElementById('<%=hdnSubCategory.ClientID%>').value == "")
                        document.getElementById('<%=hdnSubCategory.ClientID%>').value += $(item).val();
                    else {
                        var IsFound = false;
                        var a = document.getElementById('<%=hdnSubCategory.ClientID%>').value.split(", ");
                        for (var i = 0; i < a.length; i++) {
                            if (parseInt(a[i]) == parseInt($(item).val())) {
                                IsFound = true;
                                break;
                            }
                        }
                        if (!IsFound)
                            document.getElementById('<%=hdnSubCategory.ClientID%>').value += ", " + $(item).val();
                    }
                });
            }

    document.getElementById('<%=hdnInStock.ClientID%>').value = $('#chkInStock').is(":checked");
            document.getElementById('<%=hdnPricingAvailable.ClientID%>').value = $('#chkPricingAvail').is(":checked");
            document.getElementById('<%=hdnAttributes.ClientID%>').value = document.getElementById('<%=hdnAttributes.ClientID%>').value;
            return true;
        }

        /* Add to Cart*/
        function btnAddToCart_Client(prodID)
        {            
            Product.ProductID = prodID;
              $.ajax({
                  type: "POST",
                  url: "QueryPage.aspx/AddToCart",
                  contentType: "application/json; charset=utf-8",
                  data: JSON.stringify(Product),
                  dataType: "json",
                  success: function (result) {
                      document.getElementById("lblCartItemCount").innerHTML = result.d.replace("/\"/g", "");
                      alert("Item added to cart");
                  },
                  error: function () {
                      alert("Item could not be added to the cart");
                  }
              });
              return false;
        }
        /****/

        /* Request availability/ quote*/
        function ProductRequest(prodID, obj)
        {
            Product1.ProductID = prodID;
            Product1.RequestEntity = obj;
            var dialog;
            dialog = $("#EmailPopUp").dialog({
                autoOpen: false,
                height: 400,
                width: 400,
                modal: true,
                title: "Request " + obj,
                buttons: {
                    Done: function () {
                        document.getElementById('<%=btnRequestQuote.ClientID%>').click();
                    }
                }
            });
            dialog.dialog("open");
            return false;
        }

        function ProductRequest2()
        {
            Product1.Name = document.getElementById('<%=txtName.ClientID%>').value;
            Product1.Email = document.getElementById('<%=txtEmail.ClientID%>').value;
            Product1.ContactNo = document.getElementById('<%=txtContactNo.ClientID%>').value;
            $.ajax({
                type: "POST",
                url: "QueryPage.aspx/RequestEntity",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(Product1),
                dataType: "json",
                success: function (result) {
                    alert(JSON.parse(result.d).Message);
                },
                error: function () {
                    alert("Request could not be made");
                }
            });
        }
        /****/

        /* Display pop up on click of show more */
        var SearchCriteria = "";
        var obj = {};
        var Product = {};
        var Product1 = {};
        $(function () {
            var dialog;
            dialog = $("#dialog-form").dialog({
                autoOpen: false,
                height: 500,
                width: 550,
                modal: true,
                title: "Select " + SearchCriteria,
                buttons: {
                    Done: function () {
                        document.getElementById('<%=Button1.ClientID%>').click();
                    }
                }
            });
            $("#btnMoreManufacturer").button().on("click", function () {
                SearchCriteria = "Manufacturer";
                document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value = SearchCriteria;
                PopulateCheckBoxList(SearchCriteria);
                dialog.dialog("open");
            });
            $("#btnMoreCategory").button().on("click", function () {
                SearchCriteria = "Category";
                document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value = SearchCriteria;
                PopulateCheckBoxList(SearchCriteria);
                dialog.dialog("open");
            });
            $("#btnMoreSubCategory").button().on("click", function () {
                SearchCriteria = "Sub-Category";
                document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value = SearchCriteria;
                PopulateCheckBoxList(SearchCriteria);
                dialog.dialog("open");
            });
        });

        function PopulateCheckBoxList(str) {
            obj.CriteriaToExpand = str;
            var r = SetSearchFields(0); // to set hidden fields with already selected checkboxes
            $.ajax({
                type: "POST",
                url: "QueryPage.aspx/ExpandSearch",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                dataType: "json",
                success: AjaxSucceeded,
                error: AjaxFailed
            });
        }
        function AjaxSucceeded(result) {
            BindCheckBoxList(result);
        }
        function AjaxFailed(result) {
            alert('Failed to load checkbox list');
        }
        function BindCheckBoxList(result) {
            var items = JSON.parse(result.d);
            CreateCheckBoxList(items);
        }
        function CreateCheckBoxList(checkboxlistItems) {
            var table = $('<table></table>');
            var counter = 0;
            var a = "";

            if (obj.CriteriaToExpand == "Manufacturer" && document.getElementById('<%=hdnManufacturer.ClientID%>').value != "") {
                a = document.getElementById('<%=hdnManufacturer.ClientID%>').value.split(", ");
            }
            else if (obj.CriteriaToExpand == "Category" && document.getElementById('<%=hdnCategory.ClientID%>').value != "") {
                a = document.getElementById('<%=hdnCategory.ClientID%>').value.split(", ");
            }
            else if (obj.CriteriaToExpand == "Sub-Category" && document.getElementById('<%=hdnSubCategory.ClientID%>').value != "") {
                a = document.getElementById('<%=hdnSubCategory.ClientID%>').value.split(", ");
            }
    $(checkboxlistItems).each(function () {
        var IsFound = false;
        for (var i = 0; i < a.length; i++) {
            if (parseInt(a[i]) == parseInt(this.Value)) {
                IsFound = true;
                break;
            }
        }
        if (IsFound) {
            table.append($('<tr></tr>').append($('<td></td>').append($('<input>').attr({
                type: 'checkbox', name: 'chklistitem', value: this.Value, checked: 'checked', id: 'chklistitem' + counter
            })).append(
            $('<label>').attr({
                for: 'chklistitem' + counter++
            }).text(this.Name)))
            );
        }
        else {
            table.append($('<tr></tr>').append($('<td></td>').append($('<input>').attr({
                type: 'checkbox', name: 'chklistitem', value: this.Value, id: 'chklistitem' + counter
            })).append(
            $('<label>').attr({
                for: 'chklistitem' + counter++
            }).text(this.Name)))
            );
        }
    });
    $('#dvCheckBoxListControl').empty();
    $('#dvCheckBoxListControl').append(table);
}//Create Checkboxlist close
    </script>
    <table>
        <tr>
            <td style="width: 20%; vertical-align: top;">
                <%--Search Filter--%>
                <div style="border: thin;">
                    <div></div>
                    <button type="button" class="btn btn-primary" onclick="return false;">Narror Your Choices <span class="badge"></span></button>
                    <div>
                        <div style="width: 200px; height: 150px; overflow-y: hidden;">
                            Manufacturer
                       <asp:CheckBoxList ID="chkManufacturer" runat="server" Height="80px">
                       </asp:CheckBoxList>
                        </div>
                        <input type="button" id="btnMoreManufacturer" value="Show More >>" class="btn btn-info" />
                        <hr style="border: thick;" />
                        <div style="width: 200px; height: 150px; overflow-y: hidden;">
                            Category
                        <asp:CheckBoxList ID="chkCategory" runat="server" Height="80px">
                        </asp:CheckBoxList>
                        </div>
                        <input type="button" id="btnMoreCategory" value="Show More >>" class="btn btn-info" />
                        <hr style="border: thick;" />
                        <div style="width: 200px; height: 150px; overflow-y: hidden;">
                            SubCategory
                        <asp:CheckBoxList ID="chkSubCategory" runat="server" Height="80px">
                        </asp:CheckBoxList>
                        </div>
                        <input type="button" id="btnMoreSubCategory" value="Show More >>" class="btn btn-info" />
                        <hr style="border: thick;" />
                        <div id="SearchAttributes">
                            <asp:Literal ID="ltAttributes" runat="server"></asp:Literal>
                        </div>
                        <hr style="border: thick;" />
                        <asp:CheckBox ID="chkInStock" runat="server" Text="In Stock" />
                        <br />
                        <asp:CheckBox ID="chkPricingAvail" runat="server" Text="Pricing Available" />
                    </div>
                    <br />
                    <input type="button" id="btnApplyFilter1" value="Apply Filter" style="visibility: hidden;" />
                    <asp:HiddenField ID="hdnCategory" runat="server" />
                    <asp:HiddenField ID="hdnSubCategory" runat="server" />
                    <asp:HiddenField ID="hdnManufacturer" runat="server" />
                    <asp:HiddenField ID="hdnAttributes" runat="server" />
                    <asp:HiddenField ID="hdnInStock" runat="server" />
                    <asp:HiddenField ID="hdnPricingAvailable" runat="server" />
                    <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-info" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields(1);" />
                </div>
            </td>
            <td style="width: 100%; vertical-align: top;">
                <asp:Literal ID="ltCatalogue" runat="server" EnableViewState="true">

                </asp:Literal>
                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductID"
                    ShowFooter="false" CssClass="mSearchGrid" Width="900px" Style="font-size: 15px"
                    PagerStyle-CssClass="pgr" PagerSettings-Position="Bottom" AlternatingRowStyle-CssClass="alt"
                    RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true"
                    FooterStyle-Wrap="true" GridLines="None" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                    ShowHeader="true" OnRowDataBound="gvProducts_DataBound" OnRowCreated="gvProducts_RowCreated" AllowPaging="true" PageSize="10">
                    <HeaderStyle CssClass="mSearchGrid"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Image ID="imgProduct" runat="server" ToolTip="ProductImage" Width="99%" Height="99%" />
                                <asp:Label ID="lblImagePath" runat="server" style="display:none;" Text='<%# Eval("ImageName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Product" DataField="ProductCode"></asp:BoundField>
                        <asp:BoundField HeaderText="Manufacturer" DataField="Manufacturer"></asp:BoundField>
                        <asp:TemplateField HeaderText="Price" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <span>
                                    <span style="float: left;">
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                        <asp:Label ID="lblPricing" runat="server" Text='<%# Eval("Pricing") %>' Style="display: none;"></asp:Label>
                                        <asp:Literal ID="ltPricing" runat="server"></asp:Literal>
                                    </span>
                                    <span style="float: right; padding-right:30px;">
                                        <%--<asp:Button ID="btnGetPrice" runat="server" class="btn btn-primary" Text="Request" />--%>
                                        <asp:LinkButton ID="btnGetPrice" runat="server" Text="Request" 
                                            OnClientClick='<%# string.Format("javascript:return ProductRequest(\"{0}\",\"{1}\")", Eval("ProductID"),"Price") %>'></asp:LinkButton>
                                    </span>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inventory" ItemStyle-Width="35%">
                            <ItemTemplate>
                                <span>
                                    <div style="float: left;">
                                        <asp:Label ID="lblInventory" runat="server" Text='<%# Eval("Inventory") %>'></asp:Label>
                                    </div>
                                    <div style="float:right;  padding-right:30px;">
                                        <%--<asp:ImageButton class="btn btn-primary" id="btnAddToCart" title="Add to cart" runat="server" ImageUrl="~/images/Cart.png" />--%>
                                        <asp:Button ID="btnAddToCart" class="btn btn-primary" Text="Add To Cart" title="Add to cart" runat="server" 
                                            OnClientClick='<%# string.Format("javascript:return btnAddToCart_Client(\"{0}\")", Eval("ProductID")) %>'
                                             />
                                        <%--<button type="button" class="btn btn-primary" id="btnAddToCart" title="Add to cart" runat="server" style="display: none;">
                                            <span class="glyphicon glyphicon-shopping-cart"></span>
                                        </button>--%>
                                    </div>
                                    <div style="float: right; padding-right:30px;">
                                        <%--<asp:Button ID="btnCallAvail" runat="server" class="btn btn-primary" Text="Call for Availability" />--%>
                                        <asp:LinkButton ID="btnCallAvail" runat="server" Text="Call for Availability" 
                                            OnClientClick='<%# string.Format("javascript:return ProductRequest(\"{0}\",\"{1}\")", Eval("ProductID"),"Availability") %>'/>
                                    </div>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnPopUpSearchCriteria" runat="server" />
    <div id="dialog-form">
        <div>
            <%--<asp:CheckBoxList ID="chklstSearchCriteria" runat="server"></asp:CheckBoxList>--%>
            <div id="dvCheckBoxListControl"></div>
            <br />
            <br />
            <%--            <asp:Button ID="btnPopUpApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-info" OnClick="btnApplyFilter_Click" Style="display: none;" />--%>
            <%--<asp:Button ID="Button1" runat="server" Text="Apply Filter" CssClass="btn btn-info" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields2();" />--%>
        </div>
    </div>
    <div id="EmailPopUp">
        <table>
            <tr>
                <td>Name:</td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email:</td>
                <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Contact No:</td>
                <td><asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnRequestQuote" Text="Request" runat="server" OnClientClick="return ProductRequest2();" style="display:none;" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields2();" />
</asp:Content>
