using BusinessAccessLayer;
using DataAccessLayer;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class PriceList_GP_ActualEstimate : System.Web.UI.Page
    {
        int User_Id;
        int Company_Id;

        public string datajs = "";
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
                lblBPM_Id.Text = Session["BPM_Id"].ToString();
                lblPMRM_Category_Id.Text = Session["PMRM_Category_Id"].ToString();
                Grid_PriceListGP_ActualData();
                DisplayView();

            }
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null || Session["EstimateName"] != null)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                User_Id = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);
                lblBPM_Id.Text = Session["BPM_Id"].ToString();
                lblPMRM_Category_Id.Text = Session["PMRM_Category_Id"].ToString();
                lblEstimateName.Text = Session["EstimateName"].ToString();

                lblBulkProductName.Text = Session["BPMName"].ToString();
                lblStatus.Text = Session["Status"].ToString();
                lblTradeName_Id.Text = Session["TradeName_Id"].ToString();

                //code added by harshul patel
                //to get actual or estimate value
                lblactest.Text = Session["Status"].ToString();

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void Grid_PriceListGP_ActualData()
        {


            DataTable dtEstimate = new DataTable();
             DataTable dtActual = new DataTable();
            ClsPriceList_GP cls = new ClsPriceList_GP();

            datajs = "";

             if (lblStatus.Text == "Actual")
            {
                if (lblBPM_Id.Text == "" || lblPMRM_Category_Id.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Please Try Again With CheckBox Select')", true);

                }
                else
                {
                    dtActual = cls.Get_PriceListALL_GP_ActualByBPM("1", Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
                    if (dtActual.Rows.Count > 0)
                    {
                        lblBulkProductName.Text = dtActual.Rows[0]["BPM_Product_Name"].ToString();
                        Grid_Default_PriceList_GP_Actual.Visible = true;
                        Grid_PriceList_GP_ActualEstimate.Visible = false;

                        Grid_Default_PriceList_GP_Actual.DataSource = dtActual;
                        Grid_Default_PriceList_GP_Actual.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' [" + lblBulkProductName.Text + "]  Trade Name Not Found')", true);

                    }
                }

            }
            else
            {
                if (lblBPM_Id.Text == "" || lblPMRM_Category_Id.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Please Try Again With CheckBox Select')", true);

                }
                else
                {
                    dtEstimate = cls.Get_PriceListALL_GP_ActualEstimateByBPM("1", Convert.ToInt32(lblBPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text), lblEstimateName.Text);
                    if (dtEstimate.Rows.Count > 0)
                    {
                        lblBulkProductName.Text = dtEstimate.Rows[0]["BPM_Product_Name"].ToString();

                        Grid_Default_PriceList_GP_Actual.Visible = false;

                        Grid_PriceList_GP_ActualEstimate.Visible = true;
                        
                        Grid_PriceList_GP_ActualEstimate.DataSource = dtEstimate;
                        Grid_PriceList_GP_ActualEstimate.DataBind();

                       ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs , true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' [" + lblBulkProductName.Text + "]  Trade Name Not Found')", true);

                    }
                }

            }



        }

        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[39]["GroupId"]);
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[39]["CanDelete"]).ToString();
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[39]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[39]["CanEdit"]) == true)
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[39]["GroupId"]);
            lblCanView.Text = Convert.ToBoolean(dtMenuList.Rows[39]["CanView"]).ToString();
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[39]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[39]["CanEdit"]) == true)
            {

            }

        }
        protected void AdditionalPDtxt_TextChanged(object sender, EventArgs e)
        {
            decimal PD = 0;
            decimal QD = 0;
            decimal FinalPrice = 0;
            decimal AdditionalPD = 0;
            //decimal ValueZero = 0;
            decimal StaffExpense = 0;
            decimal Marketing = 0;
            decimal Incentive = 0;
            decimal Interest = 0;
            decimal DepoExpence = 0;
            decimal Other = 0;
            decimal TotalExpence = 0;
            decimal GrossProfitAmount = 0;
            decimal NetProfitAmount = 0;

            decimal ProfitPercent = 0;

            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TextBox ProfitPer = gvr.FindControl("ProfitPertxt") as TextBox;
            ProfitPercent = Convert.ToDecimal(ProfitPer.Text);
            Label PriceType_Id = gvr.FindControl("lblFkPriceTypeId") as Label;
            lblPriceType.Text = PriceType_Id.Text;
            if (lblStatus.Text != "Actual")
            {
                string strNewStatus = (gvr.FindControl("lblNewStatus") as Label).Text;

            }
            int State_Id = Convert.ToInt32((gvr.FindControl("lblFk_State_Id") as Label).Text);
            decimal FinalPricetxt = Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text);
            FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));


            if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "RPL")
            {
                foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                {

                    if ((row2.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("FinalPricettxt") as TextBox).Text != "0.00")
                    {

                        lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                        if (ProfitPercent != 0)
                        {
                            if ((row2.FindControl("AdditionalPDtxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("AdditionalPDtxt") as TextBox).Text = "0.00";
                            }
                            if ((row2.FindControl("FinalPricettxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("FinalPricettxt") as TextBox).Text = "0.00";
                            }

                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                            PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                            QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                            lblFinalPrice.Text = FinalPrice.ToString();
                            AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                            (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                            GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                            (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                            if (ProfitPercent != 0)
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                            }
                            else
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                            }

                            StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                            Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                            Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                            Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                            DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                            Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                            TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                            (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                            NetProfitAmount = GrossProfitAmount - TotalExpence;
                            (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                            (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                            break;
                        }
                        break;
                    }

                }
            }
            if (lblStatus.Text != "Actual")
            {
                if ((gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                {
                    if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                    {
                        foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                        {
                            lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                            if (lblStatus.Text == "Estimate" && (row2.FindControl("lblStatus") as Label).Text == "Estimate" && (row2.FindControl("lblFkPriceTypeId") as Label).Text == "9" && (row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                            {
                                if ((row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                                {
                                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));


                                }

                                lblFinalPrice.Text = FinalPrice.ToString();
                                AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                                if (ProfitPercent != 0)
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                                }
                                else
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                                }

                                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                                NetProfitAmount = GrossProfitAmount - TotalExpence;
                                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                                break;
                            }
                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));

                        }

                    }

                }

            }
            else
            {
                decimal FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));
                AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (FinalNRV) - (QD) - (PD)).ToString();
                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);

                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / FinalNRV).ToString("0.00");



                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                NetProfitAmount = GrossProfitAmount - TotalExpence;
                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / FinalNRV).ToString("0.00");
                if (NetProfitAmount < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NetProfitAmount is Nagetive')", true);

                }
                if (GrossProfitAmount < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('GrossProfitAmount is Nagetive')", true);

                }
            }
            if (ProfitPercent != 0 || PD != 0 || QD != 0)
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
            }
            else
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

            }
        }

        protected void FinalPricettxt_TextChanged(object sender, EventArgs e)
        {
            decimal PD = 0;
            decimal QD = 0;
            decimal FinalPrice = 0;
            decimal AdditionalPD = 0;
            //decimal ValueZero = 0;
            decimal StaffExpense = 0;
            decimal Marketing = 0;
            decimal Incentive = 0;
            decimal Interest = 0;
            decimal DepoExpence = 0;
            decimal Other = 0;
            decimal TotalExpence = 0;
            decimal GrossProfitAmount = 0;
            decimal NetProfitAmount = 0;
            decimal ProfitPercent = 0;
            TextBox btn = (TextBox)sender;

            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TextBox ProfitPer = gvr.FindControl("ProfitPertxt") as TextBox;
            ProfitPercent = Convert.ToDecimal(ProfitPer.Text);
            Label PriceType_Id = gvr.FindControl("lblFkPriceTypeId") as Label;
            lblPriceType.Text = PriceType_Id.Text;
            if (lblStatus.Text != "Actual")
            {
                string strNewStatus = (gvr.FindControl("lblNewStatus") as Label).Text;

            }
            int State_Id = Convert.ToInt32((gvr.FindControl("lblFk_State_Id") as Label).Text);
            decimal FinalPricetxt = Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text);
            FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));


            if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "RPL")
            {
                foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                {

                    if ((row2.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("FinalPricettxt") as TextBox).Text != "0.00")
                    {

                        lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                        if (ProfitPercent != 0)
                        {
                            if ((row2.FindControl("AdditionalPDtxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("AdditionalPDtxt") as TextBox).Text = "0.00";
                            }
                            if ((row2.FindControl("FinalPricettxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("FinalPricettxt") as TextBox).Text = "0.00";
                            }

                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                            PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                            QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                            lblFinalPrice.Text = FinalPrice.ToString();
                            AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                            (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                            GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                            (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                            if (ProfitPercent != 0)
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                            }
                            else
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                            }

                            StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                            Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                            Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                            Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                            DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                            Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                            TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                            (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                            NetProfitAmount = GrossProfitAmount - TotalExpence;
                            (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                            (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                            break;
                        }
                        break;
                    }

                }
                //if (NetProfitAmount < 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NetProfitAmount is Nagetive')", true);
                //    (gvr.FindControl("NetProfitAmounttxt") as Label).ForeColor = System.Drawing.Color.Red;
                //}
                //if (GrossProfitAmount < 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('GrossProfitAmount is Nagetive')", true);
                //    (gvr.FindControl("NetProfitAmounttxt") as Label).ForeColor = System.Drawing.Color.Red;

                //}
            }
            if (lblStatus.Text != "Actual" && lblGrid_Status.Text != "Actual")
            {
                if ((gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                {
                    if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                    {
                        foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                        {
                            lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                            if (lblStatus.Text == "Estimate" && (row2.FindControl("lblStatus") as Label).Text == "Estimate" && (row2.FindControl("lblFkPriceTypeId") as Label).Text == "9" && (row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                            {
                                if ((row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                                {
                                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));


                                }

                                lblFinalPrice.Text = FinalPrice.ToString();
                                AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                                if (ProfitPercent != 0)
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                                }
                                else
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                                }

                                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                                NetProfitAmount = GrossProfitAmount - TotalExpence;
                                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                                break;
                            }
                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));

                        }
                        //if (NetProfitAmount < 0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NetProfitAmount is Nagetive')", true);
                        //    (gvr.FindControl("NetProfitAmounttxt") as Label).ForeColor = System.Drawing.Color.Red;
                        //}
                        //if (GrossProfitAmount < 0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('GrossProfitAmount is Nagetive')", true);
                        //    (gvr.FindControl("NetProfitAmounttxt") as Label).ForeColor = System.Drawing.Color.Red;

                        //}
                    }

                }

            }
            else
            {
                decimal FinalNRV = 0;
                lblGrid_Status.Text = (gvr.FindControl("lblStatus") as Label).Text;
                if (lblStatus.Text == "Estimate" && lblGrid_Status.Text == "Estimate")
                {
                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                    FinalNRV = (Convert.ToDecimal((NextRow.FindControl("lblFinalNRV") as Label).Text));
                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));
                    FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));
                    AdditionalPD = (Convert.ToDecimal((NextRow.FindControl("AdditionalPDtxt") as TextBox).Text));
                }
                else
                {
                    FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                    PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                    QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                    AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                }


                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (FinalNRV) - (QD) - (PD)).ToString();
                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);

                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / FinalNRV).ToString("0.00");

                if (ProfitPercent != 0)
                {
                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                }
                else
                {
                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                }

                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                NetProfitAmount = GrossProfitAmount - TotalExpence;
                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / FinalNRV).ToString("0.00");
                if (NetProfitAmount < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NetProfitAmount is Nagetive')", true);
                    (gvr.FindControl("NetProfitAmounttxt") as Label).ForeColor = System.Drawing.Color.Red;
                }
                if (GrossProfitAmount < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('GrossProfitAmount is Nagetive')", true);
                    (gvr.FindControl("NetProfitAmounttxt") as Label).ForeColor = System.Drawing.Color.Red;

                }
            }

            if (ProfitPercent != 0 || PD != 0 || QD != 0)
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
            }
            else
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

            }
        }


        protected void ProfitPertxt_TextChanged(object sender, EventArgs e)
        {
            decimal FinalNRV = 0;
            decimal PD = 0;
            decimal QD = 0;
            decimal ProfitPer = 0;
            decimal FinalPrice = 0;
            decimal AdditionalPD = 0;

            decimal ProfitPercent = 0;
            int PriceType_Idtxt = 0;
            int State_Id = 0;
            string Status;


            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TextBox ProfitPercentage = gvr.FindControl("ProfitPertxt") as TextBox;
            ProfitPercent = Convert.ToDecimal(ProfitPercentage.Text);
            State_Id = Convert.ToInt32((gvr.FindControl("lblFk_State_Id") as Label).Text);
            Status = (gvr.FindControl("lblFk_State_Id") as Label).Text;
            Label PriceType_Id = gvr.FindControl("lblFkPriceTypeId") as Label;
            PriceType_Idtxt = Convert.ToInt32(PriceType_Id.Text);
            PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
            QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
            ProfitPer = (Convert.ToDecimal((gvr.FindControl("ProfitPertxt") as TextBox).Text));
            FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));
            AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));
            string EnumDescription = (gvr.FindControl("lblEnumDescription") as Label).Text;

            lblPriceList_Profit_Amt.Text = (gvr.FindControl("ProfitAmttxt") as Label).Text;
            lblGrid_Status.Text = (gvr.FindControl("lblStatus") as Label).Text;

            if (lblSuggestedPricetxt.Text == "" && PriceType_Idtxt == 9 && lblStatus.Text == "Estimate" && lblGrid_Status.Text == "Estimate")
            {
                GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex - 1];

                lblSuggestedPricetxt.Text = (Convert.ToDecimal((NextRow.FindControl("SuggestedPriceWithPDttxt") as Label).Text)).ToString();
                lblPDEstimate.Text = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text)).ToString();
                lblQDEstimate.Text = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text)).ToString();
                lblPriceList_Profit_Per1.Text = (Convert.ToDecimal((NextRow.FindControl("ProfitPertxt") as TextBox).Text)).ToString();

            }

            if (lblSuggestedPricetxt.Text == "" && PriceType_Idtxt == 9 && lblStatus.Text == "Estimate" && lblGrid_Status.Text == "Actual")
            {
                GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex - 1];

                lblSuggestedPricetxt.Text = (Convert.ToDecimal((NextRow.FindControl("SuggestedPriceWithPDttxt") as Label).Text)).ToString();
                lblPDActual.Text = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text)).ToString();
                lblQDActual.Text = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text)).ToString();
                lblPriceList_Profit_Per.Text = (Convert.ToDecimal((NextRow.FindControl("ProfitPertxt") as TextBox).Text)).ToString();

            }
            if (lblSuggestedPricetxt.Text == "" && PriceType_Idtxt == 9 && lblStatus.Text == "Actual" && lblGrid_Status.Text == "Actual")
            {
                GridViewRow NextRow = Grid_Default_PriceList_GP_Actual.Rows[gvr.RowIndex - 1];

                lblSuggestedPricetxt.Text = (Convert.ToDecimal((NextRow.FindControl("SuggestedPriceWithPDttxt") as Label).Text)).ToString();
                lblPDActual.Text = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text)).ToString();
                lblQDActual.Text = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text)).ToString();
                lblPriceList_Profit_Per.Text = (Convert.ToDecimal((NextRow.FindControl("ProfitPertxt") as TextBox).Text)).ToString();

            }

            if (ProfitPercent != 0 && PriceType_Idtxt == 8)

            {
                FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));

                (gvr.FindControl("ProfitAmttxt") as Label).Text = (((FinalNRV + PD + QD) * ProfitPer / 100)).ToString("0.00");

                lblPDActual.Text = PD.ToString();
                lblQDActual.Text = QD.ToString();
                lblPriceList_Profit_Per.Text = ProfitPer.ToString();
                lblPriceList_Profit_Amt1.Text = (gvr.FindControl("ProfitAmttxt") as Label).Text;
                lblPriceList_Profit_Per.Text = ProfitPercent.ToString();
                lblFinalNRV.Text = FinalNRV.ToString();
                lblSuggestedPricetxt.Text = (gvr.FindControl("SuggestedPriceWithPDttxt") as Label).Text = (((FinalNRV + PD + QD) * ProfitPer / 100) + (FinalNRV + PD + QD)).ToString("0.00");


                if ((gvr.FindControl("FinalPricettxt") as TextBox).Text == "")
                {
                    (gvr.FindControl("FinalPricettxt") as TextBox).Text = "0.00";
                }
                foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                {
                    if ((row2.FindControl("lblFk_State_Id") as Label).Text == State_Id.ToString())
                    {
                        if ((row2.FindControl("lblStatus") as Label).Text == "Estimate" && lblGrid_Status.Text == "Actual")
                        {
                            if ((row2.FindControl("lblFkPriceTypeId") as Label).Text == "8")
                            {

                                lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;
                                ((row2.FindControl("ProfitAmttxt") as Label).Text) = ((Convert.ToDecimal(lblFinalNRV.Text) + Convert.ToDecimal(lblPDActual.Text) + Convert.ToDecimal(lblQDActual.Text)) * (Convert.ToDecimal(lblPriceList_Profit_Per.Text)) / 100).ToString("0.00"); ;
                                ((row2.FindControl("ProfitPertxt") as TextBox).Text) = (lblPriceList_Profit_Per.Text).ToString();
                                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = false;

                                (row2.FindControl("PDtxt") as TextBox).Text = lblPDActual.Text;
                                (row2.FindControl("PDtxt") as TextBox).Enabled = false;
                                (row2.FindControl("QDtxt") as TextBox).Text = lblQDActual.Text;
                                (row2.FindControl("QDtxt") as TextBox).Enabled = false;
                                lblSuggestedPricetxt1.Text = (row2.FindControl("SuggestedPriceWithPDttxt") as Label).Text = (Convert.ToDecimal(lblFinalNRV.Text) + (Convert.ToDecimal(lblPDActual.Text) + (Convert.ToDecimal(lblQDActual.Text) + Convert.ToDecimal(((row2.FindControl("ProfitAmttxt") as Label).Text))))).ToString("0.00");


                            }
                        }
                    }
                }

            }

            else if (ProfitPercent != 0 && PriceType_Idtxt == 9)
            {
                lblPDEstimate.Text = PD.ToString();
                lblQDEstimate.Text = QD.ToString();
                lblPriceList_Profit_Per1.Text = ProfitPer.ToString();
                //if (lblStatus.Text == "Estimate")
                //{
                //    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                //    lblFinalNRV.Text = (((NextRow.FindControl("lblFinalNRV") as Label).Text));

                //}


                FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                lblFinalNRV.Text = FinalNRV.ToString();

                lblPriceList_Profit_Amt1.Text = (gvr.FindControl("ProfitAmttxt") as Label).Text = (((Convert.ToDecimal(lblSuggestedPricetxt.Text) * ProfitPer)) / 100).ToString("0.00");
                lblSuggestedPricetxt1.Text = (gvr.FindControl("SuggestedPriceWithPDttxt") as Label).Text = (Convert.ToDecimal(lblSuggestedPricetxt.Text) - (Convert.ToDecimal(lblPriceList_Profit_Amt1.Text)) + PD + QD).ToString("0.00");

                if ((gvr.FindControl("FinalPricettxt") as TextBox).Text == "")
                {
                    (gvr.FindControl("FinalPricettxt") as TextBox).Text = "0.00";
                }
                //PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                //QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                ProfitPer = (Convert.ToDecimal((gvr.FindControl("ProfitPertxt") as TextBox).Text));
                //FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));
                //AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                {
                    if ((row2.FindControl("lblFk_State_Id") as Label).Text == State_Id.ToString())
                    {
                        if ((row2.FindControl("lblStatus") as Label).Text == "Estimate" && lblGrid_Status.Text == "Actual")
                        {
                            if ((row2.FindControl("lblFkPriceTypeId") as Label).Text == "8")
                            {

                                lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;
                                ((row2.FindControl("ProfitAmttxt") as Label).Text) = ((Convert.ToDecimal(lblFinalNRV.Text) + Convert.ToDecimal(lblPDActual.Text) + Convert.ToDecimal(lblQDActual.Text)) * (Convert.ToDecimal(lblPriceList_Profit_Per.Text)) / 100).ToString("0.00"); ;
                                ((row2.FindControl("ProfitPertxt") as TextBox).Text) = (lblPriceList_Profit_Per.Text).ToString();
                                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = false;

                                (row2.FindControl("PDtxt") as TextBox).Text = lblPDActual.Text;
                                (row2.FindControl("PDtxt") as TextBox).Enabled = false;
                                (row2.FindControl("QDtxt") as TextBox).Text = lblQDActual.Text;
                                (row2.FindControl("QDtxt") as TextBox).Enabled = false;
                                lblSuggestedPricetxt1.Text = (row2.FindControl("SuggestedPriceWithPDttxt") as Label).Text = (Convert.ToDecimal(lblFinalNRV.Text) + (Convert.ToDecimal(lblPDActual.Text) + (Convert.ToDecimal(lblQDActual.Text) + Convert.ToDecimal(((row2.FindControl("ProfitAmttxt") as Label).Text))))).ToString("0.00");


                            }


                            if ((row2.FindControl("lblFkPriceTypeId") as Label).Text == "9")
                            {

                                lblFinalNRV1.Text = lblFinalNRV.Text;
                                //lblPriceList_Profit_Per1.Text = (row2.FindControl("ProfitPertxt") as TextBox).Text;

                                ((row2.FindControl("ProfitAmttxt") as Label).Text) = ((Convert.ToDecimal(lblSuggestedPricetxt1.Text) * (ProfitPer)) / 100).ToString("0.00"); ;
                                ((row2.FindControl("ProfitPertxt") as TextBox).Text) = (ProfitPer).ToString();
                                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = false;

                                (row2.FindControl("PDtxt") as TextBox).Text = lblPDEstimate.Text;
                                (row2.FindControl("PDtxt") as TextBox).Enabled = false;
                                (row2.FindControl("QDtxt") as TextBox).Text = lblQDEstimate.Text;
                                (row2.FindControl("QDtxt") as TextBox).Enabled = false;
                                (row2.FindControl("SuggestedPriceWithPDttxt") as Label).Text = (Convert.ToDecimal(lblSuggestedPricetxt1.Text) - Convert.ToDecimal(((row2.FindControl("ProfitAmttxt") as Label).Text)) + (Convert.ToDecimal(lblPDEstimate.Text) + (Convert.ToDecimal(lblQDEstimate.Text)))).ToString("0.00");
                                //(gvr.FindControl("SuggestedPriceWithPDttxt") as Label).Text = (Convert.ToDecimal(lblSuggestedPricetxt.Text) - (Convert.ToDecimal(lblPriceList_Profit_Amt1.Text)) + PD + QD).ToString("0.00");

                                string SuggestedPriceWithPD = (row2.FindControl("SuggestedPriceWithPDttxt") as Label).Text;
                            }

                        }

                    }
                }

            }

            foreach (GridViewRow row1 in Grid_PriceList_GP_ActualEstimate.Rows)
            {
                if ((row1.FindControl("ProfitPertxt") as TextBox).Text != "0.00")
                {
                    (row1.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                }
                else
                {
                    (row1.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                }
            }

            if (ProfitPer != 0 || PD != 0 || QD != 0)
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
            }
            else
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

            }
        }

        protected void GridAddPriceGP_Click(object sender, EventArgs e)
        {
            ClsPriceList_GP cls = new ClsPriceList_GP();
            ProPriceList_GP pro = new ProPriceList_GP();
            int status = 0;
            if (lblStatus.Text == "Estimate")
            {

                bool CheckBoxChanged = false;
                foreach (GridViewRow row1 in Grid_PriceList_GP_ActualEstimate.Rows)

                {
                    if ((row1.FindControl("ProfitPertxt") as TextBox).Text != "0.00")
                    {
                        CheckBoxChanged = (row1.FindControl("CheckBox_Check") as CheckBox).Checked = true;

                    }
                    else
                    {
                        CheckBoxChanged = false;
                    }

                    if (CheckBoxChanged == true)
                    {
                        pro.NewStatus = (row1.FindControl("lblNewStatus") as Label).Text;

                        pro.Fk_BPM_Id = (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text));
                        pro.Fk_State_Id = (Convert.ToInt32((row1.FindControl("lblFk_State_Id") as Label).Text));
                        lblStateNameAlert.Text = (((row1.FindControl("lblStateName") as Label).Text));
                        pro.Status = ((row1.FindControl("lblStatus") as Label).Text);
                        pro.NRVWithCo = Convert.ToDecimal((row1.FindControl("lblNRV") as Label).Text);
                        pro.FkPriceType = Convert.ToInt32((row1.FindControl("lblFkPriceTypeId") as Label).Text);

                        pro.Transport = Convert.ToDecimal((row1.FindControl("lblTransport") as Label).Text);
                        pro.FinalNRV = Convert.ToDecimal((row1.FindControl("lblFinalNRV") as Label).Text);
                        if (pro.NewStatus == "RPL" && pro.FkPriceType == 9)
                        {
                            pro.FinalPrice = 0;
                            pro.SuggestedPriceWithPD = Convert.ToDecimal((row1.FindControl("SuggestedPriceWithPDttxt") as TextBox).Text);

                            pro.LastSharedPrice = 0;
                            pro.LastSharedPrice = 0;
                            pro.AdditionalPD = 0;
                            pro.GrossProfitAmt = 0;
                            pro.GrossProfitPer = 0;
                            pro.TotalExpence = 0;
                            pro.NetProfitAmt = 0;
                            pro.NetProfitPer = 0;
                        }
                        if (pro.NewStatus == "RPL" && pro.FkPriceType == 8)
                        {
                            pro.FinalPrice = Convert.ToDecimal((row1.FindControl("FinalPricettxt") as TextBox).Text);
                            pro.SuggestedPriceWithPD = Convert.ToDecimal((row1.FindControl("SuggestedPriceWithPDttxt") as TextBox).Text);

                            pro.LastSharedPrice = Convert.ToDecimal((row1.FindControl("lblLast_Shared_Final_Price") as Label).Text);
                            pro.LastSharedPrice = Convert.ToDecimal((row1.FindControl("FinalPricettxt") as TextBox).Text);
                            pro.AdditionalPD = Convert.ToDecimal((row1.FindControl("AdditionalPDtxt") as TextBox).Text);
                            pro.GrossProfitAmt = Convert.ToDecimal((row1.FindControl("GrossProfitAmounttxt") as TextBox).Text);
                            pro.GrossProfitPer = Convert.ToDecimal((row1.FindControl("lblGrossProfitPer") as TextBox).Text);
                            pro.TotalExpence = Convert.ToDecimal((row1.FindControl("TotalExpencetxt") as TextBox).Text);
                            pro.NetProfitAmt = Convert.ToDecimal((row1.FindControl("NetProfitAmounttxt") as TextBox).Text);
                            pro.NetProfitPer = Convert.ToDecimal((row1.FindControl("lblNetProfitAmtPer") as TextBox).Text);
                        }
                        if (pro.NewStatus == "NCR" && pro.FkPriceType == 8)
                        {
                            GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[row1.RowIndex + 1];

                            pro.FinalPrice = 0;

                            pro.SuggestedPriceWithPD = 0;

                            pro.LastSharedPrice = 0;
                            pro.LastSharedPrice = 0;
                            pro.AdditionalPD = 0;
                            pro.GrossProfitAmt = 0;
                            pro.GrossProfitPer = 0;
                            pro.TotalExpence = 0;
                            pro.NetProfitAmt = 0;
                            pro.NetProfitPer = 0;
                        }
                        pro.SuggestedPriceWithPD = Convert.ToDecimal((row1.FindControl("SuggestedPriceWithPDttxt") as TextBox).Text);

                        if (pro.NewStatus == "NCR" && pro.FkPriceType == 9)
                        {
                            GridViewRow PreviousRow = Grid_PriceList_GP_ActualEstimate.Rows[row1.RowIndex - 1];

                            pro.FinalPrice = Convert.ToDecimal((PreviousRow.FindControl("FinalPricettxt") as TextBox).Text);
                            //pro.SuggestedPriceWithPD = Convert.ToDecimal((PreviousRow.FindControl("SuggestedPriceWithPDttxt") as Label).Text);

                            pro.LastSharedPrice = Convert.ToDecimal((PreviousRow.FindControl("lblLast_Shared_Final_Price") as Label).Text);
                            pro.LastSharedPrice = Convert.ToDecimal((PreviousRow.FindControl("FinalPricettxt") as TextBox).Text);
                            pro.AdditionalPD = Convert.ToDecimal((PreviousRow.FindControl("AdditionalPDtxt") as TextBox).Text);
                            pro.GrossProfitAmt = Convert.ToDecimal((PreviousRow.FindControl("GrossProfitAmounttxt") as TextBox).Text);
                            pro.GrossProfitPer = Convert.ToDecimal((PreviousRow.FindControl("lblGrossProfitPer") as TextBox).Text);
                            pro.TotalExpence = Convert.ToDecimal((PreviousRow.FindControl("TotalExpencetxt") as TextBox).Text);
                            pro.NetProfitAmt = Convert.ToDecimal((PreviousRow.FindControl("NetProfitAmounttxt") as TextBox).Text);
                            pro.NetProfitPer = Convert.ToDecimal((PreviousRow.FindControl("lblNetProfitAmtPer") as TextBox).Text);
                        }


                        //pro.FkPriceType = Convert.ToInt32((row1.FindControl("lblFkPriceTypeId") as Label).Text);

                        //}
                        //pro.FkPriceType = Convert.ToInt32((row1.FindControl("lblEnumDescription") as Label).Text);

                        pro.TOD = Convert.ToDecimal((row1.FindControl("TODtxt") as TextBox).Text);

                        pro.PD = Convert.ToDecimal((row1.FindControl("PDtxt") as TextBox).Text);

                        pro.QD = Convert.ToDecimal((row1.FindControl("QDtxt") as TextBox).Text);

                        
                        pro.ProfitAmt = Convert.ToDecimal((row1.FindControl("ProfitAmttxt") as TextBox).Text);

                        pro.ProfitPer = Convert.ToDecimal((row1.FindControl("ProfitPertxt") as TextBox).Text);

                        //pro.Packing_Size = Convert.ToDecimal((row1.FindControl("lblPacking_Size") as Label).Text);
                        //pro.Packing_Measurement = Convert.ToInt32((row1.FindControl("lblFk_UnitMeasurement_Id") as Label).Text);
                        pro.TradeName_Id = Convert.ToInt32(lblTradeName_Id.Text);


                        pro.Fk_PMRMCategory_Id = Convert.ToInt32((row1.FindControl("lblFk_PM_RM_Category_Id") as Label).Text);

                        pro.Fk_Company_Id = 1;
                        if (pro.Status == "Actual")
                        {
                            pro.EstimateName = "";
                        }
                        else
                        {
                            pro.EstimateName = lblEstimateName.Text;

                        }

                        DataTable dtCheckForInsert = new DataTable();
                        //if (pro.NetProfitAmt < 0 || pro.NetProfitPer < 0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + lblStateNameAlert.Text + " has a Nagetive Would You like to Proceed ?')", true);

                        //}
                        dtCheckForInsert = cls.CHECK_INSERT_PriceListALL_GP_ActualEstimate(pro);
                        if (dtCheckForInsert.Rows.Count > 0)
                        {
                            status = cls.UPDATE_PriceList_GP_ActualEstimate(pro);

                        }
                        else
                        {
                            status = cls.Insert_PriceList_GP_ActualEstimate(pro);


                        }

                        //if (pro.Status == "Actual")
                        //{

                        //    pro.Fk_BPM_Id = (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text));
                        //    pro.Fk_State_Id = (Convert.ToInt32((row1.FindControl("lblFk_State_Id") as Label).Text));
                        //    lblStateNameAlert.Text = (((row1.FindControl("lblStateName") as Label).Text));
                        //    pro.Status = ((row1.FindControl("lblStatus") as Label).Text);
                        //    pro.NRVWithCo = Convert.ToDecimal((row1.FindControl("lblNRV") as Label).Text);

                        //    pro.Transport = Convert.ToDecimal((row1.FindControl("lblTransport") as Label).Text);
                        //    pro.FinalNRV = Convert.ToDecimal((row1.FindControl("lblFinalNRV") as Label).Text);

                        //    pro.FkPriceType = Convert.ToInt32((row1.FindControl("lblFkPriceTypeId") as Label).Text);
                        //    if (pro.FkPriceType == 8)
                        //    {
                        //        pro.NewStatus = "RPL";
                        //    }
                        //    else
                        //    {
                        //        pro.NewStatus = "NCR";

                        //    }
                        //    pro.PD = Convert.ToDecimal((row1.FindControl("PDtxt") as TextBox).Text);

                        //    pro.QD = Convert.ToDecimal((row1.FindControl("QDtxt") as TextBox).Text);
                        //    pro.ProfitAmt = Convert.ToDecimal((row1.FindControl("ProfitAmttxt") as TextBox).Text);

                        //    pro.ProfitPer = Convert.ToDecimal((row1.FindControl("ProfitPertxt") as TextBox).Text);
                        //    pro.SuggestedPriceWithPD = Convert.ToDecimal((row1.FindControl("SuggestedPriceWithPDttxt") as TextBox).Text);

                        //    pro.LastSharedPrice = Convert.ToDecimal((row1.FindControl("lblLast_Shared_Final_Price") as Label).Text);
                        //    pro.FinalPrice = Convert.ToDecimal((row1.FindControl("FinalPricettxt") as TextBox).Text);
                        //    pro.LastSharedPrice = Convert.ToDecimal((row1.FindControl("FinalPricettxt") as TextBox).Text);
                        //    pro.AdditionalPD = Convert.ToDecimal((row1.FindControl("AdditionalPDtxt") as TextBox).Text);
                        //    pro.GrossProfitAmt = Convert.ToDecimal((row1.FindControl("GrossProfitAmounttxt") as TextBox).Text);
                        //    pro.GrossProfitPer = Convert.ToDecimal((row1.FindControl("lblGrossProfitPer") as TextBox).Text);
                        //    pro.TotalExpence = Convert.ToDecimal((row1.FindControl("TotalExpencetxt") as TextBox).Text);
                        //    pro.NetProfitAmt = Convert.ToDecimal((row1.FindControl("NetProfitAmounttxt") as TextBox).Text);
                        //    pro.NetProfitPer = Convert.ToDecimal((row1.FindControl("lblNetProfitAmtPer") as TextBox).Text);
                        //    //pro.Packing_Size = Convert.ToDecimal((row1.FindControl("lblPacking_Size") as Label).Text);
                        //    //pro.Packing_Measurement = Convert.ToInt32((row1.FindControl("lblFk_UnitMeasurement_Id") as Label).Text);
                        //    pro.TradeName_Id = Convert.ToInt32(lblTradeName_Id.Text);


                        //    pro.Fk_PMRMCategory_Id = Convert.ToInt32((row1.FindControl("lblFk_PM_RM_Category_Id") as Label).Text);

                        //    pro.Fk_Company_Id = 1;
                        //    if (pro.Status == "Actual")
                        //    {
                        //        pro.EstimateName = "";
                        //    }
                        //    else
                        //    {
                        //        pro.EstimateName = lblEstimateName.Text;

                        //    }

                        //    DataTable dtCheckForInsertActual = new DataTable();

                        //    dtCheckForInsertActual = cls.CHECK_INSERT_PriceListALL_GP_Actual(pro);
                        //    if (dtCheckForInsertActual.Rows.Count > 0)
                        //    {
                        //        status = cls.UPDATE_PriceList_GP_Actual(pro);
                        //    }
                        //    else
                        //    {
                        //        status = cls.Insert_PriceList_GP_Actual(pro);

                        //    }

                        //}
                    }

                }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Record has been Updated!')", true);


            }
            else
            {

                foreach (GridViewRow row1 in Grid_Default_PriceList_GP_Actual.Rows)

                {

                    bool CheckBoxChanged = (row1.FindControl("CheckBox_Check") as CheckBox).Checked;

                    if (CheckBoxChanged == true)
                    {
                        //pro.NewStatus = (row1.FindControl("lblNewStatus") as Label).Text;

                        pro.Fk_BPM_Id = (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text));
                        pro.Fk_State_Id = (Convert.ToInt32((row1.FindControl("lblFk_State_Id") as Label).Text));
                        lblStateNameAlert.Text = (((row1.FindControl("lblStateName") as Label).Text));
                        pro.Status = ((row1.FindControl("lblStatus") as Label).Text);
                        pro.NRVWithCo = Convert.ToDecimal((row1.FindControl("lblNRV") as Label).Text);

                        pro.Transport = Convert.ToDecimal((row1.FindControl("lblTransport") as Label).Text);
                        pro.FinalNRV = Convert.ToDecimal((row1.FindControl("lblFinalNRV") as Label).Text);

                        pro.FkPriceType = Convert.ToInt32((row1.FindControl("lblFkPriceTypeId") as Label).Text);
                        if (pro.FkPriceType == 8)
                        {
                            pro.NewStatus = "RPL";
                        }
                        else
                        {
                            pro.NewStatus = "NCR";

                        }
                        pro.PD = Convert.ToDecimal((row1.FindControl("PDtxt") as TextBox).Text);

                        pro.QD = Convert.ToDecimal((row1.FindControl("QDtxt") as TextBox).Text);

                        pro.TOD = Convert.ToDecimal((row1.FindControl("TODtxt") as TextBox).Text);


                        pro.ProfitPer = Convert.ToDecimal((row1.FindControl("ProfitPertxt") as TextBox).Text);

                        pro.LastSharedPrice = Convert.ToDecimal((row1.FindControl("lblLast_Shared_Final_Price") as Label).Text);
                        pro.FinalPrice = Convert.ToDecimal((row1.FindControl("FinalPricettxt") as TextBox).Text);
                        pro.LastSharedPrice = Convert.ToDecimal((row1.FindControl("FinalPricettxt") as TextBox).Text);
                        pro.AdditionalPD = Convert.ToDecimal((row1.FindControl("AdditionalPDtxt") as TextBox).Text);





                        // Code added by Harshul Patel on 22-4-20222 for calcuation done using javascript

                        /*
                        pro.ProfitAmt = Convert.ToDecimal((row1.FindControl("ProfitAmttxt") as Label).Text);
                        pro.SuggestedPriceWithPD = Convert.ToDecimal((row1.FindControl("SuggestedPriceWithPDttxt") as Label).Text);
                        pro.GrossProfitAmt = Convert.ToDecimal((row1.FindControl("GrossProfitAmounttxt") as Label).Text);
                        pro.GrossProfitPer = Convert.ToDecimal((row1.FindControl("lblGrossProfitPer") as Label).Text);
                        pro.TotalExpence = Convert.ToDecimal((row1.FindControl("TotalExpencetxt") as Label).Text);
                        pro.NetProfitAmt = Convert.ToDecimal((row1.FindControl("NetProfitAmounttxt") as Label).Text);
                        pro.NetProfitPer = Convert.ToDecimal((row1.FindControl("lblNetProfitAmtPer") as Label).Text);
                        
                         */

                        pro.ProfitAmt = Convert.ToDecimal((row1.FindControl("ProfitAmttxt") as TextBox).Text);
                        pro.SuggestedPriceWithPD = Convert.ToDecimal((row1.FindControl("SuggestedPriceWithPDttxt") as TextBox).Text);
                        pro.GrossProfitAmt = Convert.ToDecimal((row1.FindControl("GrossProfitAmounttxt") as TextBox).Text);
                        pro.GrossProfitPer = Convert.ToDecimal((row1.FindControl("lblGrossProfitPer") as TextBox).Text);
                        pro.TotalExpence = Convert.ToDecimal((row1.FindControl("TotalExpencetxt") as TextBox).Text);
                        pro.NetProfitAmt = Convert.ToDecimal((row1.FindControl("NetProfitAmounttxt") as TextBox).Text);
                        pro.NetProfitPer = Convert.ToDecimal((row1.FindControl("lblNetProfitAmtPer") as TextBox).Text);

                        // end


                        //pro.Packing_Size = Convert.ToDecimal((row1.FindControl("lblPacking_Size") as Label).Text);
                        //pro.Packing_Measurement = Convert.ToInt32((row1.FindControl("lblFk_UnitMeasurement_Id") as Label).Text);
                        pro.TradeName_Id = Convert.ToInt32(lblTradeName_Id.Text);


                        pro.Fk_PMRMCategory_Id = Convert.ToInt32((row1.FindControl("lblFk_PM_RM_Category_Id") as Label).Text);

                        pro.Fk_Company_Id = 1;


                        DataTable dtCheckForInsert = new DataTable();

                        dtCheckForInsert = cls.CHECK_INSERT_PriceListALL_GP_Actual(pro);
                        if (dtCheckForInsert.Rows.Count > 0)
                        {
                            status = cls.UPDATE_PriceList_GP_Actual(pro);
                        }
                        else
                        {
                            status = cls.Insert_PriceList_GP_Actual(pro);

                        }
                        pro.EstimateName = lblEstimateName.Text;
                        dtCheckForInsert = cls.CHECK_INSERT_PriceListALL_GP_ActualEstimate(pro);
                        if (dtCheckForInsert.Rows.Count > 0)
                        {
                            status = cls.UPDATE_PriceList_GP_ActualEstimate(pro);

                        }
                        else
                        {
                            status = cls.Insert_PriceList_GP_ActualEstimate(pro);


                        }
                    }
                }

            }
            if (status > 0)
            {
                if (lblStatus.Text=="Actual")
                {
                    Response.Redirect("~/PriceList_GP.aspx");

                }
                else
                {
                    lblCompany_Id.Text = pro.Fk_Company_Id.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DynamicClick", "DynamicClick(" + lblCompany_Id.Text + ", '" + lblEstimateName.Text + "', '" + lblStatus.Text + "'); ", true);

                }



            }

        }

        protected void ReportPopupBtn_Click(object sender, EventArgs e)
        {
            try
            {

                //int BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                //int PMRM_Category_Id=Convert.ToInt32(lblPMRM_Category_Id.Text);
                int BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                int PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);

                DataTable dt = new DataTable();
                DataTable objdtPackSize = new DataTable();
                DataTable objdtCategoryName = new DataTable();
                DataTable objdtOtherCosting = new DataTable();
                DataTable dt4 = new DataTable();
                ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();

                dt = cls.Report_TitleByBPM_PMRMCat_Id_PriceListActualGP(BPM_Id, PMRM_Category_Id);
                lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();
                lblname.Text = dt.Rows[0]["BPM_Product_Name"].ToString();
                string Packsize = dt.Rows[0]["PackingSize"].ToString();
                string PackUnitMeasurement = dt.Rows[0]["Measurement"].ToString();
                String ShipperName = dt.Rows[0]["PM_RM_Category_Name"].ToString();
                lblPackingSize.Text = Regex.Match(Packsize, @"\d+").Value;
                dt = cls.Get_FinishGoodPricingReportBy_BPM_Id(Convert.ToInt32(lblBPM_Id.Text));
                objdtPackSize = cls.Get_PackingMeasuremnt_FinishGoodsReport(Convert.ToInt32(lblBPM_Id.Text));
                objdtCategoryName = cls.Get_PMCategory_finishedGood_Report(Convert.ToInt32(lblBPM_Id.Text));
                //objdtOtherCosting = cls.Get_OtherCosting_Report(Convert.ToInt32(lblBPM_Id.Text));
                objdtOtherCosting = cls.Get_OtherCosting_Report_PriceListGP_Actual(BPM_Id, PMRM_Category_Id);
                StringBuilder htmlTable = new StringBuilder();
                //htmlTable.Append("<th>");
                //htmlTable.Append("<td>" + lblname.Text + "</td></th>");
                htmlTable.Append("Name:<div><h5>" + dt.Rows[0]["BPM_Product_Name"].ToString() + "(" + ShipperName + ")-" + "[" + lblPackingSize.Text + "-" + PackUnitMeasurement + "]</h5></div><div><table border='1' style='width:100%'>");
                htmlTable.Append("<tr class='table-hover table-responsive gridview' ><th>No</th><th>IngredientName</th><th>Formulation(%)</th><th>QTY</th><th>RateAmount_KG</th><th>Amount</th></tr>");

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
                            htmlTable.Append("<td>" + dt.Rows[i]["RateAmount_KG"] + "</td>");
                            htmlTable.Append("<td>" + dt.Rows[i]["Amount"] + "</td>");
                            strFormulation = (decimal.Parse(strFormulation) + decimal.Parse(dt.Rows[i]["Formulation"].ToString())).ToString();
                            InputQty = (decimal.Parse(InputQty) + decimal.Parse(dt.Rows[i]["QTY"].ToString())).ToString();
                            Rate = (decimal.Parse(Rate) + decimal.Parse(dt.Rows[i]["RateAmount_KG"].ToString())).ToString();
                            Amount = (decimal.Parse(Amount) + decimal.Parse(dt.Rows[i]["Amount"].ToString())).ToString();
                            htmlTable.Append("</tr>");
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
                                    DataTable objdt = cls.GET_PMCostingForReport(Convert.ToInt32(lblBPM_Id.Text),
                                                                        Convert.ToInt32(objdtCategoryName.Rows[i]["Fk_PMRM_Category_Id"]),
                                                                        Convert.ToInt32(objdtPackSize.Rows[j]["Pack_size"]),
                                                                        Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"])
                                                                        );
                                    if (objdt != null && objdt.Rows.Count > 0)
                                    {
                                        strPMCost = objdt.Rows[0]["Final_Pack_Cost_Unit"].ToString();
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

                        htmlTable.Append("<tr><td>Suggested Packing Difference</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strSuggPackDifference = string.Empty;

                            var varSuggPackDifference = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varSuggPackDifference.Any())
                            {
                                strSuggPackDifference = varSuggPackDifference.CopyToDataTable().Rows[0]["Suggest_Pack_Diff_Ltr"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strSuggPackDifference) ? 0 : Convert.ToDouble(strSuggPackDifference));
                                htmlTable.Append("<td>" + strSuggPackDifference + " </td>");
                            }
                            else
                            {
                                strSuggPackDifference = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerUnit) ? 0 : Convert.ToDouble(strTotalAmtPerUnit));
                                htmlTable.Append("<td>" + strSuggPackDifference + " </td>");
                            }

                        }
                        //htmlTable.Append("</tr>");
                        //htmlTable.Append("<tr class='table-hover table-responsive gridview overflow-scroll'><th>Total</th>");
                        //for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        //{
                        //    htmlTable.Append("<th>" + dblTotalPerLtr[i].ToString() + " </th>");
                        //}
                        htmlTable.Append("</tr></table><br></div>");


                        //**************** Gujarat PEsticide Factory Expence************************

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Gujarat Pesticide Factory Expence</th>");
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<div><table border='0' style='color: White;width:100%'>");
                        htmlTable.Append("<tr class='table-hover table-responsive gridview'>");
                        htmlTable.Append("<th>Expences</th>");

                        for (int i = 0; i < objdtPackSize.Rows.Count; i++)
                        {
                            htmlTable.Append("<th>" + objdtPackSize.Rows[i]["PackUnitMeasurement"] + " </th>");
                        }
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<tr><td>Factory Expense</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strFactoryExpense = string.Empty;
                            string FectoryExpencePer = string.Empty;
                            var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varTotalAmtPerLiter.Any())
                            {
                                strFactoryExpense = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["FactoryExpenceAmt"].ToString();
                                FectoryExpencePer = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["FectoryExpencePer"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strFactoryExpense) ? 0 : Convert.ToDouble(strFactoryExpense));
                                htmlTable.Append("<td>" + strFactoryExpense + " (" + FectoryExpencePer + "%)" + "</td>");
                            }
                            else
                            {
                                strFactoryExpense = "-";
                                FectoryExpencePer = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strFactoryExpense + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");


                        htmlTable.Append("<tr><td>MarkettedByCharges</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strMarkettedByCharges = string.Empty;
                            string MarketedByChargesPer = string.Empty;
                            var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varTotalAmtPerLiter.Any())
                            {
                                strMarkettedByCharges = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["MarketedByChargesAmt"].ToString();
                                MarketedByChargesPer = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["MarketedByChargesPer"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strMarkettedByCharges) ? 0 : Convert.ToDouble(strMarkettedByCharges));
                                htmlTable.Append("<td>" + strMarkettedByCharges + " (" + MarketedByChargesPer + "%)" + "</td>");
                            }
                            else
                            {
                                strMarkettedByCharges = "-";
                                MarketedByChargesPer = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strMarkettedByCharges + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<tr><td>Other</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strOtherAmt = string.Empty;
                            string OtherPer = string.Empty;
                            var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varTotalAmtPerLiter.Any())
                            {
                                strOtherAmt = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["OtherAmt"].ToString();
                                OtherPer = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["OtherPer"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strOtherAmt) ? 0 : Convert.ToDouble(strOtherAmt));
                                htmlTable.Append("<td>" + strOtherAmt + " (" + OtherPer + "%)" + "</td>");
                            }
                            else
                            {
                                strOtherAmt = "-";
                                OtherPer = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strOtherAmt + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");

                        htmlTable.Append("<tr><td>Total Expence</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strTotalExpence = string.Empty;
                            var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varTotalAmtPerLiter.Any())
                            {
                                strTotalExpence = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["TotalExpence"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalExpence) ? 0 : Convert.ToDouble(strTotalExpence));
                                htmlTable.Append("<td>" + strTotalExpence + "</td>");
                            }
                            else
                            {
                                strTotalExpence = "-";

                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strTotalExpence + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");


                        htmlTable.Append("<tr><td>Profit</td>");
                        for (int j = 0; j < objdtPackSize.Rows.Count; j++)
                        {
                            string strProfitAmt = string.Empty;
                            string ProfitPer = string.Empty;
                            var varTotalAmtPerLiter = objdtOtherCosting.AsEnumerable().Where(r => r.Field<int>("PackingMeasurement") == Convert.ToInt32(objdtPackSize.Rows[j]["Pack_Measurement"]) && r.Field<decimal>("PackingSize") == Convert.ToDecimal(objdtPackSize.Rows[j]["Pack_size"]));
                            if (varTotalAmtPerLiter.Any())
                            {
                                strProfitAmt = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["ProfitAmt"].ToString();
                                ProfitPer = varTotalAmtPerLiter.CopyToDataTable().Rows[0]["ProfitPer"].ToString();
                                dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strProfitAmt) ? 0 : Convert.ToDouble(strProfitAmt));
                                htmlTable.Append("<td>" + strProfitAmt + " (" + ProfitPer + "%)" + "</td>");
                            }
                            else
                            {
                                strProfitAmt = "-";
                                ProfitPer = "-";
                                //dblTotalPerLtr[j] = dblTotalPerLtr[j] + (string.IsNullOrEmpty(strTotalAmtPerLiter) ? 0 : Convert.ToDouble(strTotalAmtPerLiter));
                                htmlTable.Append("<td>" + strProfitAmt + " </td>");
                            }


                        }
                        htmlTable.Append("</tr>");

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
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "alertMessage", "ShowPopup1()", true);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        protected void Grid_PriceList_GP_ActualEstimate_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "100px");
                lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();


                Label lblEnumDescription = (Label) e.Row.FindControl("lblEnumDescription");
                Label lblNewStatus = (Label)e.Row.FindControl("lblNewStatus");
                TextBox PDtxt = (TextBox)e.Row.FindControl("PDtxt");
                if(lblEnumDescription.Text.ToUpper()== "RPL" && lblNewStatus.Text.ToUpper() == "RPL")
                {
                    datajs += "calculaterate('1','" + PDtxt.ClientID + "','1');";
                    //
                }

            }

            //


            foreach (GridViewRow row1 in Grid_PriceList_GP_ActualEstimate.Rows)
            {

                if ((row1.FindControl("lblStatus") as Label).Text == "Estimate")
                {
                    (row1.FindControl("PDtxt") as TextBox).Enabled = false;
                    (row1.FindControl("QDtxt") as TextBox).Enabled = false;
                    if (row1.FindControl("TODtxt") != null)
                    {
                        (row1.FindControl("TODtxt") as TextBox).Enabled = false;
                    }
                    (row1.FindControl("ProfitPertxt") as TextBox).Enabled = false;

                }
                Label lblStatus = row1.FindControl("lblStatus") as Label;
                lblNewStatus.Text = (row1.FindControl("lblNewStatus") as Label).Text;
                lblStatus.Text = (row1.FindControl("lblStatus") as Label).Text;
                string EnumDescription = (row1.FindControl("lblEnumDescription") as Label).Text;
                string FinalPricettxt = (row1.FindControl("FinalPricettxt") as TextBox).Text;



                string SuggestedPriceWithPD = (row1.FindControl("SuggestedPriceWithPDttxt") as TextBox).Text;

                string LastSharedPrice = (row1.FindControl("lblLast_Shared_Final_Price") as Label).Text;
                string LastSharedPrice2 = (row1.FindControl("FinalPricettxt") as TextBox).Text;
                string AdditionalPD = (row1.FindControl("AdditionalPDtxt") as TextBox).Text;
                string GrossProfitAmt = ((row1.FindControl("GrossProfitAmounttxt") as TextBox).Text);
                string GrossProfitPer = ((row1.FindControl("lblGrossProfitPer") as TextBox).Text);
                string TotalExpence = ((row1.FindControl("TotalExpencetxt") as TextBox).Text);
                string NetProfitAmt = ((row1.FindControl("NetProfitAmounttxt") as TextBox).Text);
                string NetProfitPer = ((row1.FindControl("lblNetProfitAmtPer") as TextBox).Text);

                if (lblStatus.Text == "Estimate" && lblNewStatus.Text == "NCR" && EnumDescription == "NCR")
                {
                    GridViewRow FinalRow = Grid_PriceList_GP_ActualEstimate.Rows[row1.RowIndex -1];

                    //FinalPricettxt = (FinalRow.FindControl("FinalPricettxt") as TextBox).Text;
                    (FinalRow.FindControl("FinalPricettxt") as TextBox).Text = FinalPricettxt;
                    //(FinalRow.FindControl("SuggestedPriceWithPDttxt") as Label).Text = SuggestedPriceWithPD;                    
                    (FinalRow.FindControl("lblLast_Shared_Final_Price") as Label).Text = LastSharedPrice;
                    (FinalRow.FindControl("AdditionalPDtxt") as TextBox).Text = AdditionalPD;


                    (FinalRow.FindControl("GrossProfitAmounttxt") as TextBox).Text = GrossProfitAmt;
                    (FinalRow.FindControl("lblGrossProfitPer") as TextBox).Text = GrossProfitPer;
                    (FinalRow.FindControl("TotalExpencetxt") as TextBox).Text = TotalExpence;
                    (FinalRow.FindControl("NetProfitAmounttxt") as TextBox).Text = NetProfitAmt;
                    (FinalRow.FindControl("lblNetProfitAmtPer") as TextBox).Text = NetProfitPer;

                    (FinalRow.FindControl("GrossProfitAmounttxt_lbl") as Label).Text = GrossProfitAmt;
                    (FinalRow.FindControl("lblGrossProfitPer_lbl") as Label).Text = GrossProfitPer;
                    (FinalRow.FindControl("TotalExpencetxt_lbl") as Label).Text = TotalExpence;
                    (FinalRow.FindControl("NetProfitAmounttxt_lbl") as Label).Text = NetProfitAmt;
                    (FinalRow.FindControl("lblNetProfitAmtPer_lbl") as Label).Text = NetProfitPer;



                }

            }
            for (int rowIndex = Grid_PriceList_GP_ActualEstimate.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_PriceList_GP_ActualEstimate.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_PriceList_GP_ActualEstimate.Rows[rowIndex + 1];

                Label lblPrevStateName = gvPreviousRow.FindControl("lblStateName") as Label;
                Label lblStateName = gvRow.FindControl("lblStateName") as Label;

                if (lblPrevStateName.Text == lblStateName.Text)
                {
                    if (gvPreviousRow.Cells[0].RowSpan < 2)
                    {
                        gvRow.Cells[0].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[0].Visible = false;
                }

                Label lblPrevStatus = gvPreviousRow.FindControl("lblStatus") as Label;
                Label lblStatus = gvRow.FindControl("lblStatus") as Label;
                if (lblPrevStatus.Text == lblStatus.Text)
                {
                    if (gvPreviousRow.Cells[1].RowSpan < 2)
                    {
                        gvRow.Cells[1].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[1].Visible = false;
                }
                Label lblPrevNRV = gvPreviousRow.FindControl("lblNRV") as Label;
                Label lblNRV = gvRow.FindControl("lblNRV") as Label;
                if (lblPrevNRV.Text == lblNRV.Text)
                {
                    if (gvPreviousRow.Cells[2].RowSpan < 2)
                    {
                        gvRow.Cells[2].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[2].Visible = false;
                }
                Label lblPrevTransport = gvPreviousRow.FindControl("lblTransport") as Label;
                Label lblTransport = gvRow.FindControl("lblTransport") as Label;
                if (lblPrevTransport.Text == lblTransport.Text)
                {
                    if (gvPreviousRow.Cells[3].RowSpan < 2)
                    {
                        gvRow.Cells[3].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[3].RowSpan = gvPreviousRow.Cells[3].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[3].Visible = false;
                }

                Label lblPrevFinalNRV = gvPreviousRow.FindControl("lblFinalNRV") as Label;
                Label lblFinalNRV = gvRow.FindControl("lblFinalNRV") as Label;
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[4].RowSpan < 2)
                    {
                        gvRow.Cells[4].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[4].RowSpan = gvPreviousRow.Cells[4].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[4].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[11].RowSpan < 2)
                    {
                        gvRow.Cells[11].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[11].RowSpan = gvPreviousRow.Cells[11].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[11].Visible = false;
                }
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[12].RowSpan < 2)
                    {
                        gvRow.Cells[12].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[12].RowSpan = gvPreviousRow.Cells[12].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[12].Visible = false;
                }
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[13].RowSpan < 2)
                    {
                        gvRow.Cells[13].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[13].RowSpan = gvPreviousRow.Cells[13].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[13].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[14].RowSpan < 2)
                    {
                        gvRow.Cells[14].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[14].RowSpan = gvPreviousRow.Cells[14].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[14].Visible = false;
                }
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    gvRow.Cells[14].BackColor = System.Drawing.Color.Yellow;
                    if (gvPreviousRow.Cells[15].RowSpan < 2)
                    {
                        gvRow.Cells[15].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[15].RowSpan = gvPreviousRow.Cells[15].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[15].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[16].RowSpan < 2)
                    {
                        gvRow.Cells[16].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[16].RowSpan = gvPreviousRow.Cells[16].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[16].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    gvRow.Cells[17].BackColor = System.Drawing.Color.LightGreen;

                    if (gvPreviousRow.Cells[17].RowSpan < 2)
                    {
                        gvRow.Cells[17].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[17].RowSpan = gvPreviousRow.Cells[17].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[17].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[18].RowSpan < 2)
                    {
                        gvRow.Cells[18].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[18].RowSpan = gvPreviousRow.Cells[18].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[18].Visible = false;
                }


            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 16; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevFinalNRV = gvPreviousRow.FindControl("lblTotalExpence") as Label;
                    Label lblFinalNRV = gvRow.FindControl("lblTotalExpence") as Label;
                    if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                    {
                        if (gvPreviousRow.Cells[16].RowSpan < 2)
                        {
                            gvRow.Cells[16].RowSpan = 2;

                        }
                        else
                        {
                            gvRow.Cells[16].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;

                        }
                        gvPreviousRow.Cells[16].Visible = false;

                    }
                }
            }


        }
        protected void GPActualEstimateFinalBtn_Click(object sender, EventArgs e)
        {
            GPActualEstimateFinalBtn = new Button();
            lblCompanyMasterList_Id.Text = "1";
            //GPActualEstimateFinalBtn.OnClientClick = "DynamicClick(" + lblCompanyMasterList_Id.Text + ",'" + lblEstimateName.Text + "','" + lblStatus.Text + "');";
            //Response.Redirect("~/PriceList_GP.aspx");
            if (lblStatus.Text == "Estimate")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DynamicClick", "DynamicClick(" + lblCompanyMasterList_Id.Text + ", '" + lblEstimateName.Text + "', '" + lblStatus.Text + "'); ", true);

            }
            else
            {
                Response.Redirect("~/PriceList_GP.aspx");

            }

        }

        protected void CheckBox_Check_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Grid_Default_PriceList_GP_Actual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtNew = ViewState["dtNew"] as DataTable;
                for (int i = 5; i < e.Row.Cells.Count; i++)

                    lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();

            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevStateName = gvPreviousRow.FindControl("lblStateName") as Label;
                    Label lblStateName = gvRow.FindControl("lblStateName") as Label;



                    if (lblPrevStateName.Text == lblStateName.Text)
                    {
                        if (gvPreviousRow.Cells[0].RowSpan < 2)
                        {
                            gvRow.Cells[0].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[0].Visible = false;
                    }
                }
            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 1; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevStatus = gvPreviousRow.FindControl("lblStatus") as Label;
                    Label lblStatus = gvRow.FindControl("lblStatus") as Label;
                    if (lblPrevStatus.Text == lblStatus.Text)
                    {
                        if (gvPreviousRow.Cells[1].RowSpan < 2)
                        {
                            gvRow.Cells[1].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[1].Visible = false;
                    }
                }
            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 2; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevNRV = gvPreviousRow.FindControl("lblNRV") as Label;
                    Label lblNRV = gvRow.FindControl("lblNRV") as Label;
                    if (lblPrevNRV.Text == lblNRV.Text)
                    {
                        if (gvPreviousRow.Cells[2].RowSpan < 2)
                        {
                            gvRow.Cells[2].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[2].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[2].Visible = false;
                    }
                }
            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 3; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevTransport = gvPreviousRow.FindControl("lblTransport") as Label;
                    Label lblTransport = gvRow.FindControl("lblTransport") as Label;
                    if (lblPrevTransport.Text == lblTransport.Text)
                    {
                        if (gvPreviousRow.Cells[3].RowSpan < 2)
                        {
                            gvRow.Cells[3].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[3].RowSpan = gvPreviousRow.Cells[3].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[3].Visible = false;
                    }
                }
            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 4; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevFinalNRV = gvPreviousRow.FindControl("lblFinalNRV") as Label;
                    Label lblFinalNRV = gvRow.FindControl("lblFinalNRV") as Label;
                    if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                    {
                        if (gvPreviousRow.Cells[4].RowSpan < 2)
                        {
                            gvRow.Cells[4].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[4].RowSpan = gvPreviousRow.Cells[4].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[4].Visible = false;
                    }
                }
            }
            for (int rowIndex = Grid_Default_PriceList_GP_Actual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex];
                GridViewRow gvPreviousRow = Grid_Default_PriceList_GP_Actual.Rows[rowIndex + 1];
                for (int cellCount = 5; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevFinalNRV = gvPreviousRow.FindControl("lblFinalNRV") as Label;
                    Label lblFinalNRV = gvRow.FindControl("lblFinalNRV") as Label;
                    if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                    {
                        if (gvPreviousRow.Cells[5].RowSpan < 2)
                        {
                            gvRow.Cells[5].RowSpan = 2;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = 2;

                        }
                        else
                        {
                            gvRow.Cells[5].RowSpan = gvPreviousRow.Cells[5].RowSpan + 1;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan + 1;

                        }
                        gvPreviousRow.Cells[5].Visible = false;
                        gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Visible = false;

                    }
                }
                gvRow.Cells[13].BackColor = System.Drawing.Color.Yellow;
                gvRow.Cells[15].BackColor = System.Drawing.Color.LightGreen;

            }
        }

        protected void PDtxt_TextChanged(object sender, EventArgs e)
        {
            decimal PD = 0;
            decimal QD = 0;
            decimal FinalPrice = 0;
            decimal AdditionalPD = 0;
            //decimal ValueZero = 0;
            decimal StaffExpense = 0;
            decimal Marketing = 0;
            decimal Incentive = 0;
            decimal Interest = 0;
            decimal DepoExpence = 0;
            decimal Other = 0;
            decimal TotalExpence = 0;
            decimal GrossProfitAmount = 0;
            decimal NetProfitAmount = 0;
            decimal ProfitPercent = 0;
            TextBox btn = (TextBox)sender;

            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TextBox ProfitPer = gvr.FindControl("ProfitPertxt") as TextBox;
            ProfitPercent = Convert.ToDecimal(ProfitPer.Text);
            Label PriceType_Id = gvr.FindControl("lblFkPriceTypeId") as Label;
            lblPriceType.Text = PriceType_Id.Text;
            if (lblStatus.Text != "Actual")
            {
                string strNewStatus = (gvr.FindControl("lblNewStatus") as Label).Text;

            }
            int State_Id = Convert.ToInt32((gvr.FindControl("lblFk_State_Id") as Label).Text);
            decimal FinalPricetxt = Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text);
            FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));


            if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "RPL")
            {
                foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                {

                    if ((row2.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("FinalPricettxt") as TextBox).Text != "0.00")
                    {

                        lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                        if (ProfitPercent != 0)
                        {
                            if ((row2.FindControl("AdditionalPDtxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("AdditionalPDtxt") as TextBox).Text = "0.00";
                            }
                            if ((row2.FindControl("FinalPricettxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("FinalPricettxt") as TextBox).Text = "0.00";
                            }

                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                            PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                            QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                            lblFinalPrice.Text = FinalPrice.ToString();
                            AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                            (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                            GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                            (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                            if (ProfitPercent != 0)
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                            }
                            else
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                            }

                            StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                            Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                            Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                            Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                            DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                            Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                            TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                            (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                            NetProfitAmount = GrossProfitAmount - TotalExpence;
                            (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                            (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                            break;
                        }
                        break;
                    }

                }
            }
            if (lblStatus.Text != "Actual" && lblGrid_Status.Text != "Actual")
            {
                if ((gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                {
                    if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                    {
                        foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                        {
                            lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                            if (lblStatus.Text == "Estimate" && (row2.FindControl("lblStatus") as Label).Text == "Estimate" && (row2.FindControl("lblFkPriceTypeId") as Label).Text == "9" && (row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                            {
                                if ((row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                                {
                                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));


                                }

                                lblFinalPrice.Text = FinalPrice.ToString();
                                AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                                if (ProfitPercent != 0)
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                                }
                                else
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                                }

                                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                                NetProfitAmount = GrossProfitAmount - TotalExpence;
                                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                                break;
                            }
                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));

                        }

                    }

                }

            }
            else
            {
                decimal FinalNRV = 0;
                lblGrid_Status.Text = (gvr.FindControl("lblStatus") as Label).Text;
                if (lblStatus.Text == "Estimate" && lblGrid_Status.Text == "Estimate")
                {
                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                    FinalNRV = (Convert.ToDecimal((NextRow.FindControl("lblFinalNRV") as Label).Text));
                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));
                    FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));
                    AdditionalPD = (Convert.ToDecimal((NextRow.FindControl("AdditionalPDtxt") as TextBox).Text));
                }
                else
                {
                    FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                    PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                    QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                    AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                }


                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (FinalNRV) - (QD) - (PD)).ToString();
                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);

                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / FinalNRV).ToString("0.00");

                if (ProfitPercent != 0)
                {
                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                }
                else
                {
                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                }

                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                NetProfitAmount = GrossProfitAmount - TotalExpence;
                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / FinalNRV).ToString("0.00");
                //if (NetProfitAmount < 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NetProfitAmount is Nagetive')", true);

                //}
                //if (GrossProfitAmount < 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('GrossProfitAmount is Nagetive')", true);

                //}
            }

            if (ProfitPercent != 0 || PD != 0 || QD != 0)
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
            }
            else
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

            }
        }

        protected void QDtxt_TextChanged(object sender, EventArgs e)
        {
            decimal PD = 0;
            decimal QD = 0;
            decimal FinalPrice = 0;
            decimal AdditionalPD = 0;
            //decimal ValueZero = 0;
            decimal StaffExpense = 0;
            decimal Marketing = 0;
            decimal Incentive = 0;
            decimal Interest = 0;
            decimal DepoExpence = 0;
            decimal Other = 0;
            decimal TotalExpence = 0;
            decimal GrossProfitAmount = 0;
            decimal NetProfitAmount = 0;
            decimal ProfitPercent = 0;
            TextBox btn = (TextBox)sender;

            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TextBox ProfitPer = gvr.FindControl("ProfitPertxt") as TextBox;
            ProfitPercent = Convert.ToDecimal(ProfitPer.Text);
            Label PriceType_Id = gvr.FindControl("lblFkPriceTypeId") as Label;
            lblPriceType.Text = PriceType_Id.Text;
            if (lblStatus.Text != "Actual")
            {
                string strNewStatus = (gvr.FindControl("lblNewStatus") as Label).Text;

            }
            int State_Id = Convert.ToInt32((gvr.FindControl("lblFk_State_Id") as Label).Text);
            decimal FinalPricetxt = Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text);
            FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));


            if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "RPL")
            {
                foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                {

                    if ((row2.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("FinalPricettxt") as TextBox).Text != "0.00")
                    {

                        lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                        if (ProfitPercent != 0)
                        {
                            if ((row2.FindControl("AdditionalPDtxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("AdditionalPDtxt") as TextBox).Text = "0.00";
                            }
                            if ((row2.FindControl("FinalPricettxt") as TextBox).Text == "")
                            {
                                (row2.FindControl("FinalPricettxt") as TextBox).Text = "0.00";
                            }

                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                            PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                            QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                            lblFinalPrice.Text = FinalPrice.ToString();
                            AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                            (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                            GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                            (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                            if (ProfitPercent != 0)
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                            }
                            else
                            {
                                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                            }

                            StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                            Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                            Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                            Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                            DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                            Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                            TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                            (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                            NetProfitAmount = GrossProfitAmount - TotalExpence;
                            (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                            (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                            break;
                        }
                        break;
                    }

                }
            }
            if (lblStatus.Text != "Actual" && lblGrid_Status.Text != "Actual")
            {
                if ((gvr.FindControl("lblFkPriceTypeId") as Label).Text == "8" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                {
                    if (lblStatus.Text == "Estimate" && (gvr.FindControl("lblStatus") as Label).Text == "Estimate" && (gvr.FindControl("lblNewStatus") as Label).Text == "NCR")
                    {
                        foreach (GridViewRow row2 in Grid_PriceList_GP_ActualEstimate.Rows)
                        {
                            lblFinalNRV.Text = (row2.FindControl("lblFinalNRV") as Label).Text;

                            if (lblStatus.Text == "Estimate" && (row2.FindControl("lblStatus") as Label).Text == "Estimate" && (row2.FindControl("lblFkPriceTypeId") as Label).Text == "9" && (row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                            {
                                if ((row2.FindControl("lblNewStatus") as Label).Text == "NCR")
                                {
                                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));


                                }

                                lblFinalPrice.Text = FinalPrice.ToString();
                                AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (Convert.ToDecimal(lblFinalNRV.Text)) - (QD) - (PD)).ToString();

                                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);
                                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");

                                if (ProfitPercent != 0)
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                                }
                                else
                                {
                                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                                }

                                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                                NetProfitAmount = GrossProfitAmount - TotalExpence;
                                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / Convert.ToDecimal(lblFinalNRV.Text)).ToString("0.00");
                                break;
                            }
                            //FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));

                        }

                    }

                }

            }
            else
            {
                decimal FinalNRV = 0;
                lblGrid_Status.Text = (gvr.FindControl("lblStatus") as Label).Text;
                if (lblStatus.Text == "Estimate" && lblGrid_Status.Text == "Estimate")
                {
                    GridViewRow NextRow = Grid_PriceList_GP_ActualEstimate.Rows[gvr.RowIndex + 1];
                    FinalNRV = (Convert.ToDecimal((NextRow.FindControl("lblFinalNRV") as Label).Text));
                    PD = (Convert.ToDecimal((NextRow.FindControl("PDtxt") as TextBox).Text));
                    QD = (Convert.ToDecimal((NextRow.FindControl("QDtxt") as TextBox).Text));
                    FinalPrice = (Convert.ToDecimal((gvr.FindControl("FinalPricettxt") as TextBox).Text));
                    AdditionalPD = (Convert.ToDecimal((NextRow.FindControl("AdditionalPDtxt") as TextBox).Text));
                }
                else
                {
                    FinalNRV = (Convert.ToDecimal((gvr.FindControl("lblFinalNRV") as Label).Text));
                    PD = (Convert.ToDecimal((gvr.FindControl("PDtxt") as TextBox).Text));
                    QD = (Convert.ToDecimal((gvr.FindControl("QDtxt") as TextBox).Text));
                    AdditionalPD = (Convert.ToDecimal((gvr.FindControl("AdditionalPDtxt") as TextBox).Text));

                }


                (gvr.FindControl("GrossProfitAmounttxt") as Label).Text = ((FinalPrice) - (AdditionalPD) - (FinalNRV) - (QD) - (PD)).ToString();
                GrossProfitAmount = Convert.ToDecimal((gvr.FindControl("GrossProfitAmounttxt") as Label).Text);

                (gvr.FindControl("lblGrossProfitPer") as Label).Text = ((GrossProfitAmount * 100) / FinalNRV).ToString("0.00");

                if (ProfitPercent != 0)
                {
                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
                }
                else
                {
                    (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

                }

                StaffExpense = Convert.ToDecimal((gvr.FindControl("lblStaffExpense") as Label).Text);
                Marketing = Convert.ToDecimal((gvr.FindControl("lblMarketing") as Label).Text);
                Incentive = Convert.ToDecimal((gvr.FindControl("lblIncentive") as Label).Text);
                Interest = Convert.ToDecimal((gvr.FindControl("lblInterest") as Label).Text);
                DepoExpence = Convert.ToDecimal((gvr.FindControl("lblDepoExpence") as Label).Text);
                Other = Convert.ToDecimal((gvr.FindControl("lblOther") as Label).Text);


                TotalExpence = (((FinalPrice * StaffExpense) / 100) + ((FinalPrice * Marketing) / 100) + ((FinalPrice * Incentive) / 100) + ((FinalPrice * Interest) / 100) + ((FinalPrice * DepoExpence) / 100) + ((FinalPrice * Other) / 100));
                (gvr.FindControl("TotalExpencetxt") as Label).Text = TotalExpence.ToString("0.00");

                NetProfitAmount = GrossProfitAmount - TotalExpence;
                (gvr.FindControl("NetProfitAmounttxt") as Label).Text = NetProfitAmount.ToString("0.00");
                (gvr.FindControl("lblNetProfitAmtPer") as Label).Text = (NetProfitAmount * 100 / FinalNRV).ToString("0.00");
                //if (NetProfitAmount < 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('NetProfitAmount is Nagetive')", true);

                //}
                //if (GrossProfitAmount < 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('GrossProfitAmount is Nagetive')", true);

                //}
            }

            if (ProfitPercent != 0 || PD != 0 || QD != 0)
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = true;
            }
            else
            {
                (gvr.FindControl("CheckBox_Check") as CheckBox).Checked = false;

            }
        }
    }
}