<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PackingStyleCategory.aspx.cs" Inherits="Production_Costing_Software.WebForm8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=GridPackingStyleCategory_List.ClientID %>";
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Packing Size Category Master</h1>
                            <ol class="breadcrumb mb-4">
                                <%--                        <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>--%>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Packing Size Category Master
                           
                                </div>

                                    <div class="card-body">
                                        <div class="container ">
                                            <div class="row  justify-content-center align-items-center">
                                                <div class="row mb-3">

                                                    <div class="col-md-6">

                                                        <%--  <div class="input-group">
                                                        <span class="input-group-text" id="PackingName">Packing Style Category Name</span>
                                                        <asp:TextBox ID="PackingStyleCategoryNametxt" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="PackingStyleCategoryNametxt" runat="server" ForeColor="Red" ErrorMessage="* Packing Style Category Name"></asp:RequiredFieldValidator>--%>
                                                        <div class="form-floating">

                                                            <asp:DropDownList ID="PackingCategoryDropdownList" AutoPostBack="True"
                                                                CssClass="form-select" runat="server">
                                                            </asp:DropDownList>
                                                            <label for="PackingCategoryReqired" id="PackingCategorylabel" runat="server" placeholder="Packing Category">Packing Category</label>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-floating">
                                                            <asp:TextBox ID="PackingSizetxt" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                                                            <label for="LabourtxtReqired">PackingSize</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-floating">

                                                            <asp:DropDownList ID="PackingStyleMeasurementDropdownCombo" OnSelectedIndexChanged="PackingStyleMeasurementDropdownCombo_SelectedIndexChanged" AutoPostBack="True"
                                                                class="form-select" runat="server">
                                                            </asp:DropDownList>
                                                            <label for="LabourtxtReqired">Measurement</label>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row  justify-content-center align-items-center mt-4">
                                                <asp:Label ID="lblStyleCategoryID" Visible="false" runat="server" Text=""></asp:Label>
                                                 
                                                <div class=" col-6 col-lg-2 col-lg-2">
                                                    <div class="d-grid">
                                                        <asp:Button ID="EditStyleCategoryMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditStyleCategoryMaster_Click" Visible="false" class="btn btn-secondary btn-block" runat="server" Text="Update" />
                                                        <asp:Button ID="AddStyleCategoryMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddStyleCategoryMaster_Click" CssClass="btn btn-primary btn-block" runat="server" Text="Add" />

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
                                    <div class="card-body">
                                        <div class="input-group">
                                            <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged" placeholder="Search..." AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                            <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                            <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>

                                        <asp:GridView ID="GridPackingStyleCategory_List" DataKeyNames="PackingStyleCategory_Id" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" Autopostback="true" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PackingStyleCategoryName" HeaderText="Packing Style Category Name" SortExpression="PackingStyleCategory_Name" />
                                                <asp:BoundField DataField="PackingSize" HeaderText="PackingSize" SortExpression="PackingSize" />
                                                <asp:BoundField DataField="PackingMeasurement" HeaderText="PackingMeasurement" SortExpression="PackingMeasurement" />

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="DelRMBtn" Text="Delete" runat="server" class="btn btn-danger btn-sm" />--%>
                                                        <asp:Button ID="EditStyleCatBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="EditStyleCat" OnClick="EditStyleCatBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </main>
        </div>
    </div>
</asp:Content>
