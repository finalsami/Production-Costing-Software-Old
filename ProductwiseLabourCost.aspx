<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductwiseLabourCost.aspx.cs" Inherits="Production_Costing_Software.ProductwiseLabourCost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_ProductwiseLabourCost.ClientID %>";
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
                    <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <%-----------------------%>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img alt="" src="Content/Images/EmenLoading.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Productwise Labour Cost</h1>
                            <ol class="breadcrumb mb-4">
                                <%--<li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>--%>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Product Wise Labour Cost
                           
                                </div>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="PMRMCategoryspan">Bulk Product Name</span>


                                                <asp:DropDownList ID="BulkProductDropDownList" OnSelectedIndexChanged="BulkProductDropDownList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-text" id="ShipperSizeid">Packing Size</span>
                                                <%--<asp:TextBox ID="PackingSizetxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="PackingSizeDropDownList" OnSelectedIndexChanged="PackingSizeDropDownList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="HideUnitMeasurementDrodown" runat="server">
                                            <div class="input-group">

                                                <span class="input-group-text" id="UnitDropdown">Unit Measurement</span>

                                                <%--<asp:TextBox ID="UnitMeaurementtxt"  class="form-control" type="text" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="UnitMeaurementDropdown" OnSelectedIndexChanged="UnitMeaurementDropdown_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>



                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="Transportation">Packing Description</span>
                                                <asp:TextBox ID="PackingDescriptiontxt" AutoPostBack="true" class="form-control" type="text" TextMode="SingleLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="input-group  ">
                                                <span class="input-group-text" id="PackingNameid">Packing Style Category</span>
                                                <%--<asp:TextBox ID="PackingStyleNametxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="PackingStyleCategoryDropdown" OnSelectedIndexChanged="PackingStyleCategoryDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblPowercosting" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblTotalPowerDetails" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPackSize" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPackingStyleCategory_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>

                                        <div class="col-md-4">
                                            <div class="input-group  ">
                                                <span class="input-group-text" id="PackingStyleNameSpanId">Packing Style </span>
                                                <%--<asp:TextBox ID="PackingStyleNametxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="PackingStyleNameDropdown" Enabled="false" OnSelectedIndexChanged="PackingStyleNameDropdown_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <%--**************Add Labour*************--%>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Filling Machine Output
                           
                                    </div>
                                    <div class="card-body">
                                        <div class="row mb-4">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BulkChargeSpanId">Storck / Nosel</span>
                                                    <asp:TextBox ID="StorckPerNoseltxt" Text="0" class="form-control" OnTextChanged="StorckPerNoseltxt_TextChanged" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PouchFillingSpanId">No of Nosels/Filling Line</span>
                                                    <asp:TextBox ID="NoofNoselsFillingLinetxt" Text="0" class="form-control" OnTextChanged="NoofNoselsFillingLinetxt_TextChanged" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BottleKeepingSpanId">Total Output/Minut/Filling Line</span>
                                                    <asp:TextBox ID="TotalOutputMinutFillingLinetxt" ReadOnly="true" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row mb-3">

                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BlacklinnerPouchSpanId">Total Out Put   In Liter Or KG / Shift</span>
                                                    <asp:TextBox ID="TotalOutPutInLiterOrKGShifttxt" ReadOnly="true" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Lifting_Pouch_Bottle_WtSpanId">Total out put Bottels in Net  Shift Hours</span>
                                                    <asp:TextBox ID="TotaloutputBottelsinNetShiftHourstxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <%--*****************************************--%>

                                <%--**************Add Labour*************--%>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Total Labour Costing
                                    </div>
                                    <%--**********************************************--%>
                                    <%--*******************Power Details**********************--%>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="FILLINGSpanId">No of workers</span>
                                                    <asp:TextBox ID="NOofWorkerstxt" ReadOnly="true" Text="0" OnTextChanged="NOofWorkerstxt_TextChanged" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="CappingSpanId">Supervisor</span>
                                                    <asp:TextBox ID="Supervisortxt" OnTextChanged="Supervisortxt_TextChanged" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="InductionPowerSpanId">Total Labour & Supervisior Coasting  </span>
                                                    <asp:TextBox ID="TotalLabourSupervisiorCoastinginRStxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="InductidgdgonPowerSpanId">Rs </span>

                                                </div>
                                            </div>
                                            <asp:Label ID="lblLabourCharge" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblSuperVisorCosting" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:Label ID="lblUnloadingLtr" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblUnloadingUnit" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblUnloadingKg" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:Label ID="lblLoadingLtr" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblLoadingUnit" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblLoadingKg" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:Label ID="lblMachinaryMaitExpenceLtr" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblMachinaryMaitExpenceUnit" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblMachinaryMaitExpenceKg" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:Label ID="lblAdminExpenceLtr" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblAdminExpenceUnit" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblAdminExpenceKg" runat="server" Text="" Visible="false"></asp:Label>

                                            <asp:Label ID="lblExtraExpenceLtr" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblExtraExpenceKg" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblNet_Shift_Hour" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblExtraExpenceUnit" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Total Power Costing
                                    </div>
                                    <%--**********************************************--%>
                                    <%--*******************Power Details**********************--%>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TotalpackingStylePowerUnitsXUnitCostSpanId">Total packing Style Power Units X Unit Cost</span>
                                                    <asp:TextBox ID="TotalpackingStylePowerUnitsXUnitCosttxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="TotalpackingStylePowerUnitsXUnitCostId">/ hour</span>

                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TotalShift">Total  Power Cost  (*<i style="font-size: smaller">
                                                        <asp:Label ID="lblNetShiftHours" runat="server" Text=""></asp:Label></i>)

                                                    </span>
                                                    <asp:TextBox ID="TotalShiftPerHourtxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="TotalShiftPerHour">/ Shift</span>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Other Costing

                                    </div>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="FILLINGSpanId1">Unloading</span>
                                                    <asp:TextBox ID="Unloadingtxt" ReadOnly="true" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="CappingSpanId2">Loading</span>
                                                    <asp:TextBox ID="Loadingtxt" ReadOnly="true" Text="0" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="CappingSpanId23">Machinary Maintatnce</span>
                                                    <asp:TextBox ID="MachinaryMaintain" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="CappingSpanId42">Admin Expence</span>
                                                    <asp:TextBox ID="AdminExpencetxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ExtraExpencespanid">Extra Expence</span>
                                                    <asp:TextBox ID="ExtraExpencetxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <%--*************************************************--%>
                                </div>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Final Costing
                                    </div>
                                    <%--**********************************************--%>
                                    <%--*******************Total Costing**********************--%>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ExtraExpencesid">Total Costing</span>
                                                    <asp:TextBox ID="TotalCostingtxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ExtraExpepanid">Total Output in Liter</span>
                                                    <asp:TextBox ID="TotalOutputinLitertxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Extrpencespanid">Final per liter labour costing</span>
                                                    <asp:TextBox ID="FinalPerLiterLabourCostingtxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Additional Cost (Buffer)
                                    </div>
                                    <%--**********************************************--%>
                                    <%--*******************Total Costing**********************--%>
                                    <div class="card-body">

                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ExtraExpefsfncesid">Additional Cost (Buffer) /Ltr</span>
                                                    <asp:TextBox ID="AdditionalCostBuffertxt" Text="0" OnTextChanged="AdditionalCostBuffertxt_TextChanged" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row mb-4">
                                            </div>
                                            <div class="row mb-4">
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ExtraExpepfsfanid">Net Labour Cost/Ltr</span>
                                                    <asp:TextBox ID="NetLabourCostLtrtxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Extrpenfsfscespanid">Final Per Unit Labour Cost</span>
                                                    <asp:TextBox ID="FinalPerUnitLabourCosttxt" Text="0" ReadOnly="true" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row  justify-content-center align-items-center mt-4">

                                     <div class=" col-6 col-lg-2 col-lg-2">
                                        <div class="d-grid">
                                            <asp:Button ID="Updatebtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />
                                            <asp:Button ID="Addbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-primary btn-block" />
                                        </div>
                                    </div>


                                    <div class=" col-6 col-lg-2 col-lg-2">
                                        <div class="d-grid">
                                            <%--<asp:Button ID="Button1" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />--%>
                                            <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelBtn_Click" runat="server" Text="Cancel" class="btn btn-warning btn-block" />
                                        </div>
                                    </div>
                                </div>
                                <hr />

                                <div class="card-body mb-4 overflow-auto mt-4">
                                    <div class="input-group">
                                        <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged" placeholder="Search..." AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                        <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                        <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <asp:GridView ID="Grid_ProductwiseLabourCost" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); margin-top: 8px"
                                        PagerStyle-CssClass="gridview_pager" PageSize="200"
                                        AlternatingRowStyle-CssClass="gridview_alter" OnPageIndexChanging="Grid_ProductwiseLabourCost_PageIndexChanging" DataKeyNames="Productwise_Labor_cost_Id"
                                        runat="server" Autopostback="true"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <%--<asp:Label ID="lblPWLC_Id" runat="server" Text='<%#Eval("Productwise_Labor_cost_Id") %>'></asp:Label>--%>

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product Name" SortExpression="BPM_Product_Name" HeaderStyle-CssClass="150px" />
                                            <asp:BoundField DataField="Packing_Size" HeaderText="Packing Size" SortExpression="PackingSize" />
                                            <asp:BoundField DataField="Measurement" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Packing_Description" HeaderText="Packing-Description" SortExpression="Packing_Description" />
                                            <asp:BoundField DataField="FillingMachine" HeaderText="Filling-Machine" SortExpression="FillingMachine" />
                                            <asp:BoundField DataField="TotalOutPutInLiterOrKGShift" HeaderText="Total-Out-Put-In-LiterOrKG/Shift" SortExpression="FillingMachine" />
                                            <asp:BoundField DataField="TotalPowerCosting" HeaderText="Total-Power-Costing" SortExpression="TotalPowerCosting" />
                                            <asp:BoundField DataField="TotalLabourAndSupervisiorCoastinginRS" HeaderText="Labour-Supervisior Cost" SortExpression="TotalLabourAndSupervisiorCoastinginRS" />
                                            <asp:BoundField DataField="TotalCosting" HeaderText="Total-Costing" SortExpression="TotalCosting" />
                                            <asp:BoundField DataField="TotalOutputinLiter" HeaderText="Total-Output-in-Liter" SortExpression="TotalOutputinLiter" />
                                            <asp:BoundField DataField="FinalPerLiterLabourCosting" HeaderText="Labour Costing /Ltr" SortExpression="FinalPerLiterLabourCosting" />
                                            <asp:BoundField DataField="AdditionalCostBuffer" HeaderText="Additional-Cost-Buffer" SortExpression="AdditionalCostBuffer" />
                                            <asp:BoundField DataField="NetLabourCostLtr" HeaderText="Net-Labour-Cost/Ltr" SortExpression="NetLabourCostLtr" />
                                            <asp:BoundField DataField="FinalPerUnitLabourCost" HeaderText="Labour-Cost/Unit" SortExpression="FinalPerUnitLabourCost" />
                                            <asp:TemplateField HeaderText="Action" ControlStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:Button ID="DeleteProductBtn" OnClick="DeleteProductBtn_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                    <asp:Button ID="EditProductBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); margin-top: 3px" OnClick="EditProductBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
                                    <asp:Label ID="lblProductWisrLabourCost_Id" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </main>

        </div>
    </div>
</asp:Content>
