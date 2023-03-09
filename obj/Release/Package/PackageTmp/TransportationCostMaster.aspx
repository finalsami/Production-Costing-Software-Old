<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TransportationCostMaster.aspx.cs" Inherits="Production_Costing_Software.TransportationCostMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">
        <div id="layoutSidenav_content">
            <main>
            
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid px-4">
                                <h1 class="mt-4">Transportation Cost Master</h1>
                                <ol class="breadcrumb mb-4">
                                    <li class="breadcrumb-item"><a href="TransportationCostFactors.aspx">Transportation Cost Master</a></li>
                                </ol>

                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Transportation Cost Master
                           
                                    </div>
                                    <div class="card-body">

                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <div class="form-floating">
                                                    <asp:DropDownList ID="BulkProductDropdownList" OnSelectedIndexChanged="BulkProductDropdownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server"></asp:DropDownList>
                                                    <label for="BulkProductDropdownList">Bulk Product Name</label>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-floating">
                                                    <asp:DropDownList ID="PackingStyleDropdownList" OnSelectedIndexChanged="PackingStyleDropdownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server"></asp:DropDownList>
                                                    <label for="PackingStyleDropdownList">Packing Style</label>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-floating">
                                                    <asp:DropDownList ID="PackingSizeDropdownList" OnSelectedIndexChanged="PackingSizeDropdownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server"></asp:DropDownList>
                                                    <label for="PackingSizeDropdownList">Packing Size</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="BoxWeightPerKGtxt" ClientIDMode="Static" class="form-control" runat="server" TextMode="Number" placeholder="Box weight (KG)"></asp:TextBox>
                                                    <label for="Box weight (KG)">Box weight (KG)</label>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="BoxWeightPerKGtxt" runat="server" ForeColor="Red" ErrorMessage="* Box weight (KG) !"></asp:RequiredFieldValidator>
                                            </div>
                                            <%--                                    <div class="col-md-4">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="TransportationFactToDepottxt" ClientIDMode="Static" class="form-control" runat="server" TextMode="Number" placeholder="Transportation (Fact-Depot)"></asp:TextBox>
                                                    <label for="TransportationFactToDepot">Transportation (Fact-Depot)</label>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TransportationFactToDepottxt" runat="server" ForeColor="Red" ErrorMessage="* Average Local Traspoation !"></asp:RequiredFieldValidator>
                                            </div>--%>
                                        </div>
                                        <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPack_Size" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>

                                        <div class="row  justify-content-center align-items-center mt-4">

                                            <div class=" col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="UpdateTransportationCostMasterbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateTransportationCostMasterbtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-success btn-block" />
                                                    <asp:Button ID="AddTransportationCostMasterbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddTransportationCostMasterbtn_Click" runat="server" Text="Add" class="btn btn-primary btn-block" />
                                                </div>
                                            </div>
                                            <div class=" col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">

                                                    <asp:Button ID="CancalTransportationBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancalTransportationBtn_Click" CausesValidation="false" runat="server" Text="Cancel" class="btn btn-secondary btn-block" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="card-body mb-4 overflow-auto">
                                        <asp:GridView ID="Grid_TransportationCostMaster" Autopostback="true" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter"
                                            runat="server" AutoGenerateColumns="False" DataKeyNames="TransportationCostMaster_Id" OnRowDataBound="Grid_TransportationCostFactors_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="Product Name" SortExpression="MainCategory" />

                                                <asp:BoundField DataField="PackingStyle" HeaderText="Packing Style" SortExpression="BulkProductName" />
                                                <asp:BoundField DataField="PackingSize" HeaderText="Packing Size" SortExpression="Measurement" />

                                                <asp:BoundField DataField="BoxWeightPerKg" HeaderText="Box Weight/Kg" SortExpression="BatchSize" />
                                                <%--                           <asp:BoundField  HeaderText="Gujarat" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Madhya-Pradesh" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Rajasthan" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="West-Bengal" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Chhattisgarh" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Uttar-Pradesh" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Odisha" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Bihar" SortExpression="BatchSize" />
                                                <asp:BoundField  HeaderText="Assam" SortExpression="BatchSize" />--%>


                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelTransportationCostMasterBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClientClick="return confirm('Are you sure you want to delete this item?');" OnClick="DelTransportationCostMasterBtn_Click" ValidationGroup="Del" Text="Delete" runat="server" class="btn btn-danger btn-sm" />

                                                        <asp:Button ID="EditTransportationCostFactorsBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClick="EditTransportationCostFactorsBtn_Click" ValidationGroup="Edit" Text="Edit" runat="server" class="btn btn-success btn-sm" />

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
