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
    public partial class UserMaster : System.Web.UI.Page
    {
        ProRoleMaster pro = new ProRoleMaster();
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
                DisplayView();
                GetUserMasterGrid();               
            }
        }
        protected void GetUserMasterGrid()
        {
            ClsUserMaster cls = new ClsUserMaster();
            DataTable dt = new DataTable();
            dt = cls.Get_UserMasterAll();
            GirdUserMasterList.DataSource = dt;
            GirdUserMasterList.DataBind();
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] !=null && Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[0]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[20]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[20]["CanDelete"]).ToString();

            if (Convert.ToBoolean(dtMenuList.Rows[20]["CanEdit"]) == true)
            {
                if (lblUserMaster_Id.Text!="")
                {
                    AddUser.Visible = false;
                    UserUpdateBtn.Visible = true;
                    
                }
                else
                {
                    AddUser.Visible = true;
                    UserUpdateBtn.Visible = false;
                }
               
            }
            else
            {
                AddUser.Visible = false;
                UserUpdateBtn.Visible = false;
            }
            
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[0]["GroupId"]);
            if (Convert.ToBoolean(dtMenuList.Rows[20]["CanEdit"]) == true)
            {
                if (lblUserMaster_Id.Text != "")
                {
                    AddUser.Visible = false;
                    UserUpdateBtn.Visible = true;
                }
                else
                {
                    AddUser.Visible = true;
                    UserUpdateBtn.Visible = false;
                }
            }
            else
            {
                AddUser.Visible = false;
                UserUpdateBtn.Visible = false;
            }

        }

  
        public void ClearData()
        {
            FirstNametxt.Text = "";
            LastNametxt.Text = "";
            UserNametxt.Text = "";
            Mobiletxt.Text = "";
            //lblGroupId.Text = "";
            UserRoleDropdownListData();
            UserRoleDropdownList.SelectedIndex = 0;
            Passwordtxt.Text = "";
            ConfirmPasstxt.Text = "";
            Emailtxt.Text = "";
            IsActive.Checked = false;

            lblUserId.Text = "";
            UserUpdateBtn.Visible = false;
            UserSubmitBtn.Visible = true;
        }
        public void UserRoleDropdownListData()
        {
            ClsRoleMaster cls = new ClsRoleMaster();
            DataTable dt = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dt = cls.Get_AllGroupNameFromRoleMaster(pro);
            UserRoleDropdownList.DataSource = dt;
            UserRoleDropdownList.DataTextField = "GroupName";
            UserRoleDropdownList.DataValueField = "GroupId";
            UserRoleDropdownList.DataBind();
            UserRoleDropdownList.Items.Insert(0, "Select Role ");


        }

        protected void UserSubmitBtn_Click(object sender, EventArgs e)
        {
            ClsUserMaster cls = new ClsUserMaster();
            ProUserMaster pro = new ProUserMaster();
            pro.FirstName = FirstNametxt.Text;
            pro.LastName = LastNametxt.Text;
            pro.UserName = UserNametxt.Text;
            pro.MobileNo = Mobiletxt.Text;
            pro.GroupId = (UserRoleDropdownList.SelectedValue);
            pro.Password = Passwordtxt.Text;
            pro.Email = Emailtxt.Text;
            pro.IsActive = IsActive.Checked;
            pro.UpdatedBy = (lblGroupId.Text);
            pro.UpdatedDate = DateTime.Now;
            //pro.IsChangePassword = UserNametxt.Text;

            int status = cls.Insert_UserMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                //GridRM_CompanyMaster();
                //ClearData();
                //UpdateCompanyBtn.Visible = false;
                GetUserMasterGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
        }



        protected void GridEdit_Click(object sender, EventArgs e)
        {
            ProUserMaster pro = new ProUserMaster();
            ClsUserMaster cls = new ClsUserMaster();
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            pro.UserId = Convert.ToInt32(GirdUserMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblUserMaster_Id.Text = pro.UserId.ToString();
            DataTable dt = new DataTable();
            dt = cls.Get_UserMasterById(pro);
            UserRoleDropdownListData();
            if (dt.Rows.Count > 0)
            {
             
                FirstNametxt.Text = dt.Rows[0]["FirstName"].ToString();
                LastNametxt.Text = dt.Rows[0]["LastName"].ToString();
                UserNametxt.Text = dt.Rows[0]["UserName"].ToString();
                Mobiletxt.Text = dt.Rows[0]["MobileNo"].ToString();
                UserRoleDropdownList.SelectedValue = dt.Rows[0]["GroupId"].ToString();
                Emailtxt.Text = dt.Rows[0]["Email"].ToString();
                IsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                UserUpdateBtn.Visible = true;
                UserSubmitBtn.Visible = false;
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);
                UserUpdateBtn.Visible = true;
                UserSubmitBtn.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found!')", true);

            }
        }

        protected void GridDelete_Click(object sender, EventArgs e)
        {

        }

        protected void UserUpdateBtn_Click(object sender, EventArgs e)
        {
            ClsUserMaster cls = new ClsUserMaster();
            ProUserMaster pro = new ProUserMaster();
            pro.FirstName = FirstNametxt.Text;
            pro.LastName = LastNametxt.Text;
            pro.UserName = UserNametxt.Text;
            pro.MobileNo = Mobiletxt.Text;
            pro.GroupId = (UserRoleDropdownList.SelectedValue);
            pro.Password = Passwordtxt.Text;
            pro.Email = Emailtxt.Text;
            pro.IsActive = IsActive.Checked;
            pro.UpdatedBy = (lblGroupId.Text);
            pro.UpdatedDate = DateTime.Now;
            //pro.IsChangePassword = UserNametxt.Text;
            pro.UserId = Convert.ToInt32(lblUserMaster_Id.Text);
            int status = cls.Update_UserMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                //GridRM_CompanyMaster();
                //ClearData();
                //UpdateCompanyBtn.Visible = false;
                GetUserMasterGrid();
                UserUpdateBtn.Visible = false;
                UserSubmitBtn.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed')", true);

            }
        }

        protected void AddUser_Click1(object sender, EventArgs e)
        {
            ClearData();
            UserRoleDropdownListData();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);

        }

    
    }

}