using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class Report_RMEstimation : System.Web.UI.Page
    {
        int User_Id;
        int Company_Id;
        ProRMEstimateReport pro = new ProRMEstimateReport();
        ClsRMEstimateReport cls = new ClsRMEstimateReport();
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
                //TradeNameDropDownListCombo();
                DisplayView();
                GetRMIngredients();
                Grid_RMIngredientsEstimate();
                //DynamicButtonCreate();
            }
            else
            {
                //DynamicButtonCreate();
            }

        }



        protected void Grid_RMIngredientsEstimate()
        {
            DataTable dtRM = new DataTable();
            ClsRMEstimateReport cls = new ClsRMEstimateReport();
            pro.Company_Id = Company_Id;
            dtRM = cls.Get_Grid_RM_Estimate_ModifiedPrice(pro);
            Gird_RM_PriceEstimationList.DataSource = dtRM;
            Gird_RM_PriceEstimationList.DataBind();

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
            if (Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]) == true)
            {
                if (lblGRidEstimateName.Text != "")
                {
                    AddModifiedPriceBtn.Visible = false;
                    ModifiedPriceUpdateBtn.Visible = true;
                    ViewRMEstimateReport.Visible = true;
                    AddRMEstimationBtn.Visible = true;

                }
                else
                {
                    AddModifiedPriceBtn.Visible = true;
                    ModifiedPriceUpdateBtn.Visible = false;
                    ViewRMEstimateReport.Visible = true;
                    AddRMEstimationBtn.Visible = true;

                }

            }
            else
            {
                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewRMEstimateReport.Visible = false;
                AddRMEstimationBtn.Visible = false;

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
                lblCompanyMasterList_Id.Text = Company_Id.ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        protected void GetRMIngredients()
        {
            DataTable dtRM = new DataTable();
            ClsRMEstimateReport cls = new ClsRMEstimateReport();
            dtRM = cls.Get_RMMasterWithData();
            RM_IngredientListbox.DataSource = dtRM;
            RM_IngredientListbox.DataTextField = "RMWithPurity";
            RM_IngredientListbox.DataValueField = "RM_Price_Id";
            RM_IngredientListbox.DataBind();
            RM_IngredientListbox.Items.Insert(0, "Select");

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[31]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[31]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[31]["CanEdit"]) == true)
            {
                if (lblGRidEstimateName.Text != "")
                {
                    AddModifiedPriceBtn.Visible = false;
                    ModifiedPriceUpdateBtn.Visible = true;
                    ViewRMEstimateReport.Visible = true;
                }
                else
                {
                    AddModifiedPriceBtn.Visible = true;
                    ModifiedPriceUpdateBtn.Visible = false;
                    ViewRMEstimateReport.Visible = true;
                }

            }
            else
            {
                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewRMEstimateReport.Visible = false;
            }

        }



        protected void AddRMEstimationBtn_Click(object sender, EventArgs e)
        {
            ClearData();
            if (lblGRidEstimateName.Text != "")
            {
                //GetRMEstimateTableOne();

                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = true;
                ViewRMEstimateReport.Visible = true;
            }
            else
            {

                AddModifiedPriceBtn.Visible = true;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewRMEstimateReport.Visible = true;
            }

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);
        }
        public void ClearData()
        {
            lblGRidEstimateName.Text = "";
            EstimateNametxt.Text = "";
            DOPtxt.Text = "---------";
            Grid_RMEstimateTableOne.Visible = false;
        }
        protected void GetRMEstimateTableOne()
        {
            DataTable dtRM = new DataTable();
            ClsRMEstimateReport cls = new ClsRMEstimateReport();
            pro.Company_Id = Company_Id;

            DataTable dt = new DataTable();
            dt = cls.GET_EstimateDetailByName(pro);
            DateTime d = Convert.ToDateTime(dt.Rows[0]["EstimateDate"].ToString());
            DOPtxt.Text = d.ToString("yyyy-MM-dd");
            //DOPtxt.Text = dt.Rows[0]["EstimateDate"].ToString();
            EstimateNametxt.Text = dt.Rows[0]["EstimateName"].ToString();
            Grid_RMEstimateTableOne.DataSource = dt;
            Grid_RMEstimateTableOne.DataBind();
        }


        protected void ChkRMSubmit_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //pro.EsitmateName  =EstimateNametxt.Text;
            //dt = cls.GET_EstimateDetailByName(pro);
            //if (dt.Rows.Count>0)
            //{
            //    string CheckEstimate = "";
            //    CheckEstimate = dt.Rows[0]["EstimateName"].ToString();
            //    if (EstimateNametxt.Text == CheckEstimate)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('EstimateName Already Exist!')", true);

            //    }
            //}           

            string YrStrList = "";
            int Status;
            foreach (ListItem listItem in RM_IngredientListbox.Items)
            {
                if (listItem.Selected)
                {


                    YrStrList = listItem.Value;
                    pro.Company_Id = Convert.ToInt32(lblCompanyMasterList_Id.Text);
                    pro.RM_ModifiedPrice = 0;
                    pro.EsitmateName = EstimateNametxt.Text;

                    //DateTime d = Convert.ToDateTime(DOPtxt.Text);
                    //DOPtxt.Text = d.ToString("yyyy-MM-dd");
                    pro.dateTime = DateTime.Parse(DOPtxt.Text);
                    pro.UserId = User_Id;
                    pro.RMPrice_Id = Convert.ToInt32(YrStrList);
                    Status = cls.Insert_RM_EstimateTableOne(pro);
                }

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Insert SuccessFul!')", true);

            GetRMEstimateTableOne();
            Grid_RMEstimateTableOne.Visible = true;

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "Message", "ShowPopup1()", true);

        }

        protected void AddModifiedPriceBtn_Click(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            foreach (GridViewRow row in Grid_RMEstimateTableOne.Rows)
            {
                TextBox ModifiedPricetxt = row.FindControl("ModifiedPricetxt") as TextBox;
                //Label lblEstimate_RMPrice_Id = row.FindControl("Estimate_RMPrice_Id") as Label;
                Label lblRM_EstimateTabOne_Id = row.FindControl("lblRM_EstimateTabOne_Id") as Label;
                Label lblEsitmateName = row.FindControl("lblEsitmateName") as Label;
                Label lblEstimateDate = row.FindControl("lblEstimateDate") as Label;

                pro.UserId = User_Id;
                pro.Company_Id = Company_Id;
                int result;
                SqlCommand cmd = new SqlCommand("sp_Update_RM_EstimateTableOne", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RM_EstimateTabOne_Id", lblRM_EstimateTabOne_Id.Text);
                cmd.Parameters.AddWithValue("@EstimateName", lblEsitmateName.Text);
                cmd.Parameters.AddWithValue("@EstimateDate", lblEstimateDate.Text);

                cmd.Parameters.AddWithValue("@UpdatedBy", pro.UserId);
                cmd.Parameters.AddWithValue("@RM_ModifiedPrice", ModifiedPricetxt.Text);
                cmd.Parameters.AddWithValue("@Fk_companyId", pro.Company_Id);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted SuccessFul!')", true);
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void ViewRMEstimateReport_Click(object sender, EventArgs e)
        {

            string EstimateName = "";
            foreach (GridViewRow row in Grid_RMEstimateTableOne.Rows)
            {
                EstimateName = (row.FindControl("lblEsitmateName") as Label).Text;
                break;
            }


            DataTable dtBulkCount = new DataTable();
            dtBulkCount = cls.GetBulkCountFromCompany(EstimateName);



            DataTable dtRM = new DataTable();
            dtRM = cls.GET_BULK_FOR_RM_ESTIMATE(EstimateName);
            Grid_RM_EstimateReport.DataSource = dtRM;
            Grid_RM_EstimateReport.DataBind();


            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPopup1()", true);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowRMEstimateReport()", true);

        }

        protected void GridEdit_Click(object sender, EventArgs e)
        {
            Button GridEstimateEdit = sender as Button;
            GridViewRow gdrow = GridEstimateEdit.NamingContainer as GridViewRow;
            string GRidEstimateName = Gird_RM_PriceEstimationList.DataKeys[gdrow.RowIndex].Value.ToString();
            lblGRidEstimateName.Text = GRidEstimateName.ToString();
            pro.Company_Id = Company_Id;
            pro.EsitmateName = GRidEstimateName;
            GetRMEstimateTableOne();

            DataTable dt = new DataTable();
            dt = cls.GET_EstimateDetailByName(pro);
            DateTime d = Convert.ToDateTime(dt.Rows[0]["EstimateDate"].ToString());
            DOPtxt.Text = d.ToString("yyyy-MM-dd");
            //DOPtxt.Text = dt.Rows[0]["EstimateDate"].ToString();
            EstimateNametxt.Text = dt.Rows[0]["EstimateName"].ToString();
            Grid_RMEstimateTableOne.DataSource = dt;
            Grid_RMEstimateTableOne.DataBind();
            Grid_RMEstimateTableOne.Visible = true;

            if (lblGRidEstimateName.Text != "")
            {
                GetRMEstimateTableOne();

                AddModifiedPriceBtn.Visible = false;
                ModifiedPriceUpdateBtn.Visible = true;
                ViewRMEstimateReport.Visible = true;
            }
            else
            {

                AddModifiedPriceBtn.Visible = true;
                ModifiedPriceUpdateBtn.Visible = false;
                ViewRMEstimateReport.Visible = true;
            }

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);
        }

        protected void ModifiedPriceUpdateBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            foreach (GridViewRow row in Grid_RMEstimateTableOne.Rows)
            {

                TextBox ModifiedPricetxt = row.FindControl("ModifiedPricetxt") as TextBox;
                Label lblEstimate_RMPrice_Id = row.FindControl("Estimate_RMPrice_Id") as Label;
                Label lblRM_EstimateTabOne_Id = row.FindControl("lblRM_EstimateTabOne_Id") as Label;
                Label lblEsitmateName = row.FindControl("lblEsitmateName") as Label;
                Label lblEstimateDate = row.FindControl("lblEstimateDate") as Label;

                pro.UserId = User_Id;
                pro.Company_Id = Company_Id;
                int result;
                SqlCommand cmd = new SqlCommand("sp_Update_RM_EstimateTableOne", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RM_EstimateTabOne_Id", lblRM_EstimateTabOne_Id.Text);
                cmd.Parameters.AddWithValue("@EstimateName", lblEsitmateName.Text);
                cmd.Parameters.AddWithValue("@EstimateDate", lblEstimateDate.Text);

                cmd.Parameters.AddWithValue("@UpdatedBy", pro.UserId);
                cmd.Parameters.AddWithValue("@RM_ModifiedPrice", ModifiedPricetxt.Text);
                cmd.Parameters.AddWithValue("@Fk_companyId", pro.Company_Id);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated SuccessFul!')", true);
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
        }

        protected void EstimateRateModal_Click(object sender, EventArgs e)
        {
            Button GridEstimateEdit = sender as Button;
            GridViewRow gdrow = GridEstimateEdit.NamingContainer as GridViewRow;
            string GRidEstimateName = Gird_RM_PriceEstimationList.DataKeys[gdrow.RowIndex].Value.ToString();
            lblGRidEstimateName.Text = GRidEstimateName.ToString();
            pro.Company_Id = Company_Id;
            pro.EsitmateName = GRidEstimateName;
            EstimateHeader.Text = lblGRidEstimateName.Text;
            //GetRMEstimateTableOne();

            //string EstimateName = "";
            //foreach (GridViewRow row in Grid_RMEstimateTableOne.Rows)
            //{
            //    EstimateName = (row.FindControl("lblEsitmateName") as Label).Text;
            //    break;
            //}
            DataTable dtBulkCount = new DataTable();
            dtBulkCount = cls.GetBulkCountFromCompany(GRidEstimateName);

            Button ButtonChange;
            for (int i = 0; i < dtBulkCount.Rows.Count; i++)
            {
                ButtonChange = new Button();
                ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                 
                lblStatus.Text= "Estimate";
                lblCompanyName.Text = dtBulkCount.Rows[i]["CompanyName"].ToString();
                lblCompanyMasterList_Id.Text = dtBulkCount.Rows[i]["CompanyMaster_Id"].ToString();
                string DynamicCompBtnName = lblCompanyName.Text + " (" + dtBulkCount.Rows[i]["BulkCount"] + ")";
                ButtonChange.Text = DynamicCompBtnName;
                ButtonChange.ID = "change_" + i.ToString();
                ButtonChange.Font.Size = FontUnit.Point(9);
                ButtonChange.ControlStyle.CssClass = "btn btn-primary btn-block m-1";
                ButtonChange.OnClientClick = "return DynamicClick(" + lblCompanyMasterList_Id.Text + ",'" + lblGRidEstimateName.Text + "','" + lblStatus.Text + "');";
                ButtonChange.Click += new EventHandler(ButtonChange_Click);
                Panel1.Controls.Add(ButtonChange);
            }

            //foreach (DataRow row in dtBulkCount.Rows)
            //{
            //    lblCompanyName.Text = row["CompanyName"].ToString();
            //    lblCompBulkCount.Text = row["BulkCount"].ToString();

            //    if (lblCompanyName.Text == "GP")
            //    {
            //        lnkGPBtn.Visible = true;
            //        lblCompGPNameForTitle.Text = row["CompanyName"].ToString();
            //        lblCompGPName.Text = row["CompanyName"].ToString();
            //        lblGPBtn.Text = lblCompBulkCount.Text;
            //        lblCompanyGP_Id.Text = row["CompanyMaster_Id"].ToString();
            //    }
            //    else if (lblCompanyName.Text == "Agrostar")
            //    {
            //        lnkAgroStarBtn.Visible = true;

            //        lblCompAgrostarForTitle.Text = row["CompanyName"].ToString();
            //        lblCompAgrostarName.Text = row["CompanyName"].ToString();
            //        lblAgroStarBtn.Text = lblCompBulkCount.Text;
            //        lblCompanyAgro_Id.Text = row["CompanyMaster_Id"].ToString();

            //    }
            //    else if (lblCompanyName.Text == "Gramofone")
            //    {
            //        lnkGramofoneBtn.Visible = true;

            //        lblCompGramofoneforTitle.Text = row["CompanyName"].ToString();
            //        lblCompGramofoneName.Text = row["CompanyName"].ToString();
            //        lblGramofoneBtn.Text = lblCompBulkCount.Text;
            //        lblCompanyGramo_Id.Text = row["CompanyMaster_Id"].ToString();

            //    }
            //    else if (lblCompanyName.Text == "MPPL")
            //    {
            //        lnkMPPLBtn.Visible = true;

            //        lblCompMPPLforTitle.Text = row["CompanyName"].ToString();
            //        lblCompMPPLName.Text = row["CompanyName"].ToString();
            //        lblMPPLBtn.Text = lblCompBulkCount.Text;
            //        lblCompanyMPPL_Id.Text = row["CompanyMaster_Id"].ToString();

            //    }
            //    else if (lblCompanyName.Text == "Dehaat")
            //    {
            //        lnkDehaatBtn.Visible = true;

            //        lblCompDehaatForTitle.Text = row["CompanyName"].ToString();
            //        lblCompDehaatName.Text = row["CompanyName"].ToString();
            //        lblDehaatBtn.Text = lblCompBulkCount.Text;
            //        lblCompanyDehaat_Id.Text = row["CompanyMaster_Id"].ToString();

            //    }
            //}

            DataTable dtRM = new DataTable();
            dtRM = cls.GET_BULK_FOR_RM_ESTIMATE(GRidEstimateName);
            Grid_RM_EstimateReport.DataSource = dtRM;
            Grid_RM_EstimateReport.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPopup1()", true);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowRMEstimateReport()", true);
        }
        public void DynamicButtonCreate()
        {
            DataTable dtBulkCount = new DataTable();
            dtBulkCount = cls.GetBulkCountFromCompany(lblGRidEstimateName.Text);
            Button ButtonChange;
            for (int i = 0; i < dtBulkCount.Rows.Count; i++)
            {
                ButtonChange = new Button();
                ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");

                lblCompanyName.Text = dtBulkCount.Rows[i]["CompanyName"].ToString();
                string DynamicCompBtnName = lblCompanyName.Text + " (" + dtBulkCount.Rows[i]["BulkCount"] + ")";
                ButtonChange.Text = DynamicCompBtnName;
                ButtonChange.ID = "change_" + i.ToString();
                ButtonChange.Font.Size = FontUnit.Point(9);
                ButtonChange.ControlStyle.CssClass = "btn btn-primary btn-block m-1";
                ButtonChange.Click += new EventHandler(ButtonChange_Click);
                Panel1.Controls.Add(ButtonChange);

            }
        }
        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            string str = hfDynamicCompId.Value;
            Button btn = (Button)sender;

            string id = btn.ID;
        }

        protected void RMReport_Click(object sender, EventArgs e)
        {

        }

        protected void RMEstReport_Click(object sender, EventArgs e)
        {


            pro.Company_Id = Company_Id;

            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int BPM_Id = Convert.ToInt32(Grid_RM_EstimateReport.DataKeys[gdrow.RowIndex].Value.ToString());
            ClsRMEstimateReport clsRME = new ClsRMEstimateReport();

            DataTable dt = new DataTable();
            DataTable objdtPackSize = new DataTable();
            DataTable objdtCategoryName = new DataTable();
            DataTable objdtOtherCosting = new DataTable();
            DataTable dt4 = new DataTable();
            pro.BPM_Id = BPM_Id;

            dt = clsRME.GetBPM_Id_FROM_RMEstimateReport(BPM_Id, pro.Company_Id);
            lblBPM_Id.Text = dt.Rows[0]["BPM_Id"].ToString();
            lblPrice_Id.Text = dt.Rows[0]["Estimate_RMPrice_Id"].ToString();
            lblname.Text = dt.Rows[0]["BPM_Product_Name"].ToString();
            string Packsize = dt.Rows[0]["PackingSize"].ToString();
            string PackUnitMeasurement = dt.Rows[0]["Measurement"].ToString();
            String ShipperName = dt.Rows[0]["PM_RM_Category_Name"].ToString();
            pro.RMPrice_Id = Convert.ToInt32(lblPrice_Id.Text);
            lblPackingSize.Text = Regex.Match(Packsize, @"\d+").Value;

            dt = clsRME.Get_RM_EstimateReportBy_BPM_Id(pro, lblGRidEstimateName.Text);
            //HeaderForRMEstimate.Text = lblGRidEstimateName.Text;
            ClsFactoryExpenceMaster cls = new ClsFactoryExpenceMaster();
            objdtPackSize = cls.Get_PackingMeasuremnt_FinishGoodsReport(Convert.ToInt32(lblBPM_Id.Text));
            objdtCategoryName = cls.Get_PMCategory_finishedGood_Report(Convert.ToInt32(lblBPM_Id.Text));
            objdtOtherCosting = clsRME.Get_ModifiedOtherCosting_Report(Convert.ToInt32(lblBPM_Id.Text), pro.Company_Id, lblGRidEstimateName.Text);

            StringBuilder htmlTable = new StringBuilder();
            //htmlTable.Append("<th>");
            //htmlTable.Append("<td>" + lblname.Text + "</td></th>");
            htmlTable.Append("Name:<div><h5>" + lblname.Text + " " + "(" + ShipperName + ")-" + "[" + lblPackingSize.Text + "-" + PackUnitMeasurement + "]</h5></div><div><table border='1' style='width:100%'>");
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
                        htmlTable.Append("<td>" + dt.Rows[i]["RM_ModifiedPrice"] + "</td>");
                        htmlTable.Append("<td>" + dt.Rows[i]["TransportRate"] + "</td>");
                        htmlTable.Append("<td>" + dt.Rows[i]["TotalModifiedAmount"] + "</td>");
                        strFormulation = (decimal.Parse(strFormulation) + decimal.Parse(dt.Rows[i]["Formulation"].ToString())).ToString();
                        InputQty = (decimal.Parse(InputQty) + decimal.Parse(dt.Rows[i]["QTY"].ToString())).ToString();
                        Rate = (decimal.Parse(Rate) + decimal.Parse(dt.Rows[i]["RM_ModifiedPrice"].ToString())).ToString();
                        TransportRate = (decimal.Parse(TransportRate) + decimal.Parse(dt.Rows[i]["TransportRate"].ToString())).ToString();
                        Amount = (decimal.Parse(Amount) + decimal.Parse(dt.Rows[i]["TotalModifiedAmount"].ToString())).ToString();
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
                    string strFinalBulkCost = "0";
                    var varFinalBulkCost = dt.AsEnumerable().Where(r => r.Field<decimal>("FinalBulkCost") != 0);
                    if (varFinalBulkCost.Any())
                    {
                        strFinalBulkCost = Convert.ToString(varFinalBulkCost.CopyToDataTable().Rows[0]["FinalBulkCost"]);
                    }
                    htmlTable.Append("<tr class='table-hover table-responsive gridview'><th style='width:70%;text-align: left;'>Final Bulk Cost/Ltr or Kg (With Transport)</th>");
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

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowRMReport()", true);

        }

        protected void ReportClose_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#RM_ReportModal').modal('hide').data-bs-dismiss", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideRMReport", "$('#ShowRMReport').modal('hide').data-bs-dismiss", true);
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "HidePMRMEstimateReport()", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowRMEstimateReport()", true);
        }



        protected void StateWiseRM_ReportClose_Click(object sender, EventArgs e)
        {

        }

        protected void DelGridRmModifiedBtn_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            string Grid_RMEstimateTableOne_Id = Grid_RMEstimateTableOne.DataKeys[gdrow.RowIndex].Value.ToString();

            lblPrice_Id.Text = (gdrow.FindControl("lblEstimate_RMPrice_Id") as Label).Text;
            pro.RMPrice_Id = Convert.ToInt32(lblPrice_Id.Text);
            pro.RMEstimateTableOne_Id = Convert.ToInt32(Grid_RMEstimateTableOne_Id);
            pro.Company_Id = 0;




            int status = 0;
            status = cls.Delete_ModifiedRM_Estimation(pro);
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

        protected void ReportCloseRMReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideRMReport", "$('#HideRMReport').modal('hide').data-bs-dismiss", true);
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowRMReport", "$('#ShowRMReport').modal('show').data-bs-dismiss", true);
            //ShowCompanyName();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowRMEstimateReport()", true);

        }

        protected void staticBackdropCloseBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);

        }


        protected void CloseRMEstimateModel_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideRMEstimateReport", "$('#HideRMEstimateReport').modal('hide').data-bs-dismiss", true);
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void GridDelete_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            string EstimateName = Gird_RM_PriceEstimationList.DataKeys[gdrow.RowIndex].Value.ToString();
            pro.EsitmateName = EstimateName;
            int status = cls.Delete_Grid_RM_EstimateName(pro);
            if (status > 0)
            {


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                Grid_RMIngredientsEstimate();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed')", true);

            }
        }

        protected void btnDynamic_Click(object sender, EventArgs e)
        {
            string str = hfDynamicCompId.Value;
            Button btn = (Button)sender;

            string id = btn.ID;
        }
    }
}