<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="StatewiseFinalPrice.aspx.cs" Inherits="Production_Costing_Software.StatewiseFinalPrice" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var GridId = "<%=Grid_StatewiseFinalPrice.ClientID %>";
        var ScrollHeight = 400;
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
                if (headerCellWidths[i] >= gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
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
        //Initialize tooltip with jQuery
        $(document).ready(function () {
            $('.tooltips').tooltip();
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">ipt>
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <%------------------------Label------------------------------%>
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
                <%-----------------------%>
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Statewise Final Price</h1>
                    <ol class="breadcrumb mb-4">
                    </ol>
                    <%--*************************--%>
                    <asp:Label ID="lblPacksize" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblPMRMCategory_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblpackMeasurement" runat="server" Text="" Visible="false"></asp:Label>

                    <%--******************************--%>
                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Statewise Final Price
                           
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card-body">
                                    <div class="row mb-3">

                                        <div class="col-md-3">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="StateNameDropdownspan">State Name</span>
                                                <asp:DropDownList ID="StateNameDropdown" OnSelectedIndexChanged="StateNameDropdown_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-5" runat="server">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="BulkProductDropDown">BulkProduct</span>
                                                <asp:DropDownList ID="BulkProductDropDownList" OnSelectedIndexChanged="BulkProductDropDownList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="BulkProdProductTradeuctDropDown">ProductTradeName</span>
                                                <asp:DropDownList ID="ProductTradeNameDropDown" Enabled="false" OnSelectedIndexChanged="ProductTradeNameDropDown_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>


                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="PackingSizeDropDownList" Visible="false" OnSelectedIndexChanged="PackingSizeDropDownList_SelectedIndexChanged" Enabled="false" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3" runat="server" id="HidePackMeasurementDropDownList">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="PackMeasurementDropDownList" Visible="false" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="PackingSize"></label>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4" runat="server">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="StateNameDropdopan">Master Pack</span>
                                                <asp:TextBox ID="MasterPackSizetxt" ReadOnly="true" class="form-control" runat="server" placeholder="Master Pack"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>

                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NRVspANID">NRV</span>
                                                <asp:TextBox ID="NRVtxt" ReadOnly="true" class="form-control" runat="server" type="number" placeholder="NRV"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="PackingStyle">Packing Style</span>
                                                <asp:DropDownList ID="PackingStyleDropDownList" Enabled="false" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>

                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="Transportspan">Transport</span>
                                                <asp:TextBox ID="Transporttxt" ReadOnly="true" class="form-control" runat="server" type="number" placeholder="Transport"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="ProductCategory">ProductCategory</span>
                                                <asp:DropDownList ID="ProductCategoryDropDownList" Enabled="false" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="FinalNRVspan">FinalNRV</span>
                                                <asp:TextBox ID="FinalNRVtxt" ReadOnly="true" class="form-control" runat="server" type="number" placeholder="FinalNRV"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="card-body col-md-5">
                                            <div class="card-header">
                                                RPL Pricing
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="card-body col-md-5">
                                            <div class="card-header ">
                                                <i class="fas fa-table me-1"></i>
                                                RPL To NCR
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3 ">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="RPL_ApproProfit">RPL Approx Profit (%)</span>
                                                <asp:TextBox ID="RPL_ApproProfittxt" AutoPostBack="true" OnTextChanged="RPL_ApproProfittxt_TextChanged" Text="0" class="form-control" runat="server" type="number" Style="background-color: yellow;"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="RPL_ApproProfittxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ApproProfit !"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="Diff_RPL_To_NCRtxtspn">Diffrence between RPL To NCR (%)</span>
                                                <asp:TextBox ID="Diff_RPL_To_NCRtxt" Style="background-color: yellow;" Text="0" OnTextChanged="Diff_RPL_To_NCRtxt_TextChanged" AutoPostBack="true" class="form-control" runat="server" type="number"></asp:TextBox>

                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3 ">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="RPL_ProfitAmt">RPL Profit Amt</span>
                                                <asp:TextBox ID="RPL_ProfitAmttxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="offset-1" ControlToValidate="RPL_ProfitAmttxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ProfitAmt !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="   RPTtoNCR DiffAmtSpan">RPT to NCR Diff Amt</span>
                                                <asp:TextBox ID="SuggestedPriceNCRtxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>

                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="SuggestedRPL_Price">Suggested RPL Price</span>
                                                <asp:TextBox ID="SuggestedRPL_Pricetxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="offset-1" ControlToValidate="SuggestedRPL_Pricetxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ProfitAmt !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRPriceidspan">NCR Price</span>
                                                <asp:TextBox ID="NCRPricetxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="offset-1" ControlToValidate="NCRPricetxt" runat="server" ForeColor="Red" ErrorMessage="* NCR Price !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="card-body col-md-5">
                                            <div class="card-header">
                                                <i class="fas fa-table me-1"></i>
                                                RPL Expenses
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="card-body col-md-5">
                                            <div class="card-header">
                                                <i class="fas fa-table me-1"></i>
                                                NCR Expenses
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="STAFFEXPENCEspan">Staff Expence (<asp:Label ID="lblStaffExpence" runat="server" Text=""></asp:Label>%)</label></span>
                                                <asp:TextBox ID="STAFFEXPENCEtxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRStaffExpencetxtSpan">NCR Staff Expence  (<asp:Label ID="lblNCRStaffExpence" runat="server" Text=""></asp:Label>%)</span>
                                                <asp:TextBox ID="NCRStaffExpencetxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="DEPO EXPENCEspan">Depo Expence  (<asp:Label ID="lblDepotExpence" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="DEPOEXPENCEtxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="offset-1" ControlToValidate="RPL_ProfitAmttxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ProfitAmt !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRDEPOEXPENCEspan">NCR Depo Expence    (<asp:Label ID="lblNCRDepoExpence" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="NCRDepoExpencetxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="offset-1" ControlToValidate="RPL_ProfitAmttxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ProfitAmt !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="INCENTIVEspan">INCENTIVE  (<asp:Label ID="lblIncentive" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="INCENTIVEtxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass="offset-1" ControlToValidate="SuggestedRPL_Pricetxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ProfitAmt !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRINCENTIVEspan">NCR INCENTIVE    (<asp:Label ID="lblNCRIncentive" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="NCRIncentivetxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="offset-1" ControlToValidate="SuggestedRPL_Pricetxt" runat="server" ForeColor="Red" ErrorMessage="* RPL ProfitAmt !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="MARKETINGspan">MARKETING  (<asp:Label ID="lblMarketing" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="MARKETINGTXT" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRMarketing">NCR MARKETING  (<asp:Label ID="lblNCRMarketing" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="NCRMarketingtxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="INTERESTspan">INTEREST   (<asp:Label ID="lblInterest" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="INTERESTTXT" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRInterest">NCR INTEREST   (<asp:Label ID="lblNCRInterest" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="NCRInteresttxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="Otherspan">Other   (<asp:Label ID="lblRPLOther" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="RPLOthertxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCROther">NCR Other   (<asp:Label ID="lblNCROther" runat="server" Text=""></asp:Label>%)
                                                </span>
                                                <asp:TextBox ID="NCROthertxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="TOTALspan">TOTAL
                                                </span>
                                                <asp:TextBox ID="TOTALTXT" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRTotal">NCR TOTAL
                                                </span>
                                                <asp:TextBox ID="NCRTotaltxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="card-body col-md-5">
                                            <div class="card-header">
                                                <i class="fas fa-table me-1"></i>
                                                RPL PD Scheme Working
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>

                                        <div class="card-body col-md-5">
                                            <div class="card-header ">
                                                <i class="fas fa-table me-1"></i>
                                                NCR PD Scheme Working
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="PD/Schemespan">RPL PD/Scheme</span>
                                                <asp:TextBox ID="PD_Schemetxt" Style="background-color: yellow;" AutoPostBack="true" OnTextChanged="PD_Schemetxt_TextChanged" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="offset-1" ControlToValidate="PD_Schemetxt" runat="server" ForeColor="Red" ErrorMessage="* PD_Scheme !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRPD_Schemespan">NCR PD/Scheme</span>
                                                <asp:TextBox ID="NCRPD_Schemetxt" Style="background-color: yellow;" AutoPostBack="true" OnTextChanged="NCRPD_Schemetxt_TextChanged" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="offset-1" ControlToValidate="NCRPD_Schemetxt" runat="server" ForeColor="Red" ErrorMessage="*NCR PD_Scheme !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="Addi.PDspan">RPL Addi. PD</span>
                                                <asp:TextBox ID="AddiPDtxt" Style="background-color: yellow;" OnTextChanged="AddiPDtxt_TextChanged" AutoPostBack="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="offset-1" ControlToValidate="AddiPDtxt" runat="server" ForeColor="Red" ErrorMessage="* AddiPD !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRAddiPDpan">NCR Addi. PD</span>
                                                <asp:TextBox ID="NCRAddiPDtxt" Style="background-color: yellow;" AutoPostBack="true" OnTextChanged="NCRAddiPDtxt_TextChanged" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="offset-1" ControlToValidate="NCRAddiPDtxt" runat="server" ForeColor="Red" ErrorMessage="*NCRAddi PD_Scheme !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="TotalPDspan">RPL Total PD</span>
                                                <asp:TextBox ID="TotalPDtxt" ReadOnly="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="NCRTotalPDspan">NCR Total PD</span>
                                                <asp:TextBox ID="NCRTotalPDtxt" ReadOnly="true" AutoPostBack="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" CssClass="offset-1" ControlToValidate="NCRTotalPDtxt" runat="server" ForeColor="Red" ErrorMessage="*NCRTotalPD !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="FinalRPLPriceWithPD">Final RPL Price (with PD)</span>
                                                <asp:TextBox ID="FinalRPLPriceWithPDtxt" ReadOnly="true" AutoPostBack="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" CssClass="offset-1" ControlToValidate="FinalRPLPriceWithPDtxt" runat="server" ForeColor="Red" ErrorMessage="* AddiPD !"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="FinalNRCPriceWithPD">Final NCR Price</span>
                                                <asp:TextBox ID="FinalNRCPriceWithPDtxt" ReadOnly="true" AutoPostBack="true" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" CssClass="offset-1" ControlToValidate="FinalNRCPriceWithPDtxt" runat="server" ForeColor="Red" ErrorMessage="*NCRTotalPD !"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="card-header col-md-5">
                                            <i class="fas fa-table me-1"></i>
                                            RPL Profit Working
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="card-header col-md-5">
                                            <i class="fas fa-table me-1"></i>
                                            NCR  Profit Working
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="GrossProfitinRSspan">RPL Gross Profit in RS</span>
                                                <asp:TextBox ID="GrossProfitRStxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="NCRGrossProfitSspan">NCR Gross Profit in RS</span>
                                                <asp:TextBox ID="NCRGrossProfitRStxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="PerGrossProfitspan">RPL  % oF gross Profit</span>
                                                <asp:TextBox ID="PerGrossProfittxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="BCRPerGrossProfitspan">NCR  % oF gross Profit</span>
                                                <asp:TextBox ID="NCRPerGrossProfittxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="NetProfitRStxtspan">RPL Net Profit in RS</span>
                                                <asp:TextBox ID="NetProfitRStxt" ReadOnly="true" Text="0" class="form-control " runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="NCRNetProfitRStxtspan">NCR Net Profit in RS</span>
                                                <asp:TextBox ID="NCRNetProfitRStxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="NetProfitRPLPertxtspan">Net profit RPL %</span>
                                                <asp:TextBox ID="NetProfitRPLPertxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <span class="input-group-text" id="NCRNetProfitRPLPertxtspan">Net profit NCR %</span>
                                                <asp:TextBox ID="NCRNetProfitRPLPertxt" ReadOnly="true" Text="0" class="form-control " Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" type="number"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <asp:Label ID="lblExpenceType_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPriceType_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblState_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblTradeName_Id" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblProductCategory_Id" Visible="false" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblBPM_Id" Visible="false" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="lblStatewiseFinalPrice_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblStatewiseCostFactors_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <div class="row  justify-content-center align-items-center mt-4">

                                    <div class=" col-10 col-lg-2 col-lg-4">
                                        <div class="d-grid">
                                            <asp:Button ID="AddStatewiseFinalPrice" OnClick="AddStatewiseFinalPrice_Click" CssClass="btn btn-primary btn-block" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                runat="server" Text="Add" />
                                            <asp:Button ID="UpdateStatewiseFinalPrice" OnClick="UpdateStatewiseFinalPrice_Click" CssClass="btn btn-info btn-block" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Visible="false" runat="server" Text="Update" />
                                        </div>
                                    </div>
                                    <div class=" col-10 col-lg-2 col-lg-4">
                                        <div class="d-grid">
                                            <asp:Button ID="CancelStatewiseFinalPrice" OnClick="CancelStatewiseFinalPrice_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                        </div>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--*****************************--%>

                        <div class="row mb-4 mt-3" style="margin-left: 10px">

                            <div class="col-md-3">
                                <div class="input-group mb-3">
                                    <asp:DropDownList ID="StateWiseGridDropdownList" AutoPostBack="true" OnSelectedIndexChanged="StateWiseGridDropdownList_SelectedIndexChanged" class="form-select" runat="server">
                                    </asp:DropDownList>
                                    <%--  <label for="StateName">State Name</label>--%>
                                </div>
                            </div>

                            <div class="col-md-5" style="border: 0.5px;">
                                <div class="input-group">

                                    <asp:ListBox ID="BulkProductListbox" style="height:200px" runat="server" SelectionMode="Multiple" class="form-control"></asp:ListBox>

                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="input-group ">
                                    <asp:DropDownList ID="MasterPackAllSizeDropdown" OnSelectedIndexChanged="MasterPackAllSizeDropdown_SelectedIndexChanged" class="form-select" runat="server">
                                        <asp:ListItem Selected="True" Value="1">MasterPack</asp:ListItem>
                                        <asp:ListItem Value="0">AllSize</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="ChkBulkSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="ChkBulkSubmit_Click1" />
                            </div>
                        </div>
                        <hr />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>


                                <div class="row mb-3 offset-3">
                                    <div class=" col-10 col-lg-2 col-lg-4">
                                        <div class="d-grid">
                                            <asp:Button ID="PdfReport" OnClick="PdfReport_Click" CssClass="btn btn-outline-danger btn-block" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                runat="server" Text="Pdf Report" />
                                        </div>
                                    </div>
                                    <div class=" col-10 col-lg-2 col-lg-4">
                                        <div class="d-grid">
                                            <asp:Button ID="ExcelReport" OnClick="ExcelReport_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-outline-dark btn-block" runat="server" Text="Excel Report" />
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body  overflow-auto ">

                                    <asp:GridView ID="Grid_StatewiseFinalPrice"
                                        CssClass=" table-hover table-responsive gridview overflow-auto"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter"
                                        runat="server" AutoGenerateColumns="False" DataKeyNames="StateWiseFinalPrice_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StateName" HeaderText="State" SortExpression="State" />
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk-Product-Name" SortExpression="BPM_Product_Name" />

                                            <asp:BoundField DataField="TradeName" HeaderText="Trade" SortExpression="Trade" />
                                            <%--<asp:BoundField DataField="PackingSize" HeaderText="Packing Size" SortExpression="PackingSize" />--%>
                                            <asp:TemplateField HeaderText="Packing Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("UnitPackingSize") %>'></asp:Label>
                                                    <%--<asp:Label ID="lblfinalNRV" runat="server" Text='<%#Eval("finalNRV") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PackingStyleName" HeaderText="Packing  Style" SortExpression="PackingStyleName" />
                                            <asp:BoundField DataField="ProductCategoryName" HeaderText="Product Category" SortExpression="ProductCategoryName" />
                                            <asp:TemplateField HeaderText="Final NRV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfinalNRV1" runat="server" Text='<%#Eval("finalNRV") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rNRV:--------------------->{0}" +
                                                                                                                            "\rTransport:---------------->{1}",
                                                                                                                            Eval("NRV"),
                                                                                                                            Eval("Transport")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total PD">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal_PD" runat="server" Text='<%#Eval("Total_PD") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rPD_Scheme:----------->{0}" +
                                                                                                                            "\rAdd_PD:---------------->{1}",
                                                                                                                            Eval("PD_Scheme"),
                                                                                                                            Eval("Add_PD")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Suggested-RPL-Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSuggested_RPL_Price" runat="server" Text='<%#Eval("Suggested_RPL_Price") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rfinalNRV:----------->{0}" +
                                                                                                                            "\rRPL_Profit_Amt:--------------->{1}",
                                                                                                                            Eval("finalNRV"),
                                                                                                                            Eval("RPL_Profit_Amt")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total RPL Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TotalExpence") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rStaffExpence:------------->{0}" +
                                                                                                                            "\rDepotExpence:----------->{1}"+
                                                                                                                             "\rIncentive:----------------->{2}"+
                                                                                                                              "\rMarketing:---------------->{3}"+
                                                                                                                                "\rIntrest:-------------------->{4}"+
                                                                                                                                "\rOtherExpence:------------->{5}",
                                                                                                                            Eval("StaffExpence"),
                                                                                                                            Eval("DepotExpence"),
                                                                                                                             Eval("Incentive"),
                                                                                                                              Eval("Marketing"),
                                                                                                                               Eval("Intrest"),
                                                                                                                               Eval("OtherExpence")
                                                                                                                            ) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Profit RPL(%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetProfitRPLPer" runat="server" Text='<%#Eval("RPLNetProfitPer") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rGrossProfitRs:------------->{0}" +
                                                                                                                            "\rPerGrossProfit:------------>{1}"+
                                                                                                                             "\rNetProfitRs:--------------->{2}",
                                                                                                                            Eval("GrossProfitRs"),
                                                                                                                            Eval("PerGrossProfit"),
                                                                                                                             Eval("RPLNetProfitAmount")
                                                                                                                             
                                                                                                                            ) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--   <asp:BoundField DataField="RPL_Approx_Profit" HeaderText="RPL-Approx-Profit" SortExpression="RPL_Approx_Profit" />
                                                <asp:BoundField DataField="RPL_Profit_Amt" HeaderText="RPL-Profit-Amt" SortExpression="RPL_Profit_Amt" />
                                                <asp:BoundField DataField="Suggested_RPL_Price" HeaderText="Suggested-RPL-Price" SortExpression="NRV" />--%>

                                            <asp:BoundField DataField="NRV" HeaderText="NRV" Visible="false" SortExpression="NRV" />
                                            <asp:BoundField DataField="Transport" HeaderText="Transport" Visible="false" SortExpression="Transport" />
                                            <asp:BoundField DataField="finalNRV" HeaderText="Final-NRV" Visible="false" SortExpression="finalNRV" />
                                            <asp:BoundField DataField="PD_Scheme" HeaderText="PD_Scheme" Visible="false" SortExpression="PD_Scheme" />
                                            <asp:BoundField DataField="Add_PD" HeaderText="Add_PD" Visible="false" SortExpression="Add_PD" />
                                            <asp:BoundField DataField="Total_PD" HeaderText="Total_PD" Visible="false" SortExpression="Total_PD" />
                                            <asp:BoundField DataField="RPL_Approx_Profit" HeaderText="RPL_Approx_Profit" Visible="false" SortExpression="Total_PD" />
                                            <asp:BoundField DataField="RPL_Profit_Amt" HeaderText="RPL_Profit_Amt" Visible="false" SortExpression="Total_PD" />
                                            <asp:BoundField DataField="Suggested_RPL_Price" HeaderText="Suggested_RPL_Price" Visible="false" SortExpression="Total_PD" />

                                            <asp:BoundField DataField="StaffExpence" HeaderText="StaffExpence" Visible="false" SortExpression="StaffExpence" />
                                            <asp:BoundField DataField="DepotExpence" HeaderText="DepotExpence" Visible="false" SortExpression="DepotExpence" />
                                            <asp:BoundField DataField="Incentive" HeaderText="Incentive" Visible="false" SortExpression="Incentive" />
                                            <asp:BoundField DataField="Marketing" HeaderText="Marketing" Visible="false" SortExpression="Marketing" />
                                            <asp:BoundField DataField="Intrest" HeaderText="Intrest" Visible="false" SortExpression="Intrest" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" Visible="false" SortExpression="Total" />

                                            <asp:BoundField DataField="Diff_RPL_NCR" HeaderText="Diffenrence-RPL-NCR" SortExpression="Diff_RPL_NCR" />
                                            <%--<asp:BoundField DataField="Suggest_Price_NCR" HeaderText="Suggest-Price-NCR" SortExpression="Suggest_Price_NCR" />--%>
                                            <asp:TemplateField HeaderText="NCR Total Expence">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNCR_Total" runat="server" Text='<%#Eval("NCR_Total") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rNCR_StaffExpence:------------->{0}" +
                                                                                                                            "\rNCR_DepotExpence:----------->{1}"+
                                                                                                                             "\rNCR_Incentive:------------------>{2}"+
                                                                                                                              "\rNCR_Intrest:-------------------->{3}"+
                                                                                                                               "\rNCR_Marketing:----------------->{4}"+
                                                                                                                               "\rNCR_Other:----------->{5}",
                                                                                                                            Eval("NCR_StaffExpence"),
                                                                                                                            Eval("NCR_DepotExpence"),
                                                                                                                             Eval("NCR_Incentive"),
                                                                                                                              Eval("NCR_Intrest"),
                                                                                                                               Eval("NCR_Marketing"),
                                                                                                                               Eval("NCR_Other")
                                                                                                                             
                                                                                                                            ) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--                               <asp:BoundField DataField="NCR_StaffExpence" HeaderText="NCR_StaffExpence" Visible="false" SortExpression="Total" />
                                                <asp:BoundField DataField="NCR_DepotExpence" HeaderText="NCR_DepotExpence" Visible="false" SortExpression="Total" />
                                                <asp:BoundField DataField="NCR_Incentive" HeaderText="NCR_Incentive" Visible="false" SortExpression="Total" />
                                                <asp:BoundField DataField="NCR_Marketing" HeaderText="NCR_Marketing" Visible="false" SortExpression="Total" />
                                                <asp:BoundField DataField="NCR_Intrest" HeaderText="NCR_Intrest" Visible="false" SortExpression="Total" />
                                                <asp:BoundField DataField="NCR_Total" HeaderText="NCR_Intrest" Visible="false" SortExpression="Total" />--%>

                                            <asp:TemplateField HeaderText="NCR-Net-Profit-RPL(%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNCR_NetProfitRPLPer" runat="server" Text='<%#Eval("NCR_NetProfitPer") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("\rNCR_GrossProfitRs:------------>{0}" +
                                                                                                                            "\rNCR_PerGrossProfit:----------->{1}"+
                                                                                                                             "\rNCR_NetProfitRs:-------------->{2}",
                                                                                                                            Eval("NCR_GrossProfitRs"),
                                                                                                                            Eval("NCR_PerGrossProfit"),
                                                                                                                             Eval("NCR_NetProfitRs")
                                                                                                                             
                                                                                                                            ) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditFinalPrice" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="EditFinalPrice_Click" Text="Edit" Width="80px" runat="server" class="btn btn-success btn-sm" />

                                                    <asp:Button ID="DelFinalPrice" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="DelFinalPrice_Click" Text="Delete" Width="80px" runat="server" class="btn btn-danger btn-sm mt-1" />
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ExcelReport" />
                                <asp:PostBackTrigger ControlID="PdfReport" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>
                    <%--*****************************--%>
                </div>
            </main>
        </div>
    </div>
</asp:Content>
