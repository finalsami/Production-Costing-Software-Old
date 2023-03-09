<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="PriceList.aspx.cs" Inherits="Production_Costing_Software.PriceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var GridId = "<%=Grid_PriceListDataGP.ClientID %>";
        var ScrollHeight = 400;
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
        function OnPrintReport(data) {
            document.getElementById('<%=hfData.ClientID %>').value = data;
            document.getElementById('<%=btnPrint.ClientID %>').click();
            return false;
        }

    </script>
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

    <asp:Button ID="btnPrint" runat="server" OnClick="PrintReport_Click" Style="display: none;" />
    <asp:HiddenField ID="hfData" runat="server" Value="" />
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
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
                    <asp:Label ID="lblCompany_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblEstimateName" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblPriceListName" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblState_Id" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblCanDelete" runat="server" Text="" Visible="false"></asp:Label>

                </div>
                <%----------------------------------%>
                <div class="container-fluid px-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <h2 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">PriceList
                        <asp:Label ID="CompanyHeadertxt" CssClass="font-monospace" Style="color: forestgreen" runat="server" Text=""></asp:Label></h2>
                            <ol class="breadcrumb mb-4">
                                <li class="breadcrumb-item"><a href="comp_CompanyPriceListMaster.aspx">PriceList
                                </a></li>
                            </ol>

                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    <asp:Label ID="lblCompanyMasterList_Name" runat="server" Text=""></asp:Label>
                                    <div class="col-sm-2 form-floating mb-3 float-end">
                                        <asp:DropDownList ID="CompanyDropdownList" TabIndex="2" OnSelectedIndexChanged="CompanyDropdownList_SelectedIndexChanged" AutoPostBack="True" CssClass="form-select col-sm-1" runat="server">
                                        </asp:DropDownList>
                                        <%--                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="RMDropdown" ControlToValidate="RMDropdownList" Display="Dynamic" InitialValue="Null" runat="server" ForeColor="Red" ErrorMessage="* Select RM"></asp:RequiredFieldValidator>--%>
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



                                <div class="card-body" style="overflow: auto">

                                    <asp:GridView ID="Grid_PriceListDataGP" CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager" Visible="false"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="MergePriceList_State">
                                        <Columns>

                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>

                                            <asp:BoundField DataField="PriceListName" HeaderText="Price List" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="Fk_State_Id" HeaderText="State_Id" Visible="false" SortExpression="MainCategory_Name" />

                                            <asp:BoundField DataField="AsOnDate" HeaderText="AsOnDate" SortExpression="Source" />
                                            <asp:BoundField DataField="StateName" HeaderText="State" SortExpression="Source" />
                                            <asp:BoundField DataField="CompanyMaster_Name" HeaderText="Company" SortExpression="Source" />


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="ViewReport" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="ViewReport" OnClick="ViewReport_Click" Text="View Report" runat="server" class="btn btn-success btn-sm" />--%>
                                                    <asp:Button ID="PrintReport" OnClientClick='<%#  "OnPrintReport(\""+Eval("MergePriceList_State").ToString()+"\");" %>' Text="Pdf Report" CausesValidation="false" runat="server" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-danger btn-sm" />

                                                </ItemTemplate>


                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
                                    <asp:GridView ID="Grid_PriceListDataOtherCompany" CssClass=" table-hover table-responsive gridview" Visible="false"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4);"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="Fk_Company_Id">
                                        <Columns>

                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>

                                            <asp:BoundField DataField="PriceListName" HeaderText="Price List" SortExpression="MainCategory_Name" />

                                            <asp:BoundField DataField="AsOnDate" HeaderText="AsOnDate" SortExpression="Source" />
                                            <asp:BoundField DataField="CompanyMaster_Name" HeaderText="Company" SortExpression="Source" />


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="ViewReport" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="ViewReport" OnClick="ViewReport_Click" Text="View Report" runat="server" class="btn btn-success btn-sm" />--%>
                                                    <%--<asp:Button ID="PrintReportOtherCompany" OnClick="PrintReportOtherCompany_Click" Text="Pdf Report" CausesValidation="false" runat="server" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-danger btn-sm" />--%>
                                                    <asp:Button ID="PrintReport" OnClientClick='<%#  "OnPrintReport(\""+Eval("MergePriceWithCompany").ToString()+"\");" %>' Text="Pdf Report" CausesValidation="false" runat="server" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-danger btn-sm" />

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                    <asp:GridView ID="PriceList_Report" CssClass=" table-hover table-responsive gridview"
                                        Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4); display: none"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="fk_BPM_Id">
                                        <Columns>

                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>

                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Price List" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="ProductCategoryName" HeaderText="State_Id" Visible="false" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="PackMeasure" HeaderText="PackMeasure" SortExpression="MainCategory_Name" />

                                            <asp:BoundField DataField="NCR" HeaderText="AsOnDate" SortExpression="Source" />
                                            <asp:BoundField DataField="RPL" HeaderText="State" SortExpression="Source" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="ViewReport" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="ViewReport" OnClick="ViewReport_Click" Text="View Report" runat="server" class="btn btn-success btn-sm" />--%>
                                                    <asp:Button ID="PrintReport" OnClick="PrintReport_Click" Text="Pdf Report" CausesValidation="false"  runat="server" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-danger btn-sm" />

                                                </ItemTemplate>


                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                    <asp:GridView ID="PriceList_Report_OtherCompany" CssClass=" table-hover table-responsive gridview"
                                        Style="border-radius: 5px; box-shadow: 5px 17px 10px -10px rgba(0, 0, 0, 0.4); display: none"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter" runat="server" AutoGenerateColumns="False" DataKeyNames="fk_BPM_Id">
                                        <Columns>

                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>

                                            <asp:BoundField DataField="BPM_Product_Name" HeaderText="Price List" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="ProductCategoryName" HeaderText="State_Id" Visible="false" SortExpression="MainCategory_Name" />
                                            <asp:BoundField DataField="PackMeasure" HeaderText="PackMeasure" SortExpression="MainCategory_Name" />

                                           
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="ViewReport" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" ValidationGroup="ViewReport" OnClick="ViewReport_Click" Text="View Report" runat="server" class="btn btn-success btn-sm" />--%>
                                                    <asp:Button ID="PrintReport" OnClick="PrintReport_Click" Text="Pdf Report" CausesValidation="false"  runat="server" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" class="btn btn-danger btn-sm" />

                                                </ItemTemplate>


                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
                                </div>

                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CompanyDropdownList" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </main>
        </div>

    </div>
</asp:Content>
