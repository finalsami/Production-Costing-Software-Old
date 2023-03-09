<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="CategoryMappingMaster.aspx.cs" Inherits="Production_Costing_Software.CategoryMappingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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
                    <h1 class="mt-4"  style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Trade Name & Category Mapping</h1>
                    <ol class="breadcrumb mb-4">
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Trade Name & Category Mapping
                           
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="ProductCategoryDropDown" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="ProductCategory">ProductCategory</label>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="TradeNameDropdown" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="TradeName">Trade Name</label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="BulkProductNameDropDownList" OnSelectedIndexChanged="BulkProductNameDropDownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="TradeName">Bulk Product Name</label>


                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblTradeName_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblProductCategory_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPMRM_Category_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPAckSize" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPackMeasurement" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblCompany_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblBPM_Id" Visible="false" runat="server" Text="Label"></asp:Label>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-10 col-lg-2 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="AddCategoryMapping" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary btn-block" OnClick="AddCategoryMapping_Click" runat="server" Text="Add" />
                                                <asp:Button ID="UpdateCategoryMapping" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-info btn-block" OnClick="UpdateCategoryMapping_Click" Visible="false" runat="server" Text="Update" />
                                            </div>
                                        </div>
                                        <div class=" col-10 col-lg-2 col-lg-4">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelCategoryMapping" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelCategoryMapping_Click" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <asp:Label ID="lblCategoryMapping_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <div class="card-body  mb-4 overflow-auto">

                                    <asp:GridView ID="Grid_CategoryMappingMaster" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryMapping_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="ProductCategoryName" HeaderText="ProductCategory" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="TradeName" HeaderText="TradeName" SortExpression="TradeName" />
                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product Name" SortExpression="BulkProductName" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditCategoryMapping" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditCategoryMapping_Click" Text="Edit" Width="80px" runat="server" class="btn btn-success btn-sm" />

                                                    <asp:Button ID="DelCategoryMapping" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelCategoryMapping_Click" Text="Delete" Width="80px" runat="server" class="btn btn-danger btn-sm " />
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
