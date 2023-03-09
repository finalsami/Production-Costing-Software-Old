using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class FinishGoodsPricingReport : System.Web.UI.Page
    {
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

                Grid_FactoryExpenseMasterData();
                DisplayView();
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
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[5]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[5]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[5]["CanDelete"]).ToString();


        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[5]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[5]["CanEdit"]).ToString();

        }
        public void Grid_FactoryExpenseMasterData()
        {
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            Grid_FinishGoodsPricingReport.DataSource = cls.Get_FinishGoodsPricingReportGrid();
            Grid_FinishGoodsPricingReport.DataBind();
        }

        protected void ReportPopupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Button Edit = sender as Button;
                GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
                int FactoryExpence_Id = Convert.ToInt32(Grid_FinishGoodsPricingReport.DataKeys[gdrow.RowIndex].Value.ToString());

                DataTable dt = new DataTable();
                DataTable objdtPackSize = new DataTable();
                DataTable objdtCategoryName = new DataTable();
                DataTable objdtOtherCosting = new DataTable();
                DataTable dt4 = new DataTable();
                ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();

                dt = cls.Get_BPM_Id_By_FactoryExpenceId(FactoryExpence_Id);
                lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();
                lblname.Text = dt.Rows[0]["BPM_Product_Name"].ToString();
                string Packsize = dt.Rows[0]["PackingSize"].ToString();
                string PackUnitMeasurement = dt.Rows[0]["Measurement"].ToString();
                String ShipperName = dt.Rows[0]["PM_RM_Category_Name"].ToString();
                lblPackingSize.Text = Regex.Match(Packsize, @"\d+").Value;
                dt = cls.Get_FinishGoodPricingReportBy_BPM_Id(Convert.ToInt32(lblBPM_Id.Text));
                objdtPackSize = cls.Get_PackingMeasuremnt_FinishGoodsReport(Convert.ToInt32(lblBPM_Id.Text));
                objdtCategoryName = cls.Get_PMCategory_finishedGood_Report(Convert.ToInt32(lblBPM_Id.Text));
                objdtOtherCosting = cls.Get_OtherCosting_Report(Convert.ToInt32(lblBPM_Id.Text));
                StringBuilder htmlTable = new StringBuilder();
                //htmlTable.Append("<th>");
                //htmlTable.Append("<td>" + lblname.Text + "</td></th>");
                htmlTable.Append("Name:<div><h5>" + dt.Rows[0]["BPM_Product_Name"].ToString() + "(" + ShipperName + ")-" + "[" + lblPackingSize.Text + "-" + PackUnitMeasurement + "]</h5></div><div><table border='1' style='width:100%'>");
                htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th>No</th><th>IngredientName</th><th>Formulation(%)</th><th>QTY</th><th>RateAmount_KG</th><th>TransportRate</th><th>Amount</th></tr>");

                if (!object.Equals(dt.Rows[0], null))
                {
                    if (dt.Rows.Count > 0)
                    {
                        string InputQty = "0.00";
                        string Rate = "0.00";
                        string Amount = "0.00";
                        string strFormulation = "0.00";
                        string TransportRate = "0.00";

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int RowIndex = 1 + i;
                            //**************************************RM Name *************************************
                            htmlTable.Append("<tr style='color: White;'>");
                            htmlTable.Append("<td>" + RowIndex + "</td>");

                            htmlTable.Append("<td>" + dt.Rows[i]["IngredientName"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["Formulation"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["QTY"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["RateAmount_KG"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["TransportRate"] + "</td>");

                            htmlTable.Append("<td>" + dt.Rows[i]["Amount"] + "</td>");
                            strFormulation = (decimal.Parse(strFormulation) + decimal.Parse(dt.Rows[i]["Formulation"].ToString())).ToString();
                            InputQty = (decimal.Parse(InputQty) + decimal.Parse(dt.Rows[i]["QTY"].ToString())).ToString();
                            Rate = (decimal.Parse(Rate) + decimal.Parse(dt.Rows[i]["RateAmount_KG"].ToString())).ToString();
                            TransportRate = (decimal.Parse(TransportRate) + decimal.Parse(dt.Rows[i]["TransportRate"].ToString())).ToString();
                            Amount = (decimal.Parse(Amount) + decimal.Parse(dt.Rows[i]["Amount"].ToString())).ToString();
                            htmlTable.Append("</tr>");
                        }
                        htmlTable.Append("<tr class='table-hover table-responsive gridview overflow-scroll' ><th>Total</th><th></th>");
                        htmlTable.Append("<th>" + strFormulation + "</th>");
                        htmlTable.Append("<th>" + InputQty + "</th>");
                        htmlTable.Append("<th>" + Rate + "</th>");
                        htmlTable.Append("<th>" + TransportRate + "</th>");
                        htmlTable.Append("<th>" + Amount + "</th>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</table><br></div>");

                        //*********************************************************

                        //****************Formulation************************

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        if (!string.IsNullOrEmpty(dt.Rows[0]["FM_Name"].ToString()))
                        {
                            htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                            htmlTable.Append("<th style='width:80%;text-align: left;' >Formulation (" + dt.Rows[0]["FM_Name"] + "), Batch Size (" + dt.Rows[0]["BatchSize"] + ")</th><th style='width:30%'>" + dt.Rows[0]["FM_Total_Cost"] + "</th></tr>");
                        }

                        //****************SPGR************************************************

                        htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>SPGR</th>");
                        htmlTable.Append("<th style='width:30%;'>" + dt.Rows[0]["SPGR"] + "</th>");
                        htmlTable.Append("</tr>");
                        //****************Total OutPut************************************************

                        htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Final OutPut</th>");
                        htmlTable.Append("<th style='width:30%;'>" + dt.Rows[0]["TotalOutput_LTR"] + "</th>");
                        htmlTable.Append("</tr>");
                        //****************Final Caost / L OR kg.************************                        
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Final Bulk Cost/Ltr or Kg</th>");
                        htmlTable.Append("<th style='width:30%;'>" + dt.Rows[0]["FinalCostBulk"] + "</th>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Interest</th>");
                        htmlTable.Append("<th style='width:30%;'> " + dt.Rows[0]["InterestAmount"] + "(" + dt.Rows[0]["Interest"] + "%)" + "</th>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Final Cost/Ltr or Kg</th>");
                        double dblFinalCost = (String.IsNullOrEmpty(dt.Rows[0]["InterestAmount"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["InterestAmount"].ToString())) + (String.IsNullOrEmpty(dt.Rows[0]["FinalCostBulk"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["FinalCostBulk"].ToString()));
                        htmlTable.Append("<th style='width:30%;'> " + dblFinalCost.ToString() + "</th>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</table><br></div>");

                        //****************PM Costing************************

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>PM Costing / Unit</th>");
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Packing Size</th>");

                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            if (ShipperName != "Shipper")
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + "  [" + objdtPackSize.Rows[i]["PM_RM_Category_Name"] + "] " + " </th>");

                            }
                            else
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");

                            }
                        }
                        htmlTable.Append("</tr>");

                        double[] dblarray = new double[objdtPackSize.Rows.Count];

                        for (int i = 0;  i < objdtCategoryName.Rows.Count; i++)
                        {
                            htmlTable.Append("<tr><td>" + objdtCategoryName.Rows[i]["PM_RM_Category_Name"] + " </td>");
                            for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                            {
                                try
                                {
                                    string strPMCost = string.Empty;
                                    DataTable objdt = cls.GET_PMCostingForReport(Convert.ToInt32(lblBPM_Id.Text),
                                                                        Convert.ToInt32(objdtCategoryName.Rows[i]["Fk_PMRM_Category_Id"]),
                                                                        Convert.ToInt32(objdtPackSize.Rows[j]["Pack_size"]),
                                                                        Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"])
                                                                        );
                                    if (objdt != null && objdt.Rows.Count > 0)
                                    {

                                        // Code added by Harshul Patel on 06-05-20222 for calcuation for multiple row return from database and made a sum of Final_Pack_Cost_Unit

                                        if (objdt.Rows.Count >= 2)
                                        {
                                            strPMCost = objdt.Compute("SUM(Final_Pack_Cost_Unit)", string.Empty).ToString();
                                            dblarray[j] = dblarray[j] + (string.IsNullOrEmpty(strPMCost) ? 0 : Convert.ToDouble(strPMCost));
                                            htmlTable.Append("<td>" + strPMCost + " </td>");
                                        }
                                        else
                                        {

                                            strPMCost = objdt.Rows[0]["Final_Pack_Cost_Unit"].ToString();
                                            dblarray[j] = dblarray[j] + (string.IsNullOrEmpty(strPMCost) ? 0 : Convert.ToDouble(strPMCost));
                                            htmlTable.Append("<td>" + strPMCost + " </td>");
                                        }

                                        //strPMCost = objdt.Rows[0]["Final_Pack_Cost_Unit"].ToString();
                                        //dblarray[j] = dblarray[j] + (string.IsNullOrEmpty(strPMCost) ? 0 : Convert.ToDouble(strPMCost));
                                        //htmlTable.Append("<td>" + strPMCost + " </td>");


                                    }
                                    else
                                    {
                                        strPMCost = "-";
                                        //dblarray[j] = dblarray[j] + (string.IsNullOrEmpty(strPMCost) ? 0 : Convert.ToDouble(strPMCost));
                                        htmlTable.Append("<td>" + strPMCost + " </td>");
                                    }


                                }
                                catch (Exception ex)
                                {
                                    throw ex;

                                }
                            }
                            htmlTable.Append("</tr>");
                        }

                        htmlTable.Append("<tr class='table-hover table-responsive gridview overflow-scroll'><th>Total</th>");
                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            htmlTable.Append("<th>" + dblarray[i].ToString() + " </th>");
                        }
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</table><br></div>");

                        //****************Other Costing************************

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Other Costing /Unit</th>");
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Packing Size</th>");

                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            if (ShipperName != "Shipper")
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + "  [" + objdtPackSize.Rows[i]["PM_RM_Category_Name"] + "] " + " </th>");

                            }
                            else
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");

                            }
                        }
                        htmlTable.Append("</tr>");

                        double[] dblTotalOC = new double[objdtPackSize.Rows.Count];

                        htmlTable.Append("<tr><td>Labour</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strLabourChargePer = string.Empty;
                            var varLabourChargePer = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varLabourChargePer.Any())
                            {
                                strLabourChargePer = varLabourChargePer.CopyToDataTable().Rows[0]["LabourChargePer"].ToString();
                                dblTotalOC[j] = dblTotalOC[j] + (string.IsNullOrEmpty(strLabourChargePer) ? 0 : Convert.ToDouble(strLabourChargePer));
                                htmlTable.Append("<td>" + strLabourChargePer + " </td>");
                            }
                            else
                            {
                                strLabourChargePer = "-";
                                //dblTotalOC[j] = dblTotalOC[j] + (string.IsNullOrEmpty(strLabourChargePer) ? 0 : Convert.ToDouble(strLabourChargePer));
                                htmlTable.Append("<td>" + strLabourChargePer + " </td>");
                            }

                        }
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<tr><td>Packing Loss</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strPackingLossAmtPerUnit = string.Empty;
                            var varPackingLossAmtPerUnit = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varPackingLossAmtPerUnit.Any())
                            {
                                strPackingLossAmtPerUnit = varPackingLossAmtPerUnit.CopyToDataTable().Rows[0]["PackingLossAmtPerUnit"].ToString();
                                dblTotalOC[j] = dblTotalOC[j] + (string.IsNullOrEmpty(strPackingLossAmtPerUnit) ? 0 : Convert.ToDouble(strPackingLossAmtPerUnit));
                                htmlTable.Append("<td>" + strPackingLossAmtPerUnit + " </td>");
                            }
                            else
                            {
                                strPackingLossAmtPerUnit = "-";
                                //dblTotalOC[j] = dblTotalOC[j] + (string.IsNullOrEmpty(strPackingLossAmtPerUnit) ? 0 : Convert.ToDouble(strPackingLossAmtPerUnit));
                                htmlTable.Append("<td>" + strPackingLossAmtPerUnit + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");

                        //htmlTable.Append("<tr><td>Factory Expence</td>");
                        //for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        //{
                        //    string strFactoryExpenceAmtPerUnit = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"])).CopyToDataTable().Rows[0]["FactoryExpenceAmtPerUnit"].ToString();
                        //    dblTotalOC[j] = dblTotalOC[j] + (string.IsNullOrEmpty(strFactoryExpenceAmtPerUnit) ? 0 : Convert.ToDouble(strFactoryExpenceAmtPerUnit));
                        //    htmlTable.Append("<td>" + strFactoryExpenceAmtPerUnit + " </td>");
                        //}
                        //htmlTable.Append("</tr>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview overflow-scroll'><th>Total</th>");
                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            htmlTable.Append("<th>" + dblTotalOC[i].ToString() + " </th>");
                        }
                        htmlTable.Append("</tr></table><br></div>");

                        //****************Final Cost Packing Wise ( BULK + PM + Other )************************

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Final Cost Packing Wise ( BULK + PM + Other )</th>");
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Packing Size</th>");

                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            if (ShipperName!="Shipper")
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] +"  ["+ objdtPackSize.Rows[i]["PM_RM_Category_Name"] + "] "+ " </th>");

                            }
                            else
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");

                            }
                        }
                        htmlTable.Append("</tr>");

                        double[] dblTotalPerLtr = new double[objdtPackSize.Rows.Count];
                        htmlTable.Append("<tr><td>Per Ltr Costing</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strTotalAmtPerLiter = string.Empty;
                            var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]) && r.Field<int>("Fk_PM_RM_Category_Id") == Convert.ToInt32(objdtPackSize.Rows[j]["PM_RM_Category_id"]));
                            if (varTotalAmtPerLiter.Any())
                            {
                                strTotalAmtPerLiter = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["TotalAmtPerLiter"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strTotalAmtPerLiter + " </td>");
                            }
                            else
                            {
                                strTotalAmtPerLiter = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strTotalAmtPerLiter + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<tr><td>Per Unit Costing</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strTotalAmtPerUnit = string.Empty;

                            var varTotalAmtPerUnit = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varTotalAmtPerUnit.Any())
                            {
                                strTotalAmtPerUnit = varTotalAmtPerUnit.CopyToDataTable().Rows[0]["TotalAmtPerUnit"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerUnit) ? 0 : Convert.ToDouble(strTotalAmtPerUnit));
                                htmlTable.Append("<td>" + strTotalAmtPerUnit + " </td>");
                            }
                            else
                            {
                                strTotalAmtPerUnit = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerUnit) ? 0 : Convert.ToDouble(strTotalAmtPerUnit));
                                htmlTable.Append("<td>" + strTotalAmtPerUnit + " </td>");
                            }

                        }
                        //htmlTable.Append("</tr>");
                        //htmlTable.Append("<tr class='table-hover table-responsive gridview overflow-scroll'><th>Total</th>");
                        //for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        //{
                        //    htmlTable.Append("<th>" + dblTotalPerLtr[i].ToString() + " </th>");
                        //}
                        htmlTable.Append("</tr></table><br></div>");

                        //**************************************************************************

                        //****************Packing Difference from Master Pack************************

                        decimal MasterPrice = 0;
                        string strMasterPackingName = string.Empty;
                        var varIsMaster = objdtOtherCosting.AsEnumerable().Where(r => r.Field<bool>("IsMasterPacking") == true);

                        if (varIsMaster.Any())
                        {
                            var varMasterPrice = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(varIsMaster.CopyToDataTable().Rows[0]["PackingMeasurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(varIsMaster.CopyToDataTable().Rows[0]["PackingSize"]));
                            if (varMasterPrice.Any())
                            {
                                MasterPrice = Convert.ToDecimal(varMasterPrice.CopyToDataTable().Rows[0]["TotalAmtPerLiter"].ToString());
                            }


                            var varstrMasterPackingName = objdtPackSize.AsEnumerable().Where(r => r.Field<int>("Pack_Measurement") == Convert.ToInt32(varIsMaster.CopyToDataTable().Rows[0]["PackingMeasurement"]) && r.Field<decimal>("Pack_size") == Convert.ToDecimal(varIsMaster.CopyToDataTable().Rows[0]["PackingSize"]));
                            if (varMasterPrice.Any())
                            {
                                strMasterPackingName = Convert.ToString(varstrMasterPackingName.CopyToDataTable().Rows[0]["PackUnitMeasurement"].ToString());
                            }

                        }

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Packing Difference from Master Pack (" + strMasterPackingName + ")</th>");
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Packing Size</th>");

                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            if (ShipperName != "Shipper")
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + "  [" + objdtPackSize.Rows[i]["PM_RM_Category_Name"] + "] " + " </th>");

                            }
                            else
                            {
                                htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");

                            }
                        }
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<tr><td>Difference</td>");

                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strTotalAmtPerLiter = string.Empty;
                            var TotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (TotalAmtPerLiter.Any())
                            {
                                strTotalAmtPerLiter = TotalAmtPerLiter.CopyToDataTable().Rows[0]["TotalAmtPerLiter"].ToString();

                                // code added by harshul 15-4-2022
                                /*MasterPrice- totalamount*/

                                //htmlTable.Append("<td>" + (MasterPrice - Convert.ToDecimal((string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter))) ) + " </td>");

                               htmlTable.Append("<td>" + ( Convert.ToDecimal((string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter)))- MasterPrice ) + " </td>");

                            }
                            else
                            {
                                htmlTable.Append("<td>-</td>");


                            }
                        }
                        htmlTable.Append("</tr></table><br></div>");

                        DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                    else
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td align='center' colspan='4'>There is no Record.</td>");
                        htmlTable.Append("</tr>");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}