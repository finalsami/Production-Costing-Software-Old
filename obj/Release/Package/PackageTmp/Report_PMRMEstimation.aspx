<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report_PMRMEstimation.aspx.cs" Inherits="Production_Costing_Software.Report_PMRMEstimation" %>

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
            /*margin: 290px auto;*/
            /*padding: 10px;*/
            width: 300px;
            opacity: 0.9;
        }

            .centerLoading img {
                height: 150px;
                width: 250px;
            }

            .TableWidthLayout {
                table-layout:fixed;
            }
    </style>



    <script type="text/javascript">
        function ShowPopup1() {
            $("#staticBackdrop").modal("show");
            var cells = document.getElementsByClassName("modal-backdrop");
            for (var i = 0; i < cells.length; i++) {
                cells[i].style.zIndex = 1048;
            }
        }
    </script>

    <script type="text/javascript">
        function ShowPMRMEstimateReport() {
            $("#PMRMEstimateReport").modal("show");
            var cells = document.getElementsByClassName("modal-backdrop");
            for (var i = 0; i < cells.length; i++) {
                cells[i].style.zIndex = 1054;
            }
        }
    </script>
    <script type="text/javascript">
        function ShowRMReport() {
            $("#PMRM_ReportModal").modal("show");
            var cells = document.getElementsByClassName("modal-backdrop");
            for (var i = 0; i < cells.length; i++) {
                cells[i].style.zIndex = 1059;
            }
        }
    </script>
    <script type="text/javascript">
        function HideRMReport() {
            $("#PMRM_ReportModal").modal("hide");
            var cells = document.getElementsByClassName("modal-backdrop");
            for (var i = 0; i < cells.length; i++) {
                cells[i].style.zIndex = 0;
            }
        }
    </script>
    <script type="text/javascript">
        function HidePMRMEstimateReport() {
            $("#PMRMEstimateReport").modal("hide");
            var cells = document.getElementsByClassName("modal-backdrop");
            for (var i = 0; i < cells.length; i++) {
                cells[i].style.zIndex = 0;
            }
        }
    </script>


    <script type="text/javascript">
        function ShowStateWiseRMEstReport() {
            $("#StateWiseReportModal").modal("show");
        }
    </script>
    <script type="text/javascript">
        function HideStateWiseRMEstReport() {
            $("#StateWiseReportModal").modal("hide");
            var cells = document.getElementsByClassName("modal-backdrop fade show");
            for (var i = 0; i < cells.length; i++) {
                cells[i].style.zIndex = 0;
            }
        }
    </script>
    <script type="text/javascript">
        function ClearHtml() {
            document.getElementById("ContentPlaceHolder1_UpdatePanel1").innerHTML = "";
        }
    </script>

    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <%--Lable--%>
                <asp:Label ID="lblRoleId" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblUserId" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblGroupId" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblCompanyMasterList_Id" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblUserNametxt" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblPrice_Id" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="modalLoading">
                                    <div class="centerLoading">
                                        <img src="Content/LogoImage/gifntext-gif.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div class="container-fluid px-4">
                            <h2 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">PMRM Price Estimation:</h2>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="Report_PMRMEstimation.aspx">PMRM Price Estimation</a></li>
                                <%--   <li class="breadcrumb-item active">Main Category</li>--%>
                            </ol>
                            <%--Lables--%>
                            <asp:Label ID="lblCompanyMasterList_Name" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblUserMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCanEdit" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblGRidEstimateName" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>
                            <%-----------------------%>

                            <div class="card mb-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Estimation:
                                  
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
                                            <asp:Button ID="AddPMRMEstimationBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-primary" OnClick="AddPMRMEstimationBtn_Click" CausesValidation="false" runat="server" Text="+ Add" />

                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class=" mt-4">

                                    <asp:GridView ID="Gird_PMRM_PriceEstimationList" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="EstimateName">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Estimate Date" ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("RM_Estimate_Date", "{0:dd, MMM yyyy}") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EstimateName" HeaderText="Estimate Name" SortExpression="MainCategory_Name" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="GridEdit" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="GridEdit_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CausesValidation="false" runat="server" class="btn btn-success btn-sm" Text="Edit" />
                                                    <asp:Button ID="EstimatePMRMRateModal" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EstimatePMRMRateModal_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CausesValidation="false" runat="server" class="btn btn-info btn-sm" Text="View Estimate Rate" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>



                                </div>
                            </div>
                        </div>
                        <%--RMEstimateGRid Modal--%>

                        <div class="modal fade" id="staticBackdrop" style="z-index: 1049 !important" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="staticBackdropLabel">PMRM Estimate:
                                        <h6>[<asp:Label ID="lblPMRMEstimateName" runat="server" Text=""></asp:Label>
                                            ] </h6>
                                        </h5>
                                        <%--<button type="button" runat="server" id="ModifiedPMRMModalClose" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>--%>
                                        <asp:Button ID="ModifiedPMRMModalClose" runat="server" CssClass="btn-close" OnClick="ModifiedPMRMModalClose_Click" />

                                    </div>
                                    <div class="modal-body">
                                        <div class="row mb-3">
                                            <div class="col-md-6 ">
                                                <div class="input-group-text">
                                                    <label for="Category">Date of Estimate:</label>
                                                    <asp:TextBox ID="DOPtxt" type="Date" TabIndex="3" ClientIDMode="Static" CssClass="form-control" placeholder="Please specify a date"
                                                        onClick="$(this).removeClass('placeholderclass')"
                                                        runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group-text">
                                                    <label for="EstimateNametxt">Estimate Name:</label>
                                                    <asp:TextBox ID="EstimateNametxt" class="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="offset-1" ControlToValidate="EstimateNametxt" runat="server" ForeColor="Red" ErrorMessage="* LastName "></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-6">
                                                <div class="input-group mb-3">
                                                    <label class="input-group-text">RM Estimate</label>
                                                    <asp:DropDownList ID="RMEstimateDropdown" OnSelectedIndexChanged="RMEstimateDropdown_SelectedIndexChanged1" class="form-select" runat="server">
                                                        <asp:ListItem Selected="True">Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mb-3">

                                            <div class="col-md-8" style="border: 1px;">
                                                <div class="input-group" style="height: 300px">
                                                    <label for="EstimateNametxt">Ingredient:</label>

                                                    <asp:ListBox ID="PMRM_IngredientListbox" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" SelectionMode="Multiple" class="form-control"></asp:ListBox>

                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <asp:Button ID="ChkPMRMSubmit" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ChkPMRMSubmit_Click" runat="server" CssClass="btn btn-success" Text="+ Add" />
                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="ChkBulkClear" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" CssClass="btn btn-danger" Text="ClearAll" />
                                            </div>
                                        </div>
                                        <%--             <div class="row mb-3">
                                            <div class="col-sm-5">
                                                <div class="input-group">
                                                    <div class="form-check form-switch">

                                                        <input type="checkbox" id="IsActive" runat="server" class="form-check-input ">

                                                        <label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <hr />
                                    <div class="card-body overflow-scroll">
                                        <asp:GridView ID="Grid_PMRMEstimateTableOne" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager" DataKeyNames="PMRM_EstimateTabOne_Id" AutoGenerateColumns="false"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server">

                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRMEstimate_Name" runat="server" Text='<%#  Eval("RMEstimate_Name") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstimate_PMRMPrice_Id" runat="server" Text='<%#  Eval("Estimate_PMRMPrice_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPMRM_EstimateTabOne_Id" runat="server" Text='<%#  Eval("PMRM_EstimateTabOne_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PMRM Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPM_RM_Name" runat="server" Text='<%#  Eval("PM_RM_Name") %>' HeaderText="Actual Price" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EsitmateName" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstimateName" runat="server" Text='<%#  Eval("EstimateName") %>' HeaderText="Actual Price" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPM_RM_Price_Unit" runat="server" Text='<%#  Eval("PM_RM_Price_Unit") %>' HeaderText="Actual Price" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EstimateDate" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstimateDate" runat="server" Text='<%#  Eval("EstimateDate") %>' HeaderText="EstimateDate" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modified Price">

                                                    <ItemTemplate>

                                                        <asp:TextBox ID="ModifiedPricetxt" runat="server" Text='<%#  Eval("PMRM_ModifiedPrice") %>'></asp:TextBox>

                                                        <asp:Button ID="DelGridPMRmModifiedBtn" OnClick="DelGridPMRmModifiedBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" CssClass="btn btn-danger btn-sm" Text="X" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="AddModifiedPriceBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddModifiedPriceBtn_Click" CausesValidation="false" runat="server" class="btn btn-primary" data-bs-dismiss="modal" Text="Submit" />
                                        <asp:Button ID="ModifiedPriceUpdateBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ModifiedPriceUpdateBtn_Click" runat="server" CausesValidation="false" class="btn btn-success" data-bs-dismiss="modal" Text="Update" />
                                        <asp:Button ID="ViewPMRMEstimateReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ViewPMRMEstimateReport_Click" CausesValidation="false" runat="server" class="btn btn-info" data-bs-dismiss="modal" Text="Report" />
                                    </div>
                                </div>

                            </div>
                        </div>

                        <%--------------------%>
                        <div runat="server" id="divPMRMEsitmateReportModal">
                            <div class="modal fade" id="PMRMEstimateReport" style="z-index: 1055 !important" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel2" aria-hidden="true">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            RM Estimate Name: [
                                            <asp:Label ID="lblRMEstimateName" runat="server"></asp:Label>] /  PMRM Estimate Name: [<asp:Label ID="lblPMRMEstimateNameHeader" runat="server"></asp:Label>]
                                        <%--<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>--%>
                                            <asp:Button ID="PMRMEstimateReportClose" CssClass="btn-close" runat="server" OnClick="PMRMEstimateReportClose_Click" />

                                        </div>
                                        <div class="modal-body overflow-scroll">
                                            <div class="card-body">
                                                <asp:GridView ID="Grid_PMRM_EstimateReport" CssClass=" table-hover table-responsive gridview"
                                                    GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                    PagerStyle-CssClass="gridview_pager" DataKeyNames="Fk_BPM_Id" AutoGenerateColumns="false"
                                                    AlternatingRowStyle-CssClass="gridview_alter" runat="server">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BPM_Product_Name" ControlStyle-CssClass="align-content-md-start" HeaderText="bulk Name" SortExpression="MainCategory_Name" />
                                                        <asp:BoundField DataField="PackingUnitMeasurement" ControlStyle-CssClass="align-content-md-start" HeaderText="Master Pack" SortExpression="MainCategory_Name" />
                                                        <asp:BoundField DataField="FinalBulkCostPerLtr" ControlStyle-CssClass="align-content-md-start" HeaderText="TotalAmtPerUnit" SortExpression="MainCategory_Name" />
                                                        <asp:BoundField DataField="ModifiedFinalBulkCostPerUnit" ControlStyle-CssClass="align-content-md-start" HeaderText="ModifiedTotalAmtPerLtr" SortExpression="MainCategory_Name" />

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:Button ID="PMRMEstReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-dismiss="modal" data-bs-toggle="modal" OnClick="PMRMEstReport_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CausesValidation="false" runat="server" class="btn btn-success btn-sm" Text="View Report" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="StateWise Report">
                                                            <ItemTemplate>
                                                                <asp:Button ID="StateWiseBulkCosttReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-dismiss="modal" data-bs-toggle="modal" OnClick="StateWiseBulkCosttReport_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CausesValidation="false" runat="server" class="btn btn-primary btn-sm" Text="State Report" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>


                        <%---------------PMRM_ReportModal-----------------------%>
                        <div runat="server" id="divPMRM_ReportModal">
                            <div class="modal fade" id="PMRM_ReportModal" tabindex="-1">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content col-lg-12">
                                        <div class="modal-header" style="background-color: #343a40">
                                            <div style="color: white">
                                                PMRM Estimate Report: 
                                            <h4 style="color: white">[<asp:Label ID="lblPMRMEstimateHeader" runat="server" CssClass="font-monospace"></asp:Label>]</h4>
                                            </div>


                                            <asp:Label ID="lblname" runat="server" CssClass="font-monospace" Style="color: white" Visible="false"></asp:Label>
                                            <asp:Label ID="lblPackingSize" runat="server" CssClass="font-monospace" Style="color: white" Visible="false"></asp:Label>

                                            <asp:Button ID="PMRM_ReportModalClose" CssClass="btn-close" runat="server" OnClick="PMRM_ReportModalClose_Click" />
                                            <%--<button type="button" class="btn-close" aria-hidden="true" data-bs-dismiss="modal" aria-label="Close"></button>--%>
                                        </div>
                                        <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                        <div class="modal-body overflow-scroll">

                                            <asp:Table runat="server" CssClass=" table-hover table-responsive gridview" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">


                                                <asp:TableRow>
                                                    <asp:TableCell>

                                                        <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--StateWiseRM_ReportModal--%>
                        <div runat="server" id="divStateWiseReportModal">
                            <div class="modal fade" id="StateWiseReportModal" tabindex="-1">
                                <div class="modal-dialog modal-fullscreen">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #343a40">

                                            <asp:Button ID="ReportClose" runat="server" Text="X" OnClick="ReportClose_Click2" />
                                            <%--<button type="button" class="btn-close" style="color: whitesmoke" onclick="ReportClose_Click2" data-bs-dismiss="modal" aria-label="Close"></button>--%>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Table runat="server" CssClass=" table-hover table-responsive gridview" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:GridView ID="EstimateCostBulkReportGrid" CssClass=" table-hover table-responsive gridview "
                                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                            PagerStyle-CssClass="gridview_pager" DataKeyNames="Fk_BPM_Id" AutoGenerateColumns="false"
                                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="No">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="StateName" ControlStyle-CssClass="align-content-md-start" HeaderText="State" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="finalNRV" ControlStyle-CssClass="align-content-md-start" HeaderText="Final NRV" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="RPL_Profit_Amt" ControlStyle-CssClass="align-content-md-start" HeaderText="RPL Profit Amt" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="Total_PD" ControlStyle-CssClass="align-content-md-start" HeaderText="Total RPL PD" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="Suggested_RPL_Price" ControlStyle-CssClass="align-content-md-start" HeaderText="Suggested RPL Price" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="TotalExpence" ControlStyle-CssClass="align-content-md-start" HeaderText="Total RPL Expence" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="RPLNetProfitAmount" ControlStyle-CssClass="align-content-md-start" HeaderText="Net Profit RPL Amt" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="RPLNetProfitPer" ControlStyle-CssClass="align-content-md-start" HeaderText="Net Profit RPL (%)" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="Diff_RPL_NCR" ControlStyle-CssClass="align-content-md-start" HeaderText="RPL to NCR Difference" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="RPLtoNCRDifferenceAmt" ControlStyle-CssClass="align-content-md-start" HeaderText="RPL to NCR Difference (Rs)" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="NRCTotal_PD" ControlStyle-CssClass="align-content-md-start" HeaderText="Total NCR PD" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="NRCFinalPrice" ControlStyle-CssClass="align-content-md-start" HeaderText="Suggested NCR Price" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="NCR_Total" ControlStyle-CssClass="align-content-md-start" HeaderText="Total NCR Expence" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="NCR_NetProfitRs" ControlStyle-CssClass="align-content-md-start" HeaderText="NCR NetProfit (Rs)" SortExpression="MainCategory_Name" />
                                                                <asp:BoundField DataField="NCR_NetProfitPer" ControlStyle-CssClass="align-content-md-start" HeaderText="NCR Net Profit (%)" SortExpression="MainCategory_Name" />

                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%---------------------------------%>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </main>

        </div>
    </div>
</asp:Content>
