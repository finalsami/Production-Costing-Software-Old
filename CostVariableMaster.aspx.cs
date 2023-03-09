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
    public partial class CostVariableMaster : System.Web.UI.Page
    {
        ProCostVariableMaster pro = new ProCostVariableMaster();
        ClsCostVariableMaster cls = new ClsCostVariableMaster();
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

                Get_OtherVariableForms();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[1]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[2]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[2]["CanDelete"]).ToString();
            //if (lblCanEdit.Text == "True")
            //{
            //    Addbtn.Visible = true;
            //    Updatebtn.Visible = true;
            //}
            //else
            //{
            //    Addbtn.Visible = false;
            //    Updatebtn.Visible = false;


            //}

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[2]["GroupId"]);
           

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
        protected void Addbtn_Click(object sender, EventArgs e)
        {
            pro.Shift_time = decimal.Parse(ShiftTimetxt.Text);
            pro.Break_Other_Timming = decimal.Parse(BreakAndOtherTimmingtxt.Text);
            pro.Net_Shift_Hour = decimal.Parse(NetShiftHourstxt.Text);
            pro.Power_Unit_Price = decimal.Parse(PowerUnitPricetxt.Text);
            pro.LabourCharge = decimal.Parse(LabourChargetxt.Text);
            pro.SuperVisorCosting = decimal.Parse(SupervisorCostingtxt.Text);
            pro.UnloadingExpense_Ltr = decimal.Parse(UnloadingExpenceLtrtxt.Text);
            pro.UnloadingExpense_Kg = decimal.Parse(UnLoadingExpenceKgtxt.Text);
            pro.UnloadingExpense_Unit = decimal.Parse(UnloadingExpenceUnittxt.Text);
            pro.LoadingExpense_Ltr = decimal.Parse(LoadingExpenceLtrtxt.Text);
            pro.LoadingExpense_Kg = decimal.Parse(LoadingExpenceKgtxt.Text);
            pro.LoadingExpense_Unit = decimal.Parse(loadingExpenceUnittxt.Text);
            pro.MachinaryMaitExpence_Ltr = decimal.Parse(MachinaryMaitExpenceLtrtxt.Text);
            pro.MachinaryMaitExpence_Kg = decimal.Parse(MachinaryMaitExpenceKgtxt.Text);
            pro.MachinaryMaitExpence_Unit = decimal.Parse(MachinaryMaitExpenceUnittxt.Text);
            pro.AdminExpence_Ltr = decimal.Parse(AdminExpenceLtrtxt.Text);
            pro.AdminExpence_Kg = decimal.Parse(AdminExpenceKgtxt.Text);
            pro.AdminExpence_Unit = decimal.Parse(AdminExpenceUnittxt.Text);
            pro.ExtraExpence_Ltr = decimal.Parse(ExtraExpenceLtrtxt.Text);
            pro.ExtraExpence_Kg = decimal.Parse(ExtraExpenceKgtxt.Text);
            pro.ExtraExpence_Unit = decimal.Parse(ExtraExpenceUnittxt.Text);
            pro.User_Id = UserId;

            int status = cls.Insert_OtherVariableForms(pro);
            if (status > 0)
            {
                Addbtn.Visible = false;
                Updatebtn.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            }
        }
        protected void Updatebtn_Click(object sender, EventArgs e)
        {
            pro.Shift_time = decimal.Parse(ShiftTimetxt.Text);
            pro.Break_Other_Timming = decimal.Parse(BreakAndOtherTimmingtxt.Text);
            pro.Net_Shift_Hour = decimal.Parse(NetShiftHourstxt.Text);
            pro.Power_Unit_Price = decimal.Parse(PowerUnitPricetxt.Text);
            pro.LabourCharge = decimal.Parse(LabourChargetxt.Text);
            pro.SuperVisorCosting = decimal.Parse(SupervisorCostingtxt.Text);
            pro.UnloadingExpense_Ltr = decimal.Parse(UnloadingExpenceLtrtxt.Text);
            pro.UnloadingExpense_Kg = decimal.Parse(UnLoadingExpenceKgtxt.Text);
            pro.UnloadingExpense_Unit = decimal.Parse(UnloadingExpenceUnittxt.Text);
            pro.LoadingExpense_Ltr = decimal.Parse(LoadingExpenceLtrtxt.Text);
            pro.LoadingExpense_Kg = decimal.Parse(LoadingExpenceKgtxt.Text);
            pro.LoadingExpense_Unit = decimal.Parse(loadingExpenceUnittxt.Text);
            pro.MachinaryMaitExpence_Ltr = decimal.Parse(MachinaryMaitExpenceLtrtxt.Text);
            pro.MachinaryMaitExpence_Kg = decimal.Parse(MachinaryMaitExpenceKgtxt.Text);
            pro.MachinaryMaitExpence_Unit = decimal.Parse(MachinaryMaitExpenceUnittxt.Text);
            pro.AdminExpence_Ltr = decimal.Parse(AdminExpenceLtrtxt.Text);
            pro.AdminExpence_Kg = decimal.Parse(AdminExpenceKgtxt.Text);
            pro.AdminExpence_Unit = decimal.Parse(AdminExpenceUnittxt.Text);
            pro.ExtraExpence_Ltr = decimal.Parse(ExtraExpenceLtrtxt.Text);
            pro.ExtraExpence_Kg = decimal.Parse(ExtraExpenceKgtxt.Text);
            pro.ExtraExpence_Unit = decimal.Parse(ExtraExpenceUnittxt.Text);
            pro.User_Id = UserId;
            pro.CostVariable_Id = Convert.ToInt32(lblOtherVariableForms_Id.Text);

            int status = cls.Update_OtherVariableForms_MasterData(pro);
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);

            }
        }
        public void Get_OtherVariableForms()
        {
            DataTable dt = new DataTable();
            dt = cls.Get_OtherVariableForms(UserId);
            if (dt.Rows.Count > 0)
            {
                ShiftTimetxt.Text = dt.Rows[0]["Shift_time"].ToString();
                BreakAndOtherTimmingtxt.Text = dt.Rows[0]["Break_Other_Timming"].ToString();
                NetShiftHourstxt.Text = dt.Rows[0]["Net_Shift_Hour"].ToString();
                PowerUnitPricetxt.Text = dt.Rows[0]["Power_Unit_Price"].ToString();
                LabourChargetxt.Text = dt.Rows[0]["LabourCharge"].ToString();
                SupervisorCostingtxt.Text = dt.Rows[0]["SuperVisorCosting"].ToString();
                UnloadingExpenceLtrtxt.Text = dt.Rows[0]["UnloadingExpense_Ltr"].ToString();
                UnLoadingExpenceKgtxt.Text = dt.Rows[0]["UnloadingExpense_Kg"].ToString();
                UnloadingExpenceUnittxt.Text = dt.Rows[0]["UnloadingExpense_Unit"].ToString();
                LoadingExpenceLtrtxt.Text = dt.Rows[0]["LoadingExpense_Ltr"].ToString();
                LoadingExpenceKgtxt.Text = dt.Rows[0]["LoadingExpense_Kg"].ToString();
                loadingExpenceUnittxt.Text = dt.Rows[0]["LoadingExpense_Unit"].ToString();
                MachinaryMaitExpenceLtrtxt.Text = dt.Rows[0]["MachinaryMaitExpence_Ltr"].ToString();
                MachinaryMaitExpenceKgtxt.Text = dt.Rows[0]["MachinaryMaitExpence_Kg"].ToString();
                MachinaryMaitExpenceUnittxt.Text = dt.Rows[0]["MachinaryMaitExpence_Unit"].ToString();
                AdminExpenceLtrtxt.Text = dt.Rows[0]["AdminExpence_Ltr"].ToString();
                AdminExpenceKgtxt.Text = dt.Rows[0]["AdminExpence_Kg"].ToString();
                AdminExpenceUnittxt.Text = dt.Rows[0]["AdminExpence_Unit"].ToString();
                AdminExpenceUnittxt.Text = dt.Rows[0]["ExtraExpence_Ltr"].ToString();
                ExtraExpenceKgtxt.Text = dt.Rows[0]["ExtraExpence_Kg"].ToString();
                ExtraExpenceLtrtxt.Text = dt.Rows[0]["ExtraExpence_Ltr"].ToString();
                ExtraExpenceUnittxt.Text = dt.Rows[0]["ExtraExpence_Unit"].ToString();
                lblOtherVariableForms_Id.Text = dt.Rows[0]["OtherVariableForms_Id"].ToString();

                if (dt.Rows.Count > 0)
                {
                    if (lblCanEdit.Text == "True")
                    {
                        Addbtn.Visible = false;
                        Updatebtn.Visible = true;
                    }
                    else
                    {
                        Addbtn.Visible = false;
                        Updatebtn.Visible = false;
                    }

                }
                else
                {
                    if (lblCanEdit.Text == "True")
                    {
                        Addbtn.Visible = true;
                        Updatebtn.Visible = false;
                    }
                    else
                    {
                        Addbtn.Visible = false;
                        Updatebtn.Visible = false;
                    }
                }
            }

        }

        protected void ShiftTimetxt_TextChanged(object sender, EventArgs e)
        {
            if (ShiftTimetxt.Text != "" && BreakAndOtherTimmingtxt.Text != "" && ShiftTimetxt.Text != "0" && BreakAndOtherTimmingtxt.Text != "0")
            {


                NetShiftHourstxt.Text = (decimal.Parse(ShiftTimetxt.Text) + decimal.Parse(BreakAndOtherTimmingtxt.Text)).ToString("0.00");

            }
        }

        protected void BreakAndOtherTimmingtxt_TextChanged(object sender, EventArgs e)
        {
            if (ShiftTimetxt.Text != "" && BreakAndOtherTimmingtxt.Text != "" && ShiftTimetxt.Text != "0" && BreakAndOtherTimmingtxt.Text != "0")
            {
                NetShiftHourstxt.Text = (decimal.Parse(ShiftTimetxt.Text) - decimal.Parse(BreakAndOtherTimmingtxt.Text)).ToString("0.00");

            }
        }
    }
}
