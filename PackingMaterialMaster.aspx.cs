using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class PackingMaterialMaster : System.Web.UI.Page
    {
        ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
        ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
        int UserId;
        int Status;
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
                PM_RMCategoryDataCombo();
                MeasurementDataCombo();
                BulkProductDropDownListCombo();
                //BulkProductDropDownListCombo1();
                PackingMeasurementDataCombo();
                Grid_PackingMaterialMasterData();
                PMNameDropdowm.Enabled = false;
                PMNameDropdowm.Items.Insert(0, "Select");
                PMRMCategoryDropdown.Items.Insert(0, "Select");
                ShipperTypeDataCombo();
                ShipperCostDropdownCombo();
                DisplayView();
            }
        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[17]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[17]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[17]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPackingMaterial_Id.Text != "")
                {
                    Addbtn.Visible = false;
                    Cancel.Visible = true;
                    Updatebtn.Visible = true;
                }
                else
                {
                    Addbtn.Visible = true;
                    Cancel.Visible = true;
                    Updatebtn.Visible = false;
                }

            }
            else
            {
                Addbtn.Visible = false;
                Cancel.Visible = false;
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[17]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[17]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[17]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPackingMaterial_Id.Text != "")
                {
                    Addbtn.Visible = false;
                    Cancel.Visible = true;
                    Updatebtn.Visible = true;
                }
                else
                {
                    Addbtn.Visible = true;
                    Cancel.Visible = true;
                    Updatebtn.Visible = false;
                }

            }
            else
            {
                Addbtn.Visible = false;
                Cancel.Visible = false;
                Updatebtn.Visible = false;
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
                pro.User_Id = Convert.ToInt32(Session["UserId"]);
                UserId = pro.User_Id;
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void PM_RMCategoryDataCombo()
        {

            ClsPM_RM_Category cls = new ClsPM_RM_Category();
            PMRMCategoryDropdown.DataSource = cls.sp_PM_RM_CategoryFilter();
            PMRMCategoryDropdown.DataTextField = "PM_RM_Category_Name";
            PMRMCategoryDropdown.DataValueField = "PM_RM_Category_id";
            PMRMCategoryDropdown.DataBind();

        }
        public void ShipperTypeDataCombo()
        {

            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ShipperTypeDropdown.DataSource = cls.Get_ShipperTypeCombo();
            ShipperTypeDropdown.DataTextField = "PM_RM_Category_Name";
            ShipperTypeDropdown.DataValueField = "PM_RM_Category_id";
            ShipperTypeDropdown.DataBind();
            ShipperTypeDropdown.Items.Insert(0, "Select");


        }
        public void ShipperCostDropdownCombo()
        {

            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ShipperCostDropdown.DataSource = cls.Get_ShipperTypeCombo();
            ShipperCostDropdown.DataTextField = "PM_RM_Category_Name";
            ShipperCostDropdown.DataValueField = "PM_RM_Category_id";
            ShipperCostDropdown.DataBind();
            ShipperCostDropdown.Items.Insert(0, "Select");


        }

        public void PM_RM_NameCombo()
        {

            ClsPM_RM_Master cls = new ClsPM_RM_Master();
            PMNameDropdowm.DataSource = cls.Get_PMRM_MasterData(UserId);
            PMNameDropdowm.DataTextField = "PM_Name";
            PMNameDropdowm.DataValueField = "PM_RM_id";

            PMNameDropdowm.DataBind();
            PMNameDropdowm.Items.Insert(0, "Select");

        }
        public void PM_RM_NameComboById()
        {

            ClsPM_RM_Master cls = new ClsPM_RM_Master();
            PMNameDropdowm.DataSource = cls.Get_PMRM_MasterByCategory_Id(UserId, Convert.ToInt32(lblMainCategory_Id.Text));
            PMNameDropdowm.DataTextField = "PM_RM_Name";
            PMNameDropdowm.DataValueField = "PM_RM_id";

            PMNameDropdowm.DataBind();
            PMNameDropdowm.Items.Insert(0, "Select");
            if (!IsPostBack)
            {
                PMNameDropdowm.AppendDataBoundItems = false;

            }
            else
            {
                PMNameDropdowm.AppendDataBoundItems = false;

            }
        }
        public void BulkProductDropDownListCombo()
        {
            ClsBulkProductMaster cls = new ClsBulkProductMaster();
            BulkProductDropDownList.DataSource = cls.Get_BP_MasterDataCombo(UserId);
            BulkProductDropDownList.DataTextField = "BulkProductName";
            BulkProductDropDownList.DataValueField = "BPM_Product_Id";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }
        //public void BulkProductDropDownListCombo1()
        //{
        //    ClsBulkProductMaster cls = new ClsBulkProductMaster();
        //    BulkProductDropdownList1.DataSource = cls.Get_BP_MasterDataCombo(UserId);
        //    BulkProductDropdownList1.DataTextField = "BulkProductName";
        //    BulkProductDropdownList1.DataValueField = "BPM_Product_Id";
        //    BulkProductDropdownList1.DataBind();
        //    BulkProductDropdownList1.Items.Insert(0, "Select");
        //}

        public void MeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeaurementDropdown.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeaurementDropdown.DataTextField = "EnumDescription";
            UnitMeaurementDropdown.DataValueField = "PkEnumId";
            UnitMeaurementDropdown.DataBind();

        }
        public void PackingMeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            PackingMeasurementDropdown.DataSource = cls.GetEnumMasterMeasurement();
            PackingMeasurementDropdown.DataTextField = "EnumDescription";
            PackingMeasurementDropdown.DataValueField = "PkEnumId";
            PackingMeasurementDropdown.DataBind();
            PackingMeasurementDropdown.Items.Insert(0, "Select");

        }





        protected void PackingMeasurementDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ShipperSizetxt.Text != "" && PackingSizetxt.Text != "")
            {
                if (PackingMeasurementDropdown.SelectedValue == "6" || PackingMeasurementDropdown.SelectedValue == "7" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "2" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    //UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / 1).ToString("");
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");

                }

            }
        }

        protected void PackingSizetxt_TextChanged(object sender, EventArgs e)
        {
            if (ShipperSizetxt.Text != "" && PackingSizetxt.Text != "")
            {
                if (PackingMeasurementDropdown.SelectedValue == "6" || PackingMeasurementDropdown.SelectedValue == "7" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "2" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    //UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / 1).ToString("");
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");

                }

            }
        }

        protected void ShipperSizetxt_TextChanged(object sender, EventArgs e)
        {
            if (ShipperSizetxt.Text != "" && PackingSizetxt.Text != "")
            {
                if (PackingMeasurementDropdown.SelectedValue == "6" || PackingMeasurementDropdown.SelectedValue == "7" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "2")
                {
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    //FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(UnitShippertxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "2" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    //UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / 1).ToString("");
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "2" && UnitMeaurementDropdown.SelectedValue == "2")
                {
                    //UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / 1).ToString("");
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");

                }
            }
        }

        protected void UnitMeaurementDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShipperSizetxt.Text != "" && PackingSizetxt.Text != "")
            {
                if (PackingMeasurementDropdown.SelectedValue == "6" || PackingMeasurementDropdown.SelectedValue == "7" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("0.00");

                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    string LiterConvert = "1000";
                    string SubTotal;
                    SubTotal = (decimal.Parse(LiterConvert) / decimal.Parse(PackingSizetxt.Text)).ToString("0.00");
                    UnitShippertxt.Text = (decimal.Parse(SubTotal) * decimal.Parse(ShipperSizetxt.Text)).ToString("0.00");
                }
                if (PackingMeasurementDropdown.SelectedValue == "1" && UnitMeaurementDropdown.SelectedValue == "1")
                {
                    UnitShippertxt.Text = (decimal.Parse(ShipperSizetxt.Text) / 1).ToString("0.00");
                }

            }
        }


        public void Grid_PackingMaterialCostingMasterData()
        {
            DataTable dt = new DataTable();
            dt = cls.Get_PackingMaterialMasterCostingGridByBMP_Id(UserId, Convert.ToInt32(lblPackingMaterial_Id.Text), Convert.ToInt32(lblBPM_Id.Text));
            if (dt.Rows.Count > 0)
            {
                Grid_PackingMaterialCostingMaster.DataSource = dt;
                Grid_PackingMaterialCostingMaster.DataBind();
            }
        }
        public void Grid_PackingMaterialMasterData()
        {
            pro.User_Id = UserId;
            DataTable dt = new DataTable();

            Grid_PackingMaterialMaster.DataSource = dt = cls.Get_SubPackingMaterialMaster(pro);
            Grid_PackingMaterialMaster.DataBind();
        }
        public void Grid_PackingMaterialMasterDataByMasterPacking()
        {
            pro.User_Id = UserId;
            DataTable dt = new DataTable();

            Grid_PackingMaterialMaster.DataSource = dt = cls.Get_SubPackingMaterialMasterByValuesByMasterPacking(pro);
            Grid_PackingMaterialMaster.DataBind();
        }
        protected void PM_Price_Cost_Unittxt_TextChanged(object sender, EventArgs e)
        {
            FinalPackingtxt.Text = PM_Price_Cost_Unittxt.Text;
        }


        protected void Addbtn_Click(object sender, EventArgs e)
        {
            pro.BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            pro.Pack_Measurement = Convert.ToInt32(PackingMeasurementDropdown.SelectedValue);
            pro.Pack_Unit_Measurement = Convert.ToInt32(UnitMeaurementDropdown.SelectedValue);
            pro.Pack_Name = PackingNametxt.Text; ;
            pro.Pack_size = decimal.Parse(PackingSizetxt.Text);
            pro.Pack_ShipperSize = decimal.Parse(ShipperSizetxt.Text);
            pro.Pack_Unit_Shipper = decimal.Parse(UnitShippertxt.Text);
            pro.PackingLossPer = 0;
            pro.User_Id = UserId;
            if (ShipperTypeDropdown.SelectedValue != "Select")
            {
                pro.PMRM_Category_Id = Convert.ToInt32(ShipperTypeDropdown.SelectedValue);
            }
            else
            {
                pro.PMRM_Category_Id = 5;
            }
            //if (pro.Pack_size >= 500 && pro.Pack_Measurement == 6 || pro.Pack_Measurement == 7)
            //{
            //    pro.PackingLossPer = decimal.Parse("0.500");
            //}
            //else if (pro.Pack_size >= 1 && pro.Pack_Measurement == 1 || pro.Pack_Measurement == 2)
            //{
            //    pro.PackingLossPer = decimal.Parse("0.500");
            //}
            //else if (pro.Pack_size >= 250 && pro.Pack_size < 1 &&  ( pro.Pack_Measurement == 6 || pro.Pack_Measurement == 7))
            //{
            //    pro.PackingLossPer = decimal.Parse("0.500");
            //}
            //else if (pro.Pack_size >= 100 && pro.Pack_size <= 249 && (pro.Pack_Measurement == 6 || pro.Pack_Measurement == 7))
            //{
            //    pro.PackingLossPer = decimal.Parse("1.25");
            //}
            //else
            //{
            //    pro.PackingLossPer = decimal.Parse("2.000");

            //}
            if (ChkIsPackingMaster.Checked == true)
            {
                pro.isMasterPacking = 1;
            }
            else
            {
                pro.isMasterPacking = 0;
            }
            pro.User_Id = UserId;
            int status = cls.Insert_SubPackingMaterialMaster(pro);

            if (status > 0)
            {
                System.Threading.Thread.Sleep(5000);

                ClearMaterialData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_PackingMaterialMasterData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
            DataTable dt = new DataTable();
            dt = cls.Get_SubPackingMaterialMaster(pro);
            lblPAckMaterial_Id.Text = Convert.ToInt32(dt.Rows[0]["Pack_Material_Id"]).ToString();
            lblPAckMaterialMeasurement.Text = Convert.ToInt32(dt.Rows[0]["Pack_Measurement"]).ToString();

        }

        protected void AddSubcostingBtn1_Click(object sender, EventArgs e)
        {
            if (PMRMCategoryDropdown.SelectedValue != "Select" && PMNameDropdowm.SelectedValue != "Select")
            {
                pro.Fk_PMRM_Category_Id = Convert.ToInt32(PMRMCategoryDropdown.SelectedValue);

            }
            else
            {
                pro.Fk_PMRM_Category_Id = Convert.ToInt32(ShipperCostDropdown.SelectedValue);

            }
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Fk_PMRM_Id = Convert.ToInt32(PMNameDropdowm.SelectedValue);
            pro.Fk_PMRM_Price_Cost_Unit = decimal.Parse(PM_Price_Cost_Unittxt.Text);
            pro.Final_Pack_Cost_Unit = decimal.Parse(FinalPackingtxt.Text);
            pro.User_Id = UserId;
            pro.Total_Product_PM_Cost_Unit = decimal.Parse(FinalPackingtxt.Text);
            pro.Total_Product_PM_Cost_Ltr = decimal.Parse(TotalProductPMCostLtrtxt.Text);
            pro.Pack_Material_Id = Convert.ToInt32(lblPackingMaterial_Id.Text);

            int status = cls.Insert_SubCostingMaster1(pro);

            if (status > 0)
            {
                Grid_PackingMaterialCostingMasterData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearData();
                PMNameDropdowm.SelectedIndex = 0;
                PMNameDropdowm.Enabled = false;
            }
        }

        protected void EditCostingPopup_Click(object sender, EventArgs e)
        {
            ClearDataNew();
            Button Add = sender as Button;
            GridViewRow gdrow = Add.NamingContainer as GridViewRow;
            pro.Pack_Material_Id = Convert.ToInt32(Grid_PackingMaterialMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            DataTable dt1 = new DataTable();
            pro.User_Id = UserId;
            dt1 = cls.Get_SubPackingMaterialMasterById(pro);
            if (dt1.Rows.Count > 0)
            {
                lblPackingMaterial_Id.Text = pro.Pack_Material_Id.ToString();
                lblBPM_Name.Text = (dt1.Rows[0]["BPM_Product_Name"].ToString());
                lblBPM_Id.Text = (dt1.Rows[0]["Fk_BPM_Id"].ToString());
                lblShipperSize.Text = dt1.Rows[0]["Pack_ShipperSize"].ToString();
                lblUnitPerShipper.Text = dt1.Rows[0]["Pack_Unit_Shipper"].ToString();
                lblPackSize.Text = dt1.Rows[0]["Pack_size"].ToString();
                lblPAckMaterialMeasurement.Text = dt1.Rows[0]["Pack_Measurement"].ToString();
                //ShipperCostDropdownCombo();
                // code added by harshul 15-4-2022
                /*PM_RMCategory_Id to  PM_RM_Category_Id*/
                ShipperCostDropdown.SelectedValue = Convert.ToInt32(dt1.Rows[0]["PM_RM_Category_Id"]).ToString();
                lblPM_RM_Category_Id.Text = ShipperCostDropdown.SelectedValue;
                ShipperCostDropdown.Enabled = false;
                Grid_PackingMaterialCostingMasterData();
                UpdateSubcostingBtn.Visible = false;
                lblPackSizeName.Text = dt1.Rows[0]["Pack_size"].ToString() + '-' + dt1.Rows[0]["PackMeasurement"].ToString();
                lblPMCategoryName.Text = dt1.Rows[0]["PM_RM_Category_Name"].ToString();


                //   ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup1();", true);


                Grid_FinalPakingMaterialMasterData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('In suffecient Data!')", true);
                return;
            }


        }

        decimal Total_Product_PM_Cost_Unit;
        decimal Total_Product_PM_Cost_Ltr;

        protected void Grid_PackingMaterialCostingMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            decimal ConvertToLtr;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Total_Product_PM_Cost_Unit += decimal.Parse(DataBinder.Eval(e.Row.DataItem, "Total_Product_PM_Cost_Unit").ToString());

                Total_Product_PM_Cost_Ltr += decimal.Parse(DataBinder.Eval(e.Row.DataItem, "Total_Product_PM_Cost_Ltr").ToString());

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "TotalCostUnit";
                e.Row.Cells[2].Font.Bold = true;

                e.Row.Cells[3].Text = Total_Product_PM_Cost_Unit.ToString(); ;
                e.Row.Cells[3].Font.Bold = true;

                e.Row.Cells[4].Text = "TotalCost/Ltr : ";
                e.Row.Cells[4].Font.Bold = true;

                ConvertToLtr = decimal.Parse("1000") / decimal.Parse(lblPackSize.Text);
                if (lblPAckMaterialMeasurement.Text == "6" || lblPAckMaterialMeasurement.Text == "7")

                {
                    lblTotal_Product_PM_Cost_Ltr.Text = (Total_Product_PM_Cost_Unit * ConvertToLtr).ToString("0.00");
                }
                else
                {
                    lblTotal_Product_PM_Cost_Ltr.Text = Total_Product_PM_Cost_Unit.ToString("0.00");

                }
                if (decimal.Parse(lblPackSize.Text) > 1)
                {
                    if (lblPAckMaterialMeasurement.Text == "1" || lblPAckMaterialMeasurement.Text == "2")
                    {
                        ConvertToLtr = decimal.Parse("1000") * decimal.Parse(lblPackSize.Text) / decimal.Parse("1000");
                        lblTotal_Product_PM_Cost_Ltr.Text = (Total_Product_PM_Cost_Unit / ConvertToLtr).ToString("0.00");
                    }
                }

                e.Row.Cells[5].Text = lblTotal_Product_PM_Cost_Ltr.Text;
                e.Row.Cells[5].Font.Bold = true;
            }

            lblTotal_Product_PM_Cost_Unit.Text = Total_Product_PM_Cost_Unit.ToString();
            //lblTotal_Product_PM_Cost_Ltr.Text = Total_Product_PM_Cost_Ltr.ToString();


            //decimal ConvertToLtr = decimal.Parse("1000") / decimal.Parse(lblPackSize.Text);
            //if (lblPAckMaterialMeasurement.Text == "6" || lblPAckMaterialMeasurement.Text == "7")

            //{
            //    lblTotal_Product_PM_Cost_Ltr.Text = (decimal.Parse(lblTotal_Product_PM_Cost_Unit.Text) * ConvertToLtr).ToString();
            //}
            //else
            //{
            //    lblTotal_Product_PM_Cost_Ltr.Text = Total_Product_PM_Cost_Ltr.ToString();

            //}

            //lblGridSubTotalAmount.Text = TotalAmounttxt.Text;
        }

        protected void DelCostingMaterialBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PackingCostingMaterial_Id = Convert.ToInt32(Grid_PackingMaterialCostingMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.Pack_Sub_Cost_Id = PackingCostingMaterial_Id;
            pro.User_Id = UserId;
            int status = cls.DeleteCostingMaterialMaster(pro);
            if (status > 0)
            {
                Grid_PackingMaterialCostingMasterData();
            }
        }

        protected void EditCostingMaterialBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int MaterialCosting_Id = Convert.ToInt32(Grid_PackingMaterialCostingMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            lblMaterialCosting_Id.Text = MaterialCosting_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            pro.Pack_Sub_Cost_Id = MaterialCosting_Id;
            PM_RM_NameCombo();
            //PM_RMCategoryDataCombo();
            ShipperCostDropdownCombo();
            dt = cls.Get_PackingMaterialMasterCostingGridById(pro);


            String PMRMCat_Id = Convert.ToInt32(dt.Rows[0]["PMRM_Category_Id"]).ToString();
            string CheckShipper = Convert.ToInt32(dt.Rows[0]["ChkIsShipper"]).ToString();
            lblPM_RM_Category_Id.Text = PMRMCat_Id.ToString();
            if (CheckShipper == "1")
            {
                PM_Price_Cost_Unittxt.Text = dt.Rows[0]["PMRM_Price_Cost_Unit"].ToString();

                //FinalPackingtxt.Text = dt.Rows[0]["Final_Pack_Cost_Unit"].ToString();
                UnitShippertxt.Text = dt.Rows[0]["Pack_Unit_Shipper"].ToString();
                
                FinalPackingtxt.Text = (Convert.ToDecimal(PM_Price_Cost_Unittxt.Text) / Convert.ToDecimal(UnitShippertxt.Text)).ToString("0.00");
                ShipperCostDropdown.SelectedValue = Convert.ToInt32(dt.Rows[0]["PMRM_Category_Id"]).ToString();
                PMNameDropdowm.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_PMRM_Id"]).ToString();
                TotalProductPMCostLtrtxt.Text = dt.Rows[0]["Total_Product_PM_Cost_Ltr"].ToString();
                TotalProductPMCostUnittxt.Text = FinalPackingtxt.Text;
            }
            else
            {
                PMRMCategoryDropdown.SelectedValue = Convert.ToInt32(dt.Rows[0]["PMRM_Category_Id"]).ToString();
                PMNameDropdowm.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_PMRM_Id"]).ToString();
                PM_Price_Cost_Unittxt.Text = dt.Rows[0]["PMRM_Price_Cost_Unit"].ToString();
                TotalProductPMCostLtrtxt.Text = dt.Rows[0]["Total_Product_PM_Cost_Ltr"].ToString();
                FinalPackingtxt.Text = dt.Rows[0]["Final_Pack_Cost_Unit"].ToString();

            }



            lblMaterialCosting_Id.Text = MaterialCosting_Id.ToString();

            UpdateSubcostingBtn.Visible = true;
            AddSubcostingBtn1.Visible = false;
            CancelCosting.Visible = true;
        }

        protected void DelPackingMaterialsMasterBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PackingMaterial_Id = Convert.ToInt32(Grid_PackingMaterialMaster.DataKeys[gdrow.RowIndex].Value.ToString());

            pro.Pack_Material_Id = PackingMaterial_Id;
            pro.User_Id = UserId;
            int status = cls.DeletePackingMaterialMaster(pro);
            if (status > 0)
            {
                Grid_PackingMaterialMasterData();
                ClearMaterialData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Delete..It Used in Another Master')", true);
            }
        }

        protected void EditPackingMaterialsMasterBtn_Click(object sender, EventArgs e)
        {
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();

            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int PackingMaterial_Id = Convert.ToInt32(Grid_PackingMaterialMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.Pack_Material_Id = PackingMaterial_Id;
            lblPackingMaterial_Id.Text = PackingMaterial_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            dt = cls.Get_SubPackingMaterialMasterById(pro);

            //lblPAckMaterial_Id.Text = dt.Rows[0]["Pack_Material_Id"].ToString(); ;
            BulkProductDropDownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_BPM_Id"]).ToString();
            PackingMeasurementDropdown.SelectedValue = Convert.ToInt32(dt.Rows[0]["Pack_Measurement"]).ToString();
            UnitMeaurementDropdown.SelectedValue = Convert.ToInt32(dt.Rows[0]["Pack_Unit_Measurement"]).ToString();
            PackingNametxt.Text = dt.Rows[0]["Pack_Name"].ToString();
            ShipperSizetxt.Text = dt.Rows[0]["Pack_ShipperSize"].ToString();
            PackingSizetxt.Text = dt.Rows[0]["Pack_size"].ToString();
            UnitShippertxt.Text = dt.Rows[0]["Pack_Unit_Shipper"].ToString();
            ShipperTypeDropdown.SelectedValue = dt.Rows[0]["PM_RM_Category_Id"].ToString();

            bool ISChkMasterPacking = Convert.ToBoolean(dt.Rows[0]["IsMasterPacking"]);

            if (ISChkMasterPacking == true)
            {
                ChkIsPackingMaster.Checked = true;
            }
            else
            {
                ChkIsPackingMaster.Checked = false;

            }
            if (lblPackingMaterial_Id.Text != "")
            {
                Addbtn.Visible = false;
                Cancel.Visible = true;
                Updatebtn.Visible = true;
            }
            else
            {
                Addbtn.Visible = true;
                Cancel.Visible = true;
                Updatebtn.Visible = false;
            }
        }





        public void ClearData()
        {
            PMRMCategoryDropdown.SelectedIndex = -1;
            PMNameDropdowm.SelectedIndex = -1;
            PM_Price_Cost_Unittxt.Text = "";
            FinalPackingtxt.Text = "";
            lblMaterialCosting_Id.Text = "";
            lblPackingMaterial_Id.Text = "";

        }
        public void ClearDataNew()
        {
            PMRMCategoryDropdown.SelectedIndex = -1;
            PMNameDropdowm.SelectedIndex = -1;
            //PM_Price_Cost_Unittxt.Text = "";
            //FinalPackingtxt.Text = "";
            //lblMaterialCosting_Id.Text = "";

        }

        protected void CancelCosting_Click(object sender, EventArgs e)
        {
            PMRMCategoryDropdown.SelectedIndex = 0;
            PMNameDropdowm.SelectedIndex = 0;
            PM_Price_Cost_Unittxt.Text = "";
            FinalPackingtxt.Text = "";
            //lblPAckMaterialMeasurement.Text = "";
            TotalProductPMCostLtrtxt.Text = "";
            TotalProductPMCostUnittxt.Text = "";
            lblMaterialCosting_Id.Text = "";
            UpdateSubcostingBtn.Visible = false;
            AddSubcostingBtn1.Visible = true;
            CancelCosting.Visible = false;
            //lblPackingMaterial_Id.Text="";
        }

        protected void UpdateSubcostingBtn_Click1(object sender, EventArgs e)
        {
            if (PMRMCategoryDropdown.SelectedValue == "Select")
            {
                pro.Fk_PMRM_Category_Id = Convert.ToInt32(lblPM_RM_Category_Id.Text);
            }
            else
            {
                pro.Fk_PMRM_Category_Id = Convert.ToInt32(PMRMCategoryDropdown.SelectedValue);

            }
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Fk_PMRM_Id = Convert.ToInt32(PMNameDropdowm.SelectedValue);
            pro.Fk_PMRM_Price_Cost_Unit = decimal.Parse(PM_Price_Cost_Unittxt.Text);
            pro.Final_Pack_Cost_Unit = decimal.Parse(FinalPackingtxt.Text);
            pro.User_Id = UserId;
            pro.Total_Product_PM_Cost_Unit = decimal.Parse(TotalProductPMCostUnittxt.Text);
            pro.Total_Product_PM_Cost_Ltr = decimal.Parse(TotalProductPMCostLtrtxt.Text);
            pro.Pack_Material_Id = Convert.ToInt32(lblPackingMaterial_Id.Text);
            pro.Pack_Sub_Cost_Id = Convert.ToInt32(lblMaterialCosting_Id.Text);


            int status = cls.Update_SubCostingMaster(pro);

            if (status > 0)
            {
                Grid_PackingMaterialCostingMasterData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                UpdateSubcostingBtn.Visible = false;
                AddSubcostingBtn1.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed')", true);

            }



        }

        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            pro.BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            pro.Pack_Measurement = Convert.ToInt32(PackingMeasurementDropdown.SelectedValue);
            pro.Pack_Unit_Measurement = Convert.ToInt32(UnitMeaurementDropdown.SelectedValue);
            pro.Pack_Name = PackingNametxt.Text; ;
            pro.Pack_size = decimal.Parse(PackingSizetxt.Text);
            pro.Pack_ShipperSize = decimal.Parse(ShipperSizetxt.Text);
            pro.Pack_Unit_Shipper = decimal.Parse(UnitShippertxt.Text);
            pro.Pack_Material_Id = Convert.ToInt32(lblPackingMaterial_Id.Text);
            if (ShipperTypeDropdown.SelectedValue != "Select")
            {
                pro.PMRM_Category_Id = Convert.ToInt32(ShipperTypeDropdown.SelectedValue);
            }
            else
            {
                pro.PMRM_Category_Id = 5;

            }
            if (ChkIsPackingMaster.Checked == true)
            {
                pro.isMasterPacking = 1;
            }
            else
            {
                pro.isMasterPacking = 0;
            }
            pro.User_Id = UserId;
            int status = cls.Update_SubPackingMaterialMaster(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_PackingMaterialMasterData();
                ClearMaterialData();


                Updatebtn.Visible = false;
                Addbtn.Visible = true;
            }
            DataTable dt = new DataTable();
            dt = cls.Get_SubPackingMaterialMaster(pro);
            lblPAckMaterial_Id.Text = Convert.ToInt32(dt.Rows[0]["Pack_Material_Id"]).ToString();
            lblPAckMaterialMeasurement.Text = Convert.ToInt32(dt.Rows[0]["Pack_Measurement"]).ToString();

        }
        public void ClearMaterialData()
        {
            //lblPAckMaterial_Id.Text = "";
            BulkProductDropDownList.SelectedIndex = 0;
            PackingMeasurementDropdown.SelectedIndex = 0;
            UnitMeaurementDropdown.SelectedIndex = 0;
            PackingNametxt.Text = "";
            ShipperSizetxt.Text = "";
            PackingSizetxt.Text = "";
            UnitShippertxt.Text = "";
            Updatebtn.Visible = false;
            Addbtn.Visible = true;
            ShipperTypeDropdown.SelectedIndex = -1;
            ChkShipperType.Checked = false;
            ShipperTypeDropdown.Enabled = false;
            ChkIsPackingMaster.Checked = false;

        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            lblPAckMaterial_Id.Text = "";
            BulkProductDropDownList.SelectedIndex = 0;
            PackingMeasurementDropdown.SelectedIndex = 0;
            UnitMeaurementDropdown.SelectedIndex = 0;
            PackingNametxt.Text = "";
            ShipperSizetxt.Text = "";
            PackingSizetxt.Text = "";
            UnitShippertxt.Text = "";
            Updatebtn.Visible = false;
            Addbtn.Visible = true;
            ShipperTypeDropdown.SelectedIndex = -1;
            ChkShipperType.Checked = false;
            ShipperTypeDropdown.Enabled = false;
            ChkIsPackingMaster.Checked = false;
            lblPackingMaterial_Id.Text = "";
        }

        protected void AddFinalMaterialMaster_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Pack_Material_Id = Convert.ToInt32(lblPackingMaterial_Id.Text);
            //pro.Fk_PMRM_Id = Convert.ToInt32(dt.Rows[0]["Fk_PMRM_Id"].ToString());
            pro.Fk_PMRM_Category_Id = Convert.ToInt32(lblPM_RM_Category_Id.Text);
            //pro.Fk_PMRM_Price_Cost_Unit= decimal.Parse(dt.Rows[0]["PMRM_Price_Cost_Unit"].ToString()); 
            //pro.Final_Pack_Cost_Unit= decimal.Parse(dt.Rows[0]["Final_Pack_Cost_Unit"].ToString());
            //pro.Total_Product_PM_Cost_Ltr = decimal.Parse(lblTotal_Product_PM_Cost_Ltr.Text);
            pro.Total_Product_PM_Cost_Unit = decimal.Parse(lblTotal_Product_PM_Cost_Unit.Text);

            pro.Total_Product_PM_Cost_Ltr = decimal.Parse(lblTotal_Product_PM_Cost_Ltr.Text);

            Status = cls.Insert_FinalPackingMaterialMaster(pro);



            if (Status > 0)
            {
                Grid_PackingMaterialCostingMasterData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearData();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
            Grid_FinalPakingMaterialMasterData();
        }
        public void Grid_FinalPakingMaterialMasterData()
        {
            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Pack_size = decimal.Parse(lblPackSize.Text);
            pro.Pack_Unit_Measurement = Convert.ToInt32(lblPAckMaterialMeasurement.Text);
            pro.Fk_PMRM_Category_Id = Convert.ToInt32(lblPM_RM_Category_Id.Text);
            Grid_FinalPakingMaterialMaster.DataSource = dt = cls.Get_FinalPackingMaterialMasterByPackSize_BPM_Id(pro);
            Grid_FinalPakingMaterialMaster.DataBind();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    lblFinalPackingMaterialMaster_Id.Text = dt.Rows[0]["FinalPackingMaterialMaster_Id"].ToString();
                    AddFinalMaterialMaster.Visible = false;
                    UpdateFinalMaterialMaster.Visible = true;

                }
                else
                {
                    AddFinalMaterialMaster.Visible = true;
                    UpdateFinalMaterialMaster.Visible = false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Grid_PackingMaterialMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void PMRMCategoryDropdown_TextChanged(object sender, EventArgs e)
        {
            ClsPM_RM_Master cls = new ClsPM_RM_Master();
            if (PMRMCategoryDropdown.SelectedValue != "Select")
            {
                PM_Price_Cost_Unittxt.Text = "";

                FinalPackingtxt.Text = "";
                TotalProductPMCostUnittxt.Text = "";
                TotalProductPMCostLtrtxt.Text = "";
                int PMRMCategory_ID = Convert.ToInt32(PMRMCategoryDropdown.SelectedValue);
                lblMainCategory_Id.Text = PMRMCategory_ID.ToString();
                PMRMCategoryDropdown.Enabled = true;

                PMNameDropdowm.DataSource = cls.Get_PMRM_MasterByCategory_Id(UserId, PMRMCategory_ID);
                PMNameDropdowm.DataTextField = "PM_RM_Name";
                PMNameDropdowm.DataValueField = "PM_RM_Id";
                PMNameDropdowm.DataBind();
                PMNameDropdowm.Enabled = true;

                PM_RM_NameComboById();
            }
            else
            {
                PMNameDropdowm.Enabled = false;

            }
        }



        protected void ChkShipperType_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkShipperType.Checked == true)
            {
                ShipperTypeDropdown.Enabled = true;
            }
            else
            {
                ShipperTypeDropdown.Enabled = false;
                ShipperTypeDropdown.SelectedIndex = 0;
            }
        }



        protected void ChkBOXShipper_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBOXShipper.Checked == true)
            {
                ClearDataNew();
                PMRMCategoryDropdown.SelectedIndex = 0;
                PMNameDropdowm.SelectedIndex = 0;
                PMRMCategoryDropdown.Enabled = false;
                PMNameDropdowm.Enabled = true;

                ClsPM_RM_Master cls = new ClsPM_RM_Master();
                PMNameDropdowm.DataSource = cls.Get_PMRM_MasterByCategory_Id(UserId, Convert.ToInt32(ShipperCostDropdown.SelectedValue));
                PMNameDropdowm.DataTextField = "PM_RM_Name";
                PMNameDropdowm.DataValueField = "PM_RM_Id";
                PMNameDropdowm.DataBind();
                PMNameDropdowm.Items.Insert(0, "Select");
                PMNameDropdowm.SelectedIndex = 0;

                PMNameDropdowm.Enabled = true;
            }
            else
            {
                PMRMCategoryDropdown.SelectedIndex = 0;
                PMNameDropdowm.SelectedIndex = 0;
                PMRMCategoryDropdown.Enabled = true;
                PMNameDropdowm.Enabled = false;
                FinalPackingtxt.Text = "";
                PM_Price_Cost_Unittxt.Text = "";
            }
        }
        protected void PMRMCategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PMRMCategoryDropdown.SelectedValue != "Select")
            {
                ClsPM_RM_Master cls = new ClsPM_RM_Master();
                int PMRMCategory_ID = Convert.ToInt32(PMRMCategoryDropdown.SelectedValue);
                lblMainCategory_Id.Text = PMRMCategory_ID.ToString();
                lblPM_RM_Category_Id.Text = PMRMCategory_ID.ToString();
                PMRMCategoryDropdown.Enabled = true;

                //if (PMRMCategoryDropdown.SelectedValue == "4")
                //{
                //    lblPM_RM_Category_Id.Text = "10";
                //}
                //else if (PMRMCategoryDropdown.SelectedValue == "6")
                //{
                //    lblPM_RM_Category_Id.Text = "12";
                //}

                PMNameDropdowm.DataSource = cls.Get_PMRM_MasterByCategory_Id(UserId, PMRMCategory_ID);
                PMNameDropdowm.DataTextField = "PM_RM_Name";
                PMNameDropdowm.DataValueField = "PM_RM_Id";
                PMNameDropdowm.DataBind();
                PMNameDropdowm.Items.Insert(0, "Select");
                PMNameDropdowm.Enabled = true;

                PM_RM_NameComboById();
            }

            else
            {
                PMNameDropdowm.Enabled = false;
                PMNameDropdowm.SelectedIndex = 0;
                PM_Price_Cost_Unittxt.Text = "";
                FinalPackingtxt.Text = "";
            }



        }

        protected void ShipperCostDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void PMNameDropdowm_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (PMNameDropdowm.SelectedValue != "Select")
            {
                PM_Price_Cost_Unittxt.Text = "";

                FinalPackingtxt.Text = "";
                TotalProductPMCostUnittxt.Text = "";
                TotalProductPMCostLtrtxt.Text = "";
                ClsPM_RM_PriceMaster cls = new ClsPM_RM_PriceMaster();

                int PMName_ID = Convert.ToInt32(PMNameDropdowm.SelectedValue);
                int PM_RM_Category_Id;
                DataTable dt = new DataTable();
                if (ChkBOXShipper.Checked == false)
                {
                    PM_RM_Category_Id = Convert.ToInt32(lblPM_RM_Category_Id.Text);
                    dt = cls.Get_PMRM_PriceMasterByCategoryAndNameIdOthers(UserId, PMName_ID, PM_RM_Category_Id);
                    if (dt.Rows.Count > 0)
                    {

                        PM_Price_Cost_Unittxt.Text = dt.Rows[0]["Price/Unit"].ToString();
                        lblUnitMeasurement.Text = dt.Rows[0]["PM_RM_Unit_Price_Measurement"].ToString();

                        if (lblUnitMeasurement.Text == "3" || lblUnitMeasurement.Text == "2" || lblUnitMeasurement.Text == "1")
                        {
                            FinalPackingtxt.Text = PM_Price_Cost_Unittxt.Text;

                            TotalProductPMCostUnittxt.Text = FinalPackingtxt.Text;
                            if (lblPAckMaterialMeasurement.Text == "1" && lblPackSize.Text == "1.00")
                            {
                                TotalProductPMCostLtrtxt.Text = FinalPackingtxt.Text;

                            }
                            if (lblPAckMaterialMeasurement.Text == "2" && lblPackSize.Text == "1.00")
                            {
                                TotalProductPMCostLtrtxt.Text = FinalPackingtxt.Text;

                            }
                            if ((lblPAckMaterialMeasurement.Text == "2" || lblPAckMaterialMeasurement.Text == "1"))
                            {
                                if (decimal.Parse(lblPackSize.Text) > decimal.Parse("1.00"))
                                {
                                    decimal ConvertToLtr = decimal.Parse(lblPackSize.Text) * decimal.Parse("1000") / decimal.Parse("1000");

                                    TotalProductPMCostLtrtxt.Text = (decimal.Parse(FinalPackingtxt.Text) / ConvertToLtr).ToString("0.00");
                                }
                            }
                            if ((lblPAckMaterialMeasurement.Text == "6" || lblPAckMaterialMeasurement.Text == "7"))
                            {
                                decimal CostLiter = (decimal.Parse("1000") / decimal.Parse(lblPackSize.Text));
                                TotalProductPMCostLtrtxt.Text = (decimal.Parse(FinalPackingtxt.Text) * CostLiter).ToString("0.00");

                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found')", true);

                    }

                }
                else
                {

                    PM_RM_Category_Id = Convert.ToInt32(ShipperCostDropdown.SelectedValue);
                    dt = cls.Get_PMRM_PriceMasterByCategoryAndNameIdShippers(UserId, Convert.ToInt32(lblPackingMaterial_Id.Text), PMName_ID, PM_RM_Category_Id, decimal.Parse(lblPackSize.Text), Convert.ToInt32(lblPAckMaterialMeasurement.Text));
                    if (dt.Rows.Count > 0)
                    {
                        PM_Price_Cost_Unittxt.Text = dt.Rows[0]["Price/Unit"].ToString();
                        lblUnitPerKg.Text = dt.Rows[0]["Pack_Unit_Shipper"].ToString();
                        FinalPackingtxt.Text = (decimal.Parse(PM_Price_Cost_Unittxt.Text) / decimal.Parse(lblUnitPerKg.Text)).ToString("0.00");

                        TotalProductPMCostUnittxt.Text = FinalPackingtxt.Text;
                        if (lblPAckMaterialMeasurement.Text == "1" && lblPackSize.Text == "1.00")
                        {
                            TotalProductPMCostLtrtxt.Text = FinalPackingtxt.Text;

                        }
                        if (lblPAckMaterialMeasurement.Text == "2" && lblPackSize.Text == "1.00")
                        {
                            TotalProductPMCostLtrtxt.Text = FinalPackingtxt.Text;

                        }
                        if ((lblPAckMaterialMeasurement.Text == "2" || lblPAckMaterialMeasurement.Text == "1"))
                        {
                            if (decimal.Parse(lblPackSize.Text) > decimal.Parse("1.00"))
                            {
                                decimal ConvertToLtr = decimal.Parse(lblPackSize.Text) * decimal.Parse("1000") / decimal.Parse("1000");

                                TotalProductPMCostLtrtxt.Text = (decimal.Parse(FinalPackingtxt.Text) / ConvertToLtr).ToString("0.00");
                            }
                        }
                        if ((lblPAckMaterialMeasurement.Text == "6" || lblPAckMaterialMeasurement.Text == "7"))
                        {
                            decimal CostLiter = (decimal.Parse("1000") / decimal.Parse(lblPackSize.Text));
                            TotalProductPMCostLtrtxt.Text = (decimal.Parse(FinalPackingtxt.Text) * CostLiter).ToString("0.00");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Founf')", true);

                    }

                }

            }
            else
            {
                PMRMCategoryDropdown.SelectedIndex = 0;
                PMNameDropdowm.SelectedIndex = 0;
                PM_Price_Cost_Unittxt.Text = "";
                FinalPackingtxt.Text = "";

            }
        }

        protected void ChkIsPckingMaster_CheckedChanged(object sender, EventArgs e)
        {
            //ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            //ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            //DataTable dt = new DataTable();
            //pro.User_Id = UserId;
            //int BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            //dt = cls.Get_SubPackingMaterialMasterByBPM_Id(UserId, BPM_Id);
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (BulkProductDropDownList.SelectedValue == row["Fk_BPM_Id"].ToString())
            //    {
            //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Can not Add Multiple Time!')", true);

            //        bool ismasterPAcking = Convert.ToBoolean(row["IsMasterPacking"]);
            //        if (ismasterPAcking == true)
            //        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(),  "confirmAction()", "Already MasterPacking Assigned. What To Change ?", true);


            if (ChkIsPackingMaster.Checked == true)
            {
                pro.isMasterPacking = 1;
            }
            else
            {
                pro.isMasterPacking = 0;
            }


            //}
            //else
            //{
            //    pro.isMasterPacking = 0;
            //}


            ////return;

        }




        protected void UpdateFinalMaterialMaster_Click(object sender, EventArgs e)
        {
            if (lblFinalPackingMaterialMaster_Id.Text != "")
            {
                pro.FinalPackingMaterialMaster_Id = Convert.ToInt32(lblFinalPackingMaterialMaster_Id.Text);
                pro.User_Id = UserId;
                pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
                pro.Fk_PMRM_Category_Id = Convert.ToInt32(lblPM_RM_Category_Id.Text);
                DataTable dt = cls.Get_FinalPackingMaterialMaster(pro);
                if (dt.Rows.Count > 0)
                {
                    pro.User_Id = UserId;
                    pro.BPM_Id = Convert.ToInt32(dt.Rows[0]["Fk_BPM_Id"].ToString());
                    pro.FinalPackingMaterialMaster_Id = Convert.ToInt32(dt.Rows[0]["FinalPackingMaterialMaster_Id"].ToString());
                    pro.Pack_Material_Id = Convert.ToInt32(dt.Rows[0]["Fk_SubPAckingMaterial_Id"].ToString());
                    pro.PMRM_Category_Id = Convert.ToInt32(dt.Rows[0]["Fk_PM_RM_Category_Id"].ToString());
                    //pro.Fk_PMRM_Id = Convert.ToInt32(dt.Rows[0]["Fk_PMRM_Id"].ToString());
                    //pro.Fk_PMRM_Category_Id= Convert.ToInt32(dt.Rows[0]["PM_RM_Category_id"].ToString()); 
                    //pro.Fk_PMRM_Price_Cost_Unit= decimal.Parse(dt.Rows[0]["PMRM_Price_Cost_Unit"].ToString()); 
                    //pro.Final_Pack_Cost_Unit = decimal.Parse(dt.Rows[0]["Final_Pack_Cost_Unit"].ToString());
                    //string PackSize = dt.Rows[0]["Pack_Size"].ToString();
                    //int MeasureSize =Convert.ToInt32(dt.Rows[0]["Pack_Measurement"]);
                    //decimal ConvertToLtr = decimal.Parse("1000")/ decimal.Parse(PackSize);
             
                        if (lblTotal_Product_PM_Cost_Unit.Text != "")
                    {
                        pro.Total_Product_PM_Cost_Unit = decimal.Parse(lblTotal_Product_PM_Cost_Unit.Text);

                    }
                    else
                    {
                        pro.Total_Product_PM_Cost_Ltr = 0;

                    }
                    pro.Total_Product_PM_Cost_Ltr = Convert.ToDecimal(lblTotal_Product_PM_Cost_Ltr.Text);

                    Status = cls.Update_FinalPackingMaterialMaster(pro);
                    if (Status > 0)
                    {
                        Grid_PackingMaterialCostingMasterData();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                        ClearData();
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed!')", true);

                    }
                    Grid_FinalPakingMaterialMasterData();
                }

            }
        }

        protected void FilterIsMasterPackingDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterIsMasterPackingDropdown.SelectedValue == "0")
            {
                Grid_PackingMaterialMasterData();

            }
            else
            {
                Grid_PackingMaterialMasterDataByMasterPacking();
            }
        }


        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_PM_from_PackingMaterialMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchBPM_Product_Name", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["BPM_Product_Name"].ToString());


                        }
                    }

                    conn.Close();

                    return customers;
                }
            }
        }
        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            Grid_PackingMaterialMasterData();
        }

        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            TxtSearch.Text = new string(TxtSearch.Text.Where(c => Char.IsLetter(c) && Char.IsUpper(c)).ToArray());
            TxtSearch.Text = TxtSearch.Text.Substring(0, 3);
            this.BindGrid();
        }

        protected void SearchId_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Search_PM_from_PackingMaterialMaster", con);
                cmd.Parameters.AddWithValue("@SearchBPM_Product_Name", TxtSearch.Text.Trim());


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

            Grid_PackingMaterialMaster.DataSource = dt;
            Grid_PackingMaterialMaster.DataBind();
        }

        protected void ShipperTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtCheck = new DataTable();
            pro.BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
            pro.Pack_size = decimal.Parse(PackingSizetxt.Text);
            pro.Pack_Measurement = Convert.ToInt32(PackingMeasurementDropdown.SelectedValue);
            pro.Fk_PMRM_Category_Id = Convert.ToInt32(ShipperTypeDropdown.SelectedValue);
            dtCheck = cls.CHECK_SubPackingMaterialMaster(pro);
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple  Packing Material Master !')", true);
                ClearData();
                return;
            }
        }
    }
}

