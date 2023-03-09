<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BulkProductPriceList.aspx.cs" Inherits="Production_Costing_Software.BulkProductPriceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
            

                    <div class="container-fluid px-4">
                        <h1 class="mt-4">Bulk Product Price List</h1>
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                            <li class="breadcrumb-item"><a href="BulkProductMaster.aspx">Bulk Product Master</a></li>
                            <li class="breadcrumb-item"><a href="FormulationMaster.aspx">Formulation Master</a></li>
                            <li class="breadcrumb-item"><a href="BulkRecipe(BOM).aspx">Bulk Recipe (BOM)</a></li>
                            <li class="breadcrumb-item"><a href="BulkProductPriceList.aspx">Bulk Product Price List</a></li>

                        </ol>

                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                BulkRecipe(BOM)
                           
                            </div>
                            <div class="card-body">

                                <div class="row mb-3">
                                    <div class="col-md-2">
                                        <div class="input-group md-2">
                                            <asp:DropDownList ID="MainCategoryDropdown" AppendDataBoundItems="true" OnSelectedIndexChanged="MainCategoryDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-4">

                                        <asp:DropDownList ID="BulkProductDropDownList" AutoPostBack="true" class="form-select" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <%--<asp:TextBox ID="FromDatetxt" TextMode="DateTime" placeholder="From Date" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                        <asp:TextBox ID="FromDatetxt" runat="server" TextMode="Date" placeholder="From Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <%--<asp:TextBox ID="ToDatetxt"TextMode="DateTime"  placeholder="To Date" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                        <asp:TextBox ID="ToDatetxt" runat="server" TextMode="Date" placeholder="From Date" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>



                                <div class="row  justify-content-center align-items-center mt-4">

                                    <div class=" col-10 col-lg-4 col-lg-4">
                                        <div class="d-grid">
                                            <asp:Button ID="SearchPriceList" runat="server" Text="Search" class="btn btn-primary btn-block" />
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                
            </main>

        </div>
    </div>
</asp:Content>
