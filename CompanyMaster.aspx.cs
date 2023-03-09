using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class CompanyMaster : System.Web.UI.Page
    {
        ProCompanyMaster pro = new ProCompanyMaster();
        ClsCompanyMaster cls = new ClsCompanyMaster();
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

                Grid_CompanyMaster();
                Grid_ImageUploadData();
                //EnumMasterMeasurementMeasurement();
                //RMCategoryDropdownListData();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[19]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[19]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[19]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblCompanyMaster_Id.Text!="")
                {
                    AddCompanyBtn.Visible = false;
                    UpdateCompanyBtn.Visible = true;
                }
                else
                {
                    AddCompanyBtn.Visible = true;
                    UpdateCompanyBtn.Visible = false;

                }

            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[19]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[19]["CanEdit"]) == true)
            {
                if (lblCompanyMaster_Id.Text != "")
                {
                    AddCompanyBtn.Visible = false;
                    UpdateCompanyBtn.Visible = true;
                }
                else
                {
                    AddCompanyBtn.Visible = true;
                    UpdateCompanyBtn.Visible = false;

                }

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
                UserId= Convert.ToInt32(Session["UserId"].ToString());
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }

        public void Grid_ImageUploadData()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/"));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }

            //Grid_ImageUpload.DataSource = cls.Get_CompanyMasterData();
            //Grid_ImageUpload.DataBind();
        }
        protected void AddCompanyBtn_Click(object sender, EventArgs e)
        {

            pro.CompanyMaster_Name = CompanyNametxt.Text;
            pro.UserId = UserId;
            int status = cls.Insert_CompanyMasterData(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_CompanyMaster();
                ClearData();
                UpdateCompanyBtn.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
        }
        public void Grid_CompanyMaster()
        {

            Grid_CompanyMasterList.DataSource = cls.Get_CompanyMasterData();
            Grid_CompanyMasterList.DataBind();
        }




        protected void CancelcompanyBtn_Click(object sender, EventArgs e)
        {
            CompanyNametxt.Text = "";
            UpdateCompanyBtn.Visible = false;
            AddCompanyBtn.Visible = true;
        }

        protected void DelGridBtn_Click(object sender, EventArgs e)
        {
            Button Del = sender as Button;
            GridViewRow gdrow = Del.NamingContainer as GridViewRow;
            int CompanyMaster_Id = Convert.ToInt32(Grid_CompanyMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.CompanyMaster_Id = CompanyMaster_Id;
            pro.UserId = UserId;
            DataTable dt = new DataTable();
            int status = cls.Delete_CompanyMasterData(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                Grid_CompanyMaster();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed')", true);

            }
        }

        protected void EditGridBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int CompanyMaster_Id = Convert.ToInt32(Grid_CompanyMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.CompanyMaster_Id = CompanyMaster_Id;
            lblCompanyMaster_Id.Text = CompanyMaster_Id.ToString();
            DataTable dt = new DataTable();

            dt = cls.Get_CompanyMasterBy_Id(pro);
            if (lblCompanyMaster_Id.Text != "")
            {
                AddCompanyBtn.Visible = false;
                UpdateCompanyBtn.Visible = true;
            }
            else
            {
                AddCompanyBtn.Visible = true;
                UpdateCompanyBtn.Visible = false;

            }
            try
            {
                //lblCompanyMaster_Id.Text = dt.Rows[0]["CompanyMaster_Id"].ToString();
                CompanyNametxt.Text = dt.Rows[0]["CompanyMaster_Name"].ToString();


            }
         
            catch (Exception)
            {

                throw;
            }
         
        }

        protected void UpdateCompanyBtn_Click(object sender, EventArgs e)
        {
            pro.CompanyMaster_Name = CompanyNametxt.Text;
            pro.CompanyMaster_Id = Convert.ToInt32(lblCompanyMaster_Id.Text);
            pro.UserId = UserId;
            int status = cls.Update_CompanyMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_CompanyMaster();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Failed')", true);

            }
        }
        public void ClearData()
        {
            lblCompanyMaster_Id.Text = "";
            CompanyNametxt.Text = "";
            UpdateCompanyBtn.Visible = false;
            AddCompanyBtn.Visible = true;
        }

        //protected void ImageUpload_Click(object sender, EventArgs e)
        //{
        //    string fileName = Path.GetFileName(LogoUploadId.PostedFile.FileName);
        //    LogoUploadId.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
        //    Response.Redirect(Request.Url.AbsoluteUri);

        //}

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}