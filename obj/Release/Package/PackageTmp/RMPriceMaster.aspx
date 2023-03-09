<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RMPriceMaster.aspx.cs" Inherits="Production_Costing_Software.RMPriceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <style>
        .dateclass {
            width: 100%;
        }

            .dateclass.placeholderclass::before {
                width: 100%;
                content: attr(placeholder);
            }

            .dateclass.placeholderclass:hover::before {
                width: 0%;
                content: "";
            }
    </style>--%>

        <script type = "text/javascript">
        var GridId = "<%=GridRM_Price_MasterList.ClientID %>";
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
                <div>
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
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">RM Price Master</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                                <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                                <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                                <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    RM Price Master
                           
                                </div>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="RMCategoryDropDownList" TabIndex="1" OnSelectedIndexChanged="RMCategoryDropDownList_SelectedIndexChanged" AutoPostBack="True"
                                                    class="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="Category">Main Category</label>
                                                <%--                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="RMDropdown" ControlToValidate="RMDropdownList" Display="Dynamic" InitialValue="Null" runat="server" ForeColor="Red" ErrorMessage="* Select RM"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="RMcombo" runat="server">
                                            <div class="form-floating mb-3">
                                                <asp:DropDownList ID="RMDropdownList" TabIndex="2" OnSelectedIndexChanged="RMDropdownList_SelectedIndexChanged" AutoPostBack="True" CssClass="form-select" runat="server">
                                                </asp:DropDownList>
                                                <label for="RM" id="RMLabel" runat="server">Raw Material</label>
                                                <%--                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="RMDropdown" ControlToValidate="RMDropdownList" Display="Dynamic" InitialValue="Null" runat="server" ForeColor="Red" ErrorMessage="* Select RM"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">

                                            <div class="input-group-text">
                                                <%--<label class="input-group-text" for="DOP">Date of Purchase</label>--%>
                                                <label for="Category">Date of Purchase</label>
                                                <asp:TextBox ID="DOPtxt" type="Date" TabIndex="3" ClientIDMode="Static" CssClass="form-control" placeholder="Please specify a date"
                                                    onClick="$(this).removeClass('placeholderclass')"
                                                    runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="DOPtxt" runat="server" ErrorMessage="* Date"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group-text">
                                                <div class="form-check">
                                                    <%--<div class="offset-2">Rate Full Purity</div>--%>
                                                    <span class="input-group-text" id="PurityRequired">Rate Full Purity</span>

                                                    <asp:RadioButtonList ID="IsRateFullPurity" TabIndex="4" OnSelectedIndexChanged="IsRateFullPurity_SelectedIndexChanged" AutoPostBack="true" runat="server">

                                                        <asp:ListItem SelectedValue="1">-Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">-No</asp:ListItem>

                                                    </asp:RadioButtonList>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="IsRateFullPurity" runat="server" ForeColor="Red" ErrorMessage="* Required!"></asp:RequiredFieldValidator>--%>
                                            </div>

                                        </div>
                                    </div>


                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <%--<input class="form-control" id="RateQty" type="number" placeholder="Rate/Kg-Ltr(Rs)" />--%>
                                                <asp:TextBox ID="RateQtytxt" runat="server" TabIndex="5" AutoPostBack="true" ClientIDMode="Static" class="form-control" OnTextChanged="RateQtytxt_TextChanged" TextMode="Number" placeholder="Rate/Kg-Ltr(Rs)"></asp:TextBox>
                                                <label for="RateQty">Rate/Kg-Ltr(Rs)</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="RateQtytxt" ForeColor="Red" runat="server" ErrorMessage="* RateQty"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <%--<input class="form-control" id="Quantitytxt" type="number" placeholder="Quantity" />--%>
                                                <asp:TextBox ID="Quantitytxt" runat="server" TabIndex="6" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="Quantitytxt_TextChanged" class="form-control" TextMode="Number" placeholder="Quantity"></asp:TextBox>
                                                <label for="Quantitytxt">Quantity</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="Quantitytxt" ForeColor="Red" runat="server" ErrorMessage="*Quantity"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating" id="Puritylblid">
                                                <%--<input class="form-control" id="PurityPercenttxt" type="number" placeholder="Purity(%)" />--%>
                                                <asp:TextBox ID="PurityPercenttxt" runat="server" TabIndex="7" AutoPostBack="true" OnTextChanged="PurityPercenttxt_TextChanged" class="form-control" TextMode="Number" placeholder="Purity(%)"></asp:TextBox>
                                                <label for="PurityPercenttxt">Purity(%)</label>
                                            </div>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="PurityPercenttxt" ForeColor="Red" runat="server" ErrorMessage="*Purity Percent"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblRM_Id" runat="server" Visible="false" Text=""></asp:Label>
                                    <asp:Label ID="lblPrice_Id" runat="server" Visible="false" Text=""></asp:Label>
                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="form-floating ">
                                                <%--<input class="form-control" id="ActualPricetxt" type="number" placeholder="Actual Price (Rs)/KG-Ltr" />--%>
                                                <asp:TextBox ID="ActualPricetxt" ClientIDMode="Static" TabIndex="8" class="form-control " ReadOnly="true" runat="server" TextMode="Number" placeholder="Actual Price (Rs)/KG-Ltr"></asp:TextBox>
                                                <label for="ActualPricetxt">Actual Price (Rs)/KG-Ltr</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ActualPricetxt" ForeColor="Red" runat="server" ErrorMessage="*Actual Price"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <%--<input class="form-control" id="TransportationRatetxt" type="number" placeholder="Transportation Rate" />--%>
                                                <asp:TextBox ID="TransportationRatetxt" TabIndex="9" ClientIDMode="Static" class="form-control" AutoPostBack="true" OnTextChanged="TransportationRatetxt_TextChanged" runat="server" TextMode="Number" placeholder="Transportation Rate"></asp:TextBox>
                                                <label for="TransportationRatetxt">Transportation Rate </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="TransportationRatetxt" ForeColor="Red" runat="server" ErrorMessage="* Transportation Rate"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating disabled">
                                                <%--<input class="form-control" id="TotalRatetxt" type="number" placeholder="Total Rate (Rs./Ltr)" />--%>
                                                <asp:TextBox ID="TotalRatetxt" class="form-control" TabIndex="10" runat="server" ReadOnly="true" TextMode="Number" placeholder="Total Rate (Rs./Ltr)"></asp:TextBox>
                                                <label for="TotalRatetxt">Total Rate (Rs./Ltr)</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="TotalRatetxt" ForeColor="Red" runat="server" ErrorMessage="* Total Rate"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class="row  justify-content-center align-items-center mt-4">

                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="Editbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" Visible="false" OnClick="Editbtn_Click" runat="server" Text="Update" class="btn btn-secondary btn-block" />
                                                <asp:Button ID="Addbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-primary btn-block" />
                                            </div>
                                        </div>
                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">

                                                <asp:Button ID="CancalBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" CausesValidation="false" OnClick="CancalBtn_Click" runat="server" Text="Cancel" class="btn btn-warning btn-block" />
                                            </div>
                                        </div>
                                    </div>

                                </div>


                                <div class="card-body mb-4 table-responsive">
                                    <div class="input-group">
                                        <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged"
                                            AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                        <asp:Button ID="CancelSearch" runat="server" Text="X"  OnClick="CancelSearch_Click" CausesValidation="false" />
                                       <asp:Button ID="SearchId" runat="server" Text="Search"  OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <div class="mt-2">
                                        <asp:GridView ID="GridRM_Price_MasterList"
                                            CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);"
                                            AllowPaging="true" PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" PageSize="200" runat="server" Autopostback="true"
                                            OnRowDeleting="GridRM_Price_MasterList_RowDeleting" OnPageIndexChanging="GridRM_Price_MasterList_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="RM_Price_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RM_Name" HeaderText="Rm Name" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="DateOfPurchase" HeaderText="DateOfPurchase" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="ActualPrice" HeaderText="ActualPrice" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="Purity" HeaderText="Purity" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="TransporationRate" HeaderText="TransporationRate" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="RM_Name" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelRMPriceBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" CausesValidation="false" OnClick="DelRMPriceBtn_Click1" Text="Delete" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="EditRMPriceBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CausesValidation="false" OnClick="EditRMPriceBtn_Click1" Text="Edit" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" runat="server" class="btn btn-success btn-sm" />
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
