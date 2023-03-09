using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Text.RegularExpressions;

namespace Production_Costing_Software
{
    public partial class FactoryExpenseMaster : System.Web.UI.Page
    {
        ProFactoryExpenseMaster pro = new ProFactoryExpenseMaster();
        int User_Id;
        string Shipper_Id;
        string Get_BPM_Id;
        string PackSizeBracket;
        string PackSize;
        string PackUniMeasurement;
        decimal LiterConvert;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();

            if (!IsPostBack)
            {

                hideUnitMeasurement.Visible = false;
                BulkProductDropDownListCombo();
                //PackingSizeDropDownListData();
                Grid_FactoryExpenseMasterData();
                hidePackingSize.Visible = false;
                HideTradeNameDiv.Visible = false;
            }
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);

        }
        public void BulkProductDropDownListCombo()
        {
            ClsProductwiseLabourCost cls = new ClsProductwiseLabourCost();
            ProProductWiseLabourCostMaster pro = new ProProductWiseLabourCostMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            dt = cls.Get_ProductwiseLabourCost(User_Id);
            dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + Fk_PM_RM_Category_Id + ')'+'['+Packing_Size+'-'+Measurement+ ']'").ToString();
            lblPackSize.Text = dt.Rows[0]["Packing_Size"].ToString();
            lblPackMeasurement.Text = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();

            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BPM_Product_Name";
            BulkProductDropDownList.DataSource = dt;
            BulkProductDropDownList.DataTextField = "BPM_Product_Name";

            BulkProductDropDownList.DataValueField = "BPMValue";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }
        public void PackingSizeDropDownListData()
        {
            try
            {
                ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
                ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
                DataTable dt = new DataTable();
                pro.User_Id = User_Id;

                dt = cls.sp_Get_FactoryExpenceBy_BPMId(Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text), decimal.Parse(PackSize), (PackUniMeasurement));
                dt.Columns.Add("PackingSize", typeof(string), "Packing_Size + ' ' + PackMeasurement + ''").ToString();
                PackingSizeDropDown.DataSource = dt;
                PackingSizeDropDown.DataTextField = "PackingSize";
                PackingSizeDropDown.DataValueField = "Fk_UnitMeasurement_Id";
                PackingSizeDropDown.DataBind();
                PackingSizeDropDown.Items.Insert(0, "Select");
                lblPackMeasurement.Text = dt.Rows[0]["Packing_Size"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BulkProductDropDownList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (BulkProductDropDownList.SelectedValue != "Select")
            {

                string Shippertype_Id = BulkProductDropDownList.SelectedValue.ToString();

                Shipper_Id = Shippertype_Id.Split('(', ')')[1];
                PackSizeBracket = Shippertype_Id.Split('(', ')')[2];
                PackSize = Regex.Match(PackSizeBracket, @"\d+").Value;
                lblPackSize.Text = PackSize;
                lblPMRM_Category_Id.Text = Shipper_Id;
                PackUniMeasurement = PackSizeBracket.Split('-', ']')[1];
                lblPackMeasurement.Text = PackUniMeasurement;
                Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
                lbl_BPM_Id.Text = Get_BPM_Id;
                PackingSizeDropDownListData();

                lbl_BPM_Id.Text = Get_BPM_Id;
                DataTable dt = new DataTable();
                ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();


                dt = cls.sp_Get_FactoryExpenceBy_BPMId(Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text), decimal.Parse(PackSize), (PackUniMeasurement));
                if (dt.Rows.Count > 0)
                {
                    TradeNametxt.Text = dt.Rows[0]["TradeName"].ToString();
                    lblPackMeasurement.Text = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();
                    lblPackSize.Text = dt.Rows[0]["Packing_Size"].ToString();
                    if (lblPackMeasurement.Text == "7" || lblPackMeasurement.Text == "6")
                    {
                        lblBulkCostUnit.Text = dt.Rows[0]["FinalCostBulk"].ToString();
                        BulkCostPerLtrtxt.Text = ((decimal.Parse(lblPackSize.Text) * (decimal.Parse(lblBulkCostUnit.Text))) / (decimal.Parse("1000"))).ToString("0.000");
                    }
                    else if (decimal.Parse(lblPackSize.Text) > decimal.Parse("1.000"))
                    {
                        lblBulkCostUnit.Text = dt.Rows[0]["FinalCostBulk"].ToString();
                        if (lblPackMeasurement.Text == "1" || lblPackMeasurement.Text == "2")
                        {
                            decimal ConvertToLtr;
                            ConvertToLtr = (decimal.Parse("1000")) * (decimal.Parse(lblPackSize.Text) / decimal.Parse("1000"));
                            BulkCostPerLtrtxt.Text =  (decimal.Parse(lblBulkCostUnit.Text) / ConvertToLtr).ToString("0.000");

                        }
                    }
                    else{
                        BulkCostPerLtrtxt.Text = dt.Rows[0]["FinalCostBulk"].ToString();
                        lblBulkCostUnit.Text = dt.Rows[0]["FinalCostBulk"].ToString();
                    }

                    //PackingLossAmountSpanId.InnerText = "Packing Loss (" + dt.Rows[0]["Fk_PackingLoss"].ToString() + "%)";
                    lblpackingLossPer.Text = dt.Rows[0]["Fk_PackingLoss"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Fk_PackingLoss"].ToString()))
                    {
                        PackingLossAmounttxt.Text = (((decimal.Parse(BulkCostPerLtrtxt.Text) + decimal.Parse(TotalPMCostPerUnittxt.Text) + decimal.Parse(LabourCostPerLtrtxt.Text)) * Convert.ToDecimal(dt.Rows[0]["Fk_PackingLoss"].ToString())) / 100).ToString("0.000");
                    }
                    else
                    {
                        PackingLossAmounttxt.Text = "0";
                    }
                    TotalPMCostPerUnittxt.Text = dt.Rows[0]["TotalPMCost_Unit"].ToString();
                    LabourCostPerLtrtxt.Text = dt.Rows[0]["NetLabourCostLtr"].ToString();

                    if (PackingLossAmounttxt.Text != "")
                    {
                        if (BulkCostPerLtrtxt.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('BulkCost/Ltr Not Found!')", true);
                            return;
                        }
                        else if (TotalPMCostPerUnittxt.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TotalPMCostPerUnit Not Found!')", true);
                            return;

                        }
                        else if (LabourCostPerLtrtxt.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('LabourCost/Ltr Not Found!')", true);
                            return;

                        }
                        else
                        {
                            TotalCostingtxt.Text = (decimal.Parse(BulkCostPerLtrtxt.Text) + (decimal.Parse(TotalPMCostPerUnittxt.Text)) +
                          (decimal.Parse(LabourCostPerLtrtxt.Text)) + ((decimal.Parse(PackingLossAmounttxt.Text)))).ToString("0.000");
                            TotalAmountUnittxt.Text = (decimal.Parse(TotalCostingtxt.Text)).ToString("0.000");
                        }


                        if (lblPackMeasurement.Text == "6" || lblPackMeasurement.Text == "7")
                        {
                            LiterConvert = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
                            TotalAmountLtrtxt.Text = (LiterConvert * (decimal.Parse(TotalAmountUnittxt.Text))).ToString("0.000");

                        }
                        else
                        {
                            TotalAmountLtrtxt.Text = (decimal.Parse(TotalAmountUnittxt.Text)).ToString();
                        }
                        if (decimal.Parse(lblPackSize.Text) > decimal.Parse("1.000"))
                        {
                            if (lblPackMeasurement.Text == "1" || lblPackMeasurement.Text == "2")
                            {
                                decimal ConvertToLtr;
                                ConvertToLtr = (decimal.Parse("1000")) * (decimal.Parse(lblPackSize.Text) / decimal.Parse("1000"));
                                TotalAmountLtrtxt.Text = ((decimal.Parse(TotalAmountUnittxt.Text)) / ConvertToLtr).ToString();
                            }

                        }

                    }


                    PackingSizeDropDown.DataSource = dt;

                    PackingSizeDropDown.DataTextField = "PackMeasurement";
                    PackingSizeDropDown.DataValueField = "Fk_UnitMeasurement_Id";
                    PackingSizeDropDown.DataBind();
                    //PackingSizeDropDown.Items.Insert(0, "Select");

                }


            }
            else if (BulkProductDropDownList.SelectedValue == "Select")
            {
                ClearData();
            }



        }

        //protected void FactoryExpencetxt_TextChanged(object sender, EventArgs e)
        //{

        //    if (FactoryExpencetxt.Text != "" || FactoryExpencetxt.Text != "0")
        //    {
        //        FactoryExpenceAmtUnittxt.Text = (decimal.Parse(FactoryExpencetxt.Text) * (decimal.Parse(TotalCostingtxt.Text)) / decimal.Parse("100")).ToString("0.000");
        //        TotalAmountUnittxt.Text = ((decimal.Parse(TotalCostingtxt.Text)) + (decimal.Parse(FactoryExpenceAmtUnittxt.Text))).ToString("0.000");
        //        TotalAmountLtrtxt.Text = (decimal.Parse(TotalAmountUnittxt.Text)).ToString("0.000");
        //        if (lblPackMeasurement.Text == "6" || lblPackMeasurement.Text == "7")
        //        {
        //            LiterConvert = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
        //            TotalAmountLtrtxt.Text = (LiterConvert * (decimal.Parse(TotalAmountUnittxt.Text))).ToString("0.000");
        //        }



        //        if (decimal.Parse(lblPackSize.Text) > decimal.Parse("1.000"))
        //        {
        //            if (lblPackMeasurement.Text == "1" || lblPackMeasurement.Text == "2")
        //            {
        //                decimal ConvertToLtr;
        //                ConvertToLtr = (decimal.Parse("1000")) * (decimal.Parse(lblPackSize.Text) / decimal.Parse("1000"));
        //                TotalAmountLtrtxt.Text = ((decimal.Parse(TotalAmountUnittxt.Text)) / ConvertToLtr).ToString();
        //            }

        //        }

        //    }
        //    //else
        //    //{
        //    //    TotalAmountUnittxt.Text = (decimal.Parse(TotalCostingtxt.Text)).ToString();

        //    //}
        //}
        protected void Addbtn_Click(object sender, EventArgs e)
        {
            pro.Fk_BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
            pro.TradeName = TradeNametxt.Text;
            pro.PackingSize = decimal.Parse(lblPackSize.Text);
            pro.PackingMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.FinalBulkCostPerUnit = decimal.Parse(BulkCostPerLtrtxt.Text);
            pro.FinalPMCostPerUnit = decimal.Parse(TotalPMCostPerUnittxt.Text);
            pro.LabourChargePer = decimal.Parse(LabourCostPerLtrtxt.Text);
            pro.PackingLossAmtPerUnit = decimal.Parse(PackingLossAmounttxt.Text);
            pro.TotalCosting = decimal.Parse(TotalCostingtxt.Text);
            pro.FactoryExpencePercent = decimal.Parse(FactoryExpencetxt.Text);
            pro.FactoryExpenceAmtPerUnit = decimal.Parse(FactoryExpenceAmtUnittxt.Text);
            pro.TotalAmtPerUnit = decimal.Parse(TotalAmountUnittxt.Text);
            pro.TotalAmtPerLiter = decimal.Parse(TotalAmountLtrtxt.Text);
            pro.Fk_PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);

            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            int status = cls.INSERT_FactoryExpenceMaster(pro);

            if (status > 0)
            {
                //System.Threading.Thread.Sleep(5000);

                //ClearMaterialData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_FactoryExpenseMasterData();
                ClearData();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Fail')", true);

            }
        }
        public void Grid_FactoryExpenseMasterData()
        {
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            Grid_FactoryExpenseMaster.DataSource = cls.Get_FactoryExpenceAll();
            Grid_FactoryExpenseMaster.DataBind();
        }
        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            pro.Fk_BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
            pro.TradeName = TradeNametxt.Text;
            pro.PackingSize = decimal.Parse(lblPackSize.Text);
            pro.PackingMeasurement = Convert.ToInt32(PackingSizeDropDown.SelectedValue);
            pro.FinalBulkCostPerUnit = decimal.Parse(BulkCostPerLtrtxt.Text);
            pro.FinalPMCostPerUnit = decimal.Parse(TotalPMCostPerUnittxt.Text);
            pro.LabourChargePer = decimal.Parse(LabourCostPerLtrtxt.Text);
            pro.PackingLossAmtPerUnit = decimal.Parse(PackingLossAmounttxt.Text);
            pro.TotalCosting = decimal.Parse(TotalCostingtxt.Text);
            pro.FactoryExpencePercent = decimal.Parse(FactoryExpencetxt.Text);
            pro.FactoryExpenceAmtPerUnit = decimal.Parse(FactoryExpenceAmtUnittxt.Text);
            pro.TotalAmtPerUnit = decimal.Parse(TotalAmountUnittxt.Text);
            pro.TotalAmtPerLiter = decimal.Parse(TotalAmountLtrtxt.Text);
            pro.Fk_PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.FactoryExpenseMaster_Id = Convert.ToInt32(lblFactoryExpence_Id.Text);
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            int status = cls.UPDATE_FactoryExpenceMaster(pro);

            if (status > 0)
            {
                //System.Threading.Thread.Sleep(5000);

                //ClearMaterialData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_FactoryExpenseMasterData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Fail')", true);
                Updatebtn.Visible = true;
                Addbtn.Visible = false;
            }
        }

        protected void DelFactoryExpenseBtn_Click(object sender, EventArgs e)
        {
            Button DEL = sender as Button;
            GridViewRow gdrow = DEL.NamingContainer as GridViewRow;
            int FactoryExpence_Id = Convert.ToInt32(Grid_FactoryExpenseMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            DataTable dt = new DataTable();
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            pro.FactoryExpenseMaster_Id = FactoryExpence_Id;

            int status = cls.DELETE_FactoryExpenseMaster(pro);
            if (status > 0)
            {
                Grid_FactoryExpenseMasterData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed!')", true);

            }
        }

        protected void EditFactoryExpenseBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int FactoryExpence_Id = Convert.ToInt32(Grid_FactoryExpenseMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            lblFactoryExpence_Id.Text = FactoryExpence_Id.ToString();
            DataTable dt = new DataTable();
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            pro.FactoryExpenseMaster_Id = FactoryExpence_Id;
            BulkProductDropDownListCombo();
            dt = cls.Get_FactoryExpenceById(FactoryExpence_Id);
            string BPM_Dropdown = dt.Rows[0]["Fk_BPM_Id"].ToString() + " (" + dt.Rows[0]["Fk_PMRM_Category_Id"].ToString() + ")[" + dt.Rows[0]["PackingSize"].ToString() + "-" + dt.Rows[0]["Pack_UnitMeasurement"].ToString() + "]";
            BulkProductDropDownList.SelectedValue = BPM_Dropdown;

            PackSize = dt.Rows[0]["PackingSize"].ToString();
            PackUniMeasurement = dt.Rows[0]["Pack_UnitMeasurement"].ToString();

            //lblPackMeasurement.Text = PackingSizeDropDown.SelectedValue.ToString();
            //lblPMRM_Category_Id.Text = lbl_BPM_Id.Text.Split('(', ')')[1];

            //Get_BPM_Id = lbl_BPM_Id.Text.Substring(0, 3).Trim();
            //lbl_BPM_Id.Text = Get_BPM_Id;
            lblPMRM_Category_Id.Text = dt.Rows[0]["Fk_PMRM_Category_Id"].ToString();
            lbl_BPM_Id.Text = dt.Rows[0]["Fk_BPM_Id"].ToString();
            PackingSizeDropDownListData();

            PackingSizeDropDown.SelectedValue = Convert.ToInt32(dt.Rows[0]["PackingMeasurement"]).ToString();
            BulkProductDropDownList_SelectedIndexChanged1(null, null);
            FactoryExpencetxt.Text = dt.Rows[0]["FactoryExpencePercent"].ToString();
            Updatebtn.Visible = true;
            Addbtn.Visible = false;

        }
        public void ClearData()
        {
            PackingSizeDropDown.SelectedIndex = 0;

            TradeNametxt.Text = "";
            BulkProductDropDownList.SelectedIndex = 0;
            BulkCostPerLtrtxt.Text = "0";
            TotalPMCostPerUnittxt.Text = "0";
            LabourCostPerLtrtxt.Text = "0";
            PackingLossAmounttxt.Text = "0";
            TotalCostingtxt.Text = "0";
            FactoryExpencetxt.Text = "0";
            FactoryExpenceAmtUnittxt.Text = "0";
            TotalAmountUnittxt.Text = "0";
            TotalAmountLtrtxt.Text = "0";
            lblFactoryExpence_Id.Text = "0";
            Updatebtn.Visible = false;
            Addbtn.Visible = true;
            PackingSizeDropDown.SelectedItem.Text = "";
            lblBulkCostUnit.Text = "";
            lblpackingLossPer.Text = "0";
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            PackingSizeDropDown.SelectedIndex = 0;


            TradeNametxt.Text = "";
            BulkProductDropDownList.SelectedIndex = 0;

            BulkCostPerLtrtxt.Text = "0";
            TotalPMCostPerUnittxt.Text = "0";
            LabourCostPerLtrtxt.Text = "0";
            PackingLossAmounttxt.Text = "0";
            TotalCostingtxt.Text = "0";
            FactoryExpencetxt.Text = "0";
            FactoryExpenceAmtUnittxt.Text = "0";
            TotalAmountUnittxt.Text = "0";
            TotalAmountLtrtxt.Text = "0";
            lblFactoryExpence_Id.Text = "0";
            Updatebtn.Visible = false;
            Addbtn.Visible = true;
            PackingSizeDropDown.SelectedItem.Text = "";
            lblBulkCostUnit.Text = "";
            lblpackingLossPer.Text = "0";

        }

        protected void Grid_FactoryExpenseMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_FactoryExpenseMaster.PageIndex = e.NewPageIndex;
            Grid_FactoryExpenseMasterData();
        }

        //protected void BulkProductDropDownList_TextChanged(object sender, EventArgs e)
        //{
        //    if (BulkProductDropDownList.SelectedValue != "Select")
        //    {

        //        string Shippertype_Id = BulkProductDropDownList.SelectedValue.ToString();

        //        Shipper_Id = Shippertype_Id.Split('(', ')')[1];
        //        lblPMRM_Category_Id.Text = Shipper_Id;
        //        Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
        //        //Get_BPM_Id = Shippertype_Id.Substring(0,5).Trim();
        //        lbl_BPM_Id.Text = Get_BPM_Id;
        //        PackingSizeDropDownListData();

        //        lbl_BPM_Id.Text = Get_BPM_Id;
        //        DataTable dt = new DataTable();
        //        ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();


        //        dt = cls.sp_Get_FactoryExpenceBy_BPMId(Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text), decimal.Parse(PackSize), (PackUniMeasurement));
        //        if (dt.Rows.Count > 0)
        //        {
        //            TradeNametxt.Text = dt.Rows[0]["TradeName"].ToString();
        //            //lblPackMeasurement.Text = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();
        //            lblPackSize.Text = dt.Rows[0]["Packing_Size"].ToString();
        //            if (lblPackMeasurement.Text == "7" || lblPackMeasurement.Text == "6")
        //            {
        //                lblBulkCostUnit.Text = dt.Rows[0]["FinalCostBulk"].ToString();
        //                BulkCostPerLtrtxt.Text = ((decimal.Parse(lblPackSize.Text) * (decimal.Parse(lblBulkCostUnit.Text))) / (decimal.Parse("1000"))).ToString();
        //            }
        //            else
        //            {
        //                BulkCostPerLtrtxt.Text = dt.Rows[0]["FinalCostBulk"].ToString();
        //            }




        //            PackingLossAmounttxt.Text = dt.Rows[0]["Fk_PackingLoss"].ToString();
        //            TotalPMCostPerUnittxt.Text = dt.Rows[0]["TotalPMCost_Unit"].ToString();
        //            LabourCostPerLtrtxt.Text = dt.Rows[0]["NetLabourCostLtr"].ToString();

        //            if (PackingLossAmounttxt.Text != "")
        //            {
        //                TotalCostingtxt.Text = (decimal.Parse(BulkCostPerLtrtxt.Text) + (decimal.Parse(TotalPMCostPerUnittxt.Text)) +
        //                    (decimal.Parse(LabourCostPerLtrtxt.Text)) + ((decimal.Parse(PackingLossAmounttxt.Text) + decimal.Parse(("10"))))).ToString();
        //            }


        //            PackingSizeDropDown.DataSource = dt;

        //            PackingSizeDropDown.DataTextField = "PackMeasurement";
        //            PackingSizeDropDown.DataValueField = "Fk_UnitMeasurement_Id";
        //            PackingSizeDropDown.DataBind();
        //            //PackingSizeDropDown.Items.Insert(0, "Select");

        //        }


        //    }
        //    else if (BulkProductDropDownList.SelectedValue == "Select")
        //    {
        //        ClearData();
        //    }
        //}
    }
}