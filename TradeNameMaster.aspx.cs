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
    public partial class TradeNameMaster : System.Web.UI.Page
    {
        ClsTradeNameMaster cls = new ClsTradeNameMaster();
        ProTradeNameMaster pro = new ProTradeNameMaster();
        int User_Id;
        int Company_Id;

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
                GridTradeNameListData();
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[26]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[26]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[26]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
                if (lblTradeNameMaster_Id.Text != "")
                {
                    AddTradeName.Visible = false;
                    CancelTradeName.Visible = true;
                    UpdateTradeName.Visible = true;
                }
                else
                {
                    AddTradeName.Visible = true;
                    CancelTradeName.Visible = true;
                    UpdateTradeName.Visible = false;
                }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[26]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[26]["CanEdit"]) == true)
            {
                if (lblTradeNameMaster_Id.Text!="")
                {
                    AddTradeName.Visible = false;
                    CancelTradeName.Visible = true;
                    UpdateTradeName.Visible = true;
                }
                else
                {
                    AddTradeName.Visible = true;
                    CancelTradeName.Visible = true;
                    UpdateTradeName.Visible = false;
                }
               

            }
            else
            {
                AddTradeName.Visible = false;
                CancelTradeName.Visible = false;

            }


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
                User_Id = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void GridTradeNameListData()
        {
            ProTradeNameMaster pro = new ProTradeNameMaster();
            pro.Comapny_Id = Company_Id;
            GridTradeNameList.DataSource = cls.Get_TradeNameMasterAll(pro);
            GridTradeNameList.DataBind();
        }
        protected void UpdateTradeName_Click(object sender, EventArgs e)
        {
            pro.TradeName_Id = Convert.ToInt32(lblTradeNameMaster_Id.Text);
            pro.TradeName = TradeNametxt.Text;
            pro.Comapny_Id = Company_Id;

            int status = cls.Update_TradeNameMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                GridTradeNameListData();
                ClearData();
            }
        }

        protected void AddTradeName_Click(object sender, EventArgs e)
        {
            pro.TradeName = TradeNametxt.Text;
            pro.Comapny_Id = Company_Id;
            int status = cls.Insert_TradeNameMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                GridTradeNameListData();
                ClearData();
            }
        }
        public void ClearData()
        {
            TradeNametxt.Text = "";
            UpdateTradeName.Visible = false;
            AddTradeName.Visible = true;
            lblTradeNameMaster_Id.Text = "";
        }



        protected void Edit_Click1(object sender, EventArgs e)
        {
            Console.WriteLine(e);
            Console.WriteLine("AAa");
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int TradeName_Id = Convert.ToInt32(GridTradeNameList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.TradeName_Id = TradeName_Id;

            lblTradeNameMaster_Id.Text = TradeName_Id.ToString();
            DataTable dt = new DataTable();
            dt = cls.Get_TradeNameMasterBy_Id(pro);


            lblTradeNameMaster_Id.Text = dt.Rows[0]["TradeName_Id"].ToString();
            TradeNametxt.Text = dt.Rows[0]["TradeName"].ToString();

        }

        protected void Delete_Click1(object sender, EventArgs e)
        {
            Console.WriteLine(e);
            Console.WriteLine("AAa");
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            int TradeName_Id = Convert.ToInt32(GridTradeNameList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.TradeName_Id = TradeName_Id;
            pro.Comapny_Id = Company_Id;
            int status = cls.Delete_TradeNameMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                GridTradeNameListData();
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Trade Name Used Somewhere Can Not Delete!')", true);
                return;
            }
        }

        protected void CancelTradeName_Click(object sender, EventArgs e)
        {
            TradeNametxt.Text = "";
            UpdateTradeName.Visible = false;
            AddTradeName.Visible = true;
            lblTradeNameMaster_Id.Text = "";
        }

        protected void TradeNametxt_TextChanged(object sender, EventArgs e)
        {

            pro.TradeName = TradeNametxt.Text.Trim();
           
            DataTable dtCheck = new DataTable();
            dtCheck = cls.CHECK_TradeNameMaster(pro);
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple [" + TradeNametxt.Text + "] Name !')", true);
                ClearData();
                return;
            }
        }
    }
}