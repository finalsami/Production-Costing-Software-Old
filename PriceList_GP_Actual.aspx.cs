using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Production_Costing_Software
{
    public partial class PriceList_GP_Actual : System.Web.UI.Page
    {
        int User_Id;
        int rowID = 0;
        int Company_Id;
        int CheckCount = 0;
        string BPM_Id = "";
        string BPMName = "";
        string PMRM_Category_Id = "";
        string CameFrom = "GPActual";

        string[] ArrRemoveColumns = {"Select","Action","Status","PriceBase","CompanyMaster_Name","Last_Shared_Price","Date_Last_Shared_Price","Suggested_Price","Fk_BPM_Id","Packing_Size",
                "Fk_PM_RM_Category_Id","LatestStatus","TradeName_Id","Fk_UnitMeasurement_Id","Sort_Index","FkPriceTypeId","EnumDescription","Fk_CompanyList_Id","BR_BOM_Id","BPM_Product_Name","TradeName"};

        public int GridEditbtn_Click { get; private set; }

        //protected override void OnInit(EventArgs e)
        //{
        //    // code before base oninit
        //    base.OnInit(e);
        //    Grid_PriceListGP_ActualData();
        //    // code after base oninit
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            //GetUserRights();
            if (!IsPostBack)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();

                Grid_PriceListGP_ActualData();
                //DisplayView();

            }
            else
            {

                DataTable dtNewActual = ViewState["dtNewActual"] as DataTable;
                rowID = 0;
                Grid_PriceList_GP_Actual.DataSource = dtNewActual;
                Grid_PriceList_GP_Actual.DataBind();
            }
            Session["CameFrom"] = CameFrom;
            Response.Redirect("~/PriceList_GP.aspx");

        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                User_Id = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);
                Session["CameFrom"] = CameFrom;
                Response.Redirect("~/PriceList_GP.aspx");
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        public void Grid_PriceListGP_ActualData()
        {


            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            ClsPriceList_GP cls = new ClsPriceList_GP();



            dt = cls.GetPriceList_GP_Actual_Final("1");
            if (dt.Rows.Count > 0)
            {
                lblBPM_Id.Text = dt.Rows[0]["Fk_BPM_Id"].ToString();
                lblPMRM_Category_Id.Text = dt.Rows[0]["Fk_PM_RM_Category_Id"].ToString();
            }





            DataTable dtNewActual = new DataTable();
            dtNewActual.Clear();
            dtNewActual.Columns.Add("Fk_BPM_Id", typeof(int));
            dtNewActual.Columns.Add("BPM_Product_Name", typeof(string));
            dtNewActual.Columns.Add("PriceBase", typeof(string));

            dtNewActual.Columns.Add("FkPriceTypeId", typeof(int));
            dtNewActual.Columns.Add("EnumDescription", typeof(string));
            dtNewActual.Columns.Add("Date_Last_Shared_Price", typeof(string));
            dtNewActual.Columns.Add("Fk_PM_RM_Category_Id", typeof(int));
            //dtNewActual.Columns.Add("LatestStatus", typeof(string));

            dtNewActual.Columns.Add("TradeName_Id", typeof(int));

            dtNewActual.AcceptChanges();
            var distinctBPMValuesActual = dt.AsEnumerable().Select(row => new { BPM_Id = row.Field<Int32>("Fk_BPM_Id"), }).Distinct();

            foreach (var itemBPMActual in distinctBPMValuesActual)
            {
                foreach (DataColumn itemActual in dt.Columns)
                {
                    if (!Array.Exists(ArrRemoveColumns, element => element == itemActual.ColumnName))
                    {
                        var Data = dt.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPMActual.BPM_Id && row.Field<string>(itemActual.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(itemActual.ColumnName).ToString()));
                        DataRow drActual;
                        if (Data.Any())
                        {
                            DataTable objdtData = Data.CopyToDataTable();

                            if (!dtNewActual.Columns.Contains(itemActual.ColumnName))
                            {
                                dtNewActual.Columns.Add(itemActual.ColumnName, typeof(decimal));
                                dtNewActual.Columns.Add(itemActual.ColumnName + "New", typeof(string));
                                dtNewActual.AcceptChanges();
                            }

                            foreach (DataRow drDataActual in objdtData.Rows)
                            {
                                var DataNCROrRPL = dtNewActual.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPMActual.BPM_Id && row.Field<int>("FkPriceTypeId") == Convert.ToInt32(drDataActual["FkPriceTypeId"]) && row.Field<string>("PriceBase") == Convert.ToString(drDataActual["PriceBase"]));

                                if (DataNCROrRPL.Any())
                                {
                                    foreach (var row in DataNCROrRPL)
                                    {
                                        row.SetField(itemActual.ColumnName, Convert.ToDecimal(drDataActual[itemActual.ColumnName].ToString().Split('|')[0]));
                                        row.SetField(itemActual.ColumnName + "New", Convert.ToString(drDataActual[itemActual.ColumnName].ToString().Split('|')[1]));
                                    }
                                }
                                else
                                {
                                    drActual = dtNewActual.NewRow();
                                    drActual["Fk_BPM_Id"] = Convert.ToInt32(drDataActual["Fk_BPM_Id"]);
                                    drActual["BPM_Product_Name"] = Convert.ToString(drDataActual["BPM_Product_Name"]);
                                    drActual["PriceBase"] = Convert.ToString(drDataActual["PriceBase"]);

                                    drActual["FkPriceTypeId"] = Convert.ToInt32(drDataActual["FkPriceTypeId"]);
                                    drActual["EnumDescription"] = Convert.ToString(drDataActual["EnumDescription"]);
                                    drActual["Date_Last_Shared_Price"] = Convert.ToString(drDataActual["Date_Last_Shared_Price"]);
                                    drActual["Fk_PM_RM_Category_Id"] = Convert.ToString(drDataActual["Fk_PM_RM_Category_Id"]);
                                    //drActual["LatestStatus"] = Convert.ToString(drDataActual["LatestStatus"]);
                                    drActual["TradeName_Id"] = Convert.ToInt32(drDataActual["TradeName_Id"]);


                                    drActual[itemActual.ColumnName] = Convert.ToDecimal(drDataActual[itemActual.ColumnName].ToString().Split('|')[0]);
                                    drActual[itemActual.ColumnName + "New"] = Convert.ToDecimal(drDataActual[itemActual.ColumnName].ToString().Split('|')[1]);
                                    dtNewActual.Rows.Add(drActual);
                                }
                                dtNewActual.AcceptChanges();
                            }
                        }
                        else
                        {
                            var EmptyData = dt.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPMActual.BPM_Id);
                            if (EmptyData.Any())
                            {
                                DataTable objdtItem = EmptyData.CopyToDataTable();

                                foreach (DataRow drnonStateActual in objdtItem.Rows)
                                {
                                    var IsDataAvailable = dtNewActual.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPMActual.BPM_Id && row.Field<int>("FkPriceTypeId") == Convert.ToInt32(drnonStateActual["FkPriceTypeId"]) && row.Field<string>("PriceBase") == Convert.ToString(drnonStateActual["PriceBase"]));
                                    if (!IsDataAvailable.Any())
                                    {
                                        drActual = dtNewActual.NewRow();
                                        drActual["Fk_BPM_Id"] = Convert.ToInt32(itemBPMActual.BPM_Id);
                                        drActual["BPM_Product_Name"] = Convert.ToString(drnonStateActual["BPM_Product_Name"]);
                                        drActual["PriceBase"] = Convert.ToString(drnonStateActual["PriceBase"]);

                                        drActual["FkPriceTypeId"] = Convert.ToInt32(drnonStateActual["FkPriceTypeId"]);
                                        drActual["EnumDescription"] = Convert.ToString(drnonStateActual["EnumDescription"]);
                                        drActual["Date_Last_Shared_Price"] = Convert.ToString(drnonStateActual["Date_Last_Shared_Price"]);
                                        drActual["Fk_PM_RM_Category_Id"] = Convert.ToString(drnonStateActual["Fk_PM_RM_Category_Id"]);
                                        //drActual["LatestStatus"] = Convert.ToString(drnonStateActual["LatestStatus"]);
                                        drActual["TradeName_Id"] = Convert.ToInt32(drnonStateActual["TradeName_Id"]);

                                        dtNewActual.Rows.Add(drActual);

                                    }
                                }

                            }
                        }
                    }
                }
            }

            dtNewActual.Columns.Add("Select", typeof(string));
            dtNewActual.AcceptChanges();
            ViewState["dtNewActual"] = dtNewActual;
            rowID = 0;
            Grid_PriceList_GP_Actual.DataSource = dtNewActual;

            Grid_PriceList_GP_Actual.DataBind();
        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[36]["GroupId"]);
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[36]["CanDelete"]).ToString();
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[36]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[36]["CanEdit"]) == true)
            {

            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[36]["GroupId"]);
            lblCanView.Text = Convert.ToBoolean(dtMenuList.Rows[36]["CanView"]).ToString();
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[36]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[36]["CanEdit"]) == true)
            {

            }

        }

        protected void Grid_PriceList_GP_Actual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtNew = ViewState["dtNewActual"] as DataTable;
                for (int i = 5; i < e.Row.Cells.Count; i++)
                {
                    rowID += 1;
                    string strColumnName = dtNew.Columns[i].ColumnName;
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {
                        if (strColumnName.Contains("Select"))
                        {
                            System.Web.UI.WebControls.CheckBox chkSelect = new System.Web.UI.WebControls.CheckBox();

                            chkSelect.ID = "chkSelect_" + rowID;
                            chkSelect.AutoPostBack = true;
                            e.Row.Cells[i].Controls.Add(chkSelect);


                        }
                        else if (strColumnName.Contains("Action"))
                        {

                        }

                    }
                }


                lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();
            }
            for (int rowIndex = Grid_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 1; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    if (gvRow.Cells[0].Text == gvPreviousRow.Cells[0].Text)
                    {
                        if (gvPreviousRow.Cells[0].RowSpan < 2)
                        {
                            gvRow.Cells[0].RowSpan = 2;
                            gvRow.Cells[1].RowSpan = 2;
                            gvRow.Cells[2].RowSpan = 2;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = 2;
                            // gvRow.Cells[12].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = gvPreviousRow.Cells[13].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[0].Visible = false;
                        gvPreviousRow.Cells[1].Visible = false;
                        gvPreviousRow.Cells[2].Visible = false;
                        gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Visible = false;
                        //   gvPreviousRow.Cells[12].Visible = false;
                    }
                }

            }
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ApproveVacation")
            {

            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {

        }


        public void GenerateDynamicControls()
        {


            System.Web.UI.WebControls.Button btnEdit = new System.Web.UI.WebControls.Button();
            btnEdit.ID = "btnEditDynamic";
            btnEdit.Text = "Edit";
            btnEdit.CssClass = "btn-success";
            btnEdit.Click += new EventHandler(EditButton_Click);

            Page.Form.Controls.Add(btnEdit);


            System.Web.UI.WebControls.CheckBox chkSelect = new System.Web.UI.WebControls.CheckBox();
            chkSelect.ID = "chkSelect_Id";
            chkSelect.AutoPostBack = true;

            chkSelect.CheckedChanged += new EventHandler(CheckBoxCheckChanged);

            Page.Form.Controls.Add(chkSelect);

        }

        private void CheckBoxCheckChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
            {
                GridViewRow sdf = gvr;
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]);

                string str = chk.Checked.ToString();
                if (chk.Checked == true)
                {
                    CheckCount = CheckCount + 1;
                    string strBPM = gvr.Cells[0].Text;
                    string strBPMName = gvr.Cells[1].Text;
                    string strPMRM_Category_Id = gvr.Cells[6].Text;
                    BPM_Id = strBPM + "," + BPM_Id;
                    BPMName = strBPMName;
                    PMRM_Category_Id = strPMRM_Category_Id + "," + PMRM_Category_Id;
                    CreatePriceListtxt.Enabled = true;
                }



            }

            if (BPM_Id != null)
            {
                CreatePriceListtxt.Enabled = true;

            }
            else
            {
                CreatePriceListtxt.Enabled = false;

            }
        }

        protected void ReportPopupBtn_Click(object sender, EventArgs e)
        {

        }

        protected void Grid_Default_PriceList_GP_Actual_DataBound(object sender, EventArgs e)
        {

        }

        protected void GPAcualFinalBtn_Click(object sender, EventArgs e)
        {
            Session["BPM_Id"] = lblBPM_Id.Text;
            Session["PMRM_Catgeory_Id"] = lblPMRM_Category_Id.Text;

            Response.Redirect("~/PriceList_GP_Actual_Final.aspx");

        }

        protected void Grid_PriceList_GP_Actual_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;
                DataTable dtNew = ViewState["dtNewActual"] as DataTable;
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                GridViewRow HeaderGridRowCurrentNew = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                foreach (DataColumn item in dtNew.Columns)
                {
                    if (!item.ColumnName.Contains("New") && !Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                    {
                        TableCell HeaderCell = new TableCell();
                        HeaderCell.Text = item.ColumnName;
                        HeaderCell.ColumnSpan = 2;
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRow.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Current";
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRowCurrentNew.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "New";
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRowCurrentNew.Cells.Add(HeaderCell);

                    }
                    else if (!item.ColumnName.Contains("New"))
                    {
                        if (item.ColumnName != "Fk_BPM_Id" && item.ColumnName != "FkPriceTypeId" && item.ColumnName != "Fk_PM_RM_Category_Id" && item.ColumnName != "TradeName_Id")
                        {
                            TableCell HeaderCell = new TableCell();
                            if (item.ColumnName == "LatestStatus")
                            {
                                HeaderCell.Text = "New Status";
                            }
                            else if (item.ColumnName == "Date_Last_Shared_Price")
                            {
                                HeaderCell.Text = "Last Date Shared Price";
                            }
                            else if (item.ColumnName == "EnumDescription")
                            {
                                HeaderCell.Text = "RPL / NCR";
                            }
                            else if (item.ColumnName == "BPM_Product_Name")
                            {
                                HeaderCell.Text = "BPM Product Name";
                            }
                            else
                            {
                                HeaderCell.Text = item.ColumnName;
                            }
                            HeaderCell.CssClass = "customCellMergeTH";
                            HeaderGridRow.Cells.Add(HeaderCell);

                            HeaderCell = new TableCell();
                            HeaderCell.Text = " ";
                            HeaderCell.CssClass = "customCellMergeTH";
                            HeaderGridRowCurrentNew.Cells.Add(HeaderCell);
                        }
                    }
                }



                Grid_PriceList_GP_Actual.Controls[0].Controls.AddAt(0, HeaderGridRow);
                Grid_PriceList_GP_Actual.Controls[0].Controls.AddAt(1, HeaderGridRowCurrentNew);

            }
        }


        protected void btnEditing_Click(object sender, EventArgs e)
        {
            int CheckCount = 0;
            string BPM_Id = "";
            string BPMName = "";
            string PMRM_Category_Id = "";
            string TradeName_Id = "";
            foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
            {
                GridViewRow sdf = gvr;
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]);

                string str = chk.Checked.ToString();
                if (chk.Checked == true)
                {
                    CheckCount = CheckCount + 1;
                    string strBPM = gvr.Cells[0].Text;
                    string strBPMName = gvr.Cells[1].Text;
                    string strPMRM_Category_Id = gvr.Cells[6].Text;
                    TradeName_Id = gvr.Cells[7].Text;
                    BPM_Id = strBPM;
                    BPMName = strBPMName;
                    PMRM_Category_Id = strPMRM_Category_Id;
                }

                if (CheckCount > 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Check Multiple CheckBox')", true);
                    chk.Checked = false;
                    return;
                }



            }
            Session["BPM_Id"] = BPM_Id;
            Session["BPMName"] = BPMName;
            Session["PMRM_Category_Id"] = PMRM_Category_Id;
            Session["TradeName_Id"] = TradeName_Id;

            Response.Redirect("~/PriceList_GP_Actual_Final.aspx");
        }

        protected void CreatePriceList_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record " + BPM_Id + "')", true);
            int CheckCount = 0;
            string BPMName = "";
            string TradeName_Id = "";
            lblCompany_Id.Text = "1";
            DataTable dtCreate = new DataTable();
            DataTable dtCPLGrid = new DataTable();
            ClsPriceList_GP cls = new ClsPriceList_GP();
            string PriceListName = CreatePriceListtxt.Text;
            foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
            {
                GridViewRow sdf = gvr;
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]);

                string str = chk.Checked.ToString();
                if (chk.Checked == true)
                {
                    CheckCount = CheckCount + 1;
                    string strBPM = gvr.Cells[0].Text;
                    string strBPMName = gvr.Cells[1].Text;
                    string strPMRM_Category_Id = gvr.Cells[6].Text;
                    TradeName_Id = gvr.Cells[7].Text;
                    BPM_Id = strBPM + "," + BPM_Id;
                    BPMName = strBPMName;
                    PMRM_Category_Id = strPMRM_Category_Id + "," + PMRM_Category_Id;
                    CreatePriceListtxt.Enabled = true;
                }
            }



            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_CreatePriceListData(BPM_Id, PMRM_Category_Id);
            if (dtCheck.Rows.Count > 0)
            {
                dtCreate = cls.Get_GP_UpdateActualCreatePriceList(BPM_Id, PMRM_Category_Id, Convert.ToInt32(lblCompany_Id.Text), PriceListName);

            }
            else
            {
                dtCreate = cls.Get_GP_ActualCreatePriceList(BPM_Id, PMRM_Category_Id, Convert.ToInt32(lblCompany_Id.Text), PriceListName);

            }

            //PriceListName = dtCreate.Rows[0]["PriceListName"].ToString();
            dtCPLGrid = cls.Get_CreatePriceList_Grid_GP_Actual(Convert.ToInt32(lblCompany_Id.Text), PriceListName);

            if (dtCPLGrid.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PriceList Created!')", true);
                CreatePriceListtxt.Text = "";
                Grid_PriceListGP_ActualData();
            }
        }

        protected void CreatePriceListtxt_TextChanged(object sender, EventArgs e)
        {
            if (CreatePriceListtxt.Text != "" && BPM_Id != "")
            {
                CreatePriceList.Enabled = true;
            }
            else
            {
                CreatePriceList.Enabled = true;

            }
        }

        protected void PdfReport_Click(object sender, EventArgs e)
        {
            Session["CompanyMaster_Id"] = "1";

            Response.Redirect("~/PriceList.aspx");
        }
    }
}



