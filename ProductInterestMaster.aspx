<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductInterestMaster.aspx.cs" Inherits="Production_Costing_Software.ProductInterestMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_ProductInterestMaster.ClientID %>";
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <%-----------------------------Lables---------------------%>
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
                    <%-----------------------%>
                </div>
                <div class="container-fluid px-4">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Bulk Product Interest Master</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                        <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                        <li class="breadcrumb-item"><a href="BulkProductMaster.aspx">Bulk Product Master</a></li>
                        <li class="breadcrumb-item"><a href="ProductInterestMaster.aspx">Product Interest Master</a></li>

                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Bulk Product Interest Master
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card-body">
                                    <div class="row mb-4">
                                        <div class="col-md-6">
                                            <div class="input-group mb-3">
                                                <label class="input-group-text">Bulk Product Name</label>
                                                <asp:DropDownList ID="BulkProductNameDropDown" OnSelectedIndexChanged="BulkProductNameDropDown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group mb-3">
                                                <div class="input-group mb-4">
                                                    <span class="input-group-text" id="BulkCostLtr">Bulk Cost / Ltr</span>
                                                    <asp:TextBox ID="BulkCostLtrtxt" Text="0" ReadOnly="true" class="form-control" type="number" runat="server"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-4">
                                                <span class="input-group-text" id="InterestPer">Interest (%)</span>
                                                <asp:TextBox ID="InterestPertxt" OnTextChanged="InterestPertxt_TextChanged" AutoPostBack="true" Text="0" class="form-control" type="number" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-4">
                                                <span class="input-group-text" id="InterestAmt">Interest Amount</span>
                                                <asp:TextBox ID="InterestAmttxt" Text="0" ReadOnly="true" class="form-control" type="number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-4">
                                                <span class="input-group-text" id="TotalAmount">Total Amount</span>
                                                <asp:TextBox ID="TotalAmounttxt" Text="0" ReadOnly="true" class="form-control" type="number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  justify-content-center align-items-center mt-4">

                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="BPM_Interest_AddBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="BPM_Interest_AddBtn_Click" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />
                                                <asp:Button ID="BPM_Interest_UpdateBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="BPM_Interest_UpdateBtn_Click" CssClass="btn btn-info btn-block" Visible="false" runat="server" Text="Update" />

                                            </div>

                                        </div>
                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="BPMInterestCancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="BPMInterestCancelBtn_Click" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="card-body mb-4 mt-4">
                                    <asp:GridView ID="Grid_ProductInterestMaster" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="BulkProduct_Interest_Master_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="FinalCostBulk" HeaderText="Final Cost Bulk" SortExpression="Measurement" />

                                            <asp:TemplateField HeaderText="Interest (%)">
                                                <ItemTemplate><%# Eval("Interest")%>% </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="InterestAmount" HeaderText="Interest Amount" SortExpression="Source" />
                                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" SortExpression="Source" />

                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditBulkProducationInterestBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditBulkProducationInterestBtn_Click" ValidationGroup="EditBPM" Text="Edit" runat="server" class="btn btn-success btn-sm" />

                                                    <asp:Button ID="Grid_DelBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Grid_DelBtn_Click" ValidationGroup="DelBPM" Text="Delete" runat="server" class="btn btn-danger  btn-sm" />
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Label ID="lblBulkProduct_Interest_Master_Id" runat="server" Text="" Visible="false"></asp:Label>


                    </div>
                </div>
            </main>
        </div>

    </div>

</asp:Content>
