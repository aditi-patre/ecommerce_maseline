<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="AdminPanel_ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function getQueryStringParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function SaveProduct() {
            var _Product = {};
            _Product.ID = getQueryStringParameterByName('PID');
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
                    if (JSON.parse(result.d).IsSuccess == false)
                        alert("Product could not be saved!");
                },
                error: function (e) {
                    alert("Product could not be saved!!");
                }
            });
        }

        function ValidateOn_btnRngeAdd() {
            if (document.getElementById('<%=txtRngeFrom1.ClientID %>').value == "") {
                alert("Please enter value for range.");
                return false;
            }
            if (document.getElementById('<%=txtRngePrice.ClientID %>').value == "") {
                alert("Please enter a valid value for price.");
                return false;
            }
            return true;
        }

        function ValidatePricing() {
            if (document.getElementById('<%=chkIsPricing.ClientID %>').checked == true) {
                document.getElementById('<%=tblPricing.ClientID %>').style.display = "block";
                document.getElementById('<%=txtPrice.ClientID %>').disabled = true;
            }
            else {
                document.getElementById('<%=txtPrice.ClientID %>').disabled = false;
                document.getElementById('<%=tblPricing.ClientID %>').style.display = "none";
            }
        }

        function Validate_btnSave() {
            _ddlM = document.getElementById('<%=ddlManufacturer.ClientID %>');
            _ddlC = document.getElementById('<%=ddlCategoryID.ClientID %>');
            if (document.getElementById('<%=txtProductCode.ClientID %>').value == "") {
                alert("Please enter a valid unique product code");
                return false;
            }
            else if (_ddlM.options[_ddlM.selectedIndex].value == "0") {
                alert("Please select a valid manufacturer");
                return false;
            }
            else if (_ddlC.options[_ddlC.selectedIndex].value == "0") {
                alert("Please select a valid category");
                return false;
            }

            return true;
        }

        function isNumber(evt) {

            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isDecimal(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                  (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        window.onload = function () {
            if (document.getElementById('<%=chkIsPricing.ClientID %>').checked == true) {
                document.getElementById('<%=tblPricing.ClientID %>').style.display = "block";
                document.getElementById('<%=txtPrice.ClientID %>').disabled = true;
            }
            else {
                document.getElementById('<%=txtPrice.ClientID %>').disabled = false;
                document.getElementById('<%=tblPricing.ClientID %>').style.display = "none";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
        <div style="padding: 1.5% 2.5% 2.5% 2.5%;">
            <div class="listingtbl">
                <div class="col-sm-9" style="padding: 10px 10px 10px 12px; width: 100%;">
                    <%--style="padding: 2% 2% 2% 2%; width: 144%;">--%>
                    <h2>PRODUCT DETAILS</h2>
                    <br />
                    <%-- <table style="width: 80%; text-align: center;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>Name : </td>
                                <td>
                                   <asp:TextBox ID="txtProductName" runat="server" EnableViewState="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Description : </td>
                                <td>
                                    <asp:TextBox ID="txtProductDescrip" runat="server" TextMode="MultiLine" Rows="4" EnableViewState="true" CssClass="MultiLineText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Manufacturer :</td>
                                <td>
                                    <asp:DropDownList ID="ddlManufacturer" runat="server" EnableViewState="true" CssClass="dropdownSearch"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Category : </td>
                                <td>
                                    <asp:DropDownList ID="ddlCategoryID" runat="server" EnableViewState="true" CssClass="dropdownSearch"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Technology : </td>
                                <td>
                                   <asp:TextBox ID="txtTechnology" runat="server" EnableViewState="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Price : </td>
                                <td>
                                   <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Main Image : </td>
                                <td>
                                    <input name="" type="text" />&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>Product Code : </td>
                                <td>
                                    <input name="Input3" type="text" /></td>
                            </tr>
                            <tr>
                                <td>Inventory : </td>
                                <td>
                                    <textarea name="textarea" cols="" rows=""></textarea></td>
                            </tr>
                            <tr>
                                <td>Sub Category : </td>
                                <td>
                                    <select name="select2">
                                    </select></td>
                            </tr>
                            <tr>
                                <td>Harmonized Code : </td>
                                <td>
                                    <select name="select2">
                                    </select>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <input name="" type="checkbox" value="" />
                                    &nbsp;Is multiple pricing available? </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>--%>
                </div>
                <div style="width: 128%;">
                    <div class="col-sm-15 pull-left" style="padding: 0px 5px 5px 12px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 10%;">Name:<br />
                                </td>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtProductName" runat="server" EnableViewState="true"></asp:TextBox><br />
                                    <br />
                                </td>
                                <td style="width: 20%;">Product Code:<br />
                                    <br />
                                </td>
                                <td style="width: 40%;">
                                    <asp:TextBox ID="txtProductCode" runat="server" EnableViewState="true"></asp:TextBox><br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>Description:<br />
                                    <br />
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtProductDescrip" runat="server" TextMode="MultiLine" Rows="3" EnableViewState="true" CssClass="MultiLineText" Style="width: 53%;"></asp:TextBox><br />
                                    <br />
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Manufacturer:<br />
                                    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManufacturer" runat="server" EnableViewState="true" CssClass="dropdownSearch"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlCategoryID" runat="server" EnableViewState="true" CssClass="dropdownSearch"></asp:DropDownList>
                                    <br />
                                </td>
                                <td>SubCategory:<br />
                                    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubCategoryID" runat="server" EnableViewState="true" CssClass="dropdownSearch"></asp:DropDownList>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>Technology:<br />
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTechnology" runat="server" EnableViewState="true"></asp:TextBox>
                                    <br />
                                </td>
                                <td>Harmonized Code:<br />
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHarmonizedCode" runat="server" EnableViewState="true"></asp:TextBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>Price:<br />
                                    <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrice" runat="server" onkeypress="return isDecimal(event)"></asp:TextBox><br />
                                    <br />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkIsPricing" runat="server" Text="Is Multiple Pricing Available?" onclick="ValidatePricing()" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">Main Image
                                </td>
                                <td style="vertical-align: top;">
                                    <asp:UpdatePanel ID="upnlImageList" UpdateMode="Always" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlImageList" runat="server" CssClass="dropdownSearch" OnSelectedIndexChanged="ddlImageList_SelectedIndexChanged" AutoPostBack="true" Style="float: left;"></asp:DropDownList>

                                            <div id="idivMainImage" runat="server" style="float: left; text-align: right; height: 30px; width: 30px; border: 1px solid gray; background-repeat: no-repeat; background-size: cover;">
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ddlImageList" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2" rowspan="4">
                                    <table style="width: 411px; border: 1px solid #CCCCCC;" id="tblPricing" runat="server">
                                        <tr>
                                            <td>Min Qty:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRngeFrom1" runat="server" Width="50px" onkeypress="return isNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>Max Qty:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRngeTo1" runat="server" Width="50px" onkeypress="return isNumber(event)"></asp:TextBox>
                                            </td>
                                            <td>Price:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRngePrice" runat="server" Width="100px" onkeypress="return isDecimal(event)"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnRngeAdd" runat="server" CssClass="btn btn-primary" OnClick="btnRngeAdd_Click" Text="Add" OnClientClick="return ValidateOn_btnRngeAdd();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:GridView ID="gvPricing" runat="server" DataKeyNames="RangeID" OnRowCommand="gvPricing_RowCommand" OnRowDataBound="gvPricing_RowDataBound"
                                                    OnRowCancelingEdit="gvPricing_RowCancelingEdit" OnRowEditing="gvPricing_RowEditing" OnRowUpdating="gvPricing_RowUpdating" BorderColor="LightGray"
                                                    AutoGenerateColumns="false" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Qty Range">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQtyRange" runat="server" Text='<%# Eval("QtyRange")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                From:<asp:TextBox runat="server" ID="txtQtyRange1" Text='<%# Eval("Range1")%>' Width="30px" onkeypress="return isNumber(event)" />
                                                                To:<asp:TextBox runat="server" ID="txtQtyRange2" Text='<%# Eval("Range2")%>' Width="30px" onkeypress="return isNumber(event)" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Price" DataField="Price" ItemStyle-CssClass="cart_description" ItemStyle-Width="40px" ControlStyle-Width="40px" HeaderStyle-Width="40px" />
                                                        <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" ControlStyle-CssClass="btn btn-primary" />
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
                            <tr style="vertical-align: top;">
                                <td>Images:<br />
                                    <br />
                                </td>
                                <td>
                                    <asp:FileUpload ID="fupldImage" runat="server" /><asp:ImageButton ID="btnAddImage" runat="server" OnClick="btnAddImage_Click" ImageUrl="~/images/Upload.png" ToolTip="Upload Image" Style="height: 20px; float: right; padding-right: 10%; margin-top: -7%;" />
                                    <br />
                                </td>
                                <%--<td colspan="2">
                        <asp:Button ID="btnAddImage" runat="server" OnClick="btnAddImage_Click" CssClass="btn btn-primary" Text="Upload Image" Style="margin-top: 0px;" />
                        <br />
                    </td>--%>
                            </tr>
                            <tr>
                                <td colspan="2" rowspan="2" style="vertical-align: top;">
                                    <div class="floating">
                                        <asp:DataList ID="rptImages" runat="server" OnItemCommand="rptImages_ItemCommand" RepeatDirection="Horizontal" RepeatColumns="5">
                                            <ItemTemplate>
                                                <div style="background-image: url('<%#GetImageFromByte(DataBinder.Eval(Container.DataItem,"ImagePath"),DataBinder.Eval(Container.DataItem,"ImageName")) %>'); text-align: right; height: 50px; width: 50px; border: 1px solid gray; background-repeat: no-repeat; background-size: cover;">
                                                    <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/images/delete_icon.png" Width="10px" Height="10px" CommandName="DeleteI" OnClientClick="if (confirm('Are you sure you want to delete the Image?') == false) return false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px;">
                                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" EnableViewState="true" />
                                    <asp:HiddenField ID="hdnCatInfo" runat="server" EnableViewState="true" />
                                    &nbsp;<br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="padding-top: 2%;">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary pull-right" Style="margin-top: 0px !important;" OnClick="btnBack_Click"></asp:Button>
                                </td>
                                <td style="padding-top: 2%;">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary pull-left" Style="margin-top: 0px !important;" OnClick="btnSave_Click" OnClientClick="return Validate_btnSave();"></asp:Button>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

