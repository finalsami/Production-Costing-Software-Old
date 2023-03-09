<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FinishedGoodsMaster.aspx.cs" Inherits="Production_Costing_Software.FinishedGoodsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
            
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid px-4">
                                <h1 class="mt-4">Finished Goods Master</h1>
                                <ol class="breadcrumb mb-4">
                                </ol>

                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Finished Goods
                                    </div>
                                    <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="card-body">

                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="MainCategoryspnId">Main Category</span>

                                                    <asp:DropDownList ID="MainCategoryDropdown" OnSelectedIndexChanged="MainCategoryDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="BulkProductDropdownListid">BulkProduct</span>

                                                    <asp:DropDownList ID="BulkProductDropDownList" OnSelectedIndexChanged="BulkProductDropDownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PackingSizeSpanId">Packing Size</span>
                                                    <%--<asp:TextBox ID="PackingSizetxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="PackingSizeDropDown" OnSelectedIndexChanged="PackingSizeDropDown_SelectedIndexChanged" Enabled="false" AutoPostBack="true" CssClass="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <asp:Label ID="lblpackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUnitMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPacksize" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="row mb-3">
                                            <div class="col-md-3" id="hideUnitMeasurement" runat="server">
                                                <div class="input-group  ">
                                                    <span class="input-group-text" id="PackingMeasurementSpanId">Unit Measurement</span>
                                                    <asp:DropDownList ID="UnitMeasurementDropdown" Visible="false" AutoPostBack="true" class="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6 mt-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="ShipperSizeSpanid">Packing Description</span>
                                                    <asp:TextBox ID="PackingDescriptiontxt" class="form-control" TextMode="SingleLine" runat="server"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="col-md-3 mt-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Transportation">Unit / Ltr</span>
                                                    <asp:TextBox ID="UnitPerLtrtxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3 mt-4">
                                                <div class="shadow-lg  p-3 mb-5 bg-white rounded form-control  input-group border border-danger border-2">
                                                    <span class="card input-group-text" id="PackingLossPercentSpanId">Packing Loss - %</span>
                                                    <asp:TextBox ID="PackingLossPercenttxt" Text="0" OnTextChanged="PackingLossPercenttxt_TextChanged" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 mt-4">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BulkCostPerLtrSpanId">Bulk Cost / Ltr</span>
                                                    <asp:TextBox ID="BulkCostPerLtrtxt" Text="0" ReadOnly="true" TextMode="Number" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>


                                            <div class="col-md-3 mt-4">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TotalPMCostPerLtrSpanId">Total PM Cost / Ltr</span>
                                                    <asp:TextBox ID="TotalPMCostPerLtrtxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3 mt-4">
                                                <div class="input-group">
                                                    <span class="card input-group-text" id="TotalPMCostPerUnitSpanId">Total PM Cost / Unit</span>
                                                    <asp:TextBox ID="TotalPMCostPerUnittxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3 mt-4">
                                                <div class="shadow-lg  p-3 mb-5 bg-white rounded form-control  input-group border border-danger border-2">
                                                    <span class=" card input-group-text" id="PackingLossAmountSpanId">Packing Loss - Amount</span>
                                                    <asp:TextBox ID="PackingLossAmounttxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3 mt-4">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="LabourCostPerLtrSpanId">Labour cost / Ltr</span>
                                                    <asp:TextBox ID="LabourCostPerLtrtxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="FinishedGoodsPerLtrSpanId">Finished Goods / Ltr</span>
                                                    <asp:TextBox ID="FinishedGoodsPerLtrtxt" ReadOnly="true" TextMode="Number" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>




                                        </div>
                                        <div class="row mb-3 mt-4">
                                            <div class="col-md-4">
                                                <div class="shadow-lg  p-3 mb-5 bg-white rounded form-control border border-success border-3">
                                                    <span class=" card input-group-text" id="NetFinishedGoodsCostPerLtrSpanId">Net Finished Goods Cost / Ltr</span>
                                                    <asp:TextBox ID="NetFinishedGoodsCostPerLtrtxt" ReadOnly="true" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class=" shadow-lg  p-3 mb-5 bg-white rounded  form-control border border-success border-3">
                                                    <span class="card input-group-text text-black-150" id="NetFinishedGoodsCostPerUnitSpanId">Net Finished Goods Cost/ Unit</span>
                                                    <asp:TextBox ID="NetFinishedGoodsCostPerUnittxt" ReadOnly="true" TextMode="Number" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class=" shadow-lg  p-3 mb-5 bg-white rounded form-control border border-info border-3">
                                                    <span class=" card input-group-text " id="PMDiffrenceFrom1LToBelowtxtSpanId">PM Diffrence from 1 L to Below</span>
                                                    <asp:TextBox ID="PMDiffrenceFrom1LToBelowtxt" ReadOnly="true" AutoPostBack="true" class=" form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-10 col-lg-4 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="Updatebtn" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-secondary btn-block" />
                                                <asp:Button ID="Addbtn" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-success btn-block" />
                                            </div>

                                        </div>
                                        <div class=" col-10 col-lg-4 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="Cancel" OnClick="Cancel_Click" runat="server" Text="Cancel" class="btn btn-info btn-block" />
                                            </div>

                                        </div>
                                    </div>

                                    <asp:Label ID="lblFinishedGoods_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="mb-4 mt-4" style="overflow: auto">

                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="ExportToExcel" OnClick="ExportToExcel_Click" runat="server" Text="Report Excel" class="btn btn-light btn-outline-primary btn-block" />

                                                <asp:GridView ID="Grid_FinishedGoodsMaster" CssClass=" table-hover table-responsive gridview overflow-auto"
                                                    GridLines="None"
                                                    AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                                    AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" OnPageIndexChanging="Grid_FinishedGoodsMaster_PageIndexChanging" runat="server" Autopostback="true"
                                                    AutoGenerateColumns="False" DataKeyNames="FinishedGoods_Id">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="MainCategory_Name" HeaderText="MainCategory" SortExpression="BPM_Product_Name" />
                                                        <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name     " SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="PackingSize" HeaderText="Pack_size" SortExpression="PM_RM_Category_Name" />
                                                        <%--<asp:BoundField DataField="Unit Measurement" HeaderText="Unit Measurement" SortExpression="PM_RM_Category_Name" />--%>
                                                        <asp:BoundField DataField="UnitPerLtr" HeaderText="UnitPerLtr" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="BulkCostPerLtr" HeaderText="Bulk-Cost/Ltr" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="TotalPMCostPerUnit" HeaderText="Total-PM-Cost/Unit" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="TotalPMCostPerLtr" HeaderText="Total-PM-Cost/Ltr" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="LabourCostPerLtr" HeaderText="Labour Cost /Ltr" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="FinishedGoodsPerLtr" HeaderText="Finished-Goods/Ltr" SortExpression="PM_RM_Category_Name" />
                                                        <%--<asp:BoundField DataField="PackingPerLossPer" HeaderText="Packing/Loss" SortExpression="PM_RM_Category_Name" />--%>
                                                        <asp:BoundField DataField="PackingLossAmt" HeaderText="Packing-Loss-Amt" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="NetFinishedGoodsCostPerLtr" HeaderText="Net-Finished-Goods-Cost/Ltr" SortExpression="PM_RM_Category_Name" />
                                                        <asp:BoundField DataField="NetFinishedGoodsCostPerUnit" HeaderText="Net-Finished-Goods-Cost/Unit" SortExpression="PM_RM_Category_Name" />

                                                        <asp:BoundField DataField="OnDate" HeaderText="Date FinishedGood" DataFormatString="{0:d}" SortExpression="PM_RM_Category_Name" />


                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Button ID="DelFinishedGoodsMasterBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" OnClick="DelFinishedGoodsMasterBtn_Click" Text="Delete" Style="width: 80px" runat="server" class="btn btn-danger btn-sm" />
                                                                <asp:Button ID="EditFinishedGoodsMasterBtn" OnClick="EditFinishedGoodsMasterBtn_Click" Text="Edit" Style="width: 80px" runat="server" class="btn btn-success btn-sm" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                                </hr>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ExportToExcel" />
                                            </Triggers>
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
