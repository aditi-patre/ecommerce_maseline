<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="Services.aspx.cs" Inherits="Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function LoadService(ServiceType) {
            document.getElementById('<%=lblServiceHeader.ClientID %>').innerHTML = ServiceType.toUpperCase();

            //Hide all divs initially
            document.getElementById('<%=dvServices.ClientID %>').style.display = "none";
            document.getElementById('<%=dvLogistics.ClientID %>').style.display = "none";
            document.getElementById('<%=dvLeadForming.ClientID %>').style.display = "none";
            document.getElementById('<%=dvCustomAssembly.ClientID %>').style.display = "none";
            document.getElementById('<%=dvKitting.ClientID %>').style.display = "none";
            document.getElementById('<%=dvPackaging.ClientID %>').style.display = "none";
            document.getElementById('<%=dvLabeling.ClientID %>').style.display = "none";
            document.getElementById('<%=dvFinalInspection.ClientID %>').style.display = "none";

            if (ServiceType.toLowerCase() == "logistics")
                document.getElementById('<%=dvLogistics.ClientID %>').style.display = "block";
            else if (ServiceType.toLowerCase() == "lead forming")
                document.getElementById('<%=dvLeadForming.ClientID %>').style.display = "block";
            else if (ServiceType.toLowerCase() == "custom assembly")
                document.getElementById('<%=dvCustomAssembly.ClientID %>').style.display = "block";
            else if (ServiceType.toLowerCase() == "kitting")
                document.getElementById('<%=dvKitting.ClientID %>').style.display = "block";
            else if (ServiceType.toLowerCase() == "packaging")
                document.getElementById('<%=dvPackaging.ClientID %>').style.display = "block";
            else if (ServiceType.toLowerCase() == "labeling")
                document.getElementById('<%=dvLabeling.ClientID %>').style.display = "block";
            else if (ServiceType.toLowerCase() == "final inspection")
                document.getElementById('<%=dvFinalInspection.ClientID %>').style.display = "block";
}
window.onload = function () {
    if (window.location.href.toLowerCase().indexOf("?p") == -1) {
        document.getElementById('<%=lblServiceHeader.ClientID %>').innerHTML = "SERVICES";
        document.getElementById('<%=dvServices.ClientID %>').style.display = "block";
        document.getElementById('<%=dvLogistics.ClientID %>').style.display = "none";
        document.getElementById('<%=dvLeadForming.ClientID %>').style.display = "none";
        document.getElementById('<%=dvCustomAssembly.ClientID %>').style.display = "none";
        document.getElementById('<%=dvKitting.ClientID %>').style.display = "none";
        document.getElementById('<%=dvPackaging.ClientID %>').style.display = "none";
        document.getElementById('<%=dvLabeling.ClientID %>').style.display = "none";
        document.getElementById('<%=dvFinalInspection.ClientID %>').style.display = "none";
    }
}
    </script>
    <section>
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                    <div class="left-sidebar" style="background-color: #CFD4D0">
                        <h2>SERVICES</h2>
                        <div>
                            <div style="height: 250px;" id="_divCatalog" runat="server">
                                <div class="panel-group category-products" id="accordian">

                                    <%--<div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordian" href="#AC"><span class="badge pull-right"><i class="fa fa-plus"></i></span></a><a href="ProductListing.aspx?Cat=1"><span>Audible Components</span></a>                            </h4>
                                        </div>
                                        <div id="AC" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <ul>
                                                    <li><a href="ProductListing.aspx?Cat=1&amp;SubCat=1"><span>Buzzer</span></a></li>
                                                    <li><a href="ProductListing.aspx?Cat=1&amp;SubCat=2"><span>Microphone</span></a></li>
                                                    <li><a href="ProductListing.aspx?Cat=1&amp;SubCat=3"><span>Speaker</span></a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('LOGISTICS')"><span>Logistics</span></a></h4>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('LEAD FORMING')"><span>Lead Forming</span></a></h4>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('CUSTOM ASSEMBLY')"><span>Custom Assembly</span></a></h4>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('KITTING')"><span>Kitting</span></a></h4>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('PACKAGING')"><span>Packaging</span></a></h4>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('LABELING')"><span>Labeling</span></a></h4>
                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title"><a href="#" onclick="LoadService('FINAL INSPECTION')"><span>Final Inspection</span></a></h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9" style="padding-left: 2%; padding-bottom: 2%;">
                    <div class="col-sm-9">
                        <h2>
                            <asp:Label ID="lblServiceHeader" runat="server"></asp:Label></h2>
                    </div>
                    <div class="col-md-15 pull-left" style="font-size: 17px !important;">
                        <br />
                        <div id="dvServices" runat="server">
                            <p>Masline Electronics offers a wide variety of value-added services to enhance your business. From logistics to lead forming, kitting, labeling, custom assemblies, and the "final step" in your production line, we can create a solution for you. See below for more detailed information on the various offerings.</p>
                            <img src="images/services/services.png" alt="Service Features" class="img-responsive" />
                        </div>
                        <div id="dvLogistics" runat="server">
                            <img src="images/services/logistics1.png" />
                            <br />
                            <br />
                            <p>
                                <b>Do you have vendors with high minimums, long lead times, and restrictive scheduling options?</b> Let Masline Electronics work with you in our comprehensive vendor reduction program.
                                <br />
                                <br />
                                We can reduce your carrying costs, cost of ownership, and improve your inventory process through just in time (JIT) delivery.
                            </p>
                            <br />
                            <br />
                            <img src="images/services/logistics2.png" />
                            <br />
                            <p>Masline Electronics' logistics services includes vendor consolidation for your single or few product vendors. We can get your parts to you fast; typically in one business day for most customers. Let us smooth out your supply line and put our 80 years of experience to the test! For more information, <a href="ContactUs.aspx">contact us.</a></p>
                        </div>
                        <div id="dvLeadForming" runat="server">
                            <p>
                                <b>General Information</b>
                                <br />
                                <br />
                                Masline's through-hole component lead form design and tooling center will work for you.<br />
                                <br />
                                We utilize industry-leading CAD software and state-of-the-art lead forming equipment to <b>produce forms that meet your exact design specifications.</b><br />
                                <br />
                                We have the manufacturing experience to assist with board layout solutions, and have the capabilities to customize leads for short and long run production.<br />
                                <br />
                                See below for specifications on the different lead form dimensions Masline Electronics can fulfill, and <a href="ContactUs.aspx">contact us</a> for more information.<br />
                                <br />

                            </p>
                        </div>
                        <div id="dvCustomAssembly" runat="server">
                            <p>
                                Masline Electronics can create custom products to fit your needs. We can offer products for immediate use in your manufacturing process. Masline can do <b>crimp & pokes, terminal block assembly, lead forming, terminations on switches and transformers</b> for use in your product. We can also add custom marking according to your specifications. Let us use our industry expertise to find the assembly you need.
                                <br />
                                <br />
                                We also have the capabilities to work with you on custom battery pack solutions. Masline can offer you a full line of battery accessories to support your battery requirements.
                                <br />
                                <br />
                                Our custom assembly process is complimentary to our Lead Forming service.
                                <br />
                                <br />
                                For more information, <a href="ContactUs.aspx">contact us</a>
                            </p>
                            <img src="images/services/switch2.jpg" />
                            <img src="images/services/assembly.png" />
                        </div>
                        <div id="dvKitting" runat="server">
                            <p>
                                We can create field service assembly kits for your business needs. By packing related products together in our warehouse, we can simplify your in-house production and save you valuable time. Below are examples of kits we commonly fill for our customers.
                            </p>
                            <ul class="ServicesUnorderedList">
                                <li><b>Assembly Kit</b>: Contains all components for your production line. There's no limit to the types of components we can include in the kit, from transformers and diodes to associated hardware.</li>
                                <li><b>Assembly Designs</b>: Including all associated mounting hardware for simple kits, such as the mounting assembly kit to the right.</li>
                                <li><b>Proprietary</b>: We can include customer provided parts, if your installers need it. Let us simplify your process by handling kit assembly before the parts get to your warehouse.</li>
                            </ul>
                            <p>
                                <a href="ContactUs.aspx">Contact us</a> for more detailed information on how our services can enhance your product offerings.
                                <img src="images/services/kitting.jpg" />
                                <img src="images/services/assembly.jpg" />
                            </p>
                        </div>
                        <div id="dvPackaging" runat="server">
                            <p>
                                Masline Electronics can accommodate your specific packaging requirements, including:
                            </p>
                            <ul class="ServicesUnorderedList">
                                <li>Waterproof packaging or bags</li>
                                <li>Double-walled packaging, or other reinforcements for sensitive components</li>
                                <li>Exclude environmentally harmful materials, to keep your line green. </li>
                                <li>Or any other specialty packaging your business requires.</li>
                            </ul>
                            <p>
                                For more information, <a href="ContactUs.aspx">contact us</a>.
                            </p>
                            <img src="images/services/box.jpg" />
                        </div>
                        <div id="dvLabeling" runat="server">
                            <p>We can fulfill compliant labeling and barcoding to serve your business's needs. Do you use JIT, Kanban, Lean, Pull/Push, VMI, Consignment, Shared MRP, or other processes? We can design a label that matches your in-house requirements whether it requires a single barcode or complex labeling for internal tracking.</p>
                            <p>Masline Electronics can offer next day fulfillment on standard sizes. Custom sizes or images also available. For more information, <a href="ContactUs.aspx">contact us</a>.</p>
                            <img src="images/services/mock_label.png" />
                        </div>
                        <div id="dvFinalInspection" runat="server">
                            <p>We can perform statistical sampling on dimensional &amp; tolerance criteria, sorting to your exact specifications, or verify components to almost any electrical or mechanical parameter.</p>
                            <p><a href="ContactUs.aspx">Contact us</a> for more detailed information on how our services can enhance your business.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

