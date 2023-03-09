<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="comp_ProductMaster.aspx.cs" Inherits="Production_Costing_Software.comp_ProductMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Co. Product Master</h1>
                    <ol class="breadcrumb mb-4">
                        <%--                        <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>--%>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Co. Product Master
                           
                        </div>
                    
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div class="container ">
                                            <div class="row  justify-content-center align-items-center">
                                                <div class="row mb-4">

                                                    <div class="col-md-6">
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="BulkproductDropdownList" AutoPostBack="True"
                                                                class="form-select" runat="server">
                                                            </asp:DropDownList>
                                                            <label for="BulkproductDropdownList" id="Bulk" runat="server">Bulk product</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="TradeNameDropdownList" AutoPostBack="True"
                                                                class="form-select" runat="server">
                                                            </asp:DropDownList>
                                                            <label for="TradeNameDropdownList" id="trade" runat="server">Trade Name</label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row  justify-content-center align-items-center mt-4">

                                                <div class="col-12 col-md-4 col-md-4">
                                                    <div class="d-grid">
                                                        <asp:Button ID="UpdateProductMaster" OnClick="UpdateProductMaster_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Visible="false" class="btn btn-secondary btn-block" runat="server" Text="Update" />
                                                        <asp:Button ID="AddProductMaster" OnClick="AddProductMaster_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />

                                                    </div>
                                                </div>
                                                <div class="col-12 col-md-4 col-md-4">
                                                    <div class="d-grid">
                                                        <asp:Button ID="CancelBtn" OnClick="CancelBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <asp:Label ID="lblcomp_ProductMaster_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <div class="card-body">
                                        <asp:GridView ID="Grid_comp_ProductMasterList" DataKeyNames="comp_ProductMaster_Id" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" Autopostback="true" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product" SortExpression="PackingSize" />
                                                <asp:BoundField DataField="comp_TradeName" HeaderText="Trade Name" SortExpression="PackingStyleCategory_Name" />

                                                <asp:BoundField DataField="AsOnDate" HeaderText="As On Date" SortExpression="PackingMeasurement" />

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="GridDelProductMasterBtn" OnClick="GridDelProductMasterBtn_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="GridEditProductMasterBtn" OnClick="GridEditProductMasterBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="EditStyleCat" Text="Edit" runat="server" class="btn btn-success btn-sm" />

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
