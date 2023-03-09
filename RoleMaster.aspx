<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RoleMaster.aspx.cs" Inherits="Production_Costing_Software.RoleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Content/Js/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#RIGHTSModal").modal("show");
        }          </script>
    <script type="text/javascript">
        function HidePopup(title, body) {
            $("#RIGHTSModal").modal("hide");
        }          </script>
    <script type="text/javascript">
        function ShowPopup1(title, body) {
            $("#staticBackdrop").modal("show");
        }
    </script>
    <script type="text/javascript">
        function HidePopup1(title, body) {
            $("#staticBackdrop").modal("hide");
        }
    </script>

    <%--<script type="text/javascript">  
        function StateCity(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "../Other/add.png") {

                $("#" + displayIcon).closest("tr")
                    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
                        .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "../image/hide.jpg");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "../Other/Minus.png");
            }
        }
    </script>--%>
    <link href="Content/CSS/ToggleIsActive.css" rel="stylesheet" />

    <div id="layoutSidenav">
        <div id="layoutSidenav_content">
            <main>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h2 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Role Master:</h2>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MainCategory.aspx">Role Master</a></li>
                                <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                            </ol>
                            <%--Lables--%>
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
                            <div class="card mb-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Role:
                                  
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-1">
                                            <%--  <asp:Button ID="AddUser" class="btn btn-primary" runat="server" Text="Button" data-bs-toggle="modal" data-bs-target="#staticBackdrop" />--%>
                                            <button type="button" runat="server" id="AddUser" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                                Add</button>

                                        </div>

                                    </div>
                                </div>

                                <!-- Modal -->
                                <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-fullscreen-md-down">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="staticBackdropLabel">Role Information:</h5>
                                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row mb-3">
                                                    <div class="col-md-6 ">
                                                        <div class="form-floating">
                                                            <asp:TextBox ID="RoleNametxt" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                                                            <label for="RoleNametxt">RoleName</label>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="RoleNametxt" runat="server" ForeColor="Red" ErrorMessage="* RoleName "></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="ActDAct" runat="server" Text="Active/DeActive"></asp:Label>

                                                        <div class="input-group">
                                                            <%--              <label class="switch">


                                                                <input type="checkbox" checked runat="server" id="IsActive" />
                                                                <span class="slider round"></span>
                                                            </label>--%>
                                                            <div class="form-check form-switch">
                                                                <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefault">--%>
                                                                <input type="checkbox" id="IsActive" runat="server" class="form-check-input ">
                                                                <%--<label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>--%>
                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-dismiss="modal">Close</button>
                                                <%--<button type="button" class="btn btn-primary">Submit</button>--%>
                                                <asp:Button ID="AddRoleBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddRoleBtn_Click" runat="server" Text="Add" data-bs-dismiss="modal" class="btn btn-primary btn-block" />
                                                <asp:Button ID="UpdateRoleBtn" Visible="false" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateRoleBtn_Click" Text="Update" CausesValidation="false" data-bs-dismiss="modal" runat="server" class="btn btn-info btn-block" />

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <%-------------------------------------%>

                                <%--Rights Modal--%>
                                <div class="modal fade" id="RIGHTSModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">

                                    <div class="modal-dialog modal-xl">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="RoleRights">Rights Information:</h5>
                                                <h4>[
                                                    <asp:Label ID="lblRoleName" runat="server" Text=""></asp:Label>
                                                    ]</h4>
                                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                            </div>

                                            <div class="modal-body" style="overflow:scroll">
                                                <%--<a class="font-monospace" style="font-size: large">Product Costing Software:</a>--%>

                                                <%--       <div class="form-floating mb-3 col-md-3">
                                                    <asp:DropDownList ID="PCS_Comp_RightsDropDown" AutoPostBack="true" class="form-select" runat="server">

                                                    </asp:DropDownList>
                                                    <label for="ProductCategory">PCS/Company Rights</label>

                                                </div>--%>
                                                <asp:GridView ID="GridUserRightsList" CssClass=" table-hover table-responsive gridview"
                                                    GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                    PagerStyle-CssClass="gridview_pager" OnRowDataBound="GridUserRightsList_RowDataBound"
                                                    AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="MenuId">
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="MenuName" ControlStyle-CssClass="align-content-md-start" HeaderText="Menu" SortExpression="MainCategory_Name" />
                                                        <asp:BoundField DataField="SubMenuName" ControlStyle-CssClass="align-content-md-start" HeaderText="SubMenu" SortExpression="MainCategory_Name" />
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdfMenuId" runat="server" Value='<%#  Eval("MenuId") %>' />
                                                                <asp:HiddenField ID="hdfSubMenuId" runat="server" Value='<%#  Eval("SubMenuId") %>' />



                                                                <%--<input type="checkbox" runat="server" disabled checked='<%#  Eval("CanView") %>'>--%>
                                                                <%--        <asp:CheckBox ID="CheckView" runat="server" Checked='<%#  Eval("CanView") %>' />
                                                                    <span class="slider round"></span>--%>

                                                                <div class="form-check form-switch">
                                                                    <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefault">--%>
                                                                    <%--<input type="checkbox" id="CheckView" runat="server" class="form-check-input " checked='<%#  Eval("CanView") %>'>--%>
                                                                    <asp:CheckBox ID="CheckView" runat="server" CssClass="form-check-inline" Checked='<%#  Eval("CanView") %>' />
                                                                </div>
                                                                <%--<label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>--%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <%--<label class="switch">
                                                                      </label>

                                                                    <%--<input type="checkbox" runat="server" checked='<%#  Eval("CanEdit") %>'>--%>
                                                                <%--      <asp:CheckBox ID="CheckEdit" runat="server" Checked='<%#  Eval("CanEdit") %>' />
                                                                    <span class="slider round"></span>--%>
                                                                <div class="form-check form-switch">
                                                                    <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefaults">--%>
                                                                    <%--<input type="checkbox" id="CheckEdit" runat="server" class="form-check-input " checked='<%#  Eval("CanEdit") %>'>--%>
                                                                    <asp:CheckBox ID="CheckEdit" runat="server" CssClass="form-check-inline" Checked='<%#  Eval("CanEdit") %>' />

                                                                    <%--<label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>--%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <%--<label class="switch">

                                                                    <%--<input type="checkbox"  runat="server" checked='<%#  Eval("CanDelete") %>'>--%>
                                                                <%--   <asp:CheckBox ID="CheckDelete" runat="server" Checked='<%#  Eval("CanDelete") %>' />
                                                                    <span class="slider round"></span>
                                                                </label>--%>
                                                                <div class="form-check form-switch">
                                                                    <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefaulta">--%>
                                                                    <%--<input type="checkbox" id="CheckDelete" runat="server" class="form-check-input " checked='<%#  Eval("CanDelete") %>'>--%>
                                                                    <asp:CheckBox ID="CheckDelete" runat="server" CssClass="form-check-inline" Checked='<%#  Eval("CanDelete") %>' />

                                                                    <%--<label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>--%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-dismiss="modal">Close</button>
                                                <asp:Button ID="UserRightSubmitBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UserRightSubmitBtn_Click" data-bs-dismiss="modal" CausesValidation="false" runat="server" Text="Add" class="btn btn-primary btn-block" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <%-------------------------------------%>

                                <div class="card-body">
                                    <div class=" mt-4" id="DivGirdRoleMasterList" runat="server">

                                        <asp:GridView ID="GirdRoleMasterList" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="GroupId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="MainCategory_Name" />
                                                <%--<asp:BoundField DataField="IsActive" HeaderText="Statis" SortExpression="MainCategory_Name" />--%>

                                                <asp:TemplateField HeaderText="IsActive" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <%--     <label class="switch">
                                                            <input type="checkbox" runat="server" disabled checked='<%#  Eval("IsActive") %>'>
                                                            <span class="slider round"></span>
                                                        </label>--%>
                                                        <div class="form-check form-switch " style="margin-left: 25px;">
                                                            <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefault">--%>
                                                            <input type="checkbox" runat="server" class="form-check-input " disabled checked='<%#  Eval("IsActive") %>'>
                                                            <%--<label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>--%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rights" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <%--        <button type="button" runat="server" id="Rightsid" class="fas fa-cogs" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                                    </button>--%>
                                                        <asp:Button ID="UserRightsBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' class="btn  btn-secondary btn-sm" Text="Rights" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="UserRightsBtn_Click1" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <div id="DivActionGrid" runat="server">
                                                            <asp:Button ID="GridDelete" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" OnClick="GridDelete_Click" CausesValidation="false" runat="server" class="btn btn-danger btn-sm" Text="Delete" />
                                                            <asp:Button ID="GridEdit" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="GridEdit_Click" CausesValidation="false" runat="server" class="btn btn-success btn-sm" Text="Edit" />
                                                        </div>
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
