<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="Careers.aspx.cs" Inherits="Careers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function btnShowCats() {
            ShowPopUp();
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
    <section>
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                    <div class="left-sidebar" style="background-color: #CFD4D0">
                        <h2>CATALOG</h2>
                        <asp:Literal runat="server" ID="ltList"></asp:Literal>
                        <div style="padding-top: 20px; background-color: white;">
                            <div class="shipping text-center" style="border: 1px solid lightgrey;">
                                <h2>Featured News</h2>
                                <asp:Literal ID="ltFeaturedNews" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9" style="padding-left: 2%; font-size: 16px;">
                        <div class="col-sm-9">
                            <h2>CAREERS</h2>
                        </div>
                        <div class="col-md-15 pull-left">
                            <br />
                            <p>Masline is Hiring !</p>
                            <p>We are an established, profitable and growing distributor of electromechanical, passive components and custom solutions. Masline is always seeking great talent. If you have a set of skills that could benefit our customers; WE WANT to HEAR FROM YOU !</p>
                            <p>Send your resume via email to <a href="mailto:jobs@masline.com">jobs@masline.com</a>.</p>
                            <p><b>Current Open Opportunities at Masline Electronics:</b></p>
                            <hr />
                            <ul>
                                <li><b><a href="http://www.masline.com/content/career-job-listing-helpdesk-technician">Helpdesk Technician</a></b> - <i>Rochester, NY</i> - full-time, self-starting candidate to become our lead helpdesk technician. This position has no ceiling, and is only limited by what the candidate knows, or is willing to learn. The preferred candidate will be our employee’s lifeline to the software and technical tools required to accomplish their job, and help our business grow. We are looking for someone able to find, learn and apply new technologies in appropriate situations. - last updated: 20150702</li>
                            </ul>
                            <hr />
                            <ul>
                                <li><b><a href="http://s.masline.com/rocsalesrep">Industrial Electronics Sales Representative</a></b> - <i>Rochester, NY</i> - office based / non-commission / promotional track<br />
                                    Self-Starting Dynamic Office Based Sales Representative to initiate contacts with prospective and past Masline Electronics customers. Responsibilities include developing &amp; growing business contacts by calling on buyers, engineers &amp; their managers. (Non-Commission) (Training) (Career Track Entry)  - last updated: 20150702</li>
                            </ul>
                            <hr />
                            <ul>
                                <li><b><a href="#">Inside Sales Representative</a></b> - <i>Rochester, NY</i> - Established Clients with specific responsibilities plus serious opportunities to develop and grow OEMs and contract manufacturers, calling on buyers, engineers and their managers. - last updated: 20150702</li>
                            </ul>
                            <hr />
                            <ul>
                                <li><b><a href="careers-job-listing-ne-outside-sales">Outside Sales Representative</a></b> - <i>Northeastern States</i> - Territorial responsibilities would be to develop and grow OEMs and contract manufacturers, calling on buyers, engineers and their managers
                                    <ul>
                                        <li><a href="http://s.masline.com/neohsales">Cleveland, Ohio</a> - last updated: 20150702</li>
                                        <div style="display: none;">
                                            <li><a href="http://s.masline.com/wpasales">Pittsburgh, Pennsylvania</a> - last updated: 20150702</li>
                                        </div>
                                    </ul>
                                </li>
                            </ul>
                            <hr />
                            <p>To apply submit cover letter and resume to <a href="mailto:jobs@masline.com">jobs@masline.com</a>.</p>
                            <p>
                                <br />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%--Popup for category listing--%>
    <div id="divBackground" style="position: fixed; z-index: 998; height: 100%; width: 100%; top: 0; left: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; display: none">
    </div>
    <div id="dialog-form" class="dvdialog-form" style="background-color: #CFD4D0; height: 800px; z-index: 999; position: absolute; color: #000000; filter: alpha(opacity=60); opacity: 1.0; -moz-opacity: 1.0; left: 35%; top: 12%; display: none;">
        <div style="background-color: #CFD4D0; padding-bottom: 15px; padding-left: 15px; padding-right: 15px; padding-top: 15px; width: 100%; height: 90%; overflow: auto; overflow-x: hidden;">
            <div>
                <div style="height: 30px; text-align: center;">CATALOG</div>
                <div style="float: right; margin-top: -25px;">
                    <%-- <asp:ImageButton ID="lbtnRemove" runat="server" OnClientClick="HidePopUp();" ImageUrl="~/images/delete_icon.png" />--%>
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

