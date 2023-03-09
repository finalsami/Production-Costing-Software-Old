<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PackingMaterialMaster.aspx.cs" Inherits="Production_Costing_Software.PackingMaterialMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_PackingMaterialMaster.ClientID %>";
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
    <script>
        // The function below will start the confirmation dialog
        function confirmAction() {
            let confirmAction = confirm("Are you sure to execute this action?");
            //if (confirmAction) {
            //    alert("Action successfully executed");
            //} else {
            //    alert("Action canceled");
            //}
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
                    <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <%-----------------------%>

                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Product Packing Material Master</h1>
                    <ol class="breadcrumb mb-4">
                        <%--<li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>--%>
                    </ol>

                    <div class="card mb-4 lds-roller">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Product Packing Material Master
                           
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="PMRMCategoryDropdownListid">BulkProduct</span>

                                                <asp:DropDownList ID="BulkProductDropDownList" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="PackingNameid">Packing Name</span>
                                                <asp:TextBox ID="PackingNametxt" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="PackingSizeSpanId">Packing Size</span>
                                                <asp:TextBox ID="PackingSizetxt" OnTextChanged="PackingSizetxt_TextChanged" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group  " id="HideunitMeasurement" runat="server">
                                                <span class="input-group-text" id="PackingMeasurementSpanId">Packing Measurement</span>
                                                <asp:DropDownList ID="PackingMeasurementDropdown" OnSelectedIndexChanged="PackingMeasurementDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="ShipperSizeSpanid">Shipper Size</span>
                                                <asp:TextBox ID="ShipperSizetxt" class="form-control" OnTextChanged="ShipperSizetxt_TextChanged" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="UnitDropdown">Unit Measurement</span>
                                                <asp:DropDownList ID="UnitMeaurementDropdown" OnSelectedIndexChanged="UnitMeaurementDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="Transportation">Unit/Shipper</span>
                                                <asp:TextBox ID="UnitShippertxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group-text">
                                                <asp:CheckBox ID="ChkShipperType" OnCheckedChanged="ChkShipperType_CheckedChanged" Checked="false" AutoPostBack="true" CssClass="form-check-inline" runat="server" />

                                                <span class="input-group" id="ShipperTypeDropdownSpanId">Shipper Type</span>
                                                <asp:DropDownList ID="ShipperTypeDropdown" OnSelectedIndexChanged="ShipperTypeDropdown_SelectedIndexChanged" AutoPostBack="true" Enabled="false" CssClass="form-select" runat="server">
                                                </asp:DropDownList>

                                            </div>

                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group-text mb-3">
                                                    <asp:CheckBox ID="ChkIsPackingMaster" OnCheckedChanged="ChkIsPckingMaster_CheckedChanged" AutoPostBack="true" Checked="false" CssClass="form-check-inline" runat="server" />

                                                    <div class=" disabled">
                                                        <span class="input-group-text" id="PackingLossPer">is Master Packing?</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="Updatebtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />
                                                <asp:Button ID="Addbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-primary btn-block" />
                                            </div>
                                        </div>
                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="Cancel" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Cancel_Click" runat="server" Text="Cancel" class="btn btn-warning btn-block" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="lblPM_RM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblPM_RM_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblIsMasterPacking" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblPackSize" runat="server" Text="" Visible="false"></asp:Label>

                            <div class="card-body mb-4 mt-4">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row mb-3">
                                            <div class="col-md-2">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="FilterIsMasterPackingDropdown" OnSelectedIndexChanged="FilterIsMasterPackingDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                        <asp:ListItem Value="1">Master Packing</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged" placeholder="Search..." AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                                    <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                                    <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="Grid_PackingMaterialMaster" OnRowDataBound="Grid_PackingMaterialMaster_RowDataBound"
                                            CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            AlternatingRowStyle-CssClass="gridview_alter" DataKeyNames="Pack_Material_Id" runat="server" Autopostback="true"
                                            AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM_Product_Name" SortExpression="BPM_Product_Name" />
                                                <%--<asp:BoundField DataField="PM_RM_Category_Name" HeaderText="PM_RM_Category_Name" SortExpression="PM_RM_Category_Name" />--%>
                                                <asp:BoundField DataField="Pack_Name" HeaderText="Pack Name" SortExpression="PM_RM_Name" />
                                                <asp:BoundField DataField="Pack_size" HeaderText="Pack Size" SortExpression="PMRM_Price_Cost_Unit" />
                                                <asp:BoundField DataField="PackMeasurement" HeaderText=" Pack Measurement" SortExpression="Final_Pack_Cost_Unit" />
                                                <asp:BoundField DataField="Pack_ShipperSize" HeaderText="Shipper Size" SortExpression="Final_Pack_Cost_Unit" />
                                                <asp:BoundField DataField="Pack_Unit_MeasurementData" HeaderText="Unit Measurement" SortExpression="UnitMeasurement" />
                                                <asp:BoundField DataField="Fk_PackingLoss" HeaderText="Packing Loss(%)" SortExpression="UnitMeasurement" />
                                                <%--      <asp:TemplateField HeaderText="Packing Loss (%)">
                                                        <ItemTemplate>

                                                            <%# Eval("Fk_PackingLoss")%>%
                                                        </ItemTemplate>

                                                    </asp:TemplateField>--%>
                                                <asp:BoundField DataField="Pack_Unit_Shipper" HeaderText="Unit / Shipper" SortExpression="Pack_Unit_Shipper" />
                                                <asp:BoundField DataField="TotalProduct PM Cost/Unit" HeaderText="Total Product PM Cost/Unit" Visible="false" SortExpression="Pack_Unit_Shipper" />
                                                <asp:BoundField DataField="TotalProduct PM Cost/Ltr" HeaderText="Total Product PM Cost/Ltr" Visible="false" SortExpression="Pack_Unit_Shipper" />
                                                <asp:TemplateField HeaderText="Total Cost / Unit ">
                                                    <%--  <ItemTemplate>
                                                        <%# Eval("TotalProduct PM Cost/Unit")%>%
                                                    </ItemTemplate>--%>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Total_Product_PM_Cost_Unit" runat="server" Text='<%#Eval("Total Cost/Unit") %>'></asp:Label>

                                                        <asp:Button ID="Edit" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditCostingPopup_Click" runat="server" Text="Edit" data-bs-toggle="modal" data-bs-target="#CostMaterialModal" class="btn btn-success btn-sm float-md-end" />

                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txt_Formulation" runat="server" Text='<%#Eval("Total_Product_PM_Cost_Unit") %>'></asp:TextBox>--%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Total_Product_PM_Cost_Unit" HeaderText="Total_PM_Cost_Unit" SortExpression="Pack_Unit_Shipper" />--%>
                                                <%--<asp:BoundField DataField="Total_Product_PM_Cost_Ltr" HeaderText="Total_PM_Cost_Ltr" SortExpression="Pack_Unit_Shipper" />--%>
                                                <asp:TemplateField HeaderText="Total Cost / Ltr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Total_Product_PM_Cost_Ltr" runat="server" Text='<%#Eval("Total Cost/Ltr") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelPackingMaterialsMasterBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" ValidateRequestMode="Disabled" OnClick="DelPackingMaterialsMasterBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="EditPackingMaterialsMasterBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" ValidateRequestMode="Disabled" OnClick="EditPackingMaterialsMasterBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm mt-1" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                            <%--******************************Modal*****************************--%>

                            <div class="modal fade" id="CostMaterialModal" tabindex="-1">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content col-lg-12">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="modal-header">
                                                    <h6 class="modal-title" id="exampleModalLabel">Bulk Product Name : [<asp:Label ID="lblBPM_Name" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                       Pack Measurement : [<asp:Label ID="lblPackSizeName" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                                    Category : [<asp:Label ID="lblPMCategoryName" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]

                                                    </h6>
                                                    <button aria-label="Close" class="btn-close end-0" data-bs-dismiss="modal" type="button">
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="modal-body">

                                                        <div class="row mb-3">

                                                            <asp:Label ID="lblUnitPerShipper" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblUnitMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblShipperSize" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblUnitPerKg" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblMainCategory_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                            <div class="col-md-4">
                                                                <div class="input-group ">
                                                                    <span class="input-group-text" id="PMRMCategory">PM RM Category</span>
                                                                    <asp:DropDownList ID="PMRMCategoryDropdown" OnSelectedIndexChanged="PMRMCategoryDropdown_SelectedIndexChanged" OnTextChanged="PMRMCategoryDropdown_TextChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <span class="input-group-text" id="OR">OR</span>
                                                            </div>



                                                            <div class="col-md-4">
                                                                <div class="input-group-text" aria-disabled="true">
                                                                    <asp:CheckBox ID="ChkBOXShipper" OnCheckedChanged="ChkBOXShipper_CheckedChanged" Checked="false" AutoPostBack="true" CssClass="form-check-inline" runat="server" />

                                                                    <span class="input-group-text" id="ShipperType">Shipper Type</span>

                                                                    <asp:DropDownList ID="ShipperCostDropdown" OnSelectedIndexChanged="ShipperCostDropdown_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select disabled" runat="server">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:TextBox ID="ShipperCosttxt" AutoPostBack="true" ReadOnly="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>--%>
                                                                </div>

                                                            </div>


                                                        </div>
                                                        <div class=" row mb-3">
                                                            <div class="col-md-4">
                                                                <div class="input-group">
                                                                    <span class="input-group-text" id="PMName">PM Name</span>
                                                                    <asp:DropDownList ID="PMNameDropdowm" OnSelectedIndexChanged="PMNameDropdowm_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="input-group">
                                                                    <span class="input-group-text" id="UnitsKG">PM  Price (Cost/Unit)</span>
                                                                    <asp:TextBox ID="PM_Price_Cost_Unittxt" OnTextChanged="PM_Price_Cost_Unittxt_TextChanged" AutoPostBack="true" ReadOnly="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="input-group">
                                                                    <span class="input-group-text" id="FinalPacking">Final Packing Cost/Unit</span>
                                                                    <asp:TextBox ID="FinalPackingtxt" ReadOnly="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="col-md-4 mt-3">
                                                                <asp:Button ID="AddSubcostingBtn1" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddSubcostingBtn1_Click" runat="server" Text="Add" class="btn  btn-dark" />
                                                                <asp:Button ID="CancelCosting" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelCosting_Click" runat="server" Text="Cancel" class="btn  btn-success offset-1" Visible="false" />
                                                                <asp:Button ID="UpdateSubcostingBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateSubcostingBtn_Click1" runat="server" Text="Update" class="btn  btn-info offset-2" />

                                                            </div>
                                                        </div>
                                                        <asp:Label ID="lblPAckMaterialMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblMaterialCosting_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblPAckMaterial_Id" runat="server" Text="" Visible="false"></asp:Label>

                                                    </div>

                                                    <div class="row mb-3">
                                                        <div class="col-md-6">

                                                            <asp:TextBox ID="TotalProductPMCostLtrtxt" Visible="false" ReadOnly="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>

                                                            <asp:TextBox ID="TotalProductPMCostUnittxt" Visible="false" ReadOnly="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="card-body mb-4 overflow-auto">
                                                        <div class="row">

                                                            <asp:GridView ID="Grid_PackingMaterialCostingMaster" ShowFooter="True" OnRowDataBound="Grid_PackingMaterialCostingMaster_RowDataBound" DataKeyNames="Pack_Sub_Cost_Id"
                                                                runat="server" Autopostback="true" CssClass="table-hover table-responsive gridview overflow-auto"
                                                                GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                                PagerStyle-CssClass="gridview_pager"
                                                                AlternatingRowStyle-CssClass="gridview_alter"
                                                                AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product_Name" SortExpression="BPM_Product_Name" />
                                                                    <asp:BoundField DataField="PM_RM_Category_Name" HeaderText="Category_Name" SortExpression="PM_RM_Category_Name" />
                                                                    <asp:BoundField DataField="PM_RM_Name" HeaderText="Name" SortExpression="PM_RM_Name" />
                                                                    <asp:BoundField DataField="PMRM_Price_Cost_Unit" HeaderText="Price_Cost_Unit" SortExpression="PMRM_Price_Cost_Unit" />
                                                                    <asp:BoundField DataField="Final_Pack_Cost_Unit" HeaderText="Final_Pack_Cost_Unit" SortExpression="Final_Pack_Cost_Unit" />
                                                                    <asp:BoundField DataField="Total_Product_PM_Cost_Unit" HeaderText="Total_Product_PM_Cost_Unit" Visible="false" SortExpression="Final_Pack_Cost_Unit" />
                                                                    <asp:BoundField DataField="Total_Product_PM_Cost_Ltr" HeaderText="" Visible="false" SortExpression="Final_Pack_Cost_Unit" />

                                                     <%--               <asp:TemplateField HeaderText="" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotal_Product_PM_Cost_Unit" runat="server" Text='<%#Eval("Total_Product_PM_Cost_Unit") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText=""  Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotal_Product_PM_Cost_Ltr" runat="server" Text='<%#Eval("Total_Product_PM_Cost_Ltr") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <%--<asp:TemplateField HeaderText="ok"></asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="DelCostingMaterialBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelCostingMaterialBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                                            <asp:Button ID="EditCostingMaterialBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditCostingMaterialBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>
                                                        <div class="row  mb-3">
                                                            <div class=" col-6 col-lg-2 col-lg-2 offset-5 mt-1">

                                                                <div class="d-grid">
                                                                    <asp:Button ID="UpdateFinalMaterialMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateFinalMaterialMaster_Click" Text="Update FinalCost" runat="server" class="btn btn-primary " />
                                                                    <asp:Button ID="AddFinalMaterialMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddFinalMaterialMaster_Click" Text="Add FinalCost" runat="server" class="btn btn-success m-2 " />
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <asp:Label ID="lblFinalPackingMaterialMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                        <div class="card mb-4 overflow-scroll">
                                                            <div class="row">
                                                                <asp:GridView ID="Grid_FinalPakingMaterialMaster" Visible="false" runat="server">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>



                                    <asp:Label ID="lblTotal_Product_PM_Cost_Unit" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTotal_Product_PM_Cost_Ltr" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPackingMaterial_Id" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <%--*********************************************************************--%>
                    </div>
                </div>

            </main>

        </div>
    </div>
    <%--   <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/simple-datatables@latest" crossorigin="anonymous"></script>



    <script src="Content/Admin/js/datatables-simple-demo.js"></script>
    <script src="Content/Admin/js/scripts.js"></script>
    <script src="Content/bootstrap-5.1.0-dist/js/bootstrap.min.js"></scriipt>>--%>
</asp:Content>
