<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="ProductListing.aspx.cs" Inherits="ProductListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script>var $j = jQuery.noConflict(true);</script>    
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
            $j("[id*=chkManufacturer] input:checked").each(function (index, item) {
                if (selManufacturer == "")
                    selManufacturer += $(item).val();
                else
                    selManufacturer += ", " + $(item).val();
            });
            document.getElementById('<%=hdnManufacturer.ClientID%>').value = selManufacturer;

            $j("[id*=chkCategory] input:checked").each(function (index, item) {
                if (selCategory == "")
                    selCategory += $(item).val();
                else
                    selCategory += ", " + $(item).val();
            });
            document.getElementById('<%=hdnCategory.ClientID%>').value = selCategory;

            $j("[id*=chkSubCategory] input:checked").each(function (index, item) {
                if (selSubCategory == "")
                    selSubCategory += $(item).val();
                else
                    selSubCategory += ", " + $(item).val();
            });
            document.getElementById('<%=hdnSubCategory.ClientID%>').value = selSubCategory;

            document.getElementById('<%=hdnInStock.ClientID%>').value = document.getElementById('MainContentPlaceHolder_chkInStock').checked;
            document.getElementById('<%=hdnPricingAvailable.ClientID%>').value = document.getElementById('MainContentPlaceHolder_chkPricingAvail').checked;
            document.getElementById('<%=hdnPriceRange.ClientID%>').value = $j(".tooltip-inner")[0].innerHTML;
            var Attributes = "";
            $j("#SearchAttributes input[type=text]").each(function () {
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
            if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value.indexOf("Manufacturer") != -1) {
            $j("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
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
            else if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value.indexOf("Category") != -1) {
                $j("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
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
            else if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value.indexOf("Sub-Category") != -1) {
                $j("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
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

            document.getElementById('<%=hdnInStock.ClientID%>').value = document.getElementById('MainContentPlaceHolder_chkInStock').checked;
            document.getElementById('<%=hdnPricingAvailable.ClientID%>').value = document.getElementById('MainContentPlaceHolder_chkPricingAvail').checked;
            document.getElementById('<%=hdnAttributes.ClientID%>').value = document.getElementById('<%=hdnAttributes.ClientID%>').value;
            return true;
        }

        /* Add to Cart*/
        function btnAddToCart_Client(prodID) {
            Product.ProductID = prodID;
            $.ajax({
                type: "POST",
                url: "QueryPage.aspx/AddToCart",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(Product),
                dataType: "json",
                success: function (result) {
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
        function ProductRequest(prodID, obj) {
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

        function ProductRequest2() {
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

        /**Javascript popup**/
        var popUpObj;
        function ShowMoreSearchOption(btn)
        {
            if (btn.id.indexOf("btnMoreManufacturer") != -1) {
                SearchCriteria = "Select " + "Manufacturer";               
            }
            else if (btn.id.indexOf("btnMoreCategory") != -1) {
                SearchCriteria = "Select " + "Category";
            }
            else if (btn.id.indexOf("btnMoreSubCategory") != -1) {
                SearchCriteria = "Select " + "SubCategory";
            }
            document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value = SearchCriteria;
            $("#PopUpHeading")[0].innerHTML = SearchCriteria;
            PopulateCheckBoxList(SearchCriteria);
            ShowPopUp();
        }

        function ShowPopUp()
        {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";
            document.getElementById("dialog-form").style.display = "block";
            $("#PopUpHeading")[0].innerHTML = SearchCriteria;
        }

        function HidePopUp()
        {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";
            document.getElementById("dialog-form").style.display = "none";
        }
        /**Javascript popup end**/

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

    <section>
        <div class="container">
            <div style="margin-top:-4%;">
                <h2>CATALOGUE</h2>
            </div>
            <div class="breadcrumbs">
                <ol class="breadcrumb">
                    <li><a href="#">Home</a></li>
                    <li class="active">Product Listing</li>
                </ol>
            </div>
            <div class="row" style="margin-top:-5%;">
                <div class="col-sm-3">
                    <div class="left-sidebar" style="width:85%;">
                        <h2>MODIFY SEARCH</h2>
                        <div class="brands_products">
                            <!--brands_products-->
                            <h3>Manufacture</h3>
                            <div style="height: 160px; overflow-y: hidden;">
                                <div class="brands-name">
                                    <asp:CheckBoxList ID="chkManufacturer" runat="server" Height="80px" CssClass="SearchCheckbox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <input type="button" id="btnMoreManufacturer" value="Show More >>" class="btn btn-primary" onclick="ShowMoreSearchOption(this)" />
                        </div>
                        <!--/brands_products-->
                        <div class="brands_products">
                            <!--brands_products-->
                            <h3>Category</h3>
                            <div style="height: 160px; overflow-y: hidden;">

                                <div class="brands-name">
                                    <asp:CheckBoxList ID="chkCategory" runat="server" Height="80px" CssClass="SearchCheckbox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <input type="button" id="btnMoreCategory" value="Show More >>" class="btn btn-primary"  onclick="ShowMoreSearchOption(this)" />
                        </div>
                        <!--/brands_products-->

                        <!--SubCategory-->
                        <div class="brands_products">
                            <!--brands_products-->
                            <h3>SubCategory</h3>
                            <div style="height: 160px; overflow-y: hidden;">
                                <div class="brands-name">
                                    <asp:CheckBoxList ID="chkSubCategory" runat="server" Height="80px" CssClass="SearchCheckbox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <input type="button" id="btnMoreSubCategory" value="Show More >>" class="btn btn-primary"  onclick="ShowMoreSearchOption(this)" />
                        </div>
                        <!--/SubCategory-->
                        <br />
                        <br />
                        <div id="SearchAttributes">
                            <asp:Literal ID="ltAttributes" runat="server"></asp:Literal>
                        </div>
                        <div class="price-range">
                            <!--price-range-->
                            <h3>Price Range</h3>
                            <div class="well">
                                <input type="text" class="span2" value="" data-slider-min="0" data-slider-max="600" data-slider-step="5" 
                                    data-slider-value="[250,450]" id="sl2" style=" width:98%;" /><br />
                                <b>$ 0</b> <b class="pull-right">$ 600</b>
                            </div>
                        </div>
                        <!--/price-range-->

                        <asp:CheckBox ID="chkInStock" runat="server" Text="In Stock" CssClass="SearchCheckbox"/>
                        <br />
                        <asp:CheckBox ID="chkPricingAvail" runat="server" Text="Pricing Available"  CssClass="SearchCheckbox"/>
                        <input type="button" id="btnApplyFilter1" value="Apply Filter" style="visibility: hidden;" />
                        <asp:HiddenField ID="hdnCategory" runat="server" />
                        <asp:HiddenField ID="hdnSubCategory" runat="server" />
                        <asp:HiddenField ID="hdnManufacturer" runat="server" />
                        <asp:HiddenField ID="hdnAttributes" runat="server" />
                        <asp:HiddenField ID="hdnInStock" runat="server" />
                        <asp:HiddenField ID="hdnPricingAvailable" runat="server" />
                        <asp:HiddenField ID="hdnPriceRange" runat="server" />
                        <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-primary" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields(1);" />
                    </div>
                </div>

                <div class="col-sm-9">
                    <!--features_items-->
                    <h2>PRODUCT LISTINGS</h2>
                    <div>
                        <%--<ul class="pagination">
                            <li class="active"><a href="">1</a></li>
                            <li><a href="">2</a></li>
                            <li><a href="">3</a></li>
                            <li><a href="">&raquo;</a></li>
                        </ul>--%>
                        <ul class="pagination">
                            <asp:Repeater ID="rptPager" runat="server">
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
                <div class="col-sm-15 pull-left">
                    <div class="features_items">
                        <div class="table-responsive cart_info">
                            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductID"
                                ShowFooter="false" CssClass="table table-condensed" Width="1000px" Style="font-size: 15px"
                                PagerStyle-CssClass="pgr" PagerSettings-Position="Bottom" AlternatingRowStyle-CssClass="alt"
                                RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true"
                                FooterStyle-Wrap="true" GridLines="None" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                                ShowHeader="true" OnRowDataBound="gvProducts_DataBound" OnRowCreated="gvProducts_RowCreated" AllowPaging="true" PageSize="10"
                                OnRowCommand="gvProducts_RowCommand">
                                <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                        <HeaderStyle CssClass="image" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="cart_product" />
                                        <ItemTemplate>
                                            <asp:Image ID="imgProduct" runat="server" ToolTip="ProductImage" style=" width:100px; height:75px;" />
                                            <asp:Label ID="lblImagePath" runat="server" Style="display: none;" Text='<%# Eval("ImageName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField >
                                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="30%">
                                        <HeaderStyle CssClass="description" />
                                        <ItemStyle HorizontalAlign="center" VerticalAlign="Top" CssClass="cart_description" />
                                        <ControlStyle CssClass="cart_description" />
                                        <ItemTemplate>
                                            <h4>
                                                <asp:LinkButton ID="lbtnProdName" runat="server" Text='<%# Eval("Name") %>' CommandName="ShowProductDetails" CommandArgument='<%# Eval("ProductID") %>'></asp:LinkButton>
                                            </h4>
                                          <%--  <p id="lblProductCode" runat="server"><%# Eval("ProductCode") %></p>--%>
                                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("ProductCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Item" DataField="ProductCode" ItemStyle-CssClass="cart_description"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="Manufacturer" DataField="Manufacturer" ItemStyle-CssClass="cart_description" ItemStyle-Width="15%"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                        <HeaderStyle CssClass="cart_total" />
                                        <ItemStyle CssClass="cart_total" />
                                        <ItemTemplate>
                                            <span>
                                                <asp:Label ID="lblPricing" runat="server" Text='<%# Eval("Pricing") %>' Style="display: none;"></asp:Label>
                                                <asp:Literal ID="ltQty" runat="server"></asp:Literal>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                        <HeaderStyle CssClass="cart_total" />
                                        <ItemStyle CssClass="cart_total" />
                                        <ItemTemplate>
                                            <span>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                <asp:Literal ID="ltPricing" runat="server"></asp:Literal>
                                            </span>
                                            <span style="float: right; padding-right: 30px;">
                                                <asp:LinkButton ID="btnGetPrice" runat="server" Text="Request"
                                                    OnClientClick='<%# string.Format("javascript:return ProductRequest(\"{0}\",\"{1}\")", Eval("ProductID"),"Price") %>'></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inventory" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemStyle CssClass="cart_quantity" />
                                        <ItemTemplate>
                                            <span>
                                                <div style="float: left;">
                                                    <asp:Label ID="lblInventory" runat="server" Text='<%# Eval("Inventory") %>'></asp:Label>
                                                </div>
                                                <div style="padding-left: 100px;">
                                                    <%--<asp:Button ID="btnAddToCart" class="btn btn-primary" Text="Add To Cart" title="Add to cart" runat="server"
                                                        OnClientClick='<%# string.Format("javascript:return btnAddToCart_Client(\"{0}\")", Eval("ProductID")) %>' />--%>
                                                    <asp:ImageButton ID="btnAddToCart" runat="server" ImageUrl="images/product-details/addTocart.jpg" title="Add to cart"
                                                        OnClientClick='<%# string.Format("javascript:return btnAddToCart_Client(\"{0}\")", Eval("ProductID")) %>' />
                                                </div>
                                                <div style="padding-left: 100px;">
                                                    <asp:LinkButton ID="btnCallAvail" runat="server" Text="Call for Availability"
                                                        OnClientClick='<%# string.Format("javascript:return ProductRequest(\"{0}\",\"{1}\")", Eval("ProductID"),"Availability") %>' />
                                                </div>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Price">
                                        <HeaderStyle CssClass="price" />
                                        <ItemStyle CssClass="cart_price" />
                                        <ItemTemplate>
                                            <span>
                                                <span style="float: left;">
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                    <asp:Label ID="lblPricing" runat="server" Text='<%# Eval("Pricing") %>' Style="display: none;"></asp:Label>
                                                    <asp:Literal ID="ltPricing" runat="server"></asp:Literal>
                                                </span>
                                                <span style="float: right; padding-right: 30px;">
                                                    <asp:LinkButton ID="btnGetPrice" runat="server" Text="Request"
                                                        OnClientClick='<%# string.Format("javascript:return ProductRequest(\"{0}\",\"{1}\")", Eval("ProductID"),"Price") %>'></asp:LinkButton>
                                                </span>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField> --%>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <%--                           <ul class="pagination"> <li class="active"><a href="">1</a></li>
                            <li><a href="">2</a></li>
                            <li><a href="">3</a></li>
                            <li><a href="">&raquo;</a></li>
    </ul>--%>
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
                        <%--  <asp:LinkButton ID="LinkButton2" runat="server" CssClass="labelText" Text=">>"
                            CausesValidation="false" OnClick="lbtnPrevious3_Click">&raquo;</asp:LinkButton>
                        <asp:DataList ID="dlPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="dlPaging_ItemCommand"
                            OnItemDataBound="dlPaging_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnPaging" runat="server" CssClass="labelText" CommandArgument='<%# Eval("PageIndex") %>'
                                    CommandName="Paging" Text='<%# Eval("PageText") %>'></asp:LinkButton>&nbsp;
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:LinkButton ID="LinkButton1" CssClass="labelText" runat="server" Text=">>"
                            CausesValidation="false" OnClick="lbtnNext3_Click"></asp:LinkButton>--%>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <asp:HiddenField ID="hdnPopUpSearchCriteria" runat="server" />
    <div id="EmailPopUp" style="display: none;">
        <table>
            <tr>
                <td>Name:</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email:</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Contact No:</td>
                <td>
                    <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnRequestQuote" Text="Request" runat="server" OnClientClick="return ProductRequest2();" Style="display: none;" />
                </td>
            </tr>
        </table>
    </div>


    <div id="divBackground" style="position: fixed; z-index: 998; height: 100%; width: 100%; top: 0; left: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; display: none">
    </div>
    <div id="dialog-form" style="width: 550px; height: 550px; z-index: 999; position: absolute; color: #000000; background-color: #ffffff; filter: alpha(opacity=60); opacity: 1.0; -moz-opacity: 1.0; left: 35%; top: 12%; display: none;">
        <div style="padding-bottom: 15px; padding-left: 15px; padding-right: 15px; padding-top: 15px; width: 100%; height: 90%; overflow: auto; overflow-x: hidden;">
            <div>
                <div id="PopUpHeading" style="height: 30px; text-align: center;"></div>
                <div style="float: right; margin-top: -25px;">
                    <asp:ImageButton ID="lbtnRemove" runat="server" OnClientClick="HidePopUp();" ImageUrl="~/images/delete_icon.png" /></div>
            </div>
            <br />
            <div id="dvCheckBoxListControl"></div>
            <br />
            <br />
        </div>
        <asp:Button ID="btnDone" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields2();" />
    </div>

    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields2();" />
</asp:Content>

