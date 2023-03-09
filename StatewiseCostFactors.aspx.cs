using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;

namespace Production_Costing_Software
{
    public partial class StatewiseCostFactors : System.Web.UI.Page
    {
        ProStatewiseCostFactor pro = new ProStatewiseCostFactor();
        int UserId;
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
                ProductCategoryCombo();
                StateWiseCombo();
                ExpenceTypeCombo();
                PriceTypeCombo();
                Grid_StatewiseCostFactorsData();
                Get_State_From_StateWiseCostFector();
                Get_Category_From_StateWiseCostFector();
                DisplayView();
            }
        }
        public void PriceTypeCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            //PriceTypeDropDownList.DataSource = cls.GetPriceType();
            //PriceTypeDropDownList.DataTextField = "PriceType";
            //PriceTypeDropDownList.DataValueField = "PkEnumId";
            //PriceTypeDropDownList.DataBind();
            //PriceTypeDropDownList.Items.Insert(0, "Select");
        }

        public void StateWiseCombo()
        {
            ClsTransportationCostFactors cls = new ClsTransportationCostFactors();
            StateNameDropdown.DataSource = cls.Get_AllStateData();
            StateNameDropdown.DataTextField = "StateName";
            StateNameDropdown.DataValueField = "StateID";
            StateNameDropdown.DataBind();
            StateNameDropdown.Items.Insert(0, "Select");
        }
        public void ProductCategoryCombo()
        {
            ClsProductCategoryMaster cls = new ClsProductCategoryMaster();
            ProductCategoryDropDown.DataSource = cls.Get_ProductCategoryMasterAll();
            ProductCategoryDropDown.DataTextField = "ProductCategoryName";
            ProductCategoryDropDown.DataValueField = "Product_Category_Id";
            ProductCategoryDropDown.DataBind();
            ProductCategoryDropDown.Items.Insert(0, "Select");


        }
        public void ExpenceTypeCombo()
        {
            ClsExpenceType cls = new ClsExpenceType();
            //ExpenseTypeDropDownList.DataSource = cls.Get_ExpenceType();
            //ExpenseTypeDropDownList.DataTextField = "ExpenceName";
            //ExpenseTypeDropDownList.DataValueField = "ExpenceType_Id";
            //ExpenseTypeDropDownList.DataBind();
            //ExpenseTypeDropDownList.Items.Insert(0, "Select");
        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[25]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[25]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[25]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblStatewiseCostFactors_Id.Text != "")
                {
                    AddStatewiseCostFactors.Visible = false;
                    CancelCategoryMapping.Visible = true;
                    AddNCRExpenceBtn.Visible = false;
                    CancelNCRExpenceBtn.Visible = true;
                }
                else
                {
                    AddStatewiseCostFactors.Visible = true;
                    CancelCategoryMapping.Visible = true;
                    AddNCRExpenceBtn.Visible = true;
                    CancelNCRExpenceBtn.Visible = true;

                }


            }
            else
            {
                AddStatewiseCostFactors.Visible = false;
                CancelCategoryMapping.Visible = false;
                AddNCRExpenceBtn.Visible = false;
                CancelNCRExpenceBtn.Visible = false;

            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[25]["GroupId"]);

            if (lblCanEdit.Text == "True")
            {
                if (lblStatewiseCostFactors_Id.Text != "")
                {
                    AddStatewiseCostFactors.Visible = false;
                    CancelCategoryMapping.Visible = true;
                    AddNCRExpenceBtn.Visible = false;
                    CancelNCRExpenceBtn.Visible = true;
                }
                else
                {
                    AddStatewiseCostFactors.Visible = true;
                    CancelCategoryMapping.Visible = true;
                    AddNCRExpenceBtn.Visible = true;
                    CancelNCRExpenceBtn.Visible = true;

                }


            }
            else
            {
                AddStatewiseCostFactors.Visible = false;
                CancelCategoryMapping.Visible = false;
                AddNCRExpenceBtn.Visible = false;
                CancelNCRExpenceBtn.Visible = false;

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
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }

        protected void AddStatewiseCostFactors_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
            pro.Enum_Price_Id = 8;
            pro.ProductCategoy_Id = Convert.ToInt32(ProductCategoryDropDown.SelectedValue);
            //pro.ExpenseWeightagePer = float.Parse(ExpenseWeightagetxt.Text);

            pro.RPLStaffExpense = decimal.Parse(RPLStaffExpensetxt.Text);
            pro.RPLDepoExpense = decimal.Parse(RPLDepoExpensetxt.Text);
            pro.RPLIncentive = decimal.Parse(RPLIncentivetxt.Text);
            pro.RPLMarketing = decimal.Parse(RPLMarketingtxt.Text);
            pro.RPLInterest = decimal.Parse(RPLInteresttxt.Text);
            pro.RPLOther = decimal.Parse(RPLOthertxt.Text);

            //pro.RPLTotal = decimal.Parse(RPLTotaltxt.Text);
            //pro.NCRTotal = decimal.Parse(NCRTotaltxt.Text); ;
            pro.User_Id = UserId;
            pro.FkComapny_Id = Company_Id;

            int status = cls.Insert_RPLStatewiseCostFactors(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_StatewiseCostFactorsData();
                ClearData();
            }
        }
        public void ClearData()
        {
            StateNameDropdown.SelectedIndex = 0;
            //ExpenseTypeDropDownList.SelectedIndex = 0;
            //PriceTypeDropDownList.SelectedIndex = 0;
            ProductCategoryDropDown.SelectedIndex = 0;
            //ExpenseWeightagetxt.Text = "";
            RPLStaffExpensetxt.Text = "0";
            RPLDepoExpensetxt.Text = "0";
            RPLIncentivetxt.Text = "0";
            RPLMarketingtxt.Text = "0";
            RPLInteresttxt.Text = "0";
            RPLOthertxt.Text = "0";
            RPLTotaltxt.Text = "0";
            lblPriceType_Id.Text = "";

            UpdateStatewiseCostFactors.Visible = false;
            AddStatewiseCostFactors.Visible = true;

        }
        public void ClearNCRData()
        {

            lblExpenceType_Id.Text = "";
            lblPriceType_Id.Text = "";
            lblProductCategory_Id.Text = "";
            lblState_Id.Text = "";

            StateNameDropdown.SelectedIndex = 0;
            ProductCategoryDropDown.SelectedIndex = 0;
            NCRStaffExpensetxt.Text = "0";
            NCRDepoExpensetxt.Text = "0";
            NCRIncentivetxt.Text = "0";
            NCRMarketingtxt.Text = "0";
            NCRInteresttxt.Text = "0";
            NCROthertxt.Text = "0";
            NCRTotaltxt.Text = "0";

            UpdateStatewiseCostFactors.Visible = false;
            AddStatewiseCostFactors.Visible = true;

        }
        public void Get_State_From_StateWiseCostFector()
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.FkComapny_Id = Company_Id;
            DataTable dtState = new DataTable();
            dtState = cls.Get_State_From_StateWiseCostFector(pro);
            StateFilterDropdown.DataSource = dtState;
            StateFilterDropdown.DataTextField = "StateName";
            StateFilterDropdown.DataValueField = "Fk_State_Id";

            StateFilterDropdown.DataBind();
            StateFilterDropdown.Items.Insert(0, "State");

        }
        public void Get_Category_From_StateWiseCostFector()
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.FkComapny_Id = Company_Id;
            DataTable dtState = new DataTable();
            dtState = cls.Get_Category_From_StateWiseCostFector(pro);
            CategoryFilterDropdown.DataSource = dtState;
            CategoryFilterDropdown.DataTextField = "ProductCategoryName";
            CategoryFilterDropdown.DataValueField = "Fk_ProductCategoy_Id";

            CategoryFilterDropdown.DataBind();
            CategoryFilterDropdown.Items.Insert(0, "Category");

        }
        public void Grid_StatewiseCostFactorsData()
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.FkComapny_Id = Company_Id;
            Grid_StatewiseCostFactors.DataSource = cls.Get_StatewiseCostFactors(pro);

            Grid_StatewiseCostFactors.DataBind();
        }
        protected void UpdateStatewiseCostFactors_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
            //pro.ExpenceType_Id = Convert.ToInt32(ExpenseTypeDropDownList.SelectedValue);
            //pro.Enum_Price_Id = Convert.ToInt32(PriceTypeDropDownList.SelectedValue);
            pro.ProductCategoy_Id = Convert.ToInt32(ProductCategoryDropDown.SelectedValue);
            //pro.ExpenseWeightagePer = float.Parse(ExpenseWeightagetxt.Text);
            pro.StatewiseCostFactors_Id = Convert.ToInt32(lblStatewiseCostFactors_Id.Text);
            pro.FkComapny_Id = Company_Id;
            pro.User_Id = UserId;
            pro.Enum_Price_Id = 8;

            pro.RPLStaffExpense = decimal.Parse(RPLStaffExpensetxt.Text);
            pro.RPLDepoExpense = decimal.Parse(RPLDepoExpensetxt.Text);
            pro.RPLIncentive = decimal.Parse(RPLIncentivetxt.Text);
            pro.RPLMarketing = decimal.Parse(RPLMarketingtxt.Text);
            pro.RPLInterest = decimal.Parse(RPLInteresttxt.Text);
            pro.RPLOther = decimal.Parse(RPLOthertxt.Text);
            int status = cls.Update_StatewiseCostFactors(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_StatewiseCostFactorsData();
                ClearData();
            }
        }

        protected void EditCategoryMapping_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int StatewiseCostFactors_Id = Convert.ToInt32(Grid_StatewiseCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.StatewiseCostFactors_Id = StatewiseCostFactors_Id;
            lblStatewiseCostFactors_Id.Text = StatewiseCostFactors_Id.ToString();
            DataTable dt = new DataTable();

            dt = cls.Get_StatewiseCostFactorsBy_Id(pro);

            try
            {

                //lblExpenceType_Id.Text = ExpenseTypeDropDownList.SelectedValue = dt.Rows[0]["Fk_ExpenceType_Id"].ToString();
                //lblPriceType_Id.Text = PriceTypeDropDownList.SelectedValue = dt.Rows[0]["Fk_Enum_Price_Id"].ToString();
                lblProductCategory_Id.Text = ProductCategoryDropDown.SelectedValue = dt.Rows[0]["Fk_ProductCategoy_Id"].ToString();
                lblState_Id.Text = StateNameDropdown.SelectedValue = dt.Rows[0]["Fk_State_Id"].ToString();
                lblPriceType_Id.Text = dt.Rows[0]["FkPriceTypeId"].ToString();
                if (lblPriceType_Id.Text == "8")
                {
                    RPLStaffExpensetxt.Text = dt.Rows[0]["StaffExpense"].ToString();
                    RPLDepoExpensetxt.Text = dt.Rows[0]["DepoExpence"].ToString();
                    RPLIncentivetxt.Text = dt.Rows[0]["Incentive"].ToString();
                    RPLMarketingtxt.Text = dt.Rows[0]["Marketing"].ToString();
                    RPLInteresttxt.Text = dt.Rows[0]["Interest"].ToString();
                    RPLOthertxt.Text = dt.Rows[0]["Other"].ToString();

                    RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();


                    AddStatewiseCostFactors.Visible = false;
                    UpdateStatewiseCostFactors.Visible = true;
                }
                if (lblPriceType_Id.Text == "9")
                {
                    NCRStaffExpensetxt.Text = dt.Rows[0]["StaffExpense"].ToString();
                    NCRDepoExpensetxt.Text = dt.Rows[0]["DepoExpence"].ToString();
                    NCRIncentivetxt.Text = dt.Rows[0]["Incentive"].ToString();
                    NCRMarketingtxt.Text = dt.Rows[0]["Marketing"].ToString();
                    NCRInteresttxt.Text = dt.Rows[0]["Interest"].ToString();
                    NCROthertxt.Text = dt.Rows[0]["Other"].ToString();

                    NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();

                    AddNCRExpenceBtn.Visible = false;
                    UpdateNCRExpenceBtn.Visible = true;
                }



            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void DelCategoryMapping_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            int StatewiseCostFactors_Id = Convert.ToInt32(Grid_StatewiseCostFactors.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.StatewiseCostFactors_Id = StatewiseCostFactors_Id;
            lblStatewiseCostFactors_Id.Text = StatewiseCostFactors_Id.ToString();

            int status = cls.Delete_StatewiseCostFactors(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                Grid_StatewiseCostFactorsData();
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Deleted !')", true);

            }
        }

        protected void CancelCategoryMapping_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void CancelNCRExpenceBtn_Click(object sender, EventArgs e)
        {
            ClearNCRData();
        }

        protected void UpdateNCRExpenceBtn_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
            //pro.ExpenceType_Id = Convert.ToInt32(ExpenseTypeDropDownList.SelectedValue);
            //pro.Enum_Price_Id = Convert.ToInt32(PriceTypeDropDownList.SelectedValue);
            pro.ProductCategoy_Id = Convert.ToInt32(ProductCategoryDropDown.SelectedValue);
            //pro.ExpenseWeightagePer = float.Parse(ExpenseWeightagetxt.Text);
            pro.StatewiseCostFactors_Id = Convert.ToInt32(lblStatewiseCostFactors_Id.Text);
            pro.FkComapny_Id = Company_Id;
            pro.Enum_Price_Id = 9;
            pro.User_Id = UserId;
            pro.NRVStaffExpense = decimal.Parse(NCRStaffExpensetxt.Text);
            pro.NCRDepoExpense = decimal.Parse(NCRDepoExpensetxt.Text);
            pro.NCRIncentive = decimal.Parse(NCRIncentivetxt.Text);
            pro.NCRMarketing = decimal.Parse(NCRMarketingtxt.Text);
            pro.NCRInterest = decimal.Parse(NCRInteresttxt.Text);
            pro.NCROther = decimal.Parse(NCROthertxt.Text);
            int status = cls.Update_NCRStatewiseCostFactors(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_StatewiseCostFactorsData();
                ClearNCRData();
            }
        }

        protected void AddNCRExpenceBtn_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.State_Id = Convert.ToInt32(StateNameDropdown.SelectedValue);
            pro.Enum_Price_Id = 9;
            pro.ProductCategoy_Id = Convert.ToInt32(ProductCategoryDropDown.SelectedValue);
            //pro.ExpenseWeightagePer = float.Parse(ExpenseWeightagetxt.Text);
            pro.NRVStaffExpense = decimal.Parse(NCRStaffExpensetxt.Text);
            pro.NCRDepoExpense = decimal.Parse(NCRDepoExpensetxt.Text);
            pro.NCRIncentive = decimal.Parse(NCRIncentivetxt.Text);
            pro.NCRMarketing = decimal.Parse(NCRMarketingtxt.Text);
            pro.NCRInterest = decimal.Parse(NCRInteresttxt.Text);
            pro.NCROther = decimal.Parse(NCROthertxt.Text);
            //pro.RPLTotal = decimal.Parse(RPLTotaltxt.Text);
            //pro.NCRTotal = decimal.Parse(NCRTotaltxt.Text); ;
            pro.User_Id = UserId;
            pro.FkComapny_Id = Company_Id;

            int status = cls.Insert_StatewiseCostFactors(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_StatewiseCostFactorsData();
                ClearNCRData();
            }
        }

        protected void RPLStaffExpensetxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text))
            {
                RPLStaffExpensetxt.Text = "0";
            }
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text) || String.IsNullOrEmpty(RPLDepoExpensetxt.Text) || String.IsNullOrEmpty(RPLIncentivetxt.Text) || String.IsNullOrEmpty(RPLMarketingtxt.Text) || String.IsNullOrEmpty(RPLInteresttxt.Text) || String.IsNullOrEmpty(RPLOthertxt.Text))
            {

            }
            else
            {
                RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();
            }

        }



        protected void RPLDepoExpensetxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RPLDepoExpensetxt.Text))
            {
                RPLDepoExpensetxt.Text = "0";
            }
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text) || String.IsNullOrEmpty(RPLDepoExpensetxt.Text) || String.IsNullOrEmpty(RPLIncentivetxt.Text) || String.IsNullOrEmpty(RPLMarketingtxt.Text) || String.IsNullOrEmpty(RPLInteresttxt.Text) || String.IsNullOrEmpty(RPLOthertxt.Text))
            {

            }
            else
            {
                RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();
            }
        }

        protected void NCRDepoExpensetxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NCRDepoExpensetxt.Text))
            {
                NCRDepoExpensetxt.Text = "0";
            }
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text) || String.IsNullOrEmpty(NCRDepoExpensetxt.Text) || String.IsNullOrEmpty(NCRIncentivetxt.Text) || String.IsNullOrEmpty(NCRMarketingtxt.Text) || String.IsNullOrEmpty(NCRInteresttxt.Text) || String.IsNullOrEmpty(NCROthertxt.Text))
            {

            }
            else
            {
                NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();
            }
        }

        protected void RPLIncentivetxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RPLIncentivetxt.Text))
            {
                RPLIncentivetxt.Text = "0";
            }
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text) || String.IsNullOrEmpty(RPLDepoExpensetxt.Text) || String.IsNullOrEmpty(RPLIncentivetxt.Text) || String.IsNullOrEmpty(RPLMarketingtxt.Text) || String.IsNullOrEmpty(RPLInteresttxt.Text) || String.IsNullOrEmpty(RPLOthertxt.Text))
            {

            }
            else
            {
                RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();
            }
        }

        protected void NCRIncentivetxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NCRIncentivetxt.Text))
            {
                NCRIncentivetxt.Text = "0";
            }
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text) || String.IsNullOrEmpty(NCRDepoExpensetxt.Text) || String.IsNullOrEmpty(NCRIncentivetxt.Text) || String.IsNullOrEmpty(NCRMarketingtxt.Text) || String.IsNullOrEmpty(NCRInteresttxt.Text) || String.IsNullOrEmpty(NCROthertxt.Text))
            {

            }
            else
            {
                NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();
            }
        }

        protected void RPLMarketingtxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RPLMarketingtxt.Text))
            {
                RPLMarketingtxt.Text = "0";
            }
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text) || String.IsNullOrEmpty(RPLDepoExpensetxt.Text) || String.IsNullOrEmpty(RPLIncentivetxt.Text) || String.IsNullOrEmpty(RPLMarketingtxt.Text) || String.IsNullOrEmpty(RPLInteresttxt.Text) || String.IsNullOrEmpty(RPLOthertxt.Text))
            {

            }
            else
            {
                RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();
            }
        }

        protected void NCRMarketingtxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NCRMarketingtxt.Text))
            {
                NCRMarketingtxt.Text = "0";
            }
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text) || String.IsNullOrEmpty(NCRDepoExpensetxt.Text) || String.IsNullOrEmpty(NCRIncentivetxt.Text) || String.IsNullOrEmpty(NCRMarketingtxt.Text) || String.IsNullOrEmpty(NCRInteresttxt.Text) || String.IsNullOrEmpty(NCROthertxt.Text))
            {

            }
            else
            {
                NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();
            }
        }

        protected void RPLInteresttxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RPLInteresttxt.Text))
            {
                RPLInteresttxt.Text = "0";
            }
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text) || String.IsNullOrEmpty(RPLDepoExpensetxt.Text) || String.IsNullOrEmpty(RPLIncentivetxt.Text) || String.IsNullOrEmpty(RPLMarketingtxt.Text) || String.IsNullOrEmpty(RPLInteresttxt.Text) || String.IsNullOrEmpty(RPLOthertxt.Text))
            {

            }
            else
            {
                RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();
            }
        }

        protected void NCRInteresttxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NCRInteresttxt.Text))
            {
                NCRInteresttxt.Text = "0";
            }
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text) || String.IsNullOrEmpty(NCRDepoExpensetxt.Text) || String.IsNullOrEmpty(NCRIncentivetxt.Text) || String.IsNullOrEmpty(NCRMarketingtxt.Text) || String.IsNullOrEmpty(NCRInteresttxt.Text) || String.IsNullOrEmpty(NCROthertxt.Text))
            {

            }
            else
            {
                NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();
            }
        }

        protected void RPLOthertxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RPLOthertxt.Text))
            {
                RPLOthertxt.Text = "0";
            }
            if (String.IsNullOrEmpty(RPLStaffExpensetxt.Text) || String.IsNullOrEmpty(RPLDepoExpensetxt.Text) || String.IsNullOrEmpty(RPLIncentivetxt.Text) || String.IsNullOrEmpty(RPLMarketingtxt.Text) || String.IsNullOrEmpty(RPLInteresttxt.Text) || String.IsNullOrEmpty(RPLOthertxt.Text))
            {

            }
            else
            {
                RPLTotaltxt.Text = (decimal.Parse(RPLStaffExpensetxt.Text) + decimal.Parse(RPLDepoExpensetxt.Text) + decimal.Parse(RPLIncentivetxt.Text) + decimal.Parse(RPLMarketingtxt.Text) + decimal.Parse(RPLInteresttxt.Text) + decimal.Parse(RPLOthertxt.Text)).ToString();
            }
        }

        protected void NCROthertxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NCROthertxt.Text))
            {
                NCROthertxt.Text = "0";
            }
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text) || String.IsNullOrEmpty(NCRDepoExpensetxt.Text) || String.IsNullOrEmpty(NCRIncentivetxt.Text) || String.IsNullOrEmpty(NCRMarketingtxt.Text) || String.IsNullOrEmpty(NCRInteresttxt.Text) || String.IsNullOrEmpty(NCROthertxt.Text))
            {

            }
            else
            {
                NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();
            }
        }



        protected void NCRStaffExpensetxt_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text))
            {
                NCRStaffExpensetxt.Text = "0";
            }
            if (String.IsNullOrEmpty(NCRStaffExpensetxt.Text) || String.IsNullOrEmpty(NCRDepoExpensetxt.Text) || String.IsNullOrEmpty(NCRIncentivetxt.Text) || String.IsNullOrEmpty(NCRMarketingtxt.Text) || String.IsNullOrEmpty(NCRInteresttxt.Text) || String.IsNullOrEmpty(NCROthertxt.Text))
            {

            }
            else
            {
                NCRTotaltxt.Text = (decimal.Parse(NCRStaffExpensetxt.Text) + decimal.Parse(NCRDepoExpensetxt.Text) + decimal.Parse(NCRIncentivetxt.Text) + decimal.Parse(NCRMarketingtxt.Text) + decimal.Parse(NCRInteresttxt.Text) + decimal.Parse(NCROthertxt.Text)).ToString();
            }
        }



        protected void BtnFilter_Click(object sender, EventArgs e)
        {

            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.FkComapny_Id = Company_Id;
            if (StateFilterDropdown.SelectedValue == "State")
            {
                pro.State_Id = 0;
            }
            else
            {
                pro.State_Id = Convert.ToInt32(StateFilterDropdown.SelectedValue);
            }
            if (CategoryFilterDropdown.SelectedValue == "Category")
            {
                pro.ProductCategoy_Id = 0;
            }
            else
            {
                pro.ProductCategoy_Id = Convert.ToInt32(CategoryFilterDropdown.SelectedValue);

            }

            Grid_StatewiseCostFactors.DataSource = cls.Get_StatewiseCostFectorByCategory_Id(pro);

            Grid_StatewiseCostFactors.DataBind();
        }

        protected void BtnFilterClear_Click(object sender, EventArgs e)
        {
            ClsStateWiseCostFactor cls = new ClsStateWiseCostFactor();
            pro.FkComapny_Id = Company_Id;
            pro.ProductCategoy_Id = 0;
            pro.State_Id = 0;
            CategoryFilterDropdown.SelectedIndex = 0;
            StateFilterDropdown.SelectedIndex = 0;
            Grid_StatewiseCostFactors.DataSource = cls.Get_StatewiseCostFectorByCategory_Id(pro);

            Grid_StatewiseCostFactors.DataBind();
        }

        protected void ProductCategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

          
        }
    }

}