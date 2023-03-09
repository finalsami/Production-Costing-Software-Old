using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;

namespace Production_Costing_Software
{
    public partial class PM_RM_Master : System.Web.UI.Page
    {
        ClsPM_RM_Master cls = new ClsPM_RM_Master();
        proPM_RM_Master pro = new proPM_RM_Master();
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
                GetLoginDetails();
                PM_RM_CategoryCombo();
                PM_RM_MeasurementCombo();
                //PM_RM_WeightMeasurementCombo();
                GridPMRM_MasterListData();
                //UnitDropdownList.AppendDataBoundItems = true;
                //UnitDropdownList.Items.Insert(0, "---Select---");
                
                DisplayView();
            }
            else
            {
                GetLoginDetails();
            }

        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[15]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[15]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[15]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPM_RM_Id.Text!="")
                {
                    Update_PMRMBtn.Visible = true;
                    CancelBtn.Visible = true;
                    Add_PMRMbtn.Visible = false;
                }
                else
                {
                    Update_PMRMBtn.Visible = false;
                    CancelBtn.Visible = true;
                    Add_PMRMbtn.Visible = true;
                }
               
            }
            else
            {
                Add_PMRMbtn.Visible = false;
                CancelBtn.Visible = false;
                Update_PMRMBtn.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[15]["GroupId"]);

            if (lblCanEdit.Text == "True")
            {
                if (lblPM_RM_Id.Text != "")
                {
                    Update_PMRMBtn.Visible = true;
                    CancelBtn.Visible = true;
                    Add_PMRMbtn.Visible = false;
                }
                else
                {
                    Update_PMRMBtn.Visible = false;
                    CancelBtn.Visible = true;
                    Add_PMRMbtn.Visible = true;
                }

            }
            else
            {
                Add_PMRMbtn.Visible = false;
                CancelBtn.Visible = false;
                Update_PMRMBtn.Visible = false;
            }


        }

        public void GetLoginDetails()
        {
            if (Session["UserName"].ToString() !=null && Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                pro.User_Id = Convert.ToInt32(Session["UserId"]);
                UserId = pro.User_Id;
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void PM_RM_CategoryCombo()
        {
            ClsPM_RM_Category cls = new ClsPM_RM_Category();
            DataTable dt = new DataTable();

            PMRMCategoryDropdownList.DataSource = cls.GetPM_RMCategoryData(UserId);

            //dt.Columns.Add("PM_RM_Category_Value", typeof(string), "PM_RM_Category_id + ' (' + ChkIsShipper + ')'").ToString();



            PMRMCategoryDropdownList.DataTextField = "PM_RM_Category_Name";

            PMRMCategoryDropdownList.DataValueField = "PM_RM_Category_id";
            PMRMCategoryDropdownList.DataBind();
            PMRMCategoryDropdownList.Items.Insert(0, "Select");
        }
        public void PM_RM_MeasurementCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitDropdownList.DataSource = cls.GetEnumMasterMeasurement();
            UnitDropdownList.DataTextField = "EnumDescription";
            UnitDropdownList.DataValueField = "PkEnumId";
            UnitDropdownList.DataBind();
            UnitDropdownList.Items.Insert(0, "Select");

        }
        //public void PM_RM_WeightMeasurementCombo()
        //{
        //    ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
        //    WeightMeasurementDropdown.DataSource = cls.GetEnumMasterMeasurement();
        //    WeightMeasurementDropdown.DataTextField = "EnumDescription";
        //    WeightMeasurementDropdown.DataValueField = "PkEnumId";
        //    WeightMeasurementDropdown.DataBind();
        //}
        protected void Add_PMRMbtn_Click(object sender, EventArgs e)
        {
            pro.User_Id = UserId;
            pro.PMRM_Category_ID = Convert.ToInt32(PMRMCategoryDropdownList.SelectedValue);
            pro.PMRM_Name = PMNametxt.Text;
            if (UnitDropdownList.SelectedValue == "1" && PMRMCategoryDropdownList.SelectedValue == "1")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "2" && PMRMCategoryDropdownList.SelectedValue == "1")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "2" || PMRMCategoryDropdownList.SelectedValue == "2")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "1" || PMRMCategoryDropdownList.SelectedValue == "2")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "1")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "2")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "3" && PMRMCategoryDropdownList.SelectedValue == "5")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = 0;
                pro.WeightMeasument = 0;
                pro.PerUnitWeight = 0;
                pro.UnitsPerKG = 0;
            }


            int result = cls.Insert_PMRM_MasterData(pro);
            //AlertBoxid.ConfirmText="Success!";
            if (result > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                GridPMRM_MasterListData();
                CleareData();
                ResetMaster();
            }

        }
        public void GridPMRM_MasterListData()
        {

            GridPMRM_MasterList.DataSource = cls.Get_PMRM_MasterData(UserId);
            GridPMRM_MasterList.DataBind();
        }
        protected void NoofUnittxt_TextChanged(object sender, EventArgs e)
        {
            if (NoofUnittxt.Text != "" && TotalWeightUnittxt.Text != "")
            {
                PerUnitWeighttxt.Text = (decimal.Parse(TotalWeightUnittxt.Text.ToString()) / decimal.Parse(NoofUnittxt.Text.ToString())).ToString("0.0000");



            }
            if (PerUnitWeighttxt.Text != "0.0000" && PerUnitWeighttxt.Text != "")
            {
                if (UnitDropdownList.SelectedValue == "2" || UnitDropdownList.SelectedValue == "1" || UnitDropdownList.SelectedValue == "3")
                {

                    UnitsPerKGtxt.Text = (1 / decimal.Parse(PerUnitWeighttxt.Text.ToString())).ToString("0.00");
                }
            }
        }

        protected void TotalWeightUnittxt_TextChanged(object sender, EventArgs e)
        {
            if (NoofUnittxt.Text != "" && TotalWeightUnittxt.Text != "")
            {
                PerUnitWeighttxt.Text = (decimal.Parse(TotalWeightUnittxt.Text.ToString()) / decimal.Parse(NoofUnittxt.Text.ToString())).ToString("0.0000");

            }
            if (UnitDropdownList.SelectedValue == "2" || UnitDropdownList.SelectedValue == "1" || UnitDropdownList.SelectedValue == "3")

            {
                if (PerUnitWeighttxt.Text != "0.0000" && PerUnitWeighttxt.Text != "")
                {

                    UnitsPerKGtxt.Text = (1 / decimal.Parse(PerUnitWeighttxt.Text.ToString())).ToString("0.00");
                }
            }
        }

        protected void UnitDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (PMRMCategoryDropdownList.SelectedValue == "1" && UnitDropdownList.SelectedValue == "1")
            {
                NoofUnittxt.Enabled = true;
                TotalWeightUnittxt.ReadOnly = false;
                //WeightMeasurementDropdown.Enabled = true;
                PerUnitWeighttxt.ReadOnly = true;
                UnitsPerKGtxt.ReadOnly = true;
                TotalWeightUnittxt.Visible = true;
                //WeightMeasurementDropdown.Visible = true;
                PerUnitWeighttxt.Visible = true;
                UnitsPerKGtxt.Visible = true;
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "1" && UnitDropdownList.SelectedValue == "2")
            {
                NoofUnittxt.Enabled = true;
                TotalWeightUnittxt.ReadOnly = false;
                //WeightMeasurementDropdown.Enabled = true;
                PerUnitWeighttxt.ReadOnly = true;
                UnitsPerKGtxt.ReadOnly = true;
                TotalWeightUnittxt.Visible = true;
                //WeightMeasurementDropdown.Visible = true;
                PerUnitWeighttxt.Visible = true;
                UnitsPerKGtxt.Visible = true;
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "2" && UnitDropdownList.SelectedValue == "1")
            {
                NoofUnittxt.Enabled = true;
                TotalWeightUnittxt.ReadOnly = false;
                //WeightMeasurementDropdown.Enabled = true;
                PerUnitWeighttxt.ReadOnly = true;
                UnitsPerKGtxt.ReadOnly = true;
                TotalWeightUnittxt.Visible = true;
                //WeightMeasurementDropdown.Visible = true;
                PerUnitWeighttxt.Visible = true;
                UnitsPerKGtxt.Visible = true;
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "2" && UnitDropdownList.SelectedValue == "2")
            {
                NoofUnittxt.Enabled = true;
                TotalWeightUnittxt.ReadOnly = false;
                //WeightMeasurementDropdown.Enabled = true;
                PerUnitWeighttxt.ReadOnly = true;
                UnitsPerKGtxt.ReadOnly = true;
                TotalWeightUnittxt.Visible = true;
                //WeightMeasurementDropdown.Visible = true;
                PerUnitWeighttxt.Visible = true;
                UnitsPerKGtxt.Visible = true;
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "1")
            {
                NoofUnittxt.Enabled = true;
                TotalWeightUnittxt.ReadOnly = false;
                //WeightMeasurementDropdown.Enabled = true;
                PerUnitWeighttxt.ReadOnly = true;
                UnitsPerKGtxt.ReadOnly = true;
                TotalWeightUnittxt.Visible = true;
                //WeightMeasurementDropdown.Visible = true;
                PerUnitWeighttxt.Visible = true;
                UnitsPerKGtxt.Visible = true;
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "2")
            {
                NoofUnittxt.Enabled = true;
                TotalWeightUnittxt.ReadOnly = false;
                //WeightMeasurementDropdown.Enabled = true;
                PerUnitWeighttxt.ReadOnly = true;
                UnitsPerKGtxt.ReadOnly = true;
                TotalWeightUnittxt.Visible = true;
                //WeightMeasurementDropdown.Visible = true;
                PerUnitWeighttxt.Visible = true;
                UnitsPerKGtxt.Visible = true;
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "5" && UnitDropdownList.SelectedValue == "3")
            {
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "4" && UnitDropdownList.SelectedValue == "3")
            {
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "5" && UnitDropdownList.SelectedValue == "3")
            {
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "10" && UnitDropdownList.SelectedValue == "3")
            {
                NoOfUnitDivhide.Visible = true;
                PerUnitDivhide.Visible = true;
                TotalWeightDivhide.Visible = true;
                UnitsKGDivhide.Visible = true;
            }
            else
            {
                NoOfUnitDivhide.Visible = false;
                PerUnitDivhide.Visible = false;
                TotalWeightDivhide.Visible = false;
                UnitsKGDivhide.Visible = false;
            }

        }


        protected void DelPMRMBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PM_RM_id = Convert.ToInt32(GridPMRM_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.Delete_PMRM_Master(PM_RM_id, UserId);
            if (status > 0)
            {
                GridPMRM_MasterListData();
                CleareData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Used Somewhere Else. Can Not Delete!')", true);

            }

        }
        public void CleareData()
        {
            //PMRMCategoryDropdownList.SelectedItem.Text = "";
            //UnitDropdownList.SelectedItem.Text ="";
            //WeightMeasurementDropdown.SelectedItem.Text ="";
            PMNametxt.Text = "";
            NoofUnittxt.Text = "0";
            TotalWeightUnittxt.Text = "0";
            PerUnitWeighttxt.Text = "0";
            UnitsPerKGtxt.Text = "0";
            //PM_RM_CategoryCombo();
            //PM_RM_MeasurementCombo();
            PMRMCategoryDropdownList.SelectedIndex = 0;
            UnitDropdownList.SelectedIndex = 0;
            PMRMCategoryDropdownList.Enabled = true;
            Add_PMRMbtn.Visible = true;
            Update_PMRMBtn.Visible = false;
        }

        protected void EditPMRMBtn_Click(object sender, EventArgs e)
        {
            CleareData();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int PM_RM_Id = Convert.ToInt32(GridPMRM_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.User_Id = UserId;
            lblPM_RM_Id.Text = (PM_RM_Id).ToString();
            pro.PMRM_Id = PM_RM_Id;
            DataTable dt = new DataTable();
            dt = cls.Get_PMRM_MasterBy_Id(pro);
            
            if (dt.Rows.Count > 0)
            {
                PMRMCategoryDropdownList.Enabled = false;
                pro.PMRM_Category_ID = Convert.ToInt32(dt.Rows[0]["PM_RM_Category_id"]);
                pro.PMRM_Id = PM_RM_Id;
                lblPM_RM_Id.Text = pro.PMRM_Id.ToString();
                PMRMCategoryDropdownList.SelectedValue = dt.Rows[0]["PM_RM_Category_id"].ToString();
                //UnitDropdownList.SelectedItem.Text = dt.Rows[0]["Price_KG_Unit"].ToString();
                if (UnitDropdownList.SelectedValue == "Select")
                {
                    UnitDropdownList.SelectedValue = dt.Rows[0]["PM_RM_Price_KG_Unit"].ToString();
                }
                else
                {
                    UnitDropdownList.SelectedValue = dt.Rows[0]["PM_RM_Price_KG_Unit"].ToString();
                }

                PMNametxt.Text = dt.Rows[0]["PM_Name"].ToString();
                NoofUnittxt.Text = dt.Rows[0]["No_Of_Unit"].ToString();
                TotalWeightUnittxt.Text = dt.Rows[0]["Total_Weight_Of_Unit"].ToString();
                PerUnitWeighttxt.Text = dt.Rows[0]["Per_Unit_Weight"].ToString();
                UnitsPerKGtxt.Text = dt.Rows[0]["Unit_KG"].ToString();


                //for Hide And Show Fields

                if (PMRMCategoryDropdownList.SelectedValue == "1" && UnitDropdownList.SelectedValue == "1")
                {
                    NoofUnittxt.Enabled = true;
                    TotalWeightUnittxt.ReadOnly = false;
                    //WeightMeasurementDropdown.Enabled = true;
                    PerUnitWeighttxt.ReadOnly = true;
                    UnitsPerKGtxt.ReadOnly = true;
                    TotalWeightUnittxt.Visible = true;
                    //WeightMeasurementDropdown.Visible = true;
                    PerUnitWeighttxt.Visible = true;
                    UnitsPerKGtxt.Visible = true;
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "1" && UnitDropdownList.SelectedValue == "2")
                {
                    NoofUnittxt.Enabled = true;
                    TotalWeightUnittxt.ReadOnly = false;
                    //WeightMeasurementDropdown.Enabled = true;
                    PerUnitWeighttxt.ReadOnly = true;
                    UnitsPerKGtxt.ReadOnly = true;
                    TotalWeightUnittxt.Visible = true;
                    //WeightMeasurementDropdown.Visible = true;
                    PerUnitWeighttxt.Visible = true;
                    UnitsPerKGtxt.Visible = true;
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "2" && UnitDropdownList.SelectedValue == "1")
                {
                    NoofUnittxt.Enabled = true;
                    TotalWeightUnittxt.ReadOnly = false;
                    //WeightMeasurementDropdown.Enabled = true;
                    PerUnitWeighttxt.ReadOnly = true;
                    UnitsPerKGtxt.ReadOnly = true;
                    TotalWeightUnittxt.Visible = true;
                    //WeightMeasurementDropdown.Visible = true;
                    PerUnitWeighttxt.Visible = true;
                    UnitsPerKGtxt.Visible = true;
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "2" && UnitDropdownList.SelectedValue == "2")
                {
                    NoofUnittxt.Enabled = true;
                    TotalWeightUnittxt.ReadOnly = false;
                    //WeightMeasurementDropdown.Enabled = true;
                    PerUnitWeighttxt.ReadOnly = true;
                    UnitsPerKGtxt.ReadOnly = true;
                    TotalWeightUnittxt.Visible = true;
                    //WeightMeasurementDropdown.Visible = true;
                    PerUnitWeighttxt.Visible = true;
                    UnitsPerKGtxt.Visible = true;
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "1")
                {
                    NoofUnittxt.Enabled = true;
                    TotalWeightUnittxt.ReadOnly = false;
                    //WeightMeasurementDropdown.Enabled = true;
                    PerUnitWeighttxt.ReadOnly = true;
                    UnitsPerKGtxt.ReadOnly = true;
                    TotalWeightUnittxt.Visible = true;
                    //WeightMeasurementDropdown.Visible = true;
                    PerUnitWeighttxt.Visible = true;
                    UnitsPerKGtxt.Visible = true;
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "2")
                {
                    NoofUnittxt.Enabled = true;
                    TotalWeightUnittxt.ReadOnly = false;
                    //WeightMeasurementDropdown.Enabled = true;
                    PerUnitWeighttxt.ReadOnly = true;
                    UnitsPerKGtxt.ReadOnly = true;
                    TotalWeightUnittxt.Visible = true;
                    //WeightMeasurementDropdown.Visible = true;
                    PerUnitWeighttxt.Visible = true;
                    UnitsPerKGtxt.Visible = true;
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "5" && UnitDropdownList.SelectedValue == "3")
                {
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "4" && UnitDropdownList.SelectedValue == "3")
                {
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "5" && UnitDropdownList.SelectedValue == "3")
                {
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else if (PMRMCategoryDropdownList.SelectedValue == "10" && UnitDropdownList.SelectedValue == "3")
                {
                    NoOfUnitDivhide.Visible = true;
                    PerUnitDivhide.Visible = true;
                    TotalWeightDivhide.Visible = true;
                    UnitsKGDivhide.Visible = true;
                }
                else
                {
                    NoOfUnitDivhide.Visible = false;
                    PerUnitDivhide.Visible = false;
                    TotalWeightDivhide.Visible = false;
                    UnitsKGDivhide.Visible = false;
                }
            }
            else
            {
                PMRMCategoryDropdownList.Enabled = true;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found!')", true);

            }


            Add_PMRMbtn.Visible = false;
            Update_PMRMBtn.Visible = true;
            //CancelBtn.Visible = true;
        }
        protected void Update_PMRMBtn_Click(object sender, EventArgs e)
        {
            pro.User_Id = UserId;
            pro.PMRM_Id = Convert.ToInt32(lblPM_RM_Id.Text);
            DataTable dt = new DataTable();
            dt = cls.Get_PMRM_MasterBy_Id(pro);
            pro.PMRM_Category_ID = Convert.ToInt32(dt.Rows[0]["PM_RM_Category_id"]);
            pro.PMRM_Id = Convert.ToInt32(lblPM_RM_Id.Text);
            pro.User_Id = UserId;
            pro.PMRM_Name = PMNametxt.Text;
            if (UnitDropdownList.SelectedValue == "1" && PMRMCategoryDropdownList.SelectedValue == "1")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "2" && PMRMCategoryDropdownList.SelectedValue == "1")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "2" || PMRMCategoryDropdownList.SelectedValue == "2")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "1" || PMRMCategoryDropdownList.SelectedValue == "2")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "1")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (PMRMCategoryDropdownList.SelectedValue == "7" && UnitDropdownList.SelectedValue == "2")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else if (UnitDropdownList.SelectedValue == "3" && PMRMCategoryDropdownList.SelectedValue == "5")
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = float.Parse(TotalWeightUnittxt.Text);
                //pro.WeightMeasument = Convert.ToInt32(WeightMeasurementDropdown.SelectedValue);
                pro.PerUnitWeight = float.Parse(PerUnitWeighttxt.Text);
                pro.UnitsPerKG = float.Parse(UnitsPerKGtxt.Text);
            }
            else
            {
                pro.Price_KG_per_Unit = Convert.ToInt32(UnitDropdownList.SelectedValue);
                pro.No_Of_Unit = Convert.ToInt32(NoofUnittxt.Text);
                pro.TotalWeightUnits = 0;
                pro.WeightMeasument = 0;
                pro.PerUnitWeight = 0;
                pro.UnitsPerKG = 0;
            }
            int status = cls.Update_PMRM_MasterData(pro);
            if (status > 0)
            {
                PMRMCategoryDropdownList.Enabled = true;

                GridPMRM_MasterListData();
                CleareData();
                ResetMaster();
                lblPM_RM_Id.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

            }
            Add_PMRMbtn.Visible = true;
            Update_PMRMBtn.Visible = false;
            //CancelBtn.Visible = false;

        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            CleareData();
            Add_PMRMbtn.Visible = true;
            Update_PMRMBtn.Visible = false;
            //CancelBtn.Visible = false;
            PMRMCategoryDropdownList.Enabled = true;

        }

        protected void PMRMCategoryDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitDropdownList.SelectedIndex = 0;

            //string PMRMCategory_Id = PMRMCategoryDropdownList.SelectedValue.ToString();

            //Shipper_Id = PMRMCategory_Id.Split('(', ')')[1];

            //lblPMRM_Category_Id.Text = PMRMCategory_Id.Substring(0, 3).Trim();

        }

        protected void GridPMRM_MasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridPMRM_MasterListData();


            GridPMRM_MasterList.PageIndex = e.NewPageIndex;
            GridPMRM_MasterList.DataBind();
        }
        public void ResetMaster()
        {
            NoofUnittxt.Enabled = true;
            TotalWeightUnittxt.ReadOnly = false;
            //WeightMeasurementDropdown.Enabled = true;
            PerUnitWeighttxt.ReadOnly = true;
            UnitsPerKGtxt.ReadOnly = true;
            TotalWeightUnittxt.Visible = true;
            //WeightMeasurementDropdown.Visible = true;
            PerUnitWeighttxt.Visible = true;
            UnitsPerKGtxt.Visible = true;
            NoOfUnitDivhide.Visible = true;
            PerUnitDivhide.Visible = true;
            TotalWeightDivhide.Visible = true;
            UnitsKGDivhide.Visible = true;
        }

        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_PM_from_PMRM";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchPMRM", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["PM_Name"].ToString());

                        }
                    }

                    conn.Close();

                    return customers;
                }
            }
        }

        protected void SearchId_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";

            GridPMRM_MasterListData();
        }


        private void BindGrid()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Search_PM_from_PMRM", con);
                cmd.Parameters.AddWithValue("@SearchPMRM", TxtSearch.Text.Trim());


                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                cmd.Dispose();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dt.Dispose();
            }

            GridPMRM_MasterList.DataSource = dt;
            GridPMRM_MasterList.DataBind();
        }

        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void PMNametxt_TextChanged(object sender, EventArgs e)
        {
            pro.PMRM_Name = PMNametxt.Text.Trim();
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_PMRM_Master(pro);
            if (dtCheck.Rows.Count>0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple [" + PMNametxt.Text + "] Name !')", true);
                CleareData();
                return;
            }
        }
    }
}
