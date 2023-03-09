using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class OtherPriceListMaster : System.Web.UI.Page
    {
        int UserId;
        int Company_Id;
        string Shipper_Id;

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
                lblCompanyMasterList_Name.Text = Session["CompanyMasterList_Name"].ToString();
                if (lblCompanyMasterList_Name.Text == "GP")
                {
                    CompanyVisible.Visible = false;
                }
                else
                {
                    CompanyVisible.Visible = true;

                }
                //ProductCategoryCombo();
                BulkProductMasterCombo();
                DisplayView();
                GetOtherComapnyActualPriceList();
            }
        }
        public void GetOtherComapnyActualPriceList()
        {
            ClsOtherPriceListMaster cls = new ClsOtherPriceListMaster();
            ProOtherCompanyPriceList pro = new ProOtherCompanyPriceList();
            DataTable dt = new DataTable();
            pro.Company_Id = Company_Id;
            dt = cls.Get_OtherCompanyActualPricelist(pro);

            Grid_OtherCompanyPriceMaster.DataSource = dt;
            Grid_OtherCompanyPriceMaster.DataBind();
        }
        public void BulkProductMasterCombo()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = 1;
            dt = cls.Get_BulkProductMasterFromPackingMaterialMaster();

            dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + ShipperType_Id +')'").ToString();

            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BulkProductName";
            BulkProductNameDropDownList.DataSource = dvOptions;
            BulkProductNameDropDownList.DataTextField = "BulkProductName";

            BulkProductNameDropDownList.DataValueField = "BPMValue";
            BulkProductNameDropDownList.DataBind();
            BulkProductNameDropDownList.Items.Insert(0, "Select");
        }

        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[31]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[31]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblOtherCompanyPrice_Id.Text != "")
                {
                    AddOtherCompanyPrice.Visible = false;
                    CancelOtherCompanyPrice.Visible = true;
                    UpdateOtherCompanyPrice.Visible = true;
                }
                else
                {
                    AddOtherCompanyPrice.Visible = true;
                    CancelOtherCompanyPrice.Visible = true;
                    UpdateOtherCompanyPrice.Visible = false;
                }
            }
            else
            {
                AddOtherCompanyPrice.Visible = false;
                CancelOtherCompanyPrice.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[31]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]) == true)
            {
                if (lblOtherCompanyPrice_Id.Text != "")
                {
                    AddOtherCompanyPrice.Visible = false;
                    CancelOtherCompanyPrice.Visible = true;
                    UpdateOtherCompanyPrice.Visible = true;
                }
                else
                {
                    AddOtherCompanyPrice.Visible = true;
                    CancelOtherCompanyPrice.Visible = true;
                    UpdateOtherCompanyPrice.Visible = false;
                }
            }
            else
            {
                AddOtherCompanyPrice.Visible = false;
                CancelOtherCompanyPrice.Visible = false;
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
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }

        protected void BulkProductNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BulkProductNameDropDownList.SelectedValue != "Select")
            {
                BulkPackSize.Enabled = true;
                string BPM_Full_Id = BulkProductNameDropDownList.SelectedValue.ToString();

                string Shippertype_Id = BPM_Full_Id;

                Shipper_Id = Shippertype_Id.Split('(', ')')[1];
                lblPMRM_Category_Id.Text = Shipper_Id;

                string Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
                lblBPM_Id.Text = Get_BPM_Id;
                ProCategoryMappingMaster pro = new ProCategoryMappingMaster();
                ClsCategoryMappingMaster cls = new ClsCategoryMappingMaster();
                DataTable dt = new DataTable();
                pro.Comapny_Id = Company_Id;
                lblCompany_Id.Text = Company_Id.ToString();
                dt = cls.Get_ProductCategoryMasterAll(pro);
                //foreach (DataRow row in dt.Rows)
                //{

                //    string Fk_BPM_Id = row["Fk_BPM_Id"].ToString();
                //    string PM_RM_Category_id = row["PM_RM_Category_id"].ToString();
                //    string Company_Id = (row["Fk_Company_Id"]).ToString();

                //    //if (Fk_BPM_Id == lblBPM_Id.Text && PM_RM_Category_id == lblPMRM_Category_Id.Text && Company_Id == lblCompany_Id.Text)
                //    //{
                //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple BulkProduct !')", true);
                //    //    ClearData();
                //    //    return;
                //    //}
                //}
                DataTable dtBulkCost = new DataTable();
                ClsOtherPriceListMaster clsBulkCost = new ClsOtherPriceListMaster();

                dtBulkCost = clsBulkCost.GetBulkCostPerLtrByBPM_Id(Convert.ToInt32(lblBPM_Id.Text));
                BulkCostPerLtr.Text = dtBulkCost.Rows[0]["BulkCostPerLtr"].ToString();
                BoxSizetxt.Text = "";
                PackingSizeDropDownListData();
                ClearData();

                BulkPackSize.Focus();
            }
            else
            {
                ClearData();
                BulkPackSize.Enabled = false;

            }

         

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calculatePricelist('"+ BulkInterestPertxt.ClientID+ "');", true); ;

        }


        protected void BulkPackSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BulkPackSize.SelectedValue != "Select")
            {
                string Pack = BulkPackSize.SelectedValue.ToString();

                lblPackMeasurement.Text = Pack.Split('(', ')')[1];

                string PackSize = Regex.Match(Pack, @"\d+").Value;
                lblPackSize.Text = PackSize;
                BoxSize();

                bool bb = checkexistingdata();

                string packname = BulkPackSize.SelectedItem.Text;

                if (bb)
                {

                    BulkPackSize.SelectedIndex = -1;
                    //BulkProductNameDropDownList.SelectedIndex = -1;
                    // ScriptManager.RegisterStartupScript(UpdatePanel3, UpdatePanel3.GetType(), "alert", , true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Bulk Product ["+ BulkProductNameDropDownList.SelectedItem.Text+"] with this Packsize ["+ packname + "] already exist')", true);            


                }
                else
                {

                    DataTable dtPMCost = new DataTable();
                    ClsOtherPriceListMaster clsBulkCost = new ClsOtherPriceListMaster();

                    dtPMCost = clsBulkCost.Get_PMCostByBPM_PackSize(Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPackSize.Text), Convert.ToInt32(lblPackMeasurement.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
                    PMtxt.Text = dtPMCost.Rows[0]["PMCost"].ToString();
                    dtPMCost = clsBulkCost.Get_LabourCost_UnitByBPM_PackSize(Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPackSize.Text), Convert.ToInt32(lblPackMeasurement.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
                    PMTotaltxt.Text = (Convert.ToDecimal(PMtxt.Text) + (Convert.ToDecimal(AddBuffPMtxt.Text))).ToString("0.00");

                    Labourtxt.Text = dtPMCost.Rows[0]["FinalPerUnitLabourCost"].ToString();
                    TotalLabourtxt.Text = (Convert.ToDecimal(Labourtxt.Text) + (Convert.ToDecimal(AddBuffLabourtxt.Text))).ToString("0.00");
                    TotalCalculateAtxt.Text = (Convert.ToDecimal(TotalLabourtxt.Text) + (Convert.ToDecimal(BulkCostPerUnittxt.Text)) + (Convert.ToDecimal(PMTotaltxt.Text))).ToString("0.00");
                    LossAmttxt.Text = (Convert.ToDecimal(LossPertxt.Text) * (Convert.ToDecimal(TotalCalculateAtxt.Text) / 100)).ToString("0.00");
                    TotalCalculateBtxt.Text = (Convert.ToDecimal(TotalCalculateAtxt.Text) + (Convert.ToDecimal(LossAmttxt.Text))).ToString("0.00");

                    BulkInterestPertxt.Focus();
                }

            }
            else
            {

            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calculatePricelist('" + BulkInterestPertxt.ClientID + "');", true); ;
        }

        private bool checkexistingdata()
        {
            bool ret = false;
            ClsOtherPriceListMaster clsBulkCost = new ClsOtherPriceListMaster();
            DataTable dt = clsBulkCost.CheckDuplicatePriceListMaster(Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPackMeasurement.Text),Convert.ToDecimal(lblPackSize.Text));
            if(dt.Rows.Count>0)
            {
                ret = true;
            }
            return ret;
        }

        public void ClearData()
        {
            BulkPackSize.SelectedIndex = -1;
            BoxSizetxt.Text = "";
            if (BulkProductNameDropDownList.SelectedValue != "Select")
            {

            }
            else
            {
                BulkCostPerLtr.Text = "0.00";

            }
            TotalBulkCostWithInteresttxt.Text = "0.00";
            BulkInterestAmttxt.Text = "0.00";
            BulkInterestPertxt.Text = "0.00";
            BulkCostPerUnittxt.Text = "0.00";
        }
        public void PackingSizeDropDownListData()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = 1;

            dt = cls.Get_SubPackingMaterialMasterByBPM_Id(1, Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
            dt.Columns.Add("PackingSize", typeof(string), "Pack_size + ' (' + Pack_Measurement + ')'").ToString();



            dt.AsEnumerable().Select(a => a.Field<string>("PackingSize").ToString()).Distinct(); ;
            BulkPackSize.DataSource = dt;
            BulkPackSize.DataTextField = "TotalPackSize";
            BulkPackSize.DataValueField = "PackingSize";
            BulkPackSize.DataBind();
            BulkPackSize.Items.Insert(0, "Select");
        }
        public void BoxSize()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = 1;

            dt = cls.Get_SubPackingMaterialMasteBoxSizerByBPM_Id_PackSize(Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text), Convert.ToDecimal(lblPackSize.Text), Convert.ToInt32(lblPackMeasurement.Text));
            //dt.Columns.Add("BoxSize", typeof(string), "Pack_ShipperSize").ToString();

            BoxSizetxt.Text = dt.Rows[0]["Pack_ShipperSize"].ToString();

            if (BulkInterestPertxt.Text != "0" && BulkCostPerLtr.Text != "0" && BulkPackSize.SelectedValue != "Select")
            {
                BulkInterestAmttxt.Text = (Convert.ToDecimal(BulkCostPerLtr.Text) * (Convert.ToDecimal(BulkInterestPertxt.Text) / 100)).ToString("0.00");
                TotalBulkCostWithInteresttxt.Text = (Convert.ToDecimal(BulkInterestAmttxt.Text) + (Convert.ToDecimal(BulkCostPerLtr.Text))).ToString("0.00");
                if (Convert.ToDecimal(lblPackSize.Text) < 1000 && (Convert.ToInt32(lblPackMeasurement.Text) == 7 || Convert.ToInt32(lblPackMeasurement.Text) == 6))
                {
                    decimal ConvertLtrToGmOrMl = (1000 / (Convert.ToDecimal(lblPackSize.Text)));
                    BulkCostPerUnittxt.Text = ((Convert.ToDecimal(TotalBulkCostWithInteresttxt.Text)) / ConvertLtrToGmOrMl).ToString("0.00");
                }
                if (Convert.ToDecimal(lblPackSize.Text) > 1 && (Convert.ToInt32(lblPackMeasurement.Text) == 1 || Convert.ToInt32(lblPackMeasurement.Text) == 2))
                {
                    decimal ConvertLtrToGmOrMl = ((Convert.ToDecimal(lblPackSize.Text)));
                    BulkCostPerUnittxt.Text = ((Convert.ToDecimal(TotalBulkCostWithInteresttxt.Text)) * ConvertLtrToGmOrMl).ToString("0.00");
                }
                if (Convert.ToDecimal(lblPackSize.Text) == 1 && (Convert.ToInt32(lblPackMeasurement.Text) == 1 || Convert.ToInt32(lblPackMeasurement.Text) == 2))
                {
                    decimal ConvertLtrToGmOrMl = (1000 / (Convert.ToDecimal(lblPackSize.Text)));
                    BulkCostPerUnittxt.Text = (Convert.ToDecimal(TotalBulkCostWithInteresttxt.Text).ToString("0.00"));
                }
            }
        }

        protected void BulkInterestPertxt_TextChanged(object sender, EventArgs e)
        {
            if (BulkInterestPertxt.Text != "" && BulkInterestPertxt.Text != "0" && BulkCostPerLtr.Text != "0" && BulkPackSize.SelectedValue != "Select")
            {
                BulkInterestAmttxt.Text = (Convert.ToDecimal(BulkCostPerLtr.Text) * (Convert.ToDecimal(BulkInterestPertxt.Text) / 100)).ToString("0.00");
                TotalBulkCostWithInteresttxt.Text = (Convert.ToDecimal(BulkInterestAmttxt.Text) + (Convert.ToDecimal(BulkCostPerLtr.Text))).ToString("0.00");
                if (Convert.ToDecimal(lblPackSize.Text) < 1000 && (Convert.ToInt32(lblPackMeasurement.Text) == 7 || Convert.ToInt32(lblPackMeasurement.Text) == 6))
                {
                    decimal ConvertLtrToGmOrMl = (1000 / (Convert.ToDecimal(lblPackSize.Text)));
                    BulkCostPerUnittxt.Text = ((Convert.ToDecimal(TotalBulkCostWithInteresttxt.Text)) / ConvertLtrToGmOrMl).ToString("0.00");
                }
                if (Convert.ToDecimal(lblPackSize.Text) > 1 && (Convert.ToInt32(lblPackMeasurement.Text) == 1 || Convert.ToInt32(lblPackMeasurement.Text) == 2))
                {
                    decimal ConvertLtrToGmOrMl = ((Convert.ToDecimal(lblPackSize.Text)));
                    BulkCostPerUnittxt.Text = ((Convert.ToDecimal(TotalBulkCostWithInteresttxt.Text)) * ConvertLtrToGmOrMl).ToString("0.00");
                }
                if (Convert.ToDecimal(lblPackSize.Text) == 1 && (Convert.ToInt32(lblPackMeasurement.Text) == 1 || Convert.ToInt32(lblPackMeasurement.Text) == 2))
                {
                    decimal ConvertLtrToGmOrMl = (1000 / (Convert.ToDecimal(lblPackSize.Text)));
                    BulkCostPerUnittxt.Text = (Convert.ToDecimal(TotalBulkCostWithInteresttxt.Text).ToString("0.00"));
                }


                PMTotaltxt.Text = (Convert.ToDecimal(PMtxt.Text) + (Convert.ToDecimal(AddBuffPMtxt.Text))).ToString("0.00");

                TotalLabourtxt.Text = (Convert.ToDecimal(Labourtxt.Text) + (Convert.ToDecimal(AddBuffLabourtxt.Text))).ToString("0.00");
                
                TotalCalculateAtxt.Text = (Convert.ToDecimal(TotalLabourtxt.Text) + (Convert.ToDecimal(BulkCostPerUnittxt.Text)) + (Convert.ToDecimal(PMTotaltxt.Text))).ToString("0.00");
               
                LossAmttxt.Text = (Convert.ToDecimal(LossPertxt.Text) * (Convert.ToDecimal(TotalCalculateAtxt.Text) / 100)).ToString("0.00");
                TotalCalculateBtxt.Text = (Convert.ToDecimal(TotalCalculateAtxt.Text) + (Convert.ToDecimal(LossAmttxt.Text))).ToString("0.00");

               
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please Check Packing Size!')", true);
                ClearData();
            }
        }

        protected void AddBuffPMtxt_TextChanged(object sender, EventArgs e)
        {
            if (AddBuffPMtxt.Text != "0")
            {
                PMTotaltxt.Text = (Convert.ToDecimal(PMtxt.Text) + (Convert.ToDecimal(AddBuffPMtxt.Text))).ToString("0.00");
                TotalLabourtxt.Text = (Convert.ToDecimal(Labourtxt.Text) + (Convert.ToDecimal(AddBuffLabourtxt.Text))).ToString("0.00");
                TotalCalculateAtxt.Text = (Convert.ToDecimal(TotalLabourtxt.Text) + (Convert.ToDecimal(BulkCostPerUnittxt.Text)) + (Convert.ToDecimal(PMTotaltxt.Text))).ToString("0.00");
                LossAmttxt.Text = (Convert.ToDecimal(LossPertxt.Text) * (Convert.ToDecimal(TotalCalculateAtxt.Text) / 100)).ToString("0.00");
                TotalCalculateBtxt.Text = (Convert.ToDecimal(TotalCalculateAtxt.Text) + (Convert.ToDecimal(LossAmttxt.Text))).ToString("0.00");

            }
        }

        protected void AddBuffLabourtxt_TextChanged(object sender, EventArgs e)
        {
            PMTotaltxt.Text = (Convert.ToDecimal(PMtxt.Text) + (Convert.ToDecimal(AddBuffPMtxt.Text))).ToString("0.00");

            TotalLabourtxt.Text = (Convert.ToDecimal(Labourtxt.Text) + (Convert.ToDecimal(AddBuffLabourtxt.Text))).ToString("0.00");
            TotalCalculateAtxt.Text = (Convert.ToDecimal(TotalLabourtxt.Text) + (Convert.ToDecimal(BulkCostPerUnittxt.Text)) + (Convert.ToDecimal(PMTotaltxt.Text))).ToString("0.00");
            LossAmttxt.Text = (Convert.ToDecimal(LossPertxt.Text) * (Convert.ToDecimal(TotalCalculateAtxt.Text) / 100)).ToString("0.00");
            TotalCalculateBtxt.Text = (Convert.ToDecimal(TotalCalculateAtxt.Text) + (Convert.ToDecimal(LossAmttxt.Text))).ToString("0.00");

        }

        protected void LossPertxt_TextChanged(object sender, EventArgs e)
        {
            if (LossPertxt.Text != "0")
            {
                LossAmttxt.Text = (Convert.ToDecimal(LossPertxt.Text) * (Convert.ToDecimal(TotalCalculateAtxt.Text) / 100)).ToString("0.00");
                TotalCalculateBtxt.Text = (Convert.ToDecimal(TotalCalculateAtxt.Text) + (Convert.ToDecimal(LossAmttxt.Text))).ToString("0.00");

            }
        }

        protected void MarketedByChargesPertxt_TextChanged(object sender, EventArgs e)
        {
            MarketedByChargesAmttxt.Text = ((Convert.ToDecimal(TotalCalculateBtxt.Text) * (Convert.ToDecimal(MarketedByChargesPertxt.Text))) / 100).ToString("0.00");
            TotalCalculateCtxt.Text = (Convert.ToDecimal(MarketedByChargesAmttxt.Text) + Convert.ToDecimal(FactoryExpenceAmttxt.Text) + Convert.ToDecimal(ProfitAmttxt.Text) + Convert.ToDecimal(OtherAmttxt.Text)).ToString("0.00");
            FinalSuggestedPricetxt.Text = (Convert.ToDecimal(TotalCalculateBtxt.Text) + Convert.ToDecimal(TotalCalculateCtxt.Text)).ToString("0.00");
        }

        protected void FactoryExpencePertxt_TextChanged(object sender, EventArgs e)
        {
            FactoryExpenceAmttxt.Text = ((Convert.ToDecimal(TotalCalculateBtxt.Text) * (Convert.ToDecimal(FactoryExpencePertxt.Text))) / 100).ToString("0.00");
            TotalCalculateCtxt.Text = (Convert.ToDecimal(MarketedByChargesAmttxt.Text) + Convert.ToDecimal(FactoryExpenceAmttxt.Text) + Convert.ToDecimal(ProfitAmttxt.Text) + Convert.ToDecimal(OtherAmttxt.Text)).ToString("0.00");
            FinalSuggestedPricetxt.Text = (Convert.ToDecimal(TotalCalculateBtxt.Text) + Convert.ToDecimal(TotalCalculateCtxt.Text)).ToString("0.00");

        }

        protected void ProfitPertxt_TextChanged(object sender, EventArgs e)
        {
            ProfitAmttxt.Text = ((Convert.ToDecimal(TotalCalculateBtxt.Text) * (Convert.ToDecimal(ProfitPertxt.Text))) / 100).ToString("0.00");
            TotalCalculateCtxt.Text = (Convert.ToDecimal(MarketedByChargesAmttxt.Text) + Convert.ToDecimal(FactoryExpenceAmttxt.Text) + Convert.ToDecimal(ProfitAmttxt.Text) + Convert.ToDecimal(OtherAmttxt.Text)).ToString("0.00");
            FinalSuggestedPricetxt.Text = (Convert.ToDecimal(TotalCalculateBtxt.Text) + Convert.ToDecimal(TotalCalculateCtxt.Text)).ToString("0.00");

        }

        protected void OtherPertxt_TextChanged(object sender, EventArgs e)
        {
            OtherAmttxt.Text = ((Convert.ToDecimal(TotalCalculateBtxt.Text) * (Convert.ToDecimal(OtherPertxt.Text))) / 100).ToString("0.00");
            TotalCalculateCtxt.Text = (Convert.ToDecimal(MarketedByChargesAmttxt.Text) + Convert.ToDecimal(FactoryExpenceAmttxt.Text) + Convert.ToDecimal(ProfitAmttxt.Text) + Convert.ToDecimal(OtherAmttxt.Text)).ToString("0.00");
            FinalSuggestedPricetxt.Text = (Convert.ToDecimal(TotalCalculateBtxt.Text) + Convert.ToDecimal(TotalCalculateCtxt.Text)).ToString("0.00");

        }

        protected void FinalPricePerUnittxt_TextChanged(object sender, EventArgs e)
        {
            ProfitFinalPricetxt.Text = (Convert.ToDecimal(FinalPricePerUnittxt.Text) - (Convert.ToDecimal(TotalCalculateBtxt.Text))).ToString("0.00");
            PerCent_ProfitFinalPricetxt.Text = ((Convert.ToDecimal(ProfitFinalPricetxt.Text) * 100) / (Convert.ToDecimal(FinalPricePerUnittxt.Text))).ToString("0.00");


            if (Convert.ToDecimal(lblPackSize.Text) < 1000 && (Convert.ToInt32(lblPackMeasurement.Text) == 7 || Convert.ToInt32(lblPackMeasurement.Text) == 6))
            {
                decimal ConvertLtrToGmOrMl = (1000 / (Convert.ToDecimal(lblPackSize.Text)));
                FinalPriceLtrKgtxt.Text = (Convert.ToDecimal(FinalPricePerUnittxt.Text) * ConvertLtrToGmOrMl).ToString("0.00");
                ProfitOnFinalPricetxt.Text = (Convert.ToDecimal(ProfitFinalPricetxt.Text) * ConvertLtrToGmOrMl).ToString("0.00");

                PercentOfProfitOnFinalPricetxt.Text = (((Convert.ToDecimal(ProfitOnFinalPricetxt.Text) * 100) / Convert.ToDecimal(FinalPriceLtrKgtxt.Text))).ToString("0.00");
            }
            if (Convert.ToDecimal(lblPackSize.Text) > 1 && (Convert.ToInt32(lblPackMeasurement.Text) == 1 || Convert.ToInt32(lblPackMeasurement.Text) == 2))
            {
                decimal ConvertLtrToGmOrMl = ((Convert.ToDecimal(lblPackSize.Text)));
                FinalPriceLtrKgtxt.Text = (Convert.ToDecimal(FinalPricePerUnittxt.Text) / ConvertLtrToGmOrMl).ToString("0.00");
                ProfitOnFinalPricetxt.Text = (Convert.ToDecimal(ProfitFinalPricetxt.Text) / ConvertLtrToGmOrMl).ToString("0.00");

                PercentOfProfitOnFinalPricetxt.Text = ((Convert.ToDecimal(ProfitOnFinalPricetxt.Text) * 100) / (Convert.ToDecimal(FinalPriceLtrKgtxt.Text))).ToString("0.00");

            }
            if (Convert.ToDecimal(lblPackSize.Text) == 1 && (Convert.ToInt32(lblPackMeasurement.Text) == 1 || Convert.ToInt32(lblPackMeasurement.Text) == 2))
            {
                decimal ConvertLtrToGmOrMl = ((Convert.ToDecimal(lblPackSize.Text)));
                FinalPriceLtrKgtxt.Text = (Convert.ToDecimal(FinalPricePerUnittxt.Text) * ConvertLtrToGmOrMl).ToString("0.00");
                ProfitOnFinalPricetxt.Text = (Convert.ToDecimal(ProfitFinalPricetxt.Text) * ConvertLtrToGmOrMl).ToString("0.00");

                PercentOfProfitOnFinalPricetxt.Text = (((Convert.ToDecimal(ProfitOnFinalPricetxt.Text) * 100) / Convert.ToDecimal(FinalPriceLtrKgtxt.Text))).ToString("0.00");

            }
        }

        protected void AddOtherCompanyPrice_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(BulkProductNameDropDownList.SelectedIndex) > 0 && Convert.ToInt32(BulkPackSize.SelectedIndex) > 0)
            {

                ProOtherCompanyPriceList pro = new ProOtherCompanyPriceList();
                ClsOtherPriceListMaster cls = new ClsOtherPriceListMaster();
                pro.Company_Id = Convert.ToInt32(lblCompany_Id.Text);
                pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                pro.Pack_Size = Convert.ToDecimal(lblPackSize.Text);
                pro.Pack_Measurement = Convert.ToInt32(lblPackMeasurement.Text);
                pro.PM_RM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
                pro.Bulk_Interest_Percent = Convert.ToDecimal(BulkInterestPertxt.Text);
                pro.PM_Additional_Buffer = Convert.ToDecimal(AddBuffPMtxt.Text);
                pro.Labour_Additional_Buffer = Convert.ToDecimal(AddBuffLabourtxt.Text);
                pro.PackLoss_Percent = Convert.ToDecimal(LossPertxt.Text);
                pro.MarketByCharge_Percent = Convert.ToDecimal(MarketedByChargesPertxt.Text);
                pro.FactoryExpence_Percent = Convert.ToDecimal(FactoryExpencePertxt.Text);
                pro.Other_Percent = Convert.ToDecimal(OtherPertxt.Text);
                pro.Profit_Percent = Convert.ToDecimal(ProfitPertxt.Text);
                pro.FinalPricePerUnit = Convert.ToDecimal(FinalPricePerUnittxt.Text);
                pro.Status = "Actual";


                int status = cls.Insert_OtherCompanyPriceList(pro);

                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Added Sucessfully !')", true);
                    ClearAll();
                    GetOtherComapnyActualPriceList();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Failed !')", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Bulk Product and Packsize')", true);

            }

        }

        protected void UpdateOtherCompanyPrice_Click(object sender, EventArgs e)
        {
            ProOtherCompanyPriceList pro = new ProOtherCompanyPriceList();
            ClsOtherPriceListMaster cls = new ClsOtherPriceListMaster();
            pro.OtherComapnyPriceList_ID = lblOtherCompanyPrice_Id.Text;
            pro.Company_Id = Company_Id;
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Pack_Size = Convert.ToDecimal(lblPackSize.Text);
            pro.Pack_Measurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.PM_RM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.Bulk_Interest_Percent = Convert.ToDecimal(BulkInterestPertxt.Text);
            pro.PM_Additional_Buffer = Convert.ToDecimal(AddBuffPMtxt.Text);
            pro.Labour_Additional_Buffer = Convert.ToDecimal(AddBuffLabourtxt.Text);
            pro.PackLoss_Percent = Convert.ToDecimal(LossPertxt.Text);
            pro.MarketByCharge_Percent = Convert.ToDecimal(MarketedByChargesPertxt.Text);
            pro.FactoryExpence_Percent = Convert.ToDecimal(FactoryExpencePertxt.Text);
            pro.Other_Percent = Convert.ToDecimal(OtherPertxt.Text);
            pro.Profit_Percent = Convert.ToDecimal(ProfitPertxt.Text);
            pro.FinalPricePerUnit = Convert.ToDecimal(FinalPricePerUnittxt.Text);
            pro.Status = "Actual";
            int status = cls.Update_OtherCompanyPriceList(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Updated Sucessfully !')", true);
                ClearAll();
                GetOtherComapnyActualPriceList();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Update Failed !')", true);

            }
            lblOtherCompanyPrice_Id.Text = "";
            BulkProductNameDropDownList.Enabled = true;
            BulkProductNameDropDownList.CssClass = "form-select";
        }

        protected void CancelOtherCompanyPrice_Click(object sender, EventArgs e)
        {
            BulkProductNameDropDownList.SelectedIndex = 0;

            //BulkProductNameDropDownList.SelectedValue = (dt.Rows[0]["Fk_BPM_Id"] + " (" + dt.Rows[0]["PM_RM_Category_id"] + ")").ToString();

            BulkPackSize.SelectedIndex = -1;
            BulkPackSize.Enabled = false;
            BoxSizetxt.Text = "0.00";
            BulkCostPerLtr.Text = "0.00";
            lblBPM_Id.Text = "";
            BulkInterestPertxt.Text = "0.00";
            BulkInterestAmttxt.Text = "0.00";
            TotalBulkCostWithInteresttxt.Text = "0.00";
            BulkCostPerUnittxt.Text = "0.00";
            PMtxt.Text = "0.00";
            PMTotaltxt.Text = "0.00";
            Labourtxt.Text = "0.00";
            AddBuffPMtxt.Text = "0.00";
            AddBuffLabourtxt.Text = "0.00";
            TotalLabourtxt.Text = "0.00";
            TotalCalculateAtxt.Text = "0.00";
            TotalCalculateBtxt.Text = "0.00";
            TotalCalculateCtxt.Text = "0.00";
            LossPertxt.Text = "0.00";
            LossAmttxt.Text = "0.00";

            MarketedByChargesPertxt.Text = "0.00";
            MarketedByChargesAmttxt.Text = "0.00";
            FactoryExpencePertxt.Text = "0.00";
            FactoryExpenceAmttxt.Text = "0.00";

            OtherPertxt.Text = "0.00";
            OtherAmttxt.Text = "0.00";

            ProfitPertxt.Text = "0.00";
            ProfitAmttxt.Text = "0.00";
            FinalPricePerUnittxt.Text = "0.00";
            FinalSuggestedPricetxt.Text = "0.00";

            ProfitFinalPricetxt.Text = "0.00";
            PercentOfProfitOnFinalPricetxt.Text = "0.00";
            FinalPriceLtrKgtxt.Text = "0.00";
            ProfitOnFinalPricetxt.Text = "0.00";
            PerCent_ProfitFinalPricetxt.Text = "0.00";

            UpdateOtherCompanyPrice.Visible = false;
            AddOtherCompanyPrice.Visible = true;

            lblOtherCompanyPrice_Id.Text = "";

            BulkProductNameDropDownList.Enabled = true;
            BulkProductNameDropDownList.CssClass = "form-select";
        }

        protected void EditCategoryMapping_Click(object sender, EventArgs e)
        {
            ProOtherCompanyPriceList pro = new ProOtherCompanyPriceList();
            ClsOtherPriceListMaster cls = new ClsOtherPriceListMaster();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            string OtherComapnyPriceList_ID = (Grid_OtherCompanyPriceMaster.DataKeys[gdrow.RowIndex].Value).ToString(); ;
            pro.OtherComapnyPriceList_ID = OtherComapnyPriceList_ID;
            lblOtherCompanyPrice_Id.Text = OtherComapnyPriceList_ID;
            pro.Company_Id = Company_Id;
            DataTable dt = new DataTable();
            BulkProductMasterCombo();
            dt = cls.Get_OtherCompanyActualPricelistById(pro);

            BulkProductNameDropDownList.SelectedValue = dt.Rows[0]["BPM_Id"].ToString() + " (" + dt.Rows[0]["PMRM_Category_Id"].ToString() + ")";

            BulkProductNameDropDownList_SelectedIndexChanged(null, null);
            //BulkProductNameDropDownList.SelectedValue = (dt.Rows[0]["Fk_BPM_Id"] + " (" + dt.Rows[0]["PM_RM_Category_id"] + ")").ToString();

            BoxSizetxt.Text = dt.Rows[0]["BoxSize"].ToString();
            lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();
            lblPMRM_Category_Id.Text = dt.Rows[0]["PMRM_Category_Id"].ToString();

            BulkInterestPertxt.Text = dt.Rows[0]["Bulk_Interest_Percent"].ToString();
            lblPackSize.Text = dt.Rows[0]["Pack_size"].ToString();
            lblPackMeasurement.Text = dt.Rows[0]["Pack_Measurement"].ToString();
            BulkPackSize.SelectedValue = dt.Rows[0]["Pack_Size"].ToString() + " (" + dt.Rows[0]["Pack_Measurement"].ToString() + ")";

            AddBuffPMtxt.Text = dt.Rows[0]["PM_Additional_Buffer"].ToString();
            AddBuffLabourtxt.Text = dt.Rows[0]["Labour_Additional_Buffer"].ToString();
            LossPertxt.Text = dt.Rows[0]["LossPercent"].ToString();
            MarketedByChargesPertxt.Text = dt.Rows[0]["MarketByCharge_Percent"].ToString();
            FactoryExpencePertxt.Text = dt.Rows[0]["FactoryExpence_Percent"].ToString();
            OtherPertxt.Text = dt.Rows[0]["Other_Percent"].ToString();
            ProfitPertxt.Text = dt.Rows[0]["Profit_Percent"].ToString();
            FinalPricePerUnittxt.Text = dt.Rows[0]["FinalPricePerUnit"].ToString();


            BulkCostPerLtr.Text = dt.Rows[0]["BulkCost"].ToString();
            BulkInterestAmttxt.Text = dt.Rows[0]["Intrest_Amount"].ToString();
            TotalBulkCostWithInteresttxt.Text = dt.Rows[0]["TotalBulkCost"].ToString();
            BulkCostPerUnittxt.Text = dt.Rows[0]["BulkCostPerUnit"].ToString();
            PMtxt.Text = dt.Rows[0]["PM"].ToString();
            PMTotaltxt.Text = dt.Rows[0]["TotalPM"].ToString();
            Labourtxt.Text = dt.Rows[0]["Labour"].ToString();
            TotalLabourtxt.Text = dt.Rows[0]["TotalLabour"].ToString();
            TotalCalculateAtxt.Text = dt.Rows[0]["SubTotalA"].ToString();
            TotalCalculateBtxt.Text = dt.Rows[0]["TotalB"].ToString();
            TotalCalculateCtxt.Text = dt.Rows[0]["Total"].ToString();
            LossAmttxt.Text = dt.Rows[0]["LossAmount"].ToString();

            MarketedByChargesAmttxt.Text = dt.Rows[0]["MarketedByCharge_Amount"].ToString();
            FactoryExpenceAmttxt.Text = dt.Rows[0]["FactoryExpence_Amount"].ToString();

            OtherAmttxt.Text = dt.Rows[0]["Other_Amount"].ToString();

            ProfitAmttxt.Text = dt.Rows[0]["Profit_Amount"].ToString();
            FinalSuggestedPricetxt.Text = dt.Rows[0]["SuggestedFinalTotalCost"].ToString();

            ProfitFinalPricetxt.Text = dt.Rows[0]["ProfitFinalPrice"].ToString();
            PercentOfProfitOnFinalPricetxt.Text = dt.Rows[0]["PercentOfProfitOnFinalPrice"].ToString();
            FinalPriceLtrKgtxt.Text = dt.Rows[0]["FinalPriceLtrKg"].ToString();
            ProfitOnFinalPricetxt.Text = dt.Rows[0]["ProfitOnFinalPrice"].ToString();
            PerCent_ProfitFinalPricetxt.Text = dt.Rows[0]["PerCent_ProfitFinalPricetxt"].ToString();

            UpdateOtherCompanyPrice.Visible = true;
            AddOtherCompanyPrice.Visible = false;

            BulkProductNameDropDownList.Enabled = false;
            BulkProductNameDropDownList.CssClass = "form-select";


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calculatePricelist('"+ BulkInterestPertxt .ClientID+ "');", true);
            //
        }

        protected void DelCategoryMapping_Click(object sender, EventArgs e)
        {
            ProOtherCompanyPriceList pro = new ProOtherCompanyPriceList();
            ClsOtherPriceListMaster cls = new ClsOtherPriceListMaster();
            Button DeleteBtn = sender as Button;
            GridViewRow gdrow = DeleteBtn.NamingContainer as GridViewRow;
            string OtherComapnyPriceList_ID =(Grid_OtherCompanyPriceMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.OtherComapnyPriceList_ID = OtherComapnyPriceList_ID;
            pro.Company_Id = Company_Id;
            lblOtherCompanyPrice_Id.Text = OtherComapnyPriceList_ID.ToString();
            DataTable dt = new DataTable();
             dt = cls.Get_OtherCompanyActualPricelistById(pro);

            lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();
            lblPMRM_Category_Id.Text = dt.Rows[0]["PMRM_Category_Id"].ToString();
            pro.PM_RM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Company_Id = Company_Id;
            int status = cls.Delete_OtherComapnyPriceList(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                GetOtherComapnyActualPriceList();
                ClearAll();            }
        }
        public void ClearAll()
        {
            BulkProductNameDropDownList.SelectedIndex = 0;

            //BulkProductNameDropDownList.SelectedValue = (dt.Rows[0]["Fk_BPM_Id"] + " (" + dt.Rows[0]["PM_RM_Category_id"] + ")").ToString();

            BulkPackSize.SelectedIndex = -1;
            BoxSizetxt.Text = "0.00";
            BulkCostPerLtr.Text = "0.00";
            lblBPM_Id.Text = "";
            BulkInterestPertxt.Text = "0.00";
            BulkInterestAmttxt.Text = "0.00";
            TotalBulkCostWithInteresttxt.Text = "0.00";
            BulkCostPerUnittxt.Text = "0.00";
            PMtxt.Text = "0.00";
            PMTotaltxt.Text = "0.00";
            Labourtxt.Text = "0.00";
            AddBuffPMtxt.Text = "0.00";
            AddBuffLabourtxt.Text = "0.00";
            TotalLabourtxt.Text = "0.00";
            TotalCalculateAtxt.Text = "0.00";
            TotalCalculateBtxt.Text = "0.00";
            TotalCalculateCtxt.Text = "0.00";
            LossPertxt.Text = "0.00";
            LossAmttxt.Text = "0.00";

            MarketedByChargesPertxt.Text = "0.00";
            MarketedByChargesAmttxt.Text = "0.00";
            FactoryExpencePertxt.Text = "0.00";
            FactoryExpenceAmttxt.Text = "0.00";

            OtherPertxt.Text = "0.00";
            OtherAmttxt.Text = "0.00";

            ProfitPertxt.Text = "0.00";
            ProfitAmttxt.Text = "0.00";
            FinalPricePerUnittxt.Text = "0.00";
            FinalSuggestedPricetxt.Text = "0.00";

            ProfitFinalPricetxt.Text = "0.00";
            PercentOfProfitOnFinalPricetxt.Text = "0.00";
            FinalPriceLtrKgtxt.Text = "0.00";
            ProfitOnFinalPricetxt.Text = "0.00";
            PerCent_ProfitFinalPricetxt.Text = "0.00";

            UpdateOtherCompanyPrice.Visible = false;
            AddOtherCompanyPrice.Visible = true;
        }
    }
}
