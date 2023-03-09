<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RMCategory.aspx.cs" Inherits="Production_Costing_Software.RMCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">RM Category</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="MainCategory.aspx">Main Category</a></li>
                        <li class="breadcrumb-item"><a href="MainCategory.aspx">RM Category</a></li>
                        <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            RM Category Name:
                           
                        </div>
                        <div class="card-body">

                            <div class="container ">
                                <div class="row  justify-content-center align-items-center mt-4">
                                    <div class="row mb-3">
                                        <div class="col-md-6 offset-3">
                                            <div class="input-group mb-3">
                                                <span class="input-group-text" id="inputGroup-sizing-default">RM Category Name</span>

                                                <asp:TextBox ID="RMCategoryNametxt" class="form-control disabled" type="text" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row  justify-content-center align-items-center mt-4">

                                            <div class=" col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="RMCategorySub" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="RMCategorySub_Click" runat="server" Text="Submit" class="btn btn-primary btn-block " />

                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>

                            <div class="mt-4">

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:GridView ID="GirdRMCategoryList" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="RM_Category_id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RM_Category_Name" HeaderText="RM Category Name" SortExpression="RM_Category_Name" />
                                            </Columns>

                                        </asp:GridView>


                                        <%--<asp:SqlDataSource ID="RMCategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [pcs_RM_Category]"></asp:SqlDataSource>--%>
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
