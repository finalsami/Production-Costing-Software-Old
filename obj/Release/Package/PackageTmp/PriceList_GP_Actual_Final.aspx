<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PriceList_GP_Actual_Final.aspx.cs" Inherits="Production_Costing_Software.PriceList_GP_Actual_Final" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="Content/CSS/loading.css" rel="stylesheet" />
    <style type="text/css">
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
        function ClearHtml() {
            document.getElementById("ContentPlaceHolder1_UpdatePanel2").innerHTML = "";
        }
    </script>


    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>

                <div class="modal fade" id="RM_ReportModal" tabindex="-1">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content col-lg-12">
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

                        <asp:Label ID="lblCompany_Id" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lblIsMasterPacking" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblDynamicColumnCount" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblStateNameAlert" runat="server" Text="" Visible="false"></asp:Label>


                        <asp:Label ID="lblNewStatus" runat="server" Text="" Visible="false"></asp:Label>



                        <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblFinalPrice" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblFinalNRV" runat="server" Text="" Visible="false"></asp:Label>

                        <%----------------------------------%>


                        <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                            <h1 class="mt-4">PriceList GP Actual Final</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="PriceList_GP_Actual_Final.aspx">PriceList GP Actual Final
                                </a></li>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    <asp:Label ID="lblBulkProductName" Style="font-size: larger" runat="server" Text="" Enabled="false"></asp:Label>
                                    <img src="Content/Other/Right%20Arrow.png" style="width: 20px; height: 20px" />
                                    <asp:Button ID="GPAcualFinalBtn" OnClick="GPAcualFinalBtn_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Price List GP Actual" runat="server" class="btn btn-outline-danger btn-sm" />


                                    <div class="align-content-end">
                                        <asp:Button ID="GridAddPriceGP" OnClick="GridAddPriceGP_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" ValidationGroup="add" Text="+Add" runat="server" class=" btn btn-primary  float-md-end  m-2" />
                                    </div>
                                    <div class="align-content-end">
                                        <asp:Button ID="ReportPopupBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CssClass="btn btn-success float-end  m-2" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ReportPopupBtn_Click" Text="View Report" runat="server" OnClientClick="ClearHtml();" data-bs-toggle="modal" data-bs-target="#RM_ReportModal" />
                                    </div>
                                    <div class="align-content-end">
                                        <asp:Button ID="PdfReport" OnClick="PdfReport_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Pdf Report" runat="server" class="btn btn-danger  float-end m-2" />
                                    </div>
                                </div>

                                <asp:Label ID="lblCompanyFactoryExpence_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>

                                <asp:Label ID="lblPDActual" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblQDActual" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblGrid_Status" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblTradeName_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPriceType" runat="server" Text="" Visible="false"></asp:Label>

                                <asp:Panel ID="Panel1" runat="server" Height="500px">
                                    <div class="card-body" style="overflow: auto">

                                        <asp:GridView ID="Grid_Default_PriceList_GP_Actual" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
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

                                                <asp:TemplateField HeaderText="PD" ControlStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="PDtxt" CssClass="form-control" Text='<%#Eval("PD") %>' TextMode="Number" Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QD" ControlStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="QDtxt" CssClass="form-control" Text='<%#Eval("QD") %>' TextMode="Number" Style="text-align: center" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Profit Amt(%)" ControlStyle-Width="140px">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblProfitPer" runat="server" Text=""></asp:Label>--%>
                                                        <asp:Label ID="ProfitAmttxt" AutoPostBack="true" Text='<%#Eval("ProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="ProfitPertxt" AutoPostBack="true" OnTextChanged="ProfitPertxt_TextChanged" CssClass="form-control" Text='<%#Eval("ProfitPer") %>' TextMode="Number" Style="width: 25px" runat="server"></asp:TextBox>
                                                            <span class="input-group-text" id="basic-addon2">%</span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Suggested-Price with PD" ControlStyle-Width="140px">
                                                    <ItemTemplate>

                                                        <asp:Label ID="SuggestedPriceWithPDttxt" Text='<%#Eval("SuggestedPriceWithPD") %>' Style="text-align: center" runat="server"></asp:Label>

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last Shared Final Price" ControlStyle-Width="140px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLast_Shared_Final_Price" runat="server" Style="text-align: center" Text='<%#Eval("LastSharedPrice") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FinalPrice(Rs)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="FinalPricettxt" OnTextChanged="FinalPricettxt_TextChanged" AutoPostBack="true" CssClass="form-control" TextMode="Number" Text='<%#Eval("FinalPrice") %>' Style="width: 50px" runat="server"></asp:TextBox>
                                                            <span class="input-group-text" id="finalpriceaddon">Rs</span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="AdditionalPD" ControlStyle-Width="140px">
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="AdditionalPDtxt" OnTextChanged="AdditionalPDtxt_TextChanged" AutoPostBack="true" CssClass="form-control" TextMode="Number" Text='<%#Eval("AdditionalPD") %>' Style="width: 50px" runat="server"></asp:TextBox>
                                                            <span class="input-group-text" id="finalpriceaddon2">Rs</span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gross ProfitAmount (%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <asp:Label ID="GrossProfitAmounttxt" TextMode="Number" CssClass="form-control" Text='<%#Eval("GrossProfitAmt") %>' Style="width:70px" runat="server"></asp:Label>
                                                            <span class="input-group-text" id="finalpriceaddon">Rs</span>
                                                            <asp:Label ID="lblGrossProfitPer" runat="server" Text='<%#Eval("GrossProfitPer") %>' placeholder="%"></asp:Label>
                                                            </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Expence" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TotalExpencetxt" Text='<%#Eval("TotalExpence") %>' Style="text-align: center" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Net Profit Amount (%)" ControlStyle-Width="130px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NetProfitAmounttxt" TextMode="Number" Text='<%#Eval("NetProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                        <asp:Label ID="lblNetProfitAmtPer" runat="server" Text='<%#Eval("NetProfitPer")+"%" %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStaffExpense" runat="server" Text='<%#Eval("StaffExpense") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMarketing" runat="server" Text='<%#Eval("Marketing") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIncentive" runat="server" Text='<%#Eval("Incentive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInterest" runat="server" Text='<%#Eval("Interest") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepoExpence" runat="server" Text='<%#Eval("DepoExpence") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Size" Visible="false">
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
                                            </Columns>

                                        </asp:GridView>

                                        <br />

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="PdfReport" />
                    </Triggers>
                </asp:UpdatePanel>
            </main>
        </div>

    </div>
</asp:Content>
