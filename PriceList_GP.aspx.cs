using BusinessAccessLayer;
using DataAccessLayer;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class PriceList_GP : System.Web.UI.Page
    {

        int User_Id;
        int rowID = 0;
        int Company_Id;
        int CheckCount = 0;
        string BPM_Id = "";
        string BPMName = "";

        string PMRM_Category_Id = "";
        string[] ArrRemoveColumns = {"Select","Action","Status","PriceBase","CompanyMaster_Name","Last_Shared_Price","Date_Last_Shared_Price","Suggested_Price","Fk_BPM_Id","Packing_Size",
                "Fk_PM_RM_Category_Id","LatestStatus","TradeName_Id","Fk_UnitMeasurement_Id","Sort_Index","FkPriceTypeId","EnumDescription","Fk_CompanyList_Id","BR_BOM_Id","BPM_Product_Name","TradeName"};

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

                if (Request.QueryString["EstimateName"] != null)
                {

                    Session["EstimateName"] = Request.QueryString["EstimateName"].ToString();
                    Session["Company_Id"] = Request.QueryString["CmpId"].ToString();
                }
                Grid_PriceListGP();
                DisplayView();

            }
            else
            {
                DataTable dtNew = ViewState["dtNew"] as DataTable;
                rowID = 0;
                Grid_PriceList_GP_Actual_Estimate.DataSource = dtNew;
                Grid_PriceList_GP_Actual_Estimate.DataBind();

                DataTable dtNewActual = ViewState["dtNewActual"] as DataTable;
                rowID = 0;
                Grid_PriceList_GP_Actual.DataSource = dtNewActual;
                Grid_PriceList_GP_Actual.DataBind();
            }
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
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void Grid_PriceListGP()
        {

            if (Request.QueryString["EstimateName"] != null)
            {
                //lblCompanyMasterList_Name.Text = Session["EstimateName"].ToString();
                string EstimateName = Session["EstimateName"].ToString();
                lblEstimateName.Text = EstimateName;
                string Company_Id = Session["Company_Id"].ToString();
                lblCompany_Id.Text = Company_Id;

                Grid_PriceListGP_ActualData();

            }
            else
            {
                Session["EstimateName"] = "";
                Grid_PriceListGP_ActualData();

            }
        }
        public void Grid_PriceListGP_ActualData()
        {
            DataTable dt1 = new DataTable();
            DataTable dtActual = new DataTable();
            DataTable dtActialEstimate = new DataTable();
            ClsPriceList_GP cls = new ClsPriceList_GP();
            if (Request.QueryString["EstimateName"] != null)
            {
                //lblCompanyMasterList_Name.Text = Session["EstimateName"].ToString();
                string EstimateName = Session["EstimateName"].ToString();
                lblEstimateName.Text = EstimateName;
                string Company_Id = Session["Company_Id"].ToString();
                lblCompany_Id.Text = Company_Id;

                //ForActualEstimateGrid-----------------------------


                dt1 = cls.GetPriceList_GP(Company_Id, EstimateName);
                lblBPM_Id.Text = dt1.Rows[0]["Fk_BPM_Id"].ToString();
                lblPMRM_Category_Id.Text = dt1.Rows[0]["Fk_PM_RM_Category_Id"].ToString();
                lblTradeName_Id.Text = dt1.Rows[0]["TradeName_Id"].ToString();

                DataTable dtNew = new DataTable();
                dtNew.Clear();
                dtNew.Columns.Add("Fk_BPM_Id", typeof(int));
                dtNew.Columns.Add("BPM_Product_Name", typeof(string));
                //dtNew.Columns.Add("Status", typeof(string));
                dtNew.Columns.Add("PriceBase", typeof(string));

                dtNew.Columns.Add("FkPriceTypeId", typeof(int));
                dtNew.Columns.Add("Date_Last_Shared_Price", typeof(string));
                dtNew.Columns.Add("Fk_PM_RM_Category_Id", typeof(int));
                dtNew.Columns.Add("EnumDescription", typeof(string));
                dtNew.Columns.Add("TradeName_Id", typeof(int));

                dtNew.AcceptChanges();
                var distinctBPMValues = dt1.AsEnumerable().Select(row => new { BPM_Id = row.Field<Int32>("Fk_BPM_Id"), }).Distinct();

                foreach (var itemBPM in distinctBPMValues)
                {
                    foreach (DataColumn item in dt1.Columns)
                    {
                        if (!Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                        {
                            var Data = dt1.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPM.BPM_Id && row.Field<string>(item.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(item.ColumnName).ToString()));
                            DataRow dr;
                            if (Data.Any())
                            {
                                DataTable objdtData = Data.CopyToDataTable();

                                if (!dtNew.Columns.Contains(item.ColumnName))
                                {
                                    dtNew.Columns.Add(item.ColumnName, typeof(decimal));
                                    dtNew.Columns.Add(item.ColumnName + "New", typeof(string));
                                    dtNew.AcceptChanges();
                                }

                                foreach (DataRow drData in objdtData.Rows)
                                {
                                    var DataNCROrRPL = dtNew.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPM.BPM_Id && row.Field<int>("FkPriceTypeId") == Convert.ToInt32(drData["FkPriceTypeId"]) && row.Field<string>("EnumDescription") == Convert.ToString(drData["EnumDescription"]));

                                    if (DataNCROrRPL.Any())
                                    {
                                        foreach (var row in DataNCROrRPL)
                                        {
                                            row.SetField(item.ColumnName, Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]));
                                            row.SetField(item.ColumnName + "New", Convert.ToString(drData[item.ColumnName].ToString().Split('|')[1]));
                                        }
                                    }
                                    else
                                    {
                                        dr = dtNew.NewRow();
                                        dr["Fk_BPM_Id"] = Convert.ToInt32(drData["Fk_BPM_Id"]);
                                        dr["BPM_Product_Name"] = Convert.ToString(drData["BPM_Product_Name"]);
                                        //dr["Status"] = Convert.ToString(drData["Status"]);
                                        dr["PriceBase"] = Convert.ToString(drData["PriceBase"]);
                                        dr["FkPriceTypeId"] = Convert.ToInt32(drData["FkPriceTypeId"]);
                                        dr["Date_Last_Shared_Price"] = Convert.ToString(drData["Date_Last_Shared_Price"]);
                                        dr["Fk_PM_RM_Category_Id"] = Convert.ToString(drData["Fk_PM_RM_Category_Id"]);
                                        dr["EnumDescription"] = Convert.ToString(drData["EnumDescription"]);
                                        dr["TradeName_Id"] = Convert.ToInt32(drData["TradeName_Id"]);

                                        //dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName]);
                                        dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]);
                                        dr[item.ColumnName + "New"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[1]);
                                        dtNew.Rows.Add(dr);
                                    }
                                    dtNew.AcceptChanges();
                                }
                            }
                            else
                            {
                                var EmptyData = dt1.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPM.BPM_Id);
                                if (EmptyData.Any())
                                {
                                    DataTable objdtItem = EmptyData.CopyToDataTable();

                                    foreach (DataRow drnonState in objdtItem.Rows)
                                    {
                                        var IsDataAvailable = dtNew.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPM.BPM_Id && row.Field<int>("FkPriceTypeId") == Convert.ToInt32(drnonState["FkPriceTypeId"]) && row.Field<string>("PriceBase") == Convert.ToString(drnonState["PriceBase"]) && row.Field<string>("EnumDescription") == Convert.ToString(drnonState["EnumDescription"]));
                                        if (!IsDataAvailable.Any())
                                        {
                                            dr = dtNew.NewRow();
                                            dr["Fk_BPM_Id"] = Convert.ToInt32(itemBPM.BPM_Id);
                                            dr["BPM_Product_Name"] = Convert.ToString(drnonState["BPM_Product_Name"]);
                                            //dr["Status"] = Convert.ToString(drnonState["Status"]);
                                            dr["PriceBase"] = Convert.ToString(drnonState["PriceBase"]);
                                            dr["FkPriceTypeId"] = Convert.ToInt32(drnonState["FkPriceTypeId"]);
                                            dr["Date_Last_Shared_Price"] = Convert.ToString(drnonState["Date_Last_Shared_Price"]);
                                            dr["Fk_PM_RM_Category_Id"] = Convert.ToString(drnonState["Fk_PM_RM_Category_Id"]);
                                            dr["EnumDescription"] = Convert.ToString(drnonState["EnumDescription"]);
                                            dr["TradeName_Id"] = Convert.ToInt32(drnonState["TradeName_Id"]);


                                            dtNew.Rows.Add(dr);

                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                dtNew.Columns.Add("Select", typeof(string));
                dtNew.AcceptChanges();
                ViewState["dtNew"] = dtNew;
                rowID = 0;
                Grid_PriceList_GP_Actual_Estimate.DataSource = dtNew;

                Grid_PriceList_GP_Actual_Estimate.DataBind();
                Grid_PriceList_GP_Actual_Estimate.Visible = true;
                Grid_PriceList_GP_Actual.Visible = false;

            }
            else 
            {
                dtActual = cls.GetPriceList_GP_Actual_Final(lblCompany_Id.Text);
                lblBPM_Id.Text = dtActual.Rows[0]["Fk_BPM_Id"].ToString();
                lblPMRM_Category_Id.Text = dtActual.Rows[0]["Fk_PM_RM_Category_Id"].ToString();




                DataTable dtNewActual = new DataTable();
                dtNewActual.Clear();
                dtNewActual.Columns.Add("Fk_BPM_Id", typeof(int));
                dtNewActual.Columns.Add("BPM_Product_Name", typeof(string));
                dtNewActual.Columns.Add("PriceBase", typeof(string));

                dtNewActual.Columns.Add("FkPriceTypeId", typeof(int));
                dtNewActual.Columns.Add("EnumDescription", typeof(string));
                dtNewActual.Columns.Add("Date_Last_Shared_Price", typeof(string));
                dtNewActual.Columns.Add("Fk_PM_RM_Category_Id", typeof(int));
                //dtNewActual.Columns.Add("EnumDescription", typeof(string));

                dtNewActual.Columns.Add("TradeName_Id", typeof(int));

                dtNewActual.AcceptChanges();
                var distinctBPMValuesActual = dtActual.AsEnumerable().Select(row => new { BPM_Id = row.Field<Int32>("Fk_BPM_Id"), }).Distinct();

                foreach (var itemBPMActual in distinctBPMValuesActual)
                {
                    foreach (DataColumn itemActual in dtActual.Columns)
                    {
                        if (!Array.Exists(ArrRemoveColumns, element => element == itemActual.ColumnName))
                        {
                            var Data = dtActual.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPMActual.BPM_Id && row.Field<string>(itemActual.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(itemActual.ColumnName).ToString()));
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
                                        //drActual["EnumDescription"] = Convert.ToString(drDataActual["EnumDescription"]);

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
                                var EmptyData = dtActual.AsEnumerable().Where(row => row.Field<Int32>("Fk_BPM_Id") == itemBPMActual.BPM_Id);
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
                                            //drActual["EnumDescription"] = Convert.ToString(drnonStateActual["EnumDescription"]);

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
                Grid_PriceList_GP_Actual.Visible = true;
                Grid_PriceList_GP_Actual_Estimate.Visible = false;

            }
           
            //--------------------------For Actual Grid----------------------------------------------




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
                //    if (lblCompanyFactoryExpence_Id.Text != "")
                //    {
                //        BtnCreatePricelist.Visible = false;

                //    }
                //    else
                //    {
                //        BtnCreatePricelist.Visible = true;
                //    }

                //}
                //else
                //{
                //    BtnCreatePricelist.Visible = false;

                //}
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





        //protected void Grid_Default_PriceList_GP_PreRender(object sender, EventArgs e)
        //{
        //    for (int rowIndex = Grid_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)

        //    {

        //        GridViewRow row = Grid_PriceList_GP_Actual.Rows[rowIndex];

        //        GridViewRow previousRow = Grid_PriceList_GP_Actual.Rows[rowIndex + 1];



        //        if (row.Cells[0].Text == previousRow.Cells[0].Text && row.Cells[1].Text == previousRow.Cells[1].Text)

        //        {

        //            row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 : previousRow.Cells[0].RowSpan + 1;

        //            row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 : previousRow.Cells[1].RowSpan + 1;





        //            previousRow.Cells[0].Visible = false;

        //            previousRow.Cells[1].Visible = false;

        //        }

        //    }
        //}




        [Obsolete]
        protected void CreatePriceList_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record " + BPM_Id + "')", true);
            DataTable dtCreate = new DataTable();
            DataTable dtCPLGrid = new DataTable();
            ClsPriceList_GP cls = new ClsPriceList_GP();
            string PriceListName = CreatePriceListtxt.Text;
            if (Request.QueryString["EstimateName"] != null)
            {
                string EstimateName = Request.QueryString["EstimateName"].ToString();
                foreach (GridViewRow gvr in Grid_PriceList_GP_Actual_Estimate.Rows)
                {
                    GridViewRow sdf = gvr;
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]);

                    string str = chk.Checked.ToString();
                    if (chk.Checked == true)
                    {
                        CheckCount = CheckCount + 1;
                        string strBPM = gvr.Cells[0].Text;
                        string strBPMName = gvr.Cells[1].Text;
                        string strPMRM_Category_Id = gvr.Cells[5].Text;
                        //TradeName_Id = gvr.Cells[8].Text;
                        BPM_Id = strBPM;
                        BPMName = strBPMName;
                        PMRM_Category_Id = strPMRM_Category_Id;
                        CreatePriceListtxt.Enabled = true;
                        DataTable dtCheck = new DataTable();

                        //dtCheck = cls.CHECK_CreatePriceListData(BPM_Id, PMRM_Category_Id, Convert.ToInt32(TradeName_Id));
                        //if (dtCheck.Rows.Count > 0)
                        //{
                        //dtCreate = cls.Get_GP_UpdateActualCreatePriceList(BPM_Id, PMRM_Category_Id, Convert.ToInt32(lblCompany_Id.Text), PriceListName);

                        //}
                        //else
                        //{

                        //}
                        dtCreate = cls.Get_GP_ActualEstimateDataCreatePriceList(BPM_Id, PMRM_Category_Id, 1, PriceListName, "");

                        dtCreate = cls.Get_GP_ActualEstimateDataCreatePriceList(BPM_Id, PMRM_Category_Id, 1, PriceListName,EstimateName);
                    }

                }

            }
            else
            {
                foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
                {
                    GridViewRow sdf = gvr;
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Controls[0]);

                    string str = chk.Checked.ToString();
                    if (chk.Checked == true)
                    {
                        CheckCount = CheckCount + 1;
                        string strBPM = gvr.Cells[0].Text;
                        string strBPMName = gvr.Cells[1].Text;
                        string strPMRM_Category_Id = gvr.Cells[6].Text;
                        //TradeName_Id = gvr.Cells[8].Text;
                        BPM_Id = strBPM;
                        BPMName = strBPMName;
                        PMRM_Category_Id = strPMRM_Category_Id;
                        CreatePriceListtxt.Enabled = true;
                        DataTable dtCheck = new DataTable();

                        //dtCheck = cls.CHECK_CreatePriceListData(BPM_Id, PMRM_Category_Id, Convert.ToInt32(TradeName_Id));
                        //if (dtCheck.Rows.Count > 0)
                        //{
                        //dtCreate = cls.Get_GP_UpdateActualCreatePriceList(BPM_Id, PMRM_Category_Id, Convert.ToInt32(lblCompany_Id.Text), PriceListName);

                        //}
                        //else
                        //{

                        //}
                        dtCreate = cls.Get_GP_ActualEstimateDataCreatePriceList(BPM_Id, PMRM_Category_Id, 1, PriceListName,"");
                    }

                }

            }

            //PriceListName = dtCreate.Rows[0]["PriceListName"].ToString();
            dtCPLGrid = cls.Get_CreatePriceList_Grid_GP(1, PriceListName, lblEstimateName.Text);

            if (dtCPLGrid.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PriceList Created!')", true);
                CreatePriceListtxt.Text = "";
                Grid_PriceListGP_ActualData();
            }

            //DataTable dtCheck = new DataTable();

            //dtCheck = cls.CHECK_CreatePriceListData(BPM_Id, PMRM_Category_Id);
            //if (dtCheck.Rows.Count > 0)
            //{
            //    dtCreate = cls.Get_GP_UpdateActualCreatePriceList(BPM_Id, PMRM_Category_Id, Convert.ToInt32(lblCompany_Id.Text), PriceListName);

            //}
        }
        //else
        //{
        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record " + BPM_Id + "')", true);
        //    int CheckCount = 0;
        //    string BPMName = "";
        //    string TradeName_Id = "";
        //    lblCompany_Id.Text = "1";

        //        //else
        //        //{

        //        //}
        //    }
        //}

        //dtCreate = cls.Get_GP_ActualCreatePriceList(BPM_Id, PMRM_Category_Id, Convert.ToInt32(lblCompany_Id.Text), PriceListName);




        ////PriceListName = dtCreate.Rows[0]["PriceListName"].ToString();
        //dtCPLGrid = cls.Get_CreatePriceList_Grid_GP_Actual(Convert.ToInt32(lblCompany_Id.Text), PriceListName);

        //if (dtCPLGrid.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PriceList Created!')", true);
        //    CreatePriceListtxt.Text = "";
        //    Grid_PriceListGP_ActualData();
        //}
        //    }

        //}

        protected void btnEditing_Click(object sender, EventArgs e)
        {
            int CheckCount = 0;
            string BPM_Id = "";
            string BPMName = "";
            string Status = "";
            string PMRM_Category_Id = "";
            if (Request.QueryString["EstimateName"]!=null)
            {
                foreach (GridViewRow gvr in Grid_PriceList_GP_Actual_Estimate.Rows)
                {
                    GridViewRow sdf = gvr;
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]);

                    string str = chk.Checked.ToString();
                    if (chk.Checked == true)
                    {
                        CheckCount = CheckCount + 1;
                        string strBPM = gvr.Cells[0].Text;
                        string strBPMName = gvr.Cells[1].Text;
                        Status = gvr.Cells[2].Text;

                        string strPMRM_Category_Id = gvr.Cells[5].Text;
                        string TradeName_Id = gvr.Cells[7].Text;
                        BPM_Id = strBPM;
                        BPMName = strBPMName;

                        PMRM_Category_Id = strPMRM_Category_Id;
                        Session["EstimateName"] = lblEstimateName.Text;
                        Session["TradeName_Id"] = TradeName_Id;
                        Session["Status"] = Status;

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
                Session["Status"] = Status;

                Session["PMRM_Category_Id"] = PMRM_Category_Id;
                Response.Redirect("~/PriceList_GP_ActualEstimate.aspx");
            }
            else
            {
                foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
                {
                    GridViewRow sdf = gvr;
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Controls[0]);

                    string str = chk.Checked.ToString();
                    if (chk.Checked == true)
                    {
                        CheckCount = CheckCount + 1;
                        string strBPM = gvr.Cells[0].Text;
                        string strBPMName = gvr.Cells[1].Text;
                        Status = gvr.Cells[2].Text;

                        string strPMRM_Category_Id = gvr.Cells[6].Text;
                        string TradeName_Id = gvr.Cells[7].Text;
                        BPM_Id = strBPM;
                        BPMName = strBPMName;

                        PMRM_Category_Id = strPMRM_Category_Id;
                        //Session["EstimateName"] = lblEstimateName.Text;
                        Session["TradeName_Id"] = TradeName_Id;
                        Session["Status"] = Status;

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
                Session["Status"] = Status;

                Session["PMRM_Category_Id"] = PMRM_Category_Id;
                Response.Redirect("~/PriceList_GP_ActualEstimate.aspx");
            }
        
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

        protected void Grid_PriceList_GP_Actual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtNewActual = ViewState["dtNewActual"] as DataTable;
                for (int i = 5; i < e.Row.Cells.Count; i++)
                {
                    rowID += 1;
                    string strColumnName = dtNewActual.Columns[i].ColumnName;
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {
                        if (strColumnName.Contains("Select"))
                        {
                            System.Web.UI.WebControls.CheckBox chkSelect = new System.Web.UI.WebControls.CheckBox();

                            chkSelect.ID = "chkSelect_" + rowID;
                            chkSelect.AutoPostBack = true;
                            chkSelect.CheckedChanged += new EventHandler(chkDynamic_CheckedChanged);
                            e.Row.Cells[i].Controls.Add(chkSelect);


                        }
                        else if (strColumnName.Contains("Action"))
                        {

                        }
                        else if (strColumnName.Contains("New"))
                        {
                            System.Web.UI.WebControls.Label label = new System.Web.UI.WebControls.Label();
                            //label.CssClass = "form-control input-sm";
                            label.ID = "txtStateNew" + rowID;
                            //label.ReadOnly = true;

                            e.Row.Cells[i].Controls.Add(label);
                        }
                    }
                }
                lblDynamicColumnCountActual.Text = (e.Row.Cells.Count).ToString();
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
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].RowSpan = 2;
                            // gvRow.Cells[12].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[0].Visible = false;
                        gvPreviousRow.Cells[1].Visible = false;
                        gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Visible = false;
                        //   gvPreviousRow.Cells[12].Visible = false;
                    }
                }

            }
            for (int rowIndex = Grid_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 2; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    if (gvRow.Cells[2].Text == gvPreviousRow.Cells[2].Text)
                    {
                        if (gvPreviousRow.Cells[2].RowSpan < 2)
                        {
                            gvRow.Cells[2].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[2].RowSpan + 1;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;

                        }
                        gvPreviousRow.Cells[2].Visible = false;
                    }
                }

            }
        }

        private void chkDynamic_CheckedChanged(object sender, EventArgs e)
        {

            //foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
            //{
            //    GridViewRow sdf = gvr;
            //    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Controls[0]);

            //    string str = chk.Checked.ToString();
            //    if (chk.Checked == true)
            //    {
            //        CheckCount = CheckCount + 1;
            //        string strBPM = gvr.Cells[0].Text;
            //        string strBPMName = gvr.Cells[1].Text;
            //        string strPMRM_Category_Id = gvr.Cells[6].Text;
            //        BPM_Id = strBPM + "," + BPM_Id;
            //        BPMName = strBPMName;
            //        PMRM_Category_Id = strPMRM_Category_Id + "," + PMRM_Category_Id;
            //        CreatePriceListtxt.Enabled = true;
            //    }



            //}

            if (BPM_Id != null)
            {
                CreatePriceListtxt.Enabled = true;

            }
            else
            {
                CreatePriceListtxt.Enabled = false;

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        [Obsolete]
        protected void PdfReport_Click(object sender, EventArgs e)
        {
            Session["CompanyMaster_Id"] = "1";

            Response.Redirect("~/PriceList.aspx");


            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);
            //    foreach (GridViewRow row in Grid_PriceList_GP_Actual.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            //Hide the Row if CheckBox is not checked
            //            row.Visible = ((System.Web.UI.WebControls.CheckBox)(row.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]) as CheckBox).Checked;

            //        }

            //        foreach (TableCell cell in row.Cells)
            //        {
            //            cell.BackColor = Grid_PriceList_GP_Actual.RowStyle.BackColor;
            //            List<Control> controls = new List<Control>();

            //            //Add controls to be removed to Generic List
            //            foreach (Control control in cell.Controls)
            //            {
            //                controls.Add(control);
            //            }

            //            //Loop through the controls to be removed and replace then with Literal
            //            foreach (Control control in controls)
            //            {
            //                switch (control.GetType().Name)
            //                {

            //                    case "TextBox":
            //                        cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
            //                        break;
            //                    case "Label":
            //                        cell.Controls.Add(new Literal { Text = (control as Label).Text });
            //                        break;
            //                }
            //                cell.Controls.Remove(control);
            //            }
            //        }
            //    }

            //    Grid_PriceList_GP_Actual.RenderControl(hw);
            //    StringReader sr = new StringReader(sw.ToString());
            //    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 10f, 10f, 10f, 0f);
            //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //    pdfDoc.Open();
            //    htmlparser.Parse(sr);
            //    pdfDoc.Close();
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;filename=PriceListGPEstimate [" + DateTime.Now.ToShortDateString() + "].pdf");
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.Write(pdfDoc);
            //    Response.End();

            //    //foreach (GridViewRow row in Grid_PriceList_GP_Actual.Rows)
            //    //{
            //    //    foreach (TableCell cell in row.Cells)
            //    //    {
            //    //        cell.BackColor = Grid_PriceList_GP_Actual.RowStyle.BackColor;
            //    //        List<Control> controls = new List<Control>();

            //    //        //Add controls to be removed to Generic List
            //    //        foreach (Control control in cell.Controls)
            //    //        {
            //    //            controls.Add(control);
            //    //        }

            //    //        //Loop through the controls to be removed and replace then with Literal
            //    //        foreach (Control control in controls)
            //    //        {
            //    //            switch (control.GetType().Name)
            //    //            {

            //    //                case "TextBox":
            //    //                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
            //    //                    break;
            //    //                case "Label":
            //    //                    cell.Controls.Add(new Literal { Text = (control as Label).Text });
            //    //                    break;
            //    //            }
            //    //            cell.Controls.Remove(control);
            //    //        }
            //    //    }
            //    //}


            //}
            Response.Redirect("~/PriceList.aspx");
        }

        protected void CreatePriceListtxt_TextChanged(object sender, EventArgs e)
        {
            if (CreatePriceListtxt.Text != "")
            {
                CreatePriceList.Enabled = true;
            }
            else
            {
                CreatePriceList.Enabled = false;

            }
        }


        protected void Grid_PriceList_GP_Actual_Estimate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtNewEstimate = ViewState["dtNew"] as DataTable;
                for (int i = 6; i < e.Row.Cells.Count; i++)
                {
                    rowID += 1;
                    string strColumnName = dtNewEstimate.Columns[i].ColumnName;
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {
                        if (strColumnName.Contains("Select"))
                        {
                            System.Web.UI.WebControls.CheckBox chkSelect = new System.Web.UI.WebControls.CheckBox();

                            chkSelect.ID = "chkSelect_" + rowID;
                            chkSelect.AutoPostBack = true;
                            chkSelect.CheckedChanged += new EventHandler(chkDynamicEstimate_CheckedChanged);

                            e.Row.Cells[i].Controls.Add(chkSelect);


                        }
                        else if (strColumnName.Contains("Action"))
                        {

                        }
                    }


                    lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();
                }
                for (int rowIndex = Grid_PriceList_GP_Actual_Estimate.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow gvRow = Grid_PriceList_GP_Actual_Estimate.Rows[rowIndex];
                    GridViewRow gvPreviousRow = Grid_PriceList_GP_Actual_Estimate.Rows[rowIndex + 1];
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
                            }
                            else
                            {
                                gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                                gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                                gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;


                                gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan + 1;
                            }
                            gvPreviousRow.Cells[0].Visible = false;
                            gvPreviousRow.Cells[1].Visible = false;
                            gvPreviousRow.Cells[2].Visible = false;


                            gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Visible = false;
                        }

                    }

                }


            }
        }

        private void chkDynamicEstimate_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in Grid_PriceList_GP_Actual_Estimate.Rows)
            {
                GridViewRow sdf = gvr;
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[0]);

                string str = chk.Checked.ToString();
                if (chk.Checked == true)
                {
                    CheckCount = CheckCount + 1;
                    string strBPM = gvr.Cells[0].Text;
                    string strBPMName = gvr.Cells[1].Text;
                    string strPMRM_Category_Id = gvr.Cells[5].Text;
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

        //public void GenerateDynamicControls()
        //{


        //    System.Web.UI.WebControls.Button btnEdit = new System.Web.UI.WebControls.Button();
        //    btnEdit.ID = "btnEditDynamic";
        //    btnEdit.Text = "Edit";
        //    btnEdit.CssClass = "btn-success";
        //    btnEdit.Click += new EventHandler(EditButton_Click);

        //    Page.Form.Controls.Add(btnEdit);


        //    System.Web.UI.WebControls.CheckBox chkSelect = new System.Web.UI.WebControls.CheckBox();
        //    chkSelect.ID = "chkSelect_Id";
        //    chkSelect.AutoPostBack = true;

        //    chkSelect.CheckedChanged += new EventHandler(CheckBoxCheckChanged);

        //    Page.Form.Controls.Add(chkSelect);

        //}

        //private void CheckBoxCheckChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
        //    {
        //        GridViewRow sdf = gvr;
        //        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Controls[0]);

        //        string str = chk.Checked.ToString();
        //        if (chk.Checked == true)
        //        {
        //            CheckCount = CheckCount + 1;
        //            string strBPM = gvr.Cells[0].Text;
        //            string strBPMName = gvr.Cells[1].Text;
        //            string strPMRM_Category_Id = gvr.Cells[6].Text;
        //            BPM_Id = strBPM + "," + BPM_Id;
        //            BPMName = strBPMName;
        //            PMRM_Category_Id = strPMRM_Category_Id + "," + PMRM_Category_Id;
        //            CreatePriceListtxt.Enabled = true;
        //        }
        //    }
        //}
        //private void EditButton_Click(object sender, EventArgs e)
        //{
        //    int CheckCount = 0;
        //    string BPM_Id = "";
        //    string BPMName = "";
        //    string Status = "Estimate";
        //    string PMRM_Category_Id = "";
        //    foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
        //    {
        //        GridViewRow sdf = gvr;
        //        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Controls[0]);

        //        string str = chk.Checked.ToString();
        //        if (chk.Checked == true)
        //        {
        //            CheckCount = CheckCount + 1;
        //            string strBPM = gvr.Cells[0].Text;
        //            string strBPMName = gvr.Cells[1].Text;
        //            string strPMRM_Category_Id = gvr.Cells[5].Text;
        //            BPM_Id = strBPM;
        //            BPMName = strBPMName;
        //            PMRM_Category_Id = strPMRM_Category_Id;
        //            string TradeName_Id = gvr.Cells[7].Text;
        //            Session["TradeName_Id"] = TradeName_Id;

        //        }

        //        if (CheckCount > 1)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Check Multiple CheckBox')", true);
        //            chk.Checked = false;
        //            return;
        //        }



        //    }
        //    Session["BPM_Id"] = BPM_Id;
        //    Session["BPMName"] = BPMName;
        //    Session["Status"] = Status;

        //    Session["PMRM_Category_Id"] = PMRM_Category_Id;
        //    Response.Redirect("~/PriceList_GP_ActualEstimate.aspx");
        //}

        protected void Grid_PriceList_GP_Actual_Estimate_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;
                DataTable dtNewEstimate = ViewState["dtNew"] as DataTable;
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                GridViewRow HeaderGridRowCurrentNew = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                foreach (DataColumn item in dtNewEstimate.Columns)
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
                        TableCell Cell = new TableCell();


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



                Grid_PriceList_GP_Actual_Estimate.Controls[0].Controls.AddAt(0, HeaderGridRow);
                Grid_PriceList_GP_Actual_Estimate.Controls[0].Controls.AddAt(1, HeaderGridRowCurrentNew);

            }

        }

        //protected void btnEditActual_Click(object sender, EventArgs e)
        //{
        //    int CheckCount = 0;
        //    string BPM_Id = "";
        //    string BPMName = "";
        //    string Status = "Actual";

        //    string PMRM_Category_Id = "";
        //    foreach (GridViewRow gvr in Grid_PriceList_GP_Actual.Rows)
        //    {
        //        GridViewRow sdf = gvr;
        //        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCountActual.Text) - 1].Controls[0]);

        //        string str = chk.Checked.ToString();
        //        if (chk.Checked == true)
        //        {
        //            CheckCount = CheckCount + 1;
        //            string strBPM = gvr.Cells[0].Text;
        //            string strBPMName = gvr.Cells[1].Text;
        //            string strPMRM_Category_Id = gvr.Cells[6].Text;
        //            BPM_Id = strBPM;
        //            BPMName = strBPMName;
        //            PMRM_Category_Id = strPMRM_Category_Id;
        //            string TradeName_Id = gvr.Cells[8].Text;
        //            Session["TradeName_Id"] = TradeName_Id;
        //        }

        //        if (CheckCount > 1)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can Not Check Multiple CheckBox')", true);
        //            chk.Checked = false;
        //            return;
        //        }



        //    }
        //    Session["BPM_Id"] = BPM_Id;
        //    Session["BPMName"] = BPMName;
        //    Session["Status"] = Status;
        //    Session["PMRM_Category_Id"] = PMRM_Category_Id;

        //    Response.Redirect("~/PriceList_GP_ActualEstimate.aspx");
        //}

        //protected void EstimatedPriceDropdown_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (EstimatedPriceDropdown.SelectedValue == "1")
        //    {

        //        Grid_PriceList_GP_Actual_Estimate.Visible = true;
        //        //Grid_PriceList_GP_Actual.Visible = false;
        //        //btnEditing.Visible = true;
        //        btnEditActual.Visible = false;
        //        btnEditing.Visible = true;

        //    }
        //    else if (EstimatedPriceDropdown.SelectedValue == "0")
        //    {
        //        Grid_PriceList_GP_Actual_Estimate.Visible = false;
        //        //Grid_PriceList_GP_Actual.Visible = true;
        //        btnEditing.Visible = false;
        //        btnEditActual.Visible = true;
        //    }
        //    else
        //    {
        //        //Grid_PriceList_GP_Actual.Visible = true;
        //        Grid_PriceList_GP_Actual_Estimate.Visible = true;
        //        btnEditing.Visible = true;
        //        btnEditActual.Visible = true;
        //    }
        //}
    }
}
