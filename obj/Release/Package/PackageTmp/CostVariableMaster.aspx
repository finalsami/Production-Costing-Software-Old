<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CostVariableMaster.aspx.cs" Inherits="Production_Costing_Software.CostVariableMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function UpDateSuccess() {
            alert("Update SuccessFully !");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
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

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Other Variables forms:</h1>
                            <ol class="breadcrumb mb-4">
                                <%--<li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>--%>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Work Time
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="ShiftTime">Shift Time</span>

                                                <asp:TextBox ID="ShiftTimetxt" ClientIDMode="Static" TextMode="Number" OnTextChanged="ShiftTimetxt_TextChanged" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ShiftTimetxt" runat="server" ForeColor="Red" ErrorMessage="* Shift Time"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">

                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="BreakAndOtherTimmingSpan">Break And other Timming</span>

                                                <asp:TextBox ID="BreakAndOtherTimmingtxt" ClientIDMode="Static" OnTextChanged="BreakAndOtherTimmingtxt_TextChanged" AutoPostBack="true" TextMode="Number" class="form-control" type="text" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="BreakAndOtherTimmingtxt" runat="server" ForeColor="Red" ErrorMessage="* Break And Other Timming"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="NetShiftHoursSpanId">Net Shift Hours</span>
                                                <asp:TextBox ID="NetShiftHourstxt" ClientIDMode="Static" TextMode="Number" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="BreakAndOtherTimmingtxt" runat="server" ForeColor="Red" ErrorMessage="*Net Shift Hours"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>

                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Power Unit Price
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="PowerUnitPriceSpanId">Power Unit Price/Hour</span>

                                                <asp:TextBox ID="PowerUnitPricetxt" ClientIDMode="Static" TextMode="Number" class="form-control" type="text" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="PowerUnitPricetxt" runat="server" ForeColor="Red" ErrorMessage="* Power Unit Price"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                </div>
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Labour Cost
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="LabourChargespanId">Labour Charge/Day</span>
                                                <asp:TextBox ID="LabourChargetxt" ClientIDMode="Static" TextMode="Number" class="form-control" type="Nuber" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="LabourChargetxt" runat="server" ForeColor="Red" ErrorMessage="* Labour Charge"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="SupervisorCostingSpanId">Supervisor Costing/Day</span>

                                                <asp:TextBox ID="SupervisorCostingtxt" ClientIDMode="Static" TextMode="Number" class="form-control" type="Number" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="SupervisorCostingtxt" runat="server" ForeColor="Red" ErrorMessage="*Supervisor Costing"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>

                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Other Costing Variables
                                </div>
                                <asp:Label ID="lblOtherVariableForms_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="UnLoadingExpenceLtrId">Unloading Expence /Ltr</span>
                                                <asp:TextBox ID="UnloadingExpenceLtrtxt" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="UnloadingExpenceLtrtxt" runat="server" ForeColor="Red" ErrorMessage="* Unloading Expence Ltr"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <div class="input-group mb-3 ">
                                                    <span class="input-group-text" id="UnLoadingExpenceKgSpanId">Unloading Expence /Kg</span>
                                                    <asp:TextBox ID="UnLoadingExpenceKgtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="UnLoadingExpenceKgtxt" runat="server" ForeColor="Red" ErrorMessage="* UnLoading Expence Kg"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="UnloadingExpenceUnitSpanId">Unloading Expence /Unit</span>
                                                <asp:TextBox ID="UnloadingExpenceUnittxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="UnloadingExpenceUnittxt" runat="server" ForeColor="Red" ErrorMessage="* Unloading Expence Unit"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="LoadingExpenceLtrSpanId">Loading Expence  /Ltr</span>
                                                <asp:TextBox ID="LoadingExpenceLtrtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="LoadingExpenceLtrtxt" runat="server" ForeColor="Red" ErrorMessage="* UnLoading Expence Kg"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <div class="input-group mb-3 ">
                                                    <span class="input-group-text" id="LoadingExpenceKgSpanId">Loading Expence  /Kg</span>
                                                    <asp:TextBox ID="LoadingExpenceKgtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="LoadingExpenceKgtxt" runat="server" ForeColor="Red" ErrorMessage="* Loading Expence Kg"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="loadingExpenceUnitSpanId">Loading Expence  /Unit</span>
                                                <asp:TextBox ID="loadingExpenceUnittxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="loadingExpenceUnittxt" runat="server" ForeColor="Red" ErrorMessage="* loading Expence Unit"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="MachinaryMaitExpenceLtrSpanId">Machinary Mait Expence/Ltr</span>
                                                <asp:TextBox ID="MachinaryMaitExpenceLtrtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="MachinaryMaitExpenceLtrtxt" runat="server" ForeColor="Red" ErrorMessage="* Machinary Mait Expence Ltr"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="MachinaryMaitExpenceKgSpanId">Machinary Mait Expence/Kg</span>
                                                <asp:TextBox ID="MachinaryMaitExpenceKgtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="MachinaryMaitExpenceLtrtxt" runat="server" ForeColor="Red" ErrorMessage="* Machinary Mait Expence Ltr"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="MachinaryMaitExpenceUnitSpanId">Machinary Mait Expence/Unit</span>
                                                <asp:TextBox ID="MachinaryMaitExpenceUnittxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="MachinaryMaitExpenceUnittxt" runat="server" ForeColor="Red" ErrorMessage="* Machinary Mait Expence Unit"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="AdminExpenceLtrSpanId">Admin Expence /Ltr</span>
                                                <asp:TextBox ID="AdminExpenceLtrtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="AdminExpenceLtrtxt" runat="server" ForeColor="Red" ErrorMessage="* Admin Expence Ltr"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <div class="input-group mb-3 ">
                                                    <span class="input-group-text" id="AdminExpenceKgSpanId">Admin Expence /Kg</span>
                                                    <asp:TextBox ID="AdminExpenceKgtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="AdminExpenceKgtxt" runat="server" ForeColor="Red" ErrorMessage="* Admin Expence Kg"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="AdminExpenceUnittxtSpanId">Admin Expence /Unit</span>
                                                <asp:TextBox ID="AdminExpenceUnittxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="AdminExpenceUnittxt" runat="server" ForeColor="Red" ErrorMessage="* Admin Expence Unit"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="ExtraExpenceLtrSpanId">Extra Expence/Ltr</span>
                                                <asp:TextBox ID="ExtraExpenceLtrtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="ExtraExpenceLtrtxt" runat="server" ForeColor="Red" ErrorMessage="* Extra Expence Ltr"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="ExtraExpenceKgSpanId">Extra Expence/Kg</span>
                                                <asp:TextBox ID="ExtraExpenceKgtxt" ClientIDMode="Static" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="ExtraExpenceKgtxt" runat="server" ForeColor="Red" ErrorMessage="* Extra Expence Kg"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="ExtraExpenceUnitSpanId">Extra Expence/Unit</span>
                                                <asp:TextBox ID="ExtraExpenceUnittxt" placeholder="Amount" class="form-control" type="text" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="ExtraExpenceUnittxt" runat="server" ForeColor="Red" ErrorMessage="* Extra Expence Unit "></asp:RequiredFieldValidator>

                                        </div>
                                        <hr />
                                        <div class="row  justify-content-center align-items-center mt-3">
                                            <div class=" col-10 col-lg-4 col-lg-4">
                                                <div class="d-grid">
                                                    <asp:Button ID="Updatebtn" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />
                                                    <asp:Button ID="Addbtn" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-primary btn-block" />
                                                </div>
                                            </div>
                                        </div>
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
