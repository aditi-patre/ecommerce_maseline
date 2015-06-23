<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true"
    CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        var Product = {};
        /* Add to Cart*/
        function btnAddToCart_Client() {
            Product.ProductID = document.getElementById('<%=hdnProductID.ClientID%>').value;;
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

        function btnAddToCart_Client2(prodID) {
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

        function btnShowCats() {
            ShowPopUp();
            $('#dvCataLogList').append(document.getElementById('<%=hdnCategoryListing.ClientID%>').value);
        }
        function ShowPopUp() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";
            document.getElementById("dialog-form").style.display = "block";
        }

        function HidePopUp() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";
            document.getElementById("dialog-form").style.display = "none";
        }

        function Enlarge(path) {   
            document.getElementById('<%=imgMain.ClientID %>').src = path;
            return false;
        }

        function Zoom()
        {
            EnlargeImage();
            return false;
        }

        function EnlargeImage()
        {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";
            document.getElementById("dialog-form-image").style.display = "block";
            document.getElementById('<%=imgZoomImage.ClientID %>').src = document.getElementById('<%=imgMain.ClientID %>').src;
        }

        function ShrinkImage()
        {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";
            document.getElementById("dialog-form-image").style.display = "none";
        }
    </script>
    <section>
        <div class="container">
            <div class="row">
                <div class="col-sm-3">
                    <div class="left-sidebar" style="background-color: #CFD4D0">
                        <h2>CATALOG</h2>
                        <asp:Literal runat="server" ID="ltList"></asp:Literal><asp:HiddenField ID="hdnCategoryListing" runat="server" />
                        <!--/category-products-->
                        <div style="padding-top: 20px;">
                            <div class="shipping text-center" style="border: 1px solid lightgrey;">
                                <h2>Featured News</h2>
                                <!--featured News-->

                                <div class="media commnets" style="border-bottom: 1px solid lightgrey;">
                                    <a class="pull-left" href="#">
                                        <img src="images/news/media-one.jpg" alt="" />
                                    </a>
                                    <div class="media-body">
                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt .  </p>

                                    </div>
                                </div>

                                <div class="media commnets" style="border-bottom: 1px solid lightgrey;">
                                    <a class="pull-left" href="#">
                                        <img src="images/news/media-one.jpg" alt="" />
                                    </a>
                                    <div class="media-body">
                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt .  </p>

                                    </div>
                                </div>

                                <div class="media commnets" style="border-bottom: 1px solid lightgrey;">
                                    <a class="pull-left" href="#">
                                        <img src="images/news/media-one.jpg" alt="" />
                                    </a>
                                    <div class="media-body">
                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt .  </p>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-15 pull-right" style="border: 1px solid lightgray; padding:10px;">
                    <h2 class="Listheading">PRODUCT DETAILS</h2>

                    <div class="product-details">
                        <!--product-details-->
                        <div class="col-sm-5">
                            <div class="view-product">
                                <img alt="" runat="server" id="imgMain" src="" />
                                
                            </div>
                            <div>
                                <img alt="Enlarge" src="images/enlarge.png" id="iEnlarge" onclick="return Zoom()"/>
                            </div>
                            <div id="similar-product" class="carousel slide" data-ride="carousel">

                                <!-- Wrapper for slides -->
                                <div class="carousel-inner">
                                    <asp:Literal ID="ltSimilarImages" runat="server">
                                    </asp:Literal>
                                </div>

                                <!-- Controls -->
                                <a class="left item-control" href="#similar-product" data-slide="prev">
                                    <i class="fa fa-angle-left"></i>
                                </a>
                                <a class="right item-control" href="#similar-product" data-slide="next">
                                    <i class="fa fa-angle-right"></i>
                                </a>
                            </div>

                        </div>
                        <div class="col-sm-7">
                            <div class="product-information" id="productinfo" runat="server">
                                <!--/product-information-->
                               
                                <h2>
                                    <asp:Label ID="lblProductCode" runat="server"></asp:Label>
                                    <p>
                                        <b style="color:#354362;">Description:</b><br />
                                        <asp:Label ID="lblDescription" runat="server" />
                                    </p>
                                    <p>
                                        <b style="color:#354362;">Manufacturer:</b><br />
                                        <asp:Label ID="lblManufacturer" runat="server" />
                                    </p>
                                    <%--<p><b>Catalog:</b><br/> Solder Tip Cleaner (2 EA per pack) Lumen D Ready Portable DLP</p>--%>
                                    <p>
                                        <b style="color:#354362;">Pricing:</b><br />
                                        <asp:Literal ID="ltPricing" runat="server"></asp:Literal>

                                    </p>
                                    <p>
                                        <b style="color:#354362;">Inventory:</b><br />
                                        <asp:Label ID="lblInventory" runat="server" />
                                    </p>
                                </h2>
                                <asp:ImageButton ID="btnAddToCart" runat="server" ImageUrl="images/product-details/addTocart.jpg" title="Add to cart"
                                    OnClientClick='<%# string.Format("javascript:return btnAddToCart_Client()") %>' />
                                <%--</a>--%>
                            </div>

                            <!--/product-information-->
                        </div>




                    </div>

                    <div class="recommended_items" id="recommendedItems" runat="server">
                        <!--recommended_items-->
                        <h2 style="background-color:lightgray; font-size:14px; height:35px; padding:10px 10px 10px 10px;" class=" ">Related Products</h2>

                        <div id="recommended-item-carousel" class="carousel slide" data-ride="carousel">
                            
                            <div class="carousel-inner">
                                <asp:Literal ID="ltRecommendations" runat="server" />
                                <%--<div class="item active">
                                    <div class="col-sm-44">
                                        <div class="product-image-wrapper">
                                            <div class="single-products">
                                                <div class="productinfo text-center">
                                                    <img src="images/home/recommend1.jpg" alt="" />
                                                    <h2>$56</h2>
                                                    <p>Easy Polo Black Edition</p>
                                                    <button type="button" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-44">
                                        <div class="product-image-wrapper">
                                            <div class="single-products">
                                                <div class="productinfo text-center">
                                                    <img src="images/home/recommend1.jpg" alt="" />
                                                    <h2>$56</h2>
                                                    <p>Easy Polo Black Edition</p>
                                                    <button type="button" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-44">
                                        <div class="product-image-wrapper">
                                            <div class="single-products">
                                                <div class="productinfo text-center">
                                                    <img src="images/home/recommend1.jpg" alt="" />
                                                    <h2>$56</h2>
                                                    <p>Easy Polo Black Edition</p>
                                                    <button type="button" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-sm-44">
                                        <div class="product-image-wrapper">
                                            <div class="single-products">
                                                <div class="productinfo text-center">
                                                    <img src="images/home/recommend1.jpg" alt="" />
                                                    <h2>$56</h2>
                                                    <p>Easy Polo Black Edition</p>
                                                    <button type="button" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-44">
                                        <div class="product-image-wrapper">
                                            <div class="single-products">
                                                <div class="productinfo text-center">
                                                    <img src="images/home/recommend1.jpg" alt="" />
                                                    <h2>$56</h2>
                                                    <p>Easy Polo Black Edition</p>
                                                    <button type="button" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-44">
                                        <div class="product-image-wrapper">
                                            <div class="single-products">
                                                <div class="productinfo text-center">
                                                    <img src="images/home/recommend1.jpg" alt="" />
                                                    <h2>$56</h2>
                                                    <p>Easy Polo Black Edition</p>
                                                    <button type="button" class="btn btn-default add-to-cart"><i class="fa fa-shopping-cart"></i>Add to cart</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>      
                            <a class="left recommended-item-control" href="#recommended-item-carousel" data-slide="prev">
                                <i class="fa fa-angle-left"></i>
                            </a>                      
                            <a class="right recommended-item-control" href="#recommended-item-carousel" data-slide="next" style="right:14.3% !important;">
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnProductID" runat="server" />
    </section>
    <%--Popup for category listing--%>
    <div id="divBackground" style="position: fixed; z-index: 998; height: 100%; width: 100%; top: 0; left: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; display: none">
    </div>
    <div id="dialog-form" class="dvdialog-form" style="background-color: #CFD4D0; height: 800px; z-index: 999; position: absolute; color: #000000; filter: alpha(opacity=60); opacity: 1.0; -moz-opacity: 1.0; left: 35%; top: 12%; display: none;">
        <div style="background-color: #CFD4D0; padding-bottom: 15px; padding-left: 15px; padding-right: 15px; padding-top: 15px; width: 100%; height: 90%; overflow: auto; overflow-x: hidden;">
            <div>
                <div style="height: 30px; text-align: center;">CATALOG</div>
                <div style="float: right; margin-top: -25px;">
                    <a href="#">
                        <img src="images/delete_icon.png" onclick="HidePopUp()" alt="" /></a>
                </div>
            </div>
            <br />
            <div id="dvCataLogList" style="height: 100%;"></div>
            <br />
            <br />
        </div>
    </div>
    <div id="dialog-form-image" class="dvdialog-form" style="background-color: #CFD4D0; height: 500px; width:500px; z-index: 999; position: absolute; color: #000000; filter: alpha(opacity=60); opacity: 1.0; -moz-opacity: 1.0; left: 35%; top: 20%; display: none;">
        <div style="background-color: #CFD4D0; padding:15px 15px 15px 15px; width: 100%; height: 95%; overflow:hidden;">
            <div>
                <div style="float: right; margin-top: -5px;">
                    <a href="#">
                        <img src="images/delete_icon.png" onclick="ShrinkImage()" alt="" /></a>
                </div>
            </div>
            <br />
                <img src="" alt="" id="imgZoomImage" runat="server" style="width:96%; height:96%;"/>
            <br />
            <br />
        </div>
    </div>

</asp:Content>
