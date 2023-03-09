<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PM_RM_Master.aspx.cs" Inherits="Production_Costing_Software.PM_RM_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        var GridId = "<%=GridPMRM_MasterList.ClientID %>";
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
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--Lables--%>
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
                        </div>
                        <%-----------------------%>

                        <div class="container-fluid px-4">
                            <h1 class="mt-4">PMRM Master</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="PM_RM_Category.aspx">PMRM Category</a></li>
                                <li class="breadcrumb-item"><a href="PM_RM_Master.aspx">PMRM Master</a></li>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    PM RM Master
                           
                                </div>



                                <div class="card-body">

                                    <div class="container ">
                                        <div class="row mb-3">
                                            <div class="  col-md-4">

                                                <div class="input-group mb-3">
                                                    <span class="input-group-text" id="RMCategoryDropdownList2">PM RM Category</span>
                                                    <asp:DropDownList ID="PMRMCategoryDropdownList" OnSelectedIndexChanged="PMRMCategoryDropdownList_SelectedIndexChanged" AutoPostBack="True"
                                                        class="form-select" runat="server">
                                                        <%--<asp:ListItem SelectedValue="0">Select</asp:ListItem>--%>
                                                    </asp:DropDownList>


                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group mb-3">
                                                    <span class="input-group-text" id="inputGroup-sizing-default">PM Name</span>
                                                    <asp:TextBox ID="PMNametxt" OnTextChanged="PMNametxt_TextChanged" AutoPostBack="true" ClientIDMode="Static" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="PMNametxt" ForeColor="Red" runat="server" ErrorMessage="* PM Name"></asp:RequiredFieldValidator>

                                            </div>
                                            <asp:Label runat="server" ID="lblPMRM_Category_Id" Visible="false" />
                                            <asp:Label runat="server" ID="lblPM_RM_Id" Visible="false" />

                                            <div class="col-md-4">
                                                <div class="input-group mb-3 ">
                                                    <span class="input-group-text" id="UnitDropdown">Measurement of Price</span>
                                                    <asp:DropDownList ID="UnitDropdownList" OnSelectedIndexChanged="UnitDropdownList_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" runat="server" class="form-select">
                                                        <%--<asp:ListItem SelectedValue="9">KG</asp:ListItem>--%>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mb-3">
                                            <div class="col-md-4" id="NoOfUnitDivhide" runat="server">
                                                <div class="input-group mb-3">
                                                    <span class="input-group-text" id="NoofUnitid">No of Unit</span>
                                                    <asp:TextBox ID="NoofUnittxt" ClientIDMode="Static" OnTextChanged="NoofUnittxt_TextChanged" AutoPostBack="true" class="form-control" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="NoofUnittxt" ForeColor="Red" runat="server" ErrorMessage="* No of Unit"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-md-4" id="TotalWeightDivhide" runat="server">
                                                <div class="input-group mb-3">
                                                    <span class="input-group-text" id="totalwightofunitid">Total Weight of Units</span>
                                                    <asp:TextBox ID="TotalWeightUnittxt" ClientIDMode="Static" OnTextChanged="TotalWeightUnittxt_TextChanged" AutoPostBack="true" class="form-control" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="PerKG">/KG</span>

                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TotalWeightUnittxt" ForeColor="Red" runat="server" ErrorMessage="*Total Weight Unit"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group mb-3 ">
                                                    <%--<span class="input-group-text" id="WeightMeasurementid">Weight Measurement</span>--%>
                                                    <%--       <asp:DropDownList ID="WeightMeasurementDropdown" Visible="false" runat="server" class="form-select">
                                                    </asp:DropDownList>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-4" id="PerUnitDivhide" runat="server">
                                                <div class="input-group mb-3">
                                                    <span class="input-group-text" id="PerUnitWeightid">Per Unit Weight</span>
                                                    <asp:TextBox ID="PerUnitWeighttxt" ClientIDMode="Static" ReadOnly="true" class="form-control" TextMode="Number" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="PerUnitWeighttxt" ForeColor="Red" runat="server" ErrorMessage="* Per Unit Weight"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-md-4" id="UnitsKGDivhide" runat="server">
                                                <div class="input-group mb-3">
                                                    <span class="input-group-text" id="UnitsPerKGtxtid">Units/KG</span>
                                                    <asp:TextBox ID="UnitsPerKGtxt" ReadOnly="true" class="form-control" TextMode="Number" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="UnitsPerKGtxt" ForeColor="Red" runat="server" ErrorMessage="* Units /KG"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row  justify-content-center align-items-center mt-4">

                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="Update_PMRMBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Update_PMRMBtn_Click" Visible="false" class="btn btn-success" runat="server" Text="Update" />

                                                <asp:Button ID="Add_PMRMbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Add_PMRMbtn_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                                            </div>
                                        </div>
                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="CancelBtn_Click" class="btn btn-warning" runat="server" Text="Cancel" />
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="card-body mt-4" style="overflow: auto">


                                    <hr />

                                    <div class="input-group">
                                        <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged" AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                        <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                        <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="txtSearch" FirstRowSelected="false" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>

                                    <asp:GridView ID="GridPMRM_MasterList" CssClass=" table-hover table-responsive gridview mt-1"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" DataKeyNames="PM_RM_Id" runat="server" Autopostback="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Category_Name" HeaderText="Category Name" SortExpression="Category_Name" />
                                            <asp:BoundField DataField="PM_Name" HeaderText="Name" SortExpression="PM_Name" />
                                            <asp:BoundField DataField="Price_KG_Unit" HeaderText="Price KG Unit " SortExpression="Price_KG_Unit" />
                                            <asp:BoundField DataField="No_Of_Unit" HeaderText="No Of Unit " SortExpression="No_Of_Unit" />
                                            <asp:BoundField DataField="Total_Weight_Of_Unit" HeaderText="Weight Of Unit " SortExpression="Total_Weight_Of_Unit" />
                                            <%--<asp:BoundField DataField="Weight_Measurement" HeaderText="Weight Measurement " SortExpression="Weight_Measurement" />--%>
                                            <asp:BoundField DataField="Per_Unit_Weight" HeaderText="Per Unit Weight " SortExpression="Per_Unit_Weight" />
                                            <asp:BoundField DataField="Unit_KG" HeaderText="Unit KG " SortExpression="Unit_KG" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Button ID="DelPMRMBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="DelPMRM" OnClick="DelPMRMBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                    <asp:Button ID="EditPMRMBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="EditPMRMB" Text="Edit" OnClick="EditPMRMBtn_Click" runat="server" class="btn btn-success btn-sm" />

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
