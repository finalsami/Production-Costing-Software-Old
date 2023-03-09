using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class FormulationMaster : System.Web.UI.Page
    {
        ClsFormulationMaster cls = new ClsFormulationMaster();
        ProFormulationMaster pro = new ProFormulationMaster();
        decimal LabourTotal;
        decimal PowerUnitCharge;
        decimal TotalCost;
        decimal CostPerLtrBatchSize;
        int UserId;
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
                Grid_Formulation_MasterDataList();
                FM_Measurementcombo();
                CostVariableMasterData();
                DisplayView();
            }

        }
        public void CostVariableMasterData()
        {
            ClsCostVariableMaster cls = new ClsCostVariableMaster();
            DataTable dt = new DataTable();
            dt = cls.Get_OtherVariableForms(UserId);
            LabourChargetxt.Text = dt.Rows[0]["LabourCharge"].ToString();
            PowerChargetxt.Text = dt.Rows[0]["Power_Unit_Price"].ToString();
            lblSuperVisorCost.Text = dt.Rows[0]["SuperVisorCosting"].ToString();
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"].ToString() != null && Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                UserId = Convert.ToInt32(Session["UserId"]);
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[13]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[13]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[13]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[13]["CanEdit"]) == true)
            {
                if (lblFM_Id.Text != "")
                {
                    AddFormulationbtn.Visible = false;
                    EditFormulationbtn.Visible = true;
                }
                else
                {
                    AddFormulationbtn.Visible = true;
                    EditFormulationbtn.Visible = false;

                }
            }
            else
            {
                AddFormulationbtn.Visible = false;
                EditFormulationbtn.Visible = false;
            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[13]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[13]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[13]["CanEdit"]) == true)
            {
                if (lblFM_Id.Text != "")
                {
                    AddFormulationbtn.Visible = false;
                    EditFormulationbtn.Visible = true;
                }
                else
                {
                    AddFormulationbtn.Visible = true;
                    EditFormulationbtn.Visible = false;

                }
            }
            else
            {
                AddFormulationbtn.Visible = false;
                EditFormulationbtn.Visible = false;
                CancelBtn.Visible = false;
            }
        }
        public void Grid_Formulation_MasterDataList()
        {

            Grid_Formulation_MasterList.DataSource = cls.Get_FormulationMasterALL(UserId);
            Grid_Formulation_MasterList.DataBind();
        }
        public void FM_Measurementcombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeasurementdownList.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeasurementdownList.DataTextField = "EnumDescription";
            UnitMeasurementdownList.DataValueField = "PkEnumId";

            UnitMeasurementdownList.DataBind();
            UnitMeasurementdownList.Items.Insert(0, "Select");
        }
        public void DataClear()
        {
            Formulationtxt.Text = "";
            BatchSizetxt.Text = "0";
            UnitMeasurementdownList.SelectedIndex = 0;
            Labourtxt.Text = "0";
            //LabourChargetxt.Text = "0";
            TotalLabourCosttxt.Text = "0";
            PowerUnittxt.Text = "0";
            //PowerChargetxt.Text = "0";
            TotalPowerCosttxt.Text = "0";
            Maintenancetxt.Text = "0";
            Supervisorstxt.Text = "0";
            OtherCosttxt.Text = "0";
            TotalCosttxt.Text = "0";
            AddBuffertxt.Text = "0";
            CostPerLtrBatchSizetxt.Text = "0";
            FinalCostPerLtrBatchSizetxt.Text = "0";
            AddFormulationbtn.Visible = true;
            EditFormulationbtn.Visible = false;
            lblFM_Id.Text = "";
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            pro.UnitMeasurement = Convert.ToInt32(UnitMeasurementdownList.SelectedValue);
            //pro.DateOfPurchase = DateTime.Now;
            pro.FM_Name = Formulationtxt.Text;
            pro.FM_BatchSize = Convert.ToInt32(BatchSizetxt.Text);
            pro.FM_Labour = Convert.ToInt32(Labourtxt.Text);
            pro.FM_Supervisors = Convert.ToInt32(Supervisorstxt.Text);

            pro.FM_Labour_Charge = decimal.Parse(LabourChargetxt.Text);
            pro.FM_Labour_TotalCost = decimal.Parse(TotalLabourCosttxt.Text);
            pro.FM_Power_Unit = Convert.ToInt32(PowerUnittxt.Text);
            pro.FM_PowerCharge_Unit = decimal.Parse(PowerChargetxt.Text);
            pro.FM_Total_Power_Cost = decimal.Parse(TotalPowerCosttxt.Text);
            pro.FM_Maintenance = decimal.Parse(Maintenancetxt.Text);
            pro.FM_Other_Cost = decimal.Parse(OtherCosttxt.Text);
            pro.FM_Total_Cost = decimal.Parse(TotalCosttxt.Text);
            pro.CostPerLtrBatchSize = decimal.Parse(CostPerLtrBatchSizetxt.Text);
            pro.AddBuffer = decimal.Parse(AddBuffertxt.Text);
            pro.FinalCostPerLtrBatchSize = decimal.Parse(FinalCostPerLtrBatchSizetxt.Text);
            pro.User_Id = UserId;


            int status = cls.Insert_FM_MasterData(pro);
            if (status > 0)
            {
                Grid_Formulation_MasterDataList();
                DataClear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Fail!')", true);
            }

        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            pro.User_Id = UserId;
            pro.FM_Id = Convert.ToInt32(lblFM_Id.Text);
            pro.FM_Name = Formulationtxt.Text;
            pro.FM_BatchSize = Convert.ToInt32(BatchSizetxt.Text);
            pro.FM_Labour = Convert.ToInt32(Labourtxt.Text);
            pro.FM_Supervisors = Convert.ToInt32(Supervisorstxt.Text);
            pro.FM_Labour_Charge = decimal.Parse(LabourChargetxt.Text);
            pro.FM_Labour_TotalCost = decimal.Parse(TotalLabourCosttxt.Text);
            pro.FM_Power_Unit = Convert.ToInt32(PowerUnittxt.Text);
            pro.FM_PowerCharge_Unit = decimal.Parse(PowerChargetxt.Text);
            pro.FM_Total_Power_Cost = decimal.Parse(TotalPowerCosttxt.Text);
            pro.FM_Maintenance = decimal.Parse(Maintenancetxt.Text);
            pro.FM_Other_Cost = decimal.Parse(OtherCosttxt.Text);
            pro.FM_Total_Cost = decimal.Parse(TotalCosttxt.Text);
            pro.CostPerLtrBatchSize = decimal.Parse(CostPerLtrBatchSizetxt.Text);
            pro.UnitMeasurement = Convert.ToInt32(UnitMeasurementdownList.SelectedValue);
            pro.AddBuffer = decimal.Parse(AddBuffertxt.Text);
            pro.FinalCostPerLtrBatchSize = decimal.Parse(FinalCostPerLtrBatchSizetxt.Text);
            int status = cls.Update_FM_MasterData(pro);

            if (status > 0)
            {
                lblFM_Id.Text = "";

                UnitMeasurementdownList.SelectedIndex = 0;

                AddFormulationbtn.Visible = true;
                EditFormulationbtn.Visible = false;
                DataClear();
                Grid_Formulation_MasterDataList();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed ')", true);

            }

        }

        protected void Labourtxt_TextChanged(object sender, EventArgs e)
        {
            if (Labourtxt.Text == "")
            {
                Labourtxt.Text = "0";
            }
            if (LabourChargetxt.Text == "")
            {
                LabourChargetxt.Text = "0";
            }
            if (Supervisorstxt.Text == "0")
            {
                Supervisorstxt.Text = "0";
            }
            string SuperVisorCost = (decimal.Parse(Supervisorstxt.Text) * decimal.Parse(lblSuperVisorCost.Text)).ToString();
            if (SuperVisorCost != "0")
            {
                LabourTotal = decimal.Parse(Labourtxt.Text) * decimal.Parse(LabourChargetxt.Text);
                TotalLabourCosttxt.Text = (decimal.Parse(LabourTotal.ToString()) + decimal.Parse(SuperVisorCost.ToString())).ToString();

                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            else
            {
                LabourTotal = decimal.Parse(Labourtxt.Text) * decimal.Parse(LabourChargetxt.Text);
                TotalLabourCosttxt.Text = LabourTotal.ToString();

                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }

        }

        protected void LabourChargetxt_TextChanged(object sender, EventArgs e)
        {
            if (Labourtxt.Text == "")
            {
                Labourtxt.Text = "0";
            }
            if (LabourChargetxt.Text == "")
            {
                LabourChargetxt.Text = "0";
            }
            if (Supervisorstxt.Text == "0")
            {
                Supervisorstxt.Text = "0";
            }
            string SuperVisorCost = (decimal.Parse(Supervisorstxt.Text) * decimal.Parse(lblSuperVisorCost.Text)).ToString();
            if (SuperVisorCost != "0")
            {
                LabourTotal = decimal.Parse(Labourtxt.Text) * decimal.Parse(LabourChargetxt.Text);
                TotalLabourCosttxt.Text = (decimal.Parse(LabourTotal.ToString()) + decimal.Parse(SuperVisorCost.ToString())).ToString();

                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            else
            {
                LabourTotal = decimal.Parse(Labourtxt.Text) * decimal.Parse(LabourChargetxt.Text);
                TotalLabourCosttxt.Text = LabourTotal.ToString();

                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }

        }

        protected void PowerUnittxt_TextChanged(object sender, EventArgs e)
        {

            if (PowerUnittxt.Text == "")
            {
                PowerUnittxt.Text = "0";
            }
            if (PowerChargetxt.Text == "")
            {
                PowerChargetxt.Text = "0";
            }


            PowerUnitCharge = decimal.Parse(PowerUnittxt.Text) * decimal.Parse(PowerChargetxt.Text);
            TotalPowerCosttxt.Text = PowerUnitCharge.ToString();

            TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
            TotalCosttxt.Text = TotalCost.ToString();
            CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
            CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }

        }

        protected void PowerChargetxt_TextChanged(object sender, EventArgs e)
        {
            if (PowerUnittxt.Text == "")
            {
                PowerUnittxt.Text = "0";
            }
            if (PowerChargetxt.Text == "")
            {
                PowerChargetxt.Text = "0";
            }

            PowerUnitCharge = decimal.Parse(PowerUnittxt.Text) * decimal.Parse(PowerChargetxt.Text);
            TotalPowerCosttxt.Text = PowerUnitCharge.ToString();

            TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
            TotalCosttxt.Text = TotalCost.ToString();
            CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
            CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }

        }

        protected void Maintenancetxt_TextChanged(object sender, EventArgs e)
        {
            if (Maintenancetxt.Text != "")
            {
                if (Maintenancetxt.Text != "0")
                {
                    TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                    TotalCosttxt.Text = TotalCost.ToString();
                    CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                    CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
                }
                else
                {
                    TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(OtherCosttxt.Text);
                    TotalCosttxt.Text = TotalCost.ToString();
                    CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                    CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
                }
            }
            else
            {
                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(OtherCosttxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }

            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }

        }

        protected void OtherCosttxt_TextChanged(object sender, EventArgs e)
        {
            if (OtherCosttxt.Text != "")
            {
                if (OtherCosttxt.Text != "0")
                {
                    TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                    TotalCosttxt.Text = TotalCost.ToString();
                    CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                    CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
                }
                else
                {
                    TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text);
                    TotalCosttxt.Text = TotalCost.ToString();
                    CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                    CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
                }
            }
            else
            {
                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }

        }

        protected void EditFormulationBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int RawId = Convert.ToInt32(Grid_Formulation_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblFM_Id.Text = RawId.ToString();
            DataTable dt = new DataTable();

            dt = cls.Get_FormulationMasterById(UserId, RawId);

            try
            {
                lblFM_Id.Text = dt.Rows[0]["FM_Id"].ToString();
                //UnitMeasurementdownList.SelectedItem.Text = dt.Rows[0]["Measurement"].ToString();
                UnitMeasurementdownList.SelectedValue = dt.Rows[0]["Measurement_Id"].ToString();
                Formulationtxt.Text = dt.Rows[0]["FormulationName"].ToString();
                BatchSizetxt.Text = dt.Rows[0]["BatchSize"].ToString();
                Labourtxt.Text = dt.Rows[0]["Labour"].ToString();
                Supervisorstxt.Text = dt.Rows[0]["FM_Supervisors"].ToString();
                LabourChargetxt.Text = dt.Rows[0]["LabourCharge"].ToString();
                TotalLabourCosttxt.Text = dt.Rows[0]["LabourTotalCost"].ToString();
                PowerUnittxt.Text = dt.Rows[0]["PoweUnit"].ToString();
                PowerChargetxt.Text = dt.Rows[0]["PowerChargeUnit"].ToString();
                TotalPowerCosttxt.Text = dt.Rows[0]["TotalPowerCost"].ToString();
                Maintenancetxt.Text = dt.Rows[0]["Maintenance"].ToString();
                OtherCosttxt.Text = dt.Rows[0]["OtherCost"].ToString();
                TotalCosttxt.Text = dt.Rows[0]["TotalCost"].ToString();
                CostPerLtrBatchSizetxt.Text = dt.Rows[0]["Cost_Per_Ltr_BatchSize"].ToString();
                AddBuffertxt.Text = dt.Rows[0]["AddBuffer"].ToString();
                FinalCostPerLtrBatchSizetxt.Text = dt.Rows[0]["FinalCostPerLtrBatchSize"].ToString();
            }
            catch (Exception)
            {

                throw;
            }

            AddFormulationbtn.Visible = false;
            EditFormulationbtn.Visible = true;
        }

        protected void TotalCosttxt_TextChanged(object sender, EventArgs e)
        {

            CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
            CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString();
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString();
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString();
            }
        }

        protected void DelFormulationBtn_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            int Formulation_Id = Convert.ToInt32(Grid_Formulation_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblFM_Id.Text = Formulation_Id.ToString();
            DataTable dt = new DataTable();

            int status = cls.Delete_FormulationMaster(Formulation_Id, UserId);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                Grid_Formulation_MasterDataList();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Deleted')", true);

            }

        }

        protected void Supervisorstxt_TextChanged(object sender, EventArgs e)
        {
            if (Labourtxt.Text == "")
            {
                Labourtxt.Text = "0";
            }
            if (LabourChargetxt.Text == "")
            {
                LabourChargetxt.Text = "0";
            }
            if (Supervisorstxt.Text == "0")
            {
                Supervisorstxt.Text = "0";
            }
            string SuperVisorCost = (decimal.Parse(Supervisorstxt.Text) * decimal.Parse(lblSuperVisorCost.Text)).ToString();
            if (SuperVisorCost != "0")
            {
                LabourTotal = decimal.Parse(Labourtxt.Text) * decimal.Parse(LabourChargetxt.Text);
                TotalLabourCosttxt.Text = (decimal.Parse(LabourTotal.ToString()) + decimal.Parse(SuperVisorCost.ToString())).ToString();

                TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            else
            {
                LabourTotal = decimal.Parse(Labourtxt.Text) * decimal.Parse(LabourChargetxt.Text);
                TotalLabourCosttxt.Text = LabourTotal.ToString();

                TotalCost = (decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text));
                TotalCosttxt.Text = TotalCost.ToString();
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
            }
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text) + (decimal.Parse(AddBuffertxt.Text))) / decimal.Parse(BatchSizetxt.Text)).ToString("0.000");
            }
            else
            {
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString("0.000");
            }

        }

        protected void AddBuffertxt_TextChanged(object sender, EventArgs e)
        {
            TotalCost = decimal.Parse(TotalLabourCosttxt.Text) + decimal.Parse(TotalPowerCosttxt.Text) + decimal.Parse(Maintenancetxt.Text) + decimal.Parse(OtherCosttxt.Text);
            TotalCosttxt.Text = TotalCost.ToString();
            if (AddBuffertxt.Text != "" || AddBuffertxt.Text != "0")
            {
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text) + decimal.Parse(AddBuffertxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");

                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / decimal.Parse(BatchSizetxt.Text)).ToString("0.000");
            }
            else
            {
                CostPerLtrBatchSize = decimal.Parse(TotalCosttxt.Text);
                CostPerLtrBatchSizetxt.Text = CostPerLtrBatchSize.ToString("0.000");
                FinalCostPerLtrBatchSizetxt.Text = ((decimal.Parse(CostPerLtrBatchSizetxt.Text)) / (decimal.Parse(BatchSizetxt.Text))).ToString("0.000");
            }

        }

        protected void Grid_Formulation_MasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Formulation_MasterDataList();

            Grid_Formulation_MasterList.PageIndex = e.NewPageIndex;
            Grid_Formulation_MasterList.DataBind();
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Formulationtxt.Text = "";
            BatchSizetxt.Text = "0";
            UnitMeasurementdownList.SelectedIndex = 0;
            Labourtxt.Text = "0";
            //LabourChargetxt.Text = "0";
            TotalLabourCosttxt.Text = "0";
            PowerUnittxt.Text = "0";
            //PowerChargetxt.Text = "0";
            TotalPowerCosttxt.Text = "0";
            Maintenancetxt.Text = "0";
            Supervisorstxt.Text = "0";
            OtherCosttxt.Text = "0";
            TotalCosttxt.Text = "0";
            AddBuffertxt.Text = "0";
            //CostPerLtrBatchSizetxt.Text = "0";
            lblFM_Id.Text = "";
            FinalCostPerLtrBatchSizetxt.Text = "0";
            AddFormulationbtn.Visible = true;
            EditFormulationbtn.Visible = false;
            CostPerLtrBatchSizetxt.Text = "0";

        }

        protected void Formulationtxt_TextChanged(object sender, EventArgs e)
        {
            pro.FM_Name = Formulationtxt.Text;
            DataTable dtCheck = new DataTable();
            dtCheck = cls.sp_CHECK_FormulationMaster(pro);
            if (dtCheck.Rows.Count>0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple [" + Formulationtxt.Text + "] Name !')", true);
                DataClear();
                return;
            }
        }
    }
}
