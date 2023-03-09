using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{

    public partial class BulkProductMaster : System.Web.UI.Page
    {
        ClsBulkProductMaster cls = new ClsBulkProductMaster();
        ProBulkProductMaster pro = new ProBulkProductMaster();
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

                MainCategoryCombo();
                EnumMasterMeasurementMeasurement();
                GetEnumMasterBySource();
                GetGSTPercent();
                //IngrediantGridviewData();
                GridBulkProductMaster();

                ClearData();
                DisplayView();
            }
            else
            {
            }

        }
        public void GetGSTPercent()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            GST_Dropdown.DataSource = cls.GetGST_Percent();

            GST_Dropdown.DataTextField = "EnumDescription";
            GST_Dropdown.DataValueField = "EnumValue";


            GST_Dropdown.DataBind();
            GST_Dropdown.Items.Insert(0, "Select");
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[12]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[12]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[12]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[12]["CanEdit"]) == true)
            {
                BPMAddBtn_Id.Visible = true;
                BPMCancel_Id.Visible = true;
            }
            else
            {
                BPMAddBtn_Id.Visible = false;
                BPMCancel_Id.Visible = false;

            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[12]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[12]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[12]["CanEdit"]) == true)
            {
                BPMAddBtn_Id.Visible = true;
                BPMCancel_Id.Visible = true;
            }
            else
            {
                BPMAddBtn_Id.Visible = false;
                BPMCancel_Id.Visible = false;

            }
        }
        public void MainCategoryCombo()
        {
            ClsMainCategoryMaster cls = new ClsMainCategoryMaster();
            MainCategoryDropDown.DataSource = cls.GetMainCategoryData();

            MainCategoryDropDown.DataTextField = "MainCategory_Name";
            MainCategoryDropDown.DataValueField = "PkMainCategory_Id";
            MainCategoryDropDown.DataBind();
            MainCategoryDropDown.Items.Insert(0, "Select");


        }
        //public void FormulationDropdownCombo()
        //{
        //    FormulationDropdown.DataSource = cls.Get_BP_MasterData(UserId);

        //    FormulationDropdown.DataTextField = "BulkProductName";
        //    FormulationDropdown.DataValueField = "BPM_Product_Id";
        //    FormulationDropdown.DataBind();
        //    //FormulationDropdown.Items.Insert(0, "Select");
        //}
        public void EnumMasterMeasurementMeasurement()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            EnumMasterMeasurementDropdown.DataSource = cls.GetEnumMasterMeasurement();

            EnumMasterMeasurementDropdown.DataTextField = "EnumDescription";
            EnumMasterMeasurementDropdown.DataValueField = "PkEnumId";
            

            EnumMasterMeasurementDropdown.DataBind();
            EnumMasterMeasurementDropdown.Items.Insert(0, "Select");

        }
        public void GridBulkProductMaster()
        {
            GridBulkProductMasterList.DataSource = cls.Get_BP_MasterData(UserId);
            GridBulkProductMasterList.DataBind();


        }
        //public void IngrediantGridviewData()
        //{

        //    IngrediantGridview.DataSource = cls.GridIngredientMaster();
        //    IngrediantGridview.DataBind();
        //}


        public void GetEnumMasterBySource()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            SourceDropdown.DataSource = cls.GetEnumMasterMeasurementSource();

            SourceDropdown.DataTextField = "EnumDescription";
            SourceDropdown.DataValueField = "PkEnumId";
           
            SourceDropdown.DataBind();
            SourceDropdown.Items.Insert(0, "Select");

        }

        public void ClearData()
        {
            MainCategoryDropDown.SelectedIndex = 0;
            EnumMasterMeasurementDropdown.SelectedIndex = 0;
            SourceDropdown.SelectedIndex = 0;
            GST_Dropdown.SelectedIndex = 0;
            BulkProductNametxt.Text = "";
            lblBPM_Id.Text = "";
        }

        protected void BPMCancel_Id_Click(object sender, EventArgs e)
        {
            MainCategoryDropDown.SelectedIndex = 0;
            EnumMasterMeasurementDropdown.SelectedIndex = 0;
            SourceDropdown.SelectedIndex = 0;
            GST_Dropdown.SelectedIndex = 0;
            BulkProductNametxt.Text = "";
            BPMAddBtn_Id.Visible = true;
            BPMUpdateBtn.Visible = false;
        }

        protected void BPMAddBtn_Id_Click(object sender, EventArgs e)
        {
            pro.MainCategory_Id = Convert.ToInt32(MainCategoryDropDown.SelectedValue);
            pro.EnumMaster_UnitMeasurement_Id = Convert.ToInt32(EnumMasterMeasurementDropdown.SelectedValue);
            pro.EnumMaster_Source_Id = Convert.ToInt32(SourceDropdown.SelectedValue);
            pro.EnumMaster_Source_Id = Convert.ToInt32(SourceDropdown.SelectedValue);
            pro.Gst_Percent = Convert.ToInt32(GST_Dropdown.SelectedValue);
            pro.BPM_Product_Name = BulkProductNametxt.Text;
            pro.User_Id = UserId;
            int status = cls.InsertBulkProductionMasterData(pro);

            if (status > 0)
            {
                GridBulkProductMaster();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void MainCategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int MainCategory_Id = Convert.ToInt32(MainCategoryDropDown.SelectedIndex);
            //if (MainCategoryDropDown.SelectedIndex == 0)
            //{
            //    GridBulkProductMasterList.DataSource = cls.Get_BP_MasterData(UserId);
            //    GridBulkProductMasterList.DataBind();
            //}
            //else
            //{
            //    GridBulkProductMasterList.DataSource = cls.Get_BP_MasterDataById(UserId, MainCategory_Id);
            //    GridBulkProductMasterList.DataBind();
            //}
        }

        protected void EditBulkProducationBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int BulkProductId = Convert.ToInt32(GridBulkProductMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblBPM_Id.Text = BulkProductId.ToString();
            GetGSTPercent();
            DataTable dt = new DataTable();

            dt = cls.Get_BP_MasterDataById(UserId, BulkProductId);



            BulkProductNametxt.Text = dt.Rows[0]["BulkProductName"].ToString();
            //MainCategoryDropDown.SelectedItem.Text = dt.Rows[0]["MainCategory_Name"].ToString();
            MainCategoryDropDown.SelectedValue = dt.Rows[0]["MainCategory_Id"].ToString();
            //EnumMasterMeasurementDropdown.SelectedItem.Text = dt.Rows[0]["Measurement"].ToString();
            EnumMasterMeasurementDropdown.SelectedValue = dt.Rows[0]["Measurement_Id"].ToString();
            GST_Dropdown.SelectedValue = dt.Rows[0]["GST_Percent"].ToString();
            //SourceDropdown.SelectedItem.Text = dt.Rows[0]["Source"].ToString();
            SourceDropdown.SelectedValue = dt.Rows[0]["Source_Id"].ToString();



            BPMUpdateBtn.Visible = true;
            BPMCancel_Id.Visible = true;
            BPMAddBtn_Id.Visible = false;

        }
        protected void BPMUpdateBtn_Click(object sender, EventArgs e)
        {

            pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.BPM_Product_Name = BulkProductNametxt.Text;
            pro.MainCategory_Id = Convert.ToInt32(MainCategoryDropDown.SelectedValue);
            //pro.RM_Category_ID = Convert.ToInt32(RMCategoryDropdownList.SelectedValue);
            pro.EnumMaster_UnitMeasurement_Id = Convert.ToInt32(EnumMasterMeasurementDropdown.SelectedValue);
            pro.EnumMaster_Source_Id = Convert.ToInt32(SourceDropdown.SelectedValue);
            pro.Gst_Percent = Convert.ToInt32(GST_Dropdown.SelectedValue);

            pro.User_Id = UserId;
           
            int status = cls.UpdatetBulkProductionMasterData(pro);


            if (status > 0)
            {
                GridBulkProductMaster();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed!')", true);

            }

            BPMUpdateBtn.Visible = false;
            BPMAddBtn_Id.Visible = true;
        }

        protected void BulkProductNametxt_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            pro.BPM_Product_Name = BulkProductNametxt.Text;
            dt = cls.CHECK_BulkProductMasterData( pro);
            if (dt.Rows.Count>0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple BulkProduct !')", true);
               
                ClearData();
                return;
            }
        }
    }


}
