<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="Production_Costing_Software.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/Js/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowPopup1(title, body) {
            $("#staticBackdrop").modal("show");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="layoutSidenav">
        <link href="Content/CSS/ToggleIsActive.css" rel="stylesheet" />
        <div id="layoutSidenav_content">
            <main>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="container-fluid px-4">
                            <h2 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">User Master:</h2>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MainCategory.aspx">User Master</a></li>
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
                                    User:
                                  
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
                                            <asp:Button ID="AddUser" class="btn btn-primary" CausesValidation="false" OnClick="AddUser_Click1" runat="server" Text="+Add"/>
                                            <%--     <button type="button" runat="server" id="AddUser" onclick="" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                                Add</button--%>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <!-- Button trigger modal -->


                            <!-- Modal -->
                            <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                <div class="modal-dialog modal-fullscreen-md-down">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="staticBackdropLabel">User Register:</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row mb-3">
                                                <div class="col-md-6 ">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="FirstNametxt" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                                                        <label for="FirstNametxt">First Name</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="FirstNametxt" runat="server" ForeColor="Red" ErrorMessage="* FirstName "></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="LastNametxt" class="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                        <label for="LastNametxt">Last Name</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="offset-1" ControlToValidate="LastNametxt" runat="server" ForeColor="Red" ErrorMessage="* LastName "></asp:RequiredFieldValidator>
                                                </div>


                                            </div>
                                            <div class="row mb-3">
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="Emailtxt" class="form-control" ClientIDMode="Static" runat="server" TextMode="Email"></asp:TextBox>
                                                        <label for="Emailtxt">Email</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="offset-1" ControlToValidate="Emailtxt" runat="server" ForeColor="Red" ErrorMessage="* Email "></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="Mobiletxt" ClientIDMode="Static" class="form-control" runat="server" TextMode="Phone"></asp:TextBox>
                                                        <label for="Mobiletxt">Mobile</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="offset-1" ControlToValidate="Mobiletxt" runat="server" ForeColor="Red" ErrorMessage="* Mobilet "></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mb-3">
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="UserNametxt" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                                                        <label for="UserNametxt">User Name</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="offset-1" ControlToValidate="UserNametxt" runat="server" ForeColor="Red" ErrorMessage="* UserName "></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:DropDownList ID="UserRoleDropdownList" runat="server" class="form-select" aria-label="Floating label select example"></asp:DropDownList>
                                                        <label for="UserNametxt">Role</label>

                                                    </div>

                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red" ErrorMessage="* Total Cost !" ControlToValidate="UserRoleDropdown"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="row mb-3">
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="ConfirmPasstxt" class="form-control" ClientIDMode="Static" runat="server" TextMode="Password"></asp:TextBox>
                                                        <label for="ConfirmPasstxt">Confirm Password</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ConfirmPasstxt" runat="server" ForeColor="Red" ErrorMessage="* ConfirmPasstxt "></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" ControlToCompare="ConfirmPasstxt" ControlToValidate="Passwordtxt" runat="server" ForeColor="Red" ErrorMessage="*Password Does Not Match!"></asp:CompareValidator>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="Passwordtxt" class="form-control" ClientIDMode="Static" runat="server" TextMode="Password"></asp:TextBox>
                                                        <label for="Passwordtxt">Password</label>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="offset-1" ControlToValidate="Passwordtxt" runat="server" ForeColor="Red" ErrorMessage="* Password "></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row mb-3">
                                                <div class="col-sm-5">
                                                    <div class="input-group">
                                                        <div class="form-check form-switch">
                                                            <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefault">--%>
                                                            <%--<asp:CheckBox ID="IsActive" runat="server" class="form-check-input " />--%>
                                                            <input type="checkbox" id="IsActive" runat="server" class="form-check-input ">

                                                            <label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                            <%--<button type="button" class="btn btn-primary">Submit</button>--%>
                                            <asp:Button ID="UserUpdateBtn" OnClick="UserUpdateBtn_Click" runat="server" CausesValidation="false" class="btn btn-info" data-bs-dismiss="modal" Text="Update" />
                                            <asp:Button ID="UserSubmitBtn" OnClick="UserSubmitBtn_Click" runat="server" class="btn btn-primary" data-bs-dismiss="modal" Text="Submit" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="card-body">
                                <div class=" mt-4">

                                    <asp:GridView ID="GirdUserMasterList" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        AllowPaging="true" PagerStyle-CssClass="gridview_pager" 
                                        AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" SortExpression="MainCategory_Name" />
                                            <%--<asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="MainCategory_Name" />--%>

                                            <asp:TemplateField HeaderText="IsActive" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <div class="form-check form-switch"  style="margin-left: 25px;">
                                                        <%--<input class="form-check-input" runat="server" type="checkbox" id="flexSwitchCheckDefault">--%>
                                                        <input type="checkbox" runat="server" disabled class="form-check-input " checked='<%#  Eval("IsActive") %>' >
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="GridDelete" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClick="GridDelete_Click" CausesValidation="false" runat="server" class="btn btn-danger btn-sm" Text="Delete" />
                                                    <asp:Button ID="GridEdit" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="GridEdit_Click" CausesValidation="false" runat="server" class="btn btn-success btn-sm" Text="Edit" />
                                                    <%--<asp:Button ID="AddUser" Visible='<%# lblCanEdit.Text=="True"?true:false %>' OnClick="AddUser_Click" CausesValidation="false" runat="server" Text="Add" class="btn btn-primary btn-sm" />--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>



                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <%--     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="AddUser" EventName="Click" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </main>

        </div>



    </div>

</asp:Content>
