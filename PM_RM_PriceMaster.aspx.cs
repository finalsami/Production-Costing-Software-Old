using DataAccessLayer;
using System;
using System.Web.UI;
using BusinessAccessLayer;
using System.Data;
using System.Web.UI.WebControls;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Collections.Generic;

namespace Production_Costing_Software
{
    public partial class PM_RM_PriceMaster : System.Web.UI.Page
    {
        ProPM_RM_PriceMaster pro = new ProPM_RM_PriceMaster();
        ClsPM_RM_PriceMaster cls = new ClsPM_RM_PriceMaster();
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

                PM_RM_CategoryCombo();
                PM_RM_NameCombo();

                //PM_RM_WeightMeasurementCombo();
                GridPM_RM_Price_MasterList();
                UnitMeasurementHide.Visible = false;
                DisplayView();
            }
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                pro.User_Id = Convert.ToInt32(Session["UserId"]);
                UserId = pro.User_Id;
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[1]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[1]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[1]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPM_RM_Price_Id.Text != "")
                {
                    Addbtn.Visible = false;
                    Updatebtn.Visible = true;
                }
                else
                {
                    Addbtn.Visible = true;
                    Updatebtn.Visible = false;
                }

            }
            else
            {
                Addbtn.Visible = false;
                Updatebtn.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[1]["GroupId"]);
            if (GroupId != 15)
            {
                if (Convert.ToBoolean(dtMenuList.Rows[1]["CanEdit"]) == true)
                {
                    if (lblPM_RM_Price_Id.Text != "")
                    {
                        Addbtn.Visible = false;
                        Updatebtn.Visible = true;
                    }
                    else
                    {
                        Addbtn.Visible = true;
                        Updatebtn.Visible = false;
                    }


                }
            }

        }
        public void PM_RM_CategoryCombo()
        {

            ClsPM_RM_Category cls = new ClsPM_RM_Category();
            PMRMCategoryDropdownCombo.DataSource = cls.GetPM_RMCategoryData(UserId);
            PMRMCategoryDropdownCombo.DataTextField = "PM_RM_Category_Name";
            PMRMCategoryDropdownCombo.DataValueField = "PM_RM_Category_id";
            PMRMCategoryDropdownCombo.DataBind();
            if (!IsPostBack)
            {
                PMRMCategoryDropdownCombo.Items.Insert(0, "Select");
            }
        }
        public void PM_RM_NameCombo()
        {

            ClsPM_RM_Master cls = new ClsPM_RM_Master();
            DataTable dt = new DataTable();
            dt = cls.Get_PMRM_MasterData(UserId);
            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "PM_Name";

            PMRMNameDropdownCombo.DataSource = dvOptions;
            PMRMNameDropdownCombo.DataTextField = "PM_Name";
            PMRMNameDropdownCombo.DataValueField = "PM_RM_id";

            PMRMNameDropdownCombo.DataBind();
            PMRMNameDropdownCombo.Items.Insert(0, "Select");

        }

        protected void Addbtn_Click(object sender, EventArgs e)
        {
            pro.User_Id = UserId;
            pro.PM_RM_Category_Id = Convert.ToInt32(PMRMCategoryDropdownCombo.SelectedValue);
            pro.PM_RM_Id = Convert.ToInt32(PMRMNameDropdownCombo.SelectedValue);

            if (Pricetxt.Text == "")
            {
                Pricetxt.Text = "0";
            }
            if (Transportationtxt.Text == "")
            {
                Transportationtxt.Text = "0";
            }
            if (Units_KGtxt.Text == "")
            {
                Units_KGtxt.Text = "0";
            }
            pro.Price = decimal.Parse(Pricetxt.Text);

            pro.Transportation = Convert.ToDecimal(Transportationtxt.Text);
            pro.No_of_Units_KG = decimal.Parse(Units_KGtxt.Text);
            if (UnitPriceMeaurementtxt.Text.ToUpper() == "KG")
            {
                pro.UnitMeasurement_Unit_KG = 2;
            }
            else if (UnitPriceMeaurementtxt.Text.ToUpper() == "LTR")
            {
                pro.UnitMeasurement_Unit_KG = 1;
            }
            else
            {
                pro.UnitMeasurement_Unit_KG = 3;
            }
            if (Losstxt.Text == "")
            {
                Losstxt.Text = "0";
            }
            if (Price_Unittxt.Text == "")
            {
                Price_Unittxt.Text = "0";
            }
            pro.Loss = decimal.Parse(Losstxt.Text);
            pro.Price_Unit = decimal.Parse(Price_Unittxt.Text);

            int status = cls.Insert_PMRM_PriceMasterData(pro);

            if (status > 0)
            {
                GridPM_RM_Price_MasterList();
                CleareData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }

        }

        public void GridPM_RM_Price_MasterList()

        {
            DataTable dt = new DataTable();
            GridRM_Price_MasterList.DataSource = cls.Get_PMRM_PriceMasterData(UserId);

            GridRM_Price_MasterList.DataBind();
        }

        protected void PMRMNameDropdownCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PMRMNameDropdownCombo.SelectedValue != "Select")
            {


                ClsPM_RM_PriceMaster clsCheck = new ClsPM_RM_PriceMaster();
                pro.PM_RM_Category_Id = Convert.ToInt32(PMRMCategoryDropdownCombo.SelectedValue);
                pro.PM_RM_Id = Convert.ToInt32(PMRMNameDropdownCombo.SelectedValue);
                DataTable dtCheck = new DataTable();
                dtCheck = clsCheck.CHECK_PMRM_Price_Master(pro);
                //if (dtCheck.Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple PMRM Name !')", true);
                //    CleareData();
                //    return;
                //}

                ClsPM_RM_Master cls = new ClsPM_RM_Master();
                int RMName_ID = Convert.ToInt32(PMRMNameDropdownCombo.SelectedValue);
                if (PMRMCategoryDropdownCombo.SelectedValue != "Select")
                {
                    int PMRMCategory_ID = Convert.ToInt32(PMRMCategoryDropdownCombo.SelectedValue);
                    DataTable dt = new DataTable();

                    dt = cls.Get_PMRM_Master_Unit_KG(UserId, RMName_ID, PMRMCategory_ID);

                    string Number = dt.Rows[0]["PM_RM_Price_KG_Unit"].ToString();
                    if (Number == "2")
                    {
                        lblUnitPriceMeaurement.Text = "KG";
                    }
                    if (Number == "1")
                    {
                        lblUnitPriceMeaurement.Text = "LTR";
                    }
                    if (Number == "3")
                    {
                        lblUnitPriceMeaurement.Text = "UNIT";
                    }
                    if (Number == "6")
                    {
                        lblUnitPriceMeaurement.Text = "ml";
                    }
                    if (Number == "7")
                    {
                        lblUnitPriceMeaurement.Text = "Gm";
                    }
                    if (Number == "3" || Number == "1")
                    {
                        Units_KGtxt.Text = dt.Rows[0]["PM_RM_No_of_Unit"].ToString();

                    }
                    else
                    {
                        Units_KGtxt.Text = dt.Rows[0]["PM_RM_Unit_KG"].ToString();

                    }

                }

            }
            //Pricetxt.Text = "";
            //Transportationtxt.Text = "";
            //Losstxt.Text = "";
            //Price_Unittxt.Text = "";
        }

        protected void PMRMCategoryDropdownCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PMRMCategoryDropdownCombo.SelectedValue != "Select")
            {
                ClsPM_RM_Master cls = new ClsPM_RM_Master();
                int PMRMCategory_ID = Convert.ToInt32(PMRMCategoryDropdownCombo.SelectedValue);
                PMRMNameDropdownCombo.Enabled = true;

                //if (PMRMNameDropdownCombo.SelectedIndex == 0)
                //{
                //    PMRMNameDropdownCombo.DataSource = cls.Get_PMRM_MasterData(UserId);
                //    PMRMNameDropdownCombo.DataBind();
                //}
                //else
                //{

                DataTable dt = new DataTable();
                dt = cls.Get_PMRM_MasterByCategory_Id(UserId, PMRMCategory_ID);
                DataView dvOptions = new DataView(dt);
                dvOptions.Sort = "PM_RM_Name";

                PMRMNameDropdownCombo.DataSource = dvOptions;
                PMRMNameDropdownCombo.DataTextField = "PM_RM_Name";
                PMRMNameDropdownCombo.DataValueField = "PM_RM_Id";
                PMRMNameDropdownCombo.DataBind();
                PMRMNameDropdownCombo.Items.Insert(0, "Select");

                Pricetxt.Text = "0.00";
                Transportationtxt.Text = "0.00";
                Losstxt.Text = "0.00";
                UnitPriceMeaurementtxt.Text = "0";
                Units_KGtxt.Text = "0";
                Price_Unittxt.Text = "0.00";
            }
            else
            {
                //PMRMNameDropdownCombo.DataSource = cls.Get_PMRM_MasterData(UserId);
                //PMRMNameDropdownCombo.DataTextField = "PM_RM_Name";
                //PMRMNameDropdownCombo.DataValueField = "PM_RM_Id";
                //PMRMNameDropdownCombo.DataBind();
                //if (!IsPostBack)
                //{
                //    PMRMNameDropdownCombo.Items.Insert(0, "Select");

                //}
                PMRMNameDropdownCombo.Enabled = false;
                PMRMNameDropdownCombo.SelectedIndex = 0;
            }

        }

        protected void Editbtn_Click(object sender, EventArgs e)
        {
            if (Transportationtxt.Text == "")
            {
                Transportationtxt.Text = "0";
            }
            if (Losstxt.Text == "")
            {
                Losstxt.Text = "0";
            }
            if (Pricetxt.Text == "")
            {
                Pricetxt.Text = "0";

            }
            try
            {
                string SumofPriceAndTransport;
                SumofPriceAndTransport = (decimal.Parse(Pricetxt.Text) + decimal.Parse(Transportationtxt.Text)).ToString("0.00");

                string DevideByUnit_Kg;
                DevideByUnit_Kg = decimal.Parse(Units_KGtxt.Text).ToString("0.00");
                string Loss = decimal.Parse(Losstxt.Text).ToString("0.00");
                string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(DevideByUnit_Kg)).ToString("0.00");
                Price_Unittxt.Text = (decimal.Parse(Result) - decimal.Parse(Loss)).ToString("0.00");
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void Pricetxt_TextChanged(object sender, EventArgs e)
        {
            if (Transportationtxt.Text == "")
            {
                Transportationtxt.Text = "0.00";
            }
            if (Losstxt.Text == "")
            {
                Losstxt.Text = "0.00";
            }
            if (Pricetxt.Text == "")
            {
                Pricetxt.Text = "0.00";

            }
            string SumofPriceAndTransport;
            SumofPriceAndTransport = (decimal.Parse(Pricetxt.Text) + decimal.Parse(Transportationtxt.Text)).ToString("0.00");

            if (lblUnitPriceMeaurement.Text == "UNIT")
            {
                Price_Unittxt.Text = Pricetxt.Text;
            }

            if (lblUnitPriceMeaurement.Text != "UNIT" && (Units_KGtxt.Text != "" || Units_KGtxt.Text != "0.00" || Units_KGtxt.Text != "0"))
            {


                string Percent = "100";
                string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(Units_KGtxt.Text)).ToString("0.00");
                Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                string SubTotal = (decimal.Parse(Result) * decimal.Parse(Percent)).ToString("0.00");
                Price_Unittxt.Text = (decimal.Parse(Result) - decimal.Parse(SubTotal)).ToString("0.00");



            }
            else
            {
                //string Percent = "100.00";
                //string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(Units_KGtxt.Text)).ToString("0.00");
                //Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                //string SubTotal = (decimal.Parse(Result) * decimal.Parse(Percent)).ToString("0.00");
                //Price_Unittxt.Text = (decimal.Parse(Result) + decimal.Parse(SubTotal)).ToString("0.00");
                //Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                //string SubTotal = (decimal.Parse(SumofPriceAndTransport) * decimal.Parse(Percent)).ToString("0.00");
                string Loss = (decimal.Parse(SumofPriceAndTransport) * decimal.Parse(Losstxt.Text) / 100).ToString("0.00");

                Price_Unittxt.Text = (decimal.Parse(SumofPriceAndTransport) + decimal.Parse(Loss)).ToString("0.00");

            }
        }

        protected void Transportationtxt_TextChanged(object sender, EventArgs e)
        {
            if (Transportationtxt.Text == "")
            {
                Transportationtxt.Text = "0.00";
            }
            if (Losstxt.Text == "")
            {
                Losstxt.Text = "0.00";
            }
            if (Pricetxt.Text == "")
            {
                Pricetxt.Text = "0.00";

            }
            string SumofPriceAndTransport;
            SumofPriceAndTransport = (decimal.Parse(Pricetxt.Text) + decimal.Parse(Transportationtxt.Text)).ToString("0.00");

            if (lblUnitPriceMeaurement.Text == "UNIT")
            {
                Price_Unittxt.Text = SumofPriceAndTransport;
            }
            if (lblUnitPriceMeaurement.Text != "UNIT" && (Units_KGtxt.Text != "0.00" || Units_KGtxt.Text != "0"))
            {

                string Percent = "100";
                string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(Units_KGtxt.Text)).ToString("0.00");
                Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                string SubTotal = (decimal.Parse(Result) * decimal.Parse(Percent)).ToString("0.00");
                Price_Unittxt.Text = (decimal.Parse(Result) - decimal.Parse(SubTotal)).ToString("0.00");



            }
            else
            {
                //string Percent = "100.00";
                //string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(Units_KGtxt.Text)).ToString("0.00");
                //Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                //string SubTotal = (decimal.Parse(Result) * decimal.Parse(Percent)).ToString("0.00");
                //Price_Unittxt.Text = (decimal.Parse(Result) + decimal.Parse(SubTotal)).ToString("0.00");
                //Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                //string SubTotal = (decimal.Parse(SumofPriceAndTransport) * decimal.Parse(Percent)).ToString("0.00");
                string Loss = (decimal.Parse(SumofPriceAndTransport) * decimal.Parse(Losstxt.Text) / 100).ToString("0.00");

                Price_Unittxt.Text = (decimal.Parse(SumofPriceAndTransport) + decimal.Parse(Loss)).ToString("0.00");

            }
        }

        protected void Losstxt_TextChanged(object sender, EventArgs e)
        {
            if (Transportationtxt.Text == "")
            {
                Transportationtxt.Text = "0.00";
            }
            if (Losstxt.Text == "")
            {
                Losstxt.Text = "0.00";
            }
            if (Pricetxt.Text == "")
            {
                Pricetxt.Text = "0.00";

            }
            string SumofPriceAndTransport;
            SumofPriceAndTransport = (decimal.Parse(Pricetxt.Text) + decimal.Parse(Transportationtxt.Text)).ToString("0.00");

            if (lblUnitPriceMeaurement.Text == "UNIT")
            {
                Price_Unittxt.Text = SumofPriceAndTransport;
            }

            if (lblUnitPriceMeaurement.Text != "UNIT" && (Units_KGtxt.Text != "0.00" || Units_KGtxt.Text != "0"))
            {


                string Percent = "100";

                string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(Units_KGtxt.Text)).ToString("0.00");
                Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                string SubTotal = (decimal.Parse(Result) * decimal.Parse(Percent)).ToString("0.00");
                Price_Unittxt.Text = (decimal.Parse(Result) + decimal.Parse(SubTotal)).ToString("0.00");



            }
            else
            {
                //string Percent = "100.00";
                //string Result = (decimal.Parse(SumofPriceAndTransport) / decimal.Parse(Units_KGtxt.Text)).ToString("0.00");
                //Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                //string SubTotal = (decimal.Parse(Result) * decimal.Parse(Percent)).ToString("0.00");
                //Price_Unittxt.Text = (decimal.Parse(Result) + decimal.Parse(SubTotal)).ToString("0.00");
                //Percent = (decimal.Parse(Losstxt.Text) / decimal.Parse(Percent)).ToString("0.00");
                //string SubTotal = (decimal.Parse(SumofPriceAndTransport) * decimal.Parse(Percent)).ToString("0.00");
                string Loss = (decimal.Parse(SumofPriceAndTransport) * decimal.Parse(Losstxt.Text) / 100).ToString("0.00");

                Price_Unittxt.Text = (decimal.Parse(SumofPriceAndTransport) + decimal.Parse(Loss)).ToString("0.00");

            }



        }

        protected void EditPMRMPriceBtn_Click(object sender, EventArgs e)
        {
            PM_RM_NameCombo();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int PM_RM_Price_Id = Convert.ToInt32(GridRM_Price_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());

            DataTable dt = new DataTable();
            dt = cls.Get_PMRM_PriceMasterGridBy_Id(PM_RM_Price_Id, UserId);
            pro.PM_RM_Category_Id = Convert.ToInt32(dt.Rows[0]["PM_RM_Category_id"]);
            lblPM_RM_Category_Id.Text = pro.PM_RM_Category_Id.ToString();
            pro.PM_RM_Price_Id = PM_RM_Price_Id;
            lblPM_RM_Price_Id.Text = pro.PM_RM_Price_Id.ToString();
            PMRMCategoryDropdownCombo.SelectedValue = dt.Rows[0]["PM_RM_Category_id"].ToString();
            lblPM_RM_Id.Text = dt.Rows[0]["PM_RM_id"].ToString();

            PMRMNameDropdownCombo.SelectedValue = Convert.ToInt32(dt.Rows[0]["PM_RM_id"]).ToString();
            UnitPriceMeaurementtxt.Text = dt.Rows[0]["Unit_Price"].ToString();
            Pricetxt.Text = dt.Rows[0]["PM_RM_Price"].ToString();
            Transportationtxt.Text = dt.Rows[0]["Transportation"].ToString();
            Units_KGtxt.Text = dt.Rows[0]["Unit/KG"].ToString();
            Losstxt.Text = dt.Rows[0]["Loss"].ToString();
            Price_Unittxt.Text = dt.Rows[0]["Price/Unit"].ToString();

            //Code added by Harshul -16042022

            lblUnitPriceMeaurement.Text= dt.Rows[0]["Unit_Price"].ToString();

            PMRMNameDropdownCombo.Enabled = false;
            PMRMCategoryDropdownCombo.Enabled = false;

            Addbtn.Visible = false;
            Updatebtn.Visible = true;
            CancelBtn.Visible = true;
        }

        protected void DelPMRMPriceBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PM_RM_PriceId = Convert.ToInt32(GridRM_Price_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.Delete_PMRM_PriceMaster(PM_RM_PriceId, UserId);
            if (status > 0)
            {
                GridPM_RM_Price_MasterList();
                CleareData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
        }
        public void CleareData()
        {
            Pricetxt.Text = "";
            PMRMNameDropdownCombo.Enabled = false;
            PMRMCategoryDropdownCombo.Enabled = true;
            UnitPriceMeaurementtxt.Text = "";
            Transportationtxt.Text = "0.00";
            Units_KGtxt.Text = "0.00";
            Losstxt.Text = "0.00";
            Price_Unittxt.Text = "0.00";
            lblPM_RM_Category_Id.Text = "";
            lblPM_RM_Price_Id.Text = "";            
            PMRMNameDropdownCombo.SelectedIndex = 0;
            PMRMCategoryDropdownCombo.SelectedIndex = 0;

            
        }

        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            pro.User_Id = UserId;

            if (PMRMNameDropdownCombo.SelectedValue == "Select")
            {

            }
            pro.PM_RM_Id = Convert.ToInt32(lblPM_RM_Id.Text);

            pro.PM_RM_Price_Id = Convert.ToInt32(lblPM_RM_Price_Id.Text);
            pro.Price = decimal.Parse(Pricetxt.Text);
            pro.PM_RM_Category_Id = Convert.ToInt32(lblPM_RM_Category_Id.Text);
            pro.Transportation = Convert.ToDecimal(Transportationtxt.Text);
            pro.No_of_Units_KG = decimal.Parse(Units_KGtxt.Text);
            if (UnitPriceMeaurementtxt.Text.ToUpper() == "KG")
            {
                pro.UnitMeasurement_Unit_KG = 2;
            }
            else if (UnitPriceMeaurementtxt.Text.ToUpper() == "LTR")
            {
                pro.UnitMeasurement_Unit_KG = 1;
            }
            else if (UnitPriceMeaurementtxt.Text.ToUpper() == "UNIT")
            {
                pro.UnitMeasurement_Unit_KG = 3;
            }
            else if (UnitPriceMeaurementtxt.Text.ToUpper() == "ML")
            {
                pro.UnitMeasurement_Unit_KG = 6;
            }
            else if (UnitPriceMeaurementtxt.Text.ToUpper() == "GM")
            {
                pro.UnitMeasurement_Unit_KG = 7;
            }

            pro.Loss = decimal.Parse(Losstxt.Text);
            pro.Price_Unit = decimal.Parse(Price_Unittxt.Text);

            int status = cls.Update_PM_RM_PriceMaster(pro);
            if (status > 0)
            {
                GridPM_RM_Price_MasterList();
                CleareData();
                Addbtn.Visible = true;
                Updatebtn.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Fail')", true);

            }

        }

        protected void PMRMNameDropdownCombo_TextChanged(object sender, EventArgs e)
        {
            if (PMRMNameDropdownCombo.SelectedValue != "Select")
            {
                ClsPM_RM_Master cls = new ClsPM_RM_Master();
                int RMName_ID = Convert.ToInt32(PMRMNameDropdownCombo.SelectedValue);
                if (PMRMCategoryDropdownCombo.SelectedValue != "Select")
                {
                    int PMRMCategory_ID = Convert.ToInt32(PMRMCategoryDropdownCombo.SelectedValue);
                    DataTable dt = new DataTable();

                    dt = cls.Get_PMRM_Master_Unit_KG(UserId, RMName_ID, PMRMCategory_ID);

                    string Number = dt.Rows[0]["PM_RM_Price_KG_Unit"].ToString();
                    if (Number == "2")
                    {
                        UnitPriceMeaurementtxt.Text = "KG";
                    }
                    if (Number == "1")
                    {
                        UnitPriceMeaurementtxt.Text = "LTR";
                    }
                    if (Number == "3")
                    {
                        UnitPriceMeaurementtxt.Text = "UNIT";
                    }
                    if (Number == "6")
                    {
                        UnitPriceMeaurementtxt.Text = "ml";
                    }
                    if (Number == "7")
                    {
                        UnitPriceMeaurementtxt.Text = "Gm";
                    }
                    if (Number == "3" || Number == "1")
                    {
                        Units_KGtxt.Text = dt.Rows[0]["PM_RM_No_of_Unit"].ToString();

                    }
                    else
                    {
                        Units_KGtxt.Text = dt.Rows[0]["PM_RM_Unit_KG"].ToString();

                    }
                    //Pricetxt.Text = "";
                    //Transportationtxt.Text = "";
                    //Losstxt.Text = "";
                }

            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            CleareData();
            Addbtn.Visible = true;
            Updatebtn.Visible = false;

        }

        //protected void GridRM_Price_MasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridPM_RM_Price_MasterList();

        //    GridRM_Price_MasterList.PageIndex = e.NewPageIndex;
        //    GridRM_Price_MasterList.DataBind();
        //}


        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_PM_from_PMRM_Price";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchPMRM_Price", prefixText);

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
        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void SearchId_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            GridPM_RM_Price_MasterList();
        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Search_PM_from_PMRM_Price", con);
                cmd.Parameters.AddWithValue("@SearchPMRM_Price", TxtSearch.Text.Trim());


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

            GridRM_Price_MasterList.DataSource = dt;
            GridRM_Price_MasterList.DataBind();
        }
    }


}
