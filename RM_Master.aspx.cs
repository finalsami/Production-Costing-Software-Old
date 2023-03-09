using BusinessAccessLayer;
using DataAccessLayer;
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

namespace Production_Costing_Software
{
    public partial class RM_Master : System.Web.UI.Page
    {
        ClsRMMaster cls = new ClsRMMaster();
        ProRMMaster pro = new ProRMMaster();
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
                GridRM_MasterListData();
                EnumMasterMeasurementMeasurement();
                RMCategoryDropdownListData();
                DisplayView();
            }

        }
        public void RMCategoryDropdownListData()
        {
            ClsRMCategoryMaster clsRMCategory = new ClsRMCategoryMaster();
            RMCategoryDropdownList.DataSource = clsRMCategory.Get_RM_CategoryMaster();

            RMCategoryDropdownList.DataTextField = "RM_Category_Name";
            RMCategoryDropdownList.DataValueField = "RM_Category_id";
            RMCategoryDropdownList.DataBind();
            RMCategoryDropdownList.Items.Insert(0, "Select");
        }
        public void EnumMasterMeasurementMeasurement()
        {
            UnitDropdownList.DataSource = cls.GetEnumMasterMeasurement();

            UnitDropdownList.DataTextField = "EnumDescription";
            UnitDropdownList.DataValueField = "PkEnumId";
            UnitDropdownList.DataBind();
            UnitDropdownList.Items.Insert(0, "Select");

        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().ToUpper() != "")
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[11]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[11]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[11]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[11]["CanEdit"]) == true)
            {
                if (lblRM_Id.Text != "")
                {
                    AddRmMaster.Visible = false;
                    Cancel.Visible = true;
                    EditRM.Visible = true;

                }
                else
                {
                    AddRmMaster.Visible = true;
                    Cancel.Visible = true;
                    EditRM.Visible = false;
                }
            }
            else
            {
                AddRmMaster.Visible = false;
                Cancel.Visible = false;
                EditRM.Visible = false;
            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[11]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[11]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[11]["CanEdit"]) == true)

            {
                if (lblRM_Id.Text != "")
                {
                    AddRmMaster.Visible = false;
                    Cancel.Visible = true;
                    EditRM.Visible = true;

                }
                else
                {
                    AddRmMaster.Visible = true;
                    Cancel.Visible = true;
                    EditRM.Visible = false;
                }
            }
            else
            {
                AddRmMaster.Visible = false;
                Cancel.Visible = false;
                EditRM.Visible = false;
            }
        }
        //protected void RMCategoryDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        //{



        //    int RM_Cat_ID = Convert.ToInt32(RMCategoryDropdownList.SelectedIndex);
        //    if (RMCategoryDropdownList.SelectedIndex == 0)
        //    {
        //        GridRM_MasterList.DataSource = cls.Get_RM_MasterDataAll(UserId);
        //        GridRM_MasterList.DataBind();
        //    }
        //    else
        //    {
        //        GridRM_MasterList.DataSource = cls.Get_RM_MasterByCategoryId(UserId,RM_Cat_ID);
        //        GridRM_MasterList.DataBind();
        //    }

        //}
        protected void AddRmMaster_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                pro.RM_Category_ID = Convert.ToInt32(RMCategoryDropdownList.SelectedValue);
                pro.RM_Name = RmNametxt.Text;
                pro.User_Id = UserId;
                if (IsPurityRequired.Checked == true)
                {
                    pro.RM_IsPurityRequired = true;
                }
                else
                {
                    pro.RM_IsPurityRequired = false;
                }

                pro.EnumMaster_UnitMeasurement = Convert.ToInt32(UnitDropdownList.SelectedValue);


                int status = cls.Insert_RM_MasterData(pro);
                if (status > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    GridRM_MasterListData();
                    ClearData();
                }

            }
        }
        public void ClearData()
        {
            RMCategoryDropdownList.SelectedIndex = 0;
            UnitDropdownList.SelectedIndex = 0;
            RmNametxt.Text = "";
            IsPurityRequired.Checked = false;
            AddRmMaster.Visible = true;
            EditRM.Visible = false;

        }
        public void GridRM_MasterListData()
        {
            GridRM_MasterList.DataSource = cls.Get_RM_MasterDataAll(UserId);
            GridRM_MasterList.DataBind();
        }

        protected void EditRMBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int RM_Id = Convert.ToInt32(GridRM_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblRM_Id.Text = RM_Id.ToString();
            DataTable dt = new DataTable();

            dt = cls.Get_RM_MasterBy_Id(RM_Id, UserId);
            if (lblRM_Id.Text != "")
            {
                AddRmMaster.Visible = false;
                EditRM.Visible = true;
            }
            else
            {
                AddRmMaster.Visible = true;
                EditRM.Visible = false;
            }
            try
            {
                lblRM_Id.Text = dt.Rows[0]["RM_Id"].ToString();
                lblCategory_Id.Text = dt.Rows[0]["Category_Id"].ToString();
                RmNametxt.Text = dt.Rows[0]["RM_Name"].ToString();
                RMCategoryDropdownList.SelectedValue = dt.Rows[0]["Category_Id"].ToString();
                UnitDropdownList.SelectedValue = dt.Rows[0]["Unit_Id"].ToString();
                IsPurityRequired.Checked = Convert.ToBoolean(dt.Rows[0]["IsPurityRequired"]);
                //if (Yes == 1)
                //{
                //    IsPurityRequired.SelectedIndex = 0;
                //}
                //else
                //{
                //    IsPurityRequired.SelectedIndex = 1;
                //}
                AddRmMaster.Visible = false;

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void DelRMBtn_Click(object sender, EventArgs e)
        {
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int RM_Id = Convert.ToInt32(GridRM_MasterList.DataKeys[gdrow.RowIndex].Value.ToString());

            int status = cls.Delete_RM_Master(RM_Id, UserId);
            if (status > 0)
            {
                GridRM_MasterListData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted ')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Deleted May Be Used in Another Data! ')", true);

            }
        }

        protected void EditRM_Click(object sender, EventArgs e)
        {

            pro.RM_Id = Convert.ToInt32(lblRM_Id.Text);
            //pro.BPM_Product_Id = Convert.ToInt32(lblBPM_Pro_Id.Text);
            pro.RM_Name = RmNametxt.Text;
            pro.RM_Category_ID = Convert.ToInt32(lblCategory_Id.Text);
            //pro.RM_Category_ID = Convert.ToInt32(RMCategoryDropdownList.SelectedValue);
            pro.EnumMaster_UnitMeasurement = Convert.ToInt32(UnitDropdownList.SelectedValue);
            pro.User_Id = UserId;
            //string purity = (IsPurityRequired.Checked);

            if (IsPurityRequired.Checked == true)
            {
                pro.RM_IsPurityRequired = true;
            }
            else
            {
                pro.RM_IsPurityRequired = false;
            }
            int status = cls.Update_RM_MasterData(pro);


            if (status > 0)
            {
                GridRM_MasterListData();
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed!')", true);

            }

            lblRM_Id.Text = "";
            //lblIm_Id.Text = "";
            RMCategoryDropdownList.SelectedValue = "1";
            RmNametxt.Text = "";
            IsPurityRequired.Checked = false;
            AddRmMaster.Visible = true;
            EditRM.Visible = false;
        }

        protected void GridRM_MasterList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int inId = Convert.ToInt32(GridRM_MasterList.DataKeys[e.RowIndex].Value.ToString());
            int status = cls.Delete_RM_Master(inId, UserId);
            if (status > 0)
            {
                GridRM_MasterListData();
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            lblRM_Id.Text = "";
            //lblIm_Id.Text = "";
            RMCategoryDropdownList.SelectedIndex = 0;
            UnitDropdownList.SelectedIndex = 0;
            RmNametxt.Text = "";
            IsPurityRequired.Checked = false;
            AddRmMaster.Visible = true;
            EditRM.Visible = false;
        }

        protected void GridRM_MasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRM_MasterListData();

            GridRM_MasterList.PageIndex = e.NewPageIndex;
            GridRM_MasterList.DataBind();
        }

        protected void RmNametxt_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string RM_Name = RmNametxt.Text;
            int RM_Category_Id = Convert.ToInt32(RMCategoryDropdownList.SelectedValue);
            dt = cls.Get_CheckRM_MasterBy_Id(RM_Name, RM_Category_Id);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple RM !')", true);
                ClearData();
                return;
            }
        }

        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_RM_from_RM_Master";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchRM_Name", prefixText);

                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            customers.Add(sdr["RM_Name"].ToString());

                        }
                    }

                    conn.Close();

                    return customers;
                }
            }
        }
        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void CancelSearch_Click(object sender, EventArgs e)
        {
            TxtSearch.Text = "";
            GridRM_MasterListData();

        }

        protected void SearchId_Click(object sender, EventArgs e)
        {

        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Search_RM_from_RM_Master", con);
                cmd.Parameters.AddWithValue("@SearchRM_Name", TxtSearch.Text.Trim());


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

            GridRM_MasterList.DataSource = dt;
            GridRM_MasterList.DataBind();
        }
    }
}
