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
    public partial class PackingStyleName : System.Web.UI.Page
    {
        ProPackingStyleNameMaster pro = new ProPackingStyleNameMaster();
        ClsPackingStyleNameMaster cls = new ClsPackingStyleNameMaster();
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
                GridPackingStyleNameMaster();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[16]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[16]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[16]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblPackingstyle_Id.Text != "")
                {
                    AddPSNMaster.Visible = false;
                    CancelBtn.Visible = true;
                    UpdatePSNMaster.Visible = true;
                }
                else
                {
                    AddPSNMaster.Visible = true;
                    CancelBtn.Visible = true;
                    UpdatePSNMaster.Visible = false;

                }

            }
            else
            {
                AddPSNMaster.Visible = false;
                CancelBtn.Visible = false;
                UpdatePSNMaster.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[16]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[16]["CanEdit"]) == true)
            {
                AddPSNMaster.Visible = true;
                CancelBtn.Visible = true;
            }
            else
            {
                AddPSNMaster.Visible = false;
                CancelBtn.Visible = false;

            }


        }

        public void GetLoginDetails()
        {
            if (Session["UserName"]!=null)
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
        protected void AddPSNMaster_Click(object sender, EventArgs e)
        {
            ProPackingStyleNameMaster pro = new ProPackingStyleNameMaster();

            pro.PackingStyleName = PackingStyleNametxt.Text;
            pro.User_Id = UserId;
            int status = cls.Insert_PackingStyleName(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                GridPackingStyleNameMaster();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }

        }
        public void GridPackingStyleNameMaster()
        {
            ClsPackingStyleNameMaster cls = new ClsPackingStyleNameMaster();
            GridPackingStyleNameMasterList.DataSource = cls.Get_PackingStyleNameAll(UserId);
            GridPackingStyleNameMasterList.DataBind();
        }
        public void ClearData()
        {
            PackingStyleNametxt.Text = "";
            lblPackingstyle_Id.Text = "";
            AddPSNMaster.Visible = true;
            CancelBtn.Visible = true;
            UpdatePSNMaster.Visible = false;
        }

        protected void DelPackingStyleBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PackingStyleName_Id = Convert.ToInt32(GridPackingStyleNameMasterList.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.Delete_PackingStyleName(PackingStyleName_Id, UserId);
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

                GridPackingStyleNameMaster();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Delete Failed')", true);

            }
        }

        protected void EditPackingBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int PackingStyleId = Convert.ToInt32(GridPackingStyleNameMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblPackingstyle_Id.Text = PackingStyleId.ToString();
            DataTable dt = new DataTable();
            GetUserRights();
            dt = cls.Get_PackingStyleNameById(UserId, PackingStyleId);



            PackingStyleNametxt.Text = dt.Rows[0]["PackingStyleName"].ToString();
            UpdatePSNMaster.Visible = true;
            //CancelBtn.Visible = true;
            AddPSNMaster.Visible = false;
        }

        protected void UpdatePSNMaster_Click(object sender, EventArgs e)
        {
            pro.PackingStyleName = PackingStyleNametxt.Text;
            pro.User_Id = UserId;
            pro.PackingStyleName_Id = Convert.ToInt32(lblPackingstyle_Id.Text);
            int status = cls.Update_PackingStyleName(pro);
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);

                GridPackingStyleNameMaster();
                ClearData();

            }
            UpdatePSNMaster.Visible = false;
            //CancelBtn.Visible = false;
            AddPSNMaster.Visible = true;

        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            UpdatePSNMaster.Visible = false;
            //CancelBtn.Visible = false;
            AddPSNMaster.Visible = true;
            lblPackingstyle_Id.Text = "";
            PackingStyleNametxt.Text = "";
        }

        protected void GridPackingStyleNameMasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridPackingStyleNameMaster();

            GridPackingStyleNameMasterList.PageIndex = e.NewPageIndex;
            GridPackingStyleNameMasterList.DataBind();
        }

        protected void PackingStyleNametxt_TextChanged(object sender, EventArgs e)
        {
            pro.PackingStyleName = PackingStyleNametxt.Text.Trim();
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_PackingStyleNameMaster(pro);
            if (dtCheck.Rows.Count>0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple [" + PackingStyleNametxt.Text + "] Name !')", true);
                ClearData();
                return;
            }
        }
    }
}