<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="ProductCategoriesMaster.aspx.cs" Inherits="Production_Costing_Software.ProductCategoriesMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4">Product Categories</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="ProductCategoriesMaster.aspx">Product Categories</a></li>
                                <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Product Categories Name:
                           
                                </div>
                                <div class="card-body">

                                    <div class="container ">
                                        <div class="row  justify-content-center align-items-center mt-4">

                                            <div class="row mb-3">
                                                <div class="col-12  col-lg-6 offset-3">
                                                    <div class="input-group mb-3">
                                                        <span class="input-group-text " id="inputGroup-sizing-default">Product Category Name:</span>

                                                        <asp:TextBox ID="MainCategorytxt" Enabled="false" class="form-control " type="text" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row  justify-content-center align-items-center mt-4">

                                            <div class="col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="UpdateMainCategory" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Visible="false" Enabled="false" runat="server" Text="Submit" class="btn btn-success btn-block " />
                                                    <%--<asp:Button ID="MainCategorySub"Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" Enabled="false" Text="Submit" class="btn btn-primary btn-block " />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblMainCategory_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="mb-4 mt-4">

                                            <asp:GridView ID="Grid_ProductCategories" CssClass=" table-hover table-responsive gridview"
                                                GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                PagerStyle-CssClass="gridview_pager"
                                                AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="Product_Category_Id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProductCategoryName" HeaderText="ProductCategoryName" SortExpression="MainCategory_Name" />
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

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </main>

        </div>
    </div>

</asp:Content>
