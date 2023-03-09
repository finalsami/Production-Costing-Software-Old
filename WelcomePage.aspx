<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="WelcomePage.aspx.cs" Inherits="Production_Costing_Software.WelcomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>

                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Welcome</h1>
                    <ol class="breadcrumb mb-4">
                        <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                    </ol>

                    <div class="card mb-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">

                        <div class="card-body">
                            <%--<img src="Content/LogoImage/emen%20logo.png" style="width:250px;height:60px" />--%>
                            <h5 class="card-title" text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);>Welcome To Product Costing Software</h5>
<%--                            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>--%>
                            <%--<a href="#" class="btn btn-primary">Lets Calculate..</a>--%>
                        </div>
                    </div>


                </div>

            </main>

        </div>
    </div>
</asp:Content>
