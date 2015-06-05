<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true"
    CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <section>
        <div class="container">
            <div class="row">
                <div class="col-sm-3">
                    <div class="left-sidebar" style="background-color: #CFD4D0">
                        <h2>CATALOG</h2>
                        <asp:Literal runat="server" ID="ltList"></asp:Literal>
                        <!--/category-products-->
                        <br />
                        <div class="shipping text-center">
                            <h2>Featured News</h2>
                            <!--featured News-->

                            <div class="media commnets" style="border-bottom: 1px solid black;">
                                <a class="pull-left" href="#">
                                    <img src="images/news/media-one.jpg" alt="" />
                                </a>
                                <div class="media-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt .  </p>

                                </div>
                            </div>

                            <div class="media commnets" style="border-bottom: 1px solid black;">
                                <a class="pull-left" href="#">
                                    <img src="images/news/media-one.jpg" alt="" />
                                </a>
                                <div class="media-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt .  </p>

                                </div>
                            </div>

                            <div class="media commnets" style="border-bottom: 1px solid black;">
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

                <div class="col-sm-15 pull-right">
                    <h2>PRODUCT DETAILS</h2>

                    <div class="product-details">
                        <!--product-details-->
                        <div class="col-sm-5">
                            <div class="view-product">
                                <img alt="" runat="server" id="imgMain" src="" />
                                <%--<h3>ZOOM</h3>--%>
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
                            <div class="product-information">
                                <!--/product-information-->
                                <img src="images/product-details/new.jpg" class="newarrival" alt="" />
                                <h2>
                                    <asp:Label ID="lblProductCode" runat="server"></asp:Label>
                                    <p>
                                        <b>Description:</b><br />
                                        <asp:Label ID="lblDescription" runat="server" />
                                    </p>
                                    <p>
                                        <b>Manufacturer:</b><br />
                                        <asp:Label ID="lblManufacturer" runat="server" />
                                    </p>
                                    <%--<p><b>Catalog:</b><br/> Solder Tip Cleaner (2 EA per pack) Lumen D Ready Portable DLP</p>--%>
                                    <p>
                                        <b>Pricing:</b><br />
                                        <asp:Literal ID="ltPricing" runat="server"></asp:Literal>

                                    </p>
                                    <p>
                                        <b>Inventory:</b><br />
                                        <asp:Label ID="lblInventory" runat="server" />
                                    </p>
                                </h2>
                                <%--</a>--%>
                            </div>
                            <!--/product-information-->
                        </div>




                    </div>

                    <div class="recommended_items">
                        <!--recommended_items-->
                        <h2 class="title text-center">recommended items</h2>

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
                            <a class="right recommended-item-control" href="#recommended-item-carousel" data-slide="next">
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
