<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="FormulationMaster.aspx.cs" Inherits="Production_Costing_Software.FormulationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">

        var GridId = "<%=Grid_Formulation_MasterList.ClientID %>";
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
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Formulation Master</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                                <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                                <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                                <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                                <li class="breadcrumb-item"><a href="BulkProductMaster.aspx">Bulk Product Master</a></li>
                                <li class="breadcrumb-item"><a href="FormulationMaster.aspx">Formulation Master</a></li>

                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Formulation Master
                           
                                </div>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <%--<input class="form-control" id="Formulationtxt" type="text" class="form-control" />--%>
                                                <asp:TextBox ID="Formulationtxt" OnTextChanged="Formulationtxt_TextChanged" AutoPostBack="true" ClientIDMode="Static" class="form-control" runat="server" placeholder="Formulation Name"></asp:TextBox>
                                                <label for="Formulationtxt">Formulation Name</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="Formulationtxt" runat="server" ForeColor="Red" ErrorMessage="* Formulation "></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <div class="form-floating">
                                                    <%--<input class="form-control" id="BatchSizetxt" type="number" placeholder="Batch Size" />--%>
                                                    <asp:TextBox ID="BatchSizetxt" class="form-control" ClientIDMode="Static" Text="0" runat="server" TextMode="Number" placeholder="Batch Size"></asp:TextBox>

                                                    <label for="BatchSizetxt">Batch Size</label>
                                                </div>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="offset-1" ControlToValidate="BatchSizetxt" runat="server" ForeColor="Red" ErrorMessage="* BatchSize "></asp:RequiredFieldValidator>

                                        </div>


                                        <div class="col-md-4">

                                            <asp:DropDownList ID="UnitMeasurementdownList" Style="height: 58px" AppendDataBoundItems="true" AutoPostBack="True"
                                                class="form-select" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <asp:Label ID="lblSuperVisorCost" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblFM_Id" runat="server" Text="" Visible="false"></asp:Label>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="Labourtxt" ClientIDMode="Static" OnTextChanged="Labourtxt_TextChanged" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="No. of Labours"></asp:TextBox>
                                                <label for="LabourtxtReqired">No. of Labours</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="LabourtxtReqired" runat="server" ForeColor="Red" ErrorMessage="* Labour!" ControlToValidate="Labourtxt"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="Supervisorstxt" ClientIDMode="Static" OnTextChanged="Supervisorstxt_TextChanged" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Supervisors"></asp:TextBox>
                                                <label for="SupervisorsReqired">Supervisors</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red" ErrorMessage="* Labour!" ControlToValidate="Supervisorstxt"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="LabourChargetxt" OnTextChanged="LabourChargetxt_TextChanged" ReadOnly="true" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Labour Charge"></asp:TextBox>
                                                <label for="LabourChargetxt">Labour Charge</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ErrorMessage="* Labour Charge!" ControlToValidate="LabourChargetxt"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="TotalLabourCosttxt" ReadOnly="true" ClientIDMode="Static" class="form-control" Text="0" runat="server" TextMode="Number" placeholder="Total Labour Cost"></asp:TextBox>
                                                <label for="TotalLabourCosttxt">Total Labour Cost</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ErrorMessage="*Total Labour Cost!" ControlToValidate="TotalLabourCosttxt"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="PowerUnittxt" OnTextChanged="PowerUnittxt_TextChanged" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Power (Unit)"></asp:TextBox>
                                                <label for="PowerUnittxt">Power (Unit)</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ErrorMessage="* Power Unit!" ControlToValidate="PowerUnittxt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="PowerChargetxt" ClientIDMode="Static" ReadOnly="true" OnTextChanged="PowerChargetxt_TextChanged" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Power Charge/Unit"></asp:TextBox>
                                                <label for="PowerChargetxt">Power Charge/Unit</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" ErrorMessage="* Power Charge !" ControlToValidate="PowerChargetxt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="TotalPowerCosttxt" ReadOnly="true" ClientIDMode="Static" class="form-control" Text="0" runat="server" TextMode="Number" placeholder="Total Power Cost"></asp:TextBox>
                                                <label for="TotalPowerCosttxt">Total Power Cost</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red" ErrorMessage="* Total Power Cost !" ControlToValidate="TotalPowerCosttxt"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="Maintenancetxt" ClientIDMode="Static" OnTextChanged="Maintenancetxt_TextChanged" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Maintenance"></asp:TextBox>
                                                <label for="Maintenancetxt">Maintenance</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red" ErrorMessage="* Maintenance !" ControlToValidate="Maintenancetxt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="OtherCosttxt" ClientIDMode="Static" OnTextChanged="OtherCosttxt_TextChanged" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number" placeholder="Other Cost"></asp:TextBox>
                                                <label for="OtherCosttxt">Other Cost</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red" ErrorMessage="* Other Cost !" ControlToValidate="OtherCosttxt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="TotalCosttxt" ClientIDMode="Static" OnTextChanged="TotalCosttxt_TextChanged" AutoPostBack="true" ReadOnly="true" class="form-control" runat="server" Text="0" TextMode="Number" placeholder="Total Cost"></asp:TextBox>
                                                <label for="TotalCosttxt">Total Cost</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red" ErrorMessage="* Total Cost !" ControlToValidate="TotalCosttxt"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row mb-3">


                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="AddBuffertxt" AutoPostBack="true" OnTextChanged="AddBuffertxt_TextChanged" ClientIDMode="Static" class="form-control" runat="server" Text="0" TextMode="Number" placeholder="Add Buffer"></asp:TextBox>
                                                <label for="AddBuffertxt">Add Buffer</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red" ErrorMessage="* Add Buffer !" ControlToValidate="AddBuffertxt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="CostPerLtrBatchSizetxt" ClientIDMode="Static" ReadOnly="true" class="form-control" runat="server" Text="0" TextMode="Number" placeholder="Cost per ltr/BatchSize"></asp:TextBox>
                                                <label for="CostPerLtrBatchSizeSpanId">Cost per ltr/BatchSize</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red" ErrorMessage="* Cost Per Ltr Batch Size !" ControlToValidate="CostPerLtrBatchSizetxt"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="FinalCostPerLtrBatchSizetxt" ReadOnly="true" ClientIDMode="Static" class="form-control" runat="server" Text="0" TextMode="Number" placeholder="Final Cost/LtrBatchSize"></asp:TextBox>
                                                <label for="FinalCostPerLtrBatchSize">Final Cost/LtrBatchSize</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red" ErrorMessage="* Add Buffer !" ControlToValidate="AddBuffertxt"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="EditFormulationbtn" CausesValidation="false" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Visible="false" OnClick="Edit_Click" runat="server" Text="Update" CssClass="btn btn-secondary btn-block" />
                                                <asp:Button ID="AddFormulationbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Add_Click" runat="server" Text="Add" CssClass="btn btn-primary btn-block" />

                                            </div>
                                        </div>
                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelBtn_Click" runat="server" Text="Cancel" CssClass="btn btn-warning btn-block" />

                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class=" card-body mb-4 mt-4 overflow-auto">
                                    <asp:GridView ID="Grid_Formulation_MasterList" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" Autopostback="true"
                                        AutoGenerateColumns="False" DataKeyNames="FM_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Formulation_Id" HeaderText="Formulation_Id" SortExpression="Formulation_Id" />--%>
                                            <asp:BoundField DataField="FormulationName" HeaderText="FormulationName" SortExpression="FormulationName" />
                                            <asp:BoundField DataField="BatchSize" HeaderText="BatchSize" SortExpression="BatchSize" />
                                            <asp:BoundField DataField="Labour" HeaderText="Labour" SortExpression="Labour" />
                                            <asp:BoundField DataField="FM_Supervisors" HeaderText="Supervisors" SortExpression="Labour" />
                                            <asp:BoundField DataField="LabourCharge" HeaderText="LabourCharge" SortExpression="LabourCharge" />
                                            <asp:BoundField DataField="LabourTotalCost" HeaderText="LabourTotalCost" SortExpression="LabourTotalCost" />
                                            <asp:BoundField DataField="PoweUnit" HeaderText="PoweUnit" SortExpression="PoweUnit" />
                                            <asp:BoundField DataField="Maintenance" HeaderText="Maintenance" SortExpression="Maintenance" />
                                            <asp:BoundField DataField="OtherCost" HeaderText="OtherCost" SortExpression="OtherCost" />
                                            <asp:BoundField DataField="Measurement" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="TotalCost" HeaderText="TotalCost" SortExpression="TotalCost" />
                                            <asp:BoundField DataField="CostPerLtrBatchSize" HeaderText="Cost/Ltr Batchsize" SortExpression="CostPerLtrBatchSize" />
                                            <asp:BoundField DataField="AddBuffer" HeaderText="Add Buffer" SortExpression="CostPerLtrBatchSize" />
                                            <asp:BoundField DataField="FinalCostPerLtrBatchSize" HeaderText="Final-Cost/Ltr-BatchSize" SortExpression="CostPerLtrBatchSize" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditFormulationBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="EditFormulation" OnClick="EditFormulationBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />
                                                    <asp:Button ID="DelFormulationBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelFormulationBtn_Click" ValidationGroup="DelFormulation" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </main>
        </div>
    </div>
</asp:Content>
