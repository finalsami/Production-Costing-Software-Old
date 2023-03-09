<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PackingLossMaster.aspx.cs" Inherits="Production_Costing_Software.PackingLossMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Packing Loss</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                        <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                        <li class="breadcrumb-item"><a href="BulkProductMaster.aspx">Bulk Product Master</a></li>
                        <li class="breadcrumb-item"><a href="ProductInterestMaster.aspx">Product Interest Master</a></li>
                        <li class="breadcrumb-item"><a href="PackingLossMaster.aspx">Packing Loss</a></li>

                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Packing Loss
                        </div>
                    
                            <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div class="row mb-4">
                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <div class="input-group mb-4">
                                                        <span class="input-group-text" id="PackingLoss">Packing Size</span>
                                                        <asp:TextBox ID="PackingSizetxt" Text="0" class="form-control" type="number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="PackingSizetxt" runat="server" ForeColor="Red" ErrorMessage="* PackingSize! "></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <label class="input-group-text">Packing Measurement</label>
                                                    <asp:DropDownList ID="PackingSizeDropDown" OnSelectedIndexChanged="PackingSizeDropDown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                        <asp:ListItem Selected="True">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <div class="input-group mb-4">
                                                        <span class="input-group-text" id="PackingLossPer">Packing Loss (%)</span>
                                                        <asp:TextBox ID="PackingLosstxt" Text="0" class="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="offset-1" ControlToValidate="PackingLosstxt" runat="server" ForeColor="Red" ErrorMessage="* PackingLoss! "></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row  justify-content-center align-items-center mt-4">
                                            <div class=" col-10 col-lg-2 col-lg-3">
                                                <div class="d-grid">
                                                    <asp:Button ID="PackingLoss_AddBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PackingLoss_AddBtn_Click" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                                    <asp:Button ID="PackingLoss_UpdateBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PackingLoss_UpdateBtn_Click" CssClass="btn btn-info btn-block" Visible="false" runat="server" Text="Update" />
                                                </div>

                                            </div>
                                            <div class=" col-10 col-lg-2 col-lg-3">
                                                <div class="d-grid">
                                                    <asp:Button ID="PackingLossCancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="overflow: auto">
                                        <asp:GridView ID="Grid_PackingLossMaster" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="PackingLoss_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PackSize" HeaderText="Pack Size" SortExpression="MainCategory_Name" />
                                                <asp:BoundField DataField="PackMeasurement" HeaderText="Pack Measurement" SortExpression="Measurement" />
                                                <asp:TemplateField HeaderText="Packing Loss (%)">
                                                    <ItemTemplate>
                                                        <%# Eval("PackingLossPercent")%>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="PackingLossPercent" HeaderText="Packing Loss (%)" SortExpression="PackingLossPercent" />--%>
                                                <asp:BoundField DataField="AsOnDate" HeaderText="As On Date" SortExpression="Measurement" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="EditPackLossBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditPackLossBtn_Click" Text="Edit" runat="server" class="btn btn-success " />
                                                        <asp:Button ID="DelPackLossBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelPackLossBtn_Click" Text="Delete" runat="server" class="btn btn-danger" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblPackingLoss_Id" runat="server" Text="" Visible="false"></asp:Label>
                        

                    </div>
                </div>
            </main>
        </div>

    </div>
</asp:Content>
