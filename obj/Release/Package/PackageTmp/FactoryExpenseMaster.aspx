<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="FactoryExpenseMaster.aspx.cs" Inherits="Production_Costing_Software.FactoryExpenseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
            
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid px-4">
                                <h1 class="mt-4">Factory Expense Master</h1>
                                <ol class="breadcrumb mb-4">
                                </ol>

                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Factory Expense Master
                                    </div>
                                    <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPackSize" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblFactoryExpence_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>

                                    <div class="card-body">

                                        <div class="row mb-3">

                                            <div class="col-md-6">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="BulkProductDropdownListid">BulkProduct</span>

                                                    <asp:DropDownList ID="BulkProductDropDownList" OnSelectedIndexChanged="BulkProductDropDownList_SelectedIndexChanged1" AutoPostBack="true" class="form-select" runat="server">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3" runat="server" id="HideTradeNameDiv">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TradeName">Trade Name</span>
                                                    <asp:TextBox ID="TradeNametxt" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3" id="hidePackingSize" runat="server">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PackingSizeSpanId">Packing Size</span>
                                                    <%--<asp:TextBox ID="PackingSizetxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="PackingSizeDropDown" Enabled="false" AutoPostBack="true" CssClass="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BulkCostPerLtrSpanId">Bulk Cost Amount/ Unit</span>
                                                    <asp:TextBox ID="BulkCostPerLtrtxt" Text="0" ReadOnly="true" TextMode="Number" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="BulkCostPerpanId">
                                                        <asp:Label ID="lblBulkCostUnit" runat="server" Text="" Visible="true"></asp:Label> / Ltr
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">

                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TotalPMCostPerUnitSpanId">Total PM Cost / Unit</span>
                                                    <asp:TextBox ID="TotalPMCostPerUnittxt" Text="0" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="LabourCostPerLtrSpanId">Labour cost / Unit</span>
                                                    <asp:TextBox ID="LabourCostPerLtrtxt" Text="0" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="input-group border border-danger border-3">
                                                    <span class=" input-group-text" id="PackingLossAmountSpanId">Packing Loss  (
                                            <asp:Label ID="lblpackingLossPer" runat="server" Text=""></asp:Label>%)
                                                    </span>
                                                    <asp:TextBox ID="PackingLossAmounttxt" Text="0" ReadOnly="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-3" id="hideUnitMeasurement" runat="server">
                                                <div class="input-group  ">
                                                    <span class="input-group-text" id="PackingMeasurementSpanId">Unit Measurement</span>
                                                    <asp:DropDownList ID="UnitMeasurementDropdown" Visible="false" AutoPostBack="true" class="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ShipperSizeSpanid">Total Costing</span>
                                                    <asp:TextBox ID="TotalCostingtxt" Text="0" ReadOnly="true" class="form-control" TextMode="SingleLine" runat="server"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text text-decoration-line-through" id="Transportation">Factory Expence (%)</span>
                                                    <asp:TextBox ID="FactoryExpencetxt" Text="0" ReadOnly="true"  AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group">
                                                    <span class=" input-group-text text-decoration-line-through" id="PackingLossPercentSpanId">Factory Expence Amount/Unit</span>
                                                    <asp:TextBox ID="FactoryExpenceAmtUnittxt" ReadOnly="true" Text="0" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row mb-3 mt-4">
                                            <div class="col-md-4 mt-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TotalPMCostPeUnitSpanId">Total Amount/Unit</span>
                                                    <asp:TextBox ID="TotalAmountUnittxt" Text="0" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4 mt-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TotalPMCostPerLtrSpanId">Total Amount / Ltr</span>
                                                    <asp:TextBox ID="TotalAmountLtrtxt" Text="0" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>


                                        </div>

                                    </div>

                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-10 col-lg-4 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="Updatebtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-success btn-block" />
                                                <asp:Button ID="Addbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-success btn-block" />
                                            </div>

                                        </div>
                                        <div class=" col-10 col-lg-4 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="Cancel" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Cancel_Click" runat="server" Text="Cancel" class="btn btn-info btn-block" />
                                            </div>

                                        </div>
                                    </div>

                                    <asp:Label ID="lblFinishedGoods_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="card-body mb-4 mt-4" style="overflow: auto">

                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <%--<asp:Button ID="ExportToExcel"  runat="server" Text="Report Excel" class="btn btn-light btn-outline-primary btn-block" />--%>

                                                <asp:GridView ID="Grid_FactoryExpenseMaster" CssClass="table-hover table-responsive gridview overflow-auto"
                                                    GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                    PagerStyle-CssClass="gridview_pager"
                                                    AlternatingRowStyle-CssClass="gridview_alter" PageSize="50" runat="server" Autopostback="true"
                                                    AutoGenerateColumns="False" OnPageIndexChanging="Grid_FactoryExpenseMaster_PageIndexChanging" DataKeyNames="FactoryExpence_Id">
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
                                                        <asp:BoundField DataField="FinalBulkCostPerUnit" HeaderText="Bulk-Cost/Unit" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="FinalPMCostPerUnit" HeaderText="Total-PM-Cost/Unit" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="LabourChargePer" HeaderText="Labour-Cost /Unit" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="PackingLossAmtPerUnit" HeaderText="PackingLoss Amount / Unit" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="TotalCosting" HeaderText="TotalCosting" SortExpression="PM_RM_Category_Name" />
                                                        <%--<asp:BoundField DataField="PackingPerLossPer" HeaderText="Packing/Loss" SortExpression="PM_RM_Category_Name" />--%>
                                                        <asp:BoundField DataField="FactoryExpencePercent" HeaderText="Factory Expence (%)" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="FactoryExpenceAmtPerUnit" HeaderText="Factory Expence Amount / Unit" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="TotalAmtPerUnit" HeaderText="Total-Amount / Unit" SortExpression="PM_RM_Category_Name" />

                                                        <asp:BoundField DataField="TotalAmtPerLiter" HeaderText="Total-Amount / Liter" SortExpression="PM_RM_Category_Name" />

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Button ID="DelFactoryExpenseBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClick="DelFactoryExpenseBtn_Click" Text="Delete" runat="server" class="btn btn-danger" />
                                                                <asp:Button ID="EditFactoryExpenseBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClick="EditFactoryExpenseBtn_Click" Text="Edit" runat="server" class="btn btn-success mt-1" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                                </hr>

                                            </ContentTemplate>
                                            <%-- <Triggers>
                                                <asp:PostBackTrigger ControlID="ExportToExcel" />
                                            </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </div>


                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
            </main>

        </div>
    </div>
</asp:Content>
