using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class FinishedGoodsMaster : System.Web.UI.Page
    {
        ClsFinishedGoodsMaster cls = new ClsFinishedGoodsMaster();
        ProFinishedGoodsMaster pro = new ProFinishedGoodsMaster();
        int User_Id;

        int BPM_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            if (!Page.IsPostBack)
            {
                MainCategoryCombo();
                EnumMasterMeasurementMeasurement();
                BulkProductDropDownListCombo();
                //Packingsizecombo();
                Grid_FinishedGoodsMasterData();
                BulkProductDropDownList.Enabled = false;
            }
            hideUnitMeasurement.Visible = false;
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);

        }
        public void Grid_FinishedGoodsMasterData()
        {

            Grid_FinishedGoodsMaster.DataSource = cls.Get_FinishedGoodsMaster(User_Id);

            Grid_FinishedGoodsMaster.DataBind();

        }
        public void Packingsizecombo()
        {
            ClsPackingMateriaMaster clsPack = new ClsPackingMateriaMaster();
            DataTable dtPack = new DataTable();
            PackingSizeDropDown.DataSource = dtPack = clsPack.Get_FinalGoodsPackSizeByBPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text));
            lblpackMeasurement.Text = dtPack.Rows[0]["Pack_Measurement"].ToString();
            lblPacksize.Text = dtPack.Rows[0]["Pack_size"].ToString();
            //UnitMeasurementDropdown.SelectedValue = dtPack.Rows[0]["Pack_Measurement"].ToString();

            //dtPack.Columns.Add("PackingSize", typeof(string), "Pack_size + ' ' + PackUnitMeasure +''").ToString();
            PackingSizeDropDown.DataTextField = "PackingSize";
            PackingSizeDropDown.DataValueField = "Pack_size";
            PackingSizeDropDown.DataBind();
            PackingSizeDropDown.Items.Insert(0, "Select");
        }
        public void BulkProductDropDownListCombo()
        {
            ClsBulkProductMaster cls = new ClsBulkProductMaster();
            DataTable dt = new DataTable();
            dt = cls.Get_BP_MasterDataCombo(User_Id);
            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BulkProductName";

            BulkProductDropDownList.DataSource = dvOptions;
            BulkProductDropDownList.DataTextField = "BulkProductName";
            BulkProductDropDownList.DataValueField = "BPM_Product_Id";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }
        public void MainCategoryCombo()
        {
            ClsMainCategoryMaster cls = new ClsMainCategoryMaster();
            MainCategoryDropdown.DataSource = cls.GetMainCategoryData();

            MainCategoryDropdown.DataTextField = "MainCategory_Name";
            MainCategoryDropdown.DataValueField = "PkMainCategory_Id";


            MainCategoryDropdown.DataBind();
            MainCategoryDropdown.Items.Insert(0, "Select");
        }
        public void EnumMasterMeasurementMeasurement()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeasurementDropdown.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeasurementDropdown.DataTextField = "EnumDescription";
            UnitMeasurementDropdown.DataValueField = "PkEnumId";


            UnitMeasurementDropdown.DataBind();

        }


        protected void PackingSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dtPacking = new DataTable();
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            if (PackingSizeDropDown.SelectedValue != "Select")
            {
                decimal Pack_size = decimal.Parse(PackingSizeDropDown.SelectedValue);
                dtPacking = cls.Get_SubPackingMaterialMasterByPackSize_BPM_Id(Convert.ToInt32(lblBPM_Id.Text), Pack_size);
                if (dtPacking.Rows.Count > 0)
                {
                    lblPacksize.Text = dtPacking.Rows[0]["Pack_size"].ToString();
                    lblpackMeasurement.Text = dtPacking.Rows[0]["Pack_Measurement"].ToString();

                }
                //*************************************************

                if (MainCategoryDropdown.SelectedValue != "Select")
                {

                    ClsFinishedGoodsMaster clsCheckBPM = new ClsFinishedGoodsMaster();
                    DataTable dtCheckBPM = new DataTable();
                    dtCheckBPM = clsCheckBPM.Get_FinishedGoodsMasterByPackSize_BPM_Id(Convert.ToInt32(lblBPM_Id.Text), decimal.Parse(lblPacksize.Text), Convert.ToInt32(lblpackMeasurement.Text));

                    if (dtCheckBPM.Rows.Count > 0)
                    {
                        lblBPM_Id.Text = Convert.ToInt32(dtCheckBPM.Rows[0]["Fk_BPM_Id"]).ToString();
                        lblFinishedGoods_Id.Text = Convert.ToInt32(dtCheckBPM.Rows[0]["FinishedGoods_Id"]).ToString();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Finished Goods Already Created. You Can Update!')", true);
                        EditUpdate();

                    }

                    else
                    {


                        DataTable dt = new DataTable();
                        ClsBulkRecipeBOM clsRM = new ClsBulkRecipeBOM();

                        dt = clsRM.Get_FinalCostBulk_BRBOMby_BPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text));
                        if (dt.Rows.Count > 0)
                        {
                            BulkCostPerLtrtxt.Text = Convert.ToDecimal(dt.Rows[0]["FinalCostBulk"]).ToString("0.000");
                            if (BulkCostPerLtrtxt.Text == "")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Labour Cost /Ltr Not Found!'", true);
                            }
                        }
                        ClsPackingMateriaMaster clsPacking = new ClsPackingMateriaMaster();
                        ProPackingMaterialMaster proPacking = new ProPackingMaterialMaster();
                        proPacking.User_Id = User_Id;
                        proPacking.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                        proPacking.Pack_size = decimal.Parse(lblPacksize.Text);
                        proPacking.Pack_Unit_Measurement = Convert.ToInt32(lblpackMeasurement.Text);
                        dt = clsPacking.Get_FinalPackingMaterialMasterByPackSize_BPM_Id(proPacking);

                        if (dt.Rows.Count > 0)
                        {
                            proPacking.Final_Pack_Cost_Unit = decimal.Parse(dt.Rows[0]["TotalPMCost_Unit"].ToString());
                            proPacking.Total_Product_PM_Cost_Ltr = decimal.Parse(dt.Rows[0]["TotalPMCost_Ltr"].ToString());
                            TotalPMCostPerUnittxt.Text = (proPacking.Final_Pack_Cost_Unit).ToString("0.000");
                            TotalPMCostPerLtrtxt.Text = (proPacking.Total_Product_PM_Cost_Ltr).ToString("0.000");
                            DataTable dt1 = new DataTable();
                            ClsProductwiseLabourCost clsLabour = new ClsProductwiseLabourCost();
                            dt1 = clsLabour.Get_ProductwiseLabourCostByPackSize_BPM_Id(decimal.Parse(lblPacksize.Text), Convert.ToInt32(lblpackMeasurement.Text), Convert.ToInt32(lblBPM_Id.Text));
                            if (dt1.Rows.Count > 0)
                            {
                                LabourCostPerLtrtxt.Text = Convert.ToDecimal(dt1.Rows[0]["NetLabourCostLtr"]).ToString("0.000");
                                if (LabourCostPerLtrtxt.Text == "")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Labour Cost /Ltr Not Found!'", true);

                                }
                            }
                            if (BulkCostPerLtrtxt.Text != "" || BulkCostPerLtrtxt.Text != "" && LabourCostPerLtrtxt.Text != "" || LabourCostPerLtrtxt.Text != "" && TotalPMCostPerLtrtxt.Text != "" || TotalPMCostPerLtrtxt.Text != "")
                            {
                                if (TotalPMCostPerLtrtxt.Text == "")
                                {
                                    TotalPMCostPerLtrtxt.Text = "0";
                                }
                                FinishedGoodsPerLtrtxt.Text = (decimal.Parse(BulkCostPerLtrtxt.Text) + decimal.Parse(LabourCostPerLtrtxt.Text) + decimal.Parse(TotalPMCostPerLtrtxt.Text)).ToString();

                            }

                        }
                        if (PackingSizeDropDown.SelectedValue != "Select")
                        {
                            if (lblpackMeasurement.Text == "1")
                            {
                                UnitPerLtrtxt.Text = lblPacksize.Text;
                            }
                            else if (lblpackMeasurement.Text == "2")
                            {
                                UnitPerLtrtxt.Text = lblPacksize.Text;

                            }
                            else if (lblpackMeasurement.Text == "6" || lblpackMeasurement.Text == "7")
                            {
                                UnitPerLtrtxt.Text = (decimal.Parse("1000") / decimal.Parse(lblPacksize.Text)).ToString();

                            }
                            if (PackingLossAmounttxt.Text == "0" || PackingLossAmounttxt.Text == "")
                            {
                                PackingLossAmounttxt.Text = "0";
                                NetFinishedGoodsCostPerLtrtxt.Text = (decimal.Parse(FinishedGoodsPerLtrtxt.Text) + Decimal.Parse(PackingLossAmounttxt.Text)).ToString("0.000");
                                NetFinishedGoodsCostPerUnittxt.Text = (decimal.Parse(NetFinishedGoodsCostPerLtrtxt.Text) / decimal.Parse(UnitPerLtrtxt.Text)).ToString("0.000");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Packing Size!')", true);

                        }
                    }
                }
            }

        }


        protected void BulkProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsBulkRecipeBOM cls = new ClsBulkRecipeBOM();

            if (BulkProductDropDownList.SelectedValue != "Select")
            {
                BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
                lblBPM_Id.Text = (BulkProductDropDownList.SelectedValue).ToString();
                Packingsizecombo();
                PackingSizeDropDown.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Bulk product')", true);
                PackingSizeDropDown.Enabled = false;

            }
            //if (MainCategoryDropdown.SelectedValue != "Select")
            //{

            //    ClsFinishedGoodsMaster clsCheckBPM = new ClsFinishedGoodsMaster();
            //    DataTable dtCheckBPM = new DataTable();
            //    dtCheckBPM = clsCheckBPM.Get_FinishedGoodsMasterByBPM_Id(BPM_Id);

            //    if (dtCheckBPM.Rows.Count > 0)
            //    {
            //        CheckBPM = Convert.ToInt32(dtCheckBPM.Rows[0]["Fk_BPM_Id"]);
            //        lblBPM_Id.Text = Convert.ToInt32(dtCheckBPM.Rows[0]["FinishedGoods_Id"]).ToString();
            //        if (CheckBPM == BPM_Id)
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Finished Goods Already Created. You Can Update!')", true);
            //            EditUpdate();
            //        }
            //        else
            //        {
            //            lblBPM_Id.Text = BPM_Id.ToString();
            //            DataTable dtPacking = new DataTable();
            //            ProPackingMaterialMaster proPacking = new ProPackingMaterialMaster();
            //            pro.Fk_User_Id = User_Id;
            //            pro.Fk_BPM_Id = Convert.ToInt32(lblBPM_Id.Text);

            //            PackingSizeDropDown.DataSource = dtPacking;
            //            PackingSizeDropDown.DataBind();



            //            ClsPackingMateriaMaster clsPacking = new ClsPackingMateriaMaster();
            //            dtPacking = clsPacking.Get_SubPackingMaterialMasterById(proPacking);



            //            DataTable dt = new DataTable();
            //            ClsRMMaster clsRM = new ClsRMMaster();


            //            dt = cls.Get_FinalCostBulk_BRBOMby_BPM_Id(User_Id, BPM_Id);
            //            if (dt.Rows.Count > 0)
            //            {
            //                BulkCostPerLtrtxt.Text = Convert.ToDecimal(dt.Rows[0]["FinalCostBulk"]).ToString("0.000");
            //            }



            //        }
            //    }

            //    else
            //    {
            //        lblBPM_Id.Text = BPM_Id.ToString();
            //        DataTable dtPacking = new DataTable();
            //        ClsProductwiseLabourCost clsProduct = new ClsProductwiseLabourCost();
            //        ProPackingMaterialMaster proPacking = new ProPackingMaterialMaster();
            //        pro.Fk_User_Id = User_Id;
            //        pro.Fk_BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            //        dtPacking = clsProduct.Get_ProductwiseLabourCostByBPM_Id(User_Id, Convert.ToInt32(lblBPM_Id.Text));
            //        PackingSizeDropDown.DataSource = dtPacking;
            //        PackingSizeDropDown.DataBind();
            //        PackingSizeDropDown.Enabled = true;
            //        Packingsizecombo();


            //        ClsPackingMateriaMaster clsPacking = new ClsPackingMateriaMaster();
            //        dtPacking = clsPacking.Get_SubPackingMaterialMasterById(proPacking);



            //        DataTable dt = new DataTable();
            //        ClsRMMaster clsRM = new ClsRMMaster();


            //        dt = cls.Get_FinalCostBulk_BRBOMby_BPM_Id(User_Id, BPM_Id);
            //        if (dt.Rows.Count > 0)
            //        {
            //            BulkCostPerLtrtxt.Text = Convert.ToDecimal(dt.Rows[0]["FinalCostBulk"]).ToString("0.000");
            //        }


            //        proPacking.User_Id = User_Id;
            //        proPacking.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            //        dt = clsPacking.Get_FinalPackingMaterialMaster(proPacking);

            //        if (dt.Rows.Count > 0)
            //        {
            //            proPacking.Final_Pack_Cost_Unit = decimal.Parse(dt.Rows[0]["TotalPMCost_Unit"].ToString());
            //            proPacking.Total_Product_PM_Cost_Ltr = decimal.Parse(dt.Rows[0]["TotalPMCost_Ltr"].ToString());
            //            TotalPMCostPerUnittxt.Text = (proPacking.Final_Pack_Cost_Unit).ToString("0.000");
            //            TotalPMCostPerLtrtxt.Text = (proPacking.Total_Product_PM_Cost_Ltr).ToString("0.000");
            //            DataTable dt1 = new DataTable();
            //            ClsProductwiseLabourCost clsLabour = new ClsProductwiseLabourCost();
            //            dt1 = clsLabour.Get_ProductwiseLabourCostByBPM_Id(User_Id, BPM_Id);
            //            if (dt1.Rows.Count > 0)
            //            {
            //                LabourCostPerLtrtxt.Text = Convert.ToDecimal(dt1.Rows[0]["NetLabourCostLtr"]).ToString("0.000");
            //            }
            //            if (BulkCostPerLtrtxt.Text != "" || BulkCostPerLtrtxt.Text != "" && LabourCostPerLtrtxt.Text != "" || LabourCostPerLtrtxt.Text != "" && TotalPMCostPerLtrtxt.Text != "" || TotalPMCostPerLtrtxt.Text != "")
            //            {
            //                if (TotalPMCostPerLtrtxt.Text == "")
            //                {
            //                    TotalPMCostPerLtrtxt.Text = "0";
            //                }
            //                FinishedGoodsPerLtrtxt.Text = (decimal.Parse(BulkCostPerLtrtxt.Text) + decimal.Parse(LabourCostPerLtrtxt.Text) + decimal.Parse(TotalPMCostPerLtrtxt.Text)).ToString();

            //            }

            //        }
            //    }
            //}





        }





        protected void PackingLossPercenttxt_TextChanged(object sender, EventArgs e)
        {
            if (PackingLossPercenttxt.Text != "" || PackingLossPercenttxt.Text != "0")
            {
                if (FinishedGoodsPerLtrtxt.Text != "" || FinishedGoodsPerLtrtxt.Text != "0")
                {
                    PackingLossAmounttxt.Text = (decimal.Parse(FinishedGoodsPerLtrtxt.Text) * decimal.Parse((PackingLossPercenttxt.Text)) / decimal.Parse("100")).ToString("0.000");

                }
                NetFinishedGoodsCostPerLtrtxt.Text = (decimal.Parse(FinishedGoodsPerLtrtxt.Text) + Decimal.Parse(PackingLossAmounttxt.Text)).ToString("0.000");
                NetFinishedGoodsCostPerUnittxt.Text = (decimal.Parse(NetFinishedGoodsCostPerLtrtxt.Text) / decimal.Parse(UnitPerLtrtxt.Text)).ToString("0.000");
            }
            else
            {
                NetFinishedGoodsCostPerLtrtxt.Text = (decimal.Parse(FinishedGoodsPerLtrtxt.Text) + Decimal.Parse(PackingLossAmounttxt.Text)).ToString("0.000");
                NetFinishedGoodsCostPerUnittxt.Text = (decimal.Parse(NetFinishedGoodsCostPerLtrtxt.Text) / decimal.Parse(UnitPerLtrtxt.Text)).ToString("0.000");
            }

        }

        protected void Addbtn_Click(object sender, EventArgs e)
        {
            pro.Fk_BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            pro.Fk_MainCategory_Id = Convert.ToInt32(MainCategoryDropdown.SelectedValue);
            pro.Fk_PackingSize = decimal.Parse(lblPacksize.Text);
            pro.Fk_UnitMeasuremnt_Id = Convert.ToInt32(lblpackMeasurement.Text);
            pro.PackSize = decimal.Parse(lblPacksize.Text);
            pro.PackingDescription = PackingDescriptiontxt.Text;
            pro.UnitPerLtr = decimal.Parse(UnitPerLtrtxt.Text);
            pro.BulkCostPerLtr = decimal.Parse(BulkCostPerLtrtxt.Text);
            pro.TotalPMCostPerUnit = decimal.Parse(TotalPMCostPerUnittxt.Text);
            pro.TotalPMCostPerLtr = decimal.Parse(TotalPMCostPerLtrtxt.Text);
            pro.LabourCostPerLtr = decimal.Parse(LabourCostPerLtrtxt.Text);
            pro.FinishedGoodsPerLtr = decimal.Parse(FinishedGoodsPerLtrtxt.Text);
            pro.PackingPerLossPer = decimal.Parse(PackingLossPercenttxt.Text);
            pro.PackingLossAmt = decimal.Parse(PackingLossAmounttxt.Text);
            pro.NetFinishedGoodsCostPerLtr = decimal.Parse(NetFinishedGoodsCostPerLtrtxt.Text);
            pro.NetFinishedGoodsCostPerUnit = decimal.Parse(NetFinishedGoodsCostPerUnittxt.Text);
            if (PMDiffrenceFrom1LToBelowtxt.Text != "")
            {
                pro.PMDifferenceFrom1LBelow = decimal.Parse(PMDiffrenceFrom1LToBelowtxt.Text);

            }
            else
            {
                pro.PMDifferenceFrom1LBelow = 0;

            }

            pro.Fk_User_Id = User_Id;
            int status = cls.Insert_FinishedGoodsMaster(pro);

            if (status > 0)
            {
                ClearMaterialData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_FinishedGoodsMasterData();

                Addbtn.Visible = true;
                Updatebtn.Visible = false;

            }
            //DataTable dt = new DataTable();
            //dt = cls.Get_SubPackingMaterialMaster(pro);
            //lblPAckMaterial_Id.Text = Convert.ToInt32(dt.Rows[0]["Pack_Material_Id"]).ToString();
            //lblPAckMaterialMeasurement.Text = Convert.ToInt32(dt.Rows[0]["Pack_Measurement"]).ToString();

        }
        public void ClearMaterialData()
        {
            BulkProductDropDownList.SelectedIndex = -1;
            MainCategoryDropdown.SelectedIndex = -1;
            PackingSizeDropDown.SelectedIndex = -1;
            //UnitMeasurementDropdown.SelectedIndex =-1;
            PackingDescriptiontxt.Text = "";
            UnitPerLtrtxt.Text = "";
            BulkCostPerLtrtxt.Text = "";
            TotalPMCostPerUnittxt.Text = "";
            TotalPMCostPerLtrtxt.Text = "";
            LabourCostPerLtrtxt.Text = "";
            FinishedGoodsPerLtrtxt.Text = "";
            PackingLossPercenttxt.Text = "";
            PackingLossAmounttxt.Text = "";
            NetFinishedGoodsCostPerLtrtxt.Text = "";
            NetFinishedGoodsCostPerUnittxt.Text = "";
            PMDiffrenceFrom1LToBelowtxt.Text = "";
            lblBPM_Id.Text = "";
            lblFinishedGoods_Id.Text = "";
            lblpackMeasurement.Text = "";
            lblPacksize.Text = "";
            lblUnitMeasurement.Text = "";
            PackingSizeDropDown.Enabled = false;
        }

        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            pro.Finishedgoods_Id = Convert.ToInt32(lblFinishedGoods_Id.Text);

            pro.Fk_BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            pro.Fk_MainCategory_Id = Convert.ToInt32(MainCategoryDropdown.SelectedValue);
            pro.Fk_PackingSize = decimal.Parse(lblPacksize.Text);
            pro.Fk_UnitMeasuremnt_Id = Convert.ToInt32(PackingSizeDropDown.SelectedValue);
            pro.PackingDescription = PackingDescriptiontxt.Text;
            pro.UnitPerLtr = decimal.Parse(UnitPerLtrtxt.Text);
            pro.BulkCostPerLtr = decimal.Parse(BulkCostPerLtrtxt.Text);
            pro.TotalPMCostPerUnit = decimal.Parse(TotalPMCostPerUnittxt.Text);
            pro.TotalPMCostPerLtr = decimal.Parse(TotalPMCostPerLtrtxt.Text);
            pro.LabourCostPerLtr = decimal.Parse(LabourCostPerLtrtxt.Text);
            pro.FinishedGoodsPerLtr = decimal.Parse(FinishedGoodsPerLtrtxt.Text);
            pro.PackingPerLossPer = decimal.Parse(PackingLossPercenttxt.Text);
            pro.PackingLossAmt = decimal.Parse(PackingLossAmounttxt.Text);
            pro.NetFinishedGoodsCostPerLtr = decimal.Parse(NetFinishedGoodsCostPerLtrtxt.Text);
            pro.NetFinishedGoodsCostPerUnit = decimal.Parse(NetFinishedGoodsCostPerUnittxt.Text);
            if (PMDiffrenceFrom1LToBelowtxt.Text != "")
            {
                pro.PMDifferenceFrom1LBelow = decimal.Parse(PMDiffrenceFrom1LToBelowtxt.Text);

            }
            else
            {
                pro.PMDifferenceFrom1LBelow = 0;

            }

            pro.Fk_User_Id = User_Id;
            int status = cls.Update_FinishedGoodsMaster(pro);

            if (status > 0)
            {
                ClearMaterialData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_FinishedGoodsMasterData();
            }
            ClearMaterialData();
        }

        protected void DelFinishedGoodsMasterBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int FinishedGoods_Id = Convert.ToInt32(Grid_FinishedGoodsMaster.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.Delete_FinishedGoodsMaster(FinishedGoods_Id, User_Id);
            if (status > 0)
            {
                Grid_FinishedGoodsMasterData();
                ClearMaterialData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
        }

        protected void EditFinishedGoodsMasterBtn_Click(object sender, EventArgs e)
        {
            ClsFinishedGoodsMaster cls = new ClsFinishedGoodsMaster();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int FinishedGoods_ID = Convert.ToInt32(Grid_FinishedGoodsMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            lblFinishedGoods_Id.Text = FinishedGoods_ID.ToString();
            DataTable dt = new DataTable();
            dt = cls.Get_FinishedGoodsMasterById(User_Id, FinishedGoods_ID);
            if (dt.Rows.Count > 0)
            {
                Addbtn.Visible = false;
                Updatebtn.Visible = true;
                PackingSizeDropDown.Enabled = true;
                BulkProductDropDownList.SelectedValue = dt.Rows[0]["Fk_BPM_Id"].ToString();
                lblBPM_Id.Text = BulkProductDropDownList.SelectedValue;
                Packingsizecombo();

                MainCategoryDropdown.SelectedValue = dt.Rows[0]["Fk_MainCategory_Id"].ToString();
                PackingSizeDropDown.SelectedValue = dt.Rows[0]["Fk_PackingSize"].ToString();
                //PackingSize
                UnitMeasurementDropdown.SelectedValue = dt.Rows[0]["Fk_UnitMeasuremnt_Id"].ToString();
                PackingDescriptiontxt.Text = dt.Rows[0]["PackingDescription"].ToString();
                UnitPerLtrtxt.Text = dt.Rows[0]["UnitPerLtr"].ToString();
                BulkCostPerLtrtxt.Text = dt.Rows[0]["BulkCostPerLtr"].ToString();
                TotalPMCostPerUnittxt.Text = dt.Rows[0]["TotalPMCostPerUnit"].ToString();
                TotalPMCostPerLtrtxt.Text = dt.Rows[0]["TotalPMCostPerLtr"].ToString();
                LabourCostPerLtrtxt.Text = dt.Rows[0]["LabourCostPerLtr"].ToString();
                FinishedGoodsPerLtrtxt.Text = dt.Rows[0]["FinishedGoodsPerLtr"].ToString();
                PackingLossPercenttxt.Text = dt.Rows[0]["PackingPerLossPer"].ToString();
                PackingLossAmounttxt.Text = dt.Rows[0]["PackingLossAmt"].ToString();
                NetFinishedGoodsCostPerLtrtxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerLtr"].ToString();
                NetFinishedGoodsCostPerUnittxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerUnit"].ToString();
                NetFinishedGoodsCostPerUnittxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerUnit"].ToString();
                PMDiffrenceFrom1LToBelowtxt.Text = dt.Rows[0]["PMDifferenceFrom1LBelow"].ToString();
            }





        }
        public void EditUpdate()
        {
            ClsFinishedGoodsMaster cls = new ClsFinishedGoodsMaster();


            DataTable dt = new DataTable();
            dt = cls.Get_FinishedGoodsMasterById(User_Id, Convert.ToInt32(lblFinishedGoods_Id.Text));
            if (dt.Rows.Count > 0)
            {
                Addbtn.Visible = false;
                Updatebtn.Visible = true;
                PackingSizeDropDown.Enabled = true;
                BulkProductDropDownList.SelectedValue = dt.Rows[0]["Fk_BPM_Id"].ToString();
                lblBPM_Id.Text = BulkProductDropDownList.SelectedValue;
                Packingsizecombo();

                MainCategoryDropdown.SelectedValue = dt.Rows[0]["Fk_MainCategory_Id"].ToString();
                PackingSizeDropDown.SelectedValue = dt.Rows[0]["Fk_PackingSize"].ToString();
                //PackingSize
                UnitMeasurementDropdown.SelectedValue = dt.Rows[0]["Fk_UnitMeasuremnt_Id"].ToString();
                PackingDescriptiontxt.Text = dt.Rows[0]["PackingDescription"].ToString();
                UnitPerLtrtxt.Text = dt.Rows[0]["UnitPerLtr"].ToString();
                BulkCostPerLtrtxt.Text = dt.Rows[0]["BulkCostPerLtr"].ToString();
                TotalPMCostPerUnittxt.Text = dt.Rows[0]["TotalPMCostPerUnit"].ToString();
                TotalPMCostPerLtrtxt.Text = dt.Rows[0]["TotalPMCostPerLtr"].ToString();
                LabourCostPerLtrtxt.Text = dt.Rows[0]["LabourCostPerLtr"].ToString();
                FinishedGoodsPerLtrtxt.Text = dt.Rows[0]["FinishedGoodsPerLtr"].ToString();
                PackingLossPercenttxt.Text = dt.Rows[0]["PackingPerLossPer"].ToString();
                PackingLossAmounttxt.Text = dt.Rows[0]["PackingLossAmt"].ToString();
                NetFinishedGoodsCostPerLtrtxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerLtr"].ToString();
                NetFinishedGoodsCostPerUnittxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerUnit"].ToString();
                NetFinishedGoodsCostPerUnittxt.Text = dt.Rows[0]["NetFinishedGoodsCostPerUnit"].ToString();
                PMDiffrenceFrom1LToBelowtxt.Text = dt.Rows[0]["PMDifferenceFrom1LBelow"].ToString();
            }



        }
        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(Grid_FinishedGoodsMaster);

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ExportGridToExcel(Control GridView)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "finishedGoodsReport.xlsx"));
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Grid_FinishedGoodsMaster.AllowPaging = false;
            Grid_FinishedGoodsMasterData();
            //Change the Header Row back to white color
            Grid_FinishedGoodsMaster.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < Grid_FinishedGoodsMaster.HeaderRow.Cells.Count; i++)
            {
                Grid_FinishedGoodsMaster.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            Grid_FinishedGoodsMaster.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ClearMaterialData();
            PackingSizeDropDown.Enabled = false;
        }

        protected void MainCategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainCategoryDropdown.SelectedValue != "Select")
            {
                BulkProductDropDownList.Enabled = true;

            }
            else
            {
                BulkProductDropDownList.Enabled = false;
            }
        }

        protected void Grid_FinishedGoodsMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_FinishedGoodsMasterData();

            Grid_FinishedGoodsMaster.PageIndex = e.NewPageIndex;
            Grid_FinishedGoodsMaster.DataBind();
        }
    }
}