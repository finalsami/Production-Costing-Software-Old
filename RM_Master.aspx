<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RM_Master.aspx.cs" Inherits="Production_Costing_Software.RM_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var GridId = "<%=GridRM_MasterList.ClientID %>";
        var ScrollHeight = 500;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
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
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
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
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">RM Master</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                                <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                                <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            </ol>
                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    RM Master                                                          
                                </div>
                                <div class="card-body">
                                    <div class="container ">
                                        <div class="row  justify-content-center align-items-center">
                                            <div class="row mb-3">
                                                <div class="  col-md-6">
                                                    <div class="input-group mb-3">
                                                        <span class="input-group-text" id="RMCategoryDropdownList2">RM Category</span>
                                                        <asp:DropDownList ID="RMCategoryDropdownList" AutoPostBack="True" class="form-select" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group mb-3">
                                                        <span class="input-group-text" id="inputGroup-sizing-default">RM Name</span>
                                                        <asp:TextBox ID="RmNametxt" class="form-control" OnTextChanged="RmNametxt_TextChanged" AutoPostBack="true" ClientIDMode="Static" type="text" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="RmNametxt" runat="server" ForeColor="Red" ErrorMessage="* RM Name"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row  justify-content-center align-items-center">
                                            <div class="row mb-3">
                                                <div class="col-md-6">
                                                    <div class="input-group mb-3 ">
                                                        <span class="input-group-text" id="UnitDropdown">Measurement</span>
                                                        <asp:DropDownList ID="UnitDropdownList" runat="server" class="form-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group-text">
                                                        <div class="form-check">
                                                            <span class="input-group" id="PurityRequired">IsPurityRequired</span>
                                                            <%--      <asp:RadioButtonList ID="IsPurityRequired" runat="server" AutoPostBack="True">
                                                                <asp:ListItem Value="1" Selected="True">-Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">-No</asp:ListItem>
                                                            </asp:RadioButtonList>--%>

                                                            <div class="form-check form-switch">

                                                                <input type="checkbox" id="IsPurityRequired" checked="checked" runat="server" class="form-check-input ">
                                                                <%--<asp:CheckBox ID="IsPurityRequired" runat="server" CssClass="form-check-input " Checked="false" />--%>
                                                                <label class="form-check-label" for="flexSwitchCheckDefault">Yes/No</label>
                                                            </div>



                                                            <asp:Label ID="lblRM_Id" runat="server" Visible="false" Text=""></asp:Label>
                                                            <asp:Label ID="lblCategory_Id" runat="server" Visible="false" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row  justify-content-center align-items-center mt-4">
                                                <div class=" col-10 col-lg-4 col-lg-4">
                                                    <div class="d-grid">
                                                        <asp:Button ID="EditRM" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EditRM_Click" Visible="false" class="btn btn-success" runat="server" Text="Update" />
                                                        <asp:Button ID="AddRmMaster" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddRmMaster_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                                                    </div>
                                                </div>
                                                <div class=" col-10 col-lg-4 col-lg-4">
                                                    <div class="d-grid">
                                                        <asp:Button ID="Cancel" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Cancel_Click" CssClass="btn btn-info " runat="server" Text="Cancel" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="mt-2">
                                        <div class="input-group">
                                            <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged"
                                                AutoPostBack="true" runat="server" CssClass="col-md-4" BorderStyle="Inset" />

                                            <asp:Button ID="CancelSearch" runat="server" Text="X" OnClick="CancelSearch_Click" CausesValidation="false" />
                                            <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>
                                        <%--GridRM_MasterList--%>
                                        <asp:GridView ID="GridRM_MasterList" CssClass=" table-hover table-responsive gridview Freezing"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); margin-top: 8px"
                                            AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" OnPageIndexChanging="GridRM_MasterList_PageIndexChanging" runat="server" DataKeyNames="RM_Id" Autopostback="true" OnRowDeleting="GridRM_MasterList_RowDeleting" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RM_CategoryName" HeaderText="RM-CategoryName" SortExpression="CategoryName" />
                                                <asp:BoundField DataField="RM_Name" HeaderText="RM Name" SortExpression="RM_Name" />
                                                <asp:TemplateField HeaderText="sPurityRequired">
                                                    <ItemTemplate>
                                                        <div class="form-check form-switch" style="margin-left: 65px">
                                                            <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefault">--%>
                                                            <input type="checkbox" runat="server" disabled class="form-check-input " checked='<%#  Eval("IsPurityRequired") %>'>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:CheckBoxField DataField="IsPurityRequired" HeaderText="IsPurityRequired ?" SortExpression="RM_IsPurityRequired" />--%>
                                                <asp:BoundField DataField="Unit" HeaderText="Unit " SortExpression="Fk_EnumMaster_UnitMeasurement" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelRMBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelRMBtn_Click" ValidationGroup="DelRM" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="EditRMBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Edit" OnClick="EditRMBtn_Click" ValidationGroup="EditRM" runat="server" class="btn btn-success btn-sm" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%---------------------------------------%>
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
