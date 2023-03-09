<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MRP_Price.aspx.cs" Inherits="Production_Costing_Software.MRP_Price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var GridId = "<%=Grid_MRPPrice.ClientID %>";
        var ScrollHeight = 700;
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
                if (headerCellWidths[i] >= gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
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

    <script type="text/javascript">
        // It is important to place this JavaScript code after ScriptManager1
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=Panel1.ClientID%>') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos = $get('<%=Panel1.ClientID%>').scrollLeft;
                yPos = $get('<%=Panel1.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=Panel1.ClientID%>') != null) {
                // Set X and Y positions back to the scrollbar
                // after partial postback
                $get('<%=Panel1.ClientID%>').scrollLeft = xPos;
                $get('<%=Panel1.ClientID%>').scrollTop = yPos;
            }
        }

        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <style>
                    input::-webkit-outer-spin-button,
                    input::-webkit-inner-spin-button {
                        /* display: none; <- Crashes Chrome on hover */
                        -webkit-appearance: none;
                        margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
                    }

                    input[type=number] {
                        -moz-appearance: textfield; /* Firefox */
                    }
                </style>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                    <progresstemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img src="Content/LogoImage/gifntext-gif.gif" />
                            </div>
                        </div>
                    </progresstemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <contenttemplate>
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
                            <asp:Label ID="lblBPMName" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCurrentRow" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblpreviousRow" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCanView" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblDynamicColumnCount" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblIsMasterPacking" runat="server" Text="" Visible="false"></asp:Label>

                            <%-----------------------%>
                        </div>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">MRP Price</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="MRPPrice.aspx">MRP Price</a></li>


                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    MRP Price
                                  <asp:Button ID="btnMRP_Report" OnClick="btnMRP_Report_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" Text="Report" CssClass="btn btn-danger float-md-end" />

                                </div>
                                <div class="card-body mb-4 mt-4" style="overflow: auto">

                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:Button ID="UpdateBtn" OnClick="UpdateBtn_Click" runat="server" Text="+Add" Style="z-index: 999999999999; margin-left: 5px" CssClass="btn btn-primary position-fixed mt-1" />

                                        <asp:GridView ID="Grid_MRPPrice" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnRowDataBound="Grid_MRPPrice_RowDataBound"
                                            PagerStyle-CssClass="gridview_pager"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="Fk_BPM_Id">
                                            <columns>
                                                <asp:TemplateField HeaderText="No" Visible="false">
                                                    <itemtemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bulk Product Name" ControlStyle-Width="150px">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblBPMName" runat="server" Text='<%#Eval("BPM_Product_Name") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fk_BPM_Id" Visible="false">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblFk_BPM_Id" runat="server" Text='<%#Eval("Fk_BPM_Id") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PM_RM_Category_Id" Visible="false">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblPM_RM_Category_Id" runat="server" Text='<%#Eval("Fk_PM_RM_Category_Id") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="PackingUnitMeasurement" ControlStyle-Width="130px" HeaderText="Packing" SortExpression="MainCategory_Name" />--%>

                                                <asp:TemplateField HeaderText="Packing Size">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblPackingSize" runat="server" Text='<%#Eval("Packing_Size") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PackingMeasurement" Visible="false">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblPackingMeasurement" runat="server" Text='<%#Eval("Fk_UnitMeasurement_Id") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Suggested RPL Price">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblSuggestedRPLPrice" runat="server" Text='<%#Eval("SuggestedRPLPrice") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Suggested RPL Price With GST (%)">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblSuggestedRPLPriceGST" runat="server" Text='<%#Eval("SuggestedRPLPriceGST") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Percent Of Margin MRP">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="Percent_Of_Margin_MRPtxt" OnTextChanged="Percent_Of_Margin_MRPtxt_TextChanged" AutoPostBack="true" Text='<%#Eval("Percent_Of_Margin_MRP") %>' CssClass="form-control align-content-center" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Suggested MRP">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblSuggested_MRP" AutoPostBack="true" Text='<%#Eval("Suggested_MRP") %>' Style="text-align: center" TextMode="Number" runat="server"></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Final MRP">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="Final_MRPtxt" OnTextChanged="Final_MRPtxt_TextChanged" AutoPostBack="true" Text='<%#Eval("Final_MRP") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gp" ControlStyle-Width="130px">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="Gptxt" AutoPostBack="true" Text='<%#Eval("GP") %>' CssClass="form-control align-content-center" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agrostar" ControlStyle-Width="130px">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="Agrostartxt" AutoPostBack="true" Text='<%#Eval("Agrostar") %>' CssClass="form-control align-content-center" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gramofone" ControlStyle-Width="130px">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="Gramofonetxt" AutoPostBack="true" Text='<%#Eval("Gramofone") %>' CssClass="form-control align-content-center" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MPPL" ControlStyle-Width="130px">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="MPPLtxt" AutoPostBack="true" Text='<%#Eval("MPPL") %>' CssClass="form-control align-content-center" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dehaat" ControlStyle-Width="130px">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="Dehaattxt" AutoPostBack="true" Text='<%#Eval("Dehaat") %>' CssClass="form-control" Style="text-align: center" TextMode="Number" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Packing Size" Visible="false">
                                                    <itemtemplate>
                                                        <asp:Label ID="lblPacking_Size" runat="server" Text='<%#Eval("PackingSize") %>'></asp:Label>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select" ControlStyle-Width="90px">
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="CheckBox_Check" OnCheckedChanged="CheckBox_Check_CheckedChanged" runat="server" Checked="false" CausesValidation="false" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="IsMasterPacking" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIsMasterPacking" Text='<%#Eval("IsMasterPacking") %>' ReadOnly="true" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </columns>

                                        </asp:GridView>

                                    </asp:Panel>
                                </div>

                                <asp:Label ID="lblBulkProduct_Interest_Master_Id" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                            <asp:GridView ID="MRP_Report" CssClass=" table-hover table-responsive gridview"
                                BorderColor="Red"
                                Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); display: none;" OnRowDataBound="MRP_Report_RowDataBound"
                                PagerStyle-CssClass="gridview_pager"
                                AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False">
                                <columns>
                                    <asp:TemplateField HeaderText="Bulk Product Name" ControlStyle-Width="150px">
                                        <itemtemplate>
                                            <asp:Label ID="lblBPMName" runat="server" Text='<%#Eval("BulkProductName") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="PackingUnitMeasurement" ControlStyle-Width="130px" HeaderText="Packing" SortExpression="MainCategory_Name" />--%>

                                    <asp:TemplateField HeaderText="Packing Size">
                                        <itemtemplate>
                                            <asp:Label ID="lblPackingSize" runat="server" Text='<%#Eval("Packing_Size") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gp" ControlStyle-Width="130px">
                                        <itemtemplate>
                                            <asp:Label ID="lblGP" runat="server" Text='<%#Eval("GP") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Agrostar" ControlStyle-Width="130px">
                                        <itemtemplate>
                                            <asp:Label ID="lblAgrostar" runat="server" Text='<%#Eval("Agrostar") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gramofone" ControlStyle-Width="130px">
                                        <itemtemplate>
                                            <asp:Label ID="lblGramofone" runat="server" Text='<%#Eval("Gramofone") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MPPL" ControlStyle-Width="130px">
                                        <itemtemplate>
                                            <asp:Label ID="lblMPPL" runat="server" Text='<%#Eval("MPPL") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dehaat" ControlStyle-Width="130px">
                                        <itemtemplate>
                                            <asp:Label ID="lblDehaat" runat="server" Text='<%#Eval("Dehaat") %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>

                                </columns>

                            </asp:GridView>
                        </div>
                    </contenttemplate>
                    <triggers>
                        <asp:PostBackTrigger ControlID="btnMRP_Report" />
                    </triggers>
                </asp:UpdatePanel>
            </main>
        </div>

    </div>
</asp:Content>
