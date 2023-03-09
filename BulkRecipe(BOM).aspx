<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BulkRecipe(BOM).aspx.cs" Inherits="Production_Costing_Software.BulkRecipe_BOM_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
        var GridId = "<%=GridBOM_Formulation.ClientID %>";
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
    <script type="text/javascript">
        function ShowPopup(title, body) {
            $("#BOMModal").modal("show");
        }
    </script>

    <script type="text/javascript">
        function PopupShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999;
        }
    </script>
    <%--    <script type="text/javascript">
        //Initialize tooltip with jQuery
        $(document).ready(function () {
            $('.tooltips').tooltip();
        });
    </script>--%>
    <%--<script>
        $(function () {
            var current = location.pathname;
            $('.sidenav li a').each(function () {
                var $this = $(this);
                // if the current path is like this link, make it active
                if ($this.attr('href').indexOf(current) !== -1) {
                    $this.addClass('active');
                }
            })
        })
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid ">
        <div id="layoutSidenav">
            <div id="layoutSidenav_content">
                <main>
                    <%------------------------------------------------Lables-----------------------------------------------------------%>
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
                    <%-------------------------------------------------------------------------------------------------------%>
                    <div class="container-fluid px-4">
                        <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">BulkRecipe (BOM)</h1>
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item"><a href="MainCategory.aspx" class="active">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>
                            <li class="breadcrumb-item"><a href="BulkProductMaster.aspx">Bulk Product Master</a></li>
                            <li class="breadcrumb-item"><a href="FormulationMaster.aspx">Formulation Master</a></li>
                            <li class="breadcrumb-item"><a href="BulkRecipe(BOM).aspx">Bulk Recipe (BOM)</a></li>

                        </ol>

                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                BulkRecipe(BOM)
                           
                            </div>
                            <div class="card-body">

                                <div class="row mb-3">
                                    <div class="col-md-4">

                                        <asp:DropDownList ID="MainCategoryDropdown" OnSelectedIndexChanged="MainCategoryDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-8">

                                        <asp:DropDownList ID="BulkProductDropDownList" OnSelectedIndexChanged="BulkProductDropDownList_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                            <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-4">
                                        <div class="form-floating">
                                            <asp:TextBox ID="BatchSizetxt" ClientIDMode="Static" class="form-control" runat="server" type="number" placeholder="Batch Size"></asp:TextBox>
                                            <label for="BatchSizetxt">Batch Size</label>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="BatchSizetxt" runat="server" ForeColor="Red" ErrorMessage="* Batch Size !"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="MeasurementDropdown" Height="58px" class="form-select" runat="server">
                                            <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                        </asp:DropDownList>

                                    </div>

                                </div>


                                <div class="row  justify-content-center align-items-center mt-4">

                                     <div class=" col-6 col-lg-2 col-lg-2 offset-1">
                                        <div class="d-grid">
                                            <asp:Button ID="AddBRbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" OnClick="AddBRbtn_Click" runat="server" Text="Add" class="btn btn-primary btn-block" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body mt-4" style="overflow: auto">

                                <asp:UpdatePanel ID="BOM_MasterUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <hr />

                                        <div class="input-group">
                                            <asp:TextBox ID="txtSearch" OnTextChanged="TxtSearch_TextChanged" AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                            <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                            <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="txtSearch" FirstRowSelected="false" runat="server">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </div>

                                        <asp:GridView ID="Grid_BR_BOM_MasterDataList" OnRowDataBound="Grid_BR_BOM_MasterDataList_RowDataBound" Autopostback="true"
                                            CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);margin-top:8px"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter"
                                            runat="server" DataKeyNames="BR_BOM_Id" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MainCategory" HeaderText="MainCategory" SortExpression="MainCategory" />

                                                <asp:BoundField DataField="BulkProductName" HeaderText="Bulk Product Name" SortExpression="BulkProductName" />

                                                <asp:BoundField DataField="BatchSize" HeaderText="BatchSize" SortExpression="BatchSize" />
                                                <asp:BoundField DataField="Measurement" HeaderText="Measurement" SortExpression="Measurement" />
                                                <asp:BoundField DataField="FinalCostBulk" HeaderText="FinalCostBulk" SortExpression="Measurement" />

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="DelFormulationBtn" OnClick="DelFormulationBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />--%>
                                                        <asp:Button ID="InputTechnicalBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CausesValidation="false" Text="Input Technical" OnClick="InputTechnicalBtn_Click" runat="server" data-bs-toggle="modal" data-bs-target="#BOMModal" class="btn btn-success btn-sm" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="modal fade " id="BOMModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-xl">
                                        <div class="modal-content col-lg-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="modal-header">
                                                        <h5 class="modal-title" id="exampleModalLabel">
                                                            <h6>Main Category : [<asp:Label ID="lblMainCategoryName" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                                 &nbsp;</h6>
                                                            <h6>&nbsp;/ Name : [<asp:Label ID="lblBPM_Name" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] </h6>
                                                            <h6>&nbsp; / Batch Size : [<asp:Label ID="lblBatchSize" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] </h6>
                                                            <h6>&nbsp;/ Measurement : [<asp:Label ID="lblMeasurement" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] </h6>

                                                            <h5></h5>
                                                            <asp:Label ID="lblRMId" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblRMName" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblBR_BOM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblMainCategoryId" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblGridSubTotalAmount" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblBPM_Product_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblPriceMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblPurityPercent" runat="server" Text="" Visible="false"></asp:Label>


                                                            <h5></h5>
                                                            <h5></h5>
                                                            <h5></h5>
                                                            <h5></h5>
                                                        </h5>
                                                        <button aria-label="Close" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn-close" data-bs-dismiss="modal" type="button">
                                                        </button>
                                                    </div>

                                                    <div class="modal-body">
                                                        <div class="row mb-3">

                                                            <div class="col-md-5">
                                                                <div class="input-group-text">
                                                                    <asp:Label ID="lblSearchRM" runat="server" Text="Search RM: "></asp:Label>
                                                                    <asp:TextBox ID="Searchtxt" OnTextChanged="Searchtxt_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server">

                                                                    </asp:TextBox>
                                                                    <asp:HiddenField ID="SearchRM_Id" runat="server" />
                                                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" OnClientShown="PopupShown" CompletionInterval="10" EnableCaching="false" ServiceMethod="SearchRMData" MinimumPrefixLength="2" TargetControlID="Searchtxt" FirstRowSelected="false" runat="server">
                                                                    </ajaxToolkit:AutoCompleteExtender>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="Searchtxt" runat="server" ErrorMessage="* Search RM !"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4" runat="server" id="InputFormualtion">
                                                                <div class="input-group-text  mb-3">
                                                                    <asp:TextBox ID="RequiredFormulationtxt" CssClass="form-control" runat="server" placeholder="RequiredFormulation"></asp:TextBox>
                                                                    <asp:TextBox ID="InputKGtxt" CssClass="form-control" runat="server" Visible="false" placeholder="Quantity Ltr/Kg"></asp:TextBox>
                                                                    <asp:Label ID="lblInputKG" Visible="false" CssClass="" runat="server" Text="" placeholder=""></asp:Label>
                                                                    <asp:Label ID="lnlReqruiredFormulation" class="form-check" runat="server" Text="Formulation?">
                                                                    </asp:Label>
                                                                    <asp:CheckBox ID="ChkReqruiredFormulation" Checked="true" AutoPostBack="true" OnCheckedChanged="ChkReqruiredFormulation_CheckedChanged" class="form-check-inline" runat="server" />


                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="RequiredFormulationtxt" runat="server" ErrorMessage="* Required Formulation !"></asp:RequiredFieldValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="InputKGtxt" ForeColor="Red" runat="server" ErrorMessage="* InputKG"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 " id="HideSolventTxt" runat="server">
                                                                <div class="input-group-text  mb-3">
                                                                    <asp:TextBox ID="Solventtxt" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Solvent(QS)"></asp:TextBox>
                                                                    &nbsp;&nbsp;<asp:CheckBox ID="ChkSolvent" AutoPostBack="true" Checked="false" OnCheckedChanged="ChkSolvent_CheckedChanged" CssClass="form-check-inline" Style="margin-left: 5px" runat="server" />
                                                                    <label for="Solventtxt">Solvent</label>
                                                                    <asp:TextBox ID="InputSubTotaltxt" ReadOnly="true" Visible="false" class="form-control" type="text" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row mb-4">
                                                                <div class="col-md-3"></div>
                                                                 <div class=" col-6 col-lg-2 col-lg-2 offset-2">
                                                                    <div class="d-grid">
                                                                        <asp:Button ID="AddInputTechnicalBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Del" OnClick="AddInputTechnicalBtn_Click" runat="server" Text="ADD" class="btn btn-primary btn-sm" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row mb-3 modal-footer modal-dialog-scrollable" style="overflow: auto">

                                                            <asp:GridView ID="GridBOM_Formulation" Autopostback="true"
                                                                CssClass=" table-hover table-responsive gridview"
                                                                GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                                PagerStyle-CssClass="gridview_pager"
                                                                AlternatingRowStyle-CssClass="gridview_alter"
                                                                OnRowDataBound="GridBOM_Formulation_RowDataBound"
                                                                runat="server" AutoGenerateColumns="False" DataKeyNames="BPM_Ingrdientd_Id" ShowFooter="True" OnRowDeleting="GridBOM_Formulation_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="IngredientName" HeaderText="Ingrdient_Name" SortExpression="Ingrdient_Name" />

                                                                    <asp:BoundField DataField="Formulation" HeaderText="Formulation" SortExpression="Formulation" />
                                                                    <asp:BoundField DataField="InputKg" Visible="false" HeaderText="InputKg" SortExpression="InputKg" />
                                                                    <%--                 <itemtemplate>
                                                                            <%# Eval("Formulation")%>%
                                                                        </itemtemplate>--%>

                                                                    <%--           <ItemTemplate>
                                                                            <asp:Label id="lbl_Formulation" runat="server" Text='<%#Eval("Formulation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_Formulation" runat="server" Text='<%#Eval("Formulation") %>'></asp:TextBox>

                                                                        </EditItemTemplate>--%>

                                                                    <asp:BoundField DataField="InputTechnical" ReadOnly="true" HeaderText="Input Technical (Qty)" SortExpression="InputTechnical">
                                                                        <%--    <EditItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("InputTechnical") %>'></asp:Label>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("InputTechnical") %>'></asp:Label>
                                                                        </ItemTemplate>--%>

                                                                    </asp:BoundField>

                                                                    <asp:BoundField DataField="RateAmount_KG" ReadOnly="true" HeaderText="Rate (Amount)/KG" SortExpression="Rate (Amount)/KG" />
                                                                    <asp:BoundField DataField="TransportationRate" HeaderText="Transportation Rate" SortExpression="Ingrdient_Name" />

                                                                    <asp:BoundField DataField="Amount" ReadOnly="true" HeaderText="Amount" SortExpression="Amount" />


                                                                    <%--            <asp:TemplateField HeaderText="Solvent">
                                                                            <ItemTemplate>
                                                                              <%#Eval("Solvent")%>
                                                                                  
                                                                            </ItemTemplate>
                                                                      
                                                                        </asp:TemplateField>--%>


                                                                    <%--     <EditItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                        </ItemTemplate>--%>



                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="DelFormulationBtn" OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="Del" OnClick="DelFormulationBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                                            <%--<asp:Button ID="EditFormationBtn"  Text="Edit" runat="server"   class="btn btn-success btn-sm" />--%>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>

                                                                </Columns>

                                                            </asp:GridView>

                                                        </div>
                                                        <div class="row mb-3">
                                                            <div class="col-lg-3">
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="input-group-text mb-3 ">
                                                                    <asp:CheckBox ID="ChkFormulationDropDown" Checked="false" AutoPostBack="true" OnCheckedChanged="ChkFormulationDropDown_CheckedChanged" runat="server" />

                                                                    <span class="input-group" id="FormulationDrop">Formulation</span>
                                                                    <%--<label for="FormulationDropdown">Formulation</label>--%>

                                                                    <asp:DropDownList ID="FormulationDropdown" Enabled="false" CssClass="form-select" OnSelectedIndexChanged="FormulationDropdown_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="input-group-text mb-3 ">
                                                                    <label for="BatchSizeInputtxt">BatchSize</label>

                                                                    <asp:TextBox ID="BatchSizeInputtxt" ReadOnly="true" class="form-control" runat="server" type="number" placeholder="BatchSizeInput"></asp:TextBox>
                                                                    <asp:Label ID="lblBOM_Batchsize" runat="server" Text="" Visible="true">:</asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-3 ">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="FormulationAmounttxt" ReadOnly="true" class="form-control" type="text" runat="server"></asp:TextBox>

                                                                    <label for="FormulationSelectedtxt">
                                                                        Formulation Charges  
                                                                        (@
                                                                        <asp:Label ID="lblFormulationAddBuffer" runat="server" Text="" Visible="true">:</asp:Label>)</label>

                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row mb-3">
                                                            <div class="col-lg-3">
                                                            </div>
                                                            <div class="col-lg-3">
                                                            </div>
                                                            <div class="col-lg-3">
                                                            </div>
                                                            <%--     <div class="col-lg-4 ">
                                                                <div class="input-group-text mb-3 ">
                                                                    <label for="BatchSizeInputtxt">TotalCost </label>

                                                                    <asp:TextBox ID="FormulationTotalCosttxt" ReadOnly="true" class="form-control" runat="server" type="number" placeholder="BatchSizeInput"></asp:TextBox>
                                                                    <i style="font-weight: bold">(Add Buffer)</i>

                                                                </div>
                                                            </div>--%>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                                    <asp:TextBox ID="TotalAmounttxt" Text="0.000" ReadOnly="true" OnTextChanged="TotalAmounttxt_TextChanged" AutoPostBack="true" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                                    <label for="TotalAmounttxt">Total Amount</label>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-3">
                                                            <div class="col-lg-3 ">
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="SPGRtxt" Text="0.000" OnTextChanged="SPGRtxt_TextChanged" AutoPostBack="true" class="form-control" runat="server" type="number" placeholder="SPGR (Spacigravity)"></asp:TextBox>
                                                                    <label for="SPGRtxt">SPGR</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="TotalOutput_LTR" class="form-control" ReadOnly="true" runat="server" type="number" placeholder="TotalOutput_LTR"></asp:TextBox>
                                                                    <label for="TotalOutput_LTR">TotalOutput_LTR</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating mb-3 ">
                                                                    <asp:TextBox ID="Costtxt" AutoPostBack="true" OnTextChanged="Costtxt_TextChanged" ReadOnly="true" class="form-control" runat="server" type="number"></asp:TextBox>
                                                                    <label for="Costtxt">Cost/Ltr</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row mb-3">
                                                            <div class="col-lg-3">
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="FormulationLosttxt" AutoPostBack="true" OnTextChanged="FormulationLosttxt_TextChanged" class="form-control" runat="server" placeholder="FormulationLost"></asp:TextBox>
                                                                    <label for="FormulationLosttxt">Formulation Lost (%)</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="FormulationALostmounttxt" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                                    <label for="FormulationLostAmounttxt">Formulation Lost Amount</label>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-floating border border-success border-3" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                                    <asp:TextBox ID="FinalCostBulktxt" ReadOnly="true" class="form-control" runat="server" placeholder="FinalCostBulk"></asp:TextBox>
                                                                    <label for="FinalCostBulktxt">Final Cost Bulk/Ltr</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </hr>
                                                        <asp:Label ID="lblFinalCostBulk_BRBOM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                                    <div class="card-body  justify-content-center align-items-center mt-2">

                                                        <div class="row mb-4">
                                                            <div class="col-md-3"></div>
                                                             <div class=" col-6 col-lg-2 col-lg-2 offset-2">
                                                                <div class="d-grid">
                                                                    <asp:Button ID="AddFinalBOM" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddFinalBOM_Click" CausesValidation="false" runat="server" Text="Add" class="btn btn-primary btn-block" />

                                                                </div>
                                                                <div class="d-grid">
                                                                    <asp:Button ID="UpdateFinalBOM" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="UpdateFinalBOM_Click" Visible="false" CausesValidation="false" runat="server" Text="Update" class="btn btn-secondary  btn-block" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3"></div>

                                                        </div>

                                                    </div>

                                                </ContentTemplate>

                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                </main>

            </div>
        </div>
    </div>
    <%--   <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/js/all.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/simple-datatables@latest" crossorigin="anonymous"></script>



    <script src="Content/Admin/js/datatables-simple-demo.js"></script>
    <script src="Content/Admin/js/scripts.js"></script>
    <script src="Content/bootstrap-5.1.0-dist/js/bootstrap.min.js"></script>--%>
</asp:Content>
