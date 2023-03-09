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
    public partial class ProductCategoriesMaster : System.Web.UI.Page
    {
        int UserId;
        int Company_Id;
        
        ClsProductCategoryMaster cls = new ClsProductCategoryMaster();
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
                Grid_ProductCategoriesData();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[27]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[27]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[27]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                UpdateMainCategory.Visible = true;
                

            }
            else
            {
                UpdateMainCategory.Visible = false;
               
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[27]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[27]["CanEdit"]) == true)
            {
                UpdateMainCategory.Visible = true;
            }
            else
            {
                UpdateMainCategory.Visible = false;
            }


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
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void Grid_ProductCategoriesData()
        {
            Grid_ProductCategories.DataSource = cls.Get_ProductCategoryMasterAll();
            Grid_ProductCategories.DataBind();
        }
    }
}