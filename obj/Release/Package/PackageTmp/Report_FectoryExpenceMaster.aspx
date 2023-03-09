<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_FectoryExpenceMaster.aspx.cs" Inherits="Production_Costing_Software.Report_FectoryExpenceMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <link href="Content/CSS/loading.css" rel="stylesheet" />
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }

        .modalLoading {
            position: fixed;
            z-index: 1030;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: grey;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .centerLoading {
            z-index: 1031;
            margin: 290px auto;
            padding: 10px;
            width: 300px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .centerLoading img {
                height: 128px;
                width: 300px;
            }
    </style>
    <script type="text/javascript">

        var GridId = "<%=Grid_CompanyFactoryExpenceAllReport.ClientID %>";
        var ScrollHeight = 500;
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
    <link href="Content/bootstrap-5.1.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/GridViewStyle.css" rel="stylesheet" />
    <link href="Content/Loading.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <div class="card mb-4">

            <div class="card-header">

                <h6>Transportation Costing Report</h6>
            </div>
            <asp:Label ID="lblTransportationCostFactors_Id" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblCompany_Id" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblStateID" runat="server" Text="" Visible="false"></asp:Label>
            <div class="card-body">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <%--<img alt="" src="Content/Images/EmenLoading.gif" />--%>
                                <img src="Content/LogoImage/gifntext-gif.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row mb-3">
                            <div class="col-md-5">
                                <div class="input-group">
                                    <span class="input-group-text" id="MasterPackingDropdown">MasterPacking/All</span>
                                    <asp:DropDownList ID="MasterPackingAndAllDropdownList" OnSelectedIndexChanged="MasterPackingAndAllDropdownList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                        <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">MasterPack</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5" style="border: 1px;height:300px">
                                <div class="input-group">
                                    <%--<span class="input-group-text" id="MultiSelectBulk">MultiSelectBulk</span>--%>
                                    <%--<asp:DropDownList ID="BulkProductListbox" SelectMethod="Multiple" CssClass="form-check"  runat="server">
                                                            </asp:DropDownList>--%>

                                    <%--<asp:CheckBoxList CssClass="dropdown-check-list" ID="BulkProductListbox" runat="server" SelectionMode="Multiple"></asp:CheckBoxList>--%>
                                    <asp:ListBox ID="BulkProductListbox" runat="server" SelectionMode="Multiple" class="form-control"></asp:ListBox>

                                </div>
                            </div>

                            <div class="col-md-1">
                                <asp:Button ID="ChkBulkSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="ChkBulkSubmit_Click" />
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row  justify-content-center align-items-center mt-3 ">

                                        <div class=" col-10 col-md-3 col-md-4 ">
                                            <div class="d-grid">
                                                <asp:Button ID="ExcelReport" OnClick="ExcelReport_Click" runat="server" CssClass="btn btn-info" Text="Excel Report" />

                                            </div>
                                        </div>


                                        <div class=" col-10 col-md-3 col-md-4">
                                            <div class="d-grid">

                                                <asp:Button ID="PdfReport" OnClick="PdfReport_Click" runat="server" CssClass="btn btn-warning" Text="Pdf Report" />

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ExcelReport" />
                                    <asp:PostBackTrigger ControlID="PdfReport" />
                                </Triggers>
                            </asp:UpdatePanel>
                           
                                <div class=" card-body mb-4 mt-4 " style="width: 100%; height: 500px; overflow: auto"">
                                    <asp:GridView ID="Grid_CompanyFactoryExpenceAllReport" Autopostback="true"
                                        CssClass="table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter"
                                        runat="server" AutoGenerateColumns="False" DataKeyNames="" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="Packing" HeaderText="Packing" SortExpression="Measurement" />
                                            <asp:BoundField DataField="FinalCostBulk" HeaderText="Bulk-Cost/Unit" SortExpression="Measurement" />
                                            <asp:BoundField DataField="FinalPMCostPerUnit" HeaderText="Total-PM-Cost/Unit" SortExpression="Measurement" />
                                            <asp:BoundField DataField="LabourChargePer" HeaderText="Labour-Cost /Unit" SortExpression="Measurement" />
                                            <asp:BoundField DataField="PackingLossAmt" HeaderText="Packing Loss Amt" SortExpression="Measurement" />
                                            <asp:BoundField DataField="TotalCosting" HeaderText="TotalCosting / Unit" SortExpression="Measurement" />

                                            <asp:BoundField DataField="TotalAmtPerLiter" HeaderText="Total Amount / Ltr Or KG" SortExpression="Source" />

                                            <asp:TemplateField HeaderText="Fectory Expence Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation" runat="server" Text='<%#Eval("FectoryExpenceAmount") %>'></asp:Label>
                                                    <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("FectoryExpencePer")% %>'></asp:Label>--%>
                                                       (<%# Eval("FectoryExpencePer")%>%)
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marketed By Charges Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation" runat="server" Text='<%#Eval("MarketedByChargesAmount") %>'></asp:Label>
                                                    <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("FectoryExpencePer")% %>'></asp:Label>--%>
                                                      (<%# Eval("MarketedByChargesPer")%>%)
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation" runat="server" Text='<%#Eval("OtherPerAmount") %>'></asp:Label>
                                                    <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("FectoryExpencePer")% %>'></asp:Label>--%>
                                                      (<%# Eval("OtherPer")%>%)
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalExpenceAmount" HeaderText="Total Expence Amount" SortExpression="Source" />

                                            <asp:TemplateField HeaderText="Profit Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation" runat="server" Text='<%#Eval("ProfitAmount") %>'></asp:Label>
                                                    <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("FectoryExpencePer")% %>'></asp:Label>--%>
                                                      (<%# Eval("ProfitPer")%>%)
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <%--      <asp:BoundField DataField="MarketedByChargesAmount" HeaderText="MarketedByChargesAmount" SortExpression="BulkProductName" />
                                                            <asp:BoundField DataField="OtherPerAmount" HeaderText="Other Amount" SortExpression="Source" />
                                                            <asp:BoundField DataField="ProfitAmount" HeaderText="Profit Amount" SortExpression="Source" />--%>
                                            <asp:BoundField DataField="FinalFactoryCostLiter" HeaderText="Final Factory Cost/Liter" SortExpression="Source" />
                                            <asp:BoundField DataField="FinalFectoryCostPerUnit" HeaderText="Final FectoryCost / Unit" SortExpression="Source" />
                                        </Columns>

                                    </asp:GridView>
                                </div>
                           
                        </div>
                        <triggers>
                            <%--     <asp:PostBackTrigger ControlID="ExcelReport" />
                            <asp:PostBackTrigger ControlID="PdfReport" />--%>
                        </triggers>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
    </form>
</body>

</html>
