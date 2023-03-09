<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EstimatePriceList.aspx.cs" Inherits="Production_Costing_Software.EstimatePriceList" %>

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
         .txtlbl {
            border: none;
            display: none;
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

             .txtstyle {
  
             }
             .rowstylemerge1
             {
                /* border-top:red 3px solid;
                 border-left:red 3px solid;
                 border-right:red 3px solid;
                 border-bottom:red 1px solid;*/

               
             }
               .rowstylemerge2 
             {
                background-color:#E7E7E7;
                 /*border-top:red 1px solid;
                 border-left:red 3px solid;
                 border-right:red 3px solid;
                 border-bottom:red 3px solid;*/
             }
             .rowstylealter
             {
                 background-color:#E7E7E7;
             }
             .rowstyle
             {

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
                                        AlternatingRowStyle-CssClass="gridview_alter rowstyle" runat="server" AutoGenerateColumns="False" DataKeyNames="OtherComapnyPriceList_ID">
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
                                        AlternatingRowStyle-CssClass="" runat="server" AutoGenerateColumns="False" DataKeyNames="OtherComapnyPriceList_ID">
                                        <Columns>
                                            <%--                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>
                                            
                                            
                                            <asp:TemplateField HeaderText="Id">
                                                <ItemTemplate>
                                                     <asp:Label ID="lbbpm" runat="server" style="display:none" Text='<%#Eval("bpm_id") %>'></asp:Label>
                                                    <asp:Label ID="lblid" Text="1" Style="text-align: center" runat="server"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
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
                                                     <asp:TextBox ID="BulkCosttxt" Text='<%#Eval("BulkCost") %>'  Enabled="false"  step='any' onchange="calculatePricelist(this.id);"  CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                    <asp:Label ID="lblpksize" runat="server" style="display:none" Text='<%#Eval("Pack_Size") %>'></asp:Label>
                                                     <asp:Label ID="lblpkmes" runat="server" style="display:none" Text='<%#Eval("Pack_Measurement") %>'></asp:Label>
                                                      <asp:Label ID="rown" runat="server" style="display:none" Text='<%#Eval("od") %>'></asp:Label>
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
                                                        <asp:TextBox ID="BulkInterestPercenttxt" Text='<%#Eval("Bulk_Interest_Percent") %>'  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basic-addon2">%</span>
                                                    </div>

                                                    <asp:Label ID="lbIntrest_Amount" runat="server" Text='<%#Eval("Intrest_Amount") %>'></asp:Label>
                                                    
                                                     <asp:TextBox ID="lbIntrest_Amount_lbl" Text='<%#Eval("Intrest_Amount") %>'  CssClass="txtlbl" Style="text-align: center"  runat="server"></asp:TextBox>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TotalBulkCost">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TotalBulkCosttxt" Text='<%#Eval("TotalBulkCost") %>' onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                     <asp:TextBox ID="TotalBulkCosttxt_lbl" Text='<%#Eval("TotalBulkCost") %>'  CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BulkCost/Unit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="BulkCostPerUnittxt" Text='<%#Eval("BulkCostPerUnit") %>'  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                      <asp:TextBox ID="BulkCostPerUnittxt_lbl" Text='<%#Eval("BulkCostPerUnit") %>'  CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PM&Adiitional_Buffer">
                                                <ItemTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="PMtxt" Text='<%#Eval("PM") %>'  CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">PM</span>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="PM_Additional_Buffertxt" Text='<%#Eval("PM_Additional_Buffer") %>'  onchange="calculatePricelist(this.id);"  CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPMBuffer">Buffer</span>
                                                    </div>
                                                    <asp:Label ID="lblTotalPM" runat="server" Text='<%#Eval("TotalPM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Labour&Additional_Buffer">
                                                <ItemTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Labourtxt" Text='<%#Eval("Labour") %>'  CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">Labour</span>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Labour_Additional_Buffertxt" Text='<%#Eval("Labour_Additional_Buffer") %>'  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPMBuffer">Buffer</span>
                                                    </div>
                                                    <asp:Label ID="lblTotalLabour" runat="server" Text='<%#Eval("TotalLabour") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub_Total_A">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="SubTotalAtxt" Text='<%#Eval("SubTotalA") %>'  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                      <asp:TextBox ID="SubTotalAtxt_lbl" Text='<%#Eval("SubTotalA") %>'  CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loss % And_Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLossAmt" Text='<%#Eval("LossAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="PackLoss_Percenttxt" Text='<%#Eval("PackLoss_Percent") %>'  CssClass="form-control" Style="text-align: center" type="text" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total_B_Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TotalBtxt" Text='<%#Eval("TotalB") %>'  CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                          <asp:TextBox ID="TotalBtxt_lbl" Text='<%#Eval("TotalB") %>'  CssClass="txtlbl" Style="text-align: center"  runat="server"></asp:TextBox>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarketedByCharges">
                                                <ItemTemplate>
                                                    <asp:Label ID="MarketedByCharge_Amttxt" Text='<%#Eval("MarketedByCharge_Amt") %>'  Style="text-align: center" TextMode="Number" runat="server"></asp:Label>

                                                    <div class="input-group">
                                                        <asp:TextBox ID="MarketedByChargesPertxt" Text='<%#Eval("MarketByCharge_Percent") %>'  Enabled="false"   onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Factory_Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="FactoryExpence_Amttxt" Text='<%#Eval("FactoryExpence_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="FactoryExpence_Pertxt" Text='<%#Eval("FactoryExpence_Per") %>'  Enabled="false"   onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other_Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="Other_Amttxt" Text='<%#Eval("Other_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Other_Pertxt" Text='<%#Eval("Other_Per") %>'  Enabled="false"   onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                        <span class="input-group-text" id="basicPM">%</span>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profit_Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="Profit_Amttxt" Text='<%#Eval("Profit_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Profit_Pertxt" Text='<%#Eval("Profit_Per") %>'  Enabled="false"  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
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

                                                    <asp:TextBox ID="SuggestedFinalTotalCosttxt" Text='<%#Eval("SuggestedFinalTotalCost") %>'  Enabled="false"   step='any' onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Final_Price_/_Unit">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="FinalPricePerUnittxt" Text='<%#Eval("FinalPricePerUnit") %>'  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalProfitAmount % / Unit">
                                                <ItemTemplate>

                                                    <asp:Label ID="FinalProfit_Amttxt" Text='<%#Eval("FinalProfit_Amt") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>

                                                         <asp:TextBox ID="FinalProfit_Amttxt_lbl" Text='<%#Eval("FinalProfit_Amt") %>'  CssClass="txtlbl" Style="text-align: center"  runat="server"></asp:TextBox>



                                                    <asp:TextBox ID="FinalProfit_Percenttxt" Text='<%#Eval("FinalProfit_Percent") %>'  Enabled="false"  step='any' onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalPrice_Ltr_Kg">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="FinalPriceLtrKgtxt" Text='<%#Eval("FinalPriceLtrKg") %>'  Enabled="false"  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                     <asp:TextBox ID="FinalPriceLtrKgtxt_lbl" Text='<%#Eval("FinalPriceLtrKg") %>'  CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalProfitAmount_%_Ltr_Kg">
                                                <ItemTemplate>

                                                    <asp:Label ID="FinalProfitAmount_Percent_Ltr_Kg_Amttxt" Text='<%#Eval("FinalProfitAmount_Percent_Ltr_Kg_Amt") %>'  CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:Label>

                                                    <asp:TextBox ID="FinalProfitAmount_Percent_Ltr_Kg_Amttxt_lbl" Text='<%#Eval("FinalProfitAmount_Percent_Ltr_Kg_Amt") %>'  CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                    <asp:TextBox ID="FinalProfitAmount_Percent_Ltr_Kg_Pertxt" Text='<%#Eval("FinalProfitAmount_Percent_Ltr_Kg_Per") %>' Enabled="false" step='any'  onchange="calculatePricelist(this.id);" CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>

                                                     <asp:TextBox ID="FinalProfitAmount_Percent_Ltr_Kg_Pertxt_lbl" Text='<%#Eval("FinalProfitAmount_Percent_Ltr_Kg_Per") %>'  CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


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

    <script type="text/javascript">

        function calculatePricelist(id) {           

            var mainid = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var grd = '<%=Grid_EstimatePricelistStatusWise.ClientID%>';

            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();

            //Buklcost calculation

            var BulkCostPerLtr = document.getElementById(grd + "_BulkCosttxt_" + mainid);
            var BulkInterest = document.getElementById(grd + "_BulkInterestPercenttxt_" + mainid);
            var BulkInterestAmt = document.getElementById(grd + "_lbIntrest_Amount_" + mainid);
            var BulkCostTotal = document.getElementById(grd + "_TotalBulkCosttxt_" + mainid);
            var BulkCostPerUnit = document.getElementById(grd + "_BulkCostPerUnittxt_" + mainid);

            var PackSize = document.getElementById(grd + "_lblpksize_" + mainid).innerHTML;
            var PackMeasurement = document.getElementById(grd + "_lblpkmes_" + mainid).innerHTML;


            var blkintamt = parseFloat(BulkCostPerLtr.value) * parseFloat(BulkInterest.value) / 100;
            BulkInterestAmt.innerHTML = Number(blkintamt).toFixed(2);
            var blkwithintamt = parseFloat(BulkCostPerLtr.value) + parseFloat(blkintamt);
            BulkCostTotal.value = Number(blkwithintamt).toFixed(2);

            var ltrtogm;

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


            document.getElementById(grd + "_lbIntrest_Amount_lbl_" + mainid).value = BulkInterestAmt.innerHTML;
            document.getElementById(grd + "_TotalBulkCosttxt_lbl_" + mainid).value = BulkCostTotal.value;
            document.getElementById(grd + "_BulkCostPerUnittxt_lbl_" + mainid).value = BulkCostPerUnit.value;


            //PM calculation

            var PM = document.getElementById(grd + "_PMtxt_" + mainid);
            var PMAddBuffer = document.getElementById(grd + "_PM_Additional_Buffertxt_" + mainid);
            var PMTotal = document.getElementById(grd + "_lblTotalPM_" + mainid);           

            PMTotal.innerHTML = Number(parseFloat(PM.value) + parseFloat(PMAddBuffer.value)).toFixed(2);


            //Labour Calculation

            var Labour = document.getElementById(grd + "_Labourtxt_" + mainid);
            var LabourAddBuffer = document.getElementById(grd + "_Labour_Additional_Buffertxt_" + mainid);
            var LabourTotal = document.getElementById(grd + "_lblTotalLabour_" + mainid);
            var LabourSubTotalA = document.getElementById(grd + "_SubTotalAtxt_" + mainid);            

            LabourTotal.innerHTML = Number(parseFloat(Labour.value) + parseFloat(LabourAddBuffer.value)).toFixed(2);
            LabourSubTotalA.value = Number(parseFloat(BulkCostPerUnit.value) + parseFloat(PMTotal.innerHTML) + parseFloat(LabourTotal.innerHTML)).toFixed(2);


            document.getElementById(grd + "_SubTotalAtxt_lbl_" + mainid).value = LabourSubTotalA.value;


            //Loss Calculation

            var Loss = document.getElementById(grd + "_PackLoss_Percenttxt_" + mainid);
            var LossAmt = document.getElementById(grd + "_lblLossAmt_" + mainid);
            var LossSubTotalB = document.getElementById(grd + "_TotalBtxt_" + mainid);
            
            var lossamtval = parseFloat(LabourSubTotalA.value) * parseFloat(Loss.value) / 100;
            LossAmt.innerHTML = Number(lossamtval).toFixed(2);
            LossSubTotalB.value = Number(parseFloat(LabourSubTotalA.value) + parseFloat(lossamtval)).toFixed(2);


            document.getElementById(grd + "_TotalBtxt_lbl_" + mainid).value = LossSubTotalB.value;


            //Expense Calculation

            var Expense_Market_Per = document.getElementById(grd + "_MarketedByChargesPertxt_" + mainid);
            var Expense_Market_Amt = document.getElementById(grd + "_MarketedByCharge_Amttxt_" + mainid);
            var Expense_Factory_Per = document.getElementById(grd + "_FactoryExpence_Pertxt_" + mainid);
            var Expense_Factory_Amt = document.getElementById(grd + "_FactoryExpence_Amttxt_" + mainid);
            var Expense_Other_Per = document.getElementById(grd + "_Other_Pertxt_" + mainid);
            var Expense_Other_Amt = document.getElementById(grd + "_Other_Amttxt_" + mainid);
            var Expense_Profit_Per = document.getElementById(grd + "_Profit_Pertxt_" + mainid);
            var Expense_Profit_Amt = document.getElementById(grd + "_Profit_Amttxt_" + mainid);
            var Expense_Total = document.getElementById(grd + "_Totaltxt_" + mainid);
            var Expense_Suggested = document.getElementById(grd + "_SuggestedFinalTotalCosttxt_" + mainid);

           
            var market = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Market_Per.value) / 100;
            var factory = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Factory_Per.value) / 100;
            var other = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Other_Per.value) / 100;
            var profit = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Profit_Per.value) / 100;

            Expense_Market_Amt.innerHTML = Number(market).toFixed(2);
            Expense_Factory_Amt.innerHTML = Number(factory).toFixed(2);
            Expense_Other_Amt.innerHTML = Number(other).toFixed(2);
            Expense_Profit_Amt.innerHTML = Number(profit).toFixed(2);

            Expense_Total.value = Number(parseFloat(market) + parseFloat(factory) + parseFloat(other) + parseFloat(profit)).toFixed(2);
            Expense_Suggested.value = Number(parseFloat(LossSubTotalB.value) + parseFloat(Expense_Total.value)).toFixed(2);


            //Final Calculation

            var Final_Price_Unit = document.getElementById(grd + "_FinalPricePerUnittxt_" + mainid);
            var Final_Price_Profit = document.getElementById(grd + "_FinalProfit_Amttxt_" + mainid);
            var Final_Price_Profit_per = document.getElementById(grd + "_FinalProfit_Percenttxt_" + mainid);
            var Final_Price_Ltr = document.getElementById(grd + "_FinalPriceLtrKgtxt_" + mainid);
            var Final_Profit = document.getElementById(grd + "_FinalProfitAmount_Percent_Ltr_Kg_Amttxt_" + mainid);
            var Final_Profit_Per = document.getElementById(grd + "_FinalProfitAmount_Percent_Ltr_Kg_Pertxt_" + mainid);



            var ConvertLtrToGmOrMl = 0.00;

            var unipercetage = 0.00;
            
            if (parseFloat(Final_Price_Unit.value) > 0) {

                

                if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {


                    ConvertLtrToGmOrMl = 1000 / parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.innerHTML) / parseFloat(ConvertLtrToGmOrMl)).toFixed(2);

                    //alert(unipercetage + '@' + LossSubTotalB.value + '@' + Final_Price_Unit.value);

                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);

                    Final_Price_Profit.innerHTML = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.innerHTML) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);



                    Final_Profit.innerHTML = Number(parseFloat(Final_Price_Profit.innerHTML) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit_Per.value = Number(parseFloat(Final_Profit.innerHTML) * 100 / parseFloat(Final_Price_Ltr.value)).toFixed(2);
                }
                if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {

                    ConvertLtrToGmOrMl = parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.innerHTML) * parseFloat(ConvertLtrToGmOrMl)).toFixed(2);

                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) / ConvertLtrToGmOrMl).toFixed(2);

                    Final_Price_Profit.innerHTML = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.innerHTML) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);



                    Final_Profit.innerHTML = Number(parseFloat(Final_Price_Profit.innerHTML) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit_Per.value = Number(parseFloat(Final_Profit.innerHTML) * 100 / parseFloat(Final_Price_Ltr.value)).toFixed(2);
                }
                if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                    ConvertLtrToGmOrMl = parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.innerHTML)).toFixed(2);


                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);

                    Final_Price_Profit.innerHTML = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.innerHTML) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);

                    Final_Profit.innerHTML = Number(parseFloat(Final_Price_Profit.innerHTML) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit_Per.value = Number(parseFloat(Final_Profit.innerHTML) * 100 / parseFloat(Final_Price_Ltr.value)).toFixed(2);
                }


                document.getElementById(grd + "_FinalProfit_Amttxt_lbl_" + mainid).value = Final_Price_Profit.innerHTML;
                document.getElementById(grd + "_FinalPriceLtrKgtxt_lbl_" + mainid).value = Final_Price_Ltr.value;

                //alert(document.getElementById(grd + "_FinalProfitAmount_Percent_Ltr_Kg_Amttxt_lbl_" + mainid)+'@'+Final_Profit.innerHTML)

                document.getElementById(grd + "_FinalProfitAmount_Percent_Ltr_Kg_Amttxt_lbl_" + mainid).value = Final_Profit.innerHTML;
                document.getElementById(grd + "_FinalProfitAmount_Percent_Ltr_Kg_Pertxt_lbl_" + mainid).value = Final_Profit_Per.value;

                

            }

            
          

        }

    </script>

</asp:Content>
