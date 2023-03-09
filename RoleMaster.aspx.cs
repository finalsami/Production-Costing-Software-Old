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

namespace Production_Costing_Software
{
    public partial class RoleMaster : System.Web.UI.Page
    {
        ClsRoleMaster cls = new ClsRoleMaster();
        ProRoleMaster pro = new ProRoleMaster();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                Response.Redirect("~/Login.aspx", true);
            }
            lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
            UserIdAdmin.Text = Session["UserId"].ToString();
            lblGroupId.Text = Session["GroupId"].ToString();
            //lblRoleId.Text = Session["RoleId"].ToString();
            lblUserId.Text = Session["UserId"].ToString();
            //GetLoginDetails();
            GetUserRights();
            if (!IsPostBack)
            {  
                GridGroupList();                            
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
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[21]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[21]["CanDelete"]).ToString();
            if (lblCanEdit.Text=="True")
            {
                AddUser.Visible = true;
            }
            else
            {
                AddUser.Visible = false;

            }
           
        }

        public void GridGroupList()
        {
            DataTable dt = new DataTable();
            dt = cls.Get_Group_MasterData();
            GirdRoleMasterList.DataSource = dt;
            GirdRoleMasterList.DataBind();
        }
        public void GetLoginDetails()
        {

            lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
            UserIdAdmin.Text = Session["UserId"].ToString();
            lblGroupId.Text = Session["GroupId"].ToString();
            //lblRoleId.Text = Session["RoleId"].ToString();
            lblUserId.Text = Session["UserId"].ToString();

        }
        protected void AddRoleBtn_Click(object sender, EventArgs e)
        {

            pro.GroupName = RoleNametxt.Text;
            if (IsActive.Checked)
            {
                pro.IsActive = true;

            }
            else
            {
                pro.IsActive = false;
            }
            pro.FkCompanyId = 1;
            pro.UpdatedBy = Convert.ToInt32(lblUserId.Text);
            int status = cls.Group_CreateUpdate(pro);
            if (status > 0)
            {

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ClearData();
                GridGroupList();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }



        }
        protected void ClearData()
        {
            RoleNametxt.Text = "";
            IsActive.Checked = true;
            lblRoleId.Text = "";
           

        }


        protected void UserRightsBtn_Click1(object sender, EventArgs e)
        {

            Button Add = sender as Button;
            GridViewRow gdrow = Add.NamingContainer as GridViewRow;
            pro.GroupId = Convert.ToInt32(GirdRoleMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblGroupId.Text = pro.GroupId.ToString();
            ClsMenuDisplay cls = new ClsMenuDisplay();
            DataTable dtPCS = new DataTable();
            dtPCS = cls.Get_MenuListByGroup(pro);
            DataTable dtRole = new DataTable();
            ClsRoleMaster clsRole = new ClsRoleMaster();

            dtRole = clsRole.Get_AllGroupNameFromRoleMaster(pro);
            lblRoleId.Text= (dtRole.Rows[Convert.ToInt32(lblGroupId.Text)-1]["GroupId"]).ToString();
            lblRoleName.Text = dtRole.Rows[Convert.ToInt32(lblGroupId.Text)-1]["GroupName"].ToString();

            if (dtPCS.Rows.Count > 0)
            {
                //lblRoleId.Text = dtPCS.Rows[0]["GroupId"].ToString();

                GridUserRightsList.DataSource = dtPCS;
                GridUserRightsList.DataBind();

                string strCurrentMenu = string.Empty;
                foreach (GridViewRow row in GridUserRightsList.Rows)
                {
                    if (strCurrentMenu != row.Cells[1].Text)
                    {
                        strCurrentMenu = row.Cells[1].Text;
                    }
                    else
                    {
                        row.Cells[1].Text = string.Empty;
                    }
                }
            }

            //DataTable dtComapny = new DataTable();
            //dtComapny = cls.CompanyUserRole_GetByGroupId(pro);
            //if (dtComapny.Rows.Count > 0)
            //{

            //    CompanyUserRights.DataSource = dtComapny;
            //    CompanyUserRights.DataBind();

            //}

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPopup()", true);

        }

        protected void UserRightSubmitBtn_Click(object sender, EventArgs e)
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();

            DataTable dt = new DataTable();
            dt = cls.Get_MenuListByGroup(pro);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            int result;
            int resultRole;
            try
            {
                pro.GroupId = Convert.ToInt32(lblRoleId.Text);
                SqlCommand cmd = new SqlCommand("sp_Delete_RoleMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", Convert.ToInt32(pro.GroupId));

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                result = cmd.ExecuteNonQuery();
                cmd.Dispose();

                foreach (GridViewRow row in GridUserRightsList.Rows)
                {
                    HiddenField hdfMenuId = row.FindControl("hdfMenuId") as HiddenField;
                    HiddenField hdfSubMenuId = row.FindControl("hdfSubMenuId") as HiddenField;
                    CheckBox CheckView = row.FindControl("CheckView") as CheckBox;
                    CheckBox CheckEdit = row.FindControl("CheckEdit") as CheckBox;
                    CheckBox CheckDelete = row.FindControl("CheckDelete") as CheckBox;

                    SqlCommand cmdRole = new SqlCommand("SP_Role_Create", con);
                    cmdRole.CommandType = CommandType.StoredProcedure;
                    cmdRole.Parameters.AddWithValue("@GroupId", pro.GroupId);
                    cmdRole.Parameters.AddWithValue("@MenuId", hdfMenuId.Value);
                    cmdRole.Parameters.AddWithValue("@SubMenuId", hdfSubMenuId.Value);
                    cmdRole.Parameters.AddWithValue("@CanView", CheckView.Checked);
                    cmdRole.Parameters.AddWithValue("@CanEdit", CheckEdit.Checked);
                    cmdRole.Parameters.AddWithValue("@CanDelete", CheckDelete.Checked);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    resultRole = cmdRole.ExecuteNonQuery();
                    //cmd.ExecuteNonQuery();


                    //foreach (var objSubMenu in objSetMenuRolse.SubMenu)
                    //{
                    //    SqlCommand cmdRole = new SqlCommand("SP_Role_Create");
                    //    cmdRole.Parameters.AddWithValue("@GroupId", Convert.ToInt32(lblGroupId.Text));
                    //    cmdRole.Parameters.AddWithValue("@MenuId", hdfMenuId.Value);
                    //    cmdRole.Parameters.AddWithValue("@SubMenuId", hdfSubMenuId);
                    //    cmdRole.Parameters.AddWithValue("@CanView", CheckView.Checked);
                    //    cmdRole.Parameters.AddWithValue("@CanEdit", CheckEdit.Checked);
                    //    cmdRole.Parameters.AddWithValue("@CanDelete", CheckDelete.Checked);
                    //    if (con.State == ConnectionState.Closed)
                    //    {
                    //        con.Open();
                    //    }
                    //    result = cmd.ExecuteNonQuery();
                    //    cmd.Dispose();
                    //    cmd.ExecuteNonQuery();
                    //}
                    if (resultRole > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully !')", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "HidePopup1()", true);


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            GridGroupList();

        }



        protected void GridEdit_Click(object sender, EventArgs e)
        {            
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            pro.GroupId = Convert.ToInt32(GirdRoleMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblGroupId.Text = pro.GroupId.ToString();
            DataTable dt = new DataTable();
            dt = cls.Get_GroupNameFromRoleMaster(pro);
            if (dt.Rows.Count > 0)
            {
                RoleNametxt.Text = dt.Rows[0]["GroupName"].ToString();
                IsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                lblRoleId.Text = (dt.Rows[0]["GroupId"]).ToString();
                UpdateRoleBtn.Visible = true;
                AddRoleBtn.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found!')", true);

            }

            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "alertMessage", "ShowPopup1()", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowPopup1()", true);

        }

        protected void GridDelete_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            int GroupId = Convert.ToInt32(GirdRoleMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.GroupId = GroupId;

            int result = cls.Delete_GroupFromRoleMaster(pro);
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                ClearData();
                GridGroupList();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed!')", true);

            }
        }

        protected void UpdateRoleBtn_Click(object sender, EventArgs e)
        {
            pro.GroupId = Convert.ToInt32(lblRoleId.Text);
            pro.GroupName = RoleNametxt.Text;
            pro.IsActive = IsActive.Checked;
            int result = cls.Update_GroupNameFromRoleMaster(pro);
            if (result > 0)
            {
                ClearData();
                UpdateRoleBtn.Visible = false;
                AddRoleBtn.Visible = true;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "HidePopup1()", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                GridGroupList();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed')", true);

            }
        }

        protected void AddRoleModal_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void GridUserRightsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
           
        }
    }
}
