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
    public partial class MainCategory : System.Web.UI.Page
    {
        ProMainCategory pro = new ProMainCategory();
        ClsMainCategoryMaster cls = new ClsMainCategoryMaster();
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

                GirdMainCategoryListData();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[6]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[6]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[6]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[6]["CanEdit"]) == true)
            {
                MainCategorySub.Visible = true;
            }
            else
            {
                MainCategorySub.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[6]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[6]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[6]["CanEdit"]) == true)
            {
                MainCategorySub.Visible = true;
            }
            else
            {
                MainCategorySub.Visible = false;
            }
        }
        protected void MainCategorySub_Click(object sender, EventArgs e)
        {
            int status=0;
            pro.UpdatedBy=Convert.ToInt32(lblUserId.Text);
            if (MainCategorytxt.Text!="")
            {
                pro.MainCategoryName = MainCategorytxt.Text;
                 status = cls.InsertMainCategoryData(pro);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter Main Category!')", true);

            }

            if (status > 0)
            {
                GirdMainCategoryListData();
                MainCategorytxt.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            }

            //Response.Redirect(Request.Url.AbsoluteUri);


        }
        public void GirdMainCategoryListData()
        {
            GirdMainCategoryList.DataSource = cls.GetMainCategoryData();
            GirdMainCategoryList.DataBind();
        }


        protected void Edit_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int MainCategory_Id = Convert.ToInt32(GirdMainCategoryList.DataKeys[gdrow.RowIndex].Value.ToString());

            DataTable dt = new DataTable();
            pro.MainCategory_Id = MainCategory_Id;
           dt = cls.GetMainCategoryDataById(pro);
            UpdateMainCategory.Visible = true;
            MainCategorySub.Visible = false;
            try
            {

                lblMainCategory_Id.Text = dt.Rows[0]["PkMainCategory_Id"].ToString();
                MainCategorytxt.Text = dt.Rows[0]["MainCategory_Name"].ToString();
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void UpdateMainCategory_Click(object sender, EventArgs e)
        {
            pro.MainCategory_Id = Convert.ToInt32(lblMainCategory_Id.Text);
           
            pro.MainCategoryName = MainCategorytxt.Text;

            int status = cls.Update_MainCategoryData(pro);


            if (status > 0)
            {
                GirdMainCategoryListData();
                MainCategorytxt.Text = "";
                lblMainCategory_Id.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                UpdateMainCategory.Visible = false;
                MainCategorySub.Visible = true;
            }
        }
    }
}
