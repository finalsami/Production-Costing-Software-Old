using BusinessAccessLayer;
using DataAccessLayer;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class EstimatePriceList : System.Web.UI.Page
    {
        int User_Id;
        int Company_Id;
        string Status = "";
        public string datajs = "";
        int tempbpm = 0;
        int tid = 0;
        int styleid = 0;
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
                    Status = Request.QueryString["Status"].ToString();
                }
                else
                {
                    Session["EstimateName"] = "";
                }
                //DisplayView();
                if (Status == "Actual" || Status == "")
                {
                    OtherCompanyDropdown.Visible = true;
                    EstimatedPriceDropdown.SelectedValue = "0";
                    EstimatedPriceDropdown.Enabled = false;
                    Grid_EstimatePricelist.Visible = false;
                    Grid_EstimatePricelistStatusWise.Visible = false;
                    lblCompanyMasterList_Name.Text = "";

                }
                else
                {
                    MasterPackingDropdown.Enabled = true;
                    EstimatedPriceDropdown.Enabled = false;
                    OtherCompanyDropdown.Enabled = false;

                }
                CompanyListDropDownListCombo();

                Grid_EstimatePriceListData();
                MasterPackingDropdown.Enabled = false;
            }


        }
        public void CompanyListDropDownListCombo()
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            dt = cls.Get_OtherCompanyMasterData();

            OtherCompanyDropdown.DataSource = dt;
            OtherCompanyDropdown.DataTextField = "CompanyMaster_Name";

            OtherCompanyDropdown.DataValueField = "CompanyMaster_Id";
            OtherCompanyDropdown.DataBind();
            OtherCompanyDropdown.Items.Insert(0, "Select Company");
        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[34]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[34]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[34]["CanDelete"]).ToString();
            lblCanView.Text = Convert.ToBoolean(dtMenuList.Rows[34]["CanView"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblCompanyFactoryExpence_Id.Text != "")
                {
                    BtnCreatePricelist.Visible = false;

                }
                else
                {
                    BtnCreatePricelist.Visible = true;
                }

            }
            else
            {
                BtnCreatePricelist.Visible = false;

            }
            if (lblCanView.Text == "True")
            {
                Grid_EstimatePricelistStatusWise.Visible = true;
                Grid_EstimatePricelist.Visible = true;
            }
            else
            {
                Grid_EstimatePricelistStatusWise.Visible = false;
                Grid_EstimatePricelist.Visible = false;
            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[34]["GroupId"]);
            lblCanView.Text = Convert.ToBoolean(dtMenuList.Rows[34]["CanView"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblCompanyFactoryExpence_Id.Text != "")
                {
                    BtnCreatePricelist.Visible = false;

                }
                else
                {
                    BtnCreatePricelist.Visible = true;
                }

            }
            else
            {
                BtnCreatePricelist.Visible = false;

            }
            if (lblCanView.Text == "True")
            {
                Grid_EstimatePricelistStatusWise.Visible = true;
                Grid_EstimatePricelist.Visible = true;
            }
            else
            {
                Grid_EstimatePricelistStatusWise.Visible = false;
                Grid_EstimatePricelist.Visible = false;
            }

        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().ToUpper() != "")
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
        public void Grid_EstimatePriceListData()
        {
            DataTable dt = new DataTable();
            DataTable dtActialEstimate = new DataTable();
            ClsEstimatePriceList cls = new ClsEstimatePriceList();
            if (Session["EstimateName"].ToString() != "")

            {
                string EstimateName = Session["EstimateName"].ToString();
                lblEstimateName.Text = EstimateName;
                int Company_Id = Convert.ToInt32(Session["Company_Id"]);
                lblCompany_Id.Text = Company_Id.ToString(); ;


                //dt = cls.Get_EstimatePriceListByCompanyForActualOnly(Company_Id);
                //lblCompanyMasterList_Name.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();


                //Grid_EstimatePricelist.DataSource = dt;
                //Grid_EstimatePricelist.DataBind();

                tid = 0;

                dtActialEstimate = cls.GetOtherCompanyActualEstimate(Company_Id, EstimateName);
                lblCompanyMasterList_Name.Text = dtActialEstimate.Rows[0]["CompanyMaster_Name"].ToString();
                
                Grid_EstimatePricelistStatusWise.DataSource = dtActialEstimate;
                Grid_EstimatePricelistStatusWise.DataBind();

                Grid_EstimatePricelist.Visible = false;
                Grid_EstimatePricelistStatusWise.Visible = true;

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

            }
            //else
            //{
            //    dt = cls.Get_EstimatePriceListByCompanyForActualOnly(Company_Id);
            //    //lblCompanyMasterList_Name.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();
            //    Grid_EstimatePricelist.DataSource = dt;
            //    Grid_EstimatePricelist.DataBind();
            //    Grid_EstimatePricelist.Visible = true;
            //}

        }
        protected void ChkSuggestedPrice_CheckedChanged(object sender, EventArgs e)
        {
            ProEsitmate_PriceList proSuggest = new ProEsitmate_PriceList();
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            //CheckBox ChkSelect = (CheckBox)Grid_EstimatePricelist.Rows[index].FindControl("ChkSuggestedPrice") as CheckBox);
            string AllWCFE_Id = "";
            string AllSuggestedPrice = "";
            if (EstimatedPriceDropdown.SelectedValue == "1")
            {

                foreach (GridViewRow row1 in Grid_EstimatePricelistStatusWise.Rows)
                {

                    CheckBox chkbox_Check = row1.FindControl("CheckBox_Check") as CheckBox;
                    if (chkbox_Check.Checked == true)
                    {
                        string SuggestedPricetxt = (row1.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text;
                        string CWFE_Id = (row1.FindControl("lblCompanyFactoryExpence_Id") as Label).Text;
                        if (SuggestedPricetxt == "0.00" || SuggestedPricetxt == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check Suggested Price!')", true);
                            chkbox_Check.Checked = false;
                        }
                        else
                        {

                            AllWCFE_Id = AllWCFE_Id + CWFE_Id + ",";
                            AllSuggestedPrice = AllSuggestedPrice + SuggestedPricetxt + ",";

                        }
                    }

                }
            }
            else if (EstimatedPriceDropdown.SelectedValue == "0")
            {

                foreach (GridViewRow row1 in Grid_EstimatePricelist.Rows)
                {

                    CheckBox chkbox_Check = row1.FindControl("CheckBox_Check") as CheckBox;
                    if (chkbox_Check.Checked == true)
                    {
                        string SuggestedPricetxt = (row1.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text;
                        string CWFE_Id = (row1.FindControl("lblCompanyFactoryExpence_Id") as Label).Text;
                        if (SuggestedPricetxt == "0.00" || SuggestedPricetxt == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check Suggested Price!')", true);
                            chkbox_Check.Checked = false;
                        }
                        else
                        {

                            AllWCFE_Id = AllWCFE_Id + CWFE_Id + ",";
                            AllSuggestedPrice = AllSuggestedPrice + SuggestedPricetxt + ",";

                        }
                    }

                }
            }
            else
            {
                foreach (GridViewRow row1 in Grid_EstimatePricelistStatusWise.Rows)
                {

                    CheckBox chkbox_Check = row1.FindControl("CheckBox_Check") as CheckBox;
                    if (chkbox_Check.Checked == true)
                    {
                        string SuggestedPricetxt = (row1.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text;
                        string CWFE_Id = (row1.FindControl("lblCompanyFactoryExpence_Id") as Label).Text;
                        if (SuggestedPricetxt == "0.00" || SuggestedPricetxt == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check Suggested Price!')", true);
                            chkbox_Check.Checked = false;
                        }
                        else
                        {

                            AllWCFE_Id = AllWCFE_Id + CWFE_Id + ",";
                            AllSuggestedPrice = AllSuggestedPrice + SuggestedPricetxt + ",";

                        }
                    }

                }
                foreach (GridViewRow row1 in Grid_EstimatePricelist.Rows)
                {

                    CheckBox chkbox_Check = row1.FindControl("CheckBox_Check") as CheckBox;
                    if (chkbox_Check.Checked == true)
                    {
                        string SuggestedPricetxt = (row1.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text;
                        string CWFE_Id = (row1.FindControl("lblCompanyFactoryExpence_Id") as Label).Text;
                        if (SuggestedPricetxt == "0.00" || SuggestedPricetxt == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check Suggested Price!')", true);
                            chkbox_Check.Checked = false;
                        }
                        else
                        {

                            AllWCFE_Id = AllWCFE_Id + CWFE_Id + ",";
                            AllSuggestedPrice = AllSuggestedPrice + SuggestedPricetxt + ",";

                        }
                    }

                }
            }


        }

        [Obsolete]
        protected void BtnCreatePricelist_Click(object sender, EventArgs e)
        {
            ClsEstimatePriceList cls = new ClsEstimatePriceList();
            ProEsitmate_PriceList pro = new ProEsitmate_PriceList();
            if (Session["EstimateName"].ToString() == "")
            {
                pro.Fk_Company_Id = Convert.ToInt32(OtherCompanyDropdown.SelectedValue);
                lblCompany_Id.Text = pro.Fk_Company_Id.ToString();
            }
            else
            {
                pro.Fk_Company_Id = Convert.ToInt32(Session["Company_Id"]);
                lblCompany_Id.Text = pro.Fk_Company_Id.ToString();

            }
            int status = 0;
            if (EstimatedPriceDropdown.SelectedValue == "0")
            {

                foreach (GridViewRow row1 in Grid_EstimatePricelistStatusWise.Rows)
                {

                    CheckBox chkbox_Check = row1.FindControl("CheckBox_Check") as CheckBox;
                    if (chkbox_Check.Checked == true)
                    {
                        string FinalPricePerUnit = (row1.FindControl("FinalPricePerUnittxt") as TextBox).Text;

                        int CWFE_Id = Convert.ToInt32((row1.FindControl("lblCompanyFactoryExpence_Id") as Label).Text);
                        if (FinalPricePerUnit == "0.00" || FinalPricePerUnit == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check Suggested Price!')", true);
                            chkbox_Check.Checked = false;
                        }
                        else
                        {

                            DataTable dtCWFE = new DataTable();
                            lblSuggestedPricetxt.Text = FinalPricePerUnit;
                            dtCWFE = cls.Get_CWFE_For_EstimatePriceList(CWFE_Id, Convert.ToInt32(lblCompany_Id.Text), lblEstimateName.Text);
                            if (dtCWFE.Rows.Count > 0)
                            {

                                for (int i = 0; i < dtCWFE.Rows.Count; i++)
                                {
                                    pro.Fk_CWFE_Id = Convert.ToInt32(dtCWFE.Rows[i]["comp_CompanyFactoryExpence_Id"]);
                                    pro.Fk_BPM_Id = Convert.ToInt32(dtCWFE.Rows[i]["Fk_BPM_Id"]);
                                    pro.CWFE_TotalCostPerLtr = Convert.ToDecimal(dtCWFE.Rows[i]["TotalAmtPerLiter"]);

                                    pro.CWFE_FctryExp_Amt = Convert.ToDecimal(dtCWFE.Rows[i]["FectoryExpenceAmount"]);
                                    pro.CWFE_FctryExp_Per = Convert.ToDecimal(dtCWFE.Rows[i]["FectoryExpencePer"]);

                                    pro.CWFE_Mrkt_Amt = Convert.ToDecimal(dtCWFE.Rows[i]["MarketedByChargesAmount"]);
                                    pro.CWFE_Mrkt_Per = Convert.ToDecimal(dtCWFE.Rows[i]["MarketedByChargesPer"]);

                                    pro.CWFE_Other_Amt = Convert.ToDecimal(dtCWFE.Rows[i]["OtherPerAmount"]);
                                    pro.CWFE_Other_Per = Convert.ToDecimal(dtCWFE.Rows[i]["OtherPer"]);

                                    pro.CWFE_Profit_Amt = Convert.ToDecimal(dtCWFE.Rows[i]["ProfitAmount"]);
                                    pro.CWFE_Profit_Per = Convert.ToDecimal(dtCWFE.Rows[i]["ProfitPer"]);

                                    pro.CWFE_FFCostPerLtr = Convert.ToDecimal(dtCWFE.Rows[i]["FinalFectoryCostPerUnit"]);
                                    //pro.PackingSize = Convert.ToDecimal(dtCWFE.Rows[i]["Packing_Size"]);
                                    //pro.UnitMeasurement = Convert.ToInt32(dtCWFE.Rows[i]["Fk_PMRMCategory_Id"]);
                                    pro.Fk_PMRM_Catgeory_Id = Convert.ToInt32(dtCWFE.Rows[i]["Fk_PMRMCategory_Id"]);
                                    pro.Status = (dtCWFE.Rows[i]["Status"]).ToString();

                                    pro.EstimateName = lblEstimateName.Text;
                                    pro.Fk_Company_Id = Convert.ToInt32(lblCompany_Id.Text);
                                    pro.IsSelected = true;
                                    pro.PriceListName = CreatePriceListNametxt.Text;

                                    //if (TradeName_Id == "")
                                    //{
                                    //    pro.TradeName_Id = 0;
                                    //}
                                    //else
                                    //{
                                    //    pro.TradeName_Id = Convert.ToInt32(TradeName_Id);

                                    //}

                                    pro.Suggested_Price_List = Convert.ToDecimal(lblSuggestedPricetxt.Text);



                                    DataTable dtCheckForInsert = new DataTable();
                                    dtCheckForInsert = cls.CHECK_Estimate_PriceList(lblCompany_Id.Text, lblEstimateName.Text, pro.Status, pro.Fk_CWFE_Id);
                                    if (dtCheckForInsert.Rows.Count > 0)
                                    {
                                        status = cls.UPDATE_Suggested_CWFE(pro);
                                    }
                                    else
                                    {
                                        status = cls.INSERT_Suggested_PriceList(pro);

                                    }

                                }
                            }

                        }
                    }


                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted!')", true);
                Grid_EstimatePriceListData();
                ClearData();
            }


        }


        public void ClearData()
        {
            foreach (GridViewRow row1 in Grid_EstimatePricelist.Rows)
            {

                bool chkbox_Check = (row1.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                string SuggestedPricetxt = (row1.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = "0.00";
                SuggestedPricetxt = "0.00";
            }
            CreatePriceListNametxt.Text = "";
        }

        protected void Grid_EstimatePricelist_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void CreatePriceListName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void MasterPackingDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string EstimateName = "";
            int Company_Id = 0;
            if (Session["EstimateName"].ToString() == "")
            {

                Company_Id = Convert.ToInt32(OtherCompanyDropdown.SelectedValue);
            }
            else
            {
                EstimateName = Session["EstimateName"].ToString();
                Company_Id = Convert.ToInt32(Session["Company_Id"]);

            }
            if (MasterPackingDropdown.SelectedValue == "0")
            {
                //lblCompanyMasterList_Name.Text = Session["CompanyName"].ToString();



                DataTable dt = new DataTable();
                DataTable dtActualAllpack = new DataTable();
                ClsEstimatePriceList cls = new ClsEstimatePriceList();
                if (Session["EstimateName"].ToString() == "")
                {
                    if (Status == "Actual" || Status == "")
                    {
                        dt = cls.Get_EstimatePriceListByCompanyForActualAllPack(Company_Id, EstimateName);
                        Grid_EstimatePricelist.DataSource = dt;
                        Grid_EstimatePricelist.DataBind();
                        Grid_EstimatePricelist.Visible = true;

                    }

                }
                else
                {
                    EstimateName = Session["EstimateName"].ToString();
                    lblEstimateName.Text = EstimateName;
                    //dtActualAllpack = cls.Get_EstimatePriceListByCompanyForActualAllPack(Company_Id, EstimateName);
                    //Grid_EstimatePricelist.DataSource = dtActualAllpack;
                    //Grid_EstimatePricelist.DataBind();
                    DataTable dtNew = new DataTable();

                    dtNew = cls.Get_EstimatePriceListByCompanyForActualEstimateAllPack(Company_Id, EstimateName);
                    Grid_EstimatePricelistStatusWise.DataSource = dtNew;
                    Grid_EstimatePricelistStatusWise.DataBind();

                    if (true)
                    {
                        if (EstimatedPriceDropdown.SelectedValue == "1")
                        {
                            Grid_EstimatePricelistStatusWise.Visible = true;
                            Grid_EstimatePricelist.Visible = false;
                        }
                        else if (EstimatedPriceDropdown.SelectedValue == "2")
                        {
                            Grid_EstimatePricelistStatusWise.Visible = true;
                            Grid_EstimatePricelist.Visible = true;

                        }
                        else
                        {
                            Grid_EstimatePricelistStatusWise.Visible = true;
                            Grid_EstimatePricelist.Visible = false;
                        }
                    }
                }


                decimal SuggPackDiffPerLtr = 0;
                decimal TotalAmtPerUnitMaster = 0;
                decimal Suggested_Price = 0;
                decimal Last_Shared_Price = 0;
                foreach (GridViewRow row2 in Grid_EstimatePricelist.Rows)
                {
                    DataRowView data = (DataRowView)row2.DataItem;

                    lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;
                    Suggested_Price = (Convert.ToDecimal((row2.FindControl("lblSuggested_Price") as Label).Text));

                    if (lblIsMasterPacking.Text == "True")
                    {
                        row2.BackColor = System.Drawing.Color.Cornsilk;
                        row2.Font.Bold = true;

                    }
                    Last_Shared_Price = (Convert.ToDecimal((row2.FindControl("lblLast_Shared_Price") as Label).Text));
                    string Last_Shared_Pricetxt = Last_Shared_Price.ToString();
                    if (Last_Shared_Pricetxt != "0.00")
                    {
                        SuggPackDiffPerLtr = (Convert.ToDecimal((row2.FindControl("lblSuggest_Pack_Diff_Ltr") as Label).Text));
                        string SuggPackDiffPer = SuggPackDiffPerLtr.ToString();

                        TotalAmtPerUnitMaster = (Convert.ToDecimal((row2.FindControl("lblEstimateTotalAmtPerLtr") as Label).Text));
                        (row2.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((Suggested_Price) + (SuggPackDiffPerLtr)).ToString("0.00");

                    }
                    else
                    {
                        (row2.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = "0.00";
                    }
                    if (lblIsMasterPacking.Text == "True")
                    {
                        (row2.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Enabled = true;
                        (row2.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Enabled = true;

                    }
                    else
                    {
                        (row2.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Enabled = false;

                    }
                }
                foreach (GridViewRow row2 in Grid_EstimatePricelist.Rows)
                {
                    DataRowView data = (DataRowView)row2.DataItem;
                    TextBox txtType = (TextBox)row2.FindControl("Suggest_Pack_Diff_Ltrtxt");
                    lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;

                    if (lblIsMasterPacking.Text == "True")
                    {
                        txtType.Enabled = true;

                        row2.BackColor = System.Drawing.Color.Cornsilk;
                        row2.Font.Bold = true;
                    }
                    else
                    {
                        txtType.Enabled = false;

                    }
                }
                foreach (GridViewRow row2 in Grid_EstimatePricelistStatusWise.Rows)
                {
                    DataRowView data = (DataRowView)row2.DataItem;
                    TextBox txtType = (TextBox)row2.FindControl("Suggest_Pack_Diff_Ltrtxt");
                    lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;

                    if (lblIsMasterPacking.Text == "True")
                    {
                        txtType.Enabled = true;

                        row2.BackColor = System.Drawing.Color.Cornsilk;
                        row2.Font.Bold = true;
                    }
                    else
                    {
                        txtType.Enabled = false;
                    }
                }
            }

            else if (MasterPackingDropdown.SelectedValue == "1" && Session["EstimateName"].ToString() == "")
            {
                DataTable dtActual = new DataTable();
                ClsEstimatePriceList cls = new ClsEstimatePriceList();

                dtActual = cls.Get_EstimatePriceListByCompanyForActualOnly(Company_Id);
                Grid_EstimatePricelist.DataSource = dtActual;
                Grid_EstimatePricelist.DataBind();
            }

            else
            {
                Grid_EstimatePriceListData();
            }
        }

        protected void SuggestedPricetxt_TextChanged(object sender, EventArgs e)
        {
            decimal SuggPackDiffPerLtr = 0;
            decimal TotalAmtPerUnitMaster = 0;
            decimal Last_Shared_Price = 0;
            decimal Suggested_Price = 0;
            decimal FixedSuggest_Pack = 0;

            if (MasterPackingDropdown.SelectedValue == "0")
            {
                foreach (GridViewRow row in Grid_EstimatePricelist.Rows)
                {
                    SuggPackDiffPerLtr = (Convert.ToDecimal((row.FindControl("lblSuggest_Pack_Diff_Ltr") as Label).Text));
                    Last_Shared_Price = (Convert.ToDecimal((row.FindControl("lblLast_Shared_Price") as Label).Text));
                    TotalAmtPerUnitMaster = (Convert.ToDecimal((row.FindControl("lblEstimateTotalAmtPerLtr") as Label).Text));
                    //Suggested_Price = (Convert.ToDecimal((row.FindControl("lblSuggested_Price") as Label).Text));
                    lblIsMasterPacking.Text = (row.FindControl("lblIsMasterPacking") as Label).Text;
                    string BPM_Id = (row.FindControl("lblFk_BPM_Id") as Label).Text;
                    string PMRMCategory_Id = (row.FindControl("lblFk_PMRMCategory_Id") as Label).Text;

                    //string SuggPackDiffPer = Last_Shared_Price.ToString();

                    Last_Shared_Price = (Convert.ToDecimal((row.FindControl("lblLast_Shared_Price") as Label).Text));
                    string SuggPackDiffPer = SuggPackDiffPerLtr.ToString();
                    string Last_Shared_Pricetxt = Last_Shared_Price.ToString();
                    Suggested_Price = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                    string lblstatus = (row.FindControl("lblStatus") as Label).Text;
                    if (lblIsMasterPacking.Text == "True")
                    {

                        if (Suggested_Price != 0)
                        {
                            if (lblstatus == "Estimate")
                            {
                                Suggested_Price = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((FixedSuggest_Pack) + (SuggPackDiffPerLtr)).ToString("0.00");
                                FixedSuggest_Pack = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                lbl_BPM_Id.Text = BPM_Id.ToString();
                                lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                            }
                            else
                            {
                                Suggested_Price = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((Suggested_Price) + (SuggPackDiffPerLtr)).ToString("0.00");
                                FixedSuggest_Pack = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                lbl_BPM_Id.Text = BPM_Id.ToString();
                                lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                            }


                        }
                        else
                        {
                            if (BPM_Id == lbl_BPM_Id.Text && PMRMCategory_Id == lblFk_PMRM_Catgeory_Id.Text)
                            {

                                (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((FixedSuggest_Pack) + (SuggPackDiffPerLtr)).ToString("0.00");
                                lbl_BPM_Id.Text = BPM_Id.ToString();
                                lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                            }

                        }

                    }
                    else if (lblIsMasterPacking.Text == "False" && BPM_Id == lbl_BPM_Id.Text && PMRMCategory_Id == lblFk_PMRM_Catgeory_Id.Text)
                    {
                        (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((FixedSuggest_Pack) + (SuggPackDiffPerLtr)).ToString("0.00");
                        lbl_BPM_Id.Text = BPM_Id.ToString();
                        lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                    }


                    else
                    {
                        (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = "0.00";

                    }

                }

                foreach (GridViewRow row in Grid_EstimatePricelistStatusWise.Rows)
                {
                    SuggPackDiffPerLtr = (Convert.ToDecimal((row.FindControl("lblSuggest_Pack_Diff_Ltr") as Label).Text));
                    Last_Shared_Price = (Convert.ToDecimal((row.FindControl("lblLast_Shared_Price") as Label).Text));
                    TotalAmtPerUnitMaster = (Convert.ToDecimal((row.FindControl("lblEstimateTotalAmtPerLtr") as Label).Text));
                    //Suggested_Price = (Convert.ToDecimal((row.FindControl("lblSuggested_Price") as Label).Text));
                    lblIsMasterPacking.Text = (row.FindControl("lblIsMasterPacking") as Label).Text;
                    string BPM_Id = (row.FindControl("lblFk_BPM_Id") as Label).Text;
                    string PMRMCategory_Id = (row.FindControl("lblFk_PMRMCategory_Id") as Label).Text;

                    //string SuggPackDiffPer = Last_Shared_Price.ToString();

                    Last_Shared_Price = (Convert.ToDecimal((row.FindControl("lblLast_Shared_Price") as Label).Text));
                    string SuggPackDiffPer = SuggPackDiffPerLtr.ToString();
                    string Last_Shared_Pricetxt = Last_Shared_Price.ToString();
                    Suggested_Price = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                    string lblstatus = (row.FindControl("lblStatus") as Label).Text;
                    if (lblIsMasterPacking.Text == "True")
                    {

                        if (Suggested_Price != 0)
                        {
                            if (lblstatus == "Estimate")
                            {
                                Suggested_Price = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((FixedSuggest_Pack) + (SuggPackDiffPerLtr)).ToString("0.00");
                                FixedSuggest_Pack = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                lbl_BPM_Id.Text = BPM_Id.ToString();
                                lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                            }
                            else
                            {
                                Suggested_Price = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((Suggested_Price) + (SuggPackDiffPerLtr)).ToString("0.00");
                                FixedSuggest_Pack = Convert.ToDecimal((row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text);
                                lbl_BPM_Id.Text = BPM_Id.ToString();
                                lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                            }


                        }
                        else
                        {
                            if (BPM_Id == lbl_BPM_Id.Text && PMRMCategory_Id == lblFk_PMRM_Catgeory_Id.Text)
                            {

                                (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((FixedSuggest_Pack) + (SuggPackDiffPerLtr)).ToString("0.00");
                                lbl_BPM_Id.Text = BPM_Id.ToString();
                                lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                            }

                        }

                    }
                    else if (lblIsMasterPacking.Text == "False" && BPM_Id == lbl_BPM_Id.Text && PMRMCategory_Id == lblFk_PMRM_Catgeory_Id.Text)
                    {
                        (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = ((FixedSuggest_Pack) + (SuggPackDiffPerLtr)).ToString("0.00");
                        lbl_BPM_Id.Text = BPM_Id.ToString();
                        lblFk_PMRM_Catgeory_Id.Text = PMRMCategory_Id.ToString();
                    }


                    else
                    {
                        (row.FindControl("Suggest_Pack_Diff_Ltrtxt") as TextBox).Text = "0.00";

                    }

                }
            }

        }

        protected void Grid_EstimatePricelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = Grid_EstimatePricelist.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = Grid_EstimatePricelist.Rows[i];
                GridViewRow previousRow = Grid_EstimatePricelist.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    {
                        if (previousRow.Cells[1].RowSpan == 0)
                        {
                            if (row.Cells[1].RowSpan == 0)
                            {
                                previousRow.Cells[1].RowSpan += 2;
                                previousRow.Cells[9].RowSpan += 2;
                                previousRow.Cells[10].RowSpan += 2;
                                previousRow.Cells[11].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[1].RowSpan = row.Cells[1].RowSpan + 1;
                                previousRow.Cells[9].RowSpan = row.Cells[9].RowSpan + 1;
                                previousRow.Cells[10].RowSpan = row.Cells[9].RowSpan + 1;
                                previousRow.Cells[11].RowSpan = row.Cells[9].RowSpan + 1;
                            }
                            row.Cells[1].Visible = false;
                            row.Cells[9].Visible = false;
                            row.Cells[10].Visible = false;
                            row.Cells[11].Visible = false;
                        }
                    }
                }

            }

        }

        protected void Grid_EstimatePricelistStatusWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                Label lblid = (Label)e.Row.FindControl("lblid");

                Label lbbpm = (Label)e.Row.FindControl("lbbpm");

                Label rown_no = (Label)e.Row.FindControl("rown");


                TextBox BulkCosttxt = (TextBox)e.Row.FindControl("BulkCosttxt");
                //if (lblStatus.Text.ToUpper() == "ESTIMATE")
               // {
                    datajs += "calculatePricelist('" + BulkCosttxt.ClientID + "');";
                //
                //}


               


                if (e.Row.RowIndex > 0)
                {
                    GridViewRow row=e.Row;
                    GridViewRow previousRow= Grid_EstimatePricelistStatusWise.Rows[e.Row.RowIndex-1];


                    if (Convert.ToInt32(lbbpm.Text) != tempbpm)
                    {
                        tid = tid + 1;
                        lblid.Text = tid.ToString();
                        tempbpm = Convert.ToInt32(lbbpm.Text);
                        if(tid%2==0)
                        {
                            row.Attributes.Add("class", "rowstylealter");
                        }
                        else
                        {
                            row.Attributes.Add("class", "rowstyle");
                        }
                    }
                    
                    if (row.Cells[2].Text == previousRow.Cells[2].Text)
                    {
                        if (previousRow.Cells[2].RowSpan == 0)
                        {
                            if (row.Cells[2].RowSpan == 0)
                            {
                                previousRow.Cells[0].RowSpan += 2;
                                previousRow.Cells[2].RowSpan += 2;
                                previousRow.Cells[3].RowSpan += 2;
                                previousRow.Cells[28].RowSpan += 2;

                                previousRow.Attributes.Add("class", "rowstylemerge1");
                                row.Attributes.Add("class", "rowstylemerge2");

                            }
                            else
                            {
                                previousRow.Cells[0].RowSpan = row.Cells[0].RowSpan + 1;
                                previousRow.Cells[2].RowSpan = row.Cells[2].RowSpan + 1;
                                previousRow.Cells[3].RowSpan = row.Cells[2].RowSpan + 1;                              
                                previousRow.Cells[28].RowSpan = row.Cells[28].RowSpan + 1;
                               

                            }
                           

                            row.Cells[0].Visible = false;
                            row.Cells[2].Visible = false;
                            row.Cells[3].Visible = false;                           
                            row.Cells[28].Visible = false;

                           

                        }
                    }
                    else
                    {
                        
                    }
                    
                }

                GridViewRow row2 = e.Row;

                if ((row2.FindControl("lblStatus") as Label).Text == "Actual")
                {


                    (row2.FindControl("BulkCosttxt") as TextBox).Enabled = false;
                    (row2.FindControl("BulkInterestPercenttxt") as TextBox).Enabled = false;

                    (row2.FindControl("lbIntrest_Amount") as Label).Enabled = false;
                    (row2.FindControl("TotalBulkCosttxt") as TextBox).Enabled = false;
                    (row2.FindControl("BulkCostPerUnittxt") as TextBox).Enabled = false;
                    (row2.FindControl("PMtxt") as TextBox).Enabled = false;
                    (row2.FindControl("PM_Additional_Buffertxt") as TextBox).Enabled = false;
                    (row2.FindControl("Labourtxt") as TextBox).Enabled = false;
                    (row2.FindControl("Labour_Additional_Buffertxt") as TextBox).Enabled = false;
                    (row2.FindControl("SubTotalAtxt") as TextBox).Enabled = false;
                    (row2.FindControl("lblLossAmt") as Label).Enabled = false;
                    (row2.FindControl("PackLoss_Percenttxt") as TextBox).Enabled = false;
                    (row2.FindControl("TotalBtxt") as TextBox).Enabled = false;
                    (row2.FindControl("MarketedByCharge_Amttxt") as Label).Enabled = false;
                    (row2.FindControl("MarketedByChargesPertxt") as TextBox).Enabled = false;
                    (row2.FindControl("FactoryExpence_Amttxt") as Label).Enabled = false;
                    (row2.FindControl("FactoryExpence_Pertxt") as TextBox).Enabled = false;
                    (row2.FindControl("Other_Amttxt") as Label).Enabled = false;
                    (row2.FindControl("Other_Pertxt") as TextBox).Enabled = false;
                    (row2.FindControl("Profit_Amttxt") as Label).Enabled = false;
                    (row2.FindControl("Profit_Pertxt") as TextBox).Enabled = false;
                    (row2.FindControl("Totaltxt") as TextBox).Enabled = false;

                    //GridViewRow NextRow = Grid_EstimatePricelistStatusWise.Rows[row2.RowIndex ];
                    (row2.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Enabled = false;

                    (row2.FindControl("FinalPricePerUnittxt") as TextBox).Enabled = false;
                    (row2.FindControl("FinalProfit_Amttxt") as Label).Enabled = false;
                    (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Enabled = false;
                    (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Enabled = false;
                    (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Enabled = false;
                    (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Enabled = false;

                    
                                       
                    (row2.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Visible = true;


                    Label rown = (Label) row2.FindControl("rown");
                    if (rown.Text.ToUpper() == "1")
                    {


                        (row2.FindControl("FinalPricePerUnittxt") as TextBox).Style.Add("display", "none");
                        (row2.FindControl("FinalProfit_Amttxt") as Label).Style.Add("display", "none");
                        (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Style.Add("display", "none");
                        (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Style.Add("display", "none");
                        (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Style.Add("display", "none");
                        (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Style.Add("display", "none");
                    }
                    else
                    {
                        (row2.FindControl("FinalPricePerUnittxt") as TextBox).Style.Add("display", "");
                        (row2.FindControl("FinalProfit_Amttxt") as Label).Style.Add("display", "");
                        (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Style.Add("display", "");
                        (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Style.Add("display", "");
                        (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Style.Add("display", "");
                        (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Style.Add("display", "");
                    }

                }
                if ((row2.FindControl("lblStatus") as Label).Text == "Estimate")
                {
                    (row2.FindControl("PMtxt") as TextBox).Enabled = false;
                    (row2.FindControl("Labourtxt") as TextBox).Enabled = false;
                    (row2.FindControl("PackLoss_Percenttxt") as TextBox).Enabled = false;

                    (row2.FindControl("TotalBulkCosttxt") as TextBox).Enabled = false;
                    (row2.FindControl("BulkCostPerUnittxt") as TextBox).Enabled = false;
                    (row2.FindControl("SubTotalAtxt") as TextBox).Enabled = false;
                    (row2.FindControl("TotalBtxt") as TextBox).Enabled = false;
                    (row2.FindControl("Totaltxt") as TextBox).Enabled = false;
                    (row2.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Enabled = false;
                    (row2.FindControl("FinalProfit_Amttxt") as Label).Enabled = true;
                    (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Enabled = false;
                    (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Enabled = false;
                    (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Enabled = true;
                    (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Enabled = false;                   


                }


            }
           

            /*
            for (int i = Grid_EstimatePricelistStatusWise.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = Grid_EstimatePricelistStatusWise.Rows[i];
                GridViewRow previousRow = Grid_EstimatePricelistStatusWise.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    {
                        if (previousRow.Cells[1].RowSpan == 0)
                        {
                            if (row.Cells[1].RowSpan == 0)
                            {
                                previousRow.Cells[1].RowSpan += 2;
                                previousRow.Cells[2].RowSpan += 2;

                                //previousRow.Cells[20].RowSpan += 2;
                                //previousRow.Cells[21].RowSpan += 2;
                                //previousRow.Cells[22].RowSpan += 2;
                                //previousRow.Cells[23].RowSpan += 2;
                                //previousRow.Cells[24].RowSpan += 2;
                                //previousRow.Cells[25].RowSpan += 2;

                                previousRow.Cells[27].RowSpan += 2;
                                //previousRow.Cells[11].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[1].RowSpan = row.Cells[1].RowSpan + 1;
                                previousRow.Cells[2].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[20].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[21].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[22].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[23].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[24].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[25].RowSpan = row.Cells[1].RowSpan + 1;

                                previousRow.Cells[27].RowSpan = row.Cells[1].RowSpan + 1;
                                //previousRow.Cells[11].RowSpan = row.Cells[9].RowSpan + 1;
                            }
                            row.Cells[1].Visible = false;
                            row.Cells[2].Visible = false;
                            //row.Cells[20].Visible = false;
                            //row.Cells[21].Visible = false;
                            //row.Cells[22].Visible = false;
                            //row.Cells[23].Visible = false;
                            //row.Cells[24].Visible = false;
                            //row.Cells[25].Visible = false;

                            row.Cells[27].Visible = false;
                            //row.Cells[11].Visible = false;
                        }
                    }
                    foreach (GridViewRow row2 in Grid_EstimatePricelistStatusWise.Rows)
                    {

                        if ((row2.FindControl("lblStatus") as Label).Text == "Actual")
                        {


                            (row2.FindControl("BulkCosttxt") as TextBox).Enabled = false;
                            (row2.FindControl("BulkInterestPercenttxt") as TextBox).Enabled = false;

                            (row2.FindControl("lbIntrest_Amount") as Label).Enabled = false;
                            (row2.FindControl("TotalBulkCosttxt") as TextBox).Enabled = false;
                            (row2.FindControl("BulkCostPerUnittxt") as TextBox).Enabled = false;
                            (row2.FindControl("PMtxt") as TextBox).Enabled = false;
                            (row2.FindControl("PM_Additional_Buffertxt") as TextBox).Enabled = false;
                            (row2.FindControl("Labourtxt") as TextBox).Enabled = false;
                            (row2.FindControl("Labour_Additional_Buffertxt") as TextBox).Enabled = false;
                            (row2.FindControl("SubTotalAtxt") as TextBox).Enabled = false;
                            (row2.FindControl("lblLossAmt") as Label).Enabled = false;
                            (row2.FindControl("PackLoss_Percenttxt") as TextBox).Enabled = false;
                            (row2.FindControl("TotalBtxt") as TextBox).Enabled = false;
                            (row2.FindControl("MarketedByCharge_Amttxt") as Label).Enabled = false;
                            (row2.FindControl("MarketedByChargesPertxt") as TextBox).Enabled = false;
                            (row2.FindControl("FactoryExpence_Amttxt") as Label).Enabled = false;
                            (row2.FindControl("FactoryExpence_Pertxt") as TextBox).Enabled = false;
                            (row2.FindControl("Other_Amttxt") as Label).Enabled = false;
                            (row2.FindControl("Other_Pertxt") as TextBox).Enabled = false;
                            (row2.FindControl("Profit_Amttxt") as Label).Enabled = false;
                            (row2.FindControl("Profit_Pertxt") as TextBox).Enabled = false;
                            (row2.FindControl("Totaltxt") as TextBox).Enabled = false;

                            //GridViewRow NextRow = Grid_EstimatePricelistStatusWise.Rows[row2.RowIndex ];
                            (row2.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Enabled = false;

                            (row2.FindControl("FinalPricePerUnittxt") as TextBox).Enabled = false;
                            (row2.FindControl("FinalProfit_Amttxt") as Label).Enabled = false;
                            (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Enabled = false;
                            (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Enabled = false;
                            (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Enabled = false;
                            (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Enabled = false;

                            (row2.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Visible = false;
                            (row2.FindControl("FinalPricePerUnittxt") as TextBox).Visible = false;
                            (row2.FindControl("FinalProfit_Amttxt") as Label).Visible = false;
                            (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Visible = false;
                            (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Visible = false;
                            (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Visible = false;
                            (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Visible = false;

                        }
                        if ((row2.FindControl("lblStatus") as Label).Text == "Estimate")
                        {
                            (row2.FindControl("PMtxt") as TextBox).Enabled = false;
                            (row2.FindControl("Labourtxt") as TextBox).Enabled = false;
                            (row2.FindControl("PackLoss_Percenttxt") as TextBox).Enabled = false;

                            (row2.FindControl("TotalBulkCosttxt") as TextBox).Enabled = false;
                            (row2.FindControl("BulkCostPerUnittxt") as TextBox).Enabled = false;
                            (row2.FindControl("SubTotalAtxt") as TextBox).Enabled = false;
                            (row2.FindControl("TotalBtxt") as TextBox).Enabled = false;
                            (row2.FindControl("Totaltxt") as TextBox).Enabled = false;
                            (row2.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Enabled = false;
                            (row2.FindControl("FinalProfit_Amttxt") as Label).Enabled = true;
                            (row2.FindControl("FinalProfit_Percenttxt") as TextBox).Enabled = false;
                            (row2.FindControl("FinalPriceLtrKgtxt") as TextBox).Enabled = false;
                            (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Enabled = true;
                            (row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Enabled = false;
                        }

                    }

                }

            }*/
        }

        protected void EstimatedPriceDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstimatedPriceDropdown.SelectedValue == "1")
            {
                Grid_EstimatePricelistStatusWise.Visible = true;
                Grid_EstimatePricelist.Visible = false;
            }
            else if (EstimatedPriceDropdown.SelectedValue == "2")
            {
                Grid_EstimatePricelistStatusWise.Visible = true;
                Grid_EstimatePricelist.Visible = true;

            }
            else
            {
                Grid_EstimatePricelistStatusWise.Visible = false;
                Grid_EstimatePricelist.Visible = true;
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
            if (EstimatedPriceDropdown.SelectedValue == "1")
            {


                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    foreach (GridViewRow row in Grid_EstimatePricelistStatusWise.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            //Hide the Row if CheckBox is not checked
                            //row.Visible = (row.FindControl("CheckBox_Check") as CheckBox).Checked;
                        }
                    }
                    foreach (GridViewRow row in Grid_EstimatePricelistStatusWise.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.BackColor = Grid_EstimatePricelistStatusWise.RowStyle.BackColor;
                            List<Control> controls = new List<Control>();

                            //Add controls to be removed to Generic List
                            foreach (Control control in cell.Controls)
                            {
                                controls.Add(control);
                            }

                            //Loop through the controls to be removed and replace then with Literal
                            foreach (Control control in controls)
                            {
                                switch (control.GetType().Name)
                                {

                                    case "TextBox":
                                        cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                        break;
                                    case "Label":
                                        cell.Controls.Add(new Literal { Text = (control as Label).Text });
                                        break;
                                }
                                cell.Controls.Remove(control);
                            }
                        }
                    }

                    Grid_EstimatePricelistStatusWise.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PriceListGPEstimate [" + DateTime.Now.ToShortDateString() + "].pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }

        }

        protected void OtherCompanyDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Status == "Actual" || Status == "")
            {

                EstimatedPriceDropdown.SelectedValue = "0";
                EstimatedPriceDropdown.Enabled = false;
            }
            else
            {
                MasterPackingDropdown.Enabled = true;
                EstimatedPriceDropdown.Enabled = true;
                OtherCompanyDropdown.Enabled = false;
            }

            int Company_Id = Convert.ToInt32(OtherCompanyDropdown.SelectedValue);
            DataTable dt = new DataTable();
            DataTable dtActialEstimate = new DataTable();
            ClsEstimatePriceList cls = new ClsEstimatePriceList();
            MasterPackingDropdown.SelectedValue = "1";
            dt = cls.Get_EstimatePriceListByCompanyForActualOnly(Company_Id);
            lblCompanyMasterList_Name.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();
            Grid_EstimatePricelist.DataSource = dt;
            Grid_EstimatePricelist.DataBind();
            Grid_EstimatePricelist.Visible = true;
        }

        protected void BulkCosttxt_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Bulk_Interest_Percenttxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {

                (gvr.FindControl("lbIntrest_Amount") as Label).Text = (Convert.ToDecimal((gvr.FindControl("BulkCosttxt") as TextBox).Text) * Convert.ToDecimal((gvr.FindControl("BulkInterestPercenttxt") as TextBox).Text) / 100).ToString("0.00");

                (gvr.FindControl("TotalBulkCosttxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("BulkCosttxt") as TextBox).Text) + Convert.ToDecimal((gvr.FindControl("lbIntrest_Amount") as Label).Text)).ToString("0.00");


                if (Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text) < 1000 && (((gvr.FindControl("lblPack_Measurement") as Label).Text) == "6" || ((gvr.FindControl("lblPack_Measurement") as Label).Text) == "7"))
                {
                    (gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("TotalBulkCosttxt") as TextBox).Text) / (1000 / Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("SubTotalAtxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");

                }
                else if (Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text) > 1 && (((gvr.FindControl("lblPack_Measurement") as Label).Text) == "1" || ((gvr.FindControl("lblPack_Measurement") as Label).Text) == "2"))
                {
                    (gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("TotalBulkCosttxt") as TextBox).Text) / Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text)).ToString("0.00");
                    (gvr.FindControl("SubTotalAtxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");

                }
                else
                {
                    (gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("TotalBulkCosttxt") as TextBox).Text)).ToString("0.00");
                    (gvr.FindControl("SubTotalAtxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");

                }

            }

        }

        protected void TotalBulkCosttxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void PM_Additional_Buffertxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("lblTotalPM") as Label).Text = ((Convert.ToDecimal((gvr.FindControl("PMtxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("PM_Additional_Buffertxt") as TextBox).Text))).ToString("0.00");
                (gvr.FindControl("SubTotalAtxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");
                (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");

            }
        }

        protected void PMtxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("lblTotalPM") as Label).Text = ((Convert.ToDecimal((gvr.FindControl("PMtxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("PM_Additional_Buffertxt") as TextBox).Text))).ToString("0.00");
                (gvr.FindControl("SubTotalAtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");
                (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");

            }
        }

        protected void BulkCostPerUnittxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Labour_Additional_Buffertxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("lblTotalLabour") as Label).Text = ((Convert.ToDecimal((gvr.FindControl("Labourtxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("Labour_Additional_Buffertxt") as TextBox).Text))).ToString("0.00");
                (gvr.FindControl("SubTotalAtxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");

            }
        }

        protected void Labourtxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("lblTotalLabour") as Label).Text = ((Convert.ToDecimal((gvr.FindControl("Labourtxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("Labour_Additional_Buffertxt") as TextBox).Text))).ToString("0.00");
                (gvr.FindControl("SubTotalAtxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("BulkCostPerUnittxt") as TextBox).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalPM") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("lblTotalLabour") as Label).Text))).ToString("0.00");
                (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");


            }
        }

        protected void PackLoss_Percenttxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("lblLossAmt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("PackLoss_Percenttxt") as TextBox).Text) * Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text) / 100).ToString("0.00");
                (gvr.FindControl("TotalBtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("lblLossAmt") as Label).Text) + Convert.ToDecimal((gvr.FindControl("SubTotalAtxt") as TextBox).Text)).ToString("0.00");
            }
        }
        protected void TotalBtxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void LossAmttxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void MarketedByChargesPertxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {

                (gvr.FindControl("MarketedByCharge_Amttxt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("MarketedByChargesPertxt") as TextBox).Text) * (Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)) / 100).ToString("0.00");
                (gvr.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("Totaltxt") as TextBox).Text) + (Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)))).ToString("0.00");
                (gvr.FindControl("Totaltxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("MarketedByCharge_Amttxt") as Label).Text) + (Convert.ToDecimal((gvr.FindControl("FactoryExpence_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Other_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Profit_Amttxt") as Label).Text))).ToString("0.00");

            }
        }

        protected void FactoryExpence_Pertxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {


                (gvr.FindControl("FactoryExpence_Amttxt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("FactoryExpence_Pertxt") as TextBox).Text) * Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text) / 100).ToString("0.00");
                (gvr.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("Totaltxt") as TextBox).Text) + (Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)))).ToString("0.00");

                (gvr.FindControl("Totaltxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("MarketedByCharge_Amttxt") as Label).Text) + (Convert.ToDecimal((gvr.FindControl("FactoryExpence_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Other_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Profit_Amttxt") as Label).Text))).ToString("0.00");

            }
        }

        protected void Other_Pertxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("Other_Amttxt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("Other_Pertxt") as TextBox).Text) * Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text) / 100).ToString("0.00");

                (gvr.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("Totaltxt") as TextBox).Text) + (Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)))).ToString("0.00");

                (gvr.FindControl("Totaltxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("MarketedByCharge_Amttxt") as Label).Text) + (Convert.ToDecimal((gvr.FindControl("FactoryExpence_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Other_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Profit_Amttxt") as Label).Text))).ToString("0.00");

            }
        }

        protected void Profit_Pertxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("Profit_Amttxt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("Profit_Pertxt") as TextBox).Text) * Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text) / 100).ToString("0.00");
                (gvr.FindControl("SuggestedFinalTotalCosttxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("Totaltxt") as TextBox).Text) + (Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)))).ToString("0.00");

                (gvr.FindControl("Totaltxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("MarketedByCharge_Amttxt") as Label).Text) + (Convert.ToDecimal((gvr.FindControl("FactoryExpence_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Other_Amttxt") as Label).Text)) + (Convert.ToDecimal((gvr.FindControl("Profit_Amttxt") as Label).Text))).ToString("0.00");

            }
        }

        protected void FinalPricePerUnittxt_TextChanged(object sender, EventArgs e)
        {
            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            {
                (gvr.FindControl("FinalProfit_Amttxt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text) - Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)).ToString("0.00");
                (gvr.FindControl("FinalProfit_Percenttxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("FinalProfit_Amttxt") as Label).Text) * (100)) / Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text)).ToString("0.00");
                decimal FinalProfit_Amt = Convert.ToDecimal((gvr.FindControl("FinalProfit_Amttxt") as Label).Text);


                if (Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text) < 1000 && (((gvr.FindControl("lblPack_Measurement") as Label).Text) == "6" || ((gvr.FindControl("lblPack_Measurement") as Label).Text) == "7"))
                {
                    (gvr.FindControl("FinalPriceLtrKgtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text) * (1000 / Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Text = (FinalProfit_Amt * (1000 / Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Text) * (100)) / Convert.ToDecimal((gvr.FindControl("FinalPriceLtrKgtxt") as TextBox).Text)).ToString("0.00");
                }
                else if (Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text) > 1 && (((gvr.FindControl("lblPack_Measurement") as Label).Text) == "1" || ((gvr.FindControl("lblPack_Measurement") as Label).Text) == "2"))
                {
                    (gvr.FindControl("FinalPriceLtrKgtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text) / Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text)).ToString("0.00");
                    (gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Text = (FinalProfit_Amt / (Convert.ToDecimal((gvr.FindControl("lblPack_Size") as Label).Text))).ToString("0.00");
                    (gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Text) * (100)) / Convert.ToDecimal((gvr.FindControl("FinalPriceLtrKgtxt") as TextBox).Text)).ToString("0.00");

                }
                else
                {
                    (gvr.FindControl("FinalPriceLtrKgtxt") as TextBox).Text = (Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text)).ToString("0.00");
                    (gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Text = (FinalProfit_Amt).ToString("0.00");
                    (gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt") as Label).Text) * (100)) / Convert.ToDecimal((gvr.FindControl("FinalPriceLtrKgtxt") as TextBox).Text)).ToString("0.00");

                }
            }
        }

        protected void FinalProfit_Percenttxt_TextChanged(object sender, EventArgs e)
        {
            //TextBox btn = (TextBox)sender;
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //if ((gvr.FindControl("lblStatus") as Label).Text == "Estimate")
            //{
            //    (gvr.FindControl("FinalProfit_Amttxt") as Label).Text = (Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text) - Convert.ToDecimal((gvr.FindControl("TotalBtxt") as TextBox).Text)).ToString("0.00");
            //    (gvr.FindControl("FinalProfit_Percenttxt") as TextBox).Text = ((Convert.ToDecimal((gvr.FindControl("FinalProfit_Amttxt") as Label).Text) * (100)) / Convert.ToDecimal((gvr.FindControl("FinalPricePerUnittxt") as TextBox).Text)).ToString("0.00");

            //}
        }

        protected void AddOtherCompanyPriceEstimate_Click(object sender, EventArgs e)
        {
            ProOtherCompanyPriceList pro = new ProOtherCompanyPriceList();
            ClsOtherPriceListMaster cls = new ClsOtherPriceListMaster();
            int status = 0;
            DataTable dt = new DataTable();
            DataTable dtCheckForInsert = new DataTable();
            foreach (GridViewRow row2 in Grid_EstimatePricelistStatusWise.Rows)
            {

                pro.Status = (row2.FindControl("lblStatus") as Label).Text;
                pro.Company_Id = Convert.ToInt32(lblCompany_Id.Text);
                pro.BPM_Id = Convert.ToInt32((row2.FindControl("lblBPM_Id") as Label).Text);
                pro.Pack_Size = Convert.ToDecimal((row2.FindControl("lblPack_Size") as Label).Text);
                pro.Pack_Measurement = Convert.ToInt32((row2.FindControl("lblPack_Measurement") as Label).Text);
                pro.PM_RM_Category_Id = Convert.ToInt32((row2.FindControl("lblPMRM_Category_Id") as Label).Text);
                pro.BulkCost = Convert.ToDecimal((row2.FindControl("BulkCosttxt") as TextBox).Text);

                pro.Bulk_Interest_Percent = Convert.ToDecimal((row2.FindControl("BulkInterestPercenttxt") as TextBox).Text);
                pro.PM_Additional_Buffer = Convert.ToDecimal((row2.FindControl("PM_Additional_Buffertxt") as TextBox).Text);
                pro.Labour_Additional_Buffer = Convert.ToDecimal((row2.FindControl("Labour_Additional_Buffertxt") as TextBox).Text);
                pro.PackLoss_Percent = Convert.ToDecimal((row2.FindControl("PackLoss_Percenttxt") as TextBox).Text);
                pro.MarketByCharge_Percent = Convert.ToDecimal((row2.FindControl("MarketedByChargesPertxt") as TextBox).Text);
                pro.FactoryExpence_Percent = Convert.ToDecimal((row2.FindControl("FactoryExpence_Pertxt") as TextBox).Text);
                pro.Other_Percent = Convert.ToDecimal((row2.FindControl("Other_Pertxt") as TextBox).Text);
                pro.Profit_Percent = Convert.ToDecimal((row2.FindControl("Profit_Pertxt") as TextBox).Text);
                pro.FinalPricePerUnit = Convert.ToDecimal((row2.FindControl("FinalPricePerUnittxt") as TextBox).Text);
                pro.EstimateName = lblEstimateName.Text;
                pro.OtherComapnyPriceList_ID = "NULL";

                //Code added By Harshul Patel on 09-05-2022 to add new columns in table


                pro.PM = Convert.ToDecimal((row2.FindControl("PMtxt") as TextBox).Text);
                pro.Labour = Convert.ToDecimal((row2.FindControl("Labourtxt") as TextBox).Text);


                pro.TotalBulkCost = Convert.ToDecimal((row2.FindControl("TotalBulkCosttxt_lbl") as TextBox).Text);
                pro.BulkCostPerUnit = Convert.ToDecimal((row2.FindControl("BulkCostPerUnittxt_lbl") as TextBox).Text);
                pro.Bulk_Interest_Amt = Convert.ToDecimal((row2.FindControl("lbIntrest_Amount_lbl") as TextBox).Text);               
                pro.SubTotal_A = Convert.ToDecimal((row2.FindControl("SubTotalAtxt_lbl") as TextBox).Text);
                pro.SubTotal_B = Convert.ToDecimal((row2.FindControl("TotalBtxt_lbl") as TextBox).Text);
                pro.FinalProfit_Amt = Convert.ToDecimal((row2.FindControl("FinalProfit_Amttxt_lbl") as TextBox).Text); 
                pro.FinalPrice_ltrKg = Convert.ToDecimal((row2.FindControl("FinalPriceLtrKgtxt_lbl") as TextBox).Text);
                pro.FinalProfit_Amt_ltrKg = Convert.ToDecimal((row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Amttxt_lbl") as TextBox).Text);
                pro.FinalProfit_Amt_ltrKg_Percentage = Convert.ToDecimal((row2.FindControl("FinalProfitAmount_Percent_Ltr_Kg_Pertxt_lbl") as TextBox).Text);

                /*
                pro.bulkcost_per_amt = Convert.ToDecimal((row2.FindControl("lbIntrest_Amount_lbl") as TextBox).Text);
                pro.loss_per_amt = Convert.ToDecimal(pro.SubTotal_A * pro.PackLoss_Percent/100);
                pro.factoryexp_amt = Convert.ToDecimal(pro.SubTotal_B* pro.FactoryExpence_Percent/100);
                pro.Market_amt = Convert.ToDecimal(pro.SubTotal_B * pro.MarketByCharge_Percent / 100);
                pro.other_amt = Convert.ToDecimal(pro.SubTotal_B * pro.Other_Percent / 100);
                pro.profit_amt = Convert.ToDecimal(pro.SubTotal_B * pro.Profit_Percent / 100);
                decimal total = Convert.ToDecimal(pro.factoryexp_amt.ToString("0.00")) + Convert.ToDecimal(pro.Market_amt.ToString("0.00")) + Convert.ToDecimal(pro.other_amt.ToString("0.00")) + Convert.ToDecimal(pro.profit_amt.ToString("0.00"));
                pro.suggestedPrice = Convert.ToDecimal(pro.SubTotal_B)+ total;
                pro.finalProfit_per = Convert.ToDecimal(pro.FinalProfit_Amt*100 / pro.FinalPricePerUnit);
                */


                dtCheckForInsert = cls.Check_OtherCompanyActualEstimate(pro);
                if (dtCheckForInsert.Rows.Count > 0)
                {
                    status = cls.Update_OtherCompanyPriceListActualEstimate(pro);

                }
                else
                {
                    status = cls.Insert_OtherCompanyPriceListActualEstimate(pro);

                }

            }
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Added Sucessfully !')", true);
                //ClearAll();
                Grid_EstimatePriceListData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Failed !')", true);

            }
        }

        protected void CheckBox_Check_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
