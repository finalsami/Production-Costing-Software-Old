<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PackingStyleName.aspx.cs" Inherits="Production_Costing_Software.PackingStyleName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Packing Style Name</h1>
                    <ol class="breadcrumb mb-4">
                        <%-- <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>--%>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Packing Style Name
                           
                        </div>


                        <contenttemplate>
                            <div class="card-body">

                                <div class="container ">
                                    <div class="row  justify-content-center align-items-center">
                                        <div class="row mb-3 ">
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-5">
                                                <div class="input-group mb-3 mt-4">
                                                    <span class="input-group-text" id="inputGroup-sizing-default">Packing Style Name</span>
                                                    <asp:TextBox ID="PackingStyleNametxt" OnTextChanged="PackingStyleNametxt_TextChanged" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblPackingstyle_Id" Visible="false" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="UpdatePSNMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdatePSNMaster_Click" Visible="false" class="btn btn-success btn-block" runat="server" Text="Update" />

                                                <asp:Button ID="AddPSNMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddPSNMaster_Click" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                            </div>
                                        </div>
                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelBtn_Click" class="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </div>
                    <div class="card-body">


                        <asp:GridView ID="GridPackingStyleNameMasterList" DataKeyNames="PackingStyleName_Id" CssClass=" table-hover table-responsive gridview"
                            GridLines="None" PageSize="200" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                            AllowPaging="true" PagerStyle-CssClass="gridview_pager" OnPageIndexChanging="GridPackingStyleNameMasterList_PageIndexChanging"
                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" Autopostback="true" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PackingStyleName" HeaderText="Packing Style Name" SortExpression="CategoryName" />


                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Button ID="DelPackingStyleBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelPackingStyleBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                        <asp:Button ID="EditPackingBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditPackingBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />

                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </main>
        </div>
    </div>
</asp:Content>
