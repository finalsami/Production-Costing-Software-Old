using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace Production_Costing_Software
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        int UserId;
        ClsPackingStyleCategoryMaster cls = new ClsPackingStyleCategoryMaster();
        ProPackingStyleMaster pro = new ProPackingStyleMaster();
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

                PackingStyleCategorycombo();
                PackingStyleNameCombo();
                MeasurementDataCombo();
                Grid_PackingStyleMasterList();
                PackingStyleCategoryDropdown.Items.Insert(0, "Select");
                PackingStyleNameDropdown.Items.Insert(0, "Select");
                UnitMeaurementDropdown.Items.Insert(0, "Select");
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[18]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[18]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[18]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPAckingStyleMaster_Id.Text != "")
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[19]["GroupId"]);

            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[18]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[18]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPAckingStyleMaster_Id.Text != "")
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

        public void GetLoginDetails()
        {
            if (Session["UserName"] !=null&& Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                UserId = Convert.ToInt32(Session["UserId"].ToString());
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void Grid_PackingStyleMasterList()
        {
            ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
            Grid_PackingStyleMaster.DataSource = cls.GetPackingStyleMasterGridAll(UserId);
            Grid_PackingStyleMaster.DataBind();
        }
        public void MeasurementDataCombo()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            UnitMeaurementDropdown.DataSource = cls.GetEnumMasterMeasurement();

            UnitMeaurementDropdown.DataTextField = "EnumDescription";
            UnitMeaurementDropdown.DataValueField = "PkEnumId";

            UnitMeaurementDropdown.DataBind();
            //UnitMeaurementDropdown.Items.Insert(0, "Select");

        }
        [WebMethod]
        public static List<string> GetLabourTask(string User_Id)
        {
            string Details = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "sp_Get_LabourTaskAll";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Connection = con;
                con.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        customers.Add(sdr["SearchText"].ToString());

                    }
                }

                con.Close();

                return customers;
            }

        }

        public void PackingStyleCategorycombo()
        {
            ClsPackingStyleCategoryMaster cls = new ClsPackingStyleCategoryMaster();
            ProPackingStyleCategoryMaster pro = new ProPackingStyleCategoryMaster();
            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            dt = cls.Get_PackingStyleCategoryDropdown(pro);

            dt.Columns.Add("Packing_Style", typeof(string), "PackingStyleCategoryName + ' (' + PackingSize +'- '+PackingMeasurement+')'").ToString();
            //DataView dvOptions = new DataView(dt);
            //dvOptions.Sort = "PackingStyleCategoryName";

            PackingStyleCategoryDropdown.DataSource = dt;
            PackingStyleCategoryDropdown.DataTextField = "Packing_Style";

            PackingStyleCategoryDropdown.DataValueField = "PackingStyleCategory_Id";
            PackingStyleCategoryDropdown.DataBind();

            PackingStyleCategoryDropdown.Items.Insert(0, "Select");
        }
        public void PackingStyleNameCombo()
        {
            ClsPackingStyleNameMaster cls = new ClsPackingStyleNameMaster();

            DataTable dt = new DataTable();
            dt = cls.Get_PackingStyleNameAll(UserId);

            PackingStyleNameDropdown.DataSource = dt;
            PackingStyleNameDropdown.DataTextField = "PackingStyleName";
            PackingStyleNameDropdown.DataValueField = "PackingStyleName_Id";
            PackingStyleNameDropdown.DataBind();

            //PackingStyleNameDropdown.Items.Insert(0, "Select");


        }

        protected void Addbtn_Click(object sender, EventArgs e)
        {
            ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
            pro.Fk_Packing_Style_Cat_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue);
            pro.Fk_Packing_Style_Name_Id = Convert.ToInt32(PackingStyleNameDropdown.SelectedValue);
            pro.PackingStyleMaster_Measurement = Convert.ToInt32(UnitMeaurementDropdown.SelectedValue);
            pro.PackingSize = decimal.Parse(PackingSizetxt.Text);
            pro.Task_Bulk_Charge = Convert.ToInt32(BulkChargetxt.Text);
            pro.Task_Pouch_Filling = Convert.ToInt32(PouchFillingtxt.Text);
            pro.Task_Bottle_Keeping = Convert.ToInt32(BottleKeepingtxt.Text);
            pro.Task_Lifting_Pouch_Bottle_Wt = Convert.ToInt32(Lifting_Pouch_Bottle_Wttxt.Text);
            pro.Task_Black_linner_pouch = Convert.ToInt32(BlacklinnerPouchtxt.Text);
            pro.Task_Inner_Plug = Convert.ToInt32(InnerPlugtxt.Text);
            pro.Task_Mesuring_Cap = Convert.ToInt32(MesuringCaptxt.Text);
            pro.Task_Caping = Convert.ToInt32(Capingtxt.Text);
            pro.Task_Tear_Down_Seal = Convert.ToInt32(TearDownSealtxt.Text);
            pro.Task_Induction = Convert.ToInt32(Inductiontxt.Text);
            pro.Task_Pouch_Sealing = Convert.ToInt32(PouchSealingtxt.Text);
            pro.Task_Bottle_Pouch_Cleaning = Convert.ToInt32(BottlePouchCleaningtxt.Text);
            pro.Task_Labeling = Convert.ToInt32(Labelingtxt.Text);
            pro.Task_Sleeve = Convert.ToInt32(Sleevetxt.Text);
            pro.Task_Inner_box = Convert.ToInt32(Innerboxtxt.Text);
            pro.Task_SS_Tin_Drum_Bucket_Bag = Convert.ToInt32(SSTin_Drum_Bucket_Bagtxt.Text);
            pro.Task_Inner_Box_Cello_Tape = Convert.ToInt32(InnerBoxCelloTapetxt.Text);
            pro.Task_kitchen_Tray = Convert.ToInt32(kitchenTraytxt.Text);
            pro.Task_OuterLabel_BOPP_Filling_BoxFilling = Convert.ToInt32(OuterLabel_BOPPFilling_BoxFillingtxt.Text);
            pro.Task_Stapping_Wt = Convert.ToInt32(StappingWttxt.Text);
            pro.Task_Additional_Other = Convert.ToInt32(Additional_Other.Text);
            pro.Power_Filling = decimal.Parse(FillingPowertxt.Text);
            pro.Power_Capping = decimal.Parse(CappingPowertxt.Text);
            pro.Power_Induction = decimal.Parse(InductionPowertxt.Text);
            pro.Power_Lableling = decimal.Parse(LabelingPowertxt.Text);
            pro.Power_Shrinking = decimal.Parse(ShrinkingPowertxt.Text);
            pro.Power_BOPP = decimal.Parse(BoppPowertxt.Text);
            pro.Power_Stepping = decimal.Parse(SteppingPowertxt.Text);
            pro.Power_StealingMC = decimal.Parse(SealingMCPowertxt.Text);
            pro.Power_Detail9 = decimal.Parse(PowerDetail9txt.Text);
            pro.Power_Detail10 = decimal.Parse(PowerDetail10txt.Text);
            pro.PowerUnitPerHour = decimal.Parse(PowerUnitPerhourtxt.Text);
            pro.OtherPowerCharges = decimal.Parse(OtherPowerChargestxt.Text);
            pro.PackingTotalLabourTask = pro.Task_Bulk_Charge
                                                             + pro.Task_Pouch_Filling
                                                             + pro.Task_Bottle_Keeping
                                                             + pro.Task_Lifting_Pouch_Bottle_Wt
                                                             + pro.Task_Black_linner_pouch
                                                             + pro.Task_Inner_Plug
                                                             + pro.Task_Mesuring_Cap
                                                             + pro.Task_Caping
                                                             + pro.Task_Tear_Down_Seal
                                                             + pro.Task_Induction
                                                             + pro.Task_Pouch_Sealing
                                                             + pro.Task_Bottle_Pouch_Cleaning
                                                             + pro.Task_Labeling
                                                             + pro.Task_Sleeve
                                                             + pro.Task_Inner_box
                                                             + pro.Task_SS_Tin_Drum_Bucket_Bag
                                                             + pro.Task_Inner_Box_Cello_Tape
                                                             + pro.Task_kitchen_Tray
                                                             + pro.Task_OuterLabel_BOPP_Filling_BoxFilling
                                                             + pro.Task_Stapping_Wt +
                                                             +pro.Task_Additional_Other;
            pro.PackingTotalPowerDetails = pro.Power_Filling
                                                             + pro.Power_Capping
                                                             + pro.Power_Induction
                                                             + pro.Power_Lableling
                                                             + pro.Power_Shrinking
                                                             + pro.Power_BOPP
                                                             + pro.Power_Stepping
                                                             + pro.Power_StealingMC
                                                             + pro.Power_Detail9
                                                             + pro.Power_Detail10
                                                             + pro.PowerUnitPerHour
                                                             + pro.OtherPowerCharges;

            pro.User_Id = UserId;
            int status = cls.Insert_PackingStyleMasterName(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearData();
                Grid_PackingStyleMasterList();
            }

        }

        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
            pro.Fk_Packing_Style_Cat_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue);
            pro.Fk_Packing_Style_Name_Id = Convert.ToInt32(PackingStyleNameDropdown.SelectedValue);
            pro.PackingStyleMaster_Measurement = Convert.ToInt32(UnitMeaurementDropdown.SelectedValue);
            pro.PackingSize = decimal.Parse(PackingSizetxt.Text);
            pro.Task_Bulk_Charge = Convert.ToInt32(BulkChargetxt.Text);
            pro.Task_Pouch_Filling = Convert.ToInt32(PouchFillingtxt.Text);
            pro.Task_Bottle_Keeping = Convert.ToInt32(BottleKeepingtxt.Text);
            pro.Task_Lifting_Pouch_Bottle_Wt = Convert.ToInt32(Lifting_Pouch_Bottle_Wttxt.Text);
            pro.Task_Black_linner_pouch = Convert.ToInt32(BlacklinnerPouchtxt.Text);
            pro.Task_Inner_Plug = Convert.ToInt32(InnerPlugtxt.Text);
            pro.Task_Mesuring_Cap = Convert.ToInt32(MesuringCaptxt.Text);
            pro.Task_Caping = Convert.ToInt32(Capingtxt.Text);
            pro.Task_Tear_Down_Seal = Convert.ToInt32(TearDownSealtxt.Text);
            pro.Task_Induction = Convert.ToInt32(Inductiontxt.Text);
            pro.Task_Pouch_Sealing = Convert.ToInt32(PouchSealingtxt.Text);
            pro.Task_Bottle_Pouch_Cleaning = Convert.ToInt32(BottlePouchCleaningtxt.Text);
            pro.Task_Labeling = Convert.ToInt32(Labelingtxt.Text);
            pro.Task_Sleeve = Convert.ToInt32(Sleevetxt.Text);
            pro.Task_Inner_box = Convert.ToInt32(Innerboxtxt.Text);
            pro.Task_SS_Tin_Drum_Bucket_Bag = Convert.ToInt32(SSTin_Drum_Bucket_Bagtxt.Text);
            pro.Task_Inner_Box_Cello_Tape = Convert.ToInt32(InnerBoxCelloTapetxt.Text);
            pro.Task_kitchen_Tray = Convert.ToInt32(kitchenTraytxt.Text);
            pro.Task_OuterLabel_BOPP_Filling_BoxFilling = Convert.ToInt32(OuterLabel_BOPPFilling_BoxFillingtxt.Text);
            pro.Task_Stapping_Wt = Convert.ToInt32(StappingWttxt.Text);
            pro.Task_Additional_Other = Convert.ToInt32(Additional_Other.Text);
            pro.Power_Filling = decimal.Parse(FillingPowertxt.Text);
            pro.Power_Capping = decimal.Parse(CappingPowertxt.Text);
            pro.Power_Induction = decimal.Parse(InductionPowertxt.Text);
            pro.Power_Lableling = decimal.Parse(LabelingPowertxt.Text);
            pro.Power_Shrinking = decimal.Parse(ShrinkingPowertxt.Text);
            pro.Power_BOPP = decimal.Parse(BoppPowertxt.Text);
            pro.Power_Stepping = decimal.Parse(SteppingPowertxt.Text);
            pro.Power_StealingMC = decimal.Parse(SealingMCPowertxt.Text);
            pro.Power_Detail9 = decimal.Parse(PowerDetail9txt.Text);
            pro.Power_Detail10 = decimal.Parse(PowerDetail10txt.Text);
            pro.PowerUnitPerHour = decimal.Parse(PowerUnitPerhourtxt.Text);
            pro.OtherPowerCharges = decimal.Parse(OtherPowerChargestxt.Text);
            pro.PackingTotalLabourTask = pro.Task_Bulk_Charge
                                                             + pro.Task_Pouch_Filling
                                                             + pro.Task_Bottle_Keeping
                                                             + pro.Task_Lifting_Pouch_Bottle_Wt
                                                             + pro.Task_Black_linner_pouch
                                                             + pro.Task_Inner_Plug
                                                             + pro.Task_Mesuring_Cap
                                                             + pro.Task_Caping
                                                             + pro.Task_Tear_Down_Seal
                                                             + pro.Task_Induction
                                                             + pro.Task_Pouch_Sealing
                                                             + pro.Task_Bottle_Pouch_Cleaning
                                                             + pro.Task_Labeling
                                                             + pro.Task_Sleeve
                                                             + pro.Task_Inner_box
                                                             + pro.Task_SS_Tin_Drum_Bucket_Bag
                                                             + pro.Task_Inner_Box_Cello_Tape
                                                             + pro.Task_kitchen_Tray
                                                             + pro.Task_OuterLabel_BOPP_Filling_BoxFilling
                                                             + pro.Task_Stapping_Wt +
                                                             +pro.Task_Additional_Other;
            pro.PackingTotalPowerDetails = pro.Power_Filling
                                                             + pro.Power_Capping
                                                             + pro.Power_Induction
                                                             + pro.Power_Lableling
                                                             + pro.Power_Shrinking
                                                             + pro.Power_BOPP
                                                             + pro.Power_Stepping
                                                             + pro.Power_StealingMC
                                                             + pro.Power_Detail9
                                                             + pro.Power_Detail10;

            pro.User_Id = UserId;

            pro.PackingStyleMaster_Id = Convert.ToInt32(lblPAckingStyleMaster_Id.Text);

            int status = cls.UpdatePackingStyleMasterData(pro);
            if (status > 0)
            {
                Grid_PackingStyleMasterList();
                Updatebtn.Visible = false;
                Addbtn.Visible = true;
                CancelBtn.Visible = true;
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

            }
            else
            {
                Updatebtn.Visible = true;
                Addbtn.Visible = false;
                CancelBtn.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update  Failed')", true);

            }

        }
        public void ClearData()
        {

            //PackingStyleCategoryDropdown.SelectedItem.Text ="Select";

            //PackingStyleNameDropdown.SelectedItem.Text = "Select";

            //UnitMeaurementDropdown.SelectedItem.Text = "Select";
            //PackingStyleNameDropdown.SelectedValue = dt.Rows[0]["PackingStyleMaster_Measurement"].ToString();

            PackingSizetxt.Text = "0";
            BulkChargetxt.Text = "0";
            PouchFillingtxt.Text = "0";
            BottleKeepingtxt.Text = "0";
            Lifting_Pouch_Bottle_Wttxt.Text = "0";
            BlacklinnerPouchtxt.Text = "0";
            InnerPlugtxt.Text = "0";
            MesuringCaptxt.Text = "0";
            Capingtxt.Text = "0";
            TearDownSealtxt.Text = "0";
            Inductiontxt.Text = "0";
            PouchSealingtxt.Text = "0";
            BottlePouchCleaningtxt.Text = "0";
            Labelingtxt.Text = "0";
            Sleevetxt.Text = "0";
            Innerboxtxt.Text = "0";
            SSTin_Drum_Bucket_Bagtxt.Text = "0";
            InnerBoxCelloTapetxt.Text = "0";
            kitchenTraytxt.Text = "0";
            OuterLabel_BOPPFilling_BoxFillingtxt.Text = "0";
            StappingWttxt.Text = "0";
            Additional_Other.Text = "0";
            FillingPowertxt.Text = "0";
            CappingPowertxt.Text = "0";
            InductionPowertxt.Text = "0";
            LabelingPowertxt.Text = "0";
            ShrinkingPowertxt.Text = "0";
            BoppPowertxt.Text = "0";
            SteppingPowertxt.Text = "0";
            SealingMCPowertxt.Text = "0";
            PowerDetail9txt.Text = "0";
            PowerDetail10txt.Text = "0";
            PowerUnitPerhourtxt.Text = "0";
            OtherPowerChargestxt.Text = "0";

            lblPAckingStyleMaster_Id.Text = "";
            PackingStyleCategoryDropdown.SelectedIndex = 0;
            PackingStyleNameDropdown.SelectedIndex = 0;
            UnitMeaurementDropdown.SelectedIndex = 0;
            TotalLaboutPerTasktxt.Text = "0";
            TotalPowertxt.Text = "0";
            TotalLabour_Powertxt.Text = "0";
            Addbtn.Visible = true;
            Updatebtn.Visible = false;
        }
        protected void Grid_PackingStyleMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //SubTotalLabourTask += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "InputTechnical"));
                //SubTotalPower += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "SubTotal";
                e.Row.Cells[1].Font.Bold = true;

            }
        }

        protected void DelPackingStyleMasterBtn_Click(object sender, EventArgs e)
        {
            ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PackingStyleMaster_ID = Convert.ToInt32(Grid_PackingStyleMaster.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.DeletePackingMaster(PackingStyleMaster_ID, UserId);
            if (status > 0)
            {
                Grid_PackingStyleMasterList();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Fail')", true);

            }
        }

        protected void EditPackingStyleMasterBtn_Click(object sender, EventArgs e)
        {
            ClsPackingStyleMaster cls = new ClsPackingStyleMaster();
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int PackingStyleMaster_ID = Convert.ToInt32(Grid_PackingStyleMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            lblPAckingStyleMaster_Id.Text = PackingStyleMaster_ID.ToString();
            DataTable dt = new DataTable();
            dt = cls.GetPackingStyleMasterById(PackingStyleMaster_ID, UserId);
            PackingStyleCategorycombo();

            lblpackMeasurement.Text = dt.Rows[0]["PackingStyleMaster_Measurement"].ToString();
            lblPackSize.Text = dt.Rows[0]["PackingSize"].ToString();

            if (dt.Rows.Count > 0)
            {
                //PackingStyleCategoryDropdown.SelectedItem.Text = dt.Rows[0]["PackingStyleCategoryName"].ToString();
                PackingStyleCategoryDropdown.SelectedValue = dt.Rows[0]["Fk_Packing_Style_Cat_Id"].ToString();
                //PackingStyleNameDropdown.SelectedItem.Text= dt.Rows[0]["PackingStyleName"].ToString();
                PackingStyleNameDropdown.SelectedValue = dt.Rows[0]["Fk_Packing_Style_Name_Id"].ToString();
                //UnitMeaurementDropdown.SelectedItem.Text = dt.Rows[0]["Measurement"].ToString();
                UnitMeaurementDropdown.SelectedValue = dt.Rows[0]["PackingStyleMaster_Measurement"].ToString();
                //PackingStyleNameDropdown.SelectedValue = dt.Rows[0]["PackingStyleMaster_Measurement"].ToString();

                PackingSizetxt.Text = dt.Rows[0]["PackingSize"].ToString();
                BulkChargetxt.Text = dt.Rows[0]["Task_Bulk_Charge"].ToString();
                PouchFillingtxt.Text = dt.Rows[0]["Task_Pouch_Filling"].ToString();
                BottleKeepingtxt.Text = dt.Rows[0]["Task_Bottle_Keeping"].ToString();
                Lifting_Pouch_Bottle_Wttxt.Text = dt.Rows[0]["Task_Lifting_Pouch_Bottle_Wt"].ToString();
                BlacklinnerPouchtxt.Text = dt.Rows[0]["Task_Black_linner_pouch"].ToString();
                InnerPlugtxt.Text = dt.Rows[0]["Task_Inner_Plug"].ToString();
                MesuringCaptxt.Text = dt.Rows[0]["Task_Mesuring_Cap"].ToString();
                Capingtxt.Text = dt.Rows[0]["Task_Caping"].ToString();
                TearDownSealtxt.Text = dt.Rows[0]["Task_Tear_Down_Seal"].ToString();
                Inductiontxt.Text = dt.Rows[0]["Task_Induction"].ToString();
                PouchSealingtxt.Text = dt.Rows[0]["Task_Pouch_Sealing"].ToString();
                BottlePouchCleaningtxt.Text = dt.Rows[0]["Task_Bottle_Pouch_Cleaning"].ToString();
                Labelingtxt.Text = dt.Rows[0]["Task_Labeling"].ToString();
                Sleevetxt.Text = dt.Rows[0]["Task_Sleeve"].ToString();
                Innerboxtxt.Text = dt.Rows[0]["Task_Inner_box"].ToString();
                SSTin_Drum_Bucket_Bagtxt.Text = dt.Rows[0]["Task_SS_Tin_Drum_Bucket_Bag"].ToString();
                InnerBoxCelloTapetxt.Text = dt.Rows[0]["Task_Inner_Box_Cello_Tape"].ToString();
                kitchenTraytxt.Text = dt.Rows[0]["Task_kitchen_Tray"].ToString();
                OuterLabel_BOPPFilling_BoxFillingtxt.Text = dt.Rows[0]["Task_OuterLabel_BOPP_Filling_BoxFilling"].ToString();
                StappingWttxt.Text = dt.Rows[0]["Task_Stapping_Wt"].ToString();
                Additional_Other.Text = dt.Rows[0]["Task_Additional_Other"].ToString();
                FillingPowertxt.Text = dt.Rows[0]["Power_Filling"].ToString();
                CappingPowertxt.Text = dt.Rows[0]["Power_Capping"].ToString();
                InductionPowertxt.Text = dt.Rows[0]["Power_Induction"].ToString();
                LabelingPowertxt.Text = dt.Rows[0]["Power_Lableling"].ToString();
                ShrinkingPowertxt.Text = dt.Rows[0]["Power_Shrinking"].ToString();
                BoppPowertxt.Text = dt.Rows[0]["Power_BOPP"].ToString();
                SteppingPowertxt.Text = dt.Rows[0]["Power_Stepping"].ToString();
                SealingMCPowertxt.Text = dt.Rows[0]["Power_StealingMC"].ToString();
                PowerDetail9txt.Text = dt.Rows[0]["Power_Detail9"].ToString();
                PowerDetail10txt.Text = dt.Rows[0]["Power_Detail10"].ToString();
                PowerUnitPerhourtxt.Text = dt.Rows[0]["PowerUnitPerHour"].ToString();
                OtherPowerChargestxt.Text = dt.Rows[0]["Power_Other"].ToString();

                if (dt.Rows.Count > 0)
                {
                    Updatebtn.Visible = true;
                    Addbtn.Visible = false;

                }
                else
                {
                    Updatebtn.Visible = false;
                    Addbtn.Visible = true;
                }

                pro.Task_Bulk_Charge = Convert.ToInt32(BulkChargetxt.Text);
                pro.Task_Pouch_Filling = Convert.ToInt32(PouchFillingtxt.Text);
                pro.Task_Bottle_Keeping = Convert.ToInt32(BottleKeepingtxt.Text);
                pro.Task_Lifting_Pouch_Bottle_Wt = Convert.ToInt32(Lifting_Pouch_Bottle_Wttxt.Text);
                pro.Task_Black_linner_pouch = Convert.ToInt32(BlacklinnerPouchtxt.Text);
                pro.Task_Inner_Plug = Convert.ToInt32(InnerPlugtxt.Text);
                pro.Task_Mesuring_Cap = Convert.ToInt32(MesuringCaptxt.Text);
                pro.Task_Caping = Convert.ToInt32(Capingtxt.Text);
                pro.Task_Tear_Down_Seal = Convert.ToInt32(TearDownSealtxt.Text);
                pro.Task_Induction = Convert.ToInt32(Inductiontxt.Text);
                pro.Task_Pouch_Sealing = Convert.ToInt32(PouchSealingtxt.Text);
                pro.Task_Bottle_Pouch_Cleaning = Convert.ToInt32(BottlePouchCleaningtxt.Text);
                pro.Task_Labeling = Convert.ToInt32(Labelingtxt.Text);
                pro.Task_Sleeve = Convert.ToInt32(Sleevetxt.Text);
                pro.Task_Inner_box = Convert.ToInt32(Innerboxtxt.Text);
                pro.Task_SS_Tin_Drum_Bucket_Bag = Convert.ToInt32(SSTin_Drum_Bucket_Bagtxt.Text);
                pro.Task_Inner_Box_Cello_Tape = Convert.ToInt32(InnerBoxCelloTapetxt.Text);
                pro.Task_kitchen_Tray = Convert.ToInt32(kitchenTraytxt.Text);
                pro.Task_OuterLabel_BOPP_Filling_BoxFilling = Convert.ToInt32(OuterLabel_BOPPFilling_BoxFillingtxt.Text);
                pro.Task_Stapping_Wt = Convert.ToInt32(StappingWttxt.Text);
                pro.Task_Additional_Other = Convert.ToInt32(Additional_Other.Text);
                pro.Power_Filling = decimal.Parse(FillingPowertxt.Text);
                pro.Power_Capping = decimal.Parse(CappingPowertxt.Text);
                pro.Power_Induction = decimal.Parse(InductionPowertxt.Text);
                pro.Power_Lableling = decimal.Parse(LabelingPowertxt.Text);
                pro.Power_Shrinking = decimal.Parse(ShrinkingPowertxt.Text);
                pro.Power_BOPP = decimal.Parse(BoppPowertxt.Text);
                pro.Power_Stepping = decimal.Parse(SteppingPowertxt.Text);
                pro.Power_StealingMC = decimal.Parse(SealingMCPowertxt.Text);
                pro.Power_Detail9 = decimal.Parse(PowerDetail9txt.Text);
                pro.Power_Detail10 = decimal.Parse(PowerDetail10txt.Text);
                pro.PowerUnitPerHour = decimal.Parse(PowerUnitPerhourtxt.Text);
                pro.OtherPowerCharges = decimal.Parse(OtherPowerChargestxt.Text);
                TotalLaboutPerTasktxt.Text = (pro.Task_Bulk_Charge
                                                                  + pro.Task_Pouch_Filling
                                                                  + pro.Task_Bottle_Keeping
                                                                  + pro.Task_Lifting_Pouch_Bottle_Wt
                                                                  + pro.Task_Black_linner_pouch
                                                                  + pro.Task_Inner_Plug
                                                                  + pro.Task_Mesuring_Cap
                                                                  + pro.Task_Caping
                                                                  + pro.Task_Tear_Down_Seal
                                                                  + pro.Task_Induction
                                                                  + pro.Task_Pouch_Sealing
                                                                  + pro.Task_Bottle_Pouch_Cleaning
                                                                  + pro.Task_Labeling
                                                                  + pro.Task_Sleeve
                                                                  + pro.Task_Inner_box
                                                                  + pro.Task_SS_Tin_Drum_Bucket_Bag
                                                                  + pro.Task_Inner_Box_Cello_Tape
                                                                  + pro.Task_kitchen_Tray
                                                                  + pro.Task_OuterLabel_BOPP_Filling_BoxFilling
                                                                  + pro.Task_Stapping_Wt +
                                                                  +pro.Task_Additional_Other).ToString();
                TotalPowertxt.Text = (pro.Power_Filling
                                                                  + pro.Power_Capping
                                                                  + pro.Power_Induction
                                                                  + pro.Power_Lableling
                                                                  + pro.Power_Shrinking
                                                                  + pro.Power_BOPP
                                                                  + pro.Power_Stepping
                                                                  + pro.Power_StealingMC
                                                                  + pro.Power_Detail9
                                                                  + pro.Power_Detail10
                                                                  + pro.PowerUnitPerHour
                                                                  + pro.OtherPowerCharges).ToString();

                TotalLabour_Powertxt.Text = (decimal.Parse(TotalLaboutPerTasktxt.Text) + decimal.Parse(TotalPowertxt.Text)).ToString();
            }


        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            ClearData();


        }
        protected void OuterLabel_BOPPFilling_BoxFillingtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void StappingWttxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges(); ;
        }

        protected void Additional_Other_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void kitchenTraytxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void InnerBoxCelloTapetxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void SSTin_Drum_Bucket_Bagtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Innerboxtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Sleevetxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Labelingtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void BottlePouchCleaningtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void PouchSealingtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Inductiontxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void TearDownSealtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Capingtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void MesuringCaptxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void InnerPlugtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void BlacklinnerPouchtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Lifting_Pouch_Bottle_Wttxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void BottleKeepingtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void PouchFillingtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void BulkChargetxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void Grid_PackingStyleMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_PackingStyleMasterList();


            Grid_PackingStyleMaster.PageIndex = e.NewPageIndex;
            Grid_PackingStyleMaster.DataBind();
        }
        protected void FillingPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void CappingPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void InductionPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void LabelingPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void ShrinkingPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void BoppPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void SteppingPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void SealingMCPowertxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void PowerDetail9txt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void PowerDetail10txt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();
        }

        protected void PowerUnitPerhourtxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();

        }

        protected void OtherPowerChargestxt_TextChanged(object sender, EventArgs e)
        {
            LabourCharges();
            PowerCharges();

        }
        public void PowerCharges()
        {
            if (FillingPowertxt.Text != "" || CappingPowertxt.Text != "" || InductionPowertxt.Text != "" || LabelingPowertxt.Text != ""
                || ShrinkingPowertxt.Text != "" || BoppPowertxt.Text != "" || SteppingPowertxt.Text != "" || SealingMCPowertxt.Text != ""
                || PowerDetail9txt.Text != "" || PowerDetail10txt.Text != "" || PowerUnitPerhourtxt.Text != "" || OtherPowerChargestxt.Text != "")
            {
                TotalPowertxt.Text = (decimal.Parse(FillingPowertxt.Text) + (decimal.Parse(CappingPowertxt.Text) +
              (decimal.Parse(InductionPowertxt.Text) + (decimal.Parse(LabelingPowertxt.Text) + (decimal.Parse(ShrinkingPowertxt.Text) + (decimal.Parse(BoppPowertxt.Text) + (decimal.Parse(SteppingPowertxt.Text) +
              (decimal.Parse(SealingMCPowertxt.Text) + (decimal.Parse(PowerDetail9txt.Text) +
              (decimal.Parse(PowerDetail10txt.Text) + (decimal.Parse(PowerUnitPerhourtxt.Text) + (decimal.Parse(OtherPowerChargestxt.Text))))))))))))).ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Value Can not be Blank!')", true);

            }
            if (TotalPowertxt.Text != "" || TotalLaboutPerTasktxt.Text != "")
            {
                TotalLabour_Powertxt.Text = (decimal.Parse(TotalPowertxt.Text) + decimal.Parse(TotalLaboutPerTasktxt.Text)).ToString();

            }
        }
        public void LabourCharges()
        {
            TotalLaboutPerTasktxt.Text = (decimal.Parse(OuterLabel_BOPPFilling_BoxFillingtxt.Text) + decimal.Parse(StappingWttxt.Text) + decimal.Parse(Additional_Other.Text) + decimal.Parse(kitchenTraytxt.Text) +
                          decimal.Parse(InnerBoxCelloTapetxt.Text) + decimal.Parse(SSTin_Drum_Bucket_Bagtxt.Text) + decimal.Parse(Innerboxtxt.Text) + decimal.Parse(Sleevetxt.Text) + decimal.Parse(Labelingtxt.Text) + decimal.Parse(BottlePouchCleaningtxt.Text) +
                          decimal.Parse(PouchSealingtxt.Text) + decimal.Parse(Inductiontxt.Text) + decimal.Parse(TearDownSealtxt.Text) + decimal.Parse(Capingtxt.Text) + decimal.Parse(MesuringCaptxt.Text) + decimal.Parse(InnerPlugtxt.Text) + decimal.Parse(BlacklinnerPouchtxt.Text) +
                          decimal.Parse(Lifting_Pouch_Bottle_Wttxt.Text) + decimal.Parse(BottleKeepingtxt.Text) + decimal.Parse(PouchFillingtxt.Text) + decimal.Parse(BulkChargetxt.Text)).ToString();

        }


        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_PM_from_PackingStyleMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchcPackingStyleCategoryName", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["PackingStyleCategoryName"].ToString());


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
            if (TxtSearch.Text!="")
            {
                TxtSearch.Text = TxtSearch.Text.Substring(0, 3);

            }
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            Grid_PackingStyleMasterList();

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
                SqlCommand cmd = new SqlCommand("sp_Search_PM_from_PackingStyleMaster", con);
                cmd.Parameters.AddWithValue("@SearchcPackingStyleCategoryName", TxtSearch.Text.Trim());


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

            Grid_PackingStyleMaster.DataSource = dt;
            Grid_PackingStyleMaster.DataBind();
        }

        protected void UnitMeaurementDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            pro.Fk_Packing_Style_Cat_Id = Convert.ToInt32(PackingStyleCategoryDropdown.SelectedValue);
            pro.Fk_Packing_Style_Name_Id=Convert.ToInt32(PackingStyleNameDropdown.SelectedValue);
            pro.PackingSize = decimal.Parse(PackingSizetxt.Text);
            pro.PackingStyleMaster_Measurement=Convert.ToInt32(UnitMeaurementDropdown.SelectedValue);
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_PackingStyleMaster(pro);
            if (dtCheck.Rows.Count>0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple  Style ,Category,Size And Name !')", true);
                ClearData();
                return;
            }
        }
    }
   
}