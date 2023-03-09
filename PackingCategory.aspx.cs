using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class PackingCategory : System.Web.UI.Page
    {
        int User_Id;
        ProPackingCategory pro = new ProPackingCategory();
        ClsPackingCategory cls = new ClsPackingCategory();
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
                Grid_PackingCategoryData();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[9]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[9]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[9]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[9]["CanEdit"]) == true)
            {
                if (lblPackingStyleCategore_Id.Text != "")
                {
                    UpdatePackingCategory.Visible = true;
                    PackingCategorySub.Visible = false;
                }
                else
                {
                    UpdatePackingCategory.Visible = false;
                    PackingCategorySub.Visible = true;
                }

            }
            else
            {
                PackingCategorySub.Visible = false;


            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[9]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[9]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[9]["CanEdit"]) == true)
            {
                PackingCategorySub.Visible = true;
            }
            else
            {
                PackingCategorySub.Visible = false;
            }
        }
        protected void PackingCategorySub_Click(object sender, EventArgs e)
        {
            pro.PackingCategoryName = PackingCategorytxt.Text;


            pro.User_Id = User_Id;

            int status = cls.InsertPackingCategoryData(pro);

            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_PackingCategoryData();
                ClearData();
            }
        }
        public void Grid_PackingCategoryData()
        {
            ProPackingCategory pro = new ProPackingCategory();
            pro.User_Id = User_Id;
            GirdPackingCategoryList.DataSource = cls.GetPackingCategoryData(pro);
            GirdPackingCategoryList.DataBind();
        }

        protected void DelPackingCategoryBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int PackingCategory_Id = Convert.ToInt32(GirdPackingCategoryList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.PackingCategory_Id = PackingCategory_Id;
            pro.User_Id = User_Id;
            int status = cls.Delete_PackingCategory(pro);
            if (status > 0)
            {
                Grid_PackingCategoryData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed!')", true);

            }
        }

        protected void EditPackingCategoryBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int PackingStyleCategoryName_Id = Convert.ToInt32(GirdPackingCategoryList.DataKeys[gdrow.RowIndex].Value.ToString());

            DataTable dt = new DataTable();

            dt = cls.Get_PackingCategoryById(PackingStyleCategoryName_Id, User_Id);

            try
            {
                PackingCategorytxt.Text = dt.Rows[0]["PackingStyleCategoryName"].ToString();
                lblPackingStyleCategore_Id.Text = dt.Rows[0]["PackingStyleCategoryName_Id"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
            UpdatePackingCategory.Visible = true;
            PackingCategorySub.Visible = false;
        }

        protected void UpdatePackingCategory_Click(object sender, EventArgs e)
        {
            pro.PackingCategoryName = PackingCategorytxt.Text;
            pro.User_Id = User_Id;

            pro.PackingCategory_Id = Convert.ToInt32(lblPackingStyleCategore_Id.Text);

            int status = cls.Update_PackingCategory(pro);

            if (status > 0)
            {
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_PackingCategoryData();
                UpdatePackingCategory.Visible = false;
                PackingCategorySub.Visible = true;

            }
        }
        public void ClearData()
        {
            lblPackingStyleCategore_Id.Text = "";
            PackingCategorytxt.Text = "";
        }

        protected void GirdPackingCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_PackingCategoryData();

            GirdPackingCategoryList.PageIndex = e.NewPageIndex;
            GirdPackingCategoryList.DataBind();
        }
        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_PM_from_PackingCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchPackingStyleCategoryName", prefixText);

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
            TxtSearch.Text = TxtSearch.Text.Substring(0, 3);
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            Grid_PackingCategoryData();
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
                SqlCommand cmd = new SqlCommand("sp_Search_PM_from_PackingCategory", con);
                cmd.Parameters.AddWithValue("@SearchPackingStyleCategoryName", TxtSearch.Text.Trim());


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

            GirdPackingCategoryList.DataSource = dt;
            GirdPackingCategoryList.DataBind();
        }

        protected void PackingCategorytxt_TextChanged(object sender, EventArgs e)
        {

            pro.PackingCategoryName =(PackingCategorytxt.Text.Trim());
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_PackingCategory(pro);
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple [" + PackingCategorytxt.Text + "] Name !')", true);
                ClearData();
                return;
            }
        }
    }
}