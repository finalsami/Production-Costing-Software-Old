<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="FinishGoodsPricingReport.aspx.cs" Inherits="Production_Costing_Software.FinishGoodsPricingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_FinishGoodsPricingReport.ClientID %>";
        var ScrollHeight = 700;
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
    <script type="text/javascript">
        function ClearHtml() {
            document.getElementById("ContentPlaceHolder1_UpdatePanel2").innerHTML = "";
        }
    </script>
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>

                <%-----------------------------Lables---------------------%>
                <div>
                    <asp:Label ID="lblCompanyMasterList_Name" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblUserMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanEdit" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                    <%-----------------------%>
                </div>

                <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Finish Goods Pricing Report</h1>
                    <ol class="breadcrumb mb-4">
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                            <i class="fas fa-table me-1"></i>
                            Finish Goods Pricing Report
                        </div>

                    </div>
                    <div class="card-body mt-4  mb-4 overflow-auto">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Grid_FinishGoodsPricingReport" CssClass="table-hover table-responsive gridview overflow-scroll"
                                    GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                    PagerStyle-CssClass="gridview_pager"
                                    AlternatingRowStyle-CssClass="gridview_alter" runat="server" Autopostback="true"
                                    AutoGenerateColumns="False" DataKeyNames="Productwise_Labor_cost_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="PM_RM_Category_Name" />
                                        <%--<asp:BoundField DataField="TradeName" HeaderText="Trade Name" SortExpression="PM_RM_Category_Name" />--%>
                                        <%--<asp:BoundField DataField="Unit Measurement" HeaderText="Unit Measurement" SortExpression="PM_RM_Category_Name" />--%>

                                        <asp:BoundField DataField="PackingUnitMeasurement" HeaderText="Packing Size" SortExpression="PM_RM_Category_Name" />
                                        <asp:BoundField DataField="FinalBulkCostPerUnit" HeaderText="Bulk-Cost/Unit (With Interest Amt)" SortExpression="PM_RM_Category_Name" />
                                        <asp:BoundField DataField="FinalPMCostPerUnit" HeaderText="Total-PM-Cost/Unit" SortExpression="PM_RM_Category_Name" />
                                        <asp:BoundField DataField="LabourChargePer" HeaderText="Labour-Cost /Unit" SortExpression="PM_RM_Category_Name" />
                                        <%--<asp:BoundField DataField="PackingLossAmtPerUnit" HeaderText="PackingLoss Amount / Unit" SortExpression="PM_RM_Category_Name" />--%>
                                        <asp:BoundField DataField="Fk_PackingLoss" HeaderText="Packing Loss" SortExpression="PM_RM_Category_Name" />
                                        <%--<asp:BoundField DataField="PackingLossAmtPerUnit" HeaderText="PackingLoss Amount / Unit" SortExpression="PM_RM_Category_Name" />--%>
                                        <%--<asp:BoundField DataField="TotalCosting" HeaderText="TotalCosting" SortExpression="PM_RM_Category_Name" />--%>

                                        <%--<asp:BoundField DataField="PackingPerLossPer" HeaderText="Packing/Loss" SortExpression="PM_RM_Category_Name" />--%>

                                        <%--<asp:BoundField DataField="FactoryExpencePercent" HeaderText="Factory Expence (%)" SortExpression="PM_RM_Category_Name" />--%>
                                        <%--       <asp:TemplateField HeaderText="Factory Expence Amount">
                                                <ItemTemplate>
                                                    <%# Eval("FactoryExpenceAmtPerUnit")%>    (<%# Eval("FactoryExpencePercent")%>%)
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                        <%--<asp:BoundField DataField="FactoryExpenceAmtPerUnit" HeaderText="Factory Expence Amount" SortExpression="PM_RM_Category_Name" />--%>
                                        <asp:BoundField DataField="TotalAmtPerUnit" HeaderText="Total-Amount / Unit" SortExpression="PM_RM_Category_Name" />

                                        <asp:BoundField DataField="TotalAmtPerLiterFinal" HeaderText="Total-Amount/Liter (NRV)" SortExpression="PM_RM_Category_Name" />


                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="ReportPopupBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ReportPopupBtn_Click" Text="Report" runat="server" OnClientClick="ClearHtml();" data-bs-toggle="modal" data-bs-target="#ReportlModal" class="btn btn-success btn-sm float-md-end" />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <%--******************************************Modal*******************************************--%>
                        <div class="modal fade" id="ReportlModal" tabindex="-1">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content col-lg-12">
                                    <div class="modal-header" style="background-color: #343a40">
                                        <%--                                            <h3 style="color: white">Bulk Costing</h3>/<asp:Label ID="lblname" runat="server" Text="" ></asp:Label>--%>
                                        <h4 style="color: white">&nbsp; Bulk Costing Report</h4>
                                        <asp:Label ID="lblname" runat="server" CssClass="font-monospace" Style="color: white" Visible="true"></asp:Label>

                                        <asp:Label ID="lblPackingSize" runat="server" CssClass="font-monospace" Style="color: white" Visible="true"></asp:Label>

                                        <button aria-label="Close" style="color: white" class="btn-close btn-close-white" data-bs-dismiss="modal" type="button">
                                        </button>
                                    </div>
                                    <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="modal-body overflow-scroll">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <%--                  <div class="mt-4" style="overflow: auto">
                                                        <asp:GridView ID="PopupGrid_Report" CssClass="table-hover table-responsive gridview overflow-scroll"
                                                            GridLines="None"
                                                            AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                                            AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" runat="server" Autopostback="true"
                                                            AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="No">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="IngredientName" HeaderText="RM Name" SortExpression="IngredientName" />
                                                                <asp:BoundField DataField="QTY" HeaderText="Qty" SortExpression="Qty" />
                                                                <asp:BoundField DataField="RateAmount_KG" HeaderText="Rate" SortExpression="RateAmount_KG" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Total" SortExpression="Total" />

                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>--%>
                                                <asp:Table runat="server" CssClass="table-hover table-responsive gridview" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); overflow: scroll">
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <%--***********************************************************--%>
                </div>



            </main>

        </div>
    </div>
</asp:Content>
