<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report_TransportingCosting.aspx.cs" Inherits="Production_Costing_Software.Report_TransportingCosting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>

<script type="text/javascript">

    var GridId = "<%=Grid_TransportReportList.ClientID %>";
    var ScrollHeight = 500;
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
            //if (headerCellWidths[i] >= gridRow.getElementsByTagName("TD")[i].offsetWidth) {
            width = headerCellWidths[i];
            //}
            //else {
            //    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
            //    }
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




<body>
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
            background-color: grey;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .centerLoading {
            z-index: 1031;
            margin: 290px auto;
            padding: 10px;
            width: 300px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .centerLoading img {
                height: 128px;
                width: 300px;
            }
    </style>
    <link href="Content/bootstrap-5.1.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/GridViewStyleCompany.css" rel="stylesheet" />
    <form id="form1" runat="server">
        <div class="card mb-4">

            <div class="card-header">

                <h6>Transportation Costing Report:   [<asp:Label ID="lblStateName" runat="server" Text="" Visible="true"></asp:Label>]</h6>

            </div>
            <asp:Label ID="lblTransportationCostFactors_Id" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblCompany_Id" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblStateID" runat="server" Text="" Visible="false"></asp:Label>
            <div class="card-body">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="modalLoading">
                            <div class="centerLoading">
                                <img alt="" src="Content/Images/EmenLoading.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>
                        <div class="row mb-3">
                            <div class="col-md-4">
                                <div class="form-floating">
                                    <asp:DropDownList ID="MasterAllOrMasterDropdown" OnSelectedIndexChanged="MasterAllOrMasterDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                        <asp:ListItem Selected="True" Value="1">MasterPack</asp:ListItem>
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="card-body" style="width: 100%; height: 600px; overflow: scroll">
                        <asp:GridView ID="Grid_TransportReportList" Autopostback="true" CssClass=" table-hover table-responsive gridview"
                                GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); overflow: scroll"
                            PagerStyle-CssClass="gridview_pager"
                            AlternatingRowStyle-CssClass="gridview_alter"
                            runat="server" AutoGenerateColumns="False" ShowFooter="True">
                            <%--  DataKeyNames="TransportationCostFactors_Id">--%>
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product Name" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="PackingUnitSize" HeaderText="Packing Size" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="Pack_ShipperSize" HeaderText="Box Size Liter or KG" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="Approx1CartonCharge" HeaderText="Factory To Godown Transport Cost" SortExpression="Ingrdient_Name" />

                                <asp:BoundField DataField="LocalCartageAmount" HeaderText="Godown to Transport Cost" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="UnloadingAmount" HeaderText="Unloading Charges" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="AverageLocalTransportAmount" HeaderText="Local Cartage To Party Cost" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Ingrdient_Name" />
                                <asp:BoundField DataField="CostPerLtrKG" HeaderText="Cost/Liter or KG" SortExpression="Ingrdient_Name" />
                                <%--         <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="DellocalCartageBtn" OnClick="DellocalCartageBtn_Click1" ValidationGroup="Del" Text="Delete" Style="width: 80px" runat="server" class="btn btn-danger btn-sm" />
                                <asp:Button ID="EditLocalCartageBtn" OnClick="EditLocalCartageBtn_Click1" ValidationGroup="Edit" Text="Edit" Style="width: 80px" runat="server" class="btn btn-success btn-sm" />
                            </ItemTemplate>

                        </asp:TemplateField>--%>
                            </Columns>

                        </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </div>
    </form>
</body>
</html>
