using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;

namespace Production_Costing_Software
{
    public partial class PM_RM_Category : System.Web.UI.Page
    {
        ClsPM_RM_Category cls = new ClsPM_RM_Category();
        ProPM_RM_Category pro = new ProPM_RM_Category();
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
                GirdPM_RMCategoryListData();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[8]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[8]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[8]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[8]["CanEdit"]) == true)
            {
                if (lblPMRMCategory_Id.Text != "")
                {
                    PM_RMCategoryAdd.Visible = false;
                    PM_RMCategoryUpdate.Visible = true;
                    CancelBtn.Visible = true;
                }
                else
                {
                    PM_RMCategoryUpdate.Visible = false;
                    PM_RMCategoryAdd.Visible = true;
                    CancelBtn.Visible = true;
                }

            }
            else
            {
                PM_RMCategoryAdd.Visible = false;
                CancelBtn.Visible = false;
                PM_RMCategoryUpdate.Visible = false;

            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[8]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[8]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[8]["CanEdit"]) == true)
            {
                if (lblPMRMCategory_Id.Text != "")
                {
                    PM_RMCategoryAdd.Visible = false;
                    PM_RMCategoryUpdate.Visible = true;
                    CancelBtn.Visible = true;
                }
                else
                {
                    PM_RMCategoryUpdate.Visible = false;
                    PM_RMCategoryAdd.Visible = true;
                    CancelBtn.Visible = true;
                }

            }
            else
            {
                PM_RMCategoryAdd.Visible = false;
                CancelBtn.Visible = false;
                PM_RMCategoryUpdate.Visible = false;

            }
        }
        public void GirdPM_RMCategoryListData()
        {
            GirdPM_RMCategoryList.DataSource = cls.GetPM_RMCategoryData(UserId);
            GirdPM_RMCategoryList.DataBind();
        }

        protected void ChkIsShipper_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkIsShipper.Checked == true)
            {
                pro.ChkIsShipper = true;
            }
            else
            {
                pro.ChkIsShipper = false;
            }
        }

        protected void PM_RMCategoryAdd_Click(object sender, EventArgs e)
        {
            pro.PM_RMCategoryName = PM_RMCategorytxt.Text;
            pro.ChkIsShipper = ChkIsShipper.Checked;
            pro.User_Id = UserId;

            int status = cls.Insert_PM_RMCategoryMaster(pro);

            if (status > 0)
            {
                GirdPM_RMCategoryListData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PMRM Category Inserted Successfully')", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }
        }
        public void ClearData()
        {
            PM_RMCategorytxt.Text = "";
            ChkIsShipper.Checked = false;
            PM_RMCategoryAdd.Visible = true;
            PM_RMCategoryUpdate.Visible = false;
            lblPMRMCategory_Id.Text = "";
        }

        protected void EditPMRMCategoryBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int PMRMCategory_Id = Convert.ToInt32(GirdPM_RMCategoryList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblPMRMCategory_Id.Text = PMRMCategory_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            pro.PM_RMCategory_Id = PMRMCategory_Id;

            dt = cls.GetPM_RMCategoryDataById(pro);
            if (dt.Rows.Count > 0)
            {
                PM_RMCategorytxt.Text = pro.PM_RMCategoryName;
                ChkIsShipper.Checked = pro.ChkIsShipper;
                PM_RMCategoryAdd.Visible = false;
                PM_RMCategoryUpdate.Visible = true;
            }
            else
            {
                PM_RMCategoryAdd.Visible = true;
                PM_RMCategoryUpdate.Visible = false;
            }


        }

        protected void PM_RMCategoryUpdate_Click(object sender, EventArgs e)
        {
            pro.PM_RMCategoryName = PM_RMCategorytxt.Text;
            pro.ChkIsShipper = ChkIsShipper.Checked;
            pro.PM_RMCategory_Id = Convert.ToInt32(lblPMRMCategory_Id.Text);
            pro.User_Id = UserId;
            int status = cls.Update_PM_RMCategoryMaster(pro);

            if (status > 0)
            {
                GirdPM_RMCategoryListData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('PMRM Category Updated Successfully')", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed!')", true);

            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            PM_RMCategoryAdd.Visible = true;
            PM_RMCategoryUpdate.Visible = false;
            lblPMRMCategory_Id.Text = "";
            ChkIsShipper.Checked = false;
        }
    }
}