<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
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

    </script>
    <style type="text/css">
        .lst {
            list-style: disc;
        }
    </style>
    <section>
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                    <div class="left-sidebar" style="background-color: #CFD4D0">
                        <h2>CATALOG</h2>
                        <asp:Literal runat="server" ID="ltList"></asp:Literal><asp:HiddenField ID="hdnCategoryListing" runat="server" />
                        <div style="padding-top: 20px;">
                            <div class="shipping text-center" style="border: 1px solid lightgrey;">
                                <h2>Featured News</h2>
                                <asp:Literal ID="ltFeaturedNews" runat="server"></asp:Literal>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9" style="padding: 0% 1% 1% 1%;">
                    <%-- <div class="listingtbl">--%>
                    <div class="col-sm-9" style="padding: 0.5% 0% 0% 1.5%;">
                        <h2>ABOUT US</h2>
                    </div>
                    <div style="width: 115%">
                        <div class="col-sm-15 pull-left" style="margin-left: 2%;">
                            <br />
                            <p>
                                Integrating our three core functions - product distribution, logistics, and service excellence - together with our versatile customized solutions gives our customers the power to improve efficiency and profits. We help you balance manufacturing challenges always ensuring that the job is done on-time and on-budget.
                                    <br />
                                <br />
                                Take a look at Masline Electronics and you'll discover...
                                <br />
                            </p>
                            <ul>
                                <li class="lst">A team of highly-experienced sales and service experts whose priority is customer service and satisfaction.</li>
                                <li class="lst">The longest-running distributor of industry-leading passive and electromechanical components.</li>
                                <li class="lst">An ISO 9000: 2000 Quality certified company.</li>
                                <li class="lst">More than five miles of product, a worldwide distribution network, and sophisticated inventory management systems.</li>
                                <li class="lst">A sales force of specialists with manufacturing experience and problem-solving know-how.</li>
                            </ul>
                            <p>
                                <br />
                                <br />
                                Since 1932, Masline Electronics continues to deliver world-class products and services. Contact us today to discuss how Masline can...<br />
                            </p>
                            <ul>
                                <li class="lst">improve your workflow and productivity</li>
                                <li class="lst">save you production time and costs</li>
                                <li class="lst">assist your team with creative solutions</li>
                            </ul>
                            <p>
                                Working Opportunities - Masline Electronics is hiring!:
                                <br />
                            </p>
                            <ul>
                                <li class="lst"><a href="Careers.aspx">Job Openings</a></li>
                            </ul>
                            <p>
                                <br />
                                <br />
                                Declarations:
                            </p>
                            <ul>
                                <li class="lst"><a href="404.html">Conflict Free Material</a></li>
                                <li class="lst"><a href="404.html">ISO Certification</a></li>
                            </ul>
                        </div>
                    </div>
                    <%-- </div>--%>
                </div>
            </div>
        </div>
    </section>
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
            <div id="dvCataLogList" style="height: 100%;" runat="server"></div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>

