<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="comp_CompanyPriceListMaster.aspx.cs" Inherits="Production_Costing_Software.comp_CompanyPriceListMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                    <h1 class="mt-4">Co.Price List Master</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="comp_CompanyPriceListMaster.aspx">Price List</a></li>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Co.Price List Master
                           
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card-body">
                                    <asp:Label ID="lblPAckSize" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    <div class="row mb-3">
                                        <div class="col-md-5">
                                            <div class="form-floating  mb-3">
                                                <%--<span class="input-group-text" id="MainCategoryspnId">Bulk Product</span>--%>
                                                <%--<label class="input-group-text">Bulk Product</label>--%>

                                                <asp:DropDownList ID="BulkproductDropDown" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group-text mb-3">
                                                <asp:CheckBox ID="ChkIsPackingMaster" OnCheckedChanged="ChkIsPackingMaster_CheckedChanged" AutoPostBack="true" Checked="false" CssClass="form-check-inline" runat="server" />

                                                <div class=" disabled">
                                                    <span class="input-group-text" id="PackingLossPer">is Master Packing?</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="CostInformationtxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Cost Informaion</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:TextBox ID="AdditionalCosttxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Additional Cost</label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:TextBox ID="FactoryExpencePertxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">FactoryExpence(%)</label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:TextBox ID="MarketByChargestxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Marketed By Charges</label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:TextBox ID="OtherPertxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Other (%)</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="ProfitPertxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Profit (%)</label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="TotalAmountPerUnitttxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Total Amount / Unit</label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="TotalAmountPerLiterttxt" ClientIDMode="Static" Text="0" AutoPostBack="true" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                <label for="SupervisorsReqired">Total Amount/ Liter</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-10 col-lg-2 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="PriceListAddBtn" OnClick="PriceListAddBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                                <asp:Button ID="PriceListUpdateBtn" OnClick="PriceListUpdateBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-info btn-block" Visible="false" runat="server" Text="Update" />

                                            </div>

                                        </div>
                                        <div class=" col-10 col-lg-2 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="PriceListCancelBtn" OnClick="PriceListCancelBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="card-body" style="overflow: auto">

                                    <asp:GridView ID="Grid_CompanyPriceList" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="Cost_Information" HeaderText="Cost Information" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Addition_Cost" HeaderText="Addition Cost" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Total_Liter_Amount" HeaderText="Total Amount / Ltr" SortExpression="Source" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="GridEditCompanyPriceListBtn" OnClick="GridEditCompanyPriceListBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="EditBPM" Text="Edit" Width="100px" runat="server" class="btn btn-success btn-sm" />

                                                    <asp:Button ID="GridDelCompanyPriceListBtn" OnClick="GridDelCompanyPriceListBtn_Click" Visible="false" Text="Delete" Width="100px" runat="server" class="btn btn-danger btn-sm mt-1" />
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
