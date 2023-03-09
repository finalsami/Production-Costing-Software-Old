<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCompany.Master" AutoEventWireup="true" CodeBehind="StatewiseCostFactors.aspx.cs" Inherits="Production_Costing_Software.StatewiseCostFactors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
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
                    <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Statewise Cost Factors</h1>
                    <ol class="breadcrumb mb-4">
                    </ol>

                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Statewise Cost Factors
                           
                        </div>

                        <%--<asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="StateNameDropdownspan">State Name</span>
                                                <asp:DropDownList ID="StateNameDropdown" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <%--<label for="StateName">State Name</label>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="ProductCategoryDropDownspan">ProductCategory</span>
                                                <asp:DropDownList ID="ProductCategoryDropDown" OnSelectedIndexChanged="ProductCategoryDropDown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <%--<label for="ProductCategory">ProductCategory</label>--%>
                                            </div>
                                        </div>
                                        <%--        <div class="col-md-4">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="ExpenseTypeDropDownList" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="ExpenseType">Expense Type</label>

                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="card-body col-md-5">
                                                <div class="card-header">
                                                    <i class="fas fa-table me-1"></i>
                                                    RPL Expense
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="card-body col-md-5">
                                                <div class="card-header ">
                                                    <i class="fas fa-table me-1"></i>
                                                    NCR Expense
                                                </div>
                                            </div>
                                        </div>
                                        <%--       <div class="col-md-4">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="PriceTypeDropDownList" class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="PriceType">Price Type</label>

                                            </div>
                                        </div>--%>

                                        <%--                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:TextBox ID="ExpenseWeightagetxt" class="form-control" runat="server" ClientIDMode="Static" Text="0" TextMode="Number" placeholder="Expense weightage (%)"></asp:TextBox>
                                                <label for="Expenseweightage">Expense weightage (%)</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="ExpenseWeightagetxt" runat="server" ForeColor="Red" ErrorMessage="* Expense weightage (%)"></asp:RequiredFieldValidator>
                                        </div>--%>

                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="StateNameDropdopan">Staff Expense</span>
                                                    <asp:TextBox ID="RPLStaffExpensetxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="RPLStaffExpensetxt_TextChanged" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="perspan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="NRVspANID">Staff Expense</span>
                                                    <asp:TextBox ID="NCRStaffExpensetxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="NCRStaffExpensetxt_TextChanged" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRVperspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="Depo ExpenseDropdopan">Depo Expense</span>
                                                    <asp:TextBox ID="RPLDepoExpensetxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="RPLDepoExpensetxt_TextChanged" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="DepoExpenseperspan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="NRVDepo ExpensespANID">Depo Expense</span>
                                                    <asp:TextBox ID="NCRDepoExpensetxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="NCRDepoExpensetxt_TextChanged" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRVDepo Expenseperspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="StateNIncentiveameDropdopan">Incentive</span>
                                                    <asp:TextBox ID="RPLIncentivetxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="RPLIncentivetxt_TextChanged" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="persIncentivepan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="NRVIncentivespANID">Incentive</span>
                                                    <asp:TextBox ID="NCRIncentivetxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="NCRIncentivetxt_TextChanged" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRIncentiveVperspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="StateNamMarketingeDropdopan">Marketing</span>
                                                    <asp:TextBox ID="RPLMarketingtxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="RPLMarketingtxt_TextChanged" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="perMarketingspan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="RPLdMarketingtxt">Marketing</span>
                                                    <asp:TextBox ID="NCRMarketingtxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="NCRMarketingtxt_TextChanged" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRVpRPLMarketingtxterspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="StateNamInteresteDropdopan">Interest</span>
                                                    <asp:TextBox ID="RPLInteresttxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="RPLInteresttxt_TextChanged" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="peInterestrspan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="NRVsInterestpANID">Interest</span>
                                                    <asp:TextBox ID="NCRInteresttxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="NCRInteresttxt_TextChanged" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRVpInteresterspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="StateNamOtherInteresteDropdopan">Other</span>
                                                    <asp:TextBox ID="RPLOthertxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="RPLOthertxt_TextChanged" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="peIOthernterestrspan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="NROtherVsInterestpANID">Other</span>
                                                    <asp:TextBox ID="NCROthertxt" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="NCROthertxt_TextChanged" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRVOtherpInteresterspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-4" runat="server">
                                                <div class="input-group border border-success border-3">
                                                    <span class="input-group-text" id="StateNTotalamOtherInteresteDropdopan">RPL Total</span>
                                                    <asp:TextBox ID="RPLTotaltxt" TextMode="Number" Text="0" class="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="peIOtheTotalrnterestrspan">%</span>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <div class="input-group border border-success border-3">
                                                    <span class="input-group-text" id="NROtherVsIRPLTotaltxtnterestpANID">NCR Total</span>
                                                    <asp:TextBox ID="NCRTotaltxt" TextMode="Number" Text="0" class="form-control" runat="server" type="number"></asp:TextBox>
                                                    <span class="input-group-text" id="NRVOtherRPLTotaltxtpInteresterspan">%</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">

                                            <div class="col-md-2">
                                            </div>


                                            <div class="col-md-4">
                                                <asp:Button ID="AddStatewiseCostFactors" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddStatewiseCostFactors_Click" CssClass="btn btn-primary btn-sm" runat="server" Text="RPL Add" />

                                                <asp:Button ID="CancelCategoryMapping" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelCategoryMapping_Click" CssClass="btn btn-warning btn-sm offset-1" runat="server" Text="RPL Cancel" />

                                                <asp:Button ID="UpdateStatewiseCostFactors" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateStatewiseCostFactors_Click" CssClass="btn btn-info btn-sm" Visible="false" runat="server" Text="RPL Update" />
                                            </div>

                                            <div class="col-md-2">
                                            </div>

                                            <div class="col-md-4">
                                                <asp:Button ID="AddNCRExpenceBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddNCRExpenceBtn_Click" CssClass="btn btn-primary btn-sm" runat="server" Text="NCR Add" />
                                                <asp:Button ID="UpdateNCRExpenceBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateNCRExpenceBtn_Click" CssClass="btn btn-info btn-sm " Visible="false" runat="server" Text="NCR Update" />

                                                <asp:Button ID="CancelNCRExpenceBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelNCRExpenceBtn_Click" CssClass="btn btn-warning btn-sm offset-1" runat="server" Text="NCR Cancel" />

                                            </div>
                                        </div>
                                        <asp:Label ID="lblTradeName_Id" Visible="false" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblProductCategory_Id" Visible="false" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblBPM_Id" Visible="false" runat="server" Text="Label"></asp:Label>
                                        <div class="row  justify-content-center align-items-center mt-4">
                                        </div>

                                    </div>
                                    <asp:Label ID="lblExpenceType_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPriceType_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblState_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblStatewiseCostFactors_Id" runat="server" Text="" Visible="false"></asp:Label>


                                    <hr />

                                    <%--Grid--%>
                                    <div class="card-body  mb-4 overflow-auto">
                                        <%--Filter--%>
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="StateFilter">Filter By</span>
                                                    <asp:DropDownList ID="StateFilterDropdown" ValidationGroup="State" class="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group ">
                                                    <span class="input-group-text" id="CategoryFilter">Filter By</span>
                                                    <asp:DropDownList ID="CategoryFilterDropdown" ValidationGroup="Cat" AutoPostBack="true" class="form-select" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="BtnFilter" OnClick="BtnFilter_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-primary" runat="server" Text="Filter" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="BtnFilterClear" OnClick="BtnFilterClear_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CssClass="btn btn-secondary" runat="server" Text="Clear" />
                                            </div>
                                        </div>
                                        <%-----------------------%>
                                        <asp:GridView ID="Grid_StatewiseCostFactors" CssClass="table-hover table-responsive gridview overflow-auto"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="StatewiseCostFactors_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="StateName" HeaderText="StateName" SortExpression="StateName" />
                                                <asp:BoundField DataField="ProductCategoryName" HeaderText="ProductCategoryName" SortExpression="ProductCategoryName" />
                                                <%--<asp:BoundField DataField="ExpenceName" HeaderText="Expence " SortExpression="ExpenceName" />--%>
                                                <asp:BoundField DataField="PriceType" HeaderText="PriceType " SortExpression="PriceType" />
                                                <asp:BoundField DataField="StaffExpense" HeaderText="StaffExpense " SortExpression="PriceType" />
                                                <asp:BoundField DataField="DepoExpence" HeaderText="DepoExpence " SortExpression="PriceType" />
                                                <asp:BoundField DataField="Incentive" HeaderText="Incentive " SortExpression="PriceType" />
                                                <asp:BoundField DataField="Marketing" HeaderText="Marketing " SortExpression="PriceType" />
                                                <asp:BoundField DataField="Interest" HeaderText="Interest " SortExpression="PriceType" />
                                                <asp:BoundField DataField="Other" HeaderText="Other " SortExpression="PriceType" />
                                                <%--<asp:BoundField DataField="ExpenseWeightagePer" HeaderText="Expense Weightage (%) " SortExpression="PriceType" />--%>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="EditCategoryMapping" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Edit" OnClick="EditCategoryMapping_Click" Text="Edit" Width="80px" runat="server" class="btn btn-success btn-sm" />

                                                        <asp:Button ID="DelCategoryMapping" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);margin-top:3px" ValidationGroup="Delete" OnClick="DelCategoryMapping_Click" Text="Delete" Width="80px" runat="server" class="btn btn-danger btn-sm " />
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
