<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%-- Add content controls here --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <section id="slider">
        <!--slider-->
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="mainmenu pull-left">
                        <ul class="nav navbar-nav collapse navbar-collapse">
                            <li><a href="index.html" class="active">HOME</a></li>
                            <li class="dropdown"><a href="#">CATALOG<i class="fa fa-angle-down"></i></a>
                                <ul role="menu" class="sub-menu">
                                    <li><a href="ProductListing.aspx">Products</a></li>
                                    <li><a href="ProductDetails.aspx">Product Details</a></li>
                                    <li><a href="Login.aspx?Checkout=Y">Checkout</a></li>
                                    <li><a href="ViewCart.aspx">Cart</a></li>
                                    <li><a href="Login.aspx">Login</a></li>
                                </ul>
                            </li>
                            <li class="dropdown"><a href="#">SERVICES<i class="fa fa-angle-down"></i></a>
                                <ul role="menu" class="sub-menu">
                                    <li><a href="blog.html">Blog List</a></li>
                                    <li><a href="blog-single.html">Blog Single</a></li>
                                </ul>
                            </li>
                            <li><a href="404.html">PRODUCT VIDEOS</a></li>
                            <li><a href="contact-us.html">LINE CARD</a></li>
                        </ul>
                    </div>
                    <div class="welcomeBg">welcome  to   Homepage</div>
                    <div id="slider-carousel" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#slider-carousel" data-slide-to="0" class="active"></li>
                            <li data-target="#slider-carousel" data-slide-to="1"></li>
                            <li data-target="#slider-carousel" data-slide-to="2"></li>
                        </ol>

                        <div class="carousel-inner">
                            <div class="item active">

                                <div class="col-sm-6">
                                    <img src="images/home/banner1.jpg" class="girl img-responsive" alt="" />

                                </div>
                            </div>
                            <div class="item ">

                                <div class="col-sm-6">
                                    <img src="images/home/banner2.jpg" class="girl img-responsive" alt="" />

                                </div>
                            </div>

                            <div class="item ">

                                <div class="col-sm-6">
                                    <img src="images/home/banner3.jpg" class="girl img-responsive" alt="" />

                                </div>
                            </div>

                        </div>

                        <a href="#slider-carousel" class="left control-carousel hidden-xs" data-slide="prev">
                            <i class="fa fa-angle-left"></i>
                        </a>
                        <a href="#slider-carousel" class="right control-carousel hidden-xs" data-slide="next">
                            <i class="fa fa-angle-right"></i>
                        </a>
                    </div>

                </div>




  <%--              <div>
                    <div id="slider-carousel" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#slider-carousel" data-slide-to="0" class="active"></li>
                            <li data-target="#slider-carousel" data-slide-to="1"></li>
                            <li data-target="#slider-carousel" data-slide-to="2"></li>
                        </ol>

                        <div class="carousel-inner">
                            <div class="item active">

                                <div class="col-sm-6">
                                    <img src="images/home/banner1.jpg" />
                                </div>
                                <div class="mainmenu pull-left">
                                    <ul class="nav navbar-nav collapse navbar-collapse">
                                        <li><a href="Home.aspx" class="active">HOME</a></li>
                                        <li class="dropdown"><a href="#">CATALOG<i class="fa fa-angle-down"></i></a>
                                            <ul role="menu" class="sub-menu">
                                                <li><a href="ProductListing.aspx">Products</a></li>
                                                <li><a href="ProductDetails.aspx">Product Details</a></li>
                                                <li><a href="checkout.html">Checkout</a></li>
                                                <li><a href="ViewCart.aspx">Cart</a></li>
                                                <li><a href="Login.aspx">Login</a></li>
                                            </ul>
                                        </li>
                                        <li class="dropdown"><a href="#">SERVICES<i class="fa fa-angle-down"></i></a>
                                            <ul role="menu" class="sub-menu">
                                                <li><a href="blog.html">Blog List</a></li>
                                                <li><a href="blog-single.html">Blog Single</a></li>
                                            </ul>
                                        </li>
                                        <li><a href="404.html">PRODUCT VIDEOS</a></li>
                                        <li><a href="contact-us.html">LINE CARD</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="item">

                                <div class="col-sm-6">
                                    <img src="images/home/banner2.jpg" />
                                </div>
                            </div>

                            <div class="item">

                                <div class="col-sm-6">
                                    <img src="images/home/banner3.jpg" />
                                </div>
                            </div>

                        </div>

                        <a href="#slider-carousel" class="left control-carousel hidden-xs" data-slide="prev">
                            <i class="fa fa-angle-left"></i>
                        </a>
                        <a href="#slider-carousel" class="right control-carousel hidden-xs" data-slide="next">
                            <i class="fa fa-angle-right"></i>
                        </a>
                    </div>

                </div>--%>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="col-sm-9" style="padding-left:15px; padding-right:15px;">
        <h2>
            <img src="images/home/icon.png" />WHY MASLINE?</h2>

        <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus id odio purus. Praesent non tortor sit amet neque ullamcorper cursus. Praesent rhoncus, enim ut mattis auctor, urna felis iaculis dolor, eu sollicitudin diam ipsum eget turpis. Sed placerat nunc vel pretium cursus. In posuere consequat risus, ut tristique felis ultricies et. Aliquam eu justo a purus efficitur tempus at eget turpis. In faucibus dolor erat, a efficitur nunc sodales vel. Duis tincidunt, leo vel placerat interdum, nibh urna iaculis elit, a molestie odio orci eu urna. Proin et imperdiet lectus, vel vehicula ligula. Praesent at ante semper, pulvinar erat at, scelerisque ante. Sed varius sit amet neque sed feugiat. Duis vehicula, neque iaculis cursus laoreet, arcu nisi rhoncus est, sed fermentum nibh ex ac urna. Maecenas quis sodales nisi, ut accumsan tortor. Vivamus et neque at orci consectetur blandit. Nulla non eros mi.
            <br />
            <br />

            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus id odio purus. Praesent non tortor sit amet neque ullamcorper cursus. Praesent rhoncus, enim ut mattis auctor, urna felis iaculis dolor, eu sollicitudin diam ipsum eget turpis. Sed placerat nunc vel pretium cursus. In posuere consequat risus, ut tristique felis ultricies et. Aliquam eu justo a purus efficitur tempus at eget turpis. In faucibus dolor erat, a efficitur nunc sodales vel. Duis tincidunt, leo vel placerat interdum, nibh urna iaculis elit, a molestie odio orci eu urna. Proin et imperdiet lectus, vel vehicula ligula. Praesent at ante semper, pulvinar erat at, scelerisque ante. Sed varius sit amet neque sed feugiat. Duis vehicula, neque iaculis cursus laoreet, arcu nisi rhoncus est, sed fermentum nibh ex ac urna. Maecenas quis sodales nisi, ut accumsan tortor. Vivamus et neque at orci consectetur blandit. Nulla non eros mi.

            <br />
            <br />

            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus id odio purus. Praesent non tortor sit amet neque ullamcorper cursus. Praesent rhoncus, enim ut mattis auctor, urna felis iaculis dolor, eu sollicitudin diam ipsum eget turpis. Sed placerat nunc vel pretium cursus. In posuere consequat risus, ut tristique felis ultricies et. Aliquam eu justo a purus efficitur tempus at eget turpis. In faucibus dolor erat, a efficitur nunc sodales vel. Duis tincidunt, leo vel placerat interdum, nibh urna iaculis elit, a molestie odio orci eu urna. Proin et imperdiet lectus, vel vehicula ligula. Praesent at ante semper, pulvinar erat at, scelerisque ante. Sed varius sit amet neque sed feugiat. Duis vehicula, neque iaculis cursus laoreet, arcu nisi rhoncus est, sed fermentum nibh ex ac urna. Maecenas quis sodales nisi, ut accumsan tortor. Vivamus et neque at orci consectetur blandit. Nulla non eros mi.
        </p>
    </div>
    <div class="col-sm-3 ">
        <ul class="nav nav-pills nav-stacked">
            <li>
                <img src="images/home/video1.jpg" /></li>
            <li>
                <img src="images/home/video2.jpg" /></li>
        </ul>
    </div>

</asp:Content>
