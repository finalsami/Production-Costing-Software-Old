<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MasterPacking.aspx.cs" Inherits="Production_Costing_Software.MasterPacking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_MasterPacking.ClientID %>";
        var ScrollHeight = 500;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth - 5;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                //if (headerCellWidths[i] >= gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                width = headerCellWidths[i];
                //}
                //else {
                //    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                //    }
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth);
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }

    </script>
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <div class="container-fluid px-4">
                    <h1 class="mt-4">Master Packing</h1>
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
                            Master Packing
                        </div>
                    
                            <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div class="row mb-4">
                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <label class="input-group-text">Bulk Product</label>
                                                    <asp:DropDownList ID="BulkproductDropdownlist" OnSelectedIndexChanged="BulkproductDropdownlist_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <label class="input-group-text">Packing Measurement</label>
                                                    <asp:DropDownList ID="PackingSizeDropDown" OnSelectedIndexChanged="PackingSizeDropDown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <asp:CheckBox ID="ChkIsPckingMaster" Checked="false" CssClass="form-check-inline" runat="server" />

                                                    <div class=" disabled">
                                                        <span class="input-group-text" id="PackingLossPer">is Master Packing?</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row  justify-content-center align-items-center mt-4">
                                            <div class=" col-10 col-lg-2 col-lg-3">
                                                <div class="d-grid">
                                                    <asp:Button ID="PackingMaster_AddBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PackingMaster_AddBtn_Click" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                                    <asp:Button ID="PackingMaster_UpdateBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PackingMaster_UpdateBtn_Click" CssClass="btn btn-info btn-block" Visible="false" runat="server" Text="Update" />
                                                </div>

                                            </div>
                                            <div class=" col-10 col-lg-2 col-lg-3">
                                                <div class="d-grid">
                                                    <asp:Button ID="PackingLossCancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PackingLossCancelBtn_Click" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblPackSize" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="card-body" style="overflow: auto">
                                        <asp:GridView ID="Grid_MasterPacking" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="MasterPacking_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" />
                                                <asp:BoundField DataField="BindPackingSize" HeaderText="Packing Size" SortExpression="Measurement" />


                                                <asp:CheckBoxField DataField="IsMasterPacking" HeaderText="IsMasterPacking" SortExpression="PackingLossPercent" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="EditMasterPacking" OnClick="EditMasterPacking_Click" Text="Edit" runat="server" class="btn btn-success " />--%>
                                                        <asp:Button ID="DelMasterPacking" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelMasterPacking_Click" Text="Delete" runat="server" class="btn btn-danger" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="lblPackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblMasterPacking_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblPackingLoss_Id" runat="server" Text="" Visible="false"></asp:Label>
                        

                    </div>
                </div>
            </main>
        </div>

    </div>
</asp:Content>
