<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="comp_CompanywiseFactoryExpense.aspx.cs" Inherits="Production_Costing_Software.comp_CompanywiseFactoryExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <p>
        <span id="ShipperSizeSpanid0" class="input-group-text" style="box-sizing: border-box; display: flex; align-items: center; padding: 0.375rem 0.75rem; font-size: 16px; font-weight: 400; line-height: 1.5; color: rgb(33, 37, 41); text-align: center; white-space: nowrap; background-color: rgb(233, 236, 239); border: 1px solid rgb(206, 212, 218); border-radius: 0.25rem 0px 0px 0.25rem; font-family: Arial; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; orphans: 2; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">Cost/Ltr</span><br class="Apple-interchange-newline" />
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var GridId = "<%=Grid_CompanyFactoryExpence.ClientID %>";
        var ScrollHeight = 400;
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

    </script>


    <style>
        .card {
            background-color: #fff;
            border-radius: 10px;
            border: none;
            position: relative;
            margin-bottom: 30px;
            box-shadow: 0 0.46875rem 2.1875rem rgba(90,97,105,0.1), 0 0.9375rem 1.40625rem rgba(90,97,105,0.1), 0 0.25rem 0.53125rem rgba(90,97,105,0.12), 0 0.125rem 0.1875rem rgba(90,97,105,0.1);
        }

        .l-bg-cherry {
            background: linear-gradient(to right, #493240, #f09) !important;
            color: #fff;
        }

        .l-bg-blue-dark {
            background: linear-gradient(to right, #373b44, #4286f4) !important;
            color: #fff;
        }

        .l-bg-green-dark {
            background: linear-gradient(to right, #0a504a, #38ef7d) !important;
            color: #fff;
        }

        .l-bg-orange-dark {
            background: linear-gradient(to right, #a86008, #ffba56) !important;
            color: #fff;
        }

        .card .card-statistic-3 .card-icon-large .fas, .card .card-statistic-3 .card-icon-large .far, .card .card-statistic-3 .card-icon-large .fab, .card .card-statistic-3 .card-icon-large .fal {
            font-size: 110px;
        }

        .card .card-statistic-3 .card-icon {
            text-align: center;
            line-height: 50px;
            margin-left: 15px;
            color: #000;
            position: absolute;
            right: -5px;
            top: 20px;
            opacity: 0.1;
        }

        .l-bg-cyan {
            background: linear-gradient(135deg, #289cf5, #84c0ec) !important;
            color: #fff;
        }

        .l-bg-green {
            background: linear-gradient(135deg, #23bdb8 0%, #43e794 100%) !important;
            color: #fff;
        }

        .l-bg-orange {
            background: linear-gradient(to right, #f9900e, #ffba56) !important;
            color: #fff;
        }

        .l-bg-cyan {
            background: linear-gradient(135deg, #289cf5, #84c0ec) !important;
            color: #fff;
        }
    </style>
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
                <%--Lables--%>
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
                </div>
                <%----------Label for Suggested CWFE-------------%>
                <asp:Label ID="lblCWFE_TotalCostPerLtr" runat="server" Text="" Visible="false"></asp:Label>

                <asp:Label ID="lblCWFE_FctryExp_Per" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_FctryExp_Amt" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_Other_Per" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_Profit_Per" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_Other_Amt" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_Mrkt_Per" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_Mrkt_Amt" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblFk_PMRM_Catgeory_Id" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_TotalExp" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_Profit_Amt" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCWFE_FFCostPerLtr" runat="server" Text="" Visible="false"></asp:Label>

                <%----------------------------------%>
                <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                    <h1 class="mt-4">Co.Factory Expence</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="comp_CompanyPriceListMaster.aspx">Co.Factory Expence  </a></li>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Co.Factory Expence                           
                        </div>
                        <asp:Label ID="lblCompanyFactoryExpence_Id" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>


                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <div class="modalLoading">
                                            <div class="centerLoading">
                                                <img src="Content/LogoImage/gifntext-gif.gif" />
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <div class="card-body">


                                    <asp:Label ID="lblPAckSize" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>

                                    <div class="row mb-3">
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <span class="input-group-text" id="UnitDropdown">Bulk Product</span>
                                                <asp:DropDownList ID="BulkProductDropdownlist" OnSelectedIndexChanged="BulkProductDropdownlist_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select multiple " runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="input-group">
                                                <span class="input-group-text" id="ShipperSizeSpanid">Cost/Ltr</span>
                                                <asp:TextBox ID="CostPerLtrtxt" ReadOnly="true" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <span class="input-group-text" id="Shd">Master Pack:  :
                                                    <asp:Label ID="lblBulkProductMasterPack" Style="font-weight: bolder" runat="server" Text=""></asp:Label>
                                                </span>

                                            </div>
                                        </div>

                                    </div>


                                    <div class="card mb-4">
                                        <div class="card-header">
                                            <i class="fas fa-table me-1"></i>
                                            Additional Cost on Cost/Liter or KG:                         
                                        </div>
                                        <div class="row mb-3 mt-4 offset-2">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Fact1">Factory Expense (%)</span>
                                                    <asp:TextBox ID="FactoryExpencePertxt" OnTextChanged="FactoryExpencePertxt_TextChanged" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Fact12">Factory Expense  Amount</span>
                                                    <asp:TextBox ID="FactoryExpenceAmounttxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 offset-2">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="masrk">Marketted By Charges (%)</span>
                                                    <asp:TextBox ID="MarketedByChrgtxt" OnTextChanged="MarketedByChrgtxt_TextChanged" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="masggrk">Marketted By Charges Amount</span>
                                                    <asp:TextBox ID="MarketedByChrgAmounttxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 offset-2">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="other">Other (%)</span>
                                                    <asp:TextBox ID="OtherPertxt" OnTextChanged="OtherPertxt_TextChanged" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="othedfr">Other Amount</span>
                                                    <asp:TextBox ID="OtherPerAmounttxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 ">

                                            <div class="col-md-3 offset-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Faddfct">Total Expence</span>
                                                    <asp:TextBox ID="TotalExpencetxt" ReadOnly="true" OnTextChanged="TotalExpencetxt_TextChanged" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 offset-2">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Facdft">Profit (%)</span>
                                                    <asp:TextBox ID="ProfitPertxt" OnTextChanged="ProfitPertxt_TextChanged" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Fadfct">Profit Amount</span>
                                                    <asp:TextBox ID="ProfitPerAmounttxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 offset-2">
                                            <div class="col-md-9">

                                                <%--<span class="input-group-text" id="total">Total Cost/Liter</span>--%>
                                                <%--<asp:TextBox ID="TotalCostPerLtrtxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>

                                                <div class="card l-bg-orange-dark">
                                                    <div class="card-statistic-3 p-4">
                                                        <div class="card-icon card-icon-large"><i class="fas fa-rupee-sign"></i></div>
                                                        <div class="mb-4">
                                                            <h5 class="card-title mb-0 font-monospace">Total Cost/Liter</h5>
                                                        </div>
                                                        <div class="row align-items-center mb-2 d-flex">
                                                            <div class="col-8">
                                                                <h2 class="d-flex align-items-center mb-0">
                                                                    <asp:Label ID="TotalCostPerLtrtxt" Style="font-size: xx-large" CssClass="font-monospace" runat="server" Text=""></asp:Label>
                                                                </h2>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row  justify-content-center align-items-center " style="margin-bottom: 50px">

                                            <div class=" col-10 col-md-3 col-md-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="ReportView" OnClick="ReportView_Click" data-bs-toggle="modal" data-bs-target="#ReportAll" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-secondary btn-block" runat="server" Text="View Final Factory Costing " />

                                                </div>
                                            </div>

                                            <div class=" col-10 col-lg-2 col-md-4">
                                                <div class="d-grid">

                                                    <asp:Button ID="AddComp_FactoryExpenceBtn" OnClick="AddComp_FactoryExpenceBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                                    <asp:Button ID="UpdateComp_FactoryExpence" OnClick="UpdateComp_FactoryExpence_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-info btn-block" Visible="false" runat="server" Text="Update" />

                                                </div>

                                            </div>

                                            <div class=" col-10 col-lg-2 col-md-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="CancelComp_FactoryExpence" OnClick="CancelComp_FactoryExpence_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                </div>
                                <div class="card-body" style="overflow: auto">

                                    <asp:GridView ID="Grid_CompanyFactoryExpence" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager" OnRowDataBound="Grid_CompanyFactoryExpence_RowDataBound" OnRowCommand="Grid_CompanyFactoryExpence_RowCommand"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="comp_CompanyFactoryExpence_Id">
                                        <Columns>

                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" />
                                            <%--<asp:BoundField DataField="TotalAmtPerLiter" HeaderText="Total Cost / Ltr" SortExpression="Source" />--%>
                                            <asp:TemplateField HeaderText="Total Cost / Ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmtPerLiter" runat="server" Text='<%#Eval("TotalAmtPerLiter") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Factory Expence Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation" runat="server" Text='<%#Eval("FectoryExpenceAmount") %>'></asp:Label>
                                                    (<%# Eval("FectoryExpencePer")%>%)
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Marketed By Charges Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation1" runat="server" Text='<%#Eval("MarketedByChargesAmount") %>'></asp:Label>
                                                    (<%# Eval("MarketedByChargesPer")%>%)
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Other Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation3" runat="server" Text='<%#Eval("OtherPerAmount") %>'></asp:Label>
                                                    (<%# Eval("OtherPer")%>%)
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Profit Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Formulation4" runat="server" Text='<%#Eval("ProfitAmount") %>'></asp:Label>
                                                    (<%# Eval("ProfitPer")%>%)
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TotalExpenceAmount" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalExpenceAmount" runat="server" Text='<%#Eval("TotalExpenceAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FinalFactoryCostLiter">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinalFactoryCostLiter" runat="server" Text='<%#Eval("FinalFactoryCostLiter") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="FinalFactoryCostLiter" HeaderText="Final Factory Cost/Liter" SortExpression="Source" />--%>
                                            <asp:TemplateField HeaderText="SuggestedFactory Cost/Liter">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="SuggestedCostPerLtrtxt" OnTextChanged="SuggestedCostPerLtrtxt_TextChanged" Text="0.00" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Suggested Net Profit Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetProfitAmount" runat="server"></asp:Label>
                                                    (<%# Eval("ProfitPer")%>%)
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="GridAddCompanyFectoryExpenceBtn" CommandName="Add" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="GridAddCompanyFectoryExpenceBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 55px" ValidationGroup="AddBPM" Text="Add" runat="server" class="btn btn-success btn-sm disabled" />

                                                    <asp:Button ID="GridEditCompanyFectoryExpenceBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="GridEditCompanyFectoryExpenceBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 55px" ValidationGroup="EditBPM" Text="Edit" runat="server" class="btn btn-info btn-sm mt-1" />
                                                    <asp:Button ID="GridDelCompanyFectoryExpenceBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 55px" OnClick="GridDelCompanyFectoryExpenceBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm mt-1" />
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
</asp:Content>
