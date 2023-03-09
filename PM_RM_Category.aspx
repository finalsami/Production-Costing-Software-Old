<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PM_RM_Category.aspx.cs" Inherits="Production_Costing_Software.PM_RM_Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=GirdPM_RMCategoryList.ClientID %>";
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
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">RM PM  Category</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="PM_RM_Category.aspx">PMRM Category</a></li>
                        <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            RM PM Category Name:
                           
                        </div>
                        <div class="card-body">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="container ">
                                        <div class="row  justify-content-center align-items-center mt-4">

                                            <div class="row mb-3">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group mb-3">
                                                        <span class="input-group-text disabled" id="inputGroup-sizing-default">PM RM Category Name:</span>

                                                        <asp:TextBox ID="PM_RMCategorytxt" class="form-control " type="text" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="input-group mb-3">
                                                        <asp:CheckBox ID="ChkIsShipper" Checked="false" AutoPostBack="true" OnCheckedChanged="ChkIsShipper_CheckedChanged" class="form-check-inline" Width="10px" Height="10px" runat="server" />

                                                        <span class="input-group-text " id="IsShipper">Is Shipper ?</span>
                                                        <%--<asp:TextBox ID="IsShippertxt" ReadOnly="true" class="form-control disabled" type="text" runat="server"></asp:TextBox>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row  justify-content-center align-items-center mt-4">

                                            <div class="col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="PM_RMCategoryUpdate" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PM_RMCategoryUpdate_Click" Visible="false" runat="server" Text="Update" class="btn btn-success btn-block " />
                                                    <asp:Button ID="PM_RMCategoryAdd" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="PM_RMCategoryAdd_Click" runat="server" Text="Submit" class="btn btn-primary btn-block " />

                                                </div>
                                            </div>

                                            <div class="col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelBtn_Click" runat="server" Text="Cancel" class="btn btn-warning btn-block " />

                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblPMRMCategory_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class=" mt-4">
                                            <asp:GridView ID="GirdPM_RMCategoryList" CssClass=" table-hover table-responsive gridview"
                                                GridLines="None"
                                                PagerStyle-CssClass="gridview_pager"
                                                AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="PM_RM_Category_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PM_RM_Category_Name" HeaderText="PM RM Category" SortExpression="PM_RM_Category_Name" />
                                                    <asp:CheckBoxField DataField="ChkIsShipper" HeaderText="Is Shipper ?" SortExpression="PM_RM_Category_Name" />
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <%--<asp:Button ID="DelPackingCategoryBtn" OnClick="DelPackingCategoryBtn_Click" ValidationGroup="Del" Text="Delete" Style="width: 80px" runat="server" class="btn btn-danger btn-sm" />--%>
                                                            <asp:Button ID="EditPMRMCategoryBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="EditPMRMCategoryBtn_Click" ValidationGroup="Edit" Text="Edit"  runat="server" class="btn btn-success btn-sm" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>

                        </div>
                    </div>

                </div>


            </main>
        </div>
    </div>
</asp:Content>
