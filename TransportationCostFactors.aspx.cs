using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class TransportationCostFactors : System.Web.UI.Page
    {
        ProTransportationCostFactors pro = new ProTransportationCostFactors();
        ClsTransportationCostFactors cls = new ClsTransportationCostFactors();
        int User_Id;
        int Company_Id;
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
                StatewiseDropdownList();
                Grid_TransportationCostFactorsData();
                MeasurementDataCombo();
                LocalCartageMeasurementDataCombo();
                AverageTransportDropdownCombo();
                DisplayView();
            }

        }
        public void StatewiseDropdownList()
        {

            StatewiseDropdown.DataSource = cls.Get_AllStateData();
            StatewiseDropdown.DataTextField = "StateName";
            StatewiseDropdown.DataValueField = "StateID";
            StatewiseDropdown.DataBind();
            StatewiseDropdown.Items.Insert(0, "Select State");
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
            if (lblCanEdit.Text == "True")
            {
                AddTransportationbtn.Visible = true;
                CancelTransportationBtn.Visible = true;
                
            }
            else
            {
                AddTransportationbtn.Visible = false;
                //CancelTransportationBtn.Visible = false;
               

            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[24]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[24]["CanEdit"]) == true)
            {
                AddTransportationbtn.Visible = true;
                CancelTransportationBtn.Visible = true;
                AddLocalCartageBtn.Visible = true;
                AddAverageLocalTransport.Visible = true;
                AddUnloadingChargelBtn.Visible = true;
            }
            else
            {
                AddTransportationbtn.Visible = false;
                //CancelTransportationBtn.Visible = false;
                AddLocalCartageBtn.Visible = false;
                AddAverageLocalTransport.Visible = false;
                AddUnloadingChargelBtn.Visible = false;

            }
        }

        public void GetLoginDetails()
        {
            if (Session["UserName"]!=null)
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
        public void Grid_TransportationCostFactorsData()
        {
            pro.Company_Id = Company_Id;
            Grid_TransportationCostFactors.DataSource = cls.Get_TransportationCostFactorsAll(pro);
            Grid_TransportationCostFactors.DataBind();
        }
        protected void AddTransportationbtn_Click(object sender, EventArgs e)
        {

            pro.Transportation_State_Id = Convert.ToInt32(StatewiseDropdown.SelectedValue);
            pro.TotalTruckLoadCharge = Convert.ToInt32(TruckloadChargetxt.Text);
            pro.ApproxNoOfCartonIn1Lot = Convert.ToInt32(ApproxNoOfCartonIn1Lottxt.Text);

            pro.Approx1CartonCharge = decimal.Parse(Approx1CartonChargetxt.Text);
            //pro.AverageLocalTraspoation = Convert.ToInt32(AverageLocalTraspoationtxt.Text);
            pro.Company_Id = Company_Id;
            pro.User_Id = User_Id;
            int status = cls.Insert_TransportationCostFactors(pro);

            if (status > 0)
            {
                Grid_TransportationCostFactorsData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearDataTRansportCost();
            }
        }
        public void MeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeasurementDropdownList.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeasurementDropdownList.DataTextField = "EnumDescription";
            UnitMeasurementDropdownList.DataValueField = "PkEnumId";
            UnitMeasurementDropdownList.DataBind();
            UnitMeasurementDropdownList.Items.Insert(0, "Select");
        }
        public void LocalCartageMeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeasurementDropdownLocalCartageList.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeasurementDropdownLocalCartageList.DataTextField = "EnumDescription";
            UnitMeasurementDropdownLocalCartageList.DataValueField = "PkEnumId";
            UnitMeasurementDropdownLocalCartageList.DataBind();
            UnitMeasurementDropdownLocalCartageList.Items.Insert(0, "Select");
        }
        public void AverageTransportDropdownCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            AverageTransportDropdownComboList.DataSource = cls.GetEnumMasterMeasurement();

            AverageTransportDropdownComboList.DataTextField = "EnumDescription";
            AverageTransportDropdownComboList.DataValueField = "PkEnumId";
            AverageTransportDropdownComboList.DataBind();
            AverageTransportDropdownComboList.Items.Insert(0, "Select");
        }


        protected void AddUnloadingChargelBtn_Click(object sender, EventArgs e)
        {
            pro.UnitMeasurement_Id = Convert.ToInt32(UnitMeasurementDropdownList.SelectedValue);
            pro.UnloadingStart = decimal.Parse(UnloadingChargeStarttxt.Text);
            pro.UnloadingEnd = decimal.Parse(UnloadingChargeEndtxt.Text);
            pro.User_Id = User_Id;
            pro.UnloadingAmount = decimal.Parse(UnloadingChargeAmounttxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.Company_Id = Company_Id;

            int status = cls.Insert_UnloadingCharge(pro);

            if (status > 0)
            {
                Grid_UnloadingChargeData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearData();
            }
        }
        public void Grid_UnloadingChargeData()
        {
            pro.User_Id = User_Id;
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            DataTable dt = new DataTable();
            dt = cls.Get_TransportUnloadingChargeById(pro);
            if (dt.Rows.Count > 0)
            {
                Grid_UnloadingCharge.DataSource = dt;

                Grid_UnloadingCharge.DataBind();
            }
            else
            {
                Grid_UnloadingCharge.DataSource = "";

                Grid_UnloadingCharge.DataBind();
            }

        }
        public void Grid_LocalCartageChargeData()
        {
            pro.User_Id = User_Id;
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            DataTable dt = new DataTable();
            dt = cls.Get_TransportLocalCartageChargeById(pro);
            if (dt.Rows.Count > 0)
            {
                Grid_LocalCartage.DataSource = dt;

                Grid_LocalCartage.DataBind();
            }
            else
            {
                Grid_LocalCartage.DataSource = "";

                Grid_LocalCartage.DataBind();
            }

        }
        public void Grid_AverageTransportData()
        {
            pro.Company_Id = Company_Id;
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            DataTable dt = new DataTable();
            dt = cls.Get_AverageLocalTransportByCostFectorId(pro);
            if (dt.Rows.Count > 0)
            {
                Grid_AverageTransport.DataSource = dt;
                Grid_AverageTransport.DataBind();
               
            }
            else
            {
                Grid_AverageTransport.DataSource ="";
                Grid_AverageTransport.DataBind();
             

            }
        }
        public void ClearData()
        {
            UnitMeasurementDropdownList.SelectedIndex = -1;

            UnloadingChargeStarttxt.Text = "";
            UnloadingChargeEndtxt.Text = "";
            UnloadingChargeAmounttxt.Text = "";

        }
        public void LocalCartageClearData()
        {
            UnitMeasurementDropdownLocalCartageList.SelectedIndex = -1;

            LocalCartageStarttxt.Text = "";
            LocalCartageEndtxt.Text = "";
            LocalCartageAmounttxt.Text = "";

        }
        public void AverageLocalTransportClearData()
        {
            AverageTransportDropdownComboList.SelectedIndex = -1;

            AverageLocalTransportAmounttxt.Text = "";
            AverageLocalTransportStarttxt.Text = "";
            AverageLocalTransportEndtxt.Text = "";
            lblAverageTransport_Id.Text = "";
            AddAverageLocalTransport.Visible = true;
            UpdateAverageLocalTransport.Visible = false;
        }

        protected void AddLocalCartageBtn_Click(object sender, EventArgs e)
        {
            pro.UnitMeasurement_Id = Convert.ToInt32(UnitMeasurementDropdownLocalCartageList.SelectedValue);
            pro.LocalCartageStart = Convert.ToInt32(LocalCartageStarttxt.Text);
            pro.LocalCartageEnd = Convert.ToInt32(LocalCartageEndtxt.Text);
            pro.User_Id = User_Id;
            pro.LocalCartageAmount = decimal.Parse(LocalCartageAmounttxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.Company_Id = Company_Id;

            int status = cls.Insert_TransportLocalCartageCharge(pro);

            if (status > 0)
            {
                Grid_UnloadingChargeData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                LocalCartageClearData();
                Grid_LocalCartageChargeData();
            }
        }

        protected void DelTransportationCostFactorsBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int TransportationCostFactors_Id = Convert.ToInt32(Grid_TransportationCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());

            pro.TransportationCostFactors_Id = TransportationCostFactors_Id;
            pro.User_Id = User_Id;
            pro.Company_Id = Company_Id;
            int status = cls.Delete_TransportationCostFactors(pro);
            if (status > 0)
            {
                Grid_TransportationCostFactorsData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Delete..It Used in Another Master')", true);
            }
        }

        protected void EditTransportationCostFactorsBtn_Click(object sender, EventArgs e)
        {

            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int TransportationCostFactors_Id = Convert.ToInt32(Grid_TransportationCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.TransportationCostFactors_Id = TransportationCostFactors_Id;
            lbltransportationCostFactor_Id.Text = TransportationCostFactors_Id.ToString(); ;
            DataTable dt = new DataTable();
            pro.Company_Id = Company_Id;
            dt = cls.Get_TransportationCostFactorsGridById(pro);

            TruckloadChargetxt.Text = dt.Rows[0]["TotalTruckLoadCharge"].ToString(); ;
            ApproxNoOfCartonIn1Lottxt.Text = Convert.ToInt32(dt.Rows[0]["ApproxNoOfCartonIn1Lot"]).ToString();

            Approx1CartonChargetxt.Text = dt.Rows[0]["Approx1CartonCharge"].ToString();
            //AverageLocalTraspoationtxt.Text = dt.Rows[0]["AverageLocalTraspoation"].ToString();
            lblState_Id.Text = dt.Rows[0]["Fk_State_Id"].ToString();
            StatewiseDropdown.SelectedValue = dt.Rows[0]["Fk_State_Id"].ToString();


            UpdateTransportationbtn.Visible = true;
            AddTransportationbtn.Visible = false;
        }

        protected void DelUnloadingChargeBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int LocalCartage_Id = Convert.ToInt32(Grid_UnloadingCharge.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.User_Id = User_Id;

            pro.TransportLocalCartageCharge_Id = LocalCartage_Id;
            pro.Company_Id = Company_Id;
            int status = cls.Delete_TransportUnloadingCharge(pro);
            if (status > 0)
            {
                Grid_TransportationCostFactorsData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Delete..It Used in Another Master')", true);
            }
        }

        protected void EditUnloadingChargeBtn_Click(object sender, EventArgs e)
        {

        }
        // ----------------------------------------Popup--------------------------------
        protected void AverageLocalTransportationAddRangePopup_Click(object sender, EventArgs e)
        {
            Button Add = sender as Button;
            GridViewRow gdrow = Add.NamingContainer as GridViewRow;
            pro.TransportationCostFactors_Id = Convert.ToInt32(Grid_TransportationCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            lbltransportationCostFactor_Id.Text = pro.TransportationCostFactors_Id.ToString();
            pro.Company_Id = Company_Id;
            //pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.User_Id = User_Id;

            DataTable dt1 = new DataTable();
            dt1 = cls.Get_TransportationCostFactorsById(pro);
            if (dt1.Rows.Count>0)
            {
                lblStateNameAve.Text = dt1.Rows[0]["StateName"].ToString();
                lblState_Id.Text = dt1.Rows[0]["StateID"].ToString();
            }
          
            Grid_AverageTransportData();
            //UpdateAverageLocalTransport.Visible = false;
            //AddAverageLocalTransport.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup2();", true);

        }
        protected void UnloadingChargeAddRangePopup_Click(object sender, EventArgs e)
        {

            Button Add = sender as Button;
            GridViewRow gdrow = Add.NamingContainer as GridViewRow;
            pro.TransportationCostFactors_Id = Convert.ToInt32(Grid_TransportationCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            lbltransportationCostFactor_Id.Text = pro.TransportationCostFactors_Id.ToString();
            pro.Company_Id = Company_Id;

            DataTable dt1 = new DataTable();
            dt1 = cls.Get_TransportationCostFactorsById(pro);
            lblStateName.Text = dt1.Rows[0]["StateName"].ToString();
            lblState_Id.Text = dt1.Rows[0]["StateID"].ToString();
            Grid_UnloadingChargeData();
            //UpdateUnloadingChargelBtn.Visible = false;
            //AddUnloadingChargelBtn.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }
        protected void LocalCartageAddRangePopup_Click(object sender, EventArgs e)
        {
            Button Add = sender as Button;
            GridViewRow gdrow = Add.NamingContainer as GridViewRow;
            pro.TransportationCostFactors_Id = Convert.ToInt32(Grid_TransportationCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            lbltransportationCostFactor_Id.Text = pro.TransportationCostFactors_Id.ToString();
            pro.Company_Id = Company_Id;

            DataTable dt1 = new DataTable();
            dt1 = cls.Get_TransportationCostFactorsById(pro);
            lblLocalCartageState.Text = dt1.Rows[0]["StateName"].ToString();
            lblState_Id.Text = dt1.Rows[0]["StateID"].ToString();

            Grid_LocalCartageChargeData();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup1();", true);

        }
        protected void TransportReport_Click(object sender, EventArgs e)
        {
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int TransportationCostFactors_Id = Convert.ToInt32(Grid_TransportationCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            //pro.AverageLocalTransport_Id = TransportationCostFactors_Id;
            //lblAverageTransport_Id.Text = TransportationCostFactors_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            pro.TransportationCostFactors_Id = TransportationCostFactors_Id;
            pro.Company_Id = Company_Id;
            dt = cls.Get_TransportationCostFactorsGridById(pro);
            lblState_Id.Text = dt.Rows[0]["Fk_State_Id"].ToString();
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            DataTable dtReport = new DataTable();


            dtReport = cls.Get_TransportationCostingReportByStateId(pro);
            if (dtReport.Rows.Count > 0)
            {
                //Grid_TransportReportList.DataSource = dtReport;
                //Grid_TransportReportList.DataBind();

                Session["TransportationCostFactors_Id"] = pro.TransportationCostFactors_Id;
                Session["Company_Id"] = Company_Id;
                Session["StateID"] = lblState_Id.Text;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openMsdfsdodal", "window.open('Report_TransportingCosting.aspx' ,'_blank');", true);
                //Response.Redirect("~/Report_TransportingCosting.aspx",true);
                //string URLRedirect = "Report_TransportingCosting.aspx";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URLRedirect + "','_blank')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found !')", true);
                return;
            }
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup3();", true);

        }
        //--------------------------------------------------------End popup----------------------------

        protected void UpdateTransportationbtn_Click(object sender, EventArgs e)
        {
            pro.Transportation_State_Id = Convert.ToInt32(StatewiseDropdown.SelectedValue);
            pro.TotalTruckLoadCharge = decimal.Parse(TruckloadChargetxt.Text);
            pro.ApproxNoOfCartonIn1Lot = decimal.Parse(ApproxNoOfCartonIn1Lottxt.Text);
            pro.User_Id = User_Id;

            pro.Approx1CartonCharge = decimal.Parse(Approx1CartonChargetxt.Text);
            //pro.AverageLocalTraspoation = Convert.ToInt32(AverageLocalTraspoationtxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Company_Id = Company_Id;

            int status = cls.Update_TransportationCostFactors(pro);

            if (status > 0)
            {
                Grid_TransportationCostFactorsData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);
                ClearDataTRansportCost();
                AddTransportationbtn.Visible = true;
                UpdateTransportationbtn.Visible = false;
            }
            else
            {
                AddTransportationbtn.Visible = false;
                UpdateTransportationbtn.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Updated!')", true);

            }
        }
        public void ClearDataTRansportCost()
        {
            StatewiseDropdown.SelectedIndex = 0;
            TruckloadChargetxt.Text = "";
            ApproxNoOfCartonIn1Lottxt.Text = "";
            Approx1CartonChargetxt.Text = "";
            //AverageLocalTraspoationtxt.Text = "";
        }

        protected void CancalTransportationBtn_Click(object sender, EventArgs e)
        {
            ClearDataTRansportCost();
            AddTransportationbtn.Visible = true;
            UpdateTransportationbtn.Visible = false;
        }



        protected void EditUnloadingChargeBtn_Click1(object sender, EventArgs e)
        {
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int TransportUnloadingCharge_Id = Convert.ToInt32(Grid_UnloadingCharge.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.TransportUnloadingCharge_Id = TransportUnloadingCharge_Id;
            lbltransportationUnloadingCharge_Id.Text = TransportUnloadingCharge_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.Company_Id = Company_Id;

            dt = cls.Get_TransportUnloadingChargeGridById(pro);

            UnloadingChargeStarttxt.Text = dt.Rows[0]["UnloadingStart"].ToString();
            UnloadingChargeEndtxt.Text = dt.Rows[0]["UnloadingEnd"].ToString();

            UnloadingChargeAmounttxt.Text = dt.Rows[0]["UnloadingAmount"].ToString();
            lblState_Id.Text = dt.Rows[0]["StateID"].ToString();

            UnitMeasurementDropdownList.SelectedValue = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();


            UpdateUnloadingChargelBtn.Visible = true;
            AddUnloadingChargelBtn.Visible = false;
        }

        protected void DelUnloadingChargeBtn_Click1(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int TransportUnloadingCharge_Id = Convert.ToInt32(Grid_UnloadingCharge.DataKeys[gdrow.RowIndex].Value.ToString());

            pro.TransportUnloadingCharge_Id = TransportUnloadingCharge_Id;
            pro.User_Id = User_Id;
            int status = cls.Delete_TransportUnloadingCharge(pro);
            if (status > 0)
            {
                Grid_UnloadingChargeData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Delete..It Used in Another Master')", true);
            }
        }

        protected void UpdateUnloadingChargelBtn_Click1(object sender, EventArgs e)
        {
            pro.UnitMeasurement_Id = Convert.ToInt32(UnitMeasurementDropdownList.SelectedValue);
            pro.UnloadingStart = decimal.Parse(UnloadingChargeStarttxt.Text);
            pro.UnloadingEnd = decimal.Parse(UnloadingChargeEndtxt.Text);
            pro.User_Id = User_Id;
            pro.UnloadingAmount = decimal.Parse(UnloadingChargeAmounttxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.TransportUnloadingCharge_Id = Convert.ToInt32(lbltransportationUnloadingCharge_Id.Text);
            pro.Company_Id = Company_Id;

            int status = cls.Update_TransportUnloadingCharge(pro);

            if (status > 0)
            {
                Grid_UnloadingChargeData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                ClearData();
                UpdateUnloadingChargelBtn.Visible = false;
                AddUnloadingChargelBtn.Visible = true;
            }
        }

        protected void CancelUnloadingChargelBtn_Click(object sender, EventArgs e)
        {
            UnloadingChargeStarttxt.Text = "";
            UnloadingChargeEndtxt.Text = "";

            UnloadingChargeAmounttxt.Text = "";

            UnitMeasurementDropdownList.SelectedIndex = 0;


            UpdateUnloadingChargelBtn.Visible = false;
            AddUnloadingChargelBtn.Visible = true;
        }

        protected void UpdateLocalCartageBtn_Click(object sender, EventArgs e)
        {
            pro.UnitMeasurement_Id = Convert.ToInt32(UnitMeasurementDropdownLocalCartageList.SelectedValue);
            pro.LocalCartageStart = decimal.Parse(LocalCartageStarttxt.Text);
            pro.LocalCartageEnd = decimal.Parse(LocalCartageEndtxt.Text);
            pro.User_Id = User_Id;
            pro.LocalCartageAmount = decimal.Parse(LocalCartageAmounttxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.Company_Id = Company_Id;
            pro.TransportLocalCartageCharge_Id = Convert.ToInt32(lblLocalCartage_Id.Text);

            int status = cls.Update_TransportLocalCartageCharge(pro);

            if (status > 0)
            {
                Grid_UnloadingChargeData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);
                LocalCartageClearData();
                Grid_LocalCartageChargeData();
                UpdateLocalCartageBtn.Visible = false;
                AddLocalCartageBtn.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed')", true);

            }
        }

        protected void EditLocalCartageBtn_Click1(object sender, EventArgs e)
        {
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int LocalCartage_Id = Convert.ToInt32(Grid_LocalCartage.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.TransportLocalCartageCharge_Id = LocalCartage_Id;
            lblLocalCartage_Id.Text = LocalCartage_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.Company_Id = Company_Id;

            dt = cls.Get_TransportLocalCartageChargeGridById(pro);

            LocalCartageStarttxt.Text = dt.Rows[0]["LocalCartageStart"].ToString();
            LocalCartageEndtxt.Text = dt.Rows[0]["LocalCartageEnd"].ToString();

            LocalCartageAmounttxt.Text = dt.Rows[0]["LocalCartageAmount"].ToString();
            lblState_Id.Text = dt.Rows[0]["StateID"].ToString();

            UnitMeasurementDropdownLocalCartageList.SelectedValue = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();


            UpdateLocalCartageBtn.Visible = true;
            AddLocalCartageBtn.Visible = false;
        }

        protected void DellocalCartageBtn_Click1(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int TransportLocalCartage_Id = Convert.ToInt32(Grid_LocalCartage.DataKeys[gdrow.RowIndex].Value.ToString());

            pro.TransportLocalCartageCharge_Id = TransportLocalCartage_Id;
            pro.Company_Id = Company_Id;
            int status = cls.Delete_TransportLocalCartageCharge(pro);
            if (status > 0)
            {
                Grid_LocalCartageChargeData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Delete..It Used in Another Master')", true);
            }
        }

        protected void TruckloadChargetxt_TextChanged(object sender, EventArgs e)
        {
            if (TruckloadChargetxt.Text != " " && ApproxNoOfCartonIn1Lottxt.Text != "")
            {
                Approx1CartonChargetxt.Text = (decimal.Parse(TruckloadChargetxt.Text) / decimal.Parse(ApproxNoOfCartonIn1Lottxt.Text)).ToString("0.00");
            }
        }

        protected void ApproxNoOfCartonIn1Lottxt_TextChanged(object sender, EventArgs e)
        {
            if (TruckloadChargetxt.Text != " " && ApproxNoOfCartonIn1Lottxt.Text != "")
            {
                Approx1CartonChargetxt.Text = (decimal.Parse(TruckloadChargetxt.Text) / decimal.Parse(ApproxNoOfCartonIn1Lottxt.Text)).ToString("0.00");
            }
        }


        protected void DelAverageTransportChargeBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int AverageTransport_Id = Convert.ToInt32(Grid_AverageTransport.DataKeys[gdrow.RowIndex].Value.ToString());

            pro.AverageLocalTransport_Id = AverageTransport_Id;
            pro.Company_Id = Company_Id;
            int status = cls.Delete_AverageLocalTransport(pro);
            if (status > 0)
            {
                Grid_AverageTransportData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Delete..It Used in Another Master')", true);
            }
        }

        protected void EditAverageTransportBtn_Click(object sender, EventArgs e)
        {
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int AverageTransport_Id = Convert.ToInt32(Grid_AverageTransport.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.AverageLocalTransport_Id = AverageTransport_Id;
            lblAverageTransport_Id.Text = AverageTransport_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.Company_Id = Company_Id;
            dt = cls.Get_AverageLocalTransportById(pro);

            AverageLocalTransportStarttxt.Text = dt.Rows[0]["AverageLocalTransportStart"].ToString();
            AverageLocalTransportEndtxt.Text = Convert.ToInt32(dt.Rows[0]["AverageLocalTransportEnd"]).ToString();

            AverageLocalTransportAmounttxt.Text = dt.Rows[0]["AverageLocalTransportAmount"].ToString();
            lblState_Id.Text = dt.Rows[0]["StateID"].ToString();

            AverageTransportDropdownComboList.SelectedValue = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();


            UpdateAverageLocalTransport.Visible = true;
            AddAverageLocalTransport.Visible = false;
        }

        protected void AddAverageLocalTransport_Click(object sender, EventArgs e)
        {
            pro.UnitMeasurement_Id = Convert.ToInt32(AverageTransportDropdownComboList.SelectedValue);
            pro.AverageLocalTransportStart = decimal.Parse(AverageLocalTransportStarttxt.Text);
            pro.AverageLocalTransportEnd = decimal.Parse(AverageLocalTransportEndtxt.Text);
            pro.Company_Id = Company_Id;
            pro.AverageLocalTransportAmount = decimal.Parse(AverageLocalTransportAmounttxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.User_Id = User_Id;
            int status = cls.Insert_TransportAverageLocal(pro);

            if (status > 0)
            {
                Grid_AverageTransportData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                LocalCartageClearData();
                AverageLocalTransportClearData();
            }
        }

        protected void EditAverageLocalTransport_Click(object sender, EventArgs e)
        {

        }

        protected void AverageTransportDropdownComboList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CancelAverageLocalTransport_Click(object sender, EventArgs e)
        {
            AverageTransportDropdownComboList.SelectedIndex = -1;

            AverageLocalTransportAmounttxt.Text = "";
            AverageLocalTransportStarttxt.Text = "";
            AverageLocalTransportEndtxt.Text = "";
            lblAverageTransport_Id.Text = "";
            AddAverageLocalTransport.Visible = true;
            UpdateAverageLocalTransport.Visible = false;
        }

        protected void UpdateAverageLocalTransport_Click(object sender, EventArgs e)
        {
            pro.UnitMeasurement_Id = Convert.ToInt32(AverageTransportDropdownComboList.SelectedValue);
            pro.AverageLocalTransportStart = decimal.Parse(AverageLocalTransportStarttxt.Text);
            pro.AverageLocalTransportEnd = decimal.Parse(AverageLocalTransportEndtxt.Text);
            pro.Company_Id = Company_Id;
            pro.AverageLocalTransportAmount = decimal.Parse(AverageLocalTransportAmounttxt.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lbltransportationCostFactor_Id.Text);
            pro.Transportation_State_Id = Convert.ToInt32(lblState_Id.Text);
            pro.AverageLocalTransport_Id = Convert.ToInt32(lblAverageTransport_Id.Text);
            pro.User_Id = User_Id;


            int status = cls.Update_AverageLocalTransport(pro);

            if (status > 0)
            {
                Grid_AverageTransportData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);
                LocalCartageClearData();
                AverageLocalTransportClearData();
            }
        }

        protected void CancelLocalCartageBtn_Click(object sender, EventArgs e)
        {
            UnitMeasurementDropdownLocalCartageList.SelectedIndex = -1;

            LocalCartageAmounttxt.Text = "";
            LocalCartageEndtxt.Text = "";
            LocalCartageStarttxt.Text = "";
            lblLocalCartage_Id.Text = "";
            AddLocalCartageBtn.Visible = true;
            UpdateLocalCartageBtn.Visible = false;
        }
    }
}
