<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EstimatePriceListOld.aspx.cs" Inherits="Production_Costing_Software.EstimatePriceListOld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <script type="text/javascript">
        var GridId = "<%=Grid_EstimatePricelist.ClientID %>";
        var ScrollHeight = 800;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
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
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }

    </script>--%>
    <%--<script type="text/javascript">
        var GridId = "<%=Grid_EstimatePricelistStatusWise.ClientID %>";
        var ScrollHeight = 800;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
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
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }

    </script>--%>
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
            opacity: 0.6;
        }

        .centerLoading {
            z-index: 1031;
            margin: 290px auto;
            padding: 10px;
            width: 300px;
            opacity: 1;
        }

            .centerLoading img {
                height: 150px;
                width: 250px;
            }
    </style>


    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img src="Content/LogoImage/gifntext-gif.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <%--Lables--%>
                <div>
                    <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblUserMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanEdit" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanView" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <%----------------------------------%>
                <%----------Label for Suggested CWFE-------------%>
                <asp:Label ID="lblPriceList_TotalCostPerLtr1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_TotalCostPerLtr" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_FctryExp_Per1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_FctryExp_Per" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_FctryExp_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_FctryExp_Amt" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_Other_Per1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_Other_Per" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_Profit_Per1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_Profit_Per" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_Profit_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_Profit_Amt" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_Other_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_Other_Amt" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_Mrkt_Per1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_Mrkt_Per" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_Mrkt_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_Mrkt_Amt" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblFk_PMRM_Catgeory_Id" runat="server" Text="" Visible="false"></asp:Label>


                <asp:Label ID="lblSuggestedPricetxt1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblSuggestedPricetxt" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblCompanyExpence_Id1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCompanyExpence_Id" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lbl_BPM_Id1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblAllEstimatePriceList" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblEstimateName" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblPriceList_FFCostPerLtr1" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPriceList_FFCostPerLtr" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblCompany_Id" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblIsMasterPacking" runat="server" Text="" Visible="false"></asp:Label>

                <%----------------------------------%>


                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">PriceList Other Company   [
                        <asp:Label ID="lblCompanyMasterList_Name" Style="font-size: 35px; font-family: monospace; color: forestgreen" runat="server" Text=""></asp:Label>]</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="EstimatePriceList.aspx">PriceList Other Company
                                                     

                        </a></li>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            <asp:DropDownList ID="OtherCompanyDropdown" Style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3); height: 40px;" AutoPostBack="true" Visible="false" CssClass="btn btn-success dropdown-toggle mt-1" runat="server" OnSelectedIndexChanged="OtherCompanyDropdown_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button ID="BtnCreatePricelist" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" CssClass="btn btn-primary  float-end m-1" runat="server" OnClick="BtnCreatePricelist_Click" Text="Create PriceList" />

                            <asp:DropDownList ID="MasterPackingDropdown" AutoPostBack="true" Enabled="false" Width="200px" CssClass="form-select float-end m-1" runat="server" OnSelectedIndexChanged="MasterPackingDropdown_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">MasterPack</asp:ListItem>
                                <asp:ListItem Value="0">AllSize</asp:ListItem>

                            </asp:DropDownList>
                            <div>
                                <marquee direction="left" style="color: red; font-family: monospace">
                                    Work in progess !
                                </marquee>
                            </div>
                            <div class="col-sm-3  float-end m-1">
                                <div class="input-group mb-3">
                                    <span class="input-group-text" id="inputGroup-sizing-default">Price List Name</span>
                                    <asp:TextBox ID="CreatePriceListNametxt" Enabled="false" OnTextChanged="CreatePriceListName_TextChanged" class="form-control" type="text" runat="server"></asp:TextBox>

                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="CreatePriceListNametxt" runat="server" ForeColor="Red" ErrorMessage="* Enter Price List Name"></asp:RequiredFieldValidator>

                            </div>

                            <asp:DropDownList ID="EstimatedPriceDropdown" AutoPostBack="true" Width="200px" CssClass="form-select float-end m-1" runat="server" OnSelectedIndexChanged="EstimatedPriceDropdown_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">Estimated</asp:ListItem>
                                <asp:ListItem Value="0">Actual</asp:ListItem>
                                <asp:ListItem Value="2">Both</asp:ListItem>

                            </asp:DropDownList>
                            <asp:Button ID="PdfReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" CausesValidation="false" CssClass="btn btn-danger  float-end m-1" runat="server" OnClick="PdfReport_Click" Text="Pdf Report Grid" />
                            <asp:Button ID="AddOtherCompanyPriceEstimate" OnClick="AddOtherCompanyPriceEstimate_Click" CausesValidation="false" runat="server" Style="z-index: 999999999999; margin-left: 5px" CssClass="btn btn-success position-fixed mt-1 float-end" Text="+Add" />

                        </div>


                        <asp:Label ID="lblCompanyFactoryExpence_Id" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>




                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <div class="card-body" style="overflow: auto">
                                    <asp:GridView ID="Grid_EstimatePricelist" CssClass=" table-hover table-responsive gridview" Visible="false"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="OtherComapnyPriceList_ID">
                                        <Columns>
                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>
                                            <asp:TemplateField HeaderText="Status" ControlStyle-Width="130px">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblStatus" Text='<%#Eval("Status") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="PackingSize_BoxSize" HeaderText="PackingSize & BoxSize" SortExpression="TradeName" />
                                            <%--<asp:BoundField DataField="BulkCost" HeaderText="BulkCost" SortExpression="BulkProductName" />--%>
                                            <asp:TemplateField HeaderText="BulkCost" ControlStyle-Width="130px">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="BulkCosttxt" Text='<%#Eval("BulkCost") %>' AutoPostBack="true" OnTextChanged="BulkCosttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BulkCost" ControlStyle-Width="130px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Bulk_Interest_Percenttxt" Text='<%#Eval("Bulk_Interest_Percent") %>' AutoPostBack="true" OnTextChanged="Bulk_Interest_Percenttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    (<asp:Label ID="lbIntrest_Amount" runat="server" Text='<%#Eval("Intrest_Amount") %>'></asp:Label>Rs)

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>


                                            <%-- <asp:BoundField DataField="IntrestPercent_Amount" HeaderText="Intrest(%)_&_Amount_On_Bulk" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="TotalBulkCost" HeaderText="Total_Bulk_Cost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="BulkCostPerUnit" HeaderText="Bulk_Cost/Unit" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="PM_And_Buffer" HeaderText="Final_PM_Cost_(PM+Buffer_PM)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Labour_Addiition" HeaderText="Final_Labour_Cost_(Labour+Buffer_Labour)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="SubTotalA" HeaderText="SubTotal A" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Loss_Percent_LossAmt" HeaderText="Loss(%)_&_Amount" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="TotalB" HeaderText="Total B" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="MarketedByCharge_Per" HeaderText="Marketed_By_Charge(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FactoryExpence_Percent" HeaderText="Factory_Expence(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Other_Percent" HeaderText="Other_Amount_(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Profit_Percent" HeaderText="Profit_Amount_(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="SuggestedFinalTotalCost" HeaderText="Suggested_Final_Total_Cost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalPricePerUnit" HeaderText="FinalPrice/Unit" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalProfitAmount_Percent" HeaderText="Final_Profit_Amount_&_(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalPriceLtrKg" HeaderText="FinalPrice_Ltr_Or_Kg" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalProfitAmount_Percent_Ltr_Kg" HeaderText="Final_Profit_Amount_&_(%)_/Ltr_or_KG" SortExpression="BulkProductName" />--%>
                                        </Columns>

                                    </asp:GridView>
                                </div>


                                <div class="card-body" style="overflow: auto">

                                    <asp:GridView ID="Grid_EstimatePricelistStatusWise" CssClass=" table-hover table-responsive gridview" Visible="false"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);" OnRowDataBound="Grid_EstimatePricelistStatusWise_RowDataBound"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="OtherComapnyPriceList_ID">
                                        <Columns>
                                            <%--                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblStatus" Text='<%#Eval("Status") %>' Style="text-align: center" runat="server"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM_Product_Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="PackingSize_BoxSize" HeaderText="PackingSize & BoxSize" SortExpression="TradeName" />
                                            <%--<asp:BoundField DataField="BulkCost" HeaderText="BulkCost" SortExpression="BulkProductName" />--%>
                                            <asp:TemplateField HeaderText="BulkCost/Ltr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="BulkCosttxt" Text='<%#Eval("BulkCost") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pack_Size" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPack_Size" Text='<%#Eval("Pack_Size") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pack_Measurement" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPack_Measurement" Text='<%#Eval("Pack_Measurement") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pack_Measurement" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPMRM_Category_Id" Text='<%#Eval("PMRM_Category_Id") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Intrest_%And_Amount">
                                                <ItemTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="BulkInterestPercenttxt" Text='<%#Eval("Bulk_Interest_Percent") %>' AutoPostBack="true" OnTextChanged="Bulk_Interest_Percenttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basic-addon2">%</span>
                                                    </div>

                                                    <asp:Label ID="lbIntrest_Amount" runat="server" Text='<%#Eval("Intrest_Amount") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TotalBulkCost">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TotalBulkCosttxt" Text='<%#Eval("TotalBulkCost") %>' AutoPostBack="true" OnTextChanged="TotalBulkCosttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BulkCost/Unit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="BulkCostPerUnittxt" Text='<%#Eval("BulkCostPerUnit") %>' AutoPostBack="true" OnTextChanged="BulkCostPerUnittxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PM&Adiitional_Buffer">
                                                <ItemTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="PMtxt" Text='<%#Eval("PM") %>' AutoPostBack="true" OnTextChanged="PMtxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">PM</span>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="PM_Additional_Buffertxt" Text='<%#Eval("PM_Additional_Buffer") %>' AutoPostBack="true" OnTextChanged="PM_Additional_Buffertxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPMBuffer">Buffer</span>
                                                    </div>
                                                    <asp:Label ID="lblTotalPM" runat="server" Text='<%#Eval("TotalPM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Labour&Additional_Buffer">
                                                <ItemTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Labourtxt" Text='<%#Eval("Labour") %>' AutoPostBack="true" OnTextChanged="Labourtxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">Labour</span>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Labour_Additional_Buffertxt" Text='<%#Eval("Labour_Additional_Buffer") %>' AutoPostBack="true" OnTextChanged="Labour_Additional_Buffertxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPMBuffer">Buffer</span>
                                                    </div>
                                                    <asp:Label ID="lblTotalLabour" runat="server" Text='<%#Eval("TotalLabour") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub_Total_A">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="SubTotalAtxt" Text='<%#Eval("SubTotalA") %>' AutoPostBack="true" OnTextChanged="Labourtxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loss % And_Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLossAmt" Text='<%#Eval("LossAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="PackLoss_Percenttxt" Text='<%#Eval("PackLoss_Percent") %>' AutoPostBack="true" OnTextChanged="PackLoss_Percenttxt_TextChanged" CssClass="form-control" Style="text-align: center" type="text" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total_B_Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TotalBtxt" Text='<%#Eval("TotalB") %>' AutoPostBack="true" OnTextChanged="TotalBtxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarketedByCharges">
                                                <ItemTemplate>
                                                    <asp:Label ID="MarketedByCharge_Amttxt" Text='<%#Eval("MarketedByCharge_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>

                                                    <div class="input-group">
                                                        <asp:TextBox ID="MarketedByChargesPertxt" Text='<%#Eval("MarketedByChargesPer") %>' AutoPostBack="true" OnTextChanged="MarketedByChargesPertxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Factory_Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="FactoryExpence_Amttxt" Text='<%#Eval("FactoryExpence_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="FactoryExpence_Pertxt" Text='<%#Eval("FactoryExpence_Per") %>' AutoPostBack="true" OnTextChanged="FactoryExpence_Pertxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other_Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="Other_Amttxt" Text='<%#Eval("Other_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Other_Pertxt" Text='<%#Eval("Other_Per") %>' AutoPostBack="true" OnTextChanged="Other_Pertxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profit_Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="Profit_Amttxt" Text='<%#Eval("Profit_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Profit_Pertxt" Text='<%#Eval("Profit_Per") %>' AutoPostBack="true" OnTextChanged="Profit_Pertxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total(Expences&Profit)">
                                                <ItemTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Totaltxt" Text='<%#Eval("Total") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPMBuffer">Rs</span>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SuggestedFinal TotalCost/Unit">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="SuggestedFinalTotalCosttxt" Text='<%#Eval("SuggestedFinalTotalCost") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Final_Price_/_Unit">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="FinalPricePerUnittxt" Text='<%#Eval("FinalPricePerUnit") %>' AutoPostBack="true" OnTextChanged="FinalPricePerUnittxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalProfitAmount % / Unit">
                                                <ItemTemplate>

                                                    <asp:Label ID="FinalProfit_Amttxt" Text='<%#Eval("FinalProfit_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>


                                                    <asp:TextBox ID="FinalProfit_Percenttxt" Text='<%#Eval("FinalProfit_Percent") %>' AutoPostBack="true" OnTextChanged="FinalProfit_Percenttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalPrice_Ltr_Kg">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="FinalPriceLtrKgtxt" Text='<%#Eval("FinalPriceLtrKg") %>' AutoPostBack="true" OnTextChanged="LossAmttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalProfitAmount_%_Ltr_Kg">
                                                <ItemTemplate>

                                                    <asp:Label ID="FinalProfitAmount_Percent_Ltr_Kg_Amttxt" Text='<%#Eval("FinalProfitAmount_Percent_Ltr_Kg_Amt") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:Label>


                                                    <asp:TextBox ID="FinalProfitAmount_Percent_Ltr_Kg_Pertxt" Text='<%#Eval("FinalProfitAmount_Percent_Ltr_Kg_Per") %>' AutoPostBack="true" OnTextChanged="LossAmttxt_TextChanged" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BPM_Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBPM_Id" Text='<%#Eval("BPM_iD") %>' runat="server"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BPM_iD" HeaderText="BPM Product Name" Visible="false" SortExpression="MainCategory_Name" />
                                            <asp:TemplateField HeaderText="Select" ControlStyle-Width="90px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox_Check" OnCheckedChanged="CheckBox_Check_CheckedChanged" runat="server" Checked="false" CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOtherComapnyPriceList_Id" Text='<%#Eval("OtherComapnyPriceList_Id") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="PackingSize_BoxSize" HeaderText="PackingSize & BoxSize" SortExpression="TradeName" />
                                            <asp:BoundField DataField="BulkCost" HeaderText="BulkCost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="IntrestPercent_Amount" HeaderText="Intrest(%)_&_Amount_On_Bulk" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="TotalBulkCost" HeaderText="Total_Bulk_Cost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="BulkCostPerUnit" HeaderText="Bulk_Cost/Unit" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="PM_And_Buffer" HeaderText="Final_PM_Cost_(PM+Buffer_PM)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Labour_Addiition" HeaderText="Final_Labour_Cost_(Labour+Buffer_Labour)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="SubTotalA" HeaderText="SubTotal A" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Loss_Percent_LossAmt" HeaderText="Loss(%)_&_Amount" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="TotalB" HeaderText="Total B" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="MarketedByCharge_Per" HeaderText="Marketed_By_Charge(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FactoryExpence_Percent" HeaderText="Factory_Expence(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Other_Percent" HeaderText="Other_Amount_(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Profit_Percent" HeaderText="Profit_Amount_(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="SuggestedFinalTotalCost" HeaderText="Suggested_Final_Total_Cost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalPricePerUnit" HeaderText="FinalPrice/Unit" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalProfitAmount_Percent" HeaderText="Final_Profit_Amount_&_(%)" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalPriceLtrKg" HeaderText="FinalPrice_Ltr_Or_Kg" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="FinalProfitAmount_Percent_Ltr_Kg" HeaderText="Final_Profit_Amount_&_(%)_/Ltr_or_KG" SortExpression="BulkProductName" />--%>
                                        </Columns>

                                    </asp:GridView>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="PdfReport" />
                                <asp:PostBackTrigger ControlID="AddOtherCompanyPriceEstimate" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>


            </main>
        </div>

    </div>
</asp:Content>
