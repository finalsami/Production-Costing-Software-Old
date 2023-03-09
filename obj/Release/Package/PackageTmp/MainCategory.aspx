<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MainCategory.aspx.cs" Inherits="Production_Costing_Software.MainCategory" %>

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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Main Category</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MainCategory.aspx">Main Category</a></li>
                                <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                            </ol>

                            <div class="card mb-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                <div class="card-header"  style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">
                                    <i class="fas fa-table me-1"></i>
                                    Main Category Name:
                           
                                </div>
                                <div class="card-body">


                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group mb-3">
                                                    <span class="input-group-text " id="inputGroup-sizing-default"  style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Main Category Name:</span>

                                                    <asp:TextBox ID="MainCategorytxt" class="form-control " type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="UpdateMainCategory" Visible="false" runat="server" OnClick="UpdateMainCategory_Click" Text="Submit" class="btn btn-success btn-block " />
                                                <asp:Button ID="MainCategorySub" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" OnClick="MainCategorySub_Click" Text="Submit" class="btn btn-primary btn-block " />

                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblMainCategory_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class=" mt-4">

                                        <asp:GridView ID="GirdMainCategoryList" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" runat="server" AutoGenerateColumns="False" DataKeyNames="PkMainCategory_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MainCategory_Name" HeaderText="Main Category" SortExpression="MainCategory_Name" />
                                                <%--     <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:Button ID="Edit" OnClick="Edit_Click" runat="server" class="btn btn-success btn-sm" Text="Edit" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>--%>
                                            </Columns>

                                        </asp:GridView>



                                    </div>
                                </div>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </main>

        </div>
    </div>


</asp:Content>
