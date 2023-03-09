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
    public partial class RMCategory : System.Web.UI.Page
    {
        ProRMMaster pro = new ProRMMaster();
        ClsRMMaster cls = new ClsRMMaster();
        int User_Id;
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
                GridRMCategoryListData();
                DisplayView();
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {

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
                User_Id = Convert.ToInt32(Session["UserId"]);
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[7]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[7]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[7]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[7]["CanEdit"]) == true)
            {
                RMCategorySub.Visible = true;
            }
            else
            {
                RMCategorySub.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[7]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[7]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[7]["CanEdit"]) == true)
            {
                RMCategorySub.Visible = true;
            }
            else
            {
                RMCategorySub.Visible = false;
            }
        }

        protected void RMCategorySub_Click(object sender, EventArgs e)
        {
            //pro.RM_Category_Name = RMCategoryNametxt.Text;
            //int status = cls.(pro);

            //if (status > 0)
            //{
            //    GridRMCategoryListData();
            //    RMCategoryNametxt.Text = "";
            //}
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Can only Be Inserted To Database!')", true);
        }
        public void GridRMCategoryListData()
        {
            ClsRMCategoryMaster cls = new ClsRMCategoryMaster();
            GirdRMCategoryList.DataSource = cls.Get_RM_CategoryMaster();
            GirdRMCategoryList.DataBind();
        }
    }
}