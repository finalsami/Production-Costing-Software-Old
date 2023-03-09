using BusinessAccessLayer;
using DataAccessLayer;
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

namespace Production_Costing_Software
{
    public partial class Report_PMRMEstimation : System.Web.UI.Page
    {
        int User_Id;
        int Company_Id;
        ProPMRMEstimateReport pro = new ProPMRMEstimateReport();
        ClsPMRMEstimateReport cls = new ClsPMRMEstimateReport();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            GetUserRights();
            if (!IsPostBack)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                DisplayView();
                GetPMRMIngredients();
                Grid_RMIngredientsEstimate();
                RMEstimateDropdownCombo();
            }
        }
        protected void Grid_RMIngredientsEstimate()
        {
            DataTable dtRM = new DataTable();
            pro.Company_Id = Company_Id;
            dtRM = cls.Get_Grid_PMRM_Estimate_ModifiedPrice(pro);
            Gird_PMRM_PriceEstimationList.DataSource = dtRM;
            Gird_PMRM_PriceEstimationList.DataBind();

        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[24]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[24]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[24]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[23]["CanEdit"]) == true)
            {
                if (lblGRidEstimateName.Text != "")
                {
                    AddModifiedPriceBtn.Visible = false;
                    ModifiedPriceUpdateBtn.Visible = true;
                    ViewPMRMEstimateReport.Visible = true;
                    AddPMRMEstimationBtn.Visible = true;
                }
                else
                {
                    AddModifiedPriceBtn.Visible = true;
                    ModifiedPriceUpdateBtn.Visible = false;
                    ViewPMRMEstimateReport.Visible = true;
                    AddPMRMEstimationBtn.Visible = true;

                }

            }
            else
            {
                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewPMRMEstimateReport.Visible = false;
                AddPMRMEstimationBtn.Visible = false;

            }

        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                User_Id = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);
                lblCompanyMasterList_Id.Text = Company_Id.ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        protected void GetPMRMIngredients()
        {
            DataTable dtRM = new DataTable();
            ClsPMRMEstimateReport cls = new ClsPMRMEstimateReport();
            dtRM = cls.Get_PMRMMasterWithData();
            PMRM_IngredientListbox.DataSource = dtRM;
            PMRM_IngredientListbox.DataTextField = "PM_RM_Name";
            PMRM_IngredientListbox.DataValueField = "PM_RM_Price_id";
            PMRM_IngredientListbox.DataBind();
            PMRM_IngredientListbox.Items.Insert(0, "Select");

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[23]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[23]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[23]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[23]["CanEdit"]) == true)
            {
                if (lblGRidEstimateName.Text != "")
                {
                    AddModifiedPriceBtn.Visible = false;
                    ModifiedPriceUpdateBtn.Visible = true;
                    ViewPMRMEstimateReport.Visible = true;
                }
                else
                {
                    AddModifiedPriceBtn.Visible = true;
                    ModifiedPriceUpdateBtn.Visible = false;
                    ViewPMRMEstimateReport.Visible = true;
                }

            }
            else
            {
                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewPMRMEstimateReport.Visible = false;
            }

        }
        protected void ChkPMRMSubmit_Click(object sender, EventArgs e)
        {
            ClsPMRMEstimateReport cls = new ClsPMRMEstimateReport();
            string YrStrList = "";
            int Status;
            foreach (ListItem listItem in PMRM_IngredientListbox.Items)
            {
                if (listItem.Selected)
                {


                    YrStrList = listItem.Value;
                    pro.Company_Id = Convert.ToInt32(lblCompanyMasterList_Id.Text);
                    pro.PMRM_ModifiedPrice = 0;
                    pro.EsitmateName = EstimateNametxt.Text;

                    //DateTime d = Convert.ToDateTime(DOPtxt.Text);
                    //DOPtxt.Text = d.ToString("yyyy-MM-dd");
                    pro.dateTime = DateTime.Parse(DOPtxt.Text);
                    pro.UserId = User_Id;
                    pro.RMEstimate_Name = RMEstimateDropdown.SelectedValue;
                    pro.PMRMPrice_Id = Convert.ToInt32(YrStrList);
                    Status = cls.Insert_PMRM_EstimateTableOne(pro);
                }

            }
            GetPMRMEstimateTableOne();
            Grid_PMRMEstimateTableOne.Visible = true;

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Message", "ShowPopup1()", true);

        }
        protected void GetPMRMEstimateTableOne()
        {
            DataTable dtRM = new DataTable();
            ClsPMRMEstimateReport cls = new ClsPMRMEstimateReport();
            pro.Company_Id = Company_Id;
            DataTable dt = new DataTable();
            dt = cls.GET_PMRMEstimateDetailByName(pro);
            DateTime d = Convert.ToDateTime(dt.Rows[0]["EstimateDate"].ToString());
            DOPtxt.Text = d.ToString("yyyy-MM-dd");
            EstimateNametxt.Text = dt.Rows[0]["EstimateName"].ToString();
            RMEstimateDropdown.SelectedValue = dt.Rows[0]["RMEstimate_Name"].ToString();
            Grid_PMRMEstimateTableOne.DataSource = dt;
            Grid_PMRMEstimateTableOne.DataBind();
        }
        protected void ViewPMRMEstimateReport_Click(object sender, EventArgs e)
        {
            string RM_List = "";
            string RMEstimate_Name = "";
            string PMRMEstimate_Name = "";
            foreach (GridViewRow row in Grid_PMRMEstimateTableOne.Rows)
            {
                Label lblEstimate_PMRMPrice_Id = row.FindControl("lblEstimate_PMRMPrice_Id") as Label;
                Label lblRMEstimate_Name = row.FindControl("lblRMEstimate_Name") as Label;
                Label lblEstimateName = row.FindControl("lblEstimateName") as Label;
                RMEstimate_Name = lblRMEstimate_Name.Text;
                PMRMEstimate_Name = lblEstimateName.Text;

                RM_List = RM_List + lblEstimate_PMRMPrice_Id.Text + ",";
            }


            string RMListStr = RM_List;
            RMListStr.Remove(RMListStr.Length - 1);
            DataTable dtRM = new DataTable();
            dtRM = cls.GetBulk_From_PMRMPriceMaster(RMListStr, RMEstimate_Name, PMRMEstimate_Name);

            Grid_PMRM_EstimateReport.DataSource = dtRM;
            Grid_PMRM_EstimateReport.DataBind();
            lblRMEstimateName.Text = RMEstimate_Name;
            lblPMRMEstimateHeader.Text = EstimateNametxt.Text;
            lblPMRMEstimateNameHeader.Text = EstimateNametxt.Text;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Message", "ShowPopup1()", true);
            //divPMRMEsitmateReportModal.Attributes.CssStyle.Add("z-index", "1050");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPMRMEstimateReport()", true);
            //divStateWiseReportModal.Attributes.CssStyle.Add("opacity", "0.9");


        }

        protected void ReportClose_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "HideRMReport", "$('#PMRM_ReportModal').modal('hide').data-bs-dismiss", true);

        }


        protected void ModifiedPriceUpdateBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            foreach (GridViewRow row in Grid_PMRMEstimateTableOne.Rows)
            {

                TextBox ModifiedPricetxt = row.FindControl("ModifiedPricetxt") as TextBox;
                //Label lblEstimate_PMRMPrice_Id = row.FindControl("lblEstimate_PMRMPrice_Id") as Label;
                Label lblPMRM_EstimateTabOne_Id = row.FindControl("lblPMRM_EstimateTabOne_Id") as Label;
                Label lblEstimateName = row.FindControl("lblEstimateName") as Label;
                Label lblEstimateDate = row.FindControl("lblEstimateDate") as Label;

                pro.UserId = User_Id;
                pro.Company_Id = Company_Id;
                pro.RMEstimate_Name = RMEstimateDropdown.SelectedValue;

                int result;
                SqlCommand cmd = new SqlCommand("sp_Update_PMRM_EstimateTableOne", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PMRM_EstimateTabOne_Id", lblPMRM_EstimateTabOne_Id.Text);
                cmd.Parameters.AddWithValue("@EstimateName", lblEstimateName.Text);
                cmd.Parameters.AddWithValue("@EstimateDate", lblEstimateDate.Text);

                cmd.Parameters.AddWithValue("@UpdatedBy", pro.UserId);
                cmd.Parameters.AddWithValue("@PMRM_ModifiedPrice", ModifiedPricetxt.Text);
                cmd.Parameters.AddWithValue("@Fk_companyId", pro.Company_Id);
                cmd.Parameters.AddWithValue("@RMEstimate_Name", pro.RMEstimate_Name);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated SuccessFul!')", true);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPMRMEstimateReport()", true);
        }

        protected void AddModifiedPriceBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            foreach (GridViewRow row in Grid_PMRMEstimateTableOne.Rows)
            {

                TextBox ModifiedPricetxt = row.FindControl("ModifiedPricetxt") as TextBox;
                //Label lblEstimate_PMRMPrice_Id = row.FindControl("lblEstimate_PMRMPrice_Id") as Label;
                Label lblPMRM_EstimateTabOne_Id = row.FindControl("lblPMRM_EstimateTabOne_Id") as Label;
                Label lblEstimateName = row.FindControl("lblEstimateName") as Label;
                Label lblEstimateDate = row.FindControl("lblEstimateDate") as Label;

                pro.UserId = User_Id;
                pro.Company_Id = Company_Id;
                pro.RMEstimate_Name = RMEstimateDropdown.SelectedValue;

                int result;
                SqlCommand cmd = new SqlCommand("sp_Update_PMRM_EstimateTableOne", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PMRM_EstimateTabOne_Id", lblPMRM_EstimateTabOne_Id.Text);
                cmd.Parameters.AddWithValue("@EstimateName", lblEstimateName.Text);
                cmd.Parameters.AddWithValue("@EstimateDate", lblEstimateDate.Text);

                cmd.Parameters.AddWithValue("@UpdatedBy", pro.UserId);
                cmd.Parameters.AddWithValue("@PMRM_ModifiedPrice", ModifiedPricetxt.Text);
                cmd.Parameters.AddWithValue("@Fk_companyId", pro.Company_Id);
                cmd.Parameters.AddWithValue("@RMEstimate_Name", pro.RMEstimate_Name);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Added SuccessFul!')", true);
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowRMReport()", true);

        }

        protected void PMRMEstReport_Click(object sender, EventArgs e)
        {
            pro.Company_Id = Company_Id;

            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int BPM_Id = Convert.ToInt32(Grid_PMRM_EstimateReport.DataKeys[gdrow.RowIndex].Value.ToString());
            ClsPMRMEstimateReport clsRME = new ClsPMRMEstimateReport();

            DataTable dt = new DataTable();
            DataTable objdtPackSize = new DataTable();
            DataTable objdtCategoryName = new DataTable();
            DataTable objdtOtherCosting = new DataTable();
            DataTable dt4 = new DataTable();
            pro.BPM_Id = BPM_Id;
            dt = clsRME.GetBPM_Id_FROM_PMRMEstimateReport(BPM_Id, pro.Company_Id);
            lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();


            int strEstimate_PMRMPrice_Id = 0;
            var varEstimate_PMRMPrice_Id = dt.AsEnumerable().Where(r => r.Field<int>("Estimate_PMRMPrice_Id") != 0);
            if (varEstimate_PMRMPrice_Id.Any())
            {
                strEstimate_PMRMPrice_Id = Convert.ToInt32(varEstimate_PMRMPrice_Id.CopyToDataTable().Rows[0]["Estimate_PMRMPrice_Id"]);
            }

            lblPrice_Id.Text = strEstimate_PMRMPrice_Id.ToString();

            //lblPrice_Id.Text = dt.Rows[0]["Estimate_PMRMPrice_Id"].ToString();
            lblname.Text = dt.Rows[0]["BPM_Product_Name"].ToString();
            string Packsize = dt.Rows[0]["PackingSize"].ToString();
            string PackUnitMeasurement = dt.Rows[0]["Measurement"].ToString();
            String ShipperName = dt.Rows[0]["PM_RM_Category_Name"].ToString();

            pro.PMRMPrice_Id = Convert.ToInt32(lblPrice_Id.Text);
            lblPackingSize.Text = Regex.Match(Packsize, @"\d+").Value;
            string RMEstimate_Name = lblRMEstimateName.Text;
            string PMRMEstimate_Name = lblPMRMEstimateName.Text;

            dt = clsRME.Get_PMRM_EstimateReportBy_BPM_Id(pro, RMEstimate_Name, PMRMEstimate_Name);
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            objdtPackSize = cls.Get_PackingMeasuremnt_FinishGoodsReport(pro.BPM_Id);
            objdtCategoryName = cls.Get_PMCategory_finishedGood_Report(pro.BPM_Id);
            objdtOtherCosting = clsRME.Get_ModifiedOtherPMCosting_Report(pro.BPM_Id, pro.Company_Id, lblRMEstimateName.Text, lblPMRMEstimateName.Text);

            StringBuilder htmlTable = new StringBuilder();
            //htmlTable.Append("<th>");
            //htmlTable.Append("<td>" + lblname.Text + "</td></th>");
            htmlTable.Append("Name:<div><h5>" + lblname.Text + " " + "(" + ShipperName + ") " + "[" + lblPackingSize.Text + " " + PackUnitMeasurement + "]</h5></div><div><table border='1' style='width:100%'>");
            htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th>No</th><th>IngredientName</th><th>Formulation(%)</th><th>QTY</th><th>RateAmount_KG</th><th>Amount</th></tr>");
            lblPMRMEstimateHeader.Text = lblPMRMEstimateName.Text;
            if (!object.Equals(dt.Rows[0], null))
            {
                if (dt.Rows.Count > 0)
                {
                    string InputQty = "0.00";
                    string Rate = "0.00";
                    string Amount = "0.00";
                    string strFormulation = "0.00";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int RowIndex = 1 + i;
                        //**************************************RM Name *************************************
                        htmlTable.Append("<tr style='color: White;'>");
                        htmlTable.Append("<td>" + RowIndex + "</td>");

                        htmlTable.Append("<td>" + dt.Rows[i]["IngredientName"] + "</td>");
                        htmlTable.Append("<td>" + dt.Rows[i]["Formulation"] + "</td>");
                        htmlTable.Append("<td>" + dt.Rows[i]["QTY"] + "</td>");
                        if (lblRMEstimateName.Text == "DefaultRMEstimate")
                        {
                            htmlTable.Append("<td>" + dt.Rows[i]["RateAmount_KG"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["Amount"] + "</td>");

                            strFormulation = (decimal.Parse(strFormulation) + decimal.Parse(dt.Rows[i]["Formulation"].ToString())).ToString();
                            InputQty = (decimal.Parse(InputQty) + decimal.Parse(dt.Rows[i]["QTY"].ToString())).ToString();
                            Rate = (decimal.Parse(Rate) + decimal.Parse(dt.Rows[i]["RateAmount_KG"].ToString())).ToString();
                            Amount = (decimal.Parse(Amount) + decimal.Parse(dt.Rows[i]["Amount"].ToString())).ToString();
                            htmlTable.Append("</tr>");
                        }
                        else
                        {
                            htmlTable.Append("<td>" + dt.Rows[i]["RM_ModifiedPrice"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["TotalModifiedAmount"] + "</td>");

                            strFormulation = (decimal.Parse(strFormulation) + decimal.Parse(dt.Rows[i]["Formulation"].ToString())).ToString();
                            InputQty = (decimal.Parse(InputQty) + decimal.Parse(dt.Rows[i]["QTY"].ToString())).ToString();
                            Rate = (decimal.Parse(Rate) + decimal.Parse(dt.Rows[i]["RM_ModifiedPrice"].ToString())).ToString();
                            Amount = (decimal.Parse(Amount) + decimal.Parse(dt.Rows[i]["TotalModifiedAmount"].ToString())).ToString();
                            htmlTable.Append("</tr>");
                        }


                    }
                    htmlTable.Append("<tr class='table-hover table-responsive gridview overflow-scroll' ><th>Total</th><th></th>");
                    htmlTable.Append("<th>" + strFormulation + "</th>");
                    htmlTable.Append("<th>" + InputQty + "</th>");
                    htmlTable.Append("<th>" + Rate + "</th>");
                    htmlTable.Append("<th>" + Amount + "</th>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("</table><br></div>");

                    //*********************************************************

                    //****************Formulation************************

                    htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                    if (!string.IsNullOrEmpty(dt.Rows[0]["FM_Name"].ToString()))
                    {
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th style='width:80%;text-align: left;' >Formulation (" + dt.Rows[0]["FM_Name"] + "), Batch Size (" + dt.Rows[0]["BatchSizeInput"] + ")</th><th style='width:30%'>" + dt.Rows[0]["FM_Total_Cost"] + "</th></tr>");
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
                    string strFinalBulkCost = "";
                    if (lblRMEstimateName.Text == "DefaultRMEstimate")
                    {
                        strFinalBulkCost = dt.Rows[0]["FinalCostBulk"].ToString();

                    }
                    else
                    {

                        var varFinalBulkCost = dt.AsEnumerable().Where(r => r.Field<decimal>("FinalBulkCost") != 0);
                        if (varFinalBulkCost.Any())
                        {
                            strFinalBulkCost = Convert.ToString(varFinalBulkCost.CopyToDataTable().Rows[0]["FinalBulkCost"]);
                        }
                        else
                        {
                            strFinalBulkCost = dt.Rows[0]["FinalBulkCost"].ToString();
                        }
                    }

                    htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Final Bulk Cost/Ltr or Kg</th>");
                    htmlTable.Append("<th style='width:30%;'>" + strFinalBulkCost + "</th>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Interest</th>");
                    htmlTable.Append("<th style='width:30%;'> " + dt.Rows[0]["InterestAmount"] + "(" + dt.Rows[0]["Interest"] + "%)" + "</th>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Final Cost/Ltr or Kg</th>");
                    double dblFinalCost = (String.IsNullOrEmpty(dt.Rows[0]["InterestAmount"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["InterestAmount"].ToString())) + (String.IsNullOrEmpty(strFinalBulkCost) ? 0 : Convert.ToDouble(strFinalBulkCost));
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
                        htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");
                    }
                    htmlTable.Append("</tr>");

                    double[] dblarray = new double[objdtPackSize.Rows.Count];

                    for (int i = 0; i < objdtCategoryName.Rows.Count; i++)
                    {
                        htmlTable.Append("<tr><td>" + objdtCategoryName.Rows[i]["PM_RM_Category_Name"] + " </td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            try
                            {
                                string strPMCost = string.Empty;
                                DataTable objdt = clsRME.GET_PMPMEstimateCostingForReport(Convert.ToInt32(lblBPM_Id.Text),
                                                                    Convert.ToInt32(objdtCategoryName.Rows[i]["Fk_PMRM_Category_Id"]),
                                                                    Convert.ToInt32(objdtPackSize.Rows[j]["Pack_size"]),
                                                                    Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]),
                                                                    lblPMRMEstimateHeader.Text);
                                if (objdt != null && objdt.Rows.Count > 0)
                                {
                                    strPMCost = objdt.Rows[0]["PMRM_ModifiedPrice"].ToString();
                                    dblarray[j] = dblarray[j] + (string.IsNullOrEmpty(strPMCost) ? 0 : Convert.ToDouble(strPMCost));
                                    htmlTable.Append("<td>" + strPMCost + " </td>");
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
                        htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");
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
                        htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");
                    }
                    htmlTable.Append("</tr>");

                    double[] dblTotalPerLtr = new double[objdtPackSize.Rows.Count];
                    htmlTable.Append("<tr><td>Per Ltr Costing</td>");
                    for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                    {
                        string strTotalAmtPerLiter = string.Empty;
                        var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
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
                        htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");
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
                            htmlTable.Append("<td>" + (Convert.ToDecimal((string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter))) - MasterPrice) + " </td>");

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
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Message", "ShowPopup1()", true);
            //divPMRMEsitmateReportModal.Attributes.CssStyle.Add("z-index", "1050");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPMRMEstimateReport()", true);
            //divPMRM_ReportModal.Attributes.CssStyle.Add("z-index", "1050");

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowRMReport()", true);

        }

        protected void AddPMRMEstimationBtn_Click(object sender, EventArgs e)
        {
            ClearData();
            if (lblGRidEstimateName.Text != "")
            {
                //GetRMEstimateTableOne();

                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = true;
                ViewPMRMEstimateReport.Visible = true;
            }
            else
            {

                AddModifiedPriceBtn.Visible = true;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewPMRMEstimateReport.Visible = true;
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);

        }
        public void ClearData()
        {
            lblGRidEstimateName.Text = "";
            EstimateNametxt.Text = "";
            DOPtxt.Text = "---------";
            Grid_PMRMEstimateTableOne.Visible = false;
        }
        protected void GridEdit_Click(object sender, EventArgs e)
        {

            Button GridEstimateEdit = sender as Button;
            GridViewRow gdrow = GridEstimateEdit.NamingContainer as GridViewRow;
            string GRidEstimateName = Gird_PMRM_PriceEstimationList.DataKeys[gdrow.RowIndex].Value.ToString();
            lblGRidEstimateName.Text = GRidEstimateName.ToString();
            pro.Company_Id = Company_Id;
            pro.EsitmateName = GRidEstimateName;
            GetPMRMEstimateTableOne();

            DataTable dt = new DataTable();
            dt = cls.GET_PMRMEstimateDetailByName(pro);
            DateTime d = Convert.ToDateTime(dt.Rows[0]["EstimateDate"].ToString());
            DOPtxt.Text = d.ToString("yyyy-MM-dd");
            //DOPtxt.Text = dt.Rows[0]["EstimateDate"].ToString();
            EstimateNametxt.Text = dt.Rows[0]["EstimateName"].ToString();
            lblPMRMEstimateName.Text = EstimateNametxt.Text;
            Grid_PMRMEstimateTableOne.DataSource = dt;
            Grid_PMRMEstimateTableOne.DataBind();
            Grid_PMRMEstimateTableOne.Visible = true;

            if (lblGRidEstimateName.Text != "")
            {
                GetPMRMEstimateTableOne();

                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = true;
                ViewPMRMEstimateReport.Visible = true;
            }
            else
            {

                AddModifiedPriceBtn.Visible = true;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewPMRMEstimateReport.Visible = true;
            }

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);
        }
        protected void RMEstimateDropdownCombo()
        {
            DataTable dtRM = new DataTable();
            ClsRMEstimateReport cls = new ClsRMEstimateReport();
            ProRMEstimateReport pro = new ProRMEstimateReport();
            pro.Company_Id = Company_Id;
            dtRM = cls.Get_Grid_RM_Estimate_ModifiedPrice(pro);
            RMEstimateDropdown.DataSource = dtRM;
            RMEstimateDropdown.DataTextField = "EstimateName";
            RMEstimateDropdown.DataValueField = "EstimateName";
            RMEstimateDropdown.DataBind();
            RMEstimateDropdown.Items.Insert(0, "DefaultRMEstimate");

        }
        protected void EstimatePMRMRateModal_Click(object sender, EventArgs e)
        {

            Button GridEstimateEdit = sender as Button;
            GridViewRow gdrow = GridEstimateEdit.NamingContainer as GridViewRow;
            string GRidEstimateName = Gird_PMRM_PriceEstimationList.DataKeys[gdrow.RowIndex].Value.ToString();
            lblGRidEstimateName.Text = GRidEstimateName.ToString();
            pro.Company_Id = Company_Id;
            pro.EsitmateName = GRidEstimateName;
            GetPMRMEstimateTableOne();

            DataTable dt = new DataTable();
            dt = cls.GET_PMRMEstimateDetailByName(pro);
            DateTime d = Convert.ToDateTime(dt.Rows[0]["EstimateDate"].ToString());
            DOPtxt.Text = d.ToString("yyyy-MM-dd");
            //DOPtxt.Text = dt.Rows[0]["EstimateDate"].ToString();
            EstimateNametxt.Text = dt.Rows[0]["EstimateName"].ToString();
            lblPMRMEstimateName.Text = dt.Rows[0]["EstimateName"].ToString();
            Grid_PMRMEstimateTableOne.DataSource = dt;
            Grid_PMRMEstimateTableOne.DataBind();
            Grid_PMRMEstimateTableOne.Visible = true;

            if (lblGRidEstimateName.Text != "")
            {
                GetPMRMEstimateTableOne();

                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = true;
                ViewPMRMEstimateReport.Visible = true;
            }
            else
            {

                AddModifiedPriceBtn.Visible = true;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewPMRMEstimateReport.Visible = true;
            }

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);
        }

        protected void RMEstimateDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RMEstimateDropdown_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void DelGridPMRmModifiedBtn_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            string Grid_PMRMEstimateTableOne_Id = Grid_PMRMEstimateTableOne.DataKeys[gdrow.RowIndex].Value.ToString();

            lblPrice_Id.Text = (gdrow.FindControl("lblEstimate_PMRMPrice_Id") as Label).Text;
            pro.PMRMPrice_Id = Convert.ToInt32(lblPrice_Id.Text);
            pro.PMRMEstimateTableOne_Id = Convert.ToInt32(Grid_PMRMEstimateTableOne_Id);
            pro.Company_Id = Company_Id;




            int status = 0;
            status = cls.Delete_ModifiedPMRM_Estimation(pro);
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted SuccessFul!')", true);
                Response.Redirect(Request.Url.AbsoluteUri);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed!')", true);
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Message", "ShowPopup1()", true);

            }
        }

        protected void StateWiseBulkCosttReport_Click(object sender, EventArgs e)
        {

            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int BPM_Id = Convert.ToInt32(Grid_PMRM_EstimateReport.DataKeys[gdrow.RowIndex].Value.ToString());

            DataTable dt = new DataTable();
            DataTable objdtPackSize = new DataTable();
            DataTable objdtCategoryName = new DataTable();
            DataTable objdtOtherCosting = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dtOldStateWiseFinalPrice = new DataTable();
            ProStatewisefinalPrice pro = new ProStatewisefinalPrice();

            pro.BPM_Id = BPM_Id;


            ClsStatewiseFinalPrice cls = new ClsStatewiseFinalPrice();
            pro.Company_Id = Company_Id;
            dtOldStateWiseFinalPrice = cls.Get_StateWiseFinalPriceAll(pro);
            ClsPMRMEstimateReport clsRME = new ClsPMRMEstimateReport();

            dt = clsRME.GetBPM_Id_FROM_PMRMEstimateReport(BPM_Id, pro.Company_Id);
            lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();


            int strEstimate_PMRMPrice_Id = 0;
            var varEstimate_PMRMPrice_Id = dt.AsEnumerable().Where(r => r.Field<int>("Estimate_PMRMPrice_Id") != 0);
            if (varEstimate_PMRMPrice_Id.Any())
            {
                strEstimate_PMRMPrice_Id = Convert.ToInt32(varEstimate_PMRMPrice_Id.CopyToDataTable().Rows[0]["Estimate_PMRMPrice_Id"]);
            }

            lblPrice_Id.Text = strEstimate_PMRMPrice_Id.ToString();

            //lblPrice_Id.Text = dt.Rows[0]["Estimate_PMRMPrice_Id"].ToString();
            lblname.Text = dtOldStateWiseFinalPrice.Rows[0]["BPM_Product_Name"].ToString();
            string Packsize = dtOldStateWiseFinalPrice.Rows[0]["Packing_Size"].ToString();
            string PackUnitMeasurement = dtOldStateWiseFinalPrice.Rows[0]["PackMeasurement"].ToString();
            String ShipperName = dtOldStateWiseFinalPrice.Rows[0]["PM_RM_Category_Name"].ToString();

            //pro.PMRMPrice_Id = Convert.ToInt32(lblPrice_Id.Text);
            //lblPackingSize.Text = Regex.Match(Packsize, @"\d+").Value;
            //string RMEstimate_Name = lblRMEstimateName.Text;
            //dt = clsRME.Get_PMRM_EstimateReportBy_BPM_Id(pro, RMEstimate_Name);
            ClsFactoryExpenceMaster clsGood = new ClsFactoryExpenceMaster();
            objdtPackSize = clsGood.Get_PackingMeasuremnt_FinishGoodsReport(pro.BPM_Id);
            objdtCategoryName = clsGood.Get_PMCategory_finishedGood_Report(pro.BPM_Id);
            objdtOtherCosting = clsRME.Get_ModifiedOtherPMCosting_Report(pro.BPM_Id, pro.Company_Id, lblRMEstimateName.Text, lblPMRMEstimateName.Text);
            ProPMRMEstimateReport proPMRM = new ProPMRMEstimateReport();


            StringBuilder htmlTable = new StringBuilder();
            //htmlTable.Append("<th>");
            //htmlTable.Append("<td>" + lblname.Text + "</td></th>");

            //lblBulkProductName.Text = lblname.Text;

            proPMRM.Company_Id = Company_Id;
            proPMRM.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            proPMRM.EsitmateName = lblPMRMEstimateName.Text;
            proPMRM.RMEstimate_Name = lblRMEstimateName.Text;
            dtOldStateWiseFinalPrice = clsRME.Get_EstimateCostBulkReportGrid(proPMRM);
            //lblProductCategoryName.Text = dtOldStateWiseFinalPrice.Rows[0]["ProductCategoryName"].ToString();
            //lblFactoryExpenseAmt.Text = dtOldStateWiseFinalPrice.Rows[0]["FectoryExpenceAmt"].ToString();
            //lblFactoryExpensePerCent.Text = dtOldStateWiseFinalPrice.Rows[0]["FectoryExpencePer"].ToString();
            //lblProductCategoryName.Text = dtOldStateWiseFinalPrice.Rows[0]["ProductCategoryName"].ToString();
            //lblOtherExpence.Text = dtOldStateWiseFinalPrice.Rows[0]["FectoryOtherAmt"].ToString();
            //lblOtherPercent.Text = dtOldStateWiseFinalPrice.Rows[0]["OtherPer"].ToString();
            //lblTradeName.Text = dtOldStateWiseFinalPrice.Rows[0]["TradeName"].ToString();
            //lblMasterPack.Text = dtOldStateWiseFinalPrice.Rows[0]["UnitPackingSize"].ToString();
            //lblMrktChrg.Text = dtOldStateWiseFinalPrice.Rows[0]["FectoryMarketedByChargesAmt"].ToString();
            //lblMrktChrgPercent.Text = dtOldStateWiseFinalPrice.Rows[0]["MarketedByChargesPer"].ToString();
            //lblProfitAmt.Text = dtOldStateWiseFinalPrice.Rows[0]["FectoryProfitPerAmt"].ToString();
            //lblProfitPercent.Text = dtOldStateWiseFinalPrice.Rows[0]["ProfitPer"].ToString();



            if (dtOldStateWiseFinalPrice.Rows.Count > 0)
            {
                //htmlTable.Append("Name:<div><h5>" + lblname.Text + " " + "(" + ShipperName + ") " + "[" + lblPackingSize.Text + " " + PackUnitMeasurement + "]</h5></div><div><table border='1' style='width:100%'>");
                htmlTable.Append("Name:<div Class='font - monospace'><h5>" + "RM Estimate:  " + "   [" + lblRMEstimateName.Text + "]" + "    /  " + "PMRM Estimate:   " + "   [" + lblPMRMEstimateName.Text + "]" + "<h5>" + "</div><div><table border='1' style='width:100%'>");

                htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th>Bulk Product Name:</th><th>Product Category:</th><th>Factory Expense</th><th>Other</th></tr>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["BPM_Product_Name"] + "</td>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["ProductCategoryName"] + "</td>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["FectoryExpenceAmt"] + " (" + dtOldStateWiseFinalPrice.Rows[0]["FectoryExpencePer"] + "%)" + "</td>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["FectoryOtherAmt"] + " (" + dtOldStateWiseFinalPrice.Rows[0]["OtherPer"] + "%)" + "</td></tr>");


                htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th>Trade Name::</th><th>Master Pack:</th><th>Marketted By Charges:</th><th>Profit</th></tr>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["TradeName"] + "</td>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["UnitPackingSize"] + "</td>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["FectoryMarketedByChargesAmt"] + " (" + dtOldStateWiseFinalPrice.Rows[0]["MarketedByChargesPer"] + "%)" + "</td>");
                htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[0]["FectoryProfitPerAmt"] + " (" + dtOldStateWiseFinalPrice.Rows[0]["ProfitPer"] + "%)" + "</td>");






                EstimateCostBulkReportGrid.DataSource = dtOldStateWiseFinalPrice;

                EstimateCostBulkReportGrid.DataBind();
               

                htmlTable.Append("</table ><br></div>");
                htmlTable.Append("<div><table border='0' style='table-layout: fixed;color: White;width:100%'>");
                htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th>State Name</th><th>Old vs New</th><th>Final NRV</th><th>RPL Profit</th><th>Total RPL PD</th><th>Suggested RPL Price</th><th>RPL Total Exp.</th><th>Net Profit RPL (%)</th><th>Diff RPL to NCR</th><th>Diff Amount</th><th>NCR Price</th><th>Total NCR PD</th><th>Suggested NCR Price</th><th>Total NCR Exp.</th><th>NCR Net Profit Amt</th><th>NCR Net Profit (%)</th></tr>");


                for (int j = 0; j < dtOldStateWiseFinalPrice.Rows.Count; j++)
                {

                    if (j>=1)
                    {
                        htmlTable.Append("<div><table border='0' style='table-layout: fixed;color: White;width:100%'>");
                        //htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th >State Name</th><th>Old vs New</th><th>Final NRV</th><th>RPL Profit</th><th>Total RPL PD</th><th>Suggested RPL Price</th><th>RPL Total Exp.</th><th>Net Profit RPL (%)</th><th>Diff RPL to NCR</th><th>Diff Amount</th><th>NCR Price</th><th>Total NCR PD</th><th>Suggested NCR Price</th><th>Total NCR Exp.</th><th>NCR Net Profit Amt</th><th>NCR Net Profit (%)</th></tr>");

                    }

                    //--------------------OldStateWiseFinalPrice
                    htmlTable.Append("<td rowspan='2'>" + dtOldStateWiseFinalPrice.Rows[j]["StateName"] + " </td>");
                    htmlTable.Append("<td>" + "Old" + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["finalNRV"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["RPL_Profit_Amt"] + " (" + dtOldStateWiseFinalPrice.Rows[j]["RPL_Approx_Profit"] + " %)" + "</td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["Total_PD"] + "</td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["Suggested_RPL_Price"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["TotalExpence"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["RPLNetProfitAmount"] + " (" + dtOldStateWiseFinalPrice.Rows[j]["RPLNetProfitPer"] + " %)" + "</td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["Diff_RPL_NCR"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["RPLtoNCRDifferenceAmt"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["NRCPrice"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["NRCTotal_PD"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["SuggestedPriceNCR"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["TotalNRC"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["NCR_NetProfitRs"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["NCR_NetProfitPer"] + " </td></tr>");
                    //------------------------------------------------------------------------------------

                    //--------------------ModifiedStateWiseFinalPrice
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>" + "New" + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedfinalNRV"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedRpl_ProfitAmt"] + " (" + dtOldStateWiseFinalPrice.Rows[j]["RPL_Approx_Profit"] + " %)" + "</td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["Total_PD"] + "</td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedSuggestedRPLPrice"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedRPLTotalExpence"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedNetProfitAmt"] + " (" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedNetProfitRPLPer"] + " %)" + "</td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["Diff_RPL_NCR"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["RPLtoNCRDiffAmount"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedNCRPrice"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["NRCTotal_PD"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedSuggestedNCRPrice"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedTotalNCRExpence"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedNCRNetProfitAmt"] + " </td>");
                    htmlTable.Append("<td>" + dtOldStateWiseFinalPrice.Rows[j]["ModifiedNCRNetProfitPer"] + " </td>");
                    //------------------------------------------------------------------------------------
                    htmlTable.Append("</tr></table></div>");
                   

                }
                 DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowStateWiseRMEstReport()", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data From Statewise Final Price!')", true);
                //Response.Redirect(Request.Url.AbsoluteUri);

            }

        }



        protected void ReportClose_Click2(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "HideStateWiseRMEstReport()", true);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPMRMEstimateReport()", true);


        }

        protected void PMRM_ReportModalClose_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "HideRMReport()", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPMRMEstimateReport()", true);

        }

        protected void PMRMEstimateReportClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "HidePMRMEstimateReport()", true);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Message", "ShowPopup1()", true);

        }

        protected void ModifiedPMRMModalClose_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.Url.AbsoluteUri);

        }
    }
}