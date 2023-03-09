<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BulkProductMaster.aspx.cs" EnableEventValidation="true" Inherits="Production_Costing_Software.BulkProductMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#IngredientModal").modal("show");
        }
    </script>
    <script type="text/javascript">

        var GridId = "<%=GridBulkProductMasterList.ClientID %>";
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
                <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Bulk Product Master</h1>
                    <ol class="breadcrumb mb-4">
                        <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                        <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                        <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                        <li class="breadcrumb-item"><a href="BulkProductMaster.aspx">Bulk Product Master</a></li>

                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Bulk Product Master
                           
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card-body">



                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <div class="input-group mb-3">
                                                <label class="input-group-text">Main Category</label>
                                                <asp:DropDownList ID="MainCategoryDropDown" AutoPostBack="true" OnSelectedIndexChanged="MainCategoryDropDown_SelectedIndexChanged" class="form-select" runat="server">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group mb-3">
                                                <label class="input-group-text">Measurement</label>
                                                <asp:DropDownList ID="EnumMasterMeasurementDropdown" class="form-select" runat="server">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblBPM_Id" Visible="false" runat="server" Text="Label"></asp:Label>
                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <div class="input-group mb-3">
                                                <span class="input-group-text" id="inputGroup-sizing-default">Bulk Product Name</span>
                                                <asp:TextBox ID="BulkProductNametxt" OnTextChanged="BulkProductNametxt_TextChanged" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="BulkProductNametxt" runat="server" ForeColor="Red" ErrorMessage="* BulkProductName"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group mb-3">
                                                <label class="input-group-text" for="SourceDropdown">Source</label>
                                                <asp:DropDownList ID="SourceDropdown" class="form-select" runat="server">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group mb-3">
                                                <label class="input-group-text" for="SourceDropdown">GST %</label>
                                                <asp:DropDownList ID="GST_Dropdown" class="form-select" runat="server">
                                                    <asp:ListItem Selected="True">Select</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row  justify-content-center align-items-center mt-4">

                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="BPMAddBtn_Id" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary btn-block" OnClick="BPMAddBtn_Id_Click" runat="server" Text="Add" />
                                                <asp:Button ID="BPMUpdateBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-info btn-block" OnClick="BPMUpdateBtn_Click" Visible="false" runat="server" Text="Update" />

                                            </div>

                                        </div>
                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="BPMCancel_Id" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="BPMCancel_Id_Click" CssClass="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="card-body" style="overflow: auto">

                                    <asp:GridView ID="GridBulkProductMasterList" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="BPM_Product_Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="MainCategory_Name" HeaderText="MainCategory_Name" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="Measurement" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="BulkProductName" HeaderText="BulkProductName" SortExpression="BulkProductName" />
                                            <asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" />
                                            <asp:BoundField DataField="Gst_Percent" HeaderText="GST (%)" SortExpression="Source" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="EditBulkProducationBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="EditBPM" OnClick="EditBulkProducationBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />

                                                    <%--<asp:Button ID="DelFormulationBtn" Visible="false" Text="Delete" Width="100px" runat="server" class="btn btn-danger btn-sm mt-1" />--%>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                </div>
            </main>
        </div>

    </div>


</asp:Content>
