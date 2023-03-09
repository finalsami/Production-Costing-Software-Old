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
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class ProductwiseLabourCost : System.Web.UI.Page
    {
        int User_Id;
        string Shipper_Id;
        string Get_BPM_Id;
        ClsProductwiseLabourCost cls = new ClsProductwiseLabourCost();

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


                BulkProductDropDownListCombo();
                MeasurementDataCombo();
                PackingStyleCategorycombo();
                //PackingStyleNameComboById();
                GetOtherVariableforms();
                //GetEnumMasterBySource();
                //IngrediantGridviewData();
                GridProductwiseLabourCostMaster();
                //ClearData();
                HideUnitMeasurementDrodown.Visible = false;
                //PackingSizeDropDownListData();
                PackingSizeDropDownList.Enabled = false;
                PackingStyleNameDropdowncombo();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[4]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[4]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[4]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblProductWisrLabourCost_Id.Text != "")
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
                CancelBtn.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[4]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[4]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[4]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblProductWisrLabourCost_Id.Text != "")
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
                CancelBtn.Visible = false;
            }

        }
        public void PackingStyleNameDropdowncombo()
        {
            ClsPackingStyleNameMaster cls = new ClsPackingStyleNameMaster();
            ProPackingStyleNameMaster pro = new ProPackingStyleNameMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            dt = cls.Get_PackingStyleNameAll(User_Id);

            dt.AsEnumerable().Select(a => a.Field<string>("PackingStyleName").ToString()).Distinct(); ;

            PackingStyleNameDropdown.DataSource = dt;
            PackingStyleNameDropdown.DataTextField = "PackingStyleName";
            PackingStyleNameDropdown.DataValueField = "PackingStyleName_Id";
            PackingStyleNameDropdown.DataBind();
            PackingStyleNameDropdown.Items.Insert(0, "Select");
        }
        public void PackingSizeDropDownListData()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;

            dt = cls.Get_SubPackingMaterialMasterByBPM_Id(User_Id, Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
            dt.Columns.Add("PackingSize", typeof(string), "Pack_size + ' ' + PackMeasurement + ''").ToString();

            dt.AsEnumerable().Select(a => a.Field<string>("PackingSize").ToString()).Distinct(); ;

            PackingSizeDropDownList.DataSource = dt;
            PackingSizeDropDownList.DataTextField = "PackingSize";
            PackingSizeDropDownList.DataValueField = "Pack_size";
            PackingSizeDropDownList.DataBind();
            PackingSizeDropDownList.Items.Insert(0, "Select");
        }
        public void PackingSizeDropDownListDataNew()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;

            dt = cls.Get_SubPackingMaterialMasterByBPM_Id(User_Id, Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
            //dt.Columns.Add("PackingSize", typeof(string), "Pack_size + ' ' + PackMeasurement + ''").ToString();

            dt.AsEnumerable().Select(a => a.Field<string>("PackingSize").ToString()).Distinct(); ;

            PackingSizeDropDownList.DataSource = dt;
            PackingSizeDropDownList.DataTextField = "TotalPackSize";
            PackingSizeDropDownList.DataValueField = "Pack_size";
            PackingSizeDropDownList.DataBind();
            PackingSizeDropDownList.Items.Insert(0, "Select");
        }
        public void PackingStyleNameComboById()
        {
            ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
            DataTable dt = new DataTable();

            PackingStyleNameDropdown.DataSource = dt = cls.GetPackingStyleMasterGridAll(User_Id);
            PackingStyleNameDropdown.DataTextField = "PackingStyleName";
            PackingStyleNameDropdown.DataValueField = "Fk_Packing_Style_Name_Id";
            PackingStyleNameDropdown.DataBind();

            PackingStyleNameDropdown.Items.Insert(0, "Select");
        }
        public void GetOtherVariableforms()
        {
            ClsCostVariableMaster cls = new ClsCostVariableMaster();
            ProCostVariableMaster pro = new ProCostVariableMaster();
            DataTable dt = new DataTable();
            dt = cls.Get_OtherVariableForms(User_Id);
            if (dt.Rows.Count > 0)
            {
                pro.Net_Shift_Hour = decimal.Parse(dt.Rows[0]["Net_Shift_Hour"].ToString());

                lblNetShiftHours.Text = pro.Net_Shift_Hour.ToString();
                lblPowercosting.Text = (dt.Rows[0]["Power_Unit_Price"].ToString());
                lblSuperVisorCosting.Text = (dt.Rows[0]["SuperVisorCosting"].ToString());
                lblLabourCharge.Text = (dt.Rows[0]["LabourCharge"].ToString());

                lblUnloadingUnit.Text = (dt.Rows[0]["UnloadingExpense_Unit"].ToString());
                lblUnloadingLtr.Text = (dt.Rows[0]["UnloadingExpense_Ltr"].ToString());
                lblUnloadingKg.Text = (dt.Rows[0]["UnloadingExpense_Kg"].ToString());

                lblLoadingUnit.Text = (dt.Rows[0]["LoadingExpense_Unit"].ToString());
                lblLoadingKg.Text = (dt.Rows[0]["LoadingExpense_Kg"].ToString());
                lblLoadingLtr.Text = (dt.Rows[0]["LoadingExpense_Ltr"].ToString());

                lblMachinaryMaitExpenceLtr.Text = (dt.Rows[0]["MachinaryMaitExpence_Ltr"].ToString());
                lblMachinaryMaitExpenceKg.Text = (dt.Rows[0]["MachinaryMaitExpence_Kg"].ToString());
                lblMachinaryMaitExpenceUnit.Text = (dt.Rows[0]["MachinaryMaitExpence_Unit"].ToString());

                lblAdminExpenceLtr.Text = (dt.Rows[0]["AdminExpence_Ltr"].ToString());
                lblAdminExpenceKg.Text = (dt.Rows[0]["AdminExpence_Kg"].ToString());
                lblAdminExpenceUnit.Text = (dt.Rows[0]["AdminExpence_Unit"].ToString());

                lblExtraExpenceLtr.Text = (dt.Rows[0]["ExtraExpence_Ltr"].ToString());
                lblExtraExpenceKg.Text = (dt.Rows[0]["ExtraExpence_Kg"].ToString());
                lblExtraExpenceUnit.Text = (dt.Rows[0]["ExtraExpence_Unit"].ToString());
            }
        }
        public void MeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeaurementDropdown.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeaurementDropdown.DataTextField = "EnumDescription";
            UnitMeaurementDropdown.DataValueField = "PkEnumId";
            UnitMeaurementDropdown.DataBind();
            UnitMeaurementDropdown.Items.Insert(0, "Select");

        }
        public void PackingStyleCategorycombo()
        {
            ClsPackingStyleCategoryMaster cls = new ClsPackingStyleCategoryMaster();
            ProPackingStyleCategoryMaster pro = new ProPackingStyleCategoryMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            dt = cls.Get_PackingStyleCategoryDropdown(pro);

            dt.Columns.Add("Packing_Style", typeof(string), "PackingStyleCategoryName + ' (' + PackingSize +'- '+PackingMeasurement+')'").ToString();
            //DataView dvOptions = new DataView(dt);
            //dvOptions.Sort = "PackingStyleCategoryName";

            PackingStyleCategoryDropdown.DataSource = dt;
            PackingStyleCategoryDropdown.DataTextField = "Packing_Style";

            //PackingStyleCategoryDropdown.DataTextField = "PackingStyleCategoryName";
            PackingStyleCategoryDropdown.DataValueField = "PackingStyleCategory_Id";
            PackingStyleCategoryDropdown.DataBind();

            PackingStyleCategoryDropdown.Items.Insert(0, "Select");
        }
        public void PackingStyleCategorycomboOld()
        {
            ClsPackingStyleCategoryMaster cls = new ClsPackingStyleCategoryMaster();

            DataTable dt = new DataTable();

            dt = cls.Get_PackingStyleCategoryName(User_Id);


            dt.Columns.Add("Packing_Style", typeof(string), "PackingStyleCategoryName + ' ( ' + PackingSize +'- '+PackingMeasurement+ ')'").ToString();
            PackingStyleCategoryDropdown.DataSource = dt;
            PackingStyleCategoryDropdown.DataTextField = "Packing_Style";

            //PackingStyleCategoryDropdown.DataTextField = "PackingStyleCategoryName";
            PackingStyleCategoryDropdown.DataValueField = "PackingStyleCategoryName_Id";
            PackingStyleCategoryDropdown.DataBind();

            PackingStyleCategoryDropdown.Items.Insert(0, "Select");
        }
        public void GridProductwiseLabourCostMaster()
        {
            ClsProductwiseLabourCost cls = new ClsProductwiseLabourCost();
            Grid_ProductwiseLabourCost.DataSource = cls.Get_ProductwiseLabourCost(User_Id);
            Grid_ProductwiseLabourCost.DataBind();
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

                User_Id = Convert.ToInt32(Session["UserId"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void BulkProductDropDownListCombo()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            dt = cls.Get_BulkProductMasterFromPackingMaterialMaster();

            dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + ShipperType_Id +')'").ToString();

            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BulkProductName";
            BulkProductDropDownList.DataSource = dvOptions;
            BulkProductDropDownList.DataTextField = "BulkProductName";

            BulkProductDropDownList.DataValueField = "BPMValue";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }
        public void BulkProductDropDownListComboNew()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            ProPackingMaterialMaster pro = new ProPackingMaterialMaster();
            DataTable dt = new DataTable();
            pro.User_Id = User_Id;
            dt = cls.Get_BulkProductMasterFromPackingMaterialMaster();

            dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + ShipperType_Id +')'").ToString();

            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BulkProductName";
            BulkProductDropDownList.DataSource = dvOptions;
            BulkProductDropDownList.DataTextField = "BulkProductName";

            BulkProductDropDownList.DataValueField = "BPMValue";
            BulkProductDropDownList.DataBind();
        }
        string Minuts = "60";
        string KGLiter = "1";
        string Militer = "1000";
        String TotaloutputBottelsinNetShiftHour = "";
        protected void StorckPerNoseltxt_TextChanged(object sender, EventArgs e)
        {
            if (StorckPerNoseltxt.Text != "" && StorckPerNoseltxt.Text != "0" && NoofNoselsFillingLinetxt.Text != "" && NoofNoselsFillingLinetxt.Text != "0")
            {
                TotalOutputMinutFillingLinetxt.Text = (decimal.Parse(StorckPerNoseltxt.Text) * decimal.Parse(NoofNoselsFillingLinetxt.Text)).ToString("0.00");
                TotaloutputBottelsinNetShiftHour = (decimal.Parse(TotalOutputMinutFillingLinetxt.Text) * decimal.Parse(Minuts)).ToString();
                TotaloutputBottelsinNetShiftHourstxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHour) * decimal.Parse(lblNetShiftHours.Text)).ToString("0.00");
                if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(Militer))).ToString("0.00");



                    Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); 
                    Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); 
                    MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                    AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                    ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


                }
                if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                {
                    TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(KGLiter))).ToString();

                    Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); 
                    Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00");
                    MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                    AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                    ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


                }
            }
            else
            {
                if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(Militer))).ToString("0.00");



                    Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); ;
                    Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); ;
                    MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                    AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                    ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


                }
                if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                {
                    TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(KGLiter))).ToString("0.00");

                    Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); 
                    Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00");
                    MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                    AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                    ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


                }
            }
        }

        protected void NoofNoselsFillingLinetxt_TextChanged(object sender, EventArgs e)
        {
            if (StorckPerNoseltxt.Text != "" && StorckPerNoseltxt.Text != "0" && NoofNoselsFillingLinetxt.Text != "" && NoofNoselsFillingLinetxt.Text != "0")
            {
                TotalOutputMinutFillingLinetxt.Text = (decimal.Parse(StorckPerNoseltxt.Text) * decimal.Parse(NoofNoselsFillingLinetxt.Text)).ToString("0.00");
                TotaloutputBottelsinNetShiftHour = (decimal.Parse(TotalOutputMinutFillingLinetxt.Text) * decimal.Parse(Minuts)).ToString();
                TotaloutputBottelsinNetShiftHourstxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHour) * decimal.Parse(lblNetShiftHours.Text)).ToString("0.00");
                if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(Militer))).ToString("0.00");
                    Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00");
                    Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00");
                    MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                    AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                    ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");

                    TotalLabourSupervisiorCoastinginRStxt.Text = ((decimal.Parse(lblLabourCharge.Text) * decimal.Parse(NOofWorkerstxt.Text)) + (decimal.Parse(lblSuperVisorCosting.Text) * decimal.Parse(Supervisortxt.Text))).ToString("0.00");



                    TotalCostingtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) + decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text) + decimal.Parse(Unloadingtxt.Text) + decimal.Parse(Loadingtxt.Text)
                                                         + decimal.Parse(MachinaryMaintain.Text) + decimal.Parse(AdminExpencetxt.Text) + decimal.Parse(ExtraExpencetxt.Text)).ToString("0.00");

                    TotalOutputinLitertxt.Text = TotalOutPutInLiterOrKGShifttxt.Text;
                    FinalPerLiterLabourCostingtxt.Text = (decimal.Parse(TotalCostingtxt.Text) / decimal.Parse(TotalOutputinLitertxt.Text)).ToString("0.00");


                    AdditionalCostBuffer();


                    NetLabourCostLtrtxt.Text = (decimal.Parse(FinalPerLiterLabourCostingtxt.Text) + decimal.Parse(AdditionalCostBuffertxt.Text)).ToString("0.00");
                    if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                    {
                        decimal ConvertMilToLtr = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
                        FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                    }
                    // Code added by Harshul Patel on 06-05-20222 for calcuation for more than 1ltr or 1 kg calculation

                    else if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                    {
                        decimal ConvertMilToLtr = ((decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text) * 1000));
                        FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                    }
                    //End
                    else
                    {
                        FinalPerUnitLabourCosttxt.Text = NetLabourCostLtrtxt.Text;

                    }


                }
                if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                {
                    TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(KGLiter))).ToString("0.00");

                    Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); 
                    Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); 
                    MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                    //MachinaryMtxtaintatnce.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString();
                    AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                    ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");

                    TotalLabourSupervisiorCoastinginRStxt.Text = ((decimal.Parse(lblLabourCharge.Text) * decimal.Parse(NOofWorkerstxt.Text)) + (decimal.Parse(lblSuperVisorCosting.Text) * decimal.Parse(Supervisortxt.Text))).ToString("0.00");



                    TotalCostingtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) + decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text) + decimal.Parse(Unloadingtxt.Text) + decimal.Parse(Loadingtxt.Text)
                                                         + decimal.Parse(MachinaryMaintain.Text) + decimal.Parse(AdminExpencetxt.Text) + decimal.Parse(ExtraExpencetxt.Text)).ToString("0.00");

                    TotalOutputinLitertxt.Text = TotalOutPutInLiterOrKGShifttxt.Text;
                    FinalPerLiterLabourCostingtxt.Text = (decimal.Parse(TotalCostingtxt.Text) / decimal.Parse(TotalOutputinLitertxt.Text)).ToString("0.00");

                    AdditionalCostBuffer();

                    NetLabourCostLtrtxt.Text = (decimal.Parse(FinalPerLiterLabourCostingtxt.Text) + decimal.Parse(AdditionalCostBuffertxt.Text)).ToString("0.00");
                    if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                    {
                        decimal ConvertMilToLtr = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
                        FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                    }
                    // Code added by Harshul Patel on 06-05-20222 for calcuation for more than 1ltr or 1 kg calculation

                    else if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                    {
                        decimal ConvertMilToLtr = ((decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text) * 1000));
                        FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                    }
                    //End
                    else
                    {
                        FinalPerUnitLabourCosttxt.Text = NetLabourCostLtrtxt.Text;

                    }
                }
            }
        }
        private void AdditionalCostBuffer()
        {
            if (String.IsNullOrEmpty(AdditionalCostBuffertxt.Text.Trim()))
            {
                AdditionalCostBuffertxt.Text = "0.00";
            }
        }
        protected void PackingStyleCategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PackingStyleCategoryDropdown.SelectedValue != "Select")
            {
                ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
                int PackingStyleMasterByCategory_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue);
                lblPackingStyleCategory_Id.Text = PackingStyleMasterByCategory_Id.ToString();
                DataTable dt = new DataTable();


                dt = cls.GetPackingStyleMasterByCategoryId(PackingStyleMasterByCategory_Id, User_Id, decimal.Parse(lblPackSize.Text), Convert.ToInt32(lblPackMeasurement.Text));
                //dt.AsEnumerable().Select(a => a.Field<string>("PackingStyleName").ToString()).Distinct();
                PackingStyleNameDropdown.Enabled = true;
                PackingStyleNameDropdown.DataSource = dt;
                PackingStyleNameDropdown.DataTextField = "PackingStyleName";
                PackingStyleNameDropdown.DataValueField = "PackingStyleName_Id";
                PackingStyleNameDropdown.DataBind();
                PackingStyleNameDropdown.Items.Insert(0, "Select");
            }
            else
            {
                PackingStyleNameDropdown.Enabled = false;
                PackingStyleNameDropdown.SelectedIndex = 0;
            }


        }

        protected void NOofWorkerstxt_TextChanged(object sender, EventArgs e)
        {
            if (NOofWorkerstxt.Text != "" && NOofWorkerstxt.Text != "0" && Supervisortxt.Text != "" && Supervisortxt.Text != "0")
            {
                TotalLabourSupervisiorCoastinginRStxt.Text = ((decimal.Parse(lblLabourCharge.Text) * decimal.Parse(NOofWorkerstxt.Text)) + (decimal.Parse(lblSuperVisorCosting.Text) * decimal.Parse(Supervisortxt.Text))).ToString();
                TotalCostingtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) + decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text) + decimal.Parse(Unloadingtxt.Text) + decimal.Parse(Loadingtxt.Text)
                                                     + decimal.Parse(MachinaryMaintain.Text) + decimal.Parse(AdminExpencetxt.Text) + decimal.Parse(ExtraExpencetxt.Text)).ToString();

                TotalOutputinLitertxt.Text = TotalOutPutInLiterOrKGShifttxt.Text;
                FinalPerLiterLabourCostingtxt.Text = (decimal.Parse(TotalCostingtxt.Text) / decimal.Parse(TotalOutputinLitertxt.Text)).ToString("0.00");
            }
        }

        protected void Supervisortxt_TextChanged(object sender, EventArgs e)
        {
            if (NOofWorkerstxt.Text != "" && NOofWorkerstxt.Text != "0" && Supervisortxt.Text != "" && Supervisortxt.Text != "0")
            {
                TotalLabourSupervisiorCoastinginRStxt.Text = ((decimal.Parse(lblLabourCharge.Text) * decimal.Parse(NOofWorkerstxt.Text)) + (decimal.Parse(lblSuperVisorCosting.Text) * decimal.Parse(Supervisortxt.Text))).ToString();



                TotalCostingtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) + decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text) + decimal.Parse(Unloadingtxt.Text) + decimal.Parse(Loadingtxt.Text)
                                                     + decimal.Parse(MachinaryMaintain.Text) + decimal.Parse(AdminExpencetxt.Text) + decimal.Parse(ExtraExpencetxt.Text)).ToString();

                TotalOutputinLitertxt.Text = TotalOutPutInLiterOrKGShifttxt.Text;
                FinalPerLiterLabourCostingtxt.Text = (decimal.Parse(TotalCostingtxt.Text) / decimal.Parse(TotalOutputinLitertxt.Text)).ToString("0.00");

                AdditionalCostBuffer();

                  NetLabourCostLtrtxt.Text = (decimal.Parse(FinalPerLiterLabourCostingtxt.Text) + decimal.Parse(AdditionalCostBuffertxt.Text)).ToString("0.00");
                    if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                    {
                        decimal ConvertMilToLtr = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
                        FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                    }
                    // Code added by Harshul Patel on 06-05-20222 for calcuation for more than 1ltr or 1 kg calculation

                    else if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                    {
                        decimal ConvertMilToLtr = ((decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text) * 1000));
                        FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                    }
                    //End
                    else
                    {
                        FinalPerUnitLabourCosttxt.Text = NetLabourCostLtrtxt.Text;

                    }
                
            }
        }

        protected void AdditionalCostBuffertxt_TextChanged(object sender, EventArgs e)
        {
                AdditionalCostBuffer();

             NetLabourCostLtrtxt.Text = (decimal.Parse(FinalPerLiterLabourCostingtxt.Text) + decimal.Parse(AdditionalCostBuffertxt.Text)).ToString("0.00");
                if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                {
                    decimal ConvertMilToLtr = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
                    FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                }

                // Code added by Harshul Patel on 06-05-20222 for calcuation for more than 1ltr or 1 kg calculation

                else if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                {
                    decimal ConvertMilToLtr = ((decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text) * 1000));
                    FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                }
                //End
                else
                {
                    FinalPerUnitLabourCosttxt.Text = NetLabourCostLtrtxt.Text;

                }            
        }

        protected void Addbtn_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = cls.Get_ProductwiseLabourCost(User_Id);

            string BPM_Full_Id = BulkProductDropDownList.SelectedValue.ToString();
            string Get_BPM_Id = Regex.Match(BPM_Full_Id, @"\d+").Value;
            lblPMRM_Category_Id.Text = BPM_Full_Id.Split('(', ')')[1];
            foreach (DataRow row in dt.Rows)
            {
                if (Get_BPM_Id == row["Fk_BPM_Id"].ToString() && PackingSizeDropDownList.SelectedValue == row["Packing_Size"].ToString() && UnitMeaurementDropdown.SelectedValue == row["Fk_UnitMeasurement_Id"].ToString() && lblPMRM_Category_Id.Text == row["Fk_PM_RM_Category_Id"].ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product And Pack Size Can not Add Multiple Time!')", true);
                    return;
                }
            }


            if (PackingSizeDropDownList.Enabled == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record  Not Successfully Inserted !')", true);

            }
            else
            {
                ProProductWiseLabourCostMaster pro = new ProProductWiseLabourCostMaster();
                pro.Fk_BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
                pro.Packing_Size = decimal.Parse(lblPackSize.Text);
                pro.UnitMeasurement_Id = Convert.ToInt32(lblPackMeasurement.Text);
                pro.Packing_Description = PackingDescriptiontxt.Text;
                pro.PackingStyleCategory_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue); ;
                pro.PackingStyleName_Id = Convert.ToInt32(PackingStyleNameDropdown.SelectedValue); ;
                pro.Storck_Nosel = decimal.Parse(StorckPerNoseltxt.Text);
                pro.NoOfNoselsFillingLine = decimal.Parse(NoofNoselsFillingLinetxt.Text);
                pro.TotalOutputMinutFillingLine = decimal.Parse(TotalOutputMinutFillingLinetxt.Text);
                pro.TotalOutputBottelsInNetShiftHours = decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text);
                pro.TotalOutPutInLiterOrKGShift = decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text);
                pro.NOofWorkers = Convert.ToInt32(NOofWorkerstxt.Text);
                pro.Supervisior = Convert.ToInt32(Supervisortxt.Text);
                pro.TotalLabourAndSupervisiorCoastinginRS = decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text);
                pro.Unloading = decimal.Parse(Unloadingtxt.Text);
                pro.Loading = decimal.Parse(Loadingtxt.Text);
                pro.MachinaryMaintain = decimal.Parse(MachinaryMaintain.Text);
                pro.AdminExpence = decimal.Parse(AdminExpencetxt.Text);
                pro.ExtraExpence = decimal.Parse(ExtraExpencetxt.Text);
                pro.TotalCosting = decimal.Parse(TotalCostingtxt.Text);
                pro.TotalOutputinLiter = decimal.Parse(TotalOutputinLitertxt.Text);
                pro.FinalPerLiterLabourCosting = decimal.Parse(FinalPerLiterLabourCostingtxt.Text);
                pro.AdditionCostBuffer = decimal.Parse(AdditionalCostBuffertxt.Text);
                pro.NetLabourCostLtr = decimal.Parse(NetLabourCostLtrtxt.Text);
                pro.FinalPerUnitLabourCost = decimal.Parse(FinalPerUnitLabourCosttxt.Text);
                pro.User_Id = User_Id;

                pro.TotalPowerCosting = decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text);
                pro.PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);

                int status = cls.Insert_ProductwiseLabourCost(pro);

                if (status > 0)
                {
                    GridProductwiseLabourCostMaster();
                    ClearData();
                    Updatebtn.Visible = false;
                    Addbtn.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted  Successfully')", true);

                }
            }

        }
        public void ClearData()
        {
            BulkProductDropDownList.SelectedIndex = 0;
            lblPackSize.Text = "";
            UnitMeaurementDropdown.SelectedIndex = 0;
            PackingDescriptiontxt.Text = "";
            PackingStyleCategoryDropdown.SelectedIndex = 0;
            PackingStyleNameDropdown.SelectedIndex = 0;
            StorckPerNoseltxt.Text = "0";
            NoofNoselsFillingLinetxt.Text = "0";
            TotalOutputMinutFillingLinetxt.Text = "0";
            TotaloutputBottelsinNetShiftHourstxt.Text = "0";
            TotalOutPutInLiterOrKGShifttxt.Text = "0";
            NOofWorkerstxt.Text = "0";
            Supervisortxt.Text = "0";
            TotalLabourSupervisiorCoastinginRStxt.Text = "0";
            Unloadingtxt.Text = "0";
            Loadingtxt.Text = "0";
            MachinaryMaintain.Text = "0";
            AdminExpencetxt.Text = "0";
            ExtraExpencetxt.Text = "0";
            TotalCostingtxt.Text = "0";
            TotalOutputinLitertxt.Text = "0";
            FinalPerLiterLabourCostingtxt.Text = "0";
            AdditionalCostBuffertxt.Text = "0";
            NetLabourCostLtrtxt.Text = "0";
            FinalPerUnitLabourCosttxt.Text = "0";
            //PackingSizeDropDownList.SelectedIndex = 0;
            //PackingStyleCategoryDropdown.SelectedIndex = 0;
            TotalpackingStylePowerUnitsXUnitCosttxt.Text = "0";
            PackingSizeDropDownList.Enabled = false;
            TotalShiftPerHourtxt.Text = "0";
            Updatebtn.Visible = false;
            Addbtn.Visible = true;
            lblProductWisrLabourCost_Id.Text = "";
        }
        protected void EditProductBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int ProductwiseLaboutcost_Id = Convert.ToInt32(Grid_ProductwiseLabourCost.DataKeys[gdrow.RowIndex].Value.ToString());
            lblProductWisrLabourCost_Id.Text = Convert.ToString(ProductwiseLaboutcost_Id);
            PackingStyleNameComboById();
            DataTable dtCost = new DataTable();

            dtCost = cls.Get_ProductwiseLabourCosById(User_Id, ProductwiseLaboutcost_Id);
            if (dtCost.Rows.Count > 0)
            {
                PackingStyleCategorycombo();


                //BulkProductDropDownListComboNew();
                lblPackSize.Text = dtCost.Rows[0]["Packing_Size"].ToString();
                UnitMeaurementDropdown.SelectedValue = Convert.ToInt32(dtCost.Rows[0]["Fk_UnitMeasurement_Id"]).ToString();


                string BulkIdAndCate = dtCost.Rows[0]["Fk_BPM_Id"].ToString() + " (" + dtCost.Rows[0]["Fk_PM_RM_Category_Id"].ToString() + ")";
                //BulkProductDropDownList.SelectedItem.Text = dt.Rows[0]["BPM_Product_Name"].ToString();

                BulkProductDropDownList.SelectedValue = dtCost.Rows[0]["Fk_BPM_Id"].ToString() + " (" + dtCost.Rows[0]["Fk_PM_RM_Category_Id"].ToString() + ")";
                //BulkProductDropDownList.SelectedItem.Text = dt.Rows[0]["BPM_Product_Name"].ToString();

                //UnitMeaurementDropdown.SelectedItem.Text = dt.Rows[0]["Measurement"].ToString();
                PackingDescriptiontxt.Text = dtCost.Rows[0]["Packing_Description"].ToString();
                PackingStyleCategoryDropdown.SelectedValue = dtCost.Rows[0]["Fk_PackingStyleCategory_Id"].ToString();
                PackingStyleNameDropdown.SelectedValue = dtCost.Rows[0]["Fk_PackingStyleName_Id"].ToString();
                StorckPerNoseltxt.Text = dtCost.Rows[0]["Storck_Nosel"].ToString();
                NoofNoselsFillingLinetxt.Text = dtCost.Rows[0]["NoOfNoselsFillingLine"].ToString();
                TotalOutputMinutFillingLinetxt.Text = dtCost.Rows[0]["TotalOutputMinutFillingLine"].ToString();
                TotaloutputBottelsinNetShiftHourstxt.Text = dtCost.Rows[0]["TotalOutputBottelsInNetShiftHours"].ToString();
                TotalOutPutInLiterOrKGShifttxt.Text = dtCost.Rows[0]["TotalOutPutInLiterOrKGShift"].ToString();
                NOofWorkerstxt.Text = dtCost.Rows[0]["NOofWorkers"].ToString();
                Supervisortxt.Text = dtCost.Rows[0]["Supervisior"].ToString();
                TotalLabourSupervisiorCoastinginRStxt.Text = dtCost.Rows[0]["TotalLabourAndSupervisiorCoastinginRS"].ToString();
                TotalpackingStylePowerUnitsXUnitCosttxt.Text = dtCost.Rows[0]["TotalPowerCosting"].ToString();
                Unloadingtxt.Text = dtCost.Rows[0]["Unloading"].ToString();
                Loadingtxt.Text = dtCost.Rows[0]["Loading"].ToString();
                MachinaryMaintain.Text = dtCost.Rows[0]["MachinaryMaintain"].ToString();
                AdminExpencetxt.Text = dtCost.Rows[0]["AdminExpence"].ToString();
                ExtraExpencetxt.Text = dtCost.Rows[0]["ExtraExpence"].ToString();
                TotalCostingtxt.Text = dtCost.Rows[0]["TotalCosting"].ToString();
                TotalOutputinLitertxt.Text = dtCost.Rows[0]["TotalOutputinLiter"].ToString();
                FinalPerLiterLabourCostingtxt.Text = dtCost.Rows[0]["FinalPerLiterLabourCosting"].ToString();
                AdditionalCostBuffertxt.Text = dtCost.Rows[0]["AdditionalCostBuffer"].ToString();
                NetLabourCostLtrtxt.Text = dtCost.Rows[0]["NetLabourCostLtr"].ToString();
                FinalPerUnitLabourCosttxt.Text = dtCost.Rows[0]["FinalPerUnitLabourCost"].ToString();

                TotalShiftPerHourtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) * decimal.Parse(lblNetShiftHours.Text)).ToString("0.00");


                lbl_BPM_Id.Text = BulkProductDropDownList.SelectedValue.ToString();
                lblPMRM_Category_Id.Text = lbl_BPM_Id.Text.Split('(', ')')[1];
                Get_BPM_Id = Regex.Match(lbl_BPM_Id.Text, @"\d+").Value;
                //Get_BPM_Id = lbl_BPM_Id.Text.Substring(0, 3).Trim();
                lbl_BPM_Id.Text = Get_BPM_Id;
                PackingSizeDropDownListDataNew();
                lblPackMeasurement.Text = (UnitMeaurementDropdown.SelectedValue).ToLower();
                PackingSizeDropDownList.SelectedValue = dtCost.Rows[0]["Packing_Size"].ToString();
                if (dtCost.Rows.Count > 0)
                {
                    BulkProductDropDownList.Enabled = false;

                    UnitMeaurementDropdown.Enabled = false;

                    PackingStyleCategoryDropdown.Enabled = true;
                    PackingStyleNameDropdown.Enabled = true;

                    Updatebtn.Visible = true;
                    Addbtn.Visible = false;
                    CancelBtn.Visible = true;
                }
                else
                {
                    BulkProductDropDownList.Enabled = true;

                    UnitMeaurementDropdown.Enabled = true;

                    PackingStyleCategoryDropdown.Enabled = true;
                    PackingStyleNameDropdown.Enabled = true;
                    Updatebtn.Visible = false;
                    Addbtn.Visible = true;
                    CancelBtn.Visible = true;

                }
            }

        }

        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            if (PackingSizeDropDownList.SelectedValue == "Select")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record  Not Successfully Inserted !')", true);

            }
            else
            {
                ProProductWiseLabourCostMaster pro = new ProProductWiseLabourCostMaster();
                pro.ProductWiseLabourCost_Id = Convert.ToInt32(lblProductWisrLabourCost_Id.Text);
                pro.Fk_BPM_Id = Convert.ToInt32(lbl_BPM_Id.Text);
                pro.Packing_Size = decimal.Parse(lblPackSize.Text);
                pro.UnitMeasurement_Id = Convert.ToInt32(lblPackMeasurement.Text);
                pro.Packing_Description = PackingDescriptiontxt.Text;
                pro.PackingStyleCategory_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue); ;
                pro.PackingStyleName_Id = Convert.ToInt32(PackingStyleNameDropdown.SelectedValue); ;
                pro.Storck_Nosel = decimal.Parse(StorckPerNoseltxt.Text);
                pro.NoOfNoselsFillingLine = decimal.Parse(NoofNoselsFillingLinetxt.Text);
                pro.TotalOutputMinutFillingLine = decimal.Parse(TotalOutputMinutFillingLinetxt.Text);
                pro.TotalOutputBottelsInNetShiftHours = decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text);
                pro.TotalOutPutInLiterOrKGShift = decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text);
                pro.NOofWorkers = Convert.ToInt32(NOofWorkerstxt.Text);
                pro.Supervisior = Convert.ToInt32(Supervisortxt.Text);
                pro.TotalLabourAndSupervisiorCoastinginRS = decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text);
                pro.Unloading = decimal.Parse(Unloadingtxt.Text);
                pro.Loading = decimal.Parse(Loadingtxt.Text);
                pro.MachinaryMaintain = decimal.Parse(MachinaryMaintain.Text);
                pro.AdminExpence = decimal.Parse(AdminExpencetxt.Text);
                pro.ExtraExpence = decimal.Parse(ExtraExpencetxt.Text);
                pro.TotalCosting = decimal.Parse(TotalCostingtxt.Text);
                pro.TotalOutputinLiter = decimal.Parse(TotalOutputinLitertxt.Text);
                pro.FinalPerLiterLabourCosting = decimal.Parse(FinalPerLiterLabourCostingtxt.Text);
                pro.AdditionCostBuffer = decimal.Parse(AdditionalCostBuffertxt.Text);
                pro.NetLabourCostLtr = decimal.Parse(NetLabourCostLtrtxt.Text);
                pro.FinalPerUnitLabourCost = decimal.Parse(FinalPerUnitLabourCosttxt.Text);
                pro.User_Id = User_Id;

                pro.TotalPowerCosting = decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text);
                pro.PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);

                int status = cls.Update_ProductwiseLabourCost(pro);

                if (status > 0)
                {

                    ClearData();
                    BulkProductDropDownList.Enabled = true;

                    UnitMeaurementDropdown.Enabled = true;

                    PackingStyleCategoryDropdown.Enabled = true;
                    PackingStyleNameDropdown.Enabled = true;

                    Updatebtn.Visible = false;
                    Addbtn.Visible = true;
                    CancelBtn.Visible = true;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);
                    GridProductwiseLabourCostMaster();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Not Successfully')", true);

                }
            }
        }

        protected void BulkProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (BulkProductDropDownList.SelectedValue != "Select")
            {

                string Shippertype_Id = BulkProductDropDownList.SelectedValue.ToString();

                Shipper_Id = Shippertype_Id.Split('(', ')')[1];
                lblPMRM_Category_Id.Text = Shipper_Id;
                string Get_BPM_Id =
                Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
                lbl_BPM_Id.Text = Get_BPM_Id;

                //int BPM_Id = Convert.ToInt32(BulkProductDropDownList.SelectedValue);
                //String Get_BPM_Name = BulkProductDropDownList.SelectedItem.Text;
                //string PM_RM_Category_Name;
                //try
                //{
                //    PM_RM_Category_Name = (Get_BPM_Name.Split('(', ')')[1]).Trim();

                //}
                //catch (Exception)
                //{

                //    throw;
                //}
                //DataTable dt = new DataTable();
                //ClsPM_RM_Category clsPMRMCat = new ClsPM_RM_Category();
                //dt = clsPMRMCat.Get_PM_RM_CategoryByName(Shipper_Id);
                //lblPMRM_Category_Id.Text = dt.Rows[0]["PM_RM_Category_id"].ToString();

                lbl_BPM_Id.Text = Get_BPM_Id;
                DataTable dtPacking = new DataTable();
                ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
                PackingSizeDropDownList.Enabled = true;

                dtPacking = cls.Get_SubPackingMaterialMasterByBPM_Id(User_Id, Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));
                if (dtPacking.Rows.Count > 0)
                {
                    lblPackSize.Text = dtPacking.Rows[0]["Pack_size"].ToString();
                    lblPackMeasurement.Text = dtPacking.Rows[0]["Pack_Measurement"].ToString();
                    //dtPacking.AsEnumerable().Select(a => a.Field<string>("Brand").ToString());
                    PackingSizeDropDownList.DataSource = dtPacking;

                    PackingSizeDropDownList.DataTextField = "TotalPackSize";
                    PackingSizeDropDownList.DataValueField = "Pack_size";
                    PackingSizeDropDownList.DataBind();
                    PackingSizeDropDownList.Items.Insert(0, "Select");
                    PackingSizeDropDownList.Enabled = true;
                }


            }
            else
            {
                PackingSizeDropDownList.Enabled = false;
            }


        }

        protected void PackingSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dtPacking = new DataTable();
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            decimal Pack_size = decimal.Parse(PackingSizeDropDownList.SelectedValue);
            dtPacking = cls.Get_PackingStyleCategoryByPackingSize(Pack_size, Convert.ToInt32(lbl_BPM_Id.Text), Convert.ToInt32(lblPMRM_Category_Id.Text));

            dtPacking.Columns.Add("Packing_Style", typeof(string), "PackingStyleCategoryName + ' ( ' + Pack_size + '- '+PackMeasurement+ ')'").ToString();
            PackingStyleCategoryDropdown.DataSource = dtPacking;
            PackingStyleCategoryDropdown.DataTextField = "Packing_Style";

            //PackingStyleCategoryDropdown.DataTextField = "PackingStyleCategoryName";
            PackingStyleCategoryDropdown.DataValueField = "PackingStyleCategory_Id";
            PackingStyleCategoryDropdown.DataBind();

            PackingStyleCategoryDropdown.Items.Insert(0, "Select");



            if (dtPacking.Rows.Count > 0)
            {
                lblPackSize.Text = dtPacking.Rows[0]["Pack_size"].ToString();
                lblPackMeasurement.Text = dtPacking.Rows[0]["Pack_Measurement_Id"].ToString();

                UnitMeaurementDropdown.SelectedValue = dtPacking.Rows[0]["Pack_Measurement_Id"].ToString();
            }


            if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
            {
                TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(Militer))).ToString("0.00");



                Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); 
                Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); 
                MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


            }
            if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
            {
                TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(KGLiter))).ToString("0.00");

                Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); ;
                Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); ;
                MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


            }

        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            //BulkProductDropDownList.SelectedIndex = 0;
            BulkProductDropDownList.SelectedIndex = 0;
            lblPackSize.Text = "";
            UnitMeaurementDropdown.SelectedIndex = 0;
            PackingDescriptiontxt.Text = "";
            PackingStyleCategoryDropdown.SelectedIndex = 0;
            PackingStyleNameDropdown.SelectedIndex = -1;
            StorckPerNoseltxt.Text = "0";
            NoofNoselsFillingLinetxt.Text = "0";
            TotalOutputMinutFillingLinetxt.Text = "0";
            TotaloutputBottelsinNetShiftHourstxt.Text = "0";
            TotalOutPutInLiterOrKGShifttxt.Text = "0";
            NOofWorkerstxt.Text = "0";
            Supervisortxt.Text = "0";
            TotalLabourSupervisiorCoastinginRStxt.Text = "0";
            Unloadingtxt.Text = "0";
            Loadingtxt.Text = "0";
            MachinaryMaintain.Text = "0";
            AdminExpencetxt.Text = "0";
            ExtraExpencetxt.Text = "0";
            TotalCostingtxt.Text = "0";
            TotalOutputinLitertxt.Text = "0";
            FinalPerLiterLabourCostingtxt.Text = "0";
            AdditionalCostBuffertxt.Text = "0";
            NetLabourCostLtrtxt.Text = "0";
            FinalPerUnitLabourCosttxt.Text = "0";
            lblProductWisrLabourCost_Id.Text = "";
            lblPackSize.Text = "";
            PackingSizeDropDownList.SelectedIndex = 0;
            //PackingSizeDropDownList.SelectedValue = "0";
            //PackingStyleCategoryDropdown.SelectedIndex = 0;
            TotalpackingStylePowerUnitsXUnitCosttxt.Text = "0";
            BulkProductDropDownList.Enabled = true;
            PackingSizeDropDownList.Enabled = false;
            Updatebtn.Visible = false;
            Addbtn.Visible = true;
            TotalShiftPerHourtxt.Text = "0";

        }

        protected void UnitMeaurementDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
            {
                TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(Militer))).ToString("0.00");



                Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); ;
                Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); ;
                MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


            }
            if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
            {
                TotalOutPutInLiterOrKGShifttxt.Text = (decimal.Parse(TotaloutputBottelsinNetShiftHourstxt.Text) * (decimal.Parse(lblPackSize.Text) / decimal.Parse(KGLiter))).ToString("0.00");

                Unloadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblUnloadingLtr.Text)).ToString("0.00"); ;
                Loadingtxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblLoadingLtr.Text)).ToString("0.00"); ;
                MachinaryMaintain.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblMachinaryMaitExpenceLtr.Text)).ToString("0.00");
                AdminExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblAdminExpenceLtr.Text)).ToString("0.00");
                ExtraExpencetxt.Text = (decimal.Parse(TotalOutPutInLiterOrKGShifttxt.Text) * decimal.Parse(lblExtraExpenceLtr.Text)).ToString("0.00");


            }
        }

        protected void PackingStyleNameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PackingStyleNameDropdown.SelectedValue != "Select")
            {
                ProProductWiseLabourCostMaster pro = new ProProductWiseLabourCostMaster();
                ClsProductwiseLabourCost clspwlc = new ClsProductwiseLabourCost();
                pro.PackingStyleCategory_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue);
                pro.PackingStyleName_Id = Convert.ToInt32(PackingStyleNameDropdown.SelectedValue);
                pro.Packing_Size = Convert.ToInt32(PackingSizeDropDownList.SelectedValue);
                pro.UnitMeasurement_Id = Convert.ToInt32(UnitMeaurementDropdown.SelectedValue);
                DataTable dtCheck = new DataTable();
                dtCheck = clspwlc.CHECK_ProductwiseLabourCost(pro);
                if (dtCheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple  Style ,Category,Size And Name !')", true);
                    ClearData();
                    return;
                }


                DataTable dtPackingStyleName = new DataTable();
                ClsPackingStyleNameMaster cls = new ClsPackingStyleNameMaster();
                ProProductWiseLabourCostMaster propwlc = new ProProductWiseLabourCostMaster();
                int PackingStyleName_Id = Convert.ToInt32(PackingStyleNameDropdown.SelectedValue);
                pro.PackingStyleCategory_Id = Convert.ToInt32(lblPackingStyleCategory_Id.Text);
                pro.Packing_Size = decimal.Parse(lblPackSize.Text);
                pro.UnitMeasurement_Id = Convert.ToInt32(lblPackMeasurement.Text);
                pro.PackingStyleName_Id = PackingStyleName_Id;
                dtPackingStyleName = cls.Get_PackingStyleMasterByPackingStyleNameId(pro);
                if (dtPackingStyleName.Rows.Count > 0)
                {
                    try
                    {


                        lblTotalPowerDetails.Text = dtPackingStyleName.Rows[0]["PackingTotalPowerDetails"].ToString();
                        NOofWorkerstxt.Text = dtPackingStyleName.Rows[0]["PackingTotalLabourTask"].ToString();
                        TotalpackingStylePowerUnitsXUnitCosttxt.Text = (decimal.Parse(lblTotalPowerDetails.Text) * decimal.Parse(lblPowercosting.Text)).ToString("0.00");
                        lblNetShiftHours.Text = dtPackingStyleName.Rows[0]["Net_Shift_Hour"].ToString();
                        if (lblNetShiftHours.Text == "")
                        {
                            lblNetShiftHours.Text = "8";
                        }
                        TotalShiftPerHourtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) * decimal.Parse(lblNetShiftHours.Text)).ToString("0.00");


                        if (NOofWorkerstxt.Text != "" && NOofWorkerstxt.Text != "0")
                        {
                            TotalLabourSupervisiorCoastinginRStxt.Text = ((decimal.Parse(lblLabourCharge.Text) * (decimal.Parse(NOofWorkerstxt.Text)))).ToString("0.00");



                            TotalCostingtxt.Text = (decimal.Parse(TotalpackingStylePowerUnitsXUnitCosttxt.Text) + decimal.Parse(TotalLabourSupervisiorCoastinginRStxt.Text) + decimal.Parse(Unloadingtxt.Text) + decimal.Parse(Loadingtxt.Text)
                                                                 + decimal.Parse(MachinaryMaintain.Text) + decimal.Parse(AdminExpencetxt.Text) + decimal.Parse(ExtraExpencetxt.Text)).ToString("0.00");

                            //TotalOutputinLitertxt.Text = TotalOutPutInLiterOrKGShifttxt.Text;
                            //FinalPerLiterLabourCostingtxt.Text = (decimal.Parse(TotalCostingtxt.Text) / decimal.Parse(TotalOutputinLitertxt.Text)).ToString("0.00");


                            AdditionalCostBuffer();

                                NetLabourCostLtrtxt.Text = (decimal.Parse(FinalPerLiterLabourCostingtxt.Text) + decimal.Parse(AdditionalCostBuffertxt.Text)).ToString("0.00");
                                if (UnitMeaurementDropdown.SelectedValue == "6" || UnitMeaurementDropdown.SelectedValue == "7")
                                {
                                    decimal ConvertMilToLtr = (decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text));
                                    FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                                }
                                // Code added by Harshul Patel on 06-05-20222 for calcuation for more than 1ltr or 1 kg calculation

                                else if (UnitMeaurementDropdown.SelectedValue == "1" || UnitMeaurementDropdown.SelectedValue == "2")
                                {
                                    decimal ConvertMilToLtr = ((decimal.Parse("1000")) / (decimal.Parse(lblPackSize.Text) * 1000));
                                    FinalPerUnitLabourCosttxt.Text = (decimal.Parse(NetLabourCostLtrtxt.Text) / (ConvertMilToLtr)).ToString("0.00");
                                }
                                //End
                                else
                                {
                                    FinalPerUnitLabourCosttxt.Text = NetLabourCostLtrtxt.Text;

                                }

                           
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }

        }

        protected void Grid_ProductwiseLabourCost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridProductwiseLabourCostMaster();

            Grid_ProductwiseLabourCost.PageIndex = e.NewPageIndex;
            Grid_ProductwiseLabourCost.DataBind();
        }

        protected void DeleteProductBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            ProProductWiseLabourCostMaster pro = new ProProductWiseLabourCostMaster();
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PWLC_Id = Convert.ToInt32(Grid_ProductwiseLabourCost.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.User_Id = User_Id;
            pro.ProductWiseLabourCost_Id = PWLC_Id;
            int status = cls.Delete_ProductwiseLabourCost(pro);
            if (status > 0)
            {
                GridProductwiseLabourCostMaster();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

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
                    cmd.CommandText = "sp_Search_from_PWLC";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchBPM_PWLC", prefixText);

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
        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            TxtSearch.Text = new string(TxtSearch.Text.Where(c => Char.IsLetter(c) && Char.IsUpper(c)).ToArray());
            TxtSearch.Text = TxtSearch.Text.Substring(0, 5);
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            GridProductwiseLabourCostMaster();
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
                SqlCommand cmd = new SqlCommand("sp_Search_from_PWLC", con);
                cmd.Parameters.AddWithValue("@SearchBPM_PWLC", TxtSearch.Text.Trim());


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

            Grid_ProductwiseLabourCost.DataSource = dt;
            Grid_ProductwiseLabourCost.DataBind();
        }
    }
}
