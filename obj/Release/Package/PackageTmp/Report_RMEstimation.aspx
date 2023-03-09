<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report_RMEstimation.aspx.cs" Inherits="Production_Costing_Software.Report_RMEstimation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Content/Js/jquery.min.js"></script>



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
            z-index: 9999;
            /*margin: 290px auto;*/
            /*padding: 10px;*/
            width: 300px;
            opacity: 1;
        }

            .centerLoading img {
                height: 150px;
                width: 250px;
            }
    </style>
    <%--    <script type="text/javascript">
        function ClearHtml() {
            document.getElementById("ContentPlaceHolder1_UpdatePanel1").innerHTML = "";
        }
    </script>--%>
    <script type="text/javascript">
        function DynamicClick(CompanyId, Estimate,Status) {
            document.getElementById('<%=hfDynamicCompId.ClientID %>').value = CompanyId;
            var strURL = "";
            strURL = document.URL.substring(0, document.URL.length - document.URL.indexOf("Report_RMEstimation.aspx"));
            if (CompanyId == "1") {
                strURL += "/PriceList_GP.aspx?CmpId=1&EstimateName=" + Estimate;
            }
            else {
                strURL += "/EstimatePriceList.aspx?CmpId=" + CompanyId + "&EstimateName=" + Estimate + "&Status=" + Status;
            }
            window.location.href = strURL;           
            return false;
        }
        function ShowPopup1() {
            $("#staticBackdrop").modal("show");

        }
    </script>
    <script type="text/javascript">
        function ShowRMEstimateReport() {
            $("#RMEstimateReport").modal("show");

        }
    </script>
    <script type="text/javascript">
        function HideRMEstimateReport() {
            $("#RMEstimateReport").modal("hide");

        }
    </script>

    <script type="text/javascript">
        function ShowRMReport() {
            $("#RM_ReportModal").modal("show");

        }
    </script>
    <script type="text/javascript">
        function HideRMReport() {
            $("#RM_ReportModal").modal("hide");
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
                <asp:Label ID="lblRMEstimateTableOne_Id" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblStatus" Visible="false" runat="server" Text=""></asp:Label>
                <asp:Label ID="UserIdAdmin" Visible="false" runat="server" Text=""></asp:Label>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img src="Content/LogoImage/gifntext-gif.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal fade" id="RM_ReportModal" tabindex="-1">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content col-lg-12">
                                    <div class="modal-header" style="background-color: #343a40">
                                        <h4 style="color: whitesmoke">Rm Estimate Report:
                                        </h4>
                                        <%--<h6 style="color: whitesmoke">[<asp:Label ID="HeaderForRMEstimate" runat="server" Style="font-family: monospace"></asp:Label>]</h6>--%>
                                        <asp:Label ID="lblname" runat="server" CssClass="font-monospace" Style="color: white" Visible="false"></asp:Label>

                                        <asp:Label ID="lblPackingSize" runat="server" CssClass="font-monospace" Style="color: white" Visible="false"></asp:Label>

                                        <%--<button aria-label="Close" style="color: white" class="btn-close btn-close-white" data-bs-dismiss="modal" type="button">
                                        </button>--%>
                                        <asp:Button ID="ReportCloseRMReport" runat="server" Text="X" OnClick="ReportCloseRMReport_Click" CausesValidation="false" />
                                    </div>
                                    <asp:Label ID="lblBPM_Id" runat="server" Text="" Visible="false"></asp:Label>
                                    <div class="modal-body overflow-scroll">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>

                                                <asp:Table runat="server" CssClass=" table-hover table-responsive gridview" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
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
                        <asp:HiddenField ID="hfDynamicCompId" runat="server" Value="0" />
                        <%--RMEstimateGRid Modal--%>

                        <asp:Button ID="btnDynamic" runat="server" OnClick="btnDynamic_Click" Style="display: none;" />


                        <div class="modal fade" id="RMEstimateReport" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel2" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <div style="color: black">

                                            <h5>Estimated Cost Bulk Report:</h5>
                                            <h6>[<asp:Label ID="EstimateHeader" runat="server" CssClass="font-monospace"></asp:Label>]
                                            </h6>

                                            <%--BulkCount For Company--%>
                                            <asp:Label ID="lblCompanyName" runat="server" Visible="false" CssClass="font-monospace"></asp:Label>
                      
            
                                        </div>

                                        <asp:Panel ID="Panel1" runat="server" ViewStateMode="Enabled" EnableViewState="true">
                                        </asp:Panel>
                                        <asp:Button ID="CloseRMEstimateModel" runat="server" Text="X" OnClick="CloseRMEstimateModel_Click" CausesValidation="false" />

                                        <%--<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>--%>
                                    </div>
                                    <div class="modal-body">
                                        <div class="card-body" style="overflow: scroll">
                                            <asp:GridView ID="Grid_RM_EstimateReport" CssClass=" table-hover table-responsive gridview"
                                                GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                                PagerStyle-CssClass="gridview_pager" DataKeyNames="Fk_BOM_Product_Id" AutoGenerateColumns="false"
                                                AlternatingRowStyle-CssClass="gridview_alter" runat="server">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IngredientName" ControlStyle-CssClass="align-content-md-start" HeaderText="Bulk Name" SortExpression="MainCategory_Name" />
                                                    <%--<asp:BoundField DataField="PackSize" ControlStyle-CssClass="align-content-md-start" HeaderText="Master Pack" SortExpression="MainCategory_Name" />--%>
                                                    <asp:BoundField DataField="OLDFinalCostBulk" ControlStyle-CssClass="align-content-md-start" HeaderText="Actual Cost Bulk/Ltr" SortExpression="MainCategory_Name" />
                                                    <asp:BoundField DataField="NewFinalCostBulk" ControlStyle-CssClass="align-content-md-start" HeaderText="Modified Bulk Cost/Ltr" SortExpression="MainCategory_Name" />

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="RMEstReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-dismiss="modal" data-bs-toggle="modal" data-bs-target="#RM_ReportModal" OnClick="RMEstReport_Click" CausesValidation="false" runat="server" class="btn btn-success btn-sm" Text="View Report" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>

                        <%--------------------%>


                        <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="staticBackdropLabel">RM Estimate:</h5>
                                        <%--<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>--%>
                                        <asp:Button ID="staticBackdropCloseBtn" runat="server" Text="X" OnClick="staticBackdropCloseBtn_Click" CausesValidation="false" />
                                    </div>
                                    <div class="modal-body">
                                        <div class="row mb-3">
                                            <div class="col-md-6 ">
                                                <div class="input-group-text">
                                                    <label for="Category">Date of Estimate:</label>
                                                    <asp:TextBox ID="DOPtxt" type="Date" TabIndex="3" ClientIDMode="Static" CssClass="form-control" placeholder="Please specify a date"
                                                        onClick="$(this).removeClass('placeholderclass')"
                                                        runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="offset-1" ControlToValidate="DOPtxt" runat="server" ForeColor="Red" ErrorMessage="*Date "></asp:RequiredFieldValidator>

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
                                            <div class="col-md-7" style="border: 1px;">
                                                <div class="input-group" style="height: 300px">
                                                    <label for="EstimateNametxt">Select  Ingredient:</label>

                                                    <asp:ListBox ID="RM_IngredientListbox" runat="server" SelectionMode="Multiple" class="form-control"></asp:ListBox>

                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <asp:Button ID="ChkRMSubmit" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ChkRMSubmit_Click" runat="server" CssClass="btn btn-success" Text="+ Add" />
                                            </div>

                                            <div class="col-md-1">
                                                <asp:Button ID="ChkBulkClear" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" CssClass="btn btn-danger" Text="ClearAll" />
                                            </div>
                                        </div>

                                    </div>
                                    <hr />
                                    <div class="card-body">
                                        <asp:GridView ID="Grid_RMEstimateTableOne" CssClass=" table-hover table-responsive gridview"
                                            GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)"
                                            PagerStyle-CssClass="gridview_pager" DataKeyNames="RM_EstimateTabOne_Id" AutoGenerateColumns="false"
                                            AlternatingRowStyle-CssClass="gridview_alter" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstimate_RMPrice_Id" runat="server" Text='<%#  Eval("Estimate_RMPrice_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRM_EstimateTabOne_Id" runat="server" Text='<%#  Eval("RM_EstimateTabOne_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="RMWithPurity" ControlStyle-CssClass="align-content-md-start" HeaderText="RM" SortExpression="MainCategory_Name" />--%>

                                                <asp:TemplateField HeaderText="RM">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lblRMWithPurity" runat="server" Text='<%#  Eval("RMWithPurity") %>' HeaderText="RM" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="RM_Price_TotalRate" ControlStyle-CssClass="align-content-md-start" HeaderText="Actual Price" SortExpression="MainCategory_Name" />--%>

                                                <asp:TemplateField HeaderText="Actual Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRM_Price_ActualPrice" runat="server" Text='<%#  Eval("RM_Price_ActualPrice") %>' HeaderText="Actual Price" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Rate" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEsitmateName" runat="server" Text='<%#  Eval("EstimateName") %>' HeaderText="EsitmateName" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estimate Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstimateDate" runat="server" Text='<%#  Eval("EstimateDate") %>' HeaderText="EstimateDate" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modified Price">

                                                    <ItemTemplate>

                                                        <asp:TextBox ID="ModifiedPricetxt" runat="server" Text='<%#  Eval("RM_ModifiedPrice") %>'></asp:TextBox>

                                                        <asp:Button ID="DelGridRmModifiedBtn" OnClick="DelGridRmModifiedBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" runat="server" CssClass="btn btn-danger btn-sm" Text="X" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-footer">

                                        <asp:Button ID="ModifiedPriceUpdateBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ModifiedPriceUpdateBtn_Click" runat="server" CausesValidation="false" class="btn btn-success" data-bs-dismiss="modal" Text="Update" />
                                        <asp:Button ID="ViewRMEstimateReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="ViewRMEstimateReport_Click" CausesValidation="false" runat="server" class="btn btn-info" data-bs-dismiss="modal" Text="Report" />
                                        <asp:Button ID="AddModifiedPriceBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="AddModifiedPriceBtn_Click" CausesValidation="false" runat="server" class="btn btn-primary" data-bs-dismiss="modal" Text="Submit" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <%--RMEstimateReport--%>


                        <%--------------------------------------%>

                        <div class="container-fluid px-4">
                            <h2 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">RM Price Estimation:</h2>

                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="Report_RMEstimation.aspx">RM Price Estimation</a></li>
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
                                        <div>
                                            <marquee direction="left" style="color: red; font-family: monospace">
                                                Please Note That Each Estimate You Are Creating Should be Different from Other Estimate Name!
                                            </marquee>
                                        </div>

                                        <div>
                                            <asp:Button ID="AddRMEstimationBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-primary" OnClick="AddRMEstimationBtn_Click" CausesValidation="false" runat="server" Text="+ Add" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class=" mt-4">
                                    <asp:GridView ID="Gird_RM_PriceEstimationList" CssClass=" table-hover table-responsive gridview"
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
                                                    <asp:Button ID="GridDelete" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="GridDelete_Click" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" CausesValidation="false" runat="server" class="btn btn-danger btn-sm" Text="Delete" />
                                                    <asp:Button ID="EstimateRateModal" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="EstimateRateModal_Click" Visible='<%# lblCanEdit.Text=="True"?true:false %>' CausesValidation="false" runat="server" class="btn btn-info btn-sm" Text="Report" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>



                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>


            </main>

        </div>
    </div>
</asp:Content>
