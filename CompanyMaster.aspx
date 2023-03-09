<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="Production_Costing_Software.CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
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

                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Company Master</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="CompanyMaster.aspx">Company Master</a></li>

                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Company Master
                                </div>

                                <%--*********************** Lables************************--%>
                                <asp:Label ID="lblCompanyMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <%--**************************************************************--%>



                                <div class="card-body">

                                    <div class="container ">
                                        <div class="row  justify-content-center align-items-center">
                                            <div class="row mb-3">
                                                <div class="col-md-3 mt-4">
                                                </div>
                                                <div class="col-md-6 mt-4">
                                                    <div class="input-group mb-3">
                                                        <span class="input-group-text" id="inputGroup-sizing-default">Company Name</span>
                                                        <asp:TextBox ID="CompanyNametxt" class="form-control" ClientIDMode="Static" type="text" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="CompanyNametxt" runat="server" ForeColor="Red" ErrorMessage="* Company Name"></asp:RequiredFieldValidator>

                                                </div>

                                            <%--    <div class="col-md-3  mt-4">

                                                    <div class="input-group mb-3">
                                                        <label class="input-group-text" for="LogoUpload">

                                                            <asp:FileUpload runat="server" ID="LogoUploadId" />
                                                            <asp:Button ID="ImageUpload" Enabled="false" OnClick="ImageUpload_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-dark" runat="server" Text="Upload" />

                                                        </label>

                                                    </div>

                                                </div>--%>

                                            </div>

                                        </div>
                                        <div class="row  justify-content-center align-items-center mt-4">

                                           <div class=" col-6 col-lg-2 col-lg-2">
                                                <div class="d-grid">
                                                    <asp:Button ID="UpdateCompanyBtn" OnClick="UpdateCompanyBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Visible="false" class="btn btn-primary" runat="server" Text="Update" />
                                                    <asp:Button ID="AddCompanyBtn" OnClick="AddCompanyBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary" runat="server" Text="Add" />
                                                </div>
                                            </div>
                                            <div class=" col-6 col-lg-2 col-lg-2">
                                                <div class="d-grid">

                                                    <asp:Button ID="CancelcompanyBtn" OnClick="CancelcompanyBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-info" runat="server" Text="Cancel" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="mt-3">

                                        <asp:GridView ID="Grid_CompanyMasterList" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" DataKeyNames="CompanyMaster_Id" Autopostback="true" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyMaster_Name" HeaderText="Company Name" SortExpression="CategoryName" />
                                                <asp:BoundField DataField="AsOnDate" HeaderText="As On Date" SortExpression="CategoryName" />

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelGridBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' ValidationGroup="Del" OnClick="DelGridBtn_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="EditGridBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="EditGridBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Edit" ValidationGroup="EditRM" runat="server" class="btn btn-success btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
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
