<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PackingStyleMaster.aspx.cs" Inherits="Production_Costing_Software.WebForm3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--     <script type="text/javascript">
            //Initialize tooltip with jQuery
            $(document).ready(function () {
                $('.tooltips').tooltip();
            });
        </script>--%>
    <script type="text/javascript">

        var GridId = "<%=Grid_PackingStyleMaster.ClientID %>";
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
    <div id="layoutSidenav">

        <div id="layoutSidenav_content">
            <main>
                <%--Lables--%>
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
                </div>
                <%-----------------------%>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-fluid px-4">
                            <h1 class="mt-4" style="text-shadow: 2px 8px 6px rgba(0,0,0,0.2), 0px -5px 35px rgba(255,255,255,0.3);">Packing Style Labour & Power Costing Master</h1>
                            <ol class="breadcrumb mb-4">
                                <%--<li class="breadcrumb-item"><a href="MainCategory.aspx">MainCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RMCategory</a></li>
                            <li class="breadcrumb-item"><a href="RMCategory.aspx">RM Master</a></li>
                            <li class="breadcrumb-item"><a href="RMPriceMaster.aspx">RM Price Master</a></li>--%>
                            </ol>
                            <asp:Label ID="lblpackMeasurement" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblPackSize" runat="server" Text="" Visible="false"></asp:Label>
                            <div class="card mb-4">
                                <div class="card-header">
                                    <i class="fas fa-table me-1"></i>
                                    Packing Style Labour & Power Costing Master
                           
                                </div>
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-md-5">
                                            <div class="input-group ">
                                                <span class="input-group-text" id="PMRMCategoryspan">Packing Category & Size</span>

                                                <asp:DropDownList ID="PackingStyleCategoryDropdown" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <span class="input-group-text" id="PackingNameid">Packing Style Name</span>
                                                <%--<asp:TextBox ID="PackingStyleNametxt" class="form-control" TextMode="Number" AutoPostBack="true" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="PackingStyleNameDropdown" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <span class="input-group-text" id="ShipperSizeid">Packing Size</span>
                                                <asp:TextBox ID="PackingSizetxt" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <div class="col-md-4">
                                            <div class="input-group  ">
                                                <span class="input-group-text" id="UnitDropdown">Unit Measurement</span>

                                                <%--<asp:TextBox ID="UnitMeaurementtxt"  class="form-control" type="text" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="UnitMeaurementDropdown" OnSelectedIndexChanged="UnitMeaurementDropdown_SelectedIndexChanged" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-text" id="Transportation">Unit/Shipper</span>
                                            <asp:TextBox ID="UnitShippertxt" AutoPostBack="true" class="form-control" type="text" runat="server"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                        <div class="col-md-4">
                                            <%--    <div class="input-group">
                                            <span class="input-group-text" id="UnitsKG">No. of Units/KG</span>
                                            <asp:TextBox ID="Units_KGtxt" class="form-control" ReadOnly="true" type="text" runat="server"></asp:TextBox>
                                        </div>--%>
                                        </div>
                                    </div>

                                </div>

                                <%--**************Add Labour*************--%>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Add Labour /Task
                           
                                    </div>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BulkChargeSpanId">Bulk Charge</span>
                                                    <asp:TextBox ID="BulkChargetxt" class="form-control" Text="0" AutoPostBack="true" OnTextChanged="BulkChargetxt_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PouchFillingSpanId">Pouch Filling</span>
                                                    <asp:TextBox ID="PouchFillingtxt" class="form-control" Text="0" AutoPostBack="true" OnTextChanged="PouchFillingtxt_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BottleKeepingSpanId">Bottle Keeping</span>
                                                    <asp:TextBox ID="BottleKeepingtxt" class="form-control" Text="0" AutoPostBack="true" OnTextChanged="BottleKeepingtxt_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Lifting_Pouch_Bottle_WtSpanId">Lifting/ Pouch/ Bottle Wt.</span>
                                                    <asp:TextBox ID="Lifting_Pouch_Bottle_Wttxt" Text="0" AutoPostBack="true" OnTextChanged="Lifting_Pouch_Bottle_Wttxt_TextChanged" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">

                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BlacklinnerPouchSpanId">Black linner in pouch</span>
                                                    <asp:TextBox ID="BlacklinnerPouchtxt" class="form-control" AutoPostBack="true" OnTextChanged="BlacklinnerPouchtxt_TextChanged" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="InnerPlugSpanId">Inner Plug</span>
                                                    <asp:TextBox ID="InnerPlugtxt" class="form-control" AutoPostBack="true" OnTextChanged="InnerPlugtxt_TextChanged" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="MesuringCapSpanId">Mesuring Cap</span>
                                                    <asp:TextBox ID="MesuringCaptxt" class="form-control" AutoPostBack="true" OnTextChanged="MesuringCaptxt_TextChanged" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="CapingSpanId">Caping</span>
                                                    <asp:TextBox ID="Capingtxt" class="form-control" AutoPostBack="true" Text="0" OnTextChanged="Capingtxt_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">

                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="TearDownSealSpanId">Tear Down Seal</span>
                                                    <asp:TextBox ID="TearDownSealtxt" class="form-control" AutoPostBack="true" OnTextChanged="TearDownSealtxt_TextChanged" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="InductionSpanId">Induction</span>
                                                    <asp:TextBox ID="Inductiontxt" class="form-control" AutoPostBack="true" OnTextChanged="Inductiontxt_TextChanged" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PouchSealingSpanId">Pouch Sealing</span>
                                                    <asp:TextBox ID="PouchSealingtxt" class="form-control" AutoPostBack="true" OnTextChanged="PouchSealingtxt_TextChanged" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BottlePouchCleaningSpanId">Bottle/ Pouch Cleaning</span>
                                                    <asp:TextBox ID="BottlePouchCleaningtxt" AutoPostBack="true" OnTextChanged="BottlePouchCleaningtxt_TextChanged" Text="0" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="LabelingSpanId">Labeling</span>
                                                    <asp:TextBox ID="Labelingtxt" class="form-control" AutoPostBack="true" OnTextChanged="Labelingtxt_TextChanged" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="SleeveSpanId">Sleeve</span>
                                                    <asp:TextBox ID="Sleevetxt" class="form-control" AutoPostBack="true" OnTextChanged="Sleevetxt_TextChanged" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="InnerboxSpanId">Inner box</span>
                                                    <asp:TextBox ID="Innerboxtxt" class="form-control" AutoPostBack="true" OnTextChanged="Innerboxtxt_TextChanged" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="SSTin_Drum_Bucket_BagSpanId">SS Tin, Drum, Bucket, Bag</span>
                                                    <asp:TextBox ID="SSTin_Drum_Bucket_Bagtxt" AutoPostBack="true" OnTextChanged="SSTin_Drum_Bucket_Bagtxt_TextChanged" class="form-control" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">

                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="InnerBoxCelloTapeSpanId">Inner Box Cello Tape</span>
                                                    <asp:TextBox ID="InnerBoxCelloTapetxt" AutoPostBack="true" OnTextChanged="InnerBoxCelloTapetxt_TextChanged" class="form-control" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="kitchenTraySpanId">kitchen tray</span>
                                                    <asp:TextBox ID="kitchenTraytxt" AutoPostBack="true" OnTextChanged="kitchenTraytxt_TextChanged" class="form-control" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Additional_OtherSpanId">Additional (Other)</span>
                                                    <asp:TextBox ID="Additional_Other" AutoPostBack="true" OnTextChanged="Additional_Other_TextChanged" class="form-control" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="2">Stapping/ Wt.</span>
                                                    <asp:TextBox ID="StappingWttxt" AutoPostBack="true" OnTextChanged="StappingWttxt_TextChanged" class="form-control" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="OuterLabel_BOPPFilling_BoxFillingSpanId">Outer Label/ BOPP Filling/ Box Filling</span>
                                                    <asp:TextBox ID="OuterLabel_BOPPFilling_BoxFillingtxt" AutoPostBack="true" OnTextChanged="OuterLabel_BOPPFilling_BoxFillingtxt_TextChanged" class="form-control" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="input-group border border-success border-3">
                                                    <span class="input-group-text" id="TotalLaboutPerTaskspanId">Total Labour / Task</span>
                                                    <asp:TextBox ID="TotalLaboutPerTasktxt" ReadOnly="true" class="form-control" Text="0" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <%--*****************************************--%>

                                <%--**************Add Labour*************--%>
                                <div class="card-body mb-4">
                                    <div class="card-header">
                                        <i class="fas fa-table me-1"></i>
                                        Power Details / Hour
                                    </div>
                                    <%--**********************************************--%>
                                    <%--*******************Power Details**********************--%>
                                    <div class="card-body">
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="FILLINGSpanId">Filling</span>
                                                    <asp:TextBox ID="FillingPowertxt" OnTextChanged="FillingPowertxt_TextChanged" AutoPostBack="true" class="form-control" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="CappingSpanId">Capping</span>
                                                    <asp:TextBox ID="CappingPowertxt" class="form-control" AutoPostBack="true" OnTextChanged="CappingPowertxt_TextChanged" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="InductionPowerSpanId">Induction</span>
                                                    <asp:TextBox ID="InductionPowertxt" class="form-control" AutoPostBack="true" OnTextChanged="InductionPowertxt_TextChanged" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="LabelingPowerSpanId">Labeling</span>
                                                    <asp:TextBox ID="LabelingPowertxt" class="form-control" AutoPostBack="true" OnTextChanged="LabelingPowertxt_TextChanged" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-3">

                                                <div class="input-group">
                                                    <span class="input-group-text" id="ShrinkingPowerSpanId">Shrinking</span>
                                                    <asp:TextBox ID="ShrinkingPowertxt" class="form-control" OnTextChanged="ShrinkingPowertxt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="BoppPowerSpanId">BOPP</span>
                                                    <asp:TextBox ID="BoppPowertxt" class="form-control" OnTextChanged="BoppPowertxt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="SteppingPowerSpanId">Stepping</span>
                                                    <asp:TextBox ID="SteppingPowertxt" class="form-control" OnTextChanged="SteppingPowertxt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="SealingMCPowerSpanId">Sealing M/C</span>
                                                    <asp:TextBox ID="SealingMCPowertxt" class="form-control" OnTextChanged="SealingMCPowertxt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PowerDetail9SpanId">PowerDetail9 /Hour</span>
                                                    <asp:TextBox ID="PowerDetail9txt" class="form-control" OnTextChanged="PowerDetail9txt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PowerDetail10SpanId">PowerDetail10 /Hour</span>
                                                    <asp:TextBox ID="PowerDetail10txt" class="form-control" OnTextChanged="PowerDetail10txt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="PowerUnitPerhourSpanId">Power Unit/hour</span>
                                                    <asp:TextBox ID="PowerUnitPerhourtxt" class="form-control" OnTextChanged="PowerUnitPerhourtxt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-text" id="Otherpowerchragesspanid">Other Power</span>
                                                    <asp:TextBox ID="OtherPowerChargestxt" class="form-control" OnTextChanged="OtherPowerChargestxt_TextChanged" AutoPostBack="true" TextMode="Number" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row mb-3">
                                            <div class="col-md-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                <div class="input-group  border border-success border-3">
                                                    <span class="input-group-text" id="Transportation">Total Power</span>
                                                    <asp:TextBox ID="TotalPowertxt" ReadOnly="true" AutoPostBack="true" Text="0" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row mb-3">

                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-4" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                        <div class="input-group  border border-success border-3 ">
                                            <span class="input-group-text" id="TotalLabour_Power">Total Labour & Power Charge</span>
                                            <asp:TextBox ID="TotalLabour_Powertxt" ReadOnly="true" AutoPostBack="true" Text="0" class="form-control" type="text" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--*************************************************--%>
                                <div class="row  justify-content-center align-items-center mt-4">

                                     <div class=" col-6 col-lg-2 col-lg-2">
                                        <div class="d-grid">
                                            <asp:Button ID="Updatebtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />
                                            <asp:Button ID="Addbtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="Addbtn_Click" runat="server" Text="Submit" class="btn btn-primary btn-block" />
                                        </div>
                                    </div>


                                     <div class=" col-6 col-lg-2 col-lg-2">
                                        <div class="d-grid">
                                            <%--<asp:Button ID="Button1" OnClick="Updatebtn_Click" Visible="false" runat="server" Text="Update" class="btn btn-info btn-block" />--%>
                                            <asp:Button ID="CancelBtn" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="CancelBtn_Click" runat="server" Text="Cancel" class="btn btn-secondary btn-block" />
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lblPAckingStyleMaster_Id" runat="server" Text="" Visible="false"></asp:Label>
                                <hr />

                                <div class="card-body mb-4 overflow-auto">
                                    <div class="input-group">
                                        <asp:TextBox ID="TxtSearch" OnTextChanged="TxtSearch_TextChanged" placeholder="Search..." AutoPostBack="true" runat="server" CssClass="col-md-4" />

                                        <asp:Button ID="CancelSearch" runat="server" Text="X" CssClass="btn btn-outline-dark" OnClick="CancelSearch_Click" CausesValidation="false" />
                                        <asp:Button ID="SearchId" runat="server" Text="Search" OnClick="SearchId_Click" CssClass="btn btn-danger" CausesValidation="false" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" ServiceMethod="SearchBPMData" CompletionInterval="10" EnableCaching="false" MinimumPrefixLength="3" TargetControlID="TxtSearch" FirstRowSelected="false" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                    <asp:GridView ID="Grid_PackingStyleMaster" OnRowDataBound="Grid_PackingStyleMaster_RowDataBound"
                                        CssClass=" table-hover table-responsive gridview"
                                        GridLines="None" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);margin-top:10px"
                                        PagerStyle-CssClass="gridview_pager"
                                        AlternatingRowStyle-CssClass="gridview_alter"
                                        DataKeyNames="Packing_Style_Master_Id" runat="server" Autopostback="true"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PackingStyleCategoryName">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%#Eval("PackingStyleCategoryName")+" ("+Eval ( "PackingSize") +"-"+ Eval("Measurement")+")" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PackingStyleCategoryName" Visible="false" HeaderText="Style Category  Name" SortExpression="PackingStyleCategoryName" />
                                            <asp:BoundField DataField="PackingStyleName" HeaderText="Packing Style Name" SortExpression="PackingStyleName" />
                                            <asp:BoundField DataField="Packing" HeaderText="Packing Size" SortExpression="PackingSize" />
                                            <asp:BoundField DataField="PackingStyleMaster_Measurement" Visible="false" HeaderText="PackingStyleMaster_Measurement" SortExpression="PackingSize" />
                                            <asp:TemplateField HeaderText="TotalLabourTask">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLabourTask" runat="server" Text='<%#Eval("PackingTotalLabourTask") %>' CssClass="ToolTip" data-placement="right"
                                                        data-html="true" title='<%# string.Format("Task_Bulk_Charge:{0}" +
                                                                                                                            "\r Task_Pouch_Filling:--{1}"+
                                                                                                                            "\r Task_Bottle_Keeping:--{2}"+
                                                                                                                       "\r Task_Lifting_Pouch_Bottle_Wt:--{3}"+
                                                                                                                           "\r Task_Black_linner_pouch:--{4}" + 
                                                                                                                           "\r Task_Inner_Plug:--{5}"+
                                                                                                                        "\r Task_Mesuring_Cap:---{6}" +
                                                                                                                        "\r Task_Caping:----{7}"+
                                                                                                                       "\r Task_Tear_Down_Seal:----{8}" +
                                                                                                                       "\r Task_Induction:----- {9}"+
                                                                                                                         "\r Task_Pouch_Sealing:---{10}" +
                                                                                                                         "\r Task_Bottle_Pouch_Cleaning:----- {11}"+
                                                                                                                         "\r Task_Labeling:----{12}" +
                                                                                                                         "\r Task_Sleeve:-----{13}"+
                                                                                                                          "\r Task_Inner_box:--------{14}" + 
                                                                                                                          "\r Task_SS_Tin_Drum_Bucket_Bag:----{15}"+
                                                                                                                            "\r Task_Inner_Box_Cello_Tape:--- {16}" + 
                                                                                                                            "\r Task_kitchen_Tray:-------{17}"+
                                                                                                                             "\r Task_OuterLabel_BOPP_Filling_BoxFilling:-----{18}" + 
                                                                                                                             "\r Task_Stapping_Wt:------- {19}"+
                                                                                                                               "\r Task_Additional_Other:-----{20}",

    
    
                                                                                                                Eval("Task_Bulk_Charge"),Eval("Task_Pouch_Filling"),
                                                                                                                Eval("Task_Bottle_Keeping"),Eval("Task_Lifting_Pouch_Bottle_Wt"),
                                                                                                                  Eval("Task_Black_linner_pouch"),Eval("Task_Inner_Plug"),
                                                                                                                    Eval("Task_Mesuring_Cap"),Eval("Task_Caping"),
                                                                                                                      Eval("Task_Tear_Down_Seal"),Eval("Task_Induction"),
                                                                                                                       Eval("Task_Pouch_Sealing"),Eval("Task_Bottle_Pouch_Cleaning"),
                                                                                                                        Eval("Task_Labeling"),Eval("Task_Sleeve"),
                                                                                                                         Eval("Task_Inner_box"),Eval("Task_SS_Tin_Drum_Bucket_Bag"),
                                                                                                                          Eval("Task_Inner_Box_Cello_Tape"),Eval("Task_kitchen_Tray"),
                                                                                                                          Eval("Task_OuterLabel_BOPP_Filling_BoxFilling"),Eval("Task_Stapping_Wt"),
                                                                                                                            Eval("Task_Additional_Other")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PackingTotalPowerDetails">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPowerTask" runat="server" Text='<%#Eval("PackingTotalPowerDetails") %>' CssClass="tooltips" data-placement="right"
                                                        data-html="true" title='<%# string.Format("Power_Filling: ----------------------------  {0}" +
                                                                                                                            "\rPower_Capping: ------------------------------   {1}"+
                                                                                                                                  "\rPower_Induction:--------------------------  {2}" +
                                                                                                                                  "\r Power_Lableling:-------------------------------- {3}"+
                                                                                                                                   "\rPower_BOPP: ----------------------------------  {4}" + 
                                                                                                                                   "\r Power_Shrinking:---------------------------- {5}"+
                                                                                                                                     "\rPower_Stepping: -------------------------------{6}" +
                                                                                                                                     "\r Power_StealingMC:---------------------------- {7}"+
                                                                                                                                        "\rPower_Detail9: --------------------------------{8}" +
                                                                                                                                        "\r Power_Detail10:------------------------------- {9}"+
                                                                                                                                            "\r PowerUnitPerHour:------------------------------- {10}"+
                                                                                                                                            "\r Power_Other:------------------------------- {11}",
    
    
    
                                                                                                                             Eval("Power_Filling"), Eval("Power_Capping"),
                                                                                                                             Eval("Power_Induction"), Eval("Power_Lableling"),
                                                                                                                                Eval("Power_BOPP"), Eval("Power_Shrinking"),
                                                                                                                                 Eval("Power_Stepping"), Eval("Power_StealingMC"),
                                                                                                                                  Eval("Power_Detail9"), Eval("Power_Detail10"),
                                                                                                                                  Eval("PowerUnitPerHour"), Eval("Power_Other") ) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Task_Bulk_Charge" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Pouch_Filling" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Bottle_Keeping" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Lifting_Pouch_Bottle_Wt" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Black_linner_pouch" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Inner_Plug" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Mesuring_Cap" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Caping" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Tear_Down_Seal" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Induction" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Pouch_Sealing" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Bottle_Pouch_Cleaning" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Labeling" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Sleeve" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Inner_box" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_SS_Tin_Drum_Bucket_Bag" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Inner_Box_Cello_Tape" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_kitchen_Tray" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_OuterLabel_BOPP_Filling_BoxFilling" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Stapping_Wt" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Additional_Other" Visible="false" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="Task_Stapping_Wt" Visible="false" HeaderText="Task_Stapping_Wt" SortExpression="Measurement" />


                                            <asp:BoundField DataField="Power_Filling" Visible="false" HeaderText="Power_Filling" SortExpression="Power_Filling" />
                                            <asp:BoundField DataField="Power_Capping" Visible="false" HeaderText="Power_Capping" SortExpression="Power_Capping" />
                                            <asp:BoundField DataField="Power_Induction" Visible="false" HeaderText="Power_Induction" SortExpression="Power_Induction" />
                                            <asp:BoundField DataField="Power_Lableling" Visible="false" HeaderText="Power_Lableling" SortExpression="Power_Lableling" />
                                            <asp:BoundField DataField="Power_Shrinking" Visible="false" HeaderText="Power_Shrinking" SortExpression="Power_Shrinking" />
                                            <asp:BoundField DataField="Power_BOPP" Visible="false" HeaderText="Power_BOPP" SortExpression="Power_BOPP" />
                                            <asp:BoundField DataField="Power_Stepping" Visible="false" HeaderText="Power_Stepping" SortExpression="Power_Stepping" />
                                            <asp:BoundField DataField="Power_StealingMC" Visible="false" HeaderText="Power_StealingMC" SortExpression="Power_StealingMC" />
                                            <asp:BoundField DataField="Power_Detail9" Visible="false" HeaderText="Power_Detail9" SortExpression="Power_Detail9" />
                                            <asp:BoundField DataField="Power_Detail10" Visible="false" HeaderText="Power_Detail10" SortExpression="Power_Detail10" />
                                            <asp:BoundField DataField="PowerUnitPerHour" Visible="false" HeaderText="PowerUnitPerHour" SortExpression="Power_Detail10" />
                                            <asp:BoundField DataField="Power_Other" Visible="false" HeaderText="Power_Other" SortExpression="Power_Detail10" />





                                            <asp:BoundField DataField="Measurement" HeaderText="Measurement" SortExpression="Measurement" />
                                            <asp:BoundField DataField="PackingTotalLabourTask" Visible="false" HeaderText="Total Labour Task" SortExpression="PackingTotalLabourTask" />
                                            <asp:BoundField DataField="PackingTotalPowerDetails" Visible="false" HeaderText="TotalPowerDetails" SortExpression="PackingTotalPowerDetails" />

                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Button ID="DelPackingStyleMasterBtn" Visible='<%# lblCanDelete.Text=="True"?true:false %>' OnClientClick="return confirm('Are you sure you want to delete this item?');" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClick="DelPackingStyleMasterBtn_Click" Text="Delete" runat="server" class="btn btn-danger btn-sm" />
                                                    <%--  <asp:Button ID="ViewLabourTask" Text="View Labour" runat="server" class="btn btn-info btn-sm" />
                                                    
                                                    <asp:Button ID="ViewPowerCharges" Text="View Power" runat="server" class="btn btn-primary btn-sm" />--%>
                                                    <asp:Button ID="EditPackingStyleMasterBtn" Visible='<%# lblCanEdit.Text=="True"?true:false %>' Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4); width: 80px" OnClick="EditPackingStyleMasterBtn_Click" Text="Edit" runat="server" class="btn btn-success btn-sm" />
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
