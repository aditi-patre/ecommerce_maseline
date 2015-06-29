<%@ Page Title="" Language="C#" MasterPageFile="~/ParentMaster.master" AutoEventWireup="true" CodeFile="ProductListing.aspx.cs" Inherits="ProductListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js" type="text/javascript"></script>
<%--    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>--%>
    <script>var $j = jQuery.noConflict(true);</script>
    <style class="firebugResetStyles" type="text/css" charset="utf-8">
        /* See license.txt for terms of usage */
        /** reset styling **/
        .firebugResetStyles {
            z-index: 2147483646 !important;
            top: 0 !important;
            left: 0 !important;
            display: block !important;
            border: 0 none !important;
            margin: 0 !important;
            padding: 0 !important;
            outline: 0 !important;
            min-width: 0 !important;
            max-width: none !important;
            min-height: 0 !important;
            max-height: none !important;
            position: fixed !important;
            transform: rotate(0deg) !important;
            transform-origin: 50% 50% !important;
            border-radius: 0 !important;
            box-shadow: none !important;
            background: transparent none !important;
            pointer-events: none !important;
            white-space: normal !important;
        }

        style.firebugResetStyles {
            display: none !important;
        }

        .firebugBlockBackgroundColor {
            background-color: transparent !important;
        }

        .firebugResetStyles:before, .firebugResetStyles:after {
            content: "" !important;
        }
        /**actual styling to be modified by firebug theme**/
        .firebugCanvas {
            display: none !important;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        .firebugLayoutBox {
            width: auto !important;
            position: static !important;
        }

        .firebugLayoutBoxOffset {
            opacity: 0.8 !important;
            position: fixed !important;
        }

        .firebugLayoutLine {
            opacity: 0.4 !important;
            background-color: #000000 !important;
        }

        .firebugLayoutLineLeft, .firebugLayoutLineRight {
            width: 1px !important;
            height: 100% !important;
        }

        .firebugLayoutLineTop, .firebugLayoutLineBottom {
            width: 100% !important;
            height: 1px !important;
        }

        .firebugLayoutLineTop {
            margin-top: -1px !important;
            border-top: 1px solid #999999 !important;
        }

        .firebugLayoutLineRight {
            border-right: 1px solid #999999 !important;
        }

        .firebugLayoutLineBottom {
            border-bottom: 1px solid #999999 !important;
        }

        .firebugLayoutLineLeft {
            margin-left: -1px !important;
            border-left: 1px solid #999999 !important;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        .firebugLayoutBoxParent {
            border-top: 0 none !important;
            border-right: 1px dashed #E00 !important;
            border-bottom: 1px dashed #E00 !important;
            border-left: 0 none !important;
            position: fixed !important;
            width: auto !important;
        }

        .firebugRuler {
            position: absolute !important;
        }

        .firebugRulerH {
            top: -15px !important;
            left: 0 !important;
            width: 100% !important;
            height: 14px !important;
            background: url("data:image/png,%89PNG%0D%0A%1A%0A%00%00%00%0DIHDR%00%00%13%88%00%00%00%0E%08%02%00%00%00L%25a%0A%00%00%00%04gAMA%00%00%D6%D8%D4OX2%00%00%00%19tEXtSoftware%00Adobe%20ImageReadyq%C9e%3C%00%00%04%F8IDATx%DA%EC%DD%D1n%E2%3A%00E%D1%80%F8%FF%EF%E2%AF2%95%D0D4%0E%C1%14%B0%8Fa-%E9%3E%CC%9C%87n%B9%81%A6W0%1C%A6i%9A%E7y%0As8%1CT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AATE9%FE%FCw%3E%9F%AF%2B%2F%BA%97%FDT%1D~K(%5C%9D%D5%EA%1B%5C%86%B5%A9%BDU%B5y%80%ED%AB*%03%FAV9%AB%E1%CEj%E7%82%EF%FB%18%BC%AEJ8%AB%FA'%D2%BEU9%D7U%ECc0%E1%A2r%5DynwVi%CFW%7F%BB%17%7Dy%EACU%CD%0E%F0%FA%3BX%FEbV%FEM%9B%2B%AD%BE%AA%E5%95v%AB%AA%E3E5%DCu%15rV9%07%B5%7F%B5w%FCm%BA%BE%AA%FBY%3D%14%F0%EE%C7%60%0EU%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5JU%88%D3%F5%1F%AE%DF%3B%1B%F2%3E%DAUCNa%F92%D02%AC%7Dm%F9%3A%D4%F2%8B6%AE*%BF%5C%C2Ym~9g5%D0Y%95%17%7C%C8c%B0%7C%18%26%9CU%CD%13i%F7%AA%90%B3Z%7D%95%B4%C7%60%E6E%B5%BC%05%B4%FBY%95U%9E%DB%FD%1C%FC%E0%9F%83%7F%BE%17%7DkjMU%E3%03%AC%7CWj%DF%83%9An%BCG%AE%F1%95%96yQ%0Dq%5Dy%00%3Et%B5'%FC6%5DS%95pV%95%01%81%FF'%07%00%00%00%00%00%00%00%00%00%F8x%C7%F0%BE%9COp%5D%C9%7C%AD%E7%E6%EBV%FB%1E%E0(%07%E5%AC%C6%3A%ABi%9C%8F%C6%0E9%AB%C0'%D2%8E%9F%F99%D0E%B5%99%14%F5%0D%CD%7F%24%C6%DEH%B8%E9rV%DFs%DB%D0%F7%00k%FE%1D%84%84%83J%B8%E3%BA%FB%EF%20%84%1C%D7%AD%B0%8E%D7U%C8Y%05%1E%D4t%EF%AD%95Q%BF8w%BF%E9%0A%BF%EB%03%00%00%00%00%00%00%00%00%00%B8vJ%8E%BB%F5%B1u%8Cx%80%E1o%5E%CA9%AB%CB%CB%8E%03%DF%1D%B7T%25%9C%D5(%EFJM8%AB%CC'%D2%B2*%A4s%E7c6%FB%3E%FA%A2%1E%80~%0E%3E%DA%10x%5D%95Uig%15u%15%ED%7C%14%B6%87%A1%3B%FCo8%A8%D8o%D3%ADO%01%EDx%83%1A~%1B%9FpP%A3%DC%C6'%9C%95gK%00%00%00%00%00%00%00%00%00%20%D9%C9%11%D0%C0%40%AF%3F%EE%EE%92%94%D6%16X%B5%BCMH%15%2F%BF%D4%A7%C87%F1%8E%F2%81%AE%AAvzr%DA2%ABV%17%7C%E63%83%E7I%DC%C6%0Bs%1B%EF6%1E%00%00%00%00%00%00%00%00%00%80cr%9CW%FF%7F%C6%01%0E%F1%CE%A5%84%B3%CA%BC%E0%CB%AA%84%CE%F9%BF)%EC%13%08WU%AE%AB%B1%AE%2BO%EC%8E%CBYe%FE%8CN%ABr%5Dy%60~%CFA%0D%F4%AE%D4%BE%C75%CA%EDVB%EA(%B7%F1%09g%E5%D9%12%00%00%00%00%00%00%00%00%00H%F6%EB%13S%E7y%5E%5E%FB%98%F0%22%D1%B2'%A7%F0%92%B1%BC%24z3%AC%7Dm%60%D5%92%B4%7CEUO%5E%F0%AA*%3BU%B9%AE%3E%A0j%94%07%A0%C7%A0%AB%FD%B5%3F%A0%F7%03T%3Dy%D7%F7%D6%D4%C0%AAU%D2%E6%DFt%3F%A8%CC%AA%F2%86%B9%D7%F5%1F%18%E6%01%F8%CC%D5%9E%F0%F3z%88%AA%90%EF%20%00%00%00%00%00%00%00%00%00%C0%A6%D3%EA%CFi%AFb%2C%7BB%0A%2B%C3%1A%D7%06V%D5%07%A8r%5D%3D%D9%A6%CAu%F5%25%CF%A2%99%97zNX%60%95%AB%5DUZ%D5%FBR%03%AB%1C%D4k%9F%3F%BB%5C%FF%81a%AE%AB'%7F%F3%EA%FE%F3z%94%AA%D8%DF%5B%01%00%00%00%00%00%00%00%00%00%8E%FB%F3%F2%B1%1B%8DWU%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*UiU%C7%BBe%E7%F3%B9%CB%AAJ%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5J%95*U%AAT%A9R%A5*%AAj%FD%C6%D4%5Eo%90%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5%86%AF%1B%9F%98%DA%EBm%BBV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%ADV%AB%D5j%B5Z%AD%D6%E4%F58%01%00%00%00%00%00%00%00%00%00%00%00%00%00%40%85%7F%02%0C%008%C2%D0H%16j%8FX%00%00%00%00IEND%AEB%60%82") repeat-x !important;
            border-top: 1px solid #BBBBBB !important;
            border-right: 1px dashed #BBBBBB !important;
            border-bottom: 1px solid #000000 !important;
        }

        .firebugRulerV {
            top: 0 !important;
            left: -15px !important;
            width: 14px !important;
            height: 100% !important;
            background: url("data:image/png,%89PNG%0D%0A%1A%0A%00%00%00%0DIHDR%00%00%00%0E%00%00%13%88%08%02%00%00%00%0E%F5%CB%10%00%00%00%04gAMA%00%00%D6%D8%D4OX2%00%00%00%19tEXtSoftware%00Adobe%20ImageReadyq%C9e%3C%00%00%06~IDATx%DA%EC%DD%D1v%A20%14%40Qt%F1%FF%FF%E4%97%D9%07%3BT%19%92%DC%40(%90%EEy%9A5%CB%B6%E8%F6%9Ac%A4%CC0%84%FF%DC%9E%CF%E7%E3%F1%88%DE4%F8%5D%C7%9F%2F%BA%DD%5E%7FI%7D%F18%DDn%BA%C5%FB%DF%97%BFk%F2%10%FF%FD%B4%F2M%A7%FB%FD%FD%B3%22%07p%8F%3F%AE%E3%F4S%8A%8F%40%EEq%9D%BE8D%F0%0EY%A1Uq%B7%EA%1F%81%88V%E8X%3F%B4%CEy%B7h%D1%A2E%EBohU%FC%D9%AF2fO%8BBeD%BE%F7X%0C%97%A4%D6b7%2Ck%A5%12%E3%9B%60v%B7r%C7%1AI%8C%BD%2B%23r%00c0%B2v%9B%AD%CA%26%0C%1Ek%05A%FD%93%D0%2B%A1u%8B%16-%95q%5Ce%DCSO%8E%E4M%23%8B%F7%C2%FE%40%BB%BD%8C%FC%8A%B5V%EBu%40%F9%3B%A72%FA%AE%8C%D4%01%CC%B5%DA%13%9CB%AB%E2I%18%24%B0n%A9%0CZ*Ce%9C%A22%8E%D8NJ%1E%EB%FF%8F%AE%CAP%19*%C3%BAEKe%AC%D1%AAX%8C*%DEH%8F%C5W%A1e%AD%D4%B7%5C%5B%19%C5%DB%0D%EF%9F%19%1D%7B%5E%86%BD%0C%95%A12%AC%5B*%83%96%CAP%19%F62T%86%CAP%19*%83%96%CA%B8Xe%BC%FE)T%19%A1%17xg%7F%DA%CBP%19*%C3%BA%A52T%86%CAP%19%F62T%86%CA%B0n%A9%0CZ%1DV%C6%3D%F3%FCH%DE%B4%B8~%7F%5CZc%F1%D6%1F%AF%84%F9%0F6%E6%EBVt9%0E~%BEr%AF%23%B0%97%A12T%86%CAP%19%B4T%86%CA%B8Re%D8%CBP%19*%C3%BA%A52huX%19%AE%CA%E5%BC%0C%7B%19*CeX%B7h%A9%0C%95%E1%BC%0C%7B%19*CeX%B7T%06%AD%CB%5E%95%2B%BF.%8F%C5%97%D5%E4%7B%EE%82%D6%FB%CF-%9C%FD%B9%CF%3By%7B%19%F62T%86%CA%B0n%D1R%19*%A3%D3%CA%B0%97%A12T%86uKe%D0%EA%B02*%3F1%99%5DB%2B%A4%B5%F8%3A%7C%BA%2B%8Co%7D%5C%EDe%A8%0C%95a%DDR%19%B4T%C66%82fA%B2%ED%DA%9FC%FC%17GZ%06%C9%E1%B3%E5%2C%1A%9FoiB%EB%96%CA%A0%D5qe4%7B%7D%FD%85%F7%5B%ED_%E0s%07%F0k%951%ECr%0D%B5C%D7-g%D1%A8%0C%EB%96%CA%A0%A52T%C6)*%C3%5E%86%CAP%19%D6-%95A%EB*%95q%F8%BB%E3%F9%AB%F6%E21%ACZ%B7%22%B7%9B%3F%02%85%CB%A2%5B%B7%BA%5E%B7%9C%97%E1%BC%0C%EB%16-%95%A12z%AC%0C%BFc%A22T%86uKe%D0%EA%B02V%DD%AD%8A%2B%8CWhe%5E%AF%CF%F5%3B%26%CE%CBh%5C%19%CE%CB%B0%F3%A4%095%A1%CAP%19*Ce%A8%0C%3BO*Ce%A8%0C%95%A12%3A%AD%8C%0A%82%7B%F0v%1F%2FD%A9%5B%9F%EE%EA%26%AF%03%CA%DF9%7B%19*Ce%A8%0C%95%A12T%86%CA%B8Ze%D8%CBP%19*Ce%A8%0C%95%D1ae%EC%F7%89I%E1%B4%D7M%D7P%8BjU%5C%BB%3E%F2%20%D8%CBP%19*Ce%A8%0C%95%A12T%C6%D5*%C3%5E%86%CAP%19*Ce%B4O%07%7B%F0W%7Bw%1C%7C%1A%8C%B3%3B%D1%EE%AA%5C%D6-%EBV%83%80%5E%D0%CA%10%5CU%2BD%E07YU%86%CAP%19*%E3%9A%95%91%D9%A0%C8%AD%5B%EDv%9E%82%FFKOee%E4%8FUe%A8%0C%95%A12T%C6%1F%A9%8C%C8%3D%5B%A5%15%FD%14%22r%E7B%9F%17l%F8%BF%ED%EAf%2B%7F%CF%ECe%D8%CBP%19*Ce%A8%0C%95%E1%93~%7B%19%F62T%86%CAP%19*Ce%A8%0C%E7%13%DA%CBP%19*Ce%A8%0CZf%8B%16-Z%B4h%D1R%19f%8B%16-Z%B4h%D1R%19%B4%CC%16-Z%B4h%D1R%19%B4%CC%16-Z%B4h%D1%A2%A52%CC%16-Z%B4h%D1%A2%A52h%99-Z%B4h%D1%A2%A52h%99-Z%B4h%D1%A2EKe%98-Z%B4h%D1%A2EKe%D02%5B%B4h%D1%A2EKe%D02%5B%B4h%D1%A2E%8B%96%CA0%5B%B4h%D1%A2E%8B%96%CA%A0e%B6h%D1%A2E%8B%96%CA%A0e%B6h%D1%A2E%8B%16-%95a%B6h%D1%A2E%8B%16-%95A%CBl%D1%A2E%8B%16-%95A%CBl%D1%A2E%8B%16-Z*%C3l%D1%A2E%8B%16-Z*%83%96%D9%A2E%8B%16-Z*%83%96%D9%A2E%8B%16-Z%B4T%86%D9%A2E%8B%16-Z%B4T%06-%B3E%8B%16-Z%B4T%06-%B3E%8B%16-Z%B4h%A9%0C%B3E%8B%16-Z%B4h%A9%0CZf%8B%16-Z%B4h%A9%0CZf%8B%16-Z%B4h%D1R%19f%8B%16-Z%B4h%D1R%19%B4%CC%16-Z%B4h%D1R%19%B4%CC%16-Z%B4h%D1%A2%A52%CC%16-Z%B4h%D1%A2%A52h%99-Z%B4h%D1%A2%A52h%99-Z%B4h%D1%A2EKe%98-Z%B4h%D1%A2EKe%D02%5B%B4h%D1%A2EKe%D02%5B%B4h%D1%A2E%8B%96%CA0%5B%B4h%D1%A2E%8B%96%CA%A0e%B6h%D1%A2E%8B%96%CA%A0e%B6h%D1%A2E%8B%16-%95a%B6h%D1%A2E%8B%16-%95A%CBl%D1%A2E%8B%16-%95A%CBl%D1%A2E%8B%16-Z*%C3l%D1%A2E%8B%16-Z*%83%96%D9%A2E%8B%16-Z*%83%96%D9%A2E%8B%16-Z%B4T%86%D9%A2E%8B%16-Z%B4T%06-%B3E%8B%16-Z%B4T%06-%B3E%8B%16-Z%B4h%A9%0C%B3E%8B%16-Z%B4h%A9%0CZf%8B%16-Z%B4h%A9%0CZf%8B%16-Z%B4h%D1R%19f%8B%16-Z%B4h%D1R%19%B4%CC%16-Z%B4h%D1R%19%B4%CC%16-Z%B4h%D1%A2%A52%CC%16-Z%B4h%D1%A2%A52h%99-Z%B4h%D1%A2%A52h%99-Z%B4h%D1%A2EKe%98-Z%B4h%D1%A2EKe%D02%5B%B4h%D1%A2EKe%D02%5B%B4h%D1%A2E%8B%96%CA0%5B%B4h%D1%A2E%8B%96%CA%A0e%B6h%D1%A2E%8B%96%CA%A0e%B6h%D1%A2E%8B%16-%95a%B6h%D1%A2E%8B%16-%95A%CBl%D1%A2E%8B%16-%95A%CBl%D1%A2E%8B%16-Z*%C3l%D1%A2E%8B%16-Z*%83%96%D9%A2E%8B%16-Z*%83%96%D9%A2E%8B%16-Z%B4T%86%D9%A2E%8B%16-Z%B4T%06-%B3E%8B%16-Z%B4%AE%A4%F5%25%C0%00%DE%BF%5C'%0F%DA%B8q%00%00%00%00IEND%AEB%60%82") repeat-y !important;
            border-left: 1px solid #BBBBBB !important;
            border-right: 1px solid #000000 !important;
            border-bottom: 1px dashed #BBBBBB !important;
        }

        .overflowRulerX > .firebugRulerV {
            left: 0 !important;
        }

        .overflowRulerY > .firebugRulerH {
            top: 0 !important;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        .fbProxyElement {
            position: fixed !important;
            pointer-events: auto !important;
        }
    </style>
    <script type="text/javascript">
        var dialog;

        function GetProducts(Category, SubCategory) {
            window.location("Catalogue.aspx?a=x");
        }

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
                document.getElementById('<%=hdnManufacturer.ClientID%>').value = "";
                $j("[id*=dvCheckBoxListControl] input:checked").each(function (index, item)
                {
                    if (document.getElementById('<%=hdnManufacturer.ClientID%>').value == "")
                        document.getElementById('<%=hdnManufacturer.ClientID%>').value += $(item).val();
                    else
                    {
                         document.getElementById('<%=hdnManufacturer.ClientID%>').value += ", " + $(item).val();
                    }
                });
                
                //Checking the outer checkbox (within page) if checkbox within popup are checked.
                var hdnM =  document.getElementById('<%=hdnManufacturer.ClientID%>').value.split(", ");
                for(var j=0; j<document.getElementById('MainContentPlaceHolder_chkManufacturer').getElementsByTagName("input").length; j++)
                {
                    if (hdnM.indexOf(document.getElementById('MainContentPlaceHolder_chkManufacturer').getElementsByTagName("input")[j].value) != -1)
                    {
                        document.getElementById('MainContentPlaceHolder_chkManufacturer').getElementsByTagName("input")[j].checked = true;                            
                    }
                    else
                        document.getElementById('MainContentPlaceHolder_chkManufacturer').getElementsByTagName("input")[j].checked = false;
                }
            }
            else if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value.indexOf("Category") != -1)
            {
                document.getElementById('<%=hdnCategory.ClientID%>').value = "";
                $j("[id*=dvCheckBoxListControl] input:checked").each(function (index, item) {
                    if (document.getElementById('<%=hdnCategory.ClientID%>').value == "")
                        document.getElementById('<%=hdnCategory.ClientID%>').value += $(item).val();
                    else {
                            document.getElementById('<%=hdnCategory.ClientID%>').value += ", " + $(item).val();
                    }
                });

                //Checking the outer checkbox (within page) if checkbox within popup are checked.
                var hdnM = document.getElementById('<%=hdnCategory.ClientID%>').value.split(", ");
                for (var j = 0; j < document.getElementById('MainContentPlaceHolder_chkCategory').getElementsByTagName("input").length; j++)
                {
                    if (hdnM.indexOf(document.getElementById('MainContentPlaceHolder_chkCategory').getElementsByTagName("input")[j].value) != -1)
                        document.getElementById('MainContentPlaceHolder_chkCategory').getElementsByTagName("input")[j].checked = true;
                    else
                        document.getElementById('MainContentPlaceHolder_chkCategory').getElementsByTagName("input")[j].checked = false;
                }
            }
            else if (document.getElementById('<%=hdnPopUpSearchCriteria.ClientID%>').value.indexOf("Sub-Category") != -1)
            {
                document.getElementById('<%=hdnSubCategory.ClientID%>').value = "";
                $j("[id*=dvCheckBoxListControl] input:checked").each(function (index, item)
                {
                    if (document.getElementById('<%=hdnSubCategory.ClientID%>').value == "")
                        document.getElementById('<%=hdnSubCategory.ClientID%>').value += $(item).val();
                    else
                         document.getElementById('<%=hdnSubCategory.ClientID%>').value += ", " + $(item).val();
                });

                //Checking the outer checkbox (within page) if checkbox within popup are checked.
                var hdnM = document.getElementById('<%=hdnSubCategory.ClientID%>').value.split(", ");
                for (var j = 0; j < document.getElementById('MainContentPlaceHolder_chkSubCategory').getElementsByTagName("input").length; j++)
                {
                    if (hdnM.indexOf(document.getElementById('MainContentPlaceHolder_chkSubCategory').getElementsByTagName("input")[j].value) != -1)
                        document.getElementById('MainContentPlaceHolder_chkSubCategory').getElementsByTagName("input")[j].checked = true;
                    else
                        document.getElementById('MainContentPlaceHolder_chkSubCategory').getElementsByTagName("input")[j].checked = false;
                }
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
                    dialog.dialog("close");
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
        function ShowMoreSearchOption(btn) {
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

        function ShowPopUp() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "block";
            document.getElementById("dialog-form").style.display = "block";
            $("#PopUpHeading")[0].innerHTML = SearchCriteria;
        }

        function HidePopUp() {
            var bcgDiv = document.getElementById("divBackground");
            bcgDiv.style.display = "none";
            document.getElementById("dialog-form").style.display = "none";
        }
        /**Javascript popup end**/

        var SearchObj = {};
        function FilterGrid(ddl) {
            var SearchCriteria = "";
            var _ddl = "";
            if (ddl.id.indexOf("ddlSortBy") != -1) {
                _ddl = document.getElementById('<%=ddlSortBy.ClientID %>');
                SearchObj.SearchCriteria = "SortBy";
                SearchObj.SearchValue = _ddl.options[_ddl.selectedIndex].value;
            }
            else if (ddl.id.indexOf("ddlPageSize") != -1) {
                _ddl = document.getElementById('<%=ddlPageSize.ClientID %>');
                    SearchObj.SearchCriteria = "PageSize";
                    SearchObj.SearchValue = _ddl.options[_ddl.selectedIndex].value;
                }

            $.ajax({
                type: "POST",
                url: "QueryPage.aspx/FilterGrid",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(SearchObj),
                dataType: "json",
                success: function (result) {
                    alert(JSON.parse(result.d).Message);
                },
                error: function () {
                    alert("Request could not be made");
                }
            });
        }

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

            if (obj.CriteriaToExpand.indexOf("Manufacturer") != -1 && document.getElementById('<%=hdnManufacturer.ClientID%>').value != "") {
                a = document.getElementById('<%=hdnManufacturer.ClientID%>').value.split(", ");
            }
            else if (obj.CriteriaToExpand.indexOf("Category") != -1 && document.getElementById('<%=hdnCategory.ClientID%>').value != "") {
                a = document.getElementById('<%=hdnCategory.ClientID%>').value.split(", ");
            }
            else if (obj.CriteriaToExpand.indexOf("Sub-Category") !=-1 && document.getElementById('<%=hdnSubCategory.ClientID%>').value != "") {
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


//**To expand/collapse columns
var tbl = null;
var UpperBound = 0;
var LowerBound = 1;
var CollapseImage = 'images/minus.png';
var ExpandImage = 'images/plus.png';
var n = 1;
var TimeSpan = 25;
var Rows = null;
var Cols = null;
var img = "";

window.onload = function () {
    tbl = document.getElementById('<%= this.gvProducts.ClientID %>');
    UpperBound = parseInt('<%= this.gvProducts.Rows.Count %>');
    Rows = tbl.getElementsByTagName('tr');
    Cols = tbl.getElementsByTagName('td');
}

function Toggle(Image, Index) {
    ToggleImage(Image, Index);
    ToggleColumns(Image, Index);
}

function ToggleImage(Image, Index) {
    if (Image.src.indexOf("plus") != -1) {
        Image.src = ExpandImage;
        Image.title = 'Expand';
        n = UpperBound;
        img = ExpandImage;
        Image.IsExpanded = false;
    }
    else {
        Image.src = CollapseImage;
        Image.title = 'Collapse';
        n = LowerBound;
        img = CollapseImage;
        Image.IsExpanded = true;
    }
}

function ToggleColumns(Image, Index) {
    document.getElementById('<%=hdnColNoImg.ClientID %>').value = Index + "|" + img;
    if (n < LowerBound || n > UpperBound) return;
    if (tbl.rows.length > 0) {
        var tbl_row = tbl.rows[parseInt(n)];
        var tbl_Cell = tbl_row.cells[Index];
        if (tbl_Cell.style.width >= "100px") {

            tbl_Cell.style.width = "5px";

        }
        else {
            tbl_Cell.style.width = "100px";

        }

        if (Image.IsExpanded) n++; else n--;
        setTimeout(function () { ToggleColumns(Image, Index); }, TimeSpan);
    }

    document.getElementById('<%=btn1.ClientID %>').click();

}
//**
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section>
        <div class="container">
            <%-- <div style="margin-top:-4%;">
                <h2>CATALOGUE</h2>
            </div>
            <div class="breadcrumbs">
                <ol class="breadcrumb">
                    <li><a href="#">Home</a></li>
                    <li class="active">Product Listing</li>
                </ol>
            </div>--%>
            <div class="row">
                <div class="col-sm-3">
                    <div class="left-sidebar" style="width: 85%; border: 1px solid lightgrey;">
                        <h2>MODIFY SEARCH</h2>
                        <div class="brands_products" style="padding: 3% 6% 4% 6% !important;">
                            <!--brands_products-->
                            <h4 class="ListingSrchHeading">Manufacturer</h4>
                            <div style="height: 160px; overflow-y: hidden;">
                                <div class="brands-name">
                                    <asp:CheckBoxList ID="chkManufacturer" runat="server" Height="80px" CssClass="SearchCheckbox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <input type="button" id="btnMoreManufacturer" value="Show More >>" class="btn btn-primary" onclick="ShowMoreSearchOption(this)" />
                        </div>
                        <!--/brands_products-->
                        <div class="brands_products" style="padding: 3% 6% 4% 6% !important;">
                            <!--brands_products-->
                            <h4 class="ListingSrchHeading">Category</h4>
                            <div style="height: 160px; overflow-y: hidden;">

                                <div class="brands-name">
                                    <asp:CheckBoxList ID="chkCategory" runat="server" Height="80px" CssClass="SearchCheckbox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <input type="button" id="btnMoreCategory" value="Show More >>" class="btn btn-primary" onclick="ShowMoreSearchOption(this)" />
                        </div>
                        <!--/brands_products-->

                        <!--SubCategory-->
                        <div class="brands_products" style="padding: 3% 6% 4% 6% !important;">
                            <!--brands_products-->
                            <h4 class="ListingSrchHeading">SubCategory</h4>
                            <div style="height: 160px; overflow-y: hidden;">
                                <div class="brands-name">
                                    <asp:CheckBoxList ID="chkSubCategory" runat="server" Height="80px" CssClass="SearchCheckbox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <input type="button" id="btnMoreSubCategory" value="Show More >>" class="btn btn-primary" onclick="ShowMoreSearchOption(this)" />
                        </div>
                        <!--/SubCategory-->
                        <br />
                        <br />
                        <div id="SearchAttributes" style="padding: 3% 6% 4% 6% !important;">
                            <asp:Literal ID="ltAttributes" runat="server"></asp:Literal>
                        </div>
                        <div class="price-range" style="padding: 3% 6% 4% 6% !important;">
                            <!--price-range-->
                            <h4 class="ListingSrchHeading">Price Range</h4>
                            <div class="well">
                                <input type="text" class="span2" value="" data-slider-min="0" data-slider-max="600" data-slider-step="5"
                                    data-slider-value="[0,450]" id="sl2" style="width: 98%;" /><br />
                                <b>$ 0</b> <b class="pull-right">$ 600</b>
                            </div>
                        </div>
                        <!--/price-range-->

                        <asp:CheckBox ID="chkInStock" runat="server" Text="In Stock" CssClass="SearchCheckbox" Style="padding: 3% 6% 4% 6% !important;" />
                        <br />
                        <asp:CheckBox ID="chkPricingAvail" runat="server" Text="Pricing Available" CssClass="SearchCheckbox" Style="padding: 3% 6% 4% 6% !important;" />
                        <input type="button" id="btnApplyFilter1" value="Apply Filter" style="visibility: hidden;" />
                        <asp:HiddenField ID="hdnCategory" runat="server" />
                        <asp:HiddenField ID="hdnSubCategory" runat="server" />
                        <asp:HiddenField ID="hdnManufacturer" runat="server" />
                        <asp:HiddenField ID="hdnAttributes" runat="server" />
                        <asp:HiddenField ID="hdnInStock" runat="server" />
                        <asp:HiddenField ID="hdnPricingAvailable" runat="server" />
                        <asp:HiddenField ID="hdnPriceRange" runat="server" />
                        <div style="padding: 3% 6% 4% 6% !important;">
                            <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-primary" OnClick="btnApplyFilter_Click" OnClientClick="return SetSearchFields(1);" />
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="up_gvProducts" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="col-sm-9">
                            <!--features_items-->
                            <h2>PRODUCT LISTINGS</h2>
                            <div>
                                <%--<ul class="pagination">
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <li class='<%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'>
                                            </asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>--%>
                                <div style="padding-top: 15px; padding-bottom: 15px;">
                                    Sort by
                                    <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="dropdownSearch" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Product Name: A to Z" Value="ProdA_Z"></asp:ListItem>
                                        <asp:ListItem Text="Product Name: Z to A" Value="ProdZ_A"></asp:ListItem>
                                        <asp:ListItem Text="Manufacturer: A to Z" Value="McftA_Z"></asp:ListItem>
                                        <asp:ListItem Text="Manufacturer: Z to A" Value="McftZ_A"></asp:ListItem>
                                        <asp:ListItem Text="Price: Low to High" Value="PriceL_H"></asp:ListItem>
                                        <asp:ListItem Text="Price: High to Low" Value="PriceH_L"></asp:ListItem>
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Show
                                    <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdownSearch" Style="width: 80px;" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-15 pull-left">
                            <%-- <div class="features_items">--%>
                            <div class="listingtbl">
                                <%--class="col-md-12" style=" width:98%; overflow-x:scroll;">--%>
                                <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductID"
                                    ShowFooter="false" CssClass="table table-condensed" Width="" Style="font-size: 15px"
                                    PagerStyle-CssClass="pgr" PagerSettings-Position="Bottom" BorderColor="LightGray" 
                                    RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true" EditRowStyle-Wrap="true" 
                                    FooterStyle-Wrap="true" GridLines="Both" ShowHeaderWhenEmpty="true" EnableCallBacks="False"
                                    ShowHeader="true" OnRowDataBound="gvProducts_DataBound" OnRowCreated="gvProducts_RowCreated" AllowPaging="true"
                                    OnRowCommand="gvProducts_RowCommand">
                                    <HeaderStyle CssClass="cart_menu"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <HeaderStyle CssClass="image" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgProduct" runat="server" ToolTip="Click to view details" Style="width: 100px; height: 75px; cursor:pointer;" />
                                                <asp:Label ID="lblImagePath" runat="server" Style="display: none;" Text='<%# Eval("ImageName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                            <HeaderStyle CssClass="description" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="center" VerticalAlign="Top" CssClass="cart_description" />
                                            <ControlStyle CssClass="cart_description" />
                                            <ItemTemplate>
                                                <h4>
                                                    <asp:LinkButton ID="lbtnProdName" ToolTip="Click to view details" runat="server" Text='<%# Eval("Name") %>' CommandName="ShowProductDetails" CommandArgument='<%# Eval("ProductID") %>' style="cursor:pointer;" title="Click to view details" ></asp:LinkButton>
                                                </h4>
                                                <%--  <p id="lblProductCode" runat="server"><%# Eval("ProductCode") %></p>--%>
                                                <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("ProductCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Item" DataField="ProductCode" ItemStyle-CssClass="cart_description"></asp:BoundField>--%>
                                        <asp:BoundField HeaderText="Manufacturer" DataField="Manufacturer" ItemStyle-CssClass="cart_description" ItemStyle-Width="15%"></asp:BoundField>
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
                                        <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="17%">
                                            <HeaderStyle CssClass="cart_total" />
                                            <ItemStyle CssClass="cart_total" />
                                            <ItemTemplate>
                                                <span>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                    <asp:Literal ID="ltPricing" runat="server"></asp:Literal>
                                                </span>
                                                <span style="float: right; padding-right: 30px;">
                                                    <asp:LinkButton ID="btnGetPrice" runat="server" Text="Request Quote"
                                                        OnClientClick='<%# string.Format("javascript:return ProductRequest(\"{0}\",\"{1}\")", Eval("ProductID"),"Price") %>'></asp:LinkButton>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inventory" ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle CssClass="cart_quantity" />
                                            <ItemTemplate>
                                                <span>
                                                    <div style="float: left;">
                                                        <asp:Label ID="lblInventory" runat="server" Text='<%# Eval("Inventory") %>'></asp:Label>
                                                    </div>
                                                    <div style="padding-left: 100px;">
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
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px">
                                            <HeaderTemplate>
                                                <asp:Image ID="ibtnExpand" onclick="javascript:Toggle(this,6);" runat="server" ImageUrl="~/images/plus.png" ToolTip="Expand" ImageAlign="AbsMiddle" />
                                                <br />
                                                <asp:Label ID="label1" runat="server" Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                                <%--<asp:ImageButton ID="ibtnExpand" runat="server" ImageUrl="~/images/plus.png" ToolTip="Expand" ImageAlign="AbsMiddle" CommandArgument='<%# Eval("6") %>' CommandName="CollapseExpand" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescrip" runat="server" Text='<%# Eval("Descrip") %>' Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="Shorter" />
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px">
                                            <HeaderTemplate>
                                                <asp:Image ID="ibtnExpand" onclick="javascript:Toggle(this,7);" runat="server" ImageUrl="~/images/plus.png" ToolTip="Expand" ImageAlign="AbsMiddle" />
                                                <br />
                                                <asp:Label ID="label1" runat="server" Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTechnology" runat="server" Text='<%# Eval("Technology") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px">
                                            <HeaderTemplate>
                                                <asp:Image ID="ibtnExpand" onclick="javascript:Toggle(this,8);" runat="server" ImageUrl="~/images/plus.png" ToolTip="Expand" ImageAlign="AbsMiddle" />
                                                <br />
                                                <asp:Label ID="label1" runat="server" Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblHarmonizedCode" runat="server" Text='<%# Eval("HarmonizedCode") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px">
                                            <HeaderTemplate>
                                                <asp:Image ID="ibtnExpand" onclick="javascript:Toggle(this,9);" runat="server" ImageUrl="~/images/plus.png" ToolTip="Expand" ImageAlign="AbsMiddle" />
                                                <br />
                                                <asp:Label ID="label1" runat="server" Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3px" ItemStyle-Width="3px" ControlStyle-Width="3px">
                                            <HeaderTemplate>
                                                <asp:Image ID="ibtnExpand" onclick="javascript:Toggle(this,10);" runat="server" ImageUrl="~/images/plus.png" ToolTip="Expand" ImageAlign="AbsMiddle" />
                                                <br />
                                                <asp:Label ID="label1" runat="server" Style="width: 0px; overflow-x: hidden; white-space: nowrap;"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("SubCategory") %>' Style="width: 0px; overflow: hidden; white-space: nowrap;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Shorter" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnColNoImg" runat="server" />
                                <asp:Button ID="btn1" OnClick="btnExpand_Click" runat="server" Style="display: none;" />

                            </div>
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
                            <asp:Label ID="lblGridInfo" runat="server"></asp:Label>
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
                            <%-- </div>--%><%-- feature listing--%>
                        </div>


                    </ContentTemplate>
                    <%--  <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSortBy"  />
                         <asp:AsyncPostBackTrigger ControlID="ddlPageSize" />
                    </Triggers>--%>
                </asp:UpdatePanel>
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
                    <asp:ImageButton ID="lbtnRemove" runat="server" OnClientClick="HidePopUp();" ImageUrl="~/images/delete_icon.png" />
                </div>
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

