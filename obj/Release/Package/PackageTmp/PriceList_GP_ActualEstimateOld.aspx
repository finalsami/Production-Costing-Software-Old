<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="PriceList_GP_ActualEstimateOld.aspx.cs" Inherits="Production_Costing_Software.PriceList_GP_ActualEstimateOld" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="Content/CSS/loading.css" rel="stylesheet" />
    <style type="text/css">
        .txtlbl {
            border: none;
            display: none;
        }

        .displaynone {
            display: none;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }

        .modalLoading {
            position: fixed;
            z-index: 1030;
            height: 100%;
            width: 100%;
            top: 0;
            opacity: 0.6;
        }

        .centerLoading {
            z-index: 1031;
            margin: 290px auto;
            padding: 10px;
            width: 300px;
            opacity: 1;
        }

            .centerLoading img {
                height: 150px;
                width: 250px;
            }
    </style>

    <script type="text/javascript">
        function ClearHtml() {
            document.getElementById("ContentPlaceHolder1_UpdatePanel2").innerHTML = "";
        }
    </script>
    <style>
        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        input[type=number] {
            -moz-appearance: textfield;
        }
    </style>
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


        function DynamicClick(CompanyId, Estimate) {
            document.getElementById('<%=hfDynamicCompId.ClientID %>').value = CompanyId;
            var strURL = "";
            strURL = document.URL.substring(0, document.URL.indexOf("PriceList_GP_ActualEstimate.aspx"));
            if (CompanyId == "1") {
                strURL += "PriceList_GP.aspx?CmpId=1&EstimateName=" + Estimate;
            }
            else {
                strURL += "EstimatePriceList.aspx?CmpId=" + CompanyId + "&EstimateName=" + Estimate + "&Status=" + Status;
            }
            window.location.href = strURL;
            return false;
        }

    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>

                <div class="modal fade" id="RM_ReportModal" tabindex="-1">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content modal-xl">
                            <div class="modal-header" style="background-color: #343a40">
                                <h4 style="color: white">&nbsp; Bulk Costing Report</h4>
                                <asp:Label ID="lblname" runat="server" CssClass="font-monospace" Style="color: white" Visible="true"></asp:Label>

                                <asp:Label ID="lblPackingSize" runat="server" CssClass="font-monospace" Style="color: white" Visible="true"></asp:Label>

                                <button aria-label="Close" style="color: white" class="btn-close btn-close-white" data-bs-dismiss="modal" type="button">
                                </button>
                            </div>
                            <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <div class="modal-body overflow-scroll">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Table runat="server" CssClass="table-hover table-responsive gridview" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); overflow: scroll">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img src="Content/LogoImage/gifntext-gif.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:HiddenField ID="hfDynamicCompId" runat="server" Value="0" />

                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <ContentTemplate>

                        <%--Lables--%>
                        <div>
                            <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblUserMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCanEdit" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCanView" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <%----------------------------------%>
                        <%----------Label for Suggested CWFE-------------%>
                        <asp:Label ID="lblPriceList_TotalCostPerLtr1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_TotalCostPerLtr" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_FctryExp_Per1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_FctryExp_Per" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_FctryExp_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_FctryExp_Amt" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_Other_Per1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_Other_Per" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_Profit_Per1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_Profit_Per" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_Profit_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_Profit_Amt" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_Other_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_Other_Amt" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_Mrkt_Per1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_Mrkt_Per" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_Mrkt_Amt1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_Mrkt_Amt" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblFk_PMRM_Catgeory_Id" runat="server" Text="" Visible="false"></asp:Label>


                        <asp:Label ID="lblSuggestedPricetxt1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblSuggestedPricetxt" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblCompanyExpence_Id1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCompanyExpence_Id" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lbl_BPM_Id1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblAllEstimatePriceList" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblEstimateName" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblPriceList_FFCostPerLtr1" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblPriceList_FFCostPerLtr" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblFinalNRV" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblFinalNRV1" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblNewStatus" runat="server" Text="" Visible="false"></asp:Label>


                        <asp:Label ID="lblCompany_Id" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblIsMasterPacking" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblDynamicColumnCount" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblFinalPrice" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblStateNameAlert" runat="server" Text="" Visible="false"></asp:Label>


                        <%----------------------------------%>


                        <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                            <h1 class="mt-4">PriceList GP Actual Estimate Final</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="PriceList_GP_Actual_Final.aspx">PriceList GP Actual Estimate Final
                                </a></li>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    <asp:Label ID="lblBulkProductName" Style="font-size: larger" runat="server" Text="" Enabled="false"></asp:Label>

                                    <%--<asp:Label ID="Label1" Style="font-size: x-large" runat="server" Text="Estimate Name:"></asp:Label>--%>
                                    <%--   <asp:Label ID="lblEstimateNameHeader" runat="server" Text="" Style="font-family: monospace; font-size: xx-large; font-weight: bolder"></asp:Label>
                                    <span style="font-size: xx-large">]</span>--%>
                                    <asp:Label ID="lblactest" runat="server" Style="display: none"></asp:Label>
                                    <img src="Content/Other/Right%20Arrow.png" style="width: 20px; height: 20px" />
                                    <asp:Button ID="GPActualEstimateFinalBtn" OnClick="GPActualEstimateFinalBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Price List GP ActualEstimate" runat="server" class="btn btn-outline-danger btn-sm" />
                                    <div class="align-content-end">
                                        <asp:Button ID="GridAddPriceGP" OnClick="GridAddPriceGP_Click" OnClientClick="Confirm()" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" ValidationGroup="add" Text="+Add" runat="server" class=" btn btn-primary  float-md-end m-2" />
                                    </div>
                                    <div class="align-content-end">
                                        <asp:Button ID="ReportPopupBtn" CssClass="btn btn-success float-end m-2" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ReportPopupBtn_Click" Text="View Report" runat="server" OnClientClick="ClearHtml();" data-bs-toggle="modal" data-bs-target="#RM_ReportModal" />
                                    </div>

                                </div>

                                <asp:Label ID="lblCompanyFactoryExpence_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPDEstimate" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPDActual" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblQDEstimate" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblQDActual" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblGrid_Status" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblTradeName_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPriceType" runat="server" Text="" Visible="false"></asp:Label>

                                <asp:Panel ID="Panel1" runat="server" Height="900">
                                    <div class="card-body" style="overflow: auto;">


                                        <asp:GridView ID="Grid_PriceList_GP_ActualEstimate" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4); position: relative; max-height: 700px"
                                            PagerStyle-CssClass="gridview_pager" OnRowDataBound="Grid_PriceList_GP_ActualEstimate_RowDataBound" Visible="false"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="Fk_BPM_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="State" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStateName" runat="server" Text='<%#Eval("STATENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NRV" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNRV" runat="server" Text='<%#Eval("NRV") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>


                                                <asp:TemplateField HeaderText="Transportation Cost/Liter or KG" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Final NRV with Transportation" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinalNRV" runat="server" Text='<%#Eval("FinalNRV") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="RPL/NCR Price" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnumDescription" runat="server" Text='<%#Eval("EnumDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PD" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="PDtxt"
                                                            onchange="calculaterate('1',this.id,0);"
                                                            CssClass="form-control" Text='<%#Eval("PD") %>' TextMode="Number" runat="server"></asp:TextBox>

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QD" ControlStyle-Width="150px">

                                                    <ItemTemplate>
                                                        <div class="input-group mb-3">
                                                            <asp:TextBox ID="QDtxt"
                                                                onchange="calculaterate('2',this.id,0);"
                                                                Style="width: 120px" CssClass="form-control" Text='<%#Eval("QD")%>' TextMode="Number" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Profit Amt(%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblProfitPer" runat="server" Text=""></asp:Label>--%>
                                                        <asp:Label ID="ProfitAmttxt_lbl" Text='<%#Eval("ProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        <asp:TextBox ID="ProfitAmttxt" Text='<%#Eval("ProfitAmt") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                        <div class="input-group">
                                                            <asp:TextBox ID="ProfitPertxt"
                                                                onchange="calculaterate('3',this.id,0);"
                                                                CssClass="form-control" Text='<%#Eval("ProfitPer") %>' TextMode="Number" Style="width: 25px" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Suggested-Price with PD" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblGujaratCurrent" runat="server" Text="Current"></asp:Label>--%>
                                                        <asp:Label ID="SuggestedPriceWithPDttxt_lbl" AutoPostBack="true" Text='<%#Eval("SuggestedPriceWithPD") %>' TextMode="Number" Style="text-align: center;" runat="server"></asp:Label>

                                                        <asp:TextBox ID="SuggestedPriceWithPDttxt" Text='<%#Eval("SuggestedPriceWithPD") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNewStatus" runat="server" Text='<%#Eval("NewStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Shared Final Price" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_Shared_Final_Price" runat="server" Text='<%#Eval("LastSharedPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Final Price" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="FinalPricettxt"
                                                            onchange="calculaterate('4',this.id,0);"
                                                            CssClass="form-control" TextMode="Number" Text='<%#Eval("FinalPrice") %>' Style="text-align: center" runat="server"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Additional PD" ControlStyle-Width="130px">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="AdditionalPDtxt"
                                                            onchange="calculaterate('5',this.id,0);"
                                                            CssClass="form-control" TextMode="Number" Text='<%#Eval("AdditionalPD") %>' Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gross Profit Amount (%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>

                                                        <asp:Label ID="GrossProfitAmounttxt_lbl" CssClass="form-control" Text='<%#Eval("GrossProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>


                                                        <asp:TextBox ID="GrossProfitAmounttxt" Text='<%#Eval("GrossProfitAmt") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                        <asp:Label ID="lblGrossProfitPer_lbl" CssClass="form-control" runat="server" Text='<%#Eval("GrossProfitPer")  %>'>%</asp:Label>

                                                        <asp:TextBox ID="lblGrossProfitPer" Text='<%#Eval("GrossProfitPer") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Expence" ControlStyle-Width="130px">
                                                    <ItemTemplate>

                                                        <asp:Label ID="TotalExpencetxt_lbl" Text='<%#Eval("TotalExpence") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        <asp:TextBox ID="TotalExpencetxt" Text='<%#Eval("TotalExpence") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Net Profit Amount (%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>

                                                        <asp:Label ID="NetProfitAmounttxt_lbl" AutoPostBack="true" TextMode="Number" Text='<%#Eval("NetProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        <asp:TextBox ID="NetProfitAmounttxt" Text='<%#Eval("NetProfitAmt") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                        <asp:Label ID="lblNetProfitAmtPer_lbl" runat="server" Text='<%#Eval("NetProfitPer") %>'></asp:Label>


                                                        <asp:TextBox ID="lblNetProfitAmtPer" Text='<%#Eval("NetProfitPer") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>



                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select (Make Action Default)" ControlStyle-Width="90px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox_Check" Enabled="false" OnCheckedChanged="CheckBox_Check_CheckedChanged" runat="server" Checked="false" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPacking_Size" runat="server" Text='<%#Eval("Packing_Size") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <%--            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStaffExpense" runat="server" Text='<%#Eval("StaffExpense") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMarketing" runat="server" Text='<%#Eval("Marketing") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIncentive" runat="server" Text='<%#Eval("Incentive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInterest" runat="server" Text='<%#Eval("Interest") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepoExpence" runat="server" Text='<%#Eval("DepoExpence") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther" runat="server" Text='<%#Eval("Other") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fk_State_Id Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_State_Id" runat="server" Text='<%#Eval("Fk_State_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fk_UnitMeasurement_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_UnitMeasurement_Id" runat="server" Text='<%#Eval("Fk_UnitMeasurement_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fk_PM_RM_Category_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_PM_RM_Category_Id" runat="server" Text='<%#Eval("Fk_PM_RM_Category_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fk_BPM_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_BPM_Id" runat="server" Text='<%#Eval("Fk_BPM_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FkPriceTypeId" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFkPriceTypeId" runat="server" Visible="false" Text='<%#Eval("FkPriceTypeId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" Visible="false" />

                                            </Columns>

                                        </asp:GridView>


                                        <asp:GridView ID="Grid_Default_PriceList_GP_Actual" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);" Visible="false"
                                            PagerStyle-CssClass="gridview_pager" OnRowDataBound="Grid_Default_PriceList_GP_Actual_RowDataBound"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="Fk_BPM_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="State" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStateName" runat="server" Text='<%#Eval("STATENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" Visible="false" />
                                                <asp:TemplateField HeaderText="NRV" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNRV" runat="server" Text='<%#Eval("NRV") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Transportation Cost/Liter or KG" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText=" Final Fectory Cost (NRV) / Ltr with Transportation (Final NRV)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinalNRV" runat="server" Text='<%#Eval("FinalNRV") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="RPL/NCR Price" ControlStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnumDescription" runat="server" Text='<%#Eval("EnumDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="TOD" ControlStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TODtxt" onchange="calculaterate_actual('0',this.id);" CssClass="form-control" Text='<%#Eval("TOD") %>' TextMode="Number" Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PD" ControlStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="PDtxt" onchange="calculaterate_actual('1',this.id);" CssClass="form-control" Text='<%#Eval("PD") %>' TextMode="Number" Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QD" ControlStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="QDtxt" onchange="calculaterate_actual('2',this.id);" CssClass="form-control" Text='<%#Eval("QD") %>' TextMode="Number" Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Profit Amt(%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblProfitPer" runat="server" Text=""></asp:Label>--%>
                                                        <asp:Label ID="ProfitAmttxt_lbl" AutoPostBack="true" Text='<%#Eval("ProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        <asp:TextBox ID="ProfitAmttxt" Text='<%#Eval("ProfitAmt") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                        <div class="input-group">
                                                            <asp:TextBox ID="ProfitPertxt" onchange="calculaterate_actual('3',this.id);" CssClass="form-control" Text='<%#Eval("ProfitPer") %>' TextMode="Number" Style="width: 25px" runat="server"></asp:TextBox>
                                                            <span class="input-group-text" id="basic-addon2">%</span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Suggested-Price with PD" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblGujaratCurrent" runat="server" Text="Current"></asp:Label>--%>
                                                        <asp:Label ID="SuggestedPriceWithPDttxt_lbl" Text='<%#Eval("SuggestedPriceWithPD") %>' Style="text-align: center;" runat="server"></asp:Label>

                                                        <asp:TextBox ID="SuggestedPriceWithPDttxt" Text='<%#Eval("SuggestedPriceWithPD") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last Shared Final Price" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_Shared_Final_Price" runat="server" Text='<%#Eval("LastSharedPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Final Price" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="FinalPricettxt" onchange="calculaterate_actual('4',this.id);" CssClass="form-control" TextMode="Number" Text='<%#Eval("FinalPrice") %>' Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Additional PD" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="AdditionalPDtxt" onchange="calculaterate_actual('5',this.id);" CssClass="form-control" TextMode="Number" Text='<%#Eval("AdditionalPD") %>' Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gross Profit Amount (%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="GrossProfitAmounttxt" TextMode="Number"  Text='<%#Eval("GrossProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                        <asp:Label ID="lblGrossProfitPer" runat="server" Text='<%#Eval("GrossProfitPer") %>'></asp:Label>--%>

                                                        <asp:Label ID="GrossProfitAmounttxt_lbl" CssClass="form-control" Text='<%#Eval("GrossProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>


                                                        <asp:TextBox ID="GrossProfitAmounttxt" Text='<%#Eval("GrossProfitAmt") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                        <asp:Label ID="lblGrossProfitPer_lbl" CssClass="form-control" runat="server" Text='<%#Eval("GrossProfitPer")  %>'>%</asp:Label>

                                                        <asp:TextBox ID="lblGrossProfitPer" Text='<%#Eval("GrossProfitPer") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>





                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Expence" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TotalExpencetxt_lbl" Text='<%#Eval("TotalExpence") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        <asp:TextBox ID="TotalExpencetxt" Text='<%#Eval("TotalExpence") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Net Profit Amount (%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="NetProfitAmounttxt" TextMode="Number" Text='<%#Eval("NetProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                        <asp:Label ID="lblNetProfitAmtPer" runat="server" Text='<%#Eval("NetProfitPer") %>'></asp:Label>--%>

                                                        <asp:Label ID="NetProfitAmounttxt_lbl" AutoPostBack="true" TextMode="Number" Text='<%#Eval("NetProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        <asp:TextBox ID="NetProfitAmounttxt" Text='<%#Eval("NetProfitAmt") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                        <asp:Label ID="lblNetProfitAmtPer_lbl" runat="server" Text='<%#Eval("NetProfitPer") %>'></asp:Label>


                                                        <asp:TextBox ID="lblNetProfitAmtPer" Text='<%#Eval("NetProfitPer") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>




                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPacking_Size" runat="server" Text='<%#Eval("Packing_Size") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Select (Make Action Default)" ControlStyle-Width="90px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox_Check" OnCheckedChanged="CheckBox_Check_CheckedChanged" runat="server" Checked="false" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStaffExpense" runat="server" Text='<%#Eval("StaffExpense") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMarketing" runat="server" Text='<%#Eval("Marketing") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIncentive" runat="server" Text='<%#Eval("Incentive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInterest" runat="server" Text='<%#Eval("Interest") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepoExpence" runat="server" Text='<%#Eval("DepoExpence") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther" runat="server" Text='<%#Eval("Other") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fk_State_Id Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_State_Id" runat="server" Text='<%#Eval("Fk_State_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fk_UnitMeasurement_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_UnitMeasurement_Id" runat="server" Text='<%#Eval("Fk_UnitMeasurement_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fk_PM_RM_Category_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_PM_RM_Category_Id" runat="server" Text='<%#Eval("Fk_PM_RM_Category_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fk_BPM_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_BPM_Id" runat="server" Text='<%#Eval("Fk_BPM_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FkPriceTypeId" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFkPriceTypeId" runat="server" Text='<%#Eval("FkPriceTypeId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TradeName_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsTradeName_Id" runat="server" Text='<%#Eval("TradeName_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </main>
        </div>

    </div>
    <script type="text/javascript">

        function calculaterate_actual(type, id) {

            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var grd = '<%=Grid_Default_PriceList_GP_Actual.ClientID%>';

            var lblrplncr = document.getElementById(grd + "_lblEnumDescription_" + index).innerHTML;

            if (type == '0' || type == '1' || type == '2' || type == '3') {

                if (lblrplncr.trim().toUpperCase() == "RPL") {

                    updatecalc_actual(index, index, grd, 1);

                    var t = parseInt(index) + 1;
                    updatecalc_actual(t, parseInt(t), grd, 0);


                    calculategrossprofit_actual(index, grd, type, 0);


                    var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                    if (check != null) {
                        check.checked = true;
                    }


                }
                else {

                    var lblid = parseInt(index) - 1;

                    updatecalc_actual(index, parseInt(lblid), grd, 0);

                    calculategrossprofit_actual(index, grd, type, 1);


                    var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                    if (check != null) {
                        check.checked = true;
                    }
                }


            }
            else {
                var isrplncr = 0;
                if (lblrplncr.trim().toUpperCase() == "NCR") {
                    isrplncr = 1;
                }

                calculategrossprofit_actual(index, grd, type, isrplncr)

                var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                if (check != null) {
                    check.checked = true;
                }

            }



        }

        function updatecalc_actual(mainid, lblindexid, grd, tp) {


            var pd = document.getElementById(grd + "_PDtxt_" + mainid);
            var qd = document.getElementById(grd + "_QDtxt_" + mainid);
            var tod = document.getElementById(grd + "_TODtxt_" + mainid);

            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + mainid);
            var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + lblindexid);



            if (tp == '1') {

                document.getElementById(grd + "_PDtxt_" + mainid).value = pd.value;
                document.getElementById(grd + "_QDtxt_" + mainid).value = qd.value;
                document.getElementById(grd + "_TODtxt_" + mainid).value = tod.value;

                document.getElementById(grd + "_ProfitPertxt_" + mainid).value = txtprofiramt.value;

                var final = lblFinalNAV.innerHTML;
                var total = parseFloat(tod.value) + parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final);
                var percentage = total * (parseFloat(txtprofiramt.value) / 100);

                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(percentage).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(percentage).toFixed(2);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(total + percentage).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(total + percentage).toFixed(2);

            }
            else {

                lblindexid = parseInt(lblindexid) - 1;
                var t = parseInt(mainid) - 1;


                pd = document.getElementById(grd + "_PDtxt_" + t);
                qd = document.getElementById(grd + "_QDtxt_" + t);
                tod = document.getElementById(grd + "_TODtxt_" + t);

                //alert(pd.value + '@' + qd.value);


                var rplestprice = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t).innerHTML) * (parseFloat(txtprofiramt.value) / 100);
                var newsuggprice = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t).innerHTML) - rplestprice - parseFloat(tod.value) - parseFloat(pd.value) - parseFloat(qd.value);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(newsuggprice).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(newsuggprice).toFixed(2);


                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(rplestprice).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(rplestprice).toFixed(2);



            }

        }
        function calculategrossprofit_actual(mainid, grd, type, isncr) {

            var AdditionalPDtxt = "";
            var GrossProfitAmounttxt = "";
            var GrossProfitAmountper = "";
            var TotalExpencetxt = "";
            var NetProfitAmounttxt = "";
            var NetProfitAmounttxt_per = "";
            var lblFinalNAV = "";
            var pd_lbl;
            var qd_lbl;
            var tod_lbl;

            if (isncr == '1') {

                var tid = parseInt(mainid) - 1;
                lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tid);
            }
            else {
                lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);
            }

            FinalPricettxt = document.getElementById(grd + "_FinalPricettxt_" + mainid);

            AdditionalPDtxt = document.getElementById(grd + "_AdditionalPDtxt_" + mainid);

            GrossProfitAmounttxt = document.getElementById(grd + "_GrossProfitAmounttxt_" + mainid);

            var GrossProfitAmounttxt_lbl = document.getElementById(grd + "_GrossProfitAmounttxt_lbl_" + mainid);

            GrossProfitAmountper = document.getElementById(grd + "_lblGrossProfitPer_" + mainid);

            var GrossProfitAmountper_lbl = document.getElementById(grd + "_lblGrossProfitPer_lbl_" + mainid);

            TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);



            NetProfitAmounttxt = document.getElementById(grd + "_NetProfitAmounttxt_" + mainid);

            var NetProfitAmounttxt_lbl = document.getElementById(grd + "_NetProfitAmounttxt_lbl_" + mainid);

            NetProfitAmounttxt_per = document.getElementById(grd + "_lblNetProfitAmtPer_" + mainid);

            var NetProfitAmounttxt_per_lbl = document.getElementById(grd + "_lblNetProfitAmtPer_lbl_" + mainid);

            pd_lbl = document.getElementById(grd + "_PDtxt_" + mainid);
            qd_lbl = document.getElementById(grd + "_QDtxt_" + mainid);
            tod_lbl = document.getElementById(grd + "_TODtxt_" + mainid);


            var grossprofit = parseFloat(FinalPricettxt.value) - parseFloat(AdditionalPDtxt.value) - parseFloat(lblFinalNAV.innerHTML) - parseFloat(pd_lbl.value) - parseFloat(qd_lbl.value) - parseFloat(tod_lbl.value);

            GrossProfitAmounttxt.value = Number(grossprofit).toFixed(2);
            GrossProfitAmounttxt_lbl.innerHTML = Number(grossprofit).toFixed(2);

            var grossprofit_per = Number(grossprofit * (100 / parseFloat(lblFinalNAV.innerHTML))).toFixed(2);
            GrossProfitAmountper.value = Number(grossprofit_per).toFixed(2);
            GrossProfitAmountper_lbl.innerHTML = Number(grossprofit_per).toFixed(2);
            /*
            var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);
            NetProfitAmounttxt.value = Number(netprofit).toFixed(2);
            NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);
 
            var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));
            NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);
            NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2);
            */
            var lblStaffExpense = document.getElementById(grd + "_lblStaffExpense_" + mainid);
            var lblMarketing = document.getElementById(grd + "_lblMarketing_" + mainid);
            var lblIncentive = document.getElementById(grd + "_lblIncentive_" + mainid);
            var lblInterest = document.getElementById(grd + "_lblInterest_" + mainid);
            var lblDepoExpence = document.getElementById(grd + "_lblDepoExpence_" + mainid);
            var lblOther = document.getElementById(grd + "_lblOther_" + mainid);


            var TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

            expense = (parseFloat(FinalPricettxt.value) * parseFloat(lblStaffExpense.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblMarketing.innerHTML) / 100) + + (parseFloat(FinalPricettxt.value) * parseFloat(lblIncentive.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblInterest.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblDepoExpence.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblOther.innerHTML) / 100)
            TotalExpencetxt.value = Number(expense).toFixed(2);
            TotalExpencetxt_lbl.innerHTML = Number(expense).toFixed(2);


            var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);
            NetProfitAmounttxt.value = Number(netprofit).toFixed(2);
            NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);

            var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));
            NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);
            NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2);

        }


        function calculaterate(type, id,ischk) {

            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();

            //1-pd,2-qd,3-pa,4-fp,5-adpd;

            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var grd = '<%=Grid_PriceList_GP_ActualEstimate.ClientID%>';

            var lblrplncr = document.getElementById(grd + "_lblEnumDescription_" + index).innerHTML;

            var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + index);
            var lblfinalrplncr = document.getElementById(grd + "_lblNewStatus_" + index);

            var txtpd = document.getElementById(grd + "_PDtxt_" + index);
            var txtqd = document.getElementById(grd + "_QDtxt_" + index);
            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + index);
            var txtfinalprice = document.getElementById(grd + "_FinalPricettxt_" + index);
            var txtaddpd = document.getElementById(grd + "_AdditionalPDtxt_" + index);

            var txtpd_ncr = document.getElementById(grd + "_PDtxt_" + index);
            var txtqd_ncr = document.getElementById(grd + "_QDtxt_" + index);
            var txtprofiramt_ncr = document.getElementById(grd + "_ProfitPertxt_" + index);
            var txtfinalprice_ncr = document.getElementById(grd + "_FinalPricettxt_" + index);
            var txtaddpd_ncr = document.getElementById(grd + "_AdditionalPDtxt_" + index);


            var lblProfitAmttxt = document.getElementById(grd + "_ProfitAmttxt_" + index);
            var lblSuggestedPrice = document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + index);
            var lblGrossProfitAmt = document.getElementById(grd + "_GrossProfitAmounttxt_" + index);
            var lblGrossProfitPer = document.getElementById(grd + "_lblGrossProfitPer_" + index);
            var lblNetProfitAmt = document.getElementById(grd + "_NetProfitAmounttxt_" + index);
            var lblNetProfirPer = document.getElementById(grd + "_lblNetProfitAmtPer_" + index);

            var lblNetProfitAmt_lbl = document.getElementById(grd + "_NetProfitAmounttxt_lbl_" + index);



            if (type == '1' || type == '2' || type == '3') {

                if (lblrplncr.trim().toUpperCase() == "RPL") {

                    updatecalc(index, index, grd, 1);

                    var t = parseInt(index) + 1;

                    updatecalc(t, parseInt(t), grd, 0)

                    calculategrossprofit(index, grd, type);

                    if (ischk == 0) {

                    var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                        if (check != null) {
                            check.checked = true;
                        }
                    }


                }
                else {

                    updatecalc(index, parseInt(index), grd, 0)
                    var t = parseInt(index) + 1;

                    calculategrossprofit(t, grd, type);

                    if (ischk == 0) {

                        var check = document.getElementById(grd + "_CheckBox_Check_" + t);
                        if (check != null) {
                            check.checked = true;

                        }
                    }
                }


            }
            else {

                calculategrossprofit(index, grd, type)

            }


        }

        function updatecalc(mainid, lblindexid, grd, tp) {



            var pd = document.getElementById(grd + "_PDtxt_" + mainid);
            var qd = document.getElementById(grd + "_QDtxt_" + mainid);
            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + mainid);
            var txtfinalprice = document.getElementById(grd + "_FinalPricettxt_" + mainid);
            var txtaddpd = document.getElementById(grd + "_AdditionalPDtxt_" + mainid);

            var newid = parseInt(mainid) + 2;

            if (tp == '1') {

                document.getElementById(grd + "_PDtxt_" + newid).value = pd.value;
                document.getElementById(grd + "_QDtxt_" + newid).value = qd.value;
                document.getElementById(grd + "_ProfitPertxt_" + newid).value = txtprofiramt.value;

                var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + lblindexid);
                var final = lblFinalNAV.innerHTML;
                var total = parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final);
                var percentage = total * (parseFloat(txtprofiramt.value) / 100);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(total + percentage).toFixed(2);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(total + percentage).toFixed(2);

                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(percentage).toFixed(2);

                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(percentage).toFixed(2);

                var lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + newid);
                var final_sub = lblFinalNAV_sub.innerHTML;
                var total_sub = parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final_sub);
                var percentage_sub = total_sub * (parseFloat(txtprofiramt.value) / 100);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + newid).value = Number(total_sub + percentage_sub).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + newid).innerHTML = Number(total_sub + percentage_sub).toFixed(2);


                document.getElementById(grd + "_ProfitAmttxt_" + newid).value = Number(percentage_sub).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + newid).innerHTML = Number(percentage_sub).toFixed(2);
            }
            else {




                lblindexid = parseInt(lblindexid) - 1;

               


                document.getElementById(grd + "_PDtxt_" + newid).value = pd.value;
                document.getElementById(grd + "_QDtxt_" + newid).value = qd.value;

                document.getElementById(grd + "_ProfitPertxt_" + newid).value = txtprofiramt.value;

                var t = parseInt(mainid) - 1;

                pd = document.getElementById(grd + "_PDtxt_" + t);
                qd = document.getElementById(grd + "_QDtxt_" + t);



                var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + lblindexid);
                var final = lblFinalNAV.innerHTML;

                var total = parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final);
                var percentage = total * (parseFloat(txtprofiramt.value) / 100);


                
                var rplestprice = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t).innerHTML) * (parseFloat(txtprofiramt.value) / 100);


                var newsuggprice = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t).innerHTML) - rplestprice - parseFloat(pd.value) - parseFloat(qd.value);


                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(newsuggprice).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(newsuggprice).toFixed(2);




                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(rplestprice).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(rplestprice).toFixed(2);



                var nid = parseInt(lblindexid) + 2;

                var lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + nid);

                var final_sub = lblFinalNAV_sub.innerHTML;
                var total_sub = parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final_sub);
                var percentage_sub = 0.0;
                percentage_sub = total_sub * (parseFloat(txtprofiramt.value) / 100);

                var t1 = parseInt(newid) - 1;

                //alert(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t1).innerHTML);

                percentage_sub = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t1).innerHTML) * (parseFloat(txtprofiramt.value) / 100);

                var rplestprice_sub = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t1).innerHTML) * (parseFloat(txtprofiramt.value) / 100);
                var newsuggprice_sub = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + t1).innerHTML) - rplestprice_sub - parseFloat(pd.value) - parseFloat(qd.value);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + newid).value = Number(newsuggprice_sub).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + newid).innerHTML = Number(newsuggprice_sub).toFixed(2);


                document.getElementById(grd + "_ProfitAmttxt_" + newid).value = Number(rplestprice_sub).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + newid).innerHTML = Number(rplestprice_sub).toFixed(2);


            }


            /*

            lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + newid);

            
            var final_sub = lblFinalNAV_sub.innerHTML;
            var total_sub = parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final_sub);

            var percentage_sub = total_sub * (parseFloat(txtprofiramt.value) / 100);

            document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + newid).innerHTML = Number(total_sub + percentage_sub).toFixed(2);
            document.getElementById(grd + "_ProfitAmttxt_" + newid).innerHTML = Number(percentage_sub).toFixed(2);
            */
            /*
            var tid = parseInt(newid) - 1;

            var lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + tid);
            */
        }

        function calculategrossprofit(mainid, grd, type) {

            var status = document.getElementById("<%=lblactest.ClientID%>").innerHTML;
            var status_cal = document.getElementById(grd + "_lblNewStatus_" + mainid);



            var act_est = "";
            /*
            if (type == '1' || type == '2' || type == '3') {
 
                var enum_ = document.getElementById(grd + "_lblEnumDescription_" + mainid);
                
                if (enum_.innerHTML.trim().toUpperCase() == "RPL") {
                    
                    act_est = document.getElementById(grd + "_lblStatus_" + mainid).innerHTML.trim().toUpperCase();
 
                }
                else {
                    
                    var t = parseInt(mainid) - 1;
                    act_est = document.getElementById(grd + "_lblStatus_" + t).innerHTML.trim().toUpperCase();
                    // mainid = parseInt(mainid) - 1;
                }
 
              
 
            }
            else {
               
                act_est = document.getElementById(grd + "_lblStatus_" + mainid).innerHTML.trim().toUpperCase();
            }
            */

            act_est = document.getElementById(grd + "_lblStatus_" + mainid).innerHTML.trim().toUpperCase();

            //alert(act_est);

            var lblFinalNAV = "";
            var pd_lbl = "";
            var qd_lbl = "";
            var finalprice = "";
            var newid = 0;
            var lblindex = 0;
            var qdpd = 0;
            var AdditionalPDtxt = "";
            var GrossProfitAmounttxt = "";
            var GrossProfitAmountper = "";
            var TotalExpencetxt = "";
            var NetProfitAmounttxt = "";
            var NetProfitAmounttxt_per = "";

            FinalPricettxt = document.getElementById(grd + "_FinalPricettxt_" + mainid);

            AdditionalPDtxt = document.getElementById(grd + "_AdditionalPDtxt_" + mainid);

            GrossProfitAmounttxt = document.getElementById(grd + "_GrossProfitAmounttxt_" + mainid);

            var GrossProfitAmounttxt_lbl = document.getElementById(grd + "_GrossProfitAmounttxt_lbl_" + mainid);

            GrossProfitAmountper = document.getElementById(grd + "_lblGrossProfitPer_" + mainid);

            var GrossProfitAmountper_lbl = document.getElementById(grd + "_lblGrossProfitPer_lbl_" + mainid);

            TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

            NetProfitAmounttxt = document.getElementById(grd + "_NetProfitAmounttxt_" + mainid);

            var NetProfitAmounttxt_lbl = document.getElementById(grd + "_NetProfitAmounttxt_lbl_" + mainid);

            NetProfitAmounttxt_per = document.getElementById(grd + "_lblNetProfitAmtPer_" + mainid);

            var NetProfitAmounttxt_per_lbl = document.getElementById(grd + "_lblNetProfitAmtPer_lbl_" + mainid);

            var expenseind = 0;

            if (status.trim().toUpperCase() == "ACTUAL") {


                if (act_est == "ACTUAL") {

                    lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);

                    expenseind = parseInt(mainid);

                }
                else {

                    var tid = parseInt(mainid) + 2;
                    lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tid);



                    expenseind = parseInt(mainid) + 1;

                }

                if (status_cal.innerHTML.trim().toUpperCase() == "RPL") {

                    lblindex = mainid;
                    qdpd = mainid;
                }
                else {
                    lblindex = parseInt(mainid) + 1;
                    qdpd = parseInt(mainid) + 1;
                }
            }
            else {

                var id = parseInt(mainid) + 2;

                if (act_est == "ACTUAL") {

                    var tid = parseInt(mainid) + 2;
                    lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tid);
                    expenseind = parseInt(mainid);
                }
                else {

                    lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);

                    expenseind = parseInt(mainid) + 1;
                }

                if (status_cal.innerHTML.trim().toUpperCase() == "RPL") {
                    lblindex = mainid;
                    qdpd = parseInt(id);
                }
                else {
                    lblindex = parseInt(mainid) + 1;
                    qdpd = parseInt(mainid) + 1;
                }

            }


            pd_lbl = document.getElementById(grd + "_PDtxt_" + qdpd);
            qd_lbl = document.getElementById(grd + "_QDtxt_" + qdpd);

            // alert(pd_lbl.id + '@' + qd_lbl.id);

            // alert(pd_lbl.value + '@' + qd_lbl.value);

            var grossprofit = parseFloat(FinalPricettxt.value) - parseFloat(AdditionalPDtxt.value) - parseFloat(lblFinalNAV.innerHTML) - parseFloat(pd_lbl.value) - parseFloat(qd_lbl.value);

            GrossProfitAmounttxt.value = Number(grossprofit).toFixed(2);

            GrossProfitAmounttxt_lbl.innerHTML = Number(grossprofit).toFixed(2);


            var grossprofit_per = Number(grossprofit * (100 / parseFloat(lblFinalNAV.innerHTML))).toFixed(2);

            GrossProfitAmountper.value = Number(grossprofit_per).toFixed(2);

            GrossProfitAmountper_lbl.innerHTML = Number(grossprofit_per).toFixed(2);

            /*
 
            var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);
 
            NetProfitAmounttxt.value = Number(netprofit).toFixed(2);
 
            NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);
 
            var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));
 
            NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);
 
            NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2);
            */

            var lblStaffExpense = document.getElementById(grd + "_lblStaffExpense_" + expenseind);
            var lblMarketing = document.getElementById(grd + "_lblMarketing_" + expenseind);
            var lblIncentive = document.getElementById(grd + "_lblIncentive_" + expenseind);
            var lblInterest = document.getElementById(grd + "_lblInterest_" + expenseind);
            var lblDepoExpence = document.getElementById(grd + "_lblDepoExpence_" + expenseind);
            var lblOther = document.getElementById(grd + "_lblOther_" + expenseind);

            var expense = 0.0;

            var TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

            //alert(TotalExpencetxt.innerHTML + '@' + lblStaffExpense.innerHTML + '@' + lblMarketing.innerHTML + '@' + lblIncentive.innerHTML + '@' + lblInterest.innerHTML + '@' + lblDepoExpence.innerHTML + '@' + lblOther.innerHTML);

            expense = (parseFloat(FinalPricettxt.value) * parseFloat(lblStaffExpense.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblMarketing.innerHTML) / 100) + + (parseFloat(FinalPricettxt.value) * parseFloat(lblIncentive.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblInterest.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblDepoExpence.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblOther.innerHTML) / 100)

            TotalExpencetxt.value = Number(expense).toFixed(2);

            TotalExpencetxt_lbl.innerHTML = Number(expense).toFixed(2);

            var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);

            NetProfitAmounttxt.value = Number(netprofit).toFixed(2);

            NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);

            var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));

            NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);

            NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2);


            //alert('FinalPricettxt:' + FinalPricettxt.value + '@lblStaffExpense :' + lblStaffExpense.innerHTML + '@' + 'final nrv :' + lblFinalNAV.innerHTML + 'act_est :' + act_est);

            // alert(TotalExpencetxt.innerHTML);

            // alert('FinalPricettxt:' + FinalPricettxt.value + '@status :' + status + '@' + 'final nrv :' + lblFinalNAV.innerHTML + 'status_cal :' + status_cal.innerHTML + '@qd :' + qd_lbl.value);



        }

    </script>

</asp:Content>
