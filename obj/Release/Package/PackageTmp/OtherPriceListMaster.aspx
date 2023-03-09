<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="OtherPriceListMaster.aspx.cs" Inherits="Production_Costing_Software.OtherPriceListMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <script type="text/javascript">

         var GridId = "<%=Grid_OtherCompanyPriceMaster.ClientID %>";
         var ScrollHeight = 600;
         window.onload = function () {
             var grid = document.getElementById(GridId);
             var gridWidth = grid.offsetWidth - 5;
             var gridHeight = grid.offsetHeight;
             var headerCellWidths = new Array();
             for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                 headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
             }
             grid.parentNode.appendChild(document.createElement("div"));
             var parentDiv = grid.parentNode;

             var table = document.createElement("table");
             for (i = 0; i < grid.attributes.length; i++) {
                 if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                     table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                 }
             }
             table.style.cssText = grid.style.cssText;
             table.style.width = gridWidth + "px";
             table.appendChild(document.createElement("tbody"));
             table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
             var cells = table.getElementsByTagName("TH");

             var gridRow = grid.getElementsByTagName("TR")[0];
             for (var i = 0; i < cells.length; i++) {
                 var width;
                 //if (headerCellWidths[i] >= gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                 width = headerCellWidths[i];
                 //}
                 //else {
                 //    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                 //    }
                 cells[i].style.width = parseInt(width) + "px";
                 gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
             }
             parentDiv.removeChild(grid);

             var dummyHeader = document.createElement("div");
             dummyHeader.appendChild(table);
             parentDiv.appendChild(dummyHeader);
             var scrollableDiv = document.createElement("div");
             if (parseInt(gridHeight) > ScrollHeight) {
                 gridWidth = parseInt(gridWidth);
             }
             scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
             scrollableDiv.appendChild(grid);
             parentDiv.appendChild(scrollableDiv);
         }

     </script>
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <%------------------------Label------------------------------%>
                <div>
                    <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblUserMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanEdit" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <%-----------------------%>
                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Price List    [<asp:Label ID="lblCompanyMasterList_Name" runat="server" class="font-monospace" Style="color: green"></asp:Label>]</h1>
                    <ol class="breadcrumb mb-4">
                    </ol>

                    <div class="card mb-4" id="CompanyVisible" runat="server" visible="false">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Bulk Product:
                           
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card-body">

                                    <div class="row mb-3">

                                        <div class="col-md-3">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="BulkProductNameDropDownList" OnSelectedIndexChanged="BulkProductNameDropDownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="BulkProduct">Bulk Product Name</label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="BulkPackSize" Enabled="false" OnSelectedIndexChanged="BulkPackSize_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="BulkProductPAckSize">PackSize</label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="BoxSizetxt" AutoPostBack="true" CssClass="form-control" type="text" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="BoxSize">Box Size</label>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <%--Bulk Cost--%>

                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Bulk Cost                           
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="BulkCostPerLtr" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="BulkCost">BulkCost/Ltr (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="BulkInterestPertxt" CssClass="form-control" type="text" Text="0.00" onchange="calculatePricelist(this.id);" runat="server"></asp:TextBox>
                                                    <label for="BulkInterestPer">Interest (%)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="BulkInterestAmttxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="BulkInterestAmt">Interest Amt (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="TotalBulkCostWithInteresttxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="TotalBulkCostWithInterest">Total Bulk Cost (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="BulkCostPerUnittxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="BulkCostPerUnit">Bulk Cost /Unit (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--PMCost--%>
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        PM
                           
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="PMtxt" CssClass="form-control" TextMode="Number" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="PM">PM (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="AddBuffPMtxt" CssClass="form-control" onchange="calculatePricelist(this.id);" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="AddBuffPM">Additional Buffer PM (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="PMTotaltxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="Total">Total (₹)</label>

                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <%--labour--%>
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Labour
                           
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="Labourtxt" CssClass="form-control" TextMode="Number" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="PM">Labour (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="AddBuffLabourtxt" CssClass="form-control" onchange="calculatePricelist(this.id);" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="AddBuffLabour">Additional Buffer Labour (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="TotalLabourtxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="Total">Total Labour (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="TotalCalculateAtxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="Total">Sub Total A (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Loss--%>
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Loss                           
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="LossPertxt" CssClass="form-control" onchange="calculatePricelist(this.id);" type="text" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="Loss">Loss</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="LossAmttxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <label for="LossAmt">Loss Amount (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="TotalCalculateBtxt" CssClass="form-control" Text="0.00" Enabled="false" runat="server"></asp:TextBox>
                                                    <label for="Total">Sub Total B (₹)</label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--OtherCompanyExpence--%>
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Company Expence                           
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="MarketedByChargesPertxt" CssClass="form-control" onchange="calculatePricelist(this.id);" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="Marketed">Marketed By Charges (%)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="MarketedByChargesAmttxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <label for="MarketedByChargesAmt">Marketed By Charges Amount (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="FactoryExpencePertxt" CssClass="form-control" onchange="calculatePricelist(this.id);" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="FactoryExpence">Factory Expence (%)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="FactoryExpenceAmttxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="FactoryExpenceAmt">FactoryExpence Amount (₹)</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="OtherPertxt" CssClass="form-control" onchange="calculatePricelist(this.id);" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="OtherPertxt">Other (%)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="OtherAmttxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="OtherAmttxt">Other Amount (₹)</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="ProfitPertxt" CssClass="form-control" onchange="calculatePricelist(this.id);" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="ProfitPertxt">Profit (%)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="ProfitAmttxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="ProfitAmttxt">Profit Amount (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="TotalCalculateCtxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="TotalCalculateCtxt">Total (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="FinalSuggestedPricetxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="FinalSuggestedPricetxt">Final Suggested Price (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--Final Price--%>
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Final Price                         
                                    </div>
                                    <div class="row mb-3">
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="FinalPricePerUnittxt" CssClass="form-control" onchange="calculatePricelist(this.id);" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="FinalPricePerUnittxt">Final Price /Unit (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="ProfitFinalPricetxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="PrfitFinalPricetxt">Profit Final Price (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="PerCent_ProfitFinalPricetxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="PrfitFinalPricetxt">(%) of Profit Final Price</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="FinalPriceLtrKgtxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="FinalPriceLtrKgtxt">Final Price Ltr or Kg (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="ProfitOnFinalPricetxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="ProfitOnFinalPricetxt">Profit On Final Price (₹)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-md-3 mt-3">
                                            <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="PercentOfProfitOnFinalPricetxt" CssClass="form-control" Enabled="false" TextMode="Number" Text="0.00" runat="server"></asp:TextBox>
                                                    <label for="PercentOfProfitOnFinalPricetxt">(%) Of Profit On Final Price</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblTradeName_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblProductCategory_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPMRM_Category_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPackSize" Style="display: none" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPackMeasurement" Style="display: none" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblCompany_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblBPM_Id" Visible="false" runat="server" Text="Label"></asp:Label>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-10 col-lg-2 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="AddOtherCompanyPrice" OnClick="AddOtherCompanyPrice_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                                <asp:Button ID="UpdateOtherCompanyPrice" OnClick="UpdateOtherCompanyPrice_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-info btn-block" Visible="false" runat="server" Text="Update" />
                                            </div>
                                        </div>
                                        <div class=" col-10 col-lg-2 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelOtherCompanyPrice" OnClick="CancelOtherCompanyPrice_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <asp:Label ID="lblOtherCompanyPrice_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <div class="card-body  mb-4 overflow-auto">

                                    <asp:GridView ID="Grid_OtherCompanyPriceMaster" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="OtherComapnyPriceList_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                        
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product Name" SortExpression="MainCategory_Name"  HeaderStyle-Width="130px"/>
                                            <asp:BoundField DataField="PackingSize_BoxSize" HeaderText="PackingSize & BoxSize" SortExpression="TradeName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="BulkCost" HeaderText="BulkCost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="IntrestPercent_Amount" HeaderText="IntrestAmount On Bulk" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="TotalBulkCost" HeaderText="Total Bulk Cost" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="BulkCostPerUnit" HeaderText="Bulk Cost Unit" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="PM_And_Buffer" HeaderText="FinalPMCost (PM+BufferPM)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="Labour_Addiition" HeaderText="Final Labour Cos (Labour+BufferLabour)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="SubTotalA" HeaderText="SubTotal A" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="Loss_Percent_LossAmt" HeaderText="Loss(%) & Amount" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="TotalB" HeaderText="Total B" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="MarketedByCharge_Per" HeaderText="Marketed By Charge(%)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="FactoryExpence_Percent" HeaderText="Factory Expence(%)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="Other_Percent" HeaderText="Other Amount (%)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="Profit_Percent" HeaderText="Profit Amount (%)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="SuggestedFinalTotalCost" HeaderText="Suggested Final Total Cost" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="FinalPricePerUnit" HeaderText="FinalPrice/Unit" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="FinalProfitAmount_Percent" HeaderText="FinalProfit Amount & (%)" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="FinalPriceLtrKg" HeaderText="FinalPrice Ltr Or Kg" SortExpression="BulkProductName" HeaderStyle-Width="130px" />
                                            <asp:BoundField DataField="FinalProfitAmount_Percent_Ltr_Kg" HeaderText="FinalProfit Amount&(%)/Ltr or KG" SortExpression="BulkProductName" HeaderStyle-Width="130px" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditCategoryMapping" OnClick="EditCategoryMapping_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Edit" Width="80px" runat="server" class="btn btn-success btn-sm" />

                                                    <asp:Button ID="DelCategoryMapping" OnClick="DelCategoryMapping_Click" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Delete" Width="80px" runat="server" class="btn btn-danger btn-sm " />
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                </div>
            </main>
        </div>

    </div>

    <script type="text/javascript">

        function calculatePricelist(id) {

            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();

            //alert(document.getElementById(id).value);

            var BulkCostPerLtr = document.getElementById("<%=BulkCostPerLtr.ClientID%>");
            var BulkInterest = document.getElementById("<%=BulkInterestPertxt.ClientID%>");
            var BulkInterestAmt = document.getElementById("<%=BulkInterestAmttxt.ClientID%>");
            var BulkCostTotal = document.getElementById("<%=TotalBulkCostWithInteresttxt.ClientID%>");
            var BulkCostPerUnit = document.getElementById("<%=BulkCostPerUnittxt.ClientID%>");

            var PM = document.getElementById("<%=PMtxt.ClientID%>");
            var PMAddBuffer = document.getElementById("<%=AddBuffPMtxt.ClientID%>");
            var PMTotal = document.getElementById("<%=PMTotaltxt.ClientID%>");

            var Labour = document.getElementById("<%=Labourtxt.ClientID%>");
            var LabourAddBuffer = document.getElementById("<%=AddBuffLabourtxt.ClientID%>");
            var LabourTotal = document.getElementById("<%=TotalLabourtxt.ClientID%>");
            var LabourSubTotalA = document.getElementById("<%=TotalCalculateAtxt.ClientID%>");

            var Loss = document.getElementById("<%=LossPertxt.ClientID%>");
            var LossAmt = document.getElementById("<%=LossAmttxt.ClientID%>");
            var LossSubTotalB = document.getElementById("<%=TotalCalculateBtxt.ClientID%>");

            var Expense_Market_Per = document.getElementById("<%=MarketedByChargesPertxt.ClientID%>");
            var Expense_Market_Amt = document.getElementById("<%=MarketedByChargesAmttxt.ClientID%>");
            var Expense_Factory_Per = document.getElementById("<%=FactoryExpencePertxt.ClientID%>");
            var Expense_Factory_Amt = document.getElementById("<%=FactoryExpenceAmttxt.ClientID%>");
            var Expense_Other_Per = document.getElementById("<%=OtherPertxt.ClientID%>");
            var Expense_Other_Amt = document.getElementById("<%=OtherAmttxt.ClientID%>");
            var Expense_Profit_Per = document.getElementById("<%=ProfitPertxt.ClientID%>");
            var Expense_Profit_Amt = document.getElementById("<%=ProfitAmttxt.ClientID%>");
            var Expense_Total = document.getElementById("<%=TotalCalculateCtxt.ClientID%>");
            var Expense_Suggested = document.getElementById("<%=FinalSuggestedPricetxt.ClientID%>");


            var Final_Price_Unit = document.getElementById("<%=FinalPricePerUnittxt.ClientID%>");
            var Final_Price_Profit = document.getElementById("<%=ProfitFinalPricetxt.ClientID%>");
            var Final_Price_Profit_per = document.getElementById("<%=PerCent_ProfitFinalPricetxt.ClientID%>");
            var Final_Price_Ltr = document.getElementById("<%=FinalPriceLtrKgtxt.ClientID%>");
            var Final_Profit = document.getElementById("<%=ProfitOnFinalPricetxt.ClientID%>");
            var Final_Profit_Per = document.getElementById("<%=PercentOfProfitOnFinalPricetxt.ClientID%>");


            var PackSize = document.getElementById("<%=lblPackSize.ClientID%>").innerHTML;
            var PackMeasurement = document.getElementById("<%=lblPackMeasurement.ClientID%>").innerHTML;

            //alert(PackSize + '@' + PackMeasurement)
            var ltrtogm;

            //Bulk Calculation

            var blkintamt = parseFloat(BulkCostPerLtr.value) * parseFloat(BulkInterest.value) / 100;
            BulkInterestAmt.value = Number(blkintamt).toFixed(2);
            var blkwithintamt = parseFloat(BulkCostPerLtr.value) + parseFloat(blkintamt);
            BulkCostTotal.value = Number(blkwithintamt).toFixed(2);

            if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {
                ltrtogm = 1000 / parseFloat(PackSize);
                BulkCostPerUnit.value = Number(parseFloat(blkwithintamt) / ltrtogm).toFixed(2);
            }
            if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                ltrtogm = parseFloat(PackSize);
                BulkCostPerUnit.value = Number(parseFloat(blkwithintamt) * ltrtogm).toFixed(2);
            }
            if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                ltrtogm = 1000 / parseFloat(PackSize);
                BulkCostPerUnit.value = Number(parseFloat(blkwithintamt)).toFixed(2);
            }

            //PM calculation

            PMTotal.value = Number(parseFloat(PM.value) + parseFloat(PMAddBuffer.value)).toFixed(2);


            //Labour Calculation

            LabourTotal.value = Number(parseFloat(Labour.value) + parseFloat(LabourAddBuffer.value)).toFixed(2);
            LabourSubTotalA.value = Number(parseFloat(BulkCostPerUnit.value) + parseFloat(PMTotal.value) + parseFloat(LabourTotal.value)).toFixed(2);

            //Loss Calculation

            var lossamtval = parseFloat(LabourSubTotalA.value) * parseFloat(Loss.value) / 100;
            LossAmt.value = Number(lossamtval).toFixed(2);
            LossSubTotalB.value = Number(parseFloat(LabourSubTotalA.value) + parseFloat(lossamtval)).toFixed(2);

            //Expense Calculation

            var market = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Market_Per.value) / 100;
            var factory = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Factory_Per.value) / 100;
            var other = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Other_Per.value) / 100;
            var profit = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Profit_Per.value) / 100;

            Expense_Market_Amt.value = Number(market).toFixed(2);
            Expense_Factory_Amt.value = Number(factory).toFixed(2);
            Expense_Other_Amt.value = Number(other).toFixed(2);
            Expense_Profit_Amt.value = Number(profit).toFixed(2);

            Expense_Total.value = Number(parseFloat(market) + parseFloat(factory) + parseFloat(other) + parseFloat(profit)).toFixed(2);
            Expense_Suggested.value = Number(parseFloat(LossSubTotalB.value) + parseFloat(Expense_Total.value)).toFixed(2);

            //Final Calculation

            var ConvertLtrToGmOrMl = 0.00;

            var unipercetage = 0.00;

            if (parseFloat(Final_Price_Unit.value) > 0) {                

              

                if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {


                    ConvertLtrToGmOrMl = 1000 / parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.value) / parseFloat(ConvertLtrToGmOrMl)).toFixed(2);
                   

                    Final_Price_Profit.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) +  parseFloat(unipercetage)).toFixed(2) ;
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);

                   
                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit.value = Number(parseFloat(Final_Price_Profit.value) * ConvertLtrToGmOrMl).toFixed(2);

                  

                    Final_Profit_Per.value = Number(parseFloat(Final_Price_Profit_per.value)).toFixed(2);
                }
                if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {


                    ConvertLtrToGmOrMl = parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.value) * parseFloat(ConvertLtrToGmOrMl)).toFixed(2);                   

                    Final_Price_Profit.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);
                   
                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) / ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit.value = Number(parseFloat(Final_Price_Profit.value) * ConvertLtrToGmOrMl).toFixed(2);                  

                    Final_Profit_Per.value = Number(parseFloat(Final_Price_Profit_per.value)).toFixed(2);
                }
                if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {

                    ConvertLtrToGmOrMl = parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.value)).toFixed(2);                   

                    Final_Price_Profit.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);
                    
                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit.value = Number(parseFloat(Final_Price_Profit.value) * ConvertLtrToGmOrMl).toFixed(2);
                  
                    Final_Profit_Per.value = Number(parseFloat(Final_Price_Profit_per.value)).toFixed(2);
                }

            }

        }

    </script>

</asp:Content>
