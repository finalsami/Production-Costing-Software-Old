<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="TransportationCostFactors.aspx.cs" Inherits="Production_Costing_Software.TransportationCostFactors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_TransportationCostFactors.ClientID %>";
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <script type="text/javascript">
        function ShowPopup2(title, body) {
            $("#AverageTransportModal").modal("show");
        }
    </script>
    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#UnLoadingModal").modal("show");
        }
    </script>
    <script type="text/javascript">
        function ShowPopup1(title, body) {
            $("#LocalCartageModal").modal("show");
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
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Transportation Cost Factors</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="TransportationCostFactors.aspx">Transportation Cost Factors</a></li>
                    </ol>

                    <div class="card mb-4">
                        <%--              <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <link href="Content/Loading.css" rel="stylesheet" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                        <asp:UpdatePanel ID="BOM_MasterUpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Transportation Cost Factors
                           
                                </div>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="StatewiseDropdown" AppendDataBoundItems="true" AutoPostBack="true" class="form-select" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="TruckloadChargetxt" OnTextChanged="TruckloadChargetxt_TextChanged" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Truck load Charge"></asp:TextBox>
                                                <label for="TruckloadChargetxt">Truck load Charge</label>
                                            </div>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="TruckloadChargetxt" runat="server" ForeColor="Red" ErrorMessage="* Truck load Charge !"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="ApproxNoOfCartonIn1Lottxt" OnTextChanged="ApproxNoOfCartonIn1Lottxt_TextChanged" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Approx. No Of carton in 1 Lot"></asp:TextBox>
                                                <label for="ApproxNoOfCartonIn1Lottxt">Approx No Of Carton In 1 Lot</label>
                                            </div>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ApproxNoOfCartonIn1Lottxt" runat="server" ForeColor="Red" ErrorMessage="* Approx. No Of carton in 1 Lot !"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="Approx1CartonChargetxt" ReadOnly="true" class="form-control" runat="server" TextMode="Number" placeholder="Approx. 1 Carton Charge"></asp:TextBox>
                                                <label for="Approx1CartonChargetxt">Approx. 1 Carton Charge</label>
                                            </div>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Approx1CartonChargetxt" runat="server" ForeColor="Red" ErrorMessage="* Approx. 1 Carton Charge !"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <%--<div class="col-md-4">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="AverageLocalTraspoationtxt" ClientIDMode="Static" class="form-control" runat="server" TextMode="Number" placeholder="Average Local Traspoation"></asp:TextBox>
                                                    <label for="AverageLocalTraspoationtxt">Average Local Trasportation</label>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="AverageLocalTraspoationtxt" runat="server" ForeColor="Red" ErrorMessage="* Average Local Traspoation !"></asp:RequiredFieldValidator>
                                            </div>--%>
                                    </div>


                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-10 col-lg-4 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="UpdateTransportationbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateTransportationbtn_Click" runat="server" Visible="false" Text="Update" class="btn btn-success btn-block" />
                                                <asp:Button ID="AddTransportationbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddTransportationbtn_Click" runat="server" Text="Add" class="btn btn-primary btn-block" />

                                            </div>
                                        </div>
                                        <div class=" col-10 col-lg-4 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelTransportationBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="CancalTransportationBtn_Click" runat="server" Text="Cancel" class="btn btn-info btn-block" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-body mb-4 overflow-auto mt-4">
                                        <asp:GridView ID="Grid_TransportationCostFactors" Autopostback="true" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter"
                                            runat="server" AutoGenerateColumns="False" DataKeyNames="TransportationCostFactors_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="StateName" HeaderText="State Name" SortExpression="MainCategory" />

                                                <asp:BoundField DataField="TotalTruckLoadCharge" HeaderText="Total-Truck-Load-Charge" SortExpression="BulkProductName" />

                                                <asp:BoundField DataField="ApproxNoOfCartonIn1Lot" HeaderText="Approx No Of CartonIn 1 Lot" SortExpression="BatchSize" />
                                                <asp:BoundField DataField="Approx1CartonCharge" HeaderText="Approx 1 Carton Charge" SortExpression="Measurement" />
                                                <%--<asp:BoundField DataField="AverageLocalTraspoation" HeaderText="Average Local Traspoation" SortExpression="Measurement" />--%>
                                                <asp:TemplateField HeaderText="Average Local Transportation">
                                                    <ItemTemplate>
                                                        <asp:Button ID="AverageLocalTransportationAddRangePopup" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AverageLocalTransportationAddRangePopup_Click" OnClientClick="ShowPopup2" CausesValidation="false" Text="Add Range" runat="server" data-bs-toggle="modal" data-bs-target="#AverageTransportModal" class="btn btn-success btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unloading Charge">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="DelFormulationBtn" OnClick="DelFormulationBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />--%>
                                                        <asp:Button ID="UnloadingChargeAddRangePopup" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UnloadingChargeAddRangePopup_Click" CausesValidation="false" Text="Add Range" runat="server" data-bs-toggle="modal" data-bs-target="#UnLoadingModal" class="btn btn-success btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Local Cartage">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="DelFormulationBtn" OnClick="DelFormulationBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />--%>
                                                        <asp:Button ID="LocalCartageAddRangePopup" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClientClick="ShowPopup1" OnClick="LocalCartageAddRangePopup_Click" CausesValidation="false" Text="Add range" runat="server" data-bs-toggle="modal" data-bs-target="#LocalCartageModal" class="btn btn-success btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report">
                                                    <ItemTemplate>
                                                        <asp:Button ID="TransportReport" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="TransportReport_Click" OnClientClick="SetTarget();" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" Text="Report" runat="server" class="btn btn-warning btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelTransportationCostFactorBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelTransportationCostFactorsBtn_Click" ValidationGroup="DelTrans" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="EditTransportationCostFactorBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditTransportationCostFactorsBtn_Click" ValidationGroup="UpdateTrans" Text="Edit" runat="server" class="btn btn-secondary btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                    <div class="modal fade " id="AverageTransportModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content col-lg-12">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel2">
                                                <h6>AverageTransport : [<asp:Label ID="lblStateNameAve" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                </h6>

                                            </h5>
                                            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button">
                                            </button>
                                        </div>
                                        <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="modal-body">
                                            <div class="row mb-3">
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="AverageTransportStart">Start</span>
                                                        <asp:TextBox ID="AverageLocalTransportStarttxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="AverageTransportEnd">End</span>
                                                        <asp:TextBox ID="AverageLocalTransportEndtxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="input-group ">
                                                        <span class="input-group-text" id="AverageTransportDropdownCombo">Unit Measurement</span>

                                                        <asp:DropDownList ID="AverageTransportDropdownComboList" OnSelectedIndexChanged="AverageTransportDropdownComboList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="UnloadingChargeAmount1">Amount</span>
                                                        <asp:TextBox ID="AverageLocalTransportAmounttxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row  justify-content-center align-items-center mt-4">

                                                    <div class=" col-10 col-lg-4 col-lg-4">
                                                        <div class="d-grid">
                                                            <asp:Button ID="AddAverageLocalTransport" OnClick="AddAverageLocalTransport_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Add" runat="server" Text="Add" class="btn btn-success btn-sm mt-1" />
                                                            <asp:Button ID="UpdateAverageLocalTransport" OnClick="UpdateAverageLocalTransport_Click" Visible="false" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Add" runat="server" Text="Update" class="btn btn-primary btn-sm mt-1" />

                                                        </div>
                                                    </div>
                                                    <div class=" col-10 col-lg-4 col-lg-4">
                                                        <div class="d-grid">

                                                            <asp:Button ID="CancelAverageLocalTransport" OnClick="CancelAverageLocalTransport_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Cancel" runat="server" Text="Cancel" class="btn btn-info btn-sm mt-1" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblAverageTransport_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="card-body mt-4 overflow-auto">
                                            <asp:GridView ID="Grid_AverageTransport" Autopostback="true"
                                                CssClass=" table-hover table-responsive gridview"
                                                GridLines="None"
                                                Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                PagerStyle-CssClass="gridview_pager"
                                                AlternatingRowStyle-CssClass="gridview_alter"
                                                runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="AverageLocalTransport_Id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="StateName" HeaderText="StateName" SortExpression="StateName" />
                                                    <asp:BoundField DataField="AverageLocalTransportStart" HeaderText="AverageLocalTransportStart" SortExpression="UnloadingStart" />
                                                    <asp:BoundField DataField="AverageLocalTransportEnd" HeaderText="AverageLocalTransportEnd" SortExpression="UnloadingEnd" />
                                                    <asp:BoundField DataField="UnitMeasurement" HeaderText="UnitMeasurement" SortExpression="UnitMeasurement" />

                                                    <asp:BoundField DataField="AverageLocalTransportAmount" HeaderText="AverageLocalTransportAmount" SortExpression="UnloadingAmount" />
                                                    <asp:TemplateField HeaderText="Action" >
                                                        <ItemTemplate>
                                                            <asp:Button ID="DelAverageTransportChargeBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);width:80px" OnClick="DelAverageTransportChargeBtn_Click" ValidationGroup="Del" Text="Delete" runat="server" class="btn btn-danger btn-sm " />
                                                            <asp:Button ID="EditAverageTransportBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);width:80px"  ValidationGroup="Edit" Text="Edit" runat="server" OnClick="EditAverageTransportBtn_Click" class="btn btn-success btn-sm mt-1" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade " id="UnLoadingModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content col-lg-12">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel">
                                                <h6>Unloading Charge : [<asp:Label ID="lblStateName" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                </h6>
                                                <%--<h6>/ Name : [<asp:Label ID="lblBPM_Name" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]</h6>
                                                                <h6>/ Batch Size : [<asp:Label ID="lblBatchSize" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]</h6>
                                                                <h5></h5>
                                                                <h5></h5>
                                                                <h5></h5>
                                                                <h5></h5>
                                                                <h5></h5>--%>
                                            </h5>
                                            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button">
                                            </button>
                                        </div>
                                        <asp:Label ID="lblState_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lbltransportationCostFactor_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="modal-body">
                                            <div class="row mb-3">
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="UnloadingChargeStart">Start</span>
                                                        <asp:TextBox ID="UnloadingChargeStarttxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="UnloadingChargeEnd">End</span>
                                                        <asp:TextBox ID="UnloadingChargeEndtxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="input-group ">
                                                        <span class="input-group-text" id="UnitMeasurementDropdownCombo">Unit Measurement</span>

                                                        <asp:DropDownList ID="UnitMeasurementDropdownList" AutoPostBack="true" class="form-select" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="UnloadingChargeAmount">Amount</span>
                                                        <asp:TextBox ID="UnloadingChargeAmounttxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row  justify-content-center align-items-center mt-4">

                                                    <div class=" col-10 col-lg-4 col-lg-4">
                                                        <div class="d-grid">
                                                            <asp:Button ID="UpdateUnloadingChargelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateUnloadingChargelBtn_Click1" ValidationGroup="Add" runat="server" Visible="false" Text="Update" class="btn btn-success btn-sm mt-1" />
                                                            <asp:Button ID="AddUnloadingChargelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddUnloadingChargelBtn_Click" ValidationGroup="Add" runat="server" Text="ADD" class="btn btn-primary btn-sm mt-1" />

                                                        </div>
                                                    </div>
                                                    <div class=" col-10 col-lg-4 col-lg-4">
                                                        <div class="d-grid">

                                                            <asp:Button ID="CancelUnloadingChargelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Cancel" OnClick="CancelUnloadingChargelBtn_Click" runat="server" Text="Cancel" class="btn btn-info btn-sm mt-1" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblLocalCartage_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lbltransportationUnloadingCharge_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="card-body mt-4 overflow-auto">
                                            <asp:GridView ID="Grid_UnloadingCharge" Autopostback="true"
                                                CssClass=" table-hover table-responsive gridview"
                                                GridLines="None"
                                                Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                PagerStyle-CssClass="gridview_pager"
                                                AlternatingRowStyle-CssClass="gridview_alter"
                                                runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="TransportUnloadingCharge_Id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="StateName" HeaderText="StateName" SortExpression="StateName" />
                                                    <asp:BoundField DataField="UnloadingStart" HeaderText="UnloadingStart" SortExpression="UnloadingStart" />
                                                    <asp:BoundField DataField="UnloadingEnd" HeaderText="UnloadingEnd" SortExpression="UnloadingEnd" />
                                                    <asp:BoundField DataField="UnitMeasurement" HeaderText="UnitMeasurement" SortExpression="UnitMeasurement" />

                                                    <asp:BoundField DataField="UnloadingAmount" HeaderText="UnloadingAmount" SortExpression="UnloadingAmount" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="DelUnloadingChargeBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClick="DelUnloadingChargeBtn_Click1" ValidationGroup="Del" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                            <asp:Button ID="EditUnloadingChargeBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClick="EditUnloadingChargeBtn_Click1" ValidationGroup="Edit" Text="Edit" runat="server" class="btn btn-success btn-sm" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade " id="LocalCartageModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content col-lg-12">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel123">
                                                <h6>Local Cartage Charge : [<asp:Label ID="lblLocalCartageState" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>
                                                ]
                                                                
                                            </h5>
                                            <button aria-label="Close" class="btn-close" data-bs-dismiss="modal" type="button">
                                            </button>
                                        </div>

                                        <div class="modal-body">
                                            <div class="row mb-3">
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="LocalCartageStart">Start</span>
                                                        <asp:TextBox ID="LocalCartageStarttxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="LocalCartageEnd">End</span>
                                                        <asp:TextBox ID="LocalCartageEndtxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group ">
                                                        <span class="input-group-text" id="UnitMeasurementDropdownCombo2">Unit Measurement</span>

                                                        <asp:DropDownList ID="UnitMeasurementDropdownLocalCartageList" AutoPostBack="true" class="form-select" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <span class="input-group-text" id="LocalCartageAmount">Amount</span>
                                                        <asp:TextBox ID="LocalCartageAmounttxt" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row  justify-content-center align-items-center mt-4">

                                                    <div class=" col-10 col-lg-4 col-lg-4">
                                                        <div class="d-grid">
                                                            <asp:Button ID="UpdateLocalCartageBtn" OnClick="UpdateLocalCartageBtn_Click" Visible="false" ValidationGroup="Update" runat="server" Text="Update" class="btn btn-secondary btn-sm mt-1" />
                                                            <asp:Button ID="AddLocalCartageBtn" OnClick="AddLocalCartageBtn_Click" ValidationGroup="Add" runat="server" Text="ADD" class="btn btn-primary btn-sm mt-1" />

                                                        </div>
                                                    </div>
                                                    <div class=" col-10 col-lg-4 col-lg-4">
                                                        <div class="d-grid">

                                                            <asp:Button ID="CancelLocalCartageBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Cancel" OnClick="CancelLocalCartageBtn_Click" runat="server" Text="Cancel" class="btn btn-info btn-sm mt-1" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-3 modal-footer modal-dialog-scrollable  overflow-auto">

                                                <asp:GridView ID="Grid_LocalCartage" Autopostback="true" CssClass=" table-hover table-responsive gridview"
                                                    GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                    PagerStyle-CssClass="gridview_pager"
                                                    AlternatingRowStyle-CssClass="gridview_alter"
                                                    runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="TransportLocalCartageCharge_Id">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="LocalCartageStart" HeaderText="Range Start" SortExpression="Ingrdient_Name" />
                                                        <asp:BoundField DataField="LocalCartageEnd" HeaderText="Range End" SortExpression="Ingrdient_Name" />
                                                        <asp:BoundField DataField="UnitMeasurement" HeaderText="UnitMeasurement" SortExpression="Ingrdient_Name" />

                                                        <asp:BoundField DataField="LocalCartageAmount" HeaderText="Amount" SortExpression="Ingrdient_Name" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:Button ID="DellocalCartageBtn" OnClick="DellocalCartageBtn_Click1" ValidationGroup="Del" Text="Delete" Style="width: 80px" runat="server" class="btn btn-danger btn-sm" />
                                                                <asp:Button ID="EditLocalCartageBtn" OnClick="EditLocalCartageBtn_Click1" ValidationGroup="Edit" Text="Edit" Style="width: 80px" runat="server" class="btn btn-success btn-sm" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                            </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>

    </div>



</asp:Content>
