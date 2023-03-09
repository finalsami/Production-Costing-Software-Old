<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="WelcomePageCompany.aspx.cs" Inherits="Production_Costing_Software.WelcomePageCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                    <%--Lable--%>
            <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>

            <%------------------------%>
                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Welcome</h1>
                    <ol class="breadcrumb mb-4">
                        <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                    </ol>

                    <div class="card mb-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">

                        <div class="card-body">
                            <%--<img src="Content/LogoImage/emen%20logo.png" style="width: 250px; height: 60px" />--%>
                            <h5 class="card-title" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Welcome to <asp:Label style="color:green;font-size:larger" ID="lblCompanyName" runat="server" Text=""></asp:Label></h5>
                            <%--                            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>--%>
                            <%--<a href="#" class="btn btn-primary">Lets Calculate..</a>--%>
                        </div>
                    </div>


                </div>

            </main>

        </div>
    </div>
</asp:Content>
