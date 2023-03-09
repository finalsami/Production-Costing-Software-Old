<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PM_RM_PriceMaster.aspx.cs" Inherits="Production_Costing_Software.PM_RM_PriceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type = "text/javascript">
        var GridId = "<%=GridRM_Price_MasterList.ClientID %>";
        var ScrollHeight = 300;
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
                cells[i].style.width = parseInt(width) + "px";
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
    </script>>
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
                            <h1 class="mt-4">PM RM Price Master</h1>
                            <ol class="breadcrumb mb-4">
                                <%--<li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>--%>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    PM RM Price Master
                           
                                </div>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group mb-3">
                                                <span class="input-group-text" id="PMRMCategoryDropdownListid">PM RM Category</span>
                                                <asp:DropDownList ID="PMRMCategoryDropdownCombo" OnSelectedIndexChanged="PMRMCategoryDropdownCombo_SelectedIndexChanged" AutoPostBack="True"
                                                    class="form-select" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>


                                            </div>
                                        </div>
                                        <div class="col-md-4">

                                            <div class="input-group mb-3">
                                                <span class="input-group-text" id="inputGroup-sizing-default">PM Name</span>
                                                <asp:DropDownList ID="PMRMNameDropdownCombo" OnTextChanged="PMRMNameDropdownCombo_TextChanged" Enabled="false" OnSelectedIndexChanged="PMRMNameDropdownCombo_SelectedIndexChanged" AutoPostBack="True"
                                                    CssClass="form-select" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>


                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="inputGroupPMName">Price</span>
                                                <asp:TextBox ID="Pricetxt" ClientIDMode="Static" class="form-control" TextMode="Number" OnTextChanged="Pricetxt_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <span class="input-group-text" id="inputGroupPMName123">
                                                    <asp:Label ID="lblUnitPriceMeaurement" runat="server" Text=""></asp:Label></span>


                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Pricetxt" ForeColor="Red" runat="server" ErrorMessage="* Price"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>



                                    <div class="row mb-3">
                                        <div class="col-md-4" id="UnitMeasurementHide" runat="server">
                                            <div class="input-group mb-3 ">
                                                <span class="input-group-text" id="UnitDropdown">Unit Price Measurement (KG/Unit)</span>

                                                <asp:TextBox ID="UnitPriceMeaurementtxt" ClientIDMode="Static" ReadOnly="true" class="form-control" type="text" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UnitPriceMeaurementtxt" ForeColor="Red" runat="server" ErrorMessage="* Unit Price Meaurement"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="Transportation">Transportation</span>
                                                <asp:TextBox ID="Transportationtxt" Text="0.00" OnTextChanged="Transportationtxt_TextChanged" AutoPostBack="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="Transportationtxt" ForeColor="Red" runat="server" ErrorMessage="* Transportation"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="UnitsKG">No. of Units/KG</span>
                                                <asp:TextBox ID="Units_KGtxt" Text="0" ClientIDMode="Static" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="Units_KGtxt" ForeColor="Red" runat="server" ErrorMessage="* Units /KG"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="Loss">Loss - %</span>
                                                <asp:TextBox ID="Losstxt" OnTextChanged="Losstxt_TextChanged" Text="0.00" AutoPostBack="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="Losstxt" ForeColor="Red" runat="server" ErrorMessage="* Loss"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="Price_Unit">Price/Unit</span>
                                                <asp:TextBox ID="Price_Unittxt" ClientIDMode="Static" Text="0.00" class="form-control" ReadOnly="true" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="Losstxt" ForeColor="Red" runat="server" ErrorMessage="* Price Unit"></asp:RequiredFieldValidator>

                                        </div>


                                    </div>

                                    <div class="row  justify-content-center align-items-center mt-4">

                                         <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="Updatebtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />
                                                <asp:Button ID="Addbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-primary btn-block" />
                                            </div>
                                        </div>
                                        <div class=" col-6 col-lg-2 col-lg-2">
                                            <div class="d-grid">
                                                <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="CancelBtn_Click" class="btn btn-warning btn-block" runat="server" Text="Cancel" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblPM_RM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPM_RM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPM_RM_Price_Id" runat="server" Text="" Visible="false"></asp:Label>
                                </div>

                                <div class="card-body mt-4" style="overflow: auto">


                                    <hr />

                                    <div class="input-group">
                                        <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged" AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                        <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                        <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <div class="mt-2">
                                        <asp:GridView ID="GridRM_Price_MasterList" CssClass=" table-hover table-responsive gridview overflow-auto"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" Autopostback="true"
                                            AutoGenerateColumns="False" DataKeyNames="PM_RM_PriceId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Category_Name" HeaderText="Category  Name" SortExpression="Category  Name" />
                                                <asp:BoundField DataField="PM_Name" HeaderText="PM Name" SortExpression="Name" />
                                                <asp:BoundField DataField="PM_RM_Price" HeaderText="PM Price" SortExpression="PM_RM_Price" />
                                                <asp:BoundField DataField="Transportation" HeaderText="Transportation" SortExpression="RM_Name" />
                                                <asp:BoundField DataField="Unit/KG" HeaderText="Unit/KG" SortExpression="Unit/KG" />
                                                <asp:BoundField DataField="Price/Unit" HeaderText="Price/Unit" SortExpression="Price/Unit" />
                                                <asp:BoundField DataField="Unit_Price" HeaderText="Measurement" SortExpression="Unit_Price" />

                                                <asp:TemplateField HeaderText="Loss">
                                                    <ItemTemplate>
                                                        <itemtemplate>
                                                            <%# Eval("Loss")%>%
                                                        </itemtemplate>
                                                    </ItemTemplate>

                                                    <%--<asp:BoundField DataField="Loss" HeaderText="Loss" SortExpression="Loss" />--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:Button ID="DelPMRMPriceBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" ValidationGroup="DelPMRMPrice" OnClick="DelPMRMPriceBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                        <asp:Button ID="EditPMRMPriceBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" ValidationGroup="EditPMRMPrice" OnClick="EditPMRMPriceBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Losstxt" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>


            </main>

        </div>
    </div>
</asp:Content>
