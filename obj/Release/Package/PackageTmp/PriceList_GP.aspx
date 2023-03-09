<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="PriceList_GP.aspx.cs" Inherits="Production_Costing_Software.PriceList_GP" %>

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

        .customCellMergeTH {
            color: #fff !important;
            background: #343a40;
            border-left: solid 1px #666;
            font-size: medium;
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
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img src="Content/LogoImage/gifntext-gif.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
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

                        <asp:Label ID="lblDynamicColumnCountActual" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblDynamicColumnCount" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblTradeName_Id" runat="server" Text="" Visible="false" ></asp:Label>
                        <asp:Label ID="lblIsMasterPacking" runat="server" Text="" Visible="false"></asp:Label>



                        <%----------------------------------%>


                        <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                            <h1 class="mt-4">PriceList GP Actual Estimate</h1>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="PriceList_GP.aspx">PriceList GP Actual Estimate
                                </a></li>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    <asp:Label ID="lblBulkProductName" Style="font-size: larger" runat="server" Text="" Enabled="false"></asp:Label>

                                    <div class="col-auto">
                                        <%--                                        <asp:Button ID="GridAddPriceGP" OnClick="GridAddPriceGP_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" ValidationGroup="add" Text="+Add" runat="server" class="btn btn-success align-content-end" />--%>
                                    </div>
                                    <div>
                                        <%--<asp:Button ID="ReportPopupBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ReportPopupBtn_Click" Text="Report" runat="server" OnClientClick="ClearHtml();" data-bs-toggle="modal" data-bs-target="#ReportlModal" class="btn btn-success btn-sm float-md-end" />--%>
                                        <%--<asp:Button ID="GPAcualFinalBtn" OnClick="GPAcualFinalBtn_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Price List GP Actual Final" runat="server" class="btn btn-outline-danger btn-sm float-md-end" />--%>
                                    </div>
                                    <%--         <div>
                                        <marquee direction="left" style="color: red; font-family: monospace">
                                            Work in Progress !
                                        </marquee>
                                    </div>--%>
                                    <div class="col-auto">
                                        <asp:Button ID="CreatePriceList" runat="server" Text="Create PriceList" CssClass="btn btn-danger float-md-start" Enabled="false" OnClick="CreatePriceList_Click" />
                                        <asp:TextBox ID="CreatePriceListtxt" CssClass="form-control" OnTextChanged="CreatePriceListtxt_TextChanged" Enabled="false" AutoPostBack="true" Width="200px" runat="server"></asp:TextBox>
                        <%--                <asp:DropDownList ID="EstimatedPriceDropdown" AutoPostBack="true" Width="200px" CssClass="form-select float-end m-1" runat="server" OnSelectedIndexChanged="EstimatedPriceDropdown_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Estimate</asp:ListItem>
                                            <asp:ListItem Value="0">Actual</asp:ListItem>
                                            <asp:ListItem Value="2">Both</asp:ListItem>

                                        </asp:DropDownList>--%>
                                        <asp:Button ID="btnEditing" runat="server" Text="Edit"  CssClass="btn btn-dark float-md-end m-1" OnClick="btnEditing_Click" />
                                    <%--    <div class="col-auto">
                                            <asp:Button ID="btnEditActual" runat="server" Visible="false" Text="Edit Actual" CssClass="btn btn-dark float-md-end m-1" OnClick="btnEditActual_Click" />
                                        </div>--%>
                                    </div>
                                    <div class="align-content-end">
                                        <asp:Button ID="PdfReport" OnClick="PdfReport_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Pdf Report" runat="server" class="btn btn-danger  float-end  float-end m-1" />
                                    </div>
                                </div>
                                <asp:Label ID="lblCompanyFactoryExpence_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblPMRM_Category_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lbl_BPM_Id" runat="server" Text="" Visible="false"></asp:Label>

                                <div class="card-body" style="overflow: auto">

                                    <asp:GridView ID="Grid_PriceList_GP_Actual" CssClass=" table-hover table-responsive gridview" Visible="true"
                                        OnRowCreated="Grid_PriceList_GP_Actual_RowCreated" EnableViewState="true" OnRowDataBound="Grid_PriceList_GP_Actual_RowDataBound"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" DataKeyNames="Fk_BPM_Id">
                                    </asp:GridView>

                                </div>


                                <div class="card-body" style="overflow: auto">
                                    <asp:GridView ID="Grid_PriceList_GP_Actual_Estimate" CssClass=" table-hover table-responsive gridview"
                                        OnRowCreated="Grid_PriceList_GP_Actual_Estimate_RowCreated" EnableViewState="true" Visible="false"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager" OnRowDataBound="Grid_PriceList_GP_Actual_Estimate_RowDataBound"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" DataKeyNames="Fk_BPM_Id">
                                    </asp:GridView>
                                </div>

                                <div class="modal fade" id="ReportlModal" tabindex="-1">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content col-lg-12">
                                            <div class="modal-header" style="background-color: #343a40">
                                                <%--                                            <h3 style="color: white">Bulk Costing</h3>/<asp:Label ID="lblname" runat="server" Text="" ></asp:Label>--%>
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
                            </div>
                        </div>
                        
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnEditing" />

                        <asp:PostBackTrigger ControlID="PdfReport" />
                    </Triggers>
                </asp:UpdatePanel>

            </main>
        </div>

    </div>
</asp:Content>
