using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;

namespace Production_Costing_Software
{
    public partial class comp_CompanywiseFactoryExpense : System.Web.UI.Page
    {
        int User_Id;
        int Company_Id;
        string Shipper_Id;
        Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            GetUserRights();
            if (!IsPostBack)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                Grid_comp_FactoryExpenceListData();
                BulkProductDropDownListCombo();
                //TradeNameDropDownListCombo();
                DisplayView();
            }

        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[23]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[23]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[23]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblCompanyFactoryExpence_Id.Text != "")
                {
                    AddComp_FactoryExpenceBtn.Visible = false;
                    UpdateComp_FactoryExpence.Visible = true;
                    ReportView.Visible = true;
                }
                else
                {
                    AddComp_FactoryExpenceBtn.Visible = true;
                    UpdateComp_FactoryExpence.Visible = false;

                    ReportView.Visible = true;
                }

            }
            else
            {
                AddComp_FactoryExpenceBtn.Visible = false;
                UpdateComp_FactoryExpence.Visible = false;

                ReportView.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[23]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[23]["CanEdit"]) == true)
            {
                if (lblCompanyFactoryExpence_Id.Text != "")
                {
                    AddComp_FactoryExpenceBtn.Visible = false;
                    UpdateComp_FactoryExpence.Visible = true;
                    ReportView.Visible = true;
                }
                else
                {
                    AddComp_FactoryExpenceBtn.Visible = true;
                    UpdateComp_FactoryExpence.Visible = false;

                    ReportView.Visible = true;
                }
            }
            else
            {
                AddComp_FactoryExpenceBtn.Visible = false;
                UpdateComp_FactoryExpence.Visible = false;

                ReportView.Visible = false;
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
        public void Grid_comp_FactoryExpenceListData()
        {

            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            Grid_CompanyFactoryExpence.DataSource = cls.Comp_Get_CompanyFectoryExpenceMaster(Company_Id);
            Grid_CompanyFactoryExpence.DataBind();

            foreach (GridViewRow row in Grid_CompanyFactoryExpence.Rows)
            {
                if (row.FindControl("SuggestedCostPerLtrtxt") != null || (row.FindControl("SuggestedCostPerLtrtxt") as Label).Text != "")
                {

                    (row.FindControl("SuggestedCostPerLtrtxt") as TextBox).Text = (row.FindControl("lblFinalFactoryCostLiter") as Label).Text;
                    //(row.FindControl("lblNetProfitAmount") as Label).Text=(row.FindControl("SuggestedCostPerLtrtxt") as TextBox).Text;
                }
                else
                {
                    break;
                }

            }
        }
        //public void GetLoginDetails()
        //{

        //    User_Id = Convert.ToInt32(Session["UserId"]);
        //    //CompanyName = Session["CompanyMasterList_Name"].ToString();
        //    Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);
        //}
        public void BulkProductDropDownListCombo()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            //dt = cls.Get_BP_MasterData(User_Id);

            //BulkProductDropdownlist.DataSource = dt;
            //BulkProductDropdownlist.DataTextField = "BulkProductName";


            //BulkProductDropdownlist.DataValueField = "BPM_Product_Id";
            //BulkProductDropdownlist.DataBind();
            //BulkProductDropdownlist.Items.Insert(0, "Select");
            dt = cls.Get_BulkProductMasterFromPackingMaterialMaster();

            dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + ShipperType_Id +')'").ToString();

            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BulkProductName";
            BulkProductDropdownlist.DataSource = dvOptions;
            BulkProductDropdownlist.DataTextField = "BulkProductName";

            BulkProductDropdownlist.DataValueField = "BPMValue";
            BulkProductDropdownlist.DataBind();
            BulkProductDropdownlist.Items.Insert(0, "Select");
        }
        protected void BulkProductDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            DataTable dtTotalAmtPerLiter = new DataTable();
            DataTable dtMasterPack = new DataTable();
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            ClsPackingMateriaMaster claMasterPack = new ClsPackingMateriaMaster();
            if (BulkProductDropdownlist.SelectedValue != "Select")
            {

                string Shippertype_Id = BulkProductDropdownlist.SelectedValue.ToString();

                Shipper_Id = Shippertype_Id.Split('(', ')')[1];
                //string Pack_Size = Shippertype_Id.Split('[', ']')[1];
                lblPMRM_Category_Id.Text = Shipper_Id;

                string Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
                lbl_BPM_Id.Text = Get_BPM_Id;

                //Checking BPM for Multiple Addition--------------------------

                DataTable dt = new DataTable();
                pro.BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
                pro.Fk_CompanyList_Id = Company_Id;
                dt = cls.Check_BPM_CompanyFectoryExpenceMasterBy(pro);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple BulkProduct !')", true);
                    BulkProductDropdownlist.SelectedIndex = 0;
                    Clear();
                    return;
                }
                else
                {
                    dtTotalAmtPerLiter = cls.Get_AmntPerLtrCostFor_CoFactoryExpenceMaster_MasterPacking(Convert.ToInt32(Get_BPM_Id), Convert.ToInt32(lblPMRM_Category_Id.Text));
                    dtMasterPack = cls.Get_AmntPerLtrCostFor_CoFactoryExpenceMaster_MasterPacking(Convert.ToInt32(Get_BPM_Id), Convert.ToInt32(lblPMRM_Category_Id.Text));
                    if (dtTotalAmtPerLiter.Rows.Count > 0)
                    {
                        Clear();
                        string TotalAmtPerLiter;
                        decimal PackSize;
                        int UnitMeasurement;
                        TotalAmtPerLiter = dtTotalAmtPerLiter.Rows[0]["TotalAmtPerLiter"].ToString();
                        PackSize = Convert.ToDecimal(dtTotalAmtPerLiter.Rows[0]["Packing_Size"]);
                        UnitMeasurement = Convert.ToInt32(dtTotalAmtPerLiter.Rows[0]["Fk_UnitMeasurement_Id"]);
                        lblPMRM_Category_Id.Text = dtTotalAmtPerLiter.Rows[0]["Fk_PM_RM_Category_Id"].ToString();

                        if (TotalAmtPerLiter != "")
                        {
                            //if (UnitMeasurement == 6 || UnitMeasurement == 7)
                            //{

                            //    decimal ConvertoLtr = 1000 / PackSize;
                            //    CostPerLtrtxt.Text = ((ConvertoLtr) * (decimal.Parse(TotalAmtPerLiter))).ToString();
                            //    lblBulkProductMasterPack.Text = dtTotalAmtPerLiter.Rows[0]["PackingUnitMeasurement"].ToString();
                            //}
                            //else if (UnitMeasurement == 1 || UnitMeasurement == 2 && PackSize >= 1)
                            //{
                            //    CostPerLtrtxt.Text = dtTotalAmtPerLiter.Rows[0]["TotalAmtPerLiter"].ToString();
                            //    lblBulkProductMasterPack.Text = dtTotalAmtPerLiter.Rows[0]["PackingUnitMeasurement"].ToString();
                            //}
                            //else
                            //{
                            //    CostPerLtrtxt.Text = ((PackSize) * (decimal.Parse(TotalAmtPerLiter))).ToString();
                            //    lblBulkProductMasterPack.Text = dtTotalAmtPerLiter.Rows[0]["PackingUnitMeasurement"].ToString();
                            //}
                            CostPerLtrtxt.Text = dtTotalAmtPerLiter.Rows[0]["TotalAmtPerLiter"].ToString();
                            if (dtMasterPack.Rows.Count > 0)
                            {
                                lblBulkProductMasterPack.Text = dtMasterPack.Rows[0]["PackingUnitMeasurement"].ToString();

                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Master Pack Not Assigned!')", true);

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TotalAmtPerLiter Not Found')", true);
                            Clear();
                        }

                        TotalCostPerLtrtxt.Text = (decimal.Parse(CostPerLtrtxt.Text)).ToString("0.00");
                    }
                    else
                    {
                        CostPerLtrtxt.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TotalAmtPerLiter Not Found')", true);
                        Clear();
                    }

                }

            }

        }
        public void Clear()
        {
            FactoryExpencePertxt.Text = "0";
            MarketedByChrgtxt.Text = "0";
            OtherPertxt.Text = "0";
            ProfitPertxt.Text = "0";
            FactoryExpenceAmounttxt.Text = "0";
            MarketedByChrgAmounttxt.Text = "0";
            OtherPerAmounttxt.Text = "0";
            ProfitPerAmounttxt.Text = "0";
            CostPerLtrtxt.Text = "0";
            lblBulkProductMasterPack.Text = "";
            TotalExpencetxt.Text = "0";
            TotalCostPerLtrtxt.Text = "0";
            UpdateComp_FactoryExpence.Visible = false;
            //CancelBtn.Visible = true;
            AddComp_FactoryExpenceBtn.Visible = true;
            //BulkProductDropdownlist.SelectedIndex = 0;
        }
        protected void FactoryExpencePertxt_TextChanged(object sender, EventArgs e)
        {
            if (FactoryExpencePertxt.Text == "")
            {
                FactoryExpencePertxt.Text = "0";
            }
            if (FactoryExpencePertxt.Text != "" || FactoryExpencePertxt.Text != "0")
            {
                FactoryExpenceAmounttxt.Text = (decimal.Parse(FactoryExpencePertxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100)).ToString("0.00");

                TotalExpencetxt.Text = (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text)).ToString();
                TotalCostPerLtrtxt.Text = (decimal.Parse(CostPerLtrtxt.Text) + (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text) + decimal.Parse(ProfitPerAmounttxt.Text))).ToString("0.00");

            }
        }

        protected void MarketedByChrgtxt_TextChanged(object sender, EventArgs e)
        {
            if (MarketedByChrgtxt.Text == "")
            {
                MarketedByChrgtxt.Text = "0";
            }
            if (MarketedByChrgtxt.Text != "" || MarketedByChrgtxt.Text != "0")
            {
                MarketedByChrgAmounttxt.Text = (decimal.Parse(MarketedByChrgtxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100)).ToString("0.00");
                TotalExpencetxt.Text = (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text)).ToString();

                TotalCostPerLtrtxt.Text = (decimal.Parse(CostPerLtrtxt.Text) + (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text) + decimal.Parse(ProfitPerAmounttxt.Text))).ToString("0.00");

            }
        }
        protected void ReportView_Click(object sender, EventArgs e)
        {
            //Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            //Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
            //pro.Fk_CompanyList_Id = Company_Id;
            //DataTable dt = new DataTable();
            //dt = cls.Comp_Get_BulkProdcutOfCompanyFectoryExpenceReport(pro);

            //BulkProductListbox.DataSource = dt;
            //BulkProductListbox.DataTextField = "BPM_Product_Name";
            //BulkProductListbox.DataValueField = "Fk_BPM_Id";
            //BulkProductListbox.DataBind();
            //BulkProductListbox.Items.Insert(0, "Select");

            //Grid_CompanyFactoryExpenceAllReport.DataSource = cls.Comp_Get_CompanyFectoryExpenceMasterOnlyMasterPack(pro);
            //Grid_CompanyFactoryExpenceAllReport.DataBind();
            Session["Company_Id"] = Company_Id;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openMsdfsdodal", "window.open('Report_FectoryExpenceMaster.aspx' ,'_blank');", true);
            //Response.Redirect("~/Report_OuterBrowserFectoryExpence.aspx");
        }
        protected void OtherPertxt_TextChanged(object sender, EventArgs e)
        {
            if (OtherPertxt.Text == "")
            {
                OtherPertxt.Text = "0";
            }
            if (OtherPertxt.Text != "" || OtherPertxt.Text != "0")
            {
                OtherPerAmounttxt.Text = (decimal.Parse(OtherPertxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100)).ToString("0.00");
                TotalExpencetxt.Text = (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text)).ToString();

                TotalCostPerLtrtxt.Text = (decimal.Parse(CostPerLtrtxt.Text) + (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text) + decimal.Parse(ProfitPerAmounttxt.Text))).ToString("0.00");

            }

        }

        protected void ProfitPertxt_TextChanged(object sender, EventArgs e)
        {
            if (ProfitPertxt.Text == "")
            {
                ProfitPertxt.Text = "0";
            }
            if (ProfitPertxt.Text != "" || ProfitPertxt.Text != "0")
            {
                ProfitPerAmounttxt.Text = ((decimal.Parse(TotalExpencetxt.Text) + decimal.Parse(CostPerLtrtxt.Text)) * (decimal.Parse(ProfitPertxt.Text) / 100)).ToString("0.00");
                TotalCostPerLtrtxt.Text = (decimal.Parse(CostPerLtrtxt.Text) + (decimal.Parse(ProfitPerAmounttxt.Text) + decimal.Parse(TotalExpencetxt.Text))).ToString("0.00");

            }
        }



        protected void AddComp_FactoryExpenceBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            string BPM_Full_Id = BulkProductDropdownlist.SelectedValue.ToString();
            string Get_BPM_Id = Regex.Match(BPM_Full_Id, @"\d+").Value;
            lblPMRM_Category_Id.Text = BPM_Full_Id.Split('(', ')')[1];
            pro.BPM_Id = Convert.ToInt32(Get_BPM_Id);
            pro.PMRMCategory_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.FectoryExpencePer = decimal.Parse(FactoryExpencePertxt.Text);
            pro.MarketedByChargesPer = decimal.Parse(MarketedByChrgtxt.Text);
            pro.OtherPer = decimal.Parse(OtherPertxt.Text);
            pro.ProfitPer = decimal.Parse(ProfitPertxt.Text);
            pro.TotalExpence = decimal.Parse(TotalExpencetxt.Text);
            pro.Fk_CompanyList_Id = Company_Id;
            //pro.CompanyFactoryExpence_Id = Convert.ToInt32(lblCompanyFactoryExpence_Id.Text);

            int status = cls.INSERT_CompanyWiseFactoryExpence(pro);

            if (status > 0)
            {
                Grid_comp_FactoryExpenceListData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Interest Inserted Successfully')", true);
                Clear();
                BulkProductDropdownlist.SelectedIndex = 0;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }
        }

        protected void UpdateComp_FactoryExpence_Click(object sender, EventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            string BPM_Full_Id = BulkProductDropdownlist.SelectedValue.ToString();
            string Get_BPM_Id = Regex.Match(BPM_Full_Id, @"\d+").Value;
            lblPMRM_Category_Id.Text = BPM_Full_Id.Split('(', ')')[1];
            pro.BPM_Id = Convert.ToInt32(Get_BPM_Id);
            //pro.BPM_Id = Convert.ToInt32(BulkProductDropdownlist.SelectedValue);
            pro.Fk_CompanyList_Id = Company_Id;
            pro.FectoryExpencePer = decimal.Parse(FactoryExpencePertxt.Text);
            pro.MarketedByChargesPer = decimal.Parse(MarketedByChrgtxt.Text);
            pro.OtherPer = decimal.Parse(OtherPertxt.Text);
            pro.ProfitPer = decimal.Parse(ProfitPertxt.Text);
            pro.TotalExpence = decimal.Parse(TotalExpencetxt.Text);
            pro.Fk_CompanyList_Id = Company_Id;
            pro.PMRMCategory_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.CompanyFactoryExpence_Id = Convert.ToInt32(lblCompanyFactoryExpence_Id.Text);
            int status = cls.UPDATE_CompanyWiseFactoryExpence(pro);

            if (status > 0)
            {
                Grid_comp_FactoryExpenceListData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Interest Updated Successfully')", true);
                Clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed!')", true);

            }
        }

        protected void GridDelCompanyFectoryExpenceBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();

            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int CompanyFactoryExpence_Id = Convert.ToInt32(Grid_CompanyFactoryExpence.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.CompanyFactoryExpence_Id = CompanyFactoryExpence_Id;
            pro.Fk_CompanyList_Id = Company_Id;
            DataTable dt = new DataTable();
            dt = cls.Comp_Get_CompanyFectoryExpenceMasterById(pro);
            lbl_BPM_Id.Text = dt.Rows[0]["Fk_BPM_Id"].ToString();
            pro.BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
            pro.Fk_CompanyList_Id = Company_Id;
            pro.PMRMCategory_Id = Convert.ToInt32(dt.Rows[0]["Fk_PMRMCategory_Id"]);
            int status = cls.Delete_CompanyWiseFactoryExpence(pro);
            if (status > 0)
            {
                Grid_comp_FactoryExpenceListData();
                Clear();
                AddComp_FactoryExpenceBtn.Visible = true;
                UpdateComp_FactoryExpence.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
        }

        protected void GridEditCompanyFectoryExpenceBtn_Click(object sender, EventArgs e)
        {
            BulkProductDropdownlist.Enabled = false;
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int CompanyFactoryExpence_Id = Convert.ToInt32(Grid_CompanyFactoryExpence.DataKeys[gdrow.RowIndex].Value.ToString());
            lblCompanyFactoryExpence_Id.Text = CompanyFactoryExpence_Id.ToString();
            pro.CompanyFactoryExpence_Id = CompanyFactoryExpence_Id;
            pro.Fk_CompanyList_Id = Company_Id;
            DataTable dt = new DataTable();

            dt = cls.Comp_Get_CompanyFectoryExpenceMasterById(pro);
            BulkProductDropdownlist.SelectedValue = dt.Rows[0]["Fk_BPM_Id"].ToString() + " (" + dt.Rows[0]["Fk_PMRMCategory_Id"].ToString() + ")";


            string Shippertype_Id = BulkProductDropdownlist.SelectedValue.ToString();

            Shipper_Id = Shippertype_Id.Split('(', ')')[1];
            lblPMRM_Category_Id.Text = Shipper_Id;

            string Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
            lbl_BPM_Id.Text = Get_BPM_Id;
            //int BPM_Id = Convert.ToInt32(BulkProductDropdownlist.SelectedValue);


            DataTable dtTtlAmt = new DataTable();
            dtTtlAmt = cls.Get_AmntPerLtrCostFor_CoFactoryExpenceMasterByBPM_Id(Convert.ToInt32(Get_BPM_Id),Convert.ToInt32(dt.Rows[0]["Fk_PMRMCategory_Id"].ToString()));
            CostPerLtrtxt.Text = dtTtlAmt.Rows[0]["TotalAmtPerLiter"].ToString();
            FactoryExpencePertxt.Text = dt.Rows[0]["FectoryExpencePer"].ToString();

            FactoryExpenceAmounttxt.Text = (decimal.Parse(FactoryExpencePertxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100)).ToString("0.00");
            OtherPertxt.Text = dt.Rows[0]["OtherPer"].ToString();
            ProfitPertxt.Text = dt.Rows[0]["ProfitPer"].ToString();
            OtherPerAmounttxt.Text = (decimal.Parse(OtherPertxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100)).ToString("0.00");
            MarketedByChrgtxt.Text = dt.Rows[0]["MarketedByChargesPer"].ToString();
            MarketedByChrgAmounttxt.Text = (decimal.Parse(MarketedByChrgtxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100)).ToString("0.00");

            lblPMRM_Category_Id.Text = dt.Rows[0]["Fk_PMRMCategory_Id"].ToString();
            TotalExpencetxt.Text = (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text)).ToString();
            ProfitPerAmounttxt.Text = ((decimal.Parse(TotalExpencetxt.Text) + decimal.Parse(CostPerLtrtxt.Text)) * (decimal.Parse(ProfitPertxt.Text) / 100)).ToString("0.00");
            TotalCostPerLtrtxt.Text = (decimal.Parse(TotalExpencetxt.Text) + (decimal.Parse(ProfitPerAmounttxt.Text)) + (decimal.Parse(CostPerLtrtxt.Text))).ToString();

            UpdateComp_FactoryExpence.Visible = true;
            //CancelBtn.Visible = true;
            AddComp_FactoryExpenceBtn.Visible = false;

        }

        protected void CancelComp_FactoryExpence_Click(object sender, EventArgs e)
        {
            FactoryExpencePertxt.Text = "0";
            MarketedByChrgtxt.Text = "0";
            OtherPertxt.Text = "0";
            ProfitPertxt.Text = "0";
            FactoryExpenceAmounttxt.Text = "0";
            MarketedByChrgAmounttxt.Text = "0";
            OtherPerAmounttxt.Text = "0";
            ProfitPerAmounttxt.Text = "0";
            CostPerLtrtxt.Text = "0";
            lblBulkProductMasterPack.Text = "";
            BulkProductDropdownlist.SelectedIndex = 0;
            lblBulkProductMasterPack.Text = "";
            lblCompanyFactoryExpence_Id.Text = "";
            BulkProductDropdownlist.Enabled = true;
            TotalExpencetxt.Text = "0";
            TotalCostPerLtrtxt.Text = "0";
            lblPMRM_Category_Id.Text = "";
            UpdateComp_FactoryExpence.Visible = false;
            //CancelBtn.Visible = true;
            AddComp_FactoryExpenceBtn.Visible = true;
        }


        protected void TotalExpencetxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SuggestedCostPerLtrtxt_TextChanged(object sender, EventArgs e)
        {
            decimal SuggestedCostPerLtr = 0;
            decimal TotalExpenceAmount = 0;
            decimal TotalAmtPerLtr = 0;

            foreach (GridViewRow row in Grid_CompanyFactoryExpence.Rows)
            {
                if (row.FindControl("SuggestedCostPerLtrtxt") != null || (row.FindControl("SuggestedCostPerLtrtxt") as Label).Text != "")
                {
                    SuggestedCostPerLtr = (Convert.ToDecimal((row.FindControl("SuggestedCostPerLtrtxt") as TextBox).Text));
                    TotalExpenceAmount = (Convert.ToDecimal((row.FindControl("lblTotalExpenceAmount") as Label).Text));
                    TotalAmtPerLtr = (Convert.ToDecimal((row.FindControl("lblTotalAmtPerLiter") as Label).Text));
                    (row.FindControl("lblNetProfitAmount") as Label).Text = ((SuggestedCostPerLtr) - (TotalExpenceAmount) - (TotalAmtPerLtr)).ToString();
                }
                else
                {
                    break;
                }

            }
        }



        protected void Grid_CompanyFactoryExpence_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            decimal SuggestedCostPerLtr = 0;
            decimal TotalExpenceAmount = 0;
            decimal TotalAmtPerLtr = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox Salary = (TextBox)e.Row.FindControl("SuggestedCostPerLtrtxt");
                SuggestedCostPerLtr = (Convert.ToDecimal((e.Row.FindControl("SuggestedCostPerLtrtxt") as TextBox).Text));
                TotalExpenceAmount = (Convert.ToDecimal((e.Row.FindControl("lblTotalExpenceAmount") as Label).Text));
                TotalAmtPerLtr = (Convert.ToDecimal((e.Row.FindControl("lblTotalAmtPerLiter") as Label).Text));

                //(e.Row.FindControl("lblNetProfitAmount") as Label).Text = ((SuggestedCostPerLtr) - (TotalExpenceAmount) - (TotalAmtPerLtr)).ToString();

            }
        }

        protected void GridAddCompanyFectoryExpenceBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            ProEsitmate_PriceList proSuggest = new ProEsitmate_PriceList();
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int CompanyFactoryExpence_Id = Convert.ToInt32(Grid_CompanyFactoryExpence.DataKeys[gdrow.RowIndex].Value.ToString());
            lblCompanyFactoryExpence_Id.Text = CompanyFactoryExpence_Id.ToString();
            pro.CompanyFactoryExpence_Id = CompanyFactoryExpence_Id;
            proSuggest.Fk_CWFE_Id = CompanyFactoryExpence_Id;
            pro.Fk_CompanyList_Id = Company_Id;
            DataTable dt = new DataTable();

            dt = cls.Comp_Get_CompanyFectoryExpenceMasterById(pro);
            BulkProductDropdownlist.SelectedValue = dt.Rows[0]["Fk_BPM_Id"].ToString() + " (" + dt.Rows[0]["Fk_PMRMCategory_Id"].ToString() + ")";


            string Shippertype_Id = BulkProductDropdownlist.SelectedValue.ToString();

            Shipper_Id = Shippertype_Id.Split('(', ')')[1];
            lblPMRM_Category_Id.Text = Shipper_Id;

            string Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
            lbl_BPM_Id.Text = Get_BPM_Id;
            proSuggest.Fk_BPM_Id = Convert.ToInt32(Get_BPM_Id);
            //int BPM_Id = Convert.ToInt32(BulkProductDropdownlist.SelectedValue);


            DataTable dtTtlAmt = new DataTable();
            dtTtlAmt = cls.Get_AmntPerLtrCostFor_CoFactoryExpenceMasterByBPM_Id(Convert.ToInt32(Get_BPM_Id), Convert.ToInt32(dt.Rows[0]["Fk_PMRMCategory_Id"].ToString()));
            proSuggest.CWFE_TotalCostPerLtr = Convert.ToDecimal(dtTtlAmt.Rows[0]["TotalAmtPerLiter"]);
            CostPerLtrtxt.Text = proSuggest.CWFE_TotalCostPerLtr.ToString("0.00");
            proSuggest.CWFE_FctryExp_Per = Convert.ToDecimal(dt.Rows[0]["FectoryExpencePer"]);
            FactoryExpencePertxt.Text = proSuggest.CWFE_FctryExp_Per.ToString("0.00");
            proSuggest.CWFE_FctryExp_Amt = (decimal.Parse(FactoryExpencePertxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100));
            FactoryExpenceAmounttxt.Text = proSuggest.CWFE_FctryExp_Amt.ToString("0.00");
            proSuggest.CWFE_Other_Per = Convert.ToDecimal(dt.Rows[0]["OtherPer"]);
            OtherPertxt.Text = proSuggest.CWFE_Other_Per.ToString("0.00");
            proSuggest.CWFE_Profit_Per = Convert.ToDecimal(dt.Rows[0]["ProfitPer"]);
            proSuggest.CWFE_Other_Amt = (decimal.Parse(OtherPertxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100));
            OtherPerAmounttxt.Text = proSuggest.CWFE_Other_Amt.ToString("0.00");
            proSuggest.CWFE_Mrkt_Per = Convert.ToDecimal(dt.Rows[0]["MarketedByChargesPer"]);
            MarketedByChrgtxt.Text = proSuggest.CWFE_Mrkt_Per.ToString("0.00");
            proSuggest.CWFE_Mrkt_Amt = (decimal.Parse(MarketedByChrgtxt.Text) * (decimal.Parse(CostPerLtrtxt.Text) / 100));
            MarketedByChrgAmounttxt.Text = proSuggest.CWFE_Mrkt_Amt.ToString("0.00");
            lblPMRM_Category_Id.Text = dt.Rows[0]["Fk_PMRMCategory_Id"].ToString();
            proSuggest.Fk_PMRM_Catgeory_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            proSuggest.CWFE_TotalExp = (decimal.Parse(FactoryExpenceAmounttxt.Text) + decimal.Parse(MarketedByChrgAmounttxt.Text) + decimal.Parse(OtherPerAmounttxt.Text));
            TotalExpencetxt.Text = proSuggest.CWFE_TotalExp.ToString("0.00");
            proSuggest.CWFE_Profit_Amt = ((decimal.Parse(TotalExpencetxt.Text) + decimal.Parse(CostPerLtrtxt.Text)) * (decimal.Parse(ProfitPertxt.Text) / 100));
            ProfitPerAmounttxt.Text = proSuggest.CWFE_Profit_Amt.ToString("0.00");
            proSuggest.CWFE_FFCostPerLtr = (decimal.Parse(TotalExpencetxt.Text) + (decimal.Parse(ProfitPerAmounttxt.Text)) + (decimal.Parse(CostPerLtrtxt.Text)));

        }

        protected void Grid_CompanyFactoryExpence_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            ProEsitmate_PriceList proSuggest = new ProEsitmate_PriceList();


            decimal SuggestedCostPerLtr = 0;
            decimal TotalExpenceAmount = 0;
            decimal TotalAmtPerLtr = 0;

            if (e.CommandName.Equals("Add"))
            {
                var btnSender = (Button)e.CommandSource;
                GridViewRow GrdRow = (GridViewRow)btnSender.Parent.Parent;
                SuggestedCostPerLtr = Convert.ToDecimal(((TextBox)GrdRow.Cells[0].FindControl("SuggestedCostPerLtrtxt") as TextBox).Text);
                //SuggestedCostPerLtr = Convert.ToDecimal(SuggestCostPerLtr);
                //TotalExpenceAmount = (Convert.ToDecimal((e.Row.FindControl("lblTotalExpenceAmount") as Label).Text));
                TotalExpenceAmount = Convert.ToDecimal(((Label)GrdRow.Cells[0].FindControl("lblTotalExpenceAmount") as Label).Text);
                //TotalAmtPerLtr = (Convert.ToDecimal((e.Row.FindControl("lblTotalAmtPerLiter") as Label).Text));
                TotalAmtPerLtr = Convert.ToDecimal(((Label)GrdRow.Cells[0].FindControl("lblTotalAmtPerLiter") as Label).Text);

                proSuggest.Fk_CWFE_Id = Convert.ToInt32(lblCompanyFactoryExpence_Id.Text);
                proSuggest.Fk_BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
                proSuggest.CWFE_TotalCostPerLtr = Convert.ToDecimal(lblCWFE_TotalCostPerLtr.Text);
                proSuggest.CWFE_FctryExp_Per = Convert.ToDecimal(lblCWFE_FctryExp_Per.Text);
                proSuggest.CWFE_FctryExp_Amt = Convert.ToDecimal(lblCWFE_FctryExp_Amt.Text);
                proSuggest.CWFE_Other_Per = Convert.ToDecimal(lblCWFE_Other_Per.Text);
                proSuggest.CWFE_Other_Amt = Convert.ToDecimal(lblCWFE_Other_Amt.Text);
                proSuggest.CWFE_Mrkt_Per = Convert.ToDecimal(lblCWFE_Mrkt_Per.Text);
                proSuggest.CWFE_Mrkt_Amt = Convert.ToDecimal(lblCWFE_Mrkt_Amt.Text);
                proSuggest.Fk_PMRM_Catgeory_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
                proSuggest.CWFE_TotalExp = Convert.ToDecimal(lblCWFE_TotalExp.Text);
                proSuggest.CWFE_Profit_Amt = Convert.ToDecimal(lblCWFE_Profit_Amt.Text);
                proSuggest.CWFE_FFCostPerLtr = Convert.ToDecimal(lblCWFE_FFCostPerLtr.Text);


                proSuggest.Suggested_FFCostPerLtr = SuggestedCostPerLtr;
                proSuggest.Suggested_NetProfitAmt = ((SuggestedCostPerLtr) - (TotalExpenceAmount) - (TotalAmtPerLtr));

                //int status = cls.INSERT_Suggested_CWFE(proSuggest);

                //if (status > 0)
                //{
                //    Grid_comp_FactoryExpenceListData();
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

                //}
            }
        }
    }
}