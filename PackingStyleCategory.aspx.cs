using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

namespace Production_Costing_Software
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        ClsPackingStyleCategoryMaster cls = new ClsPackingStyleCategoryMaster();
        ProPackingStyleCategoryMaster pro = new ProPackingStyleCategoryMaster();
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
                GridPackingStyleCategory();
                PackingCategoryDropdownCombo();
                EnumMasterMeasurementMeasurement();
                DisplayView();
            }
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"]!= null &&Session["UserName"].ToString().ToUpper() != "")
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[10]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[10]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[10]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[10]["CanEdit"]) == true)
            {
                if (lblStyleCategoryID.Text!="")
                {
                    AddStyleCategoryMaster.Visible = false;
                    CancelBtn.Visible = true;
                    EditStyleCategoryMaster.Visible = true;
                }
                else
                {
                    AddStyleCategoryMaster.Visible = true;
                    CancelBtn.Visible = true;
                    EditStyleCategoryMaster.Visible = false;
                }
              
            }
            else
            {
                AddStyleCategoryMaster.Visible = false;
                CancelBtn.Visible = false;
                EditStyleCategoryMaster.Visible = false;
            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[10]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[10]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[10]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[10]["CanEdit"]) == true)
            {
                if (lblStyleCategoryID.Text != "")
                {
                    AddStyleCategoryMaster.Visible = false;
                    CancelBtn.Visible = true;
                    EditStyleCategoryMaster.Visible = true;
                }
                else
                {
                    AddStyleCategoryMaster.Visible = true;
                    CancelBtn.Visible = true;
                    EditStyleCategoryMaster.Visible = false;
                }

            }
            else
            {
                AddStyleCategoryMaster.Visible = false;
                CancelBtn.Visible = false;
                EditStyleCategoryMaster.Visible = false;
            }
        }
        public void PackingCategoryDropdownCombo()
        {
            ClsPackingCategory cls = new ClsPackingCategory();
            ProPackingCategory pro = new ProPackingCategory();
            pro.User_Id = UserId;


            DataTable dt = new DataTable();

            dt = cls.GetPackingCategoryData(pro);
            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "PackingStyleCategoryName";
            PackingCategoryDropdownList.DataSource = dvOptions;

            PackingCategoryDropdownList.DataTextField = "PackingStyleCategoryName";
            PackingCategoryDropdownList.DataValueField = "PackingStyleCategoryName_Id";


            PackingCategoryDropdownList.DataBind();
            PackingCategoryDropdownList.Items.Insert(0, "Select");
        }
        public void EnumMasterMeasurementMeasurement()
        {
            ClsEnumMeasurementMaster cls = new ClsEnumMeasurementMaster();
            PackingStyleMeasurementDropdownCombo.DataSource = cls.GetEnumMasterMeasurement();

            PackingStyleMeasurementDropdownCombo.DataTextField = "EnumDescription";
            PackingStyleMeasurementDropdownCombo.DataValueField = "PkEnumId";


            PackingStyleMeasurementDropdownCombo.DataBind();
            PackingStyleMeasurementDropdownCombo.Items.Insert(0, "Select");

        }

        protected void AddStyleCategoryMaster_Click(object sender, EventArgs e)
        {
            ProPackingStyleCategoryMaster pro = new ProPackingStyleCategoryMaster();

            pro.PackingStyleCategoryNameMaster_Id = Convert.ToInt32(PackingCategoryDropdownList.SelectedValue);
            pro.PackingSize = decimal.Parse(PackingSizetxt.Text);
            pro.PackingMeasurement = Convert.ToInt32(PackingStyleMeasurementDropdownCombo.SelectedValue);
            pro.User_Id = UserId;
            int status = cls.Insert_PackingStyleCategoryName(pro);

            if (status > 0)
            {
                GridPackingStyleCategory();
                CleareData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }
        }
        public void GridPackingStyleCategory()
        {
            GridPackingStyleCategory_List.DataSource = cls.Get_PackingStyleCategoryName(UserId);
            GridPackingStyleCategory_List.DataBind();
        }

        protected void EditStyleCatBtn_Click(object sender, EventArgs e)
        {
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int PackingStyleCategory_Id = Convert.ToInt32(GridPackingStyleCategory_List.DataKeys[gdrow.RowIndex].Value.ToString());

            lblStyleCategoryID.Text = (PackingStyleCategory_Id).ToString();
            DataTable dt = new DataTable();
            dt = cls.Get_PackingStyleCategoryNameById(UserId, PackingStyleCategory_Id);

            PackingCategoryDropdownList.SelectedValue = Convert.ToInt32(dt.Rows[0]["Fk_PackingStyleCategoryName_Id"]).ToString();
            PackingSizetxt.Text = dt.Rows[0]["PackingSize"].ToString();
            //PackingStyleMeasurementDropdownCombo.SelectedItem.Text = dt.Rows[0]["PackingMeasurement"].ToString();
            PackingStyleMeasurementDropdownCombo.SelectedValue = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();
            EditStyleCategoryMaster.Visible = true;
            AddStyleCategoryMaster.Visible = false;
            CancelBtn.Visible = true;
            PackingCategoryDropdownList.Enabled = false;
            PackingCategorylabel.Visible = false;
        }

        protected void EditStyleCategoryMaster_Click(object sender, EventArgs e)
        {
            pro.PackingStyleCategoryNameMaster_Id = Convert.ToInt32(PackingCategoryDropdownList.SelectedValue);
            pro.User_Id = UserId;
            pro.PackingStyleCategory_Id = Convert.ToInt32(lblStyleCategoryID.Text);
            pro.PackingSize = decimal.Parse(PackingSizetxt.Text);
            pro.PackingMeasurement = Convert.ToInt32(PackingStyleMeasurementDropdownCombo.SelectedValue);
            int status = cls.Update_PackingStyleCategoryName(pro);
            if (status > 0)
            {
                GridPackingStyleCategory();
                CleareData();
                AddStyleCategoryMaster.Visible = true;
                EditStyleCategoryMaster.Visible = false;
                CancelBtn.Visible = true;
                PackingCategoryDropdownList.Enabled = true;
                PackingCategorylabel.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed!')", true);

            }

        }
        public void CleareData()
        {
            PackingCategoryDropdownList.SelectedIndex = -1;
            PackingStyleMeasurementDropdownCombo.SelectedIndex = -1;
            lblStyleCategoryID.Text = "";
            PackingSizetxt.Text = "";
            CancelBtn.Visible = true;

        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            CleareData();
            AddStyleCategoryMaster.Visible = true;
            EditStyleCategoryMaster.Visible = false;
            CancelBtn.Visible = true;

            lblStyleCategoryID.Text = "";
            PackingCategoryDropdownList.Enabled = true;
            PackingCategorylabel.Visible = true;

        }

        protected void GridPackingStyleCategory_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridPackingStyleCategory();

            GridPackingStyleCategory_List.PageIndex = e.NewPageIndex;
            GridPackingStyleCategory_List.DataBind();
        }
        [WebMethod]
        public static List<string> SearchBPMData(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_Search_PM_from_PackingStyleCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchPackingStyleCategory", prefixText);

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
            GridPackingStyleCategory();
            
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
                SqlCommand cmd = new SqlCommand("sp_Search_PM_from_PackingStyleCategory", con);
                cmd.Parameters.AddWithValue("@SearchPackingStyleCategory", TxtSearch.Text.Trim());


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

            GridPackingStyleCategory_List.DataSource = dt;
            GridPackingStyleCategory_List.DataBind();
        }

        protected void PackingStyleMeasurementDropdownCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            pro.PackingSize = decimal.Parse(PackingSizetxt.Text);
            pro.PackingMeasurement = Convert.ToInt32(PackingStyleMeasurementDropdownCombo.SelectedValue);
            pro.PackingStyleCategory_Id = Convert.ToInt32(PackingCategoryDropdownList.SelectedValue);
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_PackingStyleCategoryMaster(pro);
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple  Name !')", true);
                CleareData();
                return;
            }
        }
    }
}