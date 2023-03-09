<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Production_Costing_Software.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                       <%--Lables--%>
                            <asp:Label ID="lblCompanyMasterList_Name" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblUserMaster_Id" runat="server" Text="" Visible="false"></asp:Label>

                            <%-----------------------%>
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Welcome</h1>
                    <ol class="breadcrumb mb-4">
                        <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                    </ol>

                    <div class="card mb-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">

                        <div class="card-body">
                            <img src="Content/Images/AccessDiened.png" />
                          
                        </div>
                    </div>


                </div>

            </main>

        </div>
    </div>
</asp:Content>
